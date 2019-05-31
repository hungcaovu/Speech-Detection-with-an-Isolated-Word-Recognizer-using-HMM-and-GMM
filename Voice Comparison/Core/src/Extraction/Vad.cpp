#include <Extraction\Vad.h>
#include <Util\LogUtil.h>
#include <algorithm>
#include <exception>
Vad::Voice::Voice(COUNT_TYPE start, COUNT_TYPE end){
	this->start = start;
	this->end = end;
}

Vad::Voice::Voice(){
	this->start = 0;
	this->end = 0;
}

Vad::Vad(WavFile *wav){
	_valid = false;
	_wav = wav;
	_energy = NULL;
	_pitch = NULL;
	_zeroCrossing = NULL;
	if (_wav != NULL && _wav->IsValidFile()){
		_valid = true;
	}
}



void Vad::SegmentE(DATA_TYPE threshold){
	_voiceSegment.clear();
	// Segment Voice
	SIGNED_TYPE size = (SIGNED_TYPE)_energies.size();
	if (size > 0){
		DATA_TYPE min = *std::min_element(_energies.begin(), _energies.end());
		DATA_TYPE max = *std::max_element(_energies.begin(), _energies.end());

		_energyThreshold = min + (max - min)*(threshold);

		for (SIGNED_TYPE i = 0; i < size; i++){
			DATA_TYPE data = _energies[i];
			if (data > _energyThreshold){
				Voice v;
				v.start = _posEnergies[i];
				PRINT(DATA, "Threshold Energy [%d] = %2.7f %2.7f\n", i, data, _energyThreshold);

				for (i = i + 1; i < size - 1; i++){
					data = _energies[i];
					
					if (data < _energyThreshold){
						v.end = _posEnergies[i + 1];
						PRINT(DATA, " Energy [%d] = %2.7f %5.7f\n", i, data, _energyThreshold);
						break;
					}
				}
				if (i > size - 2){
					v.end = _posEnergies.back();
				}
				_voiceSegment.push_back(v);
			}
		}

		//
		PRINT(DATA, "Energy Detect VAD: %2.7f", _energyThreshold);
		for (SIGNED_TYPE i = 0; i < size; i++){
			PRINT(DATA, " Energy [%d] = %2.7f\n", i, _energies[i]);
		}
	}
}

//void Vad::SegmentZ(DATA_TYPE threshold){
//	SIGNED_TYPE size = (SIGNED_TYPE)_zeroRate.Size();
//	for (SIGNED_TYPE i = 0; i < size; i++){
//		COUNT_TYPE data = _zeroRate[i];
//		if (data < threshold){
//				Voice v;
//				v.start = _posZeroRate[i];
//				PRINT(DATA, "Threshold Energy [%d] = %2.7f %2.7f\n", i, data, threshold);
//
//				for (i = i + 1; i < size - 1; i++){
//					data = _zeroRate[i];
//					
//					if (data < threshold){
//						v.end = _posZeroRate[i + 1];
//						PRINT(DATA, " Energy [%d] = %2.7f %5.7f\n", i, data, threshold);
//						break;
//					}
//				}
//				if (i > size - 2){
//					v.end = _posZeroRate.PopBack();
//				}
//				_voiceSegment.push_back(v);
//			}
//		}
//}

void Vad::Process(DATA_TYPE threshold){
	try{
		SegmentE(threshold);
	}
	catch (std::exception ex){
		_valid = false;
	}
}

void Vad::UseEnergy(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool normalizeEnergy, SIGNED_TYPE windowSmooth, bool logEnergy){
	if (_valid){
		_energy = new Energy(_wav, timeFrame, timeShift, normalizeEnergy, logEnergy);
		_energy->SetWindowSizeMedianFilter(windowSmooth);
		_energy->Process();
		if (_energy->IsValid()){
			_energies = _energy->SmoothEnergies();
			_posEnergies = _energy->PosEnergies();
		}
	}
}
void Vad::UsePitch(DATA_TYPE timeFrame, DATA_TYPE timeShift, DATA_TYPE minF0, DATA_TYPE maxF0, PITCH_TYPE pitchType ){
	if (_valid){
		_pitch = new PitchExtraction(_wav, timeFrame, timeShift, minF0, maxF0, pitchType);
	}
}
void Vad::UseZeroRate(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool shiftZero){
	if (_valid){
		_zeroCrossing = new ZeroCrossing(_wav, timeFrame, timeShift, shiftZero);
		_zeroCrossing->Process();
		if (_zeroCrossing->IsValid()){
			_zeroRate = _zeroCrossing->ZeroRate();
			_posZeroRate = _zeroCrossing->PosZeroRate();
		}
	}
}

COUNT_TYPE Vad::NumberOfVoiceSegment(){
	return _voiceSegment.size();
}
COUNT_TYPE Vad::StartVoice(COUNT_TYPE count){
	return _voiceSegment[count].start;
}
COUNT_TYPE Vad::EndVoice(COUNT_TYPE count){
	return _voiceSegment[count].end;
}
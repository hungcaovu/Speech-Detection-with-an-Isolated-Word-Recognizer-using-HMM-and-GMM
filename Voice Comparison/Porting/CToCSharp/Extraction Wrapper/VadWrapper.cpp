#include <VadWrapper.h>

namespace ExtractionWrapper{
	VadWrapper::VadWrapper(WavFileWrapper ^wav){
		_wav = wav;
		_valid = false;
		if (_wav != nullptr && _wav->IsValid){
			_vad = new Vad(_wav->GetWavFile());
			_valid = _vad->IsValid();
		}
	}
	COUNT_TYPE VadWrapper::GetSizeOfSegment(){
		if (_valid){
			return _vad->NumberOfVoiceSegment();
		}
		return 0;
	}
	COUNT_TYPE VadWrapper::GetStartSegment(COUNT_TYPE idx){
		if (_valid){
			return _vad->StartVoice(idx);
		}
		return 0;
	}
	COUNT_TYPE VadWrapper::GetEndSegment(COUNT_TYPE idx){
		if (_valid){
			return _vad->EndVoice(idx);
		}
		return 0;
	}

	void VadWrapper::UseEnergy(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool normalizeEnergy, SIGNED_TYPE windowSmooth, bool logEnergy){
		if (_valid){
			_vad->UseEnergy(timeFrame, timeShift, normalizeEnergy, windowSmooth, logEnergy);
		}
	}

	void VadWrapper::UseZeroRate(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool shiftZero){
		if (_valid){
			_vad->UseZeroRate(timeFrame, timeShift, shiftZero);
		}
	}
	void VadWrapper::UsePitch(DATA_TYPE timeFrame, DATA_TYPE timeShift, DATA_TYPE minF0, DATA_TYPE maxF0, PITCH_TYPE pitchType){
		if (_valid){
			_vad->UsePitch(timeFrame, timeShift, minF0, maxF0, pitchType);
		}
	}

	bool VadWrapper::Process(DATA_TYPE threshold){
		_vad->Process(threshold);
		_thresholdEnergy = _vad->GetThresholdEnergy();
		_engery = UtilWrapper::ConverVectorToList(_vad->GetSmoothEnergies());
		_valid = _vad->IsValid();
		return _valid;
	}
};
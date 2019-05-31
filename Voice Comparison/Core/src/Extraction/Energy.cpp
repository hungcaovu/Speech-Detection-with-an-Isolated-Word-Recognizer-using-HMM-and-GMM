#include "Util\MedianFilter.h"
#include "Util\LogUtil.h"
#include "Util\MathUtil.h"
#include "Extraction\Energy.h"

#include "Extraction\ExtractionMFCC.h"

Energy::Energy(WavFile *wav, DATA_TYPE timeframe, DATA_TYPE timeshift, bool normalizeEnergy, bool logEnergy) : _medianWindowSizeFilter(3){
	valid = false;
	_wav = wav;
	_logEnergy = logEnergy;
	_normalizeEnergy = normalizeEnergy;
	if (_wav != NULL){
		frameSize = (COUNT_TYPE)(timeframe * wav->GetSampleRate());
		overlap = frameSize - (COUNT_TYPE)(timeshift * wav->GetSampleRate());
		data = _wav->GetSelectedData();
		valid = true;
	}
}

void Energy::Process(){

	COUNT_TYPE length = _wav->GetSelectedLength();

	for (COUNT_TYPE blockStart = 0; blockStart < length - frameSize; blockStart += frameSize - overlap)
	{
		COUNT_TYPE numSamplesInBlock = frameSize;

		if (blockStart + frameSize > length)
			numSamplesInBlock = length - blockStart;

		if (numSamplesInBlock)
		{
			DATA_TYPE *frame = ArrayUtil::CreateZeroArray1D(frameSize);
			memcpy(frame, &data[blockStart], numSamplesInBlock * sizeof(DATA_TYPE));
			_energy.push_back(EnergyFrame(frame));
			_posEnergy.push_back(blockStart);
			delete[]frame;
		}
	}

	DSP::MedianFilter1D<DATA_TYPE> median;
	median.SetWindowSize(_medianWindowSizeFilter);
	_smoothEnergy = median.Filter(_energy);
}
void Energy::SetWindowSizeMedianFilter(int windowSize){
	_medianWindowSizeFilter = windowSize;
}
std::vector<DATA_TYPE> Energy::SmoothEnergies(){
	return _smoothEnergy;
}
std::vector<DATA_TYPE> Energy::Energies(){
	return _energy;
}
std::vector<COUNT_TYPE> Energy::PosEnergies(){
	return _posEnergy;
}

DATA_TYPE Energy::EnergyFrame(DATA_TYPE* cur_frame){
	DATA_TYPE energy = 0.0;
	if (frameSize > 0.0){
		for (COUNT_TYPE counter = 0; counter < frameSize; ++counter)
			energy = energy + cur_frame[counter] * cur_frame[counter];

		if (_normalizeEnergy){
			energy = energy / (DATA_TYPE)frameSize;
		}

		if (_logEnergy){
			if (energy > 0){
				energy = 10 * log10(energy);
			}
			else {
				energy = 0;
			}
		}
	}
	return energy;
}
bool Energy::IsValid(){
	return valid;
}

DATA_TYPE MFCC::Energy(Vector &frame){
	DATA_TYPE energy = 0.0;
	COUNT_TYPE size = frame.Size();
	if (size > 0.0){
		for (COUNT_TYPE counter = 0; counter < size; ++counter){
			energy = energy + frame[counter] * frame[counter];
		}
		//energy = energy / (DATA_TYPE)size;
		if (energy > 0){
			energy = 10 * log10(energy);
		}
		else {
			energy = 0;
		}
	}
	return energy;
}

void MFCC::NormalizeEnergy(DATA_TYPE normal){
	/*if (normal > 0){
		DATA_TYPE max = 0.0;
		DATA_TYPE avg = 0.0;
		for (COUNT_TYPE i = 0; i < mfccSpectrum.size(); i++){
			if (abs(mfccSpectrum[i][nceptrums - 1]) > max){
				max = abs(mfccSpectrum[i][nceptrums - 1]);
				avg += mfccSpectrum[i][nceptrums - 1];
			}
		}
		avg /= (DATA_TYPE)mfccSpectrum.size();

		DATA_TYPE per1 = (max - abs(avg)) / normal;

		for (COUNT_TYPE i = 0; i < mfccSpectrum.size(); i++){
			mfccSpectrum[i][nceptrums - 1] = (mfccSpectrum[i][nceptrums - 1] - avg) / per1;
		}
	}*/
}
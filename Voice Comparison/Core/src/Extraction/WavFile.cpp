#include "Extraction\WavFile.h"
#include "Util\MathUtil.h"
#include "Util\LogUtil.h"

WavFile::WavFile(){
	selectedTimeLength = fullTimeLength = -1.0;
}

WavFile::WavFile(char * path){
	this->path = path;
	selectedTimeLength = fullTimeLength = -1.0;
}

bool WavFile::Load(char* p){
	if (p != NULL){
		this->path = p;
	}
	icstdsp::AudioFile audio;
	if (path != NULL && audio.Load(this->path, 0, 0, false) == 0){
		fullLength = audio.GetSize();

		float *data = audio.GetSafePt(0, true);
		fullData = ArrayUtil::CreateZeroArray1D(fullLength);//new DATA_TYPE[lengthFull];


		for (COUNT_TYPE i = 0; i < fullLength; i++){
			fullData[i] = (DATA_TYPE)data[i];
		}

		selectedData = fullData;// Default data chan is data full
		selectedLength = fullLength;
		selectedWave = true;
		sampleRate = audio.GetRate();
		selectedTimeLength = fullTimeLength = (TIME_TYPE)fullLength / sampleRate;
		validFile = true;
#ifdef _PRINT_DEBUG
		PRINT(DATA, "Load File");
		for (COUNT_TYPE i = 0; i < fullLength; i++){
			PRINT(DATA, ": %d - %2.6f ", i, fullData[i]);
		}
#endif
	}
	else{
		validFile = false;
		selectedWave = false;
	}
	return validFile;
}

DATA_TYPE WavFile::Peak(){

	DATA_TYPE max = -1.0;
	for (COUNT_TYPE n = 1; n < fullLength; n++) {
		if (max < abs(fullData[n]))
			max = abs(fullData[n]);
	}

	return max;
}

bool WavFile::NormalizeWave(DATA_TYPE peak){
	DATA_TYPE max = Peak();
	if (max <= 0.0) return false;

	DATA_TYPE ratio = peak / max;
	for (COUNT_TYPE n = 1; n < fullLength; n++) {
		fullData[n] *= ratio;
	}
	return true;
}
bool WavFile::SelectedWave(COUNT_TYPE startPos, COUNT_TYPE endPos){
	if (validFile) {
		selectedWave = true;

		if (endPos != 0){
			this->selectedData = &fullData[startPos];
			this->selectedLength = endPos - startPos;
			this->selectedTimeLength = (TIME_TYPE)selectedLength / sampleRate;
		}
		else {
			// Case nay default roi
		}

		if (endPos > fullLength){
			endPos = fullLength;
		}
	}
	return  selectedWave;
}

void WavFile::ShifToZero(){
	DATA_TYPE mean = 0.0;
	for (COUNT_TYPE n = 1; n < fullLength; n++) {
		mean += selectedData[n];
	}
	mean /= (DATA_TYPE)fullLength;
	// Shift to zero

	for (COUNT_TYPE n = 1; n < fullLength; n++) {
		fullData[n] -= mean;
	}
}

COUNT_TYPE WavFile::GetFullLength(){
	return fullLength;
}
DATA_TYPE* WavFile::GetFullData(){
	return fullData;
}

COUNT_TYPE WavFile::GetSelectedLength(){
	return selectedLength;
}
DATA_TYPE* WavFile::GetSelectedData(){
	return selectedData;
}

TIME_TYPE WavFile::GetSelectedTimeLength(){
	return selectedTimeLength;
}

TIME_TYPE WavFile::GetFullTimeLength(){
	return fullTimeLength;
}

COUNT_TYPE WavFile::GetSampleRate(){
	return sampleRate;
}



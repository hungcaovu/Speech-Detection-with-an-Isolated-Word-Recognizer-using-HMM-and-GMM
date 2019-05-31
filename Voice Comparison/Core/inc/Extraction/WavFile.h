#ifndef WAV_FILE_H
#define WAV_FILE_H
#include "AudioFile.h"
#include "Common.h"

class WavFile {
	char * path;
	bool validFile;
	bool selectedWave;
	DATA_TYPE* fullData ;
	DATA_TYPE* selectedData;

	COUNT_TYPE fullLength;
	COUNT_TYPE selectedLength;

	COUNT_TYPE sampleRate;
	TIME_TYPE fullTimeLength;
	TIME_TYPE selectedTimeLength;
public:
	WavFile();
	WavFile(char * path);
	bool Load(char* path = NULL);
	bool SelectedWave(COUNT_TYPE start, COUNT_TYPE end);
	bool IsValidFile(){ return validFile; };
	char *Path(){ return path; };
	bool NormalizeWave(DATA_TYPE peak);
	DATA_TYPE Peak();
	void ShifToZero();

	COUNT_TYPE GetFullLength();
	COUNT_TYPE GetSelectedLength();


	COUNT_TYPE GetSampleRate();

	DATA_TYPE* GetFullData();
	DATA_TYPE* GetSelectedData();

	TIME_TYPE GetSelectedTimeLength();
	TIME_TYPE GetFullTimeLength();

	~WavFile(){
		if (fullData != NULL) { delete[] fullData; fullData = NULL; }
		//if (path != NULL){ delete path; path = NULL; }
	};

};

#endif
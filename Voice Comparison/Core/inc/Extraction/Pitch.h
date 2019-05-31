/* R2D2 Pitch Processing
*
* Audio analysis for pitch extraction
*
* TODO: Both PitchDetectorHPS and this class should inherit from a base PitchDetector class...
*
* L. Anton-Canalis (info@luisanton.es)
*/

#ifndef PITCH_H
#define PITCH_H
#include "Common.h"
#include  <math.h>
#include <vector>
#include "ExtractionMFCC.h"
#include "WavFile.h"
#include <Util\MedianFilter.h>
enum PITCH_TYPE{
PITCH_YIN,
PITCH_AMDF
};

class Pitch{
public:
	virtual DATA_TYPE PitchProcess(DATA_TYPE * frame) = 0;

};
class PitchAMDF : public Pitch {
private:
	COUNT_TYPE sampleRate;
	DATA_TYPE last_period;
	DATA_TYPE current_frequency;

	DATA_TYPE F0min;
	DATA_TYPE F0max;
	COUNT_TYPE min_shift;
	COUNT_TYPE max_shift;
	COUNT_TYPE frameSize;
public:
	PitchAMDF(COUNT_TYPE sampleRate, COUNT_TYPE frameSize, DATA_TYPE F0min = 50, DATA_TYPE F0max = 400);
	DATA_TYPE PitchProcess(DATA_TYPE * frame);
private:
	void Initital();
	DATA_TYPE AMDF(DATA_TYPE * frame);
};

class PitchZeroCrossing : public Pitch {
private:
	COUNT_TYPE sampleRate;
	DATA_TYPE last_period;
	DATA_TYPE current_frequency;

	DATA_TYPE F0min;
	DATA_TYPE F0max;
	COUNT_TYPE min_shift;
	COUNT_TYPE max_shift;
	COUNT_TYPE frameSize;
public:
	PitchZeroCrossing(COUNT_TYPE sampleRate, COUNT_TYPE frameSize, DATA_TYPE F0min = 50, DATA_TYPE F0max = 400);
	DATA_TYPE PitchProcess(DATA_TYPE * frame);
private:
	void Initital();
	DATA_TYPE AMDF(DATA_TYPE * frame);
};

class PitchYIN : public Pitch {
private:
	COUNT_TYPE frameSize;
	COUNT_TYPE sampleRate;
	DATA_TYPE threshold;
	COUNT_TYPE window;
	DATA_TYPE *buffer;
	DATA_TYPE probability;
public:
	PitchYIN(COUNT_TYPE sampleRate, COUNT_TYPE frameSize, DATA_TYPE threshold = 0.1f);
	DATA_TYPE PitchProcess(DATA_TYPE *frame);
	DATA_TYPE Probability();
	~PitchYIN(){
		if (buffer){
			delete buffer;
			buffer = NULL;
		}
	}
private:
	void Initital();
	void Difference(DATA_TYPE *frame);
	void MeanNormalizedDifference();
	COUNT_TYPE AbsoluteThreshold();
	DATA_TYPE ParabolicInterpolation(COUNT_TYPE tauEstimate);

};

class PitchExtraction{
	Pitch *p;
	COUNT_TYPE sampleRate;
	COUNT_TYPE length;
	DATA_TYPE *data;
	COUNT_TYPE frameSize;
	COUNT_TYPE overlap;
	DATA_TYPE minF0;
	DATA_TYPE maxF0;
	COUNT_TYPE time;
	bool m_usingMedian;
	SIGNED_TYPE m_medianWindowSize;
	bool m_dropUnPitch;
	std::vector<DATA_TYPE> pitchs;
	std::vector<DATA_TYPE> m_smoothPitchs;
	std::vector<COUNT_TYPE> _posPitchs;
public:
	PitchExtraction(
		DATA_TYPE *data, 
		COUNT_TYPE sampleRate, 
		COUNT_TYPE length, 
		COUNT_TYPE frameSize, 
		COUNT_TYPE overlap, 
		DATA_TYPE minF0,
		DATA_TYPE maxF0,
		PITCH_TYPE pitchtype = PITCH_YIN, bool dropUnPitch = false) : m_medianWindowSize(3){
		time = 0;
		m_usingMedian = false;
		this->data = data;
		this->frameSize = frameSize;
		this->sampleRate = sampleRate;
		this->length = length;

		this->overlap = overlap;
		this->minF0 = minF0;
		this->maxF0 = maxF0;

		m_dropUnPitch = dropUnPitch;
		if (pitchtype == PITCH_YIN){
			p = new PitchYIN(this->sampleRate, this->frameSize);
		}
		else {
			p = new PitchAMDF(this->sampleRate, this->frameSize, minF0, maxF0);
		}

	}

	PitchExtraction(
		DATA_TYPE *data,
		COUNT_TYPE sampleRate,
		COUNT_TYPE length,
		DATA_TYPE timeframe,
		DATA_TYPE timeshift,
		DATA_TYPE minF0,
		DATA_TYPE maxF0,
		PITCH_TYPE pitchtype = PITCH_YIN, bool dropUnPitch = false) :m_medianWindowSize(3){

		this->data = data;
		this->sampleRate = sampleRate;
		this->frameSize = COUNT_TYPE(timeframe * sampleRate);
		this->overlap = frameSize - (COUNT_TYPE)(timeshift * sampleRate);
		this->length = length;

		this->minF0 = minF0;
		this->maxF0 = maxF0;
		m_dropUnPitch = dropUnPitch;
		if (pitchtype == PITCH_YIN){
			p = new PitchYIN(this->sampleRate, this->frameSize);
		}
		else {
			p = new PitchAMDF(this->sampleRate, this->frameSize, minF0, maxF0);
		}
	}
	PitchExtraction(
		WavFile *wav,
		COUNT_TYPE frameSize,
		COUNT_TYPE overlap,
		DATA_TYPE minF0,
		DATA_TYPE maxF0,
		PITCH_TYPE pitchtype = PITCH_YIN, bool dropUnPitch = false) :m_medianWindowSize(0){

		this->data = wav->GetSelectedData();
		this->frameSize = frameSize;
		this->sampleRate = wav->GetSampleRate();
		this->length = wav->GetSelectedLength();

		this->overlap = overlap;
		this->minF0 = minF0;
		this->maxF0 = maxF0;

		m_dropUnPitch = dropUnPitch;
		if (pitchtype == PITCH_YIN){
			p = new PitchYIN(this->sampleRate, this->frameSize);
		}
		else {
			p = new PitchAMDF(this->sampleRate, this->frameSize, minF0, maxF0);
		}
	}
	PitchExtraction(
		WavFile *wav,
		DATA_TYPE timeframe,
		DATA_TYPE timeshift,
		DATA_TYPE minF0,
		DATA_TYPE maxF0,
		PITCH_TYPE pitchtype = PITCH_YIN, bool dropUnPitch = false) :m_medianWindowSize(3){

		this->data = wav->GetSelectedData();
		this->sampleRate = wav->GetSampleRate();
		this->frameSize = COUNT_TYPE(timeframe * sampleRate);
		this->overlap = frameSize - (COUNT_TYPE)(timeshift * sampleRate);
		this->length = wav->GetSelectedLength();

		this->minF0 = minF0;
		this->maxF0 = maxF0;

		m_dropUnPitch = dropUnPitch;
		if (pitchtype == PITCH_YIN){
			p = new PitchYIN(this->sampleRate, this->frameSize);
		}
		else {
			p = new PitchAMDF(this->sampleRate, this->frameSize, minF0, maxF0);
		}
	}

	void SetWindowSizeMedianFilter(COUNT_TYPE windowsize){
		m_medianWindowSize = windowsize;
		m_usingMedian = true;
	}

	std::vector<DATA_TYPE> Process();
	std::vector<DATA_TYPE> GetPitch(){
		return pitchs;
	}
	std::vector<DATA_TYPE> GetSmoothPitch(){
		return m_smoothPitchs;
	}

	std::vector<COUNT_TYPE> GetPosPitch(){
		return _posPitchs;
	}

	~PitchExtraction(){
		if (data != NULL){
			delete[] data;
			data = NULL;
		}
		if (p != NULL){
			delete p;
			p = NULL;
		}
	}
};
#endif

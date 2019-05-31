#ifndef VAD_H
#define VAD_H
#include "Common.h"
#include <Util\MathUtil.h>
#include <Extraction\Energy.h>
#include <Extraction\Pitch.h>
#include <Extraction\ZeroCrossing.h>
#include <vector>
class Vad {
	struct Voice{
		COUNT_TYPE start;
		COUNT_TYPE end;
		Voice(COUNT_TYPE start, COUNT_TYPE end);
		Voice();
	};
	
private:
	std::vector<Voice> _voiceSegment;
	WavFile *_wav;
	Energy *_energy;
	PitchExtraction *_pitch;
	ZeroCrossing *_zeroCrossing;
	bool _valid;

	DATA_TYPE _energyThreshold;
	std::vector<DATA_TYPE> _energies;
	std::vector<COUNT_TYPE> _posEnergies;

	Vector _zeroRate;
	VectorIdx _posZeroRate;

	void SegmentE(DATA_TYPE threshold);
	//void SegmentZ(DATA_TYPE threshold);
public:
	Vad(WavFile *wav);

	void UseEnergy(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool normalizeEnergy, SIGNED_TYPE windowSmooth = 3, bool logEnergy = false);
	void UseZeroRate(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool shiftZero = true);
	void UsePitch(DATA_TYPE timeFrame, DATA_TYPE timeShift, DATA_TYPE minF0, DATA_TYPE maxF0, PITCH_TYPE pitchType = PITCH_YIN);

	DATA_TYPE GetThresholdEnergy(){
		return _energyThreshold;
	};

	bool IsValid(){
		return _valid;
	};
	void Process(DATA_TYPE threshold);

	std::vector<DATA_TYPE> GetSmoothEnergies(){
		return _energies;
	}
	
	COUNT_TYPE NumberOfVoiceSegment();
	COUNT_TYPE StartVoice(COUNT_TYPE count);
	COUNT_TYPE EndVoice(COUNT_TYPE count);
};
#endif VAD_H

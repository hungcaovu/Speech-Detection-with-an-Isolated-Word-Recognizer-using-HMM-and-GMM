#ifndef ENERGY_H
#define ENERGY_H
#include <Common.h>
#include "WavFile.h"
#include <vector>
class Energy {
private:
	WavFile *_wav;
	bool valid;
	bool _logEnergy;
	bool _normalizeEnergy;
	SIGNED_TYPE _medianWindowSizeFilter;
	COUNT_TYPE frameSize;
	COUNT_TYPE overlap;

	DATA_TYPE *data;
	std::vector<DATA_TYPE> _energy;
	std::vector<DATA_TYPE> _smoothEnergy;
	std::vector<COUNT_TYPE> _posEnergy;
	DATA_TYPE EnergyFrame(DATA_TYPE* cur_frame);
public:
	Energy(WavFile *wav, DATA_TYPE timeframe, DATA_TYPE timeshift, bool normalizeEnergy, bool logEnergy);
	void Process();
	void SetWindowSizeMedianFilter(int windowSize);
	bool IsValid();

	std::vector<DATA_TYPE> Energies();
	std::vector<DATA_TYPE> SmoothEnergies();
	std::vector<COUNT_TYPE> PosEnergies();
};
#endif
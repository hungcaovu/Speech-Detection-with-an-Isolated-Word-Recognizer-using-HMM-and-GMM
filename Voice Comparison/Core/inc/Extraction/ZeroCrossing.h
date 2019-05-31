#include <Common.h>
#include <Util\Vector.h>
#include <Extraction\WavFile.h>

class ZeroCrossing{
	WavFile *_wav;
	bool _shiftZero;
	bool _valid;
	COUNT_TYPE frameSize;
	COUNT_TYPE overlap;

	Vector _zeroRate;
	VectorIdx _posZeroRate;

private:
	DATA_TYPE ZeroCrossingRate(Vector &frame);
public:
	ZeroCrossing(WavFile *wav, DATA_TYPE timeframe, DATA_TYPE timeshift, bool shiftZero = true);

	void Process();
	bool IsValid();

	Vector ZeroRate();
	VectorIdx PosZeroRate();
};
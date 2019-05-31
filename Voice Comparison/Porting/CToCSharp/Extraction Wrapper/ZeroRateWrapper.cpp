#include <ZeroRateWrapper.h>

namespace ExtractionWrapper{
	ZeroRateWrapper::ZeroRateWrapper(WavFileWrapper ^wav, DATA_TYPE timeFrame, DATA_TYPE timeShift, bool shiftZero){
		_wav = wav;
		_valid = false;
		if (_wav != nullptr && _wav->IsValid){
			_zrc = new ZeroCrossing(_wav->GetWavFile(), timeFrame, timeShift, shiftZero);
			_valid = _zrc->IsValid();
		}
	}

	bool ZeroRateWrapper::Process(){
		_zrc->Process();
		_zeroRate = UtilWrapper::VectorToList(_zrc->ZeroRate());
		_valid = _zrc->IsValid();
		return _valid;
	}
};
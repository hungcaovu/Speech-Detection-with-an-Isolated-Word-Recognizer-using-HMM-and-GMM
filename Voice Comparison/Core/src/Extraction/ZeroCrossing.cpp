#include <Extraction\ZeroCrossing.h>
#include <Util\LogUtil.h>
ZeroCrossing::ZeroCrossing(WavFile *wav, DATA_TYPE timeframe, DATA_TYPE timeshift, bool shiftZero){
	if (wav != NULL && wav->IsValidFile()){
		_valid = true;
		_wav = new WavFile(*wav);
		_shiftZero = shiftZero;
		frameSize = (COUNT_TYPE)(timeframe * _wav->GetSampleRate());
		overlap = frameSize - (COUNT_TYPE)(timeshift * _wav->GetSampleRate());
		_wav->ShifToZero();
	} else{
		_valid = false;
	}
}

DATA_TYPE ZeroCrossing::ZeroCrossingRate(Vector &frame){
	
	COUNT_TYPE length = frame.Size();
	if (length <= 1) return 0;
	COUNT_TYPE pos = 0;
	DATA_TYPE zeroRate = 0.0;

	while (pos <  length - 1){
		DATA_TYPE mul = frame[pos] * frame[pos + 1];
		if (mul < 0){
			zeroRate += 1;
			pos++;
		}
		else if (mul == 0){
			if (frame[pos + 1] == 0.0)  pos++;
			COUNT_TYPE zero = 0;
			while (frame[pos] == 0.0 &&  pos < length){
				zero++;
				pos++;
			}
			zeroRate += 1;
		}
		else{
			pos++;
		}
	}

	return zeroRate;
}
void ZeroCrossing::Process(){
	if (!_valid) return;
	

	COUNT_TYPE length = _wav->GetSelectedLength();
	Vector data(_wav->GetSelectedData(), length);
	for (COUNT_TYPE blockStart = 0; blockStart < length - frameSize; blockStart += frameSize - overlap)
	{
		COUNT_TYPE numSamplesInBlock = frameSize;

		if (blockStart + frameSize > length){
			if (length > blockStart){
				numSamplesInBlock = length - blockStart;
			}
			else {
				numSamplesInBlock = 0;
			}
			
		}
			

		if (numSamplesInBlock)
		{
			Vector frame;
			data.GetSubVector(blockStart, numSamplesInBlock, frame);
			_zeroRate.PushBack(ZeroCrossingRate(frame));
			_posZeroRate.PushBack(blockStart + numSamplesInBlock/2);
		}
	}
}
bool ZeroCrossing::IsValid(){
	return _valid;
}
Vector ZeroCrossing::ZeroRate(){
	return _zeroRate;
}
VectorIdx ZeroCrossing::PosZeroRate(){
	return _posZeroRate;
}

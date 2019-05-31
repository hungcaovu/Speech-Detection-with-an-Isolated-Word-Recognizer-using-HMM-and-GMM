#ifndef  VAD_WRAPPER_H
#define VAD_WRAPPER_H
#include <Common.h>
#include <Extraction\ZeroCrossing.h>
#include <WavFileWrapper.h>
#include <vcclr.h>
#include <UtilWrapper.h>
using namespace System;
using namespace System::Collections::Generic;
namespace ExtractionWrapper{
	public ref class ZeroRateWrapper{
		WavFileWrapper^ _wav;
		ZeroCrossing * _zrc;

		bool _valid;
		List<DATA_TYPE>^ _zeroRate;
	public:
		ZeroRateWrapper(WavFileWrapper ^wav, DATA_TYPE timeFrame, DATA_TYPE timeShift, bool shiftZero);
		property List<DATA_TYPE>^ ZeroRate{
			List<DATA_TYPE>^ get(){
				return _zeroRate;
			}
		};
		property bool IsValid{
			bool get(){
				return _valid;
			}
		};

		bool Process();
	};
};
#endif
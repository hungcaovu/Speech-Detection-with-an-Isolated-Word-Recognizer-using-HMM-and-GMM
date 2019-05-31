#ifndef  VAD_WRAPPER_H
#define VAD_WRAPPER_H
#include <Common.h>
#include <Extraction\Vad.h>
#include <WavFileWrapper.h>
#include <vcclr.h>
#include <UtilWrapper.h>
using namespace System;
using namespace System::Collections::Generic;
namespace ExtractionWrapper{
	public ref class VadWrapper{
		WavFileWrapper^ _wav;
		Vad * _vad;

		bool _valid;
		DATA_TYPE _thresholdEnergy;
		List<DATA_TYPE>^ _engery;
		List<DATA_TYPE>^ _zeroRate;
		List<DATA_TYPE>^ _pitches;
	public:
		VadWrapper(WavFileWrapper ^wav);
		property List<DATA_TYPE>^ SmoothEnergies{
			List<DATA_TYPE>^ get(){
				return _engery;
			}
		};
		property List<DATA_TYPE>^ SmoothPitch{
			List<DATA_TYPE>^ get(){
				return _pitches;
			}
		};

		property List<DATA_TYPE>^ ZeroRate{
			List<DATA_TYPE>^ get(){
				return _pitches;
			}
		};
		property bool IsValid{
			bool get(){
				return _valid;
			}
		};

		property DATA_TYPE ThresholdEnergy{
			DATA_TYPE get(){
				return _thresholdEnergy;
			};
		};

		COUNT_TYPE GetSizeOfSegment();
		COUNT_TYPE GetStartSegment(COUNT_TYPE idx);
		COUNT_TYPE GetEndSegment(COUNT_TYPE idx);

		void UseEnergy(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool normalizeEnergy, SIGNED_TYPE windowSmooth, bool logEnergy);
		void UsePitch(DATA_TYPE timeFrame, DATA_TYPE timeShift, DATA_TYPE minF0, DATA_TYPE maxF0, PITCH_TYPE pitchType);
		void UseZeroRate(DATA_TYPE timeFrame, DATA_TYPE timeShift, bool shiftZero);

		bool Process(DATA_TYPE threshold);
	};
};
#endif
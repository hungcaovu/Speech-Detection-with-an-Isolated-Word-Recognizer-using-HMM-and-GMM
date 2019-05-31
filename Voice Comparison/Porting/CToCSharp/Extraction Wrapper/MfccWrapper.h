#ifndef MFCC_WRAPPER_H
#define MFCC_WRAPPER_H
#include <Extraction\ExtractionMFCC.h>
#include <WavFileWrapper.h>
#include <vcclr.h>
#include <UtilWrapper.h>
using namespace System;
using namespace System::Collections::Generic; 

namespace ExtractionWrapper{
	public ref class MFCCWrapper{
		MFCC *mfcc;
		WavFile *wav;
		bool valid;
		bool process;
		bool standardization;
		     
		COUNT_TYPE frameSize;
		COUNT_TYPE overlap;
		List<List<DATA_TYPE>^> ^freqFrame;
		List<List<DATA_TYPE>^> ^bandFilterFrame;
		List<List<DATA_TYPE>^> ^mfccFrame;
		List<List<DATA_TYPE>^> ^detalMfccFrame;
		List<List<DATA_TYPE>^> ^doubleDetalMfccFrame;

		String^ path;
	public:
		property String^ Path{
			String^ get(){
				return path;
			}
		}

		property bool ProcessDone{
			bool get(){
				return process && valid;
			}
		}
		
		property COUNT_TYPE FrameSize{
			COUNT_TYPE get(){
				return frameSize;
			}
		}

		property bool UserStandardization{
			bool get(){
				return standardization;
			}

			void set(bool value){
				standardization = value;
			}
		}

		property List<List<DATA_TYPE>^>^ Freq{
			List<List<DATA_TYPE>^>^ get(){ return freqFrame; }
		}

		property List<List<DATA_TYPE>^>^ BandFilter{
			List<List<DATA_TYPE>^>^ get(){ return bandFilterFrame; }
		}

		property List<List<DATA_TYPE>^>^ Mfcc{
			List<List<DATA_TYPE>^>^ get(){ return mfccFrame; }
		}

		property List<List<DATA_TYPE>^>^ DetalMfcc{
			List<List<DATA_TYPE>^>^ get(){ return detalMfccFrame; }
		}

		property List<List<DATA_TYPE>^>^ DoubleDetalMfcc{
			List<List<DATA_TYPE>^>^ get(){ return doubleDetalMfccFrame; }
		}

		
	public:

		MFCCWrapper(
			WavFileWrapper^ wav,
			DATA_TYPE timeframe,
			DATA_TYPE timeshift,
			COUNT_TYPE n_filters,
			DATA_TYPE flo,
			DATA_TYPE fhi,
			COUNT_TYPE nceptrums,
			COUNT_TYPE delta);

		MFCCWrapper(
			WavFileWrapper^ wav,
			COUNT_TYPE framesize,
			COUNT_TYPE overlap,
			COUNT_TYPE n_filters,
			DATA_TYPE flo,
			DATA_TYPE fhi,
			COUNT_TYPE nceptrums,
			COUNT_TYPE delta);
		bool Process();
		bool IsProcessed();

		void UseNormalizeMFCC(DATA_TYPE mean, DATA_TYPE var);
		bool SaveMFCC(String ^path);
		bool SaveDeltaMFCC(String ^path);
		bool SaveDoubleMFCC(String ^path);
		~MFCCWrapper();
	private:
		// Comment Old Code do not use Util Class.
		/*List<DATA_TYPE> ^FrameFreq(int idx);
		List<DATA_TYPE> ^FrameBandFilter(int idx);
		List<DATA_TYPE> ^FrameMFCC(int idx);
		List<DATA_TYPE> ^FrameDeltaMFCC(int idx);
		List<DATA_TYPE> ^FrameDoubleDeltaFMCC(int idx);*/


	};
};

#endif
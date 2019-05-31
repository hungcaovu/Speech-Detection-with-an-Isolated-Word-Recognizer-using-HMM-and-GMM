#pragma once
#ifndef PICTH_WRAPPER_H
#define PITCH_WRAPPER_H
#include <UtilWrapper.h>
#include <Extraction\Pitch.h>
#include <WavFileWrapper.h>
using namespace System;
using namespace System::Collections::Generic;

namespace ExtractionWrapper{
	public ref class PitchWrapper{
		PitchExtraction* p;

		COUNT_TYPE sampleRate;
		COUNT_TYPE length;
		DATA_TYPE *data;

		COUNT_TYPE frameSize;
		COUNT_TYPE overlap;
		DATA_TYPE minF0;
		DATA_TYPE maxF0;

		DATA_TYPE timeframe;
		DATA_TYPE timeshift;

		List<DATA_TYPE>^pitchs;
		List<DATA_TYPE>^smoothPitchs;
		
		SIGNED_TYPE m_medianWindowSize;
	public:
		property List<DATA_TYPE>^ Pitchs {
			List<DATA_TYPE>^ get(){
				return pitchs;
			}
		}

		property List<DATA_TYPE>^ SmoothPitchs {
			List<DATA_TYPE>^ get(){
				return smoothPitchs;
			}
		}

		PitchWrapper(
			ExtractionWrapper::WavFileWrapper^ wav,
			COUNT_TYPE frameSize,
			COUNT_TYPE overlap,
			DATA_TYPE minF0,
			DATA_TYPE maxF0,
			int pitchtype,
			bool dropUnPitch) : m_medianWindowSize(0){
			this->frameSize = frameSize;
			this->overlap = overlap;
			this->minF0 = minF0;
			this->maxF0 = maxF0;
			p = new PitchExtraction(wav->GetWavFile(), frameSize, overlap, minF0, maxF0, (PITCH_TYPE)pitchtype, dropUnPitch);
		}

		PitchWrapper(
			ExtractionWrapper::WavFileWrapper^ wav,
			DATA_TYPE timeframe,
			DATA_TYPE timeshift,
			DATA_TYPE minF0,
			DATA_TYPE maxF0,
			int pitchtype, 
			bool dropUnPitch) : m_medianWindowSize(0){
			this->timeframe = timeframe;
			this->timeshift = timeshift;
			this->minF0 = minF0;
			this->maxF0 = maxF0;
			p = new PitchExtraction(wav->GetWavFile(), timeframe, timeshift, minF0, maxF0, (PITCH_TYPE)pitchtype, dropUnPitch);
		}

		bool Process();

		void SetMedianWindowSize(SIGNED_TYPE windowSize){
			m_medianWindowSize = windowSize;
		}
		~PitchWrapper();
	};
};
#endif
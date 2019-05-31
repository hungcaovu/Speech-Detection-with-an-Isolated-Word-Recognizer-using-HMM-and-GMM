#ifndef  WAV_FILE_WRAPPER_H
#define WAV_FILE_WRAPPER_H
#include <Common.h>
#include <Extraction\WavFile.h>
#include <vcclr.h>
#include <UtilWrapper.h>
using namespace System;
using namespace System::Collections::Generic;
namespace ExtractionWrapper{
	public ref class WavFileWrapper{
		String ^path;
		WavFile* wav;
		COUNT_TYPE sampleRate;
		bool isValid;
		bool isSelected;
		List<DATA_TYPE>^ fullData;
		List<DATA_TYPE>^ selectedData;
	public:

		WavFileWrapper(String ^ path);

		property List<DATA_TYPE>^ FullData {
			List<DATA_TYPE>^ get(){
				return fullData;
			}
		}

		property List<DATA_TYPE>^ SelectedData {
			List<DATA_TYPE>^ get(){
				return selectedData;
			}
		}

		property COUNT_TYPE  SampleRate{
			COUNT_TYPE get(){
				return sampleRate;
			}
		}

		property String^ Path{
			String^ get(){
				return path;
			}
		}

		property bool IsValid{
			bool get(){
				return wav->IsValidFile();
			}
		};

		bool SelectedWave(COUNT_TYPE start, COUNT_TYPE end);
		
		bool Load();

		bool NormalizeWave(DATA_TYPE peak);

		DATA_TYPE Peak();

		void ShifToZero();

		WavFile *GetWavFile(){
			return wav;
		}

		TIME_TYPE GetSelectedTimeLength();

		TIME_TYPE GetFullTimeLength();

		~WavFileWrapper(){
			delete wav;
		}
	};
};
#endif
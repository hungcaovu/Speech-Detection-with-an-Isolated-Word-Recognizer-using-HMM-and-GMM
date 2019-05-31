#ifndef ENERGY_WRAPPER_H
#define ENERGY_WRAPPER_H
#include <Common.h>
#include <UtilWrapper.h>
#include <Extraction\Energy.h>
#include <WavFileWrapper.h>
using namespace System;
using namespace System::Collections::Generic;

namespace ExtractionWrapper{
	public ref class EnergyWrapper{
		String^ m_path;

		Energy *m_e;
		bool m_valid;
		List<DATA_TYPE>^ m_energies;
		List<COUNT_TYPE>^ m_posEnergies;
		List<DATA_TYPE>^ m_smoothEnergies;
	public:
		EnergyWrapper(ExtractionWrapper::WavFileWrapper^ wav, DATA_TYPE timeframe, DATA_TYPE timeshift, bool normalizeEnergy, bool logEnergy);
		void SetWindowSizeMedianFilter(int windowSize);
		void Process();
		property bool IsValid{
			bool get(){
				return m_valid;
			}
		};
		property List<DATA_TYPE> ^SmoothEnergies{
			List<DATA_TYPE> ^get(){
				return m_smoothEnergies;
			};
		};
		property List<DATA_TYPE> ^Energies{
			List<DATA_TYPE> ^get(){
				return m_energies;
			};
		};
		property List<COUNT_TYPE> ^PosEnergies{
			List<COUNT_TYPE> ^get(){
				return m_posEnergies;
			};
		};
	};
};
#endif


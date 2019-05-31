#include <EnergyWrapper.h>

namespace ExtractionWrapper{
	EnergyWrapper::EnergyWrapper(ExtractionWrapper::WavFileWrapper^ wav, DATA_TYPE timeframe, DATA_TYPE timeshift, bool normalizeEnergy, bool logEnergy){
		m_valid = false;
		m_energies = gcnew List<DATA_TYPE>();
		m_posEnergies = gcnew List<COUNT_TYPE>();

		if (wav->IsValid){
			m_e = new Energy(wav->GetWavFile(), timeframe, timeshift, normalizeEnergy, logEnergy);
			m_path = wav->Path;
			m_valid = true;
		}
	}

	void EnergyWrapper::SetWindowSizeMedianFilter(int windowSize){
		if (m_e != NULL){
			m_e->SetWindowSizeMedianFilter(windowSize);
		}
	}

	void EnergyWrapper::Process(){
		m_e->Process();
		std::vector<DATA_TYPE> energy = m_e->Energies();
		m_energies = UtilWrapper::ConverVectorToList(energy);

		std::vector<COUNT_TYPE> posEnergy = m_e->PosEnergies();
		m_posEnergies = UtilWrapper::ConverVectorToList(posEnergy);

		std::vector<DATA_TYPE> smoothEnergy = m_e->SmoothEnergies();
		m_smoothEnergies = UtilWrapper::ConverVectorToList(smoothEnergy);
		m_valid = m_e->IsValid();
	}
}
#include <PitchWrapper.h>
namespace ExtractionWrapper{
	bool PitchWrapper::Process(){
		
		if (m_medianWindowSize > 0) {
			p->SetWindowSizeMedianFilter(m_medianWindowSize);
		}

		std::vector<DATA_TYPE> result = p->Process();

		pitchs = ExtractionWrapper::UtilWrapper::ConverVectorToList(result);
		std::vector<DATA_TYPE> smoothResult = p->GetSmoothPitch();
		smoothPitchs = ExtractionWrapper::UtilWrapper::ConverVectorToList(smoothResult);

		return true;
	}
	PitchWrapper::~PitchWrapper(){
		delete p;
	}
}
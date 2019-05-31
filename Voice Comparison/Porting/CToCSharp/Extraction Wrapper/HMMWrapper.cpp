#include <HMMWrapper.h>

namespace ExtractionWrapper{

	HMMWrapper::HMMWrapper(){
		_hmm = new HMM();
	}

	HMMWrapper::HMMWrapper(String ^path){
		char * file = UtilWrapper::ConvertStringToChar(path);
		_hmm = new HMM(file);
		if (file != NULL){
			free(file);
		}
	}

	HMMWrapper::HMMWrapper(COUNT_TYPE stateNum, COUNT_TYPE gmmConponent, COUNT_TYPE gmmCoVar){
		_hmm = new HMM(stateNum, gmmConponent, gmmCoVar);
	}

	DATA_TYPE HMMWrapper::LogProbability(String ^file){
		char * path = UtilWrapper::ConvertStringToChar(file);
		DATA_TYPE pro = _hmm->LogProbability(path);
		if (path != NULL){
			free(path);
		}
		return pro;
	}

	DATA_TYPE HMMWrapper::LogProbability(List<List<DATA_TYPE> ^> ^ obsers){
		Matrix data = UtilWrapper::ListListToMatrix(obsers);
		return _hmm->LogProbability(data);
	}

	bool HMMWrapper::Load(String ^path){
		char * file = UtilWrapper::ConvertStringToChar(path);
		bool  res = _hmm->Load(file);

		if (file != NULL){
			free(file);
		}
		return res;
	}

	bool HMMWrapper::Save(String ^path){
		char * file = UtilWrapper::ConvertStringToChar(path);
		bool  res = _hmm->Save(file);

		if (file != NULL){
			free(file);
		}
		return res;
	}

	bool HMMWrapper::Trainning(List<String ^>^ files){
		std::vector<std::string> list = UtilWrapper::ConvertListStringToVectorString(files);
		return _hmm->Trainning(list);
	}

	bool HMMWrapper::Trainning(List<List<List<DATA_TYPE> ^> ^> ^obsers){
		std::vector<Matrix> data = UtilWrapper::ConvertTripListToVectorMatrix(obsers);
		return _hmm->Trainning(data);
	}

}
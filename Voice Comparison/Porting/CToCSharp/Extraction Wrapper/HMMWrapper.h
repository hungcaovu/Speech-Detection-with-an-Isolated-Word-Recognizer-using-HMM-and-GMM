#pragma once
#ifndef HMM_WRAPPER_H
#define HMM_WRAPPER_H

#include <AI\HMM.h>
#include <UtilWrapper.h>
using namespace System;
using namespace System::Collections::Generic;
namespace ExtractionWrapper{

	public ref class HMMWrapper{
		HMM *_hmm;
	public:
		HMMWrapper();
		HMMWrapper(String ^path);
		HMMWrapper(COUNT_TYPE stateNum, COUNT_TYPE gmmConponent, COUNT_TYPE gmmCoVar);
		DATA_TYPE LogProbability(String ^file);
		DATA_TYPE LogProbability(List<List<DATA_TYPE> ^> ^ obsers);

		bool Load(String ^path);
		bool Save(String ^path);

		bool Trainning(List<String ^>^ files);
		bool Trainning(List<List<List<DATA_TYPE> ^> ^> ^obsers);
	};
}
#endif
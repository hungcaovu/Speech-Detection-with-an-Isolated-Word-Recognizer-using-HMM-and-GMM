
#include <MfccWrapper.h>

namespace ExtractionWrapper{
	MFCCWrapper::MFCCWrapper(
		WavFileWrapper^ wav,
		COUNT_TYPE framesize,
		COUNT_TYPE overlap,
		COUNT_TYPE n_filters,
		DATA_TYPE flo,
		DATA_TYPE fhi,
		COUNT_TYPE nceptrums,
		COUNT_TYPE delta){


		this->overlap = overlap;
		this->frameSize = framesize;

		mfcc = new MFCC(wav->GetWavFile(), framesize, overlap, n_filters, flo, fhi, nceptrums, delta);
		valid = mfcc->IsValid();
		path = wav->Path;

		standardization = false;
		bandFilterFrame = gcnew List<List<DATA_TYPE> ^>();
		freqFrame = gcnew List<List<DATA_TYPE> ^>();
		mfccFrame = gcnew List<List<DATA_TYPE> ^>();
		detalMfccFrame = gcnew List<List<DATA_TYPE> ^>();
		doubleDetalMfccFrame = gcnew List<List<DATA_TYPE> ^>();
	};


	MFCCWrapper::MFCCWrapper(
		WavFileWrapper^ wav,
		DATA_TYPE timeframe,
		DATA_TYPE timeshift,
		COUNT_TYPE n_filters,
		DATA_TYPE flo,
		DATA_TYPE fhi,
		COUNT_TYPE nceptrums, COUNT_TYPE delta){

		mfcc = new MFCC(wav->GetWavFile(), timeframe, timeshift, n_filters, flo, fhi, nceptrums, delta);

		frameSize = mfcc->FrameSize();
		overlap = mfcc->Overlap();
		valid = mfcc->IsValid();
		path = wav->Path;

		standardization = false;
		bandFilterFrame = gcnew List<List<DATA_TYPE> ^>();
		freqFrame = gcnew List<List<DATA_TYPE> ^>();
		mfccFrame = gcnew List<List<DATA_TYPE> ^>();
		detalMfccFrame = gcnew List<List<DATA_TYPE> ^>();
		doubleDetalMfccFrame = gcnew List<List<DATA_TYPE> ^>();
	};

	bool MFCCWrapper::Process(){

		try{
			if (valid){
				if (standardization){
					mfcc->UseStandardization();
				}
				process = mfcc->Process();
			}
			
		}
		catch (...){
		}
		if (valid && process){
			freqFrame = UtilWrapper::MatrixToListList(mfcc->Freq());

			bandFilterFrame = UtilWrapper::MatrixToListList(mfcc->BandFilter());

			mfccFrame = UtilWrapper::MatrixToListList(mfcc->Mfcc());

			detalMfccFrame = UtilWrapper::MatrixToListList(mfcc->DeltaMfcc());

			doubleDetalMfccFrame = UtilWrapper::MatrixToListList(mfcc->DoubleDeltaMFCC());
		}
		
		return process && valid;
	}

	bool MFCCWrapper::IsProcessed(){
		return process && valid;
	}

	void MFCCWrapper::UseNormalizeMFCC(DATA_TYPE mean, DATA_TYPE var){
		if (valid){
			mfcc->UseNormalizeMFCC(mean, var);
		}
	}

	bool MFCCWrapper::SaveMFCC(String ^path){
		char  *file = UtilWrapper::ConvertStringToChar(path);
		bool  res = mfcc->SaveMFCC(file);

		if (file != NULL){
			free(file);
		}
		return res;
	}
	bool MFCCWrapper::SaveDeltaMFCC(String ^path){
		char  *file = UtilWrapper::ConvertStringToChar(path);
		bool  res = mfcc->SaveDeltaMFCC(file);

		if (file != NULL){
			free(file);
		}
		return res;
	}
	bool MFCCWrapper::SaveDoubleMFCC(String ^path){
		char  *file = UtilWrapper::ConvertStringToChar(path);
		bool  res = mfcc->SaveDoubleMFCC(file);

		if (file != NULL){
			free(file);
		}
		return res;
	}

	MFCCWrapper::~MFCCWrapper(){
		if (mfcc != NULL){
			delete mfcc;
		}
	}
}
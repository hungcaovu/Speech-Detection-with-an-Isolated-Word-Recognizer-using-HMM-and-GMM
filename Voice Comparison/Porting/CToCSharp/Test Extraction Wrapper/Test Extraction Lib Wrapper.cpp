// Test Extraction Lib Wrapper.cpp : main project file.

#include "stdafx.h"
#include <vector>

using namespace ExtractionWrapper;
using namespace System;
using namespace System::Collections:: Generic;

int main(array<System::String ^> ^args)
{
	OptionWrapper::SetLog(true);
#if 0
	char *name[11] = {
		"",
		"Mot",
		"Hai",
		"Ba",
		"Bon",
		"Nam",
		"Sau",
		"Bay",
		"Tam",
		"Chin",
		"Muoi",
};
	HMM *hmm[11] = { 0 };
	//int i = 5;
	for (int i = 1; i <= 10; i++){

		PRINTF("**********Train File: %s\n", name[i]);

		std::vector<std::string> files;
		for (int j = 1; j < 3; j++){
			char file[200] = { 0 };
			sprintf(file, "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\xml\\%s_%d.wav.xml", name[i], j);
			files.push_back(std::string(file));
		}

		hmm[i] = new HMM(3, 2, COV_TYPE_02);
		bool res = hmm[i]->Trainning(files);
		if (res){
			PRINTF("Train File: %s - Ok\n", name[i]);
		}
		else {
			PRINTF("Train File: %s - Failed\n", name[i]);
		}
	}

	// Phan Nhan Dang
	for (int f = 1; f < 11; f++){
		char file[200] = { 0 };
		sprintf(file, "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\xml\\%s_%d.wav.xml", name[f], 2);
		Matrix data;
		data.Load(file);
		int res = 1;
		DATA_TYPE max = hmm[1]->LogProbability(data);
		PRINTF("Prob file: %s.wav - HMM i= %d %.10f\n", name[f], 1, max);
		for (int i = 2; i <= 10; i++){
			DATA_TYPE prob = hmm[i]->LogProbability(data);
			PRINTF("Prob file: %s.wav -HMM i= %d %.10f\n", name[f], i, prob);
			if (prob > max){
				max = prob;
				res = i;
			}
		}

		PRINTF("Result: Prob file: %s.wav - %d\n", name[f], res);
	}
#endif

	String ^file = gcnew String("D:\\Working\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\Audio\\A\\7_22_2016_12_16_49_AM.wav");
	WavFileWrapper ^ wav = gcnew WavFileWrapper(file);
	wav->Load();
	wav->SelectedWave(0, 1869);
	MFCCWrapper ^mfcc = gcnew MFCCWrapper(wav, 0.015f, 0.005f, 20u, 0.0f, 5500.0f, 12u, 2);
	mfcc->Process();

	HMMWrapper ^hmm = gcnew HMMWrapper("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\HMM\\Bon.xml");
	hmm->LogProbability(mfcc->Mfcc);
	

	//List<int> ^a = gcnew List<int>();
	//a->Add(1);
	//a->Add(2);
	//a->Add(3);
	//a->Add(4);
	//List<int> ^b = gcnew List<int>();
	//b->Add(4);
	//b->Add(3);
	//b->Add(2);
	//b->Add(1);
	//List<int> ^c = gcnew List<int>();
	////MFCC m = new MFCC();
	////m.SumW(a, b, c);
	//std::vector<int> d;
	//d.push_back(1);
	//MFCCWrapper ^a = gcnew MFCCWrapper(0.025f, 0.01f, 22, 0, 4000, 13);
	//int r = a->Load("6612.wav", 0);
	//PitchWrapper ^b = gcnew PitchWrapper(a, 0.03f, 0.01f, 20.0f, 400.0f, 0);
	//b->Process();
	//List<float> ^c = b->Pitchs;
	//List<DATA_TYPE>^ a = gcnew List<DATA_TYPE>();
	//List<DATA_TYPE>^ a1 = gcnew List<DATA_TYPE>();
	//List<DATA_TYPE>^ a2 = gcnew List<DATA_TYPE>();
	//List<DATA_TYPE>^ a3 = gcnew List<DATA_TYPE>();
	//a->Add(1.0);
	//a->Add(1.0);
	//a->Add(1.0);
	//a->Add(1.0);
	//a->Add(1.0);
	//a->Add(1.0);
	//a->Add(1.0);
	//a->Add(1.0);
	//a1->Add(11.0);
	//a1->Add(11.0);
	//a1->Add(11.0);
	//a1->Add(11.0);
	//a1->Add(11.0);
	//a1->Add(11.0);
	//a1->Add(11.0);
	//a1->Add(11.0);
	//a2->Add(12.0);
	//a2->Add(12.0);
	//a2->Add(12.0);
	//a2->Add(12.0);
	//a2->Add(12.0);
	//a2->Add(12.0);
	//a2->Add(12.0);
	//a2->Add(12.0);
	//a3->Add(13.0);
	//a3->Add(13.0);
	//a3->Add(13.0);
	//a3->Add(13.0);
	//a3->Add(13.0);
	//a3->Add(13.0);
	//a3->Add(13.0);
	//a3->Add(13.0);
	//List<List<DATA_TYPE>^>^ b = gcnew List<List<DATA_TYPE>^>();
	//b->Add(a);
	//b->Add(a1);
	//b->Add(a2);
	//b->Add(a3);
	//FileWrapper::Write("test1.txt", a);
	//FileWrapper::Write("test2.txt", b);.
	//String ^file = gcnew String("C:\\Users\\hungc\\Desktop\\Voice Comparasion\\Voice Comparasion\\bin\\Debug\\Data\\Data\\a\\a.wav");
#if 0
	String ^file = gcnew String("..\\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\Toi\\01_Toi.wav");
	WavFileWrapper ^ wav = gcnew WavFileWrapper(file);
	wav->Load();
	wav->SelectedWave(12000, 513);
	MFCCWrapper ^mfcc = gcnew MFCCWrapper(wav, 0.015f, 0.005f, 20u, 0.0f, 8000.0f, 12u, 2);
	mfcc->Process();
	if (mfcc->ProcessDone) {
		FileWrapper::Write(file + ".txt", mfcc->Mfcc);
	}
#endif
    return 0;
}

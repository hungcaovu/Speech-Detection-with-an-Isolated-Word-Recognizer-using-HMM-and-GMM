#include<stdio.h>
#include <Util\LogUtil.h>
#include <AI\HMM.H>
#include <Util\File.h>
#include <Extraction\ExtractionMFCC.h>
void main(){
	Logger::SetEnabled(15);

#if 1
	std::vector<std::string> files;
	//files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Mot\\12_10_2015_4_09_24_AM.wav.Delta.xml");
	///files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Mot\\16_12_2015_11_51_04_CH.wav.Delta.xml");
	///files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Mot\\16_12_2015_11_50_39_CH.wav.Delta.xml");
	///files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Mot\\16_12_2015_11_50_22_CH.wav.Delta.xml");

	files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Tung\\17_12_2015_12_05_07_SA.wav.Delta.xml");
	files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Tung\\17_12_2015_12_05_26_SA.wav.Delta.xml");
	files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Tung\\17_12_2015_12_06_01_SA.wav.Delta.xml");



	//files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Mot\\12_10_2015_4_19_54_AM.wav.Delta.xml");
	//files.push_back("G:\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\MFCC\\Mot\\12_10_2015_4_09_34_AM.wav.Delta.xml");

	HMM *hmm = new HMM(4, 2, COV_TYPE_02);
	bool res = hmm->Trainning(files);
#endif


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
	HMM *hmm[11] = {0};
	//int i = 5;
	for (int i = 1; i <= 10; i++){

		PRINT(STEP, "**********Train File: %s\n", name[i]);
#if 0
		std::vector<std::string> files;
		for (int j = 1; j < 3; j++){
			char file[200] = { 0 };
			sprintf(file, "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\xml\\%s_%d.wav.xml", name[i], j);
			files.push_back(std::string(file));
		}
#endif
		bool res;
#if 0
		hmm[i] = new HMM(3, 2, COV_TYPE_02);
		res = hmm[i]->Trainning(files);
#endif
		char file[200] = { 0 };
		sprintf(file, "..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Train\\HMM\\%s.xml", name[i]);
#if 0
		hmm[i]->Save(file);
#endif
		hmm[i] = new HMM(3, 2, COV_TYPE_02);
		res = hmm[i]->Load(file);
		//char file2[200] = { 0 };
		//sprintf(file2, "%s_bk.xml", name[i]);
		//hmm[i]->Save(file2);
		if (res){
			PRINTF("Train File: %s - Ok\n", name[i]);
		}
		else {
			PRINTF("Train File: %s - Failed\n", name[i]);
		}
	}
	/*
	DATA_TYPE max = -100000.0;
	int m = 1;
	char file[200] = { 0 };
	sprintf(file, "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\xml\\%s_%d.wav.xml", name[3], 3);
	Matrix data;
	data.Load(file);
	int res = 0;
	for (; m < 11; m++){
		DATA_TYPE  cur = hmm[m]->LogProbability(data);
		PRINTF("Model %d = %f\n", m, cur);
		if(cur > max){
			max = cur;
			res = m;
		}
	}
	PRINTF("Res = %d\n", res);
	*/
#if 0
	// Phan Nhan Dang
	for (int h = 1; h < 11; h++){
		int f;
		int fi;
		DATA_TYPE max = -100000.0;
		for (f = 1; f < 11; f++){
			char file[200] = { 0 };
			sprintf(file, "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\xml\\%s_%d.wav.xml", name[f], 3);
			Matrix data;
			data.Load(file);
			DATA_TYPE prob = hmm[h]->LogProbability(data);

			PRINTF("Result: Prob file: %s.wav - prob = %f\n", name[f], prob);
			if (max < prob){
				max = prob;
				fi = f;
			}
		}
		PRINTF("Result: Prob file: %s.wav  H = %d\n", name[fi], h);
	}
#endif
	char file[200] = { 0 };
	sprintf(file, "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\xml\\%s_%d.wav.xml", name[3], 3);
	Matrix data;
	data.Load(file);
		int res = 1;
		DATA_TYPE max = hmm[1]->LogProbability(data);
		PRINTF("Prob file: %s.wav - HMM i= %d %.10f\n", name[3], 1, max);
		for (int i = 2; i <= 10; i++){
			DATA_TYPE prob = hmm[i]->LogProbability(data);
			PRINTF("Prob file: %s.wav -HMM i= %d %.10f\n", name[3], i, prob);
			if (prob > max){
				max = prob;
				res = i;
			}
		}

		PRINTF("Result: Prob file: %s.wav - %d\n", name[3], res);
			/*for (COUNT_TYPE f = 0; f < files.size(); f++){
		DATA_TYPE prob = hmm.LogProbability(files[f]);
		PRINTF("Prob file: %s - %.10f\n", files[f].c_str(), prob);
	}*/
#endif
#if 0
	std::vector<std::string> list = File::GetListFilesInDir(L"..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\Number\\*.wav");

	for (COUNT_TYPE i = 0; i < list.size(); i++){
		std::string path = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\Number\\" + list[i];
		std::string out = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Test\\xml\\" + list[i] + ".xml";
		WavFile xa(const_cast <char *>(path.c_str()));
		bool result = xa.Load();
		if (result)
			printf("Load File: %s OK\n", path.c_str());
		else {
			printf("Load File: %s FAILED\n", path.c_str());
		}
		MFCC mfccxa(&xa, 0.015f, 0.01f, 22, 0.0f, 2500.0f, 12, 2, true);
		result = mfccxa.Process();
		if (result)
			printf("MFCC File: %s OK\n", path.c_str());
		else
			printf("MFCC File: %s FAILED\n", path.c_str());
		Matrix mfcc = mfccxa.Mfcc();
		mfcc.Save((const_cast <char *>(out.c_str())));
	}
#endif
#if 0
	std::vector<std::string> files;
	//files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaj.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaj2.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaj3.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaj4.wav.xml");
	//files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaj5.wav.xml");
	//files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaj6.wav.xml");

	HMM hmm(2, 4, COV_TYPE_03);
	hmm.Trainning(files);
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaj.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaz.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xayz.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xay.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xax.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xa.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xas.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xaq.wav.xml");
	files.push_back("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\xais.wav.xml");

	for (COUNT_TYPE f = 0; f < files.size(); f++){
		DATA_TYPE prob = hmm.LogProbability(files[f]);
		PRINTF("Prob file: %s - %.10f\n", files[f].c_str(), prob);
	}
#endif
#if 0
	Matrix a(1, 100);
	for(int i =0; i < 100; i ++){
		a(0, i) = i;
	}
#endif
#if 0 //Test Util Matrix
	Matrix a(1, 10);
	a(0, 0) = 0;
	//a(1, 0) = 110;
	a(0,1) = 1;
	a(0,2) = 2;
	a(0, 3) = 3;
	a(0, 4) = 4;
	a(0,5) = 5;
	a(0, 6) = 6;
	a(0, 7) = 7;
	a(0, 8) = 8;
	a(0, 9) = 9;
	a.Print();
	a.AppendColumn(a);
	a.Print();
	GMM gmm(2, 2);
	gmm.DoEM(a);
	gmm.Print();
	gmm.DoEM(a);
	gmm.Print();
	//Matrix test1 = a.SubMatrixCol(3, 5);
	//test1.Print();
#endif
#if 0
	Matrix mfcc;
	mfcc.Load("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\mfcc\\xaj.wav.xml");
#endif
#if 0 // Test Train For GMM
	HMM hmm(5u,3u, COV_TYPE_02);
	//hmm.TrainForGMM(a);
#endif
}
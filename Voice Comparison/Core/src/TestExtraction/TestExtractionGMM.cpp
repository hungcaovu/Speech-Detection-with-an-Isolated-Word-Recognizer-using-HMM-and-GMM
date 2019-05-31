#include <AI\GMM.h>
#include <Extraction\ExtractionMFCC.h>
#include <Util\File.h>
#include <Util\LogUtil.h>
#include <algorithm>
int compare_value(std::pair<DATA_TYPE, std::string> const &a, std::pair<DATA_TYPE, std::string> const &b) {

	if (a.first > b.first){
		return 1;
	}
	else {
		return 0;
	}
}

void main(){
	Logger::SetEnabled(15);


#if 1
	char  path[] = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\audio\\aa.wav";

	WavFile *wav = new WavFile(path);
	wav->Load();
	MFCC mfccxa(wav, 0.015f, 0.01f, 22, 0.0f, 2500.0f, 12, 2, true);
	bool result = mfccxa.Process();

	Matrix del = mfccxa.DeltaMfcc();
	del.Save("DDDDDDDDDDDDDDDDDDD.xml");

	Matrix mfc = mfccxa.Mfcc();
	mfc.Save("MMMMMMMMMMMMMMMMM.xml");

	//GMM gmm(4, COV_TYPE_02);
	//gmm.DoEM(a);
#endif


#if 0
	WavFile xa("..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\x\\xa.wav");
	xa.Load();
	MFCC mfccxa(&xa, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxa.Process();
	mfccxa.Mfcc().Save("xa.xml");

	WavFile xaj("..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\x\\xaj.wav");
	xaj.Load();
	MFCC mfccxaj(&xaj, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaj.Process();
	mfccxaj.Mfcc().Save("xaj.xml");


	WavFile xaj1("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\Xaj1.wav");
	xaj1.Load();
	MFCC mfccxaj1(&xaj1, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaj1.Process();
	mfccxaj1.Mfcc().Save("xaj1.xml");

	WavFile xaj2("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\Xaj2.wav");
	xaj2.Load();
	MFCC mfccxaj2(&xaj2, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaj2.Process();
	mfccxaj2.Mfcc().Save("xaj2.xml");

	WavFile xaj3("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\Xaj3.wav");
	xaj3.Load();
	MFCC mfccxaj3(&xaj3, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaj3.Process();
	mfccxaj3.Mfcc().Save("xaj3.xml");

	WavFile xaj4("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\Xaj4.wav");
	xaj4.Load();
	MFCC mfccxaj4(&xaj4, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaj4.Process();
	mfccxaj4.Mfcc().Save("xaj4.xml");


	WavFile xaj5("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\Xaj5.wav");
	xaj5.Load();
	MFCC mfccxaj5(&xaj5, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaj5.Process();
	mfccxaj5.Mfcc().Save("xaj5.xml");

	WavFile xaj6("..\\..\\..\\Binary\\VTestExtraction\\Debug\\Xa\\Xaj6.wav");
	xaj6.Load();
	MFCC mfccxaj6(&xaj6, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaj6.Process();
	mfccxaj6.Mfcc().Save("xaj6.xml");

	WavFile xaq("..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\x\\xaq.wav");
	xaq.Load();
	MFCC mfccxaq(&xaq, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaq.Process();
	mfccxaq.Mfcc().Save("xaq.xml");

	WavFile xas("..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\x\\xas.wav");
	xas.Load();
	MFCC mfccxas(&xas, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxas.Process();
	mfccxas.Mfcc().Save("xas.xml");

	WavFile xax("..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\x\\xax.wav");
	xax.Load();
	MFCC mfccxax(&xax, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxax.Process();
	mfccxax.Mfcc().Save("xax.xml");


	WavFile xaz("..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\x\\xaz.wav");
	bool result4 = xaz.Load();
	MFCC mfccxaz(&xaz, 0.015f, 0.005f, 22, 0.0f, 2500.0f, 12, 2, true);
	mfccxaz.Process();
	mfccxaz.Mfcc().Save("xaz.xml");


	
	/*Matrix mfcc2Out;
	mfcc2Out.Load("MFCC_TA_2.xml");
	Matrix mfcc1Out;
	mfcc1Out.Load("MFCC_TA_1.xml");

	Matrix mfcc3Out;
	mfcc3Out.Load("MFCC_TOI_1.xml");
	Matrix mfcc4Out;
	mfcc4Out.Load("MFCC_TOI_2.xml");
	GMM gmmxaj1;
	gmmxaj1.Load("GMM_TA_1.xml");

	DATA_TYPE res8 = gmm1.LogProbability(mfcc1Out);
	DATA_TYPE res9 = gmm1.LogProbability(mfcc2Out);
	DATA_TYPE res10 = gmm1.LogProbability(mfcc3Out);
	DATA_TYPE res11 = gmm2.LogProbability(mfcc4Out);

	DATA_TYPE res12 = gmm2.LogProbability(mfcc1Out);
	DATA_TYPE res13 = gmm2.LogProbability(mfcc2Out);
	DATA_TYPE res14 = gmm2.LogProbability(mfcc3Out);
	DATA_TYPE res15 = gmm2.LogProbability(mfcc4Out);*/
	/*
	GMM gmm1(3, COV_TYPE_02);
	bool result1 = gmm1.DoEM(mfcc1Out);
	gmm1.Save("GMM_TA_1.xml");

	GMM gmm2(3, COV_TYPE_02);
	bool result2 = gmm2.DoEM(mfcc2Out);
	gmm2.Save("GMM_TOI_1.xml");

	*/
	//DATA_TYPE out2 = gmm.LogProbability(mfcc2Out);
	//DATA_TYPE out3 = gmm.LogProbability(mfcc3Out);


#else
//std::vector<std::string> list;
//list.push_back("awts.wav");
//list.push_back("chachj.wav");
//list.push_back("cachj.wav");
//list.push_back("chawpj.wav");
//list.push_back("kichj.wav");
//list.push_back("uwcs.wav");
//list.push_back("achs.wav");
//list.push_back("cawts.wav");
//list.push_back("ichs.wav");
//list.push_back("chachs.wav");
//list.push_back("kichs.wav");
//list.push_back("tichs.wav");
//list.push_back("xaj.wav");
#if 0
Matrix mfccxaj;
mfccxaj.Load("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\mfcc\\xaj.wav.xml");


GMM gmmxaj(2, COV_TYPE_03);
bool result1 = gmmxaj.DoEM(mfccxaj);
gmmxaj.Save("gmmxaj.xml");

// Get MFCC matrix
 
std::vector<std::string> list = File::GetListFilesInDir(L"..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\audio\\*.wav");
COUNT_TYPE files = 500;// list.size();
#endif
#if 0
for (COUNT_TYPE i = 0; i < files; i++){
	std::string path = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\audio\\" + list[i];
	std::string out = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\mfcc\\" + list[i] + "_dd.xml";
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
	Matrix mfcc = mfccxa.DoubleDeltaMFCC();
	mfcc.Save((const_cast <char *>(out.c_str())));

#if 0
	std::string gmmstd = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\gmm\\" + list[i] + ".xml";
	GMM gmm(5, COV_TYPE_02);
	bool res = gmm.DoEM(mfcc);
	if (!res)
	printf("FAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\n");
	else 
		printf(" gmm OK\n");
	gmm.Save(const_cast <char *>(gmmstd.c_str()));
#endif
}
#endif
#if 0
//std::vector<std::string> list = File::GetListFilesInDir(L"..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\*.xml");

for (COUNT_TYPE i = 0; i < list.size(); i++){
	Matrix mfcc;
	std::string path = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\xml\\" + list[i];
	std::string out = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\gmm\\" + list[i];
	mfcc.Load(const_cast <char *>(path.c_str()));
	GMM gmm(3, COV_TYPE_02);
	bool result = gmm.DoEM(mfcc);
	printf("%s %d\n", path.c_str(), result);
	gmm.Save(const_cast <char *>(out.c_str()));

}

#endif
#if 0
Logger::SetEnabled(false);
GMM gmm(5, COV_TYPE_03);
Matrix xaj;
xaj.Load("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\mfcc\\xaj.wav_dd.xml");
bool res = gmm.DoEM(xaj);
gmm.Save("..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\gmm\\chachs.wav_dd.xml");

std::vector<std::pair<DATA_TYPE, std::string>> outlist;

//std::vector<std::string> list2 = File::GetListFilesInDir(L"..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\audio\\*.wav");
//std::vector<std::string> list2;
//list2.push_back("awts.wav");
//list2.push_back("chachj.wav");
//list2.push_back("cachj.wav");
//list2.push_back("chawpj.wav");
//list2.push_back("kichj.wav");
//list2.push_back("uwcs.wav"); 
//list2.push_back("achs.wav");
//list2.push_back("cawts.wav");
//list2.push_back("ichs.wav");
//list2.push_back("chachs.wav");
//list2.push_back("kichs.wav");
//list2.push_back("tichs.wav");
//list2.push_back("xaj.wav");
Logger::SetEnabled(true);
for (COUNT_TYPE i = 0; i < files; i++){
	Matrix mfcc;
	std::string path = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\mfcc\\" + list[i] +"_dd.xml";
	std::string out = "..\\..\\..\\Binary\\TestExtraction\\Debug\\Xa\\gmm\\" + list[i] + ".xml";
	mfcc.Load(const_cast <char *>(path.c_str()));
	
	DATA_TYPE log = gmm.LogProbability(mfcc);
	PRINT("%d - Log %s -  %f   -- %f\n", i, list[i].c_str(), log, log / mfcc.ColumnSize());
	printf("%d - Log %s -  %f   -- %f\n", i, list[i].c_str(), log, log / mfcc.ColumnSize());
	std::pair<DATA_TYPE, std::string> pair;
	pair.first = log;
	pair.second = list[i];
	outlist.push_back(pair);
}

std::sort(outlist.begin(), outlist.end(), compare_value);


for (COUNT_TYPE i = 0; i < outlist.size(); i++){
	std::pair<DATA_TYPE, std::string> pair = outlist[i];
	PRINT(" %s - %f\n", pair.second.c_str(), pair.first);
}
printf("end");
#endif
#endif
#if 0
	Matrix mfccxa;
	mfccxa.Load("xa.xml");
	Matrix mfccxaj;
	mfccxaj.Load("xaj.xml");
	Matrix mfccxaq;
	mfccxaq.Load("xaq.xml");
	Matrix mfccxas;
	mfccxas.Load("xas.xml");
	Matrix mfccxax;
	mfccxax.Load("xax.xml");
	Matrix mfccxaz;
	mfccxaz.Load("xaz.xml");


	//GMM gmmxaj(3, COV_TYPE_02);
	//bool result1 = gmmxaj.DoEM(mfccxaj);
	//gmmxaj.Save("gmmxaj.xml");

	GMM gmmxaj;
	gmmxaj.Load("gmmxaj.xml");

	DATA_TYPE res1 = gmmxaj.LogProbability(mfccxa);
	DATA_TYPE res2 = gmmxaj.LogProbability(mfccxaj);
	DATA_TYPE res3 = gmmxaj.LogProbability(mfccxaq);
	DATA_TYPE res4 = gmmxaj.LogProbability(mfccxas);

	DATA_TYPE res5 = gmmxaj.LogProbability(mfccxax);
	DATA_TYPE res6 = gmmxaj.LogProbability(mfccxaz);
	//DATA_TYPE res7 = gmmxaj.LogProbability(mfcc3Out);
	//DATA_TYPE res8 = gmmxaj.LogProbability(mfcc4Out);


	Matrix mfcc2Out;
	mfcc2Out.Load("MFCC_TA_2.xml");
	Matrix mfcc1Out;
	mfcc1Out.Load("MFCC_TA_1.xml");

	Matrix mfcc3Out;
	mfcc3Out.Load("MFCC_TOI_1.xml");
	Matrix mfcc4Out;
	mfcc4Out.Load("MFCC_TOI_2.xml");
	GMM gmm1;
	gmm1.Load("GMM_TA_1.xml");
	GMM gmm2;
	gmm2.Load("GMM_TOI_1.xml");

	DATA_TYPE res8 = gmm1.LogProbability(mfcc1Out);
	DATA_TYPE res9 = gmm1.LogProbability(mfcc2Out);
	DATA_TYPE res10 = gmm1.LogProbability(mfcc3Out);
	DATA_TYPE res11 = gmm2.LogProbability(mfcc4Out);

	DATA_TYPE res12 = gmm2.LogProbability(mfcc1Out);
	DATA_TYPE res13 = gmm2.LogProbability(mfcc2Out);
	DATA_TYPE res14 = gmm2.LogProbability(mfcc3Out);
	DATA_TYPE res15 = gmm2.LogProbability(mfcc4Out);

#endif
}
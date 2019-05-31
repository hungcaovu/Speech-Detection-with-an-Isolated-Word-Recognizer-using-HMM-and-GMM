#include <stdio.h>
#include <io.h>
#include <stdlib.h>
#include <Common.h>
#include <Util\LogUtil.h>
#include <Util\MedianFilter.h>
#include <Extraction\ExtractionMFCC.h>
#include <Extraction\Pitch.h>
#pragma warning(disable : 4996)
int redirect_stderr(char *filename)
{
	int fid = -1;

	PRINT("Redirecting stderr to %s\n", filename);
	fflush(stdout);

	fid = _sopen(filename, O_RDWR | O_TRUNC | O_CREAT, _SH_DENYNO, _S_IWRITE);
	if (fid < 0)
	{
		perror("redirect_stderr");
		printf("Can't open file %s\n", filename);
		return(_fileno(stderr));
		//exit( 1 );
	}

	// stderr now refers to file "data" 
	if (-1 == _dup2(fid, 2))
	{
		perror("Can't _dup2 stdout");
		//exit( 1 );
	}
	return fid;


}
#include <Util\MathUtil.h>
//#include "DTW.h"
#include <Util\File.h>
#include <Extraction\Pitch.h>
#include <Extraction\Vad.h>
//#include "WriteFile.h"
void main(){
	//FILE *stream;
	//if ((stream = freopen("log.txt", "w", stdout)) == NULL)
	//	exit(-1);
	Logger::SetEnabled(true);
	//ExtractionMFCC("0094.wav",1024,500,21,50,11000);

	//MFCC a(0.025f, 0.001f, 26, 0, 4000, 12);
	//a.Load("0094.wav");
	//a.Load("C:\\Users\\Jimmy\\AppData\\Local\\Temp\\Voice Comparasion\\Recorder\\2_1_2015_1_59_44_AM.wav", 0);
	//a.Load("C:\\Users\\Jimmy\\AppData\\Local\\Temp\\Voice Comparasion\\Recoder\\3_30_2015_1_02_57_AM.wav", 0);
	//a.Load("C:\\Users\\Jimmy\\Desktop\\Voice Comparasion\\Voice Comparasion\\bin\\Debug\\TestData\\Sin 100z.wav");

	//a.Load("C:\\Users\\Jimmy\\Desktop\\Voice Comparasion\\Voice Comparasion\\bin\\Debug\\TestData\\Sin 100z.wav");
	//a.Process();

	//PitchExtraction p(&a, 0.1f, 0.005f, 30.0f, 500.0f, PITCH_AMDF);
	//std::vector<DATA_TYPE> re = p.Process();

	/*DATA_TYPE a = 1.5;
	DATA_TYPE b = 1.6;

	DATA_TYPE c = 1.7;
	DATA_TYPE d = MathUtil::Max(a, b, c);*/
	/*Logger::SetEnabled(true);

	DATA_TYPE a[] = { 1, 1,	2,	3,	2,	1 };
	COUNT_TYPE lena = sizeof(a) / sizeof(DATA_TYPE);

	DATA_TYPE b[] = { 1	,1,	1, 1 };
	COUNT_TYPE lenb = sizeof(b) / sizeof(DATA_TYPE);
	DATA_TYPE c = DTWUtil::DistanceOf2Vector(a, lena, b, lenb );*/


	//Test Transposed Maxtrix
	/*Array3D a = ArrayUtil::CreateZeroArray3D(2, 3);
	a[0][0] = 1; a[0][1] = 2; a[0][2] = 3;
	a[1][0] = 4; a[1][1] = 3; a[1][2] = 4;

	Array3D b = MathUtil::TransposeMatrix(a, 3);*/
	/*char * file = "a.txt";
	DATA_TYPE a[] = { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
	DATA_TYPE a1[] = { 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f };
	DATA_TYPE a2[] = { 3.0f, 3.0f, 3.0f, 3.0f, 3.0f, 3.0f, 3.0f };
	DATA_TYPE a3[] = { 4.0f, 4.0f, 4.0f, 4.0f, 4.0f, 4.0f, 4.0f };
	DATA_TYPE a4[] = { 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f };
	Array3D vec;
	vec.push_back(a);
	vec.push_back(a1);
	vec.push_back(a2);
	vec.push_back(a3);
	vec.push_back(a4);
	
	File::Write(file , vec, 7);




	DATA_TYPE *c = NULL;
	COUNT_TYPE b = 0;
	Array3D outvec;
	File::Read(file, outvec, b);*/
	//WavFile *wav = new WavFile("C:\\Users\\Jimmy\\Desktop\\Voice Comparasion\\Voice Comparasion\\bin\\Debug\\Data\\Data\\a\\aamq.wav");
	//wav ->Load();
	//PitchExtraction *p = new PitchExtraction(wav, 0.02f, 0.01f, 50.0f, 500.0f );
	//p->Process();
	//p->SetWindowSizeMedianFilter(3);

	//Vad *vad = new Vad(wav);
	//vad->UseEnergy(0.015, 0.01, true);
	//vad->Process(0.1);

/*
	DSP::MedianFilter1D<float> media;
	std::vector<float> v;
	v.push_back(5);
	v.push_back(1);
	v.push_back(2);
	v.push_back(9);
	v.push_back(1);
	v.push_back(20);
	std::vector<float> res = media.Filter(v);*/


	//WavFile * wav = new WavFile("C:\\Users\\Jimmy\\Desktop\\Human Voice Detector\\HumanVoiceDetector\\bin\\Debug\\a.wav");
	//WavFile * wav = new WavFile("C:\\Users\\hungc\\Desktop\\Voice Comparasion\\Voice Comparasion\\bin\\Debug\\Data\\Data\\a\\a.wav");
	//wav->Load();
	//MFCC * a = new MFCC(wav, 0.025f, 0.001f, 16, 0, 1900, 12);
	//a->Process();
	//File::Write("mfcc.txt", a->Mfcc(), a->MFCCRow());
	//Vad *vad = new Vad(wav, 0.01f, 0.005f);
	//vad->Energy();
	//WavFile *wav = new WavFile("..\\..\\..\\Binary\\TestExtraction\\Debug\\a.wav");
	//wav->Load();

	//WavFile *wav = new WavFile("..\\..\\..\\Binary\\Voice Comparasion\\Debug\\Data\\Data\\t\\tan.wav");
	//WavFile *wav = new WavFile("C:\\Users\\hungc\\Desktop\\Project\\Binary\\Voice Comparasion\\Debug\\Data\\Test\\sin100.wav");
	WavFile *wav = new WavFile("..\\..\\..\\Binary\\TestExtraction\\Debug\\Sin_100Hz.wav");
	wav->Load();
	wav->SelectedWave(0, 513);
	MFCC * a = new MFCC(wav, 512u, 0u, 20u, 0.0f, 4000.0f, 12u, 2);
	//MFCC * a = new MFCC(wav, 0.015f, 0.005f, 26u, 0.0f, 5000.0f, 12u, 2);
	//MFCC * a = new MFCC(wav, 0.015f, 0.005f, 15u, 0.0f, 2500.0f, 12u, 2);
	a->Process();
	Matrix mel = a->BandFilter();
	//Matrix mfcc = a->Mfcc();
	/*a->SaveFile("Text.txt");
	for (unsigned int i = 0; i < mfcc.size(); i++){
		Array1D frame = mfcc[i];
		PRINT("FRAME = %d \n", i);
		for (int j = 0; j < 13; j++){
			PRINT("   %3.7f  ", frame[j]);
		}

		PRINT("\n");
	}*/
}



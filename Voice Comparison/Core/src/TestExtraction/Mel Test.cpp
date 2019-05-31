#include <stdio.h>
#include <io.h>
#include <stdlib.h>
#include <Common.h>
#include <Util\LogUtil.h>
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

void main(){
	FILE *stream;
	if ((stream = freopen("log.txt", "w", stdout)) == NULL)
		exit(-1);

	//ExtractionMFCC("0094.wav",1024,500,21,50,11000);

	MFCC a(0.025f, 0.001f, 26, 0, 4000, 12);
	a.Load("0094.wav");
	//a.Load("C:\\Users\\Jimmy\\AppData\\Local\\Temp\\Voice Comparasion\\Recorder\\2_1_2015_1_59_44_AM.wav", 0);
	//a.Load("C:\\Users\\Jimmy\\AppData\\Local\\Temp\\Voice Comparasion\\Recoder\\3_30_2015_1_02_57_AM.wav", 0);
	//a.Load("C:\\Users\\Jimmy\\Desktop\\Voice Comparasion\\Voice Comparasion\\bin\\Debug\\TestData\\Sin 100z.wav");

	//a.Load("C:\\Users\\Jimmy\\Desktop\\Voice Comparasion\\Voice Comparasion\\bin\\Debug\\TestData\\Sin 100z.wav");
	//a.Process();

	PitchExtraction p(&a, 0.1f, 0.005f, 30.0f, 500.0f, PITCH_AMDF);
	std::vector<DATA_TYPE> re = p.Process();
}



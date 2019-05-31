#include <stdio.h>
#include <io.h>
#include <stdlib.h>
#include <Extraction\ExtractionMFCC.h>
#include <Util\LogUtil.h>
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
		PRINT("Can't open file %s\n", filename);
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
	Logger::SetEnabled(true);
	FILE *stream;
	/*if ((stream = freopen("log.txt", "w", stdout)) == NULL)
		exit(-1);*/
	//int b = Round(1.54);

	Dct a(12, 26);
	DATA_TYPE da[] = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,26 };
	//DATA_TYPE da[] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
	//DATA_TYPE da[] = { 8, 16, 24, 32, 40, 48, 56, 64 };
	
	//DATA_TYPE da[] = { 0.0f,0.0f,0.0f,1.0f	};
	DATA_TYPE *r = a.Process(&da[0]);
}



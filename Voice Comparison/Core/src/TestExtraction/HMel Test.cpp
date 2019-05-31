#include <stdio.h>
#include <io.h>
#include <stdlib.h>
#include <Extraction\ExtractionMFCC.h>
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
	FILE *stream;
	if ((stream = freopen("log.txt", "w", stdout)) == NULL)
		exit(-1);
	int b = Round(1.54);

	HMel a(26, 11025, 512, 0, 11025/2);

}



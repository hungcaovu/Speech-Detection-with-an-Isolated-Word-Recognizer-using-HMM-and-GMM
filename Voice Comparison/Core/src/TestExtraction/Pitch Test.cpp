
#pragma warning(disable : 4996)

#include <Util\LogUtil.h>
#include <Extraction\Pitch.h>
using namespace std;
	 /*
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

//void main(){
//	FILE *stream;
//	if ((stream = freopen("log.txt", "w", stdout)) == NULL)
//		exit(-1);
//	
//
//	
//}




double EstimatePeriod(
	const double    *x,         //  Sample data.
	const int       n,          //  Number of samples.  For best results, should be at least 2 x maxP
	const int       minP,       //  Minimum period of interest
	const int       maxP,       //  Maximum period of interest
	double&         q);        //  Quality (1= perfectly periodic)



	  */
int main(int argc, char * const argv[])

{
	  /*
		FILE *stream;
		if ((stream = freopen("log.txt", "w", stdout)) == NULL)
			exit(-1);
		
	const DATA_TYPE pi = 4 * atan(1);

	const DATA_TYPE sr = 44100;        //  Sample rate.
	const DATA_TYPE minF = 27.5;       //  Lowest pitch of interest (27.5 = A0, lowest note on piano.)
	const DATA_TYPE maxF = 4186.0;     //  Highest pitch of interest(4186 = C8, highest note on piano.)

	const COUNT_TYPE minP = COUNT_TYPE(sr / maxF - 1);    //  Minimum period
	const COUNT_TYPE maxP = COUNT_TYPE(sr / minF + 1);    //  Maximum period

	//  Generate a test signal

	const DATA_TYPE A440 = 440.0;              //  A440
	DATA_TYPE f = 200.46742;//A440 * pow(2.0, -9.0 / 12.0);   //  Middle C (9 semitones below A440)

	DATA_TYPE p = sr / f;
	DATA_TYPE q;
	const COUNT_TYPE n = 2 * maxP;
	DATA_TYPE *x = new DATA_TYPE[n];

	for (COUNT_TYPE k = 0; k < n; k++)
	{
		x[k] = 0;
		x[k] += 1.0*sin(2 * pi * 1 * k / p);    //  First harmonic
		x[k] += 0.6*sin(2 * pi * 2 * k / p);    //  Second harmonic
		x[k] += 0.3*sin(2 * pi * 3 * k / p);    //  Third harmonic
	}

	//  TODO: Add low-pass filter to remove very high frequency 
	//  energy. Harmonics above about 1/4 of Nyquist tend to mess
	//  things up, as their periods are often nowhere close to 
	//  integer numbers of samples.

	//  Estimate the period
	//double pEst = EstimatePeriod(x, n, minP, maxP, q);
	PitchYIN a(sr, 512,0.1f);
	DATA_TYPE pEst = a.PitchProcess(&x[0]);
	q = a.Probability();
	//  Compute the fundamental frequency (reciprocal of period)
	DATA_TYPE fEst = 0;
	if (pEst > 0)
		fEst = sr / pEst;
	printf("YIN Pitch \n");
	printf("Actual freq:         %8.3lf\n", f);
	printf("Estimated freq:      %8.3lf\n", pEst);
	printf("Pro freq:      %8.3lf\n", q);
	PitchAMDF b(sr, n);
	q = b.PitchProcess(&x[0]);
	printf("AMDF Pitch \n");
	printf("Actual freq:         %8.3lf\n", f);
	printf("Estimated freq:      %8.3lf\n", q);	    
	*/
	WavFile file("C5.wav");	   // Load C5 file.
	file.Load();
	file.NormalizeWave(0.05) ;

	Logger::SetEnabled(DATA);
	PitchExtraction pitch(&file, 0.05, 0.03f, 40.0f, 1000.0f, PITCH_YIN);
			std::vector<DATA_TYPE>  p = pitch.Process(); //Process picth
			pitch.GetPitch();
	return 0;
}




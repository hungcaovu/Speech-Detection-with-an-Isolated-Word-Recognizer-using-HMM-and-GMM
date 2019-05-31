
#include <stdio.h>
#include <Extraction\FFT.h>
using namespace FFT;
void main(){


	float areal[8] = {10,1,2,3,4,5,6,10 };
	float aimg[8] = {0};
	float tmpreal[8] = {0};
	float tmpimg[8] = { 0 };
	fft(areal, aimg, 8, tmpreal, tmpimg);
	abs(areal, aimg, 8, areal);
	ifft(areal, aimg, 8, tmpreal, tmpimg);

}
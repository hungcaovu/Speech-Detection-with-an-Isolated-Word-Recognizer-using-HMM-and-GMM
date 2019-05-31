#ifndef __FFT_H
#define __FFT_H
#include <math.h>
#include <Common.h>
#include <Util\MathUtil.h>
#include <Util\Vector.h>

class FFTUtil{
public:
	static void FFT(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE n, DATA_TYPE *tmpReal, DATA_TYPE *tmpImg);
	static void FFT(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE n);
	static void FFTUtil::FFT(Vector &real, Vector &img);
	static void IFFT(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE n, DATA_TYPE *tmpReal, DATA_TYPE *tmpImg);
	static void ABS(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE n, DATA_TYPE* abs);
	static void ABS(Vector &real, Vector &img, Vector & abs);
	static COUNT_TYPE NextPower2(COUNT_TYPE timesample);
};

#endif

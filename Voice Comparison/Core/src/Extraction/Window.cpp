
#include <malloc.h>
#include <Extraction\ExtractionMFCC.h>
#include <Util\LogUtil.h>
#include <Util\MathUtil.h>
void MFCC::initW(){
	COUNT_TYPE NFFTSize = frameSize;
	w = ArrayUtil::CreateZeroArray1D(NFFTSize);//new DATA_TYPE[NFFTSize];
#ifdef _PRINT_DEBUG_STEP 
	PRINT(STEP, "Init Window\n");
#endif
	for (COUNT_TYPE i = 0; i < NFFTSize; i++) {
		//w[i] = DATA_TYPE(0.53836) - DATA_TYPE(0.46164) * cos( (DATA_TYPE(2.0)*PI* DATA_TYPE(i)) / (DATA_TYPE(NFFTSize)) );
		w[i] = DATA_TYPE(0.54) - DATA_TYPE(0.46) * cos((DATA_TYPE(2.0)*PI* DATA_TYPE(i)) / (DATA_TYPE(NFFTSize)));
#ifdef _PRINT_DEBUG 
		PRINT(DATA | NONE_TIME, " %d - %2.6f ", i, w[i]);
#endif
	}

#ifdef _PRINT_DEBUG 
	PRINT(DATA | NONE_TIME, "\n");
#endif
#ifdef _PRINT_DEBUG_STEP 
	PRINT(STEP, "Init Window Completed\n");
#endif
}
void MFCC::Window(Vector &in, Vector &out){
	COUNT_TYPE NFFTSize = frameSize;
	for (COUNT_TYPE n = 0; n< NFFTSize; n++) {
		out[n] = in[n] * (DATA_TYPE)(w[n]);
	}
}
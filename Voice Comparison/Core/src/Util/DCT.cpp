#include <Util\DCT.h>
#include <Util\LogUtil.h>
#include <Util\MathUtil.h>

/*
Three slightly "different" definitions of MFCC
1. Davis & Mermelstein
MFCC[i] = SUM (j=1..N, f[j] * cos (i(j-1/2)pi/N)), i = 1..N-1
2. Vergin & O'Shaughnessy
MFCC[i] = SUM (j=0..N-1, f[j] * cos (i(j+1/2)pi/N)), i = 0..N-1
3. HTK-book
MFCC[i] = sqrt(2/n) SUM (j=1..N, f[j] * cos (i(j-1/2)pi/N)), i = 0..N-1

The f[j]'s are the MelSpectrogram values converted to dB.
We follow the definition of Davis and Mermelstein:
MFCC[i] = SUM (j=1..N, f[j] * cos (i(j-1/2)pi/N)), i=1..N,
*/

void Dct::init(){


#ifdef _PRINT_DEBUG_DCT
	PRINT(DETAIL, "How To get DCT init Matrix \n");
#endif


#ifdef _TRUNCATE_DCT
	COUNT_TYPE col = this->nfilters;
#else
	COUNT_TYPE col = this->nceptrums;
#endif // _TRUNCATE_DCT

	w = ArrayUtil::CreateZeroArray2D(col, this->nfilters);//new DATA_TYPE[this->nceptrums * this->nfilters];

	// Theo Jan 
	for (COUNT_TYPE cepstral = 0; cepstral < col; cepstral++){
		DATA_TYPE * row = w[cepstral];
		for (COUNT_TYPE filter = 0; filter < this->nfilters; filter++){
			//double aphal = PI * (DATA_TYPE)(cepstral + 1)* ((DATA_TYPE)filter + 0.5) / (DATA_TYPE)nfilters;
			//double aphal = PI * (double)(filter + 0.5)* ((double)cepstral + 1) / (double)nfilters;
			//double aphal = PI * (double)(filter)* ((double)cepstral + 0.5) / (double)nfilters;
			double aphal = PI * (double)(cepstral)* ((double)filter + 0.5) / (double)nfilters;
			row[filter] = (DATA_TYPE)cos(aphal);
			//if (filter == 0)
			//	row[filter] *= (DATA_TYPE)sqrt(1 / (double)nfilters);
			//else
			//	row[filter] *= (DATA_TYPE)sqrt(2.0 / (double)nfilters);
#ifdef _PRINT_DEBUG_DCT
			PRINT(DETAIL | NONE_TIME, " [%d,%d] alpha = %3.7f ", cepstral, filter, aphal);
#endif
		}
#ifdef _PRINT_DEBUG_DCT
		PRINT(DETAIL, "\n End matrix Phase Init DCT \n");
#endif
	}

#ifdef _PRINT_DEBUG_DCT
	PRINT(DETAIL, " Maxtrix DCT init\n");
	for (COUNT_TYPE cepstral = 0; cepstral < this->nceptrums; cepstral++){
		DATA_TYPE * row = w[cepstral];
		for (COUNT_TYPE filter = 0; filter < this->nfilters; filter++){
			PRINT(DATA | NONE_TIME, "  [%d,%d] = %3.7f  ", cepstral, filter, row[filter]);
		}
		PRINT(DATA | NONE_TIME, " \n");
	}
	PRINT(DETAIL, " End Init DCT \n");
#endif

}

Vector Dct::Process(Vector &frame){
	//Using HTK tool
	DATA_TYPE factor = (DATA_TYPE)sqrt((DATA_TYPE)2.0 / (DATA_TYPE)(nfilters));
#ifdef _PRINT_DEBUG_DCT
	PRINT(DETAIL, "DCT Process Frame:\n");
#endif

#ifdef _TRUNCATE_DCT
	Vector ceps(this->nceptrums);
	for (COUNT_TYPE cepstral = 0; cepstral < this->nceptrums; cepstral++){
#else
		Vector ceps(nceptrums);
		for (COUNT_TYPE cepstral = 0; cepstral < this->nceptrums; cepstral++){
#endif //_TRUNCATE_DCT
		DATA_TYPE sum = 0.0;
		DATA_TYPE * row = w[cepstral];
		for (COUNT_TYPE filter = 0; filter < this->nfilters; filter++){
			sum += row[filter] * frame[filter];
#ifdef _PRINT_DEBUG_DCT
			PRINT(DATA | NONE_TIME, "[%d,%d] = %3.7f * %3.7f Sum = %3.7f", cepstral, filter, row[filter], frame[filter], sum);
			//PRINT("[%d,%d] = %3.7f ", cepstral, filter, w[cepstral * nceptrums + filter]);
			//PRINT("[%d,%d] = %3.7f : %5.7f  ", cepstral, filter, w[cepstral * nceptrums + filter], sum);
#endif
		}
#ifdef _PRINT_DEBUG_DCT
		PRINT(DETAIL, "\n");
#endif
		ceps[cepstral] = sum * factor;
	}

#ifdef _PRINT_DEBUG_DCT
		PRINT(DETAIL, "InPut DCT\n");
	for (COUNT_TYPE filter = 0; filter < this->nfilters; filter++){
		PRINT(DATA | NONE_TIME, " [%d] = %3.7f ", filter, frame[filter]);
	}
	PRINT(DETAIL, "End InPut DCT\n");

	PRINT(DETAIL, "Output\n");
	for (COUNT_TYPE cepstral = 0; cepstral < this->nceptrums; cepstral++){
		PRINT(DATA | NONE_TIME, " [%d] = %3.7f ", cepstral, ceps[cepstral]);
	}
	PRINT(DETAIL, "End Output\n");
#endif
	return ceps;
}


Dct::~Dct(){
	if (w != NULL){ delete[] w; w = NULL; }
}


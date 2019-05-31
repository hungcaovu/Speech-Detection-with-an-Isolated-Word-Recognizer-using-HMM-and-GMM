#include "FFT.h"
#include <vector>

void frame(DATA_TYPE *in, long length, long FFTSize, long overlap, std::vector<DATA_TYPE *> &frames);


void mfcc(DATA_TYPE *in, long length, long simplerate, long FFTSize, long overlap,
	long n_filters, DATA_TYPE low, DATA_TYPE hi, std::vector<DATA_TYPE *> &out_mfcc, std::vector<DATA_TYPE *> &out_freq);
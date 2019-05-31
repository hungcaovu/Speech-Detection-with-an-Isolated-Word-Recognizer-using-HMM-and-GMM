#include "Extraction\ExtractionMFCC.h"
#include "Util\LogUtil.h"
#include "Util\MathUtil.h"
DATA_TYPE HMel::MelScale(DATA_TYPE freqHZ){
	// Su dung cong thuc Mel Frequence Scale.
	//return ((DATA_TYPE)2595.0 * (DATA_TYPE)log10(1.0 + (DATA_TYPE)freqHZ / 700.0));
	return (DATA_TYPE)(1125.0 * log(1.0 + (double)freqHZ / 700.0));
}
DATA_TYPE HMel::HezBack(DATA_TYPE freqMel){
	// Su dung cong thuc conver Mel to fre
	//return((DATA_TYPE)700.0*(DATA_TYPE)(pow((DATA_TYPE)10, (DATA_TYPE)(freqMel / 2595.0)) - 1.0));
	return(DATA_TYPE)(700.0*(exp(((double)freqMel / 1125.0)) - 1.0));
}
COUNT_TYPE HMel::FretoBin(COUNT_TYPE sampleRate, COUNT_TYPE NFFTSize, DATA_TYPE fre)
{
	return (long)Round(fre / ((DATA_TYPE)sampleRate / (DATA_TYPE)NFFTSize));
}
void HMel::InitFilterBand(DATA_TYPE band){

	DATA_TYPE mel_fmax = MelScale(fmax);
	DATA_TYPE mel_fhi = MelScale(fhi);
	DATA_TYPE mel_flo = MelScale(flo);

	if (band == 0) band = (mel_fhi - mel_flo) / (bnfilters + 1);

	std::vector<DATA_TYPE> melpoints;

	melpoints.push_back(mel_flo);
	for (COUNT_TYPE i = 1; i < bnfilters + 1; i++){
		melpoints.push_back(melpoints.back() + band);
	}
	melpoints.push_back(mel_fhi);

#ifdef _PRINT_DEBUG_HMEL
	PRINT(DETAIL, "Init Mel Filter\n");
	PRINT(DATA | NONE_TIME, "Mel Points: Band %2.5f \n", band);
	for (COUNT_TYPE i = 0; i < melpoints.size(); i++){
		PRINT(DATA | NONE_TIME, "  Mel : %5.4f - Freq : %5.4f \n", melpoints[i], HezBack(melpoints[i]));
	}
#endif
	DATA_TYPE bandfreq = (DATA_TYPE)samplerate / fftsize;

	//for (int i = bnfilters - 1; i >= 0; i--){
	for (COUNT_TYPE i = 0; i < bnfilters; i++){
		DATA_TYPE * hn = ArrayUtil::CreateZeroArray1D(lfilter);//Data Output

		DATA_TYPE startfreq = HezBack(melpoints[i]);
		DATA_TYPE centerfreq = HezBack(melpoints[i + 1]);
		DATA_TYPE endfreq = HezBack(melpoints[i + 2]);

		//COUNT_TYPE binstart = FretoBin(samplerate, fftsize, startfreq);
		//COUNT_TYPE bincenter = FretoBin(samplerate, fftsize, centerfreq);
		//COUNT_TYPE binend = FretoBin(samplerate, fftsize, endfreq);
#ifdef _PRINT_DEBUG_HMEL
		//PRINT("Bin Ponts %d : Start %d freq %f, Center %d freq %f, End %d freq %f, Band %d freq %f\n", i, binstart, startfreq, bincenter, centerfreq, binend, endfreq, binend - binstart, endfreq - startfreq);
		PRINT(DATA | NONE_TIME, "Idx = %d: StartFreq - %3.5f CenterFreq - %3.5f EndFreq - %3.5f\n", i, startfreq, centerfreq, endfreq);
		//PRINT("binstart - %d bincenter - %d binend - %d\n", binstart, bincenter, binend);

#endif
		for (COUNT_TYPE k = 0; k < lfilter; k++){
			DATA_TYPE freq = bandfreq *k;
			if (startfreq < freq && freq < centerfreq){
				hn[k] = (freq - startfreq) / (centerfreq - startfreq);
			}
			else if (centerfreq < freq && freq < endfreq) {
				hn[k] = (endfreq - freq) / (endfreq - centerfreq);
			}
			else if (freq == centerfreq){
				hn[k] = 1.0;
			}
			else {
				hn[k] = 0.0;
			}
		}
#ifdef _PRINT_DEBUG_HMEL
		PRINT(DETAIL, "\n");
#endif
		h.push_back(hn);
	}

#ifdef _PRINT_DEBUG_HMEL 
	PRINT(DATA, "Mel Hn: \n");
	for (COUNT_TYPE i = 0; i < h.size(); i++){
		for (COUNT_TYPE j = 0; j < lfilter; j++){
			PRINT(DATA | NONE_TIME, "  %2.5f ", h[i][j]);
		}
		PRINT(DATA | NONE_TIME, "\n");
	}

	PRINT(DETAIL, "End Mel Points\n");
#endif
}

DATA_TYPE * HMel::getH(COUNT_TYPE numberband){
	if (numberband < h.size() && numberband > 0){
		return h[numberband - 1];
	}
	return NULL;
}

HMel::~HMel(){
	for (COUNT_TYPE i = 0; i < h.size(); i++){
		DATA_TYPE *tmp = h[i];
		delete tmp;
	}
	h.clear();
}

Vector HMel::Filter(Vector &frame){

#ifdef _PRINT_DEBUG_HMEL
	PRINT(DETAIL, "Process HMel Filter\n");
#endif
	Vector frameMel(bnfilters);// = ArrayUtil::CreateZeroArray1D(bnfilters);//new DATA_TYPE[bnfilters];
	for (COUNT_TYPE i = 0; i < this->bnfilters; i++){
		DATA_TYPE sum = 0.0;
		DATA_TYPE *hn = h[i];
#ifdef _PRINT_DEBUG_HMEL
		PRINT(DATA | NONE_TIME, "Hmel = %d  ", i);
#endif
		for (COUNT_TYPE j = 0; j < lfilter; j++){
			sum += hn[j] * frame[j];
#ifdef _PRINT_DEBUG_HMEL
			PRINT(DATA | NONE_TIME, "[%d,%d] = %3.7f * %3.5f Sum = %3.7f ", i, j, hn[j], frame[j], sum);
#endif
		}
		frameMel[i] = sum;
#ifdef _PRINT_DEBUG_HMEL
		PRINT(DATA | NONE_TIME, "\n");
#endif
	}
	return frameMel;
}
// Get log of coefficients
Vector &HMel::Log(Vector &frame){
	for (COUNT_TYPE i = 0; i < bnfilters; i++){
		if (frame[i] != 0){
			frame[i] = log(frame[i] * frame[i]);
		}
		else {
			frame[i] = 0;
		}
	}
	return frame;
}

COUNT_TYPE HMel::nFilter(){
	return bnfilters;
}


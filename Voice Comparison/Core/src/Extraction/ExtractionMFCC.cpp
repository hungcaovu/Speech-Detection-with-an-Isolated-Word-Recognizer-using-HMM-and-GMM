
#include <Extraction\ExtractionMFCC.h>
#include <Extraction\AudioFile.h>
#include <util\Fft.h>
#include <Util\LogUtil.h>
#include <Util\MathUtil.h>
#include <Util\Vector.h>
#include <tinyxml.h>

using namespace std;
MFCC::MFCC(WavFile *wav,
	COUNT_TYPE frameSize,
	COUNT_TYPE overlap,
	COUNT_TYPE n_filters,
	DATA_TYPE flo,
	DATA_TYPE fhi,
	COUNT_TYPE nceptrums,
	COUNT_TYPE ndelta,
	bool includeEnergy
	){
	this->wav = wav;
	this->frameSize = frameSize;
	this->overlap = overlap;
	this->n_filters = n_filters;
	this->flo = flo;
	this->fhi = fhi;
	this->nceptrums = nceptrums;
	this->ndelta = ndelta;
	this->_includeEnergy = includeEnergy;
	usetime = false;

	PRINT(STEP, "MFCC Processing File: %s\n", wav->Path());
	Init();
}
void MFCC::Init(){
	
	validFile = false;
	processDone = True;
	w = NULL;
	standardization = false;
	normalizeMFCC = false;
	d = NULL;
	if (wav != NULL && wav->IsValidFile()){
		d = new Dct(nceptrums, this->n_filters);
		sampleRate = wav->GetSampleRate();
		selectedLength = wav->GetSelectedLength();
		selectedData = ArrayUtil::CreateZeroArray1D(selectedLength);
		memcpy(selectedData, wav->GetSelectedData(), sizeof(DATA_TYPE) * selectedLength);

		validFile = wav->IsValidFile();
		// Check validate Frequency
		if (validFile){
			if (fhi < DATA_TYPE(sampleRate) / 2 + 0.5){
				validFile = true;
			}
			else{
				validFile = false;
			}
		}
		// Time
		if (usetime){
			frameSize = FFTUtil::NextPower2((COUNT_TYPE)(timeframe * sampleRate));
			overlap = frameSize - (COUNT_TYPE)(timeshift * sampleRate);
			h = new HMel(n_filters, sampleRate, frameSize, flo, fhi);
			initW();
			if (overlap < 0){
				validFile = false;
			}
			else {
				validFile = true;
			}
		}
		else {
			h = new HMel(n_filters, sampleRate, frameSize, flo, fhi);
			initW();
		}

		PRINT(STEP, "Wave Size - %d, FrameSize - %d, Overlap - %d, Sample Shift - %d, LowF - %f, HiF - %f, Filter - %d NumCoff - %d\n",
			wav->GetSelectedLength(), frameSize, overlap, frameSize - overlap, flo, fhi, n_filters, nceptrums);
	}
}
MFCC::MFCC(
	WavFile *wav,
	DATA_TYPE timeframe,
	DATA_TYPE timeshift,
	COUNT_TYPE n_filters,
	DATA_TYPE flo,
	DATA_TYPE fhi,
	COUNT_TYPE nceptrums,
	COUNT_TYPE ndelta,
	bool includeEnergy
	){
	this->wav = wav;
	this->n_filters = n_filters;
	this->flo = flo;
	this->fhi = fhi;
	this->nceptrums = nceptrums;
	this->ndelta = ndelta;
	this->_includeEnergy = includeEnergy;
	// Time Setting
	this->timeframe = timeframe;
	this->timeshift = timeshift;
	usetime = true;
	PRINT(STEP, "MFCC Processing File: %s\n", wav->Path());
	Init();
};


COUNT_TYPE MFCC::FrameSize(){
	return frameSize;
}
COUNT_TYPE MFCC::Overlap(){
	return overlap;
}
COUNT_TYPE MFCC::FreqRow(){
	return frameSize/2;
}
COUNT_TYPE MFCC::MFCCRow(){
	return nceptrums;
}
COUNT_TYPE MFCC::DeltaMFCCRow(){
	return (nceptrums)* 2;
}
COUNT_TYPE MFCC::DoubleDeltaMFCCRow(){
	return (nceptrums) * 3;
}
COUNT_TYPE MFCC::BandFilterRow(){
	return h->nFilter();
}
void MFCC::UseStandardization(){
	standardization = true;
}
void MFCC::UseNormalizeMFCC(DATA_TYPE min, DATA_TYPE max){
	normalizeMFCC = true;
	meanNormalizeMFCC = min;
	covarianceNormalizeMFCC = max;
}
void MFCC::NormalizeMFCC(DATA_TYPE max, DATA_TYPE min){
	/*for (COUNT_TYPE j = 0; j < mfccSpectrum.size(); j++){
		for (COUNT_TYPE i = 0; i < nceptrums; i++){
			mfccSpectrum[j][i] = (mfccSpectrum[j][i] - meanNormalizeMFCC) / covarianceNormalizeMFCC;
		}
	}*/
}
void MFCC::Standardization(){
	mfccSpectrum.Standardization(true);// Using Column
}
bool MFCC::ProcessDone(){
	return validFile && processDone;
}
void MFCC::ProcessDelta(){

#ifdef _PRINT_DEBUG_DETAL_PROCESSING
	PRINT(STEP, "PROCESSING DELTA\n");
#endif

	DATA_TYPE mSqSum = 0;
	for (DATA_TYPE i = -DATA_TYPE(ndelta); i <= DATA_TYPE(ndelta); i += DATA_TYPE(1.0)) {
		mSqSum += i*i;
	}
	mSqSum *= 2;

#ifdef _PRINT_DEBUG
	PRINT(DATA, "Sum = %d \n", mSqSum);
#endif 
	COUNT_TYPE colMfcc = mfccSpectrum.ColumnSize();
	COUNT_TYPE rolMfcc = mfccSpectrum.RowSize();

	deltaMfccSpectrum = mfccSpectrum;
	deltaMfccSpectrum.Resize(rolMfcc * 2, colMfcc);

	PRINT(DATA, "MFCC");
	mfccSpectrum.Print();
	PRINT(DATA, "Init Delta MFCC");
	deltaMfccSpectrum.Print();

	for (COUNT_TYPE i = 0; i < colMfcc; i++){
		for (COUNT_TYPE j = 0; j < nceptrums; j++){
			if (i < ndelta || (i > colMfcc - ndelta && colMfcc > ndelta)){
				DATA_TYPE &data = deltaMfccSpectrum(nceptrums + j, i) = deltaMfccSpectrum(j, i);
				data /= mSqSum;
			}
			else{
				DATA_TYPE &data = deltaMfccSpectrum(nceptrums + j, i) = 0.0;
				for (COUNT_TYPE m = 1; m <= ndelta; m++) {
					DATA_TYPE tplus = mfccSpectrum(j, i + m);
					DATA_TYPE tmnus = mfccSpectrum(j, i - m);
					data += (tplus - tmnus) * DATA_TYPE(m);
				}
				data /= mSqSum;
			}
			
		}
		
	}
	PRINT(DATA, "Init Delta MFCC");
	deltaMfccSpectrum.Print();
}
void MFCC::ProcessDoubleDelta(){
#ifdef _PRINT_DEBUG_DETAL_PROCESSING
	PRINT(STEP, "PROCESSING DELTA\n");
#endif

	DATA_TYPE mSqSum = 0;
	for (DATA_TYPE i = -DATA_TYPE(ndelta); i <= DATA_TYPE(ndelta); i++) {
		mSqSum += i*i;
	}
	mSqSum *= 2;
#ifdef _PRINT_DEBUG
	PRINT(DATA, "Sum = %d \n", mSqSum);
#endif 
	COUNT_TYPE colMfcc = mfccSpectrum.ColumnSize();
	COUNT_TYPE rowMfcc = mfccSpectrum.RowSize();

	doubleDeltaMfccSpectrum = deltaMfccSpectrum;
	doubleDeltaMfccSpectrum.Resize(rowMfcc * 3, colMfcc);

	for (COUNT_TYPE i = 0; i < colMfcc; i++){
		for (COUNT_TYPE j = 0; j < nceptrums; j++){
			if (i < ndelta || (i > colMfcc - ndelta && colMfcc > ndelta)){
				DATA_TYPE &data = doubleDeltaMfccSpectrum(nceptrums * 2 + j, i) = doubleDeltaMfccSpectrum(j, i);
				data /= mSqSum;
			}
			else{
				DATA_TYPE &data = doubleDeltaMfccSpectrum(nceptrums * 2 + j, i) = 0.0;
				for (COUNT_TYPE m = 1; m <= ndelta; m++) {
					DATA_TYPE tplus = doubleDeltaMfccSpectrum(nceptrums + j, i + m);
					DATA_TYPE tmnus = doubleDeltaMfccSpectrum(nceptrums + j, i - m);
					data += (tplus - tmnus) * DATA_TYPE(m);
				}
				data /= mSqSum;
			}
		}
	}
}
bool MFCC::SaveFile(char *path){
	try{
		TiXmlDocument doc;
		COUNT_TYPE rowSize = mfccSpectrum.RowSize();
		COUNT_TYPE colSize = mfccSpectrum.ColumnSize();
		TiXmlElement *head =  new TiXmlElement("MFCC");
		//Row
		head->SetAttribute("coffs", rowSize);
		//Col
		head->SetAttribute("frames", colSize);
		//Data
		TiXmlElement *data =  new TiXmlElement("Data");
		std::string text = "";
		for (COUNT_TYPE row = 0; row < rowSize; row++){
			for (COUNT_TYPE col = 0; col < colSize; col++){
				char dig[100] = { 0 };
				sprintf(dig, " %.10lf", double(mfccSpectrum(row, col)));
				text += std::string(dig);
			}
			text += std::string("<br />");
		}
		TiXmlText *contain = new TiXmlText(text.c_str());
		data->LinkEndChild(contain);
		head->LinkEndChild(data);
		doc.LinkEndChild(head);
		doc.SaveFile(path);
		return true;
	}
	catch (exception){
		return false;
	}
	
}
bool MFCC::SaveMFCC(char *path){
	return mfccSpectrum.Save(path);
}
bool MFCC::SaveDeltaMFCC(char *path){
	return deltaMfccSpectrum.Save(path);
}
bool MFCC::SaveDoubleMFCC(char *path){
	return doubleDeltaMfccSpectrum.Save(path);
}
bool MFCC::Process(){
	
	if (!processDone || !validFile) return False;

	PRINT(STEP, "MFCC Processing\n");
	COUNT_TYPE FFTSize = frameSize;
	
	Vector data(selectedData, selectedLength);
	Vector out = /*data;*/ PreEmphasis(data);
	// Kiem tra ham MFCC
	int time = 0;
	for (COUNT_TYPE blockStart = 0; blockStart < selectedLength - FFTSize; blockStart += FFTSize - overlap)
	{
		COUNT_TYPE numSamplesInBlock = FFTSize;

		if (blockStart + FFTSize > selectedLength){
			if (selectedLength > blockStart)
				numSamplesInBlock = selectedLength - blockStart;
			else
				numSamplesInBlock = 0;
		}
			

		if (numSamplesInBlock)
		{
			Vector frame;
			out.GetSubVector(blockStart, numSamplesInBlock, frame);
#ifdef _PRINT_DEBUG_STEP
			PRINT(INFORMATION, "\n -PROCESS FRAM : %d", time);
#endif
#ifdef _PRINT_DEBUG
			PRINT(DATA | NONE_TIME, "\n\nDATA FRAME : %d", time);
			for (COUNT_TYPE i = 0; i < FFTSize; i++){
				PRINT(DATA | NONE_TIME, " - %d  %2.6f", i, frame[i]);
			}
			PRINT(DATA | NONE_TIME, "\n\nEnd DATA FRAME\n");
#endif
			mfcc(frame);
#ifdef _PRINT_DEBUG_STEP
			PRINT(INFORMATION, "\n -END FRAM : %d", time);
#endif
			time++;
		}
	}

	if (_includeEnergy){
		PRINT(STEP, "ADDING ENERGY\n");
		nceptrums += 1;
	}

	if (normalizeMFCC){
		PRINT(STEP, "NORMALIZE MFCC\n");
		DATA_TYPE max = mfccSpectrum.Max();
		DATA_TYPE min = mfccSpectrum.Min();
		NormalizeMFCC(max, min);
	}

	if (standardization){
		PRINT(STEP, "Standardization MFCC\n");
		Standardization();
	}

	ProcessDelta();

	ProcessDoubleDelta();

	PRINT(STEP, "Processed Completed\n");
	PRINT(STEP, "Number Vec MFCC = %d - Size Vec = %d\n", mfccSpectrum.ColumnSize(), mfccSpectrum.RowSize());
	PRINT(STEP, "Max Value: %5.5f - Min Value: %5.5f\n", mfccSpectrum.Max(), mfccSpectrum.Min());

	return processDone;

}
void MFCC::mfcc(Vector &frame){
	COUNT_TYPE FFTSize = frameSize;
	if (frame.Size() < frameSize){
		frame.Resize(frameSize);
	}
#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "PROCESS FRAME:\n");
#endif
#ifdef _PRINT_DEBUG 
	PRINT(DATA | NONE_TIME, "DATA FRAME :\n");
	for (COUNT_TYPE i = 0; i < FFTSize; i++){
		PRINT(DATA | NONE_TIME, "- %d :  %2.6f", i, frame[i]);
	}
#endif
	// Calculate Energy
	DATA_TYPE energy = Energy(frame);
#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "WINDOW FRAME:\n");
#endif
	// Aply Window
	Vector windowed_frame(FFTSize);
	Window(frame, windowed_frame);
#ifdef _PRINT_DEBUG 
	PRINT(DATA | NONE_TIME, "WINDOW FRAME:\n");
	for (COUNT_TYPE i = 0; i < FFTSize; i++){
		PRINT(DATA | NONE_TIME, " %2.6f ", windowed_frame[i]);
	}
#endif

#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "PROCESSING FFT :\n");
#endif
	// Create Image data while processing FFT
	Vector img(FFTSize);
	FFTUtil::FFT(windowed_frame, img);
#ifdef _PRINT_DEBUG_STEP
	PRINT(DATA | NONE_TIME, "FFT z space\n");
	for (COUNT_TYPE i = 0; i < FFTSize; i++){
		PRINT(DATA | NONE_TIME, "%3.5f + %3.5f i  ", windowed_frame[i], img[i]);
	}
	PRINT(DATA | NONE_TIME, "\n");
#endif

#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "PROCESSING ABS:\n");
#endif
	
#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "FFT Complex Data:\n");
	for (COUNT_TYPE i = 0; i < FFTSize; i++){
		PRINT(DATA | NONE_TIME, "%3.5f + %3.5f i ", windowed_frame[i], img[i]);
	}
#endif
	Vector absData(FFTSize);
	FFTUtil::ABS(windowed_frame, img, absData); // ABS frequency
#ifdef _PRINT_DEBUG 
	PRINT(DETAIL, "ABS :\n");
	for (COUNT_TYPE i = 0; i < FFTSize/2; i++){
		PRINT(DATA | NONE_TIME, "- %d :  %.4f", i, absData[i]);
	}
#endif
	Vector fre_spectrum;
	absData.GetSubVector(0, FFTSize / 2, fre_spectrum);
	PRINT(DATA, "Spectrum Sau Khi Get\n");
	fre_spectrum.Print();
	PRINT(DATA, "Min Freq = %f, Max Freq = %f\n", fre_spectrum.Max(), fre_spectrum.Min());
	//fre_spectrum *= DATA_TYPE(2);
	fre_spectrum.Normalize(5.0);

	// Put frequency spectrum to frame
	freqSpectrum.PlushBackColumn(fre_spectrum);


#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "PROCESSING FILLTER TRIANGLE SPECTRUM :\n");
#endif
	Vector fre_powerDB(fre_spectrum);
	Vector coff_filter = h->Filter(fre_powerDB);

	
#ifdef _PRINT_DEBUG 			
	PRINT(DATA | NONE_TIME, "OUTPUT MEL FILTER: \n");
	for (COUNT_TYPE k = 0; k < n_filters; k++){
		PRINT(DATA | NONE_TIME, " %f", coff_filter[k]);
	}
#endif

#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "PROCESSING LOG :\n");
#endif
	h->Log(coff_filter); // Log

#ifdef _PRINT_DEBUG 			
	PRINT(DATA | NONE_TIME, "OUTPUT LOG MEL FILTER:\n");
	for (COUNT_TYPE k = 0; k < n_filters; k++){
		PRINT(DATA | NONE_TIME, " %f", coff_filter[k]);
	}
#endif
	bandFilterSpectrum.PlushBackColumn(coff_filter);


#ifdef _PRINT_DEBUG_STEP
	PRINT(DETAIL, "PROCESSING DCT MFCC :\n");
#endif

	Vector ceps = d->Process(coff_filter);// DCT

	if (_includeEnergy){
		ceps.Resize(nceptrums + 1);
		ceps[nceptrums] = energy;
	}
#ifdef _PRINT_DEBUG 			
	PRINT(DATA | NONE_TIME, "OUTPUT Ceps:\n");
	for (COUNT_TYPE k = 0; k < nceptrums + 1; k++){
		PRINT(DATA | NONE_TIME, " %f", ceps[k]);
	}
#endif
	mfccSpectrum.PlushBackColumn(ceps);
}
MFCC::~MFCC(){
	if (selectedData != NULL) { delete[] selectedData; selectedData = NULL; }
	if (w != NULL) { delete[] w; w = NULL; }
	if (h != NULL) { delete h; h = NULL; }
	if (d != NULL) { delete d; d = NULL; }
}


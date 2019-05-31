#ifndef EXTRACTION_MFCC_H
#define EXTRACTION_MFCC_H
#include <Common.h>
#include <stdio.h>
#include <vector>
#include <Util\DCT.h>
#include "WavFile.h"
#include <Util\Vector.h>
#include <Util\Matrix.h>
class HMel {
	COUNT_TYPE bnfilters;
	COUNT_TYPE samplerate;
	COUNT_TYPE fftsize;
	COUNT_TYPE lfilter;

	DATA_TYPE flo;
	DATA_TYPE fhi;
	DATA_TYPE fmax;

	Array3D h;
public:
	HMel(COUNT_TYPE nbfilters/*So loc*/, COUNT_TYPE samplerate, COUNT_TYPE fftsize, DATA_TYPE flo, DATA_TYPE fhi){
		this->bnfilters = nbfilters;
		this->samplerate = samplerate;
		this->fftsize = fftsize;
		this->lfilter = fftsize / 2;
		this->fhi = fhi;
		this->flo = flo;
		this->fmax = (DATA_TYPE)samplerate / 2; // Dinh ly lay mau
		InitFilterBand();
	}
	DATA_TYPE * getH(COUNT_TYPE bandnum);
	Vector Filter(Vector &frame);

	Vector &Log(Vector &frame);
	COUNT_TYPE nFilter();
	~HMel();
private:
	void InitFilterBand(DATA_TYPE band = 0);
	DATA_TYPE MelScale(DATA_TYPE freqHZ);
	DATA_TYPE HezBack(DATA_TYPE freqMel);
	COUNT_TYPE FretoBin(COUNT_TYPE sampleRate, COUNT_TYPE NFFTSize, DATA_TYPE fre);
};

class MFCC{
	HMel *h;
	Dct *d;
	WavFile *wav;

	bool usetime; // Use config time

	DATA_TYPE *selectedData;
	COUNT_TYPE selectedLength;

	DATA_TYPE *w; // Window
	DATA_TYPE flo;
	DATA_TYPE fhi;
	COUNT_TYPE n_filters;
	COUNT_TYPE nceptrums;
	COUNT_TYPE ndeltaCep;
	COUNT_TYPE ndoubleCep;
	COUNT_TYPE sampleRate;

	DATA_TYPE timeframe;
	DATA_TYPE timeshift;

	COUNT_TYPE frameSize;
	COUNT_TYPE overlap;
	bool _includeEnergy;
	bool validFile;
	bool processDone;

	COUNT_TYPE ndelta;

	Matrix freqSpectrum;
	Matrix bandFilterSpectrum;
	Matrix mfccSpectrum;
	Matrix deltaMfccSpectrum;
	Matrix doubleDeltaMfccSpectrum;

	bool normalizeMFCC;
	DATA_TYPE meanNormalizeMFCC;
	DATA_TYPE covarianceNormalizeMFCC;


	bool standardization;
public:
	MFCC(WavFile *wav,
		COUNT_TYPE frameSize,
		COUNT_TYPE overlap,
		COUNT_TYPE n_filters,
		DATA_TYPE flo,
		DATA_TYPE fhi,
		COUNT_TYPE nceptrums,
		COUNT_TYPE ndelta = 2,
		bool includeEnergy = true
		);

	MFCC(
		WavFile *wav,
		DATA_TYPE timeframe,
		DATA_TYPE timeshift,
		COUNT_TYPE n_filters,
		DATA_TYPE flo,
		DATA_TYPE fhi,
		COUNT_TYPE nceptrums,
		COUNT_TYPE ndelta = 2,
		bool includeEnergy = true
		);
	COUNT_TYPE Overlap();
	COUNT_TYPE FrameSize();
	bool IsValid(){
		return validFile;
	}
	// Get state of process
	bool ProcessDone();
	// Processing method
	bool Process();
	void ProcessDelta();
	void ProcessDoubleDelta();

	// Info about frequency spectrum
	COUNT_TYPE FreqRow();
	Matrix &Freq(){
		return freqSpectrum;
	};
	// Info about Band filter spectrum
	COUNT_TYPE BandFilterRow();
	Matrix BandFilter(){
		return bandFilterSpectrum;
	}
	// info about mfcc spectrum
	COUNT_TYPE MFCCRow();
	Matrix Mfcc(){
		return mfccSpectrum;
	};
	// info about delta mfcc spectrum
	COUNT_TYPE DeltaMFCCRow();
	Matrix DeltaMfcc(){
		return deltaMfccSpectrum;
	};
	// info about double delta mfcc spectrum
	COUNT_TYPE DoubleDeltaMFCCRow();
	Matrix DoubleDeltaMFCC(){
		return doubleDeltaMfccSpectrum;
	};
	void UseStandardization();
	void UseNormalizeMFCC(DATA_TYPE mean, DATA_TYPE var);

	bool SaveFile(char *path);
	bool SaveMFCC(char *path);
	bool SaveDeltaMFCC(char *path);
	bool SaveDoubleMFCC(char *path);

	~MFCC();
	//Method
private:
	void mfcc(Vector &frame);
	void initW();
	void Window(Vector &frame, Vector &windowed_frame);
	DATA_TYPE Energy(Vector &frame);
	void NormalizeEnergy(DATA_TYPE normal = 8.0f);
	Vector PreEmphasis(Vector &in);
	void Init();
	void NormalizeMFCC(DATA_TYPE max, DATA_TYPE min);
	void Standardization();

};
#endif

#include "Util\FFT.h"
#include <stdio.h>
#include "Util\LogUtil.h"
// FFT su dung giai thuat butter fly demension 2....
// Noted: Demension 2 chua toi uu  can su dung 

void FFTUtil::FFT(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE n, DATA_TYPE *tmpReal, DATA_TYPE *tmpImg)
	{
		if (n > 1) {			/* n <2 thi thoat ko de quy nua */
			COUNT_TYPE k, m;    DATA_TYPE zReal, zImg, wReal, wImg, *voReal, *veReal, *voImg, *veImg;
			voReal = tmpReal; veReal = tmpReal + n / 2;
			voImg = tmpImg; veImg = tmpImg + n / 2;
			// Don dong lai chan le .
			for (k = 0; k < n / 2; k++) {
				veImg[k] = img[2 * k];
				veReal[k] = real[2 * k];

				voReal[k] = real[2 * k + 1];
				voImg[k] = img[2 * k + 1];
			}

			FFT(veReal, veImg, n / 2, real, img);
			FFT(voReal, voImg, n / 2, real, img);
			for (m = 0; m < n / 2; m++) {
				// Noted: chua toi uu cho can fix ma tran w ... FFT se duoc goi nhieu lan viec tinh toan se lau .
				wReal = cos( (DATA_TYPE)(2 * PI*m / (DATA_TYPE)n) );
				wImg = -sin( (DATA_TYPE)(2 * PI*m / (DATA_TYPE)n) );
				zReal = wReal*voReal[m] - wImg*voImg[m];
				zImg = wReal*voImg[m] + wImg*voReal[m];

				real[m] = veReal[m] + zReal; // Canh tren
				img[m] = veImg[m] + zImg; // Canh tren

				real[m + n / 2] = veReal[m] - zReal; // Canh tren
				img[m + n / 2] = veImg[m] - zImg; // Canh tren

			}
		}

		return;
	}

	void FFTUtil::FFT(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE FFTSize){
		// Create Tmp Data before Calculate FFT
		DATA_TYPE* temReal = ArrayUtil::CreateZeroArray1D(FFTSize); 
		DATA_TYPE* temImg = ArrayUtil::CreateZeroArray1D(FFTSize);
		FFTUtil::FFT(real, img, FFTSize, temReal, temImg);// Lay FFT
		delete[] temReal;
		delete[] temImg;
	}

	void FFTUtil::FFT(Vector &real, Vector &img){

		COUNT_TYPE FFTSize = real.Size();
		// Create Tmp Data before Calculate FFT
		DATA_TYPE* temReal = ArrayUtil::CreateZeroArray1D(FFTSize);
		DATA_TYPE* temImg = ArrayUtil::CreateZeroArray1D(FFTSize);
		FFTUtil::FFT(real.GetArray(), img.GetArray(), FFTSize, temReal, temImg);// Lay FFT
		delete[] temReal;
		delete[] temImg;
	}
	void FFTUtil::IFFT(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE n, DATA_TYPE *tmpReal, DATA_TYPE *tmpImg)
	{
		if (n > 1) {			/* otherwise, do nothing and return */
			COUNT_TYPE k, m;    DATA_TYPE zReal, zImg, wReal, wImg, *voReal, *veReal, *voImg, *veImg;
			voReal = tmpReal; veReal = tmpReal + n / 2;
			voImg = tmpImg; veImg = tmpImg + n / 2;
			for (k = 0; k < n / 2; k++) {
				veImg[k] = img[2 * k];
				veReal[k] = real[2 * k];

				voReal[k] = real[2 * k + 1];
				voImg[k] = img[2 * k + 1];
			}
			IFFT(veReal, veImg, n / 2, real, img);
			IFFT(voReal, voImg, n / 2, real, img);
			for (m = 0; m < n / 2; m++) {
				//Noted: Can tang performance -> fix thanh ma tran
				wReal = cos( (DATA_TYPE)( 2.0 * PI*m / (DATA_TYPE)n ) );
				wImg = sin( (DATA_TYPE)(2.0 * PI*m / (DATA_TYPE)n ) );
				zReal = wReal*voReal[m] - wImg*voImg[m];
				zImg = wReal*voImg[m] + wImg*voReal[m];

				real[m] = veReal[m] + zReal; // Canh tren
				img[m] = veImg[m] + zImg; // Canh tren

				real[m + n / 2] = veReal[m] - zReal; // Canh tren
				img[m + n / 2] = veImg[m] - zImg; // Canh tren
			}
		}
		return;
	}


	void FFTUtil::ABS(DATA_TYPE *real, DATA_TYPE *img, COUNT_TYPE n, DATA_TYPE* abs){
		
		for (COUNT_TYPE i = 0; i< n; i++){
			abs[i] = sqrt(real[i] * real[i] + img[i] * img[i]);
		}
	}

	void FFTUtil::ABS(Vector &real, Vector &img, Vector & abs){
		COUNT_TYPE n = real.Size();
		for (COUNT_TYPE i = 0; i< n; i++){
			abs[i] = sqrt(real[i] * real[i] + img[i] * img[i]);
		}
	}

	COUNT_TYPE FFTUtil::NextPower2(COUNT_TYPE timesample){
		COUNT_TYPE i = 0;
		for (; pow(2.0, i) < timesample; i++){}
		return (long)pow(2.0, i);
	}
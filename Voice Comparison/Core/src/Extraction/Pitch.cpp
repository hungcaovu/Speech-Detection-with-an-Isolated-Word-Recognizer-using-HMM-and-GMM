#include <Extraction\Pitch.h>
#include <string.h>
#include <Util\MathUtil.h>
#include <Util\MedianFilter.h>
#include <Util\LogUtil.h>

/*
Algorithm AMDF base on AutoColerration
*/
PitchAMDF::PitchAMDF(COUNT_TYPE sampleRate, COUNT_TYPE frameSize, DATA_TYPE F0min, DATA_TYPE F0max) {
	this->sampleRate = sampleRate;
	this->frameSize = frameSize;
	this->F0max = F0max;
	this->F0min = F0min;
	Initital();
}

DATA_TYPE PitchAMDF::PitchProcess(DATA_TYPE * frame){
	return AMDF(frame);
}

void PitchAMDF::Initital(){
		
		DATA_TYPE tmin = DATA_TYPE(1) / F0max;

		DATA_TYPE tmax = DATA_TYPE(1) / F0min;
		min_shift = COUNT_TYPE(tmin * sampleRate);
		max_shift = COUNT_TYPE(tmax * sampleRate);
		last_period = (DATA_TYPE)max_shift;
#ifdef _PRINT_DEBUG_AMDF_PITCH
		PRINT(STEP, "Initital AMDF Pitch\n");
		PRINT(INFORMATION, "Sample Rate %d\n Min Shift %d\n Max Shift %d\n", sampleRate, min_shift, max_shift);
#endif
	}


DATA_TYPE PitchAMDF::AMDF(DATA_TYPE * frame) {
		COUNT_TYPE buffer_index = 0;

		DATA_TYPE max_sum = 0;
		COUNT_TYPE period = 0;
		for (COUNT_TYPE shift = min_shift; shift < max_shift; shift++)
		{
			// Assigh higher weights to lower frequencies
			// and even higher to periods that are closer to the last period (quick temporal coherence hack)
			DATA_TYPE mod = (DATA_TYPE)(shift - min_shift) / (DATA_TYPE)(max_shift - min_shift);
			mod *= DATA_TYPE(1.0) - DATA_TYPE(1.0) / (DATA_TYPE(1.0) + (DATA_TYPE)abs((DATA_TYPE)shift - last_period));

			// Compare samples with shifted samples using autocorrelation
			DATA_TYPE dif = DATA_TYPE(0);
			for (COUNT_TYPE i = shift; i < frameSize; i++)
				dif += frame[i] * frame[i - shift];

			// Apply weight
			dif *= DATA_TYPE(1.0) + mod;
#ifdef _PRINT_DEBUG_AMDF_PITCH
			PRINT(INFORMATION, " [%d] = %3.7f ", shift, dif);
#endif
			if (dif > max_sum)
			{
				max_sum = dif;
				period = shift;
			}
		}

		if (period != 0)
		{
			last_period = DATA_TYPE(period);
			DATA_TYPE freq = DATA_TYPE(1.0) / (DATA_TYPE)period;
			freq *= (DATA_TYPE)sampleRate;
			buffer_index += period + min_shift;
#ifdef _PRINT_DEBUG_AMDF_PITCH
			PRINT(INFOMATION, "\n Freq = %3.7f \n", freq);
#endif
			return freq;
		}
		else {
			last_period = DATA_TYPE((max_shift + min_shift) / 2);
			buffer_index += min_shift;
#ifdef _PRINT_DEBUG_AMDF_PITCH
			Logger::Print("\n Freq = %3.7f \n", 0);
#endif
			return DATA_TYPE(-1);
		}
	}


/*
	YIN Pitch detector. Base on Auto Corlerration 
	http://audition.ens.fr/adc/pdf/2002_JASA_YIN.pdf
 */

PitchYIN::PitchYIN(COUNT_TYPE sampleRate, COUNT_TYPE frameSize, DATA_TYPE threshold)
{
	this->sampleRate = sampleRate;
	this->frameSize = frameSize;
	this->threshold = threshold;
	Initital();
#ifdef _PRINT_DEBUG_YIN_PITCH
	PRINT("PitchYIN Initalization \n Sample Rate %d \n Frame Size %d\n Window %d\n Threshold %4.5f\n", sampleRate, frameSize, window, threshold);
#endif
}

void PitchYIN::Initital(){
	this->window = frameSize / 2;
	this->probability = 0.0;
	buffer = ArrayUtil::CreateZeroArray1D(window);//new DATA_TYPE[window];
}

void PitchYIN::Difference(DATA_TYPE *frame){

#ifdef _PRINT_DEBUG_YIN_PITCH
	PRINT("***Difference\n");
	PRINT("Input Frame : %d\n", frameSize);
	for (COUNT_TYPE i = 0; i < frameSize; i++){
		PRINT(" [%d] = %3.7f  ", i, frame[i]);
	}
	PRINT("\n");
#endif
	DATA_TYPE delta = 0.0f;
	for (COUNT_TYPE tau = 0; tau < this->window; tau++){
		for (COUNT_TYPE i = 0; i < window; i++){
			delta = frame[i] - frame[i + tau];
			this->buffer[tau] += delta * delta;
		}
	}
#ifndef _PRINT_DEBUG_YIN_PITCH
	PRINT(DATA, "Output Frame : %d\n", window);
	for (COUNT_TYPE i = 0; i < window; i++){
		PRINT(DATA, " [%d] = %3.7f  ", i, buffer[i]);
	}
	PRINT(DATA, "\n");
#endif

}

void PitchYIN::MeanNormalizedDifference(){
	DATA_TYPE runningSum = 0;
	//this->buffer[0] = 1;

	for (COUNT_TYPE tau = 0; tau < window; tau++) {
		runningSum += buffer[tau];
		buffer[tau] *= DATA_TYPE(tau) / (runningSum);
	}

#ifndef _PRINT_DEBUG_YIN_PITCH
	PRINT(DATA,"***MeanNormalizedDifference\n");
	PRINT(DATA,"Output Frame : %d\n", window);
	for (COUNT_TYPE i = 0; i < window; i++){
		PRINT(DATA," [%d] = %3.7f  ", i, buffer[i]);
	}
	PRINT(DATA,"\n");
#endif
}

COUNT_TYPE PitchYIN::AbsoluteThreshold(){
	COUNT_TYPE tau;

	for (tau = 2; tau < window; tau++) {
		if (buffer[tau] < threshold) {
			while (tau + 1 < window && buffer[tau + 1] < buffer[tau]) {
				tau++;
			}
			probability = 1 - buffer[tau];
			break;
		}
	}

	if (tau == window || buffer[tau] >= threshold) {
		tau = -1;
		probability = 0;
	}

	return tau;
}

DATA_TYPE PitchYIN::ParabolicInterpolation(COUNT_TYPE tauEstimate){
	DATA_TYPE betterTau;
	COUNT_TYPE x0;
	COUNT_TYPE x2;

	if (tauEstimate < 1) {
		x0 = tauEstimate;
	}
	else {
		x0 = tauEstimate - 1;
	}

	if (tauEstimate + 1 < this->window) {
		x2 = tauEstimate + 1;
	}
	else {
		x2 = tauEstimate;
	}

	if (x0 == tauEstimate) {
		if (buffer[tauEstimate] <= buffer[x2]) {
			betterTau = DATA_TYPE(tauEstimate);
		}
		else {
			betterTau = DATA_TYPE(x2);
		}
	}
	else if (x2 == tauEstimate) {
		if (buffer[tauEstimate] <= buffer[x0]) {
			betterTau = DATA_TYPE(tauEstimate);
		}
		else {
			betterTau = DATA_TYPE(x0);
		}
	}
	else {
		DATA_TYPE s0, s1, s2;
		s0 = buffer[x0];
		s1 = buffer[tauEstimate];
		s2 = buffer[x2];
		betterTau = DATA_TYPE(tauEstimate) + (s2 - s0) / (2 * (2 * s1 - s2 - s0));
	}


	return betterTau;
}

DATA_TYPE PitchYIN::PitchProcess(DATA_TYPE *frame){
	COUNT_TYPE tauEstimate = -1;
	DATA_TYPE pitchInHertz = -1;
	this->probability = 0.0;
	ArrayUtil::SetZeroArray1D(buffer);
	memset(buffer, 0, sizeof(DATA_TYPE)* window);
	/* Step 1: Calculates the squared difference of the signal with a shifted version of itself. */
	Difference(frame);

	/* Step 2: Calculate the cumulative mean on the normalised difference calculated in step 1 */
	MeanNormalizedDifference();

	/* Step 3: Search through the normalised cumulative mean array and find values that are over the threshold */
	tauEstimate = AbsoluteThreshold();

	/* Step 5: Interpolate the shift value (tau) to improve the pitch estimate. */
	if (tauEstimate != -1){
		pitchInHertz = sampleRate / ParabolicInterpolation(tauEstimate);
	}
	else {
		pitchInHertz = DATA_TYPE(-1);
	}

	return pitchInHertz;
}

DATA_TYPE PitchYIN::Probability(){
	return probability;
}


std::vector<DATA_TYPE> PitchExtraction::Process(){
	m_smoothPitchs.clear();
	_posPitchs.clear();
	pitchs.clear();

	for (COUNT_TYPE blockStart = 0; blockStart < length - frameSize; blockStart += frameSize - overlap)
	{
		COUNT_TYPE numSamplesInBlock = frameSize;

		if (blockStart + frameSize > length)
			numSamplesInBlock = length - blockStart;

		if (numSamplesInBlock)
		{
			time++;
			DATA_TYPE *frame = ArrayUtil::CreateZeroArray1D(frameSize);
			memcpy(frame, &data[blockStart], numSamplesInBlock * sizeof(DATA_TYPE));
//#ifdef _PRINT_DEBUG_PITCH
//			PRINT("\n *DATA FRAME : %d", time);
//			for (COUNT_TYPE i = 0; i < frameSize; i++){
//				PRINT(" - %d  %2.6f", i, frame[i]);
//			}
//#endif
#ifdef _PRINT_DEBUG_PITCH
			PRINT("\n -PROCESS FRAM : %d Add Frame: %x", time, frame);
#endif
			DATA_TYPE pitch = p->PitchProcess(frame);
			delete[]frame;
			pitchs.push_back(pitch);
			_posPitchs.push_back(blockStart);
#ifdef _PRINT_DEBUG_PITCH
			PRINT("\n -Pitch : %3.7f", pitch);
			PRINT("\n -END FRAM : %d", time);
#endif
		}
	}

#ifdef _PRINT_DEBUG_PITCH
	PRINT("\n -Pitch Result :\n");
	for (COUNT_TYPE i = 0; i < pitchs.size(); i++){
		PRINT(" [%d] = %3.7f", i, pitchs[i]);
	}

	PRINT("\n -Pitch Result Smooth:\n");
	for (COUNT_TYPE i = 0; i < pitchsSmooth.size(); i++){
		PRINT(" [%d] = %3.7f", i, pitchs[i]);
	}
#endif

	if (m_dropUnPitch){
		COUNT_TYPE b = 0, e = pitchs.size() - 1;
		for (; b < pitchs.size() && pitchs[b] < DATA_TYPE(0); b++);
		for (; e > 0 && pitchs[e] < DATA_TYPE(0) && pitchs.size() > 0; e--);

		if (b < e && pitchs.size() > 0){
			std::vector<DATA_TYPE> p;
			for (COUNT_TYPE i = b; i <= e; i++){
				p.push_back(pitchs[i]);
			}
			pitchs = p;
		}
	}
	// Smooth with median filter
	if (m_usingMedian){
		DSP::MedianFilter1D<DATA_TYPE> median;
		median.SetWindowSize(m_medianWindowSize);
		m_smoothPitchs = median.Filter(pitchs);
	}
	else {
		m_smoothPitchs = pitchs;
	}
	

	return pitchs;
}

/* Program to measure and score the difference between   */
/* a student utterance against a teacher's reference     */

#include "fixdpt_fp.h"   
#include "paras_fp.h"  

#if RUN4PC  
#include   
#include   
#endif  

/*global variables*/

#if RUN4PC     
/* declare array only for PC run */
SHORT TeacherSpeech[MAX_LEN];
SHORT StudentSpeech[MAX_LEN];

/** each frame, 0..OUTNUM-1 --> MFCC, NO logEnergy **/
FLOAT TeacherFeature[MAX_FRM][OUTNUM];
FLOAT StudentFeature[MAX_FRM][OUTNUM];
#else    
/* for chip-run, both are pointers to flash mem */
SHORT *TeacherSpeech;
SHORT *StudentSpeech;
FLOAT *TeacherFeature;
FLOAT *StudentFeature;
#endif  

/* Global variables */
INT   av1;
INT   av2;

#if RUN4PC  
/* declare array only for PC run */
SHORT freq1[MAX_FRM];
char voicedv1[MAX_FRM];
SHORT  freq2[MAX_FRM];
char voicedv2[MAX_FRM];
#else  
/* for chip-run,  make the following point to flash mem */
SHORT  *freq1;
char *voicedv1;
SHORT  *freq2;
char *voicedv2;
#endif  

/* Global variables for DTW */
#if RUN4PC  
SHORT   from[MAX_GRID*(2 * BOUND / 4)];   /** only 2-bit for each from direction **/
#else  
/* for chip-run,  make the following point to flash mem */
SHORT   *from;
#endif  

/* Computation Profiling Analysis */
#if CPA_F   
CPA COUNT = { 0l, 0l };

PrintCOUNT(char *s, int n)
{
	printf("%s\n", s);
	printf("Add = %ld    multy = %ld\n", COUNT.addition / n, COUNT.multy / n);
}
#endif  

/*-------------------------------------*/
/* Fixed point arithmatic functions    */
/*-------------------------------------*/

FLOAT mult(FLOAT x, FLOAT y)
{
	INT res;   /* res needs to be 32-bit */

	res = x * y;

#if CPA_F  
	COUNT.addition += 3;
	COUNT.multy++;
#endif  

	if (res>(INT)0x02000000) return  MAX_16;
	if (res
		res >>= FPN;

	return (FLOAT)res;
}

FLOAT divi(FLOAT x, FLOAT y)
{
	INT num, den;    /* res needs to be 32-bit */

	num = ((INT)x)                      den = (INT)y;

#if CPA_F  
	COUNT.addition++;
	COUNT.multy++;
#endif  

	return (FLOAT)(num / den);
}

SHORT extract_h(INT L_var1)
{
	SHORT var_out;

	var_out = (SHORT)(L_var1 >> 16);

	return(var_out);
}


SHORT extract_l(INT L_var1)
{
	SHORT var_out;

	var_out = (SHORT)L_var1;

	return(var_out);
}

SHORT saturate(INT L_var1)
{
	SHORT var_out;

	if (L_var1 > 0X00007fffL)
	{
		var_out = MAX_16;
	}
	else if (L_var1 < (INT)0xffff8000L)
	{
		var_out = MIN_16;
	}
	else
	{
		var_out = extract_l(L_var1);
	}

	return(var_out);
}

SHORT add(SHORT var1, SHORT var2)
{
	SHORT var_out;
	INT L_sum;

	L_sum = (INT)var1 + var2;
	var_out = saturate(L_sum);

	return(var_out);
}

SHORT shl(SHORT var1, SHORT var2)
{
	SHORT var_out;
	INT result;

	if (var2 < 0)
	{
		var_out = shr(var1, (SHORT)(-var2));
	}
	else
	{
		result = (INT)var1 * ((INT)1                         if ((var2 > 15 && var1 != 0) || (result != (INT)((SHORT)result)))
		{
			var_out = (var1 > 0) ? MAX_16 : MIN_16;
		}
		else
		{
			var_out = extract_l(result);
		}
	}
	return(var_out);
}

SHORT shr(SHORT var1, SHORT var2)
{
	SHORT var_out;

	if (var2 < 0)
	{
		var_out = shl(var1, (SHORT)(-var2));
	}
	else
	{
		if (var2 >= 15)
		{
			var_out = (var1 < 0) ? -1 : 0;
		}
		else
		{
			if (var1 < 0)
			{
				var_out = ~((~var1) >> var2);
			}
			else
			{
				var_out = var1 >> var2;
			}
		}
	}
	return(var_out);
}

SHORT sub(SHORT var1, SHORT var2)
{
	SHORT var_out;
	INT L_diff;

	L_diff = (INT)var1 - var2;
	var_out = saturate(L_diff);

	return(var_out);
}

INT L_mult(SHORT var1, SHORT var2)
{
	INT L_var_out;

	L_var_out = (INT)var1 * (INT)var2;
	if (L_var_out != (INT)0x40000000L)
	{
		L_var_out *= 2;
	}
	else
	{
		L_var_out = MAX_32;
	}

	return(L_var_out);
}

INT L_msu(INT L_var3, SHORT var1, SHORT var2)
{
	INT L_var_out;
	INT L_product;

	L_product = L_mult(var1, var2);

	L_var_out = L_sub(L_var3, L_product);

	return(L_var_out);
}

INT L_sub(INT L_var1, INT L_var2)
{
	INT L_var_out;

	L_var_out = L_var1 - L_var2;

	if (((L_var1 ^ L_var2) & MIN_32) != 0)
	{
		if ((L_var_out ^ L_var1) & MIN_32)
		{
			L_var_out = (L_var1 < 0L) ? MIN_32 : MAX_32;
		}
	}
	return(L_var_out);
}

INT L_shl(INT L_var1, SHORT var2)
{
	INT L_var_out;

	if (var2{
		L_var_out = L_shr(L_var1, (SHORT)(-var2));
	}
	else
	{
		for (; var2>0; var2--)
		{
			if (L_var1 > (INT)0X3fffffffL)
			{
				L_var_out = MAX_32;
				break;
			}
			else
			{
				if (L_var1 < (INT)0xc0000000L)
				{
					L_var_out = MIN_32;
					break;
				}
			}
			L_var1 *= 2;
			L_var_out = L_var1;
		}
	}
	return(L_var_out);
}

INT L_shr(INT L_var1, SHORT var2)
{
	INT L_var_out;

	if (var2 < 0)
	{
		L_var_out = L_shl(L_var1, (SHORT)(-var2));
	}
	else
	{
		if (var2 >= 31)
		{
			L_var_out = (L_var1 < 0L) ? -1 : 0;
		}
		else
		{
			if (L_var1{
				L_var_out = ~((~L_var1) >> var2);
			}
			else
			{
				L_var_out = L_var1 >> var2;
			}
		}
	}
	return(L_var_out);
}

INT L_deposit_h(SHORT var1)
{
	INT L_var_out;

	L_var_out = (INT)var1                      return(L_var_out);
}

SHORT norm_l(INT L_var1)
{
	SHORT var_out;

	if (L_var1 == 0)
	{
		var_out = 0;
	}
	else
	{
		if (L_var1 == (INT)0xffffffffL)
		{
			var_out = 31;
		}
		else
		{
			if (L_var1 < 0)
			{
				L_var1 = ~L_var1;
			}

			for (var_out = 0; L_var1 < (INT)0x40000000L; var_out++)
			{
				L_var1
			}
		}
	}
	return(var_out);
}

void Log2(
	INT L_x,       /* (i)  : input value                                 */
	SHORT *exponant, /* (o)  : Integer part of Log2.   (range: 0                    SHORT *fraction  /* (o)  : Fractionnal part of Log2. (range: 0                    )
					 {
					 SHORT exp, i, a, tmp;
					 INT L_y;

					 if( L_x                     {
					 *exponant = 0;
					 *fraction = 0;
					 return;
					 }

					 exp = norm_l(L_x);
					 L_x = L_shl(L_x, exp );               /* L_x is normalized */

					 *exponant = sub(30, exp);

L_x = L_shr(L_x, 9);
i = extract_h(L_x);                 /* Extract b25-b31 */
L_x = L_shr(L_x, 1);
a = extract_l(L_x);                 /* Extract b10-b24 of fraction */
a = (SHORT)(a & (SHORT)0x7fff);

i = sub(i, 32);

L_y = L_deposit_h(log2table[i]);          /* table[i]                       tmp = sub(log2table[i], log2table[i+1]);  /* table[i] - table[i+1] */
L_y = L_msu(L_y, tmp, a);                 /* L_y -= tmp*a*2        */

*fraction = extract_h(L_y);

return;
}

INT xinvsqrt(INT L_x)
{
	SHORT exp, i, a, tmp;
	INT L_y;

	if (L_x                       return ((INT)0x3fffffffL);
}

exp = norm_l(L_x);
L_x = L_shl(L_x, exp);
exp = sub(30, exp);
if ((exp & 1) == 0)
L_x = L_shr(L_x, 1);
exp = shr(exp, 1);
exp = add(exp, 1);
L_x = L_shr(L_x, 9);
i = extract_h(L_x);
L_x = L_shr(L_x, 1);
a = extract_l(L_x);
a = (SHORT)(a & (SHORT)0x7fff);
i = sub(i, 16);
L_y = L_deposit_h(xinvtable[i]);
tmp = sub(xinvtable[i], xinvtable[i + 1]);
L_y = L_msu(L_y, tmp, a);
L_y = L_shr(L_y, exp);

return(L_y);
}

/*************************************************************/
/** module for speech endpoints detection (based on energy) **/
/*************************************************************/

INT FrameEnergy(SHORT *smp, INT frm)
{
	INT j, e, s;

	e = 0;
	for (j = 0; j                        s = (INT)smp[frm*FRAME_SHIFT + j];
		e += (s*s) >> 8;    /** >>8 == /FRAME_SIZE  -> do it here to avoid overflow **/

#if CPA_F  
		COUNT.addition += 5;
	COUNT.multy += 2;
#endif   

}
return e;
}

/** energy-based end-pointing: detect beginning and end of speech **/
/** return # of speech frames **/

INT EndPointer(SHORT *smp, INT n_frames, INT *start, INT *end)
{
	INT frm, onset;
	INT e, noise_eng;

	/** use first 10 frames to cal noise energy **/
	for (frm = 0, noise_eng = 0; frm                        noise_eng += FrameEnergy(smp, frm);
		noise_eng /= 10;

#if PRINT && RUN4PC && 0  
		printf("Noise Energy at speech beginning: %f per frame\n", (float)noise_eng);
#endif  

	noise_eng *= ABOVE_NOISE_LEVEL;

	onset = 0;  *start = 0;
	for (frm = 0; frm                       e = FrameEnergy(smp, frm);
		if (e >= noise_eng) onset++;
		else onset = 0;

	if (onset>MIN_CONSEC_FRMS) {
		/* speech beginning detected */
		*start = frm - BACKOFF_FRMS;
		if (*start                       break;
	}
}

/* detect speech ending point */
for (frm = n_frames - 1, noise_eng = 0; frm >= n_frames - 10; frm--)
noise_eng += FrameEnergy(smp, frm);
noise_eng /= 10;

#if PRINT && RUN4PC && 0  
printf("Noise Energy at speech end: %f per frame\n", (float)noise_eng);
#endif  

noise_eng *= ABOVE_NOISE_LEVEL;

onset = 0;  *end = n_frames;
for (frm = n_frames - 1; frm >= 0; frm--) {
	e = FrameEnergy(smp, frm);
	if (e >= noise_eng) onset++;
	else onset = 0;

	if (onset>MIN_CONSEC_FRMS) {
		/* speech ending detected */
		*end = frm + BACKOFF_FRMS;
		if (*end>n_frames) *end = n_frames;
		break;
	}
}

#if PRINT && RUN4PC   
printf("Speech ending points detected at (frm): start=%d end=%d  (total %d frms)\n",
	(int)*start, (int)*end, (int)(*end - *start + 1));
#endif  

return *end - *start + 1;
}

#if RUN4PC  

#define WAVHEAD 120  

/** read  speech (PCM) from WAV file if running in PC **/
/* return num of samples read from file */
INT readwav(char *fn, SHORT *smp)
{
	FILE *fp1;
	INT len;

	fp1 = fopen(fn, "rb");
	if (fp1 == NULL) {
#if PRINT  
		printf("Error: can't find the file %s\n", fn);
#endif  
		exit(1);
	}
	fseek(fp1, 0l, SEEK_END);
	len = (ftell(fp1) - WAVHEAD) / 2;  /** skip Wav header **/

	if (len > MAX_LEN) {
#if PRINT  
		printf("file too long, truncating to %d[sec] !!\n", (int)(MAX_LEN / SAMPRATE));
#endif  
		len = MAX_LEN;
	}
	fseek(fp1, (long)WAVHEAD, SEEK_SET);  /** skip Wav header **/
	fread(smp, 2, len, fp1);
	fclose(fp1);

	return len;
}

#endif  

/*****************************************************/
/** Module to code speech into mcep                 **/
/*****************************************************/

void FFT(INT *s)
{
	INT ii, jj, n, nn, limit, m, j, inc, i;
	FLOAT wr, wi;
	INT xre, xri;
	FLOAT theta;

	n = FRAME_SIZE;
	nn = n / 2; j = 1;
	for (ii = 1; ii                            i = 2 * ii - 1;
		if (j>i) {
			xre = s[j]; xri = s[j + 1];
			s[j] = s[i];  s[j + 1] = s[i + 1];
			s[i] = xre; s[i + 1] = xri;
		}
	m = n / 2;
	while (m >= 2 && j > m) {
		j -= m; m /= 2;
#if CPA_F  
		COUNT.addition++;
		COUNT.multy++;
#endif   
	}
	j += m;
}
limit = 2;
while (limit < n) {
	inc = 2 * limit; theta = PIx2 / limit;

	for (ii = 1; ii                               m = 2 * ii - 1;
		for (jj = 0; jj                                 i = m + jj * inc;
			j = i + limit;

			nn = n / (2 * limit)*(ii - 1);
	wi = WI[nn];    wr = WR[nn];

	xre = (wr*s[j] - wi*s[j + 1]) >> 10;
	xri = (wr*s[j + 1] + wi*s[j]) >> 10;

	s[j] = s[i] - xre; s[j + 1] = s[i + 1] - xri;
	s[i] = s[i] + xre; s[i + 1] = s[i + 1] + xri;
#if CPA_F  
	COUNT.addition += 27;
	COUNT.multy += 8;
#endif   
}
		}
		limit = inc;
	}
}

/* HCode: main function to extract mcep features */
INT HCode(SHORT *smp, INT n_frames, FLOAT out[][OUTNUM], INT grid_size)
{
	INT i, j, k, bin;
	INT f[2 * FRAME_SIZE + 2];
	INT bins[PORDER + 2];
	INT enk;
	INT t1, t2;
	FLOAT min, max;
	INT frm, outfrm, skipfrm;
	FLOAT skip;

	skip = divi(n_frames, grid_size);

	for (frm = 0, outfrm = 0; frm                            skipfrm = (skip*frm) >> FPN;   /*Modified referece code ????? */

		/* copy new frame */
		for (j = 0; j                             f[j + 1] = (INT)smp[skipfrm*FRAME_SHIFT + j];      /* scale down in fixed-point domain */
			for (j = FRAME_SIZE; j
				/** pre-emph frame **/
				for (j = FRAME_SIZE; j >= 2; j--)  f[j] -= (f[j - 1] * FLT_0_97) >> FPN;
	f[1] = (f[1] * FLT_0_03) >> FPN;

	/** HAMMING WINDOWS **/
	for (j = 0; j>FPN;
		for (j = FRAME_SIZE / 2; j>FPN;

#if CPA_F  
			COUNT.addition += 13 * FRAME_SIZE;
	COUNT.multy += 3 * FRAME_SIZE;
#endif   

	/* FFT */
	FFT(f);

	/** calculate log-spectrum **/
	for (j = 0; j                         bin = 0;
		for (k = 2; k                               t1 = f[2 * k - 1];
			t2 = f[2 * k];

			/* 46340 * 46340 = MAX_32 */
			if (t1 > 46340 || t1 < -46340 || t2 > 46340 || t2 < -46340) {
				t1 >>= 5;  t2 >>= 5;    /*if t1 or t2 too big, scale down to calculate square */
				enk = t1*t1 + t2*t2;

				/*  enk = (int) sqrt((double)(enk)); */
				enk = INVSQRT / xinvsqrt(enk);

				if (enk < (INT)0x4000000) enk                                   else enk = MAX_32;
			}
			else {
				t1 *= t1;
				t2 *= t2;
				if (t1>MAX_32 - t2)
					enk = 46340;
				else {
					enk = t1 + t2;
					/*enk = (int) sqrt((double)(enk));*/
					enk = INVSQRT / xinvsqrt(enk);
				}
			}

			if (k == binbnd[bin]) bin++;

			if (enk>10;
			else t1 = (INT)lowt[k - 1] * (enk >> 10);

			if (bin>0) bins[bin] += t1;
			if (bin                 #if CPA_F
				COUNT.addition += 16;
			COUNT.multy += 6;
#endif        
}

for (bin = 1; bin                             t1 = bins[bin];
	if (t1
		/*bins[bin] = (INT)(log((double)t1)*1024.0) ;*/
		Log2(t1, &max, &min);

/** merge max and min into 1.5.10 fixed point format **/
if (max>31) max = MAX_16;
else if (max                                else max = (max5)& (SHORT)0x7ff);

bins[bin] = (INT)mult(max, (FLOAT)0x2c5);   /* /log(2.0) --> covert log2() into log() */
		}

		for (k = 1; k                             t1 = 0;
			for (j = 1; j                                 if (bins[j]>32767) bins[j] = 32767;
				if (bins[j]                                 t1 += bins[j] * (INT)DCT[k - 1][j - 1];  /* DCT[][] is 1.0.15 FP format */
#if CPA_F  
					COUNT.addition += 7;
		COUNT.multy += 1;
#endif                
			}
			out[outfrm][k - 1] = (FLOAT)(t1 >> 15);   /** out[][] becomes 1.5.10 FP format */
		}
	}
	return n_frames;
}

INT Normalize_Pitch(SHORT *pitch, char *v, INT size)
{
	INT i, c;
	INT t, m, s;

	m = s = 0; c = 0;

	for (i = 0; i                         if (v[i]) {
		m += (INT)pitch[i];   /** pitch[] here is 1.13.2 FP format **/
			c++;
#if CPA_F  
		COUNT.addition += 3;
#endif   
	}
}
m /= c;

for (i = 0; i                        if (v[i]) {
	t = (INT)pitch[i] - m;
		s += t*t;
#if CPA_F  
	COUNT.addition += 3;
	COUNT.multy += 1;
#endif    
}
s /= (c - 1);

/*s = sqrt((double) s);*/
s = INVSQRT / xinvsqrt(s);

#if RUN4PC && PRINT && 0  
printf("Pitch Norm = %f %f\n", m / 4.0, s / 4.0);
#endif  

for (i = 0; i                          t = (INT)pitch[i] - m;
	pitch[i] = (FLOAT)((t                                     /**                    #if CPA_F
															  COUNT.addition += 4 ;
															  COUNT.multy += 1 ;
															  #endif
															  }
															  return size ;
															  }

															  INT CalcPitchScore(INT j1, INT j2)
															  {
															  FLOAT ft, ftd ;
															  INT s ;

															  ft =  (freq1[j1]-freq2[j2]);
															  ft = (ft>0) ? ft : -ft ;

															  ftd = ((freq1[j1]-freq1[j1-1]) - (freq2[j2]-freq2[j2-1])) ;
															  ftd = (ftd>0) ? ftd : -ftd ;

															  s = ft/PW1 + ftd/PW2 + PW3 * mult(ft,ftd) ;

															  if( !voicedv1[j1] || !voicedv2[j2] ) s /= PW4 ;

															  #if RUN4PC && PRINT && 0
															  printf("i=%d j=%d f1=%.2f(%d) f2=%.2f(%d) ft=%.2f ftd=%.2f s=%.2f\n", j1, j2,
															  freq1[j1]/1024.0, voicedv1[j1], freq2[j2]/1024.0, voicedv2[j2], ft/1024.0,
															  ftd/1024.0, s/1024.0) ;
															  #endif
															  #if CPA_F
															  COUNT.addition += 13 ;
															  COUNT.multy += 3 ;
															  #endif
															  return s ;
															  }

															  /********************************************************/
															  /**  Module to align two utterances via DTW            **/
															  /********************************************************/

															  /** Module to do DTW to align two utterance **/
															  /** always use square grid  **/

															  /* cals dist between two frames - euclid dist between two lpc vectors */
															  INT euclid_dist(FLOAT *vec1, FLOAT *vec2)
{
	SHORT i;
	INT s = 0, d;

	for (i = 0; i                        d = (INT)(vec1[i] - vec2[i]);  /** vec1[] vec2[] is 1.5.10 FB format */
		s += (d*d) >> 10;
#if CPA_F  
		COUNT.addition += 5;
	COUNT.multy += 1;
#endif   
}
return(s);
}

/** for a given column, calc legal low and high bounds
depending on slopes and ends relaxation **/
/*******
. . . . X
. . 1 2 .
. . . 3 .
. . . . .

*******/

/* the index function used to access the squeezed arrays */
#define INDEX(i,j)  (((i)+3)%3)*(2*BOUND)+((j)-(i)+BOUND)  
#define SetFrom(i,j,val)  { SHORT t; int k,l; k=(i)*(2*BOUND/4)+((j)-(i)+BOUND)/4; l=(((j)-(i)+BOUND+4)%4)*2;  t=from[k]; t &= (~(3                 #define GetFrom(i,j)   3 & (from[(i)*(2*BOUND/4)+((j)-(i)+BOUND)/4]>>((((j)-(i)+BOUND+4)%4)*2))   

/* main align procedure */
FLOAT align(INT grid_size, INT *align_x, INT *align_y)
{
	INT i_col, i_row, chosen_step;
	INT min, max;
	INT  s, align_score;
	INT  score[3 * (2 * BOUND)], t, tmp;
	INT   local[3 * (2 * BOUND)];

	*align_x = *align_y = grid_size - 1;
	align_score = MAX_32;
	chosen_step = 0;

	for (i_row = 0; i_row                          local[i_row] = score[i_row] = MAX_32;

		/** The first column **/
		for (i_row = 0; i_row                      local[INDEX(0, i_row)] = s = euclid_dist(&(TeacherFeature[0][0]), &(StudentFeature[i_row][0]));
			score[INDEX(0, i_row)] = s * (i_row + 1);
			SetFrom(0, i_row, FROM_SELF);
}

/* loop on columns */
for (i_col = 1; i_col
	/* get legal grid points, upper & lower bounds for each column */
	min = i_col - BOUND;
if (min                    max = i_col + BOUND - 1;
if (max >= grid_size)  max = grid_size - 1;

/* loop on rows */
for (i_row = min; i_row                         t = tmp = MAX_32;
	/* calc local scores for legal grid points. */
	local[INDEX(i_col, i_row)] = euclid_dist(&(TeacherFeature[i_col][0]), &(StudentFeature[i_row][0]));

#if RUN4PC && PRINT && 0  
printf("col=%d row=%d local=%f\n", i_col, i_row, local[INDEX(i_col, i_row)] / 1024.0);
#endif        
/* now find the best path to this point */
/* Skip one frame in x-axis */
if (i_col >= 2 && i_row                              tmp = score[INDEX(i_col - 2, i_row - 1)] +
	(local[INDEX(i_col - 2, i_row - 1)] + local[INDEX(i_col - 1, i_row)])*PATH2_COST;
chosen_step = FROM_LEFT;
}
if (score[INDEX(i_col - 1, i_row - 1)] < MAX_32)
	t = score[INDEX(i_col - 1, i_row - 1)] + PATH1_COST*local[INDEX(i_col - 1, i_row - 1)];
if (t                         tmp = t;
chosen_step = FROM_MID;
	  }
	  if (i_row >= 2 && i_row>i_col - BOUND && score[INDEX(i_col - 1, i_row - 2)]                           t = score[INDEX(i_col - 1, i_row - 2)] +
		  (local[INDEX(i_col - 1, i_row - 2)] + local[INDEX(i_col, i_row - 1)])*PATH2_COST;
		  }
		  if (t                         tmp = t;
		  chosen_step = FROM_RIGHT;
	  }

	  if (tmp < MAX_32) {
		  score[INDEX(i_col, i_row)] = tmp;
		  SetFrom(i_col, i_row, chosen_step);
	  }

#if RUN4PC && PRINT && 0  
	  if (PRINT) printf("col=%d row=%d local=%.3f score=%.3f step=%d\n", i_col, i_row, local[INDEX(i_col, i_row)] / 1024.0, score[INDEX(i_col, i_row)] / 1024.0, chosen_step);
#endif  

	  /* if in a legal end point */
	  if ((i_col == grid_size - 1 && (grid_size - i_row)< EDGE_RELAX) ||
		  (i_row == grid_size - 1 && (grid_size - i_col)< EDGE_RELAX)) {
		  /* if not the upper right corner, add to score to unbias short paths */
		  if (i_col == grid_size - 1) s = (grid_size - i_row) * local[INDEX(i_col, i_row)];
		  if (i_row == grid_size - 1) s = (grid_size - i_col) * local[INDEX(i_col, i_row)];
		  if (align_score > score[INDEX(i_col, i_row)] + s) {
			  align_score = score[INDEX(i_col, i_row)] + s;
			  *align_x = i_col;
			  *align_y = i_row;
		  }
	  }
#if CPA_F  
	  COUNT.addition += 100;
	  COUNT.multy += 26;
#endif        

	}
  }
  return((FLOAT)(align_score / (grid_size * 2)));
}

/* go backwards and find path */
FLOAT backtrack(INT last_x, INT last_y, INT grid_size, INT len1, INT len2)
{
	INT  x, y, path_len = 0, step;
	INT pscore = 0;
	FLOAT skip1, skip2;

	skip1 = divi(len1, grid_size);
	skip2 = divi(len2, grid_size);

	x = last_x;
	y = last_y;

	while (x>0) {
		path_len++;
		step = GetFrom(x, y);

		pscore += CalcPitchScore((x*skip1) >> 10, (y*skip2) >> 10);
#if RUN4PC && PRINT && 0  
		printf("optimal path: %d - %d (%d) %f %f \n", x, y, grid_size, pscore / 1024.0, CalcPitchScore(mult(x, skip1), mult(y, skip2)) / 1024.0);
#endif  

		switch (step) {
		case FROM_LEFT:
			x -= 2;
			y -= 1;
			break;
		case FROM_MID:
			x--;
			y--;
			break;
		case FROM_RIGHT:
			x--;
			y -= 2;
			break;
		case FROM_SELF:
			x--;
			break;
		default:
			x = 0;
			break;
		}
#if CPA_F  
		COUNT.addition += 13;
		COUNT.multy += 4;
#endif   
	}
	return (FLOAT)(PWEI*pscore / path_len);
}

/****************************************************/
/** Module to extract pitch contour                **/
/****************************************************/

/* calc correlation - samples are in vec filt, summation is performed over len
samples, advancing step samples each time, and for the pitch value period. sq
is a vec of the squares of filt, enrg is the average energy in the checked
part */
FLOAT calc_corr(INT len, INT period, INT step, SHORT *filt)
{
	INT ss, x, y;
	INT i;
	INT xi, xj;
	FLOAT cor;

	ss = x = y = 0;

	for (i = 0; i                       xi = (INT)filt[i];
		xj = (INT)filt[period + i];
		ss += xi*xj / len;
	x += xi*xi / len;
	y += xj*xj / len;
#if CPA_F  
	COUNT.addition += 9;
	COUNT.multy += 6;
#endif   
}

if (x                     return FLT_0_0;

if (ss >= (INT)0x1fffff || ss                       cor = mult((FLOAT)(ss / (x >> FPN)), (FLOAT)(ss / (y >> FPN)));
else
cor = mult((FLOAT)((ss                                      /** now cor becomes 1.5.10 FP format */

if (ss
	return cor;
}


/** find pitch - first pass of pitch finding.
first - check if energy and variance are morhe than a threshold value. if not -
decide unvoiced.
then check there is a zero crossing inside the allowed oitch period. if not - uv.
Set range of allowed pitch values to full range, or to a smaller range around the
last pitch value (if was found).
Calculate the correlation (low resolution) for the allowed pitch values. if a peak
is found, choose it.
check correlation values near the peak with high resolution, and choose the pitch
which gives the best correlation value.
check that the correlation value is more than a threshold.
Allow another check - to find if a doubling or halving occured (this is needed
because otherwise in the next step we will look only around the found pitch value,
and errors could be recoverd from only in the second pass).
last - prepare for a check - the correlation values for several multipliers.
**/
INT find_pitch(INT frm_num, SHORT *filt, FLOAT *thresh, INT *last_voiced, INT *last_pitch, INT *limit_range)
{
	INT  i, j, chosen_pitch, a, b, voiced;
	char possible = 1;
	FLOAT s, best_corr;

	if (possible) {

		/* a and b are min and max possible pitch. if limit_range, then range is limited
		around prev pitch value */
		if (*limit_range && *last_pitch>0) {
			a = *last_pitch * 2 / 3;
			if (a                       b = *last_pitch * 3 / 2;
			if (b>MAX_F0) b = MAX_F0;
		}
		else {
			a = MIN_F0;
			b = MAX_F0;
		}

		/* there must be at least two zero crossing in period */
		i = 0;     j = 1;
		while (j < b && i                          if ((INT)filt[j - 1] * filt[j + 1]                        j++;
	}
	if (j > a) a = j;
	chosen_pitch = -1;
	if (a >= b)  possible = 0;
}

if (possible) {
	best_corr = -FLT_2_0;
	for (j = a; j                            s = calc_corr(j, j, STEP1, filt);

		if (j >= a + 3 && jFLT_0_0 &&  s>best_corr) {
			chosen_pitch = j;
			best_corr = s;
		}
}
if (chosen_pitch == -1) possible = 0;
  }

  if (possible) {
	  /* last, check again around best candidate, using best resolution */
	  best_corr = -FLT_2_0;
	  a = chosen_pitch - 4;
	  if (a                       b = chosen_pitch + 4;
	  if (b>MAX_F0) b = MAX_F0;
	  chosen_pitch = 0;
	  for (j = a; j                       s = calc_corr(j, j, 1, filt);
		  if (s>best_corr) {
			  chosen_pitch = j;
			  best_corr = s;
		  }
  }
  }
  if (!possible)  best_corr = FLT_0_0;

  /* there used to be a moving threshold, but now this is disabled (TMIN=TMAX=THIGH),
  as we use here a  stricter threshold, and later use the extending proc with
  a lower thresh */

  if (*last_voiced) {
	  if (best_corr                         voiced = 0;
	  *thresh = TMIN;
  }
  else {
	  *thresh = mult(TMAX, best_corr);
	  if (*thresh                         voiced = 1;
  }
  }
  else {
	  if (best_corr>*thresh) {
		  voiced = 1;
		  *thresh = TMIN;
	  }
	  else {
		  voiced = 0;
		  *thresh = THIGH;
	  }
  }
  *limit_range = (*limit_range + 1) % SEQ;
  if (!voiced) *limit_range = 0;
  *last_voiced = voiced;

  if (chosen_pitch                    *last_pitch = chosen_pitch;
  return(chosen_pitch);
}


/** simple algorithm to remove multiples in pitch contour **/
/** A simple method to use three states to implement **/

#define ABS(x) ((x>0)? (x) : -(x))  

void remove_double(SHORT *freq, char *voicedv, INT from, INT to)
{
	INT cur, i;
	INT pools[3];
	INT count[3];
	SHORT last;
	FLOAT y, s[3];

	pools[0] = pools[1] = pools[2] = 0;
	count[0] = count[1] = count[2] = 0;
	cur = 1;
	last = freq[from];

	for (i = from; i                          if (freq[i] == 0) freq[i] = last;
		y = divi(freq[i], last);
		if (y > FLT_1_35) {  /* counted as double */
			cur++;
			if (cur>2) cur = 2;
		}
		else if (y < FLT_0_75) { /*counted as half */
			cur--;
			if (cur
		}

		pools[cur] += (INT)freq[i];
		count[cur] ++;

		if (freq[i]>0) last = freq[i];
}

for (i = 0; i                       if (count[i]) pools[i] /= count[i];
else pools[i] = 10000;   /** 10000 magic number, do we have better one **/
  }

  if (count[2]>count[1] && count[2]>count[0]) {
	  s[0] = FLT_4_0; s[1] = FLT_2_0; s[2] = FLT_1_0;
	  y = divi(pools[2], pools[1]);
	  if (ABS(y - FLT_2_0)>ABS(y - FLT_3_0))  s[1] = FLT_3_0;
  }
  else if (count[1]>count[0] && count[1]>count[2]) {
	  s[0] = FLT_2_0; s[1] = FLT_1_0; s[2] = FLT_0_5;
	  y = divi(pools[1], pools[2]);
	  if (ABS(y - FLT_0_33)                          y = divi(pools[1], pools[0]);
	  if (ABS(y - FLT_3_0) < ABS(y - FLT_2_0)) s[0] = FLT_3_0;
  }
  else {
	  s[0] = FLT_1_0; s[1] = FLT_0_5; s[2] = FLT_0_25;
	  y = divi(pools[0], pools[1]);
	  if (ABS(y - FLT_0_33)
  }

  last = freq[from];
  cur = 1;
  for (i = from; i                        y = divi(freq[i], last);

	  if (y>FLT_1_35) {  /* counted as double */
		  cur++;
		  if (cur>2) cur = 2;
	  }
	  else if (y                           cur--;
	  if (cur                       }
	  if (freq[i]>0) last = freq[i];
	  freq[i] = mult(freq[i], s[cur]);
   }
}

void interpolate_unvoiced(SHORT *freq, char * voicedv, INT len, INT /*FLOAT*/ av)
{
	SHORT   i, j, n;
	SHORT   l, m;

	/* for unvoiced parts, set freq to be linear interpolation between
	surrounding voiced */
	i = j = 0;
	while (i                      while (i                        if (i>j) {
		l = freq[j];
		if (!voicedv[j]) l = av;
		m = freq[i];
		if (!voicedv[i]) m = av;
		if (j == 0) l = m;
		if (i >= len - 2) m = l;
		if (i>j)
			for (n = j; n                             freq[n] = (SHORT)(l + (n - j)*(m - l) / (i - j));
#if CPA_F  
				COUNT.addition += 15;
		COUNT.multy += 2;
#endif   
	}
	while (i                        j = i - 1;
}
}

/* remove short and isolated voiced or unvoiced parts, calc average file freq,
call the smoothing and remove doubling */
void rect_f0(SHORT *freq, char *v, INT len, SHORT *filt_all, INT  *av)
{
	INT i, j, l, k;

	/* calc first estimate of file freq avrg */
	*av = 0;  j = 0;
	for (i = 1; i                       if (v[i]) {
		*av += (INT)freq[i];
			j++;
	}
	if (j == 0) {
		*av = 100;
		return;
	}
	*av /= j;

#if RUN4PC && PRINT && 0   
	if (PRINT) printf("av %d : %d\n", j, *av);
#endif  

	/* remove short uv parts (1 or 2 or 3) inside v parts */
	for (i = 2; i                       if (v[i] == 0 && v[i + 1] == 1 && v[i - 1] == 1 && (v[i - 2] == 1 || v[i + 2] == 1)) v[i] = 1;

		for (i = 2; i                       j = 0;   l = 0;
			for (k = i - 2; k                       if (v[i + 2] == 1 && (j >= 3 && v[i + 3] == 1)) { v[i + 1] = 1; l = 1; }
			else  if (v[i + 3] == 1 && (j>3 && v[i + 4] == 1)) { v[i + 1] = 1; v[i + 2] = 1; l = 1; }
			if (!l) v[i] = 0;
}

/* remove isolated voiced - if only one frame in 5 is voieced - turn it off */
for (i = 3; i                       if (v[i]) {
	j = 0;
		for (k = -2; k                            if (j
}

/* for each voiced part - remove doubling and halving */
j = 0;
while (j                          while (v[j] == 0 && j                         k = j;
while (k                        if (k                         remove_double(freq, v, j, k - 1);
	  }
	  j = k;
	}

	/* again - remove short uv parts (1 or 2) inside v parts */
	for (i = 0; i                       if (v[i + 2] == 1) v[i + 1] = 1;
	else  if (v[i + 3] == 1)
	{
		v[i + 1] = 1; v[i + 2] = 1;
	}
	}
}

/* fulp(): main function to extract picture contour of a speech utterance */
/** input: speech PCM in smp in num of frames (len) **/
/* smp --> beginning of speech */
INT fulp(SHORT *smp, INT len, char *voicedv, SHORT *freq, INT *av)
{
	INT     i, k, e;
	FLOAT   thresh = TMIN;
	INT     last_voiced = 0, last_pitch, limit_range = 0;

	/* find pitch */
	for (i = 0; i                       k = find_pitch(i, &(smp[i*FRAME_SHIFT]), &thresh, &last_voiced, &last_pitch, &limit_range);

		voicedv[i] = last_voiced;
	if (!last_voiced || k == 0) { freq[i] = 0; voicedv[i] = 0; }
	else freq[i] = (SHORT)((SAMPRATE
}

freq[0] = 0; voicedv[0] = 0;
voicedv[len - 1] = 0;

rect_f0(freq, voicedv, len, smp, av);

interpolate_unvoiced(freq, voicedv, len, *av);

#if RUN4PC && PRINT && 0  
for (i = 0; i                        printf("Freq[%d]=%f (%d)\r\n", i, freq[i] / 4.0, voicedv[i]);
#endif  
}

/****************************************/
/* main function to calculate the score */
/****************************************/

int main(int argc, char **argv)
{
	INT TeaLen, TeaStart, TeaEnd;
	INT StdLen, StdStart, StdEnd;
	INT     grid_size;
	FLOAT   align_score, p_score;
#if CPA_F  
	INT duration;
#endif    

#if RUN4PC  
	TeaLen = readwav(argv[1], TeacherSpeech);
	StdLen = readwav(argv[2], StudentSpeech);
#else  
	/* for running in DSP chips */
	/* initialize TeacherSpeech to teacher's PCM */
	/* initialize StudentSpeech to student's PCM */

	/* ADD HERE */

#endif  

#if CPA_F  
	duration = (StdLen>TeaLen ? StdLen : TeaLen) / SAMPRATE;
#endif  

	/* Endpointing */
	TeaLen = EndPointer(TeacherSpeech, TeaLen / FRAME_SHIFT + 1, &TeaStart, &TeaEnd);
	StdLen = EndPointer(StudentSpeech, StdLen / FRAME_SHIFT + 1, &StdStart, &StdEnd);

#if CPA_F && 0  
	PrintCOUNT("Endpointing", duration);
#endif  

	/* det grid size to be minimum of file length and MAX_GRID */
	if (StdLen                      else grid_size = TeaLen;
	if (grid_size>MAX_GRID)      grid_size = MAX_GRID;

	/** Pitch extraction **/
	fulp(&TeacherSpeech[TeaStart*FRAME_SHIFT], TeaLen, voicedv1, freq1, &av1);
	fulp(&StudentSpeech[StdStart*FRAME_SHIFT], StdLen, voicedv2, freq2, &av2);

	Normalize_Pitch(freq1, voicedv1, TeaLen);
	Normalize_Pitch(freq2, voicedv2, StdLen);

#if CPA_F && 0  
	PrintCOUNT("Pitch Extraction", duration);
#endif  

	/** MFCC extraction **/
	HCode(&TeacherSpeech[TeaStart*FRAME_SHIFT], TeaLen, TeacherFeature, grid_size);
	HCode(&StudentSpeech[StdStart*FRAME_SHIFT], StdLen, StudentFeature, grid_size);

#if CPA_F && 0  
	PrintCOUNT("MFCC", duration);
#endif  


	/**  DTW  **/
	align_score = align(grid_size, &TeaStart, &StdStart);

	/** Calculate pitch score during backtrack **/
	p_score = backtrack(TeaStart, StdStart, grid_size, TeaLen, StdLen);

#if RUN4PC && PRINT  
	printf("\nscore1 = %.2f   score2 = %.2f\n", align_score / 1024.0, p_score / 1024.0);

	printf("\nTotal score = %.2f      (the smaller the better)\n\n", (align_score + p_score) / 1024.0);
	printf("     < 3.0      --> excellent\n");
	printf("     3.0-4.0    --> very good\n");
	printf("     4.0-5.5    --> good\n");
	printf("     5.5-7.0    --> poor\n");
	printf("     > 7.0      --> very bad\n\n");
#endif  

#if CPA_F  
	PrintCOUNT("Totally", duration);
#endif  

}
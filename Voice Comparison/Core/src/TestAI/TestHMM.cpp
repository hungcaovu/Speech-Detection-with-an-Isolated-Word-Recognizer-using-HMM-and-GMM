#include <Util\Matrix.h>
#include <AI\Clustering.h>
#include <AI\DTW.h>
#include <AI\GMM.h>
#include <AI\HMM.h>

#define TEST_VITERBI_
#define TEST_FORWARD_

class TestHMM{
	HMM hmm;
public :
	void TestNewSet(){
		// SU dung vidu link
		// http://www.indiana.edu/~iulg/moss/hmmcalculations.pdf
		// Test bai toan ForWard
		hmm._stateNum = 2;
		hmm._mode = TEST;
		Vector init(2);
		init[0] = 0.85;
		init[1] = 0.15;
		hmm._vPInitState = init;
		Matrix tran(2, 2 );
		tran(0, 0) = 0.3; tran(0, 1) = 0.7;
		tran(1, 0) = 0.1; tran(1, 1) = 0.9;

		hmm._mPTranState = tran;
		Matrix obser(2, 2);
		obser(0, 0) = 0.4;	 obser(0, 1) = 0.6;    
		obser(1, 0) = 0.5;   obser(1, 1) = 0.5;    

		hmm._mPObserver = obser;
#if 0
		VectorIdx ob(3);
		ob(0) = 1;
		ob(1) = 0;
		ob(2) = 1;
#endif
#if 1
		VectorIdx ob(4);
		ob(0) = 0;
		ob(1) = 1;
		ob(2) = 1;
		ob(3) = 0;
#endif

		hmm._mIdxObser = ob;
		Matrix alpha;
		Matrix beta;
		Matrix mObser(1, 4);
		Vector scalar;
		//hmm.ForWard(mObser, alpha, scalar);
		//hmm.BackWard(mObser, alpha, scalar);
		DATA_TYPE prob1 = hmm.ForWard(mObser, alpha);
		DATA_TYPE prob2 = hmm.BackWard(mObser, beta);
		std::vector<Matrix> xi;
		//hmm.ComputeXi(mObser,alpha, beta,  xi);
		std::vector<Matrix> gamma;
		hmm.ComputeGamma(obser, alpha, beta, gamma);
	}
#ifdef TEST_FORWARD
	void TestForWard(){
		// SU dung vidu link
		// http://www.cs.rochester.edu/u/james/CSC248/Lec11.pdf
		// Test bai toan ForWard
		hmm._stateNum = 2;
		hmm._mode = TEST;
		Vector init(2);
		init[0] = 0.8;
		init[1] = 0.2;
		hmm._vPInitState = init;
		Matrix tran(2, 2 );
		tran(0, 0) = 0.6; tran(0, 1) = 0.4;
		tran(1, 0) = 0.3; tran(1, 1) = 0.7;

		hmm._mPTranState = tran;
		Matrix obser(2, 3);
		obser(0, 0) = 0.3;	 obser(0, 1) = 0.4;    obser(0, 2) = 0.3;
		obser(1, 0) = 0.4;   obser(1, 1) = 0.3;    obser(1, 2) = 0.3;
		
		hmm._mPObserver = obser;
		VectorIdx ob(4);
		ob(0) = 0;
		ob(1) = 1;
		ob(2) = 2;
		ob(3) = 2;

		hmm._mIdxObser = ob;
		Matrix alpha;
		Matrix beta;
		Matrix mObser(1, 4);
		DATA_TYPE prob1 = hmm.ForWard(mObser, alpha);
		DATA_TYPE prob2 = hmm.BackWard(mObser, beta);
	}
#endif
#ifdef TEST_VITERBI
	void TestViterbi(){
		hmm._stateNum = 3;
		hmm._mode = TEST;
		Vector init(3);
		init[0] = 0.25;
		init[1] = 0.5;
		init[2] = 0.25;
		hmm._vPInitState = init;
		Matrix tran(3, 3 );
		tran(0, 0) = 0.25; tran(0, 1) = 0.5;tran(0, 2) = 0.25;
		tran(1, 0) = 0.25; tran(1, 1) = 0.25; tran(1, 2) = 0.5;
		tran(2, 0) = 0.5; tran(2, 1) = 0.5; tran(2, 2) = 0.0;
		hmm._mPTranState = tran;
		Matrix obser(3, 4);
		obser(0, 0) = 1; obser(0, 1) = 0; obser(0, 2) = 0; obser(0, 3) = 0;
		obser(1, 0) = 0.25; obser(1, 1) = 0.5; obser(1, 2) = 0; obser(1, 3) = 0.25;
		obser(2, 0) = 0.25; obser(2, 1) = 0.25; obser(2, 2) = 0.25; obser(2, 3) = 0.25;
		hmm._mPObserver = obser;
		VectorIdx ob(5);
		ob(0) = 1;
		ob(1) = 1;
		ob(2) = 1;
		ob(3) = 1;
		ob(4) = 2;
		hmm._mIdxObser = ob;
		VectorIdx state;
		Matrix mObser(1, 5);
		DATA_TYPE prob = hmm.Viterbi(mObser, state);
		DATA_TYPE lprob = hmm.LogViterbi(mObser, state);
	};
#endif
};

void main(){
	Logger::SetEnabled(true);
#if 0
	std::vector<std::string> files;
	files.push_back();
	files.push_back();
	files.push_back();
	files.push_back();
	files.push_back();
	files.push_back();


	HMM hmm(2, 3, COV_TYPE_02);


	hmm.Trainning();
#endif
#if 0
	Matrix a(1, 18);
	a(0, 0) = 1;
	a(0, 1) = 2;
	a(0, 2) = 3;
	a(0, 3) = 4;
	a(0, 4) = 5;
	a(0, 5) = 6;
	a(0, 6) = 7;
	a(0, 7) = 8;
	a(0, 8) = 9;
	a(0, 9) = 10;
	a(0, 10) = 11;
	a(0, 11) = 12;
	a(0, 12) = 13;
	a(0, 13) = 14;
	a(0, 14) = 15;
	a(0, 15) = 16;
	a(0, 16) = 17;
	a(0, 17) = 18;
	a(0, 18) = 19;
	a(0, 19) = 20;
	a(0, 20) = 21;
	a(0, 21) = 22;
	a(0, 22) = 23;
	a(0, 23) = 24;
	std::vector<Matrix> o;
	o.push_back(a);
	o.push_back(a);
	o.push_back(a);
	o.push_back(a);

	HMM hmm(2, 3, COV_TYPE_02);
	hmm.Trainning(o);

	for (COUNT_TYPE i = 0; i < o.size(); i++){
		DATA_TYPE prob = 0.0;
		VectorIdx tract;
		prob = hmm.LogViterbi(o[i], tract);
		printf("log %d = %f \n", i, prob);
	}
#endif
#ifdef TEST_VITERBI
	TestHMM test;
#endif
#ifdef TEST_BACKWARD
	TestHMM test;
	test.TestNewSet();
#endif
#ifdef TEST_FORWARD
	TestHMM test; 
	test.TestForWard();
#endif
}
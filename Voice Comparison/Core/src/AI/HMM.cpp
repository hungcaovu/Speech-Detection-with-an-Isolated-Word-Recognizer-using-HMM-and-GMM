#include <AI\HMM.h>
#include <Util\MatrixIdx.h>
#include <Util\LogUtil.h>
HMM::HMM(char * path){
	PRINT(STEP, "Create HMM: Load file %s", path);
	Load(path);
}

HMM::HMM(COUNT_TYPE stateNum, COUNT_TYPE gmmConponent, COUNT_TYPE gmmCoVar){
	_stateNum = stateNum;
	_gmmComponent = gmmConponent;
	_gmmCoVar = gmmCoVar;
	_mode = EXCUTE;
	_threshhold = DATA_TYPE(1e-6);
	PRINT(STEP, "Create HMM: State - %d Components = %d GMM CoVar\n", _stateNum, gmmConponent, _gmmCoVar);
	InitHMM();
}

void HMM::InitHMM(){
	_maxIterator = 500;
	// Init state vector
	_vPInitState.Resize(_stateNum);
	_vPInitState.Zero();
	_vPInitState[0] = 1.0f;
	// Init matrix transition
	_mPTranState.Resize(_stateNum, _stateNum, false);
	for (COUNT_TYPE i = 0; i < _stateNum - 1; i++){
		_mPTranState(i, i) = 0.5;
		_mPTranState(i, i+1) = 0.5;
	}
	_mPTranState(_stateNum - 1, _stateNum - 1) = 1;
	PRINT(DATA, "Vector Init:\n");
	_vPInitState.Print();
	PRINT(DATA, "Tran State Matrix:\n");
	_mPTranState.Print();

	for (COUNT_TYPE i = 0; i < _stateNum; i++){
		_gStateModel.push_back(GMM(_gmmComponent, _gmmCoVar));
	}

	for (COUNT_TYPE i = 0; i < _stateNum; i++){
		_vMSegementWords.push_back(Matrix());
	}
}

DATA_TYPE HMM::LogProbability(std::string file){
	PRINT(STEP, "Compute LogProbability: %s\n", file.c_str());
	Matrix mfcc;
	bool loaded = mfcc.Load(file.c_str());
	if (loaded){
		return LogProbability(mfcc);
	}
	else {
		return 0;
	}
}
DATA_TYPE HMM::LogProbability(Matrix obsers){
	PRINT(STEP, "Compute LogProbability: Row %d, Col %d\n", obsers.RowSize(), obsers.ColumnSize());
	VectorIdx state;
	return LogViterbi(obsers, state);
}
std::vector<Matrix> HMM::SegementWord(Matrix &mObser) {
	std::vector<Matrix> result;
	COUNT_TYPE obserSize = mObser.ColumnSize();
	COUNT_TYPE apartSize = obserSize / _stateNum;

	if (apartSize > 0){
		COUNT_TYPE start = 0;
		for (COUNT_TYPE state = 0; state < _stateNum - 1; state++){
			result.push_back(mObser.SubMatrixCol(start, start + apartSize - 1));
			start += apartSize;
		}
		result.push_back(mObser.SubMatrixCol(start));
	}

	return result;
}

/*void HMM::TrainForGMM(Matrix &mObser){
	std::vector<Matrix> data = SegementWord(mObser);
	for (COUNT_TYPE i = 0; i < _stateNum; i++){
		PRINT("HMM**********************************************Train GMM state %d \n", i);
		GMM &gmm = _gStateModel[i];
		Matrix &dataState = data[i];
		dataState.Print();
		bool result = gmm.DoEM(dataState);
		if (result) printf("State : %d Do EM OK\n", i);
		gmm.Print();
		PRINT("HMM**********************************************END Train GMM state %d \n", i);
	}
}
*/

bool HMM::LoadData(std::vector<std::string> &files){
	COUNT_TYPE numFiles = files.size();
	_vMDatas.clear();
	PRINT(STEP, "Load Data For Training HMM\n");
	if (numFiles > 0){
		// Load Files
		for (COUNT_TYPE i = 0; i < numFiles; i++){
			std::string &file = files[i];
			Matrix mfcc;
			bool loaded = mfcc.Load(file.c_str());
			_vMDatas.push_back(mfcc);
#if 1
			PRINT(STEP, "Loaded MFCC: %s --- %s Col Size = %d\n", file.c_str(), (loaded) ? "Ok" : "Failed", mfcc.ColumnSize());
#endif
		}
	}
	return true;
}

bool HMM::Trainning(std::vector<std::string> files){
	// Load data
	PRINT(STEP, "**********HMM Start Training****************\n");
	LoadData(files);
	return Trainning();
}

bool HMM::Trainning(std::vector<Matrix> obsers){
	_vMDatas = obsers;
	return Trainning();
}

bool HMM::Trainning(){
	if(_vMDatas.size() == 0 ) return false;
#ifdef PRINT_DEBUG_HMM_TRAIN_FOR_GMM
	PRINT(STEP, "Init Training for GMM \n");
#endif
	for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
		std::vector<Matrix> segement = SegementWord(_vMDatas[f]);
		for (COUNT_TYPE i = 0; i < _stateNum; i++){
			_vMSegementWords[i].AppendColumn(segement[i]);
		}
	}
#if 0
	PRINT(DATA, "Data In Segement\n");
	for (COUNT_TYPE i = 0; i < _stateNum; i++){
		PRINT(DATA, " Seg %d:\n", i+1);
		_vMSegementWords[i].Print();
	}
#endif
	// Trainning for GMM
	PRINT(STEP, "Training GMM\n");
	bool res = true;
	for (COUNT_TYPE i = 0; i < _stateNum; i++){
		PRINT(STEP, "Start Training GMM State: %d\n", i);
		bool trained = _gStateModel[i].DoEM(_vMSegementWords[i]);
		res &= trained;
		PRINT(DATA, "GMM Model for HMM State %d\n", i);
#if 0
		PRINT(DATA, "Data:\n");
		_vMSegementWords[i].Print();
#endif
		_gStateModel[i].Print();
		//PRINT(DATA, "GMM Trainning for state: %d --- %s \n", i, (trained) ? "Ok" : "Failed");
		PRINT(STEP, "Completed GMM Trainning for state: %d --- %s \n", i, (trained) ? "Ok" : "Failed");

	}
#ifdef PRINT_DEBUG_HMM_TRAIN_FOR_GMM
	PRINT(STEP, "Finish Training for GMM ----- %s\n", (res)? "Ok": "Failed");
	Print();
#endif
	PRINT(STEP, "********************Start Training for HMM********************\n");
	Vector pro;
	COUNT_TYPE iter = 0;
	while (iter < _maxIterator){
		PRINT(STEP, "****************COMPUTE HMM ITER_HMM_%d****************\n", iter);
		pro.PushBack(0);
		DATA_TYPE totalProb = 0.0;
		for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
			Matrix &data = _vMDatas[f];
			VectorIdx tract;

			DATA_TYPE prob = LogViterbi(data, tract);
			//PRINT(STEP, " Log Prob File {%d} = %.10f\n", f, prob);
			totalProb += prob;
		}
		pro[iter] = totalProb;
		BaumWeclch();
		PRINT(STEP, "HMM Iter = %d P(O|HMM) = %.10f\n", iter, pro[iter]);
		if (iter > 0 && abs((pro[iter] - pro[iter - 1]) / pro[iter]) < _threshhold){
			PRINT(STEP, "*************** HMM Training Completed - Aborted at iter %d:\n", iter);
//			PRINT(DATA, "HMM Training Completed - Aborted at iter %d:\n", iter);
//			pro.Print();

			PRINT(INFORMATION, "HMM Component:\n");
			Print(INFORMATION);
		
			return true;
		}
		else if (iter > _maxIterator){
			PRINT(STEP, "HMM  Unconverage\n");
			return false;
		}
		Print(INFORMATION);
		PRINT(STEP, "************COMPUTE HMM ITER_HMM_%d COMPLETED*************\n", iter);
		iter++;
	}
	return true;
}

void HMM::ComputeXi(Matrix &obser, Matrix &alpha, Matrix &beta, std::vector<Matrix> &xi){
	xi.clear();
	COUNT_TYPE obserSize = obser.ColumnSize();
	for (COUNT_TYPE t = 0; t < obserSize - 1; t++){
		xi.push_back(Matrix());
		Matrix &xit = xi[t];
		xit.Resize(_stateNum, _stateNum);
		DATA_TYPE factor = beta.GetColumn(t) * alpha.GetColumn(t);
		DATA_TYPE sum = 0.0;
		// Tinh ma tran Si
		for (COUNT_TYPE i = 0; i< _stateNum; i++){
			for (COUNT_TYPE j = 0; j < _stateNum; j++){
				if (_mode == TEST){
					DATA_TYPE da = alpha(i, t);
					DATA_TYPE be = beta(j, t + 1);
					DATA_TYPE tran = _mPTranState(i, j);
					COUNT_TYPE idx = _mIdxObser[t + 1];
					DATA_TYPE pO = _mPObserver(j, idx);
					xit(i, j) = da * be * tran * pO;
					sum += xit(i, j);
				} else {
					DATA_TYPE dom = alpha(i, t) * beta(j, t + 1)
						* _mPTranState(i, j)
						*ProbabilityObserver(obser.GetColumn(t + 1), j);
					xit(i, j) = dom;
					sum += dom;
				}
				
			}
		}
		// Chia cho factor
		for (COUNT_TYPE i = 0; i < _stateNum; i++)
			for (COUNT_TYPE j = 0; j < _stateNum; j++)
				xit(i, j) /= factor;
		PRINT(DATA, "Xi %d\n", t);
		xit.Print();
	}
}

void HMM::ComputeXi(Matrix &obser, Matrix &alpha, Matrix &beta, Vector &scalar, std::vector<Matrix> &xi){
	xi.clear();
	PRINT(DETAIL, "*************Compute Xi***************\n");
	COUNT_TYPE obserSize = obser.ColumnSize();
	for (COUNT_TYPE t = 0; t < obserSize - 1; t++){
		xi.push_back(Matrix());
		Matrix &xit = xi[t];
		xit.Resize(_stateNum, _stateNum);
		DATA_TYPE factor = beta.GetColumn(t) * alpha.GetColumn(t);
		// Tinh ma tran Si
		for (COUNT_TYPE i = 0; i< _stateNum; i++){
			for (COUNT_TYPE j = 0; j < _stateNum; j++){

				if (_mode == TEST){
					DATA_TYPE da = alpha(i, t);
					DATA_TYPE be = beta(j, t + 1);
					DATA_TYPE tran = _mPTranState(i, j);
					COUNT_TYPE idx = _mIdxObser[t + 1];
					DATA_TYPE pO = _mPObserver(j, idx);
					xit(i, j) = da * be * tran * pO;
				} else {
					DATA_TYPE dom = alpha(i, t) * beta(j, t + 1)
						* _mPTranState(i, j)
						*ProbabilityObserver(obser.GetColumn(t + 1), j);
					xit(i, j) = dom;
				}
			}
		}
		// Chia cho factor
		for (COUNT_TYPE i = 0; i < _stateNum; i++)
			for (COUNT_TYPE j = 0; j < _stateNum; j++)
				xit(i, j) *= scalar[t] / factor;
		PRINT(DATA, "Xi %d\n", t);
		xit.Print();
	}
	PRINT(DETAIL, "*************Compute Xi Completed***************\n");
}

void HMM::ComputeGamma(Matrix &obser, Matrix &alpha, Matrix &beta, std::vector<Matrix> &gamma){
	gamma.clear();
	COUNT_TYPE obserSize = obser.ColumnSize();
	DATA_TYPE denominator = 0.0;
	PRINT(DETAIL, "*************Compute Gama***************\n");
	for (COUNT_TYPE t = 0; t < obserSize; t++)
	{
		gamma.push_back(Matrix());
		Matrix &gamaT = gamma[t];
		gamaT.Resize(_stateNum, _gmmComponent);
		// Tinh phan sac xuat roi rac cua HMM
		denominator = 0.0;
		Vector pro(_stateNum);
		pro.Zero();
		for (COUNT_TYPE l = 0; l < _stateNum; l++)
		{
			pro(l) = alpha(l, t) * beta(l, t);
			denominator += pro(l);
		}
		// Tinh Gama
		Vector x = obser.GetColumn(t);
		for (COUNT_TYPE l = 0; l < _stateNum; l++){
			//pro(l) = pro(l) / denominator; // Chuan hoa 63b
			// Tinh Phan Mo Hinh Gaussian 63b
			GMM &gmm = _gStateModel[l];
			Vector prob(_gmmComponent);
			gmm.Probability(x, prob);
			DATA_TYPE sumProb = prob.Sum();
			// Xac xuat HMM roi rac
			DATA_TYPE tmp = pro(l) / denominator;
			for (COUNT_TYPE j = 0; j < _gmmComponent; j++){
				gamaT(l, j) = tmp * prob(j) / sumProb;// Cong Thuc 63b
			}
			PRINT(DATA, " O t= %d, state l = %d\n", t, l);
			prob.Print();
		}

		PRINT(DATA, "Gama at t = %d\n", t);
		PRINT(DATA, "Prob Matrix\n");
		pro.Print();
		PRINT(DATA, "Gamma matrix\n");
		gamaT.Print();
	}
	PRINT(DETAIL, "*************Compute Gama Completed***************\n");
}

void HMM::ProcessObservations(){

	PRINT(STEP, "PROCESS OBSER\n");
	_vMAlphas.clear();
	_vMBetas.clear();
	_vVScalar.clear();
	_vVMXi.clear();
	_vVMGamma.clear();
	for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
		Matrix &obser = _vMDatas[f];
		COUNT_TYPE obserSize = obser.ColumnSize();
		// Get parameter
		// - Tinh Alpha
		Matrix alpha;
		Vector scalar(obserSize);
		ForWard(obser, alpha, scalar);
		// - Tinh Beta
		Matrix beta;
		BackWard(obser, beta, scalar);

		_vMAlphas.push_back(alpha);
		_vMBetas.push_back(beta);
		_vVScalar.push_back(scalar);

		// - Tinh Si(i,j)
		std::vector<Matrix> xi;
		ComputeXi(obser, alpha, beta, scalar, xi);
		_vVMXi.push_back(xi);
		// Tinh Gama
		// De tinh Gama nguoi ta tinh 2 phan
		std::vector<Matrix> gamma;
		ComputeGamma(obser, alpha, beta, gamma);
		_vVMGamma.push_back(gamma);
	}

	PRINT(STEP, "END PROCESS OBSER\n");
}

void HMM::UpdateTranMatrix(){
	//Estimate transition probability from Si(i)
	PRINT(STEP, "PROCESS UPDATE TRAN MATRIX\n");
	for (COUNT_TYPE i = 0; i < _stateNum - 1; i++) {
		DATA_TYPE demon = 0.0;
		// Tinh demon
		for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
			Matrix &obser = _vMDatas[f];
			COUNT_TYPE obserSize = obser.ColumnSize();
			std::vector<Matrix> &vMxi = _vVMXi[f];

			for (COUNT_TYPE t = 0; t < obserSize - 1; t++){
				Matrix &xit = vMxi[t];
				for (COUNT_TYPE j = 0; j < _stateNum; j++){
					demon += xit(i, j);
				}
			}
		}
		// Tinh va gan cho a[i,i] a[i,j]
		for (COUNT_TYPE j = i; j <= i + 1; j++){
			DATA_TYPE nom = 0.0;
			for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
				Matrix &obser = _vMDatas[f];
				COUNT_TYPE obserSize = obser.ColumnSize();
				std::vector<Matrix> &vMxi = _vVMXi[f];

				for (COUNT_TYPE t = 0; t < obserSize - 1; t++){
					Matrix &xit = vMxi[t];
					nom += xit(i, j);
				}
			}

			_mPTranState(i, j) = nom / demon;
		}
	}
	
	PRINT(DATA, "Train Matrix\n");
	_mPTranState.Print();
	PRINT(STEP, "COMPLETED UPDATE TRAN MATRIX\n");
}

void HMM::UpdateGMM(){
	for (COUNT_TYPE m = 0; m < _stateNum; m++){// Duyet tung thang GMM mot
		GMM &gmm = _gStateModel[m];
		Matrix unity;
		unity.Resize(gmm._dim, gmm._dim); 
		for (COUNT_TYPE k = 0; k < gmm._dim; k++) unity(k, k) = 1.0 * DATA_TYPE(_EPSILON);
		// Estimate GMM for each state
		for (COUNT_TYPE com = 0; com < gmm._numComponents; com++){// Duyet tung Gausse cua mot GMM
			Vector mean(gmm._dim);
			Matrix var(gmm._dim, gmm._dim);
#if 0
			DATA_TYPE denom = 0.0;
			for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
				Matrix &obser = _vMDatas[f];
				std::vector<Matrix> &gama = _vVMGamma[f];
				COUNT_TYPE obserSize = obser.ColumnSize();
				for (COUNT_TYPE t = 0; t < obserSize; t++){
					Matrix &g = gama[t];
					Vector x = obser.GetColumn(t);
					mean += x * g(m, com);

					Matrix Dif(gmm._dim, gmm._dim);
					Dif.SetColumn(gmm._mMean.GetColumn(com) - x, 0);
					Matrix TDiff = Dif.Transpose();
					Matrix tmp = Dif*TDiff;
					var += tmp * g(m, com);
					denom += g(m, com);
				}
			}
#else
			DATA_TYPE denom = 0.0;
			for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
				Matrix &obser = _vMDatas[f];
				std::vector<Matrix> &gama = _vVMGamma[f];
				COUNT_TYPE obserSize = obser.ColumnSize();
				for (COUNT_TYPE t = 0; t < obserSize; t++){
					Matrix &g = gama[t];
					Vector x = obser.GetColumn(t);
					mean += x * g(m, com);
					denom += g(m, com);
				}
			}

			mean = mean / denom;
			// Estimate Mean
			gmm._mMean.SetColumn(mean, com);

			for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
				Matrix &obser = _vMDatas[f];
				std::vector<Matrix> &gama = _vVMGamma[f];
				COUNT_TYPE obserSize = obser.ColumnSize();
				for (COUNT_TYPE t = 0; t < obserSize; t++){
					Matrix &g = gama[t];
					Vector x = obser.GetColumn(t);
					Matrix Dif(gmm._dim, gmm._dim);
					Dif.SetColumn(/*gmm._mMean.GetColumn(com)*/ mean - x, 0);
					Matrix TDiff = Dif.Transpose();
					Matrix tmp = Dif*TDiff;
					var += tmp * g(m, com);
				}
			}
#endif
			// Estimate Var
			switch (gmm._coVarType){
			case COV_TYPE_01:
				break;;
			case COV_TYPE_02:
				for (COUNT_TYPE d = 0; d < gmm._dim; d++){
					gmm._mVar[com](d, d) = var(d, d) / denom;
				}
				gmm._mVar[com] += unity;
				break;
			case COV_TYPE_03:
				gmm._mVar[com] = var / denom;
				gmm._mVar[com] += unity;
				break;
			}
			// Estimate Weight
			DATA_TYPE denomW = 0.0;
			DATA_TYPE nomW = 0.0;
			for (COUNT_TYPE f = 0; f < _vMDatas.size(); f++){
				Matrix &obser = _vMDatas[f];
				std::vector<Matrix> &gama = _vVMGamma[f];
				COUNT_TYPE obserSize = obser.ColumnSize();
				for (COUNT_TYPE t = 0; t < obserSize; t++){
					Matrix &g = gama[t];
					nomW += g(m, com);
					for (COUNT_TYPE c = 0; c < gmm._numComponents; c++)
						denomW += g(m, c);
				}
			}
			gmm._vW[com] = nomW / denomW;
		}
		PRINT(DATA, "GMM = %d\n", m);
		gmm.Print();
	}
}

void HMM::BaumWeclch(){
	PRINT(STEP, "COMPUTE BaumWeclch\n");
	//Compute Xi and Gamma
	ProcessObservations();
	//Update Transition Matrix
	UpdateTranMatrix();
	//Update GMM
	UpdateGMM();

	Print(INFORMATION);
	PRINT(STEP, "COMPLETED COMPUTE BaumWeclch\n");
}
// Tinh InitState
DATA_TYPE HMM::Viterbi(Matrix &mObser, VectorIdx &vState){
	DATA_TYPE prob = INIT_DATA_TYPE;
	COUNT_TYPE obserSize = mObser.ColumnSize();
	vState.Resize(obserSize);
	Matrix delta(_stateNum, obserSize);
	MatrixIdx si(_stateNum, obserSize);
	// Init
	Vector vObser0 = mObser.GetColumn(0);
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		DATA_TYPE proO = INIT_DATA_TYPE;
		if (_mode == TEST){
			COUNT_TYPE idx = _mIdxObser[0];
			proO = _mPObserver(state, idx);
		} else {
			proO = ProbabilityObserver(vObser0, state);
		}
		
		DATA_TYPE tran = _vPInitState(state);
		DATA_TYPE prob = tran* proO;
		delta(state, 0) = prob;
		si(state, 0) = state;
	}
#ifdef PRINT_DEBUG_HMM_VITERBI
	PRINT(DATA, "Si Matrix O =0\n");
	si.Print();
	PRINT(DATA, "Delta Matrix O = 0\n");
	delta.Print();
#endif
	// Recusive
	DATA_TYPE max, val;
	COUNT_TYPE maxIdx;
	for (COUNT_TYPE t = 1; t < obserSize; t++){ // Visit all  observation
		Vector vObser = mObser.GetColumn(t);
		for (COUNT_TYPE j = 0; j < _stateNum; j++){
			// Find max value
			max = 0.0;
			maxIdx = 0;
#ifdef PRINT_DEBUG_HMM_VITERBI
			PRINT(DATA, "Find max at Ob = %d, state = %d\n", t, j);
#endif
			for (COUNT_TYPE i = 0; i < _stateNum; i++){
				DATA_TYPE del = delta(i, t - 1);
				DATA_TYPE tran = _mPTranState(i, j);

				val = del * tran;
#ifdef PRINT_DEBUG_HMM_VITERBI
				PRINT(DATA, "Prob = Delta(%d, %d)- %f x Tran(%d, %d) - %f = %f\n", i, t - 1, del, i, j, tran, val);
#endif
				if (val > max){
					max = val;
					maxIdx = i;
				}
			}
			DATA_TYPE prob = INIT_DATA_TYPE;
			if (_mode == TEST){
				COUNT_TYPE idx = _mIdxObser[t];
				prob = _mPObserver(j, idx);
#ifdef PRINT_DEBUG_HMM_VITERBI
				PRINT(DATA, "Po t = %d, Idx = %d = %f\n", t, idx, prob);
#endif
			}
			else {
				prob = ProbabilityObserver(vObser, j);
			}
			
			delta(j, t) = max *  prob;
			si(j, t) = maxIdx;
		}
#ifdef PRINT_DEBUG_HMM_VITERBI
		PRINT(DATA, "Si Matrix O = %d\n", t);
		si.Print();
		PRINT(DATA, "Delta Matrix O = %d\n", t);
		delta.Print();
#endif
	}
	//Finalize
	prob = delta(0, obserSize - 1);
	vState[obserSize -1] = 0;
	for (COUNT_TYPE state = 1; state < _stateNum; state++){
		if (delta(state, obserSize - 1) > prob)
		{
			prob = delta(state, obserSize - 1);
			vState[obserSize - 1] = state;
		}
	}
	//Tracking back
	for (COUNT_TYPE t = obserSize - 2; t != ~(COUNT_TYPE)0 && obserSize > 0; t--){ // Visit all  observation
		COUNT_TYPE idx = vState[t + 1];
		vState[t] = si(idx, t + 1);
	}
#ifdef PRINT_DEBUG_HMM_VITERBI_SUM
	PRINT(DATA, "*****************VITERBI********************\n");
	PRINT(DATA, "Tracking\n");;
	vState.Print();
	PRINT(DATA, "SI Matrix\n");
	si.Print();
	PRINT(DATA, "Delta Matrix\n");
	delta.Print();
	PRINT(DATA, "*****************END VITERBI*****************\n");
#endif
	return prob;
}
DATA_TYPE HMM::LogViterbi(Matrix &mObser, VectorIdx &vState){


	PRINT(STEP, "******************START COMPUTE LOG VITERBI*****************\n");
	DATA_TYPE prob = INIT_DATA_TYPE;
	COUNT_TYPE obserSize = mObser.ColumnSize();
	vState.Resize(obserSize);
	Vector vPLogInitState = log(_vPInitState);
#ifdef PRINT_DEBUG_HMM_LOG_VITERBI
	PRINT(DATA, "Init Vector");
	_vPInitState.Print();
	vPLogInitState.Print();
#endif
	Matrix mPLogTranState = log(_mPTranState);
#ifdef PRINT_DEBUG_HMM_LOG_VITERBI
	PRINT(DATA, "Tran Matrix");
	_mPTranState.Print();
	mPLogTranState.Print();
#endif
	Matrix delta(_stateNum, obserSize);
	MatrixIdx si(_stateNum, obserSize);
	// Init
	Vector vObser0 = mObser.GetColumn(0);
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		DATA_TYPE proO = INIT_DATA_TYPE;
		if (_mode == TEST){
			COUNT_TYPE idx = _mIdxObser[0];
			proO = _mPObserver(state, idx);
		}
		else {
			proO = ProbabilityObserver(vObser0, state);
		}
		DATA_TYPE tranL = vPLogInitState(state);
		DATA_TYPE prob = tranL + log(proO);
		delta(state, 0) = prob;
		si(state, 0) = state;
	}
	// Recusive
	DATA_TYPE max, val;
	COUNT_TYPE maxIdx;
	for (COUNT_TYPE t = 1; t < obserSize; t++){ // Visit all  observation
		Vector vObser = mObser.GetColumn(t);
		for (COUNT_TYPE j = 0; j < _stateNum; j++){
			// Find max value
			max = delta(0, t - 1) + mPLogTranState(0, j);
			maxIdx = 0;
#ifdef PRINT_DEBUG_HMM_LOG_VITERBI
			PRINT(DATA, "Find max at Ob = %d, state = %d\n", t, j);
#endif
			for (COUNT_TYPE i = 0; i < _stateNum; i++){
				DATA_TYPE del = delta(i, t - 1);
				DATA_TYPE tran = mPLogTranState(i, j);
				val = del + tran;
#ifdef PRINT_DEBUG_HMM_LOG_VITERBI
				PRINT(DATA, "Prob = Delta(%d, %d)- %f x Tran(%d, %d) - %f = %f\n", i, t - 1, del, i, j, tran, val);
#endif
				if (val > max){
					max = val;
					maxIdx = i;
				}
			}
			//DATA_TYPE prob = ProbabilityObserver(vObser, j);
			DATA_TYPE prob = INIT_DATA_TYPE;
			if (_mode == TEST){
				COUNT_TYPE idx = _mIdxObser[t];
				prob = _mPObserver(j, idx);
#ifdef PRINT_DEBUG_HMM_LOG_VITERBI
				PRINT(DATA, "Po t = %d, Idx = %d = %f\n", t, idx, prob);
#endif
			}
			else {
				prob = ProbabilityObserver(vObser, j);
#ifdef PRINT_DEBUG_HMM_LOG_VITERBI
				PRINT(DATA, "Po t = %d, state = %d = %f\n", t, j, prob);
#endif
			}
			delta(j, t) = max  +  log(prob);
			si(j, t) = maxIdx;
		}
#ifdef PRINT_DEBUG_HMM_LOG_VITERBI
		PRINT(DATA, "SI Matrix O = %d\n", t);
		si.Print();
		PRINT(DATA, "Delta Matrix O = %d\n", t);
		delta.Print();
#endif
	}
	//Finalize
	prob = delta(0, obserSize - 1);
	vState[obserSize - 1] = 0;
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		if (delta(state, obserSize - 1) > prob)
		{
			prob = delta(state, obserSize - 1);
			vState[obserSize - 1] = state;
		}
	}
	//Tracking back
	for (COUNT_TYPE t = obserSize - 2; t != ~(COUNT_TYPE)0 && obserSize > 0; t--){ // Visit all  observation
		vState[t] = si(vState[t + 1], t + 1);
	}

#ifdef PRINT_DEBUG_HMM_LOG_VITERBI_SUM
	PRINT(DATA, "*****************VITERBI********************\n");
	PRINT(DATA, "Tracking\n");;
	vState.Print();
	PRINT(DATA, "SI Matrix\n");
	si.Print();
	PRINT(DATA, "Delta Matrix\n");
	delta.Print();
	PRINT(DATA, "*****************END VITERBI*****************\n");
#endif
	PRINT(STEP, "******************END COMPUTE LOG VITERBI*****************\n");
	return prob;
}
// Tinh P(O| Model)
DATA_TYPE HMM::BackWard(Matrix &mObser, Matrix &beta){
	COUNT_TYPE obserSize = mObser.ColumnSize();
	if (obserSize == 0) return -1;
	Vector vObserN;
	beta.Resize(_stateNum, obserSize, false);
	//Init
	if (_mode == EXCUTE){
		vObserN = mObser.GetColumn(obserSize - 1);
	}

	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		beta(state, obserSize - 1) = 1;
	}
	// Recusive
	for (COUNT_TYPE t = obserSize - 2; t != ~(COUNT_TYPE) 0; t--){
		printf("t = %d\n", t);
		for (COUNT_TYPE i = 0; i < _stateNum; i++){
			DATA_TYPE sum = DATA_TYPE(0.0);
			for (COUNT_TYPE j = 0; j < _stateNum; j++){
				if (_mode == TEST){
					COUNT_TYPE idx = _mIdxObser[t + 1];
					DATA_TYPE pO = _mPObserver(j, idx);
					DATA_TYPE tran = _mPTranState(i, j);
					DATA_TYPE be = beta(j, t + 1);
					sum += tran * be * pO;
				}
				else{
					sum += _mPTranState(i, j) * beta(j, t + 1) * ProbabilityObserver(mObser.GetColumn(t + 1), j);
				}
			}
			beta(i, t) = sum;
		}
	}

	DATA_TYPE pro = 0.0;
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		pro += beta(state, 0);
	}
#ifdef PRINT_DEBUG_HMM_BACKWARD_SUM
	PRINT(DATA, "Beta Matrix\n");
	beta.Print();
#endif
	return pro;
}
void HMM::BackWard(Matrix &mObser, Matrix &beta, Vector &scalar){
	PRINT(DETAIL, "*************Compute BACKWARD***************\n");
	COUNT_TYPE obserSize = mObser.ColumnSize();
	if (obserSize == 0) return ;
	Vector vObserN;
	//Matrix prob(_stateNum, obserSize);
	beta.Resize(_stateNum, obserSize, false);
	beta.Zero();
	//Init
	if (_mode == EXCUTE){
		vObserN = mObser.GetColumn(obserSize - 1);
	}
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		beta(state, obserSize - 1) = scalar[obserSize - 1];
	}
	// Recusive
	for (COUNT_TYPE t = obserSize - 2; t != ~(COUNT_TYPE)0; t--){
		/*for (COUNT_TYPE i = 0; i < _stateNum; i++){
			DATA_TYPE pO = ProbabilityObserver(mObser.GetColumn(t + 1), i);
			prob(i, t + 1) = pO;
		}*/
		for (COUNT_TYPE i = 0; i < _stateNum; i++){
			DATA_TYPE sum = DATA_TYPE(0.0);
			for (COUNT_TYPE j = 0; j < _stateNum; j++){
				if (_mode == TEST){
					COUNT_TYPE idx = _mIdxObser[t + 1];
					DATA_TYPE pO = _mPObserver(j, idx);
					DATA_TYPE tran = _mPTranState(i, j);
					DATA_TYPE be = beta(j, t + 1);
					sum += tran * be * pO;
				}
				else {
					DATA_TYPE pO = ProbabilityObserver(mObser.GetColumn(t + 1), j);
					sum += _mPTranState(i, j) * beta(j, t + 1) * pO;
				}
				
			}
			beta(i, t) = sum;
		}
		// Scalar
		for (COUNT_TYPE i = 0; i < _stateNum; i++){
			beta(i, t) *= scalar(t);
		}
	}

	DATA_TYPE pro = 0.0;
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		pro += beta(state, 0);
	}

#ifdef  PRINT_DEBUG_HMM_BACKWARD
	PRINT(DATA, "Scalar Vector \n");
	scalar.Print();

	PRINT(DATA, "Beta Vector \n");
	beta.Print();

	PRINT(DETAIL, "*************Compute BACKWARD Completed***************\n");
#endif
}
DATA_TYPE HMM::ForWard(Matrix &mObser, Matrix &alpha){

	COUNT_TYPE obserSize = mObser.ColumnSize();
	if (obserSize == 0) return -1;
	// Init Tinh apha(i) tren xac xuat khoi tao va chuoi quan sat dau tien la O0
	Vector vObser0;
	alpha.Resize(_stateNum, obserSize, false);

	if (_mode == EXCUTE){
		vObser0 = mObser.GetColumn(0);
	}
	// Tinh First value
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		if (_mode == EXCUTE){
			alpha(state, 0) = _vPInitState(state) * ProbabilityObserver(vObser0, state);
		}
		else {
			COUNT_TYPE idx = _mIdxObser[0];
			DATA_TYPE pO = _mPObserver(state, idx);
			DATA_TYPE init = _vPInitState(state);
			alpha(state, 0) = init * pO;
		}
	}
	// Recusive
	for (COUNT_TYPE t = 0; t < obserSize - 1; t++){ // duyet theo quan sat
		for (COUNT_TYPE curState = 0; curState < _stateNum; curState++){// For cho cai hien tai tinh At(currstate)
			DATA_TYPE sum = DATA_TYPE(0.0);
			// Tinh tong cac apha trang thai truoc do nhan ma tran chuyen tiep cua no.
			for (COUNT_TYPE preState = 0; preState < _stateNum; preState++){ //// For cho cai truoc do tinh At-1(prestate)
				DATA_TYPE tran = _mPTranState(preState, curState);
				DATA_TYPE alp = alpha(preState, t);
				sum += alp * tran;
			}

			if (_mode == EXCUTE){
				alpha(curState, t + 1) = sum * ProbabilityObserver(mObser.GetColumn(t + 1), curState);
			} else {
				COUNT_TYPE idx = _mIdxObser[t + 1];
				DATA_TYPE pO = _mPObserver(curState, idx);
				alpha(curState, t + 1) = sum *pO;
			}
		}
	}


	//Tinh Propability
	DATA_TYPE pro = 0.0;
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		pro += alpha(state, obserSize - 1);
	}
#ifdef PRINT_DEBUG_HMM_FORWARD_SUM
	PRINT(DATA, "Alpha Matrix\n");
	alpha.Print();
#endif
	return pro;
}
void HMM::ForWard(Matrix &mObser, Matrix &alpha, Vector &scalar){

	PRINT(DETAIL, "*************Compute ForWard***************\n");
	COUNT_TYPE obserSize = mObser.ColumnSize();
	if (obserSize == 0) return ;
	// Init Tinh apha(i) tren xac xuat khoi tao va chuoi quan sat dau tien la O0
	Vector vObser0;
	alpha.Resize(_stateNum, obserSize, false);
	scalar.Resize(obserSize);
	scalar.Zero();
	if (_mode == EXCUTE){
		vObser0 = mObser.GetColumn(0);
	}

	// Tinh First value
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		if (_mode == EXCUTE){
			DATA_TYPE pi = _vPInitState(state);
			DATA_TYPE pO = ProbabilityObserver(vObser0, state);
			DATA_TYPE val = pi* pO;
			alpha(state, 0) = val;
			scalar[0] += alpha(state, 0);
		}
		else {
			COUNT_TYPE idx = _mIdxObser[0];
			DATA_TYPE pO = _mPObserver(state, idx);
			DATA_TYPE init = _vPInitState(state);
			alpha(state, 0) = init * pO;
			scalar[0] += alpha(state, 0);
		}
	}
	scalar[0] = 1 / scalar[0];

	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		alpha(state, 0) *= scalar[0];
	}
	// Recusive
	for (COUNT_TYPE t = 0; t < obserSize - 1; t++){ // duyet theo quan sat
		
		for (COUNT_TYPE curState = 0; curState < _stateNum; curState++){// For cho cai hien tai tinh At(currstate)
			DATA_TYPE sum = DATA_TYPE(0.0);
			// Tinh tong cac apha trang thai truoc do nhan ma tran chuyen tiep cua no.
			for (COUNT_TYPE preState = 0; preState < _stateNum; preState++){ //// For cho cai truoc do tinh At-1(prestate)
				if (_mode == EXCUTE){
					sum += alpha(preState, t) * _mPTranState(preState, curState);
				} else {
					DATA_TYPE tran = _mPTranState(preState, curState);
					DATA_TYPE al = alpha(preState, t);
					sum += al * tran;
				}
			}

			if (_mode == EXCUTE){
				DATA_TYPE pO = ProbabilityObserver(mObser.GetColumn(t + 1), curState);;
				//prob(curState, t + 1) = pO;
				alpha(curState, t + 1) = sum * pO;
				scalar[t + 1] += alpha(curState, t + 1);
				//printf("alpha %d t = %d  = %20f scale = %20f\n", curState, t + 1, alpha(curState, t + 1), scalar[t + 1]);
			}
			else {
				COUNT_TYPE idx = _mIdxObser[0];
				DATA_TYPE pO = _mPObserver(curState, idx);
				alpha(curState, t + 1) = sum * pO;
				scalar[t + 1] += alpha(curState, t + 1);
			}
		}
		
		scalar[t + 1] = 1 / scalar[t + 1]; // Tinh He So
		for (COUNT_TYPE curState = 0; curState < _stateNum; curState++){
			alpha(curState, t + 1) *= scalar[t + 1]; // Nhan lai voi alpha
		}
	}
#ifdef  PRINT_DEBUG_HMM_FORWARD
	
	PRINT(DATA, "Scalar Vector \n");
	scalar.Print();

	PRINT(DATA, "Alpha Vector \n");
	alpha.Print();
	PRINT(DATA, "***********Compute FORWARD COMPLETED*************\n");
#endif
}

DATA_TYPE HMM::ProbabilityObserver(Vector &in, COUNT_TYPE state){
	return _gStateModel[state].Probability(in);
}

void HMM::Print(int level){
	PRINT(level, "*********************HMM*******************************\n");
	PRINT(level, "Init Matrix\n");
	_vPInitState.Print();
	PRINT(level, "Init Train\n");
	_mPTranState.Print();
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		PRINT(level, "GMM - %d\n", state);
		_gStateModel[state].Print();
	}
	PRINT(level, "*********************HMM*******************************\n");
}

bool HMM::Load(char *path){
	TiXmlDocument doc;
	doc.LoadFile(path);
	TiXmlElement *e = doc.FirstChildElement("HMM");
	return Load(e);
}

bool HMM::Load(TiXmlElement *hmm){
	bool result = false;
	if (hmm != NULL){
		SIGNED_TYPE mode = 0, stateNum = 0, gmmComponent = 0, gmmCoVar = 0, isSuccess = 0, maxIterator = 0;
		if (hmm->Attribute("Mode", &mode) != NULL &&
			hmm->Attribute("State", &stateNum) != NULL &&
			hmm->Attribute("GMM_Component", &gmmComponent) != NULL &&
			hmm->Attribute("GMM_Covar", &gmmCoVar) != NULL &&
			hmm->Attribute("Valid", &isSuccess) != NULL &&
			hmm->Attribute("MaxIterator", &maxIterator) != NULL){
			_mode = (mode) ? EXCUTE:TEST;
			_stateNum = stateNum;
			_gmmComponent = gmmComponent;
			_gmmCoVar = gmmCoVar;
			_maxIterator = maxIterator;
			_isSuccess = (isSuccess != 0) ? false : true;

			result = true;
			// Weight
			TiXmlElement *init_e = hmm->FirstChildElement("Init");
			result &= _vPInitState.Load(init_e);
			TiXmlElement *trans_e = hmm->FirstChildElement("Trans");
			result &= _mPTranState.Load(trans_e);

			TiXmlElement *gmm_e = hmm->FirstChildElement("GMM");
			for (COUNT_TYPE state = 0; state < _stateNum; state++){
				char str_var[100] = { 0 };
				sprintf(str_var, "GMM_%03d", state);
				TiXmlElement *gmmCom_e = gmm_e->FirstChildElement(str_var);
				if (gmmCom_e != NULL){
					_gStateModel.push_back(GMM(0, 0));
					GMM &gmm = _gStateModel[state];
					result &= gmm.Load(gmmCom_e);
				}
				else {
					result = false;
				}
			}
		}
	}
	return result;
}

bool HMM::Save(char *path){
	TiXmlDocument doc;
	TiXmlElement *gmm = new TiXmlElement("HMM");
	Save(gmm);
	doc.LinkEndChild(gmm);
	return doc.SaveFile(path);
}

bool HMM::Save(TiXmlElement *hmm){
	bool result = true;
	hmm->SetAttribute("Mode", _mode);
	hmm->SetAttribute("State", _stateNum);
	hmm->SetAttribute("GMM_Component", _gmmComponent);
	hmm->SetAttribute("GMM_Covar", _gmmCoVar);
	hmm->SetAttribute("Valid", _isSuccess);
	hmm->SetAttribute("MaxIterator", _maxIterator);

	// Init State
	TiXmlElement *init_e = new TiXmlElement("Init");
	if (init_e != NULL){
		_vPInitState.Save(init_e);
	}
	hmm->LinkEndChild(init_e);

	TiXmlElement *trans_e = new TiXmlElement("Trans");
	_mPTranState.Save(trans_e);

	hmm->LinkEndChild(trans_e);

	TiXmlElement *gmm_e = new TiXmlElement("GMM");
	for (COUNT_TYPE state = 0; state < _stateNum; state++){
		char str_var[100] = { 0 };
		sprintf(str_var, "GMM_%03d", state);
		TiXmlElement *gmmCom_e = new TiXmlElement(str_var);
		if (gmmCom_e != NULL){
			_gStateModel[state].Save(gmmCom_e);
			gmm_e->LinkEndChild(gmmCom_e);
		}
	}

	hmm->LinkEndChild(gmm_e);
	return result;
}
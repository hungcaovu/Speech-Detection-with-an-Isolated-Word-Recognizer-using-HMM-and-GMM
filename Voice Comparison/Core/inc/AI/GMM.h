
#ifndef GMM_H
#define GMM_H
#include <Util\Matrix.h>
#include <Util\MatrixIdx.h>


#ifndef MIN_PDF
#define MIN_PDF 1e-40
#endif

//Print Log GMM
#define PRINT_DEBUG_GMM_INIT
#define PRINT_DEBUG_GMM_LOG_PROBABILITY
#define PRINT_DEBUG_GMM_STEP_EM
#define PRINT_DEBUG_GMM_DO_EM_UPDATE_VAR
#define PRINT_DEBUG_GMM_DO_EM_UPDATE_MEAN
#define PRINT_DEBUG_GMM_DO_EM
#define PRINT_DEBUG_GMM_COST_FUNCTION

//#define PRINT_DEBUG_GMM_PDF
enum COV_TYPE{
	COV_TYPE_01,// Su dung mot so lam Phuong sai
	COV_TYPE_02,// Su dung ma tran duong cheo lam phuong sai
	COV_TYPE_03,// Full sung ma tran tuong quan de lam phuong sai
};

class GMM {
private:
	friend class HMM;
	typedef std::vector<Matrix>::iterator MatrixIterator;
	std::vector<Matrix> _mVar;// Variance
	Matrix _mMean;// Mean Su dung Kmean. Dim(Row) * Components (Col)
	Vector _vW;// Trong so for all component

	std::vector<Matrix> _mDataCluster;

	COUNT_TYPE _coVarType; // Covariant type // Dang chon ma tran duong cheo Dim x 1
	COUNT_TYPE _dim;

	Matrix unity;

	COUNT_TYPE _numComponents;// Component chinh la cluster.
	Matrix _mData;
	COUNT_TYPE _maxIterator;
	DATA_TYPE _thresholdLog;

	bool _isSuccess;
public:

	GMM(COUNT_TYPE components){
		_maxIterator = 500;
		_numComponents = components;
		_coVarType = COV_TYPE_02;
	};

	GMM(char * path);
	GMM(COUNT_TYPE components, COUNT_TYPE coVarType = COV_TYPE_02, COUNT_TYPE maxIterator = 500);
	DATA_TYPE Probability(Vector &in);

	DATA_TYPE LogProbability(Matrix &in);
	void Probability(Vector &in, Vector &prob);
	// Save Parameter to xml
	bool Load(char *path);
	bool Load(TiXmlElement *gmm);
	bool Save(char *path);
	bool Save(TiXmlElement *gmm);

	void Print(LOG_LEVEL level = DATA);

	bool DoEM(Matrix &data);

	// Megoth Get
	Matrix &GetVar(COUNT_TYPE component){
		return _mVar[component];
	};
	Vector GetMean(COUNT_TYPE component){
		return _mMean.GetColumn(component);
	}
	DATA_TYPE &GetWeight(COUNT_TYPE component){
		return _vW[component];
	}

private:
	void Init();
	DATA_TYPE pdf(Vector &in, COUNT_TYPE component);
	// For HMM
};

#endif
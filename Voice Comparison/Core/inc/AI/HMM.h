#include <Common.h>
#include <Util\Matrix.h>
#include <AI\GMM.h>
#include <vector>
#include <assert.h>
#define ASSERT_HMM assert
#define PRINT_DEBUG_HMM_TRAIN_FOR_GMM
#define PRINT_DEBUG_HMM_BACKWARD
#define PRINT_DEBUG_HMM_FORWARD
#define PRINT_DEBUG_HMM_LOG_VITERBI
#define PRINT_DEBUG_HMM_VITERBI
#define PRINT_DEBUG_HMM_LOG_VITERBI_SUM
#define PRINT_DEBUG_HMM_VITERBI_SUM
#define PRINT_DEBUG_HMM_FORWARD_SUM
#define PRINT_DEBUG_HMM_BACKWARD_SUM

enum HMMMode {
	TEST = 0,
	EXCUTE = 1
};
class HMM{
	HMMMode _mode;
	COUNT_TYPE _fileTrainNum;
	COUNT_TYPE _stateNum;
	COUNT_TYPE _gmmComponent;
	COUNT_TYPE _gmmCoVar;

	COUNT_TYPE _maxIterator;
	DATA_TYPE _threshhold;

	Vector _vPInitState;
	Matrix _mPTranState;

	Matrix _mPObserver;
	VectorIdx _mIdxObser;
	std::vector<GMM> _gStateModel;

	std::vector<Matrix> _vMSegementWords;
	std::vector<Matrix> _vMDatas;
	// Cac tham so sau khi trainning t files
	std::vector<Matrix> _vMAlphas;
	std::vector<Matrix> _vMBetas;
	std::vector<Vector> _vVScalar;
	std::vector<std::vector<Matrix>> _vVMXi;
	std::vector<std::vector<Matrix>> _vVMGamma;


	bool _isSuccess;
	friend class TestHMM;
public:
	HMM(){};
	HMM(char * path);
	HMM(COUNT_TYPE stateNum, COUNT_TYPE gmmConponent, COUNT_TYPE gmmCoVar);
	
	DATA_TYPE LogProbability(std::string file);
	DATA_TYPE LogProbability(Matrix obsers);

	bool Trainning(std::vector<std::string> files);
	bool Trainning(std::vector<Matrix> obsers);
	bool Trainning();

	void Print(int level = DATA);
	bool Load(char *path);
	bool Save(char *path);
private:
	bool LoadData(std::vector<std::string> &files);
	//void TrainForGMM(Matrix &mObser);
	std::vector<Matrix> SegementWord(Matrix &mObser);
	void InitHMM();
	void BaumWeclch();
	void ProcessObservations();
public:
	DATA_TYPE LogViterbi(Matrix &mObser, VectorIdx &vState);
	DATA_TYPE Viterbi(Matrix &mObser, VectorIdx &state);
private:

	DATA_TYPE BackWard(Matrix &mObser, Matrix &beta);
	void BackWard(Matrix &mObser, Matrix &beta, Vector &scalar);
	DATA_TYPE ForWard(Matrix &mObser, Matrix &alpha);
	void ForWard(Matrix &mObser, Matrix &alpha, Vector &scalar);
	DATA_TYPE ProbabilityObserver(Vector &in, COUNT_TYPE state);

	void ComputeXi(Matrix &obser, Matrix &alpha, Matrix &beta, Vector &scalar, std::vector<Matrix> &xi);
	void ComputeXi(Matrix &obser, Matrix &alpha, Matrix &beta, std::vector<Matrix> &xi);

	void ComputeGamma(Matrix &obser, Matrix &alpha, Matrix &beta, Vector &scalar, std::vector<Matrix> &ga);
	void ComputeGamma(Matrix &obser, Matrix &alpha, Matrix &beta, std::vector<Matrix> &ga);

	void UpdateTranMatrix();
	void UpdateGMM();
	bool Load(TiXmlElement *hmm);
	bool Save(TiXmlElement *hmm);
};
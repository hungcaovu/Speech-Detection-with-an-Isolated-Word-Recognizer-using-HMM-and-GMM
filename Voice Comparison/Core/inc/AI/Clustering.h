#ifndef CLUSTERING_H
#define CLUSTERING_H

#include <Common.h>
#include <Util\LogUtil.h>
#include <Util\Matrix.h>
#include <Util\MatrixIdx.h>
#include <Util\DistanceUtil.h>
#define PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_INIT
#define PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_IDX
#define PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_DATA
#define PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_CENTER
#define PRINT_DEBUG_KMEAN_CHECK_COVER
#define PRINT_DEBUG_KMEAN_UPDATE_CLUSTER
#define MAX_ITER_LOOP 200

enum ClusterType{
	HIERARCHICAL,
	PARTITIONAL,
	ONE_D,
	TWO_D
};

enum KmeanInitType{
	TYPE_1 = 1,
	TYPE_2 = 2,
	TYPE_3 = 3
};

class Cluster{
protected:
	COUNT_TYPE _ClusterNum;
	ClusterType _Type;
	ClusterType _DemensionType;
	DistanceType _DistanceType;
public:
	Cluster(){
		_Type = HIERARCHICAL;
		_ClusterNum = 0;
		_DemensionType = ONE_D;
	};	
};


class KMeanCluster : public Cluster{
private:
	typedef std::vector<VectorIdx>::iterator Iterator;

	COUNT_TYPE _loopCount;

	COUNT_TYPE _dim;
	COUNT_TYPE _clusterGroups;

	//Luu tru kmean output
	Vector _vDistortion;
	Matrix _mMean; // mean cua cac group
	Vector _vMean;

	VectorIdx _vCenterIdxOld;

	VectorIdx _vCenterIdx; // Luu vi tri cua center 1 x _clusterGroups luu idx cua chuoi du lieu _mData
	std::vector<VectorIdx> _mClusterDataIdx; //MatrixIdx Col group of cluster Data Size x _clusterGroups
	VectorIdx _mDataIdx;

	Vector _vWeightCluster;

	std::vector<Matrix> _mClusterData;// Store data in cluster group
	Matrix _mData;

	bool isConvergent;
public:
	KMeanCluster(COUNT_TYPE clusterNum, ClusterType type = ONE_D, COUNT_TYPE loopCount = MAX_ITER_LOOP);
	void DoClustering(Matrix &m);
	void Print(LOG_LEVEL level = DATA);
	void PrintIdx(LOG_LEVEL level = DATA);
	VectorIdx GetCenterIndex(){
		return _vCenterIdx;
	};
	VectorIdx GetClusterIndex(COUNT_TYPE group){
		return _mClusterDataIdx[group];
	};

	const Matrix &GetDataCluster(COUNT_TYPE group){
		return _mClusterData[group];
	}



	Vector GetDistortion(){
		return _vDistortion;
	};
	const Vector &GetMeanCluster(){
		return _vMean;
	};
	const Matrix &GetMeanDataCluster(){
		return _mMean;
	};

	Vector GetWeightCluster(){
		return _vWeightCluster;
	};

private:
	void Init();
	void DoClusteringMatrix(Matrix &m);
	bool IsConvergent();
	void UpdateCluster();
	void UpdateCenter();
	void UpdateDataIdx();
	void UpdateDataCluster();
	Matrix Distance();
	void InitCenter(COUNT_TYPE method);
	INDEX_TYPE FindCenterIdx(COUNT_TYPE ncluster, VectorIdx &v, DATA_TYPE &distortion);
}; 


#endif
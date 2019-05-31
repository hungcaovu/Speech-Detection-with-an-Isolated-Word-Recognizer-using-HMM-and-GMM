#include <AI\Clustering.h>
#include <Util\Matrix.h>
#include <Util\DistanceUtil.h>
#include <algorithm>

KMeanCluster::KMeanCluster(COUNT_TYPE clusterNum,  ClusterType type, COUNT_TYPE loopCount) : Cluster(){
	this->_DemensionType = type;
	_loopCount = loopCount;
	_clusterGroups = clusterNum;
	PRINT(STEP, "Created Kmean: %d Cluster Num %d\n", _Type, _ClusterNum);
	isConvergent = true;
};

void KMeanCluster::DoClustering(Matrix &m){
	DoClusteringMatrix(m);
}

void KMeanCluster::InitCenter(COUNT_TYPE method){

	switch (method){
	case TYPE_1: // Random picking
		PRINT(STEP, "Kmean Center Init TYPE_1: Random picking\n");
		_vCenterIdx.RandomIndex(0, _mDataIdx.Size());
		break;
	case TYPE_2:{ // Method 2: Choose clusterNum data points closest to the mean vector
		PRINT(STEP, "Kmean Center Init TYPE_2: Choose clusterNum data points closest to the mean vector");
		COUNT_TYPE size = _mData.ColumnSize();
		Vector mean = _mData.Mean();

		Vector dist = DistanceUtil::Distance(_mData, mean);
		_mDataIdx.Sort(dist);

		for (COUNT_TYPE i = 0; i < _clusterGroups; i++){
			_vCenterIdx[i] = _mDataIdx[i];
		}
	} break;
	case TYPE_3:{
		// Implement: Chia ra tung group theo khoan cach roi` tinh tiep
		PRINT(STEP, "Kmean Center Init TYPE_3: Find The best index by grouping datas using distance\n");
		COUNT_TYPE size = _mData.ColumnSize();
		Vector mean = _mData.Mean();

		Vector dist = DistanceUtil::Distance(_mData, mean);
		_mDataIdx.Sort(dist);
		for (COUNT_TYPE i = 0; i < _clusterGroups; i++){
			_vCenterIdx[i] = _mDataIdx[size - (i + 1) *(size / _clusterGroups)];
		}

		//PRINT(DATA, "Center Init Maxtrix\n");
		//_vCenterIdx.Print();

	}break;
	}

	//PRINT(DATA, "Center Init Maxtrix\n");
	//UpdateDataCluster();
	//PRINT(DATA, "Kmean Infomation:\n");
	//Print(DATA);
}

void KMeanCluster::Init(){
	// Update mDataIndexing
	COUNT_TYPE col = _mData.ColumnSize();
	COUNT_TYPE colIdx = _mDataIdx.Size();

	if (col != colIdx){
		for (COUNT_TYPE i = colIdx; i < col; i++){
			_mDataIdx.PushBack(i);
		}
	}
//#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_INIT
	PRINT(DATA, "Data Idx:\n");
	_mDataIdx.Print();
//#endif
	_vCenterIdx.Resize(_clusterGroups);

	InitCenter(1);

	_vCenterIdxOld = _vCenterIdx;
	_mMean.Resize(_dim, _clusterGroups, false);
	_vMean.Resize(_clusterGroups, false);

	//List Cluster
	_mClusterDataIdx.clear();
	for (COUNT_TYPE i = 0; i < _clusterGroups; i++){
		_mClusterDataIdx.push_back(VectorIdx());
	}

	_vWeightCluster.Resize(_clusterGroups, false);

}

bool KMeanCluster::IsConvergent(){
#ifdef PRINT_DEBUG_KMEAN_CHECK_COVER
	PRINT(DATA, "Center IDX Old\n");
	_vCenterIdxOld.Print();
	PRINT(DATA, "Center IDX \n");
	_vCenterIdx.Print();
#endif
	if (_vCenterIdxOld != _vCenterIdx){
		_vCenterIdxOld = _vCenterIdx;
		return false;
	}
	return true;
}


void KMeanCluster::DoClusteringMatrix(Matrix &m) {
	int countCov = 0;
	//_m += m; // apend data
	if (_dim == 0 || (_dim != 0 && _dim != m.RowSize())){
		_mData = m; // assgin new data to cur data.
		_dim = m.RowSize();
		if (m.ColumnSize() < _clusterGroups){
			PRINT(STEP, "Cluster number greater than  data size");
			return;
		}
		PRINT(STEP, "*****************KMEAN Start Doing Clustering***************\n");
		Init();
		COUNT_TYPE i = 0;
		for (i = 0; i < _loopCount; i++){
			PRINT(STEP, "DOING CLUSTERING Iter %d\n", i);
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_IDX
			PRINT(DETAIL, "*************UPDATE DATA IDX Itor = %d*************\n", i);
#endif
			UpdateDataIdx();
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_DATA
			PRINT(DETAIL, "*************UPDATE CLUSTER Itor = %d*************\n", i);
#endif
			UpdateCluster();
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_CENTER
			PRINT(DETAIL, "*************UPDATE CENTER Itor = %d*************\n", i);
#endif
			UpdateCenter();
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER
			PRINT(DETAIL,"*************PRINT GROUP  Itor = %d***************\n", i);
			Print(DETAIL);
			PRINT(DETAIL, "*************CHECK CONVERGENT  Itor = %d*************\n", i);
#endif
			if (IsConvergent()){
				countCov++;
				isConvergent = true;
				if (countCov == 3){
					break;
				}
			}


			PRINT(INFORMATION, "Kmean Covergent: Updated Var and Weight\n");
			UpdateDataCluster();
			PRINT(INFORMATION, "Kmean Infomation:\n");
			Print(INFORMATION);
		}
		if (i >= _loopCount){
			PRINT(STEP, " After Iterator time: %d - but Kmean does not covergent\n", _loopCount);
		}
	}
	else {
		PRINT(STEP, "DO Custering K Mean Different Dim of data\n");
	}

//	if (isConvergent){
		PRINT(INFORMATION, "Kmean Covergent: Updated Var and Weight\n");
		UpdateDataCluster();
		PRINT(INFORMATION, "Kmean Infomation:\n");
		Print(INFORMATION);
//	}
	PRINT(STEP, "*****************KMEAN Doing Clustering Completed***************\n");
}

void KMeanCluster::UpdateDataCluster(){
	_mClusterData.clear();
	for (COUNT_TYPE g = 0; g < _clusterGroups; g++){
		_mClusterData.push_back(Matrix());
		Matrix &m = _mClusterData[g];
		VectorIdx &vIdx = _mClusterDataIdx[g];
		for (COUNT_TYPE i = 0; i < vIdx.Size(); i++){
			m.PlushBackColumn(_mData.GetColumn(vIdx[i]));
		}
		// Update trong so
		_vWeightCluster[g] = DATA_TYPE(vIdx.Size()) / _mData.ColumnSize();
	}
}

void KMeanCluster::Print(LOG_LEVEL level){
	COUNT_TYPE col = _mData.ColumnSize();
	COUNT_TYPE row = _mData.RowSize();

	PRINT(level, "Clustering Number - %d\n", _clusterGroups);
	PRINT(level, "Demension Of Data - %d\n\n", _dim);
	PRINT(level, "DATA Size: %d\n", _mData.ColumnSize());
	PRINT(level, "          ");
	for (COUNT_TYPE j = 0; j < col; j++){
		PRINT(level | NONE_TIME, " %10d ", j);
	}
	PRINT(level | NONE_TIME, "\n");
	for (COUNT_TYPE i = 0; i < row; i++){
		PRINT(level | NONE_TIME, "R_%8d ", i);
		for (COUNT_TYPE j = 0; j < col; j++){
			PRINT(level | NONE_TIME, " %010.3f ", _mData(i, j));
		}
		PRINT(level | NONE_TIME, "\n");
	}

	PRINT(level | NONE_TIME, "\n");

	PRINT(level, "Cluster Group: Distortion - %10.3f\n", _vDistortion.PopBack());
	for (COUNT_TYPE i = 0; i < _clusterGroups; i++){
		VectorIdx &vec = _mClusterDataIdx[i];
		Vector mean = _mMean.GetColumn(i);
		PRINT(level | NONE_TIME, "Group %d : Total Data In Group : %d\n", i, vec.Size());
		PRINT(level | NONE_TIME, "Mean Vec: \n");
		mean.Print(level);
		PRINT(level | NONE_TIME, "Idx Data: ");
		for (COUNT_TYPE j = 0; j < vec.Size(); j++){
			PRINT(level | NONE_TIME, " %8d ", vec[j]);
		}
		PRINT(level | NONE_TIME, "\nData: \n");
		for (COUNT_TYPE j = 0; j < _dim; j++){
			PRINT(level | NONE_TIME, "R_%8d ", j);
			
			for (COUNT_TYPE k = 0; k < vec.Size(); k++){
				PRINT(level | NONE_TIME, " %010.3f ", _mData(j, vec[k]));
			}
			PRINT(level | NONE_TIME, "\n");
		}
	}
}
// Remove Center List from Data Idx List
void KMeanCluster::UpdateDataIdx(){
	COUNT_TYPE col = _mData.ColumnSize();
	_mDataIdx.Reset();
	for (COUNT_TYPE i = 0; i < col; i++){
		bool removed = false;

		for (COUNT_TYPE j = 0; j < _vCenterIdx.Size(); j++){
			if (i == _vCenterIdx[j]){
				removed = true;
				break;
			}
		}
		if (!removed){
			_mDataIdx.PushBack(i);
		}
	}
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_IDX
	PRINT(DATA, "\nData IDX After Update Idx \n");
	for (COUNT_TYPE i = 0; i < _mDataIdx.Size(); i++){
		PRINT(DATA, " %10d ", _mDataIdx[i]);
	}
	PRINT(DATA, "\nVector Center IDX After Update Idx \n");
	for (COUNT_TYPE i = 0; i < _vCenterIdx.Size(); i++){
		PRINT(DATA, " %10d ", _vCenterIdx[i]);
	}
	PRINT(DATA, "\n");
#endif
}

void KMeanCluster::UpdateCluster(){
	// Reset List Cluster
	for (COUNT_TYPE i = 0; i < _clusterGroups; i++){
		_mClusterDataIdx[i].Reset();
		_mClusterDataIdx[i].PushBack(_vCenterIdx[i]);
	}

	// matran dist _mData.Col dong va mDataCenter cot
	Matrix dist = Distance();
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_DATA
	PRINT(DATA, "Distance Matrix: \n");
	dist.Print();
#endif

	COUNT_TYPE mDataSize = dist.RowSize();

	for (COUNT_TYPE r = 0; r < mDataSize; r++){
		// Duyet m oi dong data
		// Lay dung dong trong ma tran khoan de sap xep lai
		// Phan chia lai cluster
		Vector  row = dist.GetRow(r);
		COUNT_TYPE clusterGroupID = row.MinId();
		VectorIdx &mCluster = _mClusterDataIdx[clusterGroupID];
		// Push IDX of Min Distance to Cluster.
		mCluster.PushBack(_mDataIdx[r]);
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_DATA
		PRINT(DATA, "Push Idx %d To Cluster Group %d\n", _mDataIdx[r], clusterGroupID);
#endif
	}
}

void KMeanCluster::UpdateCenter(){
	//Update center lai
	DATA_TYPE &distortion = _vDistortion.PushBack(INIT_DATA_TYPE);
	for (COUNT_TYPE c = 0; c < _clusterGroups; c++){
		VectorIdx &dataInCluster = _mClusterDataIdx[c];
		//Can Enhance
		DATA_TYPE distor = 0.0;
		INDEX_TYPE idx = FindCenterIdx(c, dataInCluster, distor);
		distortion += distor;
		// Push idx to Center list
		if (idx == VectorIdx::undef) {
			continue;
		}
#ifdef PRINT_DEBUG_KMEAN_UPDATE_CLUSTER_CENTER
		PRINT(DATA, "\nPush old Centeroid %d to Data Idx\n", _vCenterIdx[c]);
		PRINT(DATA, "Change Centeroid Group %d: From %d to %d\n", c, _vCenterIdx[c], idx);
#endif
		_vCenterIdx[c] = idx;
		//INDEX_TYPE tmp = dataInCluster[0];
		//dataInCluster[0] = idx;
		//idx = tmp;
	}
}

Matrix KMeanCluster::Distance(){
	COUNT_TYPE col = _vCenterIdx.Size(); // Dong
	COUNT_TYPE row = _mDataIdx.Size(); // Cot
	Matrix dist(row, col);
	for (COUNT_TYPE i = 0; i < row; i++){
		for (COUNT_TYPE j = 0; j < col; j++){
			if (_mDataIdx[i] != _vCenterIdx[j]) {
				dist(i, j) = DistanceUtil::Distance(_mData.GetColumn(_mDataIdx[i]), _mData.GetColumn(_vCenterIdx[j]));
			}
		}
	}
	return dist;
}

INDEX_TYPE KMeanCluster::FindCenterIdx(COUNT_TYPE ncluster, VectorIdx &v, DATA_TYPE &distortion){
	COUNT_TYPE length = v.Size();
	if (length > 0 ){
		// Tim vector trung binh
		_mMean.ZeroColumn(ncluster);
		for (COUNT_TYPE i = 0; i < length; i++){
			for (COUNT_TYPE j = 0; j < _dim; j++){
				_mMean(j, ncluster) += _mData(j, v[i]);
			}
		}  
		for (COUNT_TYPE j = 0; j < _dim; j++){
			_mMean(j, ncluster) /= DATA_TYPE(length);
		}
		// Tim idx gan nhat'
		INDEX_TYPE idx = v[0];
		DATA_TYPE minDis = DistanceUtil::Distance(_mData.GetColumn(idx), _mMean.GetColumn(ncluster));
		distortion = minDis;
		for (COUNT_TYPE i = 1; i <length; i++){
			DATA_TYPE dist = DistanceUtil::Distance(_mData.GetColumn(v[i]), _mMean.GetColumn(ncluster));
			if (minDis > dist){
				idx = v[i];
				minDis = dist;
			}
			distortion += dist;
		}
		return idx;
	}

	return VectorIdx::undef;
}
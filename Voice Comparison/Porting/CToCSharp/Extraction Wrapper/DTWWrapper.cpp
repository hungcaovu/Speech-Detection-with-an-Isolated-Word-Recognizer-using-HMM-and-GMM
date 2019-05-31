#include "DTWWrapper.h"

namespace ExtractionWrapper{
	
	List<DATA_TYPE>^ DTWUtilWrapper::DistanceOf2Array3DHorizon(List<List<DATA_TYPE>^>^ vec, List<List<DATA_TYPE>^>^ refVec, bool normalize){
		COUNT_TYPE vecCol = 0;
		std::vector<DATA_TYPE *> vecV = UtilWrapper::ConverListListToVectorPtr(vec, vecCol);

		COUNT_TYPE refVecCol = 0;
		std::vector<DATA_TYPE *> refVecV = UtilWrapper::ConverListListToVectorPtr(refVec, refVecCol);

		DATA_TYPE *outPtr = DTWUtil::DistanceOf2Array3DHorizon(vecV, vecCol, refVecV, refVecCol, normalize);
		List<DATA_TYPE>^ out = UtilWrapper::ConverPtrToList(outPtr, vecCol);
		return out;
	}
	DATA_TYPE DTWUtilWrapper::DistanceOf2Array3D(List<List<DATA_TYPE>^>^ vec, List<List<DATA_TYPE>^>^ refVec, bool normalize){

		COUNT_TYPE vecCol = 0;
		std::vector<DATA_TYPE *> vecV = UtilWrapper::ConverListListToVectorPtr(vec, vecCol);

		COUNT_TYPE refVecCol = 0;
		std::vector<DATA_TYPE *> refVecV = UtilWrapper::ConverListListToVectorPtr(refVec, refVecCol);

		return  DTWUtil::DistanceOf2Array3D(vecV, vecCol, refVecV, refVecCol, normalize);
	}


	DATA_TYPE DTWUtilWrapper::DistanceOf2Array3DMatchEveryWhere(List<List<DATA_TYPE>^>^ vec, List<List<DATA_TYPE>^>^ refVec){

		COUNT_TYPE vecCol = 0;
		std::vector<DATA_TYPE *> vecV = UtilWrapper::ConverListListToVectorPtr(vec, vecCol);

		COUNT_TYPE refVecCol = 0;
		std::vector<DATA_TYPE *> refVecV = UtilWrapper::ConverListListToVectorPtr(refVec, refVecCol);

		return  DTWUtil::DistanceOf2Array3DMatchEveryWhere(vecV, vecCol, refVecV, refVecCol);
	}

	DATA_TYPE DTWUtilWrapper::DistanceOf2Vector(List<DATA_TYPE>^ vec, List<DATA_TYPE>^ refVec, bool normalize){
		COUNT_TYPE sizevec = vec->Count;
		DATA_TYPE *vecPtr = UtilWrapper::ConverListToPtr(vec);

		COUNT_TYPE sizeref = refVec->Count;
		DATA_TYPE *refPtr = UtilWrapper::ConverListToPtr(refVec);

		return  DTWUtil::DistanceOf2Vector(vecPtr, sizevec, refPtr, sizeref, normalize);
	}
	DATA_TYPE DTWUtilWrapper::DistanceOf2Vector(List<List<DATA_TYPE>^>^  vec, List<List<DATA_TYPE>^>^ refVec, bool normalize){

		COUNT_TYPE sizevec = vec->Count;
		Matrix m1 = UtilWrapper::ListListToMatrix(vec);
		Matrix m2 = UtilWrapper::ListListToMatrix(refVec);
		return DTWUtil::DistanceOf2MatrixCol(m1, m2);
	}
};

#include <Common.h>
#include <Util\Matrix.h>
class DTWUtil {
public:
	// Distance of horizon vector
	// Using 
	static Array1D DistanceOf2Array3DHorizon(Array3D vec, COUNT_TYPE colVec, Array3D refVec, COUNT_TYPE colRefVec, bool normalize = false);
	static DATA_TYPE DistanceOf2Array3D(Array3D vec, COUNT_TYPE colVec, Array3D refVec, COUNT_TYPE colRefVec, bool normalize = false);

	static DATA_TYPE DistanceOf2Array3DMatchEveryWhere(Array3D vec, COUNT_TYPE colVec, Array3D refVec, COUNT_TYPE colRefVec);

	static DATA_TYPE DistanceOf2Vector(Array1D vec, COUNT_TYPE lenVec, Array1D refVec, COUNT_TYPE lenRefVec, bool normalize = false);

	static DATA_TYPE DistanceOf2MatrixCol(Matrix &m1, Matrix &m2, bool normalize = true);
	// Return: Min value
	// Phase
	static DATA_TYPE DTWType1(DATA_TYPE a /* 180 [row][col -1]*/, DATA_TYPE b/* 225 [row -1 ][col -1]*/, DATA_TYPE c /* 270 [row -1][col]*/, DATA_TYPE &pha/* Phase */);
};
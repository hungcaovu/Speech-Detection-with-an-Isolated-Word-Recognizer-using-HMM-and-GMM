#ifndef DISTANCE_UTIL_H
#define DISTANCE_UTIL_H
#include <Common.h>
#include <Util\Vector.h>
#include <Util\Matrix.h>

enum DistanceType{
	DIS_MINKOWSKI = 0,
	DIS_EUCLIDEAN = 1,
	DIS_MANHATTAN = 2,
	DIS_COSINE = 3
};


class DistanceUtil{
	
public:
	static DATA_TYPE Distance(const Vector &v1, const Vector &v2, DistanceType type = DIS_EUCLIDEAN, DATA_TYPE g = DATA_TYPE(0));
	static Matrix Distance(Matrix &a, Matrix &b, DistanceType type = DIS_EUCLIDEAN, DATA_TYPE g = DATA_TYPE(0), bool useRowAsDim = true);
	static Vector Distance(Matrix &a, Vector &b, DistanceType type = DIS_EUCLIDEAN, DATA_TYPE g = DATA_TYPE(0), bool useRowAsDim = true);
	static DATA_TYPE MinkowskiDistance(const Vector &v1, const Vector &v2, DATA_TYPE g);
	static DATA_TYPE EuclideanDistance(const Vector &v1, const Vector &v2);
	static DATA_TYPE ManhattanDistance(const Vector &v1, const Vector &v2);
	static DATA_TYPE CosineDistance(const Vector &v1, const Vector &v2);
};

#endif
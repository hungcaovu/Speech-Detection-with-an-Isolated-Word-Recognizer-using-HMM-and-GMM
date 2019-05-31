#include <Util\DistanceUtil.h>

DATA_TYPE DistanceUtil::Distance(const Vector &v1, const Vector &v2, DistanceType type, DATA_TYPE g){
	switch (type){
		case DIS_EUCLIDEAN:
			return  EuclideanDistance(v1, v2);
		case DIS_MANHATTAN:
			return  ManhattanDistance(v1, v2);
		case DIS_MINKOWSKI:
			return MinkowskiDistance(v1, v2, g);
		case DIS_COSINE:
			return CosineDistance(v1, v2);
		default:
			return DATA_TYPE(0);
	}
}

DATA_TYPE DistanceUtil::CosineDistance(const Vector &v1, const Vector &v2){
	COUNT_TYPE v1Size = v1.Size();
	if (v1Size == v2.Size()) {
		DATA_TYPE sum = DATA_TYPE(0.0);
		DATA_TYPE dotSum = DATA_TYPE(0.0);
		DATA_TYPE fractionV1 = DATA_TYPE(0.0);
		DATA_TYPE fractionV2 = DATA_TYPE(0.0);
		for (COUNT_TYPE i = 0; i < v1Size; i++){
			dotSum += v1[i] * v2[i];
			fractionV1 += v1[i] * v1[i];
			fractionV2 += v2[i] * v2[i];
		}
		return ((dotSum) / (sqrt(fractionV1) * sqrt(fractionV2)));
	}

	return DATA_TYPE(0);
}
DATA_TYPE DistanceUtil::MinkowskiDistance(const Vector &v1, const Vector &v2, DATA_TYPE g){
	COUNT_TYPE v1Size = v1.Size();
	if (v1Size == v2.Size()){
		DATA_TYPE sum = 0.0;
		for (COUNT_TYPE i = 0; i < v1Size; i++){
			sum += pow(abs(v1[i] - v2[i]), g);
		}

		return pow(sum, DATA_TYPE(1.0) / g);
	}
	return DATA_TYPE(0);
}
DATA_TYPE DistanceUtil::EuclideanDistance(const Vector &v1, const Vector &v2){
	return MinkowskiDistance(v1, v2, 2);
}
DATA_TYPE DistanceUtil::ManhattanDistance(const Vector &v1, const Vector &v2){
	return MinkowskiDistance(v1, v2, 1);
}


/// Chao vao 2 ma tran a,b
/// Tao ra ma tran khoang cach voi [a.Col(dong) x b.Col(cot)]
Matrix DistanceUtil::Distance(Matrix &a, Matrix &b, DistanceType type, DATA_TYPE g, bool useRowAsDim){
	
	COUNT_TYPE row = (useRowAsDim) ? a.ColumnSize() : a.RowSize(); // Dong
	COUNT_TYPE col = (useRowAsDim) ? b.ColumnSize() : a.RowSize(); // Cot
	Matrix result(row, col);
		for (COUNT_TYPE i = 0; i < row; i++){
			for (COUNT_TYPE j = 0; j < col; j++){
				result(i, j) = DistanceUtil::Distance((useRowAsDim) ? a.GetColumn(i) : a.GetRow(i), (useRowAsDim) ? b.GetColumn(j) : b.GetRow(j), type, g);
		}
	}
		return result;
}



Vector DistanceUtil::Distance(Matrix &a, Vector &b, DistanceType type, DATA_TYPE g, bool useRowAsDim){

	COUNT_TYPE row = (useRowAsDim) ? a.ColumnSize() : a.RowSize(); // Dong
	COUNT_TYPE col = b.Size(); // Cot
	Vector result(row);
	for (COUNT_TYPE i = 0; i < row; i++){
		result[i] = DistanceUtil::Distance((useRowAsDim) ? a.GetColumn(i) : a.GetRow(i), b, type, g);
	}
	return result;
}
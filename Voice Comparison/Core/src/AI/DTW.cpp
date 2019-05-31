#include <AI\DTW.h>
#include <Util\MathUtil.h>
#include <Util\LogUtil.h>
#include <Util\DistanceUtil.h>

DATA_TYPE DTWUtil::DistanceOf2MatrixCol(Matrix &m1, Matrix &m2, bool normalize){
	COUNT_TYPE row = m1.ColumnSize();
	COUNT_TYPE col = m2.ColumnSize();

	Matrix dist(row, col);
	Matrix phase(row, col);
	dist(0, 0) = DistanceUtil::Distance(m1.GetColumn(0), m2.GetColumn(0));

	// calculate first row
	for (COUNT_TYPE i = 1; i < row; i++){
		DATA_TYPE path = DistanceUtil::Distance(m1.GetColumn(i), m2.GetColumn(0));;
		dist(i, 0) = dist(i - 1, 0) + path;
#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
		PRINT(" [%3d,0] = %2.5f %2.5f ", i, dist(i, 0), path);
#endif

	}

#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("\nInit Row\n");
	for (COUNT_TYPE i = row; i > 0; i--){
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT(" [%5d, %5d] = %2.5f ", i, j, dist(i - 1, j - 1));
		}
		PRINT("\n");
	}
#endif
	// calculate first column
	for (COUNT_TYPE j = 1; j < col; j++)
		dist(0, j) = dist(0, j - 1) + DistanceUtil::Distance(m1.GetColumn(0), m2.GetColumn(j));
	// fill matrix
#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("Init Col\n");
	for (COUNT_TYPE i = row; i > 0; i--){
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT(" [%5d, %5d] = %2.5f ", i, j, dist(i - 1, j - 1));
		}
		PRINT("\n");
	}

#endif

#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("Init Maxtrix\n");
	for (COUNT_TYPE i = row; i > 0; i--){
		//PRINT(" [Raw Row] = %2.5f ", m1[i - 1]);
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT(" [%5d, %5d] = %2.5f ", i, j, dist(i - 1, j - 1));
		}
		PRINT("\n");
	}
	PRINT("            ");
	//for (COUNT_TYPE j = 1; j <= col; j++){
	//	PRINT(" [Raw Col] = %2.5f ", vec[j - 1]);
	//}
	PRINT("\n");

#endif
	for (COUNT_TYPE i = 1; i < row; i++){
		for (COUNT_TYPE j = 1; j < col; j++){
			DATA_TYPE pha = 0;
			DATA_TYPE min = DTWUtil::DTWType1(dist(i, j - 1), dist(i - 1, j - 1), dist(i - 1, j), pha);
			phase(i, j) = pha;
			DATA_TYPE dis = DistanceUtil::Distance(m1.GetColumn(i), m2.GetColumn(j)); 
			dist(i, j) = min + dis;
#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
			PRINT("Row %d Col %d\n", row - i, col - j);
			for (COUNT_TYPE i = row; i > 0; i--){
				//PRINT(" [Raw Row] = %2.5f ", refVec[i - 1]);
				for (COUNT_TYPE j = 1; j <= col; j++){
					PRINT(" [%5d, %5d] = %2.5f ", i, j, dist(i - 1, j - 1));
				}
				PRINT("\n");
			}
			PRINT("            ");
			/*for (COUNT_TYPE j = 1; j <= col; j++){
				PRINT(" [RawCol] = %2.5f ", vec[j - 1]);
			}*/
			PRINT("\n");

#endif
		}
	}

#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("TRace Phase");
	for (COUNT_TYPE i = row; i > 0; i--){
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT("  %2.5f ", phase(i - 1, j - 1));
		}
		PRINT("\n");
	}
	PRINT("Cost path : %2.7f \n", dist(row - 1, col - 1));
#endif
	if (normalize){
		return dist(row - 1, col - 1) / (DATA_TYPE)(row + col);
	}
	else {
		return dist(row - 1, col - 1);
	}
}

Array1D DTWUtil::DistanceOf2Array3DHorizon(Array3D vec, COUNT_TYPE colVec, Array3D refVec, COUNT_TYPE colRefVec, bool normalize){
	Array1D out = ArrayUtil::CreateZeroArray1D(colVec);
	Array3D vecTran = MathUtil::TransposeMatrix(vec, colVec);
	COUNT_TYPE colVecTran = vec.size();
	Array3D refVecTran = MathUtil::TransposeMatrix(refVec, colRefVec);
	COUNT_TYPE colRefVecTran = refVec.size();
#ifdef _PRINT_DEBUG_DWT_STEP
	PRINT("Distance of 2 Vector:\n");
#endif
	for (COUNT_TYPE i = 0; i <refVecTran.size(); i++){

		out[i] = DistanceOf2Vector(vecTran[i], colVecTran, refVecTran[i], colRefVecTran, normalize);
#ifdef _PRINT_DEBUG_DWT_STEP
		PRINT(" [%d] = %3.5f  ", i, out[i]);
#endif
	}
#ifdef _PRINT_DEBUG_DWT_STEP
	PRINT(" \n");
#endif
	return out;
}

DATA_TYPE DTWUtil::DistanceOf2Array3DMatchEveryWhere(Array3D vec, COUNT_TYPE colVec, Array3D refVec, COUNT_TYPE colRefVec){
	// Check valid input
	if (vec.size() < refVec.size()){
		return DATA_TYPE(-1);
	}
#ifdef PRINT_DEBUG_DTW_MATCHEVERYWHERE
	PRINT("DTW match every where - Size vec %d  Size ref  %d\n", vec.size(), refVec.size());
#endif
	DATA_TYPE minPath = LDBL_MAX_10_EXP;
	COUNT_TYPE posMatch = 0;
	// Break each vector same size to compare
	COUNT_TYPE col = vec.size();
	for (COUNT_TYPE i = 0; i < col; i++){
		/*Array3D tmpVec = Array3D(i, i + refVec.size());
		DATA_TYPE path = DTWUtil::DistanceOf2Array3D(tmpVec, colVec, refVec, colRefVec);
		if (path < minPath){
			minPath = path;
			posMatch = it - refVec.begin();
		}*/
#ifdef PRINT_DEBUG_DTW_MATCHEVERYWHERE
		PRINT("Vector : %d Cost Path %2.7f \n", posMatch, path);
#endif
	}
	return minPath;
}

DATA_TYPE DTWUtil::DistanceOf2Array3D(Array3D vec, COUNT_TYPE colVec, Array3D refVec, COUNT_TYPE colRefVec, bool normalize){
#ifdef _PRINT_DEBUG_DWT_STEP
	PRINT("\n Calculator Distance of 2 Array 3D Vector: \n");
#endif
	COUNT_TYPE rowVec = vec.size();
	COUNT_TYPE rowRefVec = refVec.size();
	Array3D dist = ArrayUtil::CreateZeroArray3D(rowVec, rowRefVec);
	Array3D phase = ArrayUtil::CreateZeroArray3D(rowVec, rowRefVec);
	dist[0][0] = DistanceOf2Vector(vec[0], colVec, refVec[0], colRefVec, normalize);;
	
	//Row
	for (COUNT_TYPE frameV = 1; frameV < rowVec; frameV++){
		dist[frameV][0] = dist[frameV - 1][0] + DistanceOf2Vector(vec[frameV], colVec, refVec[0], colRefVec, normalize);
	}
	//Col
	for (COUNT_TYPE frameR = 1; frameR < rowRefVec; frameR++){
		dist[0][frameR] = dist[0][frameR - 1] + DistanceOf2Vector(vec[0], colVec, refVec[frameR], colRefVec, normalize);
	}
	// Other
	for (COUNT_TYPE frameV = 1; frameV < rowVec; frameV++){
		for (COUNT_TYPE frameR = 1; frameR < rowRefVec; frameR++){
			Array1D v = vec[frameV];
			Array1D r = refVec[frameR];
			//dis[frameV][frameR] = dis[frameV][frameR]  DistanceOf2Vector(v, colVec, r, colRefVec);
			DATA_TYPE pha = 0;
			DATA_TYPE min = DTWUtil::DTWType1(dist[frameV][frameR - 1], dist[frameV - 1][frameR - 1], dist[frameV - 1][frameR], pha);
			phase[frameV][frameR] = pha;

			DATA_TYPE dis = DistanceOf2Vector(vec[frameV], colVec, refVec[frameR], colRefVec, normalize);
			dist[frameV][frameR] = min + dis;
			
#ifdef _PRINT_DEBUG_DWT_DETAIL_3DVEC
			PRINT("Row %d Col %d\n", rowVec - frameV, rowRefVec - frameR);
			for (COUNT_TYPE i = rowVec; i > 0; i--){
				for (COUNT_TYPE j = 1; j <= rowVec; j++){
					PRINT(" [%5d, %5d] = %2.5f ", i, j, dist[i - 1][j - 1]);
				}
				PRINT("\n");
			}
			PRINT("\n");

#endif
		}
	}
#ifdef _PRINT_DEBUG_DWT_DETAIL_3DVEC
		PRINT("TRace Phase");
		for (COUNT_TYPE i = rowVec; i > 0; i--){
			for (COUNT_TYPE j = 1; j <= rowRefVec; j++){
			PRINT("  %2.5f ", phase[i - 1][j - 1]);
		}
		PRINT("\n");
	}
		PRINT("Cost path : %2.7f \n", dist[rowVec - 1][rowRefVec - 1]);
#endif
	return dist[rowVec - 1][rowRefVec - 1];
}

DATA_TYPE DTWUtil::DistanceOf2Vector(Array1D vec, COUNT_TYPE lenVec, Array1D refVec, COUNT_TYPE lenRefVec, bool normalize){

	COUNT_TYPE row = lenRefVec;
	COUNT_TYPE col = lenVec;
#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT(" Row %d Col %d \n", row, col);
	PRINT(" Print Ref Vec \n");
	for (COUNT_TYPE i = 0; i < row; i++){
		PRINT(" [%d] = %2.5f ", i, refVec[i]);
	}
	PRINT("\n");
	PRINT(" Print Vec\n");
	for (COUNT_TYPE i = 0; i < col; i++){
		PRINT(" [%5d] = %2.5f ", i, vec[i]);
	}
	PRINT("\n");
#endif


	Array3D dist = ArrayUtil::CreateZeroArray3D(row, col);
	Array3D phase = ArrayUtil::CreateZeroArray3D(row, col);
	dist[0][0] = MathUtil::Dist(vec[0], refVec[0]);

	// calculate first row
	for (COUNT_TYPE i = 1; i < row; i++){
		DATA_TYPE path = MathUtil::Dist(refVec[i], vec[0]);
		dist[i][0] = dist[i - 1][0] + path;
#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
		PRINT(" [%3d,0] = %2.5f %2.5f ", i, dist[i][0], path);
#endif

	}

#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("\nInit Row\n");
	for (COUNT_TYPE i = row; i > 0; i--){
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT(" [%5d, %5d] = %2.5f ", i, j, dist[i - 1][j - 1]);
		}
		PRINT("\n");
	}
#endif
	// calculate first column
	for (COUNT_TYPE j = 1; j < col; j++)
		dist[0][j] = dist[0][j - 1] + MathUtil::Dist(refVec[0], vec[j]);
	// fill matrix
#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("Init Col\n");
	for (COUNT_TYPE i = row; i > 0; i--){
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT(" [%5d, %5d] = %2.5f ", i, j, dist[i - 1][j - 1]);
		}
		PRINT("\n");
	}

#endif

#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("Init Maxtrix\n");
	for (COUNT_TYPE i = row; i > 0; i--){
		PRINT(" [Raw Row] = %2.5f ", refVec[i - 1]);
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT(" [%5d, %5d] = %2.5f ", i, j, dist[i - 1][j - 1]);
		}
		PRINT("\n");
	}
	PRINT("            ");
	for (COUNT_TYPE j = 1; j <= col; j++){
		PRINT(" [Raw Col] = %2.5f ", vec[j - 1]);
	}
	PRINT("\n");

#endif
	for (COUNT_TYPE i = 1; i < row; i++){
		for (COUNT_TYPE j = 1; j < col; j++){
			DATA_TYPE pha = 0;
			DATA_TYPE min = DTWUtil::DTWType1(dist[i][j - 1], dist[i - 1][j - 1], dist[i - 1][j], pha);
			phase[i][j] = pha;
			DATA_TYPE dis = MathUtil::Dist(refVec[i], vec[j]);
			dist[i][j] = min + dis;
#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
			PRINT("Row %d Col %d\n", row - i, col - j);
			for (COUNT_TYPE i = row; i > 0; i--){
				PRINT(" [Raw Row] = %2.5f ", refVec[i - 1]);
				for (COUNT_TYPE j = 1; j <= col; j++){
					PRINT(" [%5d, %5d] = %2.5f ", i, j, dist[i - 1][j - 1]);
				}
				PRINT("\n");
			}
			PRINT("            ");
			for (COUNT_TYPE j = 1; j <= col; j++){
				PRINT(" [RawCol] = %2.5f ", vec[j - 1]);
			}
			PRINT("\n");

#endif
		}
	}

#ifdef _PRINT_DEBUG_DWT_DETAIL_VECTOR
	PRINT("TRace Phase");
	for (COUNT_TYPE i = row; i > 0; i--){
		for (COUNT_TYPE j = 1; j <= col; j++){
			PRINT("  %2.5f ", phase[i - 1][j - 1]);
		}
		PRINT("\n");
	}
	PRINT("Cost path : %2.7f \n", dist[row - 1][col - 1]);
#endif
	if (normalize){
		return dist[row - 1][col - 1] / (DATA_TYPE)(row + col);
	}
	else {
		return dist[row - 1][col - 1];
	}
}

DATA_TYPE DTWUtil::DTWType1(DATA_TYPE a /* 180 [row][col -1]*/, DATA_TYPE b/* 225 [row -1 ][col -1]*/, DATA_TYPE c /* 270 [row -1][col]*/, DATA_TYPE &pha/* Phase */){

	DATA_TYPE min = b;
	pha = 225;
	if (b > a){
		min = a;
		pha = 180;
	}
	if (min > c){
		min = c;
		pha = 270;
	}
	return min;
}
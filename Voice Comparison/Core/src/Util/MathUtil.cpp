#include <Util/MathUtil.h>
#include <string.h>
#include <Util/LogUtil.h>
 std::vector< DATA_TYPE**> ArrayUtil::manageArray2D;


 DATA_TYPE * ArrayUtil::CreateZeroArray1D(COUNT_TYPE size){
	 DATA_TYPE *tmp = NULL;
	try{
		
		tmp= new DATA_TYPE[size];
		memset(tmp, 0, sizeof(DATA_TYPE)* size);
		return tmp;
	}
	catch (...){

	}
	return NULL;
}


DATA_TYPE** ArrayUtil::CreateZeroArray2D(COUNT_TYPE row, COUNT_TYPE col){
	try{
		DATA_TYPE ** tmp = new DATA_TYPE *[row];
		for (COUNT_TYPE r = 0; r < row; r++){
			tmp[r] = CreateZeroArray1D(col);
		}
		return tmp;
	}
	catch (...){

	}
	return NULL;
}

Array3D ArrayUtil::CreateZeroArray3D(COUNT_TYPE row, COUNT_TYPE col){
	Array3D tmp = Array3D();
	try{
		for (COUNT_TYPE i = 0; i < row; i++){
			tmp.push_back(ArrayUtil::CreateZeroArray1D(col));
		}
	}
	catch (...){

	}
	return tmp;
}


DATA_TYPE** ArrayUtil::FreeArray2D(COUNT_TYPE row, COUNT_TYPE col){
	return NULL;
}
Array1D ArrayUtil::FreeArray1D(void * ptr){
		
	/*for (COUNT_TYPE i = 0; i < manageArray1D.size(); i++){
		if (ptr == (void *)manageArray1D[i]){
			delete ptr;
			manageArray1D[i] = NULL;
		}
	}*/

	return NULL;
}
Array1D ArrayUtil::SetZeroArray1D(void * ptr){
	return NULL;
}
Array3D ArrayUtil::AvgArray3D(Array3D in1, Array3D in2){
	Array3D out;

	return out;
}

DATA_TYPE MathUtil::Max(DATA_TYPE a, DATA_TYPE b){
	return (a > b) ? a : b;
}

DATA_TYPE MathUtil::Max(DATA_TYPE a, DATA_TYPE b, DATA_TYPE c){
	return Max(Max(a, b), c);
}

DATA_TYPE MathUtil::Min(DATA_TYPE a, DATA_TYPE b){
	return (a < b) ? a : b;
}

DATA_TYPE MathUtil::Min(DATA_TYPE a, DATA_TYPE b, DATA_TYPE c){
	return Min(Min(a, b), c);
}

DATA_TYPE MathUtil::Dist(DATA_TYPE a, DATA_TYPE b){
	return sqrt(pow((a - b), 2));
}

DATA_TYPE MathUtil::DistEuler(DATA_TYPE a, DATA_TYPE b){
	return sqrt(a * a + b * b);
}

Array3D MathUtil::TransposeMatrix(Array3D mtrix, COUNT_TYPE col){
	COUNT_TYPE row = mtrix.size();
	Array3D out = ArrayUtil::CreateZeroArray3D(col, row);
	COUNT_TYPE outR = col;
	COUNT_TYPE outC = row;

	for (COUNT_TYPE r = 0; r < outR; r++){
		for (COUNT_TYPE c = 0; c < outC; c++){
			out[r][c] = mtrix[c][r];
		}
	}
	return out;
}

void OptionUtil::SetEnableLog(int mode){
	Logger::SetEnabled(mode);
}


void OptionUtil::SeparateLog(){
	Logger::SeparateLog();
}

#pragma once
#ifndef DTW_WRAPPER_H
#define DTW_WRAPPER_H

#include <AI\DTW.h>
#include "UtilWrapper.h"
using namespace System;
using namespace System::Collections::Generic;
namespace ExtractionWrapper{
	public ref class DTWUtilWrapper{
	public:
		//Vector 3D
		static List<DATA_TYPE>^ DistanceOf2Array3DHorizon(List<List<DATA_TYPE>^>^ vec, List<List<DATA_TYPE>^>^ refVec, bool normalize);
		static DATA_TYPE DistanceOf2Array3DMatchEveryWhere(List<List<DATA_TYPE>^>^ vec, List<List<DATA_TYPE>^>^ refVec);
		static DATA_TYPE DistanceOf2Array3D(List<List<DATA_TYPE>^>^ vec, List<List<DATA_TYPE>^>^ refVec, bool normalize);

		// Vector 1D
		static DATA_TYPE DistanceOf2Vector(List<DATA_TYPE>^ vec, List<DATA_TYPE>^ refVec, bool normalize);
		static DATA_TYPE DistanceOf2Vector(List<List<DATA_TYPE>^>^  vec, List<List<DATA_TYPE>^>^ refVec, bool normalize);
	};
}

#endif
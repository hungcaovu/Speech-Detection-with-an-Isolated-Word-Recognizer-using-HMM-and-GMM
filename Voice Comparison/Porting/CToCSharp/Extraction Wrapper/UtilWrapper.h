#ifndef UTIL_WRAPPER_H
#define UTIL_WRAPPER_H
#include "Common.h"
#include <Util\MathUtil.h>
#include <Util\Matrix.h>
#include <Util\File.h>
#include <vector>
#include <vcclr.h>
using namespace System;
using namespace System::Collections::Generic;

namespace ExtractionWrapper{
	class UtilWrapper{
	public:
		static DATA_TYPE* ConverListToPtr(List<DATA_TYPE> ^ list);
		static List<DATA_TYPE>^ ConverPtrToList(DATA_TYPE *ptr, COUNT_TYPE size);
		static List<DATA_TYPE>^ ConverVectorToList(std::vector<DATA_TYPE> vec);
		static List<COUNT_TYPE>^ ConverVectorToList(std::vector<COUNT_TYPE> vec);

		static List<List<DATA_TYPE>^>^ ConverVectorPtrToListList(std::vector<DATA_TYPE*> vec, COUNT_TYPE col);
		static List<List<DATA_TYPE>^>^ MatrixToListList(Matrix &vec);
		static Matrix ListListToMatrix(List<List<DATA_TYPE>^>^ list);
		static Vector ListToVector(List<DATA_TYPE>^ list);
		static List<DATA_TYPE>^ VectorToList(Vector &vec);
		static std::vector<DATA_TYPE*> ConverListListToVectorPtr(List<List<DATA_TYPE>^>^ vec, COUNT_TYPE &col);
		static char* ConvertStringToChar(String ^ str);
		static String^ ConvertCharToString(char *str);

		static std::vector<std::string> ConvertListStringToVectorString(List<String ^>^ listStr);
		static std::vector<Matrix> ConvertTripListToVectorMatrix(List<List<List<DATA_TYPE>^>^> ^ data);
	};

	public ref class FileWrapper{
	public:
		static bool Write(String ^path, List<DATA_TYPE>^ data);
		static bool Write(String ^path, List<List<DATA_TYPE>^>^ data);

		static bool Read(String ^path, List<List<DATA_TYPE>^>^% data);
		static bool Read(String ^path, List<DATA_TYPE>^% data);
	};

	public ref class OptionWrapper{
		public:
			static void SetLog(int mode);
			static void SeparateLog();
	};
};

#endif
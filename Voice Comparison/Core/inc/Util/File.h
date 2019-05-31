#include "Common.h"
#include <stdio.h>
#include <vector>
#include <windows.h>
class File{
public:
	static bool Write(char *path, Array3D data, COUNT_TYPE row);
	static bool Write(char *path, DATA_TYPE* data, COUNT_TYPE size);

	static bool Read(char *path, Array3D &data, COUNT_TYPE &row);
	static bool Read(char *path, DATA_TYPE* &data, COUNT_TYPE &size);

	static bool Write(FILE * fp, DATA_TYPE* data, COUNT_TYPE size);
	static bool Read(FILE * fp, DATA_TYPE* &data, COUNT_TYPE size);

	static std::vector<std::string> GetListFilesInDir(LPCWSTR path);
};

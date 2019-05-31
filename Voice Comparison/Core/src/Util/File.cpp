#include <Util/File.h>
#include <Util/MathUtil.h>

bool File::Write(FILE * fp, DATA_TYPE* data, COUNT_TYPE size){
	if (data != NULL){
		for (COUNT_TYPE i = 0; i < size; i++){
			fprintf(fp, " %.10f ", data[i]);
		}
		return true;
	}
	else {
		return false;
	}
}

bool File::Write(char *path, Array3D data, COUNT_TYPE row){
	FILE * fp = NULL;
	fp = fopen(path, "w");
	COUNT_TYPE col = data.size(); // List of array. size of array is col and size of each col is row
	if (fp != NULL){
		fprintf(fp, "Col %d Row %d:", col, row);
		for (COUNT_TYPE i = 0; i < col; i++){
			fprintf(fp, "\nCol %d: ", i);
			File::Write(fp, data[i], row);
			fflush(fp);
		}
		fclose(fp);
		return true;
	}
	else{
		return false;
	}
}

bool File::Write(char *path, DATA_TYPE* data, COUNT_TYPE size){
	FILE * fp = NULL;
	fp = fopen(path, "w");
	if (fp != NULL){
		fprintf(fp, "Size %d :\n", size);
		File::Write(fp, data, size);
		fclose(fp);
		return true;
	}
	else{
		return false;
	}
}

bool File::Read(char *path, Array3D &data, COUNT_TYPE &row){
	FILE *fp = fopen(path, "r");
	if (fp != NULL){
		COUNT_TYPE col = 0;
		fscanf(fp, "Col %d Row %d:", &col, &row);
		if (col > 0 && row > 0){
			for (COUNT_TYPE i = 0; i < col; i++){
				COUNT_TYPE tmpCol = 0;
				fscanf(fp, "\nCol %d:", &tmpCol);
				if (tmpCol == i){
					DATA_TYPE * tmp = NULL;
					if (File::Read(fp, tmp, row) && tmp != NULL){
						data.push_back(tmp);
					}
					else {
						fclose(fp);
						return false;
					}
				}
				else{
					fclose(fp);
					return false;
				}
			}
			fclose(fp);
			return true;
		}
		return false;
	}
	else {
		return false;
	}
}

bool File::Read(char *path, DATA_TYPE* &data, COUNT_TYPE &size){
	FILE *fp = fopen(path, "r");
	if (fp != NULL){
		fscanf(fp, "Size %d :", &size);
		if (size > 0){
			File::Read(fp, data, size);
			fclose(fp);
			return true;
		}
		fclose(fp);
	}
	return false;
}

bool File::Read(FILE * fp, DATA_TYPE* &data, COUNT_TYPE size){
	data = ArrayUtil::CreateZeroArray1D(size);
	for (COUNT_TYPE i = 0; i < size; i++){
		DATA_TYPE value = 0.0f;
		fscanf(fp, "  %f", &value);
		data[i] = value;
	}
	return true;
}


std::vector<std::string> File::GetListFilesInDir(LPCWSTR path){
	WIN32_FIND_DATA search_data;

	memset(&search_data, 0, sizeof(WIN32_FIND_DATA));

	HANDLE handle = FindFirstFile(path, &search_data);
	std::vector<std::string> list;

	while (handle != INVALID_HANDLE_VALUE)
	{
		std::wstring ws(search_data.cFileName);
		std::string file = std::string(ws.begin(), ws.end());
		list.push_back(file);

		if (FindNextFile(handle, &search_data) == FALSE)
			break;
	}

	//Close the handle after use or memory/resource leak
	FindClose(handle);
	return list;
}

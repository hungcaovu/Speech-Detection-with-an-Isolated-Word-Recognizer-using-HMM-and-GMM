#include <Util\MatrixIdx.h>
#include <Util\LogUtil.h>

bool MatrixIdx::Save(TiXmlElement *matrix){

	matrix->SetAttribute("row", row);
	//Col
	matrix->SetAttribute("col", column);

	for (COUNT_TYPE r = 0; r < row; r++){
		char str_row[100] = { 0 };
		sprintf(str_row, "Row_%010d", r);
		TiXmlElement *row_e = new TiXmlElement(str_row);
		for (COUNT_TYPE c = 0; c < column; c++){
			char str_name[100] = { 0 };
			sprintf(str_name, "R%010d_C%010d", r, c);
			char dig[100] = { 0 };
			sprintf(dig, "%010.10lf", double(_[r*column + c]));
			row_e->SetAttribute(str_name, dig);
		}
		matrix->LinkEndChild(row_e);
	}
	return true;
}
bool MatrixIdx::Save(char *path){
	TiXmlDocument doc;
	//Data
	TiXmlElement *matrix = new TiXmlElement("Matrix");
	Save(matrix);
	doc.LinkEndChild(matrix);
	return doc.SaveFile(path);
}

bool MatrixIdx::Load(TiXmlElement *matrix){
	if (matrix != NULL){
		int lrow = 0;
		int lcol = 0;
		matrix->Attribute("row", &lrow);
		matrix->Attribute("col", &lcol);
		Resize(lrow, lcol, false);
		for (COUNT_TYPE r = 0; r < row; r++){
			char str_row[100] = { 0 };
			sprintf(str_row, "Row_%010d", r);
			TiXmlElement *row_e = matrix->FirstChildElement(str_row);
			if (row_e != NULL){
				for (COUNT_TYPE c = 0; c < column; c++){
					char str_name[100] = { 0 };
					sprintf(str_name, "R%010d_C%010d", r, c);
					INDEX_TYPE value = INIT_INDEX_TYPE;
					const char *text = row_e->Attribute(str_name, &value);
					if (text != NULL){
						// Can thiet phai change
						_[r*column + c] = value;
					}
					else {
						return false;
					}
				}
			}
			else {
				return false;
			}
		}
	}
	else {
		return false;
	}

	return true;
}
bool MatrixIdx::Load(char *path){
	TiXmlDocument doc;
	doc.LoadFile(path);
	TiXmlElement *e = doc.FirstChildElement("Matrix");
	return Load(e);
}

void MatrixIdx::Print(LOG_LEVEL level)
{
	PRINT(level | NONE_TIME, "MatrixIdx Row %d - Col %d\n", row, column);
	for (unsigned int j = 0; j < row; j++){
		for (unsigned int i = 0; i < column; i++)
			PRINT(level | NONE_TIME, "[%d,%d] = %10d ", i, j, _[j*column + i]);
		PRINT(level | NONE_TIME, "\n");
	}
}

int   MatrixIdx::bInverseOk = true;


#include <Util\VectorIdx.h>
#include <Util\Vector.h>
#include <Util\LogUtil.h>
int compare(void *context, const void *e1, const void *e2);
int compare(void *context, const void *e1, const void *e2){
	DATA_TYPE *dis = (DATA_TYPE *)context;
	INDEX_TYPE *idx1 = (INDEX_TYPE*)e1;
	INDEX_TYPE *idx2 = (INDEX_TYPE*)e2;

	DATA_TYPE d1 = dis[*idx1];
	DATA_TYPE d2 = dis[*idx2];
	if (d1 > d2)
		return 1;
	else if (d1 < d2)
		return -1;
	else return 0;
}
void VectorIdx::Sort(Vector &d){
	qsort_s((void *)_, row, sizeof(INDEX_TYPE), compare, (void *)d._);
}

bool VectorIdx::Save(TiXmlElement *vector){
	vector->SetAttribute("size", row);
	for (COUNT_TYPE r = 0; r < row; r++){
		char str_name[100] = { 0 };
		sprintf(str_name, "C%010d", r);
		char dig[100] = { 0 };
		sprintf(dig, "%010.10lf", double(_[r]));
		vector->SetAttribute(str_name, dig);
	}
	return true;
}
bool VectorIdx::Save(char *path){
	TiXmlDocument doc;
	TiXmlElement *vector = new TiXmlElement("Vector");
	Save(vector);
	doc.LinkEndChild(vector);
	return doc.SaveFile(path);
}
bool VectorIdx::Load(TiXmlElement *vector){
	int lr = 0;
	if (vector != NULL){
		const char * str_row = vector->Attribute("size", &lr);
		Resize(lr, false);
		if (str_row != NULL){
			for (COUNT_TYPE r = 0; r < row; r++){
				char str_name[100] = { 0 };
				sprintf(str_name, "C%010d", r);
				double value = 0.0;
				const char * str_value = vector->Attribute(str_name, &value);
				if (str_value != NULL){
					_[r] = INDEX_TYPE(value);
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
	else {
		return false;
	}
	return false;
}
bool VectorIdx::Load(char *path){
	TiXmlDocument doc;
	doc.LoadFile(path);
	TiXmlElement *vector = doc.FirstChildElement("Vector");
	return Load(vector);
}

void VectorIdx::Print(LOG_LEVEL level)
{
	PRINT(level | NONE_TIME, "VectorIdx: %d - ", row);
	for (unsigned int i = 0; i < row; i++)
		PRINT(level | NONE_TIME, " [%d] = %10d ", i, _[i]);
	PRINT(level | NONE_TIME, "\n");
}

INDEX_TYPE VectorIdx::undef = -1;



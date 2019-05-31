
#include <Util\Vector.h>
#include <Util\LogUtil.h>

bool Vector::Save(TiXmlElement *vector){
	vector->SetAttribute("size", row);
	for (COUNT_TYPE r = 0; r < row; r++){
		char str_name[100] = { 0 };
		sprintf(str_name, "C%010d", r);
		char dig[100] = { 0 };
#ifdef USE_EXPONENT_NUMBER
		sprintf(dig, "%e", double(_[r]));
#else
		sprintf(dig, "%040.20f", double(_[r]));
#endif
		vector->SetAttribute(str_name, dig);
	}
	return true;
}
bool Vector::Save(char *path){
	TiXmlDocument doc;
	TiXmlElement *vector = new TiXmlElement("Vector");
	Save(vector);
	doc.LinkEndChild(vector);
	return doc.SaveFile(path);
}
bool Vector::Load(TiXmlElement *vector){
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
					_[r] = DATA_TYPE(value);
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
	return true;
}
bool Vector::Load(char *path){
	TiXmlDocument doc;
	doc.LoadFile(path);
	TiXmlElement *vector = doc.FirstChildElement("Vector");
	return Load(vector);
}

void Vector::Print(LOG_LEVEL level)
{
	PRINT(level | NONE_TIME, "Vector: %d - ", row);
	for (unsigned int i = 0; i < row; i++)
		PRINT(level | NONE_TIME, " %040.20f ", _[i]);
	PRINT(level | NONE_TIME, "\n");
}

DATA_TYPE Vector::undef = INIT_DATA_TYPE;



#include <Common.h>
#include <Util\Vector.h>
class Dct{
private:
	DATA_TYPE ** w;
	COUNT_TYPE nceptrums;
	COUNT_TYPE nfilters;
public:
	Dct(COUNT_TYPE nceptrums, COUNT_TYPE nfilters){
		this->nceptrums = nceptrums;
		this->nfilters = nfilters;
		init();
	};
	void init();
	Vector Process(Vector &frame);
	~Dct();
};

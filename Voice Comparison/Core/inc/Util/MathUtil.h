#define Round(x) (int)((float)(x)+0.499999999999999)
#ifndef PI
# define PI	3.14159265358979323846264338327950288
#endif



#ifndef PIf
#define PIf 3.14159265358979323846f
#endif

#ifndef DEG2RAD
#define DEG2RAD(x) ((x)*(PIf/180.0f))
#endif

#ifndef RAD2DEG
#define RAD2DEG(x) ((x)*(180.0f/PIf))
#endif

#ifndef RND
#define RND(x) (float((x)*((double)rand())/((double)(RAND_MAX+1.0))))
#endif

#ifndef MIN
#define MIN(x,y) (((x)<(y))?(x):(y))
#endif

#ifndef MAX
#define MAX(x,y) (((x)>(y))?(x):(y))
#endif

#ifndef TRUNC
#define TRUNC(x,mn,mx) (MIN(MAX((x),(mn)),(mx)))
#endif

#ifndef SIGN2
#define SIGN(x) (((x)<0.0f)?(-1.0f):(1.0f))
#endif

#ifndef SIGN2
#define SIGN2(a,b) ((b) >= 0.0f ? fabs(a) : -fabs(a))
#endif

#ifndef ROUND
#define ROUND(x) (floor((x)+0.5f))
#endif

#ifndef EPSILON
#define EPSILON   (1e-13)
#endif

#ifndef _EPSILON
#define _EPSILON   (1e-10)
#endif

#ifndef MATH_UTIL_H
#define MATH_UTIL_H
#include "Common.h"
#include <math.h>
inline DATA_TYPE hypot_s(DATA_TYPE a, DATA_TYPE b){
	DATA_TYPE r;
	if (abs(a) > abs(b)) {
		r = b / a;
		r = abs(a) * sqrt((DATA_TYPE)1.0 + r * r);
	}
	else if (b != 0.0f) {
		r = a / b;
		r = abs(b) * sqrt((DATA_TYPE)1.0 + r * r);
	}
	else {
		r = INIT_DATA_TYPE;
	}
	return r;
}


class ArrayUtil{
private:
	static std::vector< DATA_TYPE**> manageArray2D;
	//static Array3D manageArray1D;
public:

	static DATA_TYPE* CreateZeroArray1D(COUNT_TYPE size);
	static DATA_TYPE** CreateZeroArray2D(COUNT_TYPE row, COUNT_TYPE col);
	static Array3D CreateZeroArray3D(COUNT_TYPE row, COUNT_TYPE col);

	static DATA_TYPE* SetZeroArray1D(void * ptr);

	// Implement later
	static DATA_TYPE** FreeArray2D(COUNT_TYPE row, COUNT_TYPE col);
	static Array1D FreeArray1D(void * ptr);
	static Array3D AvgArray3D(Array3D in1, Array3D in2);
};


class MathUtil{
public:
	static Array3D TransposeMatrix(Array3D mtrix, COUNT_TYPE col);
	static DATA_TYPE Max(DATA_TYPE a, DATA_TYPE b, DATA_TYPE c);
	static DATA_TYPE Max(DATA_TYPE a, DATA_TYPE b);

	static DATA_TYPE Min(DATA_TYPE a, DATA_TYPE b, DATA_TYPE c);
	static DATA_TYPE Min(DATA_TYPE a, DATA_TYPE b);

	static DATA_TYPE Dist(DATA_TYPE a, DATA_TYPE b);
	static DATA_TYPE DistEuler(DATA_TYPE a, DATA_TYPE b);
};

class OptionUtil{
public :
	static void SetEnableLog(int mode);
	static void SeparateLog();
};
#endif

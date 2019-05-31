#include <stdio.h>
#include <Util\VectorIdx.h>
#include <Util\MatrixIdx.h>

#include <Util\LogUtil.h>

void main(){

	Logger::SetEnabled(true);

	VectorIdx a(5);
	a[0] = 1;
	a[1] = 2;
	a[2] = 3;
	a[3] = 4;
	a[4] = 5;

	a.Print();


	MatrixIdx b;
	b.PlushBackColumn(a);
	b.PlushBackColumn(a);
	b.PlushBackColumn(a);
	b.PlushBackColumn(a);
	b.PlushBackColumn(a);

	b.PlushBackRow(a);
	b.Print();


	VectorIdx c = b.GetColumn(3);
	c.Print();


	c.Remove(2);

	c.Remove(2);
	c.Print();

	//VectorIdx d = b.GetRow(3);
	//d.Print();


}

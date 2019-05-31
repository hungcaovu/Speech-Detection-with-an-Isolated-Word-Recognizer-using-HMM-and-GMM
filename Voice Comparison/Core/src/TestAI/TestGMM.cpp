#include <Util\Matrix.h>
#include <AI\Clustering.h>
#include <AI\DTW.h>
void main(){
	/*Matrix a(3, 5);
	a(0, 0) = 0; a(0, 1) = 1; a(0, 2) = 2; a(0, 3) = 3; a(0, 4) = 4;
	a(1, 0) = 1; a(1, 1) = 2; a(1, 2) = 2; a(1, 3) = 3; a(1, 4) = 4;
	a(2, 0) = 2; a(2, 1) = 0; a(2, 2) = 2; a(2, 3) = 3; a(2, 4) = 4;
	Vector &l2 = a.GetRow(1);
	l2.Save("Vector.xml");
	l2.Print();
	Vector b;
	b.Load("Vector.xml");
	b.Print();
	*/
	/*Logger::SetEnabled(true);
	Matrix a(1, 5);
	a(0, 0) = 1; a(0, 1) = 2; a(0, 2) = 3; a(0, 3) = 4; a(0, 4) = 5; 
	Matrix b(1, 7);
	b(0, 0) = 1; b(0, 1) = 2; b(0, 2) = 3; b(0, 3) = 4; b(0, 4) = 5; b(0, 5) = 6; b(0, 6) = 7;
	DTWUtil::DistanceOf2MatrixCol(a,b);*/
	
	//a.Save("test.xml");

	//Matrix b;
	//b.Load("test.xml");
	//b.Print();
	// Test Remove Row/ Col
	/*Matrix b = a.RemoveColumn(3);
	a.Print();
	b.Print();
	Matrix c = a.RemoveRow(1);
	c.Print();*/
	// PushBackRow/Col
	/*a.Print();
	Vector &l2 = a.GetRow(1);
	l2.Print();
	a.PlushBackRow(l2);
	a.Print();*/

	// Test Update Center
	//Matrix m(1,5);
	//m(0, 0) = 0; m(0, 1) = 1; m(0, 2) = 2; m(0, 3) = 3; m(0, 4) = 4;
	//KMeanCluster k;
	//COUNT_TYPE idx = k.FindCenter(m);
	/*
	Test K Mean
	*/
	Logger::SetEnabled(15);
	//2 4 10 12 3 20 30 11 25 23
	Matrix a(2, 5);
	a(0, 0) = 0; a(0, 1) = 1; a(0, 2) = 2; a(0, 3) = 3; a(0, 4) = 4;
	a(1, 0) = 1; a(1, 1) = 2; a(1, 2) = 2; a(1, 3) = 3; a(1, 4) = 4;
	a(2, 0) = 2; a(2, 1) = 0; a(2, 2) = 2; a(2, 3) = 3; a(2, 4) = 4;
	PRINT(DATA, "Matrix a:\n");
	a.Print();
	Vector mean(2);
	mean[0] = 5;
	mean[1] = 2;
	PRINT(DATA, "Mean:\n");
	mean.Print();

	Vector var = a.Var(mean);

	PRINT(DATA, "Var:\n");
	var.Print();
}
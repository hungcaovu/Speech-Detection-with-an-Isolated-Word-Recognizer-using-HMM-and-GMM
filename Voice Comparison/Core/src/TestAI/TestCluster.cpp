#include <Util\Matrix.h>
#include <AI\Clustering.h>
#include <AI\DTW.h>
#include <AI\GMM.h>
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
	
	//Matrix a(1, 10);
	/// Number //2 4 10 12 3 20 30 11 25 23
	//a(0, 0) = 2; a(0, 1) = 4; a(0, 2) = 10; a(0, 3) = 12; a(0, 4) = 3;
	//a(0, 5) = 20; a(0, 6) = 30; a(0, 7) = 11; a(0, 8) = 25; a(0, 9) = 23;

	//Number  1 -> 10
	/*a(0, 0) = 1;
	a(0, 1) = 2;
	a(0, 2) = 3;
	a(0, 3) = 4;
	a(0, 4) = 5;
	a(0, 5) = 6;
	a(0, 6) = 7;
	a(0, 7) = 8;
	a(0, 8) = 9;
	a(0, 9) = 10;*/
	//a.Random();
	//a *= 100;
	//PRINT("Matrix \n");
	//Matrix var(3,3);
	//var(0, 0) = 1; var(0, 1) = 0; var(0, 2) = 0;
	//var(1, 0) = 0; var(1, 1) = -3; var(1, 2) = 0;
	//var(2, 0) = 0; var(2, 1) = 0; var(2, 2) = 4;
	//var.Print();
	//PRINT("Vector \n");
	//Vector xm(3);
	//xm(0) = 2; xm(1) = 3; xm(2) = 4;
	//xm.Print();
	//PRINT("1/Var \n");
	//Matrix div = 1.0f / var;
	//div.Print();

	//PRINT("Frist Mul \n");
	//// Ma Tran div M Dong N Cot x N Dong 1 Cot = M dong
	//Vector r1 = div * xm;
	//r1.Print();

	//PRINT("Second Mul \n");

	//DATA_TYPE r2 = xm * r1;
	//PRINT("Second Mul:  %f \n", r2);


	// Tinh Dinh Thuc
	//Matrix a(3, 3);
	//a(0, 0) = -1; a(0, 1) = 2; a(0, 2) = 3;
	//a(1, 0) = 2; a(1, 1) = -1; a(1, 2) = 4;
	//a(2, 0) = 0; a(2, 1) = -3; a(2, 2) = 2;
	// Det =  -36
	/*Matrix a(4, 4);
	a(0, 0) = 5; a(0, 1) = 4; a(0, 2) = 2; a(0, 3) = 1;
	a(1, 0) = 2; a(1, 1) = 3; a(1, 2) = 1; a(1, 3) = -2;
	a(2, 0) = -5; a(2, 1) = -7; a(2, 2) = -3; a(2, 3) = 9;
	a(3, 0) = 1; a(3, 1) = -2; a(3, 2) = -1; a(3, 3) = 4;*/
	// Det A =  38
	/*DATA_TYPE d = 0;
	a.Inverse(&d); 
	a.Print();
	PRINT("DET = %f\n", d);*/





	


	



	/*a(0, 0) = 1; a(0, 1) = 2; a(0, 2) = 3; a(0, 3) = 4; a(0, 4) = 5;
	a(0, 5) = 6; a(0, 6) = 7; a(0, 7) = 8; a(0, 8) = 9; a(0, 9) = 10;
	a -= 1;*/
	//KMeanCluster kmean1(3);
	//kmean1.DoClustering(a);

	//kmean1.Print();

	//PRINT("*************************\n********************\n********************");
	/*
	KMeanCluster kmean2(10, ONE_D, 20);
	kmean2.DoClustering(a);

	kmean2.Print();*/
	// Test sort vector
	/*VectorIdx a(5);
	a[0] = 4;
	a[1] = 3;
	a[2] = 2;
	a[3] = 1;
	a[4] = 0;

	a.Print();

	Vector d(5);

	d[0] = 9;
	d[1] = 8;
	d[2] = 7;
	d[3] = 6;
	d[4] = 5;

	d.Print();

	a.Sort(d);
	a.Print();*/

Matrix a(1, 10);
a.Load("DataCluster.xml");
COUNT_TYPE n = 1;
GMM gmm(n, 2);
gmm.DoEM(a);

gmm.Print();
/*
KMeanCluster kmean(2);
kmean.DoClustering(a);
kmean.Print();
*/
#if 0
Matrix a;
//a(0, 0) = 1; a(0, 1) = 3; a(0, 2) = 5; a(0, 3) = 8; a(0, 4) = 2; a(0, 5) = 3;
//a(1, 0) = 2; a(1, 1) = 2; a(1, 2) = 3; a(1, 3) = 9; a(1, 4) = 1; a(1, 5) = 4;
a.Load("DataTest.xml");

KMeanCluster kmean(3);
kmean.DoClustering(a);
kmean.Print();


GMM gmm(3, COV_TYPE_02);
gmm.DoEM(a);
gmm.Print();

for (COUNT_TYPE i = 0; i < a.ColumnSize(); i++){
	PRINT(DATA, " %d = %f ", i, gmm.Probability(a.GetColumn(i)));
}

#endif
//a.Random();
//a.ClearRow(1);
//a.Print();
//Matrix b(2, 2);
//b(0, 0) = 3; b(0, 1) = 2;
//b(1, 0) = 2; b(1, 1) = 3;
////b.Random();

//
//gmm.Save("GMM.xml");

//GMM gmm2;
//gmm2.Load("GMM.xml");
//DATA_TYPE prob = gmm.Probability(b.GetColumn(1));
//
//PRINT("Probe B = %f\n", prob);
//
//for (COUNT_TYPE c = 0; c < a.ColumnSize(); c++){
//	DATA_TYPE proa = gmm.Probability(a.GetColumn(c));
//	PRINT("Probe A = %f\n", proa);
//}

//gmm2.DoEM();
//for (COUNT_TYPE c = 0; c < 2; c++){
//	DATA_TYPE proa = gmm2.Probability(gmm2.GetMean(c));
//	PRINT("Mean Group %d = %f\n",c, proa);
//}
//Matrix b;
//b.Load("revData.xml");


//GMM gmm(2, COV_TYPE_02);
//gmm.DoEM(a);
////
//gmm.Print();
//gmm.Save("FirstTest2.xml");

//KMeanCluster kmean1(2);
//kmean1.DoClustering(a);
//
//kmean1.Print();
}

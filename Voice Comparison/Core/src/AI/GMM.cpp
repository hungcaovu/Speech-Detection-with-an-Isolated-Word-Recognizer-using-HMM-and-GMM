#include <AI\GMM.h>
#include <AI\Clustering.h>
GMM::GMM(char * path){
	PRINT(STEP, "Create GMM: Load file %s", path);
	this->Load(path);
}

GMM::GMM(COUNT_TYPE components, COUNT_TYPE coVarType, COUNT_TYPE maxIterator){
	_numComponents = components;
	_coVarType = coVarType;
	_maxIterator = maxIterator;
	PRINT(STEP, "Create GMM: Component - %d CoverType = %d\n", _numComponents, _coVarType);
}
void GMM::Init(){
#ifdef PRINT_DEBUG_GMM_INIT
	PRINT(INFORMATION, "*****************Start Init GMM*****************\n");
#endif
	_isSuccess = true;
	_thresholdLog = DATA_TYPE(1e-10);
	KMeanCluster kMean(_numComponents);
	kMean.DoClustering(_mData);
#ifndef PRINT_DEBUG_GMM_INIT
	PRINT(INFORMATION, "Kmean Completed\n");
	kMean.Print(INFORMATION);
#endif
	_mMean = kMean.GetMeanDataCluster();
	_vW = kMean.GetWeightCluster();
	// Cai ma tran hiep phuong sai nay su dung co nhieu loai
	// 1 gia gi mot vetor, hay la mot ma  tran
#ifdef PRINT_DEBUG_GMM_INIT
	PRINT(INFORMATION, "Kmean Completed\n");
	kMean.Print(INFORMATION);
	PRINT(INFORMATION, "Compute Var Matrix\n");
#endif
	unity.Resize(_dim, _dim); /* defining unity matrix */
	for (COUNT_TYPE k = 0; k < _dim; k++) unity(k, k) = 1.0 * DATA_TYPE(_EPSILON);

	
	switch (_coVarType){
	case 0: break;
	case 1: {// Su dung kieu duong cheo'
		_mVar.clear();
		for (COUNT_TYPE g = 0; g < _numComponents; g++){
			
			_mVar.push_back(Matrix());
			Matrix &mVar = _mVar[g];
			Matrix data = kMean.GetDataCluster(g);
			_mDataCluster.push_back(data);
			Vector vMean = _mMean.GetColumn(g);
			COUNT_TYPE dataInGroup = data.ColumnSize();
#ifdef PRINT_DEBUG_GMM_INIT
			PRINT(DATA, "Compute Var Matrix for Com =  %d Total data in Group = %d \n", g, dataInGroup);
			PRINT(DATA, "Mean Vector Comp = %d\n", g);
			vMean.Print();
#endif
			mVar.Resize(_dim, _dim, false);
			// Tinh Var Su dung kieu ma tran duong cheo'
			for (COUNT_TYPE i = 0; i < dataInGroup; i++){
				Vector x = data.GetColumn(i);
#ifdef PRINT_DEBUG_GMM_INIT
				PRINT(DATA, "Data Vector I =%d ", i);
				x.Print();
#endif
				for (COUNT_TYPE d = 0; d < _dim; d++){
					// Tinh Phuong sai su dung binh phuong(x - mean)
					DATA_TYPE val = x(d) - vMean(d);// data trong group voi index cua data la ID
					mVar(d, d) += val * val;
				}
#ifdef PRINT_DEBUG_GMM_INIT
				PRINT(DATA, "Var of Com = %d\n", g);
				mVar.Print();
#endif
			}
#ifdef PRINT_DEBUG_GMM_INIT
			PRINT(DATA, "Var of Com = %d\n", g);
			mVar.Print();
#endif
			mVar *= DATA_TYPE(1) / DATA_TYPE(dataInGroup);
#ifdef PRINT_DEBUG_GMM_INIT
			mVar.Print();
			unity.Print();
#endif
			mVar += unity;
#ifdef PRINT_DEBUG_GMM_INIT
			PRINT(DATA, "Var of Com = %d\n", g);
			mVar.Print();
#endif
		}
	}break;
	case 2:
		_mVar.clear();
		for (COUNT_TYPE g = 0; g < _numComponents; g++){
			_mVar.push_back(Matrix());
			Matrix &mVar = _mVar[g];
			Matrix data = kMean.GetDataCluster(g);
			_mDataCluster.push_back(data);
			Vector vMean = _mMean.GetColumn(g);
			COUNT_TYPE dataInGroup = data.ColumnSize();
#ifdef PRINT_DEBUG_GMM_INIT
			PRINT(DATA, "Compute Var Matrix for Com =  %d Total data in Group = %d \n", g, dataInGroup);
			PRINT(DATA, "Mean Vector Comp = %d\n", g);
			vMean.Print();
#endif
			mVar.Resize(_dim, _dim, false);
			// Tinh Var Su dung kieu ma tran duong cheo'
			for (COUNT_TYPE i = 0; i < dataInGroup; i++){
				Vector x = data.GetColumn(i);
				//PRINT("Data Vector I =%d ", i);
				//x.Print();
				Vector val = x - vMean;
				Matrix Dif(_dim, _dim);
				Dif.SetColumn(val, 0);
				Matrix TDiff = Dif.Transpose();
				mVar += Dif*TDiff;
			}
			mVar *= DATA_TYPE(1.0) / DATA_TYPE(dataInGroup);
			mVar += unity;
#ifdef PRINT_DEBUG_GMM_INIT
			PRINT(DATA, "Var of Com = %d\n", g);
			mVar.Print();
#endif
		}
		break;
	}
#ifdef PRINT_DEBUG_GMM_INIT
	Print(INFORMATION);
	PRINT(INFORMATION, "*****************Init GMM Completed*****************\n");
#endif
	
}
// Can cho training them data thoi. Minh ko can. Nhung co thi tot:D khe khe :d
bool GMM::DoEM(Matrix &data){

	COUNT_TYPE countCov = 0;
	_mData.AppendColumn(data);
	_dim = data.RowSize();
	Init();
#ifdef PRINT_DEBUG_GMM_STEP_EM
	PRINT(STEP, "*******************START DO EM*****************************\n");
#endif
	COUNT_TYPE colData = data.ColumnSize();
	// Khoi tao mot matran de luu P(X|Component)
	// Ma tran nay` chinh co do rong la Size Cua Du lieu data x Component
	// Theo cong thuc xac suat cua P(X| Mo Hinh) thi can phai nhan voi he so nua 
	// Va vec tor luu log
	Vector pLogSum;

	COUNT_TYPE iter = INIT_INDEX_TYPE;
	while (true){

		Matrix pXComponent(colData, _numComponents);
		Matrix pComponentX(colData, _numComponents);

		// Can co mot vector luu cac xac xuat luu gia tri nay
		Vector pSum(colData);
#ifdef PRINT_DEBUG_GMM_STEP_EM
		PRINT(DETAIL, "*******************All value before RUN EM *************************\n");
		this->Print(DATA);
		PRINT(DETAIL, "*******************EM ITER %d*****************************\n", iter);
#endif
		// E step:  Tinh hai xac suat P(x| component), P(Component | x)
			// Truoc mat di tinh xac suat P(x| Component)
		DATA_TYPE sum_log = INIT_DATA_TYPE;
		for (COUNT_TYPE col = 0; col < colData; col++){ // Lay Tung Vector Du lieu
			Vector xData = _mData.GetColumn(col); // Lay vector du lieu X
			pSum[col] = INIT_DATA_TYPE;
			for (COUNT_TYPE component = 0; component < _numComponents; component++){ // Duyet tung component chinh la cluster group cua Kmean do.
					DATA_TYPE p = pdf(xData, component);
					if (p > 0){
						pXComponent(col, component) = p;
						pSum[col] += _vW[component] * p; // Tinh xac xuat cua P(X tren Mo hinh tham so cu)
					}
					else { // KO lay duoc matran invert
						PRINT(STEP, "ERROR: Probability of Data Col %d / Component %d is Zero Invalid GMM Aborted\n", col, component);
						_isSuccess = false;
						
						return false;
					}
			}

			sum_log += log(pSum[col]);
		}
		pLogSum.PushBack(sum_log / colData);// Log Function day
#ifdef PRINT_DEBUG_GMM_STEP_EM	
		PRINT(DETAIL, "pSum\n");
		pSum.Print();
		PRINT(DETAIL, "Probability \n");
		pXComponent.Print();
#endif
		PRINT(STEP, "Iter =%d pLogSum[iter] = %f  delta = %.10f\n", iter, pLogSum[iter], abs(pLogSum[iter] - pLogSum[iter - 1]));
		if (iter > 1 && (abs(pLogSum[iter] - pLogSum[iter - 1]) < _thresholdLog || iter > _maxIterator)){
#ifdef PRINT_DEBUG_GMM_COST_FUNCTION
			PRINT(DATA, "Log cost for each iteration\n");
			pLogSum.Print();
#endif
			countCov++;
			PRINT(STEP, "EM Completed - Aborted at iter %d:\n", iter);

			Print(INFORMATION);
			if (countCov == 3){
				_isSuccess = true;
				return true;
			}
		}
		// Toi day du kieu cua P(x | Component) duoc luu vao pXCom matrix
		// Gio moi tinh cai can tinh ne` la P(Compoenent | X)
		// Truoc mat di tinh xac suat P(x| Component)
		for (COUNT_TYPE col = 0; col < colData; col++){ // Lay Tung Vector Du lieu
			for (COUNT_TYPE com = 0; com < _numComponents; com++){ // Duyet tung component chinh la cluster group cua Kmean do.
				// Tinh Xac suat cua P(Component|X) =  P(X|Component) * P(Component) / P(X) 
				// ma ta co P(X) =  Sigma(P(A|Component) * P(Component)) = chinh bang tong pSum o tren
				pComponentX(col, com) = (pXComponent(col, com) * _vW[com]) / pSum[col];
				//printf("pComponentX(col, com) = %f, pXComponent(col, com) =  %f. w = %f sum = %f\n", pComponentX(col, com), pXComponent(col, com), _vW[com], pSum[col]);
			}
		}
#ifdef PRINT_DEBUG_GMM_DO_EM	
		PRINT(DATA, "Matran pXComponent\n");
		pComponentX.Print();
#endif
		// Toi Day Tinh Xong cai P(Component | X)
		// M Step:  Tinh lai Phuong sai va ky vong dua tren P(x| component), P(Component | x)
		Vector vW;
		
		pComponentX.SumRow(vW); // Tinh Tong sac suat cua P(Com|X) Theo hang cot

		
		_vW = vW / DATA_TYPE(colData); // Update weight matrix
#ifdef PRINT_DEBUG_GMM_DO_EM_UPDATE_MEAN	
		PRINT(DATA, "Matrix Weight Updated\n");
		_vW.Print();
#endif
		//_mMean.(_dim, _numComponents, false);

		Matrix mean(colData, _numComponents);
		for (COUNT_TYPE com = 0; com < _numComponents; com++){
			Vector vMean(_dim);
			// Update Mean
			for (COUNT_TYPE col = 0; col < colData; col++){
				vMean += data.GetColumn(col) * pComponentX(col, com);
#ifdef PRINT_DEBUG_GMM_DO_EM_UPDATE_MEAN	
				PRINT(DATA, "Tinh Mean %d\n", col);
				vMean.Print();
#endif
			}
			vMean = vMean / vW[com];
			_mMean.SetColumn(/*vMean / vW[com]*/ vMean, com); // Update mean vector
#ifdef PRINT_DEBUG_GMM_DO_EM_UPDATE_MEAN	
			// Update CovVarian
			PRINT(DATA, "vW matrix\n");
			vW.Print();
			PRINT(DATA, "Matrix Mean Updated\n");
			vMean.Print();
			PRINT(DATA, "Mean Updated:\n");
			_mMean.Print();
#endif
			Matrix tmsigma(_dim, _dim);
			for (COUNT_TYPE col = 0; col < colData; col++){
				Matrix Dif(_dim, _dim);
				Dif.SetColumn(/*_mMean.GetColumn(com)*/ vMean - data.GetColumn(col), 0);
				
				//PRINT("Diff Matrix with data %d\n", col);
				//Dif.Print();
				Matrix TDiff = Dif.Transpose();
				//PRINT("TDiff Matrix with data %d\n", col);
				//TDiff.Print();
				Matrix tmp = Dif*TDiff;
				///PRINT("Tmp Matrix with data %d\n", col);
				//tmp.Print();
				tmsigma += tmp*pComponentX(col, com);
			}
#ifdef PRINT_DEBUG_GMM_DO_EM_UPDATE_VAR
			PRINT(DATA, "Matrix tmSigma\n");
			tmsigma.Print();
#endif			
			switch (this->_coVarType){
			case COV_TYPE_01:
				break;;
			case COV_TYPE_02:
				for (COUNT_TYPE d = 0; d < _dim; d++){
					_mVar[com](d, d) = tmsigma(d, d) / vW[com];
				}
				_mVar[com] += unity;
				break;
			case COV_TYPE_03:
				_mVar[com] = tmsigma / vW[com];
				_mVar[com] += unity;
				break;
			}
#ifdef PRINT_DEBUG_GMM_DO_EM_UPDATE_VAR
			PRINT(DATA, "Matrix Var Com = %d\n", com);
			_mVar[com].Print();
#endif
		}
#ifdef PRINT_DEBUG_GMM_STEP_EM
		Print(DATA);
		PRINT(STEP, "*******************END EM ITER %d*****************************\n", iter);
#endif
		iter++;
	}
#ifdef PRINT_DEBUG_GMM_STEP_EM
	Print(INFORMATION);
	PRINT(STEP, "*******************END DO EM*****************************\n");
#endif
	_isSuccess = false;
	return false;
}
// Cong thuc tinh sac xuat
// Tinh sac xuat P(x | Cluster Component)
DATA_TYPE GMM::pdf(Vector &in, COUNT_TYPE cluster){

	DATA_TYPE wCom = _vW[cluster];// Trong so Wj
	Vector meanCom = _mMean.GetColumn(cluster);// Mean j
	Matrix &varCom = _mVar[cluster]; // Var j
	DATA_TYPE det = INIT_DATA_TYPE;
	Vector x = in - meanCom;
#ifdef PRINT_DEBUG_GMM_PDF
	PRINT(DATA, "%s Tinh PDF cua vector: Com =  %d\n", __FUNCTIONW__, cluster);
	in.Print();
	PRINT(DATA, "%s Var\n", __FUNCTIONW__);
	varCom.Print();
	PRINT(DATA, "%s ********** Value in Comp = %d\n", __FUNCTIONW__, cluster);
	PRINT(DATA, "%s Matrix Sau Khi Tru Mean\n", __FUNCTIONW__);
	x.Print();
	PRINT(DATA, "%s Matrix In\n", __FUNCTIONW__);
	in.Print();
	PRINT(DATA, "%s Matrix Mean\n", __FUNCTIONW__);
	meanCom.Print();
#endif
	switch (_coVarType){
	case 0: break;
	case 1: {// Su dung kieu duong cheo'
		//
		varCom.Inverse(&det); // Tinh Det ma tran Com
#ifdef PRINT_DEBUG_GMM_PDF
		// Nghich dao cua ma tran Var Com
		PRINT(DATA, "%s ********** Value in Comp = %d\n", __FUNCTIONW__, cluster);
		PRINT(DATA, "%s Matrix Sau Khi Tru Mean\n", __FUNCTIONW__);
		x.Print();
		in.Print();
		PRINT(DATA, "%s Det = %f \n", __FUNCTIONW__, det);
		PRINT(DATA, "%s Matrix Var:\n", __FUNCTIONW__);
		varCom.Print();
#endif
		
		Matrix InvVar = DATA_TYPE(1.0) / varCom;
#ifdef PRINT_DEBUG_GMM_PDF
		PRINT(DATA, "%s Matrix InvVar:\n", __FUNCTIONW__);
		InvVar.Print();
#endif
		Vector v = InvVar * x;
		double val = x *v; // Tinh phan mu~ e
		//PRINT("Val = %f \n", val);

		double e = exp(-double(0.5)*val);// Tinh So mu
		//PRINT(" Phan Ma tran %f,  phan mu %f\n ", val, p);
		double factor = sqrt(pow(double(2.0)*double(PI), double(_dim)) *abs(det));//  Phan duoi can
		//PRINT(" Phan He So =  %f output %f\n ", factor, p / factor);
		double p = e / factor;
		if (p < DATA_TYPE(MIN_PDF)) return DATA_TYPE(MIN_PDF);
		return DATA_TYPE(p);
	} break;
	case 2: {// Su dung kieu duong cheo'
		//
		Matrix Invert = varCom.Inverse(&det); // Tinh Det ma tran Com
		// Nghich dao cua ma tran Var Com
#ifdef PRINT_DEBUG_GMM_PDF		
		PRINT(DATA, "%s Det = %f \n", __FUNCTIONW__, det);
		PRINT(DATA, "%s Matrix Var:\n", __FUNCTIONW__);
		varCom.Print(); 
#endif
		if (varCom.IsInverseOk()){
#ifdef PRINT_DEBUG_GMM_PDF	
			PRINT(DATA, "%s Matrix InvVar:\n", __FUNCTIONW__);
			Invert.Print();
#endif
			Vector v = Invert * x;
			double val = x *v; // Tinh phan mu~ e


			double e = exp(-double(0.5)*val);// Tinh So mu
			//PRINT(" Phan Ma tran %f,  phan mu %f\n ", val, p);
			double factor = sqrt(pow(DATA_TYPE(2.0)*DATA_TYPE(PI), DATA_TYPE(_dim)) *abs(det));//  Phan duoi can
			//PRINT(" Phan He So =  %f output %f\n ", factor, p / factor);
			double p = e / factor;
			if (p < DATA_TYPE(MIN_PDF)) return DATA_TYPE(MIN_PDF);
			return DATA_TYPE(p);
		}
		else {
			PRINT(DATA, "%s Matran Ko the kha nghich\n", __FUNCTIONW__);
			return -1;
		}

		
	} break;
	}
	PRINT(STEP, "%s Error in function %s\n", __FUNCTIONW__, "PDF");
	return DATA_TYPE(1);
}

// Tinh Sac Xuat P(x)
DATA_TYPE GMM::Probability(Vector &in){
#ifdef PRINT_DEBUG_GMM_PROBABILITY	
	PRINT(DATA, "*******Probability*******\n");
	PRINT(DATA, "X Vector\n");
	in.Print();
#endif
	DATA_TYPE pro = DATA_TYPE(0);
	for (COUNT_TYPE i = 0; i < _numComponents; i++){
		DATA_TYPE prob = pdf(in, i);
		pro += _vW[i] * prob;
		//printf(" Output at Comp = %d: Weight %f, prob = %f\n", i, _vW[i], prob);
	}
	return pro;
}

void GMM::Probability(Vector &in, Vector &prob){
	prob.Resize(_numComponents);
	for (COUNT_TYPE i = 0; i < _numComponents; i++){
		DATA_TYPE p = pdf(in, i);
		prob[i] = _vW[i] * p;
	}
}

DATA_TYPE GMM::LogProbability(Matrix &in){
	DATA_TYPE p = DATA_TYPE(0);
	for (COUNT_TYPE i = 0; i < in.ColumnSize(); i++){
		p += log(Probability(in.GetColumn(i)));
	}
	return p;
}

void GMM::Print(LOG_LEVEL level){
	PRINT(level, "%s ********************************************************\n", __FUNCTIONW__);
	for (COUNT_TYPE i = 0; i < _numComponents; i++){
		PRINT(level, "%s Cluster Num: %d \n", __FUNCTIONW__, i);
		PRINT(level, "%s Vector Mean:\n", __FUNCTIONW__);
		Vector mean = _mMean.GetColumn(i);
		mean.Print(level);
		PRINT(level, "%s Vector Weight: %f\n", __FUNCTIONW__, _vW[i]);
		
		PRINT(level, "%s Matrix Covar:\n", __FUNCTIONW__);
		_mVar[i].Print(level);
	}

	PRINT(level, "PROB DATA WITH COMPONENT\n");

	for (COUNT_TYPE data = 0; data < _mData.ColumnSize(); data++){
		PRINT(level, "Prob %5d    ", data);
		for (COUNT_TYPE i = 0; i < _numComponents; i++){
			PRINT(level, "Com %2d = %20.10f   ",i, pdf(_mData.GetColumn(data), i));
		}
		PRINT(level, "\n");
	}

	PRINT(level, "********************************************************\n");
}
/*
Node: GMM - Dim, Com, ColType
         |- Weight, Row
		     |- Data
	     |- Mean
		 |- Var
*/
bool GMM::Load(TiXmlElement *gmm){
	bool result = false;
	if (gmm != NULL){
		SIGNED_TYPE comp = 0, dim = 0, colVar = 0, maxItorator = 0, valid = 0;
		if (gmm->Attribute("Component", &comp) != NULL && 
			gmm->Attribute("ColVarType", &colVar) != NULL &&
			gmm->Attribute("Dim", &dim) != NULL &&
			gmm->Attribute("Valid", &valid) != NULL &&
			gmm->Attribute("MaxIterator", &maxItorator) != NULL){
			_numComponents = comp;
			_dim = dim;
			_coVarType = colVar;
			_maxIterator = maxItorator;
			_isSuccess = (valid != 0) ? false : true;
			result = true;
			// Weight
			TiXmlElement *weight_e = gmm->FirstChildElement("Weight");
			result &= _vW.Load(weight_e);
			TiXmlElement *mean_e = gmm->FirstChildElement("Mean");
			result &= _mMean.Load(mean_e);

			TiXmlElement *var_e = gmm->FirstChildElement("Var");
			for (COUNT_TYPE c = 0; c < _numComponents; c++){
				char str_var[100] = { 0 };
				sprintf(str_var, "Var%03d", c);
				TiXmlElement *varCom_e = var_e->FirstChildElement(str_var);
				if (varCom_e != NULL){
					Matrix var;
					result &= var.Load(varCom_e);
					_mVar.push_back(var);
				}
				else {
					result = false;
				}
			}
		}
	}
	return result;
}

bool GMM::Load(char *path){
	TiXmlDocument doc;
	PRINT(STEP, "GMM: Load file %s", path);
	doc.LoadFile(path);
	TiXmlElement *e = doc.FirstChildElement("GMM");
	return Load(e);
}

bool GMM::Save(TiXmlElement *gmm){
	gmm->SetAttribute("Component", _numComponents);
	gmm->SetAttribute("ColVarType", _coVarType);
	gmm->SetAttribute("Dim", _dim);
	gmm->SetAttribute("Valid", _isSuccess);
	gmm->SetAttribute("MaxIterator", _maxIterator);
	
	// Weight
	TiXmlElement *weight_e = new TiXmlElement("Weight");
	if (weight_e != NULL){
		_vW.Save(weight_e);
	}
	gmm->LinkEndChild(weight_e);

	TiXmlElement *mean_e = new TiXmlElement("Mean");
	_mMean.Save(mean_e);

	gmm->LinkEndChild(mean_e);

	TiXmlElement *var_e = new TiXmlElement("Var");
	for (COUNT_TYPE c = 0; c < _numComponents; c++){
		char str_var[100] = { 0 };
		sprintf(str_var, "Var%03d", c);
		TiXmlElement *varCom_e = new TiXmlElement(str_var);
		if (varCom_e != NULL){
			_mVar[c].Save(varCom_e);
			var_e->LinkEndChild(varCom_e);
		}
	}

	gmm->LinkEndChild(var_e);

	return true;
}

bool GMM::Save(char *path){
	TiXmlDocument doc;
	//Data
	PRINT(STEP, "GMM: Save file %s", path);
	TiXmlElement *gmm = new TiXmlElement("GMM");
	Save(gmm);
	doc.LinkEndChild(gmm);
	return doc.SaveFile(path);
}


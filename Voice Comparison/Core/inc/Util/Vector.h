

#ifndef VECTOR_H
#define VECTOR_H

#include <Common.h>
#include <Util\MathUtil.h>
#include <Util\VectorIdx.h>
#include <tinyxml.h>
#include <math.h>
#include <iostream>

class Vector
{
  friend class Matrix;
  friend class VectorIdx;
  friend Vector operator /(DATA_TYPE val, const Vector& in);
protected:
	static  DATA_TYPE undef;
   
          unsigned int   row;
	        DATA_TYPE         *_;
public:

	inline Vector(DATA_TYPE *data, COUNT_TYPE size){
		row = 0;
		_ = NULL;
		Resize(size, false);
		Zero();
		memcpy(_, data, sizeof(DATA_TYPE) * size);
	}
	inline Vector() {
    row = 0;
    _   = NULL;
  }
  
  inline virtual ~Vector(){
    Release(); 
  }

  inline Vector(const Vector &vector)
  {
    row = 0;
    _   = NULL;
    Resize(vector.row,false);
    for (unsigned int i = 0; i < row; i++)
      _[i] = vector._[i];
  }

  inline Vector(unsigned int size, bool clear = true)
  {
    row = 0;
    _   = NULL;
    Resize(size,false);
    if(clear)
      Zero();
  }
  
	inline Vector(const float _[], unsigned int size)
	{
    row       = 0;
    this->_   = NULL;
    Resize(size,false);
		for (unsigned int i = 0; i < row; i++)
			this->_[i] = _[i];
	}
   
  
  inline Vector& Zero()
  {
    for (unsigned int i = 0; i < row; i++)
      _[i] = 0.0f;
    return *this;    
  }

  inline Vector& One()
  {
    for (unsigned int i = 0; i < row; i++)
		_[i] = DATA_TYPE(1.0);
    return *this;    
  }

  inline Vector& Random(){
    for (unsigned int j = 0; j < row; j++)
		_[j] = ((DATA_TYPE)rand()) / ((DATA_TYPE)(RAND_MAX + 1.0));
    return *this;    
  }

 
    
  inline unsigned int Size() const{
    return row;
  }
  
  inline DATA_TYPE *GetArray() const{
    return _;
  }
  
  inline DATA_TYPE& operator[] (const unsigned int row)
  {
    if(row<this->row)
      return _[row];
    return undef; 
  }
  
  inline DATA_TYPE operator[] (const unsigned int row) const
  {
	  if (row<this->row)
		  return _[row];
	  return undef;
  }

  inline DATA_TYPE& operator() (const unsigned int row)
  {
    if(row<this->row)
      return _[row];
    return undef; 
  }

  inline DATA_TYPE operator() (const unsigned int row) const
  {
	  if (row<this->row)
		  return _[row];
	  return undef;
  }
  
	inline Vector operator - () const
	{
		Vector result(row,false);
		for (unsigned int i = 0; i < row; i++)
			result._[i] = -_[i];
    return result;
	}
  
  inline Vector& Set(const Vector &vector){
    return (*this)=vector;  
  }
  
  inline Vector& operator = (const Vector &vector)
  {
    Resize(vector.row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      _[i] = vector._[i];
    for (unsigned int i = k; i < row; i++)
      _[i] = 0;
    return *this;    
  }
  
	inline Vector& operator += (const Vector &vector)
	{
    const unsigned int k = (row<=vector.row?row:vector.row);
		for (unsigned int i = 0; i < k; i++)
			_[i] += vector._[i];
    return *this;
	}

	inline Vector& operator -= (const Vector &vector)
	{
    const unsigned int k = (row<=vector.row?row:vector.row);
		for (unsigned int i = 0; i < k; i++)
			_[i] -= vector._[i];
    return *this;
	}

  inline Vector& operator ^= (const Vector &vector)
  {
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      _[i] *= vector._[i];
    return *this;
  }

  inline Vector& operator /= (const Vector &vector)
  {
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      _[i] /= vector._[i];
    return *this;
  }

  inline Vector& operator += (DATA_TYPE scalar)
  {
    for (unsigned int i = 0; i < row; i++)
      _[i] += scalar;
    return *this;
  }

  inline Vector& operator -= (DATA_TYPE scalar)
  {
    for (unsigned int i = 0; i < row; i++)
      _[i] -= scalar;
    return *this;
  }

  inline Vector& operator *= (DATA_TYPE scalar)
	{
		for (unsigned int i = 0; i < row; i++)
			_[i] *= scalar;
    return *this;
	}

  inline Vector& operator /= (DATA_TYPE scalar)
	{
		scalar = DATA_TYPE(1.0) / scalar;
		for (unsigned int i = 0; i < row; i++)
			_[i] *= scalar;
    return *this;
	}

  inline Vector operator + (const Vector &vector) const
  {
    Vector result(row,false);
    return Add(vector,result);    
  }
  
  inline Vector& Add(const Vector &vector, Vector& result) const
  {   
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] + vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result;
  }

  inline Vector operator - (const Vector &vector) const
  {
    Vector result(row,false);
    return Sub(vector,result);    
  }
  
  inline Vector& Sub(const Vector &vector, Vector& result) const
  {
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] - vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result; 
  }

  inline Vector operator ^ (const Vector &vector) const
  {
    Vector result(row,false);
    return PMult(vector,result);    
  }
  
  inline Vector& PMult(const Vector &vector, Vector& result) const
  {   
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] * vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result;
  }

  inline Vector operator / (const Vector &vector) const
  {
    Vector result(row,false);
    return PDiv(vector,result);    
  }
  
  inline Vector& PDiv(const Vector &vector, Vector& result) const
  {
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] / vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result; 
  }

  // Nhan = Matrix(N x 1) dot Matrix (1 x N);
  inline DATA_TYPE operator * (const Vector &vector) const
  { 
    return this->Dot(vector);  
  }
  
  inline Vector operator + (DATA_TYPE scalar) const
  {
    Vector result(row,false);
    return Add(scalar,result);    
  }
  
  inline Vector& Add(DATA_TYPE scalar, Vector& result) const
  {
    result.Resize(row,false);
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] + scalar;
    return result;
  }

  inline Vector operator - (DATA_TYPE scalar) const
  {
    Vector result(row,false);
    return Sub(scalar,result);    
  }
  
  inline Vector& Sub(DATA_TYPE scalar, Vector& result) const
  {
    result.Resize(row,false);
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] - scalar;
    return result;
  }

  inline Vector operator * (DATA_TYPE scalar) const
  {
    Vector result(row,false);
    return Mult(scalar,result);    
  }
  
  inline Vector& Mult(DATA_TYPE scalar, Vector& result) const
  {
    result.Resize(row,false);
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] * scalar;
    return result;
  }

  inline Vector operator / (DATA_TYPE scalar) const
  {
    Vector result(row,false);
    return Div(scalar,result);    
  }
  
  inline Vector& Div(DATA_TYPE scalar, Vector& result) const
  {
    result.Resize(row,false);
	scalar = DATA_TYPE(1.0) / scalar;
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] * scalar;
    return result;
  }

  inline bool operator == (const Vector& vector) const
  {
    if(row!=vector.row) return false;
    for (unsigned int i = 0; i < row; i++)
      if(_[i] != vector._[i]) return false;
    return true;
  }

  inline bool operator != (const Vector& vector) const
  {
    return !(*this ==  vector);
  }

  inline DATA_TYPE& PushBack(DATA_TYPE data){
	  Resize(row + 1);
	  this->_[row - 1] = data;
	  return _[row - 1];
  }

  inline DATA_TYPE& PopBack(){
	  if (row > 0){
		  return _[row - 1];
	  }
	  return *_;
  }


  inline DATA_TYPE Sum() const
  {
	  DATA_TYPE result = INIT_DATA_TYPE;
    for (unsigned int i = 0; i < row; i++)
      result += _[i];
    return result;
  }

  inline DATA_TYPE Norm() const
  {
    return sqrt(Norm2());
  }

  inline DATA_TYPE Norm2() const
  {
	  DATA_TYPE result = INIT_DATA_TYPE;
    for (unsigned int i = 0; i < row; i++)
      result += _[i]*_[i];
    return result;
  }

	inline void Normalize()
	{
		DATA_TYPE scalar = (DATA_TYPE)1.0 / Norm();
    (*this)*=scalar;
	}
  

	inline void Normalize(DATA_TYPE peak)
	{
		DATA_TYPE scalar = (DATA_TYPE)peak / Max();
		(*this) *= scalar;
	}
	inline DATA_TYPE Distance(const Vector &vector) const
  {
    return (*this-vector).Norm();
  }
  
	inline DATA_TYPE Distance2(const Vector &vector) const
  {
    return (*this-vector).Norm2();  
  }

	inline DATA_TYPE Dot(const Vector &vector) const
  {
    const unsigned int k = (row<=vector.row?row:vector.row);
	DATA_TYPE result = 0.0f;
    for (unsigned int i = 0; i < k; i++)
      result += _[i]*vector._[i];
    return result;     
  }

  inline Vector& SetSubVector(unsigned int startPos, const Vector &vector)
  {
    if(startPos<row){
      const unsigned int k = (row-startPos<=vector.row?row-startPos:vector.row);
      for (unsigned int i = 0; i < k; i++){
        _[startPos+i] = vector._[i];  
      }
    }
    return *this;   
  }

  inline Vector GetSubVector(unsigned int startPos, unsigned int len)
  {
    Vector result(len,false);
    return GetSubVector(startPos,len,result);
  }
  
  /*inline Matrix Transpose(){
	  Matrix result(1, row);	  
	  for (COUNT_TYPE i = 0; i < row; i++){
		  result._[i] = _[i];
	  }
	  return result;
  }*/

  inline Vector& GetSubVector(unsigned int startPos, unsigned int len, Vector &result)
  {
    result.Resize(len,false);    
    if(startPos<row){
      const unsigned int k = (row-startPos<=len?row-startPos:len);
      for (unsigned int i = 0; i < k; i++){
        result[i] = _[startPos+i]; 
      }
      for (unsigned int i = k; i < len; i++){
        result[i] = 0.0f;
      }
      
    }else{
      result.Zero();  
    }
    return result;   
  }

  inline DATA_TYPE Max(){
    if(row==0)
      return 0.0f;
      
	DATA_TYPE res = _[0];
    for(unsigned int i=1;i<row;i++){
      if(_[i]>res) res = _[i];  
    }
    return res;
  }

  inline int MaxId(){
    if(row==0)
      return -1;
      
	DATA_TYPE mx = _[0];
    int   res = 0;
    for(unsigned int i=1;i<row;i++){
      if(_[i]>mx){ mx = _[i]; res = i;}  
    }
    return res;
  }

  inline DATA_TYPE Min(){
	  if (row == 0)
		  return 0.0f;

	  DATA_TYPE res = _[0];
	  for (unsigned int i = 1; i<row; i++){
		  if (_[i] < res) res = _[i];
	  }
	  return res;
  }

  inline int MinId(){
	  if (row == 0)
		  return -1;

	  DATA_TYPE mx = _[0];
	  int   res = 0;
	  for (unsigned int i = 1; i<row; i++){
		  if (_[i] < mx){ mx = _[i]; res = i; }
	  }
	  return res;
  }

  inline Vector Abs(){
    Vector result(row);
    return Abs(result);
  }

  inline Vector& Abs(Vector &result) const{
    result.Resize(row,false);
    for(unsigned int i=0;i<row;i++){
      result._[i] = fabs(_[i]);
    }
    return result;
  }

  inline Vector& GetSubVector(const Vector &ids, Vector &result) const
  {
    const unsigned int k=ids.Size();
    result.Resize(k);
    for(unsigned int i=0;i<k;i++){
      const unsigned int g = (unsigned int)(abs(ROUND(ids._[i])));
      if(g<row){
        result._[i] = _[g];
      }else{
        result._[i] = 0.0f;
      }
    }
    return result;     
  }

  inline COUNT_TYPE GetIdxClosest(DATA_TYPE val){
	  DATA_TYPE minDis = abs(_[0] - val);
	  COUNT_TYPE found = 0;
	  for (unsigned int idx = 1; idx < row;  idx++){
		  DATA_TYPE curDis = abs(_[idx] - val);
		  if (minDis > curDis){
			  minDis = curDis;
			  found = idx;
		  }
	  }
	  return found;
  }



  void Print(LOG_LEVEL level = DATA);
  
  bool Save(TiXmlElement *vector);
  bool Save(char *path);
  bool Load(TiXmlElement *vector);
  bool Load(char *path);

protected:
    inline void Release(){
    if(_!=NULL) delete [] _; 
    row = 0;
    _   = NULL;
  }  
public:  
  inline virtual void Resize(unsigned int size, bool copy = true){
    if(row!=size){
      if(size){
		  DATA_TYPE *arr = new DATA_TYPE[size];
        if(copy){
          unsigned int m = (row<size?row:size);
          for(unsigned int i=0; i<m; i++)
            arr[i] = _[i];
          for(unsigned int i=m; i<size; i++)
            arr[i] = 0.0f;
        }
        if(_!=NULL) delete [] _; 
        _   = arr;
        row = size;        
      }else{
        Release();
      }
    }
  }
};



inline Vector operator /(DATA_TYPE val, const Vector& in)
{
	Vector result(in);
	for (COUNT_TYPE i = 0; i < in.Size(); i++){
		if (in._[i] > EPSILON){
			result._[i] = val / in._[i];
		}
		else {
			result._[i] = Vector::undef;
		}
	}
	return result;
}

inline Vector log(Vector &m){
	COUNT_TYPE size = m.Size();
	Vector result(size);
	for (COUNT_TYPE r = 0; r < size; r++){
		result[r] = log(m(r));
	}
	return result;
}

#endif

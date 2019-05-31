

#ifndef VECTOR_IDX_H
#define VECTOR_IDX_H

#include <Common.h>
#include <Util\MathUtil.h>
#include <tinyxml.h>
#include <math.h>
#include <iostream>

class VectorIdx
{
	friend class MatrixIdx;
	friend class Vector;
protected:


	unsigned int   row;
	INDEX_TYPE         *_;
public:
	static  INDEX_TYPE undef;
	inline VectorIdx(INDEX_TYPE *data, COUNT_TYPE size){
		row = 0;
		_ = NULL;
		Resize(size, false);
		Zero();
		memcpy(_, data, sizeof(INDEX_TYPE)* size);
	}
	inline VectorIdx() {
		row = 0;
		_ = NULL;
	}

	inline virtual ~VectorIdx(){
		Release();
	}

	inline VectorIdx(const VectorIdx &vector)
	{
		row = 0;
		_ = NULL;
		Resize(vector.row, false);
		for (unsigned int i = 0; i < row; i++)
			_[i] = vector._[i];
	}

	inline VectorIdx(COUNT_TYPE size, bool clear = true)
	{
		row = 0;
		_ = NULL;
		Resize(size, false);
		if (clear)
			Zero();
	}

	// inline VectorIdx(INDEX_TYPE _[], COUNT_TYPE size)
	//{
	//   row       = 0;
	//   this->_   = NULL;
	//   Resize(size,false);
	//	for (unsigned int i = 0; i < row; i++)
	//		this->_[i] = _[i];
	//}

	inline INDEX_TYPE& PushBack(INDEX_TYPE data){
		Resize(row + 1);
		this->_[row - 1] = data;
		return _[row - 1];
	}

	inline INDEX_TYPE& PopBack(){
		if (row > 0){
			return _[row - 1];
		}
		return *_;
	}

  inline VectorIdx& MinusOne(){
	  for (unsigned int i = 0; i < row; i++)
		  _[i] = INDEX_TYPE(-1);
	  return *this;
  }
  
  inline VectorIdx& Zero()
  {
    for (unsigned int i = 0; i < row; i++)
      _[i] = INIT_INDEX_TYPE;
    return *this;    
  }

  inline VectorIdx& One()
  {
    for (unsigned int i = 0; i < row; i++)
		_[i] = INDEX_TYPE(1);
    return *this;    
  }

  inline VectorIdx& Random(){
    for (unsigned int j = 0; j < row; j++)
		_[j] = ((INDEX_TYPE)rand()) / ((INDEX_TYPE)(RAND_MAX + 1.0));
    return *this;    
  }

  inline VectorIdx& RandomIndex(INDEX_TYPE from, INDEX_TYPE to){
	  for (unsigned int j = 0; j < row; j++)
		  _[j] = INDEX_TYPE((rand() % (to - from)) + from );
	  return *this;
  }
 
  
  inline VectorIdx& Remove(const COUNT_TYPE row){
	  if (row < this->row){
		  memcpy(&_[row], &_[row + 1], sizeof(INDEX_TYPE)* (this->row - row));
		  this->row -= 1;
	  }
	  return *this;
  }

  inline unsigned int Size() const{
    return row;
  }
  
  inline INDEX_TYPE *GetArray() const{
    return _;
  }
  
  inline INDEX_TYPE& operator[] (const COUNT_TYPE row)
  {
    if(row<this->row)
      return _[row];
    return undef; 
  }
  
  inline INDEX_TYPE operator[] (const COUNT_TYPE row) const
  {
	  if (row<this->row)
		  return _[row];
	  return undef;
  }

  inline INDEX_TYPE& operator() (const COUNT_TYPE row)
  {
    if(row<this->row)
      return _[row];
    return undef; 
  }

  inline INDEX_TYPE operator() (const COUNT_TYPE row) const
  {
	  if (row<this->row)
		  return _[row];
	  return undef;
  }
  
  inline VectorIdx operator - () const
	{
		VectorIdx result(row,false);
		for (unsigned int i = 0; i < row; i++)
			result._[i] = -_[i];
    return result;
	}
  
  inline VectorIdx& Set(const VectorIdx &vector){
    return (*this)=vector;  
  }
  
  inline VectorIdx& operator = (const VectorIdx &vector)
  {
    Resize(vector.row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      _[i] = vector._[i];
    for (unsigned int i = k; i < row; i++)
      _[i] = 0;
    return *this;    
  }
  
  inline VectorIdx& operator += (const VectorIdx &vector)
	{
    const unsigned int k = (row<=vector.row?row:vector.row);
		for (unsigned int i = 0; i < k; i++)
			_[i] += vector._[i];
    return *this;
	}

  inline VectorIdx& operator -= (const VectorIdx &vector)
	{
    const unsigned int k = (row<=vector.row?row:vector.row);
		for (unsigned int i = 0; i < k; i++)
			_[i] -= vector._[i];
    return *this;
	}

  inline VectorIdx& operator ^= (const VectorIdx &vector)
  {
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      _[i] *= vector._[i];
    return *this;
  }

  inline VectorIdx& operator /= (const VectorIdx &vector)
  {
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      _[i] /= vector._[i];
    return *this;
  }

  inline bool operator != (const VectorIdx &vector)
  {
	  if (this->row != vector.row) return true;

	  for (unsigned int i = 0; i < row; i++)
		  if (_[i] != vector._[i]){
			  return true;
		  }
	  return false;
  }

  inline VectorIdx& operator += (INDEX_TYPE scalar)
  {
    for (unsigned int i = 0; i < row; i++)
      _[i] += scalar;
    return *this;
  }

  inline VectorIdx& operator -= (INDEX_TYPE scalar)
  {
    for (unsigned int i = 0; i < row; i++)
      _[i] -= scalar;
    return *this;
  }

  inline VectorIdx& operator *= (INDEX_TYPE scalar)
	{
		for (unsigned int i = 0; i < row; i++)
			_[i] *= scalar;
    return *this;
	}

  inline VectorIdx& operator /= (INDEX_TYPE scalar)
	{
		scalar = INDEX_TYPE(1) / scalar;
		for (unsigned int i = 0; i < row; i++)
			_[i] *= scalar;
    return *this;
	}

  inline VectorIdx operator + (const VectorIdx &vector) const
  {
	  VectorIdx result(row, false);
    return Add(vector,result);    
  }
  
  inline VectorIdx& Add(const VectorIdx &vector, VectorIdx& result) const
  {   
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] + vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result;
  }

  inline VectorIdx operator - (const VectorIdx &vector) const
  {
    VectorIdx result(row,false);
    return Sub(vector,result);    
  }
  
  inline VectorIdx& Sub(const VectorIdx &vector, VectorIdx& result) const
  {
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] - vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result; 
  }

  inline VectorIdx operator ^ (const VectorIdx &vector) const
  {
    VectorIdx result(row,false);
    return PMult(vector,result);    
  }
  
  inline VectorIdx& PMult(const VectorIdx &vector, VectorIdx& result) const
  {   
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] * vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result;
  }

  inline VectorIdx operator / (const VectorIdx &vector) const
  {
    VectorIdx result(row,false);
    return PDiv(vector,result);    
  }
  
  inline VectorIdx& PDiv(const VectorIdx &vector, VectorIdx& result) const
  {
    result.Resize(row,false);
    const unsigned int k = (row<=vector.row?row:vector.row);
    for (unsigned int i = 0; i < k; i++)
      result._[i] = _[i] / vector._[i];
    for (unsigned int i = k; i < row; i++)
      result._[i] = _[i];      
    return result; 
  }

  inline INDEX_TYPE operator * (const VectorIdx &vector) const
  { 
    return this->Dot(vector);  
  }
  
  inline VectorIdx operator + (INDEX_TYPE scalar) const
  {
    VectorIdx result(row,false);
    return Add(scalar,result);    
  }
  
  inline VectorIdx& Add(INDEX_TYPE scalar, VectorIdx& result) const
  {
    result.Resize(row,false);
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] + scalar;
    return result;
  }

  inline VectorIdx operator - (INDEX_TYPE scalar) const
  {
    VectorIdx result(row,false);
    return Sub(scalar,result);    
  }
  
  inline VectorIdx& Sub(INDEX_TYPE scalar, VectorIdx& result) const
  {
    result.Resize(row,false);
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] - scalar;
    return result;
  }

  inline VectorIdx operator * (INDEX_TYPE scalar) const
  {
    VectorIdx result(row,false);
    return Mult(scalar,result);    
  }
  
  inline VectorIdx& Mult(INDEX_TYPE scalar, VectorIdx& result) const
  {
    result.Resize(row,false);
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] * scalar;
    return result;
  }

  inline VectorIdx operator / (INDEX_TYPE scalar) const
  {
    VectorIdx result(row,false);
    return Div(scalar,result);    
  }
  
  inline VectorIdx& Div(INDEX_TYPE scalar, VectorIdx& result) const
  {
    result.Resize(row,false);
	scalar = INDEX_TYPE(1) / scalar;
    for (unsigned int i = 0; i < row; i++)
      result._[i] = _[i] * scalar;
    return result;
  }

  inline bool operator == (const VectorIdx& vector) const
  {
    if(row!=vector.row) return false;
    for (unsigned int i = 0; i < row; i++)
      if(_[i] != vector._[i]) return false;
    return true;
  }

  inline bool operator != (const VectorIdx& vector) const
  {
    return !(*this ==  vector);
  }


  inline INDEX_TYPE Sum() const
  {
	  INDEX_TYPE result = INIT_INDEX_TYPE;
    for (unsigned int i = 0; i < row; i++)
      result += _[i];
    return result;
  }

  inline INDEX_TYPE Dot(const VectorIdx &vector) const
  {
    const unsigned int k = (row<=vector.row?row:vector.row);
	INDEX_TYPE result = INDEX_TYPE(0);
    for (unsigned int i = 0; i < k; i++)
      result += _[i]*vector._[i];
    return result;     
  }

  inline VectorIdx& SetSubVector(unsigned int startPos, const VectorIdx &vector)
  {
    if(startPos<row){
      const unsigned int k = (row-startPos<=vector.row?row-startPos:vector.row);
      for (unsigned int i = 0; i < k; i++){
        _[startPos+i] = vector._[i];  
      }
    }
    return *this;   
  }

  inline VectorIdx GetSubVector(unsigned int startPos, unsigned int len)
  {
    VectorIdx result(len,false);
    return GetSubVector(startPos,len,result);
  }

  inline VectorIdx& GetSubVector(unsigned int startPos, unsigned int len, VectorIdx &result)
  {
    result.Resize(len,false);    
    if(startPos<row){
      const unsigned int k = (row-startPos<=len?row-startPos:len);
      for (unsigned int i = 0; i < k; i++){
        result[i] = _[startPos+i]; 
      }
      for (unsigned int i = k; i < len; i++){
        result[i] = INIT_INDEX_TYPE;
      }
      
    }else{
      result.Zero();  
    }
    return result;   
  }

  inline INDEX_TYPE Max(){
    if(row==0)
      return INIT_INDEX_TYPE;
      
	INDEX_TYPE res = _[0];
    for(unsigned int i=1;i<row;i++){
      if(_[i]>res) res = _[i];  
    }
    return res;
  }

  inline int MaxId(){
    if(row==0)
      return -1;
      
	INDEX_TYPE mx = _[0];
    int   res = 0;
    for(unsigned int i=1;i<row;i++){
      if(_[i]>mx){ mx = _[i]; res = i;}  
    }
    return res;
  }

  inline INDEX_TYPE Min(){
	  if (row == 0)
		  return INIT_INDEX_TYPE;

	  INDEX_TYPE res = _[0];
	  for (unsigned int i = 1; i<row; i++){
		  if (_[i] < res) res = _[i];
	  }
	  return res;
  }

  inline int MinId(){
	  if (row == 0)
		  return -1;

	  INDEX_TYPE mx = _[0];
	  int   res = 0;
	  for (unsigned int i = 1; i<row; i++){
		  if (_[i] < mx){ mx = _[i]; res = i; }
	  }
	  return res;
  }

  inline VectorIdx& GetSubVector(const VectorIdx &ids, VectorIdx &result) const
  {
    const unsigned int k=ids.Size();
    result.Resize(k);
    for(unsigned int i=0;i<k;i++){
      const unsigned int g = (unsigned int)(abs(ROUND(ids._[i])));
      if(g<row){
        result._[i] = _[g];
      }else{
        result._[i] = INIT_INDEX_TYPE;
      }
    }
    return result;     
  }

  inline COUNT_TYPE GetIdxClosest(INDEX_TYPE val){
	  INDEX_TYPE minDis = abs(_[0] - val);
	  COUNT_TYPE found = 0;
	  for (unsigned int idx = 1; idx < row;  idx++){
		  INDEX_TYPE curDis = abs(_[idx] - val);
		  if (minDis > curDis){
			  minDis = curDis;
			  found = idx;
		  }
	  }
	  return found;
  }

 
  void Sort(Vector &d);

  void Print(LOG_LEVEL level =  DATA);
  
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
	inline virtual void Resize(COUNT_TYPE size, bool copy = true){
    if(row!=size){
      if(size){
		  INDEX_TYPE *arr = new INDEX_TYPE[size];
        if(copy){
			COUNT_TYPE m = (row<size ? row : size);
		  for (COUNT_TYPE i = 0; i<m; i++)
            arr[i] = _[i];
		  for (COUNT_TYPE i = m; i<size; i++)
			  arr[i] = INIT_INDEX_TYPE;
        }
        if(_!=NULL) delete [] _; 
        _   = arr;
        row = size;        
      }else{
        Release();
      }
    }
  }
	inline virtual void Reset(){
		Release();
	}
};

#endif

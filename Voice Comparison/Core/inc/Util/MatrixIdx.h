
#ifndef MATRIX_IDX_H
#define MATRIX_IDX_H

#include <Common.h>
#include <Util\VectorIdx.h>
#include <windows.h>
#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <iostream>
#include <tinyxml.h>
class MatrixIdx
{
  friend class VectorIdx;
 
protected: 
  static int bInverseOk;
  
  unsigned int  row;
  unsigned int  column;
  INDEX_TYPE        *_;

public:

  inline MatrixIdx() {
    row    = 0;
    column = 0;
    _      = NULL;
  }
  
  inline virtual ~MatrixIdx(){
    Release(); 
  }

  inline MatrixIdx(const MatrixIdx &matrix)
  {
    row    = 0;
    column = 0;
    _      = NULL;
    Resize(matrix.row,matrix.column,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        _[j*column+i] = matrix._[j*column+i];
  }

  inline MatrixIdx(unsigned int rowSize, unsigned int colSize, bool clear = true)
  {
    row    = 0;
    column = 0;
    _      = NULL;
    Resize(rowSize,colSize,false);
    if(clear)
      Zero();
  }
  
  inline MatrixIdx(const INDEX_TYPE _[], unsigned int rowSize, unsigned int colSize)
  {
    row       = 0;
    column    = 0;
    this->_   = NULL;
    Resize(rowSize,colSize,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        this->_[j*column+i] = _[j*column+i];
  }

 
  inline MatrixIdx& Zero()
  {
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
		  _[j*column + i] = INIT_INDEX_TYPE;
    return *this;
  }

  inline MatrixIdx& ZeroColumn(unsigned int col)
  {
	  for (unsigned int j = 0; j < row; j++)
		  _[j*column + col] = INIT_INDEX_TYPE;
	  return *this;
  }

  inline MatrixIdx& ZeroRow(unsigned int row)
  {
	  for (unsigned int j = 0; j < this->column; j++)
		  _[row*j] = INIT_INDEX_TYPE;
	  return *this;
  }
    
  inline unsigned int RowSize() const{
    return row;
  }
  inline unsigned int ColumnSize() const{
    return column;
  } 
  inline INDEX_TYPE *Array() const{
    return _;
  }

  inline INDEX_TYPE& operator() (const unsigned int row, const unsigned int col)
  {
    if((row<this->row)&&(col<this->column))
      return _[row*column+col];
    return VectorIdx::undef; 
  }

  inline VectorIdx GetRow(const unsigned int row) const
  {
    VectorIdx result(column,false);    
    return GetRow(row,result);     
  }

  inline VectorIdx& GetRow(const unsigned int row, VectorIdx& result) const
  {
    result.Resize(column,false);
    for (unsigned int i = 0; i < column; i++)
      result._[i] = _[row*column+i];
    return result;     
  }

  inline VectorIdx GetColumn(const unsigned int col) const
  {
    VectorIdx result(row,false);    
    return GetColumn(col,result);     
  }

  inline VectorIdx& GetColumn(const unsigned int col, VectorIdx& result) const
  {
    result.Resize(row,false);
    if(col<column){
      for (unsigned int j = 0; j < row; j++)
        result._[j] = _[j*column+col];
    }else{
      result.Zero();
    }
    return result;     
  }

  inline MatrixIdx GetColumnSpace(const unsigned int col, const unsigned int len) const  
  {
    if(len>0){
      MatrixIdx result(row,len,false);    
      return GetColumnSpace(col,len,result);
    }else
      return MatrixIdx();     
  }

  inline MatrixIdx GetRowSpace(const unsigned int row, const unsigned int len) const
  {
    if(len>0){
      MatrixIdx result(len,column,false);    
      return GetRowSpace(row,len,result);
    }else
      return MatrixIdx();     
  }


  inline MatrixIdx& GetColumnSpace(const unsigned int col, const unsigned int len, MatrixIdx &result) const
  {
    if(len>0){
      const unsigned int end  = col+len-1;    
      const unsigned int size = len; 
      result.Resize(row,size,false);
      
      if(col<column){
        const unsigned int k = (end+1<=column?end+1:column);  
        
        for (unsigned int i = col; i < k; i++)
          for (unsigned int j = 0; j < row; j++)
            result._[j*size+(i-col)] = _[j*column+i];
        for (unsigned int i = k; i < end+1; i++)
          for (unsigned int j = 0; j < row; j++)
			  result._[j*size + (i - col)] = INIT_INDEX_TYPE;
      }else{
        result.Zero();
      }
    }else{
      result.Resize(0,0,false);
    }
    return result;     
  }

  inline MatrixIdx& GetRowSpace(const unsigned int row, const unsigned int len, MatrixIdx &result) const
  {      
    if(len>0){
      const unsigned int end  = row+len-1;
      const unsigned int size = len; 
      result.Resize(size,column,false);
      
      if(row<this->row){
        const unsigned int k = (end+1<=this->row?end+1:this->row);  
        
        for (unsigned int j = 0; j < column; j++)
          for (unsigned int i = row; i < k; i++)
            result._[(i-row)*column+j] = _[i*column+j];
        for (unsigned int j = 0; j < column; j++)
          for (unsigned int i = k; i < end+1; i++)
			  result._[(i - row)*column + j] = INIT_INDEX_TYPE;
      }else{
        result.Zero();
      }
    }else{
      result.Resize(0,0,false);
    }
    return result;     
  }

  inline MatrixIdx& SetRow(const VectorIdx &vector, const unsigned int row)
  {
    if(row<this->row){    
      const unsigned int ki = (column<=vector.row?column:vector.row);
      for (unsigned int i = 0; i < ki; i++)
        _[row*column+i] = vector._[i]; 
    }
    return *this;     
  }

  inline MatrixIdx& SetColumn(const VectorIdx &vector, const unsigned int col)
  {
    if(col<this->column){    
      const unsigned int kj = (row<=vector.row?row:vector.row);
      for (unsigned int j = 0; j < kj; j++)
        _[j*column+col] = vector._[j];
    }
    return *this;
  }

  inline MatrixIdx& SetColumnSpace(const MatrixIdx &matrix, const unsigned int col)
  {
    if(col<this->column){    
      const unsigned int kj = (row<=matrix.row?row:matrix.row);
      const unsigned int ki = (col+matrix.column<=this->column?col+matrix.column:this->column);
      for (unsigned int j = 0; j < kj; j++)
        for (unsigned int i = col; i < ki; i++)
          _[j*column+i] = matrix._[j*matrix.column+(i-col)];
    }
    return *this;
  }

  inline MatrixIdx& SetRowSpace(const MatrixIdx &matrix, const unsigned int row)
  {
    if(row<this->row){
      const unsigned int ki = (column<=matrix.column?column:matrix.column);
      const unsigned int kj = (row+matrix.row<=this->row?row+matrix.row:this->row);
      for (unsigned int j = row; j < kj; j++)
        for (unsigned int i = 0; i < ki; i++)
          _[j*column+i] = matrix._[(j-row)*matrix.column+i]; 
    }
    return *this;     
  }

  inline MatrixIdx GetRowSpace(const VectorIdx &ids) const
  {
    MatrixIdx result(ids.Size(),column);
    return GetRowSpace(ids,result);
  }

  inline MatrixIdx GetColumnSpace(const VectorIdx &ids) const
  {
    MatrixIdx result(row,ids.Size());
    return GetColumnSpace(ids,result);
  }

  inline MatrixIdx GetMatrixSpace(const VectorIdx &rowIds,const VectorIdx &colIds) const
  {
    MatrixIdx result(rowIds.Size(),colIds.Size());
    return GetMatrixSpace(rowIds,colIds,result);
  }

  inline MatrixIdx& GetColumnSpace(const VectorIdx &ids, MatrixIdx &result) const
  {
    const unsigned int k=ids.Size();
    result.Resize(row,k);
    for(unsigned int i=0;i<k;i++){
      const unsigned int g = (unsigned int)(fabs(ROUND(ids._[i])));
      if(g<column){
        for(unsigned int j=0;j<row;j++)
          result._[j*k+i] = _[j*column+g];
      }else{
        for(unsigned int j=0;j<row;j++)
			result._[j*k + i] = INIT_INDEX_TYPE;
      }
    }
    return result;     
  }

  inline MatrixIdx& GetRowSpace(const VectorIdx &ids, MatrixIdx &result) const
  {
    const unsigned int k=ids.Size();
    result.Resize(k,column);
    for(unsigned int i=0;i<k;i++){
      const unsigned int g = (unsigned int)(fabs(ROUND(ids._[i])));
      if(g<row){
        for(unsigned int j=0;j<column;j++)
          result._[i*column+j] = _[g*column+j];
      }else{
        for(unsigned int j=0;j<column;j++)
			result._[i*column + j] = INIT_INDEX_TYPE;
      }
    }
    return result;     
  }

  inline MatrixIdx& GetMatrixSpace(const VectorIdx &rowIds,const VectorIdx &colIds, MatrixIdx &result) const
  {
    const unsigned int k1=rowIds.Size();
    const unsigned int k2=colIds.Size();
    result.Resize(k1,k2);
    for(unsigned int i=0;i<k1;i++){
      const unsigned int g1 = (unsigned int)(fabs(ROUND(rowIds._[i])));
      if(g1<row){
        for(unsigned int j=0;j<k2;j++){      
          const unsigned int g2 = (unsigned int)(fabs(ROUND(colIds._[j])));
          if(g2<column){
            result._[i*k2+j] = _[g1*column+g2];            
          }else{
			  result._[i*k2 + j] = INIT_INDEX_TYPE;
          }
        }
      }else{
        for(unsigned int j=0;j<k2;j++)
			result._[i*k2 + j] = INIT_INDEX_TYPE;
      }
    }
    return result;     
  }

  inline MatrixIdx operator - () const
  {
    MatrixIdx result(row,column,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = -_[j*column+i];
    return result;
  }
  
  inline virtual MatrixIdx& operator = (const MatrixIdx &matrix)
  {
    Resize(matrix.row,matrix.column,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        _[j*column+i] = matrix._[j*column+i];
    return *this;    
  }

  inline MatrixIdx& operator += (const MatrixIdx &matrix)
  {
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++)
      for (unsigned int i = 0; i < ki; i++)
        _[j*column+i] += matrix._[j*column+i];
    return *this;
  }

  inline MatrixIdx& operator -= (const MatrixIdx &matrix)
  {
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++)
      for (unsigned int i = 0; i < ki; i++)
        _[j*column+i] -= matrix._[j*column+i];
    return *this;
  }

  inline MatrixIdx& operator ^= (const MatrixIdx &matrix)
  {
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++)
      for (unsigned int i = 0; i < ki; i++)
        _[j*column+i] *= matrix._[j*column+i];
    return *this;
  }

  inline MatrixIdx& operator /= (const MatrixIdx &matrix)
  {
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++)
      for (unsigned int i = 0; i < ki; i++)
        _[j*column+i] /= matrix._[j*column+i];
    return *this;
  }

  inline MatrixIdx& operator += (INDEX_TYPE scalar)
  {
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        _[j*column+i] += scalar;
    return *this;
  }

  inline MatrixIdx& operator -= (INDEX_TYPE scalar)
  {
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        _[j*column+i] -= scalar;
    return *this;
  }

  inline MatrixIdx& operator *= (INDEX_TYPE scalar)
  {
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        _[j*column+i] *= scalar;
    return *this;
  }

  inline MatrixIdx& operator /= (INDEX_TYPE scalar)
  {
	  scalar = INDEX_TYPE(1.0) / scalar;
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        _[j*column+i] *= scalar;
    return *this;
  }
  
  inline MatrixIdx operator + (const MatrixIdx &matrix) const
  {
    MatrixIdx result(row,column,false);  
    return Add(matrix,result);
  }
  
  inline MatrixIdx& Add(const MatrixIdx &matrix, MatrixIdx &result) const
  {   
    result.Resize(row,column,false);
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++){
      for (unsigned int i = 0; i < ki; i++)
        result._[j*column+i] = _[j*column+i] + matrix._[j*column+i];
      for (unsigned int i = ki; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    }
    for (unsigned int j = kj; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    return result;
  }
  
  inline MatrixIdx operator - (const MatrixIdx &matrix) const
  {
    MatrixIdx result(row,column,false);  
    return Sub(matrix,result);
  }
  
  inline MatrixIdx& Sub(const MatrixIdx &matrix, MatrixIdx &result) const
  {   
    result.Resize(row,column,false);
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++){
      for (unsigned int i = 0; i < ki; i++)
        result._[j*column+i] = _[j*column+i] - matrix._[j*column+i];
      for (unsigned int i = ki; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    }
    for (unsigned int j = kj; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    return result;
  }
  
  inline MatrixIdx operator ^ (const MatrixIdx &matrix) const
  {
    MatrixIdx result(row,column,false);  
    return PMult(matrix,result);
  }
  
  inline MatrixIdx& PMult(const MatrixIdx &matrix, MatrixIdx &result) const
  {   
    result.Resize(row,column,false);
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++){
      for (unsigned int i = 0; i < ki; i++)
        result._[j*column+i] = _[j*column+i] * matrix._[j*column+i];
      for (unsigned int i = ki; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    }
    for (unsigned int j = kj; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    return result;
  }
  
  inline MatrixIdx operator / (const MatrixIdx &matrix) const
  {
    MatrixIdx result(row,column,false);  
    return PDiv(matrix,result);
  }
  
  inline MatrixIdx& PDiv(const MatrixIdx &matrix, MatrixIdx &result) const
  {   
    result.Resize(row,column,false);
    const unsigned int kj = (row<=matrix.row?row:matrix.row);
    const unsigned int ki = (column<=matrix.column?column:matrix.column);
    for (unsigned int j = 0; j < kj; j++){
      for (unsigned int i = 0; i < ki; i++)
        result._[j*column+i] = _[j*column+i] / matrix._[j*column+i];
      for (unsigned int i = ki; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    }
    for (unsigned int j = kj; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i];  
    return result;
  }

  inline MatrixIdx operator + (INDEX_TYPE scalar) const
  {
    MatrixIdx result(row,column,false);  
    return Add(scalar,result);    
  }

  inline MatrixIdx& Add(INDEX_TYPE scalar, MatrixIdx& result) const
  {
    result.Resize(row,column,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i] + scalar;    
    return result;
  }

  inline MatrixIdx operator - (INDEX_TYPE scalar) const
  {
    MatrixIdx result(row,column,false);  
    return Sub(scalar,result);    
  }
  
  inline MatrixIdx& Sub(INDEX_TYPE scalar, MatrixIdx& result) const
  {
    result.Resize(row,column,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i] - scalar;    
    return result;
  }

  inline MatrixIdx operator * (INDEX_TYPE scalar) const
  {
    MatrixIdx result(row,column,false);  
    return Mult(scalar,result);    
  }

  inline MatrixIdx& Mult(INDEX_TYPE scalar, MatrixIdx& result) const
  {
    result.Resize(row,column,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i] * scalar;    
    return result;
  }


  inline MatrixIdx operator / (INDEX_TYPE scalar) const
  {
    MatrixIdx result(row,column,false);  
    return Div(scalar,result);    
  }

  inline MatrixIdx& Div(INDEX_TYPE scalar, MatrixIdx& result) const
  {
    result.Resize(row,column,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*column+i] = _[j*column+i] / scalar;    
    return result;    
  }

  inline bool operator == (const MatrixIdx& matrix) const
  {
    if((row!=matrix.row)||(column!=matrix.column)) return false;
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        if(_[j*column+i] != matrix._[j*column+i]) return false;
    return true;
  }

  inline bool operator != (const MatrixIdx& matrix) const
  {
    return !(*this ==  matrix);
  }

  inline VectorIdx operator * (const VectorIdx &vector) const
  {
    VectorIdx result(row,false);  
    return Mult(vector,result);    
  }  

  inline VectorIdx Mult(const VectorIdx &vector) const
  {
    VectorIdx result(row,false);  
    return Mult(vector,result);    
  }

  inline VectorIdx& Mult(const VectorIdx &vector, VectorIdx &result) const
  {
    result.Resize(row,false);
    const unsigned int ki = (column<=vector.row?column:vector.row);
    for (unsigned int j = 0; j < row; j++){
		result._[j] = INIT_INDEX_TYPE;
      for (unsigned int i = 0; i < ki; i++)
        result._[j] += _[j*column+i] * vector._[i];
    }
    return result;
  }


  inline MatrixIdx operator * (const MatrixIdx &matrix) const  
  {
    MatrixIdx result(row,matrix.column,false);  
    return Mult(matrix,result);
  }  

  inline MatrixIdx& Mult(const MatrixIdx &matrix, MatrixIdx &result) const
  {
    result.Resize(row,matrix.column,false);
    const unsigned int rrow = result.row;
    const unsigned int rcol = result.column;
    const unsigned int kk = (column<=matrix.row?column:matrix.row);
    for (unsigned int j = 0; j < rrow; j++){
      for (unsigned int i = 0; i < rcol; i++){
		  result._[j*rcol + i] = INIT_INDEX_TYPE;
        for(unsigned int k = 0; k< kk; k++)    
          result._[j*rcol+i] += _[j*column+k] * matrix._[k*rcol+i];
      }
    }    
    return result;
  }



  inline MatrixIdx& Identity()
  {
    const unsigned int k = (row>column?column:row);
    Zero();
    for (unsigned int i = 0; i < k; i++)
		_[i*column + i] = INDEX_TYPE(1);
    return *this;    
  }

  inline MatrixIdx& Diag(const VectorIdx &vector)
  {
    const unsigned int k = (row>column?column:row);
    const unsigned int k2 = (k>vector.row?vector.row:k);
    Zero();
    for (unsigned int i = 0; i < k2; i++)
      _[i*column+i] = vector._[i];
    return *this;    
  }

  inline MatrixIdx& Random(){
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
		  _[j*column + i] = ((INDEX_TYPE)rand()) / ((INDEX_TYPE)(RAND_MAX + 1.0));
    return *this;    
  }

	inline MatrixIdx Transpose() const
	{
    MatrixIdx result(row,column,false);
    return Transpose(result);    
	}

  inline MatrixIdx& Transpose(MatrixIdx &result) const
  {    
    result.Resize(column,row,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[i*row+j] = _[j*column+i];
    return result;    
  }

  inline MatrixIdx VCat(const MatrixIdx& matrix)
  {
    MatrixIdx result;
    return VCat(matrix,result);    
  }
  
  inline MatrixIdx& VCat(const MatrixIdx& matrix, MatrixIdx & result)
  {
    unsigned int k1 = (column>matrix.column?column:matrix.column);
    result.Resize(row+matrix.row,k1,false);
    for (unsigned int j = 0; j < row; j++){
      for (unsigned int i = 0; i < column; i++)
        result._[j*k1+i] = _[j*column+i];
      for (unsigned int i = column; i < k1; i++)
		  result._[j*k1 + i] = INIT_INDEX_TYPE;
    }
    for (unsigned int j = 0; j < matrix.row; j++){
      for (unsigned int i = 0; i < matrix.column; i++)
        result._[(row+j)*k1+i] = matrix._[j*matrix.column+i];
      for (unsigned int i = matrix.column; i < k1; i++)
		  result._[(row + j)*k1 + i] = INIT_INDEX_TYPE;
    }
    return result;
  }

  inline MatrixIdx HCat(const MatrixIdx& matrix)
  {
    MatrixIdx result;
    return HCat(matrix,result);    
  }
  
  inline MatrixIdx& HCat(const MatrixIdx& matrix, MatrixIdx & result)
  {
    unsigned int k1 = (row>matrix.row?row:matrix.row);
    unsigned int k2 = column+matrix.column;
    result.Resize(k1,k2,false);
    for (unsigned int j = 0; j < row; j++)
      for (unsigned int i = 0; i < column; i++)
        result._[j*k2+i] = _[j*column+i];
    for (unsigned int j = row; j < k1; j++)
      for (unsigned int i = 0; i < column; i++)
		  result._[j*k2 + i] = INIT_INDEX_TYPE;

    for (unsigned int j = 0; j < matrix.row; j++)
      for (unsigned int i = 0; i < matrix.column; i++)
        result._[j*k2+i+column] = matrix._[j*matrix.column+i];
    for (unsigned int j = matrix.row; j < k1; j++)
      for (unsigned int i = 0; i < matrix.column; i++)
		  result._[j*k2 + i + column] = INIT_INDEX_TYPE;
    
    return result;
  }

  inline MatrixIdx& SwapRow(unsigned int j1, unsigned int j2){
    if((j1<row)&&(j2<row)){
		INDEX_TYPE tmp;
      for (unsigned int i = 0; i < column; i++){
        tmp            = _[j1*column+i];
        _[j1*column+i] = _[j2*column+i];
        _[j2*column+i] = tmp;        
      }        
    }
    return *this; 
  }
 
  inline MatrixIdx& SwapColumn(unsigned int i1, unsigned int i2){
    if((i1<column)&&(i2<column)){
		INDEX_TYPE tmp;
      for (unsigned int j = 0; j < row; j++){
        tmp            = _[j*column+i1];
        _[j*column+i1] = _[j*column+i2];
        _[j*column+i2] = tmp;        
      }        
    }
    return *this; 
  }

  void Print(LOG_LEVEL level = DATA);
  
  

  


  MatrixIdx& TriDiag(MatrixIdx &tri){
    Resize(tri.ColumnSize(),tri.ColumnSize(),false);
    Zero();
    for(unsigned int i=0;i<column;i++){
      _[i*(column+1)] = tri._[i];
      if(i<column-1)
        _[i*(column+1)+1] = tri._[column+i+1];
      if(i>0)  
        _[i*(column+1)-1] = tri._[column+i];
    }
    return *this;
  }
 
  MatrixIdx RemoveColumn(COUNT_TYPE idx){
	  int cc = 0;
	  MatrixIdx result(row, column - 1);
	  for (COUNT_TYPE c = 0; c < column; c++){
		  if (c == idx) continue;
		  int rr = 0;
		  for (COUNT_TYPE r = 0; r < row; r++){
			  result(rr, cc) = _[r*column + c];
			  rr++;
		  }
		  cc++;
	  }
	  return result;
  }

  MatrixIdx RemoveRow(COUNT_TYPE idx){
	  int cc = 0;
	  MatrixIdx result(row - 1, column);
	  for (COUNT_TYPE c = 0; c < column; c++){
		  int rr = 0;
		  for (COUNT_TYPE r = 0; r < row; r++){
			  if (r == idx) continue;
			  result(rr, cc) = _[r*column + c];
			  rr++;
		  }
		  cc++;
	  }
	  return result;
  }

  void PlushBackColumn(VectorIdx &v){
	  if (row != v.Size()){
		  Resize(v.Size(), column + 1);
	  }
	  else {
		  Resize(row, column + 1);
	  }
	  
	  SetColumn(v, column - 1);
  }

  void PlushBackRow(VectorIdx &v){
	  Resize(row + 1, column);
	  SetRow(v, row - 1);
  }

  MatrixIdx& ClearColumn(unsigned int col){
    if(col<column){
      for(unsigned int i=0;i<row;i++){
		  _[i*column + col] = INIT_INDEX_TYPE;
      }      
    }  
    return *this;
  }

  VectorIdx SumRow(){
    VectorIdx result(column);
    return SumRow(result);
  }

  VectorIdx SumColumn(){
    VectorIdx result(row);
    return SumColumn(result);
  }
  
  VectorIdx & SumRow(VectorIdx & result){
    result.Resize(column,false);
    result.Zero();
    for(unsigned int i=0;i<column;i++){
      for(unsigned int j=0;j<row;j++){
        result._[i] += _[j*column+i];
      }      
    }
    return result;  
  }

  VectorIdx & SumColumn(VectorIdx & result){
    result.Resize(row,false);
    result.Zero();
    for(unsigned int j=0;j<row;j++){
      for(unsigned int i=0;i<column;i++){
        result._[j] += _[j*column+i];
      }      
    }
    return result;  
  }
  bool Save(TiXmlElement *matrix);
  bool Save(char *path);
  bool Load(TiXmlElement *matrix);
  bool Load(char *path);
  
  
protected:

  inline void Release(){
    if(_!=NULL) delete [] _; 
    row    = 0;
    column = 0;
    _      = NULL;
  }  
public:  
  inline virtual void Resize(unsigned int rowSize, unsigned int colSize, bool copy = true){
    if((row!=rowSize)||(column!=colSize)){
      if((rowSize)&&(colSize)){
		  INDEX_TYPE *arr = new INDEX_TYPE[rowSize*colSize];
        if(copy){
          unsigned int mj = (row<rowSize?row:rowSize);
          unsigned int mi = (column<colSize?column:colSize);
          
          for (unsigned int j = 0; j < mj; j++){
            for (unsigned int i = 0; i < mi; i++)
              arr[j*colSize+i] = _[j*column+i];
            for (unsigned int i = mi; i < colSize; i++)
				arr[j*colSize + i] = INIT_INDEX_TYPE;
          }
          for (unsigned int j = mj; j < rowSize; j++){
            for (unsigned int i = 0; i < colSize; i++)
				arr[j*colSize + i] = INIT_INDEX_TYPE;
          }
        }
        if(_!=NULL) delete [] _; 
        _      = arr;
        row    = rowSize;
        column = colSize;        
      }else{
        Release();
      }
    }
  }
};


#endif

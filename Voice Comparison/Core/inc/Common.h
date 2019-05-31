#ifndef COMMON_H
#define COMMON_H

#define _PRINT_DEBUG  // Debug all
// Print Log DCT
#define _PRINT_DEBUG_DCT  // Debug module
#define _TRUNCATE_DCT  // Debug module
#define _PRINT_DEBUG_HMEL // Debug module
#define _PRINT_DEBUG_STEP // In step

// Print Log Pitch
//#define _PRINT_DEBUG_YIN_PITCH // In step
//#define _PRINT_DEBUG_SETING
//#define _PRINT_DEBUG_AMDF_PITCH
//#define _PRINT_DEBUG_PITCH
// Print Log Debug
//#define _PRINT_DEBUG_DWT
//#define _PRINT_DEBUG_DWT_STEP
//#define _PRINT_DEBUG_DWT_DETAIL
//#define _PRINT_DEBUG_DWT_DETAIL_VECTOR
//#define _PRINT_DEBUG_DWT_DETAIL_3DVEC



//Print Log Kmean

#define PRINT_LOG


#ifndef DllExport
#define DllExport  
//_declspec( dllexport ) 
#endif
#define False 0

#define True 1

#define PRINT Logger::Print


//#define USE_EXPONENT_NUMBER

#ifndef DATA_TYPE
#include <float.h>
#define DATA_TYPE   double
#define INIT_DATA_TYPE   0.0
#define MIN_DATA_TYPE   DBL_MIN
#define MAX_DATA_TYPE   DBL_MAX
#endif

#define PRINTF printf

#ifndef INDEX_TYPE
#define INDEX_TYPE   int
#define INIT_INDEX_TYPE   0
#endif

#ifndef COUNT_TYPE
#define COUNT_TYPE   unsigned int
#endif

#ifndef TIME_TYPE
#define TIME_TYPE double
#endif

#ifndef SIGNED_TYPE
#define SIGNED_TYPE   int
#endif
#define Array1D DATA_TYPE *
//#define Array2D DATA_TYPE *

enum LOG_LEVEL {
	DATA = 0x00000008,
	DETAIL = 0x00000004,
	INFORMATION = 0x00000002,
	STEP = 0x00000001,
	NONE = 0x0000000,

	NONE_TIME = 0x80000008
};


#define Array3D std::vector<DATA_TYPE *>

#define O_RDONLY    _O_RDONLY
#define O_WRONLY    _O_WRONLY
#define O_RDWR	    _O_RDWR
#define O_APPEND    _O_APPEND
#define O_CREAT     _O_CREAT
#define O_TRUNC     _O_TRUNC
#define O_EXCL	    _O_EXCL
#define O_TEXT	    _O_TEXT
#define O_BINARY    _O_BINARY
#define O_RAW	    _O_BINARY

#define S_IFMT	 _S_IFMT
#define S_IFDIR  _S_IFDIR
#define S_IFCHR  _S_IFCHR
#define S_IFREG  _S_IFREG
#define S_IREAD  _S_IREAD
#define S_IWRITE _S_IWRITE
#define S_IEXEC  _S_IEXEC

#define	F_OK	0
#define	X_OK	1
#define	W_OK	2
#define	R_OK	4

#define _SH_DENYRW      0x10    /* deny read/write mode */
#define _SH_DENYWR      0x20    /* deny write mode */
#define _SH_DENYRD      0x30    /* deny read mode */
#define _SH_DENYNO      0x40    /* deny none mode */
#define _SH_SECURE      0x80    /* secure mode */

#define _S_IFMT         0xF000          /* file type mask */
#define _S_IFDIR        0x4000          /* directory */
#define _S_IFCHR        0x2000          /* character special */
#define _S_IFIFO        0x1000          /* pipe */
#define _S_IFREG        0x8000          /* regular */
#define _S_IREAD        0x0100          /* read permission, owner */
#define _S_IWRITE       0x0080          /* write permission, owner */
#define _S_IEXEC        0x0040          /* execute/search permission, owner */

#define _O_RDONLY       0x0000  /* open for reading only */
#define _O_WRONLY       0x0001  /* open for writing only */
#define _O_RDWR         0x0002  /* open for reading and writing */
#define _O_APPEND       0x0008  /* writes done at eof */

#define _O_CREAT        0x0100  /* create and open file */
#define _O_TRUNC        0x0200  /* open and truncate */
#define _O_EXCL         0x0400  /* open only if file doesn't already exist */
#ifdef Array3D
#include <vector>
#endif

#endif
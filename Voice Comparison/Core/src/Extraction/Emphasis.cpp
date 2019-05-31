#include <Extraction\ExtractionMFCC.h>
#include <Util\LogUtil.h>
	// y[i] = y[i] - ALPHA * y[i - 1]
Vector MFCC::PreEmphasis(Vector &in){
	Vector out(in.Size());
		DATA_TYPE ALPHA = 0.9700002861f;
		out[0] = in[0];
		for (COUNT_TYPE n = 1; n < in.Size(); n++) {
			out[n] = in[n] - ALPHA * in[n - 1];
		}
		return out;
	}

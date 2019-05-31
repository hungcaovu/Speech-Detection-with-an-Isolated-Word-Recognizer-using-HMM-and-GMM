#ifndef MEDIAN_FILTER_H
#define MEDIAN_FILTER_H
#include "Common.h"
#include <vector>

namespace DSP{
	enum MEDIAN_TYPE{
		AVERAGE_FILTER = 0
	};
	template <typename T>  class MedianFilter1D{

	private:
		//Valid filter
		bool m_valid;
		//Median filter window
		SIGNED_TYPE m_windowSize;
		//output data
		std::vector<T> m_array;
	public:
		void SetWindowSize(COUNT_TYPE windowSize){
			if (windowSize < 3 || !(windowSize % 2)){
				m_valid = false;
				//throw "Invalid window size";
			}
			m_windowSize = windowSize;
		}

		MedianFilter1D(COUNT_TYPE windowSize) :m_valid(true){
			SetWindowSize(windowSize);
		}

		MedianFilter1D():m_windowSize(3), m_valid(true){

		};

		std::vector<T> Filter(const std::vector<T>& v){
			// Check valid
			if (!m_valid) return v;

			//Clear out put array
			m_array.clear();
			std::vector<T> data(v);
			ExtandBoudary(data);
			FilterAvg(data);
			return m_array;
		}

	private:
		void ExtandBoudary(std::vector<T>& v){
			for (int i = -m_windowSize / 2; i <= m_windowSize / 2; i++){
				if (i < 0){
					v.insert(v.begin(), v.front());
				}
				else if (i > 0){
					v.push_back(v.back());
				}
			}
		}
		void FilterAvg(const std::vector<T> v){
			SIGNED_TYPE n = v.size();
			for (SIGNED_TYPE i = 0; i <= n - m_windowSize; i++){
				T res = T(0);
				for (int j = i ; j < m_windowSize + i ; j++){
					res += v[j];
				}

				m_array.push_back(res/m_windowSize);
			}
		}
	};
};
#endif
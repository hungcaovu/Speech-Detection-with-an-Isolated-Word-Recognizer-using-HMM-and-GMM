using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object
{
	public class ScoreMath
	{
		//static double a = 0.0229572f;
		//static double b = 48.8036f;

		//static double a = 8f/25f;
		//static double b = 9.162704f;

		static double a = 1.07686f;
		static double b = 5.0591f;
		static double fac = 7;
		public static double Score(double mfccDis) {

			double e = Math.Exp(mfccDis * b);
			double score = e*a - a;
			if (score > 10) score = 10;
			if (score < 0) score = 0;

			double val = (( 10 - score + 1) / 11) * fac + (10 -fac);

			return val;
		}
	}
}

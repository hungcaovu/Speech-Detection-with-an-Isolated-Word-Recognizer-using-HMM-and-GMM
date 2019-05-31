using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object
{
    public class SelectedChartOption
    {

		public bool YourWave { get; set; }
		public bool RefWave { get; set; }
        public bool YourFreq { get; set; }
        public bool RefFreq { get; set; }

        public bool YourMfcc { get; set; }
        public bool RefMfcc { get; set; }

        public bool YourDetal { get; set; }
        public bool RefDetal { get; set; }

        public bool YourDouble { get; set; }
        public bool RefDouble { get; set; }

        public bool YourPitch { get; set; }
        public bool RefPitch { get; set; }

        public SelectedChartOption() {
			YourWave = false;
			RefWave = false;
            YourFreq = false;
            RefFreq = false;
            YourMfcc = false;
            RefMfcc = false;
            YourDetal = false;
            RefDetal = false;
            YourDouble = false;
            RefDouble = false;
            YourPitch = false;
            RefPitch = false;
        }
    }
}

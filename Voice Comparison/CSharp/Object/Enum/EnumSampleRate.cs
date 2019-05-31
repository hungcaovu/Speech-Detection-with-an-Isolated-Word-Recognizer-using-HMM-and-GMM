using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UC.Enum
{
    public enum EnumSampleRate :int
    {
        KH_8 = 0,
        KH_11 = 0,
        KH_16 = 1,
        KH_444 = 2,
    }
    public class Constant{
        public static string[] TextSampleRate = { "8 000 Khz", "11 025 Khz", "16 000 Khz", "44 100 Khz" };
        public static int[] SampleRate = { 8000, 11025, 16000, 44100 };
    }

    public enum State : int
    {
        FAILED = 0,
        SUCCESSED = 1
    }

}

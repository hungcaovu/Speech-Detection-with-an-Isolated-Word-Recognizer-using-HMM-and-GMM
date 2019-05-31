using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Enum
{
    public enum LOGLEVEL: int
    {
        DATA = 0x0000000F,
        DETAIL = 0x00000007,
        INFORMATION = 0x00000003,
        STEP = 0x00000001,
        NONE = 0x0000000
    }
}

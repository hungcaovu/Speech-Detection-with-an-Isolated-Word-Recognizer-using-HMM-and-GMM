using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Event
{
    public class SettingEventArgs
    {

        public MfccOptions Option { set; get; }
        public SettingEventArgs(MfccOptions opt)
        {
            Option = opt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Event
{
    public class SelectAmTietEventArgs
    {
        public AmTiet AmTiet
        {
            set;
            get;
        }

        public string Path
        {
            set;
            get;
        }
        public SelectAmTietEventArgs(AmTiet amTiet){
            AmTiet = amTiet;
            Path = VCDir.Instance.AudioDir + AmTiet.Path;
        }
    }
}

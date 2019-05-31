using Object.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Event
{
    public class SelectedChartEventArgs: EventArgs
    {
        public FormTag Tag
        {
            set;
            get;
        }

        public bool Value { set; get; }
        public SelectedChartEventArgs( FormTag tag, bool value){
            Tag = tag;
            Value = value;
        }
    }
}

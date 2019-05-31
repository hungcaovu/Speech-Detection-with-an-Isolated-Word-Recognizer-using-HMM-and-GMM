using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Event
{
    public class TimeSelectedEventArgs
    {
        public float Start { set; get; }
        public float End { set; get; }
        public float Width
        {
            set;
            get;
        }
        public TimeSelectedEventArgs(float start, float end, float width)
        {
            Start = start;
            End = end;
            Width = width;
        }
    }
}

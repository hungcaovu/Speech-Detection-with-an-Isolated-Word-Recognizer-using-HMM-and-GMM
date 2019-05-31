using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UC
{
    public partial class WaveImage : Component
    {
        public WaveImage()
        {
            InitializeComponent();
        }

        public WaveImage(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}

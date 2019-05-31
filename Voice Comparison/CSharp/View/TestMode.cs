using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voice_Comparasion
{
    public partial class TestMode : Form
    {
        public TestMode()
        {
            InitializeComponent();
            this.Text = "Voice Comparison - TestMode Build:" + AppVersion.BuildDate + " - Cao Hoc K22 HV: Cao Vu Hung MS: 12 41 006";
        }
    }
}

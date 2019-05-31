using Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voice_Comparasion
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
			this.Text = "Voice Comparison Build:" + AppVersion.BuildDate + " - Cao Hoc K22 HV: Cao Vu Hung MS: 12 41 006";
        }
    }
}

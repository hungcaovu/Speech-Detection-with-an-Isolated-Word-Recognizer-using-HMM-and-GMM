using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Object.Event;
using System.Diagnostics;

namespace Voice_Comparasion
{
    public partial class AmTietForm : DevExpress.XtraEditors.XtraForm
    {
        public event SelectAmTietEventHandler SelectAmTietHandler {
            add {
                amVietViewControl.SelectAmTietHandler += value;

            }
            remove {
                amVietViewControl.SelectAmTietHandler -= value;
            }
        }
        public AmTietForm()
        {
            InitializeComponent();
        }
    }
}
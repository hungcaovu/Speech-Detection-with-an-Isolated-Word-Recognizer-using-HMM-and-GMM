using Model;
using Object;
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
    public partial class ProcessData : Form
    {
        public ProcessData()
        {
            InitializeComponent();
            this.Text = "Voice Comparison - ProcessData Mode Build:" + AppVersion.BuildDate + " - Cao Hoc K22 HV: Cao Vu Hung MS: 12 41 006";
        }


        private void btn_process_Click(object sender, EventArgs e)
        {
            PaserWordTask task = new PaserWordTask();
            task.LoadData(VCDir.Instance.ListWordDir);
            task.UpdateVanTrongAmTiet();
            task.UpdateListWord(VCDir.Instance.ListWordDir);

            amTietView.UpdateDataTable(task.ListAmTiet);
        }
    }
}

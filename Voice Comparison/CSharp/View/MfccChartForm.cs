using Object.Enum;
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
    public partial class MfccChartForm : Form
    {
        static string[] sColStyle = { "GRAY", "HOT", "COLOR" };
        public MfccChartForm(FormTag tag)
        {
            Tag = tag;
            InitializeComponent();

            for (int i = 0; i < sColStyle.Count<string>(); i++)
            {
                colStyle_cbx.Items.Add(sColStyle[i]);
            }
            colStyle_cbx.SelectedIndex = 0;
        }

        public MfccChartForm()
        {
            Tag = FormTag.NONE;

            InitializeComponent();

            for(int i =0; i < sColStyle.Count<string>(); i++){
                colStyle_cbx.Items.Add(sColStyle[i]);
            }
            colStyle_cbx.SelectedIndex = 0;
            
        }
        public List<List<double>> Data
        {
            set
            {
                if (value != null)
                {
                    List<List<double>> tmp = new List<List<double>>(value);
                    //NormalData(tmp);
                    setData(tmp);
                }
                
            }
        }
        //Chuan hoa ve 1.
        private void NormalData(List<List<float>> data) {
            float max = 0;
            foreach (List<float> line in data) {

                float cur_max = 0;
                foreach (float val in line){
                    if(cur_max < Math.Abs(val)){
                        cur_max = Math.Abs(val);
                    }
                }
                if (max < cur_max) {
                    max = cur_max;
                }
            }

            if (max > 0) {
                foreach (List<float> line in data)
                {
                    for (int j = 0; j < line.Count; j++ )
                    {
                        line[j] = line[j] / max * 10;
                    }
                }
            }
        }
        private void setData(List<List<double>> Data)
        {
            if (InvokeRequired)
            {
                Action<List<List<double>>> act = new Action<List<List<double>>>(setData);
                Invoke(act, Data);
            }
            else {
                spectrum.Data = Data;
            }
            
        }
        //private void setPitch(List<float> Data)
        //{
        //    if (InvokeRequired)
        //    {
        //        Action<List<float>> act = new Action<List<float>>(setPitch);
        //        Invoke(act, Data);
        //    }
        //    else
        //    {
        //        spectrum.Pitch = Data;
        //    }

        //}
        public string Title {
            set {
                spectrum.Title = value;
            }
        }

        private void colStyle_cbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            spectrum.ColorStyle = (ColorStyle)colStyle_cbx.SelectedIndex;
        }

    }
}

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtractionWrapper;
using Object.Enum;
namespace Voice_Comparasion
{
	public partial class WaveViewerForm : Form
	{
		private string namePath;
        public WaveViewerForm(FormTag tag)
        {
            Tag = tag;
            InitializeComponent();
        }
        
		public WaveViewerForm() {
			InitializeComponent();
		}

        public string FilePath
        { 
			set {
				if (value != null) {
					namePath = value;
                    newWaveViewer.FilePath = string.Copy(namePath);
				}
			}
		}


		public List<double> Data {
			set {
                setData(value);
			}
		}


        private void setData(List<double> Data){
            if(this.InvokeRequired){
                Action<List<double>> act = new Action<List<double>>(setData);
                Invoke(act, Data);
            }
            else
            {
                newWaveViewer.Data = Data;
                newWaveViewer.PaintData();
            }
        }
	}
}

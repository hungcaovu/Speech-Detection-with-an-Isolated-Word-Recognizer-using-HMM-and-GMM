using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Object;
using Object.Event;
using Object.Enum;


namespace UC
{
    public delegate void ShowChartEvent();
	public partial class SelectShowChart : UserControl
	{
		public SelectShowChart() {
			selected = new SelectedChartOption();
			InitializeComponent();
		}

        private event SelectedChartEventHandler handler = null;

        public event SelectedChartEventHandler SelectedChart
        {
            add
            {
                handler += value;
            }
            remove
            {
                handler -= value;
            }
        }

		private SelectedChartOption selected = null;
		public SelectedChartOption Selected {
			get { return selected; }
			set {
				if (value != null && selected != value) {
					selected = value;
					UpdateSelected();
				}
			}
		}
		public void UpdateSelected() {
			if (InvokeRequired) {
				Action handle = new Action(UpdateSelected);
				Invoke(handle);
				return;
			} else {
				your_wave.Checked = selected.YourWave;
				your_freq.Checked = selected.YourFreq;
				your_mfcc.Checked = selected.YourMfcc;
				your_detal.Checked = selected.YourDetal;
				your_double.Checked = selected.YourDouble;

				ref_wave.Checked = selected.RefWave;
				ref_freq.Checked = selected.RefFreq;
				ref_mfcc.Checked = selected.RefMfcc;
				ref_detal.Checked = selected.RefDetal;
				ref_double.Checked = selected.RefDouble;
			}

		}

		private event ShowChartEvent handleYourWave;

		public event ShowChartEvent ShowChartYourWave {
			add {
				handleYourWave += value;
			}
			remove {
				handleYourWave -= value;
			}
		}

		private event ShowChartEvent handleYourFreq;

		public event ShowChartEvent ShowChartYourFreq {
			add {
				handleYourFreq += value;
			}
			remove {
				handleYourFreq -= value;
			}
		}

		private event ShowChartEvent handleYourMfcc;

		public event ShowChartEvent ShowChartYourMfcc {
			add {
				handleYourMfcc += value;
			}
			remove {
				handleYourMfcc -= value;
			}
		}

		private event ShowChartEvent handleYourDetal;

		public event ShowChartEvent ShowChartYourDetal {
			add {
				handleYourDetal += value;
			}
			remove {
				handleYourDetal -= value;
			}
		}

		private event ShowChartEvent handleYourDouble;

		public event ShowChartEvent ShowChartYourDouble {
			add {
				handleYourDouble += value;
			}
			remove {
				handleYourDouble -= value;
			}
		}

        private event ShowChartEvent handleYourPitch;

        public event ShowChartEvent ShowChartYourPitch
        {
            add
            {
                handleYourPitch += value;
            }
            remove
            {
                handleYourPitch -= value;
            }
        }

		private void your_wave_CheckedChanged(object sender, EventArgs e) {
			selected.YourWave = your_wave.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.YOUR_WAVE, your_wave.Checked));
            }
			if (handleYourWave != null) {
				handleYourWave();
			}
		}
		private void your_freq_CheckedChanged(object sender, EventArgs e) {
			selected.YourFreq = your_freq.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.YOUR_FREQ, your_freq.Checked));
            }
			if (handleYourFreq != null) {
				handleYourFreq();
			}
		}
		private void your_mfcc_CheckedChanged(object sender, EventArgs e) {
			selected.YourMfcc = your_mfcc.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.YOUR_MFCC, your_mfcc.Checked));
            }
			if (handleYourMfcc != null) {
				handleYourMfcc();
			}
		}
		private void your_detal_CheckedChanged(object sender, EventArgs e) {
			selected.YourDetal = your_detal.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.YOUR_DETAL, your_detal.Checked));
            }
			if (handleYourDetal != null) {
				handleYourDetal();
			}
		}
		private void your_double_CheckedChanged(object sender, EventArgs e) {
			selected.YourDouble = your_double.Checked;

            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.YOUR_DOUBLE, your_double.Checked));
            }
			if (handleYourDouble != null) {
				handleYourDouble();
			}
		}

        private void your_pitch_CheckedChanged(object sender, EventArgs e)
        {
            selected.YourPitch = your_pitch.Checked;

            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.YOUR_PITCH, your_pitch.Checked));
            }
            if (handleYourPitch != null)
            {
                handleYourPitch();
            }
        }

		private event ShowChartEvent handleRefWave;

		public event ShowChartEvent ShowChartRefWave {
			add {
				handleRefWave += value;
			}
			remove {
				handleRefWave -= value;
			}
		}

		private event ShowChartEvent handleRefFreq;

		public event ShowChartEvent ShowChartRefFreq {
			add {
				handleRefFreq += value;
			}
			remove {
				handleRefFreq -= value;
			}
		}

		private event ShowChartEvent handleRefMfcc;

		public event ShowChartEvent ShowChartRefMfcc {
			add {
				handleRefMfcc += value;
			}
			remove {
				handleRefMfcc -= value;
			}
		}

		private event ShowChartEvent handleRefDelta;

		public event ShowChartEvent ShowChartRefDetal {
			add {
				handleRefDelta += value;
			}
			remove {
				handleRefDelta -= value;
			}
		}

		private event ShowChartEvent handleRefDouble;

		public event ShowChartEvent ShowChartRefDouble {
			add {
				handleRefDouble += value;
			}
			remove {
				handleRefDouble -= value;
			}
		}


        private event ShowChartEvent handleRefPitch;

        public event ShowChartEvent ShowChartRefPitch
        {
            add
            {
                handleRefPitch += value;
            }
            remove
            {
                handleRefPitch -= value;
            }
        }
		private void ref_wave_CheckedChanged(object sender, EventArgs e) {
			selected.RefWave = ref_wave.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.REF_WAVE, ref_wave.Checked));
            }
			if (handleRefWave != null) {
				handleRefWave();
			}
		}
		private void ref_freq_CheckedChanged(object sender, EventArgs e) {
			selected.RefFreq = ref_freq.Checked;

            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.REF_FREQ, ref_freq.Checked));
            }
			if (handleRefFreq != null) {
				handleRefFreq();
			}
		}
		private void ref_mfcc_CheckedChanged(object sender, EventArgs e) {
			selected.RefMfcc = ref_mfcc.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.REF_MFCC, ref_mfcc.Checked));
            }
			if (handleRefMfcc != null) {
				handleRefMfcc();
			}
		}
		private void ref_detal_CheckedChanged(object sender, EventArgs e) {
			selected.RefDetal = ref_detal.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.REF_DETAL, ref_detal.Checked));
            }
            
            if (handleRefDelta != null) {
				handleRefDelta();
			}
		}
		private void ref_double_CheckedChanged(object sender, EventArgs e) {
			selected.RefDouble = ref_double.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.REF_DOUBLE, ref_double.Checked));
            }
            
			if (handleRefDouble != null) {
				handleRefDouble();
			}
		}

        private void ref_pitch_CheckedChanged(object sender, EventArgs e)
        {
            selected.RefPitch = ref_pitch.Checked;
            if (handler != null)
            {
                handler(this, new SelectedChartEventArgs(FormTag.REF_PITCH, ref_pitch.Checked));
            }

            if (handleRefPitch != null)
            {
                handleRefPitch();
            }
        }

	}
}

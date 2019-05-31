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


namespace UC
{
    public delegate void ChangedSettingEvent();
	public partial class MfccSetting : UserControl
	{
		MfccOptions options;
        public event ChangedSettingEvent handle = null;
        public event SettingChangedEventHandler handle2 = null;
        public event ChangedSettingEvent ChangedSetting {
            add {
                handle += value;
            }

            remove {
                handle -= value;
            }
        }


        public event SettingChangedEventHandler Changed
        {
            add
            {
                handle2 += value;
            }

            remove
            {
                handle2 -= value;
            }
        }

		public MfccOptions Options { 
			set {
				if (options != value && value != null) {
					options = value;
					SetValueOnGUI(options);
				}
			}
			get {
				return options;
			}
		}
		public MfccSetting() {
            options = new MfccOptions();
			options.Reset();
			InitializeComponent();
		}

        private void ok_btn_Click(object sender, EventArgs e)
        {
            SaveValueChanged();
            if (handle != null)
            {
                handle();
            }
            if (handle2 != null){
                handle2(this, new SettingChangedEventArgs(options));
            }
			this.Parent.Hide();
        }

        private void apply_btn_Click(object sender, EventArgs e)
        {
            SaveValueChanged();
            if (handle != null) {
                handle();
            }
            if (handle2 != null)
            {
                handle2(this, new SettingChangedEventArgs(options));
            }
        }

        private void SaveValueChanged() {
            try
            {
                if (cepfilter_tb.Text.Length > 0)
                {
                    options.CepFilter = Convert.ToUInt32(cepfilter_tb.Text);
                }
                if (numceps_tb.Text.Length > 0)
                {
                    options.NumCeps = Convert.ToUInt32(numceps_tb.Text);
                }
                if (lowfreq_tb.Text.Length > 0)
                {
                    options.LowFreq = Convert.ToSingle(lowfreq_tb.Text);
                }
                if (highfreq_tb.Text.Length > 0)
                {
                    options.HighFreq = Convert.ToSingle(highfreq_tb.Text);
                }
                if (timeframe_tb.Text.Length > 0)
                {
                    options.TimeFrame = Convert.ToSingle(timeframe_tb.Text);
                }
                if (timeshift_tb.Text.Length > 0)
                {
                    options.TimeShift = Convert.ToSingle(timeshift_tb.Text);
                }
            } catch(Exception){

            }   
        }

		private void SetValueOnGUI(MfccOptions opt) {
			if (InvokeRequired) {
				Action<MfccOptions> action = new Action<MfccOptions>(SetValueOnGUI);
				Invoke(action, new object[] { opt });
				return;
			} else {
				//if (options.CepFilter != opt.CepFilter) {
				cepfilter_tb.Text = String.Format("{0:0}", opt.CepFilter);
				//}

				//if (options.NumCeps != opt.NumCeps) {
					numceps_tb.Text = String.Format("{0:0}", opt.NumCeps);
				//}

				//if (options.LowFreq != opt.LowFreq) {
					lowfreq_tb.Text = String.Format("{0:0.000}", opt.LowFreq);
				//}

				//if (options.HighFreq != opt.HighFreq) {
					highfreq_tb.Text = String.Format("{0:0.000}", opt.HighFreq);
				//}

				//if (options.TimeFrame != opt.TimeFrame) {
					timeframe_tb.Text = String.Format("{0:0.000}", opt.TimeFrame);
				//}

				//if (options.TimeShift != opt.TimeShift) {
					timeshift_tb.Text = String.Format("{0:0.000}", opt.TimeShift);
				//}
			}
		}
	}
}

using Model;
using Object;
using Object.Enum;
using Object.Event;
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
    public partial class Setting : Form
    {
        public Setting( /*MfccOptions opt*/ )
        {
            InitializeComponent();
            Update(VCContext.Instance.MFCCOptions);
        }

        private MfccOptions Option {
            set {
                if (value != null) {
                    Update(value);
                }
            }
            get {
                return Get();
            }
        }

        private event SettingEventHandler handler;
        public event SettingEventHandler SettingChanged
        {
            add {
                handler += value;
            }
            remove {
                handler -= value;
            }
        }
        private void Update(MfccOptions opt) {
            this.cepfilter_tb.Text = opt.CepFilter.ToString();
            this.highfreq_tb.Text = opt.HighFreq.ToString();
            this.numceps_tb.Text = opt.NumCeps.ToString();
            this.lowfreq_tb.Text = opt.LowFreq.ToString();
            this.timeshift_tb.Text = opt.TimeShift.ToString();
            this.timeframe_tb.Text = opt.TimeFrame.ToString();
            this.useStandardization_cbx.Checked = opt.UseStandardization;


            this.pitchtype_cbx.SelectedIndex = opt.PitchType;
            this.yinThreshold_tbx.Text = opt.YinThreshhold.ToString();
            this.hightFreq_tbx.Text = opt.PitchHighFreq.ToString();
            this.lowFreq_tbx.Text = opt.PitchLowFreq.ToString();
            this.timeshift_tbl.Text = opt.PitchTimeShift.ToString();
            this.timeframe_tbl.Text = opt.PitchTimeFrame.ToString();
            this.median_cb.Checked = opt.UseMedian;
            this.median_tbl.Text = opt.MedianWindow.ToString();
            this.removeUnpitch_cb.Checked = opt.DropUnPitch;

            this.pitch_tbx.Text = opt.PitchThreshold.ToString();
            this.energy_txb.Text = opt.EnergyThreshold.ToString();

            // Train Tab:
            hmmStateNum_tbx.Text = opt.TrainHMMState.ToString();
            gmmCompNum_tbx.Text = opt.TrainGMMComponent.ToString();
            dataType_cbx.SelectedIndex = (int)opt.TrainCofficientType;
            gmmCoVarType_cbx.SelectedIndex = (int)opt.TrainGMMCovVar;


            normal_audio_cbx.Checked = opt.NormalizeAudio;
            remove_noise_cbx.Checked = opt.RemoveNoiseYourAudio;
            shiftToZero_cbx.Checked = opt.ShiftSampleToZero;
            // Log
            enanblelog_cbx.Checked = opt.EnableLog;
            logLevel_cbx.SelectedIndex = 0;
            if (opt.LogLevel == (int)LOGLEVEL.STEP){
                logLevel_cbx.SelectedIndex = 1;
            }
            else if (opt.LogLevel == (int)LOGLEVEL.INFORMATION)
            {
                logLevel_cbx.SelectedIndex = 2;
            }
            else if (opt.LogLevel == (int)LOGLEVEL.DETAIL)
            {
                logLevel_cbx.SelectedIndex = 3;
            }
            else if (opt.LogLevel == (int)LOGLEVEL.DATA)
            {
                logLevel_cbx.SelectedIndex = 4;
            }
        }
        private MfccOptions Get() {
            MfccOptions options = new MfccOptions();
            try
            {
                //MFCC
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

                options.UseStandardization = useStandardization_cbx.Checked;

                //Pitch
                options.PitchType = pitchtype_cbx.SelectedIndex;

                if (yinThreshold_tbx.Text.Length > 0)
                {
                    options.YinThreshhold = Convert.ToSingle(yinThreshold_tbx.Text);
                }
                if (hightFreq_tbx.Text.Length > 0)
                {
                    options.PitchHighFreq = Convert.ToSingle(hightFreq_tbx.Text);
                }
                if (timeshift_tbl.Text.Length > 0)
                {
                    options.PitchTimeShift = Convert.ToSingle(timeshift_tbl.Text);
                }
                if (timeframe_tbl.Text.Length > 0)
                {
                    options.PitchTimeFrame = Convert.ToSingle(timeframe_tbl.Text);
                }
                if (lowFreq_tbx.Text.Length > 0)
                {
                    options.PitchLowFreq = Convert.ToSingle(lowFreq_tbx.Text);
                }

                options.UseMedian = median_cb.Checked;
                if (median_tbl.Text.Length > 0)
                {
                    options.MedianWindow = Convert.ToInt32(median_tbl.Text);
                }

                options.DropUnPitch = removeUnpitch_cb.Checked;
                // VAD
                if (energy_txb.Text.Length > 0)
                {
                    options.EnergyThreshold = Convert.ToSingle(energy_txb.Text);
                }
                if (pitch_tbx.Text.Length > 0)
                {
                    options.PitchThreshold = Convert.ToSingle(pitch_tbx.Text);
                }
                //Noise and Normalize
                options.NormalizeAudio = normal_audio_cbx.Checked;
                options.RemoveNoiseYourAudio = remove_noise_cbx.Checked;
                options.ShiftSampleToZero = shiftToZero_cbx.Checked;
                // Log
                options.EnableLog = enanblelog_cbx.Checked;
                int selectedText = logLevel_cbx.SelectedIndex;
                options.LogLevel = (int)LOGLEVEL.NONE;
                if (selectedText == 1)
                {
                    options.LogLevel = (int)LOGLEVEL.STEP;
                }
                else if (selectedText == 2)
                {
                    options.LogLevel = (int)LOGLEVEL.INFORMATION;
                }
                else if (selectedText == 3)
                {
                    options.LogLevel = (int)LOGLEVEL.DETAIL;
                }
                else if (selectedText == 4)
                {
                    options.LogLevel = (int)LOGLEVEL.DATA;
                }

               //Train
                options.TrainHMMState = (uint)Convert.ToInt32(hmmStateNum_tbx.Text);
                options.TrainGMMComponent = (uint)Convert.ToInt32(gmmCompNum_tbx.Text);
                options.TrainCofficientType = (uint) dataType_cbx.SelectedIndex;
                options.TrainGMMCovVar = (uint)gmmCoVarType_cbx.SelectedIndex;
            }
            catch (Exception)
            {

            }
            return options;
        }

        private void ok_btn_Click(object sender, EventArgs e)
        {
            if (handler != null) {
                VCContext.Instance.MFCCOptions = Get();
                handler(this, new SettingEventArgs(Get()));
            }
            this.Hide();
        }

        private void apply_btn_Click(object sender, EventArgs e)
        {
            if (handler != null)
            {
                VCContext.Instance.MFCCOptions = Get();
                handler(this, new SettingEventArgs(Get()));
            }
        }
    }
}

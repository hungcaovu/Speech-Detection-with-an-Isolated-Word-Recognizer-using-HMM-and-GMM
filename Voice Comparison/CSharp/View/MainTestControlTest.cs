using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UC.Enum;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;
using ExtractionWrapper;
using System.Threading;
using System.Configuration;
using Model;
using Object.Event;
using Voice_Comparasion;
using Object;
using Object.Enum;

namespace UC
{
    public partial class MainTestControlTest : UserControl
    {
        MfccOptions option;



        double peakRef = 0.0f;
        // Ref Right
        string refPath = "";
        //bool right_btn_clicked = false;
        //bool selectRight = false;
        bool rightChanged = true;
        //Your Left
        string yourPath = "";
        //bool left_btn_clicked = false;
        //bool selectLeft = false;
        bool leftChanged = true;



        SelectedChartOption selectedChart = null;
        public MainTestControlTest()
        {
            option = VCContext.Instance.MFCCOptions;
            //ExtractionWrapper.OptionWrapper.SetLog(option.EnableLog);
            InitializeComponent();
            selectedChart = showChart.Selected;
        }


        // Nut Setting
        // Setting Form
        Setting setting_frm = null;
        private void setting_btn_Click(object sender, EventArgs e)
        {
            if (setting_frm == null || setting_frm.IsDisposed)
            {
                setting_frm = new Setting();// (option);
                setting_frm.SettingChanged += SettingChanged;
                setting_frm.Show();
            }
            else if (setting_frm.Visible)
            {
                setting_frm.Show();
            }
            else
            {
                setting_frm.Show();
            }
        }

        private void SettingChanged(object obj, SettingEventArgs e)
        {
            leftChanged = true;
            rightChanged = true;
            option = e.Option;
            //ExtractionWrapper.OptionWrapper.SetLog(option.EnableLog);
        }

        WaveViewerForm yourvoice_wave = null;
        WaveViewerForm refvoice_wave = null;

        MfccChartForm yourvoice_freq = null;
        MfccChartForm refvoice_freq = null;

        MfccChartForm yourvoice_mfcc = null;
        MfccChartForm refvoice_mfcc = null;

        MfccChartForm yourvoice_detal = null;
        MfccChartForm refvoice_detal = null;

        MfccChartForm yourvoice_double = null;
        MfccChartForm refvoice_double = null;

        LineChartForm yourvoice_pitch = null;
        LineChartForm refvoice_pitch = null;

        private void ShowChart(FormTag tag, bool value)
        {
            if (InvokeRequired)
            {
                Action<FormTag, bool> act = new Action<FormTag, bool>(ShowChart);
                Invoke(act, new object[] { tag, value });
            }
            else
            {
                switch (tag)
                {
                    case FormTag.REF_WAVE:
                        if (refvoice_wave == null || refvoice_wave.IsDisposed)
                        {
                            refvoice_wave = new WaveViewerForm(tag);
                            SetDataChart(FormTag.REF_WAVE);

                        }
                        if (value)
                        {
                            if (!refvoice_wave.Visible)
                            {
                                refvoice_wave.Show();
                            }
                        }
                        else
                        {
                            if (refvoice_wave.Visible)
                            {
                                refvoice_wave.Hide();
                            }
                        }

                        break;
                    case FormTag.REF_FREQ:
                        if (refvoice_freq == null || refvoice_freq.IsDisposed)
                        {
                            refvoice_freq = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!refvoice_freq.Visible)
                            {
                                refvoice_freq.Show();
                            }
                        }
                        else
                        {
                            if (refvoice_freq.Visible)
                            {
                                refvoice_freq.Hide();
                            }
                        }

                        break;
                    case FormTag.REF_MFCC:

                        if (refvoice_mfcc == null || refvoice_mfcc.IsDisposed)
                        {
                            refvoice_mfcc = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!refvoice_mfcc.Visible)
                            {
                                refvoice_mfcc.Show();
                            }
                        }
                        else
                        {
                            if (refvoice_mfcc.Visible)
                            {
                                refvoice_mfcc.Hide();
                            }
                        }
                        break;
                    case FormTag.REF_DOUBLE:

                        if (refvoice_double == null || refvoice_double.IsDisposed)
                        {
                            refvoice_double = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!refvoice_double.Visible)
                            {
                                refvoice_double.Show();
                            }
                        }
                        else
                        {
                            if (refvoice_double.Visible)
                            {
                                refvoice_double.Hide();
                            }
                        }
                        break;
                    case FormTag.REF_DETAL:

                        if (refvoice_detal == null || refvoice_detal.IsDisposed)
                        {
                            refvoice_detal = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!refvoice_detal.Visible)
                            {
                                refvoice_detal.Show();
                            }
                        }
                        else
                        {
                            if (refvoice_detal.Visible)
                            {
                                refvoice_detal.Hide();
                            }
                        }
                        break;
                    case FormTag.REF_PITCH:

                        if (refvoice_pitch == null || refvoice_pitch.IsDisposed)
                        {
                            refvoice_pitch = new LineChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!refvoice_pitch.Visible)
                            {
                                refvoice_pitch.Show();
                            }
                        }
                        else
                        {
                            if (refvoice_pitch.Visible)
                            {
                                refvoice_pitch.Hide();
                            }
                        }
                        break;

                    case FormTag.YOUR_WAVE:
                        if (yourvoice_wave == null || yourvoice_wave.IsDisposed)
                        {
                            yourvoice_wave = new WaveViewerForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!yourvoice_wave.Visible)
                            {
                                yourvoice_wave.Show();
                            }
                        }
                        else
                        {
                            if (yourvoice_wave.Visible)
                            {
                                yourvoice_wave.Hide();
                            }
                        }

                        break;
                    case FormTag.YOUR_FREQ:
                        if (yourvoice_freq == null || yourvoice_freq.IsDisposed)
                        {
                            yourvoice_freq = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!yourvoice_freq.Visible)
                            {
                                yourvoice_freq.Show();
                            }
                        }
                        else
                        {
                            if (yourvoice_freq.Visible)
                            {
                                yourvoice_freq.Hide();
                            }
                        }

                        break;
                    case FormTag.YOUR_MFCC:

                        if (yourvoice_mfcc == null || yourvoice_mfcc.IsDisposed)
                        {
                            yourvoice_mfcc = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!yourvoice_mfcc.Visible)
                            {
                                yourvoice_mfcc.Show();
                            }
                        }
                        else
                        {
                            if (yourvoice_mfcc.Visible)
                            {
                                yourvoice_mfcc.Hide();
                            }
                        }
                        break;
                    case FormTag.YOUR_DOUBLE:

                        if (yourvoice_double == null || yourvoice_double.IsDisposed)
                        {
                            yourvoice_double = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!yourvoice_double.Visible)
                            {
                                yourvoice_double.Show();
                            }
                        }
                        else
                        {
                            if (yourvoice_double.Visible)
                            {
                                yourvoice_double.Hide();
                            }
                        }
                        break;
                    case FormTag.YOUR_DETAL:

                        if (yourvoice_detal == null || yourvoice_detal.IsDisposed)
                        {
                            yourvoice_detal = new MfccChartForm(tag);
                            SetDataChart(tag);
                            yourvoice_detal.Text = "";
                        }
                        if (value)
                        {
                            if (!yourvoice_detal.Visible)
                            {
                                yourvoice_detal.Show();
                            }
                        }
                        else
                        {
                            if (yourvoice_detal.Visible)
                            {
                                yourvoice_detal.Hide();
                            }
                        }
                        break;
                    case FormTag.YOUR_PITCH:

                        if (yourvoice_pitch == null || yourvoice_pitch.IsDisposed)
                        {
                            yourvoice_pitch = new LineChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!yourvoice_pitch.Visible)
                            {
                                yourvoice_pitch.Show();
                            }
                        }
                        else
                        {
                            if (yourvoice_pitch.Visible)
                            {
                                yourvoice_pitch.Hide();
                            }
                        }
                        break;
                }
            }
        }
        private void SetDataChart(FormTag tag)
        {
            if (this.InvokeRequired)
            {
                Action<FormTag> act = new Action<FormTag>(SetDataChart);
                Invoke(act, new object[] { tag });
            }
            else
            {
                switch (tag)
                {
                    case FormTag.REF_WAVE:
                        if (refvoice_wave == null || refvoice_wave.IsDisposed)
                        {
                            refvoice_wave = new WaveViewerForm(tag);
                            refvoice_wave.Text = "Ref Wave Chart";
                        }
                        if (refmfcc != null && refmfcc.ProcessDone)
                        {
                            refvoice_wave.FilePath = refmfcc.Path + " FSize: " + refmfcc.FrameSize.ToString() + " SRate:" + _refWav.SampleRate.ToString();
                            refvoice_wave.Data = _refWav.SelectedData;
                        }

                        break;
                    case FormTag.REF_FREQ:
                        if (refvoice_freq == null || refvoice_freq.IsDisposed)
                        {
                            refvoice_freq = new MfccChartForm(tag);
                            refvoice_freq.Text = "Ref Freq Chart";
                        }
                        if (refmfcc != null && refmfcc.ProcessDone)
                        {
                            refvoice_freq.Title = refmfcc.Path;
                            refvoice_freq.Data = refmfcc.Freq;
                        }
                        break;
                    case FormTag.REF_MFCC:

                        if (refvoice_mfcc == null || refvoice_mfcc.IsDisposed)
                        {
                            refvoice_mfcc = new MfccChartForm(tag);
                            refvoice_mfcc.Text = "Ref MFCC Chart";
                        }
                        if (refmfcc != null && refmfcc.ProcessDone)
                        {
                            refvoice_mfcc.Title = refmfcc.Path;
                            refvoice_mfcc.Data = refmfcc.Mfcc;
                        }
                        break;
                    case FormTag.REF_DOUBLE:

                        if (refvoice_double == null || refvoice_double.IsDisposed)
                        {
                            refvoice_double = new MfccChartForm(tag);
                            refvoice_double.Text = "Ref Double Chart";
                        }
                        if (refmfcc != null && refmfcc.ProcessDone)
                        {
                            refvoice_double.Title = refmfcc.Path;
                            refvoice_double.Data = refmfcc.DoubleDetalMfcc;
                        }
                        break;
                    case FormTag.REF_DETAL:

                        if (refvoice_detal == null || refvoice_detal.IsDisposed)
                        {
                            refvoice_detal = new MfccChartForm(tag);
                            refvoice_detal.Text = "Ref Delta Chart";
                        }
                        if (refmfcc != null && refmfcc.ProcessDone)
                        {
                            refvoice_detal.Title = refmfcc.Path;
                            refvoice_detal.Data = refmfcc.DetalMfcc;
                        }
                        break;
                    case FormTag.REF_PITCH:

                        if (refvoice_pitch == null || refvoice_pitch.IsDisposed)
                        {
                            refvoice_pitch = new LineChartForm(tag);
                            refvoice_pitch.Text = "Ref Pitch Chart";
                        }
                        if (refpitch != null /*&& refpitch.IsProcessed == (int)State.SUCCESSED*/)
                        {
                            refvoice_pitch.Data = refpitch.SmoothPitchs;
                        }
                        break;


                    case FormTag.YOUR_WAVE:
                        if (yourvoice_wave == null || yourvoice_wave.IsDisposed)
                        {
                            yourvoice_wave = new WaveViewerForm(tag);
                            yourvoice_wave.Text = "Your Wave Chart";
                        }
                        if (yourmfcc != null && yourmfcc.ProcessDone)
                        {
                            yourvoice_wave.FilePath = yourmfcc.Path + " FSize: " + yourmfcc.FrameSize.ToString() + " SRate:" + _yourWav.SampleRate.ToString(); ;
                            yourvoice_wave.Data = _yourWav.SelectedData;
                        }
                        break;
                    case FormTag.YOUR_FREQ:
                        if (yourvoice_freq == null || yourvoice_freq.IsDisposed)
                        {
                            yourvoice_freq = new MfccChartForm(tag);
                            yourvoice_freq.Text = "Your Freq Chart";
                        }
                        if (yourmfcc != null && yourmfcc.ProcessDone)
                        {
                            yourvoice_freq.Title = yourmfcc.Path;
                            yourvoice_freq.Data = yourmfcc.Freq;
                            //yourvoice_freq.Pitch = yourpitch.Pitchs;
                        }
                        break;
                    case FormTag.YOUR_MFCC:

                        if (yourvoice_mfcc == null || yourvoice_mfcc.IsDisposed)
                        {
                            yourvoice_mfcc = new MfccChartForm(tag);
                            yourvoice_mfcc.Text = "Your MFCC Chart";
                        }
                        if (yourmfcc != null && yourmfcc.ProcessDone)
                        {
                            yourvoice_mfcc.Title = yourmfcc.Path;
                            yourvoice_mfcc.Data = yourmfcc.Mfcc;
                        }
                        break;
                    case FormTag.YOUR_DOUBLE:

                        if (yourvoice_double == null || yourvoice_double.IsDisposed)
                        {
                            yourvoice_double = new MfccChartForm(tag);
                            yourvoice_double.Text = "Your Double Chart";
                        }
                        if (yourmfcc != null && yourmfcc.ProcessDone)
                        {
                            yourvoice_double.Title = yourmfcc.Path;
                            yourvoice_double.Data = yourmfcc.DoubleDetalMfcc;
                        }
                        break;
                    case FormTag.YOUR_DETAL:

                        if (yourvoice_detal == null || yourvoice_detal.IsDisposed)
                        {
                            yourvoice_detal = new MfccChartForm(tag);
                            yourvoice_detal.Text = "Your Delta Chart";
                        }
                        if (yourmfcc != null && yourmfcc.ProcessDone)
                        {
                            yourvoice_detal.Title = yourmfcc.Path;
                            yourvoice_detal.Data = yourmfcc.DetalMfcc;
                        }
                        break;
                    case FormTag.YOUR_PITCH:
                        if (yourvoice_pitch == null || yourvoice_pitch.IsDisposed)
                        {
                            yourvoice_pitch = new LineChartForm(tag);
                            yourvoice_pitch.Text = "Your Pitch Chart";
                        }
                        if (yourpitch != null)
                        {
                            yourvoice_pitch.Data = yourpitch.SmoothPitchs;
                        }
                        break;
                }
            }
        }
        private void ShowSelectedChart(FormTag tag, bool value)
        {
            //SetDataChart(tag);
            ShowChart(tag, value);
        }
        public void ShowChart()
        {
            ShowChart(FormTag.YOUR_WAVE, selectedChart.YourWave);
            ShowChart(FormTag.YOUR_MFCC, selectedChart.YourMfcc);
            ShowChart(FormTag.YOUR_FREQ, selectedChart.YourFreq);
            ShowChart(FormTag.YOUR_DOUBLE, selectedChart.YourDouble);
            ShowChart(FormTag.YOUR_DETAL, selectedChart.YourDetal);
            ShowChart(FormTag.REF_WAVE, selectedChart.RefWave);
            ShowChart(FormTag.REF_MFCC, selectedChart.RefMfcc);
            ShowChart(FormTag.REF_FREQ, selectedChart.RefFreq);
            ShowChart(FormTag.REF_DOUBLE, selectedChart.RefDouble);
            ShowChart(FormTag.REF_DETAL, selectedChart.RefDetal);
        }
        private void showChart_SelectedChart(object obj, SelectedChartEventArgs e)
        {
            selectedChart = showChart.Selected;
            Thread thread = new Thread(() => ShowSelectedChart(e.Tag, e.Value));
            thread.Start();

        }

        MFCCWrapper yourmfcc = null;
        MFCCWrapper refmfcc = null;
        WavFileWrapper _yourWav = null;
        WavFileWrapper _refWav = null;

        PitchWrapper yourpitch = null;
        PitchWrapper refpitch = null;
        private void process_btn_Click(object sender, EventArgs e)
        {
            //WavFileWrapper wav = new WavFileWrapper(@"C:\Users\Jimmy\Desktop\Voice Comparasion\Voice Comparasion\bin\Debug\Data\Data\b\b.wav");
            ////WavFileWrapper wav = new WavFileWrapper(refPath);
            //wav.Load();
            ////VadWrapper vad = new VadWrapper(wav, 0.010f, 0.005f);
            //LineChartForm charF = new LineChartForm(FormTag.NONE);
            ////charF.MaxValue = 600;
            ////charF.Data = vad.Energy;
            ////charF.Show();
            //PitchWrapper pitch = new PitchWrapper(wav, 0.030f, 0.02f, 50, 500, 0);
            //pitch.SetMedianWindowSize(3);
            //pitch.Process();
            //charF.Data = pitch.SmoothPitchs;
            ////LineChartForm charE = new LineChartForm(FormTag.NONE);
            //// charE.MaxValue = 600;
            ////charE.Data = pitch.SmoothPitchs;
            //charF.Show();
            ////charE.Show();
            //EnergyWrapper energy = new EnergyWrapper(wav, 0.030f, 0.02f, true, false);
            //energy.Process();
            //LineChartForm charEn = new LineChartForm(FormTag.NONE);
            /////int max = (int)energy.Energies.Max() + 1;
            ////charEn.MaxValue = max;
            ////charEn.StepValue = (int)(max / 10) + 1;
            //List<float> data = energy.SmoothEnergies;
            //for (int i = 0; i < data.Count; i++) {
            //    data[i] *= 1000.0f;
            //}
            //charEn.Data = data;
            //charEn.Show();
            //return;

            process_btn.Enabled = false;
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync();
        }
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            bool refpeak = false;
            if (rightChanged)
            {
                _refWav = new WavFileWrapper(refPath);
                //_refWav = new WavFileWrapper(@"C:\Users\Jimmy\Desktop\Human Voice Detector\HumanVoiceDetector\bin\Debug\Echo IVRresult_id=21774831&t=audio&r=1434196481.wav"); ;
                if (_refWav.Load())
                {
                    
                    if (option.NormalizeAudio)
                    {
                        peakRef = _refWav.Peak();
                        refpeak = option.NormalizeAudio;
                    }
                    refmfcc = new MFCCWrapper(_refWav, option.TimeFrame, option.TimeShift, option.CepFilter, option.LowFreq, option.HighFreq, option.NumCeps, 4);
                    refmfcc.Process();

                    refpitch = new PitchWrapper(_refWav, option.PitchTimeFrame, option.PitchTimeShift, option.PitchLowFreq, option.PitchHighFreq, option.PitchType, option.DropUnPitch);
                    if (option.UseMedian)
                    {
                        refpitch.SetMedianWindowSize(option.MedianWindow);
                    }

                    refpitch.Process();
                    rightChanged = false;
                    // Debug.WriteLine("Process Ref file completed Time {0}", DateTime.Now);
                }
            }
            if (refmfcc != null && refmfcc.ProcessDone)
            {
                SetDataChart(FormTag.REF_WAVE);
                //Debug.WriteLine("Process Ref: Draw Wave Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_FREQ);
                //Debug.WriteLine("Process Ref: Draw Freq Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_MFCC);
                //Debug.WriteLine("Process Ref: Draw MFCC Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_DOUBLE);
                //Debug.WriteLine("Process Ref: Draw Double Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_DETAL);
                //Debug.WriteLine("Process Ref: Draw Detal Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_PITCH);
                //Debug.WriteLine("Process Ref: Draw Pitch Done {0}", DateTime.Now);
            }

            //Debug.WriteLine("Set data chart Ref Ref Done {0}", DateTime.Now);


            if (leftChanged)
            {
                _yourWav = new WavFileWrapper(yourPath);

                if (_yourWav.Load())
                {
                    yourmfcc = new MFCCWrapper(_yourWav, option.TimeFrame, option.TimeShift, option.CepFilter, option.LowFreq, option.HighFreq, option.NumCeps, 4);
                    if (refpeak)
                    {
                        _yourWav.NormalizeWave(peakRef);
                    }
                    yourmfcc.Process();

                    yourpitch = new PitchWrapper(_yourWav, option.PitchTimeFrame, option.PitchTimeShift, option.PitchLowFreq, option.PitchHighFreq, option.PitchType, option.DropUnPitch);
                    if (option.UseMedian) {
                        yourpitch.SetMedianWindowSize(option.MedianWindow);
                    }
                    yourpitch.Process();
                    leftChanged = false;
                }
            }
            if (yourmfcc != null && yourmfcc.ProcessDone)
            {
                // TO DO: Process Bar
                SetDataChart(FormTag.YOUR_WAVE);
                SetDataChart(FormTag.YOUR_MFCC);
                SetDataChart(FormTag.YOUR_FREQ);
                SetDataChart(FormTag.YOUR_DOUBLE);
                SetDataChart(FormTag.YOUR_DETAL);
                SetDataChart(FormTag.YOUR_PITCH);
            }


            if (yourmfcc != null && refmfcc != null && yourmfcc.ProcessDone && refmfcc.ProcessDone)
            {

                // TO DO: Process Bar
				string log = string.Format("**********************************************************\nCompare : Your Path - {0}\n          Ref Path - {1}\n", yourPath, refPath);
                log += "Distance of 2 Vec Delta MFCC: ";
                
				double dis = DTWUtilWrapper.DistanceOf2Vector(yourmfcc.Mfcc, refmfcc.Mfcc, false);
                double fac = yourmfcc.Mfcc.Count > refmfcc.Mfcc.Count ? refmfcc.Mfcc.Count : yourmfcc.Mfcc.Count;
                double cos = dis / fac;
                
				log += string.Format("Dis MFCC: {0:0.###} Cos  {1:0.###} \n", cos, ScoreMath.Score(cos));
                // TO DO: Process Bar
				
                log += "Distance of 2 Vec Pitch: ";

                double resPitch = DTWUtilWrapper.DistanceOf2Vector(yourpitch.Pitchs, refpitch.Pitchs, true);
                double facD = refpitch.Pitchs.Count > yourpitch.Pitchs.Count ? yourpitch.Pitchs.Count : refpitch.Pitchs.Count;
                double cosp = resPitch / facD;
                log += string.Format("{0:0.###}  \n", cosp);

                List<double> res = DTWUtilWrapper.DistanceOf2Array3DHorizon(yourmfcc.Mfcc, refmfcc.Mfcc, false);
                _mfccViewer.MFCC = res;

				LogUtil.Info(log);
				ShowValueCostMFCC(string.Format("{0:0.###}",ScoreMath.Score(cos)));
                ShowValueCostPitch(string.Format("{0:0.###}", resPitch));
				
            }
        }
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("Complete Procesing: At - {0}", DateTime.Now);

            ShowChart();
            leftChanged = false;
            rightChanged = false;
            process_btn.Enabled = true;

            if (option.SeparateLog && option.EnableLog && (leftChanged || rightChanged))
            {
                ExtractionWrapper.OptionWrapper.SeparateLog();
            }
        }

        private void ShowValueCostMFCC(string text)
        {
            if (InvokeRequired)
            {
                Action<string> act = new Action<string>(ShowValueCostMFCC);
                Invoke(act, new[] { text });
            }
            else
            {

                costmfccvalue_lb.Text = text;
            }
        }

        private void ShowValueCostPitch(string text)
        {
            if (InvokeRequired)
            {
                Action<string> act = new Action<string>(ShowValueCostPitch);
                Invoke(act, new[] { text });
            }
            else
            {
                costpitchvalue_lb.Text = text;
            }
        }

        private AmTietForm leftForm = null;
        private AmTietForm rightForm = null;
        private void right_btn_Click(object sender, EventArgs e)
        {
            rightForm = new AmTietForm();
            rightForm.Text = "Right AmTiet";
            rightForm.SelectAmTietHandler += SelectAmTietRight;
            rightForm.Show();
        }
        public void SelectAmTietRight(object obj, SelectAmTietEventArgs e)
        {
            Debug.WriteLine(" Select file Right: {0}", e.Path);
           // if (refPath != e.Path)
            {
                refPath = e.Path;
                rightChanged = true;
            }
           // else
           // {
           //     rightChanged = false;
           // }
        }
        private void left_btn_Click(object sender, EventArgs e)
        {
            leftForm = new AmTietForm();
            leftForm.Text = "Left AmTiet";
            leftForm.SelectAmTietHandler += SelectAmTietLeft;
            leftForm.Show();
        }
        public void SelectAmTietLeft(object obj, SelectAmTietEventArgs e)
        {
            Debug.WriteLine(" Select file Left: {0}", e.Path);
            //if (yourPath != e.Path)
            {
                yourPath = e.Path;
                leftChanged = true;
            }
            //else
           // {
           ///     leftChanged = false;
           // }
        }
    }
}

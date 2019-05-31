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
    public partial class RegControl : UserControl
    {
        private MfccOptions option;
        /// <summary>
        ///  Recodring
        /// </summary>
        /// 
        private bool _regMode;
        private bool recoding;
        private List<NAudio.Wave.WaveInCapabilities> _sources = null;
        private NAudio.Wave.WaveIn _sourceStream = null;
        private NAudio.Wave.WaveOut _waveOut = null;
        private NAudio.Wave.WaveFileWriter _waveWriter = null;

        //private Dictionary<string, string> _list = null;
        /// <summary>
        /// Ref File
        /// </summary>
        private bool _isPlayRef = false;
        private PlayWave _playRef = null;
        private string refPath = string.Empty;
        private bool _refChanged = false;
        /// <summary>
        /// Your File
        /// </summary>
        private bool _yourChanged = true;
        private bool _isPlayYour = false;
        private PlayWave _playYour = null;
        private string _yourPath = string.Empty;
        private bool _recoder = true;
        private float _startSelected = 0.0f;
        private float _endSelected = 0;
        private float _widthWave = 0;
        private SelectedChartOption _selectedChart = null;
        private string _label = "";

        bool _recalled = false;
        TrainingTask _trainTask;
        public RegControl()
        {
            _regMode = false;
            
            bool designMode = (LicenseManager.UsageMode == LicenseUsageMode.Designtime);
            if (!designMode)
            {
                ExtractionWrapper.OptionWrapper.SetLog(VCContext.Instance.MFCCOptions.LogLevel);
                recoding = false;
                _trainTask = new TrainingTask();
                trainFrom = new TrainingFilesForm(_trainTask);
                trainFrom.RecalledEntry += RecalledRow;
            }
            
            InitializeComponent();
            waveViewer.TimeSelectedChanged += SelectedTimeEventHandler;
            _waveOut = new NAudio.Wave.WaveOut();
            _selectedChart = showChart.Selected;
            regMode_cbx.Checked = _regMode;
            initSampleRate_cbx();
             FreshListDevices();
        }
        /// <summary>
        /// Init sample rate list on gui
        /// </summary>
        private void initSampleRate_cbx()
        {
            sampleRate_cbx.Items.Clear();
            for (int i = 0; i < Constant.TextSampleRate.Count<string>(); i++)
            {
                sampleRate_cbx.Items.Add(Constant.TextSampleRate[i]);
            }
            sampleRate_cbx.SelectedIndex = 1;
        }
        /// <summary>
        /// Fresh Lust divices
        /// </summary>
        private void FreshListDevices()
        {
            _sources = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                _sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }
            foreach (var source in _sources)
            {
                devices_cbx.Items.Add(source.ProductName);
            }
            if (_sources.Count != 0)
            {
                devices_cbx.SelectedIndex = 0;
            }
        }
        private void startRecordSound()
        {
            int deviceNumber = devices_cbx.SelectedIndex;
            int sampleRate = sampleRate_cbx.SelectedIndex;
            if (sampleRate >= 0 && deviceNumber >= 0 && sampleRate < Constant.TextSampleRate.Count<string>())
            {
                _sourceStream = new NAudio.Wave.WaveIn();
                _sourceStream.DeviceNumber = deviceNumber;
                _sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(Constant.SampleRate[sampleRate], 1);

                _yourPath = VCDir.Instance.PathWaveFile;
                _yourChanged = true;
                _sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
                _waveWriter = new NAudio.Wave.WaveFileWriter(_yourPath, _sourceStream.WaveFormat);

                _sourceStream.StartRecording();
            }
            else
            {
                return;
            }
        }
        private void stopRecordSound()
        {

            if (_sourceStream != null)
            {
                _sourceStream.StopRecording();
                _sourceStream.Dispose();
                _sourceStream = null;
            }

            if (_waveWriter != null)
            {
                _waveWriter.Dispose();
                _waveWriter = null;
            }
        }
        private void showWaveSound()
        {

            _yourWav = new WavFileWrapper(_yourPath);
            if (_yourWav.Load()) {

                waveViewer.WaveData = _yourWav.FullData;
                waveViewer.FitToScreen();
                vadVoice();
                if (_regMode)
                {
                    uint size = (uint)_yourWav.FullData.Count;
                    uint begin = (uint)(_startSelected * size);
                    uint end = (uint)(_endSelected * size);
                    _yourWav.NormalizeWave(1.0f);
                    option = VCContext.Instance.MFCCOptions;
                    LogUtil.Info("Load Wave: {0}   -- OK\n", _yourPath);
                    if (option.ShiftSampleToZero)
                    {
                        LogUtil.Info("Shift Sample To Zero: --   -- OK\n");
                        _yourWav.ShifToZero();
                    }


                    Debug.WriteLine("Select Data voice: Start {0} End {1}", begin, end);
                    _yourWav.SelectedWave(begin, end);

                    _yourMfcc = new MFCCWrapper(_yourWav, option.TimeFrame, option.TimeShift, option.CepFilter, option.LowFreq, option.HighFreq, option.NumCeps, 4);
                    _yourMfcc.UserStandardization = option.UseStandardization;


                    if (_yourMfcc != null && _yourMfcc.Process())
                    {
                        List<List<double>> data = null;

                        switch (VCContext.Instance.MFCCOptions.TrainCofficientType)
                        {
                            case 0:
                                data = _yourMfcc.Mfcc;
                                break;
                            case 1:
                                data = _yourMfcc.DetalMfcc;
                                break;
                            case 2:
                                data = _yourMfcc.DoubleDetalMfcc;
                                break;
                        }
                        Action act = new Action(() =>
                        {
                            reg_lb.Text = _trainTask.Reg(data);
                        });

                        Invoke(act);
                    }
                    else {
                        MessageBox.Show(" Cant Extraction file {0}\n", _yourPath);
                    }
                }
            }
        }
        private void vadVoice()
        {
            option = VCContext.Instance.MFCCOptions;
            if (_yourWav != null && _yourWav.IsValid)
            {
                VadWrapper vad = new VadWrapper(_yourWav);
                ZeroRateWrapper zrc = new ZeroRateWrapper(_yourWav, 0.02f, 0.01f, true);
                vad.UseEnergy(0.015f, 0.01f, true, 3, false);
                zrc.Process();
                if (vad.Process(option.EnergyThreshold))
                {
                    float begin = 0;
                    float end = 0;
                    uint deta = 0;
                    for (uint i = 0; i < vad.GetSizeOfSegment(); i++)
                    {
                        if (vad.GetEndSegment(i) - vad.GetStartSegment(i) > deta)
                        {
                            deta = vad.GetEndSegment(i) - vad.GetStartSegment(i);
                            begin = (float)vad.GetStartSegment(i);
                            end = (float)vad.GetEndSegment(i);
                        }
                    }
                    if (deta > 0)
                    {
                        int size = _yourWav.FullData.Count;
                        _startSelected = begin / size;
                        _endSelected = end / size;
                        waveViewer.LeftSlider = _startSelected;
                        waveViewer.RightSlider = _endSelected;
                        waveViewer.ThresholdChart = (float)vad.ThresholdEnergy;
                        //waveViewer.Chart = vad.SmoothEnergies;
                        waveViewer.ChartBlue = zrc.ZeroRate;
                    }
                }
            }

        }
        private void recoder_btn_Click(object sender, EventArgs e)
        {
            recoding = !recoding;
            if (_recoder)
            {
                if (recoding)
                {
                    reg_lb.Text = "...";
                    recoder_btn.Image = Voice_Comparasion.Properties.Resources.Recording;
                    startRecordSound();
                }
                else
                {
                    recoder_btn.Image = Voice_Comparasion.Properties.Resources.Pause;
                    stopRecordSound();
                    showWaveSound();
                }
            }
            else
            {
                showWaveSound();
            }
        }
        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (_waveWriter == null) 
            { 
                return; 
            }

            _waveWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            _waveWriter.Flush();
        }
        private void SelectedTimeEventHandler(object sender, TimeSelectedEventArgs e)
        {
            Debug.WriteLine("Selected Time changed");
            _startSelected = e.Start;
            _endSelected = e.End;
            _widthWave = e.Width;
            _yourChanged = true;
        }
        private void waveOut_PlaybackStopped(object sender, EventArgs e)
        {

        }
        private void Recoder_SizeChanged(object sender, EventArgs e)
        {
            waveViewer.FitToScreen();
        }
        private void StoreWave(string path, NAudio.Wave.WaveOffsetStream stream)
        {
            using (WaveFileWriter writer = new WaveFileWriter(path, stream.WaveFormat))
            {
                byte[] buffer = new byte[stream.Length];
                while (stream.Position < stream.Length)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    writer.WriteData(buffer, 0, bytesRead);
                    writer.Flush();
                }
            }
        }
        private void PlayStopref_btn_Click(object sender, EventArgs e)
        {
            _isPlayRef = !_isPlayRef;
            if (_isPlayRef)
            {
                Debug.WriteLine("Play Audio Ref");
                _playRef = new PlayWave(refPath);
                _playRef.RaisePlayStop += RefPlayStop;
                //playStopref_btn.Image = global::Voice_Comparasion.Properties.Resources.Stop;
                _playRef.Play();
            }
            else
            {
                Debug.WriteLine("Stop Audio Ref");
                _playRef.Stop();
                _playRef = null;
            }
        }
        private void RefPlayStop(object obj, PlayWaveStopEventArgs e)
        {
            Debug.WriteLine("Stop Audio Ref");
            _isPlayRef = false;
           // playStopref_btn.Image = global::Voice_Comparasion.Properties.Resources.Play;
        }
        private void PlayStopyour_btn_Click(object sender, EventArgs e)
        {
            _isPlayYour = !_isPlayYour;
            if (_isPlayYour && _yourPath != null && File.Exists(_yourPath))
            {
                Debug.WriteLine("Play Audio Ref");
                _playYour = new PlayWave(_yourPath);
                _playYour.StartPosition = _startSelected;
                _playYour.EndPosition = _endSelected;
                _playYour.RaisePlayStop += YourPlayStop;
                playStopyour_btn.Image = global::Voice_Comparasion.Properties.Resources.Stop;
                _playYour.Play();
                
            }
            else
            {
                if (_playYour != null)
                {
                    Debug.WriteLine("Stop Audio Ref");
                    _playYour.Stop();
                    _playYour = null;
                }
            }
        }
        private void YourPlayStop(object obj, PlayWaveStopEventArgs e)
        {
            Debug.WriteLine("Stop Audio Ref");
            _isPlayYour = false;
            playStopyour_btn.Image = global::Voice_Comparasion.Properties.Resources.Play;
        }
        private void setting_btn_Click(object sender, EventArgs e)
        {
            if (_settingFrm == null || _settingFrm.IsDisposed)
            {
                _settingFrm = new Setting();// (option);
                _settingFrm.SettingChanged += SettingChanged;
                _settingFrm.Show();
            }
            else if (_settingFrm.Visible)
            {
                _settingFrm.Show();
            }
            else
            {
                _settingFrm.Show();
            }
        }
        private void SettingChanged(object obj, SettingEventArgs e)
        {
            _refChanged = true;
            _yourChanged = true;
            option = e.Option;
            ExtractionWrapper.OptionWrapper.SetLog(e.Option.LogLevel);
        }

        // Nut Setting
        // Setting Form
        private Setting _settingFrm = null;

        private WaveViewerForm _yourVoiceWave = null;
        private WaveViewerForm _refVoiceWave = null;

        private MfccChartForm _yourVoiceFreq = null;
        private MfccChartForm _refVoiceFreq = null;

        private MfccChartForm _yourVoiceMfcc = null;
        private MfccChartForm _refVoiceMfcc = null;

        private MfccChartForm _yourVoiceDetal = null;
        private MfccChartForm _refVoiceDetal = null;

        private MfccChartForm _yourVoiceDouble = null;
        private MfccChartForm _refVoiceDouble = null;

        private LineChartForm _yourVoicePitch = null;
        private LineChartForm _refVoicePitch = null;

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
                        if (_refVoiceWave == null || _refVoiceWave.IsDisposed)
                        {
                            _refVoiceWave = new WaveViewerForm(tag);
                            SetDataChart(FormTag.REF_WAVE);

                        }
                        if (value)
                        {
                            if (!_refVoiceWave.Visible)
                            {
                                _refVoiceWave.Show();
                            }
                        }
                        else
                        {
                            if (_refVoiceWave.Visible)
                            {
                                _refVoiceWave.Hide();
                            }
                        }

                        break;
                    case FormTag.REF_FREQ:
                        if (_refVoiceFreq == null || _refVoiceFreq.IsDisposed)
                        {
                            _refVoiceFreq = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_refVoiceFreq.Visible)
                            {
                                _refVoiceFreq.Show();
                            }
                        }
                        else
                        {
                            if (_refVoiceFreq.Visible)
                            {
                                _refVoiceFreq.Hide();
                            }
                        }

                        break;
                    case FormTag.REF_MFCC:

                        if (_refVoiceMfcc == null || _refVoiceMfcc.IsDisposed)
                        {
                            _refVoiceMfcc = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_refVoiceMfcc.Visible)
                            {
                                _refVoiceMfcc.Show();
                            }
                        }
                        else
                        {
                            if (_refVoiceMfcc.Visible)
                            {
                                _refVoiceMfcc.Hide();
                            }
                        }
                        break;
                    case FormTag.REF_DOUBLE:

                        if (_refVoiceDouble == null || _refVoiceDouble.IsDisposed)
                        {
                            _refVoiceDouble = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_refVoiceDouble.Visible)
                            {
                                _refVoiceDouble.Show();
                            }
                        }
                        else
                        {
                            if (_refVoiceDouble.Visible)
                            {
                                _refVoiceDouble.Hide();
                            }
                        }
                        break;
                    case FormTag.REF_DETAL:

                        if (_refVoiceDetal == null || _refVoiceDetal.IsDisposed)
                        {
                            _refVoiceDetal = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_refVoiceDetal.Visible)
                            {
                                _refVoiceDetal.Show();
                            }
                        }
                        else
                        {
                            if (_refVoiceDetal.Visible)
                            {
                                _refVoiceDetal.Hide();
                            }
                        }
                        break;
                    case FormTag.REF_PITCH:

                        if (_refVoicePitch == null || _refVoicePitch.IsDisposed)
                        {
                            _refVoicePitch = new LineChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_refVoicePitch.Visible)
                            {
                                _refVoicePitch.Show();
                            }
                        }
                        else
                        {
                            if (_refVoicePitch.Visible)
                            {
                                _refVoicePitch.Hide();
                            }
                        }
                        break;

                    case FormTag.YOUR_WAVE:
                        if (_yourVoiceWave == null || _yourVoiceWave.IsDisposed)
                        {
                            _yourVoiceWave = new WaveViewerForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_yourVoiceWave.Visible)
                            {
                                _yourVoiceWave.Show();
                            }
                        }
                        else
                        {
                            if (_yourVoiceWave.Visible)
                            {
                                _yourVoiceWave.Hide();
                            }
                        }

                        break;
                    case FormTag.YOUR_FREQ:
                        if (_yourVoiceFreq == null || _yourVoiceFreq.IsDisposed)
                        {
                            _yourVoiceFreq = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_yourVoiceFreq.Visible)
                            {
                                _yourVoiceFreq.Show();
                            }
                        }
                        else
                        {
                            if (_yourVoiceFreq.Visible)
                            {
                                _yourVoiceFreq.Hide();
                            }
                        }

                        break;
                    case FormTag.YOUR_MFCC:

                        if (_yourVoiceMfcc == null || _yourVoiceMfcc.IsDisposed)
                        {
                            _yourVoiceMfcc = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_yourVoiceMfcc.Visible)
                            {
                                _yourVoiceMfcc.Show();
                            }
                        }
                        else
                        {
                            if (_yourVoiceMfcc.Visible)
                            {
                                _yourVoiceMfcc.Hide();
                            }
                        }
                        break;
                    case FormTag.YOUR_DOUBLE:

                        if (_yourVoiceDouble == null || _yourVoiceDouble.IsDisposed)
                        {
                            _yourVoiceDouble = new MfccChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_yourVoiceDouble.Visible)
                            {
                                _yourVoiceDouble.Show();
                            }
                        }
                        else
                        {
                            if (_yourVoiceDouble.Visible)
                            {
                                _yourVoiceDouble.Hide();
                            }
                        }
                        break;
                    case FormTag.YOUR_DETAL:

                        if (_yourVoiceDetal == null || _yourVoiceDetal.IsDisposed)
                        {
                            _yourVoiceDetal = new MfccChartForm(tag);
                            SetDataChart(tag);
                            _yourVoiceDetal.Text = string.Empty;
                        }
                        if (value)
                        {
                            if (!_yourVoiceDetal.Visible)
                            {
                                _yourVoiceDetal.Show();
                            }
                        }
                        else
                        {
                            if (_yourVoiceDetal.Visible)
                            {
                                _yourVoiceDetal.Hide();
                            }
                        }
                        break;
                    case FormTag.YOUR_PITCH:

                        if (_yourVoicePitch == null || _yourVoicePitch.IsDisposed)
                        {
                            _yourVoicePitch = new LineChartForm(tag);
                            SetDataChart(tag);
                        }
                        if (value)
                        {
                            if (!_yourVoicePitch.Visible)
                            {
                                _yourVoicePitch.Show();
                            }
                        }
                        else
                        {
                            if (_yourVoicePitch.Visible)
                            {
                                _yourVoicePitch.Hide();
                            }
                        }
                        break;
                }
            }
        }
        private void SetDataChart(FormTag tag)
        {
            if (InvokeRequired)
            {
                Action<FormTag> act = new Action<FormTag>(SetDataChart);
                Invoke(act, new object[] { tag });
            }
            else
            {
                switch (tag)
                {
                    case FormTag.REF_WAVE:
                        if (_refVoiceWave == null || _refVoiceWave.IsDisposed)
                        {
                            _refVoiceWave = new WaveViewerForm(tag);
                            _refVoiceWave.Text = "Ref Wave Chart";
                        }
                        if (_refMfcc != null && _refMfcc.ProcessDone && _refWav != null && _refWav.IsValid)
                        {
                            _refVoiceWave.FilePath = _refWav.Path + " FSize: " + _refMfcc.FrameSize.ToString() + " SRate:" + _refWav.SampleRate.ToString();
                            _refVoiceWave.Data = _refWav.SelectedData;
                        }

                        break;
                    case FormTag.REF_FREQ:
                        if (_refVoiceFreq == null || _refVoiceFreq.IsDisposed)
                        {
                            _refVoiceFreq = new MfccChartForm(tag);
                            _refVoiceFreq.Text = "Ref Freq Chart";
                        }
                        if (_refMfcc != null && _refMfcc.ProcessDone)
                        {
                            _refVoiceFreq.Title = _refMfcc.Path;
                            _refVoiceFreq.Data = _refMfcc.Freq;
                        }
                        break;
                    case FormTag.REF_MFCC:

                        if (_refVoiceMfcc == null || _refVoiceMfcc.IsDisposed)
                        {
                            _refVoiceMfcc = new MfccChartForm(tag);
                            _refVoiceMfcc.Text = "Ref MFCC Chart";
                        }
                        if (_refMfcc != null && _refMfcc.ProcessDone)
                        {
                            _refVoiceMfcc.Title = _refMfcc.Path;
                            _refVoiceMfcc.Data = _refMfcc.Mfcc;
                        }
                        break;
                    case FormTag.REF_DOUBLE:

                        if (_refVoiceDouble == null || _refVoiceDouble.IsDisposed)
                        {
                            _refVoiceDouble = new MfccChartForm(tag);
                            _refVoiceDouble.Text = "Ref Double Chart";
                        }
                        if (_refMfcc != null && _refMfcc.ProcessDone)
                        {
                            _refVoiceDouble.Title = _refMfcc.Path;
                            _refVoiceDouble.Data = _refMfcc.DoubleDetalMfcc;
                        }
                        break;
                    case FormTag.REF_DETAL:

                        if (_refVoiceDetal == null || _refVoiceDetal.IsDisposed)
                        {
                            _refVoiceDetal = new MfccChartForm(tag);
                            _refVoiceDetal.Text = "Ref Delta Chart";
                        }
                        if (_refMfcc != null && _refMfcc.ProcessDone)
                        {
                            _refVoiceDetal.Title = _refMfcc.Path;
                            _refVoiceDetal.Data = _refMfcc.DetalMfcc;
                        }
                        break;
                    case FormTag.REF_PITCH:

                        if (_refVoicePitch == null || _refVoicePitch.IsDisposed)
                        {
                            _refVoicePitch = new LineChartForm(tag);
                            _refVoicePitch.Text = "Ref Pitch Chart";
                        }
                        if (_refPitch != null)
                        {
                            _refVoicePitch.Data = _refPitch.SmoothPitchs;
                        }
                        break;


                    case FormTag.YOUR_WAVE:
                        if (_yourVoiceWave == null || _yourVoiceWave.IsDisposed)
                        {
                            _yourVoiceWave = new WaveViewerForm(tag);
                            _yourVoiceWave.Text = "Your Wave Chart";
                        }
                        if (_yourWav != null && _yourWav.IsValid && _yourMfcc != null && _yourMfcc.ProcessDone)
                        {
                            _yourVoiceWave.FilePath = _yourMfcc.Path + " FSize: " + _yourMfcc.FrameSize.ToString() + " SRate:" + _yourWav.SampleRate.ToString();
                            _yourVoiceWave.Data = _yourWav.SelectedData;
                        }
                        break;
                    case FormTag.YOUR_FREQ:
                        if (_yourVoiceFreq == null || _yourVoiceFreq.IsDisposed)
                        {
                            _yourVoiceFreq = new MfccChartForm(tag);
                            _yourVoiceFreq.Text = "Your Freq Chart";
                        }
                        if (_yourMfcc != null && _yourMfcc.ProcessDone)
                        {
                            _yourVoiceFreq.Title = _yourMfcc.Path;
                            _yourVoiceFreq.Data = _yourMfcc.Freq;
                        }
                        break;
                    case FormTag.YOUR_MFCC:

                        if (_yourVoiceMfcc == null || _yourVoiceMfcc.IsDisposed)
                        {
                            _yourVoiceMfcc = new MfccChartForm(tag);
                            _yourVoiceMfcc.Text = "Your MFCC Chart";
                        }
                        if (_yourMfcc != null && _yourMfcc.ProcessDone)
                        {
                            _yourVoiceMfcc.Title = _yourMfcc.Path;
                            _yourVoiceMfcc.Data = _yourMfcc.Mfcc;
                        }
                        break;
                    case FormTag.YOUR_DOUBLE:

                        if (_yourVoiceDouble == null || _yourVoiceDouble.IsDisposed)
                        {
                            _yourVoiceDouble = new MfccChartForm(tag);
                            _yourVoiceDouble.Text = "Your Double Chart";
                        }
                        if (_yourMfcc != null && _yourMfcc.ProcessDone)
                        {
                            _yourVoiceDouble.Title = _yourMfcc.Path;
                            _yourVoiceDouble.Data = _yourMfcc.DoubleDetalMfcc;
                        }
                        break;
                    case FormTag.YOUR_DETAL:

                        if (_yourVoiceDetal == null || _yourVoiceDetal.IsDisposed)
                        {
                            _yourVoiceDetal = new MfccChartForm(tag);
                            _yourVoiceDetal.Text = "Your Delta Chart";
                        }
                        if (_yourMfcc != null && _yourMfcc.ProcessDone)
                        {
                            _yourVoiceDetal.Title = _yourMfcc.Path;
                            _yourVoiceDetal.Data = _yourMfcc.DetalMfcc;
                        }
                        break;
                    case FormTag.YOUR_PITCH:
                        if (_yourVoicePitch == null || _yourVoicePitch.IsDisposed)
                        {
                            _yourVoicePitch = new LineChartForm(tag);
                            _yourVoicePitch.Text = "Your Pitch Chart";
                        }
                        if (_yourPitch != null)
                        {
                            _yourVoicePitch.Data = _yourPitch.SmoothPitchs;
                        }
                        break;
                }
            }
        }
        private void ShowSelectedChart(FormTag tag, bool value)
        {
            ///SetDataChart(tag);
            ShowChart(tag, value);
        }
        public void ShowChart()
        {
            ShowChart(FormTag.YOUR_WAVE, _selectedChart.YourWave);
            ShowChart(FormTag.YOUR_MFCC, _selectedChart.YourMfcc);
            ShowChart(FormTag.YOUR_FREQ, _selectedChart.YourFreq);
            ShowChart(FormTag.YOUR_DOUBLE, _selectedChart.YourDouble);
            ShowChart(FormTag.YOUR_DETAL, _selectedChart.YourDetal);
        }
        private void showChart_SelectedChart(object obj, SelectedChartEventArgs e)
        {
            _selectedChart = showChart.Selected;
            Thread thread = new Thread(() => ShowSelectedChart(e.Tag, e.Value));
            thread.Start();

        }

        private MFCCWrapper _yourMfcc = null;
        private MFCCWrapper _refMfcc = null;

        private PitchWrapper _yourPitch = null;
        private PitchWrapper _refPitch = null;

        private WavFileWrapper _yourWav = null;
        private WavFileWrapper _refWav = null;

        private void process_btn_Click(object sender, EventArgs e)
        {
            process_btn.Enabled = false;
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync(false);
        }
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
			bool reg = (bool)e.Argument;
            if (_yourChanged)
            {
                _yourWav = new WavFileWrapper(_yourPath);
               
                if (_yourWav.Load())
                {
                    _yourWav.NormalizeWave(1.0);
                    option = VCContext.Instance.MFCCOptions;
                    LogUtil.Info("Load Wave: {0}   -- OK\n", _yourPath);
                    if (option.ShiftSampleToZero)
                    {
                        LogUtil.Info("Shift Sample To Zero: --   -- OK\n");
                        _yourWav.ShifToZero();
                    }

                    int size = _yourWav.FullData.Count;
                    uint startPnt = (uint)(_startSelected * size);
                    uint endPnt = (uint)(_endSelected * size);
                    Debug.WriteLine("Select Data voice: Start {0} End {1}", startPnt, endPnt);
                    _yourWav.SelectedWave(startPnt, endPnt);

                    _yourMfcc = new MFCCWrapper(_yourWav, option.TimeFrame, option.TimeShift, option.CepFilter, option.LowFreq, option.HighFreq, option.NumCeps, 4);
                    _yourMfcc.UserStandardization = option.UseStandardization;
                    _yourMfcc.Process();

                    _yourPitch = new PitchWrapper(_yourWav, option.PitchTimeFrame, option.PitchTimeShift, option.PitchLowFreq, option.PitchHighFreq, option.PitchType, option.DropUnPitch);
                    if (option.UseMedian)
                    {
                        _yourPitch.SetMedianWindowSize(option.MedianWindow);
                    }
                    _yourPitch.Process();
                }
            }

            if (_yourMfcc != null && _yourMfcc.ProcessDone)
            {
                // TO DO: Process Bar
                SetDataChart(FormTag.YOUR_WAVE);
                SetDataChart(FormTag.YOUR_MFCC);
                SetDataChart(FormTag.YOUR_FREQ);
                SetDataChart(FormTag.YOUR_DOUBLE);
                SetDataChart(FormTag.YOUR_DETAL);
                SetDataChart(FormTag.YOUR_PITCH);
            }
			e.Result = reg;
            if(reg){
                if (_yourMfcc != null)
                {
                    List<List<double>> data = null;

                    switch (VCContext.Instance.MFCCOptions.TrainCofficientType)
                    {
                        case 0:
                            data = _yourMfcc.Mfcc;
                            break;
                        case 1:
                            data = _yourMfcc.DetalMfcc;
                            break;
                        case 2:
                            data = _yourMfcc.DoubleDetalMfcc;
                            break;
                    }
                    Action act = new Action(() =>
                    {
                        reg_lb.Text = _trainTask.Reg(data);
                    });

                    Invoke(act);

                }
            }
        }
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("Complete Procesing: At - {0}", DateTime.Now);

            ShowChart();
            _refChanged = false;
            _yourChanged = false;
            process_btn.Enabled = true;
            option = VCContext.Instance.MFCCOptions;
			if ((bool)e.Result) {
                reg_btn.Enabled = true;
			}

            if (option.SeparateLog && option.EnableLog && (_refChanged || _yourChanged))
            {
                ExtractionWrapper.OptionWrapper.SeparateLog();
            }
        }
        private void autoSelect_btn_Click(object sender, EventArgs e)
        {
            vadVoice();
        }
        private AmTietForm referForm = null;
        private void refWord_btn_Click(object sender, EventArgs e)
        {
            referForm = new AmTietForm();
            referForm.Text = "Refer AmTiet";
            referForm.SelectAmTietHandler += SelectReferAmTiet;
            referForm.Show();
        }
        public void SelectReferAmTiet(object obj, SelectAmTietEventArgs e)
        {
            if (refPath != e.Path)
            {
                refPath = e.Path;
                _refChanged = true;
            }
            else
            {
                _refChanged = false;
            }
        }
        TrainingFilesForm trainFrom = null;
        private void listTrainFiles_btn_Click(object sender, EventArgs e)
        {
            if (trainFrom == null || trainFrom.IsDisposed)
            {
                trainFrom = new TrainingFilesForm(_trainTask);
                trainFrom.RecalledEntry += RecalledRow;
            }
                
            trainFrom.Show();
        }
        private void RecalledRow(object obj, RecallEntryEventArgs e) {
            _yourChanged = true;
            _recalled = true;
            UpdateRow(e.RecalledRow);
        }
        private void addToTrainFile_btn_Click(object sender, EventArgs e)
        {
            if (_yourWav != null)
            {
				int size = _yourWav.FullData.Count;
				int startPnt = (int)(_startSelected * size);
				int endPnt = (int)(_endSelected * size);
				string word = "";
				DialogResult re = InputBox("Nhap Word", "Vui Long nhap tu muon training:", ref word);
				if (re == DialogResult.OK && !string.IsNullOrEmpty(word)) {
					string traingDir = VCDir.Instance.TrainDirAudio + word;
					VCDir.CreateDirectory(traingDir);
					string file = Path.GetFileName(_yourPath);
					string path = word + "\\" + file;
                    _label = word;
					string newpath = VCDir.Instance.TrainDirAudio + path;
					if (File.Exists(_yourPath))
						File.Move(_yourPath, newpath);
					trainFrom.AddRow(word, path, startPnt, endPnt);
				}
			}
        }
        private void UpdateRow(TrainFilesCarrier.TrainFileRow row) {
            string file = VCDir.Instance.TrainDirAudio + row.Path;
            if (File.Exists(file))
            {
                _yourChanged = true;
                _yourPath = file;
                _yourWav = new WavFileWrapper(_yourPath);

                 if (_yourWav.Load())
                 {
                     _yourWav.NormalizeWave(1.0);
                     LogUtil.Info("Load Wave: {0}   -- OK\n", _yourPath);
                     option = VCContext.Instance.MFCCOptions;
                     if (option.ShiftSampleToZero)
                     {
                         LogUtil.Info("Shift Sample To Zero: --   -- OK\n");
                         _yourWav.ShifToZero();
                     }
                     waveViewer.WaveData = _yourWav.FullData;
                     waveViewer.FitToScreen();
                     _label = row.Word;
                     int size = _yourWav.FullData.Count;
                     _startSelected = (float)row.Start / size;
                     _endSelected = (float)row.End / size;
                     waveViewer.LeftSlider = _startSelected;
                     waveViewer.RightSlider = _endSelected;
                 }
            }
        }
        private DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
        private void train_btn_Click(object sender, EventArgs e)
        {
            train_btn.Enabled = false;
			BackgroundWorker bgw = new BackgroundWorker();
			bgw.DoWork += bgwTrain_DoWork;
			bgw.RunWorkerCompleted += bgwTrain_RunWorkerCompleted;
			bgw.RunWorkerAsync();
        }
        private void bgwTrain_DoWork(object sender, DoWorkEventArgs e)
        {
            trainFrom.Train();
        }
        private void bgwTrain_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            train_btn.Enabled = true;
		}

        private void regMode_cbx_CheckedChanged(object sender, EventArgs e)
        {
            _regMode = regMode_cbx.Checked;
        }

        private void reg_btn_Click(object sender, EventArgs e)
        {
            reg_btn.Enabled = false;
			ProcessFile(true);
            //_trainTask.Test();
            //_trainTask.Reg(null);
        }

		private void ProcessFile(bool reg) {
			BackgroundWorker bgw = new BackgroundWorker();
			bgw.DoWork += bgw_DoWork;
			bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
			bgw.RunWorkerAsync(reg);
		}

        private void update_btn_Click(object sender, EventArgs e)
        {
            if (_yourWav != null && _recalled)
            {
                int size = _yourWav.FullData.Count;
                int startPnt = (int)(_startSelected * size);
                int endPnt = (int)(_endSelected * size);

                //string traingDir = VCDir.Instance.TrainDirAudio + _label;
                //VCDir.CreateDirectory(traingDir);
                string file = Path.GetFileName(_yourPath);
                string path = _label + "\\" + file;
                //string newpath = VCDir.Instance.TrainDirAudio + path;
                //if (File.Exists(_yourPath))
                //    File.Move(_yourPath, newpath);
                trainFrom.AddRow(_label, path, startPnt, endPnt);
            }
        }
    }
}

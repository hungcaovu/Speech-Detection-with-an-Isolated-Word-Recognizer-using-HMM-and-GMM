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
    public partial class MainControl : UserControl
    {
        private MfccOptions option;
        /// <summary>
        ///  Recodring
        /// </summary>
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
        private double peakRef = 0.0f;
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
        public MainControl()
        {
            option = VCContext.Instance.MFCCOptions;
            //ExtractionWrapper.OptionWrapper.SetLog(option.EnableLog);
            recoding = false;
            InitializeComponent();
            waveViewer.TimeSelectedChanged += SelectedTimeEventHandler;
            _waveOut = new NAudio.Wave.WaveOut();
            _selectedChart = showChart.Selected;
            initSampleRate_cbx();
            //initListWords();
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
            waveViewer.File = _yourPath;
            waveViewer.FitToScreen();
            vadVoice(_yourPath);
        }
        private void vadVoice(string path)
        {
            WavFileWrapper wav = new WavFileWrapper(path);
            if (wav.Load())
            {
                VadWrapper vad = new VadWrapper(wav);
                vad.UseEnergy(0.015f, 0.01f, true, 3, false);
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
                        int size = wav.FullData.Count;
                        _startSelected = begin / size;
                        _endSelected = end / size;
                        waveViewer.LeftSlider = _startSelected;
                        waveViewer.RightSlider = _endSelected;
                        waveViewer.ThresholdChart = (float)vad.ThresholdEnergy;
                        waveViewer.Chart = vad.SmoothEnergies;
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
        private void listVoice_lbx_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        private void PlayStopref_btn_Click(object sender, EventArgs e)
        {
            _isPlayRef = !_isPlayRef;
            if (_isPlayRef)
            {
                Debug.WriteLine("Play Audio Ref");
                _playRef = new PlayWave(refPath);
                _playRef.RaisePlayStop += RefPlayStop;
                playStopref_btn.Image = global::Voice_Comparasion.Properties.Resources.Stop;
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
            playStopref_btn.Image = global::Voice_Comparasion.Properties.Resources.Play;
        }
        private void PlayStopyour_btn_Click(object sender, EventArgs e)
        {
            _isPlayYour = !_isPlayYour;
            if (_isPlayYour)
            {
                Debug.WriteLine("Play Audio Ref");
                _playYour = new PlayWave(_yourPath);
                _playYour.RaisePlayStop += YourPlayStop;
                playStopyour_btn.Image = global::Voice_Comparasion.Properties.Resources.Stop;
                _playYour.Play();
            }
            else
            {
                Debug.WriteLine("Stop Audio Ref");
                _playYour.Stop();
                _playYour = null;
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
            //ExtractionWrapper.OptionWrapper.SetLog(option.EnableLog);
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
            ShowChart(FormTag.REF_WAVE, _selectedChart.RefWave);
            ShowChart(FormTag.REF_MFCC, _selectedChart.RefMfcc);
            ShowChart(FormTag.REF_FREQ, _selectedChart.RefFreq);
            ShowChart(FormTag.REF_DOUBLE, _selectedChart.RefDouble);
            ShowChart(FormTag.REF_DETAL, _selectedChart.RefDetal);
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
            bgw.RunWorkerAsync();
        }
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            bool refpeak = false;
            if (_refChanged)
            {
                _refWav = new WavFileWrapper(refPath);

                if (_refWav.Load())
                {
                    _refMfcc = new MFCCWrapper(_refWav, option.TimeFrame, option.TimeShift, option.CepFilter, option.LowFreq, option.HighFreq, option.NumCeps, 4);
                    if (option.NormalizeAudio)
                    {
                        refpeak = option.NormalizeAudio;
                        peakRef = _refWav.Peak();
                    }

                    _refMfcc.Process();

                    _refPitch = new PitchWrapper(_refWav, option.PitchTimeFrame, option.PitchTimeShift, option.PitchLowFreq, option.PitchHighFreq, option.PitchType, option.DropUnPitch);
                    if (option.UseMedian)
                    {
                        _refPitch.SetMedianWindowSize(option.MedianWindow);
                    }
                    _refPitch.Process();
                    /// Debug.WriteLine("Process Ref file completed Time {0}", DateTime.Now);
                }
            }
            if (_refMfcc != null && _refMfcc.ProcessDone)
            {
                SetDataChart(FormTag.REF_WAVE);
                Debug.WriteLine("Process Ref: Draw Wave Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_FREQ);
                Debug.WriteLine("Process Ref: Draw Freq Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_MFCC);
                Debug.WriteLine("Process Ref: Draw MFCC Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_DOUBLE);
                Debug.WriteLine("Process Ref: Draw Double Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_DETAL);
                Debug.WriteLine("Process Ref: Draw Detal Done {0}", DateTime.Now);
                SetDataChart(FormTag.REF_PITCH);
                Debug.WriteLine("Process Ref: Draw Pitch Done {0}", DateTime.Now);
            }

            Debug.WriteLine("Set data chart Ref Ref Done {0}", DateTime.Now);


            if (_yourChanged)
            {
                _yourWav = new WavFileWrapper(_yourPath);

                if (_yourWav.Load())
                {
                    int size = _yourWav.FullData.Count;
                    uint startPnt = (uint)(_startSelected * size);
                    uint endPnt = (uint)(_endSelected * size);
                    Debug.WriteLine("Select Data voice: Start {0} End {1}", startPnt, endPnt);
                    _yourWav.SelectedWave(startPnt, endPnt);

                    if (refpeak)
                    {
                        _yourWav.NormalizeWave(peakRef);
                    }

                    _yourMfcc = new MFCCWrapper(_yourWav, option.TimeFrame, option.TimeShift, option.CepFilter, option.LowFreq, option.HighFreq, option.NumCeps, 4);
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

            if (_yourMfcc != null && _refMfcc != null && _yourMfcc.ProcessDone && _refMfcc.ProcessDone)
            {
				//// TO DO: Process Bar
				//string log = string.Format("\nCompare : Your Path - {0}\n          Ref Path - {1}\n", _yourPath, refPath);
                
				//// TO DO: Process Bar
				//log += "Distance of 2 Vec Pitch: ";
				//float resPitch = DTWUtilWrapper.DistanceOf2Vector(_yourPitch.Pitchs, _refPitch.Pitchs, true);
				//log += string.Format("{0:0.###}", resPitch);

				//// TO DO: Process Bar
				//log += "Compute DistanceOf2Array3DHorizon:";
				//List<float> res = DTWUtilWrapper.DistanceOf2Array3DHorizon(_yourMfcc.DetalMfcc, _refMfcc.DetalMfcc, true);
				//_mfccViewer.MFCC = res;


				//log += "Distance of 2 Vec Delta MFCC: ";
				//float resMFCC = DTWUtilWrapper.DistanceOf2Array3D(_yourMfcc.Mfcc, _refMfcc.Mfcc, true);
				
				//log += string.Format("{0:0.###}", resMFCC);

				////float sum = 0.0f;
				////for (int i = 0; i < res.Count; i++)
				////{
				////    log += string.Format("  {0}: {1}", i, res[i]);
				////    sum += res[i];
				////}
				////ShowValueCostMFCC(string.Format("{0:0.###}", res / res.Count));
				//LogUtil.Info(log);

				////ShowValueCostPitch(string.Format("{0:0.###}", resPitch));

				// TO DO: Process Bar
				string log = string.Format("**********************************************************\nCompare : Your Path - {0}\n          Ref Path - {1}\n", _yourPath, refPath);
				log += "Distance of 2 Vec Delta MFCC: ";
				//float resMFCC = DTWUtilWrapper.DistanceOf2Array3D(yourmfcc.Mfcc, refmfcc.Mfcc, true);
                double dis = DTWUtilWrapper.DistanceOf2Vector(_yourMfcc.Mfcc, _refMfcc.Mfcc, false);
                double fac = _yourMfcc.Mfcc.Count > _refMfcc.Mfcc.Count ? _refMfcc.Mfcc.Count : _yourMfcc.Mfcc.Count;
                double cos = dis / fac;
				//log += string.Format("Dis MFCC: {0:0.###} Score MFCC: {1:0.###}\n", 10 *dis / fac, -4.51297 * Math.Log10(-0.083653 * dis / fac + 1));
				log += string.Format("Dis MFCC: {0:0.###} Cos  {1:0.###} \n", cos, ScoreMath.Score(cos));
				// TO DO: Process Bar

				log += "Distance of 2 Vec Pitch: ";

                double resPitch = DTWUtilWrapper.DistanceOf2Vector(_yourPitch.Pitchs, _refPitch.Pitchs, true);
                double facD = _refPitch.Pitchs.Count > _yourPitch.Pitchs.Count ? _yourPitch.Pitchs.Count : _refPitch.Pitchs.Count;
                double cosp = resPitch / facD;
				log += string.Format("{0:0.###}  \n", cosp);


				// log += string.Format(" Score {0:0.###} {1:0.###} {2:0.###}  \n", );
				// TO DO: Process Bar
				//log += "Distance of Component MFCC:\n";
                List<double> res = DTWUtilWrapper.DistanceOf2Array3DHorizon(_yourMfcc.Mfcc, _refMfcc.Mfcc, false);
				_mfccViewer.MFCC = res;
				//for (int i = 0; i < res.Count; i++) {
				//	log += string.Format("  {0}: {1}\n", i, res[i]);
				//}

				LogUtil.Info(log);
				ShowValueCostMFCC(string.Format("{0:0.###}", ScoreMath.Score(cos)));

				ShowValueCostPitch(string.Format("{0:0.###}", resPitch));
            }
        }
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("Complete Procesing: At - {0}", DateTime.Now);

            ShowChart();
            _refChanged = false;
            _yourChanged = false;
            process_btn.Enabled = true;

            if (option.SeparateLog && option.EnableLog && (_refChanged || _yourChanged))
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
        private void autoSelect_btn_Click(object sender, EventArgs e)
        {
            vadVoice(_yourPath);
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
    }
}

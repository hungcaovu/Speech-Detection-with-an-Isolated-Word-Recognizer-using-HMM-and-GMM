using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Gui;
using NAudio.Wave;
using System.IO;
using System.Threading;
using ExtractionWrapper;
using Object;
using UC.Enum;

namespace Voice_Comparasion
{
    public partial class TestControl : UserControl
    {

        string yourNameFile = "";
        string refNameFile = "";
        // MFCC Config
        //float timeframe = 0.050f;
        //float timeshift = 0.01f;
        //int n_filters = 22;
        //float flo = 0.0f;
        //float fhi = 4000f;
        //int nceptrums = 12;

        MfccOptions mfcc_setting = null;
        Point[] locationChart;

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

        MFCCWrapper yourmfcc = null;
        MFCCWrapper refmfcc = null;

        bool changed_yourfile = false;
        bool changed_reffile = false;

        SettingForm setting_frm = null;
        public TestControl()
        {
            locationChart = new Point[8];

            locationChart[0] = new Point(Screen.PrimaryScreen.WorkingArea.X + 10, Screen.PrimaryScreen.WorkingArea.Y + 10);
            // yourvoice_wave = new WaveViewerForm();
            refvoice_wave = new WaveViewerForm();

            yourvoice_freq = new MfccChartForm();
            refvoice_freq = new MfccChartForm();
            locationChart[1] = new Point(locationChart[0].X + yourvoice_freq.Width + 10, locationChart[0].Y);

            locationChart[2] = new Point(locationChart[0].X, locationChart[0].Y + yourvoice_freq.Height + 10);
            locationChart[3] = new Point(locationChart[0].X + yourvoice_freq.Width + 10, locationChart[0].Y + yourvoice_freq.Height + 10);

            yourvoice_mfcc = new MfccChartForm();
            refvoice_mfcc = new MfccChartForm();

            locationChart[4] = new Point(locationChart[0].X, locationChart[0].Y + 2 * yourvoice_freq.Height + 20);
            locationChart[5] = new Point(locationChart[0].X + yourvoice_freq.Width + 10, locationChart[0].Y + 2 * yourvoice_freq.Height + 20);

            yourvoice_detal = new MfccChartForm();
            refvoice_detal = new MfccChartForm();
            locationChart[6] = new Point(locationChart[0].X, locationChart[0].Y + 2 * yourvoice_freq.Height + 20);
            locationChart[7] = new Point(locationChart[0].X + yourvoice_freq.Width + 10, locationChart[0].Y + 2 * yourvoice_freq.Height + 20);
            yourvoice_double = new MfccChartForm();
            refvoice_double = new MfccChartForm();

            InitializeComponent();
            selectShowChart.ShowChartYourWave += DisplayYourWaveChart;
            selectShowChart.ShowChartYourFreq += DisplayYourFreqChart;
            selectShowChart.ShowChartYourMfcc += DisplayYourMfccChart;
            selectShowChart.ShowChartYourDetal += DisplayYourDetalChart;
            selectShowChart.ShowChartYourDouble += DisplayYourDoubleChart;

            selectShowChart.ShowChartRefWave += DisplayRefWaveChart;
            selectShowChart.ShowChartRefFreq += DisplayRefFreqChart;
            selectShowChart.ShowChartRefMfcc += DisplayRefMfccChart;
            selectShowChart.ShowChartRefDetal += DisplayRefDetalChart;
            selectShowChart.ShowChartRefDouble += DisplayRefDoubleChart;
            mfcc_setting = new MfccOptions();

        }
        private void yourvoice_btn_Click(object sender, EventArgs e)
        {
            if (browser_file.ShowDialog() == DialogResult.OK)
            {
                if (yourNameFile != browser_file.FileName)
                {
                    changed_yourfile = true;
                    yourNameFile = browser_file.FileName;
                    this.yourvoice_tb.Text = yourNameFile;
                    Thread run = new Thread(new ThreadStart(this.ProcessYourVoice));
                    run.Start();
                }
            }
        }
        private void refvoice_btn_Click(object sender, EventArgs e)
        {
            if (browser_file.ShowDialog() == DialogResult.OK)
            {
                if (refNameFile != browser_file.FileName)
                {
                    changed_reffile = true;
                    refNameFile = browser_file.FileName;
                    this.refvoice_tb.Text = refNameFile;
                    Thread run = new Thread(new ThreadStart(this.ProcessRefVoice));
                    run.Start();
                }
                else
                {
                    changed_reffile = false;
                }

            }
        }
        private void ProcessYourVoice()
        {
            if (changed_yourfile)
            {
                //yourmfcc;// ;//= new MFCCWrapper(mfcc_setting.TimeFrame, mfcc_setting.TimeShift, mfcc_setting.CepFilter, mfcc_setting.LowFreq, mfcc_setting.HighFreq, mfcc_setting.NumCeps);
                if (yourmfcc.Load(yourNameFile, 0) == (int) State.SUCCESSED)
                {
                    yourmfcc.Process();
                }
            }

            DisplayYourCharts();
            changed_yourfile = false;
        }
        private void SetDataYourWaveChart()
        {
            if (yourvoice_wave == null || yourvoice_wave.IsDisposed)
            {
                yourvoice_wave = new WaveViewerForm();
                yourvoice_wave.Hide();
                yourvoice_wave.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsValid == (int)State.SUCCESSED)
                { 
                    yourvoice_wave.Data = yourmfcc.Data;
                }
                yourvoice_wave.Text = "Wave Chart";
            }

            if (changed_yourfile)
            {
                yourvoice_wave.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsValid == (int)State.SUCCESSED)
                {
                    yourvoice_wave.Data = yourmfcc.Data;
                }
                yourvoice_wave.Text = "Wave Chart";
            }
        }
        private void SetDataYourFreqChart()
        {

            if (yourvoice_freq.IsDisposed)
            {
                yourvoice_freq = new MfccChartForm();
                yourvoice_freq.Hide();
                yourvoice_freq.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_freq.Data = yourmfcc.FreqFrame;
                }
                yourvoice_freq.Text = "Freq Chart";

            }
            if (changed_yourfile)
            {
                yourvoice_freq.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_freq.Data = yourmfcc.FreqFrame;
                }
                yourvoice_freq.Text = "Freq Chart";
            }
        }
        private void SetDataYourMfccChart()
        {
            if (yourvoice_mfcc.IsDisposed)
            {
                yourvoice_mfcc = new MfccChartForm();
                yourvoice_mfcc.Hide();
                yourvoice_mfcc.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourvoice_mfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_mfcc.Data = yourmfcc.MfccFrame;
                }
                yourvoice_mfcc.Text = "MFCC Chart";
            }
            if (changed_yourfile)
            {
                yourvoice_mfcc.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_mfcc.Data = yourmfcc.MfccFrame;
                }
                yourvoice_mfcc.Text = "MFCC Chart";
            }
        }
        private void SetDataYourDetalChart()
        {
            if (yourvoice_detal.IsDisposed)
            {
                yourvoice_detal = new MfccChartForm();
                yourvoice_detal.Hide();
                yourvoice_detal.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_detal.Data = yourmfcc.DetalMfccFrame;
                }
                yourvoice_detal.Text = "Detal MFCC Chart";

            }
            if (changed_yourfile)
            {
                yourvoice_detal.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_detal.Data = yourmfcc.DetalMfccFrame;
                }
                yourvoice_detal.Text = "Detal MFCC Chart";
            }
        }
        private void SetDataYourDoubleChart()
        {
            if (yourvoice_double.IsDisposed)
            {
                yourvoice_double = new MfccChartForm();
                yourvoice_double.Hide();
                yourvoice_double.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_double.Data = yourmfcc.DoubleDetalMfccFrame;
                }
                yourvoice_double.Text = "Detal MFCC Chart";

            }
            if (changed_yourfile)
            {
                yourvoice_double.Title = "Your File: " + Path.GetFileName(yourNameFile);
                if (yourmfcc != null && yourmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    yourvoice_double.Data = yourmfcc.DoubleDetalMfccFrame;
                }
                yourvoice_double.Text = "Detal MFCC Chart";
            }
        }

        public void DisplayYourWaveChart()
        {
            SetDataYourWaveChart();

            if (selectShowChart.Selected.YourWave)
            {
                if (!yourvoice_wave.Visible && yourmfcc != null)
                {
                    yourvoice_wave.Location = locationChart[0];
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
        }
        public void DisplayYourFreqChart()
        {
            SetDataYourFreqChart();

            if (selectShowChart.Selected.YourFreq)
            {
                if (!yourvoice_freq.Visible && yourmfcc != null)
                {
                    yourvoice_freq.Location = locationChart[0];
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
        }
        public void DisplayYourMfccChart()
        {
            SetDataYourMfccChart();

            if (selectShowChart.Selected.YourMfcc)
            {
                if (!yourvoice_mfcc.Visible && yourmfcc != null)
                {
                    yourvoice_mfcc.Location = locationChart[2];
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
        }
        public void DisplayYourDetalChart()
        {
            SetDataYourDetalChart();

            if (selectShowChart.Selected.YourDetal && yourmfcc != null)
            {
                if (!yourvoice_detal.Visible && yourmfcc != null)
                {
                    yourvoice_detal.Location = locationChart[4];
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
        }
        public void DisplayYourDoubleChart()
        {
            SetDataYourDoubleChart();

            if (selectShowChart.Selected.YourDouble && yourmfcc != null)
            {
                if (!yourvoice_double.Visible && yourmfcc != null)
                {
                    yourvoice_double.Location = locationChart[4];
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
        }
        private void ProcessRefVoice()
        {
            if (changed_reffile)
            {
                //refmfcc;// = new MFCCWrapper(mfcc_setting.TimeFrame, mfcc_setting.TimeShift, mfcc_setting.CepFilter, mfcc_setting.LowFreq, mfcc_setting.HighFreq, mfcc_setting.NumCeps);
                if (refmfcc.Load(refNameFile, 0) == (int)State.SUCCESSED)
                {
                    refmfcc.Process();
                }
            }
            DisplayRefCharts();
            changed_reffile = false;
        }

        private void SetDataRefWaveChart()
        {
            if (refvoice_wave.IsDisposed)
            {
                refvoice_wave = new WaveViewerForm();
                refvoice_wave.Title = "Your File: " + Path.GetFileName(yourNameFile);

                if (refmfcc != null && refmfcc.IsValid == (int)State.SUCCESSED)
                {
                    refvoice_wave.Data = refmfcc.Data;
                }
                refvoice_wave.Text = "Wave Chart";
            }
            if (changed_reffile)
            {
                if (refvoice_wave != null && refmfcc.IsValid == (int)State.SUCCESSED)
                {
                    refvoice_wave.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    refvoice_wave.Data = refmfcc.Data;
                }
                refvoice_wave.Text = "Wave Chart";
            }
        }
        private void SetDataRefFreqChart()
        {

            if (refvoice_freq.IsDisposed)
            {
                refvoice_freq = new MfccChartForm();
                refvoice_freq.Title = "Your File: " + Path.GetFileName(yourNameFile);

                if (refmfcc != null && refmfcc.ProcessDone)
                {
                    //refvoice_freq.Data = refmfcc.FreqFrame;
                }
                refvoice_freq.Text = "Freq Chart";

            }
            if (changed_reffile)
            {
                if (refvoice_freq != null && refmfcc.ProcessDone)
                {
                    refvoice_freq.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    //refvoice_freq.Data = refmfcc.FreqFrame;
                }
                refvoice_freq.Text = "Freq Chart";
            }
        }
        private void SetDataRefMfccChart()
        {

            if (refvoice_mfcc.IsDisposed)
            {
                refvoice_mfcc = new MfccChartForm();
                if (refmfcc != null)
                {
                    refvoice_mfcc.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    //refvoice_mfcc.Data = refmfcc.MfccFrame;
                }
                refvoice_mfcc.Text = "MFCC Chart";
            }
            if (changed_reffile)
            {
                if (refvoice_mfcc != null && refmfcc.ProcessDone)
                {
                    refvoice_mfcc.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    //refvoice_mfcc.Data = refmfcc.MfccFrame;
                }
                refvoice_mfcc.Text = "MFCC Chart";
            }
        }
        private void SetDataRefDetalChart()
        {
            if (refvoice_detal.IsDisposed)
            {
                refvoice_detal = new MfccChartForm();
                if (refmfcc != null && refmfcc.IsProcessed == (int)State.SUCCESSED)
                {
                    refvoice_detal.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    refvoice_detal.Data = refmfcc.DetalMfccFrame;
                }
                refvoice_detal.Text = "Detal MFCC Chart";
            }
            if (changed_reffile)
            {
                if (refvoice_freq != null && refmfcc.ProcessDone)
                {
                    refvoice_detal.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    //refvoice_detal.Data = refmfcc.DetalMfccFrame;
                }
                refvoice_detal.Text = "Detal MFCC Chart";
            }
        }
        private void SetDataRefDoubleChart()
        {
            if (refvoice_double.IsDisposed)
            {
                refvoice_double = new MfccChartForm();
                if (refmfcc != null && refmfcc.ProcessDone)
                {
                    refvoice_double.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    //refvoice_double.Data = refmfcc.DoubleDetalMfccFrame;
                }
                refvoice_double.Text = "Detal MFCC Chart";
            }

            if (changed_reffile)
            {
                if (refvoice_double != null && refmfcc.ProcessDone)
                {
                    refvoice_double.Title = "Ref File: " + Path.GetFileName(refNameFile);
                    //refvoice_double.Data = refmfcc.DoubleDetalMfccFrame;
                }
                refvoice_detal.Text = "Detal MFCC Chart";
            }
        }

        public void DisplayRefWaveChart()
        {
            if (this.InvokeRequired)
            {
                Action handle = new Action(DisplayRefWaveChart);
                this.Invoke(handle);
            }
            else
            {
                SetDataRefWaveChart();

                if (selectShowChart.Selected.RefWave)
                {
                    if (!refvoice_wave.Visible && refmfcc != null)
                    {
                        refvoice_wave.Location = locationChart[1];
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
            }
        }
        public void DisplayRefFreqChart()
        {
            SetDataRefFreqChart();

            if (selectShowChart.Selected.RefFreq)
            {
                if (!refvoice_freq.Visible && refmfcc != null)
                {
                    refvoice_freq.Location = locationChart[1];
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
        }
        public void DisplayRefMfccChart()
        {
            SetDataRefMfccChart();

            if (selectShowChart.Selected.RefMfcc)
            {
                if (!refvoice_mfcc.Visible && refmfcc != null)
                {
                    refvoice_mfcc.Location = locationChart[3];
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
        }
        public void DisplayRefDetalChart()
        {
            SetDataRefDetalChart();

            if (selectShowChart.Selected.RefDetal && refmfcc != null)
            {
                if (!refvoice_detal.Visible)
                {
                    refvoice_detal.Location = locationChart[5];
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
        }
        public void DisplayRefDoubleChart()
        {
            SetDataRefDoubleChart();

            if (selectShowChart.Selected.RefDouble && refmfcc != null)
            {
                if (!refvoice_double.Visible)
                {
                    refvoice_double.Location = locationChart[5];
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
        }
        private void DisplayYourCharts()
        {
            //if (this.InvokeRequired)
            //{
            //	Action handle = new Action(DisplayYourCharts);
            //	this.Invoke(handle);
            //}
            //else
            {
                DisplayYourWaveChart();
                DisplayYourFreqChart();
                DisplayYourMfccChart();
                DisplayYourDetalChart();
                DisplayYourDoubleChart();
            }
        }
        private void DisplayRefCharts()
        {
            //if (this.InvokeRequired) {
            //	Action handle = new Action(DisplayRefCharts);
            //	this.Invoke(handle);
            //} else 
            {
                DisplayRefWaveChart();
                DisplayRefFreqChart();
                DisplayRefMfccChart();
                DisplayRefDetalChart();
                DisplayRefDoubleChart();
            }
        }
        private void setting_btn_Click(object sender, EventArgs e)
        {
            if (setting_frm == null || setting_frm.IsDisposed)
            {
                setting_frm = new SettingForm();
                setting_frm.SettingChanged += SettingChanged;
                setting_frm.Options = mfcc_setting;
                setting_frm.Show();
            }
            else if (setting_frm.Visible)
            {
                setting_frm.Options = mfcc_setting;
                setting_frm.Show();
            }
            else
            {
                setting_frm.Options = mfcc_setting;
                setting_frm.Show();
            }
        }
        private void SettingChanged()
        {
            changed_yourfile = true;
            changed_reffile = true;
            ProcessYourVoice();
            ProcessRefVoice();
        }

        private void process_btn_Click(object sender, EventArgs e)
        {

        }
    }
}

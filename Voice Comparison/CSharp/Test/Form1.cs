using ExtractionWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Voice_Comparasion;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            OptionWrapper.SetLog(true);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // WavFileWrapper wav = new WavFileWrapper(@"C:\Users\hungc\Desktop\Project\Binary\Voice Comparasion\Debug\Data\Test\sin100.wav"/*"C:\\Users\\hungc\\Desktop\\Project\\Binary\\TestExtractionLib\\Debug\\what_movies_have_you_seen_recently.wav"*/);
            WavFileWrapper wav = new WavFileWrapper(@"C:\Users\hungc\Desktop\Project\Binary\TestExtraction\Debug\what_movies_have_you_seen_recently.wav");
            bool result = wav.Load();
            wav.SelectedWave(11999, 12512);
            MFCCWrapper mfcc = new MFCCWrapper(wav, 512u, 0u, 20u, 0.0f, 8000.0f, 12u, 2);
            mfcc.Process();
            WaveViewerForm wavView = new WaveViewerForm();
            wavView.Data = wav.SelectedData;
            wavView.Show();
            LineChartForm chart = new LineChartForm(Object.Enum.FormTag.NONE);
            chart.MaxValue = (int)mfcc.Mfcc[0].Max() + 1;
            chart.MinValue = (int)mfcc.Mfcc[0].Min() - 1;
            chart.Data = mfcc.Mfcc[0];
            chart.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OptionWrapper.SetLog(false);
            WavFileWrapper wav = new WavFileWrapper(@"C:\Users\hungc\Desktop\Project\Binary\Voice Comparasion\Debug\Data\Test\sin100.wav");
            bool result = wav.Load();
            wav.SelectedWave(0, 20000);
            MFCCWrapper mfcc = new MFCCWrapper(wav, 0.015f, 0.005f, 18, 0.0f, 4000, 12, 2);
            mfcc.Process();

            MfccChartForm chart = new MfccChartForm(Object.Enum.FormTag.NONE);
            chart.Text = "MFCC";
            chart.Data = mfcc.Mfcc;
            chart.Show();
            /*
            MfccChartForm chart2 = new MfccChartForm(Object.Enum.FormTag.NONE);
            chart2.Text = "Bank Log";
            chart2.Data = mfcc.BandFilter;
            chart2.Show();

            MfccChartForm chart3 = new MfccChartForm(Object.Enum.FormTag.NONE);
            chart3.Text = "FREQ";
            chart3.Data = mfcc.Freq;
            chart3.Show();

            MfccChartForm chart4 = new MfccChartForm(Object.Enum.FormTag.NONE);
            chart4.Text = "Delta";
            chart4.Data = mfcc.DetalMfcc;
            chart4.Show();

            MfccChartForm chart5 = new MfccChartForm(Object.Enum.FormTag.NONE);
            chart5.Text = "Double";
            chart5.Data = mfcc.DoubleDetalMfcc;
            chart5.Show();*/
        }
    }
}

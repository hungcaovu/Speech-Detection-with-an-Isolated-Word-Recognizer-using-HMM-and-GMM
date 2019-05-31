using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using System.Diagnostics;
using System.IO;
using ExtractionWrapper;
using System.Threading;
using System.Configuration;
using Object;
using UC.Enum;
namespace UC
{
	public partial class Recoder : UserControl
	{
		bool recoding;
		List<NAudio.Wave.WaveInCapabilities> sources = null;
		NAudio.Wave.WaveIn sourceStream = null;
		NAudio.Wave.WaveOut waveOut = null;
		NAudio.Wave.WaveFileWriter waveWriter = null;
		NAudio.Wave.WaveOffsetStream playStream = null;
        string pathFile;
  
		public Recoder() {
			Random rand = new Random();
			recoding = false;
			InitializeComponent();
			spectrumCurr.Title = "Your Voice :";
			spectrumRefer.Title = "Referrence :";
			//waveViewer.SelectedTimeEvent += SelectedTimeEventHandler;
			waveOut = new NAudio.Wave.WaveOut();
			initSampleRate_cbx();
			FreshListDevices();
		}

		private void initSampleRate_cbx() {
			sampleRate_cbx.Items.Clear();
			for (int i = 0; i < Constant.TextSampleRate.Count<string>(); i++) {
				sampleRate_cbx.Items.Add(Constant.TextSampleRate[i]);
			}
			sampleRate_cbx.SelectedIndex = 1;
		}

		private void FreshListDevices() {
			sources = new List<NAudio.Wave.WaveInCapabilities>();
			for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++) {
				sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
			}
			foreach (var source in sources) {
				devices_cbx.Items.Add(source.ProductName);
			}
			if (sources.Count != 0) {
				devices_cbx.SelectedIndex = 0;
			}
		}


		private void startRecordSound() {
			int deviceNumber = devices_cbx.SelectedIndex;
			int sampleRate = devices_cbx.SelectedIndex;

			if (sampleRate >= 0 && deviceNumber >= 0 && sampleRate < Constant.TextSampleRate.Count<string>()) {
				sourceStream = new NAudio.Wave.WaveIn();
				sourceStream.DeviceNumber = deviceNumber;
				sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(Constant.SampleRate[sampleRate], NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);
				//NAudio.Wave.WaveInProvider waveIn = null;
				//waveIn = new NAudio.Wave.WaveInProvider(sourceStream);

				pathFile = VCDir.Instance.PathWaveFile;
				sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
				waveWriter = new NAudio.Wave.WaveFileWriter(pathFile, sourceStream.WaveFormat);

				sourceStream.StartRecording();
				// waveOut.Play();
			} else {
				MessageBox.Show("");
				return;
			}
		}

		private void stopRecordSound() {

			if (sourceStream != null) {
				sourceStream.StopRecording();
				sourceStream.Dispose();
				sourceStream = null;
			}

			if (waveWriter != null) {
				waveWriter.Dispose();
				waveWriter = null;
			}
		}


		private void showWaveSound() {

			waveViewer.WaveStream = new NAudio.Wave.WaveFileReader(pathFile);

			if (playStream != null) {
				playStream.Dispose();
				playStream = null;
			}

			playStream = new WaveOffsetStream(new NAudio.Wave.WaveFileReader(pathFile));
			waveViewer.FitToScreen();

		}
		private void recoder_btn_Click(object sender, EventArgs e) {
			recoding = !recoding;
			if (recoding) {
				recoder_btn.Image = UC.Properties.Resources.Recording;
				startRecordSound();
			} else {
				recoder_btn.Image = UC.Properties.Resources.Pause;
				stopRecordSound();
				showWaveSound();
			}

		}

		private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e) {
			if (waveWriter == null) return;

			waveWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
			waveWriter.Flush();
		}


        //private void SelectedTimeEventHandler(object sender, TimeSelectedEventArgs e) {

        //    WaveViewer view = sender as WaveViewer;
        //    WaveStream wavStr = view.WaveStream;

        //    if (view != null && e != null && playStream != null && !processing) {
        //        try {
        //            //wavStr.Position = e.StartPosition;
        //            playStream.Seek(0, SeekOrigin.Begin);
        //            playStream.StartTime = e.StartTime;
        //            playStream.SourceOffset = e.StartTime;
        //            playStream.SourceLength = e.LengthTime;

        //            Thread demoThread = new Thread(new ThreadStart(this.ProcessWave));
        //            demoThread.Start();

        //            //playStream.Seek(0, SeekOrigin.Begin);
        //            //playStream.StartTime = e.StartTime;
        //            //playStream.SourceOffset = e.StartTime;
        //            //playStream.SourceLength = e.LengthTime;
        //            waveOut.Init(playStream);
        //            waveOut.Play();

        //            //waveOut.PlaybackStopped += waveOut_PlaybackStopped;
        //        } catch (Exception) {
        //        }
        //    }
        //}

		private void waveOut_PlaybackStopped(object sender, EventArgs e) {

		}
		private void Recoder_SizeChanged(object sender, EventArgs e) {
			waveViewer.FitToScreen();
		}

		private void StoreWave(string path, NAudio.Wave.WaveOffsetStream stream) {
			using (WaveFileWriter writer = new WaveFileWriter(path, stream.WaveFormat)) {
				byte[] buffer = new byte[stream.Length];
				while (stream.Position < stream.Length) {
					int bytesRead = stream.Read(buffer, 0, buffer.Length);
					if (bytesRead == 0)
						break;
					writer.WriteData(buffer, 0, bytesRead);
					writer.Flush();
				}
			}
		}

        private void ProcessWave() {

            //processing = true;
            StoreWave("Tmp.wav", playStream);
            //MFCCWrapper a = new MFCCWrapper(timeframe, timeshift, n_filters, flo, fhi, nceptrums);
            ////if (a.Load(yourNameFile, 0) == (int)State.SUCCESSED)
            //{
            //    a.Process();
            //    spectrumCurr.Data = a.FreqFrame;
            //}
            //processing = false;
        }


        private void GetMFCCConfig() { 

        }
	}
}

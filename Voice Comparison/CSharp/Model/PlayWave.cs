using NAudio.Wave;
using Object.Event;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Model
{
    public class PlayWave : WaveFileReader
    {
        string path= "";
        bool stop = false;
        float start = 0;
        float end = 0;

        long startPos = 0;
        long endPos = 0;
        private PlayWaveStopEventHandler handle = null;
        public event PlayWaveStopEventHandler RaisePlayStop {
            add {
                handle += value;
            }
            remove {
                handle -= value;
            }
        }
        public PlayWave(string path)
            : base (path)
        {
            this.path = path;
        }

        public override long Position { 
            get { return base.Position; }
            set { base.Position = value; }
        }
      
        public float StartPosition {
            set {
                start = value;
                long length = (this.Length / (this.WaveFormat.BitsPerSample / 8)) / this.WaveFormat.Channels;
                startPos = (long)(start * (float)length) * this.WaveFormat.BitsPerSample / 8;
            }
        }

        public float EndPosition
        {
            set
            {
                end = value;
                long length = (base.Length / (base.WaveFormat.BitsPerSample / 8)) / base.WaveFormat.Channels;
                endPos = (long)(end * (float)length) * base.WaveFormat.BitsPerSample / 8;
                if (startPos > endPos || endPos <= 0)
                {
                    endPos = base.Length;
                }
            }
        }

        private void PlayW() {
            if (path.Length == 0 || !File.Exists(path))
            {
                if (handle != null)
                {
                    handle(this, new PlayWaveStopEventArgs());
                }
                return;
            }
            try
            {
                if (startPos >= 0 && startPos < base.Length)
                {
                    Position = startPos;
                }
                using (var wc = new WaveChannel32(this) { PadWithZeroes = true})
                {
                    using (var audioOutput = new DirectSoundOut())
                    {
                        audioOutput.Init(wc);
                        audioOutput.Play();
                        while (audioOutput.PlaybackState != PlaybackState.Stopped && !stop)
                        {
                            Thread.Sleep(20);
                        }
                        audioOutput.Stop();
                        if (handle != null)
                        {
                            handle(this, new PlayWaveStopEventArgs());
                        }
                    }
                }
                
            } catch(Exception){
                if (handle != null)
                {
                    handle(this, new PlayWaveStopEventArgs());
                }
            }
        }

        public void Play() {
            Thread thread = new Thread(new ThreadStart(PlayW));
            thread.Start();
        }

        public override long Length
        {
            get
            {
                if (endPos != 0 && endPos < base.Length)
                {
                    return endPos;
                }
                return base.Length;                
            }
        }

        public override WaveFormat WaveFormat
        {
            get
            {
                return base.WaveFormat;
            }
        }

        protected override void Dispose(bool disposing){
            base.Dispose(disposing);
        }

        public override int Read(byte[] array, int offset, int count) {
            if (this.Position + count < Length)
            {
                return base.Read(array, offset, count);
            }
            else if (this.Position < Length)
            {
                count -= (int)(this.Position - Length) + count;
                return base.Read(array, offset, count);
            }
            Stop();
            return 0;
        }
        public void Stop() {
            stop = true;
        }
    }
}

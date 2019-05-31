using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace UC
{
    public partial class NewWaveViewer : UserControl
    {
        private object lock_chart = new object();
        FormWindowState preState = FormWindowState.Normal;
        private double samplesPerPixel;
        private int positionData;
        private List<double> data;
        private Bitmap _bit;
        private Graphics _g;
        private string filePath;
        private Font TheFont = new Font("Times New Roman", 8, FontStyle.Regular);
        public Color PenColor { get; set; }
        public Color LineColor { get; set; }
        public float PenWidth { get; set; }
        public int MarginChart { get; set; }

        Rectangle chartRec;

        public string FilePath {
            get { return filePath; }
            set {
                if (value != null)
                {
                    filePath = string.Copy(value);
                }               
            }
        }

        public void PaintData() {
            if (InvokeRequired)
            {
                Action act = new Action(() => { this.PaintData(); });
                Invoke(act);
            }
            else
            {
                Redraw();
            }
            
        }

        private void RefreshInvoked() {
            if (InvokeRequired)
            {
                Action act = new Action(() => { this.RefreshInvoked(); });
                Invoke(act);
            }
            else {
                this.Refresh();
            }
            
        }

        public List<double> Data
        {  
            set
            {
                if (value != null)
                {
                    data = value;
                    samplesPerPixel = (double)data.Count / this.Width;
                }
                //this.Redraw();
            }
            get { return data; }
        }

        public NewWaveViewer()
        {
            MarginChart = 10;
            PenColor = Color.Red;
            LineColor = Color.SkyBlue;
            PenWidth = 0.1f;
            positionData = 0;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.MouseWheel += new MouseEventHandler(NewWaveViewer_MouseWheel);
            InitializeComponent();
            
            _bit = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            _g = Graphics.FromImage(_bit);
            UpdateRec();
        }

        private void UpdateRec() {
            chartRec = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
        }

        public double SamplesPerPixel
        {
            get
            {
                return samplesPerPixel;
            }
            set
            {
                samplesPerPixel = Math.Max(1, value);
                //this.Redraw();
            }
        }

        private void Redraw() {
            lock (lock_chart)
            {
                BackgroundWorker bgw = new BackgroundWorker();
                bgw.DoWork += bgw_DoWork;
                bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
                bgw.RunWorkerAsync();
            }
        }
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Draw(_g);
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.RefreshInvoked();
        }
        private void Draw(Graphics g) {
            if (data == null) return;
            if (data.Count == 0) return;
            g.Clear(SystemColors.Control);

            string text = string.Format("File Path : {0}", filePath);
            g.DrawString(text.ToString(), TheFont, Brushes.Black, /*ClientRectangle.X*/ 0, /*ClientRectangle.Y*/0, new StringFormat());

            int curr_pos = positionData;
            double max = data.Max();
            double min = data.Min();
            max = Math.Abs(min) > max ? Math.Abs(min) : max;

            double rationHigh = (chartRec.Height - MarginChart) / 2.0f / max;
            double center = (double)chartRec.X + ((double)chartRec.Height) / 2f;
            using (Pen linePen = new Pen(PenColor, PenWidth))
            {
                double preX = 0.0f;
                double preY = center;
                for (float x = (float)chartRec.X; x < chartRec.Right; x += 1.0f)
                {
                    int stat = (int)(x * samplesPerPixel);
                    int end = (int)((x + 1) * samplesPerPixel);
                    if (end >= data.Count)
                    {
                        end = data.Count;
                    }
                    if (stat >= data.Count)
                    {
                        continue;
                    }
                    double sum = 0f;
                    int count = 0;
                    double y1 = 0f;
                    if (stat != end)
                    {
                        for (int i = stat; i < end; i++)
                        {
                            sum += data[curr_pos + i];
                            count++;
                        }
                        y1 = center - (sum / count) * rationHigh;
                    }
                    else
                    {
                        y1 = center - data[curr_pos + stat] * rationHigh;
                    }

                    try
                    {
                        g.DrawLine(linePen, (float)x, (float)y1, (float)preX, (float)preY);
                        preX = x;
                        preY = y1;
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(_bit, 0, 0);
            //using (Pen linePen = new Pen(Color.Red, PenWidth))
            //{
            //    GraphicUtil.DrawBorderRec(e.Graphics, ClientRectangle, linePen);
            //}
            base.OnPaint(e);
        }

        private void NewWaveViewer_MouseWheel(object sender, ScrollEventArgs e)
        {
        }

        private void NewWaveViewer_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void NewWaveViewer_Resize(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                if (((Form)this.Parent).WindowState != FormWindowState.Minimized && FormWindowState.Minimized != preState)
                {
                    Debug.WriteLine(" NOT Minimized ");
                    UpdateRec();
                    Redraw();
                }
                else
                {
                    Debug.WriteLine(" Minimized ");
                }

                preState = ((Form)this.Parent).WindowState;
            }
            else
            {
                UpdateRec();
                Redraw();
            }
        }
    }
}

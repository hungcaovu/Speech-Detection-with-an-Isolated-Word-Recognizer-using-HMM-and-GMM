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
using System.Threading;
using Event;
using Object;
using Object.Event;
using System.Drawing.Drawing2D;

namespace UC
{
    public partial class WaveViewer : UserControl
    {
        /// <summary>
        /// Control for viewing waveforms
        /// </summary>
        public Color PenColor { get; set; }
        public float PenWidth { get; set; }
        public void FitToScreen()
        {
            if (waveStream == null) return;
            int samples = (int)(waveStream.Length / bytesPerSample);
        }
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private WaveStream waveStream;
        private string pathFile = "";
        private float samplesPerPixel = 128;
        private int averageBytesPerSecond;
        private int bytesPerSample;
        long length = 0;
        private float hightOfSlider;
        private float widthOfSlider;

        private float widthOfBar;

        private float leftLimit;
        private float rightLimit;

        private float leftSliderPoint;
        private float rightSliderPoint;

        private float leftSliderPointPre;
        private float rightSliderPointPre;

        private float marginSlider;

        private bool selectRight;
        private bool selectLeft;

        private Graphics gWave;
        private Bitmap bitWave;

        private Graphics gL;
        private Bitmap bitL;

        private Graphics gR;
        private Bitmap bitR;

        private Graphics gChart;
        private Graphics gChartB;
        private Bitmap bitChart;
        private Bitmap bitChartB;
        List<double> _chart;
        List<double> _chartB;
        List<double> _waveData;
        SolidBrush _charBrush;
        SolidBrush _charBrushBlue;

        float _thresholdChart;
        public List<double> Chart
        {
            set {
                if (value != null && _chart != value)
                {
                    _chart = value;
                    DrawChart();
                    this.Refresh();
                }
            }
        }

        public List<double> ChartBlue
        {
            set
            {
                if (value != null && _chartB != value)
                {
                    _chartB = value;
                    DrawChartB();
                    this.Refresh();
                }
            }
        }

        public List<double> WaveData
        {
            set
            {
                if (value != null && _waveData != value)
                {
                    _waveData = value;
                    DrawWaveChart();
                    this.Refresh();
                }
            }
        }
        public float ThresholdChart {
            set {
                _thresholdChart = value;
            }
        }

        RectangleF leftRec;
        RectangleF rightRec;
        RectangleF recRegion;

        /// <summary>
        /// Creates a new WaveViewer control
        /// </summary>
        public WaveViewer()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);


            this.DoubleBuffered = true;
            this.PenColor = Color.DodgerBlue;
            this.PenWidth = 1;
            this.ResizeRedraw = true;

            widthOfSlider = 10f;
            marginSlider = 0f;

            UpdateSize();

            rightRec = new RectangleF(new PointF(rightSliderPoint, recRegion.Y), new SizeF(widthOfSlider, hightOfSlider));
            leftRec = new RectangleF(new PointF(leftSliderPoint, recRegion.Y), new SizeF(widthOfSlider, hightOfSlider));


            selectRight = false;
            selectLeft = false;


            _charBrush = new SolidBrush(Color.Red);
            _charBrushBlue = new SolidBrush(Color.DarkBlue);
            UpdateSize();
        }
        /// <summary>
        /// sets the associated wavestream
        /// </summary>
        public WaveStream WaveStream
        {
            get
            {
                return waveStream;
            }
            set
            {
                if (waveStream != null)
                {
                    waveStream.Dispose();
                    waveStream = null;
                }
                waveStream = value;
                if (waveStream != null)
                {
                    waveStream = value;
                    bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * waveStream.WaveFormat.Channels;
                    averageBytesPerSecond = waveStream.WaveFormat.AverageBytesPerSecond;
                    length = waveStream.Length / (waveStream.WaveFormat.BitsPerSample / 8);

                    DrawWave();

                    waveStream.Close();
                    waveStream.Dispose();

                }
                else
                {
                    length = 0;
                }

            }
        }
        public string File
        {
            set
            {
                if (value != null && value != pathFile)
                {
                    pathFile = value;
                    waveStream = new NAudio.Wave.WaveFileReader(pathFile);

                    bytesPerSample = (waveStream.WaveFormat.BitsPerSample / 8) * waveStream.WaveFormat.Channels;
                    averageBytesPerSecond = waveStream.WaveFormat.AverageBytesPerSecond / waveStream.WaveFormat.Channels;
                    length = (waveStream.Length / (waveStream.WaveFormat.BitsPerSample / 8)) / waveStream.WaveFormat.Channels;

                    DrawWave();
                    waveStream.Close();
                    waveStream.Dispose();
                    waveStream = null;
                    this.Refresh();
                }
                else
                {
                    length = 0;
                }
            }
        }
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void Draw(Graphics g)
        {
            g.Clear(Control.DefaultBackColor);
            //Debug.WriteLine(" A Rec ViewWave {0} {1} {2} {3}", recRegion.Width, recRegion.Height, recRegion.X, recRegion.Y);
            //Debug.WriteLine(" B Rec ViewWave {0} {1} {2} {3}", ClientRectangle.Width, ClientRectangle.Height, ClientRectangle.X, ClientRectangle.Y);

            g.DrawImage(bitWave, 0, 0);
            g.DrawImage(bitChart, 0, 0);
            DrawMidleLine(g);
            //Debug.WriteLine(" On Paint");
            try
            {
                PaintBarAndSlider(g);
            }
            catch
            {
            }
        }

        /// <summary>
        /// <see cref="Control.OnPaint"/>
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
            base.OnPaint(e);
        }
        private void DrawWave()
        {
            if (waveStream != null)
            {
                Graphics g = gWave;
                g.Clear(this.BackColor);
                RectangleF r = recRegion;

                long samples = waveStream.Length / bytesPerSample;
                samplesPerPixel = (float)samples / r.Width;
                float detal = 10.0f;
                float hight = this.Height - 2 * detal;

                int bytesRead;
                byte[] waveData = new byte[(int)samplesPerPixel * bytesPerSample];

                using (Pen linePen = new Pen(PenColor, PenWidth))
                {
                    for (float x = r.X; x < r.Right; x += 1)
                    {
                        short low = 0;
                        short high = 0;
                        bytesRead = waveStream.Read(waveData, 0, (int)samplesPerPixel * bytesPerSample);
                        if (bytesRead == 0)
                            break;
                        for (int n = 0; n < bytesRead; n += 2)
                        {
                            short sample = BitConverter.ToInt16(waveData, n);
                            if (sample < low) low = sample;
                            if (sample > high) high = sample;
                        }
                        float lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
                        float highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);
                        g.DrawLine(linePen, x, hight * lowPercent + detal / 2, x, hight * highPercent + detal);
                    }
                }
            }
        }


        private void DrawWaveChart() {
            try {
                if (_waveData != null)
                {
                    double mx = _waveData.Max();
                    double mn = _waveData.Min();
                    double max = Math.Abs(mx) < Math.Abs(mn) ? Math.Abs(mn) : Math.Abs(mx);
                    Graphics g = gWave;
                    g.Clear(this.BackColor);
                    RectangleF r = recRegion;
                    int samples = _waveData.Count;
                    float pixelPerSample = (float)r.Width / samples;
                    float detal = 5.0f;
                    float fractionY = (r.Height / 2 - detal) / (float)max;
                    float centerY = r.Y + r.Height / 2;
                    float x = r.X;
                    using (Pen linePen = new Pen(PenColor, PenWidth))
                    {
                        for (int i = 1; i < samples; i++)
                        {
                            g.DrawLine(linePen, x, centerY + fractionY * (float)_waveData[i - 1], x, centerY + fractionY * (float)_waveData[i]);
                            x += pixelPerSample;
                        }
                        //for (float x = r.X; x < r.Right; x += 1)
                        //{
                        //    short low = 0;
                        //    short high = 0;
                        //    for (int n = 0; n < bytesRead; n += 2)
                        //    {
                        //        short sample = 
                        //        if (sample < low) low = sample;
                        //        if (sample > high) high = sample;
                        //    }
                        //    float lowPercent = ((((float)low) - short.MinValue) / ushort.MaxValue);
                        //    float highPercent = ((((float)high) - short.MinValue) / ushort.MaxValue);
                        //    g.DrawLine(linePen, x, hight * lowPercent + detal / 2, x, hight * highPercent + detal);
                        //}
                    }
                }
            } catch (Exception ex){
                LogUtil.Error("Draw Chart Error : {0}", ex.Message);
            }
        }
        private void DrawMidleLine(Graphics g) {
            Pen _midleLinePen = new Pen(Color.DarkGreen);
            g.DrawLine(_midleLinePen, new PointF(0.0f, recRegion.Height / 2.0f), new PointF(recRegion.Width, recRegion.Height / 2.0f));
        }

        private void DrawChart()
        {
            Graphics g = gChart;
            // Check conditional
            if(_chart != null && _chart.Count > 0){
                int pointSize = _chart.Count;
                double width = recRegion.Width;
                double hight = recRegion.Height;
                double maxValue = _chart.Max();
                double minValue = _chart.Min();

                double pointWidth = 2.0f;

                double maxAbs = Math.Abs(maxValue) > Math.Abs(minValue) ? Math.Abs(maxValue) : Math.Abs(minValue);

                double coffHight = hight / 2.0f / maxAbs;
                double coffWidth = width / (float)pointSize;
                double x = 0.0f;
                for (int cout = 0; x < width && cout < pointSize; x += coffWidth, cout ++)
                {
                    g.FillRectangle(_charBrush,
                        (float)(x + coffWidth / 2.0f),
                        (float)( hight / 2.0f * (1 - _chart[cout] / maxAbs)),
                        (float)(pointWidth), (float)(pointWidth));
                }

                Pen _thresholdChartLinePen = new Pen(Color.DarkGreen);
                g.DrawLine(_thresholdChartLinePen, new PointF(0.0f, (float)(hight / 2.0f * (1 - _thresholdChart / maxAbs))), new PointF((float)(recRegion.Width), (float)(hight / 2.0f * (1 - _thresholdChart / maxAbs))));
            }
        }

        private void DrawChartB()
        {
            Graphics g = gChartB;
            // Check conditional
            if (_chartB != null && _chartB.Count > 0)
            {
                int pointSize = _chartB.Count;
                double width = recRegion.Width;
                double hight = recRegion.Height;
                double maxValue = _chartB.Max();
                double minValue = _chartB.Min();

                double pointWidth = 2.0f;

                double maxAbs = (double)(Math.Abs(maxValue) > Math.Abs(minValue) ? Math.Abs(maxValue) : Math.Abs(minValue));

                double coffHight = hight / 2.0f / maxAbs;
                double coffWidth = width / (double)pointSize;
                double x = 0.0f;
                for (int cout = 0; x < width && cout < pointSize; x += coffWidth, cout++)
                {
                    g.FillRectangle(_charBrushBlue,
                        (float)(x + coffWidth / 2.0f),
                        (float)(hight / 2.0f * (1 - _chartB[cout] / maxAbs)),
                        (float)(pointWidth), (float)(pointWidth));
                }

                //Pen _thresholdChartLinePen = new Pen(Color.DarkGreen);
                //g.DrawLine(_thresholdChartLinePen, new PointF(0.0f, (float)(hight / 2.0f * (1 - _thresholdChart / maxAbs))), new PointF((float)(recRegion.Width), (float)(hight / 2.0f * (1 - _thresholdChart / maxAbs))));
            }
        }
        /// <summary>
        /// Raise Event Selected time
        /// </summary>
        private event TimeSelectedEventHandler handler;

        public event TimeSelectedEventHandler TimeSelectedChanged
        {
            add
            {
                handler += value;
            }
            remove
            {
                handler -= value;
            }
        }
        private void WaveViewer_Resize(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                if (((Form)this.Parent).WindowState != FormWindowState.Minimized)
                {
                    Debug.WriteLine("Resize Object");
                    UpdateSize();
                    DrawWave();
                    DrawChart();
                    DrawChartB();
                    this.Refresh();
                }
            }
            else
            {
                //Debug.WriteLine("Resize Object");
                UpdateSize();
                DrawWave();
                DrawChart();
                DrawChartB();
                this.Refresh();
            }
        }
        private void WaveViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (rightRec.Contains(e.Location))
            {
                Debug.WriteLine("Select Right: {0} {1}", e.Location.X, e.Location.Y);
                selectRight = true;
            }

            if (leftRec.Contains(e.Location))
            {
                Debug.WriteLine("Select Left: {0} {1}", e.Location.X, e.Location.Y);
                selectLeft = true;
            }
            //animateSider = false;
        }
        public float RightSlider {
            set {
                if (value != rightSliderPoint && value >= recRegion.X && value <= recRegion.X + recRegion.Width)
                {
                    moveRightSliderPoint(value);
                }
                
            }
            get {
                return rightSliderPoint/ recRegion.Width;
            }
        }
        public float LeftSlider
        {
            set
            {
                if (value != leftSliderPoint && value >= recRegion.X && value <= recRegion.X + recRegion.Width)
                {
                    moveLeftSliderPoint(value);
                }

            }
            get {
                return (leftSliderPoint + widthOfSlider)/recRegion.Width;
            }
        }
        private void moveRightSliderPoint(float point)
        {
            if (InvokeRequired)
            {
                Action<float> act = new Action<float>(moveRightSliderPoint);
                Invoke(act, point);
            }
            else {
                rightSliderPoint = point * recRegion.Width;
                Debug.WriteLine("Set Right Silider Point: Actual Value {0}", rightSliderPoint);
                this.Refresh();
            }
        }
        private void moveLeftSliderPoint(float point)
        {
            if (InvokeRequired)
            {
                Action<float> act = new Action<float>(moveLeftSliderPoint);
                Invoke(act, point);
            }
            else
            {
                leftSliderPoint = point * recRegion.Width - widthOfSlider;
                Debug.WriteLine("Set Left Silider Point: Star Rec {0} Actual Value {1}", leftSliderPoint, point * recRegion.Width);
                this.Refresh();
            }
        }
        private void WaveViewer_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Debug.WriteLine("Mouse Move Moved : {0} ", e.X);

                if (selectRight && e.Button == System.Windows.Forms.MouseButtons.Left
                    && e.X >= recRegion.X && e.X <= recRegion.X + recRegion.Width)
                {
                    //Debug.WriteLine("Mouse Move Right Event : {0}  {1}", e.X, leftSliderPoint + widthOfSlider);
                    if (e.X - widthOfSlider / 2 > leftSliderPoint + widthOfSlider / 2 && e.X - widthOfSlider / 2 <= rightLimit)
                    {
                        rightSliderPoint = e.X - widthOfSlider / 2;
                        this.Refresh();
                    }
                    else
                    {
                        selectRight = false;
                    }
                }

                //Debug.WriteLine("Mouse Move Left Event : {0}  {1} {2}", e.X, recRegion.X, recRegion.X + recRegion.Width - widthOfSlider);
                if (selectLeft && e.Button == System.Windows.Forms.MouseButtons.Left
                    && e.X >= recRegion.X && e.X <= recRegion.X + recRegion.Width)
                {
                    //Debug.WriteLine("Mouse Move Left Event : {0}  {1}", e.X, rightSliderPoint - widthOfSlider);
                    if (e.X - widthOfSlider / 2 < rightSliderPoint - widthOfSlider / 2 && e.X - widthOfSlider / 2 >= leftLimit)
                    {
                        leftSliderPoint = e.X - widthOfSlider / 2;
                        this.Refresh();
                    }
                    else
                    {
                        selectLeft = false;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Throw Ex : {0}", ex.Message);
            }

        }
        private void WaveViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectLeft || selectRight)
            {
                selectRight = false;
                selectLeft = false;
                //animateSider = true;
                leftSliderPointPre = leftSliderPoint;
                rightSliderPointPre = rightSliderPoint;
                if (handler != null)
                {
                    handler(this, new TimeSelectedEventArgs((leftSliderPoint + widthOfSlider) / widthOfBar, rightSliderPoint / widthOfBar, widthOfBar));
                }
                this.Refresh();
            }
        }
        private void PaintBarAndSlider(Graphics myGraphics)
        {
            Debug.WriteLine("Draw");
            System.Drawing.Brush brSolidBrush;
            //System.Drawing.Pen myPen;

            //If Interesting mouse event happened on the Thumb1 Draw Thumb1
            if (selectRight)
            {
                brSolidBrush = new System.Drawing.SolidBrush(this.BackColor);
                myGraphics.FillRectangle(brSolidBrush, rightRec);
            }
            //if interesting mouse event happened on Thumb2 draw thumb2
            if (selectLeft)
            {
                brSolidBrush = new System.Drawing.SolidBrush(this.BackColor);
                myGraphics.FillRectangle(brSolidBrush, leftRec);
            }

            //myPen = new System.Drawing.Pen(Color.Red, 10);
            //myGraphics.DrawLine(myPen, leftRec.Location, rightRec.Location);

            rightRec.X = rightSliderPoint;
            leftRec.X = leftSliderPoint;
            Debug.WriteLine("L Rec {0} R Rec {1}", rightSliderPoint, leftSliderPoint);
            // If the Thumb is an Image it draws the Image or else it draws the Thumb

            brSolidBrush = new System.Drawing.SolidBrush(Color.Blue);
            myGraphics.FillRectangle(brSolidBrush, rightRec);
            Debug.WriteLine("Draw Right Rec : {0} {1}", rightRec.X, rightRec.Y);
            // If the Thumb is an Image it draws the Image or else it draws the Thumb

            brSolidBrush = new System.Drawing.SolidBrush(Color.Blue);
            myGraphics.FillRectangle(brSolidBrush, leftRec);
            Debug.WriteLine("Draw Left Rec : {0} {1}", leftRec.X, leftRec.Y);
        }
        private void PaintBarAndSlider()
        {
            Debug.WriteLine("Draw");
            System.Drawing.Brush brSolidBrush;
            //System.Drawing.Pen myPen;

            //If Interesting mouse event happened on the Thumb1 Draw Thumb1
            if (selectRight)
            {
                brSolidBrush = new System.Drawing.SolidBrush(Color.Transparent);
                gR.FillRectangle(brSolidBrush, rightRec);
                //gR.Clear(Color.Transparent);
            }
            //if interesting mouse event happened on Thumb2 draw thumb2
            if (selectLeft)
            {
                //brSolidBrush = new System.Drawing.SolidBrush(this.BackColor);
                //gL.FillRectangle(brSolidBrush, leftRec);
                gL.Clear(Color.Transparent);
            }

            //myPen = new System.Drawing.Pen(Color.Red, 10);
            //myGraphics.DrawLine(myPen, leftRec.Location, rightRec.Location);

            rightRec.X = rightSliderPoint;
            leftRec.X = leftSliderPoint;
            Debug.WriteLine("L Rec {0} R Rec {1}", rightSliderPoint, leftSliderPoint);
            // If the Thumb is an Image it draws the Image or else it draws the Thumb
            if (selectRight)
            {
                brSolidBrush = new System.Drawing.SolidBrush(Color.Blue);
                gR.FillRectangle(brSolidBrush, rightRec);
                Debug.WriteLine("Draw Right Rec : {0} {1}", rightRec.X, rightRec.Y);
            }

            // If the Thumb is an Image it draws the Image or else it draws the Thumb
            if (selectLeft)
            {
                brSolidBrush = new System.Drawing.SolidBrush(Color.Blue);
                gL.FillRectangle(brSolidBrush, leftRec);
                Debug.WriteLine("Draw Left Rec : {0} {1}", leftRec.X, leftRec.Y);
            }
            //DrawWave();
        }
        private void UpdateSize()
        {
            Graphics myGraphics = this.CreateGraphics();
            recRegion = myGraphics.VisibleClipBounds;

            bitWave = new Bitmap((int)recRegion.Width, (int)recRegion.Height);
            gWave = Graphics.FromImage(bitWave);
            gWave.Clear(this.BackColor);
            GraphicUtil.DrawBorderRec(gWave, recRegion, new Pen(Color.DarkBlue));

            bitChart = new Bitmap((int)recRegion.Width, (int)recRegion.Height);
            bitChart.MakeTransparent();
            gChart = Graphics.FromImage(bitWave);
            gChart.Clear(Color.Transparent);

            //bitChartB = new Bitmap((int)recRegion.Width, (int)recRegion.Height);
            //bitChartB.MakeTransparent();
            gChartB = Graphics.FromImage(bitWave);
            gChartB.Clear(Color.Transparent);

            bitL = new Bitmap((int)recRegion.Width, (int)recRegion.Height);
            bitL.MakeTransparent();
            gL = Graphics.FromImage(bitL);
            gL.Clear(Color.Transparent);

            bitR = new Bitmap((int)recRegion.Width, (int)recRegion.Height);
            bitR.MakeTransparent();
            gR = Graphics.FromImage(bitR);
            gR.Clear(Color.Transparent);

            widthOfBar = recRegion.Width;

            hightOfSlider = recRegion.Height;


            leftLimit = recRegion.X + marginSlider;
            rightLimit = recRegion.X + recRegion.Width - widthOfSlider - marginSlider;

            leftSliderPoint = leftLimit;
            rightSliderPoint = rightLimit;

            rightRec.X = rightSliderPoint;
            rightRec.Height = hightOfSlider;

            leftRec.X = leftSliderPoint;
            leftRec.Height = hightOfSlider;
        }
    }
}

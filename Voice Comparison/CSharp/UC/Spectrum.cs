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
using Object;
using System.Threading;
using Object.Enum;

namespace UC
{
    public partial class Spectrum : UserControl
    {
        private object lockChart = new object();
        FormWindowState preState = FormWindowState.Normal;
        // Phan do hoa chart
        private string title_txt = "";
        private int hightTitle = 20;
        private int bandsNumber = 10;
        private int nFrame = 1;
        private int widthBandRuler = 25;
        private int widthColorRuler = 50;
        private int hightFrameRuler = 25;
        private int paddingXPixel = 5;
        private int paddingYPixel = 5;
        private List<List<double>> data;

        private ColorStyle colStyle = ColorStyle.GRAY;

        Rectangle bandRuler;
        Rectangle sizeRuler;
        Rectangle colorRuler;
        Rectangle colorRulerWithoutText;
        Rectangle chartRec;
        Rectangle title;

        Bitmap bit;
        Graphics gBit;

        // Color Danh co rule mau tham khao
        private float maxValue = 100f;
        private float minValue = -100f;
        private int numColor = 4000;
        private int pre_numColor = 400;
        private int colorRulerBand = 9; // So text tren map mau
        int stepColorRulerBand = 0; // Text tren cai map mau
        private float resolutionValue = 0.005f;// Dung do phan giai mau :d

        private ColorMap clMap = null;

        // WOrkgroundBacker
        BackgroundWorker _bgwUpdateColor = null;


        // List chua cac gia tri start cua x va y trong chart.
        List<PointF> listX = null;
        List<PointF> listY = null;

        public ColorStyle ColorStyle {
            set {
                if (colStyle != value)
                {
                    colStyle = value;
                    ReDraw();
                }
            }
        }

        #region Cac gia tri cua Color Ruler
        // So so hien thi tren thanh thang mau
        public int ColorRulerBand {
            set {
                if (value != colorRulerBand)
                {
                    colorRulerBand = value;
                } 
            }
            get { return colorRulerBand; }
        }
        //Gia tri max array
        public float MaxValue
        {
            set
            {
                if (value != maxValue)
                {
                    maxValue = value;
                }
            }
            get { return maxValue; }
        }
        // Gia tri min cua array
        public float MinValue
        {
            set
            {
                if (value != minValue)
                {
                    minValue = value;
                }
            }
            get { return minValue; }
        }
        // Do phan giai gia tri
        public float ResolutionValue
        {
            set
            {
                if (value != resolutionValue)
                {
                    resolutionValue = value;
                }
            }
            get { return resolutionValue; }
        }
        // Do rong cua dong ke tren thuoc
        public int WidthLine { set; get; }
       
        public string Title
        {
            set
            {
                title_txt = value;
            }
            get { return title_txt; }
        }

        // Disable this method
        /*
        public int NumberColors
        {
            set
            {
                numColor = value;
                RefreshValue();
            }
            get { return numColor; }
        }
        /*
        // So gia tri cot doc // Banks FMCC
        private int BandsNumber
        {
            set
            {
                bandsNumber = value;
            }
            get
            {
                return bandsNumber;
            }
        }
        // So gia tri hangngang // Frames
        private int SizeFrame
        {
            set
            {
                sizeFrame = value;
            }
            get
            {
                return sizeFrame;
            }
        } */
        #endregion

        #region Cac tham so du lieu ve char
        public List<List<double>> Data
        {
            set
            {
                data = value;

                if (data != null)
                {
                    setData(data);
                }

            }
            get
            {
                return data;
            }

        }

        private void setData(List<List<double>> Data)
        {
                nFrame = data.Count;
                if (nFrame > 0)
                {
                    List<double> tmp = data[0];
                    bandsNumber = tmp.Count<double>();
                    ReDraw();
                } 
        }
        #endregion

        #region ReDraw
        public void RefreshInvoked()
        {
            if (InvokeRequired)
            {
                Action act = new Action(() => { this.RefreshInvoked(); });
                Invoke(act);
            }
            else
            {
                this.Refresh();
            }

        }
        private void ReDraw()
        {
            Debug.WriteLine("ReDraw");
            BackgroundWorker bgw_f = new BackgroundWorker();
            bgw_f.DoWork += bgw_f_DoWork;
            bgw_f.RunWorkerCompleted += bgw_f_RunWorkerCompleted;
            bgw_f.RunWorkerAsync();
        }
        void bgw_f_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (lockChart)
            {
                if (clMap == null)
                {
                    Debug.WriteLine("ReDraw : Update Colors ");
                    e.Result = false;
                    Debug.WriteLine("Spectrum : DoWork BGW: Result False");
                    UpdateColors();

                }
                else {
                    Debug.WriteLine("ReDraw : Update Draw ");
                    e.Result = true;
                    Debug.WriteLine("Spectrum : DoWork BGW: Result True");
                    Draw(gBit);
                }
            }
        }
        void bgw_f_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (InvokeRequired)
            {
                Debug.WriteLine("Spectrum : Completed BGW: Need Invoke");
            }
            else {
                Debug.WriteLine("Spectrum : Completed BGW: No Need Invoke");
            }
            if ((bool)e.Result)
            {
                Debug.WriteLine("Spectrum : DoWork BGW: Result True");
                this.RefreshInvoked();
            }
            
        }
        #endregion

        #region Update Color
        private void ExcuteUpdateColors() {
            if (_bgwUpdateColor == null)
            {
                UpdateColors();
            } else if (_bgwUpdateColor.IsBusy)
            {
                _bgwUpdateColor.CancelAsync();
                UpdateColors();
            }
            else
            {
                UpdateColors();
            }

            if (data != null) {
                ReDraw();
            }
        }
        private void UpdateColors()
        {
            stepColorRulerBand = ((int)MaxValue - (int)MinValue) / ColorRulerBand;
            numColor = (int)((maxValue - minValue) / resolutionValue) + 1;// Update so mau

            _bgwUpdateColor = new BackgroundWorker();
            _bgwUpdateColor.WorkerSupportsCancellation = true;
            _bgwUpdateColor.DoWork += bgw_DoWork;
            _bgwUpdateColor.RunWorkerCompleted += bgw_RunWorkerCompleted;
            _bgwUpdateColor.RunWorkerAsync();
        }
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (lockChart)
            {
                if (pre_numColor != numColor)
                {
                    Debug.WriteLine(" DoWork : Create clMap");
                    pre_numColor = numColor;
                    clMap = new ColorMap(numColor);
                    Debug.WriteLine(" DoWork : Create clMap : Done");
                }
            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (data != null)
            {
                Debug.WriteLine("Update Color Completed. Trigger Redraw");
                this.ReDraw();
            }
        }
        #endregion

        #region Update Size
        private void UpdateRecs()
        {
            int x = ClientRectangle.X;
            int y = ClientRectangle.Y + hightTitle;
            int xf = x + Size.Width - 1;
            int yf = y + Size.Height - 1 - hightTitle;
            int newXL = x + widthBandRuler + paddingXPixel;
            int newXR = xf - widthColorRuler - paddingXPixel;

            int newYT = y;
            int newYB = yf - hightFrameRuler - paddingYPixel;
            title = new Rectangle(ClientRectangle.X, ClientRectangle.Y, widthBandRuler, hightTitle);
            bandRuler = new Rectangle(x, y, widthBandRuler, newYB - newYT);
            sizeRuler = new Rectangle(newXL, newYB + paddingYPixel, newXR - newXL, hightFrameRuler);
            colorRuler = new Rectangle(newXR + paddingXPixel, y, widthColorRuler, newYB - newYT);
            colorRulerWithoutText = new Rectangle(colorRuler.X, colorRuler.Y, colorRuler.Width / 2, colorRuler.Height);
            chartRec = new Rectangle(newXL, newYT, newXR - newXL, newYB - newYT);
            bit = new Bitmap(this.Size.Width, this.Size.Height);
            gBit = Graphics.FromImage(bit);
        }
        private void Spectrum_SizeChanged(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {
                if (((Form)this.Parent).WindowState != FormWindowState.Minimized && FormWindowState.Minimized != preState)
                {
                     Debug.WriteLine(" NOT Minimized ");
                    UpdateRecs();
                    ReDraw();
                }
                else
                {
                    Debug.WriteLine(" Minimized ");
                }

                preState = ((Form)this.Parent).WindowState;
            } else {
                UpdateRecs();
                ReDraw();
            }
        }
        #endregion

        private Font TheFont = new Font("Times New Roman", 8, FontStyle.Regular);

        public Spectrum()
        {
            bandsNumber = 10;
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

             listX = new List<PointF>();
             listY = new List<PointF>();
             UpdateRecs();
        }

        //Ve thuoc tren truc y
        private void DrawRulerBand(Graphics g, Rectangle BandRec)
        {
            listY.Clear();
            float RulerHeight = BandRec.Height;
            float RulerWidth = BandRec.Width;
            float posX = BandRec.X;
            float posY = BandRec.Y;
            float pixelPerBand = RulerHeight / bandsNumber;
            int incFrameSize = 1;

            if (pixelPerBand < RulerHeight * 20.0 / 100.0)
            {
                incFrameSize = (int)((float)bandsNumber * 20.0 / 100.0);
            }

            float posLiney = RulerHeight + posY;
            float posLinex = posX + BandRec.Width - WidthLine;

            float posNumY = RulerHeight + posY + (/*pixelPerBand */- TheFont.Height) / 2;
            
            int count = 0;
            for (int i = 0; i <= bandsNumber; i++)
            {
                if (i % incFrameSize == 0)
                {
                    listY.Add(new PointF(posLinex, posLiney));
                    g.DrawString(count.ToString(), TheFont, Brushes.Black, posLinex - (float)(count.ToString().Length) * TheFont.Height /2, posNumY, new StringFormat()); // Write number
                    g.DrawLine(Pens.Black, posLinex, posLiney, posLinex + WidthLine, posLiney); // Draw the short line
                    posLiney -= pixelPerBand;
                    posNumY -= pixelPerBand;
                    count += incFrameSize;
                }
                else {
                    listY.Add(new PointF(posLinex, posLiney));
                    g.DrawLine(Pens.Black, posLinex + 3.0f *WidthLine / 4, posLiney, posLinex + WidthLine , posLiney); // Draw the short line
                    posLiney -= pixelPerBand;
                    posNumY -= pixelPerBand;
                }
               
                //Debug.WriteLine("posLinex : " + posLinex + " Off :" + (posLinex + WidthLine) + "posLiney : " + posLiney);
            }
        }
        // Ve thuoc nam ngang ve thuoc x
        private void DrawRulerSize(Graphics g, Rectangle BandRec)
        {
            listX.Clear();
            int RulerHeight = BandRec.Height;
            int RulerWidth = BandRec.Width;
            float posX = (float)BandRec.X;
            float posY = (float)BandRec.Y;
            float pixelPerFrame = (float)RulerWidth / nFrame;

            int incFrameSize = 1;

            if (pixelPerFrame < RulerWidth * 20.0 / 100.0) {
                incFrameSize = (int)((float)nFrame * 20.0 / 100.0);
            }

            float posLiney = posY;
            float posLinex = posX;

            float posNumX = posX;// +(pixelPerFrame - TheFont.Height) / 2;// Khoi tao gia tri bat dau

            int count = 0;

            for (int i = 0; i <= nFrame; i++)
            {
                if (i % incFrameSize == 0)
                {
                    listX.Add(new PointF(posLinex, posLiney));
                    // Ve chu o vi tri leng cua chuoi chia hai nhan voi do rong cua size
                    g.DrawString(count.ToString(), TheFont, Brushes.Black, posNumX - (float)(count.ToString().Length) / 2 * TheFont.Size, posLiney + WidthLine / 2, new StringFormat()); // Write number
                    g.DrawLine(Pens.Black, posLinex, posLiney, posLinex, posLiney + WidthLine); // Draw the short line
                    posLinex += pixelPerFrame;
                    posNumX += pixelPerFrame;
                    count += incFrameSize;
                }
                else {
                    listX.Add(new PointF(posLinex, posLiney));
                    g.DrawLine(Pens.Black, posLinex, posLiney, posLinex, posLiney + WidthLine/4); // Draw the short line
                    posLinex += pixelPerFrame;
                    posNumX += pixelPerFrame;
                }
                
               // Debug.WriteLine("posLinex : " + posLinex + " Off :" + (posLinex + WidthLine) + "posLiney : " + posLiney);
            }


        }
        private void DrawChart(Graphics g, Rectangle BandRec)
        {
            if (nFrame > 0 && data != null)
            {
                for (int i = 0; data != null && i < nFrame && i < data.Count; i++)// List X
                {
                    List<double> frame = data[i];
                    for (int j = 0; frame != null && j < bandsNumber && j < frame.Count<double>(); j++)
                    { // List Y
                        if (listX.Count > 0 && listY.Count > 0) {
                            PointF[] points = new PointF[4];
                            points[0] = new PointF((float)listX[i].X, (float)listY[j].Y);
                            points[1] = new PointF((float)listX[i].X, (float)listY[j + 1].Y);
                            points[2] = new PointF((float)listX[i + 1].X, (float)listY[j + 1].Y);
                            points[3] = new PointF((float)listX[i + 1].X, (float)listY[j].Y);
                            DrawColorMap(g, points, (float)maxValue, (float)minValue, (float)frame[j]);
                        }   
                    }
                }
            }
        }
        // Ham ve thuoc cua thuoc mau
        private void DrawRulerColor(Graphics g, Rectangle ColorRec)
        {
            int RulerHeight = ColorRec.Height;
            int RulerWidth = ColorRec.Width;
            int posX = ColorRec.X;
            int posY = ColorRec.Y;
            int pixelPerBand = RulerHeight *9 / ((ColorRulerBand - 1) * 10);


            int posLiney = posY;
            int posLinex = posX + (int)(0.66 * RulerWidth);

            int countUp = stepColorRulerBand;
            int countDown = -stepColorRulerBand;


            int center = posY + (RulerHeight) / 2;
            int UpY = center + pixelPerBand;
            int DownY = center - pixelPerBand;
            // Ve gach so 0
            g.DrawString("0", TheFont, Brushes.Black, posLinex, center - TheFont.Height / 2, new StringFormat()); // Write number
            //g.DrawLine(Pens.Black, posLinex , posLiney, posLinex + /*WidthLine/2*/ 3, posLiney); // Draw the short line

            for (int i = 1; i <= ColorRulerBand / 2; i++)
            {
                //Up
                g.DrawString(countUp.ToString(), TheFont, Brushes.Black, posLinex, UpY - TheFont.Height / 2, new StringFormat()); // Write number
                //g.DrawLine(Pens.Black, posLinex, UpY, posLinex + /*WidthLine/2*/ 3, UpY); // Draw the short line
                UpY += pixelPerBand;
                countUp += stepColorRulerBand;

                //Down
                g.DrawString(countDown.ToString(), TheFont, Brushes.Black, posLinex, DownY - TheFont.Height / 2, new StringFormat()); // Write number
                //g.DrawLine(Pens.Black, posLinex, DownY, posLinex + /*WidthLine/2*/ 3, DownY); // Draw the short line
                DownY -= pixelPerBand;
                countDown -= stepColorRulerBand;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(bit, 0, 0);
            //base.OnPaint(e);
            //this.Refresh();
        }

        private void DrawColorMap(Graphics g, PointF[] pts,
                                    float ymin, float ymax, float y)
        {
            Color color = clMap.DrawColorMap(ymin, ymax, y, colStyle);
            //if (!isColor)
            //{
            //    //create the grayscale version of the pixel
            //    int grayScale = (int)((color.R * .3) + (color.G * .59)
            //        + (color.B * .11));

            //    //create the color object
            //    color = Color.FromArgb(grayScale, grayScale, grayScale);
            //}
            SolidBrush aBrush = new SolidBrush(color);
            g.FillPolygon(aBrush, pts);
           
        }
        // Ve mot cai cot. Color Ruler
        private void DrawAColRuler(Graphics g, Rectangle recCol) {
            float hight = (float)recCol.Height + (float)recCol.Y;
            float incValue = (maxValue - minValue) / ( numColor); //Khoi tao gia tri them vao khi ve gradient
            float incY = (float)recCol.Height / numColor;// Do tang cua tung mau
            float value = minValue;
            float xL = recCol.X;
            float xR = recCol.X + recCol.Width;
            PointF[] points = new PointF[4];
            for (float j = recCol.Y; j < hight; j += incY)
            {
                points[0] = new PointF(xL, j + incY);
                points[1] = new PointF(xR, j + incY);
                points[2] = new PointF(xR, j );
                points[3] = new PointF(xL, j);
                DrawColorMap(g, points, maxValue, minValue, value);
                value += incValue;
            }
        }
        private void Draw(Graphics _g) {
            Debug.WriteLine(" Draw : Start Draw");
            try
            {
                _g.Clear(SystemColors.Control);
                _g.DrawString(Title, TheFont, Brushes.Black, title.X, title.Y);

                if (bandsNumber > 0)
                {
                    // Ve truc y ve band
                    DrawRulerBand(_g, bandRuler);
                    // Ve Ruler Color ve chu
                    DrawRulerColor(_g, colorRuler);
                    // Ve Ruler Color ve mau
                    DrawAColRuler(_g, colorRulerWithoutText);
                }

                if (nFrame > 0)
                {
                    //ve size ve truc x
                    DrawRulerSize(_g, sizeRuler);
                }
                // Ve Chart
                DrawChart(_g, chartRec);
                //bool result = GraphicUtil.DrawLineChart(chartRec, pitch, _g, 20, 0);//GraphicUtil.DrawLineChart(chartRec, pitch, out bitPitch, 20, 0);
            }
            catch(Exception)
            {

            }
            

        }
    }
}

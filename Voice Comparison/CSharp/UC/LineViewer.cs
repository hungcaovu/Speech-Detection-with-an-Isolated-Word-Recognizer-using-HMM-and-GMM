using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace UC
{
    public partial class LineViewer : UserControl
    {
        object lockDraw = new object();
        FormWindowState preState = FormWindowState.Normal;
        Rectangle lefRec;
        Rectangle botRec;
        Rectangle chartRec;
        List<double> data = null;
        Font font = new Font("Times New Roman", 8, FontStyle.Regular);
        Bitmap bit = null;
        int lenY = 10;
        public LineViewer()
        {
            MaxValue = 700;
            StepValue = 100;
            InitializeComponent();
            UpdateSize();
        }

        public int MaxValue { set; get; }

        public int MinValue { set; get; }

        public int StepValue { set; get; }
        private int subRow = 2;
        private Pen penGrid = new Pen(Color.FromArgb(64, Color.Blue));
        private Pen penSubLineGrid = new Pen(Color.FromArgb(32, Color.Green));
        private int widthLine = 10;

        private Pen penChart = new Pen(Color.Red, 4.5f);
        public List<double> Data
        {
            set {
                if (data != value) {
                    data = value;
                    lenY = data.Count;
                    this.ReDraw();
                }
            }
        }

        private void UpdateSize(int widthLeftRuler = 25, int hightBottomRuler = 20, int margindrawarea = 20, int marginruler = 10) {
            bit = new Bitmap(Size.Width, Size.Height);

            Rectangle drawRec = new Rectangle(ClientRectangle.X + margindrawarea, ClientRectangle.Y + margindrawarea, ClientRectangle.Width - 2 * margindrawarea, ClientRectangle.Height - 2 * margindrawarea);

            lefRec = new Rectangle(drawRec.X, drawRec.Y, widthLeftRuler, drawRec.Height - hightBottomRuler - marginruler);

            botRec = new Rectangle(drawRec.X + marginruler + widthLeftRuler, drawRec.Y + lefRec.Height + marginruler, drawRec.Width - marginruler - widthLeftRuler, hightBottomRuler);

            chartRec = new Rectangle(botRec.X, lefRec.Y, botRec.Width, lefRec.Height);
           
        }

        private void Draw(){
            Graphics g = Graphics.FromImage(bit);
            g.Clear(SystemColors.Control);
            try
            {
                GraphicUtil.DrawRulerY(g, lefRec, font, MaxValue, StepValue, subRow, widthLine, false);
                GraphicUtil.DrawRulerX(g, botRec, font, lenY, 1, 1, widthLine, false);
                GraphicUtil.DrawGrid(g, chartRec, penGrid, penSubLineGrid, lenY, 1, MaxValue, StepValue, subRow, 1);
                GraphicUtil.DrawLineChart(g, chartRec, data, penChart, MaxValue, 0, 0);
            } catch(Exception){
                Debug.WriteLine(" Line Viewer: Draw Through Exception");
            }
        }

        public void RefreshInvoked()
        {
            if (InvokeRequired)
            {
                Action act = new Action(RefreshInvoked);
                Invoke(act);
            }
            else
            {
                this.Refresh();
            }
        }

        public void ReDraw() {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync();
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e) {
            lock (lockDraw)
            {
                try
                {
                    Draw();
                } catch (Exception){
                    Debug.WriteLine("Line Viewer: Draw Throgh Exception");
                }
            }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.RefreshInvoked();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(bit, 0, 0);
            base.OnPaint(e);
        }

        private void LineViewer_Resize(object sender, EventArgs e)
        {
           
                lock (lockDraw)
                {
                    if (this.Parent != null)
                    {
                        if (((Form)this.Parent).WindowState != FormWindowState.Minimized && FormWindowState.Minimized != preState)
                        {
                            Debug.WriteLine(" NOT Minimized ");

                            UpdateSize();
                            this.ReDraw();
                        }
                        else
                        {
                            Debug.WriteLine(" Minimized ");
                        }

                        preState = ((Form)this.Parent).WindowState;
                    }
                    else
                    {

                        UpdateSize();
                        this.ReDraw();
                    }
                }
        }

    }
}

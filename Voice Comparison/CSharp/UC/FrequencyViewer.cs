using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Imaging;

namespace UC
{
    public partial class FrequencyViewer : UserControl
    {
        #region Variable
        //long with_control;
        double time;// ms

        double max_hz;
        double min_hz;
        long size_Fre;
        List <double []> data_frames = null;
        double ratio_Px_Height;
        double ratio_Px_Width;
        #endregion

        #region Method
       

        public double Time
        {
            set
            {
                time = value;
            }
        }

        public double Min_Hz
        {
            set { min_hz = value; }
        }

        public double Max_Hz
        {
            set { max_hz = value; }
        }

        public long Size_Fre
        {
            set
            {
                size_Fre = value;
            }
        }
        public List<double[]> Data
        {

            set
            {
                data_frames = value;
            }
        }

        #endregion
        public FrequencyViewer()
        {
            InitializeComponent();
           
        }

        private void spectrumPic_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            Bitmap canvas = new Bitmap(pic.Width, pic.Height);
            Graphics offScreenDC = Graphics.FromImage(canvas);

            int width = canvas.Width;
            int height = canvas.Height;

            double min = double.MaxValue;
            double max = double.MinValue;
            double range = 0;

            ratio_Px_Height = (double)size_Fre / (double)height;
            ratio_Px_Width = (double)data_frames.Count / (double)width;


            // get min/max
            for (int w = 0; w < data_frames.Count; w++)
                for (int x = 0; x < ((double[])data_frames[w]).Length; x++)
                {
                    double amplitude = ((double[])data_frames[w])[x];
                    if (min > amplitude)
                    {
                        min = amplitude;
                    }
                    if (max < amplitude)
                    {
                        max = amplitude;
                    }
                }

            // get range
            if (min < 0 || max < 0)
                if (min < 0 && max < 0)
                    range = max - min;
                else
                    range = Math.Abs(min) + max;
            else
                range = max - min;



            PixelFormat format = canvas.PixelFormat;
            BitmapData data = canvas.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, format);
            int stride = data.Stride;
            int offset = stride - width * 4;

            try
            {
                unsafe
                {
                    byte* pixel = (byte*)data.Scan0.ToPointer();

                    // for each cloumn
                    for (int y = 0; y <= height; y++)
                    {
                        if (y < data_frames.Count)
                        {
                            // for each row
                            for (int x = 0; x < width; x++, pixel += 4)
                            {
                                double amplitude = ((double[])data_frames[data_frames.Count - y - 1])[(int)(((double)(size_Fre) / (double)(width)) * x)];
                                double color = GetColor(min, max, range, amplitude);
                                pixel[0] = (byte)0;
                                pixel[1] = (byte)color;
                                pixel[2] = (byte)0;
                                pixel[3] = (byte)255;
                            }
                            pixel += offset;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // unlock image
            canvas.UnlockBits(data);

            // Clean up
            pic.Image = canvas;
            offScreenDC.Dispose();
        }



        private static int GetColor(double min, double max, double range, double amplitude)
        {
            double color;
            if (min != double.NegativeInfinity && min != double.MaxValue & max != double.PositiveInfinity && max != double.MinValue && range != 0)
            {
                if (min < 0 || max < 0)
                    if (min < 0 && max < 0)
                        color = (255 / range) * (Math.Abs(min) - Math.Abs(amplitude));
                    else
                        if (amplitude < 0)
                            color = (255 / range) * (Math.Abs(min) - Math.Abs(amplitude));
                        else
                            color = (255 / range) * (amplitude + Math.Abs(min));
                else
                    color = (255 / range) * (amplitude - min);
            }
            else
                color = 0;
            return (int)color;
        }


    }
}

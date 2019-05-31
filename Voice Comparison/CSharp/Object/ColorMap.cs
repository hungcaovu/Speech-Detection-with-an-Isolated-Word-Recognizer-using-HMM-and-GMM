using Object.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Object;
using DATA = System.Single;
namespace Object
{
    public class ColorMap
    {
        private Mutex mutex = new Mutex();
        private int colormapLength = 64;
        private int alphaValue = 255;
        private int[,] cmapHot;
        private int[,] cmapJet;
        private int[,] cmapGray;

        public ColorMap()
        {
        }

        public ColorMap(int colorLength)
        {
            colormapLength = colorLength;
            //Thread oThread = new Thread(new ThreadStart(ProcessInit));
            //oThread.Start();
            cmapHot = Hot();
            cmapJet = Jet();
            cmapGray = Gray();
            
        }

        private void ProcessInit(object sender, DoWorkEventArgs e)
        {
            //mutex.WaitOne();
            cmapHot = Hot();
            cmapJet = Jet();
            cmapGray = Gray();
            //mutex.ReleaseMutex();
        }


        public ColorMap(int colorLength, int alpha)
        {
            colormapLength = colorLength;
            alphaValue = alpha;
        }

        public int[,] Spring()
        {
            int[,] cmap = new int[colormapLength, 4];
            DATA[] spring = new DATA[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                spring[i] = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = 255;
                cmap[i, 2] = (int)(255 * spring[i]);
                cmap[i, 3] = 255 - cmap[i, 1];
            }
            return cmap;
        }

        public int[,] Summer()
        {
            int[,] cmap = new int[colormapLength, 4];
            DATA[] summer = new DATA[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                summer[i] = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * summer[i]);
                cmap[i, 2] = (int)(255 * 0.5f * (1 + summer[i]));
                cmap[i, 3] = (int)(255 * 0.4f);
            }
            return cmap;
        }

        public int[,] Autumn()
        {
            int[,] cmap = new int[colormapLength, 4];
            DATA[] autumn = new DATA[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                autumn[i] = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = 255;
                cmap[i, 2] = (int)(255 * autumn[i]);
                cmap[i, 3] = 0;
            }
            return cmap;
        }

        public int[,] Winter()
        {
            int[,] cmap = new int[colormapLength, 4];
            DATA[] winter = new DATA[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                winter[i] = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = 0;
                cmap[i, 2] = (int)(255 * winter[i]);
                cmap[i, 3] = (int)(255 * (1.0f - 0.5f * winter[i]));
            }
            return cmap;
        }

        public int[,] Gray()
        {
            int[,] cmap = new int[colormapLength, 4];
            DATA[] gray = new DATA[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                gray[i] = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * gray[i]);
                cmap[i, 2] = (int)(255 * gray[i]);
                cmap[i, 3] = (int)(255 * gray[i]);
            }
            return cmap;
        }

        public int[,] Jet()
        {
            int[,] cmap = new int[colormapLength, 4];
            DATA[,] cMatrix = new DATA[colormapLength, 3];
            int n = (int)Math.Ceiling(colormapLength / 4.0f);
            int nMod = 0;
            DATA[] fArray = new DATA[3 * n - 1];
            int[] red = new int[fArray.Length];
            int[] green = new int[fArray.Length];
            int[] blue = new int[fArray.Length];

            if (colormapLength % 4 == 1)
            {
                nMod = 1;
            }

            for (int i = 0; i < fArray.Length; i++)
            {
                if (i < n)
                    fArray[i] = (DATA)(i + 1) / n;
                else if (i >= n && i < 2 * n - 1)
                    fArray[i] = 1.0f;
                else if (i >= 2 * n - 1)
                    fArray[i] = (DATA)(3 * n - 1 - i) / n;
                green[i] = (int)Math.Ceiling(n / 2.0f) - nMod + i;
                red[i] = green[i] + n;
                blue[i] = green[i] - n;
            }

            int nb = 0;
            for (int i = 0; i < blue.Length; i++)
            {
                if (blue[i] > 0)
                    nb++;
            }

            for (int i = 0; i < colormapLength; i++)
            {
                for (int j = 0; j < red.Length; j++)
                {
                    if (i == red[j] && red[j] < colormapLength)
                    {
                        cMatrix[i, 0] = fArray[i - red[0]];
                    }
                }
                for (int j = 0; j < green.Length; j++)
                {
                    if (i == green[j] && green[j] < colormapLength)
                        cMatrix[i, 1] = fArray[i - (int)green[0]];
                }
                for (int j = 0; j < blue.Length; j++)
                {
                    if (i == blue[j] && blue[j] >= 0)
                        cMatrix[i, 2] = fArray[fArray.Length - 1 - nb + i];
                }
            }

            for (int i = 0; i < colormapLength; i++)
            {
                cmap[i, 0] = alphaValue;
                for (int j = 0; j < 3; j++)
                {
                    cmap[i, j + 1] = (int)(cMatrix[i, j] * 255);
                }
            }
            return cmap;
        }

        public int[,] Hot()
        {
            int[,] cmap = new int[colormapLength, 4];
            int n = 3 * colormapLength / 8;
            DATA[] red = new DATA[colormapLength];
            DATA[] green = new DATA[colormapLength];
            DATA[] blue = new DATA[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                if (i < n)
                    red[i] = 1.0f * (i + 1) / n;
                else
                    red[i] = 1.0f;
                if (i < n)
                    green[i] = 0f;
                else if (i >= n && i < 2 * n)
                    green[i] = 1.0f * (i + 1 - n) / n;
                else
                    green[i] = 1f;
                if (i < 2 * n)
                    blue[i] = 0f;
                else
                    blue[i] = 1.0f * (i + 1 - 2 * n) / (colormapLength - 2 * n);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * red[i]);
                cmap[i, 2] = (int)(255 * green[i]);
                cmap[i, 3] = (int)(255 * blue[i]);
            }
            return cmap;
        }

        public int[,] Cool()
        {
            int[,] cmap = new int[colormapLength, 4];
            float[] cool = new float[colormapLength];
            for (int i = 0; i < colormapLength; i++)
            {
                cool[i] = 1.0f * i / (colormapLength - 1);
                cmap[i, 0] = alphaValue;
                cmap[i, 1] = (int)(255 * cool[i]);
                cmap[i, 2] = (int)(255 * (1 - cool[i]));
                cmap[i, 3] = 255;
            }
            return cmap;
        }

        public Color DrawColorMap(DATA ymin, DATA ymax, DATA y, ColorStyle style = ColorStyle.GRAY)
        {
            Color color = Color.White;
            switch (style)
            {
                case ColorStyle.HOT:
                    if (cmapHot != null)
                    {
                        int colorLength = cmapHot.GetLength(0);
                        int cindex = colorLength / 2 + (int)Math.Round(y / ((ymax - ymin) / colorLength));
                        if (cindex < 1)
                            cindex = 1;
                        if (cindex > colorLength)
                            cindex = colorLength;
                        color = Color.FromArgb(cmapHot[cindex - 1, 0], cmapHot[cindex - 1, 1],
                                                      cmapHot[cindex - 1, 2], cmapHot[cindex - 1, 3]);
                    }
                    break;
                case ColorStyle.COLOR:
                    if (cmapJet != null)
                    {
                        int colorLength = cmapJet.GetLength(0);
                        int cindex = colorLength / 2 + (int)Math.Round(y / ((ymax - ymin) / colorLength));
                        if (cindex < 1)
                            cindex = 1;
                        if (cindex > colorLength)
                            cindex = colorLength;
                        color = Color.FromArgb(cmapJet[cindex - 1, 0], cmapJet[cindex - 1, 1],
                                                      cmapJet[cindex - 1, 2], cmapJet[cindex - 1, 3]);
                    }
                    break;
                case ColorStyle.GRAY:
                    if (cmapGray != null)
                    {
                        int colorLength = cmapGray.GetLength(0);
                        int cindex = colorLength / 2 + (int)Math.Round(y / ((ymax - ymin) / colorLength));
                        if (cindex < 1)
                            cindex = 1;
                        if (cindex > colorLength)
                            cindex = colorLength;
                        color = Color.FromArgb(cmapGray[cindex - 1, 0], cmapGray[cindex - 1, 1],
                                                      cmapGray[cindex - 1, 2], cmapGray[cindex - 1, 3]);
                    }
                    break;
            }
            
            
            return color;
        }
    }
}

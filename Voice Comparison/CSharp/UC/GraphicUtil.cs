using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UC
{
    public class GraphicUtil
    {
        static public bool DrawLineChart(Graphics gBit, Rectangle rec, List<double> list, Pen p, int Max, int marginHight = 20, int marginWidth = 20)
        {
            //Debug.WriteLine(" Start Draw Pitch X : {0} Y {1} ", rec.X, rec.Y);
            double X = (double)marginWidth / 2 + (double)rec.X;
            double Y = (double)rec.Height - (double)marginHight / 2 + (double)rec.Y;
  
            if (list != null && list.Count > 0)
            {
                double pixelperWidth = (double)(rec.Width - marginWidth) / list.Count;
                //float maxvalue = list.Max();
                double pixelperHight = (double)(rec.Height - marginHight) / Max;
              
                for (int i = 0; i < list.Count; i++)
                {
                    double actY = 0.0f;
                    if (list[i] > 0)
                    {
                        actY = list[i] * pixelperHight;
                    }
                    double actX = i * pixelperWidth;
                    //Debug.WriteLine(" Pitch X : {0} Y: {1} X : {0} Y: {1}", actX, Y - actY, actX + pixelperWidth, Y - actY);
                    gBit.DrawLine(p, (float)(actX + X), (float)(Y - actY), (float)(actX + pixelperWidth + X), (float)(Y - actY));
                }

            }
            else
            {
                return false;
            }


            return true;
        }
        static public void DrawRulerX(Graphics g, Rectangle BandRec, Font TheFont, int col, int incFrameSize, int subline, int WidthLine = 10, bool stringtop = false)
        {

            int RulerHeight = BandRec.Height;
            int RulerWidth = BandRec.Width;
            float posX = (float)BandRec.X;
            float posY = (float)BandRec.Y;
            float pixelPerFrame = (float)RulerWidth / col;

            float posLiney = posY;
            float posLinex = posX;

            float posNumX = posX;

            int count = 0;

            for (int i = 0; i <= col; i++)
            {
                if (i % incFrameSize == 0)
                {
                    // Ve chu o vi tri leng cua chuoi chia hai nhan voi do rong cua size
                    g.DrawString(count.ToString(), TheFont, Brushes.Black, posNumX - (float)(count.ToString().Length) / 2 * TheFont.Size, posLiney + WidthLine / 2, new StringFormat()); // Write number
                    g.DrawLine(Pens.Black, posLinex, posLiney, posLinex, posLiney + WidthLine); // Draw the short line
                    posLinex += pixelPerFrame;
                    posNumX += pixelPerFrame;
                    count += incFrameSize;
                }
                else
                {
                    g.DrawLine(Pens.Black, posLinex, posLiney, posLinex, posLiney + WidthLine / 4); // Draw the short line
                    posLinex += pixelPerFrame;
                    posNumX += pixelPerFrame;
                }
            }
        }
        static public void DrawRulerY(Graphics g, Rectangle BandRec, Font TheFont, int row, int incFrameSize, int subline, int WidthLine = 10, bool stringleft = true)
        {
            float RulerHeight = BandRec.Height;
            float RulerWidth = BandRec.Width;
            float posX = BandRec.X;
            float posY = BandRec.Y;
            float pixelPerBand = RulerHeight / row;

            float posLiney = RulerHeight + posY;
            float posLinex = posX + BandRec.Width - WidthLine;

            float posNumY = RulerHeight + posY + (-TheFont.Height) / 2;

            int count = 0;
            for (int i = 0; i <= row; i++)
            {
                int div = i % incFrameSize;
                if (div  == 0)
                {
                    g.DrawString(count.ToString(), TheFont, Brushes.Black, posLinex - (float)(count.ToString().Length) * TheFont.Height / 2, posNumY, new StringFormat()); // Write number
                    g.DrawLine(Pens.Black, posLinex, posLiney, posLinex + WidthLine, posLiney); // Draw the short line
                    posLiney -= pixelPerBand;
                    posNumY -= pixelPerBand;
                    count += incFrameSize;
                }
                else if (div % (incFrameSize / subline) == 0)
                {
                    g.DrawLine(Pens.Black, posLinex + 3.0f * WidthLine / 4, posLiney, posLinex + WidthLine, posLiney); // Draw the short line
                    posLiney -= pixelPerBand;
                    posNumY -= pixelPerBand;
                }
                else {
                    posLiney -= pixelPerBand;
                    posNumY -= pixelPerBand;
                }
            }
        }
        static public void DrawGrid(Graphics g, Rectangle BandRec, Pen p, Pen ps, int col, int incCol, int row, int incRow, int subrow, int subcol)
        {
            float X1 = BandRec.X;
            float Y1 = BandRec.Y;
            float X2 = BandRec.X + BandRec.Width;
            float Y2 = BandRec.Y + BandRec.Height;
            float pixelIncX = (float)BandRec.Width / col ;
            float pixelIncY = (float)BandRec.Height / row ;

            float rowPer = BandRec.Height / col;
            float y = Y1;
            for (int r = 0; r <= row; r ++) {
                int div = r % incRow;
                if (div == 0)
                {
                    g.DrawLine(p, new PointF(X1, y), new PointF(X2, y));
                    y += pixelIncY;
                }
                else if (div % (incRow / subrow) == 0)
                {
                    g.DrawLine(ps, new PointF(X1, y), new PointF(X2, y));
                    y += pixelIncY;
                }
                else {
                    y += pixelIncY;
                }
                
            }
            float x  =  X1;
            for (int c = 0; c <= col; c += incCol) { 
                
                int div = c % incCol;
                if (div == 0)
                {
                    g.DrawLine(p, new PointF(x, Y1), new PointF(x, Y2));
                    x += pixelIncX;
                }
                else if (div % (incCol / subcol) == 0)
                {
                    g.DrawLine(p, new PointF(x, Y1), new PointF(x, Y2));
                    x += pixelIncX;
                }
                else
                {
                    x += pixelIncX;
                }
            }
        }

        // Draw Chart Support Debug
        static public void DrawBorderRec(Graphics g, Rectangle rec, Pen p) {
            int X1 = rec.X + rec.Width;
            int Y1 = rec.Y + rec.Height;
            g.DrawLine(p, new Point(rec.X, rec.Y), new Point(X1, rec.Y)); // Canh Tren
            g.DrawLine(p, new Point(rec.X, Y1), new Point(X1, Y1)); // Canh Duoi

            g.DrawLine(p, new Point(rec.X, rec.Y), new Point(rec.X, Y1)); // Canh Trai
            g.DrawLine(p, new Point(X1, rec.Y), new Point(X1, Y1)); // Canh Phai
        }

        static public void DrawBorderRec(Graphics g, RectangleF rec, Pen p)
        {
            float X1 = rec.X + rec.Width;
            float Y1 = rec.Y + rec.Height;
            g.DrawLine(p, new PointF(rec.X, rec.Y), new PointF(X1, rec.Y)); // Canh Tren
            g.DrawLine(p, new PointF(rec.X, Y1), new PointF(X1, Y1)); // Canh Duoi

            g.DrawLine(p, new PointF(rec.X, rec.Y), new PointF(rec.X, Y1)); // Canh Trai
            g.DrawLine(p, new PointF(X1, rec.Y), new PointF(X1, Y1)); // Canh Phai
        }
    }
}

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
using Object.Event;

namespace UC
{
    public partial class NewSlider : UserControl
    {

        private string strLeftImagePath;				// Left Thumb Image Path
        private string strRightImagePath;				// Right Thumb Image Path
        private float fHeightOfThumb;					// Height Of  the Thumb
        private float fWidthOfThumb;					// Width of the Thumb

        private bool bMouseEventThumb1;			// Variable for Thumb1Click
        private bool bMouseEventThumb2;			// Variable for Thumb2Click
        private float fThumb1Point;				// Variable to hold Mouse point on Thumb1
        private float fThumb2Point;				// Variable to hold Mouse point on Thumb2

        private Color clrThumbColor;					// Color of the Thumb, If not Image
        private Color clrInFocusBarColor;				// In Focus Bar Colour
        private Color clrDisabledBarColor;			// Disabled Bar Color


        private uint unSizeOfMiddleBar;				// Thickness of the Middle bar
        private uint unGapFromLeftMargin;			// Gap from the Left Margin to draw the Bar
        private uint unGapFromRightMargin;			// Gap from the Right Margin to draw the Bar

        private Image imImageLeft;				//Variable for Left Image
        private Image imImageRight;				// Variable for Right Image



        private float fLeftCol;					// Left Column
        private float fLeftRow;					// Left Row
        private float fRightCol;					// Right Column
        private float fRightRow;					// Right Row


        private PaintEventArgs ePaintArgs;					// Paint Args


        private PointF[] ptThumbPoints1;				// To Store Thumb Point1
        private PointF[] ptThumbPoints2;				// To Store Thumb2 Point


        private bool bAnimateTheSlider;          // Animate the Control
        private float fThumbPoint1Prev;			// To Store Thumb Point1
        private float fThumbPoint2Prev;			// To Store Thumb2 Point


        /// <summary>
        /// Raise Event Selected time
        /// </summary>
       // private event TimeSelectedEventHandler handler;

        //public event TimeSelectedEventHandler TimeSelectedChanged {
        //    add {
        //        handler += value;
        //    }
        //    remove {
        //        handler -= value;
        //    }
        //}

        public NewSlider()
        {
            InitializeComponent();

            if (null != strLeftImagePath)
            {
                imImageLeft = System.Drawing.Image.FromFile(strLeftImagePath);
            }
            if (null != strRightImagePath)
            {
                imImageRight = System.Drawing.Image.FromFile(strRightImagePath);
            }
           
            strLeftImagePath = null;
            strRightImagePath = null;
            fHeightOfThumb = 20.0f;
            fWidthOfThumb = 10.0f;
            this.BackColor = System.Drawing.Color.LightBlue;
            clrInFocusBarColor = System.Drawing.Color.Magenta;
            clrDisabledBarColor = System.Drawing.Color.White;
            clrThumbColor = System.Drawing.Color.Purple;
            unSizeOfMiddleBar = 3;
            unGapFromLeftMargin = 10;
            unGapFromRightMargin = 10;
           
           
            ptThumbPoints1 = new System.Drawing.PointF[3];
            ptThumbPoints2 = new System.Drawing.PointF[3];

            bMouseEventThumb1 = false;
            bMouseEventThumb2 = false;
            bAnimateTheSlider = false;

            // Creating the Graphics object
            System.Drawing.Graphics myGraphics = this.CreateGraphics();
            // Calculate the Left, Right values based on the Clip region bounds
            RectangleF recRegion = myGraphics.VisibleClipBounds;
            fLeftCol = unGapFromLeftMargin;
            fLeftRow = recRegion.Height / 2.0f;  // To display the Bar in the middle
            fRightCol = recRegion.Width - unGapFromRightMargin;
            fRightRow = fLeftRow;
            fThumb1Point = fLeftCol;
            fThumb2Point = fRightCol;
        }



        private void CalculateValues()
        {
            try
            {
                
                // If there's an image load the Image from the file
                if (null != strLeftImagePath)
                {
                    imImageLeft = System.Drawing.Image.FromFile(strLeftImagePath);
                }
                if (null != strRightImagePath)
                {
                    imImageRight = System.Drawing.Image.FromFile(strRightImagePath);
                }




                
                // This is for Calculating the final Thumb points
                ptThumbPoints1[0].X = fThumb1Point;
                Debug.WriteLine("Not 1 : + {0}", ptThumbPoints1[0].X);
                ptThumbPoints1[0].Y = fLeftRow - 3.0f;
                ptThumbPoints1[1].X = fThumb1Point;
                ptThumbPoints1[1].Y = fLeftRow - 3.0f - fHeightOfThumb;
                ptThumbPoints1[2].X = (fThumb1Point + fWidthOfThumb);
                ptThumbPoints1[2].Y = fLeftRow - 3.0f - fHeightOfThumb / 2.0f;
               
                ptThumbPoints2[0].X = fThumb2Point;
                Debug.WriteLine("Not 1 : + {0}", ptThumbPoints2[0].X);
                ptThumbPoints2[0].Y = fRightRow - 3.0f;
                ptThumbPoints2[1].X = fThumb2Point;
                ptThumbPoints2[1].Y = fRightRow - 3.0f - fHeightOfThumb;
                ptThumbPoints2[2].X = fThumb2Point - fWidthOfThumb;
                ptThumbPoints2[2].Y = fRightRow - 3.0f - fHeightOfThumb / 2.0f;
            }
            catch
            {
                //throw;
                //System.Windows.Forms.MessageBox.Show("An unexpected Error occured.  Please contact the vendor of this control", "Error");
            }
        }
        private void NewSlider_MouseDown(object sender, MouseEventArgs e)
        {
            // If the Mouse is Down and also on the Thumb1
            if (e.X >= ptThumbPoints1[0].X && e.X <= ptThumbPoints1[2].X &&
                e.Y >= ptThumbPoints1[1].Y && e.Y <= ptThumbPoints1[0].Y)
            {
                bMouseEventThumb1 = true;
            }
            // Else If the Mouse is Down and also on the Thumb2
            else if (e.X >= ptThumbPoints2[2].X && e.X <= ptThumbPoints2[0].X &&
                e.Y >= ptThumbPoints2[1].Y && e.Y <= ptThumbPoints2[0].Y)
            {
                bMouseEventThumb2 = true;
            }
            bAnimateTheSlider = false;
        }

        private void NewSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (bMouseEventThumb1 && e.Button == System.Windows.Forms.MouseButtons.Left && e.X >= fLeftCol)
            {
                if (fThumb2Point - 1/2 * fWidthOfThumb > e.X)
                {
                    fThumb1Point = e.X - fWidthOfThumb / 2;
                    OnPaint(ePaintArgs);
                }
                else
                {
                    bMouseEventThumb1 = false;
                }
            }
            //Else If the Mouse is moved pressing the left button on Thumb2
            else if (bMouseEventThumb2 && e.Button == System.Windows.Forms.MouseButtons.Left && e.X <= fRightCol)
            {

                if (fThumb1Point + 3/2*fWidthOfThumb < e.X)
                {
                    fThumb2Point = e.X - fWidthOfThumb/2;
                    OnPaint(ePaintArgs);
                }
                else
                {
                    bMouseEventThumb2 = false;
                }
            }
        }

        private void OnPaintDrawSliderAndBar(System.Drawing.Graphics myGraphics, PaintEventArgs e)
        {
            System.Drawing.Brush brSolidBrush;
            System.Drawing.Pen myPen;

            // If Interesting mouse event happened on the Thumb1 Draw Thumb1
            if (bMouseEventThumb1)
            {
                brSolidBrush = new System.Drawing.SolidBrush(this.BackColor);
                if (null != strLeftImagePath)
                {
                    myGraphics.FillRectangle(brSolidBrush, ptThumbPoints1[0].X, ptThumbPoints1[1].Y, fWidthOfThumb, fHeightOfThumb);
                }
                else
                {
                    myGraphics.FillClosedCurve(brSolidBrush, ptThumbPoints1, System.Drawing.Drawing2D.FillMode.Winding, 0f);
                }
            }
            //if interesting mouse event happened on Thumb2 draw thumb2
            if (bMouseEventThumb2)
            {
                brSolidBrush = new System.Drawing.SolidBrush(this.BackColor);

                if (null != strRightImagePath)
                {
                    myGraphics.FillRectangle(brSolidBrush, ptThumbPoints2[2].X, ptThumbPoints2[1].Y, fWidthOfThumb, fHeightOfThumb);
                }
                else
                {
                    myGraphics.FillClosedCurve(brSolidBrush, ptThumbPoints2, System.Drawing.Drawing2D.FillMode.Winding, 0f);
                }
            }

            // The Below lines are to draw the Thumb and the Lines 
            // The Infocus and the Disabled colors are drawn properly based
            // onthe  calculated values
            //brSolidBrush = new System.Drawing.SolidBrush(clrInFocusRangeLabelColor);
            //myPen = new System.Drawing.Pen(clrInFocusRangeLabelColor, unSizeOfMiddleBar);
            //Debug.WriteLine("Nut 1 : + {0}", fThumb1Point);
            // Nut 1
            ptThumbPoints1[0].X = fThumb1Point;
            ptThumbPoints1[1].X = fThumb1Point;
            ptThumbPoints1[2].X = fThumb1Point + fWidthOfThumb;
            // Nut 2
            //Debug.WriteLine("Nut 2 : + {0}", fThumb2Point);
            ptThumbPoints2[0].X = fThumb2Point;
            ptThumbPoints2[1].X = fThumb2Point;
            ptThumbPoints2[2].X = fThumb2Point - fWidthOfThumb;

            myPen = new System.Drawing.Pen(clrDisabledBarColor, unSizeOfMiddleBar);
            myGraphics.DrawLine(myPen, fLeftCol, ptThumbPoints1[2].Y, fThumb1Point, ptThumbPoints1[2].Y);

            //myGraphics.DrawLine(myPen, fLeftCol, ptThumbPoints1[2].Y, fLeftCol, ptThumbPoints1[2].Y + fntLabelFont.SizeInPoints);
            //myGraphics.DrawLine(myPen, fRightCol, ptThumbPoints1[2].Y, fRightCol, ptThumbPoints1[2].Y + fntLabelFont.SizeInPoints);

            //brSolidBrush = new System.Drawing.SolidBrush(clrStringOutputFontColor);
            //myGraphics.DrawString(strRangeString, fntRangeOutputStringFont, brSolidBrush, fLeftCol, fLeftRow * 2 - fntRangeOutputStringFont.Size - 3);

            myPen = new System.Drawing.Pen(clrInFocusBarColor, unSizeOfMiddleBar);
            myGraphics.DrawLine(myPen, ptThumbPoints1[2].X, ptThumbPoints1[2].Y, fThumb2Point,/* - fWidthOfThumb*/ ptThumbPoints1[2].Y);

            myPen = new System.Drawing.Pen(clrDisabledBarColor, unSizeOfMiddleBar);
            myGraphics.DrawLine(myPen, fThumb2Point, ptThumbPoints2[2].Y, fRightCol, ptThumbPoints2[2].Y);

            // If the Thumb is an Image it draws the Image or else it draws the Thumb
            if (null != strLeftImagePath)
            {
                myGraphics.DrawImage(imImageLeft, ptThumbPoints1[0].X, ptThumbPoints1[1].Y, fWidthOfThumb, fHeightOfThumb);
            }
            else
            {
                brSolidBrush = new System.Drawing.SolidBrush(clrThumbColor);
                myGraphics.FillClosedCurve(brSolidBrush, ptThumbPoints1, System.Drawing.Drawing2D.FillMode.Winding, 0f);
            }

            // If the Thumb is an Image it draws the Image or else it draws the Thumb
            if (null != strRightImagePath)
            {
                myGraphics.DrawImage(imImageRight, ptThumbPoints2[2].X, ptThumbPoints2[1].Y, fWidthOfThumb, fHeightOfThumb);
            }
            else
            {
                brSolidBrush = new System.Drawing.SolidBrush(clrThumbColor);
                myGraphics.FillClosedCurve(brSolidBrush, ptThumbPoints2, System.Drawing.Drawing2D.FillMode.Winding, 0f);
            }

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            {
                try
                {
                    // Declaration of the local variables that are used.
                    System.Drawing.Brush brSolidBrush;
                   // float fDividerCounter;
                   // float fIsThumb1Crossed, fIsThumb2Crossed;

                    // Initialization of the local variables.
                    System.Drawing.Graphics myGraphics = this.CreateGraphics();
                    ePaintArgs = e;
                    //fDividerCounter = 0;
                    brSolidBrush = new System.Drawing.SolidBrush(clrDisabledBarColor);



                    if (bAnimateTheSlider)
                    {
                        float fTempThumb1Point = fThumb1Point;
                        float fTempThumb2Point = fThumb2Point;
                        int nToMakeItTimely = System.Environment.TickCount;

                        for (fThumb1Point = fThumbPoint1Prev, fThumb2Point = fThumbPoint2Prev;
                            fThumb1Point <= fTempThumb1Point || fThumb2Point >= fTempThumb2Point;
                            fThumb1Point += 3.0f, fThumb2Point -= 3.0f)
                        {
                            bMouseEventThumb1 = true;
                            bMouseEventThumb2 = true;

                            if (fThumb1Point > fTempThumb1Point)
                            {
                                fThumb1Point = fTempThumb1Point;
                            }

                            if (fThumb2Point < fTempThumb2Point)
                            {
                                fThumb2Point = fTempThumb2Point;
                            }

                            OnPaintDrawSliderAndBar(myGraphics, e);
                            if (System.Environment.TickCount - nToMakeItTimely >= 1000)
                            {
                                // Hey its not worth having animation for more than 1 sec.  
                                break;
                            }
                            System.Threading.Thread.Sleep(1);
                        }

                        fThumb1Point = fTempThumb1Point;
                        fThumb2Point = fTempThumb2Point;
                        bMouseEventThumb1 = true;
                        bMouseEventThumb2 = true;
                        OnPaintDrawSliderAndBar(myGraphics, e);

                        bAnimateTheSlider = false;
                        bMouseEventThumb1 = false;
                        bMouseEventThumb2 = false;
                        OnPaintDrawSliderAndBar(myGraphics, e);
                    }
                    else
                    {
                        OnPaintDrawSliderAndBar(myGraphics, e);
                    }

                    // calling the base class.
                    base.OnPaint(e);
                }
                catch
                {
                    //System.Windows.Forms.MessageBox.Show("An Unexpected Error occured. Please contact the tool vendor", "Error!");
                    //throw;
                }
            }
        }



        private void NewSlider_MouseUp(object sender, MouseEventArgs e)
        {
            // If the Mouse is Up then set the Event to false
            bMouseEventThumb1 = false;
            bMouseEventThumb2 = false;

            // Storing these values for animating the slider
            fThumbPoint1Prev = fThumb1Point;
            fThumbPoint2Prev = fThumb2Point;
            Debug.WriteLine("Mouse Up 1 : + {0}", ptThumbPoints1[0].X);
            CalculateValues();
            bAnimateTheSlider = true;
            this.Refresh();



        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections;
using System.Xml;

namespace UC
{
    //[ToolboxBitmap(typeof(UC.Slider), "RangeScale.bmp")]
    public partial class Slider : UserControl
    {
        /// <ControlVariables>
        /// The Below are Design time (Also Runtime) Control Variables.  These variables 
        /// can be used by the client to change the appearance of the control.  These are
        /// private varibles.  The user of the control will be using the public properties
        /// to change/modify the values.
        /// </ControlVariables>
        /// 
        #region Design Time Control Variables -- Private Variables

        //private string strXMLFileName;					// XML File Name that is used for picking up the Label Values
        //private string strRangeString;					// The String that is displayed at the bottom of the control.  
        //private string strRange;						// An alternate to the XML File Name where the Range Label values are stored
        //private Font fntLabelFont;					// Font of the Label
        //private FontStyle fntLabelFontStyle;				// Font Style of the Label 
        //private float fLabelFontSize;					// Size of the Label 
        //private FontFamily fntLabelFontFamily;				// Font Family of the Label 
        private string strLeftImagePath;				// Left Thumb Image Path
        private string strRightImagePath;				// Right Thumb Image Path
        private float fHeightOfThumb;					// Height Of  the Thumb
        private float fWidthOfThumb;					// Width of the Thumb

        private Color clrThumbColor;					// Color of the Thumb, If not Image
        private Color clrInFocusBarColor;				// In Focus Bar Colour
        private Color clrDisabledBarColor;			// Disabled Bar Color
        private Color clrInFocusRangeLabelColor;		// In Focus Range Label Color
        private Color clrDisabledRangeLabelColor;		// Disabled Range label Color
        private uint unSizeOfMiddleBar;				// Thickness of the Middle bar
        private uint unGapFromLeftMargin;			// Gap from the Left Margin to draw the Bar
        private uint unGapFromRightMargin;			// Gap from the Right Margin to draw the Bar
        private string strDelimiter;					// Delimiter used to seperate the Labels in strRange variable
        private string strRange1;						// Thumb 1 Position bar
        private string strRange2;						// Thumb 2 Position in the bar
        private string strRange1Temp;
        private string strRange2Temp;
        private Font fntRangeOutputStringFont;		// Range Output string font
        private float fStringOutputFontSize;			// String Output Font Size
        private Color clrStringOutputFontColor;		// Color of the Output Font 
        private FontFamily fntStringOutputFontFamily;		// Font Family to display the Range string

        /// <ControlVariables>
        /// The Above are Design time Control Variables.  These variables can be used by the client
        /// to change the appearance of the control.
        /// </ControlVariables>
        /// 
        #endregion

        /// <ControlProperties>
        /// The Below are Design time (Also Runtime) Control Variable properties.  These variables 
        /// can be used by the client to change the appearance of the control.
        /// </ControlProperties>
        /// 

        #region Design Time Control Properties -- Public -- Design Time User properites  - Can also be changed runtime
        /// <XMLFileName>
        /// XMLFileName is a property that can be used to set the Range Labels
        /// For Example:
        /// <?xml version="1.0" encoding="utf-8" ?>
        /// <RangeController>
        ///		<Values>
        /// 		<Value> Excellent</Value>
        /// 		<Value> Good</Value>
        /// 		<Value> Fair</Value>
        /// 		<Value> Poor</Value>
        ///		</Values>
        /// </RangeController>
        /// 
        /// Here the values Excellent, Good, Fair and Poor will be taken as Labels for the 
        /// Control.  
        /// </XMLFileName>
        /// 
        //public string XMLFileName
        //{
        //    set
        //    {
        //        try
        //        {
        //            strXMLFileName = value;

        //            if (null != strXMLFileName)
        //            {
        //                xmlTextReader = new System.Xml.XmlTextReader(strXMLFileName);
        //                strRange = null;
        //                while (xmlTextReader.Read())
        //                {
        //                    switch (xmlTextReader.NodeType)
        //                    {
        //                        case System.Xml.XmlNodeType.Text:
        //                            strRange += xmlTextReader.Value.Trim();
        //                            strRange += strDelimiter;
        //                            break;
        //                    }
        //                }
        //                strRange = strRange.Remove(strRange.Length - strDelimiter.Length, strDelimiter.Length);

        //                CalculateValues();
        //                this.Refresh();
        //                OnPaint(ePaintArgs);
        //            }
        //        }
        //        catch
        //        {
        //            strXMLFileName = null;
        //            //strRange = "";
        //            //CalculateValues();
        //            //this.Refresh();
        //            //OnPaint(ePaintArgs);

        //            System.Windows.Forms.MessageBox.Show("The XML Path entered may be invalid (or) The XML file is not well formed", "Error!");
        //        }
        //    }

        //    get
        //    {
        //        return strXMLFileName;

        //    }
        //}


        /// <RangeString>
        /// RangeString is a property that can be used to set the Range String
        /// This is the string that is displayed at the bottom of the control
        /// </RangeString>
        /// 

        //public string RangeString
        //{
        //    set
        //    {
        //        strRangeString = value;
        //        CalculateValues();
        //        this.Refresh();
        //        OnPaint(ePaintArgs);
        //    }

        //    get
        //    {
        //        return strRangeString;
        //    }
        //}

        /// <RangeValues>
        /// Range Values are the values displayed as labels.  These values can be given by the user
        /// seperated by a Delimiter (Usually a comma ',');
        /// </RangeValues>
        /// 

        //public string RangeValues
        //{
        //    set
        //    {
        //        // Splitting the Range Value to display in the control
        //        strSplitLabels = strRange.Split(strDelimiter.ToCharArray(), 1024);
        //        nNumberOfLabels = strSplitLabels.Length;
        //        strRange = value;

        //        strRange1 = strRange1Temp;
        //        strRange2 = strRange2Temp;

        //        CalculateValues();
        //        this.Refresh();
        //        OnPaint(ePaintArgs);
        //    }

        //    get
        //    {
        //        return strRange;
        //    }
        //}


        /// <LabelFont>
        /// The user can specify the font to use for the labels. The Setter and getter methods are as below
        /// </Label Font>
        /// 
        //public Font LabelFont
        //{
        //    set
        //    {
        //        fntLabelFont = value;

        //        CalculateValues();
        //        this.Refresh();
        //        OnPaint(ePaintArgs);

        //    }

        //    get
        //    {
        //        return fntLabelFont;
        //    }
        //}

        /// <LeftThumbImagepath>
        /// The user can specify the Left Thumb Image path to use. The Setter and getter methods are as below
        /// </LeftThumbImagePath>
        /// 

        public string LeftThumbImagePath
        {
            set
            {
                try
                {
                    strLeftImagePath = value;

                    CalculateValues();
                    this.Refresh();
                    OnPaint(ePaintArgs);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("Invalid Image Path.  Please Re-Enter", "Error!");
                }
            }

            get
            {
                return strLeftImagePath;
            }
        }

        /// <RightThumbImagepath>
        /// The user can specify the Right Thumb Image path to use. The Setter and getter methods are as below
        /// </RightThumbImagePath>
        /// 

        public string RightThumbImagePath
        {
            set
            {
                try
                {
                    strRightImagePath = value;

                    CalculateValues();
                    this.Refresh();
                    OnPaint(ePaintArgs);
                }
                catch
                {
                    strRightImagePath = null;

                    System.Windows.Forms.MessageBox.Show("Invalid Image Path.  Please Re-Enter", "Error!");
                }
            }

            get
            {
                return strRightImagePath;
            }
        }


        /// <HeightOfThumb>
        /// The user can specify the Height of the Thumb Image path to use. The Setter and getter methods are as below
        /// </HeightOfThumb>
        /// 	   

        public float HeightOfThumb
        {
            set
            {
                fHeightOfThumb = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return fHeightOfThumb;
            }

        }

        /// <WidthOfThumb>
        /// The user can specify the Width of the Thumb Image path to use. The Setter and getter methods are as below
        /// </WidthOfThumb>
        /// 	   

        public float WidthOfThumb
        {
            set
            {
                fWidthOfThumb = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return fWidthOfThumb;
            }

        }

        /// <InFocusBarColor>
        /// The user can specify the Infocus Bar Color to use. The Setter and getter methods are as below
        /// </InFocusBarColor>
        /// 


        public Color InFocusBarColor
        {
            set
            {
                clrInFocusBarColor = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return clrInFocusBarColor;
            }

        }

        /// <DisabledBarColor>
        /// The user can specify the Disabled Bar Color to use. The Setter and getter methods are as below
        /// </DisabledBarColor>
        /// 

        public Color DisabledBarColor
        {
            set
            {
                clrDisabledBarColor = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return clrDisabledBarColor;
            }

        }

        /// <ThumbColor>
        /// The user can specify the Thumb Color to use. The Setter and getter methods are as below
        /// </ThumbColor>
        /// 

        public Color ThumbColor
        {
            set
            {
                clrThumbColor = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return clrThumbColor;

            }
        }


        /// <InFocusRangeLabelColor>
        /// The user can specify the InFocus Range Label Color to use. The Setter and getter methods are as below
        /// </InFocusRangeLabelColor>
        /// 

        public Color InFocusRangeLabelColor
        {
            set
            {
                clrInFocusRangeLabelColor = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return clrInFocusRangeLabelColor;
            }
        }

        /// <DisabledRangeLabelColor>
        /// The user can specify the InFocus Range Label Color to use. The Setter and getter methods are as below
        /// </DisabledRangeLabelColor>
        /// 

        public Color DisabledRangeLabelColor
        {
            set
            {
                clrDisabledRangeLabelColor = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return clrDisabledRangeLabelColor;
            }
        }

        /// <SizeOfMiddleBar>
        /// The user can specify the Sizeof Middle Bar to use. The Setter and getter methods are as below
        /// </SizeOfMiddleBar>
        /// 

        public uint MiddleBarWidth
        {
            set
            {
                unSizeOfMiddleBar = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return unSizeOfMiddleBar;
            }
        }

        /// <GapFromLeftMargin>
        /// The user can specify the Gap from Left margin. The Setter and getter methods are as below
        /// </GapFromLeftMargin>
        /// 	

        public uint GapFromLeftMargin
        {
            set
            {
                unGapFromLeftMargin = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return unGapFromLeftMargin;
            }
        }

        /// <GapFromRightMargin>
        /// The user can specify the Gap from Left margin. The Setter and getter methods are as below
        /// </GapFromRightMargin>
        /// 	

        public uint GapFromRightMargin
        {
            set
            {
                unGapFromRightMargin = value;

                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return unGapFromRightMargin;
            }
        }

        /// <DelimeterForRange>
        /// The user can specify the Delimiter for the Range Values. The Setter and getter methods are as below
        /// </DelimeterForRange>
        /// 	

        public string DelimiterForRange
        {
            set
            {
                try
                {
                    strDelimiter = value;
                    string strTempString;
                    strTempString = ")*~`!@#/?\"'][{}=-_+&^%$\\|";
                    if (!strDelimiter.Equals(strDelimiter.TrimStart(strTempString.ToCharArray())))
                    {
                        System.Windows.Forms.MessageBox.Show("The Delimiter specified is not right", "Error!");
                        strDelimiter = ",";
                    }

                    CalculateValues();
                    this.Refresh();
                    OnPaint(ePaintArgs);
                }
                catch
                {
                    System.Windows.Forms.MessageBox.Show("The Delimiter specified is not right", "Error!");
                    strDelimiter = ",";
                }
            }

            get
            {
                return strDelimiter;
            }
        }

        /// <Range1>
        /// The user can specify the Range1 Value. The Setter and getter methods are as below
        /// </Range1>
        /// 	

        //public string Range1
        //{
        //    set
        //    {

        //        strRange1Temp = value;
        //        if (strSplitLabels.Length != 0)
        //        {

        //            strRange1 = value;
        //            CalculateValues();
        //            this.Refresh();
        //            OnPaint(ePaintArgs);
        //        }
        //    }

        //    get
        //    {
        //        return strRange1Temp;
        //    }
        //}

        /// <Range2>
        /// The user can specify the Range2 Value. The Setter and getter methods are as below
        /// </Range2>
        /// 	

        //public string Range2
        //{
        //    set
        //    {
        //        strRange2Temp = value;

        //        if (strSplitLabels.Length != 0)
        //        {
        //            strRange2 = value;
        //            CalculateValues();
        //            this.Refresh();
        //            OnPaint(ePaintArgs);
        //        }
        //    }

        //    get
        //    {
        //        return strRange2Temp;
        //    }
        //}

        /// <OutputStringFontColor>
        /// The user can specify the Output String Font Color Value. The Setter and getter methods are as below
        /// </OutputStringFontColor>
        /// 	
        public Color OutputStringFontColor
        {
            set
            {
                clrStringOutputFontColor = value;
                CalculateValues();
                this.Refresh();
                OnPaint(ePaintArgs);
            }

            get
            {
                return clrStringOutputFontColor;
            }
        }

        /// <ControlProperties>
        /// The Above are Design time (Also Runtime) Control Variable properties.  These variables 
        /// can be used by the client to change the appearance of the control.
        /// </ControlProperties>
        /// 
        #endregion

        /// <ProgramVariables>
        /// The Below are Variables used for computation.  
        /// </ProgramVariables>
        /// 

        #region Variables Used for Computation
        private Image imImageLeft;				//Variable for Left Image
        private Image imImageRight;				// Variable for Right Image
        private NotifyClient objNotifyClient;			// This is For Client Notification object
        private bool bMouseEventThumb1;			// Variable for Thumb1Click
        private bool bMouseEventThumb2;			// Variable for Thumb2Click
        private float fThumb1Point;				// Variable to hold Mouse point on Thumb1
        private float fThumb2Point;				// Variable to hold Mouse point on Thumb2

        private float fLeftCol;					// Left Column
        private float fLeftRow;					// Left Row
        private float fRightCol;					// Right Column
        private float fRightRow;					// Right Row
        private float fTotalWidth;				// Total Width
        private float fDividedWidth;				// Divided Width

        private PaintEventArgs ePaintArgs;					// Paint Args
        private int nNumberOfLabels;			// Total Number of Labels
        private string[] strSplitLabels;				// To store the Split Labels
        private PointF[] ptThumbPoints1;				// To Store Thumb Point1
        private PointF[] ptThumbPoints2;				// To Store Thumb2 Point
        private XmlTextReader xmlTextReader;				// XML Reader Class

        private bool bAnimateTheSlider;          // Animate the Control
        private float fThumbPoint1Prev;			// To Store Thumb Point1
        private float fThumbPoint2Prev;			// To Store Thumb2 Point


        #endregion
        /// <ProgramVariables>
        /// The Below are Variables used for computation.  
        /// </ProgramVariables>
        /// 

        private System.ComponentModel.Container components = null;

        public Slider()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();


            /// <VariableInit>
            /// The Below are Initialization of Variables to its Default values.  
            /// </VariableInit>
            /// 

            #region Initialization of Variables to its Default Values

            if (null != strLeftImagePath)
            {
                imImageLeft = System.Drawing.Image.FromFile(strLeftImagePath);
            }
            if (null != strRightImagePath)
            {
                imImageRight = System.Drawing.Image.FromFile(strRightImagePath);
            }
            objNotifyClient = null;
            //strRangeString = "Range";
            //strDelimiter = ",";  // Because in Germany decimal point is represented as , i.e., "10.50 in US" is "10,50 in Germany"
            //strRange = "0,10,20,30,Good,50,60,70,Great,90,100";
            //strRange1 = "10";
            //strRange2 = "90";
            strLeftImagePath = null;
            strRightImagePath = null;
            fHeightOfThumb = 20.0f;
            fWidthOfThumb = 10.0f;
            this.BackColor = System.Drawing.Color.LightBlue;
            clrInFocusBarColor = System.Drawing.Color.Magenta;
            clrDisabledBarColor = System.Drawing.Color.Gray;
            clrInFocusRangeLabelColor = System.Drawing.Color.Green;
            clrDisabledRangeLabelColor = System.Drawing.Color.Gray;
            clrThumbColor = System.Drawing.Color.Purple;
            fStringOutputFontSize = 10.0f;
            clrStringOutputFontColor = System.Drawing.Color.Black;
            fntStringOutputFontFamily = System.Drawing.FontFamily.GenericSerif;
            fntRangeOutputStringFont = new System.Drawing.Font(fntStringOutputFontFamily, fStringOutputFontSize, System.Drawing.FontStyle.Bold);

            unSizeOfMiddleBar = 3;
            unGapFromLeftMargin = 10;
            unGapFromRightMargin = 10;
            fntLabelFontFamily = System.Drawing.FontFamily.GenericSansSerif;
            fLabelFontSize = 8.25f;
            fntLabelFontStyle = System.Drawing.FontStyle.Bold;
            fntLabelFont = new System.Drawing.Font(fntLabelFontFamily, fLabelFontSize, fntLabelFontStyle);

            strSplitLabels = new string[1024];
            ptThumbPoints1 = new System.Drawing.PointF[3];
            ptThumbPoints2 = new System.Drawing.PointF[3];

            bMouseEventThumb1 = false;
            bMouseEventThumb2 = false;
            bAnimateTheSlider = false;
            #endregion

            /// <VariableInit>
            /// The Below are Initialization of Variables to its Default values.  
            /// </VariableInit>
            /// 

        }


        /// <InterfacesExposed>
        /// The Below are Interfaces/Methods exposed to the client
        /// </InterfacesExposed>
        /// 
        #region Methods Exposed to client at runtime

        /// <QueryRange>
        /// The client can query this method to get the range
        /// </QueryRange>
        /// 
        //public void QueryRange(out string strGetRange1, out string strGetRange2)
        //{
        //    strGetRange1 = strRange1.ToString();
        //    strGetRange2 = strRange2.ToString();
        //}

        /// <RegisterForChangeEvent>
        /// The client can Register  for automatic update whenever the values are changing
        /// </RegisterForChangeEvent>
        /// 
        public void RegisterForChangeEvent(ref NotifyClient refNotifyClient)
        {
            // If there's a valid object, the values are copied.
            try
            {
                if (null != refNotifyClient)
                {
                    objNotifyClient = refNotifyClient;
                    objNotifyClient.Range1 = strRange1;
                    objNotifyClient.Range2 = strRange2;
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("The Registered Event object has a Bad memory.  Please correct it", "Error!");
            }
        }
        #endregion

        /// <CalculateValues>
        /// The below is the method that calculates the values to be place while painting
        /// </CalculateValues>
        /// 
        #region This is a Private method that calculates the values to be placed while painting
        private void CalculateValues()
        {
            try
            {
                // Creating the Graphics object
                System.Drawing.Graphics myGraphics = this.CreateGraphics();

                // Split the Labels to be displayed below the Bar
                strSplitLabels = strRange.Split(strDelimiter.ToCharArray(), 1024);
                nNumberOfLabels = strSplitLabels.Length;

                // If there's an image load the Image from the file
                if (null != strLeftImagePath)
                {
                    imImageLeft = System.Drawing.Image.FromFile(strLeftImagePath);
                }
                if (null != strRightImagePath)
                {
                    imImageRight = System.Drawing.Image.FromFile(strRightImagePath);
                }

                // Calculate the Left, Right values based on the Clip region bounds
                RectangleF recRegion = myGraphics.VisibleClipBounds;
                fLeftCol = unGapFromLeftMargin;
                fLeftRow = recRegion.Height / 2.0f;  // To display the Bar in the middle
                fRightCol = recRegion.Width - unGapFromRightMargin;
                fRightRow = fLeftRow;
                fThumb1Point = fLeftCol;
                fThumb2Point = fRightCol;
                fTotalWidth = recRegion.Width - (unGapFromRightMargin + unGapFromLeftMargin);
                fDividedWidth = fTotalWidth / (float)(nNumberOfLabels - 1);

                int nRangeIndex1Selected = 0;
                int nRangeIndex2Selected = nNumberOfLabels - 1;

                // This is used to calculate the Thumb Point from the  Range1, Range2 Value
                for (int nIndexer = 0; nIndexer < nNumberOfLabels; nIndexer++)
                {
                    if (strRange1.Equals(strSplitLabels[nIndexer]))
                    {
                        fThumb1Point = fLeftCol + fDividedWidth * nIndexer;
                        nRangeIndex1Selected = nIndexer;
                    }
                    if (strRange2.Equals(strSplitLabels[nIndexer]))
                    {
                        fThumb2Point = fLeftCol + fDividedWidth * nIndexer;
                        nRangeIndex2Selected = nIndexer;
                    }
                }

                if (strRange1 == strRange2)
                {
                    if (nRangeIndex1Selected != 0)
                    {
                        fThumb1Point -= fDividedWidth / 2.0f;
                    }
                    if (nRangeIndex2Selected != nNumberOfLabels - 1)
                    {
                        fThumb2Point += fDividedWidth / 2.0f;
                    }
                }

                // This is for Calculating the final Thumb points
                ptThumbPoints1[0].X = fThumb1Point;
                ptThumbPoints1[0].Y = fLeftRow - 3.0f;
                ptThumbPoints1[1].X = fThumb1Point;
                ptThumbPoints1[1].Y = fLeftRow - 3.0f - fHeightOfThumb;
                ptThumbPoints1[2].X = (fThumb1Point + fWidthOfThumb);
                ptThumbPoints1[2].Y = fLeftRow - 3.0f - fHeightOfThumb / 2.0f;

                ptThumbPoints2[0].X = fThumb2Point;
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

        /// <CalculateValues>
        /// The below is the method that calculates the values to be place while painting
        /// </CalculateValues>
        /// 
        #endregion


        /// <Paint >
        /// The below is the method that draws the control on the screen
        /// </Paint >
        /// 
        #region Paint Method Override -- This method draws the control on the screen

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
            brSolidBrush = new System.Drawing.SolidBrush(clrInFocusRangeLabelColor);
            myPen = new System.Drawing.Pen(clrInFocusRangeLabelColor, unSizeOfMiddleBar);

            ptThumbPoints1[0].X = fThumb1Point;
            ptThumbPoints1[1].X = fThumb1Point;
            ptThumbPoints1[2].X = fThumb1Point + fWidthOfThumb;

            ptThumbPoints2[0].X = fThumb2Point;
            ptThumbPoints2[1].X = fThumb2Point;
            ptThumbPoints2[2].X = fThumb2Point - fWidthOfThumb;

            myPen = new System.Drawing.Pen(clrDisabledBarColor, unSizeOfMiddleBar);
            myGraphics.DrawLine(myPen, fLeftCol, ptThumbPoints1[2].Y, fThumb1Point, ptThumbPoints1[2].Y);

            myGraphics.DrawLine(myPen, fLeftCol, ptThumbPoints1[2].Y, fLeftCol, ptThumbPoints1[2].Y + fntLabelFont.SizeInPoints);
            myGraphics.DrawLine(myPen, fRightCol, ptThumbPoints1[2].Y, fRightCol, ptThumbPoints1[2].Y + fntLabelFont.SizeInPoints);

            brSolidBrush = new System.Drawing.SolidBrush(clrStringOutputFontColor);
            myGraphics.DrawString(strRangeString, fntRangeOutputStringFont, brSolidBrush, fLeftCol, fLeftRow * 2 - fntRangeOutputStringFont.Size - 3);

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
                    float fDividerCounter;
                    float fIsThumb1Crossed, fIsThumb2Crossed;
                    string strRangeOutput;
                    string strNewRange1, strNewRange2;

                    // Initialization of the local variables.
                    System.Drawing.Graphics myGraphics = this.CreateGraphics();
                    ePaintArgs = e;
                    fDividerCounter = 0;
                    brSolidBrush = new System.Drawing.SolidBrush(clrDisabledRangeLabelColor);
                    strNewRange1 = null;
                    strNewRange2 = null;

                    // This loop is to draw the Labels on the screen.
                    //for (int nIndexer = 0; nIndexer < nNumberOfLabels; nIndexer++)
                    //{
                    //    fDividerCounter = fLeftCol + fDividedWidth * nIndexer;
                    //    fIsThumb1Crossed = fDividerCounter + strSplitLabels[nIndexer].Length * fntLabelFont.SizeInPoints / 2;
                    //    fIsThumb2Crossed = fDividerCounter - (strSplitLabels[nIndexer].Length - 1) * fntLabelFont.SizeInPoints / 2;
                    //    if (fIsThumb1Crossed >= fThumb1Point && strNewRange1 == null)
                    //    {
                    //        // If Thumb1 Crossed this Label Make it in Focus color
                    //        brSolidBrush = new System.Drawing.SolidBrush(clrInFocusRangeLabelColor);
                    //        strNewRange1 = strSplitLabels[nIndexer];
                    //    }
                    //    if (fIsThumb2Crossed > fThumb2Point)
                    //    {
                    //        // If Thumb2 crossed this draw the labes following this in disabled color
                    //        brSolidBrush = new System.Drawing.SolidBrush(clrDisabledRangeLabelColor);
                    //        //strNewRange2	= strSplitLabels[nIndexer];
                    //    }
                    //    else
                    //    {
                    //        strNewRange2 = strSplitLabels[nIndexer];
                    //    }

                    //    myGraphics.DrawString(strSplitLabels[nIndexer], fntLabelFont, brSolidBrush, fDividerCounter - ((fntLabelFont.SizeInPoints) * strSplitLabels[nIndexer].Length) / 2, fLeftRow);
                    //}

                    // This is to draw exactly the Range String like "Range 10 to 100" 
                    // This will draw the information only if there is a change. 
                    //if //(strNewRange1 != null && strNewRange2 != null &&
                    //    //(!strRange1.Equals(strNewRange1) || !strRange2.Equals(strNewRange2)) ||
                    //    (!bMouseEventThumb1 && !bMouseEventThumb2))
                    //{
                    //    brSolidBrush = new System.Drawing.SolidBrush(this.BackColor);
                    //    strRangeOutput = strRange1 + " - " + strRange2;
                    //    myGraphics.DrawString(strRangeOutput, fntRangeOutputStringFont, brSolidBrush, fLeftCol + fntRangeOutputStringFont.Size * strRangeString.Length, fLeftRow * 2 - fntRangeOutputStringFont.Size - 3);

                    //    brSolidBrush = new System.Drawing.SolidBrush(clrStringOutputFontColor);
                    //    //strRangeOutput = strNewRange1 + " - " + strNewRange2;
                    //    myGraphics.DrawString(strRangeOutput, fntRangeOutputStringFont, brSolidBrush, fLeftCol + fntRangeOutputStringFont.Size * strRangeString.Length, fLeftRow * 2 - fntRangeOutputStringFont.Size - 3);

                    //    //strRange1 = strNewRange1;
                    //    //strRange2 = strNewRange2;
                    //}

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
        /// <Paint >
        /// The Above is the method that draws the control on the screen
        /// </Paint >
        #endregion


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (components != null)
        //            components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Slider
            // 
            this.Name = "Slider";
            this.Size = new System.Drawing.Size(427, 21);
            this.Load += new System.EventHandler(this.RangeSelectorControl_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RangeSelectorControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RangeSelectorControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RangeSelectorControl_MouseUp);
            this.Resize += new System.EventHandler(this.RangeSelectorControl_Resize);
            this.ResumeLayout(false);

        }
        #endregion


        private void RangeSelectorControl_Load(object sender, System.EventArgs e)
        {
            CalculateValues();
        }

        /// <MouseEvents>
        /// The below are the methods used for handling Mouse Events
        /// </Mouse Events>
        /// 
        #region Methods used for handling Mouse Events
        private void RangeSelectorControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // If the Mouse is Up then set the Event to false
            bMouseEventThumb1 = false;
            bMouseEventThumb2 = false;

            // Storing these values for animating the slider
            fThumbPoint1Prev = fThumb1Point;
            fThumbPoint2Prev = fThumb2Point;

            CalculateValues();
            bAnimateTheSlider = true;
            this.Refresh();
        }

        private void RangeSelectorControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
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

        private void RangeSelectorControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // If the Mouse is moved pressing the left button on Thumb1
            if (bMouseEventThumb1 && e.Button == System.Windows.Forms.MouseButtons.Left && e.X >= fLeftCol)
            {
                // The below code is for handlling the Thumb1 Point
                if (strRange1.Equals(strRange2))
                {
                    if (e.X < fThumb1Point)
                    {
                        fThumb1Point = e.X;
                        OnPaint(ePaintArgs);
                    }
                }
                else if (fThumb2Point - fWidthOfThumb > e.X)
                {
                    fThumb1Point = e.X;
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
                // The below code is for handlling the Thumb1 Point
                if (strRange1.Equals(strRange2))
                {
                    if (e.X > fThumb2Point)
                    {
                        fThumb2Point = e.X;
                        OnPaint(ePaintArgs);
                    }
                }
                else if (fThumb1Point + fWidthOfThumb < e.X)
                {
                    fThumb2Point = e.X;
                    OnPaint(ePaintArgs);
                }
                else
                {
                    bMouseEventThumb2 = false;
                }
            }

            // If there is an Object Notification
            if (null != objNotifyClient)
            {
                objNotifyClient.Range1 = strRange1;
                objNotifyClient.Range2 = strRange2;
            }
        }

        /// <MouseEvents>
        /// The below are the methods used for handling Mouse Events
        /// </Mouse Events>
        /// 
        #endregion

        /// <RangeSelectorControl_Resize>
        /// The below are the method is used if the form is resized 
        /// </RangeSelectorControl_Resize>
        /// 
        private void RangeSelectorControl_Resize(object sender, System.EventArgs e)
        {
            CalculateValues();
            this.Refresh();
            OnPaint(ePaintArgs);

        }
    }

    /// <RangeSelectorControl_Resize>
    /// The below is the small Notification class that can be used by the client
    /// </RangeSelectorControl_Resize>
    /// 
    #region Notification class for client to register with the control for changes

    public class NotifyClient
    {
        private string strRange1, strRange2;

        public string Range1
        {
            set
            {
                strRange1 = value;

            }

            get
            {
                return strRange1;
            }
        }

        public string Range2
        {
            set
            {
                strRange2 = value;
            }

            get
            {
                return strRange2;
            }
        }
    }

    /// <RangeSelectorControl_Resize>
    /// The Above is the small Notification class that can be used by the client
    /// </RangeSelectorControl_Resize>
    /// 

    #endregion
}

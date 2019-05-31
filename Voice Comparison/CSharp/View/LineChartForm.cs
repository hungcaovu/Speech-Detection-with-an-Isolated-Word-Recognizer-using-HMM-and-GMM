using Object.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voice_Comparasion
{
    public partial class LineChartForm : Form
    {

        public LineChartForm(FormTag tag)
        {
                InitializeComponent();
        }
        /// <summary>
        /// Get Data and show on chart
        /// </summary>
        public List<double> Data{
            set {
                lineViewer.Data = value;
            }
        }

        /// <summary>
        /// Set Min of values
        /// </summary>
        public int MinValue{
            set {
                lineViewer.MinValue = value;
            }
        }
        /// <summary>
        /// Set max value
        /// </summary>
        public int MaxValue
        {
            set {
                lineViewer.MaxValue = value;
            }
        }

        public int StepValue
        {
            set
            {
                lineViewer.MaxValue = value;
            }
        }
    }
}

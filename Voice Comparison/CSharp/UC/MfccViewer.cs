using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UC
{
    public partial class MfccViewer : UserControl
    {
        //private int countS = 0;
        public MfccViewer()
        {
            InitializeComponent();
        }

        private void AddAChart(List<double> data, string note)
        {
            //countS++;
            //string series = string.Format("MFCC_{0}{1}", note, countS);
            ////_chart.Legends.Add(series);
            ////_chart.Legends[series].LegendStyle = LegendStyle.Column;

            //_chart.Series.Add(series);
            //_chart.Series[series].ChartType = SeriesChartType.Bar;
            //foreach (float val in data)
            //{
            //    _chart.Series[series].Points.AddY(val);
            //}
            //_chart.Series[series].ChartArea = "Series";
            //_chart.Series[series].Legend = "Series";
        }

        private void AddAChart(List<double> data)
        {
            if (InvokeRequired) {
                Action<List<double>> action = new Action<List<double>>(AddAChart);
                Invoke(action, data);
            }
            else
            {
                _chart.Series.Clear();
                _chart.Legends.Clear();
                _chart.Series.Add("MFCC");
                _chart.Series["MFCC"].ChartType = SeriesChartType.Bar;
                foreach (double val in data)
                {
                    _chart.Series["MFCC"].Points.AddY(val);
                }
                _chart.Series["MFCC"].ChartArea = "ChartArea1";
            }
        }

        public List<double> MFCC
        {
            set {
                if (value != null) {
                    AddAChart(value);
                }
            }
        }

        private void clearChart_btn_Click(object sender, EventArgs e)
        {
            _chart.Series.Clear();
            _chart.Legends.Clear();
            //countS = 0;
        }

        private void _chart_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
        {
            e.LegendItems.Clear();
            foreach (var series in this._chart.Series)
            {
                var legendItem = new LegendItem();
                legendItem.SeriesName = series.Name;
                legendItem.ImageStyle = LegendImageStyle.Rectangle;
                legendItem.BorderColor = Color.Transparent;
                legendItem.Name = series.Name + "_legend_item";

                int i = legendItem.Cells.Add(LegendCellType.SeriesSymbol, "", ContentAlignment.MiddleCenter);
                legendItem.Cells.Add(LegendCellType.Text, series.Name, ContentAlignment.MiddleCenter);

                if (series.Enabled)
                    legendItem.Color = series.Color;
                else
                    legendItem.Color = Color.FromArgb(100, series.Color);
                e.LegendItems.Add(legendItem);

            }
        }

    }
}

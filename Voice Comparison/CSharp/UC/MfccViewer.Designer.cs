namespace UC
{
    partial class MfccViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this._chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.clearChart_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._chart)).BeginInit();
            this.SuspendLayout();
            // 
            // _chart
            // 
            this._chart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this._chart.ChartAreas.Add(chartArea1);
            this._chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this._chart.Legends.Add(legend1);
            this._chart.Location = new System.Drawing.Point(0, 0);
            this._chart.Name = "_chart";
            this._chart.Size = new System.Drawing.Size(760, 314);
            this._chart.TabIndex = 0;
            title1.Name = "Title1";
            title1.Text = "MFCC Comparison";
            this._chart.Titles.Add(title1);
            this._chart.CustomizeLegend += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CustomizeLegendEventArgs>(this._chart_CustomizeLegend);
            // 
            // clearChart_btn
            // 
            this.clearChart_btn.Location = new System.Drawing.Point(682, 3);
            this.clearChart_btn.Name = "clearChart_btn";
            this.clearChart_btn.Size = new System.Drawing.Size(75, 23);
            this.clearChart_btn.TabIndex = 1;
            this.clearChart_btn.Text = "Clear Chart";
            this.clearChart_btn.UseVisualStyleBackColor = true;
            this.clearChart_btn.Click += new System.EventHandler(this.clearChart_btn_Click);
            // 
            // MfccViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clearChart_btn);
            this.Controls.Add(this._chart);
            this.Name = "MfccViewer";
            this.Size = new System.Drawing.Size(760, 314);
            ((System.ComponentModel.ISupportInitialize)(this._chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart _chart;
        private System.Windows.Forms.Button clearChart_btn;
    }
}

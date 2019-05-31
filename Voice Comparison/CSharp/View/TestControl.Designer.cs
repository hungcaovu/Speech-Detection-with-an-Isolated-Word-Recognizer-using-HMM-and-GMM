namespace Voice_Comparasion
{
    partial class TestControl
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
            Object.SelectedChartOption selectedChartOption1 = new Object.SelectedChartOption();
            this.yourvoice_lb = new System.Windows.Forms.Label();
            this.refvoice_lb = new System.Windows.Forms.Label();
            this.yourvoice_tb = new System.Windows.Forms.TextBox();
            this.refvoice_tb = new System.Windows.Forms.TextBox();
            this.yourvoice_btn = new System.Windows.Forms.Button();
            this.refvoice_btn = new System.Windows.Forms.Button();
            this.browser_file = new System.Windows.Forms.OpenFileDialog();
            this.selectShowChart = new UC.SelectShowChart();
            this.setting_btn = new System.Windows.Forms.Button();
            this.process_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // yourvoice_lb
            // 
            this.yourvoice_lb.AutoSize = true;
            this.yourvoice_lb.Location = new System.Drawing.Point(55, 29);
            this.yourvoice_lb.Name = "yourvoice_lb";
            this.yourvoice_lb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.yourvoice_lb.Size = new System.Drawing.Size(65, 13);
            this.yourvoice_lb.TabIndex = 12;
            this.yourvoice_lb.Text = "Your Voice :";
            // 
            // refvoice_lb
            // 
            this.refvoice_lb.AutoSize = true;
            this.refvoice_lb.Location = new System.Drawing.Point(55, 68);
            this.refvoice_lb.Name = "refvoice_lb";
            this.refvoice_lb.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.refvoice_lb.Size = new System.Drawing.Size(93, 13);
            this.refvoice_lb.TabIndex = 12;
            this.refvoice_lb.Text = "Reference Voice :";
            // 
            // yourvoice_tb
            // 
            this.yourvoice_tb.Location = new System.Drawing.Point(154, 27);
            this.yourvoice_tb.Name = "yourvoice_tb";
            this.yourvoice_tb.Size = new System.Drawing.Size(176, 20);
            this.yourvoice_tb.TabIndex = 13;
            // 
            // refvoice_tb
            // 
            this.refvoice_tb.Location = new System.Drawing.Point(154, 65);
            this.refvoice_tb.Name = "refvoice_tb";
            this.refvoice_tb.Size = new System.Drawing.Size(176, 20);
            this.refvoice_tb.TabIndex = 13;
            // 
            // yourvoice_btn
            // 
            this.yourvoice_btn.Location = new System.Drawing.Point(354, 25);
            this.yourvoice_btn.Name = "yourvoice_btn";
            this.yourvoice_btn.Size = new System.Drawing.Size(75, 23);
            this.yourvoice_btn.TabIndex = 14;
            this.yourvoice_btn.Text = "Browser";
            this.yourvoice_btn.UseVisualStyleBackColor = true;
            this.yourvoice_btn.Click += new System.EventHandler(this.yourvoice_btn_Click);
            // 
            // refvoice_btn
            // 
            this.refvoice_btn.Location = new System.Drawing.Point(354, 62);
            this.refvoice_btn.Name = "refvoice_btn";
            this.refvoice_btn.Size = new System.Drawing.Size(75, 23);
            this.refvoice_btn.TabIndex = 14;
            this.refvoice_btn.Text = "Browser";
            this.refvoice_btn.UseVisualStyleBackColor = true;
            this.refvoice_btn.Click += new System.EventHandler(this.refvoice_btn_Click);
            // 
            // selectShowChart
            // 
            this.selectShowChart.Location = new System.Drawing.Point(455, 11);
            this.selectShowChart.Name = "selectShowChart";
            selectedChartOption1.RefDetal = false;
            selectedChartOption1.RefDouble = false;
            selectedChartOption1.RefFreq = false;
            selectedChartOption1.RefMfcc = false;
            selectedChartOption1.RefWave = false;
            selectedChartOption1.YourDetal = false;
            selectedChartOption1.YourDouble = false;
            selectedChartOption1.YourFreq = false;
            selectedChartOption1.YourMfcc = false;
            selectedChartOption1.YourWave = false;
            this.selectShowChart.Selected = selectedChartOption1;
            this.selectShowChart.Size = new System.Drawing.Size(123, 211);
            this.selectShowChart.TabIndex = 15;
            // 
            // setting_btn
            // 
            this.setting_btn.Location = new System.Drawing.Point(104, 135);
            this.setting_btn.Name = "setting_btn";
            this.setting_btn.Size = new System.Drawing.Size(75, 23);
            this.setting_btn.TabIndex = 16;
            this.setting_btn.Text = "Settings";
            this.setting_btn.UseVisualStyleBackColor = true;
            this.setting_btn.Click += new System.EventHandler(this.setting_btn_Click);
            // 
            // process_btn
            // 
            this.process_btn.Location = new System.Drawing.Point(286, 135);
            this.process_btn.Name = "process_btn";
            this.process_btn.Size = new System.Drawing.Size(75, 23);
            this.process_btn.TabIndex = 16;
            this.process_btn.Text = "Process";
            this.process_btn.UseVisualStyleBackColor = true;
            this.process_btn.Click += new System.EventHandler(this.process_btn_Click);
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.process_btn);
            this.Controls.Add(this.setting_btn);
            this.Controls.Add(this.selectShowChart);
            this.Controls.Add(this.refvoice_btn);
            this.Controls.Add(this.yourvoice_btn);
            this.Controls.Add(this.refvoice_tb);
            this.Controls.Add(this.yourvoice_tb);
            this.Controls.Add(this.refvoice_lb);
            this.Controls.Add(this.yourvoice_lb);
            this.Name = "TestControl";
            this.Size = new System.Drawing.Size(634, 230);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label yourvoice_lb;
        private System.Windows.Forms.Label refvoice_lb;
        private System.Windows.Forms.TextBox yourvoice_tb;
        private System.Windows.Forms.TextBox refvoice_tb;
        private System.Windows.Forms.Button yourvoice_btn;
        private System.Windows.Forms.Button refvoice_btn;
        private System.Windows.Forms.OpenFileDialog browser_file;
        private UC.SelectShowChart selectShowChart;
        private System.Windows.Forms.Button setting_btn;
        private System.Windows.Forms.Button process_btn;
    }
}

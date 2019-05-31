namespace UC
{
    partial class MainTestControlTest
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
			Object.SelectedChartOption selectedChartOption2 = new Object.SelectedChartOption();
			this.setting_btn = new System.Windows.Forms.Button();
			this.showChart = new UC.SelectShowChart();
			this.process_btn = new System.Windows.Forms.Button();
			this.processMFCCRef = new System.ComponentModel.BackgroundWorker();
			this.processMFCCYour = new System.ComponentModel.BackgroundWorker();
			this.costmfcc_lb = new System.Windows.Forms.Label();
			this.costmfccvalue_lb = new System.Windows.Forms.Label();
			this.costpitch_bl = new System.Windows.Forms.Label();
			this.costpitchvalue_lb = new System.Windows.Forms.Label();
			this._mfccViewer = new UC.MfccViewer();
			this.left_btn = new System.Windows.Forms.Button();
			this.right_btn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// setting_btn
			// 
			this.setting_btn.Location = new System.Drawing.Point(604, 29);
			this.setting_btn.Name = "setting_btn";
			this.setting_btn.Size = new System.Drawing.Size(100, 23);
			this.setting_btn.TabIndex = 8;
			this.setting_btn.Text = "Setting";
			this.setting_btn.UseVisualStyleBackColor = true;
			this.setting_btn.Click += new System.EventHandler(this.setting_btn_Click);
			// 
			// showChart
			// 
			this.showChart.Location = new System.Drawing.Point(730, 80);
			this.showChart.Name = "showChart";
			selectedChartOption2.RefDetal = false;
			selectedChartOption2.RefDouble = false;
			selectedChartOption2.RefFreq = false;
			selectedChartOption2.RefMfcc = false;
			selectedChartOption2.RefPitch = false;
			selectedChartOption2.RefWave = false;
			selectedChartOption2.YourDetal = false;
			selectedChartOption2.YourDouble = false;
			selectedChartOption2.YourFreq = false;
			selectedChartOption2.YourMfcc = false;
			selectedChartOption2.YourPitch = false;
			selectedChartOption2.YourWave = false;
			this.showChart.Selected = selectedChartOption2;
			this.showChart.Size = new System.Drawing.Size(125, 252);
			this.showChart.TabIndex = 13;
			this.showChart.SelectedChart += new Object.Event.SelectedChartEventHandler(this.showChart_SelectedChart);
			// 
			// process_btn
			// 
			this.process_btn.Location = new System.Drawing.Point(755, 28);
			this.process_btn.Name = "process_btn";
			this.process_btn.Size = new System.Drawing.Size(100, 23);
			this.process_btn.TabIndex = 8;
			this.process_btn.Text = "Process";
			this.process_btn.UseVisualStyleBackColor = true;
			this.process_btn.Click += new System.EventHandler(this.process_btn_Click);
			// 
			// costmfcc_lb
			// 
			this.costmfcc_lb.AutoSize = true;
			this.costmfcc_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.costmfcc_lb.Location = new System.Drawing.Point(76, 377);
			this.costmfcc_lb.Name = "costmfcc_lb";
			this.costmfcc_lb.Size = new System.Drawing.Size(65, 24);
			this.costmfcc_lb.TabIndex = 10;
			this.costmfcc_lb.Text = "Score:";
			// 
			// costmfccvalue_lb
			// 
			this.costmfccvalue_lb.AutoSize = true;
			this.costmfccvalue_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.costmfccvalue_lb.Location = new System.Drawing.Point(137, 370);
			this.costmfccvalue_lb.Name = "costmfccvalue_lb";
			this.costmfccvalue_lb.Size = new System.Drawing.Size(67, 31);
			this.costmfccvalue_lb.TabIndex = 11;
			this.costmfccvalue_lb.Text = "0.00";
			// 
			// costpitch_bl
			// 
			this.costpitch_bl.AutoSize = true;
			this.costpitch_bl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.costpitch_bl.Location = new System.Drawing.Point(334, 377);
			this.costpitch_bl.Name = "costpitch_bl";
			this.costpitch_bl.Size = new System.Drawing.Size(56, 24);
			this.costpitch_bl.TabIndex = 10;
			this.costpitch_bl.Text = "Pitch:";
			this.costpitch_bl.Visible = false;
			// 
			// costpitchvalue_lb
			// 
			this.costpitchvalue_lb.AutoSize = true;
			this.costpitchvalue_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.costpitchvalue_lb.Location = new System.Drawing.Point(391, 370);
			this.costpitchvalue_lb.Name = "costpitchvalue_lb";
			this.costpitchvalue_lb.Size = new System.Drawing.Size(67, 31);
			this.costpitchvalue_lb.TabIndex = 11;
			this.costpitchvalue_lb.Text = "0.00";
			this.costpitchvalue_lb.Visible = false;
			// 
			// _mfccViewer
			// 
			this._mfccViewer.Location = new System.Drawing.Point(34, 80);
			this._mfccViewer.Name = "_mfccViewer";
			this._mfccViewer.Size = new System.Drawing.Size(670, 252);
			this._mfccViewer.TabIndex = 12;
			// 
			// left_btn
			// 
			this.left_btn.Location = new System.Drawing.Point(80, 29);
			this.left_btn.Name = "left_btn";
			this.left_btn.Size = new System.Drawing.Size(100, 23);
			this.left_btn.TabIndex = 8;
			this.left_btn.Text = "Left Window";
			this.left_btn.UseVisualStyleBackColor = true;
			this.left_btn.Click += new System.EventHandler(this.left_btn_Click);
			// 
			// right_btn
			// 
			this.right_btn.Location = new System.Drawing.Point(201, 28);
			this.right_btn.Name = "right_btn";
			this.right_btn.Size = new System.Drawing.Size(100, 23);
			this.right_btn.TabIndex = 8;
			this.right_btn.Text = "Right Window";
			this.right_btn.UseVisualStyleBackColor = true;
			this.right_btn.Click += new System.EventHandler(this.right_btn_Click);
			// 
			// MainTestControlTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.costmfccvalue_lb);
			this.Controls.Add(this.costmfcc_lb);
			this.Controls.Add(this.costpitchvalue_lb);
			this.Controls.Add(this.costpitch_bl);
			this.Controls.Add(this.showChart);
			this.Controls.Add(this._mfccViewer);
			this.Controls.Add(this.process_btn);
			this.Controls.Add(this.right_btn);
			this.Controls.Add(this.left_btn);
			this.Controls.Add(this.setting_btn);
			this.Name = "MainTestControlTest";
			this.Size = new System.Drawing.Size(861, 410);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button setting_btn;
        private SelectShowChart showChart;
        private System.Windows.Forms.Button process_btn;
        private System.ComponentModel.BackgroundWorker processMFCCRef;
        private System.ComponentModel.BackgroundWorker processMFCCYour;
        private System.Windows.Forms.Label costmfcc_lb;
        private System.Windows.Forms.Label costmfccvalue_lb;
        private System.Windows.Forms.Label costpitch_bl;
        private System.Windows.Forms.Label costpitchvalue_lb;
        private MfccViewer _mfccViewer;
        private System.Windows.Forms.Button left_btn;
        private System.Windows.Forms.Button right_btn;
    }
}

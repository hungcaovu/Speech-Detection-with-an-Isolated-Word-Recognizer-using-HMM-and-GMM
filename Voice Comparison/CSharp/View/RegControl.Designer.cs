namespace UC
{
    partial class RegControl
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.devices_cbx = new System.Windows.Forms.ComboBox();
            this.devices_lb = new System.Windows.Forms.Label();
            this.sampleRate_cbx = new System.Windows.Forms.ComboBox();
            this.sampleRate_lb = new System.Windows.Forms.Label();
            this.freshDevices_btn = new System.Windows.Forms.Button();
            this.waveViewer = new UC.WaveViewer();
            this.yourvoice_lb = new System.Windows.Forms.Label();
            this.recoder_btn = new System.Windows.Forms.Button();
            this.playStopyour_btn = new System.Windows.Forms.Button();
            this.setting_btn = new System.Windows.Forms.Button();
            this.showChart = new UC.SelectShowChart();
            this.process_btn = new System.Windows.Forms.Button();
            this.processMFCCRef = new System.ComponentModel.BackgroundWorker();
            this.processMFCCYour = new System.ComponentModel.BackgroundWorker();
            this.autoSelect_btn = new System.Windows.Forms.Button();
            this.addToTrainFile_btn = new System.Windows.Forms.Button();
            this.listTrainFiles_btn = new System.Windows.Forms.Button();
            this.train_btn = new System.Windows.Forms.Button();
            this.reg_lb = new System.Windows.Forms.Label();
            this.regMode_cbx = new System.Windows.Forms.CheckBox();
            this.reg_btn = new System.Windows.Forms.Button();
            this.update_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // devices_cbx
            // 
            this.devices_cbx.FormattingEnabled = true;
            this.devices_cbx.Location = new System.Drawing.Point(160, 29);
            this.devices_cbx.Name = "devices_cbx";
            this.devices_cbx.Size = new System.Drawing.Size(109, 21);
            this.devices_cbx.TabIndex = 1;
            // 
            // devices_lb
            // 
            this.devices_lb.AutoSize = true;
            this.devices_lb.Location = new System.Drawing.Point(90, 33);
            this.devices_lb.Name = "devices_lb";
            this.devices_lb.Size = new System.Drawing.Size(65, 13);
            this.devices_lb.TabIndex = 2;
            this.devices_lb.Text = "List Devices";
            // 
            // sampleRate_cbx
            // 
            this.sampleRate_cbx.FormattingEnabled = true;
            this.sampleRate_cbx.Location = new System.Drawing.Point(443, 29);
            this.sampleRate_cbx.Name = "sampleRate_cbx";
            this.sampleRate_cbx.Size = new System.Drawing.Size(109, 21);
            this.sampleRate_cbx.TabIndex = 1;
            // 
            // sampleRate_lb
            // 
            this.sampleRate_lb.AutoSize = true;
            this.sampleRate_lb.Location = new System.Drawing.Point(373, 33);
            this.sampleRate_lb.Name = "sampleRate_lb";
            this.sampleRate_lb.Size = new System.Drawing.Size(68, 13);
            this.sampleRate_lb.TabIndex = 2;
            this.sampleRate_lb.Text = "Sample Rate";
            // 
            // freshDevices_btn
            // 
            this.freshDevices_btn.Location = new System.Drawing.Point(285, 27);
            this.freshDevices_btn.Name = "freshDevices_btn";
            this.freshDevices_btn.Size = new System.Drawing.Size(75, 23);
            this.freshDevices_btn.TabIndex = 3;
            this.freshDevices_btn.Text = "List Devices";
            this.freshDevices_btn.UseVisualStyleBackColor = true;
            // 
            // waveViewer
            // 
            this.waveViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waveViewer.LeftSlider = 0.01466276F;
            this.waveViewer.Location = new System.Drawing.Point(20, 117);
            this.waveViewer.Name = "waveViewer";
            this.waveViewer.PenColor = System.Drawing.Color.DodgerBlue;
            this.waveViewer.PenWidth = 1F;
            this.waveViewer.RightSlider = 0.9853373F;
            this.waveViewer.Size = new System.Drawing.Size(684, 211);
            this.waveViewer.TabIndex = 4;
            this.waveViewer.WaveStream = null;
            // 
            // yourvoice_lb
            // 
            this.yourvoice_lb.AutoSize = true;
            this.yourvoice_lb.Location = new System.Drawing.Point(17, 92);
            this.yourvoice_lb.Name = "yourvoice_lb";
            this.yourvoice_lb.Size = new System.Drawing.Size(59, 13);
            this.yourvoice_lb.TabIndex = 6;
            this.yourvoice_lb.Text = "Your Voice";
            // 
            // recoder_btn
            // 
            this.recoder_btn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.recoder_btn.Image = global::Voice_Comparasion.Properties.Resources.RecorderIco;
            this.recoder_btn.Location = new System.Drawing.Point(19, 18);
            this.recoder_btn.Name = "recoder_btn";
            this.recoder_btn.Size = new System.Drawing.Size(39, 41);
            this.recoder_btn.TabIndex = 0;
            this.recoder_btn.UseVisualStyleBackColor = false;
            this.recoder_btn.Click += new System.EventHandler(this.recoder_btn_Click);
            // 
            // playStopyour_btn
            // 
            this.playStopyour_btn.Image = global::Voice_Comparasion.Properties.Resources.Play;
            this.playStopyour_btn.Location = new System.Drawing.Point(89, 80);
            this.playStopyour_btn.Name = "playStopyour_btn";
            this.playStopyour_btn.Size = new System.Drawing.Size(30, 31);
            this.playStopyour_btn.TabIndex = 7;
            this.playStopyour_btn.UseVisualStyleBackColor = true;
            this.playStopyour_btn.Click += new System.EventHandler(this.PlayStopyour_btn_Click);
            // 
            // setting_btn
            // 
            this.setting_btn.Location = new System.Drawing.Point(568, 29);
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
            selectedChartOption1.RefDetal = false;
            selectedChartOption1.RefDouble = false;
            selectedChartOption1.RefFreq = false;
            selectedChartOption1.RefMfcc = false;
            selectedChartOption1.RefPitch = false;
            selectedChartOption1.RefWave = false;
            selectedChartOption1.YourDetal = false;
            selectedChartOption1.YourDouble = false;
            selectedChartOption1.YourFreq = false;
            selectedChartOption1.YourMfcc = false;
            selectedChartOption1.YourPitch = false;
            selectedChartOption1.YourWave = false;
            this.showChart.Selected = selectedChartOption1;
            this.showChart.Size = new System.Drawing.Size(125, 252);
            this.showChart.TabIndex = 13;
            this.showChart.SelectedChart += new Object.Event.SelectedChartEventHandler(this.showChart_SelectedChart);
            // 
            // process_btn
            // 
            this.process_btn.Location = new System.Drawing.Point(785, 28);
            this.process_btn.Name = "process_btn";
            this.process_btn.Size = new System.Drawing.Size(70, 23);
            this.process_btn.TabIndex = 8;
            this.process_btn.Text = "Process";
            this.process_btn.UseVisualStyleBackColor = true;
            this.process_btn.Click += new System.EventHandler(this.process_btn_Click);
            // 
            // autoSelect_btn
            // 
            this.autoSelect_btn.Location = new System.Drawing.Point(132, 84);
            this.autoSelect_btn.Name = "autoSelect_btn";
            this.autoSelect_btn.Size = new System.Drawing.Size(100, 23);
            this.autoSelect_btn.TabIndex = 8;
            this.autoSelect_btn.Text = "Auto Select";
            this.autoSelect_btn.UseVisualStyleBackColor = true;
            this.autoSelect_btn.Click += new System.EventHandler(this.autoSelect_btn_Click);
            // 
            // addToTrainFile_btn
            // 
            this.addToTrainFile_btn.Location = new System.Drawing.Point(422, 84);
            this.addToTrainFile_btn.Name = "addToTrainFile_btn";
            this.addToTrainFile_btn.Size = new System.Drawing.Size(106, 23);
            this.addToTrainFile_btn.TabIndex = 14;
            this.addToTrainFile_btn.Text = "Add To Train Files";
            this.addToTrainFile_btn.UseVisualStyleBackColor = true;
            this.addToTrainFile_btn.Click += new System.EventHandler(this.addToTrainFile_btn_Click);
            // 
            // listTrainFiles_btn
            // 
            this.listTrainFiles_btn.Location = new System.Drawing.Point(629, 84);
            this.listTrainFiles_btn.Name = "listTrainFiles_btn";
            this.listTrainFiles_btn.Size = new System.Drawing.Size(75, 23);
            this.listTrainFiles_btn.TabIndex = 15;
            this.listTrainFiles_btn.Text = "Train Files";
            this.listTrainFiles_btn.UseVisualStyleBackColor = true;
            this.listTrainFiles_btn.Click += new System.EventHandler(this.listTrainFiles_btn_Click);
            // 
            // train_btn
            // 
            this.train_btn.Location = new System.Drawing.Point(693, 29);
            this.train_btn.Name = "train_btn";
            this.train_btn.Size = new System.Drawing.Size(75, 23);
            this.train_btn.TabIndex = 16;
            this.train_btn.Text = "Train";
            this.train_btn.UseVisualStyleBackColor = true;
            this.train_btn.Click += new System.EventHandler(this.train_btn_Click);
            // 
            // reg_lb
            // 
            this.reg_lb.AutoSize = true;
            this.reg_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.reg_lb.Location = new System.Drawing.Point(341, 385);
            this.reg_lb.Name = "reg_lb";
            this.reg_lb.Size = new System.Drawing.Size(90, 76);
            this.reg_lb.TabIndex = 17;
            this.reg_lb.Text = "...";
            this.reg_lb.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // regMode_cbx
            // 
            this.regMode_cbx.AutoSize = true;
            this.regMode_cbx.Location = new System.Drawing.Point(245, 87);
            this.regMode_cbx.Name = "regMode_cbx";
            this.regMode_cbx.Size = new System.Drawing.Size(76, 17);
            this.regMode_cbx.TabIndex = 18;
            this.regMode_cbx.Text = "Reg Mode";
            this.regMode_cbx.UseVisualStyleBackColor = true;
            this.regMode_cbx.CheckedChanged += new System.EventHandler(this.regMode_cbx_CheckedChanged);
            // 
            // reg_btn
            // 
            this.reg_btn.Location = new System.Drawing.Point(334, 84);
            this.reg_btn.Name = "reg_btn";
            this.reg_btn.Size = new System.Drawing.Size(75, 23);
            this.reg_btn.TabIndex = 19;
            this.reg_btn.Text = "Reg";
            this.reg_btn.UseVisualStyleBackColor = true;
            this.reg_btn.Click += new System.EventHandler(this.reg_btn_Click);
            // 
            // update_btn
            // 
            this.update_btn.Location = new System.Drawing.Point(548, 84);
            this.update_btn.Name = "update_btn";
            this.update_btn.Size = new System.Drawing.Size(61, 23);
            this.update_btn.TabIndex = 20;
            this.update_btn.Text = "Update";
            this.update_btn.UseVisualStyleBackColor = true;
            this.update_btn.Click += new System.EventHandler(this.update_btn_Click);
            // 
            // RegControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.update_btn);
            this.Controls.Add(this.reg_btn);
            this.Controls.Add(this.regMode_cbx);
            this.Controls.Add(this.reg_lb);
            this.Controls.Add(this.train_btn);
            this.Controls.Add(this.listTrainFiles_btn);
            this.Controls.Add(this.addToTrainFile_btn);
            this.Controls.Add(this.showChart);
            this.Controls.Add(this.autoSelect_btn);
            this.Controls.Add(this.process_btn);
            this.Controls.Add(this.setting_btn);
            this.Controls.Add(this.playStopyour_btn);
            this.Controls.Add(this.yourvoice_lb);
            this.Controls.Add(this.waveViewer);
            this.Controls.Add(this.freshDevices_btn);
            this.Controls.Add(this.sampleRate_lb);
            this.Controls.Add(this.sampleRate_cbx);
            this.Controls.Add(this.devices_lb);
            this.Controls.Add(this.devices_cbx);
            this.Controls.Add(this.recoder_btn);
            this.Name = "RegControl";
            this.Size = new System.Drawing.Size(867, 519);
            this.SizeChanged += new System.EventHandler(this.Recoder_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button recoder_btn;
        private System.Windows.Forms.ComboBox devices_cbx;
        private System.Windows.Forms.Label devices_lb;
        private System.Windows.Forms.ComboBox sampleRate_cbx;
        private System.Windows.Forms.Label sampleRate_lb;
        private System.Windows.Forms.Button freshDevices_btn;
        private WaveViewer waveViewer;
        private System.Windows.Forms.Label yourvoice_lb;
        private System.Windows.Forms.Button playStopyour_btn;
        private System.Windows.Forms.Button setting_btn;
        private SelectShowChart showChart;
        private System.Windows.Forms.Button process_btn;
        private System.ComponentModel.BackgroundWorker processMFCCRef;
        private System.ComponentModel.BackgroundWorker processMFCCYour;
        private System.Windows.Forms.Button autoSelect_btn;
        private System.Windows.Forms.Button addToTrainFile_btn;
        private System.Windows.Forms.Button listTrainFiles_btn;
        private System.Windows.Forms.Button train_btn;
        private System.Windows.Forms.Label reg_lb;
        private System.Windows.Forms.CheckBox regMode_cbx;
        private System.Windows.Forms.Button reg_btn;
        private System.Windows.Forms.Button update_btn;
    }
}

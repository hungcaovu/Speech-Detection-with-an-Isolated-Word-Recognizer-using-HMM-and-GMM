namespace UC
{
    partial class MainControl
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
            this.ref_voice = new System.Windows.Forms.Label();
            this.playStopref_btn = new System.Windows.Forms.Button();
            this.recoder_btn = new System.Windows.Forms.Button();
            this.playStopyour_btn = new System.Windows.Forms.Button();
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
            this.autoSelect_btn = new System.Windows.Forms.Button();
            this.refWord_btn = new System.Windows.Forms.Button();
            this.totalScore_lb = new System.Windows.Forms.Label();
            this.score_lb = new System.Windows.Forms.Label();
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
            this.waveViewer.Location = new System.Drawing.Point(20, 117);
            this.waveViewer.Name = "waveViewer";
            this.waveViewer.PenColor = System.Drawing.Color.DodgerBlue;
            this.waveViewer.PenWidth = 1F;
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
            // ref_voice
            // 
            this.ref_voice.AutoSize = true;
            this.ref_voice.Location = new System.Drawing.Point(496, 92);
            this.ref_voice.Name = "ref_voice";
            this.ref_voice.Size = new System.Drawing.Size(54, 13);
            this.ref_voice.TabIndex = 6;
            this.ref_voice.Text = "Ref Voice";
            // 
            // playStopref_btn
            // 
            this.playStopref_btn.Image = global::Voice_Comparasion.Properties.Resources.Play;
            this.playStopref_btn.Location = new System.Drawing.Point(560, 80);
            this.playStopref_btn.Name = "playStopref_btn";
            this.playStopref_btn.Size = new System.Drawing.Size(30, 31);
            this.playStopref_btn.TabIndex = 7;
            this.playStopref_btn.UseVisualStyleBackColor = true;
            this.playStopref_btn.Click += new System.EventHandler(this.PlayStopref_btn_Click);
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
            this.costmfcc_lb.Location = new System.Drawing.Point(30, 409);
            this.costmfcc_lb.Name = "costmfcc_lb";
            this.costmfcc_lb.Size = new System.Drawing.Size(69, 24);
            this.costmfcc_lb.TabIndex = 10;
            this.costmfcc_lb.Text = "MFCC:";
            // 
            // costmfccvalue_lb
            // 
            this.costmfccvalue_lb.AutoSize = true;
            this.costmfccvalue_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costmfccvalue_lb.Location = new System.Drawing.Point(91, 402);
            this.costmfccvalue_lb.Name = "costmfccvalue_lb";
            this.costmfccvalue_lb.Size = new System.Drawing.Size(67, 31);
            this.costmfccvalue_lb.TabIndex = 11;
            this.costmfccvalue_lb.Text = "0.00";
            // 
            // costpitch_bl
            // 
            this.costpitch_bl.AutoSize = true;
            this.costpitch_bl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costpitch_bl.Location = new System.Drawing.Point(30, 489);
            this.costpitch_bl.Name = "costpitch_bl";
            this.costpitch_bl.Size = new System.Drawing.Size(56, 24);
            this.costpitch_bl.TabIndex = 10;
            this.costpitch_bl.Text = "Pitch:";
            // 
            // costpitchvalue_lb
            // 
            this.costpitchvalue_lb.AutoSize = true;
            this.costpitchvalue_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costpitchvalue_lb.Location = new System.Drawing.Point(87, 482);
            this.costpitchvalue_lb.Name = "costpitchvalue_lb";
            this.costpitchvalue_lb.Size = new System.Drawing.Size(67, 31);
            this.costpitchvalue_lb.TabIndex = 11;
            this.costpitchvalue_lb.Text = "0.00";
            // 
            // _mfccViewer
            // 
            this._mfccViewer.Location = new System.Drawing.Point(200, 338);
            this._mfccViewer.Name = "_mfccViewer";
            this._mfccViewer.Size = new System.Drawing.Size(655, 304);
            this._mfccViewer.TabIndex = 12;
            // 
            // autoSelect_btn
            // 
            this.autoSelect_btn.Location = new System.Drawing.Point(146, 84);
            this.autoSelect_btn.Name = "autoSelect_btn";
            this.autoSelect_btn.Size = new System.Drawing.Size(100, 23);
            this.autoSelect_btn.TabIndex = 8;
            this.autoSelect_btn.Text = "Auto Select";
            this.autoSelect_btn.UseVisualStyleBackColor = true;
            this.autoSelect_btn.Click += new System.EventHandler(this.autoSelect_btn_Click);
            // 
            // refWord_btn
            // 
            this.refWord_btn.Location = new System.Drawing.Point(604, 88);
            this.refWord_btn.Name = "refWord_btn";
            this.refWord_btn.Size = new System.Drawing.Size(100, 23);
            this.refWord_btn.TabIndex = 14;
            this.refWord_btn.Text = "Words Ref";
            this.refWord_btn.UseVisualStyleBackColor = true;
            this.refWord_btn.Click += new System.EventHandler(this.refWord_btn_Click);
            // 
            // totalScore_lb
            // 
            this.totalScore_lb.AutoSize = true;
            this.totalScore_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalScore_lb.Location = new System.Drawing.Point(30, 553);
            this.totalScore_lb.Name = "totalScore_lb";
            this.totalScore_lb.Size = new System.Drawing.Size(111, 24);
            this.totalScore_lb.TabIndex = 10;
            this.totalScore_lb.Text = "Total Score:";
            // 
            // score_lb
            // 
            this.score_lb.AutoSize = true;
            this.score_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_lb.Location = new System.Drawing.Point(140, 546);
            this.score_lb.Name = "score_lb";
            this.score_lb.Size = new System.Drawing.Size(67, 31);
            this.score_lb.TabIndex = 11;
            this.score_lb.Text = "0.00";
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.refWord_btn);
            this.Controls.Add(this.costmfccvalue_lb);
            this.Controls.Add(this.costmfcc_lb);
            this.Controls.Add(this.score_lb);
            this.Controls.Add(this.costpitchvalue_lb);
            this.Controls.Add(this.totalScore_lb);
            this.Controls.Add(this.costpitch_bl);
            this.Controls.Add(this.showChart);
            this.Controls.Add(this._mfccViewer);
            this.Controls.Add(this.autoSelect_btn);
            this.Controls.Add(this.process_btn);
            this.Controls.Add(this.setting_btn);
            this.Controls.Add(this.playStopyour_btn);
            this.Controls.Add(this.playStopref_btn);
            this.Controls.Add(this.ref_voice);
            this.Controls.Add(this.yourvoice_lb);
            this.Controls.Add(this.waveViewer);
            this.Controls.Add(this.freshDevices_btn);
            this.Controls.Add(this.sampleRate_lb);
            this.Controls.Add(this.sampleRate_cbx);
            this.Controls.Add(this.devices_lb);
            this.Controls.Add(this.devices_cbx);
            this.Controls.Add(this.recoder_btn);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(867, 642);
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
        private System.Windows.Forms.Label ref_voice;
        private System.Windows.Forms.Button playStopref_btn;
        private System.Windows.Forms.Button playStopyour_btn;
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
        private System.Windows.Forms.Button autoSelect_btn;
        private System.Windows.Forms.Button refWord_btn;
        private System.Windows.Forms.Label totalScore_lb;
        private System.Windows.Forms.Label score_lb;
    }
}

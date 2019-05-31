namespace UC
{
    partial class MainTestControl
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
            this.listVoice_lbx = new System.Windows.Forms.ListBox();
            this.yourvoice_lb = new System.Windows.Forms.Label();
            this.ref_voice = new System.Windows.Forms.Label();
            this.PlayStopref_btn = new System.Windows.Forms.Button();
            this.PlayStopyour_btn = new System.Windows.Forms.Button();
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
            this.listVoice2_lbx = new System.Windows.Forms.ListBox();
            this.yourTreeView = new System.Windows.Forms.TreeView();
            this.refTreeview = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // listVoice_lbx
            // 
            this.listVoice_lbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listVoice_lbx.FormattingEnabled = true;
            this.listVoice_lbx.ItemHeight = 20;
            this.listVoice_lbx.Location = new System.Drawing.Point(388, 124);
            this.listVoice_lbx.Name = "listVoice_lbx";
            this.listVoice_lbx.Size = new System.Drawing.Size(100, 204);
            this.listVoice_lbx.TabIndex = 5;
            this.listVoice_lbx.SelectedIndexChanged += new System.EventHandler(this.listVoice_lbx_SelectedIndexChanged);
            // 
            // yourvoice_lb
            // 
            this.yourvoice_lb.AutoSize = true;
            this.yourvoice_lb.Location = new System.Drawing.Point(22, 92);
            this.yourvoice_lb.Name = "yourvoice_lb";
            this.yourvoice_lb.Size = new System.Drawing.Size(59, 13);
            this.yourvoice_lb.TabIndex = 6;
            this.yourvoice_lb.Text = "Your Voice";
            // 
            // ref_voice
            // 
            this.ref_voice.AutoSize = true;
            this.ref_voice.Location = new System.Drawing.Point(385, 92);
            this.ref_voice.Name = "ref_voice";
            this.ref_voice.Size = new System.Drawing.Size(54, 13);
            this.ref_voice.TabIndex = 6;
            this.ref_voice.Text = "Ref Voice";
            // 
            // PlayStopref_btn
            // 
            this.PlayStopref_btn.Image = global::Voice_Comparasion.Properties.Resources.Play;
            this.PlayStopref_btn.Location = new System.Drawing.Point(449, 80);
            this.PlayStopref_btn.Name = "PlayStopref_btn";
            this.PlayStopref_btn.Size = new System.Drawing.Size(30, 31);
            this.PlayStopref_btn.TabIndex = 7;
            this.PlayStopref_btn.UseVisualStyleBackColor = true;
            this.PlayStopref_btn.Click += new System.EventHandler(this.PlayStopref_btn_Click);
            // 
            // PlayStopyour_btn
            // 
            this.PlayStopyour_btn.Image = global::Voice_Comparasion.Properties.Resources.Play;
            this.PlayStopyour_btn.Location = new System.Drawing.Point(94, 80);
            this.PlayStopyour_btn.Name = "PlayStopyour_btn";
            this.PlayStopyour_btn.Size = new System.Drawing.Size(30, 31);
            this.PlayStopyour_btn.TabIndex = 7;
            this.PlayStopyour_btn.UseVisualStyleBackColor = true;
            this.PlayStopyour_btn.Click += new System.EventHandler(this.PlayStopyour_btn_Click);
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
            this.costpitch_bl.Location = new System.Drawing.Point(30, 511);
            this.costpitch_bl.Name = "costpitch_bl";
            this.costpitch_bl.Size = new System.Drawing.Size(56, 24);
            this.costpitch_bl.TabIndex = 10;
            this.costpitch_bl.Text = "Pitch:";
            // 
            // costpitchvalue_lb
            // 
            this.costpitchvalue_lb.AutoSize = true;
            this.costpitchvalue_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.costpitchvalue_lb.Location = new System.Drawing.Point(87, 504);
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
            // listVoice2_lbx
            // 
            this.listVoice2_lbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listVoice2_lbx.FormattingEnabled = true;
            this.listVoice2_lbx.ItemHeight = 20;
            this.listVoice2_lbx.Location = new System.Drawing.Point(25, 124);
            this.listVoice2_lbx.Name = "listVoice2_lbx";
            this.listVoice2_lbx.Size = new System.Drawing.Size(100, 204);
            this.listVoice2_lbx.TabIndex = 5;
            this.listVoice2_lbx.SelectedIndexChanged += new System.EventHandler(this.listVoice2_lbx_SelectedIndexChanged);
            // 
            // yourTreeView
            // 
            this.yourTreeView.Location = new System.Drawing.Point(130, 124);
            this.yourTreeView.Name = "yourTreeView";
            this.yourTreeView.Size = new System.Drawing.Size(233, 204);
            this.yourTreeView.TabIndex = 14;
            // 
            // refTreeview
            // 
            this.refTreeview.Location = new System.Drawing.Point(493, 124);
            this.refTreeview.Name = "refTreeview";
            this.refTreeview.Size = new System.Drawing.Size(233, 204);
            this.refTreeview.TabIndex = 14;
            // 
            // MainTestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.refTreeview);
            this.Controls.Add(this.yourTreeView);
            this.Controls.Add(this.costmfccvalue_lb);
            this.Controls.Add(this.costmfcc_lb);
            this.Controls.Add(this.costpitchvalue_lb);
            this.Controls.Add(this.costpitch_bl);
            this.Controls.Add(this.showChart);
            this.Controls.Add(this._mfccViewer);
            this.Controls.Add(this.process_btn);
            this.Controls.Add(this.setting_btn);
            this.Controls.Add(this.PlayStopyour_btn);
            this.Controls.Add(this.PlayStopref_btn);
            this.Controls.Add(this.ref_voice);
            this.Controls.Add(this.yourvoice_lb);
            this.Controls.Add(this.listVoice2_lbx);
            this.Controls.Add(this.listVoice_lbx);
            this.Name = "MainTestControl";
            this.Size = new System.Drawing.Size(867, 642);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listVoice_lbx;
        private System.Windows.Forms.Label yourvoice_lb;
        private System.Windows.Forms.Label ref_voice;
        private System.Windows.Forms.Button PlayStopref_btn;
        private System.Windows.Forms.Button PlayStopyour_btn;
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
        private System.Windows.Forms.ListBox listVoice2_lbx;
        private System.Windows.Forms.TreeView yourTreeView;
        private System.Windows.Forms.TreeView refTreeview;
    }
}

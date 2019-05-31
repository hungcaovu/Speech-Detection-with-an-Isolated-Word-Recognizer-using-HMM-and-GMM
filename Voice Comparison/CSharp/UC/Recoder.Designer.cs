namespace UC
{
    partial class Recoder
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
            this.devices_cbx = new System.Windows.Forms.ComboBox();
            this.devices_lb = new System.Windows.Forms.Label();
            this.sampleRate_cbx = new System.Windows.Forms.ComboBox();
            this.sampleRate_lb = new System.Windows.Forms.Label();
            this.freshDevices_btn = new System.Windows.Forms.Button();
            this.recoder_btn = new System.Windows.Forms.Button();
            this.spectrumRefer = new UC.Spectrum();
            this.spectrumCurr = new UC.Spectrum();
            this.waveViewer = new UC.WaveViewer();
            this.SuspendLayout();
            // 
            // devices_cbx
            // 
            this.devices_cbx.FormattingEnabled = true;
            this.devices_cbx.Location = new System.Drawing.Point(182, 29);
            this.devices_cbx.Name = "devices_cbx";
            this.devices_cbx.Size = new System.Drawing.Size(109, 21);
            this.devices_cbx.TabIndex = 1;
            // 
            // devices_lb
            // 
            this.devices_lb.AutoSize = true;
            this.devices_lb.Location = new System.Drawing.Point(112, 33);
            this.devices_lb.Name = "devices_lb";
            this.devices_lb.Size = new System.Drawing.Size(65, 13);
            this.devices_lb.TabIndex = 2;
            this.devices_lb.Text = "List Devices";
            // 
            // sampleRate_cbx
            // 
            this.sampleRate_cbx.FormattingEnabled = true;
            this.sampleRate_cbx.Location = new System.Drawing.Point(465, 29);
            this.sampleRate_cbx.Name = "sampleRate_cbx";
            this.sampleRate_cbx.Size = new System.Drawing.Size(109, 21);
            this.sampleRate_cbx.TabIndex = 1;
            // 
            // sampleRate_lb
            // 
            this.sampleRate_lb.AutoSize = true;
            this.sampleRate_lb.Location = new System.Drawing.Point(395, 33);
            this.sampleRate_lb.Name = "sampleRate_lb";
            this.sampleRate_lb.Size = new System.Drawing.Size(68, 13);
            this.sampleRate_lb.TabIndex = 2;
            this.sampleRate_lb.Text = "Sample Rate";
            // 
            // freshDevices_btn
            // 
            this.freshDevices_btn.Location = new System.Drawing.Point(307, 27);
            this.freshDevices_btn.Name = "freshDevices_btn";
            this.freshDevices_btn.Size = new System.Drawing.Size(75, 23);
            this.freshDevices_btn.TabIndex = 3;
            this.freshDevices_btn.Text = "List Devices";
            this.freshDevices_btn.UseVisualStyleBackColor = true;
            // 
            // recoder_btn
            // 
            this.recoder_btn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.recoder_btn.Image = global::UC.Properties.Resources.Pause;
            this.recoder_btn.Location = new System.Drawing.Point(41, 18);
            this.recoder_btn.Name = "recoder_btn";
            this.recoder_btn.Size = new System.Drawing.Size(39, 41);
            this.recoder_btn.TabIndex = 0;
            this.recoder_btn.UseVisualStyleBackColor = false;
            this.recoder_btn.Click += new System.EventHandler(this.recoder_btn_Click);
            // 
            // spectrumRefer
            // 
            this.spectrumRefer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spectrumRefer.AutoSize = true;
            this.spectrumRefer.ColorRulerBand = 9;
            this.spectrumRefer.Data = null;
            this.spectrumRefer.Location = new System.Drawing.Point(10, 459);
            this.spectrumRefer.MaxValue = 10F;
            this.spectrumRefer.MinValue = -10F;
            this.spectrumRefer.Name = "spectrumRefer";
            this.spectrumRefer.ResolutionValue = 0.0005F;
            this.spectrumRefer.Size = new System.Drawing.Size(901, 192);
            this.spectrumRefer.TabIndex = 5;
            this.spectrumRefer.Title = "";
            this.spectrumRefer.WidthLine = 10;
            // 
            // spectrumCurr
            // 
            this.spectrumCurr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spectrumCurr.AutoSize = true;
            this.spectrumCurr.BackColor = System.Drawing.SystemColors.Control;
            this.spectrumCurr.ColorRulerBand = 9;
            this.spectrumCurr.Data = null;
            this.spectrumCurr.Location = new System.Drawing.Point(10, 270);
            this.spectrumCurr.MaxValue = 10F;
            this.spectrumCurr.MinValue = -10F;
            this.spectrumCurr.Name = "spectrumCurr";
            this.spectrumCurr.ResolutionValue = 0.0005F;
            this.spectrumCurr.Size = new System.Drawing.Size(901, 216);
            this.spectrumCurr.TabIndex = 5;
            this.spectrumCurr.Title = "";
            this.spectrumCurr.WidthLine = 10;
            // 
            // waveViewer
            // 
            this.waveViewer.Location = new System.Drawing.Point(41, 81);
            this.waveViewer.Name = "waveViewer";
            this.waveViewer.PenColor = System.Drawing.Color.DodgerBlue;
            this.waveViewer.PenWidth = 1F;
            //this.waveViewer.SamplesPerPixel = 128;
            this.waveViewer.Size = new System.Drawing.Size(809, 116);
            this.waveViewer.TabIndex = 4;
            this.waveViewer.WaveStream = null;
            // 
            // Recoder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spectrumRefer);
            this.Controls.Add(this.spectrumCurr);
            this.Controls.Add(this.waveViewer);
            this.Controls.Add(this.freshDevices_btn);
            this.Controls.Add(this.sampleRate_lb);
            this.Controls.Add(this.sampleRate_cbx);
            this.Controls.Add(this.devices_lb);
            this.Controls.Add(this.devices_cbx);
            this.Controls.Add(this.recoder_btn);
            this.Name = "Recoder";
            this.Size = new System.Drawing.Size(926, 654);
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
        private Spectrum spectrumCurr;
        private Spectrum spectrumRefer;
    }
}

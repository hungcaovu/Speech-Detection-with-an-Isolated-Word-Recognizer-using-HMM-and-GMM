namespace UC
{
    partial class FrequencyViewer
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
            this.spectrumPic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.spectrumPic)).BeginInit();
            this.SuspendLayout();
            // 
            // spectrumPic
            // 
            this.spectrumPic.Dock = System.Windows.Forms.DockStyle.Left;
            this.spectrumPic.Location = new System.Drawing.Point(0, 0);
            this.spectrumPic.Name = "spectrumPic";
            this.spectrumPic.Size = new System.Drawing.Size(980, 545);
            this.spectrumPic.TabIndex = 0;
            this.spectrumPic.TabStop = false;
            this.spectrumPic.Paint += new System.Windows.Forms.PaintEventHandler(this.spectrumPic_Paint);
            // 
            // FrequencyViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spectrumPic);
            this.Name = "FrequencyViewer";
            this.Size = new System.Drawing.Size(1085, 545);
            ((System.ComponentModel.ISupportInitialize)(this.spectrumPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox spectrumPic;
    }
}

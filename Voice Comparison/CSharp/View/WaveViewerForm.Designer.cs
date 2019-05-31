namespace Voice_Comparasion
{
	partial class WaveViewerForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.newWaveViewer = new UC.NewWaveViewer();
            this.SuspendLayout();
            // 
            // newWaveViewer
            // 
            this.newWaveViewer.Data = null;
            this.newWaveViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newWaveViewer.FilePath = string.Empty;
            this.newWaveViewer.LineColor = System.Drawing.Color.SkyBlue;
            this.newWaveViewer.Location = new System.Drawing.Point(0, 0);
            this.newWaveViewer.MarginChart = 10;
            this.newWaveViewer.Name = "newWaveViewer";
            this.newWaveViewer.PenColor = System.Drawing.Color.Red;
            this.newWaveViewer.PenWidth = 1F;
            this.newWaveViewer.SamplesPerPixel = 1F;
            this.newWaveViewer.Size = new System.Drawing.Size(986, 250);
            this.newWaveViewer.TabIndex = 0;
            // 
            // WaveViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 250);
            this.Controls.Add(this.newWaveViewer);
            this.Name = "WaveViewerForm";
            this.Text = "WaveViewer";
            this.ResumeLayout(false);

		}

		#endregion

        private UC.NewWaveViewer newWaveViewer;

    }
}
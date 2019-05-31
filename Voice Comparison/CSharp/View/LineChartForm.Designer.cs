namespace Voice_Comparasion
{
    partial class LineChartForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lineViewer = new UC.LineViewer();
            this.SuspendLayout();
            // 
            // lineViewer
            // 
            this.lineViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lineViewer.ForeColor = System.Drawing.Color.Cornsilk;
            this.lineViewer.Location = new System.Drawing.Point(0, 0);
            this.lineViewer.MaxValue = 700;
            this.lineViewer.MinValue = 0;
            this.lineViewer.Name = "lineViewer";
            this.lineViewer.Size = new System.Drawing.Size(678, 261);
            this.lineViewer.StepValue = 100;
            this.lineViewer.TabIndex = 0;
            // 
            // LineChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 261);
            this.Controls.Add(this.lineViewer);
            this.Name = "LineChartForm";
            this.Text = "LineChartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.LineViewer lineViewer;
    }
}
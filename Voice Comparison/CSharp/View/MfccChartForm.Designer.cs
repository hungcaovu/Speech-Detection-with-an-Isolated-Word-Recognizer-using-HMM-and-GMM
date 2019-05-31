using System;
namespace Voice_Comparasion
{
    partial class MfccChartForm
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
            this.colStyle_cbx = new System.Windows.Forms.ComboBox();
            this.spectrum = new UC.Spectrum();
            this.SuspendLayout();
            // 
            // colStyle_cbx
            // 
            this.colStyle_cbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colStyle_cbx.FormattingEnabled = true;
            this.colStyle_cbx.Location = new System.Drawing.Point(649, 0);
            this.colStyle_cbx.Name = "colStyle_cbx";
            this.colStyle_cbx.Size = new System.Drawing.Size(75, 21);
            this.colStyle_cbx.TabIndex = 1;
            this.colStyle_cbx.SelectedIndexChanged += new System.EventHandler(this.colStyle_cbx_SelectedIndexChanged);
            // 
            // spectrum
            // 
            this.spectrum.ColorRulerBand = 9;
            this.spectrum.Data = null;
            this.spectrum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spectrum.Location = new System.Drawing.Point(0, 0);
            this.spectrum.MaxValue = 10F;
            this.spectrum.MinValue = -10F;
            this.spectrum.Name = "spectrum";
            this.spectrum.ResolutionValue = 0.002F;
            this.spectrum.Size = new System.Drawing.Size(784, 261);
            this.spectrum.TabIndex = 0;
            this.spectrum.Title = "Path File:";
            this.spectrum.WidthLine = 5;
            // 
            // MfccChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.colStyle_cbx);
            this.Controls.Add(this.spectrum);
            this.Name = "MfccChartForm";
            this.Text = "MfccChartForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.Spectrum spectrum;
        private System.Windows.Forms.ComboBox colStyle_cbx;

        //public  void Show(){
        //    if (InvokeRequired)
        //    {
        //        Action act = new Action(Show);
        //        Invoke(act);
        //    } else {
        //        base.Show();
        //    }
        //}
    }
}
﻿namespace Voice_Comparasion
{
    partial class MainForm
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
            this.statusBar_strip = new System.Windows.Forms.StatusStrip();
            this.mainUC = new UC.MainControl();
            this.SuspendLayout();
            // 
            // statusBar_strip
            // 
            this.statusBar_strip.Location = new System.Drawing.Point(0, 636);
            this.statusBar_strip.Name = "statusBar_strip";
            this.statusBar_strip.Size = new System.Drawing.Size(870, 22);
            this.statusBar_strip.TabIndex = 1;
            // 
            // mainUC
            // 
            this.mainUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainUC.Location = new System.Drawing.Point(0, 0);
            this.mainUC.Name = "mainUC";
            this.mainUC.Size = new System.Drawing.Size(870, 658);
            this.mainUC.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 658);
            this.Controls.Add(this.statusBar_strip);
            this.Controls.Add(this.mainUC);
            this.Name = "MainForm";
            this.Text = "Voice Comparision";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UC.MainControl mainUC;
        private System.Windows.Forms.StatusStrip statusBar_strip;
    }
}
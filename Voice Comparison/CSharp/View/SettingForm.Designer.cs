﻿using Model;
using Object;
namespace Voice_Comparasion
{
    partial class SettingForm
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
            this.mfccSetting = new UC.MfccSetting();
            this.SuspendLayout();
            // 
            // mfccSetting
            // 
            this.mfccSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mfccSetting.Location = new System.Drawing.Point(0, 0);
            this.mfccSetting.Name = "mfccSetting";
            this.mfccSetting.Options = VCContext.Instance.MFCCOptions;
            this.mfccSetting.Size = new System.Drawing.Size(410, 169);
            this.mfccSetting.TabIndex = 0;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 169);
            this.Controls.Add(this.mfccSetting);
            this.Name = "SettingForm";
            this.Text = "SettingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.MfccSetting mfccSetting;
    }
}
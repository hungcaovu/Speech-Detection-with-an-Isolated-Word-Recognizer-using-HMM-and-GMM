namespace Voice_Comparasion
{
    partial class Form1
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
            this.mainUC = new UC.MainControl();
            this.SuspendLayout();
            // 
            // mainUC
            // 
            this.mainUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainUC.Location = new System.Drawing.Point(0, 0);
            this.mainUC.Name = "mainUC";
            this.mainUC.Size = new System.Drawing.Size(928, 586);
            this.mainUC.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 586);
            this.Controls.Add(this.mainUC);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.MainControl mainUC;
    }
}
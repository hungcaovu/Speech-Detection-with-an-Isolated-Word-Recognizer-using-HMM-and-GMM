namespace Voice_Comparasion
{
    partial class TestMode
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
            this.mainTestControlTest1 = new UC.MainTestControlTest();
            this.SuspendLayout();
            // 
            // mainTestControlTest1
            // 
            this.mainTestControlTest1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTestControlTest1.Location = new System.Drawing.Point(0, 0);
            this.mainTestControlTest1.Name = "mainTestControlTest1";
            this.mainTestControlTest1.Size = new System.Drawing.Size(875, 446);
            this.mainTestControlTest1.TabIndex = 0;
            // 
            // TestMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 446);
            this.Controls.Add(this.mainTestControlTest1);
            this.Name = "TestMode";
            this.Text = "TestMode";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.MainTestControlTest mainTestControlTest1;
    }
}
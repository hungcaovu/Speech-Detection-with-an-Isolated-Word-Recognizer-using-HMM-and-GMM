namespace Voice_Comparasion
{
    partial class TestForm
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
            this.testControl = new Voice_Comparasion.TestControl();
            this.SuspendLayout();
            // 
            // testControl
            // 
            this.testControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testControl.Location = new System.Drawing.Point(0, 0);
            this.testControl.Name = "testControl";
            this.testControl.Size = new System.Drawing.Size(591, 230);
            this.testControl.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 230);
            this.Controls.Add(this.testControl);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private TestControl testControl;
    }
}
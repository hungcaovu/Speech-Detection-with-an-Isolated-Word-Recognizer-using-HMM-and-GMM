namespace Voice_Comparasion
{
    partial class TestGridView
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
            this.viewControl1 = new UC.ViewControl();
            this.SuspendLayout();
            // 
            // viewControl1
            // 
            this.viewControl1.Location = new System.Drawing.Point(34, 13);
            this.viewControl1.Name = "viewControl1";
            this.viewControl1.Size = new System.Drawing.Size(843, 518);
            this.viewControl1.TabIndex = 0;
            // 
            // TestGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 577);
            this.Controls.Add(this.viewControl1);
            this.Name = "TestGridView";
            this.Text = "TestGridView";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.ViewControl viewControl1;


    }
}
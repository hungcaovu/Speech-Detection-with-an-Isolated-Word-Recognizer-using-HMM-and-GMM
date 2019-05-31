namespace Voice_Comparasion
{
    partial class AmTietForm
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
            this.amVietViewControl = new UC.AmVietViewControl();
            this.SuspendLayout();
            // 
            // amVietViewControl
            // 
            this.amVietViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.amVietViewControl.Location = new System.Drawing.Point(0, 0);
            this.amVietViewControl.Name = "amVietViewControl";
            this.amVietViewControl.Size = new System.Drawing.Size(532, 380);
            this.amVietViewControl.TabIndex = 0;
            // 
            // AmTietForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 380);
            this.Controls.Add(this.amVietViewControl);
            this.Name = "AmTietForm";
            this.Text = "Am Tiet Form";
            this.ResumeLayout(false);

        }

        #endregion

        private UC.AmVietViewControl amVietViewControl;
    }
}
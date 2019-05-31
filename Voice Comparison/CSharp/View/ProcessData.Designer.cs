namespace Voice_Comparasion
{
    partial class ProcessData
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
            this.btn_process = new System.Windows.Forms.Button();
            this.amTietView = new UC.AmVietViewControl();
            this.SuspendLayout();
            // 
            // btn_process
            // 
            this.btn_process.Location = new System.Drawing.Point(12, 12);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(93, 23);
            this.btn_process.TabIndex = 0;
            this.btn_process.Text = "Process Data";
            this.btn_process.UseVisualStyleBackColor = true;
            this.btn_process.Click += new System.EventHandler(this.btn_process_Click);
            // 
            // amTietView
            // 
            this.amTietView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.amTietView.Location = new System.Drawing.Point(0, 41);
            this.amTietView.Name = "amTietView";
            this.amTietView.Size = new System.Drawing.Size(883, 380);
            this.amTietView.TabIndex = 1;
            // 
            // ProcessData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 421);
            this.Controls.Add(this.amTietView);
            this.Controls.Add(this.btn_process);
            this.Name = "ProcessData";
            this.Text = "ProcessData";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_process;
        private UC.AmVietViewControl amTietView;
    }
}
namespace UC
{
    partial class ListWordsView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._treeView = new System.Windows.Forms.TreeView();
            this.rdb_PhuAmDau = new System.Windows.Forms.RadioButton();
            this.rdb_AmDem = new System.Windows.Forms.RadioButton();
            this.rdb_AmChinh = new System.Windows.Forms.RadioButton();
            this.rdb_AmCuoi = new System.Windows.Forms.RadioButton();
            this.rdb_Thanh = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // _treeView
            // 
            this._treeView.Location = new System.Drawing.Point(41, 19);
            this._treeView.Name = "_treeView";
            this._treeView.Size = new System.Drawing.Size(641, 401);
            this._treeView.TabIndex = 0;
            // 
            // rdb_PhuAmDau
            // 
            this.rdb_PhuAmDau.AutoSize = true;
            this.rdb_PhuAmDau.Location = new System.Drawing.Point(123, 448);
            this.rdb_PhuAmDau.Name = "rdb_PhuAmDau";
            this.rdb_PhuAmDau.Size = new System.Drawing.Size(85, 17);
            this.rdb_PhuAmDau.TabIndex = 1;
            this.rdb_PhuAmDau.TabStop = true;
            this.rdb_PhuAmDau.Text = "Phụ Âm Đầu";
            this.rdb_PhuAmDau.UseVisualStyleBackColor = true;
            this.rdb_PhuAmDau.CheckedChanged += new System.EventHandler(this.rdb_PhuAmDau_CheckedChanged);
            // 
            // rdb_AmDem
            // 
            this.rdb_AmDem.AutoSize = true;
            this.rdb_AmDem.Location = new System.Drawing.Point(249, 448);
            this.rdb_AmDem.Name = "rdb_AmDem";
            this.rdb_AmDem.Size = new System.Drawing.Size(65, 17);
            this.rdb_AmDem.TabIndex = 1;
            this.rdb_AmDem.TabStop = true;
            this.rdb_AmDem.Text = "Âm Đệm";
            this.rdb_AmDem.UseVisualStyleBackColor = true;
            this.rdb_AmDem.CheckedChanged += new System.EventHandler(this.rdb_AmDem_CheckedChanged);
            // 
            // rdb_AmChinh
            // 
            this.rdb_AmChinh.AutoSize = true;
            this.rdb_AmChinh.Location = new System.Drawing.Point(355, 448);
            this.rdb_AmChinh.Name = "rdb_AmChinh";
            this.rdb_AmChinh.Size = new System.Drawing.Size(72, 17);
            this.rdb_AmChinh.TabIndex = 1;
            this.rdb_AmChinh.TabStop = true;
            this.rdb_AmChinh.Text = "Âm Chính";
            this.rdb_AmChinh.UseVisualStyleBackColor = true;
            this.rdb_AmChinh.CheckedChanged += new System.EventHandler(this.rdb_AmChinh_CheckedChanged);
            // 
            // rdb_AmCuoi
            // 
            this.rdb_AmCuoi.AutoSize = true;
            this.rdb_AmCuoi.Location = new System.Drawing.Point(468, 448);
            this.rdb_AmCuoi.Name = "rdb_AmCuoi";
            this.rdb_AmCuoi.Size = new System.Drawing.Size(64, 17);
            this.rdb_AmCuoi.TabIndex = 1;
            this.rdb_AmCuoi.TabStop = true;
            this.rdb_AmCuoi.Text = "Âm Cuối";
            this.rdb_AmCuoi.UseVisualStyleBackColor = true;
            this.rdb_AmCuoi.CheckedChanged += new System.EventHandler(this.rdb_AmCuoi_CheckedChanged);
            // 
            // rdb_Thanh
            // 
            this.rdb_Thanh.AutoSize = true;
            this.rdb_Thanh.Location = new System.Drawing.Point(573, 448);
            this.rdb_Thanh.Name = "rdb_Thanh";
            this.rdb_Thanh.Size = new System.Drawing.Size(56, 17);
            this.rdb_Thanh.TabIndex = 1;
            this.rdb_Thanh.TabStop = true;
            this.rdb_Thanh.Text = "Thanh";
            this.rdb_Thanh.UseVisualStyleBackColor = true;
            this.rdb_Thanh.CheckedChanged += new System.EventHandler(this.rdb_Thanh_CheckedChanged);
            // 
            // ListWordsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdb_Thanh);
            this.Controls.Add(this.rdb_AmCuoi);
            this.Controls.Add(this.rdb_AmChinh);
            this.Controls.Add(this.rdb_AmDem);
            this.Controls.Add(this.rdb_PhuAmDau);
            this.Controls.Add(this._treeView);
            this.Name = "ListWordsView";
            this.Size = new System.Drawing.Size(724, 492);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView _treeView;
        private System.Windows.Forms.RadioButton rdb_PhuAmDau;
        private System.Windows.Forms.RadioButton rdb_AmDem;
        private System.Windows.Forms.RadioButton rdb_AmChinh;
        private System.Windows.Forms.RadioButton rdb_AmCuoi;
        private System.Windows.Forms.RadioButton rdb_Thanh;
    }
}

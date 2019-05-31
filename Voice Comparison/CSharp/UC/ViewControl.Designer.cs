namespace UC
{
    partial class ViewControl
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
            this.components = new System.ComponentModel.Container();
            this.amTietBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rdb_Thanh = new System.Windows.Forms.RadioButton();
            this.rdb_AmCuoi = new System.Windows.Forms.RadioButton();
            this.rdb_AmChinh = new System.Windows.Forms.RadioButton();
            this.rdb_AmDem = new System.Windows.Forms.RadioButton();
            this.rdb_PhuAmDau = new System.Windows.Forms.RadioButton();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unicodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vanDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.thanhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amCuoiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amChinhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amDemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amDauDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vietnameseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.amTietBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // amTietBindingSource
            // 
            this.amTietBindingSource.DataMember = "AmTiet";
            this.amTietBindingSource.DataSource = typeof(Object.AmTietCarrier);
            // 
            // rdb_Thanh
            // 
            this.rdb_Thanh.AutoSize = true;
            this.rdb_Thanh.Location = new System.Drawing.Point(533, 437);
            this.rdb_Thanh.Name = "rdb_Thanh";
            this.rdb_Thanh.Size = new System.Drawing.Size(56, 17);
            this.rdb_Thanh.TabIndex = 2;
            this.rdb_Thanh.TabStop = true;
            this.rdb_Thanh.Text = "Thanh";
            this.rdb_Thanh.UseVisualStyleBackColor = true;
            // 
            // rdb_AmCuoi
            // 
            this.rdb_AmCuoi.AutoSize = true;
            this.rdb_AmCuoi.Location = new System.Drawing.Point(428, 437);
            this.rdb_AmCuoi.Name = "rdb_AmCuoi";
            this.rdb_AmCuoi.Size = new System.Drawing.Size(64, 17);
            this.rdb_AmCuoi.TabIndex = 3;
            this.rdb_AmCuoi.TabStop = true;
            this.rdb_AmCuoi.Text = "Âm Cuối";
            this.rdb_AmCuoi.UseVisualStyleBackColor = true;
            // 
            // rdb_AmChinh
            // 
            this.rdb_AmChinh.AutoSize = true;
            this.rdb_AmChinh.Location = new System.Drawing.Point(315, 437);
            this.rdb_AmChinh.Name = "rdb_AmChinh";
            this.rdb_AmChinh.Size = new System.Drawing.Size(72, 17);
            this.rdb_AmChinh.TabIndex = 4;
            this.rdb_AmChinh.TabStop = true;
            this.rdb_AmChinh.Text = "Âm Chính";
            this.rdb_AmChinh.UseVisualStyleBackColor = true;
            // 
            // rdb_AmDem
            // 
            this.rdb_AmDem.AutoSize = true;
            this.rdb_AmDem.Location = new System.Drawing.Point(209, 437);
            this.rdb_AmDem.Name = "rdb_AmDem";
            this.rdb_AmDem.Size = new System.Drawing.Size(65, 17);
            this.rdb_AmDem.TabIndex = 5;
            this.rdb_AmDem.TabStop = true;
            this.rdb_AmDem.Text = "Âm Đệm";
            this.rdb_AmDem.UseVisualStyleBackColor = true;
            // 
            // rdb_PhuAmDau
            // 
            this.rdb_PhuAmDau.AutoSize = true;
            this.rdb_PhuAmDau.Location = new System.Drawing.Point(83, 437);
            this.rdb_PhuAmDau.Name = "rdb_PhuAmDau";
            this.rdb_PhuAmDau.Size = new System.Drawing.Size(85, 17);
            this.rdb_PhuAmDau.TabIndex = 6;
            this.rdb_PhuAmDau.TabStop = true;
            this.rdb_PhuAmDau.Text = "Phụ Âm Đầu";
            this.rdb_PhuAmDau.UseVisualStyleBackColor = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // unicodeDataGridViewTextBoxColumn
            // 
            this.unicodeDataGridViewTextBoxColumn.DataPropertyName = "Unicode";
            this.unicodeDataGridViewTextBoxColumn.HeaderText = "Unicode";
            this.unicodeDataGridViewTextBoxColumn.Name = "unicodeDataGridViewTextBoxColumn";
            this.unicodeDataGridViewTextBoxColumn.ReadOnly = true;
            this.unicodeDataGridViewTextBoxColumn.Visible = false;
            // 
            // pathDataGridViewTextBoxColumn
            // 
            this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
            this.pathDataGridViewTextBoxColumn.HeaderText = "Đường Đẫn";
            this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
            this.pathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vanDataGridViewTextBoxColumn
            // 
            this.vanDataGridViewTextBoxColumn.DataPropertyName = "Van";
            this.vanDataGridViewTextBoxColumn.HeaderText = "Vần";
            this.vanDataGridViewTextBoxColumn.Name = "vanDataGridViewTextBoxColumn";
            this.vanDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // thanhDataGridViewTextBoxColumn
            // 
            this.thanhDataGridViewTextBoxColumn.DataPropertyName = "Thanh";
            this.thanhDataGridViewTextBoxColumn.HeaderText = "Thanh";
            this.thanhDataGridViewTextBoxColumn.Name = "thanhDataGridViewTextBoxColumn";
            this.thanhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amCuoiDataGridViewTextBoxColumn
            // 
            this.amCuoiDataGridViewTextBoxColumn.DataPropertyName = "AmCuoi";
            this.amCuoiDataGridViewTextBoxColumn.HeaderText = "Âm Cuối";
            this.amCuoiDataGridViewTextBoxColumn.Name = "amCuoiDataGridViewTextBoxColumn";
            this.amCuoiDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amChinhDataGridViewTextBoxColumn
            // 
            this.amChinhDataGridViewTextBoxColumn.DataPropertyName = "AmChinh";
            this.amChinhDataGridViewTextBoxColumn.HeaderText = "Âm Chính";
            this.amChinhDataGridViewTextBoxColumn.Name = "amChinhDataGridViewTextBoxColumn";
            this.amChinhDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amDemDataGridViewTextBoxColumn
            // 
            this.amDemDataGridViewTextBoxColumn.DataPropertyName = "AmDem";
            this.amDemDataGridViewTextBoxColumn.HeaderText = "Âm Đệm";
            this.amDemDataGridViewTextBoxColumn.Name = "amDemDataGridViewTextBoxColumn";
            this.amDemDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amDauDataGridViewTextBoxColumn
            // 
            this.amDauDataGridViewTextBoxColumn.DataPropertyName = "AmDau";
            this.amDauDataGridViewTextBoxColumn.HeaderText = "Âm Đầu";
            this.amDauDataGridViewTextBoxColumn.Name = "amDauDataGridViewTextBoxColumn";
            this.amDauDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // vietnameseDataGridViewTextBoxColumn
            // 
            this.vietnameseDataGridViewTextBoxColumn.DataPropertyName = "Vietnamese";
            this.vietnameseDataGridViewTextBoxColumn.HeaderText = "Âm Tiết";
            this.vietnameseDataGridViewTextBoxColumn.Name = "vietnameseDataGridViewTextBoxColumn";
            this.vietnameseDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vietnameseDataGridViewTextBoxColumn,
            this.amDauDataGridViewTextBoxColumn,
            this.amDemDataGridViewTextBoxColumn,
            this.amChinhDataGridViewTextBoxColumn,
            this.amCuoiDataGridViewTextBoxColumn,
            this.thanhDataGridViewTextBoxColumn,
            this.vanDataGridViewTextBoxColumn,
            this.pathDataGridViewTextBoxColumn,
            this.unicodeDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.amTietBindingSource;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(675, 419);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView_CellPainting);
            // 
            // ViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rdb_Thanh);
            this.Controls.Add(this.rdb_AmCuoi);
            this.Controls.Add(this.rdb_AmChinh);
            this.Controls.Add(this.rdb_AmDem);
            this.Controls.Add(this.rdb_PhuAmDau);
            this.Controls.Add(this.dataGridView);
            this.Name = "ViewControl";
            this.Size = new System.Drawing.Size(675, 487);
            ((System.ComponentModel.ISupportInitialize)(this.amTietBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource amTietBindingSource;
        private System.Windows.Forms.RadioButton rdb_Thanh;
        private System.Windows.Forms.RadioButton rdb_AmCuoi;
        private System.Windows.Forms.RadioButton rdb_AmChinh;
        private System.Windows.Forms.RadioButton rdb_AmDem;
        private System.Windows.Forms.RadioButton rdb_PhuAmDau;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unicodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vanDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn thanhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amCuoiDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amChinhDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amDemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amDauDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vietnameseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridView dataGridView;

    }
}

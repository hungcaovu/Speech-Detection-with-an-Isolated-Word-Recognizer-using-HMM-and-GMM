namespace UC
{
    partial class AmVietViewControl
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
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.amTietBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmDau = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmDem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmChinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmCuoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVietnamese = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnicode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cb_AmDau = new System.Windows.Forms.CheckBox();
            this.cb_AmDem = new System.Windows.Forms.CheckBox();
            this.cb_AmChinh = new System.Windows.Forms.CheckBox();
            this.cb_AmCuoi = new System.Windows.Forms.CheckBox();
            this.cb_Thanh = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.amTietBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.DataSource = this.amTietBindingSource;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(518, 334);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // amTietBindingSource
            // 
            this.amTietBindingSource.DataMember = "AmTiet";
            this.amTietBindingSource.DataSource = typeof(Object.AmTietCarrier);
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colAmDau,
            this.colAmDem,
            this.colAmChinh,
            this.colAmCuoi,
            this.colThanh,
            this.colPath,
            this.colVietnamese,
            this.colVan,
            this.colUnicode});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.gridView_CustomDrawGroupRow);
            this.gridView.CustomRowCellEdit += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.gridView_CustomRowCellEdit);
            this.gridView.CustomColumnGroup += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(this.gridView_CustomColumnGroup);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            this.gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView_CellValueChanged);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            this.gridView.DataSourceChanged += new System.EventHandler(this.gridView_DataSourceChanged);
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            this.colId.Width = 50;
            // 
            // colAmDau
            // 
            this.colAmDau.Caption = "Âm Đầu";
            this.colAmDau.FieldName = "AmDau";
            this.colAmDau.Name = "colAmDau";
            this.colAmDau.Visible = true;
            this.colAmDau.VisibleIndex = 3;
            this.colAmDau.Width = 50;
            // 
            // colAmDem
            // 
            this.colAmDem.Caption = "Âm Đệm";
            this.colAmDem.FieldName = "AmDem";
            this.colAmDem.Name = "colAmDem";
            this.colAmDem.Visible = true;
            this.colAmDem.VisibleIndex = 4;
            this.colAmDem.Width = 50;
            // 
            // colAmChinh
            // 
            this.colAmChinh.Caption = "Âm Chính";
            this.colAmChinh.FieldName = "AmChinh";
            this.colAmChinh.Name = "colAmChinh";
            this.colAmChinh.Visible = true;
            this.colAmChinh.VisibleIndex = 5;
            this.colAmChinh.Width = 50;
            // 
            // colAmCuoi
            // 
            this.colAmCuoi.Caption = "Âm Cuối";
            this.colAmCuoi.FieldName = "AmCuoi";
            this.colAmCuoi.Name = "colAmCuoi";
            this.colAmCuoi.Visible = true;
            this.colAmCuoi.VisibleIndex = 6;
            this.colAmCuoi.Width = 50;
            // 
            // colThanh
            // 
            this.colThanh.Caption = "Thanh";
            this.colThanh.FieldName = "Thanh";
            this.colThanh.Name = "colThanh";
            this.colThanh.Visible = true;
            this.colThanh.VisibleIndex = 7;
            this.colThanh.Width = 50;
            // 
            // colPath
            // 
            this.colPath.Caption = "Đường Dẫn";
            this.colPath.FieldName = "Path";
            this.colPath.Name = "colPath";
            this.colPath.OptionsColumn.AllowEdit = false;
            this.colPath.Visible = true;
            this.colPath.VisibleIndex = 8;
            this.colPath.Width = 64;
            // 
            // colVietnamese
            // 
            this.colVietnamese.Caption = "Âm Tiết";
            this.colVietnamese.FieldName = "Vietnamese";
            this.colVietnamese.Name = "colVietnamese";
            this.colVietnamese.Visible = true;
            this.colVietnamese.VisibleIndex = 1;
            this.colVietnamese.Width = 50;
            // 
            // colVan
            // 
            this.colVan.FieldName = "Van";
            this.colVan.Name = "colVan";
            this.colVan.Visible = true;
            this.colVan.VisibleIndex = 9;
            this.colVan.Width = 36;
            // 
            // colUnicode
            // 
            this.colUnicode.FieldName = "Unicode";
            this.colUnicode.Name = "colUnicode";
            this.colUnicode.Visible = true;
            this.colUnicode.VisibleIndex = 2;
            this.colUnicode.Width = 50;
            // 
            // cb_AmDau
            // 
            this.cb_AmDau.AutoSize = true;
            this.cb_AmDau.Location = new System.Drawing.Point(347, 352);
            this.cb_AmDau.Name = "cb_AmDau";
            this.cb_AmDau.Size = new System.Drawing.Size(64, 17);
            this.cb_AmDau.TabIndex = 12;
            this.cb_AmDau.Text = "Âm Đầu";
            this.cb_AmDau.UseVisualStyleBackColor = true;
            this.cb_AmDau.CheckedChanged += new System.EventHandler(this.cb_AmDau_CheckedChanged);
            // 
            // cb_AmDem
            // 
            this.cb_AmDem.AutoSize = true;
            this.cb_AmDem.Location = new System.Drawing.Point(138, 352);
            this.cb_AmDem.Name = "cb_AmDem";
            this.cb_AmDem.Size = new System.Drawing.Size(66, 17);
            this.cb_AmDem.TabIndex = 12;
            this.cb_AmDem.Text = "Âm Đệm";
            this.cb_AmDem.UseVisualStyleBackColor = true;
            this.cb_AmDem.CheckedChanged += new System.EventHandler(this.cb_AmDem_CheckedChanged);
            // 
            // cb_AmChinh
            // 
            this.cb_AmChinh.AutoSize = true;
            this.cb_AmChinh.Location = new System.Drawing.Point(28, 352);
            this.cb_AmChinh.Name = "cb_AmChinh";
            this.cb_AmChinh.Size = new System.Drawing.Size(71, 17);
            this.cb_AmChinh.TabIndex = 12;
            this.cb_AmChinh.Text = "Âm Chính";
            this.cb_AmChinh.UseVisualStyleBackColor = true;
            this.cb_AmChinh.CheckedChanged += new System.EventHandler(this.cb_AmChinh_CheckedChanged);
            // 
            // cb_AmCuoi
            // 
            this.cb_AmCuoi.AutoSize = true;
            this.cb_AmCuoi.Location = new System.Drawing.Point(243, 352);
            this.cb_AmCuoi.Name = "cb_AmCuoi";
            this.cb_AmCuoi.Size = new System.Drawing.Size(65, 17);
            this.cb_AmCuoi.TabIndex = 12;
            this.cb_AmCuoi.Text = "Âm Cuối";
            this.cb_AmCuoi.UseVisualStyleBackColor = true;
            this.cb_AmCuoi.CheckedChanged += new System.EventHandler(this.cb_AmCuoi_CheckedChanged);
            // 
            // cb_Thanh
            // 
            this.cb_Thanh.AutoSize = true;
            this.cb_Thanh.Location = new System.Drawing.Point(450, 352);
            this.cb_Thanh.Name = "cb_Thanh";
            this.cb_Thanh.Size = new System.Drawing.Size(56, 17);
            this.cb_Thanh.TabIndex = 12;
            this.cb_Thanh.Text = "Thanh";
            this.cb_Thanh.UseVisualStyleBackColor = true;
            this.cb_Thanh.CheckedChanged += new System.EventHandler(this.cb_Thanh_CheckedChanged);
            // 
            // AmVietViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cb_Thanh);
            this.Controls.Add(this.cb_AmCuoi);
            this.Controls.Add(this.cb_AmChinh);
            this.Controls.Add(this.cb_AmDem);
            this.Controls.Add(this.cb_AmDau);
            this.Controls.Add(this.gridControl);
            this.Name = "AmVietViewControl";
            this.Size = new System.Drawing.Size(518, 380);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.amTietBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.BindingSource amTietBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colAmDau;
        private DevExpress.XtraGrid.Columns.GridColumn colAmDem;
        private DevExpress.XtraGrid.Columns.GridColumn colAmChinh;
        private DevExpress.XtraGrid.Columns.GridColumn colAmCuoi;
        private DevExpress.XtraGrid.Columns.GridColumn colThanh;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colVietnamese;
        private DevExpress.XtraGrid.Columns.GridColumn colVan;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private System.Windows.Forms.CheckBox cb_AmDau;
        private System.Windows.Forms.CheckBox cb_AmDem;
        private System.Windows.Forms.CheckBox cb_AmChinh;
        private System.Windows.Forms.CheckBox cb_AmCuoi;
        private System.Windows.Forms.CheckBox cb_Thanh;
        private DevExpress.XtraGrid.Columns.GridColumn colUnicode;
    }
}

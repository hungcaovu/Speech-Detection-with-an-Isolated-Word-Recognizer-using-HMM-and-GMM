namespace Voice_Comparasion
{
    partial class TrainingFilesForm
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
            this.components = new System.ComponentModel.Container();
            this.gridTrain = new DevExpress.XtraGrid.GridControl();
            this.trainFileDataTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.viewTrain = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWord = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPath = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStart = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEnd = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.right_popMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridTrain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainFileDataTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTrain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.right_popMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTrain
            // 
            this.gridTrain.DataSource = this.trainFileDataTableBindingSource;
            this.gridTrain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTrain.Location = new System.Drawing.Point(0, 0);
            this.gridTrain.MainView = this.viewTrain;
            this.gridTrain.Name = "gridTrain";
            this.gridTrain.Size = new System.Drawing.Size(574, 428);
            this.gridTrain.TabIndex = 0;
            this.gridTrain.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTrain});
            this.gridTrain.DataSourceChanged += new System.EventHandler(this.gridTrain_DataSourceChanged);
            // 
            // trainFileDataTableBindingSource
            // 
            this.trainFileDataTableBindingSource.DataSource = typeof(Object.TrainFilesCarrier.TrainFileDataTable);
            // 
            // viewTrain
            // 
            this.viewTrain.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWord,
            this.colPath,
            this.colStart,
            this.colEnd,
            this.colID});
            this.viewTrain.GridControl = this.gridTrain;
            this.viewTrain.Name = "viewTrain";
            this.viewTrain.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.viewTrain_CustomDrawGroupRow);
            this.viewTrain.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.viewTrain_FocusedRowChanged);
            this.viewTrain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.viewTrain_KeyDown);
            // 
            // colWord
            // 
            this.colWord.FieldName = "Word";
            this.colWord.Name = "colWord";
            this.colWord.Visible = true;
            this.colWord.VisibleIndex = 1;
            this.colWord.Width = 108;
            // 
            // colPath
            // 
            this.colPath.FieldName = "Path";
            this.colPath.Name = "colPath";
            this.colPath.Visible = true;
            this.colPath.VisibleIndex = 2;
            this.colPath.Width = 204;
            // 
            // colStart
            // 
            this.colStart.FieldName = "Start";
            this.colStart.Name = "colStart";
            this.colStart.Visible = true;
            this.colStart.VisibleIndex = 3;
            this.colStart.Width = 93;
            // 
            // colEnd
            // 
            this.colEnd.FieldName = "End";
            this.colEnd.Name = "colEnd";
            this.colEnd.Visible = true;
            this.colEnd.VisibleIndex = 4;
            this.colEnd.Width = 80;
            // 
            // colID
            // 
            this.colID.FieldName = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = true;
            this.colID.VisibleIndex = 0;
            this.colID.Width = 71;
            // 
            // right_popMenu
            // 
            this.right_popMenu.Name = "right_popMenu";
            // 
            // TrainingFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 428);
            this.Controls.Add(this.gridTrain);
            this.Name = "TrainingFilesForm";
            this.Text = "Training Files";
            ((System.ComponentModel.ISupportInitialize)(this.gridTrain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainFileDataTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTrain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.right_popMenu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridTrain;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTrain;
        private System.Windows.Forms.BindingSource trainFileDataTableBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colWord;
        private DevExpress.XtraGrid.Columns.GridColumn colPath;
        private DevExpress.XtraGrid.Columns.GridColumn colStart;
        private DevExpress.XtraGrid.Columns.GridColumn colEnd;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraBars.PopupMenu right_popMenu;
    }
}
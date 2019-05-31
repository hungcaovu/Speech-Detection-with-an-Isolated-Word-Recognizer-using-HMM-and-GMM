using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Model;
using Object;
using Object.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voice_Comparasion
{
    public partial class TrainingFilesForm : Form
    {
        TrainingTask _trainTask;

        private event RecallEntryEventHandler handler;

        public event RecallEntryEventHandler RecalledEntry {
            add {
                handler += value;
            }
            remove {
                handler -= value;
            }
        }
        public TrainingFilesForm(TrainingTask trainTask)
        {
            _trainTask = trainTask;
            _trainTask.Load(VCDir.Instance.TrainXmlFile);
            InitializeComponent();
            gridTrain.DataSource = _trainTask.Entries;
        }
        public bool AddRow(string word, string path, int start, int end) {
            TrainFilesCarrier.TrainFileRow row = _trainTask.Entries.NewTrainFileRow();
            row.End = end;
            row.Start = start;
            row.Word = word;
            row.Path = path;
            return AddRow(row);
        }
        public bool AddRow(TrainFilesCarrier.TrainFileRow row) {
            bool res = _trainTask.AddorUpdate(row);
            _trainTask.Save(VCDir.Instance.TrainXmlFile);
            gridTrain.DataSource = _trainTask.Entries;
            return res;
        }
        private void gridTrain_DataSourceChanged(object sender, EventArgs e)
        {
            _trainTask.Entries = gridTrain.DataSource as TrainFilesCarrier.TrainFileDataTable;
            _trainTask.Save(VCDir.Instance.TrainXmlFile);
        }

        private void viewTrain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (viewTrain.IsGroupRow(e.FocusedRowHandle))
            {
                bool expanded = viewTrain.GetRowExpanded(e.FocusedRowHandle);
                viewTrain.SetRowExpanded(e.FocusedRowHandle, !expanded);
            }
            else
            {
                TrainFilesCarrier.TrainFileRow row = (TrainFilesCarrier.TrainFileRow)viewTrain.GetDataRow(e.FocusedRowHandle);
                if (handler != null)
                {
                    handler(this, new RecallEntryEventArgs(row));
                }
            }
        }

        public bool Train() {
            return _trainTask.Train();
        }

        private void viewTrain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) {
                GridView view = sender as GridView;
                view.DeleteRow(view.FocusedRowHandle);
            }
        }

        private void viewTrain_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            int numElement = view.GetChildRowCount(e.RowHandle);
            int rowHdl = view.GetChildRowHandle(e.RowHandle, 0);
            if (rowHdl >= 0)
            {
                TrainFilesCarrier.TrainFileRow dataRow = view.GetDataRow(rowHdl) as TrainFilesCarrier.TrainFileRow;
                info.GroupText = String.Format(" {0} - {1}", dataRow.Word, numElement);
            }
        }
    }
}

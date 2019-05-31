using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Object;
using Model;
using Object.Event;
using Object.Enum;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;

namespace UC
{
    public partial class AmVietViewControl : DevExpress.XtraEditors.XtraUserControl
    {

        //private bool _isPlayAudio = false;
        private PlayWave _playAudio = null;
        
        private event SelectAmTietEventHandler selectAmTietHandler;

        public event SelectAmTietEventHandler SelectAmTietHandler
        {
            add
            {
                selectAmTietHandler += value;
            }
            remove
            {
                selectAmTietHandler -= value;
            }
        }
        public AmVietViewControl()
        {
            InitializeComponent();
            UpdateDataTable(VCContext.Instance.ListAmTiet);
        }
        AmTietCarrier.AmTietDataTable dtbAmTiet;
        public AmTietCarrier.AmTietDataTable AmTietTable
        {
            get
            {
                return dtbAmTiet;
            }
        }
        private AmTietCarrier.AmTietRow ToAmTietRow(AmTiet amTiet)
        {
            AmTietCarrier.AmTietRow amTietRow = dtbAmTiet.NewAmTietRow();
            amTietRow.AmDau = amTiet.PhuAmDau;
            amTietRow.AmChinh = amTiet.AmChinh;
            amTietRow.AmCuoi = amTiet.AmCuoi;
            amTietRow.AmDem = amTiet.AmDem;
            amTietRow.Unicode = amTiet.Unicode;
            amTietRow.Vietnamese = amTiet.Vietnamese;
            amTietRow.Thanh = amTiet.Thanh;
            amTietRow.Path = amTiet.Path;
            amTietRow.Van = amTiet.Van;
            return amTietRow;
        }
        public void UpdateDataTable(List<AmTiet> list)
        {

            if (list == null) return;
            if (InvokeRequired)
            {
                Action<List<AmTiet>> action = new Action<List<AmTiet>>(UpdateDataTable);
                Invoke(action, list);
            }
            else
            {
                dtbAmTiet = new AmTietCarrier.AmTietDataTable();
                foreach (AmTiet amTiet in list)
                {
                    AmTietCarrier.AmTietRow amTietRow = ToAmTietRow(amTiet);
                    dtbAmTiet.AddAmTietRow(amTietRow);
                }

                gridControl.DataSource = dtbAmTiet;
                gridControl.Refresh();
            }
        }

        private void GroupSelect(RBDSelected type, bool check)
        {

            if (RBDSelected.AMCHINH == type)
            {
                if (check)
                {
                    colAmChinh.Group();
                }
                else
                {
                    colAmChinh.UnGroup();
                }

            }
            else if (RBDSelected.AMCUOI == type)
            {
                if (check)
                {
                    colAmCuoi.Group();
                }
                else
                {
                    colAmCuoi.UnGroup();
                }
            }
            else if (RBDSelected.AMDEM == type)
            {
                if (check)
                {
                    colAmDem.Group();
                }
                else
                {
                    colAmDem.UnGroup();
                }

            }
            else if (RBDSelected.PHUAMDAU == type)
            {
                if (check)
                {
                    colAmDau.Group();
                }
                else
                {
                    colAmDau.UnGroup();
                }
            }
            else if (RBDSelected.THANH == type)
            {
                if (check)
                {
                    colThanh.Group();
                }
                else
                {
                    colThanh.UnGroup();
                }
            }
            else if (RBDSelected.NONE == type)
            {
                if (check)
                {
                    colId.Group();
                }
                else
                {
                    colId.UnGroup();
                }
            }
        }

        private void cb_AmChinh_CheckedChanged(object sender, EventArgs e)
        {
            GroupSelect(RBDSelected.AMCHINH, cb_AmChinh.Checked);
        }

        private void cb_AmDem_CheckedChanged(object sender, EventArgs e)
        {
            GroupSelect(RBDSelected.AMDEM, cb_AmDem.Checked);
        }

        private void cb_AmCuoi_CheckedChanged(object sender, EventArgs e)
        {
            GroupSelect(RBDSelected.AMCUOI, cb_AmCuoi.Checked);
        }

        private void cb_AmDau_CheckedChanged(object sender, EventArgs e)
        {
            GroupSelect(RBDSelected.PHUAMDAU, cb_AmDau.Checked);
        }

        private void cb_Thanh_CheckedChanged(object sender, EventArgs e)
        {
            GroupSelect(RBDSelected.THANH, cb_Thanh.Checked);
        }

        private void gridView_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            string caption = info.Column.Caption;
            if (info.Column.Caption == string.Empty)
                caption = info.Column.ToString();
            info.GroupText = string.Format("{0} : {1} Count = {2}", caption, info.GroupValueText, view.GetChildRowCount(e.RowHandle));
        }

        private void gridView_CustomColumnGroup(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {

        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.IsGroupRow(e.FocusedRowHandle))
            {
                bool expanded = gridView.GetRowExpanded(e.FocusedRowHandle);
                gridView.SetRowExpanded(e.FocusedRowHandle, !expanded);
            }
            else
            {
                AmTietCarrier.AmTietRow amTietRow = (AmTietCarrier.AmTietRow)gridView.GetDataRow(e.FocusedRowHandle);
                if (selectAmTietHandler != null)
                {
                    selectAmTietHandler(this, new SelectAmTietEventArgs(Transform.ToAmTiet(amTietRow)));
                }
            }
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);
            if ((info.InRow || info.InRowCell) && info.Column != null)
            {
                AmTietCarrier.AmTietRow focusRow = view.GetDataRow(info.RowHandle) as AmTietCarrier.AmTietRow;
                PlayAudio(VCDir.Instance.AudioDir + focusRow.Path);
                if (selectAmTietHandler != null)
                {
                    selectAmTietHandler(this, new SelectAmTietEventArgs(Transform.ToAmTiet(focusRow)));
                }
            }
        }

        private void PlayAudio(string path) {
            //_isPlayAudio = true;
            try
            {
                _playAudio = new PlayWave(path);
                _playAudio.RaisePlayStop += YourPlayStop;
                _playAudio.Play();
            }
            catch (Exception) {
                Debug.WriteLine("Some Bug call play\n");
            }
        }

        private void YourPlayStop(object obj, PlayWaveStopEventArgs e)
        {
            Debug.WriteLine("Stop Audio Ref");
            //_isPlayAudio = false;
        }

        private void gridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            Debug.WriteLine("Data Source Changed: Edit change");
        }

        private void gridView_DataSourceChanged(object sender, EventArgs e)
        {
            //Debug.WriteLine("Data Source Changed");
        }

        AmTietCarrier.AmTietRow GetRow(int rowHandle) {
            return gridView.GetDataRow(rowHandle) as AmTietCarrier.AmTietRow;
        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            AmTietCarrier.AmTietRow row = GetRow(e.RowHandle);
            VCContext.Instance.UpdateAmTiet(row);
        }
    }
}

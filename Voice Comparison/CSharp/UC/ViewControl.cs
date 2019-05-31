using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Object;
using Model;
namespace UC
{
    public partial class ViewControl : UserControl
    {
       
        public ViewControl()
        {
            InitializeComponent();
            UpdateDataTable(VCContext.Instance.ListAmTiet);
        }
        AmTietCarrier.AmTietDataTable dtbAmTiet;
        public AmTietCarrier.AmTietDataTable AmTietTable
        {
            get {
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
            amTietRow.Unicode = amTiet.AmTietTelex;
            amTietRow.Vietnamese = amTiet.AmTietTiengViet;
            amTietRow.Thanh = amTiet.Thanh;
            amTietRow.Path = amTiet.Path;
            amTietRow.Van = amTiet.Van;
            return amTietRow;
        }
        private void UpdateDataTable(List<AmTiet> list) {

            if (list == null) return;
            if (InvokeRequired)
            {
                Action<List<AmTiet>> action = new Action<List<AmTiet>>(UpdateDataTable);
                Invoke(action, list);
            }
            else {
                dtbAmTiet = new AmTietCarrier.AmTietDataTable();
                foreach (AmTiet amTiet in list)
                {
                    AmTietCarrier.AmTietRow amTietRow = ToAmTietRow(amTiet);
                    dtbAmTiet.AddAmTietRow(amTietRow);
                }

                dataGridView.DataSource = dtbAmTiet;
            }
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }
    }
}

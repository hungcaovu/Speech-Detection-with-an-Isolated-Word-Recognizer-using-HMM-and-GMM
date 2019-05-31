using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Object;
using Model;
using Object.Enum;

namespace UC
{
    

    public partial class ListWordsView : UserControl
    {
        List<AmTiet> _listAmTiet;
        public ListWordsView()
        {
            _listAmTiet = VCContext.Instance.ListAmTiet;
            InitializeComponent();
        }

        private void FillToTreeView(RBDSelected type)
        {
            Sort(type);
            _treeView.Nodes.Clear();
            if(_listAmTiet == null || _listAmTiet.Count == 0) return;
            string group = string.Empty;
            TreeNode treNodeGroup = null;
            foreach (AmTiet amTiet in _listAmTiet) {
                if (group != amTiet.GetValue(type))
                {
                    group = amTiet.GetValue(type);
                    treNodeGroup = new TreeNode(group);
                    _treeView.Nodes.Add(treNodeGroup);
                }
                else if (amTiet.GetValue(type) == string.Empty)
                {
                    group = amTiet.GetValue(type);
                    treNodeGroup = new TreeNode(group);
                    _treeView.Nodes.Add(treNodeGroup);
                }
                TreeNode treNode = new TreeNode(amTiet.AmTietTiengViet);
                treNodeGroup.Nodes.Add(treNode);
            }
        }

        
        private void UpdateTreeView(RBDSelected type) {
            //Debug.WriteLine("Selected Raido Button: {0}", type.ToString());
            FillToTreeView(type);
        }



        private void rdb_PhuAmDau_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTreeView(RBDSelected.PHUAMDAU);
        }

        private void rdb_AmDem_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTreeView(RBDSelected.AMDEM);
        }

        private void rdb_AmChinh_CheckedChanged(object sender, EventArgs e)
        {

            UpdateTreeView(RBDSelected.AMCHINH);
        }

        private void rdb_AmCuoi_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTreeView(RBDSelected.AMCUOI);
        }

        private void rdb_Thanh_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTreeView(RBDSelected.THANH);
        }


        private void Sort(RBDSelected type) {

            if (_listAmTiet != null) {
                if (type == RBDSelected.NONE)
                {
                    _listAmTiet = _listAmTiet.OrderBy(x => x.Path).ToList<AmTiet>();
                }
                else if (type == RBDSelected.THANH)
                {
                    _listAmTiet = _listAmTiet.OrderBy(x => x.Thanh).ToList<AmTiet>();
                }
                else if (type == RBDSelected.AMCUOI)
                {
                    _listAmTiet = _listAmTiet.OrderBy(x => x.AmCuoi).ToList<AmTiet>();
                }
                else if (type == RBDSelected.AMCHINH)
                {
                    _listAmTiet = _listAmTiet.OrderBy(x => x.AmChinh).ToList<AmTiet>();
                }
                else if (type == RBDSelected.AMDEM)
                {
                    _listAmTiet = _listAmTiet.OrderBy(x => x.AmDem).ToList<AmTiet>();
                }
                else if (type == RBDSelected.PHUAMDAU)
                {
                    _listAmTiet = _listAmTiet.OrderBy(x => x.PhuAmDau).ToList<AmTiet>();
                }
            }
        }
    }
}

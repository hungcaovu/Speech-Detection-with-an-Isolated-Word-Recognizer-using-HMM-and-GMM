using Object.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object
{
    public class AmTiet
    {
        public string Unicode { set; get; }
        public string Vietnamese { set; get; }
        public string PhuAmDau { set; get; }
        public string Van { set; get; }
        public string AmDem { set; get; }
        public string AmChinh { set; get; }
        public string AmCuoi { set; get; }
        public string Thanh { set; get; }

        public string Path { set; get; }

        public Boolean Edited { set; get; }

        public override string ToString()
        {
            return string.Format("{0}", Vietnamese);
            //try
            //{
            //    return string.Format("{0} {1} {2} {3} {4}", PhuAmDau, AmDem, AmChinh, AmCuoi, Thanh);
            //}
            //catch(Exception) {
            //    return string.Format("{0}", AmTietTiengViet);
            //}
        }
        public string GetValue(RBDSelected type) {
            if (type == RBDSelected.NONE)
            {
                return Path;
            }
            else if (type == RBDSelected.THANH)
            {
                return Thanh;
            }
            else if (type == RBDSelected.AMCUOI)
            {
                return AmCuoi;
            }
            else if (type == RBDSelected.AMCHINH)
            {
                return AmChinh;
            }
            else if (type == RBDSelected.AMDEM)
            {
                return AmDem;
            }
            else if (type == RBDSelected.PHUAMDAU)
            {
                return PhuAmDau;
            }
            return "";
        }
    }
}

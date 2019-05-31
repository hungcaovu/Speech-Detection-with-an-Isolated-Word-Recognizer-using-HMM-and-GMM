using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object
{
    public class Transform
    {
        public static AmTiet ToAmTiet(AmTietCarrier.AmTietRow amTietRow){
            AmTiet amTiet = new AmTiet();
            amTiet.AmChinh = amTietRow.AmChinh;
            amTiet.AmCuoi = amTietRow.AmCuoi;
            amTiet.AmDem = amTietRow.AmDem;
            amTiet.Unicode = amTietRow.Unicode;
            amTiet.Vietnamese = amTietRow.Vietnamese;
            amTiet.Thanh = amTietRow.Thanh;
            amTiet.Path = amTietRow.Path;
            amTiet.Van = amTietRow.Van;
            amTiet.Edited = amTietRow.Edited;
            return amTiet;
        }

       
    }
}

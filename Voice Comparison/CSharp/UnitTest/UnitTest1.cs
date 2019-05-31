using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Object;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest_PaserWorkTask
    {
        [TestMethod]
        public void TestLoad()
        {
            PaserWordTask task = new PaserWordTask();
            task.LoadData(@"I:\Dropbox\Dropbox\Luan Van Thac Si\RefListWordsFull.xml");
            task.UpdateVanTrongAmTiet();
        }


        [TestMethod]
        public void TestUpdateLoad() {
            PaserWordTask task = new PaserWordTask();
            task.LoadData(@"C:\Users\hungc\Desktop\Project\Binary\Voice Comparasion\Debug\Data\Xml\RefListWordsFull.xml");
            task.UpdateVanTrongAmTiet();
            task.UpdateListWord(@"C:\Users\hungc\Desktop\Project\Binary\Voice Comparasion\Debug\Data\Xml\RefListWordsFull_Test.xml");
        }

        [TestMethod]
        public void OrderBy() {
            AmTiet amt1 = new AmTiet();
            amt1.Path = "abc";
            AmTiet amt2 = new AmTiet();
            amt2.Path = "bc";
            AmTiet amt3 = new AmTiet();
            amt3.Path = "c";
            List<AmTiet> _list = new List<AmTiet>();
            _list.Add(amt1);
            _list.Add(amt2);
            _list.Add(amt3);

            //_list.OrderBy(x => x.Pa).ToList();
        }
    }
}

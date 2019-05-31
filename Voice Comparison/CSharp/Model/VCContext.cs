using Object;
using Object.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VCContext
    {
        private List<AmTiet> _listAmTiet;
        private FileSystemWatcher _watcherListFile;

        static private VCContext instance = null;

        private MfccOptions option;
        public MfccOptions MFCCOptions
        {
            set
            {
                option = value;
                SaveSetting();
            }
            get
            {
                return option;
            }
        }
        private VCContext()
        {
            _watcherListFile = new FileSystemWatcher(VCDir.Instance.XMLDir);
            _watcherListFile.NotifyFilter = NotifyFilters.LastWrite;
            _watcherListFile.Filter = ".xml";
            _watcherListFile.Changed += FileSystemChangeEventHandler;
            _watcherListFile.EnableRaisingEvents = true;
            option = new MfccOptions();
            option.LoadFromXML(VCDir.Instance.SettingFile);
            PaserWordTask listWordParser = new PaserWordTask();
            if (listWordParser.LoadData(VCDir.Instance.ListWordDir))
            {
                _listAmTiet = listWordParser.ListAmTiet;
            }
        }
        private void FileSystemChangeEventHandler(object sender, FileSystemEventArgs e) {
            if (e.FullPath == VCDir.Instance.ListWordDir)
            {
                PaserWordTask listWordParser = new PaserWordTask();
                if (listWordParser.LoadData(VCDir.Instance.ListWordDir))
                {
                    _listAmTiet = listWordParser.ListAmTiet;
                }

            }
            Debug.WriteLine("File Changed");
        }
        private void SaveSetting()
        {
            option.StoreToXML(VCDir.Instance.SettingFile);
        }
        public static VCContext Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    instance = new VCContext();
                    return instance;
                }
            }
        }
        public List<AmTiet> ListAmTiet
        {
            get
            {
                return _listAmTiet;
            }
        }
        public void UpdateAmTiet(AmTietCarrier.AmTietRow row) {
            if (row != null)
            {
                foreach (AmTiet amTiet in _listAmTiet)
                {
                    if (amTiet.Path == row.Path)
                    {
                        amTiet.Thanh = row.Thanh;
                        if(amTiet.Vietnamese != row.Vietnamese || amTiet.Unicode != row.Unicode){
                            row.Edited = false;
                        } else {
                            row.Edited = true;
                        }

                        amTiet.Vietnamese = row.Vietnamese;
                        amTiet.Unicode = row.Unicode;
                        amTiet.PhuAmDau = row.AmDau;
                        amTiet.AmDem = row.AmDem;
                        amTiet.AmChinh = row.AmChinh;
                        amTiet.AmCuoi = row.AmCuoi;
                        amTiet.Edited = row.Edited;
                    }
                }
                _watcherListFile.EnableRaisingEvents = false;
                PaserWordTask.UpdateAWord(VCDir.Instance.ListWordDir, row);
                _watcherListFile.EnableRaisingEvents = true;
            }
        }
    }
}

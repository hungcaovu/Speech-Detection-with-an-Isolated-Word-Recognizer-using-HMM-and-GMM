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
using UC;

namespace Voice_Comparasion
{
    public partial class SettingForm : Form
    {
        public event ChangedSettingEvent SettingChanged {
            add {
                mfccSetting.ChangedSetting += value;
            }
            remove {
                mfccSetting.ChangedSetting -= value;
            }
        }

        public event SettingChangedEventHandler Changed
        {
            add
            {
                mfccSetting.Changed += value;
            }
            remove
            {
                mfccSetting.Changed -= value;
            }
        }

		public MfccOptions Options {
            set{
                mfccSetting.Options = value;
            }
            get{
                return mfccSetting.Options;
            }
        }
        public SettingForm()
        {
            InitializeComponent();
        }
    }
}

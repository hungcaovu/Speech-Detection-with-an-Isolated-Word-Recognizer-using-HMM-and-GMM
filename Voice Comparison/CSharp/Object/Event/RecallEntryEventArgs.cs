using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object.Event
{
    public class RecallEntryEventArgs
    {
       public  TrainFilesCarrier.TrainFileRow RecalledRow;
        public RecallEntryEventArgs(TrainFilesCarrier.TrainFileRow row) {
            RecalledRow = row;
        }
    }
}

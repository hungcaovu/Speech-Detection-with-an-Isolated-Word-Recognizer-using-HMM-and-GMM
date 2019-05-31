using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVoiceDetector.Task
{
    public class FolderParser
    {
        private List<string> folders = null;
        public List<string> Folders
        {
            get { return folders; }
        }
        public FolderParser(string path)
        {
            try
            {
                string [] sub = Directory.GetDirectories(path);
                if (sub != null)
                {
                    folders = sub.ToList<string>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
                throw ex;
            }
        }
    }
}

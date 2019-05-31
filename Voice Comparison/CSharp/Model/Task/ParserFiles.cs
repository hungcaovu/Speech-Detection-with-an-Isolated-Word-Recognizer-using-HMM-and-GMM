using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanVoiceDetector.Task
{
    public class ParserFiles
    {
        private List<string> names = null;
        public List<string> Names
        {
            get { return names; }
        }
        public ParserFiles(string dir, string file_type) {
            try
            {
                string[] alls = Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly);
                names = alls.Where(s => (s.ToUpper().EndsWith(file_type.ToUpper()))).ToList<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : {0}", ex.Message);
                throw ex;
            }
        }
    }
}

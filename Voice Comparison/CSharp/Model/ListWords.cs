using Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Object.Event;
using System.Diagnostics;

namespace Model
{
    public class ListWords
    {
        public Dictionary<string, string> words = null;

        public Dictionary<string, string> Words{
            get {
                return words;
            }
        }


        private event ListWordsChangeEventHandler handle;


        public event ListWordsChangeEventHandler ListWordsChanged { 
            add {
                handle += value;
            }
            remove {
                handle -= value;
            }
        }
        public ListWords(){
            Load(VCDir.Instance.ListWordDir);
        }

        public void Load(string path){
            try
            {
                words = new Dictionary<string, string>();

                XDocument doc = XDocument.Load(path);
                IEnumerable<XElement> listWord = doc.Root.Element("List").Elements();

                foreach (XElement word in listWord)
                {
                    string w = word.Attribute("w").Value;
                    string p = VCDir.Instance.AudioDir + word.Attribute("Path").Value;
                    if (!words.ContainsKey(w) )
                    {
                        if (File.Exists(p))
                        {
                            words.Add(w, p);
                        }
                        else {
                            Debug.WriteLine("Cant Load {0}", w);
                        }
                        
                    }
                }
                var result = from w in words
                             orderby w.Value ascending
                             select w;
                //words = result.ToDictionary<string, string>(r => r.Key, r => r.Value);
                //words.Sort((f, s) => { return f.Value.CompareTo(s.Value); });

                if (handle != null)
                {
                    handle(this, new ListWordsChangeEventArgs());
                }
            }
            catch(Exception) {

            }
             
        }
    }
}

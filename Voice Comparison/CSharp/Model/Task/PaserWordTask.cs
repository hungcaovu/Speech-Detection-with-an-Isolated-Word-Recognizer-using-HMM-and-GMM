using HumanVoiceDetector.Task;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Object
{

    public class YeuToAmTiet {
        public string AmTo{set;get;}
        public List<string> PrefixRequired{set;get;}
        public List<string> PostRequired { set; get; }
        public List<string> ContainRequired { set; get; }
    }

    public class PaserWordTask
    {
        private Dictionary<int, string> listPaths = null;
        private Dictionary<int, string> listWords = null;
        private Dictionary<int, string> listWordsUniCode = null;

        private List<AmTiet> _listAmTiet;

        private List<string> _listThanh;
        private List<string> _listPhuAmDau;
        private List<YeuToAmTiet> _listAmChinh;
        private List<YeuToAmTiet> _listAmCuoi;
        private List<YeuToAmTiet> _listAmDem;
        public List<AmTiet> ListAmTiet {
            get {
                return _listAmTiet;
            }
        }

        public PaserWordTask(){
            _listAmTiet = new List<AmTiet>();
            _listThanh = new List<string>();
            _listPhuAmDau = new List<string>();
            _listAmCuoi = new List<YeuToAmTiet>();
            _listAmChinh = new List<YeuToAmTiet>();
            _listAmDem = new List<YeuToAmTiet>();

        }
        public void PaserWordFromDir(string path, string typeFile  = "wav")
        {
            listPaths = new Dictionary<int, string>();
            listWords = new Dictionary<int, string>();

            int count = 0;
            FolderParser dirParser = new FolderParser(path);
            List<string> dirs = dirParser.Folders;
            foreach(string dir in dirs){
                ParserFiles fileParser = new ParserFiles(dir, typeFile);
                List<string> files = fileParser.Names;
                foreach (string file in files) { 
                    listWords.Add(count, Path.GetFileNameWithoutExtension(file));
                    listPaths.Add(count, file);
                    count++;
                }
            }
        }
        public void ConvertToUnicodeWords() {
            foreach (KeyValuePair<int, string> pair in listWords) { 

            }
        }
        private string ConvertToUniCode(string str){
            return "";
        }
        public bool StoreListWord(string path){
            try{
                StreamWriter str = new StreamWriter(path);

                foreach (KeyValuePair<int, string> pair in listWordsUniCode)
                {
                    string line = string.Format("<Word Path=\"{0}\\{1}.wav\" w = \"{2}\" />", Path.GetDirectoryName(listPaths[pair.Key]), listWords[pair.Key], pair.Value);
                    str.WriteLine(line);
                }
            str.Flush();
            str.Close();
            } catch (Exception){
                return false;
            }
            
            return true;
        }
        public bool LoadListWord(string path)
        {
            try
            {
                listWordsUniCode = new Dictionary<int, string>();

                StreamReader str = new StreamReader(path);

                while(!str.EndOfStream)
                {
                   // string line = string.Format("{0} {1}", pair.Key, pair.Value);
                    char [] key = {' '};
                    string line = str.ReadLine();
                    string [] tokens = line.Split(key); 
                    if(tokens.Length > 1){
                        listWordsUniCode.Add(Convert.ToInt32(tokens[0]), tokens[1]);
                    }
                }

                str.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

// Update Info for list word
        public void UpdateVanTrongAmTiet()
        {
            for (int i = 0; i < _listAmTiet.Count; i++)
            {
                AmTiet amTiet = _listAmTiet[i];
                if (string.IsNullOrEmpty(amTiet.Unicode))
                {
                    AmTietTelex(ref amTiet);
                }
                PhanTichAmTiet(ref amTiet);
            }
        }

// Parser File XML
        public void ParserListWord(XmlDocument doc)
        {

            XmlNode node = doc.SelectSingleNode("Root/List");
            if (node != null)
            {
                _listAmTiet.Clear();
                XmlNodeList listWords = node.SelectNodes("Word");
                foreach (XmlNode word in listWords)
                {
                    AmTiet amTiet = new AmTiet();
                    if (word != null && word.Attributes != null) {
                        if (word.Attributes["Path"] != null) {
                            amTiet.Path = word.Attributes["Path"].Value;
                        }

                        if (word.Attributes["w"] != null)
                        {
                            amTiet.Vietnamese = word.Attributes["w"].Value;
                        }

                        if (word.Attributes["PhuAmDau"] != null)
                        {
                            amTiet.PhuAmDau = word.Attributes["PhuAmDau"].Value;
                        }

                        if (word.Attributes["AmDem"] != null)
                        {
                            amTiet.AmDem = word.Attributes["AmDem"].Value;
                        }

                        if (word.Attributes["AmChinh"] != null)
                        {
                            amTiet.AmChinh = word.Attributes["AmChinh"].Value;
                        }

                        if (word.Attributes["AmCuoi"] != null)
                        {
                            amTiet.AmCuoi = word.Attributes["AmCuoi"].Value;
                        }

                        if (word.Attributes["Thanh"] != null)
                        {
                            amTiet.Thanh = word.Attributes["Thanh"].Value;
                        }

                        if (word.Attributes["Unicode"] != null)
                        {
                            amTiet.Unicode = word.Attributes["Unicode"].Value;
                        }

                        if (word.Attributes["Van"] != null)
                        {
                            amTiet.Van = word.Attributes["Van"].Value;
                        }

                        if (word.Attributes["Edited"] != null)
                        {
                            amTiet.Edited = Convert.ToBoolean(word.Attributes["Edited"].Value);
                        }
                        else {
                            amTiet.Edited = false;
                        }


                        if (string.IsNullOrEmpty(amTiet.Unicode))
                        {
                            AmTietTelex(ref amTiet);
                        }

                        _listAmTiet.Add(amTiet);
                    }
                }
                _listAmTiet = _listAmTiet.OrderBy(x => x.Path).ToList<AmTiet>();
            }
        }
        public XmlElement StoreListWord(XmlDocument doc)
        { 
            if(doc == null 
                || _listAmTiet == null  
                || (_listAmTiet != null && _listAmTiet.Count == 0) ) return null;

             XmlElement eList = doc.CreateElement("List");
             if (eList != null)
             {
                 foreach (AmTiet amTiet in _listAmTiet) {
                     XmlElement eWord = doc.CreateElement("Word");
                     eWord.SetAttribute("Path", amTiet.Path);
                     eWord.SetAttribute("w", amTiet.Vietnamese);
                     eWord.SetAttribute("PhuAmDau", amTiet.PhuAmDau);
                     eWord.SetAttribute("AmDem", amTiet.AmDem);
                     eWord.SetAttribute("AmChinh", amTiet.AmChinh);
                     eWord.SetAttribute("AmCuoi", amTiet.AmCuoi);
                     eWord.SetAttribute("Thanh", amTiet.Thanh);
                     eWord.SetAttribute("Van", amTiet.Van);
                     eWord.SetAttribute("Unicode", amTiet.Unicode);
                     eWord.SetAttribute("Edited", amTiet.Edited.ToString());
                     eList.AppendChild(eWord);
                 }
             }
             return eList;
        }

        static public bool UpdateAWord(string path, AmTietCarrier.AmTietRow row) { 
             XmlDocument doc = new XmlDocument();
             if (File.Exists(path))
             {
                 try
                 {
                     doc.Load(path);
                     XmlNodeList nodes = doc.SelectNodes("Root/List/Word");

                     if (nodes != null)
                     {
                         foreach (XmlNode node in nodes)
                         {
                             if (node.Attributes["Path"] != null && node.Attributes["Path"].Value == row.Path) {
                                 node.Attributes["Thanh"].Value = row.Thanh;
                                 node.Attributes["PhuAmDau"].Value = row.AmDau;
                                 node.Attributes["AmDem"].Value = row.AmDem;
                                 node.Attributes["AmChinh"].Value = row.AmChinh;
                                 node.Attributes["AmCuoi"].Value = row.AmCuoi;
                                 node.Attributes["w"].Value = row.Vietnamese;
                                 node.Attributes["Unicode"].Value = row.Unicode;
                                 if (node.Attributes["Edited"] == null)
                                 {
                                     XmlAttribute att = doc.CreateAttribute("Edited");
                                     node.Attributes.Append(att);
                                     node.Attributes["Edited"].Value = row.Edited.ToString();
                                 }
                                 else {
                                     node.Attributes["Edited"].Value = row.Edited.ToString();
                                 }

                                 doc.Save(path);
                             }
                         }
                     }
                 }
                 catch (Exception)
                 {
                 }
             }
             return false;

        }
        public List<string> ParserListToken(string text)
        {
            List<string> list = new List<string>();
            text = text.Trim(' ');
            if (text.Length > 0)
            {
                string[] parts = text.Split(',');

                foreach (string part in parts)
                {
                    string[] sames = part.Split('/');
                    foreach (string same in sames)
                    {
                        list.Add(same.ToLower());
                    }
                }
            }
            return list;
        }
        public void ParserVietNameseStructure(XmlNode nodeVietNameStructure)
        {
            //Thanh
            _listThanh.Clear();
            XmlNode subNodeThanh = nodeVietNameStructure.SelectSingleNode("Thanh");
            if (subNodeThanh != null)
            {
               string text = string.Copy(subNodeThanh.InnerText);
               text = text.Trim(' ');
               string []parts = text.Split(',');
               foreach (string part in parts) {
                   if (part.Contains('/'))
                   {
                       string[] sames = part.Split('/');
                       foreach (string same in sames)
                       {
                           _listThanh.Add(same.ToLower());
                       }
                   }
                   else {
                       _listThanh.Add(part.ToLower());
                   }
               }
            }

            //PhuAmDau
            _listPhuAmDau.Clear();
            XmlNode subNodePhuAmDau = nodeVietNameStructure.SelectSingleNode("PhuAmDau");
            if (subNodePhuAmDau != null)
            {
                string text = string.Copy(subNodePhuAmDau.InnerText);
                char[] charsToTrim = { ' ', '\t' };
                text.Trim(charsToTrim);
                string[] parts = text.Split(',');
                foreach (string part in parts)
                {
                    if (part.Contains('/'))
                    {
                        string[] sames = part.Split('/');
                        foreach (string same in sames)
                        {
                            _listPhuAmDau.Add(same.ToLower());
                        }
                    }
                    else
                    {
                        _listPhuAmDau.Add(part.ToLower());
                    }
                }
            }
            //Am Chinh
            _listAmChinh.Clear();
            XmlNodeList subNodeAmChinh = nodeVietNameStructure.SelectNodes("AmChinh");
            if (subNodeAmChinh != null) {
                foreach (XmlNode node in subNodeAmChinh)
                {
                    YeuToAmTiet yt = new YeuToAmTiet();
                    yt.AmTo = node.InnerText.ToLower();
                    if (node.Attributes != null)
                    {
                        if (node.Attributes["TRUOC"] != null)
                        {
                            yt.PrefixRequired = ParserListToken(node.Attributes["TRUOC"].Value.ToLower());
                        }
                        if (node.Attributes["SAU"] != null)
                        {
                            yt.PostRequired = ParserListToken(node.Attributes["SAU"].Value.ToLower());
                        }

                        if (node.Attributes["TRONG"] != null)
                        {
                            yt.ContainRequired = ParserListToken(node.Attributes["TRONG"].Value.ToLower());
                        }
                    }

                    _listAmChinh.Add(yt);
                }
                //string text = string.Copy(subNodeAmChinh.InnerText);
                //char[] charsToTrim = { ' ', '\t' };
                //text.Trim(charsToTrim);
                //string[] parts = text.Split(',');
                //foreach (string part in parts)
                //{
                //    if (part.Contains('/'))
                //    {
                //        string[] sames = part.Split('/');
                //        foreach (string same in sames)
                //        {
                //            _listAmChinh.Add(same.ToLower());
                //        }
                //    }
                //    else
                //    {
                //        _listAmChinh.Add(part.ToLower());
                //    }
                //}
            }
            //PhuAmCuoi
            _listAmCuoi.Clear();
            XmlNodeList subNodeAmCuoi = nodeVietNameStructure.SelectNodes("AmCuoi");
            if (subNodeAmCuoi != null)
            {
                foreach (XmlNode node in subNodeAmCuoi) {
                    YeuToAmTiet yt = new YeuToAmTiet();
                    yt.AmTo = node.InnerText.ToLower();
                    if (node.Attributes != null) {
                        if (node.Attributes["TRUOC"] != null) {
                            yt.PrefixRequired = ParserListToken(node.Attributes["TRUOC"].Value.ToLower());
                        }
                        if (node.Attributes["SAU"] != null)
                        {
                            yt.PostRequired = ParserListToken(node.Attributes["SAU"].Value.ToLower());
                        }

                        if (node.Attributes["TRONG"] != null)
                        {
                            yt.ContainRequired = ParserListToken(node.Attributes["TRONG"].Value.ToLower());
                        }
                    }

                    _listAmCuoi.Add(yt);
                }
            }

            //AmDem
            _listAmDem.Clear();
            XmlNodeList subNodeAmDem = nodeVietNameStructure.SelectNodes("AmDem");
            if (subNodeAmDem != null)
            {
                foreach (XmlNode node in subNodeAmDem)
                {
                    YeuToAmTiet yt = new YeuToAmTiet();
                    yt.AmTo = node.InnerText.ToLower();
                    if (node.Attributes != null)
                    {
                        if (node.Attributes["TRUOC"] != null)
                        {
                            yt.PrefixRequired = ParserListToken(node.Attributes["TRUOC"].Value.ToLower());
                        }
                        if (node.Attributes["SAU"] != null)
                        {
                            yt.PostRequired = ParserListToken(node.Attributes["SAU"].Value.ToLower());
                        }

                        if (node.Attributes["TRONG"] != null)
                        {
                            yt.ContainRequired = ParserListToken(node.Attributes["TRONG"].Value.ToLower());
                        }
                    }

                    _listAmDem.Add(yt);
                }
            }
        }
        public bool LoadData(string path){
            XmlDocument doc = new XmlDocument();
            if(File.Exists(path)){
                try{
                    doc.Load(path);
                    ParserListWord(doc);
                    XmlNode node = doc.SelectSingleNode("Root/StructureVietnamese");
                    ParserVietNameseStructure(node);
                } catch(Exception){
                    return false;
                }
                return true;
            }
            return false;
        }
        public void UpdateListWord(string path) {
            _listAmTiet = _listAmTiet.OrderBy(x => x.Path).ToList<AmTiet>();
            XmlDocument doc = new XmlDocument();
            if (File.Exists(path))
            {
                try
                {
                    doc.Load(path);
                    //ParserListWord(doc);
                    XmlNode node = doc.SelectSingleNode("Root");
                    XmlNode oldNode = node.SelectSingleNode("List");
                    XmlNode listNode = StoreListWord(doc);
                    node.ReplaceChild(listNode, oldNode);
                    doc.Save(path);
                }
                catch (Exception)
                {
                }

            }
        }
        // Phan Tach Am Tiet
        public AmTiet AmTietTelex(ref AmTiet amTiet){
            string name = Path.GetFileName(amTiet.Path);
            string[] parts = name.Split('.');
            if (parts.Count<string>() > 1) {
                amTiet.Unicode = parts[0];
            }
            return amTiet;
        }

        private bool StartWith(string a, string b) {
            if (a.Length < b.Length) return false;
            int len = b.Length;
            int i = 0;
            while (i < len && a[i] == b[i]) {
                i++;
            }
            if (i == len)
                return true;
            return false;
        }


        public AmTiet PhanTichAmTiet(ref AmTiet amTiet)
        {
            if (amTiet.Edited == true) {
                return amTiet;
            }

            string text = string.Copy(amTiet.Unicode);
            amTiet.Thanh = PhanTichThanh(ref text);
            amTiet.PhuAmDau = PhanTichPhuAmDau(ref text);
            string van = string.Copy(text);
            amTiet.AmCuoi = PhanTichAmCuoi(ref text);
            amTiet.AmChinh = PhanTichAmChinh(amTiet, ref text);

            amTiet.Van = van;
            PhanTichAmDem(amTiet, van);


			
            return amTiet;
        }
        public string PhanTichThanh(ref string amTiet)
        {
            foreach (string thanh in _listThanh)
            {
                if (amTiet.EndsWith(thanh))
                {
                    amTiet = amTiet.Substring(0, amTiet.Length - thanh.Length);
                    return thanh;
                }
            }
            return "-";
        }
        //Ok Roi`
        public string PhanTichPhuAmDau(ref string amTiet)
        {
            foreach (string phuAmDau in _listPhuAmDau)
            {
                if (StartWith(amTiet, phuAmDau))
                {
                    amTiet = amTiet.Substring(phuAmDau.Length);
                    return phuAmDau;
                }
            }
            return "-";
        }
        public string PhanTichAmChinh(AmTiet amTiet, ref string van)
        {
            foreach (YeuToAmTiet amChinh in _listAmChinh)
            {
                string tmp = string.Copy(van);
                if (tmp.EndsWith(amChinh.AmTo))
                {
                    
                    if (amChinh.ContainRequired != null && amChinh.ContainRequired.Count > 0)
                    {
                        foreach (string contain in amChinh.ContainRequired)
                        {
                            if (tmp.EndsWith(contain))
                            {
                                if (amChinh.PrefixRequired != null && amChinh.PrefixRequired.Count > 0)
                                {
                                    foreach (string pre in amChinh.PrefixRequired)
                                    {
                                        if (amTiet.PhuAmDau == pre)
                                        {
                                            tmp = tmp.Substring(0, tmp.Length - amChinh.AmTo.Length);
                                            van = tmp;
                                            amTiet.AmChinh = amChinh.AmTo;
                                            return amChinh.AmTo;
                                        }
                                    }
                                }
                                else
                                {
                                    tmp = tmp.Substring(0, tmp.Length - amChinh.AmTo.Length);
                                    van = tmp;
                                    amTiet.AmChinh = amChinh.AmTo;
                                    return amChinh.AmTo;
                                }
                                
                            }
                        }
                    }
                    if ((amChinh.PrefixRequired == null || (amChinh.PrefixRequired != null && amChinh.PrefixRequired.Count == 0))
                        && (amChinh.ContainRequired == null || (amChinh.ContainRequired != null && amChinh.ContainRequired.Count == 0))
                        && (amChinh.PostRequired == null || (amChinh.PostRequired != null && amChinh.PostRequired.Count == 0))
                        )
                    {
                        amTiet.AmChinh = amChinh.AmTo;
                        van = van.Substring(0, van.Length - amTiet.AmChinh.Length);
                        return amChinh.AmTo;
                    }
                }
            }
          
           Debug.WriteLine("Co Van De");
			return "-";
		}
        public string PhanTichAmCuoi(ref string amTiet) {
            foreach (YeuToAmTiet amCuoi in _listAmCuoi)
            {
                string tmp= string.Copy(amTiet);
                if (tmp.EndsWith(amCuoi.AmTo))
                {
                    if (amCuoi.PrefixRequired != null && amCuoi.PrefixRequired.Count > 0)
                    {
                        tmp = tmp.Substring(0, tmp.Length - amCuoi.AmTo.Length);
                        foreach(string pre  in amCuoi.PrefixRequired){
                            if(tmp.EndsWith(pre)){
                                amTiet = tmp;
                                return amCuoi.AmTo;
                            }
                        }
                    }
                    if (amCuoi.ContainRequired != null && amCuoi.ContainRequired.Count > 0)
                    {
                        foreach(string contain in amCuoi.ContainRequired){
                            if (tmp.EndsWith(contain)) {
                                tmp = tmp.Substring(0, tmp.Length - amCuoi.AmTo.Length);
                                amTiet = tmp;
                                return amCuoi.AmTo;
                            }
                        }                        
                    }
                    if ((amCuoi.PrefixRequired == null || (amCuoi.PrefixRequired != null && amCuoi.PrefixRequired.Count == 0))
                        && (amCuoi.ContainRequired == null || (amCuoi.ContainRequired != null && amCuoi.ContainRequired.Count == 0))
                        && (amCuoi.PostRequired == null || (amCuoi.PostRequired != null && amCuoi.PostRequired.Count == 0))
                        ) {
                            tmp = tmp.Substring(0, tmp.Length - amCuoi.AmTo.Length);
                            amTiet = tmp;
                            return amCuoi.AmTo;
                    }
                    
                }
            }
            return "";
        }

        public void PhanTichAmDem(AmTiet amTiet, string van) {
			string fullAmTiet = string.Copy(amTiet.Unicode);

			string fullWithoutPhuAmCuoi = van;
            foreach (YeuToAmTiet amDem in _listAmDem)
            {
				if (amDem.ContainRequired != null && amDem.ContainRequired.Count > 0)
				{
					foreach (string contain in amDem.ContainRequired){
						if (StartWith(fullWithoutPhuAmCuoi, contain)){
							if (amDem.PostRequired != null && amDem.PostRequired.Count > 0) {
								foreach (string post in amDem.PostRequired) {
									if (StartWith(fullWithoutPhuAmCuoi, post)) {
										amTiet.AmDem = amDem.AmTo;
									}
								}
							} else {
								amTiet.AmDem = amDem.AmTo;
							}
						}

						if (fullAmTiet.EndsWith(contain)) {
							if (amDem.PostRequired != null && amDem.PostRequired.Count > 0) {
								foreach (string post in amDem.PostRequired) {
									if (StartWith(fullAmTiet, post)) {
										amTiet.AmDem = amDem.AmTo;
									}
								}
							} else {
								amTiet.AmDem = amDem.AmTo;
							}
						}
					}
				}

                if (amDem.PrefixRequired != null && amDem.PrefixRequired.Count > 0)
                {
                    foreach (string pre in amDem.PrefixRequired)
                    {
                        if (amTiet.PhuAmDau == pre && StartWith(fullWithoutPhuAmCuoi, amDem.AmTo))
                        {
                            amTiet.AmDem = amDem.AmTo;
                        }
                    }
                }
            }
        }
    }
}

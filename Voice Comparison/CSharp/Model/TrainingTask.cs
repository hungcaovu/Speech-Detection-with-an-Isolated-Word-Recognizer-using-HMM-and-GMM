using Object;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ExtractionWrapper;
namespace Model
{
    public class TrainingTask
    {
        List<string> _words;
        List<string> _hmms;
        List<HMMWrapper> _models;
        
        TrainFilesCarrier.TrainFileDataTable tbEntry;
        public TrainFilesCarrier.TrainFileDataTable Entries {
            get {
                return tbEntry;
            }

            set {
                tbEntry = value;
            }
        }

        public TrainingTask()
        {
            _words = new List<string>();
            _hmms = new List<string>();
            _models = new List<HMMWrapper>();
        }
        public bool Load(string path) {
            tbEntry = new TrainFilesCarrier.TrainFileDataTable();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode node = doc.SelectSingleNode("Train/TrainFiles");
                if (node != null) {
                    XmlNodeList listWords = node.SelectNodes("File");
                    foreach (XmlNode word in listWords) {
                        TrainFilesCarrier.TrainFileRow row = tbEntry.NewTrainFileRow();
                        row.Word = word.Attributes["Word"].Value;
                        row.Path = word.Attributes["Path"].Value;
                        row.Start = Convert.ToInt32(word.Attributes["Start"].Value);
                        row.End = Convert.ToInt32(word.Attributes["End"].Value);
                        // row.ID = word.Attribute("ID").Value;
                        string p = VCDir.Instance.TrainDirAudio + row.Path;
                        //if (!words.ContainsKey(w))
                        // {
                        if (File.Exists(p))
                        {
                            tbEntry.AddTrainFileRow(row);
                        }
                        else
                        {
                            Debug.WriteLine("Cant Load {0} - Path {1}", row.Word, row.Path);
                        }
                    }
                }

                node = doc.SelectSingleNode("Train/HMMModels");
                if (node != null) { 
                     XmlNodeList listModels = node.SelectNodes("Model");
                     foreach (XmlNode models in listModels)
                     {
                         string word = models.Attributes["Word"].Value;
                         string file = models.Attributes["Path"].Value;
                         HMMWrapper hmm = new HMMWrapper();

                         if (word != null && file != null && hmm.Load(VCDir.Instance.TrainDirHMM + file))
                         {
                             Debug.WriteLine("Load Model {0}: {1}", word, VCDir.Instance.TrainDirHMM + file);
                             _words.Add(word);
                             _hmms.Add(file);
                             _models.Add(hmm);
                         }
                     }
                }
            }
            catch (Exception)
            {

            }

            return true;
        }
        public bool Save(string path) {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    XmlElement eTran = doc.CreateElement("Train");
                    XmlElement eList = doc.CreateElement("TrainFiles");
                    foreach (TrainFilesCarrier.TrainFileRow row in tbEntry)
                    {
                        XmlElement eWord = doc.CreateElement("File");
                        eWord.SetAttribute("Word", row.Word);
                        eWord.SetAttribute("Path", row.Path);
                        eWord.SetAttribute("Start", row.Start.ToString());
                        eWord.SetAttribute("End", row.End.ToString());
                        eWord.SetAttribute("ID", row.ID.ToString());

                        eList.AppendChild(eWord);

                    }
                    eTran.AppendChild(eList);
                    XmlElement eModels = doc.CreateElement("HMMModels");
                    for (int i = 0; i < _words.Count; i++)
                    {
                        if (File.Exists(VCDir.Instance.TrainDirHMM + _hmms[i]))
                        {
                            XmlElement eModel = doc.CreateElement("Model");
                            eModel.SetAttribute("Word", _words[i]);
                            eModel.SetAttribute("Path", _hmms[i]);

                            eModels.AppendChild(eModel);
                        }

                    }
                    eTran.AppendChild(eModels);
                    doc.AppendChild(eTran);
                    doc.Save(path);
                }
                catch (Exception)
                {
                }
            return true;
        }
        public TrainFilesCarrier.TrainFileRow IsRowExisted(TrainFilesCarrier.TrainFileRow row)
        {
            foreach (TrainFilesCarrier.TrainFileRow ent in tbEntry)
            {
                if (String.Equals(ent.Path, row.Path, StringComparison.OrdinalIgnoreCase)) {
                    return ent;
                }
            }
            return null;
        }
        public void Remove(TrainFilesCarrier.TrainFileRow row) {
            tbEntry.RemoveTrainFileRow(row);
        }
        public bool AddorUpdate(TrainFilesCarrier.TrainFileRow row) {
            TrainFilesCarrier.TrainFileRow existedRow = IsRowExisted(row);
            if ( existedRow == null)
            {
                tbEntry.AddTrainFileRow(row);
                return true;
            }
            else {
                if (existedRow.Start != row.Start) {
                    existedRow.Start = row.Start;
                }
                if (existedRow.End != row.End)
                {
                    existedRow.End = row.End;
                }
                if (existedRow.Word != row.Word) {
                    existedRow.Word = row.Word;
                }
            }

            return false;
        }
        public bool Train() {
            _words = new List<string>();
            _hmms = new List<string>();
            _models = new List<HMMWrapper>();

            foreach (TrainFilesCarrier.TrainFileRow ent in tbEntry)
            {
                if (!_words.Contains(ent.Word))
                {
                    _words.Add(ent.Word);
                    LogUtil.Info("Word : {0}\n", ent.Word);
                }
            }

            foreach (string word in _words)
            {
                List<string> files = new List<string>();
                LogUtil.Info("List File  for Word : {0}\n", word);
                VCDir.CreateDirectory(VCDir.Instance.TrainDirMFCC + word);
                VCDir.CreateDirectory(VCDir.Instance.TrainDirHMM);
                foreach (TrainFilesCarrier.TrainFileRow ent in tbEntry)
                {
                    if (word.Equals(ent.Word))
                    {
                        string audio = VCDir.Instance.TrainDirAudio + ent.Path;
                        string mfcc = VCDir.Instance.TrainDirMFCC + ent.Path;
                        WavFileWrapper Wav = new WavFileWrapper(audio);

                        if (Wav.Load())
                        {
                            Wav.NormalizeWave(1.0f);
                            if (VCContext.Instance.MFCCOptions.ShiftSampleToZero) {
                                Wav.ShifToZero();
                            }

                            Wav.SelectedWave((uint)ent.Start, (uint)ent.End);
                            MFCCWrapper Mfcc = new MFCCWrapper(Wav, VCContext.Instance.MFCCOptions.TimeFrame, VCContext.Instance.MFCCOptions.TimeShift, VCContext.Instance.MFCCOptions.CepFilter, VCContext.Instance.MFCCOptions.LowFreq, VCContext.Instance.MFCCOptions.HighFreq, VCContext.Instance.MFCCOptions.NumCeps, 2);
                            Mfcc.UserStandardization = VCContext.Instance.MFCCOptions.UseStandardization;

                            bool res = Mfcc.Process();
                            res &= Mfcc.SaveMFCC(mfcc + ".Mfcc" + ".xml");
                            res &= Mfcc.SaveDeltaMFCC(mfcc + ".Delta" + ".xml");
                            res &= Mfcc.SaveDoubleMFCC(mfcc + ".Double" + ".xml");
                            if (res)
                            {
                                switch (VCContext.Instance.MFCCOptions.TrainCofficientType) { 
                                    case 0:
                                        files.Add(mfcc + ".Mfcc" + ".xml");
                                        break;
                                    case 1:
                                        files.Add(mfcc + ".Delta" + ".xml");
                                        break;
                                    case 2:
                                        files.Add(mfcc + ".Double" + ".xml");
                                        break;
                                }
                                
                            }
                            LogUtil.Info("File : W - {0} Path - {1} MFCC process - {2}\n", ent.Word, VCDir.Instance.TrainDirMFCC + ent.Path + ".xml", (res) ? "Completed" : "Failed");
                        }
                    }
                }

                HMMWrapper hmm = new HMMWrapper(VCContext.Instance.MFCCOptions.TrainHMMState, VCContext.Instance.MFCCOptions.TrainGMMComponent, VCContext.Instance.MFCCOptions.TrainGMMCovVar);
                bool ok = hmm.Trainning(files);
                LogUtil.Info("Train word: {0}  - {1}\n", word, (ok)? "Completed": "Failed");
                if (ok)
                {
                    hmm.Save(VCDir.Instance.TrainDirHMM + word+".xml");
                    _models.Add(hmm);
                    _hmms.Add(word + ".xml");
                    LogUtil.Info("Save {0} Model to {1}\n", word, VCDir.Instance.TrainDirHMM + word + ".xml");
                }
            }

            Save(VCDir.Instance.TrainXmlFile);
            return true;
        }
        public string Reg(string path, int start, int end) {
            if (_words == null || _words.Count == 0) return " Models Failed";
             WavFileWrapper Wav = new WavFileWrapper(path);
             LogUtil.Info("*******************Reg*******************\n");
             LogUtil.Info("Load Wave: {0}\n",  path);
             if (Wav.Load())
             {
                 Wav.NormalizeWave(1.0f);
                 LogUtil.Info("Load Wave: {0}   -- OK\n", path);
                 if (VCContext.Instance.MFCCOptions.ShiftSampleToZero)
                 {
                     LogUtil.Info("Shift Sample To Zero: --   -- OK\n");
                     Wav.ShifToZero();
                 }
                 
                 Wav.SelectedWave((uint)start, (uint)end);

                 MFCCWrapper Mfcc = new MFCCWrapper(Wav, VCContext.Instance.MFCCOptions.TimeFrame, VCContext.Instance.MFCCOptions.TimeShift, VCContext.Instance.MFCCOptions.CepFilter, VCContext.Instance.MFCCOptions.LowFreq, VCContext.Instance.MFCCOptions.HighFreq, VCContext.Instance.MFCCOptions.NumCeps, 2);
                 bool res = Mfcc.Process();
                 List<List<double>> data = null;
                 if (res)
                 {
                     switch (VCContext.Instance.MFCCOptions.TrainCofficientType)
                     {
                         case 0:
                             data = Mfcc.Mfcc;
                             break;
                         case 1:
                             data = Mfcc.DetalMfcc;
                             break;
                         case 2:
                             data = Mfcc.DoubleDetalMfcc;
                             break;
                         default:
                             data = Mfcc.Mfcc;
                             break;
                     }

                     LogUtil.Info("Load Wave: {0}   Process MFCC -- OK\n", path);
                     int reg = 0;
                     double max = _models[reg].LogProbability(data);

                     LogUtil.Info("HModel = {0}  Log Value = {1}\n", _words[reg], max);
                     for (int i = 1; i < _models.Count; i++)
                     {
                         double cur = _models[i].LogProbability(data);
                         LogUtil.Info("HModel = {0}  Log Value = {1}\n", _words[i], cur);
                         if (max < cur)
                         {
                             max = cur;
                             reg = i;
                         }
                     }
                     LogUtil.Info("HModel = {0}  Log Value = {1}   Reg = {2}\n", _words[reg], max, _words[reg]);
                     return _words[reg];
                 }
                 else {
                     LogUtil.Info("Load Wave: {0}   -- FAILED\n", path);
                 }

             }
             return "NONE";
        }

		public string Reg(List<List<double>> mfcc) {
            LogUtil.Info("********************REG**********************\n");

            if (mfcc == null | mfcc.Count == 0)
            {
                LogUtil.Info("********************INPUT INVALID**********************\n");
                return "Error MFCC";
            }
            LogUtil.Info("     Feature = {0}   Coff = {1}", mfcc.Count, mfcc[0].Count);
			int reg = 0;
            //double max = _models[reg].LogProbability(@"G:\Project\Binary\Voice Comparasion\Debug\Data.xml");
            double max = _models[reg].LogProbability(mfcc);
			LogUtil.Info("HModel = {0}  Log Value = {1}\n", _words[reg], max);
			for (int i = 1; i < _models.Count; i++) {
                //double cur = _models[i].LogProbability(@"G:\Project\Binary\Voice Comparasion\Debug\Data.xml");
                double cur = _models[i].LogProbability(mfcc);
				LogUtil.Info("HModel = {0}  Log Value = {1}\n", _words[i], cur);
				if (max < cur) {
					max = cur;
					reg = i;
				}
			}
			LogUtil.Info("HModel = {0}  Log Value = {1}   Reg = {2}\n", _words[reg], max, _words[reg]);
			return _words[reg];
		}
        public void Test() {
            //double data = _models[3].LogProbability(@"G:\Project\Binary\Voice Comparasion\Debug\Data\Train\MFCC\Bon\Bon_1.wav.Mfcc.xml");
            double data = _models[3].LogProbability(@"G:\Project\Binary\Voice Comparasion\Debug\Data.xml");
        }
    }
}

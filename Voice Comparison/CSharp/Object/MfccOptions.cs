using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Object
{
    public class MfccOptions
    {
        public MfccOptions()
        {
            LowFreq = 300f;
            HighFreq = 4000.0f;
            TimeFrame = 0.025f;
            TimeShift = 0.01f;
            NumCeps = 12;
            CepFilter = 22;
            UseStandardization = false;


            PitchTimeFrame = 0.03f;
            PitchTimeShift = 0.005f;
            PitchLowFreq = 20.0f;
            PitchHighFreq = 500.0f;
            YinThreshhold = 0.1f;
            PitchType = 0;
            NormalizeAudio = true;
            RemoveNoiseYourAudio = true;
            EnableLog = false;
            SeparateLog = true;
            LogLevel = 0x00000001;
            
            PitchThreshold = 80.0f;
            EnergyThreshold = 0.3f;
            UseMedian =  true;
            MedianWindow = 5;
            DropUnPitch = true;

            TrainCofficientType = 0;
            TrainHMMState = 3;
            TrainGMMComponent = 2;
            TrainGMMCovVar = 1;
        }
        // Tab Mfcc option
        public float LowFreq { set; get; }
        public float HighFreq { set; get; }
        public float TimeFrame { set; get; }
        public float TimeShift { set; get; }
        public uint NumCeps { set; get; }
        public uint CepFilter { set; get; }

        public bool UseStandardization { set; get; }

        // Tab Pitch
        public float PitchLowFreq { set; get; }
        public float PitchHighFreq { set; get; }
        public float PitchTimeFrame { set; get; }
        public float PitchTimeShift { set; get; }
        public float YinThreshhold { set; get; }
        public int PitchType { set; get; }

        //Smoothing Pitch
        public bool DropUnPitch { set; get; }
        public bool UseMedian { set; get; }
        public int MedianWindow { set; get; }
        //Table Log
        public int LogLevel { set; get; }
        public bool EnableLog { set; get; }
        public bool SeparateLog { set; get; }

        //Tabe Noise
        public bool RemoveNoiseYourAudio { set; get; }
        public bool NormalizeAudio { set; get; }
        public bool ShiftSampleToZero { set; get; }
        //Tab VAD
        public float PitchThreshold { set; get; }
        public float EnergyThreshold { set; get; }
        //Tab Training
        public uint TrainCofficientType { set; get; }
        public uint TrainHMMState { set; get; }
        public uint TrainGMMComponent { set; get; }
        public uint TrainGMMCovVar { set; get; }

        public void LoadFromXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            try { 
                if (File.Exists(path))
            {
                doc.Load(path);
                XmlNode node = doc.SelectSingleNode("Root/Mfcc_Setting");
                if (node != null)
                {
                    
                    XmlNode subNode = node.SelectSingleNode("LowFreq");
                    if (subNode != null)
                    {
                        LowFreq = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("HighFreq");
                    if (subNode != null)
                    {
                        HighFreq = Convert.ToSingle(subNode.InnerText);
                    }


                    subNode = node.SelectSingleNode("TimeFrame");
                    if (subNode != null)
                    {
                        TimeFrame = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("TimeShift");
                    if (subNode != null)
                    {
                        TimeShift = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("NumCeps");
                    if (subNode != null)
                    {
                        NumCeps = Convert.ToUInt32(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("CepFilter");
                    if (subNode != null)
                    {
                        CepFilter = Convert.ToUInt32(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("UseStandardization");
                    if (subNode != null)
                    {
                        UseStandardization = Convert.ToBoolean(subNode.InnerText);
                    }
                }

                node = doc.SelectSingleNode("Root/Pitch_Setting");
                if (node != null)
                {
                    XmlNode subNode = node.SelectSingleNode("PitchLowFreq");
                    if (subNode != null)
                    {
                        PitchLowFreq = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("PitchHighFreq");
                    if (subNode != null)
                    {
                        PitchHighFreq = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("PitchTimeFrame");
                    if (subNode != null)
                    {
                        PitchTimeFrame = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("PitchTimeShift");
                    if (subNode != null)
                    {
                        PitchTimeShift = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("YinThreshhold");
                    if (subNode != null)
                    {
                        YinThreshhold = Convert.ToSingle(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("PitchType");
                    if (subNode != null)
                    {
                        PitchType = Convert.ToInt32(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("UseMedian");
                    if (subNode != null)
                    {
                        UseMedian = Convert.ToBoolean(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("MedianWindow");
                    if (subNode != null)
                    {
                        MedianWindow = Convert.ToInt32(subNode.InnerText);
                    }
                }

                node = doc.SelectSingleNode("Root/Logs_Setting");
                if (node != null)
                {
                    XmlNode subNode = node.SelectSingleNode("EnableLog");
                    if (subNode != null)
                    {
                        EnableLog = Convert.ToBoolean(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("SeparateLog");
                    if (subNode != null)
                    {
                        SeparateLog = Convert.ToBoolean(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("LogLevel");
                    if (subNode != null)
                    {
                        LogLevel = Convert.ToInt32(subNode.InnerText);
                    }
                }

                node = doc.SelectSingleNode("Root/Wave_Setting");
                if (node != null)
                {
                    XmlNode subNode = node.SelectSingleNode("RemoveNoiseYourAudio");
                    if (subNode != null)
                    {
                        RemoveNoiseYourAudio = Convert.ToBoolean(subNode.InnerText);
                    }

                    subNode = node.SelectSingleNode("NormalizeAudio");
                    if (subNode != null)
                    {
                        NormalizeAudio = Convert.ToBoolean(subNode.InnerText);
                    }


                    subNode = node.SelectSingleNode("ShiftSampleToZero");
                    if (subNode != null)
                    {
                        ShiftSampleToZero = Convert.ToBoolean(subNode.InnerText);
                    }
                }
                
                 node = doc.SelectSingleNode("Root/VAD_Setting");
                 if (node != null) {
                     XmlNode subNode = node.SelectSingleNode("PitchThreshold");
                     if (subNode != null)
                     {
                         PitchThreshold = Convert.ToSingle(subNode.InnerText);
                     }

                     subNode = node.SelectSingleNode("EnergyThreshold");
                     if (subNode != null)
                     {
                         EnergyThreshold = Convert.ToSingle(subNode.InnerText);
                     }
                 }

                 node = doc.SelectSingleNode("Root/Train_Setting");
                 if (node != null)
                 {
                     XmlNode subNode = node.SelectSingleNode("TrainCofficientType");
                     if (subNode != null)
                     {
                         TrainCofficientType = (uint)Convert.ToInt32(subNode.InnerText);
                     }

                     subNode = node.SelectSingleNode("TrainHMMState");
                     if (subNode != null)
                     {
                         TrainHMMState = (uint)Convert.ToInt32(subNode.InnerText);
                     }

                     subNode = node.SelectSingleNode("TrainGMMComponent");
                     if (subNode != null)
                     {
                         TrainGMMComponent = (uint)Convert.ToInt32(subNode.InnerText);
                     }

                     subNode = node.SelectSingleNode("TrainGMMCovVar");
                     if (subNode != null)
                     {
                         TrainGMMCovVar = (uint)Convert.ToInt32(subNode.InnerText);
                     }
                 }
            }
            } catch(Exception){
            }
            
        }
        public void StoreToXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("Root");
            doc.AppendChild(root);

            XmlElement mfccSetting_e = doc.CreateElement("Mfcc_Setting");
            if (mfccSetting_e != null)
            {

                XmlElement e_LowFreq = doc.CreateElement("LowFreq");
                e_LowFreq.InnerText = LowFreq.ToString();
                mfccSetting_e.AppendChild(e_LowFreq);



                XmlElement e_HighFreq = doc.CreateElement("HighFreq");
                e_HighFreq.InnerText = HighFreq.ToString();
                mfccSetting_e.AppendChild(e_HighFreq);


                XmlElement e_TimeFrame = doc.CreateElement("TimeFrame");
                e_TimeFrame.InnerText = TimeFrame.ToString();
                mfccSetting_e.AppendChild(e_TimeFrame);


                XmlElement e_TimeShift = doc.CreateElement("TimeShift");
                e_TimeShift.InnerText = TimeShift.ToString();
                mfccSetting_e.AppendChild(e_TimeShift);


                XmlElement e_NumCeps = doc.CreateElement("NumCeps");
                e_NumCeps.InnerText = NumCeps.ToString();
                mfccSetting_e.AppendChild(e_NumCeps);

                XmlElement e_CepFilter = doc.CreateElement("CepFilter");
                e_CepFilter.InnerText = CepFilter.ToString();
                mfccSetting_e.AppendChild(e_CepFilter);

                XmlElement e_UseStandardization = doc.CreateElement("UseStandardization");
                e_UseStandardization.InnerText = UseStandardization.ToString();
                mfccSetting_e.AppendChild(e_UseStandardization);

                root.AppendChild(mfccSetting_e);
            }
            // Tab Pitch
            XmlElement pitchSetting_e = doc.CreateElement(string.Empty, "Pitch_Setting", string.Empty);
            if (pitchSetting_e != null)
            {
                XmlElement e_PitchLowFreq = doc.CreateElement("PitchLowFreq");
                e_PitchLowFreq.InnerText = PitchLowFreq.ToString();
                pitchSetting_e.AppendChild(e_PitchLowFreq);

                XmlElement e_PitchHighFreq = doc.CreateElement("PitchHighFreq");
                e_PitchHighFreq.InnerText = PitchHighFreq.ToString();
                pitchSetting_e.AppendChild(e_PitchHighFreq);

                XmlElement e_PitchTimeFrame = doc.CreateElement("PitchTimeFrame");
                e_PitchTimeFrame.InnerText = PitchTimeFrame.ToString();
                pitchSetting_e.AppendChild(e_PitchTimeFrame);

                XmlElement e_PitchTimeShift = doc.CreateElement("PitchTimeShift");
                e_PitchTimeShift.InnerText = PitchTimeShift.ToString();
                pitchSetting_e.AppendChild(e_PitchTimeShift);

                XmlElement e_YinThreshhold = doc.CreateElement("YinThreshhold");
                e_YinThreshhold.InnerText = YinThreshhold.ToString();
                pitchSetting_e.AppendChild(e_YinThreshhold);

                XmlElement e_PitchType = doc.CreateElement("PitchType");
                e_PitchType.InnerText = PitchType.ToString();
                pitchSetting_e.AppendChild(e_PitchType);

                XmlElement e_UseMedian = doc.CreateElement("UseMedian");
                e_UseMedian.InnerText = UseMedian.ToString();
                pitchSetting_e.AppendChild(e_UseMedian);

                XmlElement e_MedianWindow = doc.CreateElement("MedianWindow");
                e_MedianWindow.InnerText = MedianWindow.ToString();
                pitchSetting_e.AppendChild(e_MedianWindow);

                root.AppendChild(pitchSetting_e);
            }
            // Table Log
            XmlElement logSetting_e = doc.CreateElement(string.Empty, "Logs_Setting", string.Empty);
            if (logSetting_e != null)
            {
                XmlElement e_EnableLog = doc.CreateElement("EnableLog");
                e_EnableLog.InnerText = EnableLog.ToString();
                logSetting_e.AppendChild(e_EnableLog);

                XmlElement e_SeparateLog = doc.CreateElement("SeparateLog");
                e_SeparateLog.InnerText = SeparateLog.ToString();
                logSetting_e.AppendChild(e_SeparateLog);

                XmlElement e_LevelLog = doc.CreateElement("LogLevel");
                e_LevelLog.InnerText = LogLevel.ToString();
                logSetting_e.AppendChild(e_LevelLog);
                
                root.AppendChild(logSetting_e);
            }
            // Tab Noise
            XmlElement waveSetting_e = doc.CreateElement(string.Empty, "Wave_Setting", string.Empty);
            if (waveSetting_e != null)
            {
                XmlElement e_NormalizeAudio = doc.CreateElement("NormalizeAudio");
                e_NormalizeAudio.InnerText = NormalizeAudio.ToString();
                waveSetting_e.AppendChild(e_NormalizeAudio);

                XmlElement e_RemoveNoiseYourAudio = doc.CreateElement("RemoveNoiseYourAudio");
                e_RemoveNoiseYourAudio.InnerText = RemoveNoiseYourAudio.ToString();
                waveSetting_e.AppendChild(e_RemoveNoiseYourAudio);

                XmlElement e_ShiftSampleToZero = doc.CreateElement("ShiftSampleToZero");
                e_ShiftSampleToZero.InnerText = ShiftSampleToZero.ToString();
                waveSetting_e.AppendChild(e_ShiftSampleToZero);
                
                root.AppendChild(waveSetting_e);
            }
            //Tab VAD
            XmlElement vadSetting_e = doc.CreateElement("VAD_Setting");
            if (vadSetting_e != null)
            {
                XmlElement e_PitchThreshold = doc.CreateElement("PitchThreshold");
                e_PitchThreshold.InnerText = PitchThreshold.ToString();
                vadSetting_e.AppendChild(e_PitchThreshold);

                XmlElement e_EnergyThreshold = doc.CreateElement("EnergyThreshold");
                e_EnergyThreshold.InnerText = EnergyThreshold.ToString();
                vadSetting_e.AppendChild(e_EnergyThreshold);

                root.AppendChild(vadSetting_e);
            }


    //Tab Train
        XmlElement train_e = doc.CreateElement("Train_Setting");
            if (train_e != null)
            {
                XmlElement e_TrainCofficientType = doc.CreateElement("TrainCofficientType");
                e_TrainCofficientType.InnerText = TrainCofficientType.ToString();
                train_e.AppendChild(e_TrainCofficientType);

                XmlElement e_TrainHMMState = doc.CreateElement("TrainHMMState");
                e_TrainHMMState.InnerText = TrainHMMState.ToString();
                train_e.AppendChild(e_TrainHMMState);

                XmlElement e_TrainGMMComponent = doc.CreateElement("TrainGMMComponent");
                e_TrainGMMComponent.InnerText = TrainGMMComponent.ToString();
                train_e.AppendChild(e_TrainGMMComponent);

                XmlElement e_TrainGMMCovVar = doc.CreateElement("TrainGMMCovVar");
                e_TrainGMMCovVar.InnerText = TrainGMMCovVar.ToString();
                train_e.AppendChild(e_TrainGMMCovVar);


                root.AppendChild(train_e);
            }
            doc.Save(path);
        }
    }
}

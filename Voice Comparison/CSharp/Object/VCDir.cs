using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object
{
    public class VCDir
    {
        private string workingDir = "";
        private string tmpDir = "";
        private static VCDir instance = null;
        private string listWordsPath = "";
        private string dataDir = "";
        private string xmlDir = "";
        private string audioDir = "";
        private string settingFile = "";

        private string trainDir = "";
        private string trainDirAudio = "";
        private string trainDirMFCC = "";
        private string trainDirHMM = "";
        private string trainXmlFile = "";

        private string tmpDirProgramRecoder = "";
        private string tmpDirProgram = "";
        public static VCDir Instance {
            get {
                if (instance == null)
                {
                    return new VCDir();
                }
                else {
                    return instance;
                }
            }
        }

        public string WorkingDir
        {
            get { return workingDir; }
        }

        public string TmpDir
        {
            get { return tmpDir; }
        }


        public string DataDir {
            get { return dataDir; }
        }

        public string AudioDir
        {
            get { return audioDir; }
        }

        public string TrainDir
        {
            get { return trainDir; }
        }

        public string TrainDirAudio
        {
            get { return trainDirAudio; }
        }

        public string TrainDirMFCC
        {
            get { return trainDirMFCC; }
        }

        public string TrainDirHMM
        {
            get { return trainDirHMM; }
        }
        public string TrainXmlFile
        {
            get { return trainXmlFile; }
        }

        public string ListWordDir{
            get { return listWordsPath; }
        }

        public string SettingFile {
            get { return settingFile; }
        }

        public string TmpProgramDir {
            get {
                return tmpDirProgram;
            }
        }

        public string TmpRecoderDir {
            get { return tmpDirProgramRecoder; }
        }

        public string XMLDir {
            get { return xmlDir; }
        }

        public static string CreateDirectory(string path)
        {
            try
            {
                // Determine whether the directory exists. 
                if (Directory.Exists(path))
                {
                    //Console.WriteLine("That path exists already.");
                    return path;
                }
                DirectoryInfo di = Directory.CreateDirectory(path);
                return path;
            }
            catch (Exception)
            {

            }
            return null;
        }
        public string PathWaveFile
        {

            get
            {

                string tmpFile = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_");
                return tmpDirProgramRecoder + tmpFile + ".wav";
            }
        }
        private VCDir()
        {
            workingDir = Directory.GetCurrentDirectory() + "\\";
            tmpDir = Path.GetTempPath();
            dataDir = workingDir + VCConstant.DataDir;
            audioDir = dataDir + VCConstant.AudioDir;

            trainDir = dataDir + VCConstant.TrainDir;
            trainDirAudio = dataDir + VCConstant.TrainDirAudio;
            trainDirMFCC = dataDir + VCConstant.TrainDirMFCC;
            trainDirHMM = dataDir + VCConstant.TrainDirHMM;
            trainXmlFile = trainDir + VCConstant.TrainXmlFile;

            xmlDir = dataDir + VCConstant.XmlDir;
            listWordsPath = xmlDir + VCConstant.ListWords;
            settingFile = xmlDir + VCConstant.SettingFile;

            tmpDirProgram = tmpDir + "Voice_Comparasion\\";
            tmpDirProgramRecoder = tmpDirProgram + "Recoder\\";
            CreateDirectory(tmpDirProgram);
            CreateDirectory(tmpDirProgramRecoder);
        }
    }
}

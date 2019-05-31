using System;
using System.Collections.Generic;
using System.Linq;
using log4net.Config;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtractionWrapper;
using Model;
using Object;
using System.Reflection;
using System.Threading;
using System.Globalization;

[assembly: XmlConfigurator(Watch = true)]
namespace Voice_Comparasion
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        [STAThread]

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                BasicConfigurator.Configure();
                LogUtil.LoggerName = "MainLogger";
                try
                {
                    Assembly.Load("log4net");
                }
                catch (Exception e)
                {
                    LogUtil.Error(e.Message);
                }

               
                try
                {
                    Assembly.Load("ExtractionWrapper");
                }
                catch(Exception e)
                {
                    LogUtil.Error(e.Message);
                }

                if (args.Count<string>() > 0 && string.Compare(args[0], "Reg", true) == 0)
                {
                    Application.Run(new RegForm());
                }
                else if (args.Count<string>() > 0 && string.Compare(args[0], "TestMode", true) == 0)
                {
                    Application.Run(new TestMode());
                }
                else if (args.Count<string>() > 0 && string.Compare(args[0], "ProcessData", true) == 0)
                {
                    Application.Run(new ProcessData());
                }
                else
                {
                    Application.Run(new MainForm());
                }
            }
            catch(Exception ex) {
                LogUtil.Error(ex.Message);
            }
            
        }
    }
}

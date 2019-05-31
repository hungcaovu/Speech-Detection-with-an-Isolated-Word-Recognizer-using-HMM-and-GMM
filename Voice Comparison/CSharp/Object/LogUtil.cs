using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using DATA = System.Double;

namespace Object
{
    public enum LogLevel
    {
        DEBUG = 1,
        ERROR,
        FATAL,
        INFO,
        WARN
    }

    //public static class LogUtil
    //{
    //    #region Members
    //    private static readonly ILog logger = LogManager.GetLogger(typeof(LogUtil));
    //    #endregion

    //    #region Constructors
    //    static LogUtil()
    //    {
    //        XmlConfigurator.Configure();
    //    }
    //    #endregion

    //    #region Methods

    //    public static void WriteLog(LogLevel logLevel, string format, params object[] paramList ) {

    //        string log = string.Format(format, paramList);

    //        if (logLevel.Equals(LogLevel.DEBUG))
    //        {
    //            logger.Debug(log);
    //        }

    //        else if (logLevel.Equals(LogLevel.ERROR))
    //        {
    //            logger.Error(log);
    //        }

    //        else if (logLevel.Equals(LogLevel.FATAL))
    //        {
    //            logger.Fatal(log);
    //        }

    //        else if (logLevel.Equals(LogLevel.INFO))
    //        {
    //            logger.Info(log);
    //        }

    //        else if (logLevel.Equals(LogLevel.WARN))
    //        {

    //            logger.Warn(log);

    //        }

    //    }

    //    #endregion

    //}

    /// <summary>
    /// Using log configuration to perform log command
    /// </summary>
    public class LogUtil
    {
        private static string loggerName = "Default";
        private static ILog logger;

        /// <summary>
        /// Name of log in thread
        /// </summary>
        public static string LoggerName
        {
            get { return loggerName; }
            set { loggerName = value; }
        }

        /// <summary>
        /// Get current logger
        /// </summary>
        public static ILog Logger
        {
            get
            {
                if (null == logger)
                {
                    logger = LogManager.GetLogger(LoggerName);
                }
                return logger;
            }
        }

        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="msg">Message</param>
        public static void Debug(string msg)
        {
            Logger.Debug(msg);
        }

        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="format">Message (+format)</param>
        /// <param name="args">Argument for message format</param>
        public static void Debug(string format, params object[] args)
        {
            Logger.DebugFormat(format, args);
        }

        /// <summary>
        /// Log information message
        /// </summary>
        /// <param name="msg">Message</param>
        public static void Info(string msg)
        {
            Logger.Info(msg);
        }

        /// <summary>
        /// Log information message
        /// </summary>
        /// <param name="format">Message (+format)</param>
        /// <param name="args">Argument for message format</param>
        public static void Info(string format, params object[] args)
        {
            Logger.InfoFormat(format, args);
        }

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="msg">Message</param>
        public static void Warn(string msg)
        {
            Logger.Warn(msg);
        }

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="format">Message (+format)</param>
        /// <param name="args">Argument for message format</param>
        public static void Warn(string format, params object[] args)
        {
            Logger.WarnFormat(format, args);
        }

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="msg">Message</param>
        public static void Error(string msg)
        {
            Logger.Error(msg);
        }

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="format">Message (+format)</param>
        /// <param name="args">Argument for message format</param>
        public static void Error(string format, params object[] args)
        {
            Logger.ErrorFormat(format, args);
        }

        /// <summary>
        /// Log fatal message
        /// </summary>
        /// <param name="msg">Message</param>
        public static void Fatal(string msg)
        {
            Logger.Fatal(msg);
        }

        /// <summary>
        /// Log fatal message
        /// </summary>
        /// <param name="format">Message (+format)</param>
        /// <param name="args">Argument for message format</param>
        public static void Fatal(string format, params object[] args)
        {
            Logger.FatalFormat(format, args);
        }
    }

}
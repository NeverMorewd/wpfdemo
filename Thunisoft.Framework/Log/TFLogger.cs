using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Thunisoft.Framework.Log
{
    public class TFLogger
    {
        private static readonly string LogPah = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "CMLogs");
        private static readonly LogLevelEnum LogLevel = LogLevelEnum.Info;
        private static readonly object SyncObject = new object();
        static TFLogger()
        {
            LogPah = string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogPath"]?.ToString()) ? LogPah : ConfigurationManager.AppSettings["LogPath"]?.ToString();
            LogLevel = string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogLevel"]?.ToString()) ? LogLevel : (LogLevelEnum)int.Parse(ConfigurationManager.AppSettings["LogLevel"]?.ToString());
        }

        public static void LogDebug(string aContent)
        {
            Log(aContent, LogLevelEnum.Debug);
        }
        public static void LogInfo(string aContent)
        {
            Log(aContent, LogLevelEnum.Info);
        }
        public static void LogWarning(string aContent)
        {
            Log(aContent, LogLevelEnum.Warning);
        }
        public static void LogError(string aContent)
        {
            Log(aContent, LogLevelEnum.Error);
        }
        public static void LogError(Exception anException,string anAttachedMessage = null)
        {
            Log(ExceptionParse(anException, anAttachedMessage),LogLevelEnum.Error);
        }

        private static void Log(string aContent, LogLevelEnum aLogLevelEnum)
        {
            lock (SyncObject)
            {
                StringBuilder message = new StringBuilder();
                try
                {
                    if (aLogLevelEnum.CompareTo(LogLevel) < 0)
                    {
                        return;
                    }
                    string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    if (Directory.Exists(LogPah) == false)
                    {
                        Directory.CreateDirectory(LogPah);
                    }
                    string logLevelText = aLogLevelEnum.ToString();
                    string fileName = Path.Combine(LogPah, DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + logLevelText + ".log");

                    //message.Append("FileName: ");
                    //message.Append(new StackTrace(true).GetFrame(2).GetFileName());
                    //message.Append(Environment.NewLine);
                    //message.Append("LineNumber: ");
                    //message.Append(new StackTrace(true).GetFrame(2).GetFileLineNumber());
                    //message.Append(Environment.NewLine);
                    message.Append("LogTime: ");
                    message.Append(dateTime);
                    message.Append(Environment.NewLine);
                    message.Append("LogMessage: ");
                    message.Append(aContent);
                    message.Append(Environment.NewLine);
                    message.Append("------------------------------------------------------------------------------------------------------");
                    FileStream fs;
                    if (File.Exists(fileName))
                    {
                        fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                    }
                    else
                    {
                        fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    }
                    using (fs)
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(message);
                        }
                    }
                }
                catch(IOException)
                {
                    //ignore
                }
            }
        }
        private static void LogInternal(string aContent, LogLevelEnum aLogLevelEnum)
        {
            lock (SyncObject)
            {
                if (aLogLevelEnum.CompareTo(LogLevel) < 0)
                {
                    return;
                }
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (Directory.Exists(LogPah) == false)
                {
                    Directory.CreateDirectory(LogPah);
                }
                string logLevelText = aLogLevelEnum.ToString();
                string fileName = Path.Combine(LogPah, DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + logLevelText + ".log");

                StringBuilder message = new StringBuilder();
                message.Append("aFileName: ");
                message.Append(new StackTrace(true).GetFrame(2).GetFileName());
                message.Append(Environment.NewLine);
                message.Append("LineNumber: ");
                message.Append(new StackTrace(true).GetFrame(2).GetFileLineNumber());
                message.Append(Environment.NewLine);
                message.Append("LogTime: ");
                message.Append(dateTime);
                message.Append(Environment.NewLine);
                message.Append("LogMessage: ");
                message.Append(aContent);
                message.Append(Environment.NewLine);
                message.Append("------------------------------------------------------------------------------------------------------");
                FileInfo fileInfo = new FileInfo(fileName);
                TextWriter writer;
                if (File.Exists(fileName) == true)
                {
                    writer = fileInfo.AppendText();
                }
                else
                {
                    writer = fileInfo.CreateText();
                }
                writer?.WriteLine();
                writer?.WriteLine(message.ToString());
                writer?.Flush();
                writer?.Close();
            }
        }

        private static string ExceptionParse(Exception anException,string anExceptionMessage = null)
        {
            if (anException != null)
            {
                anExceptionMessage += Environment.NewLine + "Exception: " + anException.GetType().FullName + Environment.NewLine + "ExceptionMessage: " + anException.Message + Environment.NewLine + "ExceptionStackTrace: " + anException.StackTrace + Environment.NewLine;
                return ExceptionParse(anException.InnerException, anExceptionMessage);
            }
            else
            {
                return anExceptionMessage;
            }
        }

        private static void LogLight(string aContent, LogModule aModuleEnum, LogLevelEnum aLogLevelEnum)
        {
            lock (SyncObject)
            {
                if (aLogLevelEnum.CompareTo(LogLevel) < 0)
                {
                    return;
                }
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff");
                if (Directory.Exists(LogPah) == false)
                {
                    Directory.CreateDirectory(LogPah);
                }
                string logLevelText = aLogLevelEnum.ToString();
                string fileName = Path.Combine(LogPah, DateTime.Now.Date.ToString("yyyy-MM-dd") + "_" + aModuleEnum + "_" + logLevelText + ".log");

                StringBuilder message = new StringBuilder();
                message.Append($"{dateTime}  {aContent}");
                FileInfo fileInfo = new FileInfo(fileName);
                TextWriter writer;
                if (File.Exists(fileName) == true)
                {
                    writer = fileInfo.AppendText();
                }
                else
                {
                    writer = fileInfo.CreateText();
                }

                writer?.WriteLine(message.ToString());
                writer?.Flush();
                writer?.Close();
            }
        }

        public static void LogDebug(LogModule aModuleEnum, string aContent)
        {
            LogLight(aContent, aModuleEnum, LogLevelEnum.Debug);
        }
        public static void LogInfo(LogModule aModuleEnum, string aContent)
        {
            LogLight(aContent, aModuleEnum, LogLevelEnum.Info);
        }
        public static void LogWarning(LogModule aModuleEnum, string aContent)
        {
            LogLight(aContent, aModuleEnum, LogLevelEnum.Warning);
        }
        public static void LogError(LogModule aModuleEnum, string aContent)
        {
            LogLight(aContent, aModuleEnum, LogLevelEnum.Error);
        }
    }
}

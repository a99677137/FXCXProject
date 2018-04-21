using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Game.Tools.CommonTools
{
    public class GameLog
    {
        public enum LogType : int
        {
            Debug = 0,
            Lua = 1,
            Error = 2,
            LuaError = 3,
            Exception = 4,
        }

        public static bool IsGameDebug = true;
        public static bool IsAutoClearGUILog = true;
        public static StringBuilder GUIStringBulider = new StringBuilder();

        private static StreamWriter streamWriter = null;
        public static string StrLogPath = "";
        private static int GUIMaxLen = 1024 * 8;
        private static int GUIMinLen = 1024 * 4;
        static GameLog()
        {
            Init();
        }

        private static void Init()
        {
            try
            {
                if (Application.platform == RuntimePlatform.IPhonePlayer)
                {
                    string path = Application.persistentDataPath.Substring(0, Application.persistentDataPath.Length - 5);
                    path = path.Substring(0, path.LastIndexOf('/'));
                    StrLogPath = Path.Combine(Path.Combine(path, "Documents"), String.Format("{0}{1}", GetFileDateTime(), "_GameLog.txt"));
                }
                else
                {
                    StrLogPath = String.Format("{0}{1}{2}", Application.persistentDataPath, GetFileDateTime(), "_GameLog.txt");
                }

                if (File.Exists(StrLogPath) == false)
                    File.Create(StrLogPath).Close();
                streamWriter = File.AppendText(StrLogPath);
            }
            catch (Exception)
            {
                streamWriter = null;
            }
        }

        private static string GetFileDateTime() {
            var time = System.DateTime.Now;
            return time.ToString("[yyyy-MM-dd_HH-mm-ss-ffff]");
        }

        private static string GetCurDateTime()
        {
            var time = System.DateTime.Now;
            return time.ToString("[yyyy-MM-dd HH:mm:ss:ffff]");
        }

        public static void Debug(string msg, params object[] args)
        {
            Log(LogType.Debug, msg, args);
            UnityEngine.Debug.Log(string.Format(msg, args));
        }

        public static void LuaDebug(string msg, params object[] args)
        {
            Log(LogType.Lua, msg, args);
            UnityEngine.Debug.Log(string.Format(msg, args));
        }

        public static void Error(string msg, params object[] args)
        {
            Log(LogType.Error, msg, args);
            UnityEngine.Debug.LogError(string.Format(msg, args));
        }

        public static void LuaError(string msg, params object[] args)
        {
            Log(LogType.LuaError, msg, args);
            UnityEngine.Debug.LogError(string.Format(msg, args));
        }

        public static void Exception(string msg, params object[] args)
        {
            Log(LogType.Exception, msg, args);
            UnityEngine.Debug.LogException(new System.Exception(string.Format(msg, args)));
        }


        private static void Log(LogType type, string msg, params object[] args)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(GetCurDateTime());
            switch (type)
            {
                case LogType.Debug:
                    builder.Append("[Debug]:");
                    break;
                case LogType.Lua:
                    builder.Append("[Lua]:");
                    break;
                case LogType.Error:
                    builder.Append("[Error]:");
                    break;
                case LogType.LuaError:
                    builder.Append("[LuaError]:");
                    break;
                case LogType.Exception:
                    builder.Append("[Exception]:");
                    break;
            }
            builder.AppendFormat(msg, args);
            builder.Append("\r\n");
            OutPut(builder.ToString());
        }


        private static void OutPut(string msg)
        {
            if (streamWriter != null)
            {
                streamWriter.Write(msg);
                streamWriter.Flush();
            }
            GUIStringBulider.Append(msg);
            CheckGUILogLen();
        }

        private static void CheckGUILogLen()
        {
            if (!IsAutoClearGUILog)
            {
                return;
            }
            if (GUIStringBulider.Length > GUIMaxLen)
            {
                GUIStringBulider.Remove(0, GUIMaxLen - GUIMinLen);
                UnityEngine.Debug.Log("Clear GUI Log...");
            }

        }

    }
}

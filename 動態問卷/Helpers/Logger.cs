using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace 動態問卷.Helpers
{
    public class Logger
    {
        private const string _savePath = "";
        public static void WriteLog(string moduleName, Exception ex)
        {
            string content = "";
            File.AppendAllText(_savePath, content);
        }
    }
}
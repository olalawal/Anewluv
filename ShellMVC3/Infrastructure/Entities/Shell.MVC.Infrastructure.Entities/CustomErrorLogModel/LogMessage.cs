using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
    
    public class LogMessage
    {
        private string m_text;
        private string m_assemblyName;
        private string m_className;
        private string m_methodName;
        private string m_serverIp;

        private Nullable<byte> m_logLevel;
        public LogMessage(string text, string assemblyName, string className, string methodName, string serverIp, byte logLevel)
        {
            m_text = text;
            m_assemblyName = assemblyName;
            m_className = className;
            m_methodName = methodName;
            m_serverIp = serverIp;
            m_logLevel = logLevel;
        }
        public LogMessage(string delimitedString)
        {
            string[] items = delimitedString.Split('|');
            m_text = items[0];
            m_assemblyName = items[1];
            m_className = items[2];
            m_methodName = items[3];
            m_serverIp = items[4];
            m_logLevel = Convert.ToByte(items[5]);
        }
        public LogMessage()
        {
            m_text = Text;
            m_assemblyName = string.Empty;
            m_className = string.Empty;
            m_methodName = string.Empty;
            m_serverIp = string.Empty;
            m_logLevel = 0;
        }
        public string Text
        {
            get { return m_text; }
            set { m_text = value; }
        }
        public string AssemblyName
        {
            get { return m_assemblyName; }
            set { m_assemblyName = value; }
        }
        public string ClassName
        {
            get { return m_className; }
            set { m_className = value; }
        }
        public string MethodName
        {
            get { return m_methodName; }
            set { m_methodName = value; }
        }
        public string ServerIP
        {
            get { return m_serverIp; }
            set { m_serverIp = value; }
        }
        public byte? LogLevel
        {
            get { return m_logLevel; }
            set { m_logLevel = value; }
        }
        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}", m_text, m_assemblyName, m_className, m_methodName, m_serverIp, m_logLevel.ToString());
        }
    }

}

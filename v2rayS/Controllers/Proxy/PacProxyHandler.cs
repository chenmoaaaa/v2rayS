using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using v2rayS.Utils;

namespace v2rayS.Controllers.Proxy
{
    class PacProxyHandler : ProxyHandler
    {
        private static PacProxyHandler _instance;

        public new static PacProxyHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PacProxyHandler();
            }
            return _instance;
        }

        public readonly string PAC_FILENAME = "pac";

        public string GetPacServerName()
        {
            return "http://127.0.0.1:1081/pac/";
        }

        public void SetProxy()
        {
            Process.SysProxyProcess = ProcessStarter.StartProcess(Process.SysDirectory, "sysproxy.exe", $"pac {GetPacServerName()}");
        }
    }
}

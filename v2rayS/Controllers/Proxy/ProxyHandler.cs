using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using v2rayS.Utils;

namespace v2rayS.Controllers.Proxy
{
    class ProxyHandler
    {
        private static ProxyHandler _instance;

        public static ProxyHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ProxyHandler();
            }
            return _instance;
        }

        public void UnsetProxy()
        {
            Process.SysProxyProcess = ProcessStarter.StartProcess(Process.SysDirectory, "sysproxy.exe", "set 9");
        }
    }
}

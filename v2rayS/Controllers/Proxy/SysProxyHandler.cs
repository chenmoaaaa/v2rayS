using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using v2rayS.Utils;

namespace v2rayS.Controllers.Proxy
{
    class SysProxyHandler : ProxyHandler
    {
        private static SysProxyHandler _instance;

        public new static SysProxyHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SysProxyHandler();
            }
            return _instance;
        }

        public void SetProxy()
        {
            Process.SysProxyProcess = 
                ProcessStarter.StartProcess(Process.SysDirectory, 
                    "sysproxy.exe", 
                    @"global 127.0.0.1:8123 ""localhost;127.*;10.*;172.16.*;172.17.*;172.18.*;172.19.*;172.20.*;172.21.*;172.22.*;172.23.*;172.24.*;172.25.*;172.26.*;172.27.*;172.28.*;172.29.*;172.30.*;172.31.*;172.32.*;192.168.*;<local>""");
        }
    }
}

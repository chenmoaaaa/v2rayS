using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using v2rayS.Controllers.Proxy;
using v2rayS.Models;
using v2rayS.Utils;

namespace v2rayS.Controllers
{
    class Process
    {
        public static BaseForm BaseForm { get; set; }
        public static System.Diagnostics.Process PolipoProcess { get; set; }
        public static System.Diagnostics.Process SysProxyProcess { get; set; }
        public static System.Diagnostics.Process V2rayProcess { get; set; }

        private static Configuration _config;
        public static readonly string CurrentDirectory = Application.ExecutablePath.Replace("\\v2rayS.exe", "");
        public static readonly string SysDirectory = CurrentDirectory + "\\sys";

        public static void Exit()
        {
            HttpServer.Listener.Close();
            ProxyHandler.GetInstance().UnsetProxy();
            SysProxyProcess.WaitForExit();
            try
            {
                PolipoProcess.Kill();
                V2rayProcess.Kill();
            }
            catch (Exception) { }

            ConfigHandler.SaveFile();
            Application.Exit();
        }

        public static void RefreshProxyMode()
        {
            _config = ConfigHandler.GetConfig();
            switch (_config.IsProxyOn)
            {
                case true:
                    switch (_config.ProxyMode)
                    {
                        case ProxyMode.Pac:
                            PacProxyHandler.GetInstance().SetProxy();
                            break;
                        case ProxyMode.Sys:
                            SysProxyHandler.GetInstance().SetProxy();
                            break;
                    }
                    break;
                case false:
                    ProxyHandler.GetInstance().UnsetProxy();
                    break;
            }
            
        }
    }
}

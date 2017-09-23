using System;
using System.IO;
using System.Windows.Forms;
using v2rayS.Controllers;
using v2rayS.Utils;

namespace v2rayS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigHandler.LoadFile();
            Process.PolipoProcess = ProcessStarter.StartProcess(Process.SysDirectory, "v2_polipo.exe", 
                $"-c {Process.SysDirectory + "\\v2_polipo.conf"}");
            Process.V2rayProcess = ProcessStarter.StartProcess(null, "v2ray.exe", "");

            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Process.BaseForm = new BaseForm();
            Process.RefreshProxyMode();
            HttpServer.StartListener();

            Application.Run(Process.BaseForm);
        }
    }
}

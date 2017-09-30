using System;
using System.IO;
using System.Reflection;
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
            Directory.SetCurrentDirectory(Process.CurrentDirectory);

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

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

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                string resourceName = "v2rayS." + new AssemblyName(args.Name).Name + ".dll";
                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        return null;
                    }
                    byte[] assemblyData = new byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

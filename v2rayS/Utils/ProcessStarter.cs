using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace v2rayS.Utils
{
    class ProcessStarter
    {
        public static Process StartProcess(string path, string name, string arguments)
        {
            ProcessStartInfo Info = new ProcessStartInfo
            {
                FileName = path != null ? path + "\\" + name : name,
                Arguments = arguments,
                CreateNoWindow = true,
                WorkingDirectory = path ?? Directory.GetCurrentDirectory(),
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process _proc;
            try
            {
                _proc = Process.Start(Info);
                return _proc;
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                MessageBox.Show($"{e.Message}\n{e.StackTrace}");
                return null;
            }
        }

        public static Process StartProcessForeground(string path, string name, string arguments)
        {
            ProcessStartInfo Info = new ProcessStartInfo
            {
                FileName = path != null ? path + "\\" + name : name,
                Arguments = arguments,
                CreateNoWindow = true,
                WorkingDirectory = path ?? Directory.GetCurrentDirectory()
            };
            Process _proc;
            try
            {
                _proc = Process.Start(Info);
                return _proc;
            }
            catch (System.ComponentModel.Win32Exception e)
            {
                MessageBox.Show($"{e.Message}\n{e.StackTrace}");
                return null;
            }

        }
    }
}

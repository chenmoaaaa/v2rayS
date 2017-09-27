using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using v2rayS.Controllers;
using v2rayS.Utils;

namespace v2rayS.Models
{
    class TaskbarContextMenu
    {
        private static readonly int ITEM_COUNT = 9;
        private static ContextMenu _menu;
        private static MenuItem[] _items;
        private static Configuration _config = ConfigHandler.GetConfig();
        
        public static ContextMenu GetInstance()
        {
            if (_menu == null)
            {
                _items = new MenuItem[ITEM_COUNT];

                _items[0] = new MenuItem("启用系统代理", new EventHandler(IsProxyOn_OnClick))
                {
                    Checked = _config.IsProxyOn
                };
                MenuItem[] _proxyModeItems = new MenuItem[2];
                _proxyModeItems[0] = new MenuItem("PAC模式", new EventHandler(PacProxyMode_OnClick))
                {
                    Checked = _config.ProxyMode == ProxyMode.Pac
                };
                _proxyModeItems[1] = new MenuItem("全局模式", new EventHandler(SysProxyMode_OnClick))
                {
                    Checked = _config.ProxyMode == ProxyMode.Sys
                };
                _items[1] = new MenuItem("系统代理模式", _proxyModeItems) { Enabled = _config.IsProxyOn };
                _items[2] = new MenuItem("-");
                _items[3] = new MenuItem("编辑config.json", new EventHandler(EditConfigFile_OnClick));
                _items[4] = new MenuItem("编辑PAC文件", new EventHandler(EditPacFile_OnClick));
                _items[5] = new MenuItem("重启V2Ray进程", new EventHandler(RestartProcess_OnClick));
                _items[6] = new MenuItem("开机启动", new EventHandler(AutoStart_OnClick)) { Checked = _config.IsAutoStart };
                _items[7] = new MenuItem("-");
                _items[8] = new MenuItem("退出", (sender, e) => Process.Exit());

                _menu = new ContextMenu(_items);
            }
            return _menu;
        }

        private static void EditConfigFile_OnClick(object sender, EventArgs e)
        {
            ProcessStarter.StartProcessForeground(null, "notepad.exe", "config.json");
        }

        private static void EditPacFile_OnClick(object sender, EventArgs e)
        {
            ProcessStarter.StartProcessForeground(null, "notepad.exe", "pac");
        }

        private static void RestartProcess_OnClick(object sender, EventArgs e)
        {
            try
            {
                Process.V2rayProcess.Kill();
                Process.V2rayProcess = ProcessStarter.StartProcess(null, "v2ray.exe", "");
            }
            catch (Exception)
            {
                MessageBox.Show("V2Ray进程重启失败");
            }
        }

        private static void IsProxyOn_OnClick(object sender, EventArgs e)
        {
            if (_config.IsProxyOn == false)
            {
                _config.IsProxyOn = true;
            }
            else
            {
                _config.IsProxyOn = false;
            }

            _items[0].Checked = _config.IsProxyOn;
            _items[1].Enabled = _config.IsProxyOn;

            Process.RefreshProxyMode();
        }

        private static void SysProxyMode_OnClick(object sender, EventArgs e)
        {
            _config.ProxyMode = ProxyMode.Sys;
            _items[1].MenuItems[0].Checked = false;
            _items[1].MenuItems[1].Checked = true;
            Process.RefreshProxyMode();
        }

        private static void PacProxyMode_OnClick(object sender, EventArgs e)
        {
            _config.ProxyMode = ProxyMode.Pac;
            _items[1].MenuItems[0].Checked = true;
            _items[1].MenuItems[1].Checked = false;
            Process.RefreshProxyMode();
        }

        private static void AutoStart_OnClick(object sender, EventArgs e)
        {
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            if (!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                MessageBox.Show("请以管理员权限运行");
                return;
            }
            string strName = Application.ExecutablePath;
            string strnewName = strName.Substring(strName.LastIndexOf("\\") + 1);
            if (_config.IsAutoStart)
            {
                //修改注册表，使程序开机时不自动执行
                _items[6].Checked = false;
                Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                Rkey.DeleteValue("v2rayS", false);
                _config.IsAutoStart = false;
            }
            else
            {
                _items[6].Checked = true;
                if (!File.Exists(strName))//指定文件是否存在  
                    return;
                Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (Rkey == null)
                    Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
                Rkey.SetValue("v2rayS", strName);//修改注册表，使程序开机时自动执行
                _config.IsAutoStart = true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using v2rayS.Controllers;
using v2rayS.Utils;

namespace v2rayS.Models
{
    class TaskbarContextMenu
    {
        private static readonly int ITEM_COUNT = 3;
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

                _items[2] = new MenuItem("退出", (sender, e) => Process.Exit());

                _menu = new ContextMenu(_items);
            }
            return _menu;
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
    }
}

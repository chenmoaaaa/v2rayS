using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using v2rayS.Controllers;
using v2rayS.Forms;
using v2rayS.Models;

namespace v2rayS
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            InitializeComponent();
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();

            TaskbarNotifyIcon.ContextMenu = TaskbarContextMenu.GetInstance();
        }
    }
}
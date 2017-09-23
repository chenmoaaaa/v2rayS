using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace v2rayS.Models
{
    enum ProxyMode
    {
        Pac,
        Sys
    }

    [Serializable]
    class Configuration
    {
        public bool IsProxyOn { get; set; }
        public ProxyMode ProxyMode { get; set; }
    }
}

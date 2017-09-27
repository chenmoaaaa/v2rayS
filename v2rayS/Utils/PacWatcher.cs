using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace v2rayS.Utils
{
    class PacWatcher
    {
        public PacWatcher(string path, string filter)
        {
            this.WatcherStrat(path, filter);
        }

        private void WatcherStrat(string path, string filter)
        {
            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = path,
                Filter = filter
            };

            watcher.Changed += (sender, e) =>
            {
                HttpServer.Refresh();
            };

            watcher.EnableRaisingEvents = true;
        }
    }
}

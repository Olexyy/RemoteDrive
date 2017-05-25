using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://msdn.microsoft.com/en-us/library/system.io.filesystemeventargs(v=vs.110).aspx
namespace RemoteDrive
{
    public class FileWatcher
    {
        private FileSystemWatcher FileSystemWatcher { get; set; }
        public bool Started { get; private set; }

        public FileWatcher(string path, FileSystemEventHandler onChanged, FileSystemEventHandler onCreated,
            FileSystemEventHandler onDeleted, RenamedEventHandler onRenamed)
        {
            this.FileSystemWatcher = new FileSystemWatcher();
            this.FileSystemWatcher.Path = path;
            this.FileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            this.FileSystemWatcher.Filter = "*.*";
            this.FileSystemWatcher.Created += new FileSystemEventHandler(onCreated);
            this.FileSystemWatcher.Changed += new FileSystemEventHandler(onChanged);
            this.FileSystemWatcher.Deleted += new FileSystemEventHandler(onDeleted);
            this.FileSystemWatcher.Renamed += new RenamedEventHandler(onRenamed);
        }

        public void Start()
        {
            this.FileSystemWatcher.EnableRaisingEvents = true;
            this.Started = true;
        }

        public void Stop()
        {
            this.FileSystemWatcher.EnableRaisingEvents = false;
            this.Started = false;
        }
    }
}

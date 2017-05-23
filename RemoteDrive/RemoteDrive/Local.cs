using System;
using System.IO;

namespace FtpClient
{
    public enum LocalEventType
    {
        Exception, ListDirectory, UploadOk, DeleteFileOk,
        DeleteFolderOk, DownloadOk, MakeDirectoryOk,
    }
    public class LocalEventArgs : EventArgs
    {
        public LocalEventType Type { get; set; }
        public LocalCwd Cwd { get; set; }
        public Exception Exception { get; set; }
        public LocalEventArgs(LocalEventType type, LocalCwd cwd, Exception exception = null)
        {
            this.Type = type;
            this.Cwd = cwd;
            this.Exception = exception;
        }
    }
    public delegate void LocalEventHandler(object sender, LocalEventArgs args);
    public class Local
    {
        public LocalCwd Cwd { get; private set; }
        public Local(string root, LocalEventHandler eventHandler)
        {
            this.CreateDirectoryIfNotExist(root);
            this.Cwd = new LocalCwd(root);
            this.Cwd.LocalEvent += eventHandler;
            this.Cwd.GetCwd();
        }
        private void CreateDirectoryIfNotExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}

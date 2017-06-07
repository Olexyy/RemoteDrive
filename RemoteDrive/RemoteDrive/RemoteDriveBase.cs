using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteDrive.ServiceReference;

namespace RemoteDrive
{
    public enum RemoteDriveEventType
    {
        CreateRemoteBegin, CreateRemoteOk, CreateRemoteFail, DeleteRemoteBegin, DeleteRemoteOk, DeleteRemoteFail,
        CreateLocalBegin, CreateLocalOk, CreateLocalFail, ReadRemoteBegin, ReadRemoteOk, ReadRemoteFail,
        ReadLocalBegin, ReadLocalOk, ReadLocalFail, DownloadBegin, DownloadOk, DownloadFail, DeleteLocalOk,
        DeleteLocalFail, DeleteLocalBegin, MoveRemoteBegin, MoveRemoteOk, MoveRemoteFail,
    }

    public class RemoteDriveEventArgs : EventArgs
    {
        public RemoteDriveEventType Type { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public RemoteDriveItem Item { get; set; }
        public RemoteDriveEventArgs(RemoteDriveEventType type, RemoteDriveItem item = null,
            string message = null, Exception exception = null)
        {
            this.Type = type;
            this.Item = item;
            this.Message = message;
            this.Exception = exception;
        }
    }

    public delegate void RemoteDriveEventHandler(object sender, RemoteDriveEventArgs args);

    class RemoteDriveBase
    {
        private ServiceClient ServiceClient { get; set; }
        public RemoteDriveItem DirectoryLocal { get; private set; }
        public RemoteDriveItem DirectoryRemote { get; private set; }
        public event RemoteDriveEventHandler RemoteDriveEvent;
        private RemoteDrivePath PathResolver { get; set; }

        public RemoteDriveBase(ServiceClient serviceClient, RemoteDrivePath pathResolver, RemoteDriveEventHandler remoteDriveEventHandler = null)
        {
            this.ServiceClient = serviceClient;
            this.PathResolver = pathResolver;
            if (remoteDriveEventHandler != null)
                this.RemoteDriveEvent += remoteDriveEventHandler;
        }
        private void InvokeRemoteDriveEvent(RemoteDriveEventType type, RemoteDriveItem item = null,
            string message = null, Exception exception = null)
        {
            RemoteDriveEventArgs args = new RemoteDriveEventArgs(type, item, message, exception);
            this.RemoteDriveEvent?.Invoke(this, args);
        }
        public void ReadDirectoryRemote(string path = null)
        {
            path = (path == null) ? this.DirectoryRemote.FullPath : path;
            Task.Run(() => this.ReadDirectoryRemoteThread(path));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.ReadRemoteBegin);
        }
        private void ReadDirectoryRemoteThread(string path)
        {
            try
            {
                RemoteDriveItem item = this.ServiceClient.ReadItem(path);
                this.DirectoryRemote = item;
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.ReadRemoteOk, item);
            }
            catch(Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.ReadRemoteFail, null, null, e);
            }
        }
        public void ReadDirectoryLocal(string path = null)
        {
            path = (path == null) ? this.DirectoryLocal.FullPath : path;
            Task.Run(() => this.ReadDirectoryLocalThread(path));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.ReadLocalBegin);
        }
        private void ReadDirectoryLocalThread(string path)
        {
            try
            {
                RemoteDriveItem item = new RemoteDriveItem(path);
                this.DirectoryLocal = item;
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.ReadLocalOk, item);
            }
            catch (Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.ReadLocalFail, null, null, e);
            }
        }
        public void Download(string path)
        {
            Task.Run(() => this.DownloadThread(path));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.DownloadBegin);
        }
        private void DownloadThread(string path)
        {
            try
            {
                RemoteDriveItem item = this.ServiceClient.ReadItem(path);
                if(this.PathResolver.UserRoot != item.Name)
                    item.Localize(this.PathResolver).CreateOrUpdate();
                if (item.IsDirectory())
                    foreach (RemoteDriveItem child in item.Children())
                        this.DownloadRecursion(child);
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.DownloadOk);
            }
            catch (Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.DownloadFail, null, null, e);
            }
        }
        private void DownloadRecursion(RemoteDriveItem item)
        {
            RemoteDriveItem downloaded = this.ServiceClient.ReadItem(item.FullPath);
            downloaded.Localize(this.PathResolver).CreateOrUpdate();
            if (item.IsDirectory())
                foreach (RemoteDriveItem child in downloaded.Children())
                    this.DownloadRecursion(child);
        }
        public void CreateRemote(RemoteDriveItem item)
        {
            Task.Run(() => this.CreateRemoteThread(item));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.CreateRemoteBegin);
        }
        private void CreateRemoteThread(RemoteDriveItem item)
        {
            try
            {
                this.ServiceClient.CreateItem(item.Load());
                if (item.IsDirectory())
                    foreach (RemoteDriveItem child in item.Children())
                        this.CreateRemoteRecursion(child);
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.CreateRemoteOk);
            }
            catch (Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.CreateRemoteFail, null, null, e);
            }
        }
        private void CreateRemoteRecursion(RemoteDriveItem item)
        {
            if (item.IsFile())
                item.GetBinary();
            this.ServiceClient.CreateItem(item.Load());
            if (item.IsDirectory())
                foreach (RemoteDriveItem child in item.Children())
                    this.CreateRemoteRecursion(child);
        }
        public void DeleteRemote(RemoteDriveItem item)
        {
            Task.Run(() => this.DeleteRemoteThread(item));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.DeleteRemoteBegin);
        }
        private void DeleteRemoteThread(RemoteDriveItem item)
        {
            try
            {
                this.ServiceClient.DeleteItem(item);
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.DeleteRemoteOk);
            }
            catch (Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.DeleteRemoteFail, null, null, e);
            }
        }
        public void DeleteLocal(RemoteDriveItem item)
        {
            Task.Run(() => this.DeleteLocalThread(item));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.DeleteLocalBegin);
        }
        private void DeleteLocalThread(RemoteDriveItem item)
        {
            try
            {
                item.Delete();
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.DeleteLocalOk);
            }
            catch (Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.DeleteLocalFail, null, null, e);
            }
        }
        public void MoveRemote(RemoteDriveItem item, string newPath)
        {
            Task.Run(() => this.MoveRemoteThread(item, newPath));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.MoveRemoteBegin);
        }
        private void MoveRemoteThread(RemoteDriveItem item, string newPath)
        {
            try
            {
                this.ServiceClient.MoveItem(item, newPath);
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.MoveRemoteOk);
            }
            catch (Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.MoveRemoteFail, null, null, e);
            }
        }
        public void CreateLocal(RemoteDriveItem item)
        {
            Task.Run(() => this.CreateLocalThread(item));
            this.InvokeRemoteDriveEvent(RemoteDriveEventType.CreateLocalBegin);
        }
        private void CreateLocalThread(RemoteDriveItem item)
        {
            try
            {
                item.Create();
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.CreateLocalOk);
            }
            catch (Exception e)
            {
                this.InvokeRemoteDriveEvent(RemoteDriveEventType.CreateLocalFail, null, null, e);
            }
        }
        public void ClearDirectories()
        {
            this.DirectoryLocal = null;
            this.DirectoryRemote = null;
        }
        public void Navigate(string path)
        {
            this.ReadDirectoryLocal(path);
            this.ReadDirectoryRemote(path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;

namespace FtpClient
{
    public enum FtpEventType { Exception, ListDirectory, UploadOk, DeleteFileOk,
                               DeleteFolderOk, DownloadOk, MakeDirectoryOk, }
    public class FtpEventArgs : EventArgs
    {
        public FtpEventType Type { get; set; }
        public FtpCwd Cwd { get; set; }
        public Exception Exception { get; set; }
        public FtpEventArgs(FtpEventType type, FtpCwd cwd, Exception exception = null)
        {
            this.Type = type;
            this.Cwd = cwd;
            this.Exception = exception;
        }
    }
    public delegate void FtpEventHandler (object sender, FtpEventArgs args);
    public class FtpFactory
    {
        private const string DIR = "<DIR>";
        private FtpCwd Cwd { get; set; }
        public NetworkCredential Credentials { get; private set; }
        public FtpFactory(FtpCwd cwd, NetworkCredential credentials)
        {
            this.Cwd = cwd;
            this.Credentials = credentials;
        }
        public FtpItem FtpItemNew(string raw)
        {
            List<string> parsed = raw.Split(' ').ToList();
            parsed.RemoveAll(i => i == String.Empty);
            if(parsed.Count > 4) // Handle names with slashes
                for (int i = 4; i < parsed.Count; i++)
                    parsed[3] += " " + parsed[i];
            if(parsed.Count >= 4)
            {
                string date = parsed[0];
                string time = parsed[1];
                string type = parsed[2];
                string name = parsed[3];
                DateTime datetime = DateTime.Parse(date + " " + time);
                string fullPath = Cwd.FullPath + "/" + name;
                if (type == DIR)
                    return new FtpFolder(name, fullPath, Cwd.FullPath, datetime);
                else
                    return new FtpFile(name, fullPath, Cwd.FullPath, datetime);
            }
            else
                return new FtpFolder("Invalid folder name", Cwd.FullPath, Cwd.FullPath, DateTime.Now);   
        }
        public IEnumerable<FtpItem> FtpItemDefault()
        {
            List<FtpItem> defaultItems = new List<FtpItem>();
            if(this.Credentials.Domain != this.Cwd.FullPath)
                defaultItems.Add(new FtpFolder(".", this.Credentials.Domain, null, null));
            if(this.Cwd.Root != null && this.Credentials.Domain != this.Cwd.Root)
                defaultItems.Add(new FtpFolder("..", this.Cwd.Root, this.DefineRoot(this.Cwd.Root), null));
            return defaultItems;
        }
        public FtpWebRequest FtpRequestNew(string method, string path = null)
        {
            if (path == null)
                path = Cwd.FullPath;
            FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(path);
            ftpRequest.Credentials = new NetworkCredential(this.Credentials.UserName, this.Credentials.Password);
            ftpRequest.Method = method;
            ftpRequest.UseBinary = true;
            ftpRequest.UsePassive = true;
            ftpRequest.KeepAlive = true;
            return ftpRequest;
        }
        private string DefineRoot(string oldRoot)
        {
            List<string> parsed = oldRoot.Split('/').ToList();
            parsed.RemoveAt(parsed.Count - 1);
            return String.Join("/", parsed);
        }
    }
    public class Ftp
    {
        public FtpCwd Cwd { get; private set; }
        public event FtpEventHandler FtpEvent;
        private object Lock { get; set; }
        private const int BufferSize = 2048;
        public FtpFactory Factory { get; }
        public Ftp(string domain, string userName, string password, FtpEventHandler eventHandler)
        {
            NetworkCredential credentials = new NetworkCredential(userName, password, domain);
            this.Cwd = new FtpCwd("", domain, null);
            this.Lock = new object();
            this.FtpEvent += eventHandler;
            ThreadPool.SetMaxThreads(3, 3);
            this.Factory = new FtpFactory(this.Cwd, credentials);
        }
        public Ftp(NetworkCredential credentials, FtpEventHandler eventHandler)
        {
            this.Cwd = new FtpCwd(String.Empty, credentials.Domain, null);
            this.Lock = new object();
            this.FtpEvent += eventHandler;
            ThreadPool.SetMaxThreads(3, 3);
            this.Factory = new FtpFactory(this.Cwd, credentials);
        }
        public void GetCwd(FtpItem item = null)
        {
            if (item != null && item.Type == FtpItemType.Folder)
            {
                this.Cwd.FullPath = item.FullPath;
                this.Cwd.Root = item.Root;
            }
            Task.Run( () => this.GetCwdProcess(this.Cwd.FullPath) );
        }
        /*public void GetCwd(string folder)
        {
            this.Cwd.Root = this.Cwd.FullPath;
            this.Cwd.FullPath = this.Cwd.FullPath + "/" + folder;
            Task.Run(() => this.GetCwdProcess(this.Cwd.FullPath));
        }*/
        private void GetCwdProcess(string cwdPath)
        {
            try
            {
                FtpWebRequest ftpRequest = this.Factory.FtpRequestNew(WebRequestMethods.Ftp.ListDirectoryDetails, cwdPath);
                FtpWebResponse ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;
                StreamReader reader = new StreamReader(ftpResponse.GetResponseStream(), Encoding.ASCII);
                lock (this.Lock)
                {
                    this.Cwd.Items.Clear();
                    this.Cwd.Items.AddRange(this.Factory.FtpItemDefault());
                    while (!reader.EndOfStream)
                        this.Cwd.Items.Add(this.Factory.FtpItemNew(reader.ReadLine()));
                }
                FtpEventArgs args = new FtpEventArgs(FtpEventType.ListDirectory, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch (Exception e)
            {
                throw new Exception("List directory fail.", e);
            }
        }
        public void Upload(LocalItem localItem)
        {
            if(localItem.Type == LocalItemType.File)
                Task.Run(() => this.UploadProcess(this.Cwd.FullPath + "/" + localItem.Name, localItem.FullPath));
        }
        private void UploadProcess(string ftpPath, string localPath)
        {
            Stream upload = null;
            FileStream local = null;
            try
            {
                FtpWebRequest ftpRequest = this.Factory.FtpRequestNew(WebRequestMethods.Ftp.UploadFile, ftpPath);
                upload = ftpRequest.GetRequestStream();
                local = File.Open(localPath, FileMode.Open);
                byte[] byteBuffer = new byte[BufferSize];
                int bytesSent = local.Read(byteBuffer, 0, BufferSize);
                while (bytesSent != 0)
                {
                    upload.Write(byteBuffer, 0, bytesSent);
                    bytesSent = local.Read(byteBuffer, 0, BufferSize);
                }
                FtpEventArgs args = new FtpEventArgs(FtpEventType.UploadOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch(Exception e)
            {
                throw new Exception("Upload fail.", e);
            }
            finally
            {
                if (upload != null)
                    upload.Close();
                if(local != null)
                    local.Close();
            }
        }
        public void Download(FtpItem ftpItem, LocalCwd localCwd, string newName = null)
        {
            newName = (newName == null) ? ftpItem.Name : newName;
            if (ftpItem.Type == FtpItemType.File)
                Task.Run(() => this.DownloadProcess(ftpItem.FullPath, localCwd.FullPath, newName));
        }
        private void DownloadProcess(string ftpPath, string localCwdPath, string newName)
        {
            Stream download = null;
            FileStream local = null;
            try
            {
                string localNewPath = Path.Combine(localCwdPath, newName);
                FtpWebRequest ftpRequest = this.Factory.FtpRequestNew(WebRequestMethods.Ftp.DownloadFile, ftpPath);
                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                download = ftpResponse.GetResponseStream();
                local = File.Open(localNewPath, FileMode.OpenOrCreate);
                byte[] byteBuffer = new byte[BufferSize];
                int bytesRecieve = download.Read(byteBuffer, 0, BufferSize);
                while (bytesRecieve != 0)
                {
                    local.Write(byteBuffer, 0, bytesRecieve);
                    bytesRecieve = download.Read(byteBuffer, 0, BufferSize);
                }
                FtpEventArgs args = new FtpEventArgs(FtpEventType.DownloadOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch (Exception e)
            {
                throw new Exception("Upload fail.", e);
            }
            finally
            {
                if (download != null)
                    download.Close();
                if (local != null)
                    local.Close();
            }
        }
        public void Delete(FtpItem item = null)
        {
            if (item == null) ;
            else if (item.Type == FtpItemType.Folder)
              Task.Run(() => this.DeleteFolderProcess(item.FullPath));
            else if (item.Type == FtpItemType.File)
              Task.Run(() => this.DeleteFileProcess(item.FullPath));
        }
        private void DeleteFileProcess(string ftpPath)
        {
            FtpWebResponse response = null;
            try
            {
                FtpWebRequest ftpRequest = this.Factory.FtpRequestNew(WebRequestMethods.Ftp.DeleteFile, ftpPath);
                response = (FtpWebResponse)ftpRequest.GetResponse();
                FtpEventArgs args = new FtpEventArgs(FtpEventType.DeleteFileOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch(Exception e)
            {
                throw new Exception("Delete fail.", e);
            }
            finally
            {
                if(response != null)
                    response.Close();
            }
        }
        private void DeleteFolderProcess(string ftpPath)
        {
            FtpWebResponse response = null;
            try
            {
                FtpWebRequest ftpRequest = this.Factory.FtpRequestNew(WebRequestMethods.Ftp.RemoveDirectory, ftpPath);
                response = (FtpWebResponse)ftpRequest.GetResponse();
                FtpEventArgs args = new FtpEventArgs(FtpEventType.DeleteFolderOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch (Exception e)
            {//TODO
                //throw new Exception("Delete fail.", e);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        public void NewFolder(string name)
        {
            string ftpPath = this.Cwd.FullPath + "/" + name;
            Task.Run(() => this.NewFolderProcess(ftpPath));
        }
        private void NewFolderProcess(string ftpPath)
        {
            FtpWebResponse response = null;
            try
            {
                FtpWebRequest ftpRequest = this.Factory.FtpRequestNew(WebRequestMethods.Ftp.MakeDirectory, ftpPath);
                response = (FtpWebResponse)ftpRequest.GetResponse();
                FtpEventArgs args = new FtpEventArgs(FtpEventType.MakeDirectoryOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch (Exception e)
            { // TODO: change to special event type.
                //throw new Exception("New folder fail.", e);
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
        public void InitCwd(string path)
        {
            FtpWebResponse response = null;
            try
            {
                FtpWebRequest ftpRequest = this.Factory.FtpRequestNew(WebRequestMethods.Ftp.MakeDirectory, path);
                response  = (FtpWebResponse)ftpRequest.GetResponse();
            }
            catch (Exception e) { }
            if (response != null)
                response.Close();
            Task.Run(() => this.GetCwdProcess(path));
        }
       
    }
}
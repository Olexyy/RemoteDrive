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
    public enum FtpItemType { Folder, File, Cwd }
    public enum LocalItemType { Folder, File, Cwd }
    public abstract class BaseItem
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string Root { get; set; }
        public Nullable<DateTime> Timestamp { get; set; }
        public BaseItem(string name, string fullPath, string root, Nullable<DateTime> timestamp)
        {
            this.Name = name;
            this.Root = root;
            this.FullPath = fullPath;
            this.Timestamp = timestamp;
        }
    }
    public abstract class FtpItem : BaseItem
    {
        public FtpItemType Type { get; private set; }
        public FtpItem(FtpItemType type, string name, string fullPath, string root, Nullable<DateTime> timestamp) : base(name, fullPath, root, timestamp)
        {
            this.Type = type;
        }
    }
    public class FtpFolder : FtpItem
    {
        public FtpFolder(string name, string fullPath, string root, Nullable<DateTime> timestamp) : base(FtpItemType.Folder, name, fullPath, root, timestamp) { }
    }
    public class FtpFile : FtpItem
    {
        public string Extension { get; private set; }
        public FtpFile(string name, string fullPath, string root, DateTime timestamp) : base(FtpItemType.File, name, fullPath, root, timestamp) { }
    }
    public class FtpCwd : FtpItem
    {
        public List<FtpItem> Items { get; private set; }
        public bool IsRoot { get { return this.Root == null; } }
        public FtpCwd(string name, string fullPath, string root) : base(FtpItemType.Cwd, name, fullPath, root, null)
        {
            this.Items = new List<FtpItem>();
        }
    }
    public abstract class LocalItem : BaseItem
    {
        public LocalItemType Type { get; private set; }
        public LocalItem(LocalItemType type, string name, string fullPath, string root, Nullable<DateTime> timestamp) : base(name, fullPath, root, timestamp)
        {
            this.Type = type;
        }
    }
    public class LocalFolder : LocalItem
    {
        public LocalFolder(string name, string fullPath, string root, DateTime timestamp) : base(LocalItemType.Folder, name, fullPath, root, timestamp) { }
    }
    public class LocalFile : LocalItem
    {
        public string Extension { get; private set; }
        public LocalFile(string name, string fullPath, string root, DateTime timestamp) : base(LocalItemType.File, name, fullPath, root, timestamp)
        {
            this.Extension = Path.GetExtension(this.FullPath);
        }
    }
    public class LocalCwd : LocalItem
    {
        public List<LocalItem> Items { get; private set; }
        public event LocalEventHandler LocalEvent;
        public LocalCwd(string fullPath) : base(LocalItemType.Cwd, null, fullPath, null, null)
        {
            this.Name = Path.GetFileName(fullPath);
            this.Root = Path.GetDirectoryName(fullPath);
            this.Items = new List<LocalItem>();
        }
        private void GetChildren()
        {
            this.Items.Clear();
            foreach (string itemFullPath in Directory.GetDirectories(this.FullPath))
            {
                DateTime timestamp = Directory.GetLastWriteTime(itemFullPath);
                string folderName = Path.GetFileName(itemFullPath);
                this.Items.Add(new LocalFolder(folderName, itemFullPath, this.FullPath, timestamp));
            }
            foreach (string itemFullPath in Directory.GetFiles(this.FullPath))
            {
                DateTime timestamp = Directory.GetLastWriteTime(itemFullPath);
                string fileName = Path.GetFileName(itemFullPath);
                this.Items.Add(new LocalFile(fileName, itemFullPath, this.FullPath, timestamp));
            }
        }
        public void GetCwd(string fullPath = null)
        {
            if(fullPath != null)
            {
                this.FullPath = fullPath;
                this.Name = Path.GetFileName(fullPath);
                this.Root = Path.GetDirectoryName(fullPath);
            }
            this.GetChildren();
            if(this.LocalEvent!= null)
                this.LocalEvent(this, new LocalEventArgs(LocalEventType.ListDirectory, this));
        }
        public void DeleteItem(LocalItem localItem)
        {
            try
            {
                if(localItem.Type == LocalItemType.Folder)
                    Directory.Delete(localItem.FullPath, true);
                else
                    File.Delete(localItem.FullPath);
                this.GetChildren();
                if (this.LocalEvent != null)
                    if (localItem.Type == LocalItemType.Folder)
                        this.LocalEvent(this, new LocalEventArgs(LocalEventType.DeleteFolderOk, this));
                    else
                        this.LocalEvent(this, new LocalEventArgs(LocalEventType.DeleteFileOk, this));
            }
            finally { }
        }
        public void DeleteAll()
        {
            try
            {
                foreach (LocalItem localItem in this.Items)
                {
                    if (localItem.Type == LocalItemType.Folder)
                        Directory.Delete(localItem.FullPath, true);
                    else
                        File.Delete(localItem.FullPath);
                }
                this.GetChildren();
                if (this.LocalEvent != null)
                    // TODO: special event 
                    this.LocalEvent(this, new LocalEventArgs(LocalEventType.DeleteFileOk, this));
            }
            finally { }
        }
        public void AddFile(string name = null)
        {
            try
            {
                name = (name == null) ? "New_file.txt" : name;
                using (FileStream file = File.Create(Path.Combine(this.FullPath, name))) { }
                    this.GetChildren();
                if (this.LocalEvent != null)
                    // TODO specioal event
                    this.LocalEvent(this, new LocalEventArgs(LocalEventType.MakeDirectoryOk, this));
            }
            finally { }
        }
        public void AddFolder(string name = null)
        {
            try
            {
                name = (name == null) ? "New_folder" : name;
                Directory.CreateDirectory(Path.Combine(this.FullPath, name));
                this.GetChildren();
                if (this.LocalEvent != null)
                    this.LocalEvent(this, new LocalEventArgs(LocalEventType.MakeDirectoryOk, this));
            }
            finally { }
        }
    }
}

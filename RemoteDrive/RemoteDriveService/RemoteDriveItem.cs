using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RemoteDriveService
{
    [DataContract]
    public class RemoteDriveItem
    {
        public enum RemoteDriveTypes { Empty, File, Directory };
        public enum RemoteDriveCompareStates { NotCompared, NameMatches, NameAndSizeMatches };
        [DataMember]
        public string FullPath { get; private set; }
        [DataMember]
        public RemoteDriveTypes Type { get; private set; }
        [DataMember]
        public RemoteDriveCompareStates CompareState { get; private set; }
        [DataMember]
        public byte[] Binary { get; private set; }
        [DataMember]
        private RemoteDriveItem[] RemoteDriveItems { get; set; }
        [DataMember]
        public long Length { get; private set; }
        [DataMember]
        public string Name { get; private set; }
        [DataMember]
        public string Parent { get; private set; }
        [DataMember]
        public DateTime Created { get; private set; }
        [DataMember]
        public DateTime Changed { get; private set; }
        [DataMember]
        public DateTime Accessed { get; private set; }
        [DataMember]
        public bool Loaded { get; private set; }
        private FileSystemInfo SystemInfo { get; set; }
        public bool Localized { get; private set; }
        public RemoteDriveItem(string initialPath)
        {
            this.FullPath = initialPath;
            this.SetType();
            if(this.Exists())
            {
                this.SetName();
                this.SetLength();
                this.SetParent();
                this.SetAccessed();
                this.SetChanged();
                this.SetCreated();
            }
        }
        public RemoteDriveItem Load()
        {
            this.GetChildren();
            this.GetBinary();
            this.Loaded = true;
            return this;
        }
        public RemoteDriveItem Localize(IRemoteDriveResolver pathResolver)
        {
            this.FullPath = pathResolver.Resolve(this.FullPath);
            this.Parent = Path.GetDirectoryName(this.FullPath);
            this.Localized = true;
            return this;
        }
        public RemoteDriveItem EnsureParents(IRemoteDriveResolver pathResolver)
        {
            foreach (string directory in pathResolver.FullNames(this.Parent))
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            return this;
        }
        private FileSystemInfo Info()
        {
            if(this.SystemInfo == null)
            {
                if (this.IsFile())
                    return new FileInfo(this.FullPath);
                else if (this.IsDirectory())
                    return new DirectoryInfo(this.FullPath);
            }
            return this.SystemInfo;
        }
        private void SetName()
        {
           this.Name = this.Info().Name;
        }
        public void SetLength()
        {
            if (this.IsFile())
                this.Length = (this.Info() as FileInfo).Length;
        }
        public void GetBinary()
        {
            if (this.IsFile())
            {
                using (FileStream fileStream = File.Open(this.FullPath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
                {
                    this.Binary = this.StreamToBytes(fileStream);
                }
            }
        }
        protected void SetType()
        {
            if (this.ExistsAsFile())
                this.Type = RemoteDriveTypes.File;
            else if (this.ExistsAsDirectory())
                this.Type = RemoteDriveTypes.Directory;
            else
                this.Type = RemoteDriveTypes.Empty;
        }
        private void GetChildren()
        {
            List<RemoteDriveItem> children = new List<RemoteDriveItem>();
            if (this.IsDirectory())
            {
                DirectoryInfo[] directories = (this.Info() as DirectoryInfo).GetDirectories();
                foreach (DirectoryInfo directory in directories)
                    children.Add(new RemoteDriveItem(Path.Combine(this.FullPath, directory.Name)));
                FileInfo[] files = (this.Info() as DirectoryInfo).GetFiles();
                foreach (FileInfo file in files)
                    children.Add(new RemoteDriveItem(Path.Combine(this.FullPath, file.Name)));
            }
            this.RemoteDriveItems = children.ToArray();
            this.SetChildrenLength();
        }
        private RemoteDriveItem SetChildrenLength()
        {
            foreach (RemoteDriveItem child in this.Children())
                if (child.IsDirectory())
                    child.Length = child.DirSize();
            return this;
        }
        private long DirSize()
        {
            long size = 0;
            DirectoryInfo d = this.Info() as DirectoryInfo;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
                size += fi.Length;
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
                size += DirSize(di);
            return size;
        }
        private long DirSize(DirectoryInfo dir)
        {
            long length = 0;
            FileInfo[] fis = dir.GetFiles();
            foreach (FileInfo fi in fis)
                length += fi.Length;
            DirectoryInfo[] dis = dir.GetDirectories();
            foreach (DirectoryInfo di in dis)
                length += DirSize(di);
            return length;
        }
        public bool ExistsAsFile()
        {
            return File.Exists(this.FullPath);
        }
        public bool ExistsAsDirectory()
        {
            return Directory.Exists(this.FullPath);
        }
        public bool IsFile()
        {
            return this.Type == RemoteDriveTypes.File;
        }
        public bool IsDirectory()
        {
            return this.Type == RemoteDriveTypes.Directory;
        }
        public bool Exists()
        {
            bool existsAsDirectory = this.ExistsAsDirectory();
            if (existsAsDirectory)
                this.Type = RemoteDriveTypes.Directory;
            bool existsAsFile = this.ExistsAsFile();
            if (existsAsFile)
                this.Type = RemoteDriveTypes.File;
            return existsAsDirectory || existsAsFile;
        }
        public bool SystemInfoExists()
        {
            return this.SystemInfo != null;
        }
        public bool HasBinary()
        {
            return this.Binary != null;
        }
        public FileAttributes? Attributes()
        {
            if (this.SystemInfoExists())
                return this.Info().Attributes;
            else return null;
        }
        private void SetCreated()
        {
            if (this.SystemInfoExists())
                this.Created = this.Info().CreationTime;
        }
        private void SetAccessed()
        {
            if (this.SystemInfoExists())
                this.Accessed = this.Info().LastAccessTime;
        }
        private void SetChanged()
        {
            if (this.SystemInfoExists())
                this.Changed = this.Info().LastWriteTime;
        }
        public string Extension()
        {
            if (this.Exists())
                return this.Info().Extension;
            else return String.Empty;
        }
        private void SetParent()
        {
            if (this.IsDirectory())
                this.Parent = (this.Info() as DirectoryInfo).Parent.FullName;
            else if (this.IsFile())
                this.Parent = (this.Info() as FileInfo).Directory.FullName;
            else this.Parent = String.Empty;
        }
        public bool Create()
        {
            if (!this.Exists())
            {
                if(this.IsDirectory())
                    Directory.CreateDirectory(this.FullPath);
                else
                    using (FileStream newFile = File.Create(this.FullPath))
                    {
                        if (this.HasBinary())
                            newFile.Write(this.Binary, 0, this.Binary.Length);
                    }
                return true;
            }
            return false;
        }
        public bool CreateOrUpdate()
        {
            if (!this.Create())
            {
                if (this.IsFile())
                    using (FileStream newFile = File.Open(this.FullPath, FileMode.Truncate, FileAccess.ReadWrite, FileShare.None))
                    {
                        if (this.HasBinary())
                            newFile.Write(this.Binary, 0, this.Binary.Length);
                    }
                return true;
            }
            return true;
        }
        public bool Delete()
        {
            if (this.Exists())
            {
                if (this.IsDirectory())
                    (this.Info() as DirectoryInfo).Delete(true);
                else if (this.IsFile())
                    (this.Info() as FileInfo).Delete();
                return true;
            }
            return false;
        }
        public void MoveTo(string path)
        {
            if (this.Exists())
                if(this.IsFile())
                    (this.Info() as FileInfo).MoveTo(path);
                else
                    (this.Info() as DirectoryInfo).MoveTo(path);
        }
        public IEnumerable<RemoteDriveItem> Children()
        {
            if(this.IsDirectory())
            {
                if (this.RemoteDriveItems == null)
                    this.GetChildren();
                return new List<RemoteDriveItem>(this.RemoteDriveItems); ;
            }
            return new List<RemoteDriveItem>();
        }
        public bool IsSimilarTo(RemoteDriveItem other)
        {
            if(this.Name == other.Name)
            {
                if (this.Length == other.Length)
                    this.CompareState = other.CompareState = RemoteDriveCompareStates.NameAndSizeMatches;
                else
                    this.CompareState = other.CompareState = RemoteDriveCompareStates.NameMatches;
                return true;
            }
            return false;
        }
        public bool NameMatches()
        {
            return this.CompareState == RemoteDriveCompareStates.NameMatches;
        }
        public bool NameAndSizeMatches()
        {
            return this.CompareState == RemoteDriveCompareStates.NameAndSizeMatches;
        }
        public bool NotMatched()
        {
            return this.CompareState == RemoteDriveCompareStates.NotCompared;
        }
        public byte[] StreamToBytes(FileStream fileSteram)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileSteram.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public RemoteDriveItem SetTypeDirectory()
        {
            this.Type = RemoteDriveTypes.Directory;
            return this;
        }
        public RemoteDriveItem SetTypeFile()
        {
            this.Type = RemoteDriveTypes.File;
            return this;
        }
    }
}
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace RemoteDriveService
{
    public interface IRemoteDriveResolver
    {
        string Resolve(string path);
        bool IsHostPath(string path);
        string FilterName(string name);
        IEnumerable<string> FullNames(string path);
    }
    public class RemoteDrivePath : IRemoteDriveResolver
    {

        public const string Repositories = "Repositories";
        public string UserRoot { get; private set; }
        public string Separator { get; private set; }
        public string ApplicationPath { get; private set; }
        public string HostPath { get; private set; }
        public string UserPath { get; private set; }

        public RemoteDrivePath(string userRoot = null)
        {
            this.ApplicationPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
            if (this.ApplicationPath == null)
                this.ApplicationPath = new DirectoryInfo(Directory.GetCurrentDirectory()).FullName;
            this.HostPath = Path.Combine(this.ApplicationPath, RemoteDrivePath.Repositories);
            if (userRoot != null)
                this.SetUserRoot(userRoot);
            this.Separator = Path.DirectorySeparatorChar.ToString();
        }

        public bool HasUserRoot()
        {
            return !String.IsNullOrEmpty(this.UserRoot);
        }

        public void SetUserRoot(string userRoot)
        {
            if (!String.IsNullOrEmpty(userRoot))
            {
                this.UserRoot = userRoot;
                this.UserPath = Path.Combine(this.HostPath, userRoot);
            }
        }

        public string Resolve(string path)
        {
            if (!String.IsNullOrEmpty(this.UserRoot))
            {
                StringBuilder relativePath = new StringBuilder();
                string pathPart = Path.GetFileName(path);
                while (pathPart != this.UserRoot)
                {
                    relativePath.Insert(0, this.Separator + pathPart);
                    path = Path.GetDirectoryName(path);
                    pathPart = Path.GetFileName(path);
                }
                return this.UserPath + relativePath.ToString();
            }
            return String.Empty;
        }
        public IEnumerable<string> FullNames(string path)
        {
            List<string> result = new List<string>();
            if (!String.IsNullOrEmpty(this.UserRoot))
            {
                string pathPart = Path.GetFileName(path);
                while (pathPart != this.UserRoot)
                {
                    result.Add(path);
                    path = Path.GetDirectoryName(path);
                    pathPart = Path.GetFileName(path);
                }
            }
            result.Reverse();
            return result;
        }
        public bool IsHostPath(string path)
        {
            return this.HostPath == path;
        }
        public string FilterName(string name)
        {
            return (name == this.UserRoot) ? "Root" : name ;
        }
    }
}
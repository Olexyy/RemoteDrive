using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace RemoteDrive
{
    public class FtpPath
    {
        private string Host { get; set; }
        private string HostRoot { get; set; }
        private string UserRoot { get; set; }

        public FtpPath(string host, string hostRoot, string userRoot)
        {
            this.Host = host;
            this.HostRoot = hostRoot;
            this.UserRoot = userRoot;
        }

        public string Resolve(string path, bool includeHost = true)
        {
            StringBuilder result = new StringBuilder();
            string pathPart = Path.GetFileName(path);
            while (pathPart != this.UserRoot)
            {
                result.Insert(0, @"/" + pathPart);
                path = Path.GetDirectoryName(path);
                pathPart = Path.GetFileName(path);
            }
            if (includeHost)
                return this.Host + @"/" + this.HostRoot + @"/" + this.UserRoot + result.ToString();
            return @"/" + this.HostRoot + @"/" + this.UserRoot + result.ToString();
        }

    }
}

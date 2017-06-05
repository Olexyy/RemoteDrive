using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RemoteDriveService
{
    public class Service : IService
    {
        private User User { get; set; }
        private RemoteDrivePath PathResolver { get; set; }

        private bool LoggedIn()
        {
            return this.User != null;
        }

        public bool CreateUser(string mail, string pass)
        {
            User user = new User(mail, pass);
            Db db = new Db();
            db.Users.Add(user);
            if(db.SaveChanges() > 0)
                return true;
            return false;
        }

        public User Login(string mail, string pass)
        {
            string hash = Hashing.GetHashString(pass);
            this.User = new Db().Users.Where(i => i.Email == mail && i.Pass == hash).FirstOrDefault();
            if(this.LoggedIn())
            {
                this.PathResolver = new RemoteDrivePath(this.User.Root);
                if (!Directory.Exists(this.PathResolver.HostPath))
                    Directory.CreateDirectory(this.PathResolver.HostPath);
                if (!Directory.Exists(this.PathResolver.UserPath))
                    Directory.CreateDirectory(this.PathResolver.UserPath);
            }
            return this.User;
        }

        public bool CreateItem(RemoteDriveItem item)
        {
            if (this.LoggedIn())
                return item.Localize(this.PathResolver).EnsureParents(this.PathResolver).CreateOrUpdate();
            return false;
        }

        public bool DeleteItem(RemoteDriveItem item)
        {
            if (this.LoggedIn())
                return item.Localize(this.PathResolver).Delete();
            return false;
        }

        public RemoteDriveItem ReadItem(string path)
        {
            if (this.LoggedIn())
            {
                string resolved = this.PathResolver.Resolve(path);
                if(!this.PathResolver.IsHostPath(resolved))
                    return new RemoteDriveItem(resolved).Load();
            }        
            return null;
        }

    }
}
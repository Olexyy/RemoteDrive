using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RemoteDriveService
{
    public class Service : IService
    {
        public bool CreateUser(string mail, string pass, string root)
        {
            User user = new User(mail, pass, root);
            Db db = new Db();
            db.Users.Add(user);
            if(db.SaveChanges() > 0)
                return true;
            return false;
        }
        public User Login(string mail, string pass)
        {
            string hash = Hashing.GetHashString(pass);
            return new Db().Users.Where(i => i.Email == mail && i.Pass == hash).FirstOrDefault();
        }
    }
}
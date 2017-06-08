using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.IO;

namespace RemoteDriveService
{
    [Serializable]
    [DataContract]
    public class User
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Pass { get; set; }
        [Index("Email", IsUnique = true)]
        [MaxLength(50)]
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Root { get; set; }
        public User() { }
        public User(string email, string pass)
        {
            this.Email = email;
            this.Pass = Hashing.GetHashString(pass);
            this.Root = Hashing.GetHashString(Email);
        }
    }

    public class Db : DbContext
    {
        public Db() : base("name=RemoteDrive") { } // Test RemoteDrive
        public DbSet<User> Users { get; set; }
    }
}

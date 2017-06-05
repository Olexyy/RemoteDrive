using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RemoteDriveService
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        bool CreateUser(string mail, string pass);

        [OperationContract]
        User Login(string mail, string pass);

        [OperationContract]
        bool CreateItem(RemoteDriveItem item);

        [OperationContract]
        bool DeleteItem(RemoteDriveItem item);

        [OperationContract]
        RemoteDriveItem ReadItem(string path);

    }
}

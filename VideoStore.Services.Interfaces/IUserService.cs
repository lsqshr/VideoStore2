using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;
using System.ServiceModel;

namespace VideoStore.Services.Interfaces
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        void CreateUser(User pUser);

        [OperationContract]
        User ReadUserById(int pUserId);

        [OperationContract]
        void UpdateUser(User pUser);

        [OperationContract]
        void DeleteUser(User pUser);

        [OperationContract]
        bool ValidateUserLoginCredentials(string username, string password);

        [OperationContract]
        User GetUserByUserNamePassword(string username, string password);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IUserProvider
    {
        void CreateUser(User pUser);

        User ReadUserById(int pUserId);

        void UpdateUser(User pUser);

        void DeleteUser(User pUser);

        bool ValidateUserCredentials(string username, string password);

        Business.Entities.User GetUserByUserNamePassword(string username, string password);
    }
}

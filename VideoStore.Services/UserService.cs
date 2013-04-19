using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Services.Interfaces;
using VideoStore.Business.Components.Interfaces;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;

namespace VideoStore.Services
{
    public class UserService : IUserService
    {
        private IUserProvider UserProvider
        {
            get
            {
                return ServiceFactory.GetService<IUserProvider>();
            }
        }

        public void CreateUser(Business.Entities.User pUser)
        {
            UserProvider.CreateUser(pUser);
        }


        public Business.Entities.User ReadUserById(int pUserId)
        {
            return UserProvider.ReadUserById(pUserId);
        }


        public void UpdateUser(Business.Entities.User pUser)
        {
            UserProvider.UpdateUser(pUser);
        }

        public void DeleteUser(Business.Entities.User pUser)
        {
            UserProvider.DeleteUser(pUser);
        }


        public bool ValidateUserLoginCredentials(string username, string password)
        {
            return UserProvider.ValidateUserCredentials(username, password);
        }


        public Business.Entities.User GetUserByUserNamePassword(string username, string password)
        {
            return UserProvider.GetUserByUserNamePassword(username, password);
        }
    }
}

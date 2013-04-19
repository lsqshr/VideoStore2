using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Services.Interfaces;
using VideoStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using VideoStore.Business.Entities;

namespace VideoStore.Services
{
    public class RoleService : IRoleService
    {

        private IRoleProvider RoleProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IRoleProvider>();
            }
        }

        public List<Role> GetRolesForUser(Business.Entities.User pUser)
        {
            return RoleProvider.GetRolesForUser(pUser);
        }


        public List<Role> GetRolesForUserName(string pUserName)
        {
            return RoleProvider.GetRolesForUserName(pUserName);
        }
    }
}

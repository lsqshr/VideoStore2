using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using VideoStore.WebClient.RoleService;
using VideoStore.Business.Entities;

namespace VideoStore.WebClient.CustomRole
{
    public class CustomRoleProvider : RoleProvider
    {

        private RoleServiceClient RoleService
        {
            get
            {
                return new RoleServiceClient();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {

            List<Role> lRoles = RoleService.GetRolesForUserName(username);
            string[] lStringRoles = (from lRole in lRoles select lRole.Name).ToArray();
            return lStringRoles;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoStore.Business.Entities;
using VideoStore.WebClient.UserService;
using VideoStore.WebClient.ViewModels;

namespace VideoStore.WebClient
{
    public class LoggedInUserBinder : IModelBinder
    {
        private const String UserSessionKey = "_loggedInUser";
        public const String UserIdKey = "UserId";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");

            UserCache lUserCache = (UserCache)controllerContext.HttpContext.Session[UserSessionKey];
            if (lUserCache == null)
            {
                User lUser = controllerContext.HttpContext.Session[UserIdKey] as User;
                controllerContext.HttpContext.Session[UserSessionKey] = lUserCache = new UserCache( lUser);
            }
            return lUserCache;
        }

    }
    public class UserCache
    {
        public User Model
        {
            get;
            set;
        }

        public UserCache(User pModel)
        {
            Model = pModel;
        }

        public void UpdateUserCache()
        {
            Model = new UserServiceClient().ReadUserById(Model.Id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;
using System.Transactions;
using System.ComponentModel.Composition;

namespace VideoStore.Business.Components
{
    public class UserProvider : IUserProvider
    {
        public void CreateUser(VideoStore.Business.Entities.User pUser)
        {
            using(TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                lContainer.Users.AddObject(pUser);
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }


        public User ReadUserById(int pUserId)
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                User lCustomer = lContainer.Users.Where((pUser) => pUser.Id == pUserId).FirstOrDefault();
                return lCustomer;
            }
        }


        public void UpdateUser(User pUser)
        {
            using(TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                lContainer.Users.Attach(pUser);
                lContainer.ObjectStateManager.ChangeObjectState(pUser, System.Data.EntityState.Modified);
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }


        public void DeleteUser(User pUser)
        {
            using(TransactionScope lScope = new TransactionScope())
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                lContainer.Users.DeleteObject(pUser);
                lContainer.SaveChanges();
            }
        }


        public bool ValidateUserCredentials(string username, string password)
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                string lHashedPassword = Common.Cryptography.sha512encrypt(password);
                var lCredentials = from lCredential in lContainer.LoginCredentials
                            where lCredential.UserName == username && lCredential.EncryptedPassword == lHashedPassword
                            select lCredential;
                return lCredentials.Count() > 0;
            }
        }


        public User GetUserByUserNamePassword(string username, string password)
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                string lHashedPassword = Common.Cryptography.sha512encrypt(password);
                var lCredentials = from lCredential in lContainer.LoginCredentials
                            where lCredential.UserName == username && lCredential.EncryptedPassword == lHashedPassword
                            select lCredential;

                if (lCredentials.Count() > 0)
                {
                    LoginCredential lCredential = lCredentials.First();
                    var lUsers = from lCustomer in lContainer.Users
                                 where lCustomer.LoginCredential.Id == lCredential.Id
                                 select lCustomer;
                    if (lUsers.Count() > 0)
                    {
                        return lUsers.First();
                    }
                }
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoStore.Business.Entities;
using VideoStore.WebClient.UserService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VideoStore.WebClient.ViewModels
{

    public class UserViewModel
    {
        private UserServiceClient UserService
        {
            get
            {
                return new UserServiceClient();
            }
        }


        public UserViewModel(User pUser)
        {
            Address = pUser.Address;
            Email = pUser.Email;
        }

        public UserViewModel()
        {
        }


        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Postal Address")]
        public String Address
        {
            get;
            set;
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email address")]
        public String Email
        {
            get;
            set;
        }

        public void UpdateUser(User pUser)
        {
            pUser.Address = this.Address;
            pUser.Email = this.Email;
            UserService.UpdateUser(pUser);
        }
    }
}
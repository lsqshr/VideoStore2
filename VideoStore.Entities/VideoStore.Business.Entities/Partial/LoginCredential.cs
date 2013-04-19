using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VideoStore.Business.Entities
{
    public partial class LoginCredential
    {

        public LoginCredential()
        {
        }

        [DataMember]
        public String Password
        {
            get
            {
                return EncryptedPassword;
            }

            set
            {
                String lSetValue = Common.Cryptography.sha512encrypt(value);
                if (!this.IsDeserializing && this.EncryptedPassword != lSetValue)
                {
                    this.EncryptedPassword = lSetValue;
                }
            }
        }

    }
}

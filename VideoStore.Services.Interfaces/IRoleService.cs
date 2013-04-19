using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;
using System.ServiceModel;

namespace VideoStore.Services.Interfaces
{
    [ServiceContract]
    public interface IRoleService
    {
        [OperationContract]
        List<Role> GetRolesForUser(User pUser);

        [OperationContract]
        List<Role> GetRolesForUserName(String pUserName);
    }
}

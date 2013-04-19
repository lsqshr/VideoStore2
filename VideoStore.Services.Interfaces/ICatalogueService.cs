using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using VideoStore.Business.Entities;

namespace VideoStore.Services.Interfaces
{
    [ServiceContract]
    public interface ICatalogueService
    {
        [OperationContract]
        List<Media> GetMediaItems(int pOffset, int pCount);

        [OperationContract]
        Media GetMediaById(int pId);
    }
}

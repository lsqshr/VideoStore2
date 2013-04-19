using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components.Interfaces
{
    public interface ICatalogueProvider
    {
        List<Business.Entities.Media> GetMediaItems(int pOffset, int pCount);
        Media GetMediaById(int pId);
    }
}

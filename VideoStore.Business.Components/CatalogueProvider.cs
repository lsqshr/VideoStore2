using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components
{
    public class CatalogueProvider : ICatalogueProvider
    {
        public List<Entities.Media> GetMediaItems(int pOffset, int pCount)
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                return (from MediaItem in lContainer.Media.Include(Common.ReflectionUtil.GetPropertyName(() => new Media().Stocks))
                       orderby MediaItem.Id
                       select MediaItem).Skip(pOffset).Take(pCount).ToList();
            }
        }


        public Media GetMediaById(int pId)
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                return (from MediaItem in lContainer.Media.Include(Common.ReflectionUtil.GetPropertyName(() => new Media().Stocks))
                        where MediaItem.Id == pId
                        select MediaItem).FirstOrDefault();
            }
        }
    }
}

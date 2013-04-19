using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoStore.Business.Entities;

namespace VideoStore.WebClient.ViewModels
{
    public class CatalogueViewModel
    {

        private CatalogueService.CatalogueServiceClient CatalogueService
        {
            get
            {
                return new CatalogueService.CatalogueServiceClient();
            }
        }

        public CatalogueViewModel()
        {

        }



        public List<Media> MediaListPage
        {
            get
            {
                return CatalogueService.GetMediaItems(0, Int32.MaxValue);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoStore.WebClient.Models;
using VideoStore.Business.Entities;
using VideoStore.WebClient.CatalogueService;
using VideoStore.WebClient.ViewModels;

namespace VideoStore.WebClient.Controllers
{
    public class CartController : Controller
    {

        public ViewResult Index(Cart pCart, string pReturnUrl)
        {
            ViewData["returnUrl"] = pReturnUrl;
            ViewData["CurrentCategory"] = "Cart";
            return View(pCart);
        }

        public RedirectToRouteResult AddToCart(Cart pCart, int pMediaId, string pReturnUrl)
        {
            pCart.AddItem(FetchMediaById(pMediaId), 1);
            return RedirectToAction("Index", new { pReturnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(Cart pCart, int pMediaId, string pReturnUrl)
        {
            pCart.RemoveLine(FetchMediaById(pMediaId));
            return RedirectToAction("Index", new { pReturnUrl });
        }

        public ActionResult CheckOut(Cart pCart, UserCache pUser)
        {
            pCart.SubmitOrderAndClearCart(pUser);
            return View(pUser.Model);
        }

        public ViewResult Summary(Cart pCart)
        {
            return View(pCart);
        }

        private Media FetchMediaById(int pId)
        {
            CatalogueServiceClient lCatClient = new CatalogueServiceClient();
            return lCatClient.GetMediaById(pId);
        }
    }
}

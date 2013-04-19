using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VideoStore.WebClient.Controllers
{
    public class AdministrationController : Controller
    {
        //
        // GET: /Administration/
        [Authorize(Roles = "Operator")]
        public ActionResult Index()
        {
            return View();
        }

    }
}

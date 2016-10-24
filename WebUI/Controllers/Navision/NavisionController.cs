using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.ServiceGateway;

namespace WebUI.Controllers.Navision
{
    public class NavisionController : Controller
    {
        NavisionGateway _gateway = new NavisionGateway();

        // GET: Navision
        public ActionResult Index()
        {
            return View(_gateway.GetContacts());
        }
    }
}
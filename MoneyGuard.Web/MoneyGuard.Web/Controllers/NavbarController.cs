using MoneyGuard.Web.Domain;
using MoneyGuard.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyGuard.Web.Controllers
{
    public class NavbarController : Controller
    {
        // GET: Navbar
        public ActionResult Index()
        {
            Session["funcionLogin"] = "Log In";
            var data = new Data();
            return PartialView("_Navbar", data.navbarItems().ToList());
        }
    }
}
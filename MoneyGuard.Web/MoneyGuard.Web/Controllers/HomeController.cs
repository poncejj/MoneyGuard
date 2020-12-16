using MoneyGuard.Web.Models;
using MoneyGuard.Web.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyGuard.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["login"] != null)
                return View();
            else
                return View("Login");
        }

        

        public ActionResult Login()
        {
            return View("Login");
        }

        public ActionResult Error()
        {
            return View("Error");
        }
        public ActionResult GetData()
        {
            List<clsDatosGrafico> lstDatosGrafico = new List<clsDatosGrafico>();
            clsServicioApp objServicio = new Servicio.clsServicioApp();
            var Requests = objServicio.ConsultarTransacciones();
            
            int CountPerDay = 0;
            // count of request per day

            for(int i = 0; i<4;i++)
            {
                DateTime day = DateTime.Today.AddDays(-i);
                CountPerDay = Requests.FindAll(x => x.fechaMovimiento == day.ToShortDateString()).Count();
                lstDatosGrafico.Add(new clsDatosGrafico(day.ToString("yyyy-MM-dd"), CountPerDay));
            }
            return Json(lstDatosGrafico, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EjecutarLogin()
        {
            if(Session["login"] == null)
                return View("Login");
            else
            {
                Session["login"] = null;
                return View("Login");
            }
        }

        public ActionResult IniciarSesion(string email, string password)
        {
            clsServicioApp objServicioApp = new Servicio.clsServicioApp();
            try
            {
                if(objServicioApp.ValidarUsuario(email,password))
                {
                    Session["funcionLogin"] = "Log Out";
                    Session["login"] = "Bienvenido";
                    return View("Index");
                }
                else
                {
                    Session["funcionLogin"] = "Log In";
                }
            }
            catch(Exception)
            {
                return View("Error","Home");
            }
            return View("Login");
        }
    }
}
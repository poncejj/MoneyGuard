using MoneyGuard.Web.Models;
using MoneyGuard.Web.Servicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyGuard.Web.Controllers
{
    public class AplicacionController : Controller
    {
        List<clsUsuarioApp> lstUsuarioApp = new List<clsUsuarioApp>();
        List<clsCooperativaUsuario> lstCooperativaUsuario = new List<clsCooperativaUsuario>();
        List<clsCooperativa> lstCooperativa = new List<clsCooperativa>();
        List<clsCatalogo> lstCatalogo = new List<clsCatalogo>();
        List<clsTransaccion> lstTransacciones = new List<clsTransaccion>();
        List<clsAlerta> lstAlertas = new List<clsAlerta>();

        // GET: Aplicacion
        public ActionResult Index()
        {
            if (Session["login"] != null)
                return View();
            else
                return RedirectToAction("Login", "Home");
        }

        #region

        public ActionResult UsuarioApp()
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstUsuarioApp = objServicioApp.ConsultarTodosUsuarios();
                    Session["usuariosRegistrados"] = lstUsuarioApp;
                    return View("UsuarioApp", lstUsuarioApp);
                }
                else
                    return RedirectToAction("Login", "Home");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            
        }

        public ActionResult ModificarUsuarioApp(string idUsuario)
        {
            try
            {
                if (Session["login"] != null)
                {
                    lstUsuarioApp = (List<clsUsuarioApp>)Session["usuariosRegistrados"];
                    clsUsuarioApp objUsuarioApp = lstUsuarioApp.Find(x => x.idUsuarioApp == idUsuario);
                    return View(objUsuarioApp);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EjecutarModificarUsuarioApp(string idUsuario, string nombreUsuario, string identificacionUsuario, string pinUsuario, string eliminadoUsuario)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            bool blUsuarioEliminado = false;
            try
            {
                if (eliminadoUsuario == "on")
                    blUsuarioEliminado = true;
                else
                    blUsuarioEliminado = false;

                if (objServicioApp.ModificarUsuarioApp(idUsuario, identificacionUsuario, nombreUsuario, pinUsuario, blUsuarioEliminado))
                {
                    return RedirectToAction("UsuarioApp", "Aplicacion");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EliminarUsuarioApp(string idUsuarioEliminar)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    if (objServicioApp.EliminarUsuarioApp(idUsuarioEliminar))
                    {
                        return RedirectToAction("UsuarioApp", "Aplicacion");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }  

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
#endregion 

        #region
        public ActionResult CooperativaUsuarioApp()
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstCooperativaUsuario = objServicioApp.ConsultarCooperativaUsuario();
                    Session["cooperativausuariosRegistrados"] = lstUsuarioApp;
                    return View("CooperativaUsuarioApp", lstCooperativaUsuario);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                    
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult ModificarCooperativaUsuario(string idCooperativaUsuario)
        {
            clsCooperativaUsuarioContenedor objCooperativaUsuarioContenedor = new Models.clsCooperativaUsuarioContenedor();
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstCooperativaUsuario = (List<clsCooperativaUsuario>)Session["cooperativausuariosRegistrados"];
                    clsCooperativaUsuario objCooperativaUsuario = lstCooperativaUsuario.Find(x => x.idCooperativaUsuario == idCooperativaUsuario);
                    List<clsCooperativa> objCooperativa = objServicioApp.ConsultarCooperativas();
                    List<clsUsuarioApp> objUsuarioApp = objServicioApp.ConsultarTodosUsuarios();

                    objCooperativaUsuarioContenedor.objCooperativaUsuario = objCooperativaUsuario;
                    objCooperativaUsuarioContenedor.objCooperativa = objCooperativa;
                    objCooperativaUsuarioContenedor.objUsuario = objUsuarioApp;

                    return View(objCooperativaUsuarioContenedor);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EjecutarModificarCooperativaUsuario(string idCooperativaUsuario, string idUsuario, string idCooperativa, string eliminadoUsuario)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            bool blUsuarioEliminado = false;
            try
            {
                if (eliminadoUsuario == "on")
                    blUsuarioEliminado = true;
                else
                    blUsuarioEliminado = false;

                if (objServicioApp.ModificarCooperativaUsuarioApp(idCooperativaUsuario, idUsuario, idCooperativa, blUsuarioEliminado))
                {
                    return RedirectToAction("CooperativaUsuarioApp", "Aplicacion");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EliminarCooperativaUsuario(string idCooperativaUsuario)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    if (objServicioApp.EliminarCooperativaUsuarioApp(idCooperativaUsuario))
                    {
                        return RedirectToAction("CooperativaUsuarioApp", "Aplicacion");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region
        public ActionResult Cooperativa()
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstCooperativa = objServicioApp.ConsultarCooperativas();
                    Session["cooperativaRegistradas"] = lstCooperativa;
                    return View("Cooperativa", lstCooperativa);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult ModificarCooperativa(string idCooperativa)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstCooperativa = (List<clsCooperativa>)Session["cooperativaRegistradas"];
                    clsCooperativa objCooperativa = lstCooperativa.Find(x => x.idCooperativa == idCooperativa);

                    if (objCooperativa != null)
                    {
                        return View(objCooperativa);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                    

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EjecutarModificarCooperativa(string idCooperativa,
            string nombreCooperativa, string nombreColaEnvio, string nombreColaRespuesta,
            string nombreBusServicio, string eliminadoCooperativa)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            bool blCooperativaEliminado = false;
            try
            {
                    if (eliminadoCooperativa == "on")
                    blCooperativaEliminado = true;
                else
                    blCooperativaEliminado = false;

                if (objServicioApp.ModificarCooperativa(idCooperativa, nombreCooperativa,
                    nombreColaEnvio, nombreColaRespuesta, nombreBusServicio, blCooperativaEliminado))
                {
                    return RedirectToAction("Cooperativa", "Aplicacion");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EliminarCooperativa(string idCooperativaEliminar)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    if (objServicioApp.EliminarCooperativa(idCooperativaEliminar))
                    {
                        return RedirectToAction("Cooperativa", "Aplicacion");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EjecutarAgregarCooperativa(string nombreCooperativa, string nombreColaRespuesta, string nombreColaEnvio, string nombreBusServicio, string eliminadoCooperativa)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            bool blCooperativaEliminado = false;
            try
            {
                if (eliminadoCooperativa == "on")
                    blCooperativaEliminado = true;
                else
                    blCooperativaEliminado = false;


                if (objServicioApp.AgregarCooperativa(nombreCooperativa, nombreColaEnvio, nombreColaRespuesta, nombreBusServicio, blCooperativaEliminado))
                {
                    return RedirectToAction("Cooperativa", "Aplicacion");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult AgregarCooperativa()
        {
            clsCooperativa objCooperativa = new clsCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    return View(objCooperativa);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region
        public ActionResult Catalogo()
        {

            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstCatalogo = objServicioApp.ConsultarCatalogo();
                    Session["catalogosRegistrados"] = lstCatalogo;
                    return View("Catalogo", lstCatalogo);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult ModificarCatalogo(string idCatalogo)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstCatalogo = (List<clsCatalogo>)Session["catalogosRegistrados"];
                    clsCatalogo objCatalogo = lstCatalogo.Find(x => x.idCatalogo == idCatalogo);
                    return View(objCatalogo);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EjecutarModificarCatalogo(string idCatalogo,
            string nombreCatalogo, string catalogoPadre, string idCatalogoPadre,
            string eliminadoCatalogo)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            bool blCatalogoEliminado = false;
            bool blCatalogoPadre = false;
            try
            {
                if (eliminadoCatalogo == "on")
                    blCatalogoEliminado = true;
                else
                    blCatalogoEliminado = false;
                if (catalogoPadre == "on")
                    blCatalogoPadre = true;
                else
                    blCatalogoPadre = false;

                if (objServicioApp.ModificarCatalogo(idCatalogo, nombreCatalogo,
                    blCatalogoPadre, idCatalogoPadre, blCatalogoEliminado))
                {
                    return RedirectToAction("Catalogo", "Aplicacion");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EliminarCatalogo(string idCatalogoEliminado)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (objServicioApp.EliminarCatalogo(idCatalogoEliminado))
                {
                    if (Session["login"] != null)
                    {
                        return RedirectToAction("Catalogo", "Aplicacion");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EjecutarAgregarCatalogo(string nombreCatalogo,
            string blCatalogoPadre, string idCatalogoPadre,
            string eliminadoCatalogo)
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            bool blCatalogoEliminado = false;
            bool blCatalogoPadreTemp = false;
            try
            {
                if (eliminadoCatalogo == "on")
                    blCatalogoEliminado = true;
                else
                    blCatalogoEliminado = false;
                if (blCatalogoPadre == "on")
                    blCatalogoPadreTemp = true;
                else
                    blCatalogoPadreTemp = false;
                if(blCatalogoPadreTemp == true && idCatalogoPadre.Length == 0)
                {
                    idCatalogoPadre = "0";
                }

                if (objServicioApp.AgregarCatalogo(nombreCatalogo, blCatalogoPadreTemp, idCatalogoPadre, blCatalogoEliminado))
                {
                    return RedirectToAction("Catalogo", "Aplicacion");
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult AgregarCatalogo()
        {
            clsCatalogo objCat = new clsCatalogo();
            List<clsCatalogo> lstCatalogos = new List<clsCatalogo>();
            List<SelectListItem> items = new List<SelectListItem>();

            try 
            {
                if (Session["login"] != null)
                {
                    lstCatalogos = (List<clsCatalogo>)Session["catalogosRegistrados"];
                    lstCatalogos = lstCatalogos.FindAll(x => x.catalogoPadre == true);
                    foreach (clsCatalogo objCatalogo in lstCatalogos)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objCatalogo.nombreCatalogo,
                            Value = objCatalogo.idCatalogo
                        });
                    }
                    ViewBag.lstCatalogoPadre = items;
                    return View(objCat);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
               
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        #endregion

        #region
        public ActionResult Transacciones()
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstTransacciones = objServicioApp.ConsultarTransacciones();
                    return View("Transacciones", lstTransacciones);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        #endregion

        #region
        public ActionResult Alertas()
        {
            clsServicioApp objServicioApp = new clsServicioApp();
            try
            {
                if (Session["login"] != null)
                {
                    lstAlertas = objServicioApp.ConsultarAlertas();
                    return View("Alertas", lstAlertas);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
               
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

        }
        #endregion

    }
}
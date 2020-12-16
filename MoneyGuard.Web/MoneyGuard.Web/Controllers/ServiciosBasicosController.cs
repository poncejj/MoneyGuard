using MoneyGuard.Web.Models;
using MoneyGuard.Web.Servicio;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MoneyGuard.Web.Controllers
{
    public class ServiciosBasicosController : Controller
    {
        List<clsSuministro> lstSuministro = new List<clsSuministro>();
        List<clsClienteServicios> lstCliente = new List<clsClienteServicios>();
        List<clsFactura> lstFactura = new List<clsFactura>();
        List<clsDetalleFactura> lstDetalleFactura = new List<clsDetalleFactura>();
        // GET: ServiciosBasicios
        public ActionResult Index()
        {
            return View();
        }

        #region clientes
        public ActionResult Cliente()
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    lstCliente = objServicioOtros.ConsultarCliente();
                    Session["lista"] = lstCliente;
                    return View("Cliente", lstCliente);
                }
                else
                {
                    return RedirectToAction("Login","Home");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult EjecutarModificarCliente(string strIdCliente,
                    string strIdentificacionCliente, string strNombreCliente,
                    string strApellidoCliente)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if (objServicioOtros.ModificarCliente(int.Parse(strIdCliente),
                    strIdentificacionCliente, strNombreCliente, strApellidoCliente))
                {
                    return RedirectToAction("Cliente");
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

        public ActionResult EjecutarAgregarCliente(string strIdentificacionCliente,
            string strNombreCliente, string strApellidoCliente)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();

            try
            {
                if (objServicioOtros.AgregarCliente(strIdentificacionCliente, strNombreCliente,
                    strApellidoCliente))
                {
                    return RedirectToAction("Cliente");
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

        public ActionResult AgregarCliente()
        {
            clsClienteServicios objCliente = new clsClienteServicios();

            try
            {
                if(Session["login"] != null)
                {
                    return View(objCliente);
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

        public ActionResult ModificarCliente(string idCliente)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    lstCliente = (List<clsClienteServicios>)Session["lista"];
                    clsClienteServicios objCliente = lstCliente.Find(x => x.idCliente == int.Parse(idCliente));

                    if (objCliente != null)
                    {
                        return View(objCliente);
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

        public ActionResult EliminarCliente(string idClienteEliminado)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    if (objServicioOtros.EliminarCliente(int.Parse(idClienteEliminado)))
                    {
                        return RedirectToAction("Cliente");
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

        #region Suministros
        public ActionResult Suministro()
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    lstSuministro = objServicioOtros.ConsultarSuministro();
                    Session["lista"] = lstSuministro;
                    return View("Suministro", lstSuministro);
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

        public ActionResult EjecutarModificarSuministro(string idSuministro, 
            string idServicioBasico, string strNumeroSuministro, string idCliente, 
            string idDireccion, string strDescripcionDireccion, string idParroquia)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if (objServicioOtros.ModificarSuministro(int.Parse(idSuministro),
                    int.Parse(idServicioBasico), strNumeroSuministro, int.Parse(idCliente), 
                    int.Parse(idDireccion), strDescripcionDireccion, int.Parse(idParroquia)))
                {
                    return RedirectToAction("Suministro");
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

        public ActionResult EjecutarAgregarSuministro(string idServicioBasico, 
            string strNumeroSuministro, string idCliente, string strDescripcionDireccion, 
            string idParroquia)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();

            try
            {
                if (objServicioOtros.AgregarSuministro(int.Parse(idServicioBasico), strNumeroSuministro, int.Parse(idCliente),
                    strDescripcionDireccion, int.Parse(idParroquia)))
                {
                    return RedirectToAction("Suministro");
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

        public ActionResult AgregarSuministro()
        {
            clsSuministro objSuministro = new clsSuministro();
            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items1 = new List<SelectListItem>();
            List<SelectListItem> items2 = new List<SelectListItem>();
            List<clsGenerico> lstGenerico = new List<clsGenerico>();
            clsServicioOtro objServicioOtro = new clsServicioOtro();

            try
            {
                if(Session["login"] != null)
                {
                    lstCliente = objServicioOtro.ConsultarCliente();
                    foreach (clsClienteServicios objCliente in lstCliente)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objCliente.nombreCliente + " " + objCliente.apellidoCliente,
                            Value = objCliente.idCliente.ToString()
                        });
                    }
                    lstGenerico = objServicioOtro.ConsultarParroquias();
                    foreach (clsGenerico objParroquia in lstGenerico)
                    {
                        items1.Add(new SelectListItem
                        {
                            Text = objParroquia.strDescripcion,
                            Value = objParroquia.idGenerico.ToString()
                        });
                    }

                    lstGenerico = objServicioOtro.ConsultarTipoServicios();
                    foreach (clsGenerico objTipoServicio in lstGenerico)
                    {
                        items2.Add(new SelectListItem
                        {
                            Text = objTipoServicio.strDescripcion,
                            Value = objTipoServicio.idGenerico.ToString()
                        });
                    }

                    ViewBag.lstClientes = items;
                    ViewBag.lstParroquia = items1;
                    ViewBag.lstTipoServicios = items2;

                    return View(objSuministro);
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

        public ActionResult ModificarSuministro(string idSuministro)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items1 = new List<SelectListItem>();
            List<SelectListItem> items2 = new List<SelectListItem>();
            List<clsGenerico> lstGenerico = new List<clsGenerico>();
            clsServicioOtro objServicioOtro = new clsServicioOtro();

            try
            {
                if(Session["login"] != null)
                {
                    lstCliente = objServicioOtro.ConsultarCliente();
                    foreach (clsClienteServicios objCliente in lstCliente)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objCliente.nombreCliente + " " + objCliente.apellidoCliente,
                            Value = objCliente.idCliente.ToString()
                        });
                    }
                    lstGenerico = objServicioOtro.ConsultarParroquias();
                    foreach (clsGenerico objParroquia in lstGenerico)
                    {
                        items1.Add(new SelectListItem
                        {
                            Text = objParroquia.strDescripcion,
                            Value = objParroquia.idGenerico.ToString()
                        });
                    }

                    lstGenerico = objServicioOtro.ConsultarTipoServicios();
                    foreach (clsGenerico objTipoServicio in lstGenerico)
                    {
                        items2.Add(new SelectListItem
                        {
                            Text = objTipoServicio.strDescripcion,
                            Value = objTipoServicio.idGenerico.ToString()
                        });
                    }

                    lstSuministro = (List<clsSuministro>)Session["lista"];
                    clsSuministro objSuministro = lstSuministro.Find(x => x.idSuministro == int.Parse(idSuministro));

                    if (objSuministro != null)
                    {
                        ViewBag.lstClientes = items;
                        ViewBag.lstParroquia = items1;
                        ViewBag.lstTipoServicios = items2;

                        return View(objSuministro);
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

        public ActionResult EliminarSuministro(string idSuministroEliminado)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    if (objServicioOtros.EliminarSuministro(int.Parse(idSuministroEliminado)))
                    {
                        return RedirectToAction("Suministro");
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

        #region Facturas
        public ActionResult Factura()
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    lstFactura = objServicioOtros.ConsultarFactura();
                    Session["lista"] = lstFactura;
                    return View("Factura", lstFactura);
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

        public ActionResult EjecutarModificarFactura(string idFactura, string idSuministro,
            string dblValorFactura, string strFechaEmision, string strFechaVencimiento,
            string dblValorPendiente, string blValorPagado)
        {

            clsServicioOtro objServicioOtros = new clsServicioOtro();
            bool valorPagado = false;
            try
            {

                if (blValorPagado == "on")
                    valorPagado = true;
                else
                    valorPagado = false;

                if (objServicioOtros.ModificarFactura(int.Parse(idSuministro), int.Parse(idFactura),double.Parse(dblValorFactura),
                    strFechaEmision,strFechaVencimiento, double.Parse(dblValorPendiente),valorPagado))
                {
                    return RedirectToAction("Factura");
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

        public ActionResult EjecutarAgregarFactura(string idSuministro,
            string dblValorFactura, string strFechaEmision, string strFechaVencimiento,
            string dblValorPendiente, string blValorPagado)
        {

            clsServicioOtro objServicioOtros = new clsServicioOtro();
            bool valorPagado = false;

            try
            {
                if(Session["login"] != null)
                {
                    if (objServicioOtros.AgregarFactura(int.Parse(idSuministro), double.Parse(dblValorFactura),
                    strFechaEmision, strFechaVencimiento, double.Parse(dblValorPendiente), valorPagado))
                    {
                        return RedirectToAction("Factura");
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

        public ActionResult AgregarFactura()
        {
            clsFactura objFactura = new clsFactura();
            clsServicioOtro objServicioOtro = new clsServicioOtro();
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                if(Session["login"] != null)
                {
                    lstSuministro = objServicioOtro.ConsultarSuministro();
                    foreach (clsSuministro objSuministro in lstSuministro)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objSuministro.numeroSuministro,
                            Value = objSuministro.idSuministro.ToString()
                        });
                    }
                    ViewBag.lstSuministro = items;

                    return View(objFactura);
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

        public ActionResult ModificarFactura(string idFactura)
        {
            clsServicioOtro objServicioOtro = new clsServicioOtro();
            List<SelectListItem> items = new List<SelectListItem>();
            try
            {
                if(Session["login"] != null)
                {

                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
                lstSuministro = objServicioOtro.ConsultarSuministro();
                foreach (clsSuministro objSuministro in lstSuministro)
                {
                    items.Add(new SelectListItem
                    {
                        Text = objSuministro.numeroSuministro,
                        Value = objSuministro.idSuministro.ToString()
                    });
                }
                
                lstFactura = (List<clsFactura>)Session["lista"];
                clsFactura objFactura = lstFactura.Find(x => x.idFactura == int.Parse(idFactura));

                if (objFactura != null)
                {
                    ViewBag.lstSuministro = items;
                    return View(objFactura);
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

        public ActionResult EliminarFactura(string idFacturaEliminado)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    if (objServicioOtros.EliminarFactura(int.Parse(idFacturaEliminado)))
                    {
                        return RedirectToAction("Factura");
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

        #region Facturas
        public ActionResult DetalleFactura(string idFactura)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    lstDetalleFactura = objServicioOtros.ConsultarDetalleFactura(int.Parse(idFactura));
                    Session["lista"] = lstDetalleFactura;
                    return View("DetalleFactura", lstDetalleFactura);
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

        public ActionResult EjecutarAgregarDetalleFactura(string idFactura,
            string strDescripcionDetalle, string dblValorDetalle)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();

            try
            {
                if (objServicioOtros.AgregarDetalleFactura(int.Parse(idFactura), strDescripcionDetalle, double.Parse(dblValorDetalle)))
                {
                    return RedirectToAction("DetalleFactura");
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

        public ActionResult AgregarDetalleFactura()
        {
            clsDetalleFactura objDetalleFactura = new clsDetalleFactura();

            try
            {
                if(Session["login"] != null)
                {
                    return View(objDetalleFactura);
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

        public ActionResult EliminarDetalleFactura(string idDetalleFacturaEliminado)
        {
            clsServicioOtro objServicioOtros = new clsServicioOtro();
            try
            {
                if(Session["login"] != null)
                {
                    if (objServicioOtros.EliminarDetalleFactura(int.Parse(idDetalleFacturaEliminado)))
                    {
                        return RedirectToAction("DetalleFactura");
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
    }
}
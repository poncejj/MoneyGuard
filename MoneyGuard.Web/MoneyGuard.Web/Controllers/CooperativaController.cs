using MoneyGuard.Web.Models;
using MoneyGuard.Web.Servicio;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MoneyGuard.Web.Controllers
{
    public class CooperativaController : Controller
    {
        List<clsCliente> lstCliente = new List<clsCliente>();
        List<clsCuenta> lstCuenta = new List<clsCuenta>();
        List<clsTarjetaCredito> lstTarjetaCredito = new List<clsTarjetaCredito>();
        List<clsMovimiento> lstMovimiento = new List<clsMovimiento>();
        // GET: Cooperativa
        public ActionResult Index()
        {
            return View();
        }
        #region clientes
        public ActionResult Clientes(string strServicio)
        {
            Session["cooperativa"] = strServicio;
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    lstCliente = objServicioCooperativa.ConsultarClientes(strServicio);
                    Session["clientesRegistrados"] = lstCliente;
                    return View("Clientes", lstCliente);
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

        public ActionResult EjecutarModificarCliente(string strIdCliente,
                    string strIdentificacionCliente, string strNombreCliente, 
                    string strApellidoCliente, string strTelefonoCliente, string strEmailCliente)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                string strServicio = Session["cooperativa"].ToString();

                if (objServicioCooperativa.ModificarCliente(strServicio, int.Parse(strIdCliente),
                    strIdentificacionCliente,strNombreCliente,strApellidoCliente,strTelefonoCliente,strEmailCliente))
                {
                    return RedirectToAction("Clientes",new {strServicio});
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
            string strNombreCliente, string strApellidoCliente, string strTelefonoCliente, 
                    string strEmailCliente)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();

            try
            {
                string strServicio = Session["cooperativa"].ToString();

                if (objServicioCooperativa.AgregarCliente(strServicio,strIdentificacionCliente,strNombreCliente,
                    strApellidoCliente,strTelefonoCliente,strEmailCliente))
                {
                    return RedirectToAction("Clientes", new { strServicio });
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
            clsCliente objCliente = new clsCliente();

            try
            {
                if (Session["login"] != null)
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
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    string strServicio = Session["cooperativa"].ToString();

                    lstCliente = (List<clsCliente>)Session["clientesRegistrados"];
                    clsCliente objCliente = lstCliente.Find(x => x.idCliente == int.Parse(idCliente));

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
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    string strServicio = Session["cooperativa"].ToString();

                    if (objServicioCooperativa.EliminarCliente(strServicio, int.Parse(idClienteEliminado)))
                    {
                        return RedirectToAction("Clientes", new { strServicio });
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

        #region cuentas
        public ActionResult Cuentas(string strServicio)
        {
            Session["cooperativa"] = strServicio;
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    lstCuenta = objServicioCooperativa.ConsultarCuentas(strServicio);
                    Session["cuentasRegistrados"] = lstCuenta;
                    return View("Cuentas", lstCuenta);
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

        public ActionResult EjecutarModificarCuenta(string strIdCuenta,
                    string strIdCliente, string strNumeroCuenta, string strTipoCuenta,
                    string strSaldo, string strEstado)
        {
            bool blEstado = false;
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (strEstado == "on")
                    blEstado = true;
                else
                    blEstado = false;
                string strServicio = Session["cooperativa"].ToString();

                if (objServicioCooperativa.ModificarCuenta(strServicio,int.Parse(strIdCuenta),int.Parse(strIdCliente),
                    strNumeroCuenta,strTipoCuenta,double.Parse(strSaldo),blEstado))
                {
                    return RedirectToAction("Cuentas", new { strServicio });
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

        public ActionResult EjecutarAgregarCuenta(string strIdCliente, string strNumeroCuenta, 
            string strTipoCuenta, string strSaldo, string strEstado)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            bool blEstado = false;
            try
            {
                string strServicio = Session["cooperativa"].ToString();

                if(strEstado == "on")
                {
                    blEstado = true;
                }
                else
                {
                    blEstado = false;
                }

                if (objServicioCooperativa.AgregarCuenta(strServicio, int.Parse(strIdCliente), strNumeroCuenta,
                    strTipoCuenta, double.Parse(strSaldo), blEstado))
                {
                    return RedirectToAction("Cuentas", new { strServicio });
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

        public ActionResult AgregarCuenta()
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            clsCuenta objCuenta = new clsCuenta();
            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items1 = new List<SelectListItem>();
            string strServicio = Session["cooperativa"].ToString();
            try
            {
                if (Session["login"] != null)
                {
                    lstCliente = objServicioCooperativa.ConsultarClientes(strServicio);
                    foreach (clsCliente objCliente in lstCliente)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objCliente.nombreCliente + " " + objCliente.apellidoCliente,
                            Value = objCliente.idCliente.ToString()
                        });
                    }

                    items1.Add(new SelectListItem
                    {
                        Text = "Ahorros",
                        Value = "A"
                    });
                    items1.Add(new SelectListItem
                    {
                        Text = "Corriente",
                        Value = "C"
                    });
                    ViewBag.lstClientes = items;
                    ViewBag.lstTipoCuenta = items1;

                    return View(objCuenta);
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

        public ActionResult ModificarCuenta(string idCuenta)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items1 = new List<SelectListItem>();

            try
            {
                if (Session["login"] != null)
                {
                    string strServicio = Session["cooperativa"].ToString();

                    lstCuenta = (List<clsCuenta>)Session["cuentasRegistrados"];
                    clsCuenta objCuenta = lstCuenta.Find(x => x.idCuenta == int.Parse(idCuenta));

                    lstCliente = objServicioCooperativa.ConsultarClientes(strServicio);
                    foreach (clsCliente objCliente in lstCliente)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objCliente.nombreCliente + " " + objCliente.apellidoCliente,
                            Value = objCliente.idCliente.ToString()
                        });
                    }

                    items1.Add(new SelectListItem
                    {
                        Text = "Ahorros",
                        Value = "A"
                    });
                    items1.Add(new SelectListItem
                    {
                        Text = "Corriente",
                        Value = "C"
                    });
                    ViewBag.lstClientes = items;
                    ViewBag.lstTipoCuenta = items1;


                    if (objCuenta != null)
                    {
                        return View(objCuenta);
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

        public ActionResult EliminarCuenta(string strNumeroCuenta)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    string strServicio = Session["cooperativa"].ToString();

                    if (objServicioCooperativa.EliminarCuenta(strServicio, strNumeroCuenta))
                    {
                        return RedirectToAction("Cuentas", new { strServicio });
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

        #region tarjetaCreditos
        public ActionResult TarjetasCredito(string strServicio)
        {
            Session["cooperativa"] = strServicio;
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    lstTarjetaCredito = objServicioCooperativa.ConsultarTarjetaCreditos(strServicio);
                    Session["tarjetaCreditosRegistrados"] = lstTarjetaCredito;
                    return View("TarjetasCredito", lstTarjetaCredito);
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

        
        public ActionResult EjecutarModificarTarjetaCredito(string strIdTarjetaCredito,
                    string strIdCuenta, string strNumeroTarjetaCredito, string strMarcaTarjeta)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                string strServicio = Session["cooperativa"].ToString();

                if (objServicioCooperativa.ModificarTarjetaCredito(strServicio, int.Parse(strIdTarjetaCredito),int.Parse(strIdCuenta),
                    strNumeroTarjetaCredito, strMarcaTarjeta))
                {
                    return RedirectToAction("TarjetasCredito", new { strServicio });
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

        public ActionResult EjecutarAgregarTarjetaCredito(string strIdCuenta,
            string strNumeroTarjetaCredito, string strMarcaTarjeta)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                string strServicio = Session["cooperativa"].ToString();
                
                if (objServicioCooperativa.AgregarTarjetaCredito(strServicio, int.Parse(strIdCuenta),
                    strNumeroTarjetaCredito, strMarcaTarjeta))
                {
                    return RedirectToAction("TarjetasCredito", new { strServicio });
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

        public ActionResult AgregarTarjetaCredito()
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            clsTarjetaCredito objTarjetaCredito = new clsTarjetaCredito();
            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items1 = new List<SelectListItem>();
            string strServicio = Session["cooperativa"].ToString();
            try
            {
                if (Session["login"] != null)
                {
                    lstCuenta = objServicioCooperativa.ConsultarCuentas(strServicio);
                    foreach (clsCuenta objCuenta in lstCuenta)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objCuenta.numeroCuenta,
                            Value = objCuenta.idCuenta.ToString()
                        });
                    }

                    items1.Add(new SelectListItem
                    {
                        Text = "Mastercard",
                        Value = "Mastercard"
                    });
                    items1.Add(new SelectListItem
                    {
                        Text = "Visa",
                        Value = "Visa"
                    });


                    ViewBag.lstCuentas = items;
                    ViewBag.lstTipoTarjetas = items1;

                    return View(objTarjetaCredito);
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

        public ActionResult ModificarTarjetaCredito(string idTarjetaCredito)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> items1 = new List<SelectListItem>();

            try
            {
                if(Session["login"] == null)
                {
                    string strServicio = Session["cooperativa"].ToString();

                    lstTarjetaCredito = (List<clsTarjetaCredito>)Session["tarjetaCreditosRegistrados"];
                    clsTarjetaCredito objTarjetaCredito = lstTarjetaCredito.Find(x => x.idTarjetaCredito == int.Parse(idTarjetaCredito));

                    lstCuenta = objServicioCooperativa.ConsultarCuentas(strServicio);
                    foreach (clsCuenta objCuenta in lstCuenta)
                    {
                        items.Add(new SelectListItem
                        {
                            Text = objCuenta.numeroCuenta,
                            Value = objCuenta.idCuenta.ToString()
                        });
                    }

                    items1.Add(new SelectListItem
                    {
                        Text = "Mastercard",
                        Value = "Mastercard"
                    });
                    items1.Add(new SelectListItem
                    {
                        Text = "Visa",
                        Value = "Visa"
                    });


                    ViewBag.lstCuentas = items;
                    ViewBag.lstTipoTarjetas = items1;

                    if (objTarjetaCredito != null)
                    {
                        return View(objTarjetaCredito);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Login","Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult EliminarTarjetaCredito(string idTarjetaCreditoEliminado)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    string strServicio = Session["cooperativa"].ToString();

                    if (objServicioCooperativa.EliminarTarjetaCredito(strServicio, int.Parse(idTarjetaCreditoEliminado)))
                    {
                        return RedirectToAction("TarjetasCredito", new { strServicio });
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

        #region CupoTarjetaCredito
        public ActionResult CupoTarjeta(string idTarjetaCreditoCupo)
        {
            string strServicio = Session["cooperativa"].ToString();
            clsSaldoTarjetaCredito objSaldoTarjetaCredito = new clsSaldoTarjetaCredito();
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if(Session["login"] == null)
                {
                    objSaldoTarjetaCredito = objServicioCooperativa.ConsultarSaldoTarjetaCredito(strServicio, int.Parse(idTarjetaCreditoCupo));
                    return View("CupoTarjeta", objSaldoTarjetaCredito);
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

        public ActionResult EjecutarModificarSaldoTarjetaCredito(string strIdSaldoTarjeta, string strNumeroTarjetaCredito,
            string strDblSaldoPendiente, string strDblCupoTarjeta, string strDblConsumoTarjeta, string strDblMinimoPagarTarjeta,
            string strFechaCorte, string strFechaPago)
        {
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                string strServicio = Session["cooperativa"].ToString();

                if (objServicioCooperativa.ModificarSaldoTarjetaCredito(strServicio, int.Parse(strIdSaldoTarjeta),
                    strNumeroTarjetaCredito,double.Parse(strDblSaldoPendiente), double.Parse(strDblConsumoTarjeta),
                    double.Parse(strDblCupoTarjeta), double.Parse(strDblMinimoPagarTarjeta), strFechaCorte,strFechaPago))
                {
                    return RedirectToAction("TarjetasCredito", new { strServicio });
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
        #endregion

        public ActionResult Movimientos(string strServicio)
        {
            
            clsServicioCooperativa objServicioCooperativa = new clsServicioCooperativa();
            try
            {
                if (Session["login"] != null)
                {
                    lstMovimiento = objServicioCooperativa.ConsultarMovimientos(strServicio);
                    Session["lista"] = lstMovimiento;
                    return View("Movimientos", lstMovimiento);
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
    }
}
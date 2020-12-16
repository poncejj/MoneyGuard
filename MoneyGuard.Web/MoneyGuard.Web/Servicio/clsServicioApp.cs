using MoneyGuard.Web.Models;
using MoneyGuard.Web.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;

namespace MoneyGuard.Web.Servicio
{
    public class clsServicioApp
    {
        public List<clsUsuarioApp> ConsultarTodosUsuarios()
        {
            List<clsUsuarioApp> lstUsuarioApp = new List<clsUsuarioApp>();
            DataTable dtRespuesta = null;
            clsPersistenciaUsuario objPersistenciaApp = new clsPersistenciaUsuario();
            try
            {
                dtRespuesta = objPersistenciaApp.ConsultarTodoUsuario("AppMovil");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsUsuarioApp objUsuarioApp = new clsUsuarioApp();
                        objUsuarioApp.idUsuarioApp = dr[0].ToString();
                        objUsuarioApp.nombreUsuarioApp = dr[1].ToString();
                        objUsuarioApp.identificacionUsuarioApp = dr[2].ToString();
                        objUsuarioApp.pinUsuarioApp = dr[3].ToString();
                        DateTime fecha = DateTime.Parse(dr[4].ToString());
                        objUsuarioApp.fechaCreacionUsuarioApp = fecha.ToString("dd-MM-yyyy HH:mm:ss");
                        objUsuarioApp.eliminadoUsuarioApp = bool.Parse(dr[5].ToString());
                        lstUsuarioApp.Add(objUsuarioApp);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstUsuarioApp;
        }

        internal bool ModificarUsuarioApp(string strId, string strIdentificacion, string strNombre, string strPin, bool eliminado)
        {
            clsPersistenciaUsuario objPersistenciaApp = new clsPersistenciaUsuario();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaApp.ModificarUsuarioApp("AppMovil",
                    strId, strIdentificacion, strNombre, strPin, eliminado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ValidarUsuario(string email, string password)
        {
            clsPersistenciaUsuario objPersistenciaApp = new clsPersistenciaUsuario();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaApp.InicioSession("AppMovil", email,password);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarUsuarioApp(string idUsuario)
        {
            clsPersistenciaUsuario objPersistenciaApp = new clsPersistenciaUsuario();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaApp.EliminarUsuarioApp("AppMovil", idUsuario);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        public List<clsCooperativaUsuario> ConsultarCooperativaUsuario()
        {
            List<clsCooperativaUsuario> lstCooperativaUsuario = new List<clsCooperativaUsuario>();
            DataTable dtRespuesta = null;
            clsPersistenciaCooperativaCliente objPersistenciaApp = new clsPersistenciaCooperativaCliente();
            try
            {
                dtRespuesta = objPersistenciaApp.ConsultarCooperativaUsuario("AppMovil");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsCooperativaUsuario objCooperativaUsuario = new clsCooperativaUsuario();
                        objCooperativaUsuario.idCooperativaUsuario = dr[0].ToString();
                        objCooperativaUsuario.idUsuario = dr[1].ToString();
                        objCooperativaUsuario.nombreUsuario = dr[2].ToString();
                        objCooperativaUsuario.idCooperativa = dr[3].ToString();
                        DateTime fecha = DateTime.Parse(dr[4].ToString());
                        objCooperativaUsuario.fechaCreacionCooperativaUsuario = fecha.ToString("dd-MM-yyyy HH:mm:ss");
                        objCooperativaUsuario.estadoCooperativaUsuario = bool.Parse(dr[5].ToString());
                        lstCooperativaUsuario.Add(objCooperativaUsuario);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstCooperativaUsuario;
        }

        internal bool ModificarCooperativaUsuarioApp(string strIdCooperativaUsuario, string idUsuario, string idCooperativa, bool eliminado)
        {
            clsPersistenciaCooperativaCliente objPersistenciaCooperativaCliente = new clsPersistenciaCooperativaCliente();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCooperativaCliente.ModificarCooperativaUsuario("AppMovil",
                    strIdCooperativaUsuario, idUsuario, idCooperativa, eliminado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarCooperativaUsuarioApp(string idCooperativaUsuario)
        {
            clsPersistenciaCooperativaCliente objPersistenciaApp = new clsPersistenciaCooperativaCliente();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaApp.EliminarCooperativaUsuario("AppMovil", idCooperativaUsuario);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        public List<clsCooperativa> ConsultarCooperativas()
        {
            List<clsCooperativa> lstCooperativa = new List<clsCooperativa>();
            DataTable dtRespuesta = null;
            clsPersistenciaCooperativa objPersistenciaCooperativa = new clsPersistenciaCooperativa();
            try
            {
                dtRespuesta = objPersistenciaCooperativa.ConsultarCooperativa("AppMovil");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsCooperativa objCooperativa = new clsCooperativa();
                        objCooperativa.idCooperativa = dr[0].ToString();
                        objCooperativa.nombreCooperativa = dr[1].ToString();
                        objCooperativa.nombreColaEnvio = dr[2].ToString();
                        objCooperativa.nombreColaRespuesta = dr[3].ToString();
                        objCooperativa.nombreBusServicios = dr[4].ToString();
                        DateTime fecha = DateTime.Parse(dr[5].ToString());
                        objCooperativa.fechaCreacionCooperativa = fecha.ToString("dd-MM-yyyy HH:mm:ss");
                        objCooperativa.estadoCooperativa = bool.Parse(dr[6].ToString());
                        lstCooperativa.Add(objCooperativa);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstCooperativa;
        }

        internal bool ModificarCooperativa(string strIdCooperativa, string nombreCoopertiva, string nombreColaEnvioCooperativa,
            string nombreColaRespuestaCooperativa, string nombreBusServiciosCooperativa, bool eliminado)
        {
            clsPersistenciaCooperativa objPersistenciaCooperativa = new clsPersistenciaCooperativa();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCooperativa.ModificarCooperativa("AppMovil",
                strIdCooperativa, nombreCoopertiva, nombreColaEnvioCooperativa, 
                nombreColaRespuestaCooperativa, nombreBusServiciosCooperativa, eliminado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarCooperativa(string idCooperativa)
        {
            clsPersistenciaCooperativa objPersistenciaCooperativa = new clsPersistenciaCooperativa();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCooperativa.EliminarCooperativa("AppMovil", idCooperativa);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool AgregarCooperativa(string nombreCooperativa, string nombreColaEnvio, 
            string nombreColaRespuesta, string nombreBusServicio, bool blCooperativaServicio)
        {
            clsPersistenciaCooperativa objPersistenciaCooperativa = new clsPersistenciaCooperativa();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCooperativa.AgregarCooperativa("AppMovil", nombreCooperativa,
                    nombreColaEnvio,nombreColaRespuesta,nombreBusServicio,blCooperativaServicio);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        public List<clsCatalogo> ConsultarCatalogo()
        {
            List<clsCatalogo> lstCatalogo = new List<clsCatalogo>();
            DataTable dtRespuesta = null;
            clsPersistenciaCatalogo objPersistenciaCatalogo = new clsPersistenciaCatalogo();
            try
            {
                dtRespuesta = objPersistenciaCatalogo.ConsultarCatalogo("AppMovil");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsCatalogo objCatalogo = new clsCatalogo();
                        objCatalogo.idCatalogo = dr[0].ToString();
                        objCatalogo.nombreCatalogo = dr[1].ToString();
                        objCatalogo.catalogoPadre = bool.Parse(dr[2].ToString());
                        objCatalogo.idCatalogoPadre = dr[3].ToString();
                        DateTime fecha = DateTime.Parse(dr[4].ToString());
                        objCatalogo.fechaCreacionCatalogo = fecha.ToString("dd-MM-yyyy HH:mm:ss");
                        objCatalogo.estadoCatalogo = bool.Parse(dr[5].ToString());
                        lstCatalogo.Add(objCatalogo);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstCatalogo;
        }

        internal bool ModificarCatalogo(string strIdCatalogo, string strNombreCatalogo, bool blCatalogoPadre,
            string strIdCatalogoPadre, bool blEliminado)
        {
            clsPersistenciaCatalogo objPersistenciaCatalogo = new clsPersistenciaCatalogo();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCatalogo.ModificarCatalogo("AppMovil",
                strIdCatalogo, strNombreCatalogo, blCatalogoPadre,strIdCatalogoPadre, blEliminado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarCatalogo(string idCatalogo)
        {
            clsPersistenciaCatalogo objPersistenciaCatalogo = new clsPersistenciaCatalogo();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCatalogo.EliminarCatalogo("AppMovil", idCatalogo);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool AgregarCatalogo(string strNombreCatalogo, bool blCatalogoPadre,
            string strIdCatalogoPadre, bool blEliminado)
        {
            clsPersistenciaCatalogo objPersistenciaCatalogo = new clsPersistenciaCatalogo();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCatalogo.AgregarCatalogo("AppMovil", strNombreCatalogo, blCatalogoPadre, strIdCatalogoPadre, blEliminado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal List<clsTransaccion> ConsultarTransacciones()
        {
            List<clsTransaccion> lstTransaccion = new List<clsTransaccion>();
            DataTable dtRespuesta = null;
            clsPersistenciaMovimiento objPersistenciaTransaccion = new clsPersistenciaMovimiento();
            try
            {
                dtRespuesta = objPersistenciaTransaccion.ConsultarMovimientos("AppMovil");
                if(dtRespuesta != null)
                {
                    foreach(DataRow dr in dtRespuesta.Rows)
                    {
                        clsTransaccion objTransaccion = new clsTransaccion();
                        objTransaccion.idMovimiento = dr[0].ToString();
                        objTransaccion.identificacionBeneficiario = dr[1].ToString();
                        objTransaccion.numeroCuentaBeneficiario = dr[2].ToString();
                        objTransaccion.entidadBeneficiario = dr[3].ToString();
                        objTransaccion.identificacionEmisor = dr[4].ToString();
                        objTransaccion.numeroCuentaEmsior = dr[5].ToString();
                        objTransaccion.entidadEmisor = dr[6].ToString();
                        objTransaccion.montoMovimiento = double.Parse(dr[7].ToString());
                        objTransaccion.motivoMovimiento = dr[8].ToString();
                        DateTime fecha = DateTime.Parse(dr[9].ToString());
                        objTransaccion.fechaMovimiento = fecha.ToShortDateString();
                        objTransaccion.Tipo = dr[10].ToString();

                        lstTransaccion.Add(objTransaccion);
                    }
                }
            }
            catch(Exception)
            {
                throw;
            }
            return lstTransaccion;
        }

        internal List<clsAlerta> ConsultarAlertas()
        {
            List<clsAlerta> lstAlerta = new List<clsAlerta>();
            DataTable dtRespuesta = null;
            clsPersistenciaAlerta objPersistenciaAlerta = new clsPersistenciaAlerta();
            try
            {
                dtRespuesta = objPersistenciaAlerta.ConsultarAlertas("AppMovil");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsAlerta objAlerta = new clsAlerta();
                        objAlerta.idAlerta = dr[0].ToString();
                        objAlerta.strTituloAlerta = dr[1].ToString();
                        objAlerta.strMensajeAlerta = dr[2].ToString();
                        objAlerta.fechaAlerta = dr[3].ToString();

                        lstAlerta.Add(objAlerta);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstAlerta;
        }
    }
}
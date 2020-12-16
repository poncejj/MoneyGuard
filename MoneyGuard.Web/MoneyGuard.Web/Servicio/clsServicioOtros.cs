using MoneyGuard.Web.Models;
using MoneyGuard.Web.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;

namespace MoneyGuard.Web.Servicio
{
    public class clsServicioOtro
    {
        #region Suministro
        public List<clsSuministro> ConsultarSuministro()
        {
            List<clsSuministro> lstSuministro = new List<clsSuministro>();
            DataTable dtRespuesta = null;
            clsPersistenciaSuministro objPersistenciaSuministro = new clsPersistenciaSuministro();
            try
            {
                dtRespuesta = objPersistenciaSuministro.ConsultarSuministro("ServiciosBasicos");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsSuministro objSuministro = new clsSuministro();
                        objSuministro.idSuministro = int.Parse(dr[0].ToString());
                        objSuministro.numeroSuministro = dr[1].ToString();
                        objSuministro.nombreServicio = dr[2].ToString();
                        objSuministro.nombreCliente = dr[3].ToString();
                        objSuministro.direccionSuministro = dr[4].ToString();
                        objSuministro.idDireccion = int.Parse(dr[5].ToString());
                        lstSuministro.Add(objSuministro);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstSuministro;
        }

        internal bool AgregarSuministro(int idServicioBasico, 
            string strNumeroSuministro, int idCliente, string strDescripcionDireccion, 
            int idParroquia)
        {
            clsPersistenciaSuministro objPersistenciaSuministro = new clsPersistenciaSuministro();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaSuministro.AgregarSuministro("ServiciosBasicos", idServicioBasico,
            strNumeroSuministro, idCliente,
            strDescripcionDireccion, idParroquia);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ModificarSuministro(int idSuministro,
            int idServicioBasico, string strNumeroSuministro, int idCliente,
            int idDireccion, string strDescripcionDireccion, int idParroquia)
        {
            clsPersistenciaSuministro objPersistenciaSuministro = new clsPersistenciaSuministro();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaSuministro.ModificarSuministro("ServiciosBasicos", idSuministro,
                idServicioBasico, strNumeroSuministro, idCliente, idDireccion, 
                strDescripcionDireccion, idParroquia);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarSuministro(int idCliente)
        {
            clsPersistenciaSuministro objPersistenciaSuministro = new clsPersistenciaSuministro();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaSuministro.EliminarSuministro("ServiciosBasicos", idCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        #endregion

        #region Cliente
        public List<clsClienteServicios> ConsultarCliente()
        {
            List<clsClienteServicios> lstClienteServicios = new List<clsClienteServicios>();
            DataTable dtRespuesta = null;
            clsPersistenciaClienteServicio objPersistenciaClienteServicio = new clsPersistenciaClienteServicio();
            try
            {
                dtRespuesta = objPersistenciaClienteServicio.ConsultarCliente("ServiciosBasicos");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsClienteServicios objClienteServicios = new clsClienteServicios();
                        objClienteServicios.idCliente = int.Parse(dr[0].ToString());
                        objClienteServicios.identificacionCliente = dr[1].ToString();
                        objClienteServicios.nombreCliente = dr[2].ToString();
                        objClienteServicios.apellidoCliente = dr[3].ToString();
                        lstClienteServicios.Add(objClienteServicios);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstClienteServicios;
        }

        internal bool AgregarCliente(string strIdentificacionCliente,
            string strNombreCliente, string strApellidoCliente)
        {
            clsPersistenciaClienteServicio objPersistenciaClienteServicio = new clsPersistenciaClienteServicio();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaClienteServicio.AgregarCliente("ServiciosBasicos", strIdentificacionCliente, 
                    strNombreCliente, strApellidoCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ModificarCliente(int idCliente,string strIdentificacionCliente, string strNombreCliente, string strApellidoCliente)
        {
            clsPersistenciaClienteServicio objPersistenciaClienteServicio = new clsPersistenciaClienteServicio();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaClienteServicio.ModificarCliente("ServiciosBasicos", 
                    idCliente, strIdentificacionCliente,strNombreCliente,strApellidoCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarCliente(int idCliente)
        {
            clsPersistenciaClienteServicio objPersistenciaClienteServicio = new clsPersistenciaClienteServicio();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaClienteServicio.EliminarCliente("ServiciosBasicos", idCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        #endregion

        #region Factura
        public List<clsFactura> ConsultarFactura()
        {
            List<clsFactura> lstFacturas = new List<clsFactura>();
            DataTable dtRespuesta = null;
            clsPersistenciaFactura objPersistenciaFactura = new clsPersistenciaFactura();
            try
            {
                dtRespuesta = objPersistenciaFactura.ConsultarFactura("ServiciosBasicos");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsFactura objFactura = new clsFactura();
                        objFactura.idFactura = int.Parse(dr[0].ToString());
                        objFactura.numeroSuministro = dr[1].ToString();
                        objFactura.valorFactura = double.Parse(dr[2].ToString());
                        DateTime fecha = DateTime.Parse(dr[3].ToString());
                        objFactura.fechaEmision = fecha.ToString("yyyy-MM-dd");
                        fecha = DateTime.Parse(dr[4].ToString());
                        objFactura.fechaVencimiento = fecha.ToString("yyyy-MM-dd");
                        objFactura.valorPendiente = double.Parse(dr[5].ToString());
                        objFactura.valorPagado = bool.Parse(dr[6].ToString());
                        lstFacturas.Add(objFactura);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstFacturas;
        }

        internal List<clsGenerico> ConsultarTipoServicios()
        {
            List<clsGenerico> lstTipoServicios = new List<clsGenerico>();
            DataTable dtRespuesta = null;
            clsPersistenciaSuministro objPersistenciaSuministro = new clsPersistenciaSuministro();
            try
            {
                dtRespuesta = objPersistenciaSuministro.ConsultarTipoServicios("ServiciosBasicos");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsGenerico objGenerico = new clsGenerico();
                        objGenerico.idGenerico = int.Parse(dr[0].ToString());
                        objGenerico.strDescripcion = dr[1].ToString();
                        lstTipoServicios.Add(objGenerico);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstTipoServicios;
        }

        internal List<clsGenerico> ConsultarParroquias()
        {
            List<clsGenerico> lstParroquias = new List<clsGenerico>();
            DataTable dtRespuesta = null;
            clsPersistenciaSuministro objPersistenciaSuministro = new clsPersistenciaSuministro();
            try
            {
                dtRespuesta = objPersistenciaSuministro.ConsultarParroquias("ServiciosBasicos");
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsGenerico objGenerico = new clsGenerico();
                        objGenerico.idGenerico = int.Parse(dr[0].ToString());
                        objGenerico.strDescripcion = dr[1].ToString();
                        lstParroquias.Add(objGenerico);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstParroquias;
        }

        internal bool AgregarFactura(int idSuministro, double valorFactura, 
            string strFechaEmision, string strFechaVencimiento,double dblValorPendiente, 
            bool blValorPagado)
        {
            clsPersistenciaFactura objPersistenciaFactura = new clsPersistenciaFactura();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaFactura.AgregarFactura("ServiciosBasicos",
                    idSuministro, valorFactura,strFechaEmision,strFechaVencimiento,
                    dblValorPendiente,blValorPagado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ModificarFactura(int idSuministro, int idFactura, double valorFactura,
            string strFechaEmision, string strFechaVencimiento, double dblValorPendiente,
            bool blValorPagado)
        {
            clsPersistenciaFactura objPersistenciaFactura = new clsPersistenciaFactura();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaFactura.ModificarFactura("ServiciosBasicos",idFactura,
                    idSuministro, valorFactura, strFechaEmision, strFechaVencimiento,
                    dblValorPendiente, blValorPagado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarFactura(int idFactura)
        {
            clsPersistenciaFactura objPersistenciaFactura = new clsPersistenciaFactura();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaFactura.EliminarFactura("ServiciosBasicos", idFactura);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        #endregion

        #region DetalleFactura
        public List<clsDetalleFactura> ConsultarDetalleFactura(int idFactura)
        {
            List<clsDetalleFactura> lstDetalleFacturas = new List<clsDetalleFactura>();
            DataTable dtRespuesta = null;
            clsPersistenciaFactura objPersistenciaFactura = new clsPersistenciaFactura();
            try
            {
                dtRespuesta = objPersistenciaFactura.ConsultarDetalleFactura("ServiciosBasicos", idFactura);
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsDetalleFactura objDetalleFactura = new clsDetalleFactura();
                        objDetalleFactura.idDetalleFactura = int.Parse(dr[0].ToString());
                        objDetalleFactura.idFactura = int.Parse(dr[1].ToString());
                        objDetalleFactura.descripcionDetalleFactura = dr[2].ToString();
                        objDetalleFactura.valorDetalleFactura = double.Parse(dr[3].ToString());
                        lstDetalleFacturas.Add(objDetalleFactura);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstDetalleFacturas;
        }

        internal bool AgregarDetalleFactura(int idFactura, string strDescripcion, double valorDetalle)
        {
            clsPersistenciaFactura objPersistenciaFactura = new clsPersistenciaFactura();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaFactura.AgregarDetalleFactura("ServiciosBasicos",
                    idFactura, strDescripcion, valorDetalle);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarDetalleFactura(int idDetalleFactura)
        {
            clsPersistenciaFactura objPersistenciaFactura = new clsPersistenciaFactura();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaFactura.EliminarFactura("ServiciosBasicos", idDetalleFactura);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        #endregion
    }
}
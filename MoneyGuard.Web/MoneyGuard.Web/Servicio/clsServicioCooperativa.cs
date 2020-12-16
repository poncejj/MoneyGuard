using MoneyGuard.Web.Models;
using MoneyGuard.Web.Persistencia;
using System;
using System.Collections.Generic;
using System.Data;

namespace MoneyGuard.Web.Servicio
{
    public class clsServicioCooperativa
    {

        #region Cliente
        public List<clsCliente> ConsultarClientes(string strServicio)
        {
            List<clsCliente> lstCliente = new List<clsCliente>();
            DataTable dtRespuesta = null;
            clsPersistenciaCliente objPersistenciaCliente = new clsPersistenciaCliente();
            try
            {
                dtRespuesta = objPersistenciaCliente.ConsultarCliente(strServicio);
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsCliente objCliente = new clsCliente();
                        objCliente.idCliente = int.Parse(dr[0].ToString());
                        objCliente.identificacionCliente = dr[1].ToString();
                        objCliente.nombreCliente = dr[2].ToString();
                        objCliente.apellidoCliente = dr[3].ToString();
                        objCliente.telefonoCliente = dr[4].ToString();
                        objCliente.emailCliente = dr[5].ToString();
                        lstCliente.Add(objCliente);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstCliente;
        }

        internal bool AgregarCliente(string strServicio, string strIdentificacionCliente,
            string strNombreCliente, string strApellidoCliente, string strTelefonoCliente,
            string strEmailCliente)
        {
            clsPersistenciaCliente objPersistenciaCliente = new clsPersistenciaCliente();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCliente.AgregarCliente(strServicio,
                    strIdentificacionCliente, strNombreCliente, strApellidoCliente, strTelefonoCliente,
                    strEmailCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ModificarCliente(string strServicio, int idCliente, string strIdentificacionCliente,
            string strNombreCliente, string strApellidoCliente, string strTelefonoCliente,
            string strEmailCliente)
        {
            clsPersistenciaCliente objPersistenciaCliente = new clsPersistenciaCliente();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCliente.ModificarCliente(strServicio,
                    idCliente, strIdentificacionCliente, strNombreCliente, strApellidoCliente, strTelefonoCliente,
                    strEmailCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarCliente(string strServicio, int idCliente)
        {
            clsPersistenciaCliente objPersistenciaCliente = new clsPersistenciaCliente();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCliente.EliminarCliente(strServicio, idCliente);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        #endregion

        #region Cuenta
        public List<clsCuenta> ConsultarCuentas(string strServicio)
        {
            List<clsCuenta> lstCuenta = new List<clsCuenta>();
            DataTable dtRespuesta = null;
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            try
            {
                dtRespuesta = objPersistenciaCuenta.ConsultarCuenta(strServicio);
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsCuenta objCuenta = new clsCuenta();
                        objCuenta.idCuenta = int.Parse(dr[0].ToString());
                        objCuenta.numeroCuenta = dr[1].ToString();
                        objCuenta.idCliente = int.Parse(dr[2].ToString());
                        objCuenta.nombreCliente = dr[3].ToString();
                        objCuenta.tipoCuenta = dr[4].ToString();
                        objCuenta.saldo = double.Parse(dr[5].ToString());
                        objCuenta.estado = bool.Parse(dr[6].ToString());
                        lstCuenta.Add(objCuenta);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstCuenta;
        }

        internal bool AgregarCuenta(string strServicio, int idCliente,
            string strNumeroCuenta, string strTipoCuenta, double dblSaldo, bool estado)
        {
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCuenta.AgregarCuenta(strServicio,
                    idCliente,strNumeroCuenta,strTipoCuenta,dblSaldo,estado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ModificarCuenta(string strServicio, int idCuenta, int idCliente,
            string strNumeroCuenta, string strTipoCuenta, double dblSaldo, bool estado)
        {
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCuenta.ModificarCuenta(strServicio, idCuenta, 
                    idCliente,strNumeroCuenta,strTipoCuenta,dblSaldo,estado);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarCuenta(string strServicio, string strNumeroCuenta)
        {
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaCuenta.EliminarCuenta(strServicio, strNumeroCuenta);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        #endregion

        #region Movimiento
        public List<clsMovimiento> ConsultarMovimientos(string strServicio)
        {
            List<clsMovimiento> lstMovimiento = new List<clsMovimiento>();
            DataTable dtRespuesta = null;
            clsPersistenciaMovimiento objPersistenciaMovimiento = new clsPersistenciaMovimiento();
            try
            {
                dtRespuesta = objPersistenciaMovimiento.ConsultarMovimientos(strServicio);
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsMovimiento objMovimiento = new clsMovimiento();
                        DateTime fechaMovimiento = DateTime.Parse(dr[0].ToString());
                        objMovimiento.fechaMovimiento = fechaMovimiento.ToString("yyyy-MM-dd");
                        objMovimiento.numeroCuentaBeneficiario = dr[1].ToString();
                        objMovimiento.motivoMovimiento = dr[2].ToString();
                        objMovimiento.entidadBeneficiario = dr[3].ToString();
                        objMovimiento.Tipo = dr[4].ToString();
                        objMovimiento.montoMovimiento = double.Parse(dr[5].ToString());
                        objMovimiento.saldoDisponible = double.Parse(dr[6].ToString());
                        objMovimiento.lugarMovimiento = dr[7].ToString();
                        lstMovimiento.Add(objMovimiento);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstMovimiento;
        }
        #endregion

        #region TarjetaCredito
        public List<clsTarjetaCredito> ConsultarTarjetaCreditos(string strServicio)
        {
            List<clsTarjetaCredito> lstTarjetaCredito = new List<clsTarjetaCredito>();
            DataTable dtRespuesta = null;
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            try
            {
                dtRespuesta = objPersistenciaTarjetaCredito.ConsultarTarjetaCredito(strServicio);
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsTarjetaCredito objTarjetaCredito = new clsTarjetaCredito();
                        objTarjetaCredito.idTarjetaCredito = int.Parse(dr[0].ToString());
                        objTarjetaCredito.strNombreCliente = dr[1].ToString();
                        objTarjetaCredito.strNumeroCuenta = dr[2].ToString();
                        objTarjetaCredito.strNumeroTarjeta = dr[3].ToString();
                        objTarjetaCredito.strMarcaTarjeta = dr[4].ToString();
                        objTarjetaCredito.dblCupoTarjeta = double.Parse(dr[5].ToString());
                        lstTarjetaCredito.Add(objTarjetaCredito);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstTarjetaCredito;
        }

        internal bool AgregarTarjetaCredito(string strServicio, int idCuenta,
            string strNumeroTarjetaCredito, string strMarcaTarjeta)
        {
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaTarjetaCredito.AgregarTarjetaCredito(strServicio,idCuenta,
                    strNumeroTarjetaCredito,strMarcaTarjeta);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ModificarTarjetaCredito(string strServicio, int idTarjetaCredito, int idCuenta,
            string strNumeroTarjeta, string strMarcaTarjeta)
        {
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaTarjetaCredito.ModificarTarjetaCredito(strServicio, 
                    idTarjetaCredito, idCuenta, strNumeroTarjeta, strMarcaTarjeta);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarTarjetaCredito(string strServicio, int idTarjetaCredito)
        {
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaTarjetaCredito.EliminarTarjetaCredito(strServicio, idTarjetaCredito);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }
        #endregion

        #region SaldoTarjetaCredito
        public List<clsSaldoTarjetaCredito> ConsultarSaldoTarjetaCreditos(string strServicio)
        {
            List<clsSaldoTarjetaCredito> lstSaldoTarjetaCredito = new List<clsSaldoTarjetaCredito>();
            DataTable dtRespuesta = null;
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            try
            {
                dtRespuesta = objPersistenciaTarjetaCredito.ConsultarSaldoTarjetaCredito(strServicio);
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        clsSaldoTarjetaCredito objSaldoTarjetaCredito = new clsSaldoTarjetaCredito();
                        objSaldoTarjetaCredito.idSaldoTarjetaCredito = int.Parse(dr[0].ToString());
                        objSaldoTarjetaCredito.strNumeroTarjeta = dr[1].ToString();
                        objSaldoTarjetaCredito.dblCupoTarjeta = double.Parse(dr[2].ToString());
                        objSaldoTarjetaCredito.dblSaldoTarjeta = double.Parse(dr[3].ToString());
                        objSaldoTarjetaCredito.dblConsumoTarjeta = double.Parse(dr[4].ToString());
                        objSaldoTarjetaCredito.dblMinimoPagar = double.Parse(dr[5].ToString());
                        DateTime fechaTemp = DateTime.Parse(dr[6].ToString());
                        objSaldoTarjetaCredito.strFechaPago = fechaTemp.ToString("yyyy-MM-dd");
                        fechaTemp = DateTime.Parse(dr[7].ToString());
                        objSaldoTarjetaCredito.strFechaCorte = fechaTemp.ToString("yyyy-MM-dd");
                        lstSaldoTarjetaCredito.Add(objSaldoTarjetaCredito);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lstSaldoTarjetaCredito;
        }

        internal clsSaldoTarjetaCredito ConsultarSaldoTarjetaCredito(string strServicio, int intIdTarjeta)
        {
            clsSaldoTarjetaCredito objSaldoTarjetaCredito = new clsSaldoTarjetaCredito();
            DataTable dtRespuesta = null;
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            try
            {
                dtRespuesta = objPersistenciaTarjetaCredito.ConsultarSaldoTarjetaCredito(strServicio, intIdTarjeta);
                if (dtRespuesta != null)
                {
                    foreach (DataRow dr in dtRespuesta.Rows)
                    {
                        objSaldoTarjetaCredito.idSaldoTarjetaCredito = int.Parse(dr[0].ToString());
                        objSaldoTarjetaCredito.strNumeroTarjeta = dr[1].ToString();
                        objSaldoTarjetaCredito.dblCupoTarjeta = double.Parse(dr[2].ToString());
                        objSaldoTarjetaCredito.dblSaldoTarjeta = double.Parse(dr[3].ToString());
                        objSaldoTarjetaCredito.dblConsumoTarjeta = double.Parse(dr[4].ToString());
                        objSaldoTarjetaCredito.dblMinimoPagar = double.Parse(dr[5].ToString());
                        DateTime fechaTemp = DateTime.Parse(dr[6].ToString());
                        objSaldoTarjetaCredito.strFechaPago = fechaTemp.ToString("yyyy-MM-dd");
                        fechaTemp = DateTime.Parse(dr[7].ToString());
                        objSaldoTarjetaCredito.strFechaCorte = fechaTemp.ToString("yyyy-MM-dd");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return objSaldoTarjetaCredito;
        }

        internal bool AgregarSaldoTarjetaCredito(string strServicio, int idTarjeta,
            double dblSaldoPendiente, double dblConsumoMes, double dblCupoDisponible,
            double dblMinimoPagar, string strFechaCorte, string strFechaPago)
        {
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaTarjetaCredito.AgregarSaldoTarjetaCredito(strServicio,
                idTarjeta, dblSaldoPendiente, dblConsumoMes, dblCupoDisponible, dblMinimoPagar,
                strFechaCorte, strFechaPago);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool ModificarSaldoTarjetaCredito(string strServicio, int idSaldoTarjeta, 
            string strNumeroTarjeta, double dblSaldoPendiente, double dblConsumoMes, double dblCupoDisponible,
            double dblMinimoPagar, string strFechaCorte, string strFechaPago)
        {
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaTarjetaCredito.ModificarSaldoTarjetaCredito(strServicio,
                idSaldoTarjeta,strNumeroTarjeta, dblSaldoPendiente, dblConsumoMes, dblCupoDisponible, 
                dblMinimoPagar, strFechaCorte, strFechaPago);
            }
            catch (Exception)
            {
                throw;
            }
            return retorno;
        }

        internal bool EliminarSaldoTarjetaCredito(string strServicio, int idSaldoTarjetaCredito)
        {
            clsPersistenciaTarjetaCredito objPersistenciaTarjetaCredito = new clsPersistenciaTarjetaCredito();
            bool retorno = false;
            try
            {
                retorno = objPersistenciaTarjetaCredito.EliminarSaldoTarjetaCredito(strServicio, idSaldoTarjetaCredito);
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
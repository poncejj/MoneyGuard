using SimuladorCooperativaAmarilla.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimuladorCooperativaAmarilla.CapaPersistencia;

namespace SimuladorCooperativaAmarilla.Logica
{
    public class clsLogicaCuenta
    {
        public clsCuenta consultaCuentaCliente(string strIdentificacionCliente, string strNumeroCuenta)
        {
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            clsCuenta objCuenta = null;
            try
            {
               objCuenta = objPersistenciaCuenta.consultarCuentaCliente(strIdentificacionCliente,strNumeroCuenta);
            }
            catch (Exception ex)
            {
                Console.Write("Error al consultar cuenta en el simulador: " +ex.Message);
            }
            return objCuenta;
        }

        public List<clsCuenta> consultaCuenta(string strIdentificacionCliente)
        {
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            try
            {
                List<clsCuenta> lstCuenta = objPersistenciaCuenta.consultarCuentaIdentificacion(strIdentificacionCliente);
                return lstCuenta;
            }
            catch (Exception ex)
            {
                Console.Write("Error al consultar cuenta en el simulador: " + ex.Message);
                return null;
            }
        }

        public List<clsTarjetaCedito> consultaTarjeta(string strIdentificacionCliente)
        {
            clsPersistenciaCuenta objPersistenciaTarjeta = new clsPersistenciaCuenta();
            try
            {
                List<clsTarjetaCedito> lstTarjetaCedito = objPersistenciaTarjeta.consultarTarjetasCliente(strIdentificacionCliente);
                return lstTarjetaCedito;
            }
            catch (Exception ex)
            {
                Console.Write("Error al consultar tarjeta de credito en el simulador: " + ex.Message);
                return null;
            }
        }

        public List<clsMovimiento> consultaMovimientos(string strNumeroCuenta, int intCantidad)
        {
            clsPersistenciaMovimiento objPersistenciaMovimientos = new clsPersistenciaMovimiento();
            try
            {
                List<clsMovimiento> lstMovimientos = objPersistenciaMovimientos.consultarMovimientos(strNumeroCuenta,intCantidad);
                return lstMovimientos;
            }
            catch (Exception ex)
            {
                Console.Write("Error al consultar movimientos en el simulador: " + ex.Message);
                return null;
            }
        }

        public bool pagarTarjetaCredito(string strIdentificacionCliente, string strNumeroTarjeta, double dblMonto)
        {
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            try
            {
                return objPersistenciaCuenta.pagarTarjeta(strIdentificacionCliente, strNumeroTarjeta, dblMonto);
            }
            catch (Exception ex)
            {
                Console.Write("Error al consultar movimientos en el simulador: " + ex.Message);
            }
            return false;
        }
    }
}

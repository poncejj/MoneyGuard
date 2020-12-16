using SimuladorCooperativaAmarilla.CapaPersistencia;
using SimuladorCooperativaAmarilla.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCooperativaAmarilla.Logica
{
    public class clsLogicaCliente
    {
        public clsCuenta validarExistenciaCuentaCliente(string identificacionCliente, string numeroCuenta)
        {
            clsCuenta objCuenta = null;
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            try
            {
                objCuenta = objPersistenciaCuenta.consultarCuentaCliente(identificacionCliente,numeroCuenta);
                return objCuenta;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public string consultarNombreCliente(string identificacionCliente, string numeroCuenta)
        {
            string strNombreCliente = null;
            clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
            try
            {
                strNombreCliente = objPersistenciaCuenta.consultarNombreCliente(identificacionCliente, numeroCuenta);
                return strNombreCliente;
            }
            catch (Exception ex)
            {
                return "Error: " +ex.Message;
            }
        }
    }
}

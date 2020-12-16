using SimuladorCooperativaVerde.CapaPersistencia;
using SimuladorCooperativaVerde.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCooperativaVerde.Logica
{
    public class clsLogicaTransferencia
    {
        clsPersistenciaMovimiento objPersistenciaMovimiento = new clsPersistenciaMovimiento();
        clsPersistenciaCuenta objPersistenciaCuenta = new clsPersistenciaCuenta();
        public string realizarDebito(clsMovimiento objMovimiento, string strNumeroCuentaCliente, string strIdentificacionCliente)
        {
            double saldo = 0;
            string strRespuesta = string.Empty;
            try
            {
                saldo = objPersistenciaCuenta.consultarSaldoCuentaCliente(strNumeroCuentaCliente, strIdentificacionCliente);
                if(saldo != -1)
                {
                    if (saldo >= objMovimiento.monto_movimiento)
                    {
                        objMovimiento.saldo_cuenta_movimiento = saldo;
                        if (objPersistenciaMovimiento.insertarMovimiento(objMovimiento, strNumeroCuentaCliente, strIdentificacionCliente))
                        {
                            return "Transferencia se realizo con exito!";
                        }
                        else
                        {
                            return "Error: No se pudo registrar la transferencia";
                        }
                    }
                    else
                    {
                        return "Error: Saldo no disponible para realizar la transferencia";
                    }
                }
                else
                {
                    return "Error: Cliente o la cuenta de cliente beneficiario no existe";
                }
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string realizarCredito(clsMovimiento objMovimiento, string strNumeroCuentaCliente, string strIdentificacionCliente)
        {
            double saldo = 0;
            string strRespuesta = string.Empty;
            try
            {
                saldo = objPersistenciaCuenta.consultarSaldoCuentaCliente(strNumeroCuentaCliente, strIdentificacionCliente);
                if (saldo != -1)
                {
                    if (saldo >= objMovimiento.monto_movimiento)
                    {
                        objMovimiento.saldo_cuenta_movimiento = saldo;
                        if (objPersistenciaMovimiento.insertarMovimiento(objMovimiento, strNumeroCuentaCliente, strIdentificacionCliente))
                        {
                            return "Transferencia se realizo con exito!";
                        }
                        else
                        {
                            return "Error: No se pudo registrar la transferencia";
                        }
                    }
                    else
                    {
                        return "Error: Saldo no disponible para realizar la transferencia";
                    }
                }
                else
                {
                    return "Error: Cliente o la cuenta de cliente beneficiario no existe";
                }
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}

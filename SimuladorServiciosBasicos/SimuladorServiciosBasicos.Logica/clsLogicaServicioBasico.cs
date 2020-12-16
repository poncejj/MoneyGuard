using Newtonsoft.Json;
using SimuladorServiciosBasicos.CapaPersistencia;
using SimuladorServiciosBasicos.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorServiciosBasicos.Logica
{
    public class clsLogicaServicioBasico
    {
        clsPersistenciaServicioBasico objPersistenciaServicioBasico = new clsPersistenciaServicioBasico();
        public string realizarPago(string strIdentificacionCliente, string strNumeroSuministro, decimal dblValor, string strTipo)
        {
            int idSuministro = 0;
            string strRespuesta = string.Empty;
            try
            {
                idSuministro = objPersistenciaServicioBasico.verificarSuministro(strIdentificacionCliente, strNumeroSuministro, strTipo);
                if(idSuministro != -1)
                {
                    if (objPersistenciaServicioBasico.verificarMonto(idSuministro, dblValor))
                        return objPersistenciaServicioBasico.pagarServicio(idSuministro);
                    else
                        return "Error al realizar el pago del servicio, el monto debe ser exacto al de la factura";
                }
                else
                {
                    return "Error: Suministro ingresado no existe o no está registrado con este cliente";
                }
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string consultarFactura(string strIdentificacionCliente, string strNumeroSuministro, string strTipo)
        {
            int idSuministro = 0;
            string strRespuesta = string.Empty;
            try
            {
                idSuministro = objPersistenciaServicioBasico.verificarSuministro(strIdentificacionCliente, strNumeroSuministro, strTipo);
                if (idSuministro != -1)
                {
                    clsCabeceraFacturaServicio objCabeceraFacturaServicio = objPersistenciaServicioBasico.consultarFactura(idSuministro);
                    if(objCabeceraFacturaServicio != null)
                    {
                        strRespuesta = JsonConvert.SerializeObject(objCabeceraFacturaServicio);
                        return strRespuesta;
                    }
                    else
                    {
                        return "Error Factura no registrada";
                    }
                }
                else
                {
                    return "Error: Suministro ingresado no existe o no está registrado con este cliente";
                }
            }
            catch (Exception ex)
            {
                return "Error al obtener la factura del suministro";
            }
        }
        
    }
}

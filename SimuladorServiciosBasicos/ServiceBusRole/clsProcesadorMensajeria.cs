using System;
using Microsoft.ServiceBus.Messaging;
using SimuladorServiciosBasicos.ModeloDatos;
using Newtonsoft.Json;
using System.Collections.Generic;
using SimuladorServiciosBasicos.Logica;
using SimuladorServiciosBasicos.Comun;

namespace ServiceBusRole
{
    public class clsProcesadorMensajeria
    {
        public string procesarMensaje(string strTipo, BrokeredMessage message)
        {
            clsLogicaServicioBasico objLogicaServicioBasico = new clsLogicaServicioBasico();
            string strRespuesta = string.Empty;
            switch (strTipo)
            {
                case "PagoServicio":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strNumeroSuministroTemp = message.Properties["strNumeroSuministro"].ToString();
                        decimal dblValor = decimal.Parse(message.Properties["dblMonto"].ToString());
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        string strTipoSuministro = message.Properties["strTipoSuministro"].ToString();
                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp,strContrasena);
                        string strNumeroSuministro = clsSeguridad.Desencriptar(strNumeroSuministroTemp, strContrasena);

                        strRespuesta = objLogicaServicioBasico.realizarPago(strIdentificacionCliente,strNumeroSuministro,dblValor,strTipoSuministro);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al realizar el pago de servicio: " + ex.Message);
                        strRespuesta = "Error al realizar el pago de servicio: " + ex.Message;
                    }
                    break;
                case "ConsultaFactura":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strNumeroSuministroTemp = message.Properties["strNumeroSuministro"].ToString();
                        string strTipoSuministro = message.Properties["strTipoServicio"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        string strNumeroSuministro = clsSeguridad.Desencriptar(strNumeroSuministroTemp, strContrasena);
                        strRespuesta = objLogicaServicioBasico.consultarFactura(strIdentificacionCliente, strNumeroSuministro, strTipoSuministro);
                        strRespuesta = clsSeguridad.Encriptar(strRespuesta,strContrasena);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al realizar el pago de servicio: " + ex.Message);
                        strRespuesta = "Error al realizar el pago de servicio: " + ex.Message;
                    }
                    break;
    
            }
            return strRespuesta;
        }
    }
}

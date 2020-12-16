using apptesisService.Comun;
using apptesisService.DataObjects;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Web.Http;

namespace apptesisService.Controllers
{
    [MobileAppController]
    public class PagoServicioController : ApiController
    {
        private string connectionString = Conexiones.busConnectionString;

        public string Get(string strNombreColaEnvio, string strNombreColaRespuesta, 
            string strIdentificacionCliente, string strNumeroSuministro, 
            string strNombreColaEnvioDebito, string strNombreColaRespuestaDebito, 
            string strBancoBeneficiario, string strBancoEmisor, 
            string strTipoServicio, string jsonTransferenciaCifrada, string strContrasena)
        {
            string strRespuestaDebito = string.Empty;
            string strRespuestaReverso = string.Empty;
            string strRespuestaPagoServicio = string.Empty;
            string strNombreCliente = string.Empty;
            string result = string.Empty;
            string jsonTransferenciaDescifrada = string.Empty;
            try
            {
                jsonTransferenciaDescifrada = clsSeguridad.Desencriptar(jsonTransferenciaCifrada, strContrasena);
                clsTransfer objTransferencia = JsonConvert.DeserializeObject<clsTransfer>(jsonTransferenciaDescifrada);
                strRespuestaDebito = clsFuncionesGenerales.realizarDebito(strNombreColaEnvioDebito, strNombreColaRespuestaDebito, strBancoBeneficiario, objTransferencia, "Pago Servicio", strContrasena);
                bool flag = !strRespuestaDebito.ToUpper().Contains("ERROR");
                if (flag)
                {
                    strRespuestaPagoServicio = this.realizarPagoServicio(strNombreColaEnvio, 
                        strNombreColaRespuesta, strIdentificacionCliente, 
                        strNumeroSuministro, objTransferencia.dblMonto, 
                        strTipoServicio, strContrasena);
                    bool flag2 = !strRespuestaDebito.ToUpper().Contains("ERROR");
                    if (flag2)
                    {
                        result = strRespuestaPagoServicio;
                    }
                    else
                    {
                        strRespuestaReverso = clsFuncionesGenerales.realizarReverso(strNombreColaEnvioDebito, strNombreColaRespuestaDebito, strBancoEmisor, objTransferencia, "Pago Servicio", strContrasena);
                        result = strRespuestaReverso;
                    }
                }
                else
                {
                    result = strRespuestaDebito;
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private string realizarPagoServicio(string strNombreColaEnvio, 
            string strNombreColaRespuesta, string strIdentificacionCliente, 
            string strNumeroSuministro, double dblMonto, 
            string strTipoSuministro, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(this.connectionString, strNombreColaEnvio);
            string strMensajeRecibido = string.Empty;
            string strTipo = "PagoServicio";
            string result;
            try
            {
                BrokeredMessage message = new BrokeredMessage("Hola");
                message.Properties["Tipo"] = strTipo;
                message.Properties["strIdentificacionCliente"] = strIdentificacionCliente;
                message.Properties["strNumeroSuministro"] = strNumeroSuministro;
                message.Properties["dblMonto"] = dblMonto;
                message.Properties["strTipoSuministro"] = strTipoSuministro;
                message.Properties["strContrasena"] = strContrasena;
                client.Send(message);
                strMensajeRecibido = clsFuncionesGenerales.RecibirRespuesta(strNombreColaRespuesta, strTipo);
                result = strMensajeRecibido;
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }
    }
}
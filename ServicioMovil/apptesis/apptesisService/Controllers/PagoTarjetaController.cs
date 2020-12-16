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
    public class PagoTarjetaController : ApiController
    {
        private string connectionString = Conexiones.busConnectionString;

        public string Get(string strNombreColaEnvio, string strNombreColaRespuesta, 
            string strIdentificacionCliente, string strNumeroTarjeta, 
            string dblPagoMinimo, string strNombreColaEnvioDebito, 
            string strNombreColaRespuestaDebito, string strBancoBeneficiario, 
            string strBancoEmisor, string jsonTransferenciaCifrada, string strContrasena)
        {
            string strRespuestaDebito = string.Empty;
            string strRespuestaReverso = string.Empty;
            string strRespuestaPagoTarjeta = string.Empty;
            string strNombreCliente = string.Empty;
            string jsonTransferenciaDescifrada = string.Empty;
            double dblPagoMinimoTarjeta = double.Parse(dblPagoMinimo);
            string result;
            try
            {
                jsonTransferenciaDescifrada = clsSeguridad.Desencriptar(jsonTransferenciaCifrada,strContrasena);
                clsTransfer objTransferencia = JsonConvert.DeserializeObject<clsTransfer>(jsonTransferenciaDescifrada);
                bool flag = objTransferencia.dblMonto >= dblPagoMinimoTarjeta;
                if (flag)
                {
                    strRespuestaDebito = clsFuncionesGenerales.realizarDebito(strNombreColaEnvioDebito, strNombreColaRespuestaDebito, strBancoBeneficiario, objTransferencia, "Tarjeta Credito",strContrasena);
                    bool flag2 = !strRespuestaDebito.ToUpper().Contains("ERROR");
                    if (flag2)
                    {
                        strRespuestaPagoTarjeta = this.realizarPagoTarjeta(strNombreColaEnvio, strNombreColaRespuesta, strIdentificacionCliente, strNumeroTarjeta, objTransferencia.dblMonto, strContrasena);
                        bool flag3 = !strRespuestaDebito.ToUpper().Contains("ERROR");
                        if (flag3)
                        {
                            result = strRespuestaPagoTarjeta;
                            return result;
                        }
                        strRespuestaReverso = clsFuncionesGenerales.realizarReverso(strNombreColaEnvioDebito, strNombreColaRespuestaDebito, strBancoEmisor, objTransferencia, "Tarjeta Credito",strContrasena);
                        result = strRespuestaReverso;
                        return result;
                    }
                }
                result = "Error: Monto no puede ser menor al pago minimo";
            }
            catch (Exception ex)
            {
                result = ex.Message;
                Console.WriteLine("Error: " + ex.Message);
            }
            return result;
        }

        private string realizarPagoTarjeta(string strNombreColaEnvio, 
            string strNombreColaRespuesta, string strIdentificacionCliente, 
            string strNumeroTarjeta, double dblMonto, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(this.connectionString, strNombreColaEnvio);
            string strMensajeRecibido = string.Empty;
            string strTipo = "PagoTarjeta";
            string result;
            try
            {
                BrokeredMessage message = new BrokeredMessage("Hola");
                message.Properties["Tipo"] = strTipo;
                message.Properties["strIdentificacionCliente"] = strIdentificacionCliente;
                message.Properties["strNumeroTarjeta"] = strNumeroTarjeta;
                message.Properties["dblMonto"] = dblMonto;
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

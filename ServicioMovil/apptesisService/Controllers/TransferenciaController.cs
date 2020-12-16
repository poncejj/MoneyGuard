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
    public class TransferenciaController : ApiController
    {
        private string connectionString = Conexiones.busConnectionString;

        public string Get(string strNombreColaEnvioDebito, 
            string strNombreColaRespuestaDebito, string strNombreColaEnvioCredito, 
            string strNombreColaRespuestaCredito, string strBancoBeneficiario, 
            string strBancoEmisor, string jsonTransferenciaCifrado, string strContrasena)
        {
            string strRespuestaDebito = string.Empty;
            string strRespuestaCredito = string.Empty;
            string strNombreCliente = string.Empty;
            string result = string.Empty;
            string jsonTransferenciaDescifrado = string.Empty;
            try
            {
                jsonTransferenciaDescifrado = clsSeguridad.Desencriptar(jsonTransferenciaCifrado,strContrasena);
                clsTransfer objTransferencia = JsonConvert.DeserializeObject<clsTransfer>(jsonTransferenciaDescifrado);
                bool flag = strNombreColaEnvioDebito != strNombreColaEnvioCredito;
                if (flag)
                {
                    strNombreCliente = this.consultarNombreClienteBeneficiario(strNombreColaEnvioCredito,
                        strNombreColaRespuestaCredito, objTransferencia.strIdentificacionBeneficiario, 
                        objTransferencia.strNumeroCuentaBeneficiario, strContrasena);
                    if (strNombreCliente.ToUpper().Contains("ERROR") || strNombreCliente.Length == 0)
                    {
                        result = "Alerta:El cliente beneficiario es inválido verifique ";
                        return result;
                    }
                }
                strRespuestaDebito = clsFuncionesGenerales.realizarDebito(strNombreColaEnvioDebito, strNombreColaRespuestaDebito, strBancoBeneficiario, objTransferencia, strNombreCliente,strContrasena);
                bool flag3 = strRespuestaDebito.ToUpper().Contains("ERROR");
                if (flag3)
                {
                    result = false.ToString();
                }
                else
                {
                    strRespuestaCredito = clsFuncionesGenerales.realizarCredito(strNombreColaEnvioCredito, strNombreColaRespuestaCredito, strBancoEmisor, objTransferencia, strNombreCliente, strContrasena);
                    bool flag4 = strRespuestaCredito.ToUpper().Contains("ERROR");
                    if (flag4)
                    {
                        
                        clsFuncionesGenerales.realizarReverso(strNombreColaEnvioDebito, strNombreColaRespuestaDebito, strBancoEmisor, objTransferencia, strNombreCliente, strContrasena);
                        string strResultadoTemp = strRespuestaCredito.ToUpper().Replace("ERROR","ALERTA");
                        result = strResultadoTemp.ToLower();
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private string consultarNombreClienteBeneficiario(string strNombreColaConsultaCliente,
            string strNombreColaRespuestaCliente, string strIdentificacionBeneficiario, 
            string strNumeroCuentaBeneficiario, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(this.connectionString, strNombreColaConsultaCliente);
            string strMensajeRecibido = string.Empty;
            string strTipo = "ConsultarNombreCliente";
            string result;
            try
            {
                BrokeredMessage message = new BrokeredMessage("Hola");
                message.Properties["Tipo"] = strTipo;
                message.Properties["strIdentificacionCliente"] = clsSeguridad.Encriptar(strIdentificacionBeneficiario, strContrasena);
                message.Properties["strNumeroCuentaCliente"] = clsSeguridad.Encriptar(strNumeroCuentaBeneficiario,strContrasena);
                message.Properties["strContrasena"] = strContrasena;
                client.Send(message);
                strMensajeRecibido = clsFuncionesGenerales.RecibirRespuesta(strNombreColaRespuestaCliente, strTipo);
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
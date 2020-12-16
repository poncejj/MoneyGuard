using apptesisService.Comun;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Web.Http;

namespace apptesisService.Controllers
{
    [MobileAppController]
    public class ConsultaCuentaController : ApiController
    {
        private string connectionString = Conexiones.busConnectionString;

        public string Get(string strNombreColaEnvio, string strNombreColaRespuesta, 
            string strTipo, string strIdentificacionCliente, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(this.connectionString, strNombreColaEnvio);
            string strMensajeRecibido = string.Empty;
            string result;
            try
            {
                BrokeredMessage message = new BrokeredMessage("Mensaje de envio");
                message.Properties["Tipo"] = strTipo;
                message.Properties["strIdentificacionCliente"] = strIdentificacionCliente;
                message.Properties["strContrasena"] = strContrasena;
                client.Send(message);
                strMensajeRecibido = clsFuncionesGenerales.RecibirRespuesta(strNombreColaRespuesta, strTipo);
            }
            catch (Exception ex)
            {
                result = "Error al obtener las cuentas registradas por usuario: " + ex.Message;
                return result;
            }
            result = strMensajeRecibido;
            return result;
        }
    }
}

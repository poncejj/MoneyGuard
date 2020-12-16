using apptesisService.Comun;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Web.Http;

namespace apptesisService.Controllers
{
    [MobileAppController]
    public class ConsultaMovimientoController : ApiController
    {
        private string connectionString = Conexiones.busConnectionString;

        public string Get(string strNombreColaEnvio, string strNombreColaRespuesta, 
            string strTipo, string strNumeroCuenta, int intCantidadMovimientos, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(this.connectionString, strNombreColaEnvio);
            string strMensajeRecibido = string.Empty;
            string result;
            try
            {
                BrokeredMessage message = new BrokeredMessage("Hola");
                message.Properties["Tipo"] = strTipo;
                message.Properties["NumeroCuentaCliente"] = strNumeroCuenta;
                message.Properties["intCantidadMovimientos"] = intCantidadMovimientos;
                message.Properties["strContrasena"] = strContrasena;
                client.Send(message);
                strMensajeRecibido = clsFuncionesGenerales.RecibirRespuesta(strNombreColaRespuesta, strTipo);
            }
            catch (Exception ex)
            {
                result = "Error al obtener los movimientos registrados en la cuenta: " + ex.Message;
                return result;
            }
            result = strMensajeRecibido;
            return result;
        }
    }
}

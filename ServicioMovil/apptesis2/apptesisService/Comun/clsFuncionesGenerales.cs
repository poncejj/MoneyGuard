using apptesisService.DataObjects;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;

namespace apptesisService.Comun
{
    public static class clsFuncionesGenerales
    {
        private static string connectionString = Conexiones.busConnectionString;

        public static string RecibirRespuesta(string strNombreColaRespuesta, string strTipo)
        {
            QueueClient Client = QueueClient.CreateFromConnectionString(clsFuncionesGenerales.connectionString, strNombreColaRespuesta);
            string strRespuesta = string.Empty;
            BrokeredMessage message;
            bool flag;
            do
            {
                message = Client.Receive(TimeSpan.FromSeconds(1.0));
                flag = (message != null);
            }
            while (!flag);
            try
            {
                strRespuesta = message.Properties["Respuesta"].ToString();
                message.Complete();
            }
            catch (Exception ex_55)
            {
                strRespuesta = "Error al obtener la respuesta";
                message.Abandon();
            }
            return strRespuesta;
        }

        public static string realizarDebito(string strNombreColaEnvioDebito, 
            string strNombreColaRespuestaDebito, string strBancoBeneficiario, 
            clsTransfer objTransferencia, string strNombreCliente, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(clsFuncionesGenerales.connectionString, strNombreColaEnvioDebito);
            string strMensajeRecibido = string.Empty;
            string strTipo = "RealizarDebito";
            string result;
            try
            {
                string strMensajeJson = JsonConvert.SerializeObject(objTransferencia);
                BrokeredMessage message = new BrokeredMessage("Hola");
                message.Properties["Tipo"] = strTipo;
                message.Properties["strIdentificacionCliente"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionEmisor, strContrasena);
                message.Properties["strNumeroCuentaCliente"] = clsSeguridad.Encriptar(objTransferencia.strNumeroCuentaEmisor, strContrasena);
                message.Properties["dblMonto"] = objTransferencia.dblMonto;
                message.Properties["strMotivo"] = objTransferencia.strMotivo;
                message.Properties["strNombreClienteBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionBeneficiario, strContrasena);
                message.Properties["strIdentificacionBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionBeneficiario, strContrasena);
                message.Properties["strNumeroCuentaBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strNumeroCuentaBeneficiario, strContrasena);
                message.Properties["strBancoBeneficiario"] = strBancoBeneficiario;
                message.Properties["strNombreCliente"] = clsSeguridad.Encriptar(strNombreCliente,strContrasena);
                message.Properties["strContrasena"] = strContrasena;
                client.Send(message);
                strMensajeRecibido = clsFuncionesGenerales.RecibirRespuesta(strNombreColaRespuestaDebito, strTipo);
                result = strMensajeRecibido;
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        public static string realizarCredito(string strNombreColaEnvioCredito, 
            string strNombreColaRespuestaCredito, string strBancoEmisor, 
            clsTransfer objTransferencia, string strNombreCliente, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(clsFuncionesGenerales.connectionString, strNombreColaEnvioCredito);
            string strMensajeRecibido = string.Empty;
            string strTipo = "RealizarCredito";
            string result;
            try
            {
                string strMensajeJson = JsonConvert.SerializeObject(objTransferencia);
                BrokeredMessage message = new BrokeredMessage("Hola");
                message.Properties["Tipo"] = strTipo;
                message.Properties["strIdentificacionCliente"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionBeneficiario,strContrasena);
                message.Properties["strNumeroCuentaCliente"] = clsSeguridad.Encriptar(objTransferencia.strNumeroCuentaBeneficiario, strContrasena);
                message.Properties["dblMonto"] = objTransferencia.dblMonto;
                message.Properties["strMotivo"] = objTransferencia.strMotivo;
                message.Properties["strNombreClienteBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionEmisor, strContrasena);
                message.Properties["strIdentificacionBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionEmisor, strContrasena);
                message.Properties["strNumeroCuentaBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strNumeroCuentaEmisor, strContrasena);
                message.Properties["strBancoEmisor"] = strBancoEmisor;
                message.Properties["strNombreCliente"] = strNombreCliente;
                message.Properties["strContrasena"] = strContrasena;
                client.Send(message);
                strMensajeRecibido = clsFuncionesGenerales.RecibirRespuesta(strNombreColaRespuestaCredito, strTipo);
                result = strMensajeRecibido;
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            return result;
        }

        public static string realizarReverso(string strNombreColaEnvioReverso, 
            string strNombreColaRespuestaReverso, string strBancoEmisor, 
            clsTransfer objTransferencia, string strNombreCliente, string strContrasena)
        {
            QueueClient client = QueueClient.CreateFromConnectionString(clsFuncionesGenerales.connectionString, strNombreColaEnvioReverso);
            string strMensajeRecibido = string.Empty;
            string strTipo = "RealizarReverso";
            string result;
            try
            {
                string strMensajeJson = JsonConvert.SerializeObject(objTransferencia);
                BrokeredMessage message = new BrokeredMessage("Hola");
                message.Properties["Tipo"] = strTipo;
                message.Properties["strIdentificacionCliente"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionEmisor, strContrasena);
                message.Properties["strNumeroCuentaCliente"] = clsSeguridad.Encriptar(objTransferencia.strNumeroCuentaEmisor, strContrasena);
                message.Properties["dblMonto"] = objTransferencia.dblMonto;
                message.Properties["strMotivo"] = objTransferencia.strMotivo;
                message.Properties["strNombreClienteBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionBeneficiario, strContrasena);
                message.Properties["strIdentificacionBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strIdentificacionBeneficiario, strContrasena);
                message.Properties["strNumeroCuentaBeneficiario"] = clsSeguridad.Encriptar(objTransferencia.strNumeroCuentaBeneficiario, strContrasena);
                message.Properties["strBancoBeneficiario"] = strBancoEmisor;
                message.Properties["strNombreCliente"] = clsSeguridad.Encriptar(strNombreCliente, strContrasena);
                message.Properties["strContrasena"] = strContrasena;
                client.Send(message);
                strMensajeRecibido = clsFuncionesGenerales.RecibirRespuesta(strNombreColaRespuestaReverso, strTipo);
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

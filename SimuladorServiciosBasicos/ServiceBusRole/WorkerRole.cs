using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace ServiceBusRole
{
    public class WorkerRole : RoleEntryPoint
    {
        // The name of your queue
        string ColaEnvio = Conexiones.ColaEnvio;
        string ColaRespuesta = Conexiones.ColaRespuesta;
        string connectionString = Conexiones.ConexionString;
        //string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
        QueueClient ClientEnvio = null;
        QueueClient ClientRespuesta = null;
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);

        public void ProcesarCola()
        {
            ClientEnvio = QueueClient.CreateFromConnectionString(connectionString, ColaEnvio);
            clsProcesadorMensajeria objProcesadorMensajeria = new clsProcesadorMensajeria();

            Console.WriteLine("Starting processing of messages");

            while(true)
            {
                BrokeredMessage message = ClientEnvio.Receive(TimeSpan.FromSeconds(1));
                if(message != null)
                {
                    try
                    {
                        string strRespuesta =  objProcesadorMensajeria.procesarMensaje(message.Properties["Tipo"].ToString(),message);
                        if (strRespuesta != null)
                        {
                            Console.WriteLine("Se recibio un mensaje...");
                            ProcesarRespuesta(strRespuesta);
                            message.Complete();
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Error al procesar consulta cuenta cliente: " + ex.Message);
                        message.Abandon();
                    }
                }
            } 
        }

        private void ProcesarRespuesta(string strRespuesta)
        {
            BrokeredMessage messageRespuesta = new BrokeredMessage();
            ClientRespuesta = QueueClient.CreateFromConnectionString(connectionString, ColaRespuesta);

            try
            {
                Console.WriteLine("Starting answering messages\n"+ strRespuesta);
                messageRespuesta.Properties["Respuesta"] = strRespuesta;
                ClientRespuesta.Send(messageRespuesta);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la respuesta del mensaje: " +ex.Message);
            }
        }

        public override bool OnStart()
        {
            try
            {
                // Set the maximum number of concurrent connections 
                ServicePointManager.DefaultConnectionLimit = 12;

                // Create the queue if it does not exist already
                var namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
                if (!namespaceManager.QueueExists(ColaEnvio))
                {
                    namespaceManager.CreateQueue(ColaEnvio);
                }

                // Iniciar a procesar la cola
                ProcesarCola();
                return base.OnStart();
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        public override void OnStop()
        {
            // Close the connection to Service Bus Queue
            ClientEnvio.Close();
            ClientRespuesta.Close();
            CompletedEvent.Set();
            base.OnStop();
        }
    }
}
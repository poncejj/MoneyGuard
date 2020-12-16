using System;
using Microsoft.WindowsAzure.MobileServices;
using MoneyGuard.Model;
using System.Threading.Tasks;
using System.Net.Http;
using MoneyGuard.Comun;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoneyGuard.Servicios
{
    public class clsServicioConsulta
    {
        string applicationURL = @"https://apptesis.azurewebsites.net";
        private string idDispositivo = Android.OS.Build.Serial;

        private MobileServiceClient client;

        public async Task<string> consultarServicios(string strIdentificacion, string strNumeroSuministro, string strTipoServicio)
        {
            var strNombreColaEnvio = clsConstantes.colasCooperativaEnvio(clsConstantes.obtenerIdCooperativa(strTipoServicio));
            var strNombreColaRespuesta = clsConstantes.colasCooperativaRecibir(clsConstantes.obtenerIdCooperativa(strTipoServicio));
            string strTipo = "ConsultaFactura";
            string strMensaje = string.Empty;
            var parametros = new Dictionary<string, string>();
            
            try
            {
                parametros.Add("strNombreColaEnvio", strNombreColaEnvio);
                parametros.Add("strNombreColaRespuesta", strNombreColaRespuesta);
                parametros.Add("strTipo", strTipo);
                parametros.Add("strIdentificacionCliente", clsSeguridad.Encriptar(strIdentificacion,idDispositivo));
                parametros.Add("strNumeroSuministro", clsSeguridad.Encriptar(strNumeroSuministro, idDispositivo));
                parametros.Add("strTipoServicio", strTipoServicio);
                parametros.Add("strContrasena", idDispositivo);

                client = new MobileServiceClient(applicationURL);
                var mensajeRecibido = await client.InvokeApiAsync("ConsultaFacturaServicio", HttpMethod.Get, parametros);
                strMensaje = mensajeRecibido.ToString();
                if(!strMensaje.ToUpper().Contains("ERROR"))
                {
                    strMensaje = clsSeguridad.Desencriptar(strMensaje,idDispositivo);
                }
                return strMensaje;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al consultar la factura del servicio: " + ex.Message);
                return null;
            }
        }

        public async Task<string> consultarCatalogo()
        {
            client = new MobileServiceClient(applicationURL);
            List<clsCatalogo> objCatalogo = new List<clsCatalogo>();
            string strRespuesta = string.Empty;
            try
            {
                objCatalogo = await client.GetTable<clsCatalogo>().ToListAsync();
                if(objCatalogo.Count > 0)
                {
                    strRespuesta = JsonConvert.SerializeObject(objCatalogo);
                }
                return strRespuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al consultar catalogo: " + ex.Message);
                return null;
            }
        }

        public async Task<string> consultarCooperativas()
        {
            client = new MobileServiceClient(applicationURL);
            List<clsCooperativa> objCooperativa = new List<clsCooperativa>();
            string strRespuesta = string.Empty;
            try
            {
                objCooperativa = await client.GetTable<clsCooperativa>().ToListAsync();
                if (objCooperativa.Count > 0)
                {
                    strRespuesta = JsonConvert.SerializeObject(objCooperativa);
                }
                return strRespuesta;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al consultar cooperativa: " + ex.Message);
                return null;
            }
        }
    }
}
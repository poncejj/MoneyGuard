using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using MoneyGuard.Model;
using System;
using MoneyGuard.Comun;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;

namespace MoneyGuard.Servicios
{
    public class clsServicioTransferencia
    {
        private const string applicationURL = @"https://apptesis.azurewebsites.net";
        private MobileServiceClient client;
        private string idDispositivo = Android.OS.Build.Serial;

        public static MobileServiceClient MobileService =
            new MobileServiceClient(applicationURL);

        public async Task<string> realizarTransferencia(clsTransferencia objTransferencia, clsCooperativa objCooperativaEmisor, clsCooperativa objCooperativaBeneficiario)
        {
            string strRespuesta = string.Empty;
            try
            {
                strRespuesta = await realizarTransferenciaCooperativas(objTransferencia,objCooperativaEmisor,objCooperativaBeneficiario);
                if(!strRespuesta.ToUpper().Contains("ALERTA"))
                {
                    Console.WriteLine("Respuesta transferencia " + strRespuesta);
                    objTransferencia.strTipo = "TRANSFERENCIA";
                    client = new MobileServiceClient(applicationURL);
                    await client.GetTable<clsTransferencia>().InsertAsync(objTransferencia);

                }

                return strRespuesta;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Alerta al realizar la transferencia: " + ex.Message);
                return "Alerta: " +ex.Message;
            }
        }

        private async Task<string> realizarTransferenciaCooperativas(clsTransferencia objTransferencia, clsCooperativa objCooperativaEmisor, clsCooperativa objCooperativaBeneficiario)
        {
            var strNombreColaEnvioDebito = objCooperativaEmisor.nombreColaEnvio;
            var strNombreColaRespuestaDebito = objCooperativaEmisor.nombreColaRespuesta;
            var strNombreColaEnvioCredito = objCooperativaBeneficiario.nombreColaEnvio;
            var strNombreColaRespuestaCredito = objCooperativaBeneficiario.nombreColaRespuesta;
            var strBancoBeneficiario = objTransferencia.strEntidadBeneficiario;
            var strBancoEmisor = objTransferencia.strEntidadEmisor;
            var parametros = new Dictionary<string, string>();
            try
            {
                string idTransferencia = DateTime.Now.ToString("DyyyyMMddhhmmssfff") + objTransferencia.strIdentificacionBeneficiario;
                objTransferencia.Id = idTransferencia;
                string jsonTransferencia = JsonConvert.SerializeObject(objTransferencia);
                parametros.Add("strNombreColaEnvioDebito", strNombreColaEnvioDebito);
                parametros.Add("strNombreColaRespuestaDebito", strNombreColaRespuestaDebito);
                parametros.Add("strNombreColaEnvioCredito", strNombreColaEnvioCredito);
                parametros.Add("strNombreColaRespuestaCredito", strNombreColaRespuestaCredito);
                parametros.Add("strBancoBeneficiario", strBancoBeneficiario);
                parametros.Add("strBancoEmisor", strBancoEmisor);
                parametros.Add("jsonTransferenciaCifrado", clsSeguridad.Encriptar(jsonTransferencia,idDispositivo));
                parametros.Add("strContrasena", idDispositivo);

                client = new MobileServiceClient(applicationURL);
                var mensajeRecibido = await client.InvokeApiAsync("Transferencia", HttpMethod.Get, parametros);
                Console.WriteLine("Se proceso la transferencia: " + mensajeRecibido);
                return mensajeRecibido.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al relizar la transferencia con la cooperativa: " + ex.Message);
                return "Alerta: " +ex.Message;
            }
        }

        public async Task<string> realizarPagoTarjeta(string strIdentificacion, string strNumeroTarjeta, clsTransferencia objTransferencia, double dblPagoMinimo, clsCooperativa objCooperativaEmisor, clsCooperativa objCooperativaBeneficiario)
        {
            var strNombreColaEnvioDebito = objCooperativaEmisor.nombreColaEnvio;
            var strNombreColaRespuestaDebito = objCooperativaEmisor.nombreColaRespuesta;
            var strNombreColaEnvio = objCooperativaBeneficiario.nombreColaEnvio;
            var strNombreColaRespuesta = objCooperativaBeneficiario.nombreColaRespuesta;
            var strBancoBeneficiario = objTransferencia.strEntidadBeneficiario;
            var strBancoEmisor = objTransferencia.strEntidadEmisor;
            var parametros = new Dictionary<string, string>();

            try
            {
                string jsonTransferencia = JsonConvert.SerializeObject(objTransferencia);

                parametros.Add("strNombreColaRespuesta", strNombreColaRespuesta);
                parametros.Add("strNombreColaEnvio", strNombreColaEnvio);
                parametros.Add("strIdentificacionCliente", clsSeguridad.Encriptar(strIdentificacion,idDispositivo));
                parametros.Add("strNumeroTarjeta", clsSeguridad.Encriptar(strNumeroTarjeta,idDispositivo));
                parametros.Add("dblPagoMinimo", dblPagoMinimo.ToString());
                parametros.Add("strNombreColaEnvioDebito", strNombreColaEnvioDebito);
                parametros.Add("strNombreColaRespuestaDebito", strNombreColaRespuestaDebito);
                parametros.Add("strBancoBeneficiario", strBancoBeneficiario);
                parametros.Add("strBancoEmisor", strBancoEmisor);
                parametros.Add("jsonTransferenciaCifrada", clsSeguridad.Encriptar(jsonTransferencia,idDispositivo));
                parametros.Add("strContrasena", idDispositivo);

                client = new MobileServiceClient(applicationURL);
                var mensajeRecibido = await client.InvokeApiAsync("PagoTarjeta", HttpMethod.Get, parametros);
                Console.WriteLine("Se proceso el pago de la tarjeta: " + mensajeRecibido);

                objTransferencia.strTipo = "PAGO TARJETA";
                await client.GetTable<clsTransferencia>().InsertAsync(objTransferencia);

                return mensajeRecibido.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al relizar pago de la tarjeta: " + ex.Message);
                return "Alerta: " + ex.Message;
            }
        }

        public async Task<string> realizarPagoServicios(string strIdentificacion, string strNumeroSuministro,string strTipoServicio, clsTransferencia objTransferencia, clsCooperativa objCooperativa)
        {
            var strNombreColaEnvioDebito = objCooperativa.nombreColaEnvio;
            var strNombreColaRespuestaDebito = objCooperativa.nombreColaRespuesta;
            var strNombreColaEnvio = clsConstantes.colasCooperativaEnvio(clsConstantes.obtenerIdCooperativa(strTipoServicio));
            var strNombreColaRespuesta = clsConstantes.colasCooperativaRecibir(clsConstantes.obtenerIdCooperativa(strTipoServicio));
            var strBancoBeneficiario = objTransferencia.strEntidadBeneficiario;
            var strBancoEmisor = objTransferencia.strEntidadEmisor;
            var parametros = new Dictionary<string, string>();

            try
            { 

                string jsonTransferencia = JsonConvert.SerializeObject(objTransferencia);
                Console.WriteLine("Transferencia" +jsonTransferencia);
                parametros.Add("strNombreColaEnvio", strNombreColaEnvio);
                parametros.Add("strNombreColaRespuesta", strNombreColaRespuesta);
                parametros.Add("strIdentificacionCliente", clsSeguridad.Encriptar(strIdentificacion,idDispositivo));
                parametros.Add("strNumeroSuministro", clsSeguridad.Encriptar(strNumeroSuministro,idDispositivo));
                parametros.Add("strNombreColaEnvioDebito", strNombreColaEnvioDebito);
                parametros.Add("strNombreColaRespuestaDebito", strNombreColaRespuestaDebito);
                parametros.Add("strBancoBeneficiario", strBancoBeneficiario);
                parametros.Add("strBancoEmisor", strBancoEmisor);
                parametros.Add("strTipoServicio", strTipoServicio);
                parametros.Add("jsonTransferenciaCifrada", clsSeguridad.Encriptar(jsonTransferencia,idDispositivo));
                parametros.Add("strContrasena", idDispositivo);

                client = new MobileServiceClient(applicationURL);
                var mensajeRecibido = await client.InvokeApiAsync("PagoServicio", HttpMethod.Get, parametros);
                Console.WriteLine("Se proceso el pago del servicio: " + mensajeRecibido);
                objTransferencia.strTipo = "PAGO SERVICIO";
                await client.GetTable<clsTransferencia>().InsertAsync(objTransferencia);
                return mensajeRecibido.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al relizar pago del servicio: " + ex.Message);
                return "Alerta: " + ex.Message;
            }
        }
    }
}
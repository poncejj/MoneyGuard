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
    public class clsServicioCuenta
    {
        string applicationURL = @"https://apptesis.azurewebsites.net";
        private MobileServiceClient client;
        private string idDispositivo = Android.OS.Build.Serial;

        public async Task<string> consultarCuentasRegistradas(string strIdentificacion, clsCooperativa objCooperativa)
        {
            var strNombreColaEnvio = objCooperativa.nombreColaEnvio;
            var strNombreColaRespuesta = objCooperativa.nombreColaRespuesta;
            string strTipo = "ConsultaCuenta";
            string strMensaje = string.Empty;
            var parametros = new Dictionary<string, string>();
            try
            {
                parametros.Add("strNombreColaEnvio", strNombreColaEnvio);
                parametros.Add("strNombreColaRespuesta", strNombreColaRespuesta);
                parametros.Add("strTipo", strTipo);
                parametros.Add("strIdentificacionCliente", clsSeguridad.Encriptar(strIdentificacion,idDispositivo));
                parametros.Add("strContrasena", idDispositivo);
                client = new MobileServiceClient(applicationURL);


                var mensajeRecibido = await client.InvokeApiAsync("ConsultaCuenta", HttpMethod.Get, parametros);
                strMensaje = mensajeRecibido.ToString();
                if (!strMensaje.ToUpper().Contains("ERROR"))
                {
                    strMensaje = clsSeguridad.Desencriptar(strMensaje, idDispositivo);
                }
                return strMensaje;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al registrar cuentas : " + ex.Message);
                return null;
            }
            
        }
        public async Task<string> consultarCuentasRegistradas(string strIdentificacion,string strIdUsuario, List<clsCooperativa> lstCooperativas)
        {
            List<clsCuenta> lstCuentaRespuesta = new List<clsCuenta>();
            List<clsTarjetaCedito> lstTarjetaRespuesta = new List<clsTarjetaCedito>();
            string strTipo = "ConsultaCuenta";
            try
            {
                List<string> lstIdCooperativas = await consultarCooperativasRegistradas(strIdUsuario);

                if(lstIdCooperativas.Count > 0)
                {
                    foreach (string idCooperativaRegistrada in lstIdCooperativas)
                    {
                        var strNombreColaEnvio = lstCooperativas.Find(x => x.Id == idCooperativaRegistrada).nombreColaEnvio;
                        var strNombreColaRespuesta = lstCooperativas.Find(x => x.Id == idCooperativaRegistrada).nombreColaRespuesta;
                        var parametros = new Dictionary<string, string>();

                        parametros.Add("strNombreColaEnvio", strNombreColaEnvio);
                        parametros.Add("strNombreColaRespuesta", strNombreColaRespuesta);
                        parametros.Add("strTipo", strTipo);
                        parametros.Add("strIdentificacionCliente", clsSeguridad.Encriptar(strIdentificacion,idDispositivo));
                        parametros.Add("strContrasena", idDispositivo);
                        client = new MobileServiceClient(applicationURL);
                        
                        var mensajeRecibido = await client.InvokeApiAsync("ConsultaCuenta", HttpMethod.Get, parametros);
                        Console.WriteLine("Parametros encriptados: " +parametros["strNombreColaEnvio"]+" " + parametros["strNombreColaRespuesta"] + " " + parametros["strTipo"] + " " + parametros["strIdentificacionCliente"] + " " + parametros["strContrasena"]);
                        Console.WriteLine("Respuesta encriptada: " +mensajeRecibido);
                        string strMensaje = clsSeguridad.Desencriptar(mensajeRecibido.ToString(),idDispositivo);
                        Console.WriteLine("Respuesta desencriptada: " +strMensaje);
                        string[] arrMensaje = strMensaje.Split('|');
                        string strMensajeCuentas = arrMensaje[0];
                        string strMensajeTarjetas = arrMensaje[1];
                        List<clsCuenta> lstCuentasObtenidas = JsonConvert.DeserializeObject<List<clsCuenta>>(strMensajeCuentas);
                        List<clsTarjetaCedito> lstTarjetasObtenidas = JsonConvert.DeserializeObject<List<clsTarjetaCedito>>(strMensajeTarjetas);

                        if (lstCuentasObtenidas.Count > 0)
                        {
                            foreach (clsCuenta objCuentaResultado in lstCuentasObtenidas)
                            {
                                if(!lstCuentaRespuesta.Contains(objCuentaResultado))
                                {
                                    objCuentaResultado.idCooperativa = idCooperativaRegistrada;
                                    lstCuentaRespuesta.Add(objCuentaResultado);
                                }
                            }
                        }
                        if(lstTarjetasObtenidas.Count > 0)
                        {
                            foreach (clsTarjetaCedito objTarjetaResultado in lstTarjetasObtenidas)
                            {
                                if (!lstTarjetaRespuesta.Contains(objTarjetaResultado))
                                {
                                    objTarjetaResultado.idCooperativa = idCooperativaRegistrada;
                                    lstTarjetaRespuesta.Add(objTarjetaResultado);
                                }
                            }
                        }
                    }
                }
                string strRespuestaCuentas = JsonConvert.SerializeObject(lstCuentaRespuesta);
                string strRespuestaTarjetas = JsonConvert.SerializeObject(lstTarjetaRespuesta);
                string strRespuestaDesencriptada = strRespuestaCuentas + "|" + strRespuestaTarjetas;
                return strRespuestaDesencriptada;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al consultar cuentas existentes: " + ex.Message);
                return null;
            }
        }

        public async Task<string> consultaMovimientos(string strNumeroCuentaSeleccionada, int intCantidadMovimiemtos, string strNombreColaEnvio, string strNombreColaRespuesta)
        {
            string strTipo = "ConsultaMovimientos";
            var parametros = new Dictionary<string, string>();
            try
            {
                parametros.Add("strNombreColaEnvio", strNombreColaEnvio);
                parametros.Add("strNombreColaRespuesta", strNombreColaRespuesta);
                parametros.Add("strTipo", strTipo);
                parametros.Add("strNumeroCuenta", clsSeguridad.Encriptar(strNumeroCuentaSeleccionada,idDispositivo));
                parametros.Add("intCantidadMovimientos", intCantidadMovimiemtos.ToString());
                parametros.Add("strContrasena", idDispositivo);
                client = new MobileServiceClient(applicationURL);

                var mensajeRecibido = await client.InvokeApiAsync("ConsultaMovimiento", HttpMethod.Get, parametros);
                string strMensaje = mensajeRecibido.ToString();
                if (!strMensaje.ToUpper().Contains("ERROR"))
                {
                    strMensaje = clsSeguridad.Desencriptar(strMensaje, idDispositivo);
                }
                return strMensaje;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al consultar los movimientos: " + ex.Message);
                return null;
            }
        }

        private async Task<List<string>> consultarCooperativasRegistradas(string strIdUsuario)
        {
            client = new MobileServiceClient(applicationURL);
            List<string> lstIdCooperativasUsuario = new List<string>();
            try
            {
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("idUsuario", strIdUsuario);

                List<clsCooperativaCliente> tabla = await client.GetTable<clsCooperativaCliente>().WithParameters(parametros).ToListAsync();

                foreach (clsCooperativaCliente objCooperativaCliente in tabla)
                {
                    if (!lstIdCooperativasUsuario.Contains(objCooperativaCliente.intCooperativa))
                    {
                        if(objCooperativaCliente.idUsuario.Equals(strIdUsuario))
                        {
                            lstIdCooperativasUsuario.Add(objCooperativaCliente.intCooperativa);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Alerta al consultar cooperativa: " + ex.Message);
            }
            return lstIdCooperativasUsuario;
        }

        public async Task<bool> registrarCooperativa(string idUsuario, string idCooperativa)
        {
            Random rnd = new Random();
            client = new MobileServiceClient(applicationURL);
            clsCooperativaCliente objCooperativaCliente = new clsCooperativaCliente();
            objCooperativaCliente.idUsuario = idUsuario;
            objCooperativaCliente.intCooperativa = idCooperativa;
            objCooperativaCliente.Id = idUsuario + rnd.Next(1,100);
            try
            {
                await client.GetTable<clsCooperativaCliente>().InsertAsync(objCooperativaCliente);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al registrar cooperativa usuario {0},{1}: " + ex.Message,idUsuario,idCooperativa);
                return false;
            }
        }

        public string unirCuentasJson(string strCuentaJsonAntigua, string strCuentaJsonNueva, string idCooperativaNueva)
        {
            List<clsCuenta> lstCuentaRespuesta = new List<clsCuenta>();
            List<clsTarjetaCedito> lstTarjetaRespuesta = new List<clsTarjetaCedito>();

            try
            {
                string[] arrRespuestaAntigua = strCuentaJsonAntigua.Split('|');
                string strCuentasAntiguas = arrRespuestaAntigua[0];
                string strTarjetasAntiguas = arrRespuestaAntigua[1];
                List<clsCuenta> lstCuentasAntiguas = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentasAntiguas);
                List<clsTarjetaCedito> lstTarjetasAntiguas = JsonConvert.DeserializeObject<List<clsTarjetaCedito>>(strTarjetasAntiguas);

                string[] arrRespuestaNueva = strCuentaJsonNueva.Split('|');
                string strCuentasNuevas = arrRespuestaNueva[0];
                string strTarjetasNuevas = arrRespuestaNueva[1];
                List<clsCuenta> lstCuentasNuevas = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentasNuevas);
                List<clsTarjetaCedito> lstTarjetasNuevas = JsonConvert.DeserializeObject<List<clsTarjetaCedito>>(strTarjetasNuevas);

                if(lstCuentasAntiguas.Count > 0)
                {
                    foreach(clsCuenta objCuenta in lstCuentasAntiguas)
                    {
                        lstCuentaRespuesta.Add(objCuenta);
                    }
                }

                if (lstTarjetasAntiguas.Count > 0)
                {
                    foreach (clsTarjetaCedito objTarjeta in lstTarjetasAntiguas)
                    {
                        objTarjeta.idCooperativa = idCooperativaNueva;
                        lstTarjetaRespuesta.Add(objTarjeta);
                    }
                }

                if (lstCuentasNuevas.Count > 0)
                {
                    foreach (clsCuenta objCuenta in lstCuentasNuevas)
                    {
                        objCuenta.idCooperativa = idCooperativaNueva;
                        lstCuentaRespuesta.Add(objCuenta);
                    }
                }

                if (lstTarjetasNuevas.Count > 0)
                {
                    foreach (clsTarjetaCedito objTarjeta in lstTarjetasNuevas)
                    {
                        lstTarjetaRespuesta.Add(objTarjeta);
                    }
                }

            }
            catch (Exception)
            {
                return "Alerta al unir las cuentas";
            }
            string strRespuestaCuentas = JsonConvert.SerializeObject(lstCuentaRespuesta);
            string strRespuestaTarjetas = JsonConvert.SerializeObject(lstTarjetaRespuesta);
            string strRespuestaDesencriptada = strRespuestaCuentas + "|" + strRespuestaTarjetas;
            return strRespuestaDesencriptada;
        }
    }
}
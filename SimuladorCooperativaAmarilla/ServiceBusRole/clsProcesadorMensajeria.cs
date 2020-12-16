using System;
using Microsoft.ServiceBus.Messaging;
using SimuladorCooperativaAmarilla.ModeloDatos;
using Newtonsoft.Json;
using SimuladorCooperativaAmarilla.Logica;
using System.Collections.Generic;
using SimuladorCooperativaAmarilla.Comun;

namespace SimuladorCooperativaAmarilla.ServiceBusRole
{
    public class clsProcesadorMensajeria
    {
        public string procesarMensaje(string strTipo, BrokeredMessage message)
        {
            clsLogicaCuenta objLogicaCuenta = new clsLogicaCuenta();
            clsLogicaTransferencia objLogicaTransferencia = new clsLogicaTransferencia();
            clsLogicaCliente objLogicaCliente = new clsLogicaCliente();
            List<clsCuenta> lstCuentasRespuesta = new List<clsCuenta>();
            List<clsTarjetaCedito> lstTarjetasRespuesta = new List<clsTarjetaCedito>();
            clsMovimiento objMovimiento = new clsMovimiento();
            string strRespuesta = string.Empty;
            switch (strTipo)
            {
                case "ConsultaCuenta":
                    try
                    {
                        string strMovimientos = string.Empty;
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        lstCuentasRespuesta = objLogicaCuenta.consultaCuenta(strIdentificacionCliente);
                        if (lstCuentasRespuesta.Count > 0)
                        {
                            lstTarjetasRespuesta = objLogicaCuenta.consultaTarjeta(strIdentificacionCliente);
                            strRespuesta = JsonConvert.SerializeObject(lstCuentasRespuesta);
                            strRespuesta += "|" + JsonConvert.SerializeObject(lstTarjetasRespuesta);
                            strRespuesta = clsSeguridad.Encriptar(strRespuesta, strContrasena);
                        }
                        else
                        {
                            strRespuesta = "Error no existe cuentas registradas con esa identificacion";


                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al consultar cuentas o tarjetas: " + ex.Message);
                        strRespuesta = "Error al consultar cuentas o tarjetas: " + ex.Message;
                    }
                    break;
                case "ConsultaCuentaCliente":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["IdentificacionCliente"].ToString();
                        string strNumeroCuentaClienteTemp = message.Properties["NumeroCuentaCliente"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        string strNumeroCuentaCliente = clsSeguridad.Desencriptar(strNumeroCuentaClienteTemp, strContrasena);


                        clsCuenta cuenta = new clsCuenta();
                        cuenta = objLogicaCuenta.consultaCuentaCliente(strIdentificacionCliente, strNumeroCuentaCliente);
                        if (cuenta.numero_cuenta != "0")
                        {
                            strRespuesta = JsonConvert.SerializeObject(cuenta);
                            strRespuesta = clsSeguridad.Encriptar(strRespuesta, strContrasena);
                        }
                        else
                        {
                            strRespuesta = "No existen datos de la cuenta";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al consultar cuentas: " + ex.Message);
                        strRespuesta = "Error al consultar cuentas: " + ex.Message;
                    }

                    break;
                case "ConsultaMovimientos":
                    try
                    {
                        string strNumeroCuentaClienteTemp = message.Properties["NumeroCuentaCliente"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        int intCantidad = int.Parse(message.Properties["intCantidadMovimientos"].ToString());
                        string strNumeroCuentaCliente = clsSeguridad.Desencriptar(strNumeroCuentaClienteTemp, strContrasena);

                        List<clsMovimiento> lstMovimientos = new List<clsMovimiento>();
                        lstMovimientos = objLogicaCuenta.consultaMovimientos(strNumeroCuentaCliente, intCantidad);
                        if (lstMovimientos.Count > 0)
                        {
                            strRespuesta = JsonConvert.SerializeObject(lstMovimientos);
                            strRespuesta = clsSeguridad.Encriptar(strRespuesta,strContrasena);
                        }
                        else
                        {
                            strRespuesta = "No existen datos de la cuenta";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al consultar movimientos: " + ex.Message);
                        strRespuesta = "Error al consultar movimientos: " + ex.Message;
                    }
                    break;
                case "RealizarDebito":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strNumeroCuentaClienteTemp = message.Properties["strNumeroCuentaCliente"].ToString();
                        double dblMonto = double.Parse(message.Properties["dblMonto"].ToString());
                        string strMotivo = message.Properties["strMotivo"].ToString();
                        string strNombreClienteBeneficiarioTemp = message.Properties["strNombreClienteBeneficiario"].ToString();
                        string strIdentificacionBeneficiarioTemp = message.Properties["strIdentificacionBeneficiario"].ToString();
                        string strNumeroCuentaBeneficiarioTemp = message.Properties["strNumeroCuentaBeneficiario"].ToString();
                        string strBancoBeneficiario = message.Properties["strBancoBeneficiario"].ToString();
                        string strNombreClienteTemp = message.Properties["strNombreCliente"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        string strNombreCliente = clsSeguridad.Desencriptar(strNombreClienteTemp, strContrasena);
                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        string strNumeroCuentaCliente = clsSeguridad.Desencriptar(strNumeroCuentaClienteTemp, strContrasena);
                        string strNombreClienteBeneficiario = clsSeguridad.Desencriptar(strNombreClienteBeneficiarioTemp, strContrasena);
                        string strIdentificacionBeneficiario = clsSeguridad.Desencriptar(strIdentificacionBeneficiarioTemp, strContrasena);
                        string strNumeroCuentaBeneficiario = clsSeguridad.Desencriptar(strNumeroCuentaBeneficiarioTemp, strContrasena);

                        objMovimiento.descripcion_movimiento = strMotivo;
                        objMovimiento.estado_movimiento = true;
                        objMovimiento.identificacion_cliente_beneficiario = strIdentificacionBeneficiario;
                        objMovimiento.lugar_movimiento = "AppMovil";
                        objMovimiento.monto_movimiento = dblMonto;
                        objMovimiento.nombre_banco_beneficiario = strBancoBeneficiario;
                        if (strNombreClienteBeneficiario == strIdentificacionBeneficiario)
                        {
                            objMovimiento.nombre_cliente_beneficiario = objLogicaCliente.consultarNombreCliente(strIdentificacionBeneficiario, strNumeroCuentaBeneficiario);
                        }
                        else
                        {
                            objMovimiento.nombre_cliente_beneficiario = strNombreCliente;
                        }
                        objMovimiento.tipo_transaccion = 'D';
                        strRespuesta = objLogicaTransferencia.realizarDebito(objMovimiento, strNumeroCuentaCliente, strIdentificacionCliente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al realizar el debito: " + ex.Message);
                        strRespuesta = "Error al realizar el debito: " + ex.Message;
                    }
                    break;
                case "RealizarCredito":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strNumeroCuentaClienteTemp = message.Properties["strNumeroCuentaCliente"].ToString();
                        double dblMonto = double.Parse(message.Properties["dblMonto"].ToString());
                        string strMotivo = message.Properties["strMotivo"].ToString();
                        string strNombreClienteBeneficiarioTemp = message.Properties["strNombreClienteBeneficiario"].ToString();
                        string strIdentificacionBeneficiarioTemp = message.Properties["strIdentificacionBeneficiario"].ToString();
                        string strNumeroCuentaBeneficiarioTemp = message.Properties["strNumeroCuentaBeneficiario"].ToString();
                        string strBancoBeneficiario = message.Properties["strBancoEmisor"].ToString();
                        string strNombreClienteTemp = message.Properties["strNombreCliente"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        string strNombreCliente = clsSeguridad.Desencriptar(strNombreClienteTemp, strContrasena);
                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        string strNumeroCuentaCliente = clsSeguridad.Desencriptar(strNumeroCuentaClienteTemp, strContrasena);
                        string strNombreClienteBeneficiario = clsSeguridad.Desencriptar(strNombreClienteBeneficiarioTemp, strContrasena);
                        string strIdentificacionBeneficiario = clsSeguridad.Desencriptar(strIdentificacionBeneficiarioTemp, strContrasena);
                        string strNumeroCuentaBeneficiario = clsSeguridad.Desencriptar(strNumeroCuentaBeneficiarioTemp, strContrasena);

                        objMovimiento.descripcion_movimiento = strMotivo;
                        objMovimiento.estado_movimiento = true;
                        objMovimiento.identificacion_cliente_beneficiario = strIdentificacionBeneficiario;
                        objMovimiento.lugar_movimiento = "AppMovil";
                        objMovimiento.monto_movimiento = dblMonto;
                        objMovimiento.nombre_banco_beneficiario = strBancoBeneficiario;
                        if (strNombreClienteBeneficiario == strIdentificacionBeneficiario)
                        {
                            objMovimiento.nombre_cliente_beneficiario = objLogicaCliente.consultarNombreCliente(strIdentificacionBeneficiario, strNumeroCuentaBeneficiario);
                        }
                        else
                        {
                            objMovimiento.nombre_cliente_beneficiario = strNombreCliente;
                        }

                        objMovimiento.tipo_transaccion = 'C';
                        strRespuesta = objLogicaTransferencia.realizarCredito(objMovimiento, strNumeroCuentaCliente, strIdentificacionCliente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al realizar el credito: " + ex.Message);
                        strRespuesta = "Error al realizar el credito: " + ex.Message;
                    }
                    break;
                case "ConsultarNombreCliente":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strNumeroCuentaClienteTemp = message.Properties["strNumeroCuentaCliente"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();
                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        string strNumeroCuentaCliente = clsSeguridad.Desencriptar(strNumeroCuentaClienteTemp, strContrasena);

                        strRespuesta = objLogicaCliente.consultarNombreCliente(strIdentificacionCliente, strNumeroCuentaCliente);
                        strRespuesta = clsSeguridad.Encriptar(strRespuesta, strContrasena);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al consultar nombre del cliente: " + ex.Message);
                        strRespuesta = "Error al consultar nombre del cliente: " + ex.Message;
                    }

                    break;
                case "RealizarReverso":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strNumeroCuentaClienteTemp = message.Properties["strNumeroCuentaCliente"].ToString();
                        double dblMonto = double.Parse(message.Properties["dblMonto"].ToString());
                        string strMotivo = message.Properties["strMotivo"].ToString();
                        string strNombreClienteBeneficiarioTemp = message.Properties["strNombreClienteBeneficiario"].ToString();
                        string strIdentificacionBeneficiarioTemp = message.Properties["strIdentificacionBeneficiario"].ToString();
                        string strNumeroCuentaBeneficiarioTemp = message.Properties["strNumeroCuentaBeneficiario"].ToString();
                        string strBancoBeneficiario = message.Properties["strBancoBeneficiario"].ToString();
                        string strContrasena = message.Properties["strContrasena"].ToString();

                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        string strNumeroCuentaCliente = clsSeguridad.Desencriptar(strNumeroCuentaClienteTemp, strContrasena);
                        string strNombreClienteBeneficiario = clsSeguridad.Desencriptar(strNombreClienteBeneficiarioTemp, strContrasena);
                        string strIdentificacionBeneficiario = clsSeguridad.Desencriptar(strIdentificacionBeneficiarioTemp, strContrasena);
                        string strNumeroCuentaBeneficiario = clsSeguridad.Desencriptar(strNumeroCuentaBeneficiarioTemp, strContrasena);


                        objMovimiento.descripcion_movimiento = strMotivo;
                        objMovimiento.estado_movimiento = true;
                        objMovimiento.identificacion_cliente_beneficiario = strIdentificacionBeneficiario;
                        objMovimiento.lugar_movimiento = "AppMovil";
                        objMovimiento.monto_movimiento = dblMonto;
                        objMovimiento.nombre_banco_beneficiario = strBancoBeneficiario;
                        if (strNombreClienteBeneficiario == strIdentificacionBeneficiario)
                        {
                            objMovimiento.nombre_cliente_beneficiario = objLogicaCliente.consultarNombreCliente(strIdentificacionBeneficiario, strNumeroCuentaBeneficiario);
                        }
                        objMovimiento.tipo_transaccion = 'C';
                        strRespuesta = objLogicaTransferencia.realizarDebito(objMovimiento, strNumeroCuentaCliente, strIdentificacionCliente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al realizar reverso de la transaccion: " + ex.Message);
                        strRespuesta = "Error al realizar reverso de la transaccion: " + ex.Message;
                    }
                    break;
                case "PagoTarjeta":
                    try
                    {
                        string strIdentificacionClienteTemp = message.Properties["strIdentificacionCliente"].ToString();
                        string strNumeroTarjetaTemp = message.Properties["strNumeroTarjeta"].ToString();
                        double dblMonto = double.Parse(message.Properties["dblMonto"].ToString());
                        string strContrasena = message.Properties["strContrasena"].ToString();

                        string strIdentificacionCliente = clsSeguridad.Desencriptar(strIdentificacionClienteTemp, strContrasena);
                        string strNumeroTarjeta = clsSeguridad.Desencriptar(strNumeroTarjetaTemp, strContrasena);

                        if (objLogicaCuenta.pagarTarjetaCredito(strIdentificacionCliente, strNumeroTarjeta, dblMonto))
                        {
                            strRespuesta = "Pago de tarjeta realizado con exito!";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al consultar movimientos: " + ex.Message);
                        strRespuesta = "Error al pagar Tarjeta: " + ex.Message;
                    }
                    break;
            }
            return strRespuesta;
        }
    }
}

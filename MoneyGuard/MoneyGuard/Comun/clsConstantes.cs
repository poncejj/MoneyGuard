using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MoneyGuard.Comun
{
    public class clsConstantes
    {
        public const string strTipoTransaccion = "Tipo";
        public const string strNuevaCuenta = "NuevaCuenta"; 
        public const string strCuentaExistente = "CuentaExistente";

        public const string strIdUsuario = "idUsuario";
        public const string strNombreUsuario = "nombreUsuario";
        public const string strFechaNacimiento = "fechaNacimientoUsuario";
        public const string strEstadoCivilUsuario = "estadoCivilUsuario";
        public const string strGeneroUsuario = "generoUsuario";
        public const string strFacturaServicio = "facturaServicio";
        public const string strURLImagenUsuario = "urlImage";
        public const string strIdentificacionUsuario = "identificacionUsuario";
        public const string strPINUsuario = "pinUsuario";
        public const string strCuentaJson = "cuentaJSON";
        public const string idCooperativa = "idCooperativa";

        public const string strNumeroCuentaSeleccionada = "numeroCuentaSeleccionada";
        public const string strNumeroTarjetaSeleccionada = "numeroTarjetaSeleccionada";
        public const string strIdentificacionCooperativa = "identificacionCooperativa";
        public const string strCooperativas = "cooperativas";

        public static string ObtenerGeneroUsuario(int intGender)
        {
            string strRetorno = string.Empty;
            switch (intGender)
            {
                case 0:
                    strRetorno += "Male";
                    break;
                case 1:
                    strRetorno += "Female";
                    break;
                case 2:
                    strRetorno += "Other";
                    break;
                default:
                    strRetorno += "Unknown";
                    break;
            }
            return strRetorno;
        }

        public static string ObtenerEstadoCivilUsuario(int relationshipStatus)
        {
            string strRetorno = string.Empty;
            switch (relationshipStatus)
            {
                case 0:
                    strRetorno += "Single";
                    break;
                case 1:
                    strRetorno += "In a relationship";
                    break;
                case 2:
                    strRetorno += "Engaged";
                    break;
                case 3:
                    strRetorno += "Married";
                    break;
                case 4:
                    strRetorno += "It's compliated";
                    break;
                case 5:
                    strRetorno += "Windowed";
                    break;
                case 6:
                    strRetorno += "In a open relationship";
                    break;
                case 7:
                    strRetorno += "In a domestic partnership";
                    break;
                case 8:
                    strRetorno += "In a civil union";
                    break;
                default:
                    strRetorno += "Unknown";
                    break;
            }
            return strRetorno;
        }

        public static string nombreCooperativa(int idCooperativa)
        {
            switch(idCooperativa)
            {
                case 1:
                    return "Cooperativa Amarilla";
                case 2:
                    return "Cooperativa Verde";
            }
            return null;
        }

        public static int obtenerIdCooperativa(string strNombreCooperativa)
        {
            switch (strNombreCooperativa)
            {
                case "Cooperativa Amarilla":
                    return 1;
                case "Cooperativa Verde":
                    return 2;
                case "Luz":
                    return 3;
                case "Agua":
                    return 3;
                case "Telefono":
                    return 3;
            }
            return 0;
        }

        public static string colasCooperativaEnvio(int idCooperativa)
        {
            switch (idCooperativa)
            {
                case 1:
                    return "enviarcooperativaamarilla";
                case 2:
                    return "enviarcooperativaverde";
                case 3:
                    return "enviarserviciosbasicos";
            }
            return null;
        }
        public static string colasCooperativaRecibir(int idCooperativa)
        {
            switch (idCooperativa)
            {
                case 1:
                    return "recibircooperativaamarilla";
                case 2:
                    return "recibircooperativaverde";
                case 3:
                    return "recibirserviciosbasicos";
            }
            return null;
        }
    }
}
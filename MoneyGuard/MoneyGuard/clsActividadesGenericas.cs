using System;
using System.Threading.Tasks;
using MoneyGuard.Servicios;
using MoneyGuard.Model;
using System.Collections.Generic;

namespace MoneyGuard
{
    public static class clsActividadesGenericas
    {
        public static async Task<string> modificarCuentaJson(string strIdentificacion, string strIdUsuario, List<clsCooperativa> lstCooperativas)
        {
            clsServicioCuenta objServicioCuenta = new clsServicioCuenta();
            string strCuentaJsonTemp = string.Empty;
            try
            {
                strCuentaJsonTemp = await objServicioCuenta.consultarCuentasRegistradas(strIdentificacion, strIdUsuario, lstCooperativas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta " + ex.Message);

            }
            return strCuentaJsonTemp;
        }
    }
}
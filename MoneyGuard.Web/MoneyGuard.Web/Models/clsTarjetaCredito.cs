using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsTarjetaCredito
    {
        public int idTarjetaCredito { get; set; }
        public string strNombreCliente { get; set; }
        public string strNumeroCuenta { get; set; }
        public string strNumeroTarjeta { get; set; }
        public string strMarcaTarjeta { get; set; }
        public double dblCupoTarjeta { get; set; }
    }

    public class clsSaldoTarjetaCredito
    {
        public int idSaldoTarjetaCredito { get; set; }
        public string strNumeroTarjeta { get; set; }
        public double dblCupoTarjeta { get; set; }
        public double dblSaldoTarjeta { get; set; }
        public double dblConsumoTarjeta { get; set; }
        public double dblMinimoPagar { get; set; }
        public string strFechaPago { get; set; }
        public string strFechaCorte { get; set; }
    }
}
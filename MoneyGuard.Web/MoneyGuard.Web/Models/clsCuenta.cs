using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsCuenta
    {
        public int idCuenta { get; set; }
        public int idCliente { get; set; }
        public string numeroCuenta { get; set; }
        public string nombreCliente { get; set; }
        public string tipoCuenta { get; set; }
        public double saldo { get; set; }
        public bool estado { get; set; }
    }
}
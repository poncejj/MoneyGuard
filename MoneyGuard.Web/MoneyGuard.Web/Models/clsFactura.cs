using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsFactura
    {
        public int idFactura { get; set; }
        public string numeroSuministro { get; set; }
        public double valorFactura { get; set; }
        public string fechaEmision { get; set; }
        public string fechaVencimiento { get; set; }
        public double valorPendiente { get; set; }
        public bool valorPagado { get; set; }
    }
}
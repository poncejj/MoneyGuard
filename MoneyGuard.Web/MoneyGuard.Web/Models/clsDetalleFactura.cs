using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsDetalleFactura
    {
        public int idDetalleFactura { get; set; }
        public int idFactura { get; set; }
        public string descripcionDetalleFactura { get; set; }
        public double valorDetalleFactura { get; set; }
    }
}
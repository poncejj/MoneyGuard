using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsCliente
    {
        public int idCliente { get; set; }
        public string identificacionCliente { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public string telefonoCliente { get; set; }
        public string emailCliente { get; set; }

    }
}
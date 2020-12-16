using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsUsuarioApp
    {
        public string idUsuarioApp { get; set; }
        public string nombreUsuarioApp { get; set; }
        public string identificacionUsuarioApp { get; set; }
        public string pinUsuarioApp { get; set; }
        public string fechaCreacionUsuarioApp { get; set; }
        public bool eliminadoUsuarioApp { get; set; }
    }
}
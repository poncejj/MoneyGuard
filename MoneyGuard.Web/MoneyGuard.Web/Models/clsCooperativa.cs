using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsCooperativa
    {
        public string idCooperativa { get; set; }
        public string nombreCooperativa { get; set; }
        public string nombreColaEnvio { get; set; }
        public string nombreColaRespuesta { get; set; }
        public string nombreBusServicios { get; set; }
        public string fechaCreacionCooperativa { get; set; }
        public bool estadoCooperativa { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsCatalogo
    {
        public string idCatalogo { get; set; }
        public string nombreCatalogo { get; set; }
        public bool catalogoPadre { get; set; }
        public string idCatalogoPadre { get; set; }
        public string fechaCreacionCatalogo { get; set; }
        public bool estadoCatalogo { get; set; }
    }
}
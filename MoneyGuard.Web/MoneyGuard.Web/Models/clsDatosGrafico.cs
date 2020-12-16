using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsDatosGrafico
    {
        public string label { get; set; }
        public int value { get; set; }

        public clsDatosGrafico(string label, int value)
        {
            this.label = label;
            this.value = value;
        }
    }
}
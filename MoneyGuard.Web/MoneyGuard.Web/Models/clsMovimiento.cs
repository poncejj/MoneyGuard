using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsMovimiento
    {
        public string idMovimiento { get; set; }
        public string identificacionBeneficiario { get; set; }
        public string entidadBeneficiario { get; set; }
        public string numeroCuentaBeneficiario { get; set; }
        public string Tipo { get; set; }
        public double montoMovimiento { get; set; }
        public double saldoDisponible { get; set; }
        public string motivoMovimiento { get; set; }
        public string lugarMovimiento { get; set; }
        public string fechaMovimiento { get; set; }
    }

    public class clsTransaccion
    {
        public string idMovimiento { get; set; }
        public string identificacionBeneficiario { get; set; }
        public string entidadBeneficiario { get; set; }
        public string numeroCuentaBeneficiario { get; set; }
        public string identificacionEmisor { get; set; }
        public string entidadEmisor { get; set; }
        public string numeroCuentaEmsior { get; set; }
        public string Tipo { get; set; }
        public double montoMovimiento { get; set; }
        public double saldoDisponible { get; set; }
        public string motivoMovimiento { get; set; }
        public string lugarMovimiento { get; set; }
        public string fechaMovimiento { get; set; }
    }
}
using Microsoft.Azure.Mobile.Server;
using System;

namespace apptesisService.DataObjects
{
    public class clsTransferencia : EntityData
    {
        public string strIdentificacionEmisor
        {
            get;
            set;
        }

        public string strNumeroCuentaEmisor
        {
            get;
            set;
        }

        public string strEntidadEmisor
        {
            get;
            set;
        }

        public string strIdentificacionBeneficiario
        {
            get;
            set;
        }

        public string strTipoCuentaBeneficiario
        {
            get;
            set;
        }

        public string strNumeroCuentaBeneficiario
        {
            get;
            set;
        }

        public string strEntidadBeneficiario
        {
            get;
            set;
        }

        public double dblMonto
        {
            get;
            set;
        }

        public string strMotivo
        {
            get;
            set;
        }

        public string strTipo { get; set; }
    }
}

using System;
using System.Runtime.Serialization;

namespace apptesisService.DataObjects
{
    [DataContract]
    public class clsTransfer
    {
        [DataMember]
        public string strIdentificacionEmisor
        {
            get;
            set;
        }

        [DataMember]
        public string strNumeroCuentaEmisor
        {
            get;
            set;
        }

        [DataMember]
        public string intEntidadEmisor
        {
            get;
            set;
        }

        [DataMember]
        public string strIdentificacionBeneficiario
        {
            get;
            set;
        }

        [DataMember]
        public string strTipoCuentaBeneficiario
        {
            get;
            set;
        }

        [DataMember]
        public string strNumeroCuentaBeneficiario
        {
            get;
            set;
        }

        [DataMember]
        public string intEntidadBeneficiario
        {
            get;
            set;
        }

        [DataMember]
        public double dblMonto
        {
            get;
            set;
        }

        [DataMember]
        public string strMotivo
        {
            get;
            set;
        }
    }
}

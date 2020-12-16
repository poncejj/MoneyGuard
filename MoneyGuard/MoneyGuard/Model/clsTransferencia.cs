using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MoneyGuard.Model
{
    public class clsTransferencia
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "strIdentificacionEmisor")]
        public string strIdentificacionEmisor { get; set; }
        [JsonProperty(PropertyName = "strNumeroCuentaEmisor")]

        public string strNumeroCuentaEmisor { get;set; }
        [JsonProperty(PropertyName = "strEntidadEmisor")]

        public string strEntidadEmisor { get; set; }
        [JsonProperty(PropertyName = "strIdentificacionBeneficiario")]

        public string strIdentificacionBeneficiario { get; set; }
        [JsonProperty(PropertyName = "strTipoCuentaBeneficiario")]

        public string strTipoCuentaBeneficiario { get; set; }
        [JsonProperty(PropertyName = "strNumeroCuentaBeneficiario")]

        public string strNumeroCuentaBeneficiario { get; set; }
        [JsonProperty(PropertyName = "strEntidadBeneficiario")]

        public string strEntidadBeneficiario { get; set; }
        [JsonProperty(PropertyName = "dblMonto")]

        public double dblMonto { get; set; }
        [JsonProperty(PropertyName = "strMotivo")]

        public string strMotivo { get; set; }
        [JsonProperty(PropertyName = "strTipo")]

        public string strTipo { get; set; }
    }
}
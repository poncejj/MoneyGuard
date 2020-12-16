using Newtonsoft.Json;

namespace MoneyGuard.Model
{
    public class clsCooperativa
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "nombreCooperativa")]
        public string nombreCooperativa { get; set; }
        [JsonProperty(PropertyName = "nombreColaEnvio")]
        public string nombreColaEnvio { get; set; }
        [JsonProperty(PropertyName = "nombreColaRespuesta")]
        public string nombreColaRespuesta { get; set; }
        [JsonProperty(PropertyName = "nombreBusServicios")]
        public string nombreBusServicios { get; set; }
    }
}
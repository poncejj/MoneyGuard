using Newtonsoft.Json;

namespace MoneyGuard.Model
{
    public class clsCooperativaCliente
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "idUsuario")]
        public string idUsuario { get; set; }

        [JsonProperty(PropertyName = "intCooperativa")]
        public string intCooperativa { get; set; }

    }

}
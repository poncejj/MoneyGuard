using Newtonsoft.Json;

namespace MoneyGuard.Model
{
     public class clsUsuario
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string displayName { get; set; }

        [JsonProperty(PropertyName = "birthday")]
        public string birthday { get; set; }

        [JsonProperty(PropertyName = "relationshipStatus")]
        public string relationshipStatus { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string gender { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string imageUrl { get; set; }

        [JsonProperty(PropertyName = "identificacion")]
        public string identificacion { get; set; }

        [JsonProperty(PropertyName = "pin")]
        public string pin { get; set; }
    }

    public class UsuarioWrapper : Java.Lang.Object
    {
        public UsuarioWrapper(clsUsuario item)
        {
            Usuario = item;
        }

        public clsUsuario Usuario { get; private set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace MoneyGuard.Model
{
    public class clsCatalogo
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "descripcionCatalogo")]
        public string descripcionCatalogo {get; set;}

        [JsonProperty(PropertyName = "catalogoPadre")]
        public bool catalogoPadre { get; set; }
        [JsonProperty(PropertyName = "idCatalogoPadre")]
        public int idCatalogoPadre { get; set; }

    }
}

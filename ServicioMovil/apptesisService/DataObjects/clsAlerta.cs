using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apptesisService.DataObjects
{
    public class clsAlerta : EntityData
    {
        public string strTituloAlerta { get; set; }
        public string strMensajeAlerta { get; set; }
    }
}
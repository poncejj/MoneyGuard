using Microsoft.Azure.Mobile.Server;
using System;

namespace apptesisService.DataObjects
{
    public class clsCooperativaCliente : EntityData
    {
        public string idUsuario
        {
            get;
            set;
        }

        public string intCooperativa
        {
            get;
            set;
        }
    }
}
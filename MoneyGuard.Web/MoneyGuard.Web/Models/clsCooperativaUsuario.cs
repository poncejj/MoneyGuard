using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyGuard.Web.Models
{
    public class clsCooperativaUsuario
    {
        public string idCooperativaUsuario { get; set; }
        public string idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string idCooperativa { get; set; }
        public string fechaCreacionCooperativaUsuario { get; set; }
        public bool estadoCooperativaUsuario { get; set; }
    }

    public class clsCooperativaUsuarioContenedor
    {
        public clsCooperativaUsuario objCooperativaUsuario { get; set; }
        public List<clsCooperativa> objCooperativa { get; set; }
        public List<clsUsuarioApp> objUsuario { get; set; }
    }
}
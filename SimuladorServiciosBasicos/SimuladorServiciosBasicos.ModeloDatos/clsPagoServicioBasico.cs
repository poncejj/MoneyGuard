using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;


namespace SimuladorServiciosBasicos.ModeloDatos
{
    [DataContract]
    public class clsPagoServicioBasico
    {
        [DataMember]
        public string identificacion_cliente { get; set; }
        [DataMember]
        public string numero_suministro { get; set; }
        [DataMember]
        public double monto_suministro { get; set; }
        [DataMember]
        public string tipo_suministro { get; set; }

    }
}

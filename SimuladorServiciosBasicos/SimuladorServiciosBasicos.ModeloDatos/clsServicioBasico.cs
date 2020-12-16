using System;
using System.Runtime.Serialization;

namespace SimuladorServiciosBasicos.ModeloDatos
{
    [DataContract]
    public class clsServicioBasico
    {
        [DataMember]
        public int id_servicio { get; set; }
        [DataMember]
        public string nombre_servicio { get; set; }

    }
}

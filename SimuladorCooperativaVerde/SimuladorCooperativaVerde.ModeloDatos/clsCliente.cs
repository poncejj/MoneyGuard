using System;
using System.Runtime.Serialization;

namespace SimuladorCooperativaVerde.ModeloDatos
{
    [DataContract]
    public class clsCliente
    {
        [DataMember]
        public int id_cliente { get; set; }
        [DataMember]
        public string identificacion_cliente { get; set; }
        [DataMember]
        public string nombre_cliente { get; set; }
        [DataMember]
        public string apellido_cliente { get; set; }
        [DataMember]
        public string telefono_cliente { get; set; }
        [DataMember]
        public string email_cliente { get; set; }

    }
}

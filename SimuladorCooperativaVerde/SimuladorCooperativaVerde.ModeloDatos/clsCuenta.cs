using System;
using System.Runtime.Serialization;

namespace SimuladorCooperativaVerde.ModeloDatos
{
    [DataContract]
    public class clsCuenta
    {
        [DataMember]
        public int id_cuenta { get; set; }
        [DataMember]
        public int id_cliente { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public string tipo_cuenta { get; set; }
        [DataMember]
        public double saldo { get; set; }
        [DataMember]
        public bool estado { get; set; }
    }
}

using System;
using System.Runtime.Serialization;

namespace SimuladorCooperativaAmarilla.ModeloDatos
{
    [DataContract]
    public class clsMovimiento
    {
        [DataMember]
        public int id_movimiento { get; set; }
        [DataMember]
        public int id_cuenta { get; set; }
        [DataMember]
        public string identificacion_cliente_beneficiario { get; set; }
        [DataMember]
        public string nombre_cliente_beneficiario { get; set; }
        [DataMember]
        public char tipo_transaccion { get; set; }
        [DataMember]
        public string nombre_banco_beneficiario { get; set; }
        [DataMember]
        public double monto_movimiento { get; set; }
        [DataMember]
        public string descripcion_movimiento { get; set; }
        [DataMember]
        public string fecha_movimiento { get; set; }
        [DataMember]
        public bool estado_movimiento { get; set; }
        [DataMember]
        public string lugar_movimiento { get; set; }
        [DataMember]
        public double saldo_cuenta_movimiento { get; set; }
    }
}

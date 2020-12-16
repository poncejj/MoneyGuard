using System.Runtime.Serialization;

namespace SimuladorCooperativaVerde.ModeloDatos
{
    [DataContract]
    public class clsSaldoTarjeta
    {
        [DataMember]
        public int id_saldo_tarjeta { get; set; }
        [DataMember]
        public int id_tarjeta { get; set; }
        [DataMember]
        public double saldo_pendiente_tarjeta { get; set; }
        [DataMember]
        public double consumo_mes_tarjeta { get; set; }
        [DataMember]
        public double cupo_disponible_tarjeta { get; set; }
        [DataMember]
        public double minimo_pagar { get; set; }
        [DataMember]
        public string fecha_pago { get; set; }
    }
}

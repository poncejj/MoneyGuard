using System.Runtime.Serialization;

namespace MoneyGuard.Model
{
    [DataContract]
    public class clsTarjetaCedito
    {
        [DataMember]
        public int id_tarjeta { get; set; }
        [DataMember]
        public int id_cuenta { get; set; }
        [DataMember]
        public string numero_tarjeta { get; set; }
        [DataMember]
        public string marca_tarjeta { get; set; }
        [DataMember]
        public double cupo_disponible_tarjeta { get; set; }
        [DataMember]
        public double saldo_total_tarjeta { get; set; }
        [DataMember]
        public double consumo_mes_tarjeta { get; set; }
        [DataMember]
        public double minimo_pagar { get; set; }
        [DataMember]
        public string fecha_corte_tarjeta { get; set; }
        [DataMember]
        public string fecha_pago_tarjeta { get; set; }
        [DataMember]
        public string idCooperativa { get; set; }
    }
}
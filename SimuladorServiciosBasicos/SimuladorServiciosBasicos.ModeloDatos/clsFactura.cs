using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SimuladorServiciosBasicos.ModeloDatos
{
    [DataContract]
    public class clsCabeceraFacturaServicio
    {
        [DataMember]
        public string identificacion_cliente { get; set; }
        [DataMember]
        public string nombre_cliente { get; set; }
        [DataMember]
        public string direccion_suministro { get; set; }
        [DataMember]
        public string ubicacion_suministro { get; set; }
        [DataMember]
        public string tipo_suministro { get; set; }
        [DataMember]
        public string numero_suministro { get; set; }
        [DataMember]
        public List<clsDetalleFacturaServicio> detalle_factura { get; set; }
    }

    [DataContract]
    public class clsDetalleFacturaServicio
    {
        [DataMember]
        public string detalle_item { get; set; }
        [DataMember]
        public double valor_detalle { get; set; }
    }
}

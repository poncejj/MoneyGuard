using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCooperativaAmarilla.ModeloDatos
{
    [DataContract]
    public class clsRespuestaMensaje
    {
        [DataMember]
        public string queueRespuesta { get; set; }
        [DataMember]
        public string idRespuesta { get; set; }
        [DataMember]
        public string bodyRespuesta { get; set; }
    }
}

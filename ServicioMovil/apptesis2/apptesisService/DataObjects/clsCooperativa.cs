using Microsoft.Azure.Mobile.Server;

namespace apptesisService.DataObjects
{
    public class clsCooperativa : EntityData
    {
        public string nombreCooperativa
        {
            get;
            set;
        }
        public string nombreColaEnvio
        {
            get;
            set;
        }

        public string nombreColaRespuesta
        {
            get;
            set;
        }

        public string nombreBusServicios
        {
            get;
            set;
        }
    }
}
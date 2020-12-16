using Microsoft.Azure.Mobile.Server;

namespace apptesisService.DataObjects
{
    public class clsCatalogo : EntityData
    {
        public string descripcionCatalogo
        {
            get;
            set;
        }

        public bool catalogoPadre
        {
            get;
            set;
        }
        public int idCatalogoPadre
        {
            get;
            set;
        }
    }
}
using Microsoft.Azure.Mobile.Server;
using System;

namespace apptesisService.DataObjects
{
    public class clsUsuario : EntityData
    {
        public string displayName
        {
            get;
            set;
        }

        public string birthday
        {
            get;
            set;
        }

        public string relationshipStatus
        {
            get;
            set;
        }

        public string gender
        {
            get;
            set;
        }

        public string imageUrl
        {
            get;
            set;
        }

        public string identificacion
        {
            get;
            set;
        }

        public string pin
        {
            get;
            set;
        }
    }
}
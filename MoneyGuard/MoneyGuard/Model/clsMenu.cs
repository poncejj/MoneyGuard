using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MoneyGuard.Model
{
    public class clsMenu
    {
        public string strItemMenu { get; set; }
        public int imgResourceId { get; set; }

        public clsMenu(string strItemMenu, int imgResourceId)
        {
            this.strItemMenu = strItemMenu;
            this.imgResourceId= imgResourceId;
        }


    }
}
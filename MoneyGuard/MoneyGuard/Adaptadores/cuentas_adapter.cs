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
using MoneyGuard.Model;
using Android.Content.Res;
using MoneyGuard.Comun;

namespace MoneyGuard
{
    public class cuentas_adapter : BaseAdapter<clsCuenta>
    {
        List<clsCuenta> items;
        List<clsCooperativa> itemsCooperativa;
        Activity context;
        public cuentas_adapter(Activity context, List<clsCuenta> items, List<clsCooperativa> itemsCooperativa)  : base()
        {
            this.context = context;
            this.items = items;
            this.itemsCooperativa = itemsCooperativa;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override clsCuenta this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.itemCuentas, null);

            view.FindViewById<TextView>(Resource.Id.txtNumeroCuentaItems).Text = item.numero_cuenta;
            string saldoCuenta = string.Format("{0:N2}", item.saldo);
            view.FindViewById<TextView>(Resource.Id.txtSaldoItem).Text = "USD "+saldoCuenta;
            view.FindViewById<TextView>(Resource.Id.txtNombreInstitucionItem).Text = itemsCooperativa.Find(x => x.Id == item.idCooperativa).nombreCooperativa;
            return view;
        }
    }
}
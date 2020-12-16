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

namespace MoneyGuard
{
    public class tarjetas_adapter : BaseAdapter<clsTarjetaCedito>
    {
        List<clsTarjetaCedito> items;
        Activity context;
        public tarjetas_adapter(Activity context, List<clsTarjetaCedito> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override clsTarjetaCedito this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.itemTarjeta, null);
            view.FindViewById<TextView>(Resource.Id.txtMarcaTarjeta).Text = item.marca_tarjeta;
            view.FindViewById<TextView>(Resource.Id.txtNumeroTarjeta).Text = item.numero_tarjeta;
            string minimoPagoTarjeta = string.Format("{0:N2}", item.minimo_pagar);
            view.FindViewById<TextView>(Resource.Id.txtMinimoPago).Text = "Minimo Pago " + minimoPagoTarjeta;
            string totalPagoTarjeta = string.Format("{0:N2}", item.saldo_total_tarjeta);
            view.FindViewById<TextView>(Resource.Id.txtTotalPago).Text = "Total Pago " + totalPagoTarjeta;
            view.FindViewById<ImageView>(Resource.Id.imgTarjeta).SetImageResource(Resource.Drawable.ic_creditCard);
            return view;
        }
    }
}
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
    public class detalle_factura_adapter : BaseAdapter<clsDetalleFacturaServicio>
    {
        List<clsDetalleFacturaServicio> items;
        Activity context;
        public detalle_factura_adapter(Activity context, List<clsDetalleFacturaServicio> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override clsDetalleFacturaServicio this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.itemDetalleFactura, null);
            view.FindViewById<TextView>(Resource.Id.txtDetalleFacturaServicio).Text = item.detalle_item;
            string valorDetalle = string.Format("{0:N2}", item.valor_detalle);
            view.FindViewById<TextView>(Resource.Id.txtMontoDetalleFactura).Text = "USD " + valorDetalle;
            
            return view;
        }
    }
}
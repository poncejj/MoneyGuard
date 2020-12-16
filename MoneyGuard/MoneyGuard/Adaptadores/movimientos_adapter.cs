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
    public class movimientos_adapter : BaseAdapter<clsMovimiento>
    {
        List<clsMovimiento> items;
        Activity context;
        public movimientos_adapter(Activity context, List<clsMovimiento> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override clsMovimiento this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.itemMovimiento, null);
            view.FindViewById<TextView>(Resource.Id.txtFechaMovimiento).Text = item.fecha_movimiento;
            view.FindViewById<TextView>(Resource.Id.txtDescripcionMovimiento).Text = item.descripcion_movimiento;
            string montoMovimiento = string.Format("{0:N2}", item.monto_movimiento);
            view.FindViewById<TextView>(Resource.Id.txtMontoMovimiento).Text = "USD " + montoMovimiento;
            string saldoMovimiento = string.Format("{0:N2}", item.saldo_cuenta_movimiento);
            view.FindViewById<TextView>(Resource.Id.txtSaldoCuentaMovimiento).Text = "USD " + saldoMovimiento;
            if(item.tipo_transaccion.Equals('C'))
            {
                view.FindViewById<ImageView>(Resource.Id.imgTipoMovimiento).SetImageResource(Resource.Drawable.add);
            }
            if (item.tipo_transaccion.Equals('D'))
            {
                view.FindViewById<ImageView>(Resource.Id.imgTipoMovimiento).SetImageResource(Resource.Drawable.remove);
            }
            return view;
        }
    }
}
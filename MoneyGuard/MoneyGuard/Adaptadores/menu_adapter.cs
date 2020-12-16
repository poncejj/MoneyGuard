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
    public class menu_adapter : BaseAdapter<clsMenu>
    {
        List<clsMenu> items;
        Activity context;
        public menu_adapter(Activity context, List<clsMenu> items) : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override clsMenu this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.itemMenu, null);

            view.FindViewById<TextView>(Resource.Id.txtItemMenu).Text = item.strItemMenu;
            view.FindViewById<ImageView>(Resource.Id.imgImagenMenu).SetImageResource(item.imgResourceId);
            return view;
        }
    }
}
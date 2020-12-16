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
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using MoneyGuard.Comun;
using Newtonsoft.Json;

namespace MoneyGuard
{
    [Activity(Theme = "@style/MyTheme")]
    public class ActivitySeleccionCuenta : AppCompatActivity
    {
        ListView listView;
        List<clsCuenta> cuentasItems = new List<clsCuenta>();
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();
        private int mConsultaMovimiento = Resource.String.consultaMovimiento;
        private SupportToolbar mToolBar;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strCooperativa = string.Empty;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelecionCuenta);

            // Create your application here
            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar2);
            SetSupportActionBar(mToolBar);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            SupportActionBar.SetTitle(mConsultaMovimiento);

            //Guardar temporalmente varaibles pantalla
            strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
            strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
            imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
            strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
            strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

            string[] arrRespuesta = strCuentaJson.Split('|');
            string strCuentas = arrRespuesta[0];
            string strTarjetas = arrRespuesta[1];
            cuentasItems = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentas);

            lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);


            listView = FindViewById<ListView>(Resource.Id.ListaSeleccion);

            listView.Adapter = new cuentas_adapter(this, cuentasItems, lstCooperativas);

            listView.ItemClick += OnListItemClick;
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = cuentasItems[e.Position];
            var intent = new Intent(this, typeof(ActivityConsultaMovimientos));
            intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
            intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
            intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
            intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
            intent.PutExtra(clsConstantes.strNumeroCuentaSeleccionada, t.numero_cuenta);
            intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
            StartActivity(intent);
            this.Finish();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Android.Resource.Id.Home)
            {
                var intent = new Intent(this, typeof(ActivityMenu));
                intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                StartActivity(intent);
                this.Finish();
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}
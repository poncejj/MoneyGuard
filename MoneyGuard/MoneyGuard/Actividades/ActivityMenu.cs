using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using System.Collections.Generic;
using MoneyGuard.Model;
using MoneyGuard.Comun;
using Newtonsoft.Json;

namespace MoneyGuard
{
    [Activity(Theme = "@style/MyTheme")]
    public class ActivityMenu : AppCompatActivity
    {
        private SupportToolbar mToolBar;
        private MyActionBarDrawerToogle mDrawerToggle;
        private DrawerLayout mDrawerLayout;
        private ListView mLeftDrawer;
        private List<clsMenu> mListIems;
        menu_adapter mArrayAdapter;
        ListView listViewCuentas;
        ListView listViewTarjetas;
        List<clsCuenta> cuentasItems = new List<clsCuenta>();
        List<clsTarjetaCedito> tarjetasItems = new List<clsTarjetaCedito>();
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();
        private int mBienvenidoResource = Resource.String.bienvenido;
        string idDispositivo = Android.OS.Build.Serial;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strCooperativa = string.Empty;

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                SetContentView(Resource.Layout.Menu);

                mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar1);
                mDrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                mLeftDrawer = FindViewById<ListView>(Resource.Id.left_drawer);
                SetSupportActionBar(mToolBar);

                mDrawerToggle = new MyActionBarDrawerToogle(
                    this,
                    mDrawerLayout,
                    Resource.String.openDrawer,
                    Resource.String.closeDrawer
                    );

                mDrawerLayout.SetDrawerListener(mDrawerToggle);
                SupportActionBar.SetHomeButtonEnabled(true);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                mDrawerToggle.SyncState();

                SupportActionBar.SetTitle(mBienvenidoResource);
                mListIems = new List<clsMenu>();
                clsMenu objMenuMovimientos = new clsMenu("Consulta de Movimientos", Resource.Drawable.ic_consulta_movimiento);
                clsMenu objMenuTransferencias = new clsMenu("Transferencias", Resource.Drawable.ic_transferencia);
                clsMenu objMenuPagoServicios = new clsMenu("Pago Servicios", Resource.Drawable.ic_pago_servicios);
                clsMenu objMenuPagoTarjeras = new clsMenu("Pago Tarjetas", Resource.Drawable.ic_pago_tarjetas);
                clsMenu objMenuAgregarCuenta = new clsMenu("Agregar Cuenta", Resource.Drawable.ic_agregar_cuenta);
                clsMenu objMenuSalir = new clsMenu("Salir", Resource.Drawable.ic_salir);
                mListIems.Add(objMenuMovimientos);
                mListIems.Add(objMenuTransferencias);
                mListIems.Add(objMenuPagoServicios);
                mListIems.Add(objMenuPagoTarjeras);
                mListIems.Add(objMenuAgregarCuenta);
                mListIems.Add(objMenuSalir);

                imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
                strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
                strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
                strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
                strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

                lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);

                string[] arrRespuesta = strCuentaJson.Split('|');
                string strCuentas = arrRespuesta[0];
                string strTarjetas = arrRespuesta[1];
                cuentasItems = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentas);
                tarjetasItems = JsonConvert.DeserializeObject<List<clsTarjetaCedito>>(strTarjetas);

                mArrayAdapter = new menu_adapter(this,mListIems);
                mLeftDrawer.Adapter = mArrayAdapter;
                mLeftDrawer.ItemClick += mLeftDrawer_ItemClick;

                //Llenar cuentas
                if(cuentasItems.Count > 0)
                {
                    listViewCuentas = FindViewById<ListView>(Resource.Id.ListCuenta);

                    listViewCuentas.Adapter = new cuentas_adapter(this, cuentasItems, lstCooperativas);

                    listViewCuentas.ItemClick += OnListItemClickCuentas;  // to be defined
                }
                //Llenar tarjetas
                if (tarjetasItems.Count > 0)
                {
                    listViewTarjetas = FindViewById<ListView>(Resource.Id.ListTarjeta);

                    listViewTarjetas.Adapter = new tarjetas_adapter(this, tarjetasItems);

                    listViewTarjetas.ItemClick += OnListItemClickTarjetas;  // to be defined
                }
            }
            catch (Exception ex)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                alert.SetTitle("Alerta");
                alert.SetMessage("Ocurió un problema con al abrir la pantalla");

                RunOnUiThread(() => {
                    alert.Show();
                });
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            mDrawerToggle.OnOptionsItemSelected(item);
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            if(mDrawerLayout.IsDrawerOpen((int)GravityFlags.Left))
            {
                outState.PutString("DrawerState", "Opened");
            }
            else
            {
                outState.PutString("DrawerState", "Closed");
            }

            base.OnSaveInstanceState(outState);
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            mDrawerToggle.SyncState();
        }

        private void mLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            int item = e.Position;
            if(item == 0)
            {
                var intent = new Intent(this, typeof(ActivitySeleccionCuenta));
                intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                StartActivity(intent);
                this.Finish();
            }
            else
            if(item == 1)
            {
                var intent = new Intent(this, typeof(ActivityTransferencia));
                intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                StartActivity(intent);
                this.Finish();
            }
            else
            if (item == 2)
            {
                var intent = new Intent(this, typeof(ActivityConsultaServicios));
                intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                StartActivity(intent);
                this.Finish();
            }
            else
            if (item == 3)
            {
                if(tarjetasItems.Count > 0)
                {
                    var intent = new Intent(this, typeof(ActivityPagoTarjeta));
                    intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                    intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                    intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                    intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                    intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                    StartActivity(intent);
                    this.Finish();
                }
                else
                {
                    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                    alert.SetTitle("Alerta");
                    alert.SetMessage("No dispone de tarjetas para el pago");

                    RunOnUiThread(() => {
                        alert.Show();
                    });
                }
            }
            else
            if (item == 4)
            {
                var intent = new Intent(this, typeof(ActivityAgregarCuenta));
                intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                StartActivity(intent);
                this.Finish();
            }
            else
            if (item == 5)
            {
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            }

        }

        void OnListItemClickCuentas(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = cuentasItems[e.Position];
            var intent = new Intent(this, typeof(ActivityConsultaMovimientos));
            intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
            intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
            intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
            intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
            intent.PutExtra(clsConstantes.strNumeroCuentaSeleccionada, t.numero_cuenta);
            intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
            StartActivity(intent);
            this.Finish();
        }

        void OnListItemClickTarjetas(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listView = sender as ListView;
            var t = tarjetasItems[e.Position];
            var intent = new Intent(this, typeof(ActivityPagoTarjeta));
            intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
            intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
            intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
            intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
            intent.PutExtra(clsConstantes.strNumeroTarjetaSeleccionada, t.numero_tarjeta);
            intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
            StartActivity(intent);
            this.Finish();
        }
    }
}
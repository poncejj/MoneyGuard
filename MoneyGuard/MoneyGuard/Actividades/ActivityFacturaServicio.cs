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
using MoneyGuard.Comun;
using MoneyGuard.Servicios;
using MoneyGuard.Model;
using Newtonsoft.Json;

namespace MoneyGuard
{
    [Activity(Theme = "@style/MyTheme")]
    public class ActivityFacturaServicio : AppCompatActivity
    {
        private SupportToolbar mToolBar;
        ListView listView;
        private Button btnPagarFacturaServicio;
        private TextView lblNombreFacturaServicio;
        private TextView lblIdentificacionFacturaServicio;
        private TextView lblDireccionFacturaServicio;
        private TextView lblUbicacionFacturaServicio;
        private TextView lblTipoFacturaServicio;
        private TextView lblSuministroFacturaServicio;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strTipoServicio = string.Empty;
        string strFacturaServicio = string.Empty;
        string strCooperativa = string.Empty;
        int mPagoServicios = Resource.String.facturaServicio;
        clsServicioConsulta objServicioConsulta = new clsServicioConsulta();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FacturaServicio);

            btnPagarFacturaServicio = FindViewById<Button>(Resource.Id.btnPagarFacturaServicio);
            lblIdentificacionFacturaServicio = FindViewById<TextView>(Resource.Id.lblIdentificacionFacturaServicio);
            lblNombreFacturaServicio = FindViewById<TextView>(Resource.Id.lblNombreFacturaServicio);
            lblDireccionFacturaServicio = FindViewById<TextView>(Resource.Id.lblDireccionFacturaServicio);
            lblSuministroFacturaServicio = FindViewById<TextView>(Resource.Id.lblSuministroFacturaServicio);
            lblTipoFacturaServicio = FindViewById<TextView>(Resource.Id.lblTipoFacturaServicio);
            lblUbicacionFacturaServicio = FindViewById<TextView>(Resource.Id.lblUbicacionFacturaServicio);
            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar9);
            SetSupportActionBar(mToolBar);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            SupportActionBar.SetTitle(mPagoServicios);

            //Guardar temporalmente varaibles pantalla
            strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
            strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
            imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
            strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
            strFacturaServicio = Intent.GetStringExtra(clsConstantes.strFacturaServicio);
            strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

            clsCabeceraFacturaServicio objCabeceraFacturaServicio = JsonConvert.DeserializeObject<clsCabeceraFacturaServicio>(strFacturaServicio);

            List<clsDetalleFacturaServicio> detalleFacturaItems = objCabeceraFacturaServicio.detalle_factura;

            listView = FindViewById<ListView>(Resource.Id.ListaDetalleFacturaServicio);

            lblIdentificacionFacturaServicio.Text = objCabeceraFacturaServicio.identificacion_cliente;
            lblNombreFacturaServicio.Text = objCabeceraFacturaServicio.nombre_cliente;
            lblDireccionFacturaServicio.Text = objCabeceraFacturaServicio.direccion_suministro;
            lblSuministroFacturaServicio.Text = objCabeceraFacturaServicio.numero_suministro;
            lblTipoFacturaServicio.Text = objCabeceraFacturaServicio.tipo_suministro;
            lblUbicacionFacturaServicio.Text = objCabeceraFacturaServicio.ubicacion_suministro;

            if (detalleFacturaItems.Count > 0)
                listView.Adapter = new detalle_factura_adapter(this, detalleFacturaItems);


            btnPagarFacturaServicio.Click += delegate
            {
                var intent = new Intent(this, typeof(ActivityPagoServicios));
                intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                intent.PutExtra(clsConstantes.strFacturaServicio, strFacturaServicio);
                StartActivity(intent);
                this.Finish();
            };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //Back button pressed -> toggle event
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
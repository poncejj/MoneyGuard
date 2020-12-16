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
    public class ActivityConsultaServicios : AppCompatActivity
    {
        private SupportToolbar mToolBar;
        private Spinner spiTipoConsultaServicio;
        private Button btnConsultarPagos;
        Android.App.ProgressDialog progress;
        private EditText txtSuministroConsultaServicio;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strTipoServicio = string.Empty;
        string strCooperativa = string.Empty;
        int mPagoServicios = Resource.String.consultaServicio;
        clsServicioConsulta objServicioConsulta = new clsServicioConsulta();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ConsultaServicio);

            spiTipoConsultaServicio = FindViewById<Spinner>(Resource.Id.spiTipoConsultaServicio);
            btnConsultarPagos = FindViewById<Button>(Resource.Id.btnConsultarServicio);
            txtSuministroConsultaServicio = FindViewById<EditText>(Resource.Id.txtSuministroConsultaServicio);
            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar8);
            SetSupportActionBar(mToolBar);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            SupportActionBar.SetTitle(mPagoServicios);

            //Guardar temporalmente varaibles pantalla
            strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
            strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
            imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
            strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
            strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

            spiTipoConsultaServicio.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.servicio_array, Android.Resource.Layout.SimpleDropDownItem1Line);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            spiTipoConsultaServicio.Adapter = adapter;

            btnConsultarPagos.Click += async delegate
            {
                if (String.IsNullOrEmpty(txtSuministroConsultaServicio.Text))
                {
                    Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                    alert.SetTitle("Alerta");
                    alert.SetMessage("Faltan campos por llenar");
                    
                    RunOnUiThread(() => {
                        alert.Show();
                    });
                }
                else
                {
                    progress = new Android.App.ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("Consultando Factura");
                    progress.SetCancelable(false);
                    progress.Show();

                    string strNumeroSuministro = txtSuministroConsultaServicio.Text.ToUpper();
                    string strFacturaServicio = await objServicioConsulta.consultarServicios(strIdentificacion, strNumeroSuministro, strTipoServicio);
                    if (!strFacturaServicio.ToUpper().Contains("ERROR"))
                    {
                        var intent = new Intent(this, typeof(ActivityFacturaServicio));
                        intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                        intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                        intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                        intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                        intent.PutExtra(clsConstantes.strFacturaServicio, strFacturaServicio);
                        intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                        StartActivity(intent);
                        this.Finish();
                    }
                    else
                    {
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                        alert.SetTitle("Alerta");
                        alert.SetMessage(strFacturaServicio);


                        RunOnUiThread(() => {
                            alert.Show();
                        });

                        progress.Cancel();

                    }
                }
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

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string tipo_servicio_seleccionada = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            strTipoServicio = tipo_servicio_seleccionada;

        }
    }
}
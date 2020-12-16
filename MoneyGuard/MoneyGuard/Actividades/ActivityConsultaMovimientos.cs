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
using MoneyGuard.Servicios;
using Newtonsoft.Json;

namespace MoneyGuard
{
    [Activity(Theme = "@style/MyTheme")]
    public class ActivityConsultaMovimientos : AppCompatActivity
    {
        ListView listView;
        clsServicioCuenta objServicioCuenta = new clsServicioCuenta();
        private int mConsultaMovimiento = Resource.String.consultaMovimiento;
        private SupportToolbar mToolBar;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strNumeroCuentaSeleccionada = string.Empty;
        string strIdUsuario = string.Empty;
        string strCooperativa = string.Empty;
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.ConsultaMovimientos);

                TextView lblNumeroCuentaConsultaMovimientos = FindViewById<TextView>(Resource.Id.lblNumeroCuentaConsultaMovimientos);
                TextView lblNombreEntidadConsultaMovimientos = FindViewById<TextView>(Resource.Id.lblNombreEntidadConsultaMovimientos);
                TextView lblSaldoDisponibleConsultaMovimientos = FindViewById<TextView>(Resource.Id.lblSaldoDisponibleConsultaMovimientos);

                // Create your application here
                mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar5);
                SetSupportActionBar(mToolBar);
                SupportActionBar.SetHomeButtonEnabled(true);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetTitle(mConsultaMovimiento);

                //Guardar temporalmente varaibles pantalla
                strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
                strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
                strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
                imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
                strNumeroCuentaSeleccionada = Intent.GetStringExtra(clsConstantes.strNumeroCuentaSeleccionada);
                strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

                lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);

                string[] arrRespuesta = strCuentaJson.Split('|');
                string strCuentas = arrRespuesta[0];
                string strTarjetas = arrRespuesta[1];
                List<clsCuenta> lstCuentas = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentas);
                clsCuenta objCuentaSeleccionada = lstCuentas.Where(x => x.numero_cuenta == strNumeroCuentaSeleccionada).Single();

                if (objCuentaSeleccionada != null)
                {
                    string saldoCuenta = string.Format("{0:N2}", objCuentaSeleccionada.saldo);
                    lblSaldoDisponibleConsultaMovimientos.Text = "USD " + saldoCuenta;
                    lblNombreEntidadConsultaMovimientos.Text = lstCooperativas.Find(x => x.Id == objCuentaSeleccionada.idCooperativa).nombreCooperativa;
                    lblNumeroCuentaConsultaMovimientos.Text = objCuentaSeleccionada.numero_cuenta.ToString();
                }

                clsCooperativa objCooperativa = lstCooperativas.Find(x => x.Id == objCuentaSeleccionada.idCooperativa);

                string strJsonMovimientos = await objServicioCuenta.consultaMovimientos(strNumeroCuentaSeleccionada, 10, objCooperativa.nombreColaEnvio, objCooperativa.nombreColaRespuesta);
                List<clsMovimiento> movimientosItems = JsonConvert.DeserializeObject<List<clsMovimiento>>(strJsonMovimientos);
                listView = FindViewById<ListView>(Resource.Id.ListaMovimientos);
                if (movimientosItems.Count > 0)
                    listView.Adapter = new movimientos_adapter(this, movimientosItems);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Alerta: Al mostrar la pantalla de consulta de movimientos... " + ex.Message);
            }
            
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
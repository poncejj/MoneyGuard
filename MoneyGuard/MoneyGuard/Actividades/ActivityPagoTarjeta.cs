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
using MoneyGuard.Model;
using Newtonsoft.Json;
using MoneyGuard.Servicios;

namespace MoneyGuard
{
    [Activity(Theme = "@style/MyTheme")]
    public class ActivityPagoTarjeta : AppCompatActivity
    {
        private SupportToolbar mToolBar;
        private Spinner spinnerCuentaOrigenPagoTarjeta;
        private Spinner spinnerNumeroTarjeta;
        private Button btnRealizarPagoTarjeta;
        private EditText txtTipoTarjetaSeleccionada;
        private EditText txtMontoPagoTarjeta;
        private TextView lblSaldoCuentaOrigen;
        Android.App.ProgressDialog progress;
        private int mPagoTarjeta = Resource.String.pagoTarjeta;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strNumeroTarjeta = string.Empty;
        string strNumeroCuenta = string.Empty;
        string strCooperativa = string.Empty;
        double dblPagoMinimo = 0;
        List<clsCuenta> lstCuentas = new List<clsCuenta>();
        List<clsTarjetaCedito> lstTarjetas = new List<clsTarjetaCedito>();
        clsServicioTransferencia objServicioTransferencia = new clsServicioTransferencia();
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();
        clsCooperativa objCooperativaEmisor = new clsCooperativa();
        clsCooperativa objCooperativaBeneficiario = new clsCooperativa();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PagoTarjetas);

            spinnerCuentaOrigenPagoTarjeta = FindViewById<Spinner>(Resource.Id.spiCuentaOrigenPagoTarjeta);
            spinnerNumeroTarjeta = FindViewById<Spinner>(Resource.Id.spiNumeroTarjetaPago);
            btnRealizarPagoTarjeta = FindViewById<Button>(Resource.Id.btnRealizarPagoTarjeta);
            txtTipoTarjetaSeleccionada = FindViewById<EditText>(Resource.Id.txtTipoTarjetaSeleccionada);
            txtMontoPagoTarjeta = FindViewById<EditText>(Resource.Id.txtMontoPagoTarjeta);
            lblSaldoCuentaOrigen = FindViewById<TextView>(Resource.Id.lblSaldoCuentaPagoTarjeta);

            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar6);
            SetSupportActionBar(mToolBar);

            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            SupportActionBar.SetTitle(mPagoTarjeta);

            //Guardar temporalmente varaibles pantalla
            strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
            strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
            imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
            strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
            strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

            lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);

            string[] arrRespuesta = strCuentaJson.Split('|');
            string strCuentas = arrRespuesta[0];
            string strTarjetas = arrRespuesta[1];
            lstCuentas = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentas);
            lstTarjetas = JsonConvert.DeserializeObject<List<clsTarjetaCedito>>(strTarjetas);

            //spinner tarjeta credito
            spinnerNumeroTarjeta.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedTarjeta);
            List<string> lstTarjetasString = new List<string>();
            foreach (clsTarjetaCedito objTarjeta in lstTarjetas)
            {
                if (!lstTarjetasString.Contains(objTarjeta.numero_tarjeta))
                    lstTarjetasString.Add(objTarjeta.numero_tarjeta);
            }
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lstTarjetasString);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);

            spinnerNumeroTarjeta.Adapter = adapter;

            //spinner cuenta de origen 
            spinnerCuentaOrigenPagoTarjeta.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedCuenta);
            List<string> lstCuentasString = new List<string>();
            foreach (clsCuenta objCuenta in lstCuentas)
            {
                if (!lstCuentasString.Contains(objCuenta.numero_cuenta))
                    lstCuentasString.Add(objCuenta.numero_cuenta);
            }
            var adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lstCuentasString);
            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);

            spinnerCuentaOrigenPagoTarjeta.Adapter = adapter1;

            //Boton realizar pago de tarjeta
            btnRealizarPagoTarjeta.Click += async delegate
            {
                if (String.IsNullOrEmpty(txtMontoPagoTarjeta.Text)
                || String.IsNullOrEmpty(txtTipoTarjetaSeleccionada.Text))
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
                    //Se mestra el cuadro de cargando 
                    progress = new Android.App.ProgressDialog(this);
                    progress.Indeterminate = true;
                    progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                    progress.SetMessage("Realizando Transferencia");
                    progress.SetCancelable(false);
                    progress.Show();

                    clsTransferencia objTransferencia = llenarDatosTransferencia();
                    string strRespuesta = await objServicioTransferencia.realizarPagoTarjeta(strIdentificacion,strNumeroTarjeta, objTransferencia, dblPagoMinimo, objCooperativaEmisor, objCooperativaBeneficiario);
                    if(!strRespuesta.ToLower().Contains("Alerta"))
                    {
                        strCuentaJson = await clsActividadesGenericas.modificarCuentaJson(strIdentificacion,strIdUsuario, lstCooperativas);
                        var intent = new Intent(this, typeof(ActivityMenu));
                        intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacion);
                        intent.PutExtra(clsConstantes.strURLImagenUsuario, imageUrl);
                        intent.PutExtra(clsConstantes.strIdUsuario, strIdUsuario);
                        intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                        intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                        StartActivity(intent);
                        this.Finish();
                    }
                }
            };
        }

        private clsTransferencia llenarDatosTransferencia()
        {
            clsTransferencia objTransferencia = new clsTransferencia();
            try
            {
                objTransferencia.strIdentificacionEmisor = strIdentificacion;
                objTransferencia.strNumeroCuentaEmisor = strNumeroCuenta;
                clsCuenta objCuenta = lstCuentas.Find(x => x.numero_cuenta == strNumeroCuenta);
                objCooperativaEmisor = lstCooperativas.Find(x => x.Id == objCuenta.idCooperativa);
                objTransferencia.strEntidadEmisor = objCooperativaEmisor.nombreCooperativa;
                objTransferencia.strIdentificacionBeneficiario = "9999999999";
                objTransferencia.strTipoCuentaBeneficiario = objCuenta.tipo_cuenta;
                objTransferencia.strNumeroCuentaBeneficiario = "99999999999";
                objTransferencia.strEntidadBeneficiario = objCooperativaBeneficiario.nombreCooperativa;
                objTransferencia.dblMonto = double.Parse(txtMontoPagoTarjeta.Text);
                objTransferencia.strMotivo = "PAGO TARJETA " +txtTipoTarjetaSeleccionada.Text;
            }
            catch (Exception)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                progress.Cancel();
                alert.SetTitle("Alerta");
                alert.SetMessage("Ocurió un problema al realizar la transferencia");

                RunOnUiThread(() => {
                    alert.Show();
                });
            }
            return objTransferencia;
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

        private void spinner_ItemSelectedTarjeta(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string numero_tarjeta_seleccionada = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            strNumeroTarjeta = numero_tarjeta_seleccionada;

            foreach (clsTarjetaCedito objTarjeta in lstTarjetas)
            {
                if (objTarjeta.numero_tarjeta.Equals(numero_tarjeta_seleccionada))
                {
                    txtTipoTarjetaSeleccionada.Text = objTarjeta.marca_tarjeta;
                    objCooperativaBeneficiario = lstCooperativas.Find(x => x.Id == objTarjeta.idCooperativa);

                }
            }
        }

        private void spinner_ItemSelectedCuenta(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string cuenta_seleccionada = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            strNumeroCuenta = cuenta_seleccionada;
            foreach(clsCuenta objCuenta in lstCuentas)
            {
                if(objCuenta.numero_cuenta.Equals(cuenta_seleccionada))
                {
                    string saldoCuenta = string.Format("{0:N2}", objCuenta.saldo);

                    lblSaldoCuentaOrigen.Text = "(Saldo disponible USD " + saldoCuenta + ")";
                }
            }
        }
    }
}
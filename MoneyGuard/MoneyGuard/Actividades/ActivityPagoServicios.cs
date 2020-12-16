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
    public class ActivityPagoServicios : AppCompatActivity
    {
        private SupportToolbar mToolBar;
        private Spinner spinnerCuentaOrigen;
        private EditText txtTipoSuministro;
        private Button btnRealizarPagos;
        private EditText txtNumeroSuministro;
        private TextView lblSaldoCuentaOrigen;
        private EditText txtMontoPago;
        private int mPagoServicios = Resource.String.pagoServicios;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strNumeroCuenta = string.Empty;
        string strFacturaServicio = string.Empty;
        string strCooperativa = string.Empty;
        Android.App.ProgressDialog progress;
        List<clsCuenta> lstCuentas = new List<clsCuenta>();
        clsServicioTransferencia objServicioTransferencia = new clsServicioTransferencia();
        clsCabeceraFacturaServicio objCabeceraFacturaServicio = new clsCabeceraFacturaServicio();
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();
        clsCooperativa objCooperativa = new clsCooperativa();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.PagoServicios);

            spinnerCuentaOrigen = FindViewById<Spinner>(Resource.Id.spiCuentaOrigenPagoServicio);
            txtTipoSuministro = FindViewById<EditText>(Resource.Id.txtTipoSuministroPagoServicio);
            btnRealizarPagos = FindViewById<Button>(Resource.Id.btnRealizarPagoServicio);
            txtNumeroSuministro = FindViewById<EditText>(Resource.Id.txtSuministroPagoServicio);
            txtMontoPago = FindViewById<EditText>(Resource.Id.txtMontoPagoServicio);
            lblSaldoCuentaOrigen = FindViewById<TextView>(Resource.Id.lblSaldoCuentaPagoServicio);
            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar4);
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

            lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);

            string[] arrRespuesta = strCuentaJson.Split('|');
            string strCuentas = arrRespuesta[0];
            string strTarjetas = arrRespuesta[1];
            lstCuentas = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentas);

            objCabeceraFacturaServicio = JsonConvert.DeserializeObject<clsCabeceraFacturaServicio>(strFacturaServicio);

            
            spinnerCuentaOrigen.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            List<string> lstCuentasString = new List<string>();
            foreach (clsCuenta objCuenta in lstCuentas)
            {
                if (!lstCuentasString.Contains(objCuenta.numero_cuenta))
                    lstCuentasString.Add(objCuenta.numero_cuenta);
            }
            var adapter1 = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lstCuentasString);

            adapter1.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            spinnerCuentaOrigen.Adapter = adapter1;

            txtTipoSuministro.Text = objCabeceraFacturaServicio.tipo_suministro;
            txtNumeroSuministro.Text = objCabeceraFacturaServicio.numero_suministro;
            txtMontoPago.Text = objCabeceraFacturaServicio.detalle_factura.FindLast(x => x.detalle_item.ToLower().Contains("total")).valor_detalle.ToString();

            btnRealizarPagos.Click += async (sender, e) =>
            {
                if (String.IsNullOrEmpty(txtMontoPago.Text)
                || String.IsNullOrEmpty(txtNumeroSuministro.Text))
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
                    progress.SetMessage("Realizando Pago");
                    progress.SetCancelable(true);
                    progress.Show();

                    string strNumeroSuministro = objCabeceraFacturaServicio.numero_suministro;
                    string strTipoServicio = objCabeceraFacturaServicio.tipo_suministro;
                    clsTransferencia objTransferencia = llenarDatosTransferencia();
                    string strRespuesta = await objServicioTransferencia.realizarPagoServicios(strIdentificacion, strNumeroSuministro, strTipoServicio, objTransferencia, objCooperativa);
                    if (!strRespuesta.ToLower().Contains("Alerta"))
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
                    else
                    {
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                        progress.Cancel();
                        alert.SetTitle("Alerta");
                        alert.SetMessage("No se pudo procesar el pago");

                        RunOnUiThread(() => {
                            alert.Show();
                        });
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

            string cuenta_seleccionada = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            strNumeroCuenta = cuenta_seleccionada;
            foreach (clsCuenta objCuenta in lstCuentas)
            {
                if (objCuenta.numero_cuenta.Equals(cuenta_seleccionada))
                {
                    string saldoCuenta = string.Format("{0:N2}", objCuenta.saldo);

                    lblSaldoCuentaOrigen.Text = "(Saldo disponible USD " + saldoCuenta + ")";
                }
            }

        }

        private clsTransferencia llenarDatosTransferencia()
        {
            clsTransferencia objTransferencia = new clsTransferencia();
            try
            {
                objTransferencia.Id = "0";
                objTransferencia.strIdentificacionEmisor = strIdentificacion;
                objTransferencia.strNumeroCuentaEmisor = strNumeroCuenta;
                clsCuenta objCuenta = lstCuentas.Find(x => x.numero_cuenta == strNumeroCuenta);
                objCooperativa = lstCooperativas.Find(x => x.Id == objCuenta.idCooperativa);
                objTransferencia.strEntidadEmisor = objCooperativa.nombreCooperativa;
                objTransferencia.strIdentificacionBeneficiario = "9999999999";
                objTransferencia.strTipoCuentaBeneficiario = objCuenta.tipo_cuenta;
                objTransferencia.strNumeroCuentaBeneficiario = "99999999999";
                objTransferencia.strEntidadBeneficiario = objCooperativa.nombreCooperativa;
                objTransferencia.dblMonto = double.Parse(txtMontoPago.Text);
                objTransferencia.strMotivo = "PAGO SERVICIO " + objCabeceraFacturaServicio.tipo_suministro +" SUMINISTRO " + txtNumeroSuministro.Text;

            }
            catch (Exception)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                progress.Cancel();
                alert.SetTitle("Alerta");
                alert.SetMessage("Ocurió un problema al realizar el pago");

                RunOnUiThread(() => {
                    alert.Show();
                });
            }
            return objTransferencia;
        }
    }
}
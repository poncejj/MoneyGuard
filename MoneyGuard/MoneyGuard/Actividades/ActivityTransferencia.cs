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
using MoneyGuard.Servicios;
using MoneyGuard.Model;
using System.Threading.Tasks;
using MoneyGuard.Comun;
using Newtonsoft.Json;

namespace MoneyGuard
{
    [Activity(Theme = "@style/MyTheme")]
    public class ActivityTransferencia: AppCompatActivity
    {
        private SupportToolbar mToolBar;
        private Spinner spinnerCuentaOrigen;
        private Spinner spinnerTipoCuenta;
        private Spinner spinnerTipoIdentificacion;
        private Spinner spinnerCooperativasDestino;
        private Button btnRealizarTransferencia;
        private EditText txtIdentificacionBeneficiario;
        private EditText txtCuentaBeneficiario;
        private EditText txtMontoTransferencia;
        private EditText txtMotivoTransferencia;
        Android.App.ProgressDialog progress;
        private int mTransferencia = Resource.String.transferencias;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strCooperativa = string.Empty;
        List<clsCuenta> lstCuentas = new List<clsCuenta>();
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();
        List<string> lstCooperativasString = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Transferencia);

            spinnerCuentaOrigen = FindViewById<Spinner>(Resource.Id.spiCuentaOrigenTrasnfer);
            spinnerCooperativasDestino = FindViewById<Spinner>(Resource.Id.spiCooperativaDestinoTrasnfer);
            spinnerTipoCuenta = FindViewById<Spinner>(Resource.Id.spiTipoCuenta);
            spinnerTipoIdentificacion = FindViewById<Spinner>(Resource.Id.spiTipoIdentificacion);
            btnRealizarTransferencia = FindViewById<Button>(Resource.Id.btnRealizarTransfer);
            txtIdentificacionBeneficiario = FindViewById<EditText>(Resource.Id.txtIdentificacionBeneficiarioTransfer);
            txtCuentaBeneficiario = FindViewById<EditText>(Resource.Id.txtCuentaDestinoTransfer);
            txtMontoTransferencia = FindViewById<EditText>(Resource.Id.txtMontoTransfer);
            txtMotivoTransferencia = FindViewById<EditText>(Resource.Id.txtMotivoTransfer);

            mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar3);
            SetSupportActionBar(mToolBar);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetTitle(mTransferencia);

            //Guardar temporalmente varaibles pantalla
            strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
            strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
            imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
            strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
            strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

            string[] arrRespuesta = strCuentaJson.Split('|');
            string strCuentas = arrRespuesta[0];
            string strTarjetas = arrRespuesta[1];
            lstCuentas = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentas);
            lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);
            
            //spinner cooperativas
            spinnerCooperativasDestino.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelectedCooeprativa);

            foreach(clsCooperativa objCooperativa in lstCooperativas)
            {
                if (!lstCooperativasString.Contains(objCooperativa.nombreCooperativa))
                    lstCooperativasString.Add(objCooperativa.nombreCooperativa);
            }
            var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lstCooperativasString);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            spinnerCooperativasDestino.Adapter = adapter;

            //spinner cuenta de origen OJO
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

            //spinner tipo cuenta
            spinnerTipoCuenta.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter2 = ArrayAdapter.CreateFromResource(
            this, Resource.Array.tipo_cuentas_array, Android.Resource.Layout.SimpleDropDownItem1Line);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            spinnerTipoCuenta.Adapter = adapter2;

            //spinner tipo identificacion
            spinnerTipoIdentificacion.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter3 = ArrayAdapter.CreateFromResource(
            this, Resource.Array.tipo_identificaciones_array, Android.Resource.Layout.SimpleDropDownItem1Line);
            adapter3.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
            spinnerTipoIdentificacion.Adapter = adapter3;

            btnRealizarTransferencia.Click += async delegate
            {
                if (String.IsNullOrEmpty(txtMontoTransferencia.Text)
                || String.IsNullOrEmpty(txtCuentaBeneficiario.Text)
                || String.IsNullOrEmpty(txtIdentificacionBeneficiario.Text))
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
                    progress.SetCancelable(true);
                    progress.Show();

                    //Se realiza la transferencia
                    string strMensaje = await realizarTransferencia();
                    strCuentaJson = await clsActividadesGenericas.modificarCuentaJson(strIdentificacion,strIdUsuario, lstCooperativas);

                    if (strMensaje.ToUpper().Contains("ALERTA"))
                    {
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                        alert.SetTitle("Alerta");
                        alert.SetMessage(strMensaje.Replace("Alerta:",""));
                        alert.SetMessage(strMensaje.Replace("alerta:",""));

                        RunOnUiThread(() => {
                            alert.Show();
                        });
                        progress.Cancel();
                    }
                    else
                    {
                        //Cuando se completa la transferencia 
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

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

        }

        private void spinner_ItemSelectedCooeprativa(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

        }

        private async Task<string> realizarTransferencia()
        {
            clsTransferencia objTransferencia = new clsTransferencia();
            clsServicioTransferencia objServicioTransferencia = new Servicios.clsServicioTransferencia();
            try
            {
                objTransferencia.strIdentificacionEmisor = strIdentificacion;
                objTransferencia.strNumeroCuentaEmisor = spinnerCuentaOrigen.SelectedItem.ToString();
                clsCuenta objCuenta = lstCuentas.Find(x => x.numero_cuenta == objTransferencia.strNumeroCuentaEmisor);
                clsCooperativa objCooperativaEmisor = lstCooperativas.Find(x => x.Id == objCuenta.idCooperativa);
                objTransferencia.strEntidadEmisor = objCooperativaEmisor.nombreCooperativa;
                objTransferencia.strIdentificacionBeneficiario = txtIdentificacionBeneficiario.Text;
                string tipo_cuenta_seleccionada = string.Format("{0}", spinnerTipoCuenta.SelectedItem);
                objTransferencia.strTipoCuentaBeneficiario = tipo_cuenta_seleccionada;
                objTransferencia.strNumeroCuentaBeneficiario = txtCuentaBeneficiario.Text;
                objTransferencia.strEntidadBeneficiario = lstCooperativas[spinnerCooperativasDestino.SelectedItemPosition].nombreCooperativa;
                objTransferencia.dblMonto = double.Parse(txtMontoTransferencia.Text);
                objTransferencia.strMotivo = txtMotivoTransferencia.Text;

                return await objServicioTransferencia.realizarTransferencia(objTransferencia, objCooperativaEmisor, lstCooperativas[spinnerCooperativasDestino.SelectedItemPosition]);
            }
            catch (Exception ex)
            {
                return "Alerta transferencia: " + ex.Message;
            }
        }
    }
}
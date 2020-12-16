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
    [Activity(Label = "ActivityRegistroDatosCuenta", Theme = "@style/MyTheme")]
    public class ActivityAgregarCuenta : AppCompatActivity
    {
        clsServicioCuenta objServicioCuenta = new clsServicioCuenta();
        SupportToolbar mToolBar;
        Spinner spinnerCooperativa;
        Button btnAgregarCuenta;
        EditText txtIdentificacionAgregarCuenta;
        Android.App.ProgressDialog progress;
        private int mAgregarCuenta = Resource.String.agregarCuenta;
        string strIdentificacion = string.Empty;
        string imageUrl = string.Empty;
        string strCuentaJson = string.Empty;
        string strIdUsuario = string.Empty;
        string strCooperativa = string.Empty;
        string idCooperativaSeleccionada = string.Empty;
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();
        List<string> lstCooperativasString = new List<string>();
        clsCooperativa objCooperativa = new clsCooperativa();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.RegistroNuevaCuenta);
                spinnerCooperativa = FindViewById<Spinner>(Resource.Id.spiCooperativaAgregarCuenta);
                btnAgregarCuenta = FindViewById<Button>(Resource.Id.btnAgregarCuenta);
                txtIdentificacionAgregarCuenta = FindViewById<EditText>(Resource.Id.txtIdentificacionAgregarCuenta);
                mToolBar = FindViewById<SupportToolbar>(Resource.Id.toolbar7);
                SetSupportActionBar(mToolBar);

                SupportActionBar.SetHomeButtonEnabled(true);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetTitle(mAgregarCuenta);

                //Guardar temporalmente varaibles pantalla
                strIdentificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
                strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
                imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
                strIdUsuario = Intent.GetStringExtra(clsConstantes.strIdUsuario);
                strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

                lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);

                spinnerCooperativa.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                foreach (clsCooperativa objCooperativa in lstCooperativas)
                {
                    if (!lstCooperativasString.Contains(objCooperativa.nombreCooperativa))
                        lstCooperativasString.Add(objCooperativa.nombreCooperativa);
                }
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lstCooperativasString);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
                spinnerCooperativa.Adapter = adapter;

                btnAgregarCuenta.Click += async delegate
                {
                    if (String.IsNullOrEmpty(txtIdentificacionAgregarCuenta.Text))
                    {
                        Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                        progress.Cancel();
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
                        progress.SetMessage("Agregando cuenta");
                        progress.SetCancelable(true);
                        progress.Show();

                        string strRespuesta = await objServicioCuenta.consultarCuentasRegistradas(strIdentificacion, objCooperativa);
                        if (!strRespuesta.ToUpper().Contains("ERROR"))
                        {

                            if (await objServicioCuenta.registrarCooperativa(strIdUsuario, idCooperativaSeleccionada))
                            {
                                strCuentaJson = objServicioCuenta.unirCuentasJson(strCuentaJson,strRespuesta, idCooperativaSeleccionada);
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
                        else
                        {
                            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                            progress.Cancel();
                            alert.SetTitle("Alerta");
                            alert.SetMessage("Ocurrió un problema al conectar con la cooperativa");

                            RunOnUiThread(() => {
                                alert.Show();
                            });
                        }
                        
                    }
                };
            }
            catch (Exception)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                alert.SetTitle("Alerta");
                alert.SetMessage("Ocurrió un problema al cargar la pantalla");

                RunOnUiThread(() => {
                    alert.Show();
                });
            }
        }

        

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string cooperativa_seleccionada = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            objCooperativa = lstCooperativas[spinner.SelectedItemPosition];
            idCooperativaSeleccionada = lstCooperativas[spinner.SelectedItemPosition].Id;
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
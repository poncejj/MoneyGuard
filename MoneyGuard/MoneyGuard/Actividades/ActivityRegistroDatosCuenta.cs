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
using Android.Graphics;
using System.Net;
using MoneyGuard.Model;
using MoneyGuard.Comun;
using MoneyGuard.Servicios;
using Newtonsoft.Json;

namespace MoneyGuard
{
    [Activity(Label = "ActivityRegistroDatosCuenta", Theme = "@style/MyTheme")]
    public class ActivityRegistroDatosCuenta : Activity
    {
        clsServicioCuenta objServicioCuenta = new clsServicioCuenta();
        Spinner spinner;
        Button btnSiguiente;
        EditText txtIdentificacion;
        ImageView imgFotoPerfil;
        clsUsuario objUsuario = new clsUsuario();
        Android.App.ProgressDialog progress;
        string strCooperativa = string.Empty;
        string idCooperativa = string.Empty;
        List<clsCooperativa> lstCooperativas = new List<clsCooperativa>();
        List<string> lstCooperativasString = new List<string>();
        clsCooperativa objCooperativa = new clsCooperativa();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            string strJsonCuenta = string.Empty;
            try
            {
                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.RegistroDatosCuenta);

                spinner = FindViewById<Spinner>(Resource.Id.spiCooperativa);
                btnSiguiente = FindViewById<Button>(Resource.Id.btnSiguiente);
                txtIdentificacion = FindViewById<EditText>(Resource.Id.txtIdentificacionRegistroDatosCuenta);
                imgFotoPerfil = FindViewById<ImageView>(Resource.Id.imgFoto);

                objUsuario.Id = Intent.GetStringExtra(clsConstantes.strIdUsuario);
                objUsuario.displayName = Intent.GetStringExtra(clsConstantes.strNombreUsuario);
                objUsuario.birthday = Intent.GetStringExtra(clsConstantes.strFechaNacimiento);
                objUsuario.relationshipStatus = Intent.GetStringExtra(clsConstantes.strEstadoCivilUsuario);
                objUsuario.gender = Intent.GetStringExtra(clsConstantes.strGeneroUsuario);
                objUsuario.imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
                strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

                lstCooperativas = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);

                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
                foreach (clsCooperativa objCooperativa in lstCooperativas)
                {
                    if (!lstCooperativasString.Contains(objCooperativa.nombreCooperativa))
                        lstCooperativasString.Add(objCooperativa.nombreCooperativa);
                }
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleDropDownItem1Line, lstCooperativasString);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleDropDownItem1Line);
                spinner.Adapter = adapter;

                btnSiguiente.Click += async (sender, e) =>
                {
                    if (String.IsNullOrEmpty(txtIdentificacion.Text))
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
                        string strIdentificacionCliente = txtIdentificacion.Text;

                        progress = new Android.App.ProgressDialog(this);
                        progress.Indeterminate = true;
                        progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                        progress.SetMessage("Conectando con la cooperativa");
                        progress.SetCancelable(false);
                        progress.Show();

                        strJsonCuenta = await objServicioCuenta.consultarCuentasRegistradas(strIdentificacionCliente, objCooperativa);

                        if (strJsonCuenta.Length != 0 && !strJsonCuenta.ToUpper().Contains("ERROR"))
                        {
                            var intent = new Intent(this, typeof(ActivityPIN));
                            intent.PutExtra(clsConstantes.strIdUsuario, objUsuario.Id);
                            intent.PutExtra(clsConstantes.strNombreUsuario, objUsuario.displayName);
                            intent.PutExtra(clsConstantes.strFechaNacimiento, objUsuario.birthday);
                            intent.PutExtra(clsConstantes.strEstadoCivilUsuario, objUsuario.relationshipStatus);
                            intent.PutExtra(clsConstantes.strGeneroUsuario, objUsuario.gender);
                            intent.PutExtra(clsConstantes.strURLImagenUsuario, objUsuario.imageUrl);
                            intent.PutExtra(clsConstantes.strIdentificacionUsuario, strIdentificacionCliente);
                            intent.PutExtra(clsConstantes.strIdentificacionCooperativa, spinner.SelectedItemId.ToString());
                            intent.PutExtra(clsConstantes.strTipoTransaccion, clsConstantes.strNuevaCuenta);
                            intent.PutExtra(clsConstantes.idCooperativa, idCooperativa);
                            intent.PutExtra(clsConstantes.strCuentaJson, strJsonCuenta);
                            intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                            StartActivity(intent);
                            this.Finish();
                        }
                        else
                        {
                            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                            alert.SetTitle("Alerta");
                            alert.SetMessage(strJsonCuenta);

                            RunOnUiThread(() => {
                                alert.Show();
                            });

                            progress.Cancel();
                        }
                    }
                };


                var imageBitmap = GetImageBitmapFromUrl(objUsuario.imageUrl);
                imgFotoPerfil.SetImageBitmap(imageBitmap);

            }
            catch (Exception)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                progress.Cancel();
                alert.SetTitle("Alerta");
                alert.SetMessage("Ocurió un problema al cargar la pantalla");

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
            idCooperativa = lstCooperativas[spinner.SelectedItemPosition].Id;
        }
        
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}
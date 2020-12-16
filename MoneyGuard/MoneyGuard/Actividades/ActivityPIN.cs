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
using MoneyGuard.Model;
using MoneyGuard.Comun;
using MoneyGuard.Servicios;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoneyGuard
{
    [Activity(Theme = "@style/MyTheme")]
    public class ActivityPIN : Activity
    {
        Button btnUno;
        Button btnDos;
        Button btnTres;
        Button btnCuatro;
        Button btnCinco;
        Button btnSeis;
        Button btnSiete;
        Button btnOcho;
        Button btnNueve;
        Button btnCero;
        Button btnBorrar;
        EditText txtPassword;
        TextView lblMensaje;
        clsUsuario objUsuario = new clsUsuario();
        string strPassword = string.Empty;
        string strValorNumero = string.Empty;
        string strTipo;
        string strCuentaJson = string.Empty;
        string strCooperativa = string.Empty;
        string idCooperativa = string.Empty;
        List<clsCooperativa> lstCooperativa = new List<clsCooperativa>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            strTipo = Intent.GetStringExtra(clsConstantes.strTipoTransaccion);
            
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PinSeguridad);

            if (strTipo == clsConstantes.strNuevaCuenta)
            {
                string strNombreUsuario = Intent.GetStringExtra(clsConstantes.strNombreUsuario);
                string strIdentificacionUsuario = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
                objUsuario.Id = Intent.GetStringExtra(clsConstantes.strIdUsuario);
                objUsuario.displayName = strNombreUsuario;
                objUsuario.birthday = Intent.GetStringExtra(clsConstantes.strFechaNacimiento);
                objUsuario.relationshipStatus = Intent.GetStringExtra(clsConstantes.strEstadoCivilUsuario);
                objUsuario.gender = Intent.GetStringExtra(clsConstantes.strGeneroUsuario);
                objUsuario.imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
                objUsuario.identificacion = strIdentificacionUsuario;
                strCuentaJson = Intent.GetStringExtra(clsConstantes.strCuentaJson);
                idCooperativa = Intent.GetStringExtra(clsConstantes.idCooperativa);
                strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);
                lstCooperativa = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);
            }
            else
            {
                clsServicioCuenta objServicioCuenta = new clsServicioCuenta();
                objUsuario.Id = Intent.GetStringExtra(clsConstantes.strIdUsuario);
                objUsuario.pin = Intent.GetStringExtra(clsConstantes.strPINUsuario);
                objUsuario.displayName = Intent.GetStringExtra(clsConstantes.strNombreUsuario);
                objUsuario.imageUrl = Intent.GetStringExtra(clsConstantes.strURLImagenUsuario);
                objUsuario.identificacion = Intent.GetStringExtra(clsConstantes.strIdentificacionUsuario);
                strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);
                lstCooperativa = JsonConvert.DeserializeObject<List<clsCooperativa>>(strCooperativa);
                strCuentaJson = await objServicioCuenta.consultarCuentasRegistradas(objUsuario.identificacion, objUsuario.Id, lstCooperativa);
            }

            // Buscar en la pantalla botones
            lblMensaje = FindViewById<TextView>(Resource.Id.lblMensajePin);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPasswordPin);
            btnCero = FindViewById<Button>(Resource.Id.btn0);
            btnUno = FindViewById<Button>(Resource.Id.btn1);
            btnDos = FindViewById<Button>(Resource.Id.btn2);
            btnTres = FindViewById<Button>(Resource.Id.btn3);
            btnCuatro = FindViewById<Button>(Resource.Id.btn4);
            btnCinco = FindViewById<Button>(Resource.Id.btn5);
            btnSeis = FindViewById<Button>(Resource.Id.btn6);
            btnSiete = FindViewById<Button>(Resource.Id.btn7);
            btnOcho = FindViewById<Button>(Resource.Id.btn8);
            btnNueve = FindViewById<Button>(Resource.Id.btn9);
            btnBorrar = FindViewById<Button>(Resource.Id.btnClear);

            lblMensaje.SetTextColor(Color.White);

            btnUno.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "1";
                btnNumero_Click(sender, args);
            };
            btnDos.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "2";
                btnNumero_Click(sender, args);
            };
            btnTres.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "3";
                btnNumero_Click(sender, args);
            };
            btnCuatro.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "4";
                btnNumero_Click(sender, args);
            };
            btnCinco.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "5";
                btnNumero_Click(sender, args);
            };
            btnSeis.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "6";
                btnNumero_Click(sender, args);
            };
            btnSiete.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "7";
                btnNumero_Click(sender, args);
            };
            btnOcho.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "8";
                btnNumero_Click(sender, args);
            };
            btnNueve.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "9";
                btnNumero_Click(sender, args);
            };
            btnCero.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = "0";
                btnNumero_Click(sender, args);
            };
            btnBorrar.Click += (object sender, EventArgs args) =>
            {
                strValorNumero = string.Empty;
                btnClear_Click(sender, args);
            };
        }

        private async void btnNumero_Click(object sender, EventArgs e)
        {
            lblMensaje.SetTextColor(Color.White);
            string strNumero = strValorNumero;
            string strPasswordDesencriptada = objUsuario.pin;
            strPassword += strNumero;
            txtPassword.Text = strPassword;

            if (strPassword.Length == 4)
            {
                if(strTipo == clsConstantes.strNuevaCuenta)
                {
                    strPasswordDesencriptada = strPassword;
                    if(await registrarUsuario())
                    {
                        validarPasword();
                    }
                }
                else
                {
                    validarPasword();
                }
            }
        }

        private async Task<bool> registrarUsuario()
        {
            bool boolRespuesta = false;
            string strCuentaJsonDesencriptada = strCuentaJson;
            string[] arrRespuesta = strCuentaJsonDesencriptada.Split('|');
            string strCuentas = arrRespuesta[0];
            List<clsCuenta> lstCuentas = JsonConvert.DeserializeObject<List<clsCuenta>>(strCuentas);

            clsServicioUsuario objServicioUsuario = new clsServicioUsuario();
            clsServicioCuenta objServicioCuenta = new clsServicioCuenta();
            if(lstCuentas.Count > 0)
            {
                if (await objServicioUsuario.registrarUsuario(objUsuario))
                {
                    boolRespuesta = await objServicioCuenta.registrarCooperativa(objUsuario.Id,idCooperativa);
                }
            }
            
            return boolRespuesta;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblMensaje.SetTextColor(Color.White);
            txtPassword.Text = string.Empty;
            strPassword = string.Empty;
        }

        private void validarPasword()
        {
            clsServicioCuenta objServicioCuenta = new clsServicioCuenta();
            if (strPassword == objUsuario.pin)
            {
                strPassword = "";
                txtPassword.Text = "";
                var intent = new Intent(this, typeof(ActivityMenu));
                intent.PutExtra(clsConstantes.strIdentificacionUsuario, objUsuario.identificacion);
                intent.PutExtra(clsConstantes.strURLImagenUsuario, objUsuario.imageUrl);
                intent.PutExtra(clsConstantes.strIdUsuario, objUsuario.Id);
                intent.PutExtra(clsConstantes.strCuentaJson, strCuentaJson);
                intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                StartActivity(intent);
            }
            else
            {
                strPassword = "";
                txtPassword.Text = "";
                lblMensaje.SetTextColor(Color.Red);
                lblMensaje.Text = "PIN Incorrecto";
            }
        }
    }
}
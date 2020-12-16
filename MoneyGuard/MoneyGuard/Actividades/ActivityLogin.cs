using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using System;
using Android.Gms.Plus;
using Android.Content;
using Android.Runtime;
using Android.Gms.Plus.Model.People;
using MoneyGuard.Model;
using MoneyGuard.Servicios;
using System.Threading.Tasks;
using MoneyGuard.Comun;

namespace MoneyGuard
{
    [Activity(Label = "MoneyGuard", Icon = "@drawable/icon", Theme = "@style/MyTheme")]
    public class ActivityLogin : Activity, IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener
    {
        private IGoogleApiClient mGoogleApiClient;
        private SignInButton mGoogleSingIn;
        private ConnectionResult mConnectionResult;
        private bool mIntentInProgress;
        private bool mSignInClicked;
        public clsUsuario objUsuario = new clsUsuario();
        Android.App.ProgressDialog progress;
        string strCooperativa = string.Empty;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            strCooperativa = Intent.GetStringExtra(clsConstantes.strCooperativas);

            mGoogleSingIn = FindViewById<SignInButton>(Resource.Id.sign_in_button);

            mGoogleSingIn.Click += mGoogleSignIn_Click;

            GoogleApiClientBuilder builder = new GoogleApiClientBuilder(this);
            builder.AddConnectionCallbacks(this);
            builder.AddOnConnectionFailedListener(this);
            builder.AddApi(PlusClass.API);
            builder.AddScope(PlusClass.ScopePlusProfile);
            builder.AddScope(PlusClass.ScopePlusLogin);

            mGoogleApiClient = builder.Build();
        }

        void mGoogleSignIn_Click(object sender, EventArgs e)
        {
            if (!mGoogleApiClient.IsConnecting)
            {
                mSignInClicked = true;
                ResolveSignInAlerta();
            }
        }

        private void ResolveSignInAlerta()
        {
            if (mGoogleApiClient.IsConnected)
            {
                return;
            }
            if (mConnectionResult.HasResolution)
            {
                try
                {
                    mIntentInProgress = true;
                    StartIntentSenderForResult(mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
                }
                catch (Android.Content.IntentSender.SendIntentException e)
                {
                    mIntentInProgress = false;
                    mGoogleApiClient.Connect();
                }
            }
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 0)
            {
                if (resultCode != Result.Ok)
                {
                    mSignInClicked = false;
                }
                mIntentInProgress = false;

                if (!mGoogleApiClient.IsConnecting)
                {
                    mGoogleApiClient.Connect();
                }
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            mGoogleApiClient.Connect();
        }

        protected override void OnStop()
        {
            base.OnStop();
            if (mGoogleApiClient.IsConnected)
                mGoogleApiClient.Disconnect();
        }

        public async void OnConnected(Bundle connectionHint)
        {
            try
            {
                mSignInClicked = false;

                IPerson plusUser = PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient);
                if (plusUser.HasId)
                    objUsuario.Id += plusUser.Id;
                
                //Si usuario esta registrado va al ingreso del PIN
                if (await ExisteUsuarioRegistrado())
                {
                    var intent = new Intent(this, typeof(ActivityPIN));
                    intent.PutExtra(clsConstantes.strIdUsuario, objUsuario.Id);
                    intent.PutExtra(clsConstantes.strURLImagenUsuario, objUsuario.imageUrl);
                    intent.PutExtra(clsConstantes.strPINUsuario, objUsuario.pin);
                    intent.PutExtra(clsConstantes.strNombreUsuario, objUsuario.displayName);
                    intent.PutExtra(clsConstantes.strIdentificacionUsuario, objUsuario.identificacion);
                    intent.PutExtra(clsConstantes.strTipoTransaccion, clsConstantes.strCuentaExistente);
                    intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                    StartActivity(intent);
                    this.Finish();

                }
                //Caso contrario se va a registrar su usuario
                else
                {
                    if (PlusClass.PeopleApi.GetCurrentPerson(mGoogleApiClient) != null)
                    {
                        if (plusUser.HasDisplayName)
                            objUsuario.displayName += plusUser.DisplayName;
                        else
                            objUsuario.displayName += "";

                        if (plusUser.HasRelationshipStatus)
                            objUsuario.relationshipStatus += clsConstantes.ObtenerEstadoCivilUsuario(plusUser.RelationshipStatus);
                        else
                            objUsuario.relationshipStatus += "";

                        if (plusUser.HasGender)
                            objUsuario.gender += clsConstantes.ObtenerGeneroUsuario(plusUser.Gender);
                        else
                            objUsuario.gender += "";

                        if (plusUser.HasBirthday)
                            objUsuario.birthday += plusUser.Birthday;
                        else
                            objUsuario.birthday += "";

                        if (plusUser.HasImage)
                            objUsuario.imageUrl = plusUser.Image.Url.Replace("?sz=50", "");
                        else
                            objUsuario.imageUrl = "";

                        var intent = new Intent(this, typeof(ActivityRegistroDatosCuenta));
                        intent.PutExtra(clsConstantes.strIdUsuario, objUsuario.Id);
                        intent.PutExtra(clsConstantes.strNombreUsuario, objUsuario.displayName);
                        intent.PutExtra(clsConstantes.strFechaNacimiento, objUsuario.birthday);
                        intent.PutExtra(clsConstantes.strEstadoCivilUsuario, objUsuario.relationshipStatus);
                        intent.PutExtra(clsConstantes.strGeneroUsuario, objUsuario.gender);
                        intent.PutExtra(clsConstantes.strURLImagenUsuario, objUsuario.imageUrl);
                        intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
                        StartActivity(intent);
                        this.Finish();
                    }
                }
            }
            catch(Exception ex)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);

                alert.SetTitle("Alerta");
                alert.SetMessage("Ocurió un problema al consultar el usuario");

                RunOnUiThread(() => {
                    alert.Show();
                }); ;
            }
        }

        private async Task<bool> ExisteUsuarioRegistrado()
        {
            clsServicioUsuario objServicioUsuario = new clsServicioUsuario();
            clsUsuario objUsuarioRespuesta = new clsUsuario();
            try
            {
                progress = new Android.App.ProgressDialog(this);
                progress.Indeterminate = true;
                progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
                progress.SetMessage("Consultando Usuario");
                progress.SetCancelable(false);
                progress.Show();

                objUsuarioRespuesta = await objServicioUsuario.consultarUsuario(objUsuario.Id);

                if (objUsuarioRespuesta != null)
                {
                    objUsuario = objUsuarioRespuesta;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
                progress.Cancel();
                alert.SetTitle("Alerta");
                alert.SetMessage("Ocurrió un problema al validar usuario registrado");

                RunOnUiThread(() => {
                    alert.Show();
                });
            }
            return false;
        }

        

        public void OnConnectionFailed(ConnectionResult result)
        {
            if (!mIntentInProgress)
            {
                mConnectionResult = result;

                if (mSignInClicked)
                {
                    ResolveSignInAlerta();
                }
            }
        }

        public void OnConnectionSuspended(int cause)
        {
            Android.Support.V7.App.AlertDialog.Builder alert = new Android.Support.V7.App.AlertDialog.Builder(this);
            progress.Cancel();
            alert.SetTitle("Alerta");
            alert.SetMessage("Ocurió un problema con la conexión");

            RunOnUiThread(() => {
                alert.Show();
            });
        }
    }
}


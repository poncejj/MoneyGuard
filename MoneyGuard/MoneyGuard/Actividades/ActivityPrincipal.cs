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
using System.Threading.Tasks;
using Android.Content.PM;
using MoneyGuard.Servicios;
using MoneyGuard.Comun;

namespace MoneyGuard.Actividades
{
    [Activity(Label = "MoneyGuard", Icon = "@drawable/icon",MainLauncher = true, NoHistory = true, Theme = "@style/MyTheme", Immersive = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class ActivityPrincipal : Activity   
    {
        string strCooperativa = string.Empty;
        string strCatalogo = string.Empty;
        clsServicioConsulta objServicioConsulta = new clsServicioConsulta();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PantallaPrincipal);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        async void SimulateStartup()
        {
            //await Task.Delay(2000); // Simulate a bit of startup work.
            //strCatalogo = await objServicioConsulta.consultarCatalogo();
            strCooperativa = await objServicioConsulta.consultarCooperativas();
            var intent = new Intent(this, typeof(ActivityLogin));
            intent.PutExtra(clsConstantes.strCooperativas, strCooperativa);
            StartActivity(intent);
        }
    }
}
using apptesisService.DataObjects;
using apptesisService.Models;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;

namespace apptesisService
{
    public class Startup : IStartup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.EnableSystemDiagnosticsTracing();
            new MobileAppConfiguration().UseDefaultConfiguration().ApplyTo(config);
            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();
            bool flag = string.IsNullOrEmpty(settings.HostName);
            if (flag)
            {
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new string[]
                    {
                        ConfigurationManager.AppSettings["ValidAudience"]
                    },
                    ValidIssuers = new string[]
                    {
                        ConfigurationManager.AppSettings["ValidIssuer"]
                    },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }

        public void Configuration(IAppBuilder app)
        {
            Startup.ConfigureMobileApp(app);
        }
    }
}

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System;
using MoneyGuard.Model;

namespace MoneyGuard.Servicios
{
    public class clsServicioUsuario
    {
        string applicationURL = @"https://apptesis.azurewebsites.net";

        private MobileServiceClient client;

        public async Task<clsUsuario> consultarUsuario(string id)
        {
            client = new MobileServiceClient(applicationURL);
            clsUsuario objUsuario = new clsUsuario();

            try
            {
                objUsuario = await client.GetTable<clsUsuario>().LookupAsync(id);
                return objUsuario;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al consultar usuario: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> registrarUsuario(clsUsuario objUsuario)
        {
            client = new MobileServiceClient(applicationURL);
            try
            {
                await client.GetTable<clsUsuario>().InsertAsync(objUsuario);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Alerta al ingresar el usuario: " + ex.Message);
                return false;
            }
        }
    }
}
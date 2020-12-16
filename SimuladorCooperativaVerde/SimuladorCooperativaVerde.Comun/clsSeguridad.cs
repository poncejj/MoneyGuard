using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace SimuladorCooperativaVerde.Comun
{
    public static class clsSeguridad
    {
        private static readonly byte[] salt = Encoding.ASCII.GetBytes("Xamarin.iOS Version: 7.0.6.168");

        public static string Encriptar(string strTextoEncriptar, string strPassword)
        {
            var algorithm = ObtenerAlgorithmo(strPassword);

            if (strTextoEncriptar == null || strTextoEncriptar == "") return "";

            byte[] bytEncriptados;
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV))
            {
                byte[] bytAEncriptar = Encoding.UTF8.GetBytes(strTextoEncriptar);
                bytEncriptados = EncritacionMemoria(bytAEncriptar, encryptor);
            }
            return Convert.ToBase64String(bytEncriptados);
        }

        public static string Desencriptar(string strTextoEncriptar, string strPassword)
        {
            var algorithm = ObtenerAlgorithmo(strPassword);

            if (strTextoEncriptar == null || strTextoEncriptar == "") return "";

            byte[] descryptedBytes;
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV))
            {
                byte[] encryptedBytes = Convert.FromBase64String(strTextoEncriptar);
                descryptedBytes = EncritacionMemoria(encryptedBytes, decryptor);
            }
            return Encoding.UTF8.GetString(descryptedBytes);
        }

        private static byte[] EncritacionMemoria(byte[] bytDatos, ICryptoTransform transform)
        {
            MemoryStream memory = new MemoryStream();
            using (Stream stream = new CryptoStream(memory, transform, CryptoStreamMode.Write))
            {
                stream.Write(bytDatos, 0, bytDatos.Length);
            }
            return memory.ToArray();
        }

        private static RijndaelManaged ObtenerAlgorithmo(string strPasswordEncriptacion)
        {
            var llave = new Rfc2898DeriveBytes(strPasswordEncriptacion, salt);

            var algorithm = new RijndaelManaged();
            int bytesForKey = algorithm.KeySize / 8;
            int bytesForIV = algorithm.BlockSize / 8;
            algorithm.Key = llave.GetBytes(bytesForKey);
            algorithm.IV = llave.GetBytes(bytesForIV);
            return algorithm;
        }
    }
}
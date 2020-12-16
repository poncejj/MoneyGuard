using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsConexion
    {
        public SqlConnection conn;
        public void abrirConexion(string strServicio)
        {
            try
            {
                string strCadenaConexion = ConsultarCadenaConexion(strServicio);
                conn = new SqlConnection(strCadenaConexion);
                conn.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string ConsultarCadenaConexion(string strServicio)
        {
            string strRespuesta = string.Empty;

            try
            {
                if(strServicio.Equals("AppMovil"))
                {
                    strRespuesta = CadenasConexion.AppMovil;
                }
                else
                if (strServicio.Equals("CooperativaAmarilla"))
                {
                    strRespuesta = CadenasConexion.CooperativaAmarilla;
                }
                else
                if (strServicio.Equals("CooperativaVerde"))
                {
                    strRespuesta = CadenasConexion.CooperativaVerde;
                }
                else
                if (strServicio.Equals("ServiciosBasicos"))
                {
                    strRespuesta = CadenasConexion.ServiciosBasicos;
                }
            }
            catch(Exception)
            {
                throw;
            }
            return strRespuesta;
        }

        public void cerrarConexion()
        {
            conn.Close();
        }

        public void commit()
        {
            try
            {
                String sql1 = "COMMIT";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void rollback()
        {
            String sql1 = "ROLLBACK";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apptesis.Persistencia
{
    public class clsConexion
    {
        public SqlConnection conn;
        public void abrirConexion()
        {
            try
            {
                string strCadenaConexion = Conexion.alertaConnectionSstring;
                conn = new SqlConnection(strCadenaConexion);

                conn.Open();
            }
            catch (Exception)
            {
                throw;
            }
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

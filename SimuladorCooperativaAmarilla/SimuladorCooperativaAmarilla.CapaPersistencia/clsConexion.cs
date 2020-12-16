using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCooperativaAmarilla.CapaPersistencia
{
    public class clsConexion
    {
        public SqlConnection conn;
        public void abrirConexion()
        {
            try
            {
                    string cadenaEntidad1 = "Server=tcp:e3l0y6e554.database.windows.net,1433;Initial Catalog=CooperativaAmarilla;Persist Security Info=False;User ID=prueba1;Password=Richd@d20152015;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    conn = new SqlConnection(cadenaEntidad1);
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

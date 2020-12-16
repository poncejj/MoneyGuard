using System;
using System.Data;
using System.Data.SqlClient;

namespace apptesis.Persistencia
{
    public class clsPersistenciaAlerta
    {
        clsConexion connect;
        public void InsertarAlertas(string strTituloAlerta, string strMensajeAlerta)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_agregar_alerta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@titulo_alerta", SqlDbType.VarChar)).Value = strTituloAlerta;
                cmd.Parameters.Add(new SqlParameter("@mensaje_alerta", SqlDbType.VarChar)).Value = strMensajeAlerta;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connect.cerrarConexion();
            }
        }
    }
}
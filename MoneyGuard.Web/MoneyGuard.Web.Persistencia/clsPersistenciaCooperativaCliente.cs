using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaCooperativaCliente
    {
        clsConexion connect;
        public DataTable ConsultarCooperativaUsuario(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_cooperativaCliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsRespuesta);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsRespuesta != null)
                    dsRespuesta.Dispose();
            }
            return dsRespuesta.Tables[0];
        }

        public bool ModificarCooperativaUsuario(string strServicio, string strIdCooperativaUsuario, string strIdUsuario,
            string strIdCooperativa, bool boolEliminadoCooperativaUsuario)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_cooperativaCliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cooperativaCliente", SqlDbType.VarChar)).Value = strIdCooperativaUsuario;
                cmd.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.VarChar)).Value = strIdUsuario;
                cmd.Parameters.Add(new SqlParameter("@id_cooperativa", SqlDbType.VarChar)).Value = strIdCooperativa;
                cmd.Parameters.Add(new SqlParameter("@eliminado_cooperativaCliente", SqlDbType.Bit)).Value = boolEliminadoCooperativaUsuario;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connect.cerrarConexion();
            }
        }

        public bool EliminarCooperativaUsuario(string strServicio, string strIdCooperativaUsuario)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_cooperativaCliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cooperativaCliente", SqlDbType.VarChar)).Value = strIdCooperativaUsuario;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connect.cerrarConexion();
            }
        }
    }
}

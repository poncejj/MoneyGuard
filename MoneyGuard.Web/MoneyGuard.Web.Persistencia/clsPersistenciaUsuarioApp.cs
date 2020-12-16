using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaUsuario
    {
        clsConexion connect;
        public DataTable ConsultarTodoUsuario(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_usuarios", connect.conn);
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

        public bool ModificarUsuarioApp(string strServicio, string strIdUsuario, string strIdentificacionUsuario,
            string strNombreUsuario, string strPinUsuario, bool boolEliminadoUsuario)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_usuario", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.VarChar)).Value = strIdUsuario;
                cmd.Parameters.Add(new SqlParameter("@nombre_usuario", SqlDbType.VarChar)).Value = strNombreUsuario;
                cmd.Parameters.Add(new SqlParameter("@identificacion_usuario", SqlDbType.VarChar)).Value = strIdentificacionUsuario;
                cmd.Parameters.Add(new SqlParameter("@pin_usuario", SqlDbType.VarChar)).Value = strPinUsuario;
                cmd.Parameters.Add(new SqlParameter("@eliminado_usuario", SqlDbType.Bit)).Value = boolEliminadoUsuario;
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

        public bool EliminarUsuarioApp(string strServicio, string strIdUsuario)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_usuario", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_usuario", SqlDbType.VarChar)).Value = strIdUsuario;
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

        public bool InicioSession(string strServicio, string email, string password)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_usuarios_web", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@usuario", SqlDbType.VarChar)).Value = email;
                cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar)).Value = password;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsRespuesta);
                if (dsRespuesta.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsRespuesta != null)
                    dsRespuesta.Dispose();
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaCliente
    {
        clsConexion connect;
        public DataTable ConsultarCliente(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_clientes", connect.conn);
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

        public bool ModificarCliente(string strServicio,int idCliente, string strIdentificacionCliente, 
            string strNombreCliente, string strApellidoCliente, string strTelefonoCliente,
            string strEmailCliente)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = strIdentificacionCliente;
                cmd.Parameters.Add(new SqlParameter("@nombre_cliente", SqlDbType.VarChar)).Value = strNombreCliente;
                cmd.Parameters.Add(new SqlParameter("@apellido_cliente", SqlDbType.VarChar)).Value = strApellidoCliente;
                cmd.Parameters.Add(new SqlParameter("@telefono_cliente", SqlDbType.VarChar)).Value = strTelefonoCliente;
                cmd.Parameters.Add(new SqlParameter("@email_cliente", SqlDbType.VarChar)).Value = strEmailCliente;
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

        public bool EliminarCliente(string strServicio, int idCliente)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connect.cerrarConexion();
            }
        }

        public bool AgregarCliente(string strServicio, string strIdentificacionCliente,
            string strNombreCliente, string strApellidoCliente, string strTelefonoCliente,
            string strEmailCliente)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_insertar_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = strIdentificacionCliente;
                cmd.Parameters.Add(new SqlParameter("@nombre_cliente", SqlDbType.VarChar)).Value = strNombreCliente;
                cmd.Parameters.Add(new SqlParameter("@apellido_cliente", SqlDbType.VarChar)).Value = strApellidoCliente;
                cmd.Parameters.Add(new SqlParameter("@telefono_cliente", SqlDbType.VarChar)).Value = strTelefonoCliente;
                cmd.Parameters.Add(new SqlParameter("@email_cliente", SqlDbType.VarChar)).Value = strEmailCliente;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
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

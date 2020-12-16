using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaSuministro
    {
        clsConexion connect;
        public DataTable ConsultarSuministro(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_suministro", connect.conn);
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

        public bool ModificarSuministro(string strServicio, int idSuministro,
            int idServicioBasico, string strNumeroSuministro, int idCliente,
            int idDireccion, string strDescripcionDireccion, int idParroquia)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_suministro", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_suministro", SqlDbType.Int)).Value = idSuministro;
                cmd.Parameters.Add(new SqlParameter("@id_servicio_basico", SqlDbType.Int)).Value = idServicioBasico;
                cmd.Parameters.Add(new SqlParameter("@numero_suministro", SqlDbType.VarChar)).Value = strNumeroSuministro;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;
                cmd.Parameters.Add(new SqlParameter("@id_direccion", SqlDbType.Int)).Value = idDireccion;
                cmd.Parameters.Add(new SqlParameter("@descripcion_direccion", SqlDbType.VarChar)).Value = strDescripcionDireccion;
                cmd.Parameters.Add(new SqlParameter("@id_parroquia", SqlDbType.Int)).Value = idParroquia;
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

        public bool EliminarSuministro(string strServicio, int idSuministro)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_suministro", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_suministro", SqlDbType.Int)).Value = idSuministro;
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

        public bool AgregarSuministro(string strServicio, int idServicioBasico, 
            string strNumeroSuministro, int idCliente, string strDescripcionDireccion, 
            int idParroquia)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_agregar_suministro", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_servicio_basico", SqlDbType.Int)).Value = idServicioBasico;
                cmd.Parameters.Add(new SqlParameter("@numero_suministro", SqlDbType.VarChar)).Value = strNumeroSuministro;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;
                cmd.Parameters.Add(new SqlParameter("@descripcion_direccion", SqlDbType.VarChar)).Value = strDescripcionDireccion;
                cmd.Parameters.Add(new SqlParameter("@id_parroquia", SqlDbType.Int)).Value = idParroquia;
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

        public DataTable ConsultarTipoServicios(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_tipo_servicio", connect.conn);
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

        public DataTable ConsultarParroquias(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_parroquias", connect.conn);
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
    }
}

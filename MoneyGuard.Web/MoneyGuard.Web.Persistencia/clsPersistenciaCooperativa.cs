using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaCooperativa
    {
        clsConexion connect;
        public DataTable ConsultarCooperativa(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_cooperativa", connect.conn);
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

        public bool ModificarCooperativa(string strServicio, string strIdCooperativa, string strNombreCooperativa,
            string strNombreColaEnvio, string strNombreColaRespuesta, string strNombreBusServicios, bool boolEliminadoCooperativa)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_cooperativa", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cooperativa", SqlDbType.VarChar)).Value = strIdCooperativa;
                cmd.Parameters.Add(new SqlParameter("@nombre_cooperativa", SqlDbType.VarChar)).Value = strNombreCooperativa;
                cmd.Parameters.Add(new SqlParameter("@nombre_cola_envio_cooperativa", SqlDbType.VarChar)).Value = strNombreColaEnvio;
                cmd.Parameters.Add(new SqlParameter("@nombre_cola_respuesta_cooperativa", SqlDbType.VarChar)).Value = strNombreColaRespuesta;
                cmd.Parameters.Add(new SqlParameter("@nombre_bus_servicio_cooperativa", SqlDbType.VarChar)).Value = strNombreBusServicios;
                cmd.Parameters.Add(new SqlParameter("@eliminado_cooperativa", SqlDbType.Bit)).Value = boolEliminadoCooperativa;
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

        public bool EliminarCooperativa(string strServicio, string strIdCooperativa)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_cooperativa", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cooperativa", SqlDbType.VarChar)).Value = strIdCooperativa;
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

        public bool AgregarCooperativa(string strServicio, string strNombreCooperativa, 
            string strNombreColaEnvio, string strNombreColaRespuesta, 
            string strNombreBusServicios, bool boolEliminadoCooperativa)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_agregar_cooperativa", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre_cooperativa", SqlDbType.VarChar)).Value = strNombreCooperativa;
                cmd.Parameters.Add(new SqlParameter("@nombre_cola_envio_cooperativa", SqlDbType.VarChar)).Value = strNombreColaEnvio;
                cmd.Parameters.Add(new SqlParameter("@nombre_cola_respuesta_cooperativa", SqlDbType.VarChar)).Value = strNombreColaRespuesta;
                cmd.Parameters.Add(new SqlParameter("@nombre_bus_servicio_cooperativa", SqlDbType.VarChar)).Value = strNombreBusServicios;
                cmd.Parameters.Add(new SqlParameter("@eliminado_cooperativa", SqlDbType.Bit)).Value = boolEliminadoCooperativa;
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

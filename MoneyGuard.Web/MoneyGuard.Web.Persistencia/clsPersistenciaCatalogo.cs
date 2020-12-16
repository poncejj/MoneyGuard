using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaCatalogo
    {
        clsConexion connect;
        public DataTable ConsultarCatalogo(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_catalogo", connect.conn);
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

        public bool ModificarCatalogo(string strServicio, string strIdCatalogo, string strNombreCatalogo,
            bool blCatalogoPadre, string strIdCatalogoPadre, bool boolEliminadoCatalogo)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_catalogo", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_catalogo", SqlDbType.VarChar)).Value = strIdCatalogo;
                cmd.Parameters.Add(new SqlParameter("@nombre_catalogo", SqlDbType.VarChar)).Value = strNombreCatalogo;
                cmd.Parameters.Add(new SqlParameter("@catalogo_padre", SqlDbType.Bit)).Value = blCatalogoPadre;
                cmd.Parameters.Add(new SqlParameter("@id_catalogo_padre", SqlDbType.VarChar)).Value = strIdCatalogoPadre;
                cmd.Parameters.Add(new SqlParameter("@eliminado_catalogo", SqlDbType.Bit)).Value = boolEliminadoCatalogo;
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

        public bool EliminarCatalogo(string strServicio, string strIdCatalogo)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_catalogo", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_catalogo", SqlDbType.VarChar)).Value = strIdCatalogo;
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

        public bool AgregarCatalogo(string strServicio, string strNombreCatalogo,
            bool blCatalogoPadre, string strIdCtalogoPadre,
            bool boolEliminadoCatalogo)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_agregar_catalogo", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@nombre_catalogo", SqlDbType.VarChar)).Value = strNombreCatalogo;
                cmd.Parameters.Add(new SqlParameter("@catalogo_padre", SqlDbType.Bit)).Value = blCatalogoPadre;
                cmd.Parameters.Add(new SqlParameter("@id_catalogo_padre", SqlDbType.VarChar)).Value = strIdCtalogoPadre;
                cmd.Parameters.Add(new SqlParameter("@eliminado_catalogo", SqlDbType.Bit)).Value = boolEliminadoCatalogo;
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

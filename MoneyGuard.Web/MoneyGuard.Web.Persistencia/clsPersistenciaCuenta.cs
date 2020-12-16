using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaCuenta
    {
        clsConexion connect;
        public DataTable ConsultarCuenta(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_cuentas", connect.conn);
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

        public bool ModificarCuenta(string strServicio, int idCuenta, int idCliente, 
            string strNumeroCuenta, string strTipoCuenta, double dblSaldo, bool estado)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_cuenta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cuenta", SqlDbType.Int)).Value = idCuenta;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = strNumeroCuenta;
                cmd.Parameters.Add(new SqlParameter("@tipo_cuenta", SqlDbType.VarChar)).Value = strTipoCuenta;
                cmd.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Decimal)).Value = dblSaldo;
                cmd.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit)).Value = estado;
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

        public bool EliminarCuenta(string strServicio, string strNumeroCuenta)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_cuenta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = strNumeroCuenta;
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

        public bool AgregarCuenta(string strServicio, int idCliente,
            string strNumeroCuenta, string strTipoCuenta, double dblSaldo, bool estado)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_insertar_cuenta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = strNumeroCuenta;
                cmd.Parameters.Add(new SqlParameter("@tipo_cuenta", SqlDbType.VarChar)).Value = strTipoCuenta;
                cmd.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Decimal)).Value = dblSaldo;
                cmd.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit)).Value = estado;
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

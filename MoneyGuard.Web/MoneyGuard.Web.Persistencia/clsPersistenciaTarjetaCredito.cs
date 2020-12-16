using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaTarjetaCredito
    {
        clsConexion connect;
        public DataTable ConsultarTarjetaCredito(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_tarjetas_credito", connect.conn);
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

        public bool ModificarTarjetaCredito(string strServicio, int idTarjetaCredito, int idCuenta,
            string strNumeroTarjeta, string strMarcaTarjeta)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_tarjeta_credito", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_tarjeta_credito", SqlDbType.Int)).Value = idTarjetaCredito;
                cmd.Parameters.Add(new SqlParameter("@id_cuenta", SqlDbType.Int)).Value = idCuenta;
                cmd.Parameters.Add(new SqlParameter("@numero_tarjeta", SqlDbType.VarChar)).Value = strNumeroTarjeta;
                cmd.Parameters.Add(new SqlParameter("@marca_tarjeta", SqlDbType.VarChar)).Value = strMarcaTarjeta;
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

        public bool EliminarTarjetaCredito(string strServicio, int idTarjetaCredito)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_tarjeta_credito", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_tarjeta_credito", SqlDbType.Int)).Value = idTarjetaCredito;
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

        public bool AgregarTarjetaCredito(string strServicio, int idCuenta,
            string strNumeroTarjeta, string strMarcaTarjeta)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_insertar_tarjeta_credito", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cuenta", SqlDbType.Int)).Value = idCuenta;
                cmd.Parameters.Add(new SqlParameter("@numero_tarjeta", SqlDbType.VarChar)).Value = strNumeroTarjeta;
                cmd.Parameters.Add(new SqlParameter("@marca_tarjeta", SqlDbType.VarChar)).Value = strMarcaTarjeta;
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

        public DataTable ConsultarSaldoTarjetaCredito(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_saldo_tarjetas_credito", connect.conn);
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

        public bool ModificarSaldoTarjetaCredito(string strServicio, int idSaldoTarjeta, string strNumeroTarjeta, double dblSaldoPendiente,
            double dblConsumoMes, double dblCupoDisponible, double dblMinimoPagar, string strFechaPago, string strFechaCorte)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_saldo_tarjeta_credito", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_saldo_tarjeta", SqlDbType.Int)).Value = idSaldoTarjeta;
                cmd.Parameters.Add(new SqlParameter("@numero_tarjeta", SqlDbType.VarChar)).Value = strNumeroTarjeta;
                cmd.Parameters.Add(new SqlParameter("@saldo_pendiente", SqlDbType.Money)).Value = dblSaldoPendiente;
                cmd.Parameters.Add(new SqlParameter("@consumo_mes", SqlDbType.Money)).Value = dblConsumoMes;
                cmd.Parameters.Add(new SqlParameter("@cupo_disponible", SqlDbType.Money)).Value = dblCupoDisponible;
                cmd.Parameters.Add(new SqlParameter("@minimo_pagar", SqlDbType.Money)).Value = dblMinimoPagar;
                cmd.Parameters.Add(new SqlParameter("@fecha_corte", SqlDbType.Date)).Value = DateTime.Parse(strFechaCorte);
                cmd.Parameters.Add(new SqlParameter("@fecha_pago", SqlDbType.Date)).Value = DateTime.Parse(strFechaPago);
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

        public bool EliminarSaldoTarjetaCredito(string strServicio, int idSaldoTarjetaCredito)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_saldo_tarjeta_credito", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_saldo_tarjeta_credito", SqlDbType.Int)).Value = idSaldoTarjetaCredito;
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

        public bool AgregarSaldoTarjetaCredito(string strServicio, int idTarjetaCredito, double dblSaldoPendiente,
            double dblConsumoMes, double dblCupoDisponible, double dblMinimoPagar, string strFechaPago, string strFechaCorte)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_insertar_saldo_tarjeta_credito", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_tarjeta", SqlDbType.Int)).Value = idTarjetaCredito;
                cmd.Parameters.Add(new SqlParameter("@saldo_pendiente", SqlDbType.Money)).Value = dblSaldoPendiente;
                cmd.Parameters.Add(new SqlParameter("@consumo_mes", SqlDbType.Money)).Value = dblConsumoMes;
                cmd.Parameters.Add(new SqlParameter("@cupo_disponible", SqlDbType.Money)).Value = dblCupoDisponible;
                cmd.Parameters.Add(new SqlParameter("@minimo_pagar", SqlDbType.Money)).Value = dblMinimoPagar;
                cmd.Parameters.Add(new SqlParameter("@fecha_corte", SqlDbType.Date)).Value = strFechaCorte;
                cmd.Parameters.Add(new SqlParameter("@fecha_pago", SqlDbType.Date)).Value = strFechaPago;
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

        public DataTable ConsultarSaldoTarjetaCredito(string strServicio, int idTarjetaCredito)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_saldo_tarjetas_credito_id", connect.conn);
                cmd.Parameters.Add(new SqlParameter("@id_tarjeta", SqlDbType.Int)).Value = idTarjetaCredito;
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

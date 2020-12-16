using SimuladorCooperativaAmarilla.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorCooperativaAmarilla.CapaPersistencia
{
    public class clsPersistenciaCuenta
    {
        clsConexion connect;

        public List<clsCuenta> consultarCuentaIdentificacion(string identificacionCuenta)
        {
            List<clsCuenta> lstCuentas = new List<clsCuenta>();
            DataSet dsCuenta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_cuentas_identificacion", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = identificacionCuenta;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsCuenta);
                if (dsCuenta.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCuenta.Tables[0].Rows)
                    {
                        clsCuenta objCuenta = new clsCuenta();
                        objCuenta.id_cuenta = int.Parse(dr[0].ToString());
                        objCuenta.id_cliente = int.Parse(dr[1].ToString());
                        objCuenta.numero_cuenta = dr[2].ToString();
                        objCuenta.tipo_cuenta = dr[3].ToString();
                        objCuenta.saldo = double.Parse(dr[4].ToString());
                        objCuenta.estado = bool.Parse(dr[5].ToString());
                        lstCuentas.Add(objCuenta);
                    }
                }
                return lstCuentas;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsCuenta != null)
                    dsCuenta.Dispose();
            }
        }

        public List<clsTarjetaCedito> consultarTarjetasCliente(string strIdentificacionCliente)
        {
            List<clsTarjetaCedito> lstTarjetaCedito = new List<clsTarjetaCedito>();
            DataSet dsTarjeta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_tarjeta_identificacion", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = strIdentificacionCliente;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsTarjeta);
                if (dsTarjeta.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsTarjeta.Tables[0].Rows)
                    {
                        clsTarjetaCedito objTarjetaCedito = new clsTarjetaCedito();
                        objTarjetaCedito.id_tarjeta = int.Parse(dr[0].ToString());
                        objTarjetaCedito.id_cuenta = int.Parse(dr[1].ToString());
                        objTarjetaCedito.numero_tarjeta = dr[2].ToString();
                        objTarjetaCedito.marca_tarjeta = dr[3].ToString();
                        objTarjetaCedito.cupo_disponible_tarjeta = double.Parse(dr[4].ToString());
                        objTarjetaCedito.saldo_total_tarjeta = double.Parse(dr[5].ToString());
                        objTarjetaCedito.consumo_mes_tarjeta = double.Parse(dr[6].ToString());
                        objTarjetaCedito.minimo_pagar = double.Parse(dr[7].ToString());
                        objTarjetaCedito.fecha_corte_tarjeta = dr[8].ToString();
                        objTarjetaCedito.fecha_pago_tarjeta = dr[9].ToString();
                        lstTarjetaCedito.Add(objTarjetaCedito);
                    }
                }
                return lstTarjetaCedito;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsTarjeta != null)
                    dsTarjeta.Dispose();
            }
        }

        public bool pagarTarjeta(string strIdentificacionCliente, string strNumeroTarjeta, double dblMonto)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            string strNombreCliente = string.Empty;
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_realizar_pago_tarjeta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@numero_tarjeta", SqlDbType.VarChar)).Value = strNumeroTarjeta;
                cmd.Parameters.Add(new SqlParameter("@monto", SqlDbType.Decimal)).Value = dblMonto;
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

        public string consultarNombreCliente(string identificacionCliente, string numeroCuenta)
        {
            SqlCommand cmd;
            DataSet dsCuenta = null;
            SqlDataAdapter da;
            connect = new clsConexion();
            string strNombreCliente = string.Empty;
            try
            {
                dsCuenta = new DataSet();
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_nombre_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = identificacionCliente;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = numeroCuenta;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsCuenta);
                if (dsCuenta.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsCuenta.Tables[0].Rows[0];
                    strNombreCliente = dr[0].ToString();
                }
                return strNombreCliente;
            }
            catch (Exception ex)
            {
                return "Error al consultar cliente: " +ex.Message;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsCuenta != null)
                    dsCuenta.Dispose();
            }
        }

        public bool insertarCuenta(clsCuenta objCuenta)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_insertar_cuenta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = objCuenta.id_cliente;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = objCuenta.numero_cuenta;
                cmd.Parameters.Add(new SqlParameter("@tipo_cuenta", SqlDbType.Char)).Value = objCuenta.tipo_cuenta;
                cmd.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Decimal)).Value = objCuenta.saldo;
                cmd.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit)).Value = objCuenta.estado;
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

        public double consultarSaldoCuentaCliente(string strNumeroCuentaCliente, string strIdentificacionCliente)
        {
            SqlCommand cmd;
            DataSet dsCuenta = null;
            SqlDataAdapter da;
            connect = new clsConexion();
            double dblSaldoCuenta = 0;
            try
            {
                dsCuenta = new DataSet();
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_saldo_cuenta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = strNumeroCuentaCliente;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = strIdentificacionCliente;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsCuenta);
                if (dsCuenta.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsCuenta.Tables[0].Rows[0];
                    dblSaldoCuenta = double.Parse(dr[0].ToString());
                }
                else
                {
                    dblSaldoCuenta = -1;
                }
                return dblSaldoCuenta;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsCuenta != null)
                    dsCuenta.Dispose();
            }
        }

        public clsCuenta consultarCuentaCliente(string identificacionCliente, string numeroCuenta)
        {
            SqlCommand cmd;
            DataSet dsCuenta = null;
            SqlDataAdapter da;
            connect = new clsConexion();
            clsCuenta objCuenta = new clsCuenta();

            try
            {
                dsCuenta = new DataSet();
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_cuenta_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = identificacionCliente;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = numeroCuenta;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsCuenta);
                if (dsCuenta.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsCuenta.Tables[0].Rows[0];
                    objCuenta.id_cuenta = int.Parse(dr[0].ToString());
                    objCuenta.id_cliente = int.Parse(dr[1].ToString());
                    objCuenta.numero_cuenta = dr[2].ToString();
                    objCuenta.tipo_cuenta = dr[3].ToString();
                    objCuenta.saldo = double.Parse(dr[4].ToString());
                    objCuenta.estado = bool.Parse(dr[5].ToString());
                }
                return objCuenta;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsCuenta != null)
                    dsCuenta.Dispose();
            }
        }

        public bool modificarCuenta(clsCuenta objCuenta)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_modificar_cuenta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cuenta", SqlDbType.Int)).Value = objCuenta.id_cuenta;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = objCuenta.id_cliente;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = objCuenta.numero_cuenta;
                cmd.Parameters.Add(new SqlParameter("@tipo_cuenta", SqlDbType.Char)).Value = objCuenta.tipo_cuenta;
                cmd.Parameters.Add(new SqlParameter("@saldo", SqlDbType.Decimal)).Value = objCuenta.saldo;
                cmd.Parameters.Add(new SqlParameter("@estado", SqlDbType.Bit)).Value = objCuenta.estado;
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

        public bool eliminarCuenta(string numeroCuenta)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_eliminar_cuenta", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = numeroCuenta;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connect.cerrarConexion();
            }
        }
    }
}

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
    public class clsPersistenciaMovimiento
    {
        clsConexion connect;

        public bool insertarMovimiento(clsMovimiento objMovimiento,string strNumeroCuentaCliente, string strIdentificacionCliente)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_insertar_movimiento", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta_cliente", SqlDbType.VarChar)).Value = strNumeroCuentaCliente;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = strIdentificacionCliente;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente_beneficiario", SqlDbType.VarChar)).Value = objMovimiento.identificacion_cliente_beneficiario;
                cmd.Parameters.Add(new SqlParameter("@nombre_cliente_beneficiario", SqlDbType.VarChar)).Value = objMovimiento.nombre_cliente_beneficiario;
                cmd.Parameters.Add(new SqlParameter("@tipo_transaccion", SqlDbType.Char)).Value = objMovimiento.tipo_transaccion;
                cmd.Parameters.Add(new SqlParameter("@nombre_banco_beneficiario", SqlDbType.VarChar)).Value = objMovimiento.nombre_banco_beneficiario;
                cmd.Parameters.Add(new SqlParameter("@monto_movimiento", SqlDbType.Decimal)).Value = objMovimiento.monto_movimiento;
                cmd.Parameters.Add(new SqlParameter("@descripcion_movimiento", SqlDbType.VarChar)).Value = objMovimiento.descripcion_movimiento;
                cmd.Parameters.Add(new SqlParameter("@estado_movimiento", SqlDbType.Bit)).Value = objMovimiento.estado_movimiento;
                cmd.Parameters.Add(new SqlParameter("@lugar_movimiento", SqlDbType.VarChar)).Value = objMovimiento.lugar_movimiento;
                cmd.Parameters.Add(new SqlParameter("@saldo_cuenta_movimiento", SqlDbType.Money)).Value = objMovimiento.saldo_cuenta_movimiento;
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

        public List<clsMovimiento> consultarMovimientos(string numeroCuenta, int intCantidad)
        {
            List<clsMovimiento> lstMovimientos = new List<clsMovimiento>();
            DataSet dsMovimiento = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_movimientos", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@numero_cuenta", SqlDbType.VarChar)).Value = numeroCuenta;
                cmd.Parameters.Add(new SqlParameter("@cantidad", SqlDbType.Int)).Value = intCantidad;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsMovimiento);
                if (dsMovimiento.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsMovimiento.Tables[0].Rows)
                    {
                        clsMovimiento objMovimiento = new clsMovimiento();
                        objMovimiento.id_movimiento = int.Parse(dr[0].ToString());
                        objMovimiento.id_cuenta = int.Parse(dr[1].ToString());
                        objMovimiento.identificacion_cliente_beneficiario = dr[2].ToString();
                        objMovimiento.nombre_cliente_beneficiario = dr[3].ToString();
                        objMovimiento.tipo_transaccion = char.Parse(dr[4].ToString());
                        objMovimiento.nombre_banco_beneficiario = dr[5].ToString();
                        objMovimiento.monto_movimiento = double.Parse(dr[6].ToString());
                        objMovimiento.descripcion_movimiento = dr[7].ToString();
                        objMovimiento.fecha_movimiento = DateTime.Parse(dr[8].ToString()).ToString("dd-MM-yyyy");
                        objMovimiento.estado_movimiento = bool.Parse(dr[9].ToString());
                        objMovimiento.lugar_movimiento = dr[10].ToString();
                        objMovimiento.saldo_cuenta_movimiento = double.Parse(dr[11].ToString());
                        lstMovimientos.Add(objMovimiento);
                    }
                }
                return lstMovimientos;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsMovimiento != null)
                    dsMovimiento.Dispose();
            }
        }
    }
}
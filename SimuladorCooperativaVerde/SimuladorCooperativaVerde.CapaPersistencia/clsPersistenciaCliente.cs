using SimuladorCooperativaVerde.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SimuladorCooperativaVerde.CapaPersistencia
{
    public class clsPersistenciaCliente
    {
        clsConexion connect;

        public clsCliente consultarClienteId(int idCliente)
        {
            clsCliente objCliente = new clsCliente();
            DataSet dsCliente = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter da;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_cliente_id", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = idCliente;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsCliente);
                if (dsCliente.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsCliente.Tables[0].Rows[0];
                    objCliente.id_cliente = int.Parse(dr[0].ToString());
                    objCliente.identificacion_cliente = dr[1].ToString();
                    objCliente.nombre_cliente = dr[2].ToString();
                    objCliente.apellido_cliente = dr[3].ToString();
                    objCliente.telefono_cliente = dr[4].ToString();
                    objCliente.email_cliente = dr[4].ToString();
                }
                return objCliente;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if(dsCliente != null)
                    dsCliente.Dispose();
            }
        }

        public clsCliente consultarClienteIdentificacion(string identificacionCliente)
        {
            clsCliente objCliente = new clsCliente();
            DataSet dsCliente = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter da;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_cliente_identificacion", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = objCliente.id_cliente;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsCliente);
                if (dsCliente.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsCliente.Tables[0].Rows[0];
                    objCliente.id_cliente = int.Parse(dr[0].ToString());
                    objCliente.identificacion_cliente = dr[1].ToString();
                    objCliente.nombre_cliente = dr[2].ToString();
                    objCliente.apellido_cliente = dr[3].ToString();
                    objCliente.telefono_cliente = dr[4].ToString();
                    objCliente.email_cliente = dr[4].ToString();
                }
                return objCliente;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsCliente != null)
                    dsCliente.Dispose();
            }
        }

        public List<clsCliente> consultarTodosClientes()
        {
            DataSet dsCliente = new DataSet();
            List<clsCliente> objListaCliente = new List<clsCliente>();
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_clientes", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsCliente);
                if (dsCliente.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCliente.Tables[0].Rows)
                    {
                        clsCliente objCliente = new clsCliente();

                        objCliente.id_cliente = int.Parse(dr[0].ToString());
                        objCliente.identificacion_cliente = dr[1].ToString();
                        objCliente.nombre_cliente = dr[2].ToString();
                        objCliente.apellido_cliente = dr[3].ToString();
                        objCliente.telefono_cliente = dr[4].ToString();
                        objCliente.email_cliente = dr[5].ToString();

                        objListaCliente.Add(objCliente);
                    }

                }
                return objListaCliente;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                connect.cerrarConexion();
                if (dsCliente != null)
                    dsCliente.Dispose();
            }
        }

        public bool insertarCliente(clsCliente objCliente)
        {
            SqlCommand cmd; 
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_insertar_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = objCliente.identificacion_cliente;
                cmd.Parameters.Add(new SqlParameter("@nombre_cliente", SqlDbType.VarChar)).Value = objCliente.nombre_cliente;
                cmd.Parameters.Add(new SqlParameter("@apellido_cliente", SqlDbType.VarChar)).Value = objCliente.apellido_cliente;
                cmd.Parameters.Add(new SqlParameter("@telefono_cliente", SqlDbType.VarChar)).Value = objCliente.telefono_cliente;
                cmd.Parameters.Add(new SqlParameter("@email_cliente", SqlDbType.VarChar)).Value = objCliente.email_cliente;
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

        public bool modificarCliente(clsCliente objCliente)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_modificar_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_cliente", SqlDbType.Int)).Value = objCliente.id_cliente;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = objCliente.identificacion_cliente;
                cmd.Parameters.Add(new SqlParameter("@nombre_cliente", SqlDbType.VarChar)).Value = objCliente.nombre_cliente;
                cmd.Parameters.Add(new SqlParameter("@apellido_cliente", SqlDbType.VarChar)).Value = objCliente.apellido_cliente;
                cmd.Parameters.Add(new SqlParameter("@telefono_cliente", SqlDbType.VarChar)).Value = objCliente.telefono_cliente;
                cmd.Parameters.Add(new SqlParameter("@email_cliente", SqlDbType.VarChar)).Value = objCliente.email_cliente;
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

        public bool eliminarCliente(string identificacionCliente)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_eliminar_cliente", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = identificacionCliente;
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

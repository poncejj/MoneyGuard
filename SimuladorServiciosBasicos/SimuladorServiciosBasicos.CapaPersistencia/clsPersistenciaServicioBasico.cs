using SimuladorServiciosBasicos.ModeloDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SimuladorServiciosBasicos.CapaPersistencia
{
    public class clsPersistenciaServicioBasico
    {
        clsConexion connect;

        public int verificarSuministro(string strIdentificacionCliente, string strNumeroSuministro, string strTipo)
        {
            int idSuministro = -1;
            DataSet dsRespuesta = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter da;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_id_suministro", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@identificacion_cliente", SqlDbType.VarChar)).Value = strIdentificacionCliente;
                cmd.Parameters.Add(new SqlParameter("@numero_suministro", SqlDbType.VarChar)).Value = strNumeroSuministro;
                cmd.Parameters.Add(new SqlParameter("@tipo", SqlDbType.VarChar)).Value = strTipo;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsRespuesta);
                if (dsRespuesta.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsRespuesta.Tables[0].Rows[0];
                    idSuministro = int.Parse(dr[0].ToString());
                }
                return idSuministro;
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connect.cerrarConexion();
                if(dsRespuesta != null)
                    dsRespuesta.Dispose();
            }
        }

        public bool verificarMonto(int idSuministro, decimal dblValor)
        {
            bool boolRespesta = false;
            DataSet dsRespuesta = new DataSet();
            SqlCommand cmd;
            SqlDataAdapter da;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_validar_suministro", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_suministro", SqlDbType.VarChar)).Value = idSuministro;
                cmd.Parameters.Add(new SqlParameter("@monto", SqlDbType.Decimal)).Value = dblValor;
                cmd.Parameters.Add(new SqlParameter("@respuesta", SqlDbType.Bit)).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                boolRespesta = bool.Parse(cmd.Parameters["@respuesta"].Value.ToString());
                return boolRespesta;
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

        public string pagarServicio(int idSuministro)
        {
            string strRespuesta = string.Empty;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_pagar_servicios", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_suministro", SqlDbType.Int)).Value = idSuministro;
                cmd.ExecuteNonQuery();
                return "Se realizo con exito el pago del serivicio";
            }
            catch (Exception)
            {
                return "Error al pagar el servicio";
            }
            finally
            {
                connect.cerrarConexion();
            }
        }
        public clsCabeceraFacturaServicio consultarFactura(int idSuministro)
        {
            SqlCommand cmd;
            SqlDataAdapter da;
            DataSet dsFactura = new DataSet();
            clsCabeceraFacturaServicio objCabeceraFacturaServicio = new clsCabeceraFacturaServicio();
            connect = new clsConexion();
            try
            {
                connect.abrirConexion();
                cmd = new SqlCommand("sp_consultar_factura", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_suministro", SqlDbType.Int)).Value = idSuministro;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsFactura);
                if (dsFactura.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsFactura.Tables[0].Rows[0];
                    objCabeceraFacturaServicio.identificacion_cliente = dr[0].ToString();
                    objCabeceraFacturaServicio.nombre_cliente = dr[1].ToString();
                    objCabeceraFacturaServicio.direccion_suministro = dr[2].ToString();
                    objCabeceraFacturaServicio.ubicacion_suministro = dr[3].ToString();
                    objCabeceraFacturaServicio.tipo_suministro = dr[4].ToString();
                    objCabeceraFacturaServicio.numero_suministro = dr[5].ToString();
                    int id_factura = int.Parse(dr[6].ToString());
                    objCabeceraFacturaServicio.detalle_factura = consultarDetalleFactura(id_factura);
                    return objCabeceraFacturaServicio;
                }
                else
                {
                    return null;
                }
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

        private List<clsDetalleFacturaServicio> consultarDetalleFactura(int id_factura)
        {
            string strRespuesta = string.Empty;
            SqlCommand cmd;
            SqlDataAdapter da;
            DataSet dsFactura = new DataSet();
            List<clsDetalleFacturaServicio> lstDetalleFactura = new List<clsDetalleFacturaServicio>();
            try
            {
                cmd = new SqlCommand("sp_consultar_detalle_factura", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_factura", SqlDbType.Int)).Value = id_factura;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsFactura);
                if (dsFactura.Tables[0].Rows.Count > 0)
                {
                    foreach(DataRow dr in dsFactura.Tables[0].Rows)
                    {
                        clsDetalleFacturaServicio objDetalleFacturaServicio = new clsDetalleFacturaServicio();
                        objDetalleFacturaServicio.detalle_item = dr[0].ToString();
                        objDetalleFacturaServicio.valor_detalle = double.Parse(dr[1].ToString());
                        lstDetalleFactura.Add(objDetalleFacturaServicio);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return lstDetalleFactura;
        }
    }
}

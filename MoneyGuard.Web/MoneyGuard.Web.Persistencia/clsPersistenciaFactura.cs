using System;
using System.Data;
using System.Data.SqlClient;

namespace MoneyGuard.Web.Persistencia
{
    public class clsPersistenciaFactura
    {
        clsConexion connect;

        public DataTable ConsultarFactura(string strServicio)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_factura_web", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                da.Fill(dsRespuesta);
            }
            catch (Exception ex)
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

        public bool ModificarFactura(string strServicio, int idFactura, int idSuministro, 
            double valorFactura, string strFechaEmision, string  strFechaVencimiento, 
            double dblValorPendiente, bool blValorPagado)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_modificar_factura", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_factura", SqlDbType.Int)).Value = idFactura;
                cmd.Parameters.Add(new SqlParameter("@id_suministro", SqlDbType.Int)).Value = idSuministro;
                cmd.Parameters.Add(new SqlParameter("@valor_factura", SqlDbType.Money)).Value = valorFactura;
                cmd.Parameters.Add(new SqlParameter("@fecha_emision", SqlDbType.Date)).Value = DateTime.Parse(strFechaEmision);
                cmd.Parameters.Add(new SqlParameter("@fecha_vencimiento", SqlDbType.Date)).Value = DateTime.Parse(strFechaVencimiento);
                cmd.Parameters.Add(new SqlParameter("@valor_pendiente", SqlDbType.Money)).Value = dblValorPendiente;
                cmd.Parameters.Add(new SqlParameter("@valor_pagado", SqlDbType.Bit)).Value = blValorPagado;
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

        public bool EliminarFactura(string strServicio, int idFactura)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_factura", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_factura", SqlDbType.Int)).Value = idFactura;
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

        public bool AgregarFactura(string strServicio, int idSuministro,
            double valorFactura, string strFechaEmision, string strFechaVencimiento,
            double dblValorPendiente, bool blValorPagado)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_agregar_factura", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_suministro", SqlDbType.Int)).Value = idSuministro;
                cmd.Parameters.Add(new SqlParameter("@valor_factura", SqlDbType.Money)).Value = valorFactura;
                cmd.Parameters.Add(new SqlParameter("@fecha_emision", SqlDbType.Date)).Value = DateTime.Parse(strFechaEmision);
                cmd.Parameters.Add(new SqlParameter("@fecha_vencimiento", SqlDbType.Date)).Value = DateTime.Parse(strFechaVencimiento);
                cmd.Parameters.Add(new SqlParameter("@valor_pendiente", SqlDbType.Money)).Value = dblValorPendiente;
                cmd.Parameters.Add(new SqlParameter("@valor_pagado", SqlDbType.Bit)).Value = blValorPagado;
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

        public DataTable ConsultarDetalleFactura(string strServicio, int idFactura)
        {
            DataSet dsRespuesta = new DataSet();
            SqlDataAdapter da;
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_consultar_detalle_factura_web", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_factura", SqlDbType.Int)).Value = idFactura;
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

        public bool EliminarDetalleFactura(string strServicio, int idDetalleFactura)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_eliminar_detalle_factura", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_factura", SqlDbType.Int)).Value = idDetalleFactura;
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

        public bool AgregarDetalleFactura(string strServicio, int idFactura,
            string strDescripcion, double valorDetalle)
        {
            SqlCommand cmd;
            connect = new clsConexion();
            try
            {
                connect.abrirConexion(strServicio);
                cmd = new SqlCommand("sp_agregar_detalle_factura", connect.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id_factura", SqlDbType.Int)).Value = idFactura;
                cmd.Parameters.Add(new SqlParameter("@descripcion_detalle", SqlDbType.VarChar)).Value = strDescripcion;
                cmd.Parameters.Add(new SqlParameter("@valor_detalle", SqlDbType.Money)).Value = valorDetalle;
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Utilidad
{
    public class Gestion
    {
        public Gestion()
        {  }

        public DataTable ObtenerCuadroVentas(string mes, string anho)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_GestionCuadroVentas",
                mes, anho);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable ObtenerCuadroDetalle(string item, string mes, string anho, string idEjecutiva)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_GestionCuadroDetalle",
                mes, anho, item, idEjecutiva);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable ObtenerDiasCierre(string anho)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_GestionCierresObtener",
                anho);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public string CargarDiaCierre(string dia, string mes, string anho)
        {
            string salida = "";
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand com = db.GetStoredProcCommand("pa_GestionCierreInsertar",
                    dia, mes, anho);

                SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
                DataTable dt = new DataTable();
                dt.Load(sqlDataReader);
                com.Connection.Close();
            }
            catch (Exception ex)
            {
                salida = ex.Message;
            }
            return salida;
        }

        public string ObtenerProyeccionVendedor(int mes, int anho, int idVendedor)
        {
            string salida = ""; 
            
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_ProyeccionVendedorObtener",
                mes, anho, idVendedor);

            salida = Convert.ToString(db.ExecuteScalar(com));
            com.Connection.Close(); 
            
            return salida;
        }

        public DataTable ObtenerProyecciones(int mes, int anho)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_ProyeccionesObtener",
                mes, anho);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();
            
            return dt;
        }

        public string GuardarProyecciones(int mes, int anho, int idVendedor, int idUsuario, string monto)
        {
            string salida = "";
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand com = db.GetStoredProcCommand("pa_GuardarProyecciones",
                    mes, anho, idVendedor, idUsuario, monto);

                db.ExecuteNonQuery(com);
                com.Connection.Close();
            }
            catch (Exception ex)
            {
                salida = ex.Message;
            }
            return salida;
        }

        public DataTable ObtenerProcesosVentas()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_ProcesosVentasObtener");

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public void GrabarUsuarioAgenda(string nombre, string fono, bool activo, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_UsuarioAgendaGrabar",
                idUsuario, nombre, fono, activo);

            db.ExecuteNonQuery(com);
            com.Connection.Close();
        }

        public DataTable ListarUsuarioAgenda(string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_UsuarioAgendaListar",
                idUsuario);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public void ModificarUsuarioAgenda(string idAgenda, string nombre, string fono, bool activo, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_UsuarioAgendaModificar",
                idAgenda, idUsuario, nombre, fono, activo);

            db.ExecuteNonQuery(com);
            com.Connection.Close();
        }

        public DataRow TraerUsuarioAgenda(string idAgenda)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_UsuarioAgendaTraer",
                idAgenda);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt.Rows[0];
        }

        public DataTable ObtenerLlamadasSalientes(int idUsuario, int idDepartamento, int tipoLlamada, string fechaDesde, string fechaHasta)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_GestionLlamadasSalientesListar",
                idUsuario, idDepartamento, tipoLlamada, fechaDesde, fechaHasta);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable ContarLlamadasSalientes(int idUsuario, int idDepartamento, int tipoLlamada, string fechaDesde, string fechaHasta)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_GestionLlamadasSalientesContar",
                idUsuario, idDepartamento, tipoLlamada, fechaDesde, fechaHasta);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable ListarTiposLlamadasSalientes()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TipoLlamadaAsteriskListar");

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

    }
}

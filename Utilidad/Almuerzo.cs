using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Utilidad
{
    public class Almuerzo
    {
        private DataTable TablaObtener(string sp)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand(sp);

            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }
        private string TablaGrabar(string sp, string descripcion, string idUsuario)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand(sp,
                    descripcion, idUsuario);

                db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (com != null)
                {
                    com.Connection.Close();
                    com = null;
                }
            }
            return resultado;
        }


        public DataTable EntradasObtener()
        {
            return TablaObtener("pa_AlmuerzoEntradaObtener");
        }

        public DataTable PlatoPrincipalObtener()
        {
            return TablaObtener("pa_AlmuerzoPlatoPrincipalObtener");
        }

        public DataTable PostreObtener()
        {
            return TablaObtener("pa_AlmuerzoPostreObtener");
        }

        public DataTable FechaEspecialesObtener()
        {
            return TablaObtener("pa_AlmuerzoFechaEspecialesObtener");
        }


        public string EntradaIngresar(string entrada, string usuario)
        {
            return TablaGrabar("pa_AlmuerzoEntradaInsertar", entrada, usuario);
        }

        public string PlatoPrincipalIngresar(string platop, string usuario)
        {
            return TablaGrabar("pa_AlmuerzoPlatoPrincipalInsertar", platop, usuario);
        }

        public string PostreIngresar(string postre, string usuario)
        {
            return TablaGrabar("pa_AlmuerzoPostreIngresar", postre, usuario);
        }

        public string FechaEspecialIngresar(string fechaEsp, string usuario)
        {
            return TablaGrabar("pa_AlmuerzoFechaEspecialesInsertar", fechaEsp, usuario);
        }

        public string IngresarAlmuerzo(string idEntrada, string idAlmuerzo, string idPostre, string idFechaEspecial, DateTime fechaAlmuerzo, string idUsuario)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("pa_AlmuerzoIngresar",
                   idEntrada, idAlmuerzo, idPostre, idFechaEspecial, fechaAlmuerzo, idUsuario);

                db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (com != null)
                {
                    com.Connection.Close();
                    com = null;
                }
            }
            return resultado;
        }

        public DataRow ObtenerAlmuerzoHoy()
        {
            DataRow resultado = null; 
            
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoHoyObtener");
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            if (dt.Rows.Count > 0)
                resultado = dt.Rows[0];

            return resultado;
        }

        public string ObtenerAlmuerzo()
        {
            DataRow dr = ObtenerAlmuerzoHoy();
            if (dr == null)
                return "";

            string idAlumno = dr[0].ToString();
            string entrada = dr[1].ToString();
            string platoPrincipal = dr[2].ToString();
            string postre = dr[3].ToString();
            string fechaEspecial = dr[4].ToString();

            string resultado = "[Identificador: " + idAlumno + "]<br/>";
            if (entrada.Length > 0)
                resultado += "<br/>entrada: <b>" + entrada + "</b>";
            if (platoPrincipal.Length > 0)
                resultado += "<br/>plato principal: <b>" + platoPrincipal + "</b>";
            if (postre.Length > 0)
                resultado += "<br/>postre: <b>" + postre + "</b>";
            if (fechaEspecial.Length > 0)
                resultado += "<br/>fecha especial: <b>" + fechaEspecial + "</b>";

            return resultado;
        }

        public DataTable AlmuerzosTodosObtener()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzosTodosObtener");
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public string AlmuerzoPersonalIngresar(string idAlmuerzo, string item01, string item02, string item03, string item04, string obs, string IP)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("pa_AlmuerzoPersonalIngresar",
                   idAlmuerzo, item01, item02, item03, item04, obs, IP);

                db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (com != null)
                {
                    com.Connection.Close();
                    com = null;
                }
            }
            return resultado;
        }

        public string AlmuerzoPersonalIngresarSimple(string idAlmuerzo, string item, string obs, string IP)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("pa_AlmuerzoPersonalIngresarSimple",
                   idAlmuerzo, item, obs, IP);

                db.ExecuteNonQuery(com);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            finally
            {
                if (com != null)
                {
                    com.Connection.Close();
                    com = null;
                }
            }
            return resultado;
        }

        public DataTable ObtenerAlmuerzoOtros()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzosOtrosObtener");
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable ObservacionesObtenerSimple(string anho, string mes)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoObservacionReporteSimple",
                anho, mes);
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable ObservacionesObtener(string fecha)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoObservacionReporte",
                fecha);
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable EstadisticasItemObtener()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoItemsReporte",
                0);
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public DataTable ItemsDetalleObtener(string fecha)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoItemDetalleReporte",
                fecha);
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

        public string EliminarAlmuerzo(int idAlmuerzo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoEliminar", idAlmuerzo);
                db.ExecuteNonQuery(com);

                return string.Empty;
            }
            catch (Exception ex) { return ex.Message; }
        }

        public DataTable ObtenerPorFecha(DateTime fecha)
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoObtenerPorFecha", fecha);
                using (IDataReader dr = db.ExecuteReader(com))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch
            {
                return dt;
            }
        }

        /// <summary>
        /// Busca la evaluacion de almuerzo por dia. 
        /// </summary>
        /// <param name="ip">direccion ip del equipo</param>
        /// <returns></returns>
        public bool ValidaEvaluacionDiaria(string ip)
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoComprobacion");
                db.AddInParameter(com, "ip ", DbType.String, ip);                
                using (IDataReader dr = db.ExecuteReader(com))
                {
                    dt.Load(dr);
                    return dt.Rows.Count > 0 ?  true: false;
                }
            }
            catch
            {
                return true;
            }
        }

        public DataTable EstadisticasItemObtenerSimple(string anho, string mes)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_AlmuerzoItemsReporteSimple",
                anho, mes);
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();

            return dt;
        }

    }
}

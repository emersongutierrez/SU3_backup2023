using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Utilidad
{
    public class AdmTareas
    {
        public AdmTareas()
        { }
        public AdmTareas(string idTarea)
        {
            SqlDataReader dr = DatosObtener(idTarea);
            if (dr.HasRows)
            {
                dr.Read();
                _fechaPropuesta = Convert.ToDateTime(dr["fechaPropuesta"]);
            }
        }

        #region Enumeraciones
        public enum Meses : int
        {
            Enero = 1,
            Febrero = 2,
            Marzo = 3,
            Abril = 4,
            Mayo = 5,
            Junio = 6,
            Julio = 7,
            Agosto = 8,
            Septiembre = 9,
            Octubre = 10,
            Noviembre = 11,
            Diciembre = 12
        }

        public enum DiasSemana : int
        {
            Lunes = 1,
            Martes = 2,
            Miércoles = 3,
            Jueves = 4,
            Viernes = 5
        };

        public enum FrecuenciaRepeticion : int
        {
            Diaria = 1,
            Semanal = 2,
            Mensual = 3 //,
            //Anual = 4
        };

        public enum Prioridad : int
        {
            Alta = 1,
            Media = 2,
            Baja = 3
        }
        #endregion

        #region Propiedades
        public DateTime _fechaPropuesta { get; set; }
        #endregion Propiedades

        public string TareaInsertar(string breve, string detalle, int idCategoria, DateTime fechaCreacion,
            DateTime fechaPropuesta, short horasestimadas, short horasfinalizadas, DateTime fechaAcordada,
            short porcentajeasignacion, int idSolicitante, int idResponsable, short prioridad, short porcAvance,
            short estado, short vigencia, short evaluacion, int costoadicional, string ruta01, string ruta02)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                DbCommand com = db.GetStoredProcCommand("pa_TareaInsertar",
                    breve, detalle, idCategoria, fechaCreacion, fechaPropuesta, horasestimadas, horasfinalizadas, fechaAcordada,
                    porcentajeasignacion, idSolicitante, idResponsable, prioridad, porcAvance, estado, vigencia, evaluacion, costoadicional, ruta01, ruta02);
                string idTarea = db.ExecuteScalar(com).ToString();

                return idTarea;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string TareaModificar(string idTarea, string masdetalle, int categoria, DateTime fechapropuesta, short horasestimadas, short horasfinalizadas,
            DateTime fechaObservada, DateTime fechaAcordada, short porcentajeasignacion, int responsable, string observacion, short prioridad, short porcentajeavance,
            short estado, short evaluacion, string observacionfinal, int costoadicional, string razonrechazo, string ruta01, string ruta02, DateTime fechafinalizada)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("pa_TareaModificar",
                    Convert.ToInt32(idTarea), masdetalle, categoria, fechapropuesta, horasestimadas, horasfinalizadas, fechaObservada, fechaAcordada, porcentajeasignacion,
                    responsable, observacion, prioridad, porcentajeavance, estado, evaluacion, observacionfinal, costoadicional, razonrechazo, ruta01, ruta02, fechafinalizada);
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

        public DataTable CategoriaListar()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaCategoriaListar");
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();
            return dt;
        }
        public DataTable EstadoListar()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaEstadoListar");
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();
            return dt;
        }
        public DataTable UsuarioListar()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaUsuarioListar");
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();
            return dt;
        }
        public SqlDataReader DatosObtener(string idTarea)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaDatosObtener",
                idTarea);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            return dr;
        }
        public string HoraObterner()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaHoraObtener");
            string hora = db.ExecuteScalar(com).ToString();
            return hora;
        }

        public DataTable ListarTextoHistorialIntranet()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_ListarTextoHistorialIntranet");
            SqlDataReader sqlDataReader = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);
            com.Connection.Close();
            return dt;
        }

        public string InsertarTextoIntranet(string texto, string fecha, int idUsuario)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("pa_IngresoTextoPortada",
                    texto, fecha, idUsuario);

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

        public SqlDataReader ObtenerTextoIntranet()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TextoPortadaObtener");
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);

            return dr;
        }

        public string RepetirTareas(string idTarea, int diasMas, string fechaInicio)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("pa_TareaRepetir",
                    idTarea, diasMas, fechaInicio);

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

        public DataTable TareasPendientesTraer(DateTime fecha, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaPendientesObtener",
                fecha, idUsuario);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(dr);

            return dt;
        }

        public DataTable TareasEstadosTraer(DateTime fecha, int estado, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaEstadosObtener",
                fecha, estado, idUsuario);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(dr);

            return dt;
        }

        public DataTable TareaRelacionadas(string idTarea)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaRelacionadasObtener",
                idTarea);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(dr);

            return dt;
        }

        public DataTable TareasObtener(string idResponsable, string idSolicitante, string idCategoria, string idEstado, string fechaInicio, string fechaTermino)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaObtenerFiltro",
                idResponsable, idSolicitante, idCategoria, idEstado, fechaInicio, fechaTermino);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(dr);

            return dt;
        }

        public DataTable TareasObtenerGrafico(string idResponsable, string idSolicitante, string idCategoria, string idEstado, string fechaInicio, string fechaTermino)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaObtenerFiltroGrafico",
                idResponsable, idSolicitante, idCategoria, idEstado, fechaInicio, fechaTermino);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(dr);

            return dt;
        }

        public DataTable TareasObtenerGraficoPendientes(string idResponsable, string idSolicitante, string idCategoria, string idEstado, string fechaInicio, string fechaTermino)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaObtenerPendienteGrafico",
                idResponsable, idSolicitante, idCategoria, idEstado, fechaInicio, fechaTermino);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(dr);

            return dt;
        }

        public string TareaRelacionadasAnular(string idTarea)
        {
            string resultado = "";
            DbCommand com = null;

            try
            {
                Database db = DatabaseFactory.CreateDatabase();
                com = db.GetStoredProcCommand("pa_TareaRelacionadasAnular",
                    idTarea);

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
        
        public DataTable TareasSolicitadasTraer(DateTime fecha, string idUsuario)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand com = db.GetStoredProcCommand("pa_TareaSolicitadasObtener",
                fecha, idUsuario);
            SqlDataReader dr = (SqlDataReader)db.ExecuteReader(com);
            DataTable dt = new DataTable();
            dt.Load(dr);

            return dt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Tipodoc
    {
        #region Properties


        [Data(key = true, auto = true)]
        public int tpd_codigo { get; set; }
        [Data(originalkey = true)]
        public int tpd_codigo_key { get; set; }
        public string tpd_id { get; set; }
        public string tpd_nombre { get; set; }
        public int? tpd_modulo { get; set; }
        public int? tpd_for_imp { get; set; }
        public int? tpd_for_eje { get; set; }
        public int? tpd_for_con { get; set; }
        public int? tpd_nocontable { get; set; }
        public int? tpd_nivel_aprobacion { get; set; }
        public int? tpd_nro_copias { get; set; }
        public int? tpd_estado { get; set; }
        public string tpd_opciones { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        #endregion

        #region Constructors


        public Tipodoc()
        {
        }

        public Tipodoc(int codigo, string id, string nombre, int? modulo, int? for_imp, int? for_emp, int? for_con, int? nocontable, int? nivel_aprevacion, int? nro_copias, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {
            this.tpd_codigo = codigo;
            this.tpd_codigo_key = codigo;
            this.tpd_id = id;
            this.tpd_nombre = nombre;
            this.tpd_modulo = modulo;
            this.tpd_for_imp = for_imp;
            this.tpd_for_eje = tpd_for_eje;
            this.tpd_for_con = for_con;
            this.tpd_nocontable = nocontable;
            this.tpd_nivel_aprobacion = nivel_aprevacion;
            this.tpd_nro_copias = nro_copias;
            this.tpd_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
        }

        public Tipodoc(IDataReader reader)
        {
            this.tpd_codigo = (int)reader["tpd_codigo"];
            this.tpd_codigo_key = (int)reader["tpd_codigo"];
            this.tpd_id = reader["tpd_id"].ToString();
            this.tpd_nombre = reader["tpd_nombre"].ToString();
            this.tpd_modulo = (reader["tpd_modulo"] != DBNull.Value) ? (int?)reader["tpd_modulo"] : null;
            this.tpd_for_imp = (reader["tpd_for_imp"] != DBNull.Value) ? (int?)reader["tpd_for_imp"] : null;
            this.tpd_for_eje = (reader["tpd_for_eje"] != DBNull.Value) ? (int?)reader["tpd_for_eje"] : null;
            this.tpd_for_con = (reader["tpd_for_con"] != DBNull.Value) ? (int?)reader["tpd_for_con"] : null;
            this.tpd_nocontable = (reader["tpd_nocontable"] != DBNull.Value) ? (int?)reader["tpd_nocontable"] : null;
            this.tpd_nivel_aprobacion = (reader["tpd_nivel_aprobacion"] != DBNull.Value) ? (int?)reader["tpd_nivel_aprobacion"] : null;
            this.tpd_nro_copias = (reader["tpd_nro_copias"] != DBNull.Value) ? (int?)reader["tpd_nro_copias"] : null;
            this.tpd_estado = (reader["tpd_estado"] != DBNull.Value) ? (int?)reader["tpd_estado"] : null;
            this.tpd_opciones= reader["tpd_opciones"].ToString();
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }

        public Tipodoc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object tpd_codigo = null;
                object tpd_id = null;
                object tpd_nombre = null;
                object tpd_modulo = null;
                object tpd_for_imp = null;
                object tpd_for_eje = null;
                object tpd_for_con = null;
                object tpd_nocontable = null;
                object tpd_nivel_aprobacion = null;
                object tpd_nro_copias = null;
                object tpd_estado = null;
                object tpd_opciones = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("tpd_codigo", out tpd_codigo);
                tmp.TryGetValue("tpd_id", out tpd_id);
                tmp.TryGetValue("tpd_nombre", out tpd_nombre);
                tmp.TryGetValue("tpd_modulo", out tpd_modulo);
                tmp.TryGetValue("tpd_for_imp", out tpd_for_imp);
                tmp.TryGetValue("tpd_for_eje", out tpd_for_eje);
                tmp.TryGetValue("tpd_for_con", out tpd_for_con);
                tmp.TryGetValue("tpd_nocontable", out tpd_nocontable);
                tmp.TryGetValue("tpd_nivel_aprobacion", out tpd_nivel_aprobacion);
                tmp.TryGetValue("tpd_nro_copias", out tpd_nro_copias);
                tmp.TryGetValue("tpd_estado", out tpd_estado);
                tmp.TryGetValue("tpd_opciones", out tpd_opciones);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.tpd_codigo = (Int32)Conversiones.GetValueByType(tpd_codigo, typeof(Int32));
                this.tpd_id = (String)Conversiones.GetValueByType(tpd_id, typeof(String));
                this.tpd_nombre = (String)Conversiones.GetValueByType(tpd_nombre, typeof(String));
                this.tpd_modulo = (Int32)Conversiones.GetValueByType(tpd_modulo, typeof(Int32));
                this.tpd_for_imp = (Int32?)Conversiones.GetValueByType(tpd_for_imp, typeof(Int32?));
                this.tpd_for_eje = (Int32?)Conversiones.GetValueByType(tpd_for_eje, typeof(Int32?));
                this.tpd_for_con = (Int32?)Conversiones.GetValueByType(tpd_for_con, typeof(Int32?));
                this.tpd_nocontable = (Int32?)Conversiones.GetValueByType(tpd_nocontable, typeof(Int32?));
                this.tpd_nivel_aprobacion = (Int32?)Conversiones.GetValueByType(tpd_nivel_aprobacion, typeof(Int32?));
                this.tpd_nro_copias = (Int32?)Conversiones.GetValueByType(tpd_nro_copias, typeof(Int32?));
                this.tpd_estado = (Int32?)Conversiones.GetValueByType(tpd_estado, typeof(Int32?));
                this.tpd_opciones= (string)Conversiones.GetValueByType(tpd_opciones, typeof(string));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));

            }
        }


        #endregion

        #region Methods
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }
        #endregion


    }
}

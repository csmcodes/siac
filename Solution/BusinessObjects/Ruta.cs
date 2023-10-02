using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Ruta
    {
        #region Properties


        [Data(key = true, auto = true)]
        public int rut_codigo { get; set; }
        [Data(key = true)]
        public int rut_empresa { get; set; }
        [Data(originalkey = true)]
        public int rut_codigo_key { get; set; }
        [Data(originalkey = true)]
        public int rut_empresa_key { get; set; }
        public string rut_nombre { get; set; }
        public string rut_id { get; set; }
        public string rut_origen { get; set; }
        public string rut_destino { get; set; }
        public decimal? rut_kilometros { get; set; }
        public decimal? rut_duracion { get; set; }
        public int? rut_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public decimal? rut_porcentaje { get; set; }
      
        #endregion

        #region Constructors


        public Ruta()
        {

        }

        public Ruta(int codigo, int empresa, string nombre, string id, string origen, string destino, decimal? kilometros, decimal? tiempo, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {
            this.rut_codigo = codigo;
            this.rut_codigo_key = codigo;
            this.rut_empresa = empresa;
            this.rut_empresa_key = empresa;
            this.rut_id = id;
            this.rut_nombre = nombre;             
            this.rut_origen= origen;
            this.rut_destino = destino;    
            this. rut_kilometros= kilometros;
            this.rut_duracion = tiempo;
            this.rut_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;

        }

        public Ruta(IDataReader reader)
        {
            this.rut_codigo = (int)reader["rut_codigo"];
            this.rut_codigo_key = (int)reader["rut_codigo"];
            this.rut_empresa = (int)reader["rut_empresa"];
            this.rut_empresa_key = (int)reader["rut_empresa"];
            this.rut_id = reader["rut_id"].ToString();
            this.rut_nombre = reader["rut_nombre"].ToString();
            this.rut_origen = reader["rut_origen"].ToString();
            this.rut_destino = reader["rut_destino"].ToString();
            this.rut_kilometros = (reader["rut_kilometros"] != DBNull.Value) ? (decimal?)reader["rut_kilometros"] : null;
            this.rut_duracion = (reader["rut_duracion"] != DBNull.Value) ? (decimal?)reader["rut_duracion"] : null;
            this.rut_estado = (reader["rut_estado"] != DBNull.Value) ? (int?)reader["rut_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.rut_porcentaje = (reader["rut_porcentaje"] != DBNull.Value) ? (decimal?)reader["rut_porcentaje"] : null;
        }
        public Ruta(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object rut_codigo = null;
                object rut_empresa = null;
                object rut_id = null;
                object rut_nombre = null;
                object rut_origen = null;
                object rut_destino = null;
                object rut_kilometros = null;
                object rut_duracion = null;
                object rut_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("rut_codigo", out rut_codigo);
                tmp.TryGetValue("rut_empresa", out rut_empresa);
                tmp.TryGetValue("rut_id", out rut_id);
                tmp.TryGetValue("rut_nombre", out rut_nombre);
                tmp.TryGetValue("rut_origen", out rut_origen);
                tmp.TryGetValue("rut_destino", out rut_destino);
                tmp.TryGetValue("rut_kilometros", out rut_kilometros);
                tmp.TryGetValue("rut_duracion", out rut_duracion);
                tmp.TryGetValue("rut_estado", out rut_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.rut_codigo = (Int32)Conversiones.GetValueByType(rut_codigo, typeof(Int32));
                this.rut_empresa = (Int32)Conversiones.GetValueByType(rut_empresa, typeof(Int32));
                this.rut_id = (String)Conversiones.GetValueByType(rut_id, typeof(String));
                this.rut_nombre = (String)Conversiones.GetValueByType(rut_nombre, typeof(String));
                this.rut_origen = (String)Conversiones.GetValueByType(rut_origen, typeof(String));
                this.rut_destino = (String)Conversiones.GetValueByType(rut_destino, typeof(String));
                this.rut_kilometros = (Decimal?)Conversiones.GetValueByType(rut_kilometros, typeof(Decimal?));
                this.rut_duracion = (Decimal?)Conversiones.GetValueByType(rut_duracion, typeof(Decimal?));
                this.rut_estado = (Int32?)Conversiones.GetValueByType(rut_estado, typeof(Int32?));
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

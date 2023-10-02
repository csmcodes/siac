using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Transacc
    {
        #region Properties

        [Data(key = true)]
        public int tra_secuencia { get; set; }
        [Data(originalkey = true)]
        public int tra_secuencia_key { get; set; }      
        [Data(key = true)]
        public int tra_modulo{ get; set; }
        [Data(originalkey = true)]
        public int tra_modulo_key { get; set; }
        public string tra_id { get; set; }
        public string tra_nombre { get; set; }        
        public int? tra_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        [Data(nosql = true, tablaref = "modulo", camporef = "mod_nombre", foreign = "tra_modulo", keyref = "mod_codigo")]
        public string tra_nombremodulo { get; set; }
        


        #endregion

        #region Constructors


        public Transacc()
        {
        }

        public Transacc(int secuencia ,int modulo  , string id ,  string nombre ,  int? estado ,    string crea_usr ,     DateTime? crea_fecha ,   string mod_usr ,     DateTime? mod_fecha )
        {                
            this.tra_secuencia =secuencia;
            this.tra_secuencia_key = secuencia;       
            this.tra_modulo =modulo;
            this.tra_modulo_key = modulo;
            this.tra_id =id;
            this.tra_nombre =nombre;           
            this.tra_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Transacc(IDataReader reader)
        {
            this.tra_secuencia = (int)reader["tra_secuencia"];
            this.tra_secuencia_key = (int)reader["tra_secuencia"];
            this.tra_modulo = (int)reader["tra_modulo"];
            this.tra_modulo_key = (int)reader["tra_modulo"];
            this.tra_id = reader["tra_id"].ToString();
            this.tra_nombre = reader["tra_nombre"].ToString();           
            this.tra_estado = (reader["tra_estado"] != DBNull.Value) ? (int?)reader["tra_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.tra_nombremodulo = reader["tra_nombremodulo"].ToString(); 
        }



        public Transacc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object tra_modulo = null;
                object tra_secuencia = null;
                object tra_id = null;
                object tra_nombre = null;
                object tra_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object nombremodulo = null;

                tmp.TryGetValue("tra_modulo", out tra_modulo);
                tmp.TryGetValue("tra_secuencia", out tra_secuencia);
                tmp.TryGetValue("tra_id", out tra_id);
                tmp.TryGetValue("tra_nombre", out tra_nombre);
                tmp.TryGetValue("tra_estado", out tra_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("tra_nombremodulo", out nombremodulo);

                this.tra_modulo = (Int32)Conversiones.GetValueByType(tra_modulo, typeof(Int32));
                this.tra_secuencia = (Int32)Conversiones.GetValueByType(tra_secuencia, typeof(Int32));
                this.tra_id = (String)Conversiones.GetValueByType(tra_id, typeof(String));
                this.tra_nombre = (String)Conversiones.GetValueByType(tra_nombre, typeof(String));
                this.tra_estado = (Int32?)Conversiones.GetValueByType(tra_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.tra_nombremodulo = (String)Conversiones.GetValueByType(nombremodulo, typeof(String));
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

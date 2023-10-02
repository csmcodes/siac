using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Ctipocom
    {
        #region Properties

        [Data(key = true)]
        public int cti_empresa { get; set; }
        [Data(originalkey = true)]
        public int cti_empresa_key { get; set; }   
        [Data(key = true, auto = true)]
        public int cti_codigo { get; set; }
        [Data(originalkey = true)]
        public int cti_codigo_key { get; set; }
        public string cti_id { get; set; }
        public string cti_nombre { get; set; }
        public int? cti_tipo { get; set; }
        public int? cti_autoriza { get; set; }
        public int? cti_retdato { get; set; }        
        public int? cti_estado { get; set; }       
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

      
              
        #endregion

        #region Constructors


        public Ctipocom()
        {
        }

        public Ctipocom(int codigo, int empresa, string id, string nombre, int? tipo, int? autoriza, int? retdato,   int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {       
            this.cti_codigo =codigo;        
            this.cti_codigo_key =codigo;
            this.cti_empresa =empresa;      
            this.cti_empresa_key=empresa;
            this.cti_id = id;
            this.cti_nombre = nombre;
            this.cti_tipo = tipo;
            this.cti_autoriza = autoriza;
            this.cti_retdato = retdato;
            this.cti_estado = estado;   
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Ctipocom(IDataReader reader)
        {
            this.cti_codigo = (int)reader["cti_codigo"];
            this.cti_codigo_key = (int)reader["cti_codigo"];
            this.cti_empresa = (int)reader["cti_empresa"];
            this.cti_empresa_key = (int)reader["cti_empresa"];
            this.cti_id = reader["cti_id"].ToString();
            this.cti_nombre = reader["cti_nombre"].ToString();
            this.cti_tipo = (reader["cti_tipo"] != DBNull.Value) ? (int?)reader["cti_tipo"] : null;
            this.cti_autoriza = (reader["cti_autoriza"] != DBNull.Value) ? (int?)reader["cti_autoriza"] : null;
            this.cti_retdato = (reader["cti_retdato"] != DBNull.Value) ? (int?)reader["cti_retdato"] : null; 
            this.cti_estado = (reader["cti_estado"] != DBNull.Value) ? (int?)reader["cti_estado"] : null;             
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }




        public Ctipocom(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object cti_empresa = null;
                object cti_codigo = null;
                object cti_id = null;
                object cti_nombre = null;
                object cti_tipo = null;
                object cti_autoriza = null;
                object cti_retdato = null;
                object cti_tablacoa = null;
                object cti_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("cti_empresa", out cti_empresa);
                tmp.TryGetValue("cti_codigo", out cti_codigo);
                tmp.TryGetValue("cti_id", out cti_id);
                tmp.TryGetValue("cti_nombre", out cti_nombre);
                tmp.TryGetValue("cti_tipo", out cti_tipo);
                tmp.TryGetValue("cti_autoriza", out cti_autoriza);
                tmp.TryGetValue("cti_retdato", out cti_retdato);
                tmp.TryGetValue("cti_tablacoa", out cti_tablacoa);
                tmp.TryGetValue("cti_estado", out cti_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.cti_empresa = (Int32)Conversiones.GetValueByType(cti_empresa, typeof(Int32));
                this.cti_codigo = (Int32)Conversiones.GetValueByType(cti_codigo, typeof(Int32));
                this.cti_id = (String)Conversiones.GetValueByType(cti_id, typeof(String));
                this.cti_nombre = (String)Conversiones.GetValueByType(cti_nombre, typeof(String));
                this.cti_tipo = (Int32)Conversiones.GetValueByType(cti_tipo, typeof(Int32));
                this.cti_autoriza = (Int32?)Conversiones.GetValueByType(cti_autoriza, typeof(Int32?));
                this.cti_retdato = (Int32?)Conversiones.GetValueByType(cti_retdato, typeof(Int32?));
                this.cti_estado = (Int32?)Conversiones.GetValueByType(cti_estado, typeof(Int32?));
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

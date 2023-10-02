using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{

    public class Puntoventa
    {
        #region Properties


        [Data(key = true)]
        public int pve_empresa { get; set; }
        [Data(originalkey = true)]
        public int pve_empresa_key { get; set; }
        [Data(key = true)]
        public int pve_almacen { get; set; }
        [Data(originalkey = true)]
        public int pve_almacen_key { get; set; }
        [Data(key = true)]
        public int pve_secuencia { get; set; }
        [Data(originalkey = true)]
        public int pve_secuencia_key { get; set; }
        public string pve_id { get; set; }
        public string pve_nombre { get; set; }
        public string pve_responsable { get; set; }
        public int? pve_centro { get; set; }
        public int? pve_autoimpresor { get; set; }
        public int? pve_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        [Data(nosql = true, tablaref = "almacen", camporef = "alm_id", foreign = "pve_empresa,pve_almacen", keyref = "alm_empresa, alm_codigo")]
        public string pve_idalmacen { get; set; }
        #endregion
        
         #region Constructors
       
        public Puntoventa()
        {
        }

        public Puntoventa(int empresa,  int almacen, int secuencia,string id, string nombre, string responsable, int? centro, int? autoimpresor, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {

            this.pve_empresa = empresa;
            this.pve_empresa_key = empresa;
            this.pve_almacen = almacen;
            this.pve_almacen_key = almacen;
            this.pve_secuencia = secuencia;
            this.pve_secuencia_key = secuencia;
            this.pve_id = id;
            this.pve_nombre = nombre;
            this.pve_responsable = responsable;
            this.pve_centro = centro;
            this.pve_autoimpresor = autoimpresor;
            this.pve_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;

        }

        public Puntoventa(IDataReader reader)
        {
           
           
            this.pve_empresa = (int)reader["pve_empresa"];
            this.pve_empresa_key = (int)reader["pve_empresa"];
            this.pve_almacen = (int)reader["pve_almacen"];
            this.pve_almacen_key = (int)reader["pve_almacen"];
            this.pve_secuencia = (int)reader["pve_secuencia"];
            this.pve_secuencia_key = (int)reader["pve_secuencia"];
            this.pve_id = reader["pve_id"].ToString();
            this.pve_nombre = reader["pve_nombre"].ToString();
            this.pve_responsable = reader["pve_responsable"].ToString();
            this.pve_centro = (reader["pve_centro"] != DBNull.Value) ? (int?)reader["pve_centro"] : null;
            this.pve_autoimpresor = (reader["pve_autoimpresor"] != DBNull.Value) ? (int?)reader["pve_autoimpresor"] : null;
            this.pve_estado = (reader["pve_estado"] != DBNull.Value) ? (int?)reader["pve_estado"] : null;                  
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.pve_idalmacen = reader["pve_idalmacen"].ToString();
        }
        public Puntoventa(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object pve_empresa = null;
                object pve_almacen = null;
                object pve_secuencia = null;
                object pve_id = null;
                object pve_nombre = null;
                object pve_responsable = null;
                object pve_centro = null;
                object pve_autoimpresor = null;
                object pve_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("pve_empresa", out pve_empresa);
                tmp.TryGetValue("pve_almacen", out pve_almacen);
                tmp.TryGetValue("pve_secuencia", out pve_secuencia);
                tmp.TryGetValue("pve_id", out pve_id);
                tmp.TryGetValue("pve_nombre", out pve_nombre);
                tmp.TryGetValue("pve_responsable", out pve_responsable);
                tmp.TryGetValue("pve_centro", out pve_centro);
                tmp.TryGetValue("pve_autoimpresor", out pve_autoimpresor);
                tmp.TryGetValue("pve_estado", out pve_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.pve_empresa = (Int32)Conversiones.GetValueByType(pve_empresa, typeof(Int32));
                this.pve_almacen = (Int32)Conversiones.GetValueByType(pve_almacen, typeof(Int32));
                this.pve_secuencia = (Int32)Conversiones.GetValueByType(pve_secuencia, typeof(Int32));
                this.pve_id = (String)Conversiones.GetValueByType(pve_id, typeof(String));
                this.pve_nombre = (String)Conversiones.GetValueByType(pve_nombre, typeof(String));
                this.pve_responsable = (String)Conversiones.GetValueByType(pve_responsable, typeof(String));
                this.pve_centro = (Int32?)Conversiones.GetValueByType(pve_centro, typeof(Int32?));
                this.pve_autoimpresor = (Int32?)Conversiones.GetValueByType(pve_autoimpresor, typeof(Int32?));
                this.pve_estado = (Int32?)Conversiones.GetValueByType(pve_estado, typeof(Int32?));
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

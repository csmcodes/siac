using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
  public  class Usuarioxempresa
    {
        #region Properties

        [Data(key = true)]
        public string uxe_usuario { get; set; }
        [Data(key = true)]
        public int uxe_empresa { get; set; }
        [Data(originalkey = true)]
        public string uxe_usuario_key { get; set; }      
        [Data(originalkey = true)]
        public int uxe_empresa_key { get; set; }
        public int? uxe_almacen { get; set; }
        public int? uxe_puntoventa { get; set; }
        public int? uxe_empresapuntoventa { get; set; }
        public int? uxe_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(nosql = true, tablaref = "empresa", camporef = "emp_nombre", foreign = "uxe_empresa", keyref = "emp_codigo")]
        public string uxe_nombreempresa { get; set; }
        [Data(nosql = true, tablaref = "usuario", camporef = "usr_nombres", foreign = "uxe_usuario", keyref = "usr_id")]
        public string uxe_nombreusuario { get; set; }

        [Data(nosql = true, tablaref = "almacen", camporef = "alm_nombre", foreign = "uxe_empresa, uxe_almacen", keyref = "alm_empresa, alm_codigo", join = "left")]
        public string uxe_nombrealmacen { get; set; }

        [Data(nosql = true, tablaref = "puntoventa", camporef = "pve_nombre", foreign = "uxe_empresapuntoventa, uxe_almacen, uxe_puntoventa", keyref = "pve_empresa, pve_almacen, pve_secuencia", join = "left")]
        public string uxe_nombrepuntoventa { get; set; }


        #endregion
       #region Constructors
       
        public Usuarioxempresa()
        {
        }

        public Usuarioxempresa(string usuario, int empresa, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {
            this.uxe_empresa=empresa;       
            this.uxe_empresa_key=empresa;    
            this.uxe_usuario=usuario;
            this.uxe_usuario_key = usuario;
            this.uxe_estado=estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
        }

        public Usuarioxempresa(IDataReader reader)
        {            
            this.uxe_empresa = (int)reader["uxe_empresa"];
            this.uxe_empresa_key = (int)reader["uxe_empresa"];

            this.uxe_almacen =(reader["uxe_almacen"] != DBNull.Value) ? (int?)reader["uxe_almacen"] : null;
            this.uxe_puntoventa = (reader["uxe_puntoventa"] != DBNull.Value) ? (int?)reader["uxe_puntoventa"] : null;
            this.uxe_empresapuntoventa = (reader["uxe_empresapuntoventa"] != DBNull.Value) ? (int?)reader["uxe_empresapuntoventa"] : null;   
           
            this.uxe_usuario = reader["uxe_usuario"].ToString();
            this.uxe_usuario_key = reader["uxe_usuario"].ToString();
            this.uxe_estado = (reader["uxe_estado"] != DBNull.Value) ? (int?)reader["uxe_estado"] : null;                     
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.uxe_nombreempresa = reader["uxe_nombreempresa"].ToString();
            this.uxe_nombreusuario = reader["uxe_nombreusuario"].ToString();
            this.uxe_nombrealmacen = reader["uxe_nombrealmacen"].ToString();
            this.uxe_nombrepuntoventa = reader["uxe_nombrepuntoventa"].ToString();
        }

        public Usuarioxempresa(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object uxe_usuario = null;
                object uxe_empresa = null;
                object uxe_almacen = null;
                object uxe_empresapuntoventa = null;
                object uxe_puntoventa = null;
                object uxe_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object nombreempresa = null;
                object nombreusuario = null;
                object nombrealmacen= null;
                object nombrepuntoventa = null;

                tmp.TryGetValue("uxe_usuario", out uxe_usuario);
                tmp.TryGetValue("uxe_empresa", out uxe_empresa);
                tmp.TryGetValue("uxe_almacen", out uxe_almacen);
                tmp.TryGetValue("uxe_empresapuntoventa", out uxe_empresapuntoventa);
                tmp.TryGetValue("uxe_puntoventa", out uxe_puntoventa);
                tmp.TryGetValue("uxe_estado", out uxe_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("uxe_nombreempresa", out nombreempresa);
                tmp.TryGetValue("uxe_nombreusuario", out nombreusuario);
                tmp.TryGetValue("uxe_nombrealmacen", out nombrealmacen);
                tmp.TryGetValue("uxe_nombrepuntoventa", out nombrealmacen);

                this.uxe_usuario = (String)Conversiones.GetValueByType(uxe_usuario, typeof(String));
                this.uxe_empresa = (Int32)Conversiones.GetValueByType(uxe_empresa, typeof(Int32));
                this.uxe_almacen = (Int32?)Conversiones.GetValueByType(uxe_almacen, typeof(Int32?));
                this.uxe_empresapuntoventa = (Int32?)Conversiones.GetValueByType(uxe_empresapuntoventa, typeof(Int32?));
                this.uxe_puntoventa = (Int32?)Conversiones.GetValueByType(uxe_puntoventa, typeof(Int32?));
                this.uxe_estado = (Int32?)Conversiones.GetValueByType(uxe_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.uxe_nombreempresa = (String)Conversiones.GetValueByType(nombreempresa, typeof(String));
                this.uxe_nombreusuario = (String)Conversiones.GetValueByType(nombreusuario, typeof(String));
                this.uxe_nombrealmacen= (String)Conversiones.GetValueByType(nombrealmacen, typeof(String));
                this.uxe_nombrepuntoventa = (String)Conversiones.GetValueByType(nombrepuntoventa, typeof(String));
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

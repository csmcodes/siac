using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BusinessObjects
{
  public  class Personaxtipo
    {
        #region Properties

        [Data(key = true)]
        public int pxt_persona { get; set; }
        [Data(key = true)]
        public int pxt_tipo { get; set; }
        [Data(key = true)]
        public int pxt_empresa { get; set; }     
        [Data(originalkey = true)]
        public int pxt_persona_key { get; set; }      
        [Data(originalkey = true)]
        public int pxt_tipo_key { get; set; }
        [Data(originalkey = true)]
        public int pxt_empresa_key { get; set; }
        public int? pxt_cat_persona { get; set; }
        public int? pxt_politicas { get; set; } 
        public int? pxt_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

       /* [Data(nosql = true, tablaref = "EMPRESA", camporef = "emp_nombre", foreign = "uxe_empresa", keyref = "emp_codigo")]
        public string uxe_nombreempresa { get; set; }
        [Data(nosql = true, tablaref = "USUARIO", camporef = "usr_nombres", foreign = "uxe_usuario", keyref = "usr_id")]
        public string uxe_nombreusuario { get; set; }
      */
        #endregion
       #region Constructors
       
        public Personaxtipo()
        {
        }

        public Personaxtipo(int persona, int tipo,int empresa, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {
            this.pxt_tipo=tipo;
            this.pxt_tipo_key = tipo;
            this.pxt_persona = persona;
            this.pxt_persona_key = persona;
            this.pxt_empresa = empresa;
            this.pxt_empresa_key = empresa;
            this.pxt_estado=estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
        }

        public Personaxtipo(IDataReader reader)
        {
            this.pxt_tipo = (int)reader["pxt_tipo"];
            this.pxt_tipo_key = (int)reader["pxt_tipo"];
            this.pxt_persona = (int)reader["pxt_persona"];
            this.pxt_persona_key = (int)reader["pxt_persona"];
            this.pxt_empresa = (int)reader["pxt_empresa"];
            this.pxt_empresa_key = (int)reader["pxt_empresa"];

            this.pxt_cat_persona = (reader["pxt_cat_persona"] != DBNull.Value) ? (int?)reader["pxt_cat_persona"] : null;
            this.pxt_politicas = (reader["pxt_politicas"] != DBNull.Value) ? (int?)reader["pxt_politicas"] : null;   

            this.pxt_estado = (reader["pxt_estado"] != DBNull.Value) ? (int?)reader["pxt_estado"] : null;                     
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        //    this.uxe_nombreempresa = reader["uxe_nombreempresa"].ToString();
        //    this.uxe_nombreusuario = reader["uxe_nombreusuario"].ToString();
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

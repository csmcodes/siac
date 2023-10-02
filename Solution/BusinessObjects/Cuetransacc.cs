using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Cuetransacc
    {
        #region Properties

        [Data(key = true)]
        public Int32 ctr_empresa { get; set; }
        [Data(key = true)]
        public Int32 ctr_transacc { get; set; }
        [Data(key = true)]
        public Int32 ctr_categoria { get; set; }
        [Data(originalkey = true)]
        public Int32 ctr_empresa_key { get; set; }
        [Data(originalkey = true)]
        public Int32 ctr_transacc_key { get; set; }
        [Data(originalkey = true)]
        public Int32 ctr_categoria_key { get; set; }
        public Int32 ctr_tipo { get; set; }
        public Int32? ctr_modulo { get; set; }
        public Int32 ctr_cuenta { get; set; }
        public Int32? ctr_estado { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }



        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "ctr_empresa ,ctr_cuenta", keyref = "cue_empresa,cue_codigo")]
        public string ctr_cuenombre { get; set; }
        [Data(nosql = true, tablaref = "catcliente", camporef = "cat_nombre", foreign = "ctr_empresa ,ctr_categoria", keyref = "cat_empresa,cat_codigo")]
        public string ctr_catclinombre{ get; set; }
        [Data(nosql = true, tablaref = "transacc", camporef = "tra_nombre", foreign = "ctr_transacc,ctr_modulo", keyref = "tra_secuencia,tra_modulo")]
        public string ctr_transaccnombre { get; set; }



        #endregion

        #region Constructors


        public Cuetransacc()
        {
        }

        public Cuetransacc(Int32 ctr_empresa, Int32 ctr_transacc, Int32 ctr_tipo, Int32 ctr_categoria, Int32 ctr_cuenta, Int32 ctr_modulo, Int32 ctr_estado, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.ctr_empresa = ctr_empresa;
            this.ctr_transacc = ctr_transacc;
            this.ctr_categoria = ctr_categoria;
            this.ctr_empresa_key = ctr_empresa;
            this.ctr_transacc_key = ctr_transacc;
            this.ctr_categoria_key = ctr_categoria;
            this.ctr_tipo = ctr_tipo;
            this.ctr_cuenta = ctr_cuenta;
            this.ctr_modulo = ctr_modulo;
            this.ctr_estado = ctr_estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }

        public Cuetransacc(IDataReader reader)
        {
            this.ctr_empresa = (Int32)reader["ctr_empresa"];
            this.ctr_transacc = (Int32)reader["ctr_transacc"];
            this.ctr_categoria = (Int32)reader["ctr_categoria"];
            this.ctr_empresa_key = (Int32)reader["ctr_empresa"];
            this.ctr_transacc_key = (Int32)reader["ctr_transacc"];
            this.ctr_categoria_key = (Int32)reader["ctr_categoria"];
            this.ctr_tipo = (Int32)reader["ctr_tipo"];
            this.ctr_cuenta = (Int32)reader["ctr_cuenta"];
            this.ctr_modulo = (reader["ctr_modulo"] != DBNull.Value) ? (Int32?)reader["ctr_modulo"] : null;
            this.ctr_estado = (reader["ctr_estado"] != DBNull.Value) ? (Int32?)reader["ctr_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.ctr_cuenombre = reader["ctr_cuenombre"].ToString();
            this.ctr_catclinombre = reader["ctr_catclinombre"].ToString();
            this.ctr_transaccnombre = reader["ctr_transaccnombre"].ToString();        
        }


        public Cuetransacc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object ctr_empresa = null;
                object ctr_transacc = null;
                object ctr_categoria = null;
                object ctr_empresa_key = null;
                object ctr_transacc_key = null;
                object ctr_categoria_key = null;
                object ctr_tipo = null;
                object ctr_cuenta = null;
                object ctr_modulo = null;
                object ctr_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                tmp.TryGetValue("ctr_empresa", out ctr_empresa);
                tmp.TryGetValue("ctr_transacc", out ctr_transacc);
                tmp.TryGetValue("ctr_categoria", out ctr_categoria);
                tmp.TryGetValue("ctr_empresa_key", out ctr_empresa_key);
                tmp.TryGetValue("ctr_transacc_key", out ctr_transacc_key);
                tmp.TryGetValue("ctr_categoria_key", out ctr_categoria_key);
                tmp.TryGetValue("ctr_tipo", out ctr_tipo);
                tmp.TryGetValue("ctr_cuenta", out ctr_cuenta);
                tmp.TryGetValue("ctr_modulo", out ctr_modulo);
                tmp.TryGetValue("ctr_estado", out ctr_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                this.ctr_empresa = (Int32)Conversiones.GetValueByType(ctr_empresa, typeof(Int32));
                this.ctr_transacc = (Int32)Conversiones.GetValueByType(ctr_transacc, typeof(Int32));                
                this.ctr_categoria = (Int32)Conversiones.GetValueByType(ctr_categoria, typeof(Int32));
                this.ctr_empresa_key = (Int32)Conversiones.GetValueByType(ctr_empresa_key, typeof(Int32));
                this.ctr_transacc_key = (Int32)Conversiones.GetValueByType(ctr_transacc_key, typeof(Int32));
                this.ctr_categoria_key = (Int32)Conversiones.GetValueByType(ctr_categoria_key, typeof(Int32));
                this.ctr_tipo = (Int32)Conversiones.GetValueByType(ctr_tipo, typeof(Int32));
                this.ctr_cuenta = (Int32)Conversiones.GetValueByType(ctr_cuenta, typeof(Int32));
                this.ctr_modulo = (Int32?)Conversiones.GetValueByType(ctr_modulo, typeof(Int32?));
                this.ctr_estado = (Int32?)Conversiones.GetValueByType(ctr_estado, typeof(Int32?));
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

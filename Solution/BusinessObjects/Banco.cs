using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Banco
    {
        #region Properties

    [Data(key = true)]
	public Int32 ban_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 ban_empresa_key { get; set; }
	[Data(key = true ,auto= true)]
	public Int32 ban_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 ban_codigo_key { get; set; }
	public String ban_id { get; set; }
	public String ban_nombre { get; set; }
	public Int32? ban_tipo { get; set; }
	public Int32? ban_cuenta { get; set; }
	public Int32? ban_numero { get; set; }
	public Int32? ban_nro_cheque { get; set; }
	public DateTime? ban_ultcsc { get; set; }
    public String ban_codimp { get; set; }
	public Int32? ban_estado { get; set; }
        [Data(noupdate = true)]
	public String crea_usr { get; set; }
        [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }

	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
    public string crea_ip { get; set; }
    public string mod_ip { get; set; }

    [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "ban_empresa, ban_cuenta", keyref = "cue_empresa, cue_codigo", join = "left")]
    public string ban_nombrecuenta { get; set; }
              
        #endregion

        #region Constructors


        public  Banco()
        {
        }

        public Banco(Int32 ban_empresa, Int32 ban_codigo, String ban_id, String ban_nombre, Int32 ban_tipo, Int32 ban_cuenta, Int32 ban_numero, Int32 ban_nro_cheque, DateTime ban_ultcsc, String ban_codimp, Int32 ban_estado, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {                
    	this.ban_empresa = ban_empresa;
	this.ban_codigo = ban_codigo;
    this.ban_empresa_key = ban_empresa;
    this.ban_codigo_key = ban_codigo;
	this.ban_id = ban_id;
	this.ban_nombre = ban_nombre;
	this.ban_tipo = ban_tipo;
	this.ban_cuenta = ban_cuenta;
	this.ban_numero = ban_numero;
	this.ban_nro_cheque = ban_nro_cheque;
	this.ban_ultcsc = ban_ultcsc;
	this.ban_codimp = ban_codimp;
	this.ban_estado = ban_estado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Banco(IDataReader reader)
        {
    	this.ban_empresa = (Int32)reader["ban_empresa"];
	this.ban_codigo = (Int32)reader["ban_codigo"];
    this.ban_empresa_key = (Int32)reader["ban_empresa"];
    this.ban_codigo_key = (Int32)reader["ban_codigo"];
	this.ban_id = reader["ban_id"].ToString();
	this.ban_nombre = reader["ban_nombre"].ToString();
	this.ban_tipo = (reader["ban_tipo"] != DBNull.Value) ? (Int32?)reader["ban_tipo"] : null;
	this.ban_cuenta = (reader["ban_cuenta"] != DBNull.Value) ? (Int32?)reader["ban_cuenta"] : null;
	this.ban_numero = (reader["ban_numero"] != DBNull.Value) ? (Int32?)reader["ban_numero"] : null;
	this.ban_nro_cheque = (reader["ban_nro_cheque"] != DBNull.Value) ? (Int32?)reader["ban_nro_cheque"] : null;
	this.ban_ultcsc = (reader["ban_ultcsc"] != DBNull.Value) ? (DateTime?)reader["ban_ultcsc"] : null;
    this.ban_codimp = reader["ban_codimp"].ToString();
	this.ban_estado = (reader["ban_estado"] != DBNull.Value) ? (Int32?)reader["ban_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
    this.ban_nombrecuenta = reader["ban_nombrecuenta"].ToString();
            
        }

        public  Banco (object objeto)
        {
           
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object empresa = null;
                object empresakey = null;
                object codigokey = null;
                object id = null;
                object nombre = null;
                object activo = null;
                object tipo = null;
                object cuenta = null;
                object numero = null;
                object nro_cheque = null;
                object ultcsc = null;
                object codimp = null;


                tmp.TryGetValue("ban_codigo", out codigo);
                tmp.TryGetValue("ban_empresa", out empresa);
                tmp.TryGetValue("ban_codigo_key", out codigokey);
                tmp.TryGetValue("ban_empresa_key", out empresakey);
                tmp.TryGetValue("ban_id", out id);
                tmp.TryGetValue("ban_nombre", out nombre);
                tmp.TryGetValue("ban_tipo", out tipo);
                tmp.TryGetValue("ban_cuenta", out cuenta);
                tmp.TryGetValue("ban_nro_cheque", out nro_cheque);
                tmp.TryGetValue("ban_ultcsc", out ultcsc);
                tmp.TryGetValue("ban_codimp", out codimp);
                tmp.TryGetValue("ban_numero", out numero);
                tmp.TryGetValue("ban_estado", out activo);
                if (empresa != null && !empresa.Equals(""))
                {
                    this.ban_empresa = int.Parse(empresa.ToString());
                }
                if (empresakey != null && !empresakey.Equals(""))
                {
                    this.ban_empresa_key = int.Parse(empresakey.ToString());
                }
                if (codigo != null && !codigo.Equals(""))
                {
                    this.ban_codigo = int.Parse(codigo.ToString());
                }
                if (codigokey != null && !codigokey.Equals(""))
                {
                    this.ban_codigo_key = int.Parse(codigokey.ToString());
                }

                this.ban_id = (string)id;
                this.ban_nombre = (string)nombre;
                this.ban_tipo = Convert.ToInt32(tipo);
                this.ban_cuenta = Convert.ToInt32(cuenta);
                this.ban_numero = Convert.ToInt32(numero);
                this.ban_nro_cheque = Convert.ToInt32(nro_cheque);
                this.ban_ultcsc = Convert.ToDateTime(ultcsc);
                this.ban_codimp = (string)codimp;

                this.ban_estado = (int?)activo;
                this.crea_usr = "admin";
                this.crea_fecha = DateTime.Now;
                this.mod_usr = "admin";
                this.mod_fecha = DateTime.Now;
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

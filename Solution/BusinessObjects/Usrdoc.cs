using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Usrdoc
    {
        #region Properties

    [Data(key = true)]
	public Int32 udo_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 udo_empresa_key { get; set; }
	[Data(key = true)]
	public String udo_usuario { get; set; }
	[Data(originalkey = true)]
	public String udo_usuario_key { get; set; }
	[Data(key = true)]
	public Int32 udo_tipodoc { get; set; }
	[Data(originalkey = true)]
	public Int32 udo_tipodoc_key { get; set; }
	public Int32? udo_nivel_aprb { get; set; }
	public Int32? udo_ctipocom { get; set; }
	public Int32? udo_estado { get; set; }
         [Data(noupdate = true)]
	public String crea_usr { get; set; }
         [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

    [Data(nosql = true, tablaref = "ctipocom", camporef = "cti_id", foreign = "udo_ctipocom", keyref = "cti_codigo")]
    public string udo_idctipocom { get; set; }
    [Data(nosql = true, tablaref = "tipodoc", camporef = "tpd_id", foreign = "udo_tipodoc", keyref = "tpd_codigo")]
    public string udo_idtipodoc { get; set; }
    [Data(nosql = true, tablaref = "tipodoc", camporef = "tpd_nombre", foreign = "udo_tipodoc", keyref = "tpd_codigo")]
    public string udo_nombretipodoc { get; set; } 



        #endregion

        #region Constructors


        public  Usrdoc()
        {
        }

        public  Usrdoc( Int32 udo_empresa,String udo_usuario,Int32 udo_tipodoc,Int32 udo_nivel_aprb,Int32 udo_ctipocom,Int32 udo_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
            this.udo_empresa = udo_empresa;
	        this.udo_usuario = udo_usuario;
            this.udo_empresa_key = udo_empresa;
            this.udo_usuario_key = udo_usuario;
	        this.udo_tipodoc = udo_tipodoc;
            this.udo_tipodoc_key = udo_tipodoc;
	        this.udo_nivel_aprb = udo_nivel_aprb;
	        this.udo_ctipocom = udo_ctipocom;
	        this.udo_estado = udo_estado;
	        this.crea_usr = crea_usr;
	        this.crea_fecha = crea_fecha;
	        this.mod_usr = mod_usr;
	        this.mod_fecha = mod_fecha;

           
       }

        public  Usrdoc(IDataReader reader)
        {
    	    this.udo_empresa = (Int32)reader["udo_empresa"];
	        this.udo_usuario = reader["udo_usuario"].ToString();
            this.udo_empresa_key = (Int32)reader["udo_empresa"];
            this.udo_usuario_key = reader["udo_usuario"].ToString();
	        this.udo_tipodoc = (Int32)reader["udo_tipodoc"];
            this.udo_tipodoc_key = (Int32)reader["udo_tipodoc"];
	        this.udo_nivel_aprb = (reader["udo_nivel_aprb"] != DBNull.Value) ? (Int32?)reader["udo_nivel_aprb"] : null;
	        this.udo_ctipocom = (reader["udo_ctipocom"] != DBNull.Value) ? (Int32?)reader["udo_ctipocom"] : null;
	        this.udo_estado = (reader["udo_estado"] != DBNull.Value) ? (Int32?)reader["udo_estado"] : null;
	        this.crea_usr = reader["crea_usr"].ToString();
	        this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	        this.mod_usr = reader["mod_usr"].ToString();
	        this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.udo_idctipocom = reader["udo_idctipocom"].ToString();
            this.udo_idtipodoc = reader["udo_idtipodoc"].ToString();
            this.udo_nombretipodoc = reader["udo_nombretipodoc"].ToString();

        }


        public Usrdoc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object udo_empresa = null;
                object udo_usuario = null;
                object udo_tipodoc = null;
                object udo_nivel_aprb = null;
                object udo_ctipocom = null;
                object udo_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object id = null;

                tmp.TryGetValue("udo_empresa", out udo_empresa);
                tmp.TryGetValue("udo_usuario", out udo_usuario);
                tmp.TryGetValue("udo_tipodoc", out udo_tipodoc);
                tmp.TryGetValue("udo_nivel_aprb", out udo_nivel_aprb);
                tmp.TryGetValue("udo_ctipocom", out udo_ctipocom);
                tmp.TryGetValue("udo_estado", out udo_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("udo_nombretipodoc", out id);  

                this.udo_empresa = (Int32)Conversiones.GetValueByType(udo_empresa, typeof(Int32));
                this.udo_usuario = (String)Conversiones.GetValueByType(udo_usuario, typeof(String));
                this.udo_tipodoc = (Int32)Conversiones.GetValueByType(udo_tipodoc, typeof(Int32));
                this.udo_nivel_aprb = (Int32?)Conversiones.GetValueByType(udo_nivel_aprb, typeof(Int32?));
                this.udo_ctipocom = (Int32?)Conversiones.GetValueByType(udo_ctipocom, typeof(Int32?));
                this.udo_estado = (Int32?)Conversiones.GetValueByType(udo_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.udo_nombretipodoc = (String)Conversiones.GetValueByType(id, typeof(String));
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

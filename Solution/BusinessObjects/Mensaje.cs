using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Mensaje
    {
        #region Properties

    	[Data(key = true,auto=true)]
	public Int32 msj_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 msj_codigo_key { get; set; }
	[Data(key = true)]
	public Int32 msj_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 msj_empresa_key { get; set; }
	public String msj_usuarioenvia { get; set; }
	public String msj_mensaje { get; set; }
	public String msj_estadoenvio { get; set; }
	public Int32? msj_estado { get; set; }
        [Data(noupdate = true)]
	public String crea_usr { get; set; }
        [Data(noupdate = true)]
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }
	public String msj_id { get; set; }
	public String msj_asunto { get; set; }
	public DateTime? msj_fechacreacion { get; set; }
	public DateTime? msj_fechaenvio { get; set; }
    [Data(noprop = true)]
    public List<Mensajedestino> destino { get; set; }
              
        #endregion

        #region Constructors


        public  Mensaje()
        {
        }

        public  Mensaje( Int32 msj_codigo,Int32 msj_empresa,String msj_usuarioenvia,String msj_mensaje,String msj_estadoenvio,Int32 msj_estado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha,String msj_id,String msj_asunto,DateTime msj_fechacreacion,DateTime msj_fechaenvio)
        {                
    	this.msj_codigo = msj_codigo;
	this.msj_empresa = msj_empresa;
    this.msj_codigo_key = msj_codigo;
    this.msj_empresa_key = msj_empresa;
	this.msj_usuarioenvia = msj_usuarioenvia;
	this.msj_mensaje = msj_mensaje;
	this.msj_estadoenvio = msj_estadoenvio;
	this.msj_estado = msj_estado;
           
	this.crea_usr = crea_usr;
           
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;
	this.msj_id = msj_id;
	this.msj_asunto = msj_asunto;
	this.msj_fechacreacion = msj_fechacreacion;
	this.msj_fechaenvio = msj_fechaenvio;

           
       }

        public  Mensaje(IDataReader reader)
        {
    	this.msj_codigo = (Int32)reader["msj_codigo"];
	this.msj_empresa = (Int32)reader["msj_empresa"];
    this.msj_codigo_key = (Int32)reader["msj_codigo"];
    this.msj_empresa_key = (Int32)reader["msj_empresa"];
	this.msj_usuarioenvia = reader["msj_usuarioenvia"].ToString();
	this.msj_mensaje = reader["msj_mensaje"].ToString();
	this.msj_estadoenvio = reader["msj_estadoenvio"].ToString();
	this.msj_estado = (reader["msj_estado"] != DBNull.Value) ? (Int32?)reader["msj_estado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
	this.msj_id = reader["msj_id"].ToString();
	this.msj_asunto = reader["msj_asunto"].ToString();
	this.msj_fechacreacion = (reader["msj_fechacreacion"] != DBNull.Value) ? (DateTime?)reader["msj_fechacreacion"] : null;
	this.msj_fechaenvio = (reader["msj_fechaenvio"] != DBNull.Value) ? (DateTime?)reader["msj_fechaenvio"] : null;

        }




        public Mensaje(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object msj_usuarioenvia = null;
                object msj_mensaje = null;
                object msj_codigo = null;
                object msj_empresa = null;
                object msj_id = null;
                object msj_asunto = null;
                object msj_fechacreacion = null;
                object msj_fechaenvio = null;
                object msj_estadoenvio = null;
                object msj_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object destino = null;

                tmp.TryGetValue("msj_usuarioenvia", out msj_usuarioenvia);
                tmp.TryGetValue("msj_mensaje", out msj_mensaje);
                tmp.TryGetValue("msj_codigo", out msj_codigo);
                tmp.TryGetValue("msj_empresa", out msj_empresa);
                tmp.TryGetValue("msj_id", out msj_id);
                tmp.TryGetValue("msj_asunto", out msj_asunto);
                tmp.TryGetValue("msj_fechacreacion", out msj_fechacreacion);
                tmp.TryGetValue("msj_fechaenvio", out msj_fechaenvio);
                tmp.TryGetValue("msj_estadoenvio", out msj_estadoenvio);
                tmp.TryGetValue("msj_estado", out msj_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("destino", out destino);

                this.msj_usuarioenvia = (String)Conversiones.GetValueByType(msj_usuarioenvia, typeof(String));
                this.msj_mensaje = (String)Conversiones.GetValueByType(msj_mensaje, typeof(String));
                this.msj_codigo = (Int32)Conversiones.GetValueByType(msj_codigo, typeof(Int32));
                this.msj_empresa = (Int32)Conversiones.GetValueByType(msj_empresa, typeof(Int32));
                this.msj_id = (String)Conversiones.GetValueByType(msj_id, typeof(String));
                this.msj_asunto = (String)Conversiones.GetValueByType(msj_asunto, typeof(String));
                this.msj_fechacreacion = (DateTime?)Conversiones.GetValueByType(msj_fechacreacion, typeof(DateTime?));
                this.msj_fechaenvio = (DateTime?)Conversiones.GetValueByType(msj_fechaenvio, typeof(DateTime?));
                this.msj_estadoenvio = (String)Conversiones.GetValueByType(msj_estadoenvio, typeof(String));
                this.msj_estado = (Int32?)Conversiones.GetValueByType(msj_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                if (destino != null)
                {
                    List<Mensajedestino> lista = new List<Mensajedestino>();

                    foreach (object item in (object[])destino)
                    {
                        Mensajedestino t = new Mensajedestino();
                        t.msd_mensaje = this.msj_codigo;
                        t.msd_mensaje_key = this.msj_codigo_key;
                        t.msd_usuario = item.ToString();
                        t.msd_usuario_key = item.ToString();
                        t.msd_empresa = this.msj_empresa;
                        t.msd_empresa_key = this.msj_empresa;
                        t.msd_estado = 1;
                        t.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                        t.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                        t.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                        t.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                        lista.Add(t);
                    }
                    this.destino = lista;
                }
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

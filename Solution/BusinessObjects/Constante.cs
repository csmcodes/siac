using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Constante
    {
        #region Properties

    	[Data(key = true, auto=true)]
	public Int32 con_codigo { get; set; }
	[Data(originalkey = true)]
	public Int32 con_codigo_key { get; set; }
	public String con_usuario { get; set; }
	public Int32? con_empresa { get; set; }
	public Int32? con_almacen { get; set; }
	public Int32? con_pventa { get; set; }
    public String con_nombre { get; set; }
	public String con_valor { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Constante()
        {
        }

        public  Constante( Int32 con_codigo,String con_usuario,Int32 con_empresa,Int32 con_almacen,Int32 con_pventa,String con_nombre,String con_valor,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.con_codigo = con_codigo;
	this.con_usuario = con_usuario;
	this.con_empresa = con_empresa;
	this.con_almacen = con_almacen;
	this.con_pventa = con_pventa;
	this.con_nombre = con_nombre;
	this.con_valor = con_valor;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Constante(IDataReader reader)
        {
    	this.con_codigo = (Int32)reader["con_codigo"];
	this.con_usuario = reader["con_usuario"].ToString();
	this.con_empresa = (reader["con_empresa"] != DBNull.Value) ? (Int32?)reader["con_empresa"] : null;
	this.con_almacen = (reader["con_almacen"] != DBNull.Value) ? (Int32?)reader["con_almacen"] : null;
	this.con_pventa = (reader["con_pventa"] != DBNull.Value) ? (Int32?)reader["con_pventa"] : null;
	this.con_nombre = reader["con_nombre"].ToString();
	this.con_valor = reader["con_valor"].ToString();
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Constante(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object con_codigo = null;
	object con_usuario = null;
	object con_empresa = null;
	object con_almacen = null;
	object con_pventa = null;
	object con_nombre = null;
	object con_valor = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("con_codigo", out con_codigo);
	tmp.TryGetValue("con_usuario", out con_usuario);
	tmp.TryGetValue("con_empresa", out con_empresa);
	tmp.TryGetValue("con_almacen", out con_almacen);
	tmp.TryGetValue("con_pventa", out con_pventa);
	tmp.TryGetValue("con_nombre", out con_nombre);
	tmp.TryGetValue("con_valor", out con_valor);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.con_codigo = (Int32)Conversiones.GetValueByType(con_codigo, typeof(Int32));
	this.con_usuario = (String)Conversiones.GetValueByType(con_usuario, typeof(String));
	this.con_empresa = (Int32?)Conversiones.GetValueByType(con_empresa, typeof(Int32?));
	this.con_almacen = (Int32?)Conversiones.GetValueByType(con_almacen, typeof(Int32?));
	this.con_pventa = (Int32?)Conversiones.GetValueByType(con_pventa, typeof(Int32?));
	this.con_nombre = (String)Conversiones.GetValueByType(con_nombre, typeof(String));
	this.con_valor = (String)Conversiones.GetValueByType(con_valor, typeof(String));
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

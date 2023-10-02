using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dlistaprecio
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int dlpr_codigo { get; set; }
        [Data(originalkey = true)]
        public int dlpr_codigo_key { get; set; }      
        [Data(key = true)]
        public int dlpr_empresa { get; set; }
        [Data(originalkey = true)]
        public int dlpr_empresa_key { get; set; }
        [Data(key = true)]
        public int dlpr_listaprecio { get; set; }
        [Data(originalkey = true)]
        public int dlpr_listaprecio_key { get; set; }
        public int? dlpr_almacen { get; set; }
        public int? dlpr_producto { get; set; }
        public int? dlpr_umedida { get; set; }
        public DateTime? dlpr_fecha_ini{ get; set; }
        public DateTime? dlpr_fecha_fin { get; set; }
        public decimal? dlpr_precio { get; set; }   
        public int? dlpr_estado { get; set; }
        public int? dlpr_ruta { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        [Data(nosql = true, tablaref = "almacen", camporef = "alm_id", foreign = "dlpr_almacen", keyref = "alm_codigo")]
        public string dlpr_idalmacen { get; set; }
        [Data(nosql = true, tablaref = "producto", camporef = "pro_nombre", foreign = "dlpr_producto", keyref = "pro_codigo")]
        public string dlpr_nombreproducto { get; set; }
        [Data(nosql = true, tablaref = "umedida", camporef = "umd_nombre", foreign = "dlpr_umedida", keyref = "umd_codigo")]
        public string dlpr_nombreumedida { get; set; }

        [Data(nosql = true, tablaref = "ruta", camporef = "rut_nombre", foreign = "dlpr_ruta", keyref = "rut_codigo")]
        public string dlpr_nombreruta { get; set; }
        [Data(nosql = true, tablaref = "listaprecio", camporef = "lpr_nombre", foreign = "dlpr_listaprecio", keyref = "lpr_codigo")]
        public string dlpr_nombrelistaprecio { get; set; }


        #endregion

        #region Constructors


        public Dlistaprecio()
        {
        }

        public Dlistaprecio(int codigo, int empresa, int listaprecio, int? almacen, int? producto, int? umedida, DateTime? fecha_ini, DateTime? fecha_fin, decimal? precio, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {                
            this.dlpr_codigo =codigo;    	
            this.dlpr_codigo_key =codigo;       
            this.dlpr_empresa =empresa;    
            this.dlpr_empresa_key =empresa;
            this.dlpr_listaprecio = listaprecio;
            this.dlpr_listaprecio_key = listaprecio;
            this.dlpr_almacen = almacen;
            this.dlpr_producto = producto;
            this.dlpr_umedida = umedida;
            this.dlpr_fecha_ini = fecha_ini;
            this.dlpr_fecha_fin = fecha_fin;
            this.dlpr_precio = precio;
            this.dlpr_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Dlistaprecio(IDataReader reader)
        {
            this.dlpr_codigo = (int)reader["dlpr_codigo"];
            this.dlpr_codigo_key = (int)reader["dlpr_codigo"];
            this.dlpr_empresa = (int)reader["dlpr_empresa"];
            this.dlpr_empresa_key = (int)reader["dlpr_empresa"];
            this.dlpr_listaprecio = (int)reader["dlpr_listaprecio"];
            this.dlpr_listaprecio_key = (int)reader["dlpr_listaprecio"];
            this.dlpr_almacen = (reader["dlpr_almacen"] != DBNull.Value) ? (int?)reader["dlpr_almacen"] : null;
            this.dlpr_producto = (reader["dlpr_producto"] != DBNull.Value) ? (int?)reader["dlpr_producto"] : null;
            this.dlpr_umedida = (reader["dlpr_umedida"] != DBNull.Value) ? (int?)reader["dlpr_umedida"] : null;
            this.dlpr_fecha_ini = (reader["dlpr_fecha_ini"] != DBNull.Value) ? (DateTime?)reader["dlpr_fecha_ini"] : null;
            this.dlpr_fecha_fin = (reader["dlpr_fecha_fin"] != DBNull.Value) ? (DateTime?)reader["dlpr_fecha_fin"] : null;
            this.dlpr_precio = (reader["dlpr_precio"] != DBNull.Value) ? (decimal?)reader["dlpr_precio"] : null;
            this.dlpr_estado = (reader["dlpr_estado"] != DBNull.Value) ? (int?)reader["dlpr_estado"] : null;
            this.dlpr_ruta = (reader["dlpr_ruta"] != DBNull.Value) ? (int?)reader["dlpr_ruta"] : null;
            
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.dlpr_idalmacen = reader["dlpr_idalmacen"].ToString();
            this.dlpr_nombreproducto = reader["dlpr_nombreproducto"].ToString();
            this.dlpr_nombreumedida = reader["dlpr_nombreumedida"].ToString();
            this.dlpr_nombrelistaprecio = reader["dlpr_nombrelistaprecio"].ToString();
            this.dlpr_nombreruta = reader["dlpr_nombreruta"].ToString();

        

        }

        public  Dlistaprecio(object objeto)
        {
           
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object empresa = null;
                object empresakey = null;
                object listaprecio = null;
                object listapreciokey = null;
                object almacen = null;
                object producto = null;
                object umedida = null;
                object fecha_ini = null;
                object fecha_fin = null;
                object precio = null;
                object activo = null;
                object idalmacen = null;
                object dlpr_ruta = null;

                object nombreproducto = null;
                object nombreumedida = null;
                tmp.TryGetValue("dlpr_codigo", out codigo);
                tmp.TryGetValue("dlpr_codigo_key", out codigokey);
                tmp.TryGetValue("dlpr_empresa", out empresa);
                tmp.TryGetValue("dlpr_empresa_key", out empresakey);
                tmp.TryGetValue("dlpr_listaprecio", out listaprecio);
                tmp.TryGetValue("dlpr_listaprecio_key", out listapreciokey);
                tmp.TryGetValue("dlpr_almacen", out almacen);
                tmp.TryGetValue("dlpr_producto", out producto);
                tmp.TryGetValue("dlpr_umedida", out umedida);
                tmp.TryGetValue("dlpr_fecha_ini", out fecha_ini);
                tmp.TryGetValue("dlpr_fecha_fin", out fecha_fin);
                tmp.TryGetValue("dlpr_precio", out precio);
                tmp.TryGetValue("dlpr_estado", out activo);
                tmp.TryGetValue("dlpr_idalmacen", out idalmacen);
                tmp.TryGetValue("dlpr_nombreproducto", out nombreproducto);
                tmp.TryGetValue("dlpr_nombreumedida", out nombreumedida);
                tmp.TryGetValue("dlpr_ruta", out dlpr_ruta);
                if (codigo != null && !codigo.Equals(""))
                {
                    this.dlpr_codigo = Convert.ToInt32(codigo);
                }
                if (codigokey != null && !codigokey.Equals(""))
                {
                    this.dlpr_codigo_key = Convert.ToInt32(codigokey);
                }
                this.dlpr_empresa =  Convert.ToInt32(empresa);
                this.dlpr_empresa_key = Convert.ToInt32(empresakey);
                this.dlpr_listaprecio = Convert.ToInt32(listaprecio);
                this.dlpr_listaprecio_key = Convert.ToInt32(listapreciokey);
                this.dlpr_almacen = (Int32?)Conversiones.GetValueByType(almacen, typeof(Int32?));
                this.dlpr_producto = Convert.ToInt32(producto); 
                this.dlpr_umedida = Convert.ToInt32(umedida);
                this.dlpr_fecha_ini = Convert.ToDateTime(fecha_ini);
                this.dlpr_fecha_fin = Convert.ToDateTime(fecha_fin);
                this.dlpr_precio = Convert.ToDecimal(precio);
                this.dlpr_ruta = (Int32?)Conversiones.GetValueByType(dlpr_ruta, typeof(Int32?));
                this.dlpr_estado = (int?)activo;
                this.crea_usr = "admin";
                this.crea_fecha = DateTime.Now;
                this.mod_usr = "admin";
                this.mod_fecha = DateTime.Now;
                this.dlpr_idalmacen = (string)idalmacen;
                this.dlpr_nombreproducto = (string)nombreproducto;
                this.dlpr_nombreumedida = (string)nombreumedida;
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

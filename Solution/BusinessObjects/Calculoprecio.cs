using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Calculoprecio
    {
        #region Properties

        [Data(key = true)]
        public Int32 cpr_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 cpr_empresa_key { get; set; }
        [Data(key = true)]
        public Int32 cpr_listaprecio { get; set; }
        [Data(originalkey = true)]
        public Int32 cpr_listaprecio_key { get; set; }
        [Data(key = true, auto = true)]
        public Int32 cpr_codigo { get; set; }
        [Data(originalkey = true)]
        public Int32 cpr_codigo_key { get; set; }
        public Int32? cpr_almacen { get; set; }
        public Int32? cpr_ruta { get; set; }
        public Int32? cpr_tproducto { get; set; }
        public String cpr_nombre { get; set; }
        public Decimal? cpr_indice { get; set; }
        public Decimal? cpr_valor { get; set; }
        public Decimal? cpr_peso { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        [Data(nosql = true, tablaref = "almacen", camporef = "alm_nombre", foreign = "cpr_empresa, cpr_almacen", keyref = "alm_empresa,alm_codigo", join = "inner")]
        public string cpr_almacennombre { get; set; }

        [Data(nosql = true, tablaref = "listaprecio", camporef = "lpr_nombre", foreign = "cpr_empresa, cpr_listaprecio", keyref = "lpr_empresa,lpr_codigo", join = "inner")]
        public string cpr_listaprecionombre { get; set; }

        [Data(nosql = true, tablaref = "ruta", camporef = "rut_nombre", foreign = "cpr_empresa, cpr_ruta", keyref = "rut_empresa,rut_codigo", join = "inner")]
        public string cpr_rutanombre { get; set; }

        [Data(nosql = true, tablaref = "tproducto", camporef = "tpr_nombre", foreign = "cpr_empresa, cpr_tproducto", keyref = "tpr_empresa,tpr_codigo", join = "inner")]
        public string cpr_tproductonombre { get; set; }



        #endregion

        #region Constructors


        public Calculoprecio()
        {
        }

        public Calculoprecio(Int32 cpr_empresa, Int32 cpr_listaprecio, Int32 cpr_codigo, Int32 cpr_almacen, Int32 cpr_ruta, Int32 cpr_tproducto, String cpr_nombre, Decimal cpr_indice, Decimal cpr_valor, Decimal cpr_peso, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.cpr_empresa = cpr_empresa;
            this.cpr_listaprecio = cpr_listaprecio;
            this.cpr_codigo = cpr_codigo;
            this.cpr_almacen = cpr_almacen;
            this.cpr_ruta = cpr_ruta;
            this.cpr_tproducto = cpr_tproducto;
            this.cpr_nombre = cpr_nombre;
            this.cpr_indice = cpr_indice;
            this.cpr_valor = cpr_valor;
            this.cpr_peso = cpr_peso;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }

        public Calculoprecio(IDataReader reader)
        {
            this.cpr_empresa = (Int32)reader["cpr_empresa"];
            this.cpr_listaprecio = (Int32)reader["cpr_listaprecio"];
            this.cpr_codigo = (Int32)reader["cpr_codigo"];
            this.cpr_almacen = (reader["cpr_almacen"] != DBNull.Value) ? (Int32?)reader["cpr_almacen"] : null;
            this.cpr_ruta = (reader["cpr_ruta"] != DBNull.Value) ? (Int32?)reader["cpr_ruta"] : null;
            this.cpr_tproducto = (reader["cpr_tproducto"] != DBNull.Value) ? (Int32?)reader["cpr_tproducto"] : null;
            this.cpr_nombre = reader["cpr_nombre"].ToString();
            this.cpr_indice = (reader["cpr_indice"] != DBNull.Value) ? (Decimal?)reader["cpr_indice"] : null;
            this.cpr_valor = (reader["cpr_valor"] != DBNull.Value) ? (Decimal?)reader["cpr_valor"] : null;
            this.cpr_peso = (reader["cpr_peso"] != DBNull.Value) ? (Decimal?)reader["cpr_peso"] : null;
            this.cpr_almacennombre = (reader["cpr_almacennombre"] != DBNull.Value) ? (string)reader["cpr_almacennombre"] : null;
            this.cpr_listaprecionombre = (reader["cpr_listaprecionombre"] != DBNull.Value) ? (string)reader["cpr_listaprecionombre"] : null;
            this.cpr_rutanombre = (reader["cpr_rutanombre"] != DBNull.Value) ? (string)reader["cpr_rutanombre"] : null;
            this.cpr_tproductonombre = (reader["cpr_tproductonombre"] != DBNull.Value) ? (string)reader["cpr_tproductonombre"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;


        }


        public Calculoprecio(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object cpr_empresa = null;
                object cpr_listaprecio = null;
                object cpr_codigo = null;
                object cpr_almacen = null;
                object cpr_ruta = null;
                object cpr_tproducto = null;
                object cpr_nombre = null;
                object cpr_indice = null;
                object cpr_valor = null;
                object cpr_peso = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;

                tmp.TryGetValue("cpr_empresa", out cpr_empresa);
                tmp.TryGetValue("cpr_listaprecio", out cpr_listaprecio);
                tmp.TryGetValue("cpr_codigo", out cpr_codigo);
                tmp.TryGetValue("cpr_almacen", out cpr_almacen);
                tmp.TryGetValue("cpr_ruta", out cpr_ruta);
                tmp.TryGetValue("cpr_tproducto", out cpr_tproducto);
                tmp.TryGetValue("cpr_nombre", out cpr_nombre);
                tmp.TryGetValue("cpr_indice", out cpr_indice);
                tmp.TryGetValue("cpr_valor", out cpr_valor);
                tmp.TryGetValue("cpr_peso", out cpr_peso);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.cpr_empresa = (Int32)Conversiones.GetValueByType(cpr_empresa, typeof(Int32));
                this.cpr_listaprecio = (Int32)Conversiones.GetValueByType(cpr_listaprecio, typeof(Int32));
                this.cpr_codigo = (Int32)Conversiones.GetValueByType(cpr_codigo, typeof(Int32));
                this.cpr_almacen = (Int32?)Conversiones.GetValueByType(cpr_almacen, typeof(Int32?));
                this.cpr_ruta = (Int32?)Conversiones.GetValueByType(cpr_ruta, typeof(Int32?));
                this.cpr_tproducto = (Int32?)Conversiones.GetValueByType(cpr_tproducto, typeof(Int32?));
                this.cpr_nombre = (String)Conversiones.GetValueByType(cpr_nombre, typeof(String));
                this.cpr_indice = (Decimal?)Conversiones.GetValueByType(cpr_indice, typeof(Decimal?));
                this.cpr_valor = (Decimal?)Conversiones.GetValueByType(cpr_valor, typeof(Decimal?));
                this.cpr_peso = (Decimal?)Conversiones.GetValueByType(cpr_peso, typeof(Decimal?));
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

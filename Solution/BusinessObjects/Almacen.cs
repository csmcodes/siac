﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Almacen
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int alm_codigo { get; set; }
        [Data(originalkey = true)]
        public int alm_codigo_key { get; set; }      
        [Data(key = true)]
        public int alm_empresa { get; set; }
        [Data(originalkey = true)]
        public int alm_empresa_key { get; set; }
        public string alm_id { get; set; }
        public string alm_nombre { get; set; }
        public string alm_subfijo { get; set; }
        public string alm_gerente { get; set; }
        public int? alm_pais { get; set; }
        public int? alm_provincia { get; set; }
        public int? alm_canton { get; set; }    
        public string alm_direccion { get; set; }
        public string alm_telefono1 { get; set; }
        public string alm_telefono2 { get; set; }
        public string alm_telefono3 { get; set; }        
        
        public string alm_ruc { get; set; }
        public string alm_fax { get; set; }
        public int? alm_cliente_def { get; set; }
        public int? alm_centro { get; set; }
        public int? alm_matriz { get; set; }
        public int? alm_cuentacaja { get; set; }
        public int? alm_cuentapago { get; set; }
        public int? alm_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public int? alm_cat_cliente { get; set; }
        public string crea_ip { get; set; }
        public string mod_ip { get; set; }

        [Data(nosql = true, tablaref = "cuenta", camporef = "cue_nombre", foreign = "alm_empresa, alm_cuentacaja", keyref = "cue_empresa, cue_codigo", join = "left")]
        public string alm_nombrecuenta { get; set; }


        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "alm_empresa, alm_cliente_def", keyref = "per_empresa, per_codigo", join = "left")]
        public string alm_nombrepersona { get; set; }

        #endregion

        #region Constructors


        public Almacen()
        {
        }

        public Almacen(int codigo ,int empresa  , string id ,  string nombre , string subfijo ,   string gerente , int? pais ,  int? provincia , int? canton , string direccion , string telefono1 ,         string telefono2 ,         string telefono3 , string per_observacion, string ruc ,  string fax ,     int? cliente_def ,   int? centro ,    int? matriz ,     int? cuentacaja ,  int? estado, int? cat_cliente ,    string crea_usr ,     DateTime? crea_fecha ,   string mod_usr ,     DateTime? mod_fecha )
        {                
            this.alm_codigo =codigo;    	
            this.alm_codigo_key =codigo;       
            this.alm_empresa =empresa;    
            this.alm_empresa_key =empresa;
            this.alm_id =id;
            this.alm_nombre =nombre;
            this.alm_subfijo =subfijo;
            this.alm_gerente =gerente;
            this.alm_pais =pais;
            this.alm_provincia =provincia;
            this.alm_canton = canton;   
            this.alm_direccion =direccion;
            this.alm_telefono1 =telefono1;
            this.alm_telefono2 =telefono2;
            this.alm_telefono3 =telefono3;
            this.alm_ruc =ruc;
            this.alm_fax =fax;
            this.alm_cliente_def =cliente_def;
            this.alm_centro =centro;
            this.alm_matriz =matriz;
            this.alm_cuentacaja =cuentacaja;
            this.alm_estado =estado;
            this.alm_cat_cliente = cat_cliente;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Almacen(IDataReader reader)
        {
            this.alm_codigo = (int)reader["alm_codigo"];
            this.alm_codigo_key = (int)reader["alm_codigo"];
            this.alm_empresa = (int)reader["alm_empresa"];
            this.alm_empresa_key = (int)reader["alm_empresa"];
            this.alm_id = reader["alm_id"].ToString();
            this.alm_nombre = reader["alm_nombre"].ToString();
            this.alm_subfijo = reader["alm_subfijo"].ToString();
            this.alm_gerente = reader["alm_gerente"].ToString();
            this.alm_pais = (reader["alm_pais"] != DBNull.Value) ? (int?)reader["alm_pais"] : null;
            this.alm_provincia = (reader["alm_provincia"] != DBNull.Value) ? (int?)reader["alm_provincia"] : null;
            this.alm_canton = (reader["alm_canton"] != DBNull.Value) ? (int?)reader["alm_canton"] : null;
            this.alm_direccion = reader["alm_direccion"].ToString();
            this.alm_telefono1 = reader["alm_telefono1"].ToString();
            this.alm_telefono2 = reader["alm_telefono2"].ToString();
            this.alm_telefono3 = reader["alm_telefono3"].ToString();
            this.alm_ruc = reader["alm_ruc"].ToString();
            this.alm_fax = reader["alm_fax"].ToString();
            this.alm_cliente_def = (reader["alm_cliente_def"] != DBNull.Value) ? (int?)reader["alm_cliente_def"] : null;
            this.alm_centro = (reader["alm_centro"] != DBNull.Value) ? (int?)reader["alm_centro"] : null;
            this.alm_matriz = (reader["alm_matriz"] != DBNull.Value) ? (int?)reader["alm_matriz"] : null;
            this.alm_cuentacaja = (reader["alm_cuentacaja"] != DBNull.Value) ? (int?)reader["alm_cuentacaja"] : null;
            this.alm_cuentapago= (reader["alm_cuentapago"] != DBNull.Value) ? (int?)reader["alm_cuentapago"] : null;
            this.alm_estado = (reader["alm_estado"] != DBNull.Value) ? (int?)reader["alm_estado"] : null;
            this.alm_cat_cliente = (reader["alm_cat_cliente"] != DBNull.Value) ? (int?)reader["alm_cat_cliente"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.alm_nombrecuenta = reader["alm_nombrecuenta"].ToString();
        }


        public Almacen(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object alm_empresa = null;
                object alm_codigo = null;
                object alm_id = null;
                object alm_nombre = null;
                object alm_subfijo = null;
                object alm_gerente = null;
                object alm_pais = null;
                object alm_provincia = null;
                object alm_canton = null;
                object alm_direccion = null;
                object alm_telefono1 = null;
                object alm_telefono2 = null;
                object alm_telefono3 = null;
                object alm_ruc = null;
                object alm_fax = null;
                object alm_cliente_def = null;
                object alm_centro = null;
                object alm_matriz = null;
                object alm_cuentacaja = null;
                object alm_cuentapago = null;
                object alm_estado = null;
                object alm_cat_cliente = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("alm_empresa", out alm_empresa);
                tmp.TryGetValue("alm_codigo", out alm_codigo);
                tmp.TryGetValue("alm_id", out alm_id);
                tmp.TryGetValue("alm_nombre", out alm_nombre);
                tmp.TryGetValue("alm_subfijo", out alm_subfijo);
                tmp.TryGetValue("alm_gerente", out alm_gerente);
                tmp.TryGetValue("alm_pais", out alm_pais);
                tmp.TryGetValue("alm_provincia", out alm_provincia);
                tmp.TryGetValue("alm_canton", out alm_canton);
                tmp.TryGetValue("alm_direccion", out alm_direccion);
                tmp.TryGetValue("alm_telefono1", out alm_telefono1);
                tmp.TryGetValue("alm_telefono2", out alm_telefono2);
                tmp.TryGetValue("alm_telefono3", out alm_telefono3);
                tmp.TryGetValue("alm_ruc", out alm_ruc);
                tmp.TryGetValue("alm_fax", out alm_fax);
                tmp.TryGetValue("alm_cliente_def", out alm_cliente_def);
                tmp.TryGetValue("alm_centro", out alm_centro);
                tmp.TryGetValue("alm_matriz", out alm_matriz);
                tmp.TryGetValue("alm_cuentacaja", out alm_cuentacaja);
                tmp.TryGetValue("alm_cuentapago", out alm_cuentapago);
                tmp.TryGetValue("alm_estado", out alm_estado);
                tmp.TryGetValue("alm_cat_cliente", out alm_cat_cliente);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.alm_empresa = (Int32)Conversiones.GetValueByType(alm_empresa, typeof(Int32));
                this.alm_codigo = (Int32)Conversiones.GetValueByType(alm_codigo, typeof(Int32));
                this.alm_id = (String)Conversiones.GetValueByType(alm_id, typeof(String));
                this.alm_nombre = (String)Conversiones.GetValueByType(alm_nombre, typeof(String));
                this.alm_subfijo = (String)Conversiones.GetValueByType(alm_subfijo, typeof(String));
                this.alm_gerente = (String)Conversiones.GetValueByType(alm_gerente, typeof(String));
                this.alm_pais = (Int32?)Conversiones.GetValueByType(alm_pais, typeof(Int32?));
                this.alm_provincia = (Int32?)Conversiones.GetValueByType(alm_provincia, typeof(Int32?));
                this.alm_canton = (Int32?)Conversiones.GetValueByType(alm_canton, typeof(Int32?));
                this.alm_direccion = (String)Conversiones.GetValueByType(alm_direccion, typeof(String));
                this.alm_telefono1 = (String)Conversiones.GetValueByType(alm_telefono1, typeof(String));
                this.alm_telefono2 = (String)Conversiones.GetValueByType(alm_telefono2, typeof(String));
                this.alm_telefono3 = (String)Conversiones.GetValueByType(alm_telefono3, typeof(String));
                this.alm_ruc = (String)Conversiones.GetValueByType(alm_ruc, typeof(String));
                this.alm_fax = (String)Conversiones.GetValueByType(alm_fax, typeof(String));
                this.alm_cliente_def = (Int32?)Conversiones.GetValueByType(alm_cliente_def, typeof(Int32?));
                this.alm_centro = (Int32?)Conversiones.GetValueByType(alm_centro, typeof(Int32?));
                this.alm_matriz = (Int32?)Conversiones.GetValueByType(alm_matriz, typeof(Int32?));
                this.alm_cuentacaja = (Int32?)Conversiones.GetValueByType(alm_cuentacaja, typeof(Int32?));
                this.alm_cuentapago = (Int32?)Conversiones.GetValueByType(alm_cuentapago, typeof(Int32?));
                this.alm_estado = (Int32?)Conversiones.GetValueByType(alm_estado, typeof(Int32?));
                this.alm_cat_cliente= (Int32?)Conversiones.GetValueByType(alm_cat_cliente, typeof(Int32?));
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

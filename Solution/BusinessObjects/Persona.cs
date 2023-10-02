using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Persona
    {
        #region Properties
        [Data(key = true, auto = true)]
        public int per_codigo { get; set; }
        [Data(originalkey = true)]
        public int per_codigo_key { get; set; }      
        [Data(key = true)]
        public int per_empresa { get; set; }
        [Data(originalkey = true)]
        public int per_empresa_key { get; set; }
        public string per_ciruc { get; set; }
        public string per_tipoid { get; set; }            
        public string per_nombres { get; set; }
        public string per_apellidos { get; set; }
        public string per_id { get; set; }
        public string per_direccion { get; set; }
        public string per_telefono { get; set; }
        public string per_celular { get; set; }
        public string per_mail { get; set; }            
        public string per_observacion{ get; set; }
        public int? per_pais{ get; set; }
        public int? per_provincia{ get; set; }
        public int? per_canton{ get; set; }
        public int? per_parroquia { get; set; }
        public string per_contribuyente{ get; set; }
        public int? per_contribuyente_especial{ get; set; }
        public string per_contacto{ get; set; }
        public  string per_contacto_direccion{ get; set; }
        public string per_contacto_telefono{ get; set; }
        public string per_razon { get; set; }
        public string per_representantelegal { get; set; }
        public string per_paginaweb { get; set; }

        public String per_genero { get; set; }  
        public Int32? per_cpersona { get; set; }
        public Int32? per_tpersona { get; set; }
        public int? per_listaprecio { get; set; }
        public int? per_politica { get; set; }
              
        public Int32? per_retiva { get; set; }
        public Int32? per_retfuente { get; set; }

        public Int32? per_agente { get; set; }
        public Int32? per_bloqueo { get; set; }
        public Int32? per_tarjeta { get; set; }
        public Decimal? per_cupo { get; set; }
        public Decimal? per_ilimitado { get; set; }
        public Int32? per_impuesto { get; set; }  

        public int? per_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        [Data(noprop = true)]
        public List<Personaxtipo> tipos { get; set; }
        [Data(noprop = true)]
        public Socioempleado socio { get; set; }
        [Data(noprop = true)]
        public Chofer chofer { get; set; }
        
        //[Data(nosql = true, tablaref = "persona", camporef = "per_nombre", foreign = "per_agente", keyref = "per_codigo")]
        [Data(noprop = true)]
        public string per_agentenombre { get; set; }
        //[Data(nosql = true, tablaref = "persona", camporef = "per_id", foreign = "per_agente", keyref = "per_codigo")]
        [Data(noprop = true)]
        public string per_agenteid { get; set; }
        [Data(nosql = true, tablaref = "listaprecio", camporef = "lpr_nombre", foreign = "per_listaprecio", keyref = "lpr_codigo", join = "left")]
        public string per_listanombre { get; set; }
        [Data(nosql = true, tablaref = "listaprecio", camporef = "lpr_id", foreign = "per_listaprecio", keyref = "lpr_codigo", join = "left")]
        public string per_listaid { get; set; }
        
        [Data(nosql = true, tablaref = "politica", camporef = "pol_nombre", foreign = "per_politica", keyref = "pol_codigo", join = "left")]
        public string per_politicanombre { get; set; }
        [Data(nosql = true, tablaref = "politica", camporef = "pol_id", foreign = "per_politica", keyref = "pol_codigo", join = "left")]
        public string per_politicaid { get; set; }
        [Data(nosql = true, tablaref = "politica", camporef = "pol_porc_desc", foreign = "per_politica", keyref = "pol_codigo", join = "left")]
        public decimal? per_politicadesc { get; set; }
        [Data(nosql = true, tablaref = "politica", camporef = "pol_nro_pagos", foreign = "per_politica", keyref = "pol_codigo", join = "left")]
        public int? per_politicanropagos { get; set; }
        [Data(nosql = true, tablaref = "politica", camporef = "pol_dias_plazo", foreign = "per_politica", keyref = "pol_codigo", join = "left")]
        public int? per_politicadiasplazo { get; set; }
        [Data(nosql = true, tablaref = "politica", camporef = "pol_porc_pago_con", foreign = "per_politica", keyref = "pol_codigo", join = "left")]
        public decimal? per_politicaporpagocon { get; set; }

      
        #endregion

        #region Constructors


        public Persona()
        {

        }

        


        public Persona(Int32 per_empresa, Int32 per_codigo, String per_id, String per_ciruc, String per_tipoid, String per_nombres, String per_apellidos, String per_direccion, String per_telefono, String per_celular, String per_mail, String per_genero, String per_observacion, Int32 per_pais, Int32 per_provincia, Int32 per_canton, Int32 per_parroquia, String per_contacto, String per_contacto_direccion, String per_contacto_telefono, String per_razon, String per_representantelegal, String per_paginaweb, Int32 per_cpersona, Int32 per_tpersona, Int32 per_impuesto, Int32 per_agente, Int32 per_listaprecio, Int32 per_politica, Int32 per_bloqueo, Int32 per_tarjeta, Decimal per_cupo, Decimal per_ilimitado, String per_contribuyente, Int32 per_contribuyente_especial, Int32 per_retiva, Int32 per_retfuente, Int32 per_estado, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.per_codigo = per_codigo;
            this.per_codigo_key = per_codigo;
            this.per_empresa = per_empresa;
            this.per_empresa_key = per_empresa;
            this.per_id = per_id;
            this.per_ciruc = per_ciruc;
            this.per_tipoid = per_tipoid;
            this.per_nombres = per_nombres;
            this.per_apellidos = per_apellidos;
            this.per_direccion = per_direccion;
            this.per_telefono = per_telefono;
            this.per_celular = per_celular;
            this.per_mail = per_mail;
            this.per_genero = per_genero;
            this.per_observacion = per_observacion;
            this.per_pais = per_pais;
            this.per_provincia = per_provincia;
            this.per_canton = per_canton;
            this.per_parroquia = per_parroquia;
            this.per_contacto = per_contacto;
            this.per_contacto_direccion = per_contacto_direccion;
            this.per_contacto_telefono = per_contacto_telefono;
            this.per_razon = per_razon;
            this.per_representantelegal = per_representantelegal;
            this.per_paginaweb = per_paginaweb;
            this.per_cpersona = per_cpersona;
            this.per_tpersona = per_tpersona;
            this.per_impuesto = per_impuesto;
            this.per_agente = per_agente;
            this.per_listaprecio = per_listaprecio;
            this.per_politica = per_politica;
            this.per_bloqueo = per_bloqueo;
            this.per_tarjeta = per_tarjeta;
            this.per_cupo = per_cupo;
            this.per_ilimitado = per_ilimitado;
            this.per_contribuyente = per_contribuyente;
            this.per_contribuyente_especial = per_contribuyente_especial;
            this.per_retiva = per_retiva;
            this.per_retfuente = per_retfuente;
            this.per_estado = per_estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }




        public Persona(IDataReader reader)
        {
            this.per_codigo =(int)reader["per_codigo"]; 
            this.per_codigo_key =(int)reader["per_codigo"];
            this.per_empresa = (int)reader["per_empresa"];
            this.per_empresa_key = (int)reader["per_empresa"];
            this.per_ciruc = reader["per_ciruc"].ToString();
            this.per_id = reader["per_id"].ToString();
            this.per_tipoid = reader["per_tipoid"].ToString();
            this.per_nombres = reader["per_nombres"].ToString();
            this.per_apellidos = reader["per_apellidos"].ToString();
            this.per_direccion = reader["per_direccion"].ToString();
            this.per_telefono = reader["per_telefono"].ToString();
            this.per_celular = reader["per_celular"].ToString();
            this.per_mail = reader["per_mail"].ToString();
            this.per_observacion = reader["per_observacion"].ToString();
            this.per_pais = (reader["per_pais"] != DBNull.Value) ? (int?)reader["per_pais"] : null;
            this.per_provincia =  (reader["per_provincia"] != DBNull.Value) ? (int?)reader["per_provincia"] : null;
            this.per_canton =  (reader["per_canton"] != DBNull.Value) ? (int?)reader["per_canton"] : null;
            this.per_parroquia = (reader["per_parroquia"] != DBNull.Value) ? (int?)reader["per_parroquia"] : null;
            this.per_contribuyente = reader["per_contribuyente"].ToString();
            this.per_contribuyente_especial = (reader["per_contribuyente_especial"] != DBNull.Value) ? (int?)reader["per_contribuyente_especial"] : null;
            this.per_contacto =  reader["per_contacto"].ToString();
            this.per_contacto_direccion =reader["per_contacto_direccion"].ToString();
            this.per_contacto_telefono = reader["per_contacto_telefono"].ToString();
            this.per_razon =  reader["per_razon"].ToString();
            this.per_representantelegal = reader["per_representantelegal"].ToString();
            this.per_paginaweb = reader["per_paginaweb"].ToString();
            this.per_genero = reader["per_genero"].ToString();
            this.per_cpersona = (reader["per_cpersona"] != DBNull.Value) ? (Int32?)reader["per_cpersona"] : null;
            this.per_tpersona = (reader["per_tpersona"] != DBNull.Value) ? (Int32?)reader["per_tpersona"] : null;
            this.per_impuesto = (reader["per_impuesto"] != DBNull.Value) ? (Int32?)reader["per_impuesto"] : null;
            this.per_bloqueo = (reader["per_bloqueo"] != DBNull.Value) ? (Int32?)reader["per_bloqueo"] : null;
            this.per_tarjeta = (reader["per_tarjeta"] != DBNull.Value) ? (Int32?)reader["per_tarjeta"] : null;
            this.per_cupo = (reader["per_cupo"] != DBNull.Value) ? (Decimal?)reader["per_cupo"] : null;
            this.per_ilimitado = (reader["per_ilimitado"] != DBNull.Value) ? (Decimal?)reader["per_ilimitado"] : null;
            this.per_retiva = (reader["per_retiva"] != DBNull.Value) ? (Int32?)reader["per_retiva"] : null;
            this.per_retfuente = (reader["per_retfuente"] != DBNull.Value) ? (Int32?)reader["per_retfuente"] : null;
            this.per_agente= (reader["per_agente"] != DBNull.Value) ? (int?)reader["per_agente"] : null;
            this.per_listaprecio = (reader["per_listaprecio"] != DBNull.Value) ? (int?)reader["per_listaprecio"] : null;
            this.per_listaid= reader["per_listaid"].ToString();
            this.per_listanombre= reader["per_listanombre"].ToString();
            this.per_politica = (reader["per_politica"] != DBNull.Value) ? (int?)reader["per_politica"] : null;
            this.per_politicaid = reader["per_politicaid"].ToString();
            this.per_politicanombre = reader["per_politicanombre"].ToString();
            this.per_politicadesc = (reader["per_politicadesc"] != DBNull.Value) ? (decimal?)reader["per_politicadesc"] : null;
            this.per_politicanropagos = (reader["per_politicanropagos"] != DBNull.Value) ? (int?)reader["per_politicanropagos"] : null;
            this.per_politicadiasplazo = (reader["per_politicadiasplazo"] != DBNull.Value) ? (int?)reader["per_politicadiasplazo"] : null;
            this.per_politicaporpagocon = (reader["per_politicaporpagocon"] != DBNull.Value) ? (decimal?)reader["per_politicaporpagocon"] : null;

            this.per_estado = (reader["per_estado"] != DBNull.Value) ? (int?)reader["per_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }

        public Persona(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object per_empresa = null;
                object per_codigo = null;
                object per_id = null;
                object per_ciruc = null;
                object per_tipoid = null;
                object per_nombres = null;
                object per_apellidos = null;
                object per_direccion = null;
                object per_telefono = null;
                object per_celular = null;
                object per_mail = null;
                object per_genero = null;
                object per_observacion = null;
                object per_pais = null;
                object per_provincia = null;
                object per_canton = null;
                object per_parroquia = null;
                object per_contacto = null;
                object per_contacto_direccion = null;
                object per_contacto_telefono = null;
                object per_razon = null;
                object per_representantelegal = null;
                object per_paginaweb = null;
                object per_cpersona = null;
                object per_tpersona = null;
                object per_impuesto = null;
                object per_agente = null;
                object per_listaprecio = null;
                object per_politica = null;
                object per_bloqueo = null;
                object per_tarjeta = null;
                object per_cupo = null;
                object per_ilimitado = null;
                object per_contribuyente = null;
                object per_contribuyente_especial = null;
                object per_retiva = null;
                object per_retfuente = null;
                object per_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("per_empresa", out per_empresa);
                tmp.TryGetValue("per_codigo", out per_codigo);
                tmp.TryGetValue("per_id", out per_id);
                tmp.TryGetValue("per_ciruc", out per_ciruc);
                tmp.TryGetValue("per_tipoid", out per_tipoid);
                tmp.TryGetValue("per_nombres", out per_nombres);
                tmp.TryGetValue("per_apellidos", out per_apellidos);
                tmp.TryGetValue("per_direccion", out per_direccion);
                tmp.TryGetValue("per_telefono", out per_telefono);
                tmp.TryGetValue("per_celular", out per_celular);
                tmp.TryGetValue("per_mail", out per_mail);
                tmp.TryGetValue("per_genero", out per_genero);
                tmp.TryGetValue("per_observacion", out per_observacion);
                tmp.TryGetValue("per_pais", out per_pais);
                tmp.TryGetValue("per_provincia", out per_provincia);
                tmp.TryGetValue("per_canton", out per_canton);
                tmp.TryGetValue("per_parroquia", out per_parroquia);
                tmp.TryGetValue("per_contacto", out per_contacto);
                tmp.TryGetValue("per_contacto_direccion", out per_contacto_direccion);
                tmp.TryGetValue("per_contacto_telefono", out per_contacto_telefono);
                tmp.TryGetValue("per_razon", out per_razon);
                tmp.TryGetValue("per_representantelegal", out per_representantelegal);
                tmp.TryGetValue("per_paginaweb", out per_paginaweb);
                tmp.TryGetValue("per_cpersona", out per_cpersona);
                tmp.TryGetValue("per_tpersona", out per_tpersona);
                tmp.TryGetValue("per_impuesto", out per_impuesto);
                tmp.TryGetValue("per_agente", out per_agente);
                tmp.TryGetValue("per_listaprecio", out per_listaprecio);
                tmp.TryGetValue("per_politica", out per_politica);
                tmp.TryGetValue("per_bloqueo", out per_bloqueo);
                tmp.TryGetValue("per_tarjeta", out per_tarjeta);
                tmp.TryGetValue("per_cupo", out per_cupo);
                tmp.TryGetValue("per_ilimitado", out per_ilimitado);
                tmp.TryGetValue("per_contribuyente", out per_contribuyente);
                tmp.TryGetValue("per_contribuyente_especial", out per_contribuyente_especial);
                tmp.TryGetValue("per_retiva", out per_retiva);
                tmp.TryGetValue("per_retfuente", out per_retfuente);
                tmp.TryGetValue("per_estado", out per_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.per_empresa = (Int32)Conversiones.GetValueByType(per_empresa, typeof(Int32));
                this.per_codigo = (Int32)Conversiones.GetValueByType(per_codigo, typeof(Int32));
                this.per_id = (String)Conversiones.GetValueByType(per_id, typeof(String));
                this.per_ciruc = (String)Conversiones.GetValueByType(per_ciruc, typeof(String));
                this.per_tipoid = (String)Conversiones.GetValueByType(per_tipoid, typeof(String));
                this.per_nombres = (String)Conversiones.GetValueByType(per_nombres, typeof(String));
                this.per_apellidos = (String)Conversiones.GetValueByType(per_apellidos, typeof(String));
                this.per_direccion = (String)Conversiones.GetValueByType(per_direccion, typeof(String));
                this.per_telefono = (String)Conversiones.GetValueByType(per_telefono, typeof(String));
                this.per_celular = (String)Conversiones.GetValueByType(per_celular, typeof(String));
                this.per_mail = (String)Conversiones.GetValueByType(per_mail, typeof(String));
                this.per_genero = (String)Conversiones.GetValueByType(per_genero, typeof(String));
                this.per_observacion = (String)Conversiones.GetValueByType(per_observacion, typeof(String));
                this.per_pais = (Int32?)Conversiones.GetValueByType(per_pais, typeof(Int32?));
                this.per_provincia = (Int32?)Conversiones.GetValueByType(per_provincia, typeof(Int32?));
                this.per_canton = (Int32?)Conversiones.GetValueByType(per_canton, typeof(Int32?));
                this.per_parroquia = (Int32?)Conversiones.GetValueByType(per_parroquia, typeof(Int32?));
                this.per_contacto = (String)Conversiones.GetValueByType(per_contacto, typeof(String));
                this.per_contacto_direccion = (String)Conversiones.GetValueByType(per_contacto_direccion, typeof(String));
                this.per_contacto_telefono = (String)Conversiones.GetValueByType(per_contacto_telefono, typeof(String));
                this.per_razon = (String)Conversiones.GetValueByType(per_razon, typeof(String));
                this.per_representantelegal = (String)Conversiones.GetValueByType(per_representantelegal, typeof(String));
                this.per_paginaweb = (String)Conversiones.GetValueByType(per_paginaweb, typeof(String));
                this.per_cpersona = (Int32?)Conversiones.GetValueByType(per_cpersona, typeof(Int32?));
                this.per_tpersona = (Int32?)Conversiones.GetValueByType(per_tpersona, typeof(Int32?));
                this.per_impuesto = (Int32?)Conversiones.GetValueByType(per_impuesto, typeof(Int32?));
                this.per_agente = (Int32?)Conversiones.GetValueByType(per_agente, typeof(Int32?));
                this.per_listaprecio = (Int32?)Conversiones.GetValueByType(per_listaprecio, typeof(Int32?));
                this.per_politica = (Int32?)Conversiones.GetValueByType(per_politica, typeof(Int32?));
                this.per_bloqueo = (Int32?)Conversiones.GetValueByType(per_bloqueo, typeof(Int32?));
                this.per_tarjeta = (Int32?)Conversiones.GetValueByType(per_tarjeta, typeof(Int32?));
                this.per_cupo = (Decimal?)Conversiones.GetValueByType(per_cupo, typeof(Decimal?));
                this.per_ilimitado = (Decimal?)Conversiones.GetValueByType(per_ilimitado, typeof(Decimal?));
                this.per_contribuyente = (String)Conversiones.GetValueByType(per_contribuyente, typeof(String));
                this.per_contribuyente_especial = (Int32?)Conversiones.GetValueByType(per_contribuyente_especial, typeof(Int32?));
                this.per_retiva = (Int32?)Conversiones.GetValueByType(per_retiva, typeof(Int32?));
                this.per_retfuente = (Int32?)Conversiones.GetValueByType(per_retfuente, typeof(Int32?));
                this.per_estado = (Int32?)Conversiones.GetValueByType(per_estado, typeof(Int32?));
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


        public List<Persona> GetEnum()
        {
            return new List<Persona>();
        }


        #endregion


    }
}

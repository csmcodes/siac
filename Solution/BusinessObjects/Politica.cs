using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Politica
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int pol_codigo { get; set; }
        [Data(originalkey = true)]
        public int pol_codigo_key { get; set; }      
        [Data(key = true)]
        public int pol_empresa { get; set; }
        [Data(originalkey = true)]
        public int pol_empresa_key { get; set; }
        public string pol_id { get; set; }
        public string pol_nombre { get; set; }        
        public decimal? pol_monto_ini { get; set; }
        public decimal?pol_monto_fin  { get; set; }
        public decimal? pol_porc_desc { get; set; }
        public decimal?  pol_promocion { get; set; }
        public decimal?pol_porc_pago_con  { get; set; }
        public decimal? pol_porc_finanacia { get; set; }
        public int? pol_nro_pagos { get; set; }
        public int? pol_dias_plazo { get; set; }
        public int? pol_tclipro { get; set; }        
        public int? pol_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
              
        #endregion

        #region Constructors


        public Politica()
        {
        }

        public Politica(int codigo, int empresa, string id, string nombre, decimal? monto_ini, decimal? monto_fin, decimal? porc_desc, decimal? promocion, decimal? porc_pago_con, decimal? porc_finanacia, int? nro_pagos, int? dias_plazo, int? estado, int?  pol_tclipro, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {                
            this.pol_codigo =codigo;    	
            this.pol_codigo_key =codigo;       
            this.pol_empresa =empresa;    
            this.pol_empresa_key =empresa;
            this.pol_id =id;
            this.pol_nombre =nombre;
            this.pol_monto_ini = monto_ini;
            this.pol_monto_fin = monto_fin;
            this.pol_porc_desc = porc_desc;
            this.pol_promocion = promocion;
            this.pol_porc_pago_con = porc_pago_con;
            this.pol_porc_finanacia = porc_finanacia;
            this.pol_nro_pagos = nro_pagos;
            this.pol_dias_plazo = dias_plazo;            
            this.pol_estado =estado;
            this.pol_tclipro = pol_tclipro;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Politica(IDataReader reader)
        {
            this.pol_codigo = (int)reader["pol_codigo"];
            this.pol_codigo_key = (int)reader["pol_codigo"];
            this.pol_empresa = (int)reader["pol_empresa"];
            this.pol_empresa_key = (int)reader["pol_empresa"];
            this.pol_id = reader["pol_id"].ToString();
            this.pol_nombre = reader["pol_nombre"].ToString();
            this.pol_monto_ini = (reader["pol_monto_ini"] != DBNull.Value) ? (decimal?)reader["pol_monto_ini"] : null;
            this.pol_monto_fin = (reader["pol_monto_fin"] != DBNull.Value) ? (decimal?)reader["pol_monto_fin"] : null;
            this.pol_porc_desc = (reader["pol_porc_desc"] != DBNull.Value) ? (decimal?)reader["pol_porc_desc"] : null;
            this.pol_promocion = (reader["pol_promocion"] != DBNull.Value) ? (decimal?)reader["pol_promocion"] : null;
            this.pol_porc_pago_con = (reader["pol_porc_pago_con"] != DBNull.Value) ? (decimal?)reader["pol_porc_pago_con"] : null;
            this.pol_porc_finanacia = (reader["pol_porc_finanacia"] != DBNull.Value) ? (decimal?)reader["pol_porc_finanacia"] : null;
            this.pol_nro_pagos = (reader["pol_nro_pagos"] != DBNull.Value) ? (int?)reader["pol_nro_pagos"] : null;
            this.pol_dias_plazo = (reader["pol_dias_plazo"] != DBNull.Value) ? (int?)reader["pol_dias_plazo"] : null; 
            this.pol_estado = (reader["pol_estado"] != DBNull.Value) ? (int?)reader["pol_estado"] : null;
            this.pol_tclipro = (reader["pol_tclipro"] != DBNull.Value) ? (int?)reader["pol_tclipro"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }


        public Politica(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object pol_nro_pagos = null;
                object pol_dias_plazo = null;
                object pol_tclipro = null;
                object pol_empresa = null;
                object pol_codigo = null;
                object pol_id = null;
                object pol_nombre = null;
                object pol_monto_ini = null;
                object pol_monto_fin = null;
                object pol_porc_desc = null;
                object pol_promocion = null;
                object pol_porc_pago_con = null;
                object pol_porc_finanacia = null;
                object pol_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("pol_nro_pagos", out pol_nro_pagos);
                tmp.TryGetValue("pol_dias_plazo", out pol_dias_plazo);
                tmp.TryGetValue("pol_tclipro", out pol_tclipro);
                tmp.TryGetValue("pol_empresa", out pol_empresa);
                tmp.TryGetValue("pol_codigo", out pol_codigo);
                tmp.TryGetValue("pol_id", out pol_id);
                tmp.TryGetValue("pol_nombre", out pol_nombre);
                tmp.TryGetValue("pol_monto_ini", out pol_monto_ini);
                tmp.TryGetValue("pol_monto_fin", out pol_monto_fin);
                tmp.TryGetValue("pol_porc_desc", out pol_porc_desc);
                tmp.TryGetValue("pol_promocion", out pol_promocion);
                tmp.TryGetValue("pol_porc_pago_con", out pol_porc_pago_con);
                tmp.TryGetValue("pol_porc_finanacia", out pol_porc_finanacia);
                tmp.TryGetValue("pol_estado", out pol_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.pol_nro_pagos = (Int32)Conversiones.GetValueByType(pol_nro_pagos, typeof(Int32));
                this.pol_dias_plazo = (Int32)Conversiones.GetValueByType(pol_dias_plazo, typeof(Int32));
                this.pol_tclipro = (Int32)Conversiones.GetValueByType(pol_tclipro, typeof(Int32));
                this.pol_empresa = (Int32)Conversiones.GetValueByType(pol_empresa, typeof(Int32));
                this.pol_codigo = (Int32)Conversiones.GetValueByType(pol_codigo, typeof(Int32));
                this.pol_id = (String)Conversiones.GetValueByType(pol_id, typeof(String));
                this.pol_nombre = (String)Conversiones.GetValueByType(pol_nombre, typeof(String));
                this.pol_monto_ini = (Decimal)Conversiones.GetValueByType(pol_monto_ini, typeof(Decimal));
                this.pol_monto_fin = (Decimal)Conversiones.GetValueByType(pol_monto_fin, typeof(Decimal));
                this.pol_porc_desc = (Decimal?)Conversiones.GetValueByType(pol_porc_desc, typeof(Decimal?));
                this.pol_promocion = (Decimal?)Conversiones.GetValueByType(pol_promocion, typeof(Decimal?));
                this.pol_porc_pago_con = (Decimal?)Conversiones.GetValueByType(pol_porc_pago_con, typeof(Decimal?));
                this.pol_porc_finanacia = (Decimal?)Conversiones.GetValueByType(pol_porc_finanacia, typeof(Decimal?));
                this.pol_estado = (Int32?)Conversiones.GetValueByType(pol_estado, typeof(Int32?));
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

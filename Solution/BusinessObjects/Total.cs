using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Total
    {
        #region Properties

        public Decimal tot_subtotal { get; set; }
        public Decimal tot_descuento1 { get; set; }
        public Decimal tot_descuento2 { get; set; }
        public Decimal tot_subtot_0 { get; set; }
        public Decimal tot_desc1_0 { get; set; }
        public Decimal tot_desc2_0 { get; set; }
        public Decimal tot_timpuesto { get; set; }
        public Decimal tot_tservicio { get; set; }
        public Decimal tot_transporte { get; set; }
        public Decimal tot_ice { get; set; }
        public Decimal tot_financia { get; set; }
        public Decimal tot_total { get; set; }
        [Data(key = true)]
        public Int32 tot_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 tot_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 tot_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 tot_comprobante_key { get; set; }
        public Int32? tot_impuesto { get; set; }
        public Int32? tot_servicio { get; set; }
        public Decimal? tot_porc_desc { get; set; }
        public Decimal? tot_porc_financ { get; set; }
        public Int32? tot_dias_plazo { get; set; }
        public Int32? tot_nro_pagos { get; set; }
        public Int32? tot_transportista { get; set; }
        public Decimal? tot_porc_impuesto { get; set; }
        public Decimal? tot_porc_servicio { get; set; }
        public Decimal? tot_porc_seguro { get; set; }
        public Decimal? tot_anticipo { get; set; }
        public Decimal? tot_paga { get; set; }
        public Decimal? tot_vseguro { get; set; }
        [Data(noupdate = true)]
        public String crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public Decimal? tot_tseguro { get; set; }



        [Data(noprop = true)]
        public Decimal? tot_cantidad { get; set; }

        [Data(noprop = true)]
        public Object[] poliNombres { get; set; }

        [Data(noprop = true)]
        public Decimal[] poliValores { get; set; }



        #endregion

        #region Constructors


        public Total()
        {
        }

        public Total(Decimal tot_subtotal, Decimal tot_descuento1, Decimal tot_descuento2, Decimal tot_subtot_0, Decimal tot_desc1_0, Decimal tot_desc2_0, Decimal tot_timpuesto, Decimal tot_tservicio, Decimal tot_transporte, Decimal tot_ice, Decimal tot_financia, Decimal tot_total, Int32 tot_empresa, Int64 tot_comprobante, Int32 tot_impuesto, Int32 tot_servicio, Decimal tot_porc_desc, Decimal tot_porc_financ, Int32 tot_dias_plazo, Int32 tot_nro_pagos, Int32 tot_transportista, Decimal tot_porc_impuesto, Decimal tot_porc_servicio, Decimal tot_porc_seguro, Decimal tot_anticipo, Decimal tot_paga, Decimal tot_vseguro, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, Decimal tot_tseguro)
        {
            this.tot_subtotal = tot_subtotal;
            this.tot_descuento1 = tot_descuento1;
            this.tot_descuento2 = tot_descuento2;
            this.tot_subtot_0 = tot_subtot_0;
            this.tot_desc1_0 = tot_desc1_0;
            this.tot_desc2_0 = tot_desc2_0;
            this.tot_timpuesto = tot_timpuesto;
            this.tot_tservicio = tot_tservicio;
            this.tot_transporte = tot_transporte;
            this.tot_ice = tot_ice;
            this.tot_financia = tot_financia;
            this.tot_total = tot_total;
            this.tot_empresa = tot_empresa;
            this.tot_comprobante = tot_comprobante;
            this.tot_empresa_key = tot_empresa;
            this.tot_comprobante_key = tot_comprobante;
            this.tot_impuesto = tot_impuesto;
            this.tot_servicio = tot_servicio;
            this.tot_porc_desc = tot_porc_desc;
            this.tot_porc_financ = tot_porc_financ;
            this.tot_dias_plazo = tot_dias_plazo;
            this.tot_nro_pagos = tot_nro_pagos;
            this.tot_transportista = tot_transportista;
            this.tot_porc_impuesto = tot_porc_impuesto;
            this.tot_porc_servicio = tot_porc_servicio;
            this.tot_porc_seguro = tot_porc_seguro;
            this.tot_anticipo = tot_anticipo;
            this.tot_paga = tot_paga;
            this.tot_vseguro = tot_vseguro;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.tot_tseguro = tot_tseguro;


        }

        public Total(IDataReader reader)
        {
            this.tot_subtotal = (Decimal)reader["tot_subtotal"];
            this.tot_descuento1 = (Decimal)reader["tot_descuento1"];
            this.tot_descuento2 = (Decimal)reader["tot_descuento2"];
            this.tot_subtot_0 = (Decimal)reader["tot_subtot_0"];
            this.tot_desc1_0 = (Decimal)reader["tot_desc1_0"];
            this.tot_desc2_0 = (Decimal)reader["tot_desc2_0"];
            this.tot_timpuesto = (Decimal)reader["tot_timpuesto"];
            this.tot_tservicio = (Decimal)reader["tot_tservicio"];
            this.tot_transporte = (Decimal)reader["tot_transporte"];
            this.tot_ice = (Decimal)reader["tot_ice"];
            this.tot_financia = (Decimal)reader["tot_financia"];
            this.tot_total = (Decimal)reader["tot_total"];
            this.tot_empresa = (Int32)reader["tot_empresa"];
            this.tot_comprobante = (Int64)reader["tot_comprobante"];
            this.tot_empresa_key = (Int32)reader["tot_empresa"];
            this.tot_comprobante_key = (Int64)reader["tot_comprobante"];
            this.tot_impuesto = (reader["tot_impuesto"] != DBNull.Value) ? (Int32?)reader["tot_impuesto"] : null;
            this.tot_servicio = (reader["tot_servicio"] != DBNull.Value) ? (Int32?)reader["tot_servicio"] : null;
            this.tot_porc_desc = (reader["tot_porc_desc"] != DBNull.Value) ? (Decimal?)reader["tot_porc_desc"] : null;
            this.tot_porc_financ = (reader["tot_porc_financ"] != DBNull.Value) ? (Decimal?)reader["tot_porc_financ"] : null;
            this.tot_dias_plazo = (reader["tot_dias_plazo"] != DBNull.Value) ? (Int32?)reader["tot_dias_plazo"] : null;
            this.tot_nro_pagos = (reader["tot_nro_pagos"] != DBNull.Value) ? (Int32?)reader["tot_nro_pagos"] : null;
            this.tot_transportista = (reader["tot_transportista"] != DBNull.Value) ? (Int32?)reader["tot_transportista"] : null;
            this.tot_porc_impuesto = (reader["tot_porc_impuesto"] != DBNull.Value) ? (Decimal?)reader["tot_porc_impuesto"] : null;
            this.tot_porc_servicio = (reader["tot_porc_servicio"] != DBNull.Value) ? (Decimal?)reader["tot_porc_servicio"] : null;
            this.tot_porc_seguro = (reader["tot_porc_seguro"] != DBNull.Value) ? (Decimal?)reader["tot_porc_seguro"] : null;
            this.tot_anticipo = (reader["tot_anticipo"] != DBNull.Value) ? (Decimal?)reader["tot_anticipo"] : null;
            this.tot_paga = (reader["tot_paga"] != DBNull.Value) ? (Decimal?)reader["tot_paga"] : null;
            this.tot_vseguro = (reader["tot_vseguro"] != DBNull.Value) ? (Decimal?)reader["tot_vseguro"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.tot_tseguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
          

        }

        public Total(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;


                object tot_empresa = null;
                object tot_comprobante = null;
                object tot_empresa_key = null;
                object tot_comprobante_key = null;
                object tot_subtotal = null;
                object tot_descuento1 = null;
                object tot_descuento2 = null;
                object tot_subtot_0 = null;
                object tot_desc1_0 = null;
                object tot_desc2_0 = null;
                object tot_timpuesto = null;
                object tot_tservicio = null;
                object tot_transporte = null;
                object tot_ice = null;
                object tot_financia = null;
                object tot_total = null;
                object tot_anticipo = null;
                object tot_paga = null;
                object tot_vseguro = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object tot_tseguro = null;
                object tot_impuesto = null;
                object tot_servicio = null;
                object tot_porc_desc = null;
                object tot_porc_financ = null;
                object tot_dias_plazo = null;
                object tot_nro_pagos = null;
                object tot_transportista = null;
                object tot_porc_impuesto = null;
                object tot_porc_servicio = null;
                object tot_porc_seguro = null;
                object tot_cantidad = null;
               


                tmp.TryGetValue("tot_empresa", out tot_empresa);
                tmp.TryGetValue("tot_comprobante", out tot_comprobante);
                tmp.TryGetValue("tot_empresa_key", out tot_empresa_key);
                tmp.TryGetValue("tot_comprobante_key", out tot_comprobante_key);
                tmp.TryGetValue("tot_subtotal", out tot_subtotal);
                tmp.TryGetValue("tot_descuento1", out tot_descuento1);
                tmp.TryGetValue("tot_descuento2", out tot_descuento2);
                tmp.TryGetValue("tot_subtot_0", out tot_subtot_0);
                tmp.TryGetValue("tot_desc1_0", out tot_desc1_0);
                tmp.TryGetValue("tot_desc2_0", out tot_desc2_0);
                tmp.TryGetValue("tot_timpuesto", out tot_timpuesto);
                tmp.TryGetValue("tot_tservicio", out tot_tservicio);
                tmp.TryGetValue("tot_transporte", out tot_transporte);
                tmp.TryGetValue("tot_ice", out tot_ice);
                tmp.TryGetValue("tot_financia", out tot_financia);
                tmp.TryGetValue("tot_total", out tot_total);
                tmp.TryGetValue("tot_anticipo", out tot_anticipo);
                tmp.TryGetValue("tot_paga", out tot_paga);
                tmp.TryGetValue("tot_vseguro", out tot_vseguro);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("tot_tseguro", out tot_tseguro);
                tmp.TryGetValue("tot_impuesto", out tot_impuesto);
                tmp.TryGetValue("tot_servicio", out tot_servicio);
                tmp.TryGetValue("tot_porc_desc", out tot_porc_desc);
                tmp.TryGetValue("tot_porc_financ", out tot_porc_financ);
                tmp.TryGetValue("tot_dias_plazo", out tot_dias_plazo);
                tmp.TryGetValue("tot_nro_pagos", out tot_nro_pagos);
                tmp.TryGetValue("tot_transportista", out tot_transportista);
                tmp.TryGetValue("tot_porc_impuesto", out tot_porc_impuesto);
                tmp.TryGetValue("tot_porc_servicio", out tot_porc_servicio);
                tmp.TryGetValue("tot_porc_seguro", out tot_porc_seguro);
                tmp.TryGetValue("tot_cantidad", out tot_cantidad);
               


                this.tot_empresa = (Int32)Conversiones.GetValueByType(tot_empresa, typeof(Int32));
                this.tot_comprobante = (Int64)Conversiones.GetValueByType(tot_comprobante, typeof(Int64));
                this.tot_empresa_key = (Int32)Conversiones.GetValueByType(tot_empresa_key, typeof(Int32));
                this.tot_comprobante_key = (Int64)Conversiones.GetValueByType(tot_comprobante_key, typeof(Int64));
                this.tot_subtotal = (Decimal)Conversiones.GetValueByType(tot_subtotal, typeof(Decimal));
                this.tot_descuento1 = (Decimal)Conversiones.GetValueByType(tot_descuento1, typeof(Decimal));
                this.tot_descuento2 = (Decimal)Conversiones.GetValueByType(tot_descuento2, typeof(Decimal));
                this.tot_subtot_0 = (Decimal)Conversiones.GetValueByType(tot_subtot_0, typeof(Decimal));
                this.tot_desc1_0 = (Decimal)Conversiones.GetValueByType(tot_desc1_0, typeof(Decimal));
                this.tot_desc2_0 = (Decimal)Conversiones.GetValueByType(tot_desc2_0, typeof(Decimal));
                this.tot_timpuesto = (Decimal)Conversiones.GetValueByType(tot_timpuesto, typeof(Decimal));
                this.tot_tservicio = (Decimal)Conversiones.GetValueByType(tot_tservicio, typeof(Decimal));
                this.tot_transporte = (Decimal)Conversiones.GetValueByType(tot_transporte, typeof(Decimal));
                this.tot_ice = (Decimal)Conversiones.GetValueByType(tot_ice, typeof(Decimal));
                this.tot_financia = (Decimal)Conversiones.GetValueByType(tot_financia, typeof(Decimal));
                this.tot_total = (Decimal)Conversiones.GetValueByType(tot_total, typeof(Decimal));
                this.tot_anticipo = (Decimal?)Conversiones.GetValueByType(tot_anticipo, typeof(Decimal?));
                this.tot_paga = (Decimal?)Conversiones.GetValueByType(tot_paga, typeof(Decimal?));
                this.tot_vseguro = (Decimal?)Conversiones.GetValueByType(tot_vseguro, typeof(Decimal?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.tot_tseguro = (Decimal?)Conversiones.GetValueByType(tot_tseguro, typeof(Decimal?));
                this.tot_impuesto = (Int32?)Conversiones.GetValueByType(tot_impuesto, typeof(Int32?));
                this.tot_servicio = (Int32?)Conversiones.GetValueByType(tot_servicio, typeof(Int32?));
                this.tot_porc_desc = (Decimal?)Conversiones.GetValueByType(tot_porc_desc, typeof(Decimal?));
                this.tot_porc_financ = (Decimal?)Conversiones.GetValueByType(tot_porc_financ, typeof(Decimal?));
                this.tot_dias_plazo = (Int32?)Conversiones.GetValueByType(tot_dias_plazo, typeof(Int32?));
                this.tot_nro_pagos = (Int32?)Conversiones.GetValueByType(tot_nro_pagos, typeof(Int32?));
                this.tot_transportista = (Int32?)Conversiones.GetValueByType(tot_transportista, typeof(Int32?));
                this.tot_porc_impuesto = (Decimal?)Conversiones.GetValueByType(tot_porc_impuesto, typeof(Decimal?));
                this.tot_porc_servicio = (Decimal?)Conversiones.GetValueByType(tot_porc_servicio, typeof(Decimal?));
                this.tot_porc_seguro = (Decimal?)Conversiones.GetValueByType(tot_porc_seguro, typeof(Decimal?));
                this.tot_cantidad = (Decimal?)Conversiones.GetValueByType(tot_cantidad, typeof(Decimal?));



                /*obj.crea_usr = (crea_usr != null) ? (int)crea_usr : obj.crea_usr;
                obj.crea_fecha = (crea_fecha != null) ? (int)crea_fecha : obj.crea_fecha;
                obj.mod_usr = (mod_usr != null) ? (int)mod_usr : obj.mod_usr;
                obj.mod_fecha = (mod_fecha != null) ? (int)mod_fecha : obj.mod_fecha;*/


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

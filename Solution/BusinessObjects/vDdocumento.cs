using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDdocumento
    {
        public int? ddo_empresa { get; set; }
        public Int64? ddo_comprobante { get; set; }
        public int? ddo_transacc { get; set; }
        public string ddo_doctran { get; set; }
        public int? ddo_pago { get; set; }
        public int? ddo_comprobante_guia { get; set; }
        public int? ddo_codclipro { get; set; }


        public int? ddo_debcre { get; set; }
        public int? ddo_tipo_cambio { get; set; }
        public Decimal? ddo_monto_ext { get; set; }
        public Decimal? ddo_cancela_ext { get; set; }
        public int? ddo_agente { get; set; }
        public int? ddo_cuenta { get; set; }
        public int? ddo_modulo { get; set; }


        public DateTime? ddo_fecha_emi { get; set; }
        public DateTime? ddo_fecha_ven { get; set; }
        public decimal? ddo_monto { get; set; }
        public decimal? ddo_cancela { get; set; }
        public int? ddo_cancelado { get; set; }
        public int? cenv_socio { get; set; }
        public string per_ciruc { get; set; }
        public string per_nombres { get; set; }
        public string per_apellidos { get; set; }
        public string per_razon { get; set; }

        public string cdoc_aut_factura { get; set; }
        public string com_doctran_guia { get; set; }
        public int? cenv_socio_guia { get; set; }
        public string per_ciruc_guia { get; set; }
        public string per_nombres_guia { get; set; }
        public string per_apellidos_guia { get; set; }
        public string per_razon_guia { get; set; }

        public Decimal? total { get; set; }
        public Decimal? tot_subtot_0 { get; set; }
        public Decimal? tot_subtotal { get; set; }
        public Decimal? tot_impuesto { get; set; }
        public Decimal? tot_tseguro { get; set; }
        public Decimal? tot_transporte { get; set; }
        public Decimal? tot_ice { get; set; }
        public Decimal? tot_tservicio { get; set; }
        public Decimal? tot_descuento1 { get; set; }
        

           #region Constructors


        public vDdocumento()
        {

        }



        public vDdocumento(IDataReader reader)
        {
            this.ddo_empresa = (reader["ddo_empresa"] != DBNull.Value) ? (int?)reader["ddo_empresa"] : null;
            this.ddo_comprobante = (reader["ddo_comprobante"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante"] : null;
            this.ddo_transacc = (reader["ddo_transacc"] != DBNull.Value) ? (int?)reader["ddo_transacc"] : null;
            this.ddo_doctran = (reader["ddo_doctran"] != DBNull.Value) ? (string)reader["ddo_doctran"] : null;
            this.ddo_pago = (reader["ddo_pago"] != DBNull.Value) ? (int?)reader["ddo_pago"] : null;
            this.ddo_codclipro = (reader["ddo_codclipro"] != DBNull.Value) ? (int?)reader["ddo_codclipro"] : null;

            this.ddo_debcre = (reader["ddo_debcre"] != DBNull.Value) ? (int?)reader["ddo_debcre"] : null;
            this.ddo_tipo_cambio = (reader["ddo_tipo_cambio"] != DBNull.Value) ? (int?)reader["ddo_tipo_cambio"] : null;
            this.ddo_monto_ext = (reader["ddo_monto_ext"] != DBNull.Value) ? (decimal?)reader["ddo_monto_ext"] : null;
            this.ddo_cancela_ext = (reader["ddo_cancela_ext"] != DBNull.Value) ? (decimal?)reader["ddo_cancela_ext"] : null;
            this.ddo_agente= (reader["ddo_agente"] != DBNull.Value) ? (int?)reader["ddo_agente"] : null;
            this.ddo_cuenta= (reader["ddo_cuenta"] != DBNull.Value) ? (int?)reader["ddo_cuenta"] : null;
            this.ddo_modulo = (reader["ddo_modulo"] != DBNull.Value) ? (int?)reader["ddo_modulo"] : null;

            this.ddo_fecha_emi = (reader["ddo_fecha_emi"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_emi"] : null;
            this.ddo_fecha_ven = (reader["ddo_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_ven"] : null;
            this.ddo_monto = (reader["ddo_monto"] != DBNull.Value) ? (decimal?)reader["ddo_monto"] : null;
            this.ddo_cancela = (reader["ddo_cancela"] != DBNull.Value) ? (decimal?)reader["ddo_cancela"] : null;
            this.ddo_cancelado = (reader["ddo_cancelado"] != DBNull.Value) ? (int?)reader["ddo_cancelado"] : null;
            this.cenv_socio = (reader["cenv_socio"] != DBNull.Value) ? (int?)reader["cenv_socio"] : null;
            this.per_ciruc = (reader["per_ciruc"] != DBNull.Value) ? (string)reader["per_ciruc"] : null;
            this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
            this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
            this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
            this.com_doctran_guia = (reader["com_doctran_guia"] != DBNull.Value) ? (string)reader["com_doctran_guia"] : null;

            this.cdoc_aut_factura = (reader["cdoc_aut_factura"] != DBNull.Value) ? (string)reader["cdoc_aut_factura"] : null;

            this.cenv_socio_guia = (reader["cenv_socio_guia"] != DBNull.Value) ? (int?)reader["cenv_socio_guia"] : null;
            this.per_ciruc_guia = (reader["per_ciruc_guia"] != DBNull.Value) ? (string)reader["per_ciruc_guia"] : null;
            this.per_nombres_guia = (reader["per_nombres_guia"] != DBNull.Value) ? (string)reader["per_nombres_guia"] : null;
            this.per_apellidos_guia = (reader["per_apellidos_guia"] != DBNull.Value) ? (string)reader["per_apellidos_guia"] : null;
            this.per_razon_guia = (reader["per_razon_guia"] != DBNull.Value) ? (string)reader["per_razon_guia"] : null;
           
            this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;
            this.tot_subtot_0 = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
            this.tot_subtotal = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
            this.tot_impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
            this.tot_tseguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
            this.tot_transporte = (reader["tot_transporte"] != DBNull.Value) ? (Decimal?)reader["tot_transporte"] : null;
            this.tot_ice = (reader["tot_ice"] != DBNull.Value) ? (Decimal?)reader["tot_ice"] : null;
            this.tot_tservicio = (reader["tot_tservicio"] != DBNull.Value) ? (Decimal?)reader["tot_tservicio"] : null;
            this.tot_descuento1 = (reader["tot_descuento1"] != DBNull.Value) ? (Decimal?)reader["tot_descuento1"] : null;
        }

        #endregion

        public string GetSQL()
        {
          /*
            string sql = "SELECT  " +
                            "ddo_empresa, " +
                            "ddo_comprobante, " +
                            "ddo_transacc,  " +
                            "ddo_doctran,  " +
                            "ddo_pago,  " +
                            "ddo_comprobante_guia, "+
                            "ddo_codclipro, " +
                            "ddo_fecha_emi,  " +
                            "ddo_fecha_ven,  " +
                            "ddo_monto,  " +
                            "ddo_cancela,  " +
                            "ddo_cancelado,  " +
                            "cenv_socio,  " +
                            "per_ciruc,  " +
                            "per_nombres,  " +
                            "per_apellidos,  " +
                            "per_razon, " +
                            "com_doctran as com_doctran_guia" +
                       " FROM   " +
                           " ddocumento	  " +
                           " LEFT JOIN ccomenv ON ddo_empresa = cenv_empresa AND ddo_comprobante = cenv_comprobante  " +
                           " LEFT JOIN persona ON cenv_empresa = per_empresa AND cenv_socio = per_codigo " +
                           " LEFT JOIN comprobante ON ddo_empresa = com_empresa AND ddo_comprobante_guia = com_codigo";

            */
            string sql = "SELECT   " +
                            "ddo_empresa, " +
                            "ddo_comprobante, " +
                            "ddo_transacc,   " +
                            "ddo_doctran,    " +
                            "ddo_pago,    " +
                            "ddo_comprobante_guia,   " +
                            "ddo_codclipro,   " +
                            "ddo_debcre, "+
                            "ddo_tipo_cambio, " +
                            "ddo_fecha_emi,   " +
                            "ddo_fecha_ven,   " +
                            "ddo_monto,    " +
                            "ddo_monto_ext, " +
                            "ddo_cancela,   " +
                            "ddo_cancela_ext, "+
                            "ddo_cancelado,   " +
                            "ddo_agente, "+
                            "ddo_cuenta, "+
                            "ddo_modulo, " +
                            "a.cenv_socio,    " +
                            "b.per_ciruc,   " +
                            "b.per_nombres,   " +
                            "b.per_apellidos,    " +
                            "b.per_razon,   " +
                            "cdoc_aut_factura, "+
                            "com_doctran as com_doctran_guia,  " +
                            "c.cenv_socio as cenv_socio_guia,    " +
                            "d.per_ciruc as per_ciruc_guia,   " +
                            "d.per_nombres as per_nombres_guia,   " +
                            "d.per_apellidos as per_apellidos_guia,    " +
                            "d.per_razon as per_razon_guia,   " +                            
                            " case when ddo_comprobante_guia is not null then t.tot_total else tf.tot_total end as tot_total, " +
                            " case when ddo_comprobante_guia is not null then t.tot_subtot_0 else tf.tot_subtot_0 end as tot_subtot_0, " +
                            " case when ddo_comprobante_guia is not null then t.tot_subtotal else tf.tot_subtotal end as tot_subtotal, " +
                            " case when ddo_comprobante_guia is not null then t.tot_timpuesto else tf.tot_timpuesto end as tot_timpuesto, " +
                            " case when ddo_comprobante_guia is not null then t.tot_tseguro else tf.tot_tseguro end as tot_tseguro, " +
                            " case when ddo_comprobante_guia is not null then t.tot_transporte else tf.tot_transporte end as tot_transporte, " +
                            " case when ddo_comprobante_guia is not null then t.tot_ice else tf.tot_ice end as tot_ice, " +
                            " case when ddo_comprobante_guia is not null then t.tot_tservicio else tf.tot_tservicio end as tot_tservicio, " +
                            " case when ddo_comprobante_guia is not null then t.tot_descuento1 else tf.tot_descuento1 end as tot_descuento1 " +

                            //"  t.tot_total, " +
                            //"  t.tot_subtot_0, " +
                            //"  t.tot_subtotal,   " +
                            //"  t.tot_timpuesto, " +
                            //"  t.tot_tseguro, " +
                            //"  t.tot_transporte, " +
                            //"  t.tot_ice, " +
                            //"  t.tot_tservicio, " +
                            //"  t.tot_descuento1 " +
                        " FROM     " +
                            " ddocumento	    " +
                            " LEFT JOIN ccomenv a ON ddo_empresa = a.cenv_empresa AND ddo_comprobante = a.cenv_comprobante    " +
                            " LEFT JOIN ccomdoc ON ddo_empresa = cdoc_empresa AND ddo_comprobante = cdoc_comprobante    " +
                            " LEFT JOIN persona b ON a.cenv_empresa = b.per_empresa AND a.cenv_socio = b.per_codigo   " +
                            " LEFT JOIN comprobante ON ddo_empresa = com_empresa AND ddo_comprobante_guia = com_codigo  " +
                            " LEFT JOIN ccomenv c ON com_empresa = c.cenv_empresa AND com_codigo= c.cenv_comprobante    " +
                            //" LEFT JOIN ccomdoc ON com_empresa = cdoc_empresa AND com_codigo= cdoc_comprobante    " +
                            " LEFT JOIN persona d ON c.cenv_empresa = d.per_empresa AND c.cenv_socio = d.per_codigo " +
                            " LEFT JOIN total tf ON ddo_empresa = tf.tot_empresa AND ddo_comprobante  = tf.tot_comprobante " +
                            " LEFT JOIN total t ON ddo_empresa = t.tot_empresa AND ddo_comprobante_guia = t.tot_comprobante ";


            return sql;
        }


        public string GetSQL1()
        {
            string sql = "select  " +
                            "ddo_empresa, " +
                            "ddo_comprobante, " +
                            "ddo_transacc,   " +
                            "ddo_doctran,    " +
                            "ddo_pago,    " +
                            "ddo_comprobante_guia,   " +
                            "ddo_codclipro,   " +
                            "ddo_debcre, " +
                            "ddo_tipo_cambio, " +
                            "ddo_fecha_emi,   " +
                            "ddo_fecha_ven,   " +
                            "ddo_monto,    " +
                            "ddo_monto_ext, " +
                            "ddo_cancela,   " +
                            "ddo_cancela_ext, " +
                            "ddo_cancelado,   " +
                            "ddo_agente, " +
                            "ddo_cuenta, " +
                            "ddo_modulo, " +
                            "null cenv_socio,    " +
                            "null per_ciruc,   " +
                            "null per_nombres,   " +
                            "null per_apellidos,    " +
                            "null per_razon,   " +
                            "null as cdoc_aut_factura, " +
                            "null as com_doctran_guia,  " +
                            "null as cenv_socio_guia,    " +
                            "null as per_ciruc_guia,   " +
                            "null as per_nombres_guia,   " +
                            "null as per_apellidos_guia,    " +
                            "null as per_razon_guia,   " +
                            " null as tot_total, " +
                            " null as tot_subtot_0, " +
                            " null as tot_subtotal, " +
                            " null as tot_timpuesto, " +
                            " null as tot_tseguro, " +
                            " null as tot_transporte, " +
                            " null as tot_ice, " +
                            " null as tot_tservicio, " +
                            " null as tot_descuento1 " +
                            "from " +
                            " ddocumento inner join comprobante on ddo_empresa = com_empresa and ddo_comprobante = com_codigo " +
                            "";


            

            return sql;
        }


        public List<vDdocumento> GetStruc()
        {
            return new List<vDdocumento>();
        }
	
    }
}

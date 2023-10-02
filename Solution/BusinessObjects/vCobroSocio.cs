using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;


namespace BusinessObjects
{
    public class vCobroSocio
    {

        public DateTime? fecha { get; set; }
        public Int32? periodo { get; set; }
        public Int32? mes { get; set; }
        public string doctran { get; set; }
        public string guia { get; set; }
        public Int32? socio { get; set; }
        public string nombreso { get; set; }
        public Int32? chofer { get; set; }
        public string nombrecho { get; set; }
        public string crea_usuario { get; set; }
        public Decimal? subtotal0 { get; set; }
        public Decimal? transporte { get; set; }
        public Decimal? subtotal { get; set; }
        public Decimal? seguro { get; set; }
        public Decimal? impuesto { get; set; }
        public Decimal? total { get; set; }
        public Decimal? ddo_monto { get; set; }
        public Decimal? ddo_cancela { get; set; }

        public Int32? politica { get; set; }
        public string politicaid { get; set; }
        public string politicanombre { get; set; }
        public Decimal? flete { get; set; }
        public Decimal? cobro { get; set; }
        public Decimal? credito { get; set; }

        public Int32? vehiculo { get; set; }
        public string disco { get; set; }
        public string placa { get; set; }


        public vCobroSocio()
        {


        }

        public vCobroSocio(object objeto)
        {

            if (objeto != null)
            {

                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

                object fecha = null;
                object periodo = null;
                object mes = null;
                object doctran = null;
                object guia = null;
                object nombreso = null;                

                object subtotal0 = null;
                object transporte = null;
                object subtotal = null;
                object seguro = null;
                object impuesto = null;
                object total = null;
                object ddo_monto = null;
                object crea_usuario = null;
                object ddo_cancela = null;

                tmp.TryGetValue("fecha", out fecha);
                tmp.TryGetValue("periodo", out periodo);
                tmp.TryGetValue("mes", out mes);
                tmp.TryGetValue("doctran", out doctran);
                tmp.TryGetValue("guia", out guia);
                tmp.TryGetValue("nombreso", out nombreso);
                tmp.TryGetValue("subtotal0", out subtotal0);
                tmp.TryGetValue("transporte", out transporte);
                tmp.TryGetValue("subtotal", out subtotal);
                tmp.TryGetValue("seguro", out seguro);
                tmp.TryGetValue("impuesto", out impuesto);
                tmp.TryGetValue("total", out total);
                tmp.TryGetValue("ddo_monto", out ddo_monto);
                tmp.TryGetValue("crea_usuario", out crea_usuario);
                tmp.TryGetValue("ddo_cancela", out ddo_cancela);


                this.fecha = (DateTime?)Conversiones.GetValueByType(fecha, typeof(DateTime?));
                this.periodo = (Int32)Conversiones.GetValueByType(periodo, typeof(Int32));
                this.mes = (Int32)Conversiones.GetValueByType(mes, typeof(Int32));
                this.doctran = (String)Conversiones.GetValueByType(doctran, typeof(String));
                this.guia = (String)Conversiones.GetValueByType(guia, typeof(String));
                this.crea_usuario = (String)Conversiones.GetValueByType(crea_usuario, typeof(String));
                this.nombreso = (String)Conversiones.GetValueByType(nombreso, typeof(String));
                this.subtotal0 = (Decimal?)Conversiones.GetValueByType(subtotal0, typeof(Decimal?));
                this.transporte = (Decimal?)Conversiones.GetValueByType(transporte, typeof(Decimal?));
                this.subtotal = (Decimal?)Conversiones.GetValueByType(subtotal, typeof(Decimal?));
                this.seguro = (Decimal?)Conversiones.GetValueByType(seguro, typeof(Decimal?));
                this.impuesto = (Decimal?)Conversiones.GetValueByType(impuesto, typeof(Decimal?));
                this.total = (Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?));
                this.ddo_monto = (Decimal?)Conversiones.GetValueByType(ddo_monto, typeof(Decimal?));
                this.ddo_cancela = (Decimal?)Conversiones.GetValueByType(ddo_cancela, typeof(Decimal?));




            }



        }


        public vCobroSocio(IDataReader reader)
        {
            this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.periodo = (reader["com_periodo"] != DBNull.Value) ? (Int32?)reader["com_periodo"] : null;
            this.mes = (reader["com_mes"] != DBNull.Value) ? (Int32?)reader["com_mes"] : null;
            this.doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.guia = (reader["gui"] != DBNull.Value) ? (string)reader["gui"] : null;
            this.socio = (reader["socio"] != DBNull.Value) ? (Int32?)reader["socio"] : null;
            this.nombreso = (reader["nombressocio"] != DBNull.Value) ? (string)reader["nombressocio"] : null;
            this.chofer= (reader["chofer"] != DBNull.Value) ? (Int32?)reader["chofer"] : null;
            this.nombrecho = (reader["nombreschofer"] != DBNull.Value) ? (string)reader["nombreschofer"] : null;
            this.crea_usuario = (reader["crea_usr"] != DBNull.Value) ? (string)reader["crea_usr"] : null;
            this.subtotal0 = (reader["subtotal0"] != DBNull.Value) ? (Decimal?)reader["subtotal0"] : null;
            this.transporte = (reader["transporte"] != DBNull.Value) ? (Decimal?)reader["transporte"] : null;
            this.subtotal = (reader["subtotal"] != DBNull.Value) ? (Decimal?)reader["subtotal"] : null;
            this.seguro = (reader["seguro"] != DBNull.Value) ? (Decimal?)reader["seguro"] : null;
            this.impuesto = (reader["impuesto"] != DBNull.Value) ? (Decimal?)reader["impuesto"] : null;
            this.total = (reader["total"] != DBNull.Value) ? (Decimal?)reader["total"] : null;
            this.ddo_monto = (reader["ddo_monto"] != DBNull.Value) ? (Decimal?)reader["ddo_monto"] : null;
            this.ddo_cancela = (reader["ddo_cancela"] != DBNull.Value) ? (Decimal?)reader["ddo_cancela"] : null;

            this.politica = (reader["politica"] != DBNull.Value) ? (Int32?)reader["politica"] : null;
            this.politicaid = (reader["politicaid"] != DBNull.Value) ? (string)reader["politicaid"] : null;
            this.politicanombre = (reader["politicanombre"] != DBNull.Value) ? (string)reader["politicanombre"] : null;

            this.flete = (reader["flete"] != DBNull.Value) ? (Decimal?)reader["flete"] : null;
            this.cobro = (reader["cobro"] != DBNull.Value) ? (Decimal?)reader["cobro"] : null;
            this.credito = (reader["credito"] != DBNull.Value) ? (Decimal?)reader["credito"] : null;


            this.vehiculo = (reader["vehiculo"] != DBNull.Value) ? (Int32?)reader["vehiculo"] : null;
            this.disco = (reader["disco"] != DBNull.Value) ? (string)reader["disco"] : null;
            this.placa = (reader["placa"] != DBNull.Value) ? (string)reader["placa"] : null;

        }

        public string GetSQLALL()
        {

            string sql = "SELECT     " +
                          " f.com_fecha, f.com_periodo, f.com_mes, f.com_doctran, g.com_doctran as gui ,f.crea_usr,  " +

                          " CASE WHEN cf.cenv_socio IS NOT NULL THEN cf.cenv_socio  ELSE cg.cenv_socio END as socio, " +

                          " CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_nombres || ' ' || pf.per_apellidos  ELSE pg.per_nombres || ' ' || pg.per_apellidos END as nombressocio, " +

                          " CASE WHEN cf.cenv_chofer IS NOT NULL THEN cf.cenv_chofer ELSE cg.cenv_chofer END as chofer, " +

                          " CASE WHEN cf.cenv_chofer IS NOT NULL THEN chf.per_nombres || ' ' || chf.per_apellidos  ELSE chg.per_nombres || ' ' || chg.per_apellidos END as nombreschofer, " +


                          " CASE WHEN cf.cenv_socio IS NOT NULL THEN cf.cenv_vehiculo  ELSE cg.cenv_vehiculo END as vehiculo, " +
                          " CASE WHEN cf.cenv_socio IS NOT NULL THEN cf.cenv_disco ELSE cg.cenv_disco END as disco, " +
                          " CASE WHEN cf.cenv_socio IS NOT NULL THEN cf.cenv_placa ELSE cg.cenv_placa END as placa, " +

                          " CASE WHEN ddo_comprobante_guia IS NULL THEN tf.tot_subtot_0 ELSE tg.tot_subtot_0 END as subtotal0, " +
                          " CASE WHEN ddo_comprobante_guia IS NULL THEN tf.tot_transporte ELSE tg.tot_transporte END as transporte, " +
                          " CASE WHEN ddo_comprobante_guia IS NULL THEN tf.tot_subtotal ELSE tg.tot_subtotal END as subtotal,    " +
                          " CASE WHEN ddo_comprobante_guia IS NULL THEN tf.tot_tseguro ELSE tg.tot_tseguro END as seguro, " +
                          " CASE WHEN ddo_comprobante_guia IS NULL THEN tf.tot_timpuesto ELSE tg.tot_timpuesto END as impuesto," +
                          " CASE WHEN ddo_comprobante_guia IS NULL THEN tf.tot_total ELSE tg.tot_total END as total,  " +
                          " cd.cdoc_politica as politica, pol_nombre as politicanombre, pol_id as politicaid," +

                          " CASE WHEN pol_nombre ilike '%PAGADO%' THEN  CASE WHEN ddo_comprobante_guia IS NULL THEN (tf.tot_subtot_0 + tf.tot_transporte) ELSE (tg.tot_subtot_0 + tg.tot_transporte) END ELSE 0 END as flete,  " +
                          " CASE WHEN pol_nombre ilike '%COBRO%' THEN  CASE WHEN ddo_comprobante_guia IS NULL THEN (tf.tot_subtot_0 + tf.tot_transporte) ELSE (tg.tot_subtot_0 + tg.tot_transporte) END ELSE 0 END as cobro,  " +
                          " CASE WHEN pol_nombre ilike '%CREDITO%' THEN  CASE WHEN ddo_comprobante_guia IS NULL THEN (tf.tot_subtot_0 + tf.tot_transporte) ELSE (tg.tot_subtot_0 + tg.tot_transporte) END ELSE 0 END as credito,  " +

                          " ddo_monto,  ddo_cancela" +
                          " FROM  ddocumento " +
                          " INNER JOIN comprobante f ON ddo_empresa = f.com_empresa AND ddo_comprobante = f.com_codigo " +
                          " INNER JOIN total tf on f.com_empresa = tf.tot_empresa AND f.com_codigo = tf.tot_comprobante " +
                          " LEFT JOIN ccomenv cf ON f.com_empresa = cf.cenv_empresa AND f.com_codigo = cf.cenv_comprobante " +
                          " LEFT JOIN persona pf ON cf.cenv_empresa = pf.per_empresa AND cf.cenv_socio = pf.per_codigo " +
                          " LEFT JOIN persona chf ON cf.cenv_empresa = chf.per_empresa AND cf.cenv_chofer = chf.per_codigo " +
                          " LEFT JOIN ccomdoc cd ON f.com_empresa = cd.cdoc_empresa AND f.com_codigo = cd.cdoc_comprobante  " +//FILTRADO DE PLETES PAGADOS
                          " LEFT JOIN politica ON cd.cdoc_empresa = pol_empresa AND cd.cdoc_politica = pol_codigo  " +//FILTRADO DE PLETES PAGADOS
                          " LEFT JOIN persona cl ON f.com_empresa = cl.per_empresa AND f.com_codclipro  = cl.per_codigo " +
                          " LEFT JOIN comprobante g ON ddo_empresa = g.com_empresa AND ddo_comprobante_guia = g.com_codigo " +
                          " LEFT JOIN total tg on g.com_empresa = tg.tot_empresa AND g.com_codigo = tg.tot_comprobante " +
                          " LEFT JOIN ccomenv cg ON g.com_empresa = cg.cenv_empresa AND g.com_codigo = cg.cenv_comprobante " +
                          " LEFT JOIN persona pg ON cg.cenv_empresa = pg.per_empresa AND cg.cenv_socio = pg.per_codigo  " +
                          " LEFT JOIN persona chg ON cg.cenv_empresa = chg.per_empresa AND cg.cenv_chofer = chg.per_codigo  " +
                          "   ";


            return sql;


        }

        public List<vCobroSocio> GetStruc()
        {
            return new List<vCobroSocio>();
        }


    }
}

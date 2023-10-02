using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;

namespace BusinessObjects
{
   public class vDocumentoVenta
    {

        public Int32? empresa { get; set; }
        public Int64? comprobante { get; set; }
        public Int32? transaccion { get; set; }
        public string doctran { get; set; }
        public Int32? pago { get; set; }
        public Int32? codclipro { get; set; }
        public DateTime? fecha_emi { get; set; }
        public DateTime? fecha_ven { get; set; }
        public Int32? debcre { get; set; }
        public Decimal? monto { get; set; }
        public Decimal? cancela { get; set; }
        public Int32? cuenta { get; set; }
        public Int32? modulo { get; set; }
        public Decimal? total { get; set; }
        public Decimal? subtotal { get; set; }
        public Decimal? subimpuesto { get; set; }
        public Decimal? impuesto { get; set; }
        public Decimal? seguro { get; set; }
        public Decimal? transporte { get; set; }
        public Int32? socio { get; set; }
        public string nombreso { get; set; }
        public string apellidoso { get; set; }
        public string razon { get; set; }




       public vDocumentoVenta(){




       }

       public vDocumentoVenta(object objeto)
       {


           if (objeto != null)
           {
               Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

               object empresa = null;
               object comprobante = null;
               object transaccion = null;
               object doctran = null;
               object pago = null;
               object fecha_emi = null;
               object fecha_ven = null;
               object debcre = null;
               object monto = null;
               object cancela = null;
               object cuenta = null;
               object modulo = null;
               object total = null;
               object subtotal = null;
               object subimpuesto = null;
               object impuesto = null;
               object seguro = null;
               object transporte = null;
               object socio = null;
               object nombreso = null;
               object apellidoso = null;
               object razon = null;




               tmp.TryGetValue("empresa", out empresa);
               tmp.TryGetValue("comprobante", out comprobante);
               tmp.TryGetValue("transaccion", out transaccion);
               tmp.TryGetValue("doctran", out doctran);
               tmp.TryGetValue("pago", out pago);
               tmp.TryGetValue("fecha_emi", out fecha_emi);
               tmp.TryGetValue("fecha_ven", out fecha_ven);
               tmp.TryGetValue("debcre", out debcre);
               tmp.TryGetValue("monto", out monto);
               tmp.TryGetValue("cancela", out cancela);
               tmp.TryGetValue("cuenta", out cuenta);
               tmp.TryGetValue("modulo", out modulo);
               tmp.TryGetValue("total", out total);
               tmp.TryGetValue("subtotal", out subtotal);
               tmp.TryGetValue("subimpuesto", out subimpuesto);
               tmp.TryGetValue("impuesto", out impuesto);
               tmp.TryGetValue("seguro", out seguro);
               tmp.TryGetValue("transporte", out transporte);
               tmp.TryGetValue("socio", out socio);
               tmp.TryGetValue("nombreso", out nombreso);
               tmp.TryGetValue("apellidoso", out apellidoso);
               tmp.TryGetValue("razon", out razon);


               this.empresa = (Int32?)Conversiones.GetValueByType(empresa, typeof(Int64?));
               this.comprobante = (Int64?)Conversiones.GetValueByType(comprobante, typeof(String));
               this.transaccion = (Int32?)Conversiones.GetValueByType(transaccion, typeof(Int32));
               this.doctran = (string)Conversiones.GetValueByType(doctran, typeof(Int32));
               this.pago = (Int32?)Conversiones.GetValueByType(pago, typeof(DateTime?));
               this.fecha_emi = (DateTime?)Conversiones.GetValueByType(fecha_emi, typeof(DateTime?));
               this.fecha_ven = (DateTime?)Conversiones.GetValueByType(fecha_ven, typeof(DateTime?));
               this.debcre = (Int32?)Conversiones.GetValueByType(debcre, typeof(string));
               this.monto = (Decimal?)Conversiones.GetValueByType(monto, typeof(Int32?));
               this.cancela = (Decimal?)Conversiones.GetValueByType(cancela, typeof(Int32?));
               this.cuenta = (Int32?)Conversiones.GetValueByType(cuenta, typeof(Int32?));
               this.modulo = (Int32?)Conversiones.GetValueByType(modulo, typeof(Int32?));
               this.total = (Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?));


               this.subtotal = (Decimal?)Conversiones.GetValueByType(subtotal, typeof(Decimal?));
               this.subimpuesto = (Decimal?)Conversiones.GetValueByType(subimpuesto, typeof(Decimal?));
               this.seguro = (Decimal?)Conversiones.GetValueByType(seguro, typeof(Decimal?));
               this.impuesto = (Decimal?)Conversiones.GetValueByType(impuesto, typeof(Decimal?));
               this.transporte = (Decimal?)Conversiones.GetValueByType(transporte, typeof(Decimal?));
               this.nombreso = (string)Conversiones.GetValueByType(nombreso, typeof(Int32));
               this.apellidoso = (string)Conversiones.GetValueByType(apellidoso, typeof(Int32));
               this.razon = (string)Conversiones.GetValueByType(razon, typeof(Int32));

           }

               



       }
       public vDocumentoVenta(IDataReader reader)
       {



           this.empresa = (reader["ddo_empresa"] != DBNull.Value) ? (Int32?)reader["ddo_empresa"] : null;
           this.comprobante = (reader["ddo_comprobante"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante"] : null;
           this.transaccion = (reader["ddo_transacc"] != DBNull.Value) ? (Int32?)reader["ddo_transacc"] : null;
           this.doctran = (reader["ddo_doctran"] != DBNull.Value) ? (string)reader["ddo_doctran"] : null;
           this.pago = (reader["ddo_pago"] != DBNull.Value) ? (Int32?)reader["ddo_pago"] : null;
           this.fecha_emi = (reader["ddo_fecha_emi"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_emi"] : null;
           this.fecha_ven = (reader["ddo_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_ven"] : null;
           this.debcre = (reader["ddo_debcre"] != DBNull.Value) ? (Int32?)reader["ddo_debcre"] : null;
           this.monto = (reader["ddo_monto"] != DBNull.Value) ? (Decimal?)reader["ddo_monto"] : null;
           this.cancela = (reader["ddo_cancela"] != DBNull.Value) ? (Decimal?)reader["ddo_cancela"] : null;
           this.modulo = (reader["ddo_modulo"] != DBNull.Value) ? (Int32?)reader["ddo_modulo"] : null;
           this.socio = (reader["socio"] != DBNull.Value) ? (Int32?)reader["socio"] : null;
           this.total = (reader["total"] != DBNull.Value) ? (Decimal?)reader["total"] : null;
           this.subtotal = (reader["subtotal"] != DBNull.Value) ? (Decimal?)reader["subtotal"] : null;
           this.subimpuesto = (reader["subtotal0"] != DBNull.Value) ? (Decimal?)reader["subtotal0"] : null;
           this.seguro = (reader["seguro"] != DBNull.Value) ? (Decimal?)reader["seguro"] : null;
           this.impuesto = (reader["impuesto"] != DBNull.Value) ? (Decimal?)reader["impuesto"] : null;
           this.transporte = (reader["transporte"] != DBNull.Value) ? (Decimal?)reader["transporte"] : null;
           this.nombreso = (reader["nombressocio"] != DBNull.Value) ? (string)reader["nombressocio"] : null;
           this.apellidoso = (reader["apellidossocio"] != DBNull.Value) ? (string)reader["apellidossocio"] : null;
           this.razon = (reader["razoncliente"] != DBNull.Value) ? (string)reader["razoncliente"] : null;
        
       }
       public string GetSQLALL()
       {



           string sql = "SELECT" +
                         "    ddo_empresa, ddo_comprobante,  ddo_transacc, ddo_doctran, ddo_pago, ddo_comprobante_guia, ddo_codclipro, ddo_fecha_emi, ddo_fecha_ven, ddo_debcre, ddo_monto, " +
                            " ddo_monto_ext, ddo_cancela,  ddo_cancela_ext,      ddo_cancelado,  ddo_cuenta,  ddo_modulo,  " +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_codigo ELSE pg.per_codigo END as socio, " +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_id ELSE pg.per_id END as idsocio," +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_nombres ELSE pg.per_nombres END as nombressocio," +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_apellidos ELSE pg.per_apellidos END as apellidossocio," +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN tf.tot_subtotal ELSE tg.tot_subtotal END as subtotal, " +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN tf.tot_subtot_0 ELSE tg.tot_subtot_0 END as subtotal0," +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN tf.tot_tseguro ELSE tg.tot_tseguro END as seguro, " +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN tf.tot_transporte ELSE tg.tot_transporte END as transporte, " +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN tf.tot_timpuesto ELSE tg.tot_timpuesto END as impuesto, " +
                            "CASE WHEN cf.cenv_socio IS NOT NULL THEN tf.tot_total ELSE tg.tot_total END as total,    " +
                           " g.com_doctran doctran_guia,  cl.per_ciruc ruccliente,  cl.per_razon razoncliente " +
                           " FROM " +
                            "  ddocumento " +
                         " INNER JOIN comprobante f ON ddo_empresa = f.com_empresa AND ddo_comprobante = f.com_codigo " +
                         " INNER JOIN total tf on f.com_empresa = tf.tot_empresa AND f.com_codigo = tf.tot_comprobante " +
                         " LEFT JOIN ccomenv cf ON f.com_empresa = cf.cenv_empresa AND f.com_codigo = cf.cenv_comprobante " +
                         " LEFT JOIN persona pf ON cf.cenv_empresa = pf.per_empresa AND cf.cenv_socio = pf.per_codigo " +
                         " LEFT JOIN persona cl ON f.com_empresa = cl.per_empresa AND f.com_codclipro  = cl.per_codigo " +
                         " LEFT JOIN comprobante g ON ddo_empresa = g.com_empresa AND ddo_comprobante_guia = g.com_codigo " +
                         " LEFT JOIN total tg on g.com_empresa = tg.tot_empresa AND g.com_codigo = tg.tot_comprobante " +
                         " LEFT JOIN ccomenv cg ON g.com_empresa = cg.cenv_empresa AND g.com_codigo = cg.cenv_comprobante " +
                           " LEFT JOIN persona pg ON cg.cenv_empresa = pg.per_empresa AND cg.cenv_socio = pg.per_codigo     " +
                           "   ";
                           


               return sql;






       }


       public List<vDocumentoVenta> GetStruc()
       {
           return new List<vDocumentoVenta>();
       }

    }
}

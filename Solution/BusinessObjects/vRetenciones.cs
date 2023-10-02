using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;


namespace BusinessObjects
{
   public class vRetenciones
    {
       public DateTime? fecha { get; set; }
       public DateTime? fecharet { get; set; }
       public string doctran { get; set; }
       public string agente { get; set; }
       public string ciruc { get; set; }
       public Decimal? impuesto { get; set; }
       public Decimal? total { get; set; }
       public Decimal? subtotal { get; set; }
       public Decimal? subimpuesto { get; set; }
       public Decimal? desc { get; set; }
       public Decimal? desc1 { get; set; }
       public string persona { get; set; }
       public string impnombre { get; set; }

       public vRetenciones()
       {

       }

       public vRetenciones(object objeto)
       {
           if (objeto != null)
           {
               Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

               object fecha = null;
               object doctran = null;
               object agente = null;
               object ciruc = null;
               object impuesto = null;
               object total = null;
               object subtotal = null;
               object subimpuesto = null;
               object desc = null;
               object desc1 = null;
               object fecharet = null;
               object persona = null;
               object impnombre = null;

               tmp.TryGetValue("fecha", out fecha);
               tmp.TryGetValue("doctran", out doctran);
               tmp.TryGetValue("agente", out agente);
               tmp.TryGetValue("ciruc", out ciruc);
               tmp.TryGetValue("impuesto", out impuesto);
               tmp.TryGetValue("subtotal", out subtotal);
               tmp.TryGetValue("total", out total);
               tmp.TryGetValue("subimpuesto", out subimpuesto);
               tmp.TryGetValue("desc", out desc);
               tmp.TryGetValue("desc1", out desc1);
               tmp.TryGetValue("fecharet", out fecharet);
               tmp.TryGetValue("persona", out persona);
               tmp.TryGetValue("impnombre", out impnombre);



               this.fecha = (DateTime?)Conversiones.GetValueByType(fecha, typeof(DateTime?));
               this.fecharet = (DateTime?)Conversiones.GetValueByType(fecha, typeof(DateTime?));
               this.doctran = (String)Conversiones.GetValueByType(doctran, typeof(String));
               this.agente = (String)Conversiones.GetValueByType(doctran, typeof(String));
               this.ciruc = (string)Conversiones.GetValueByType(ciruc, typeof(string));
               this.impuesto = (Decimal?)Conversiones.GetValueByType(impuesto, typeof(Decimal?));
               this.subtotal = (Decimal?)Conversiones.GetValueByType(subtotal, typeof(Decimal?));
               this.total = (Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?));
               this.subimpuesto = (Decimal?)Conversiones.GetValueByType(subimpuesto, typeof(Decimal?));
               this.desc = (Decimal?)Conversiones.GetValueByType(desc, typeof(Decimal?));
               this.desc1 = (Decimal?)Conversiones.GetValueByType(desc1, typeof(Decimal?));
               this.persona = (String)Conversiones.GetValueByType(doctran, typeof(String));
               this.impnombre = (String)Conversiones.GetValueByType(doctran, typeof(String));
           }


       }


       public vRetenciones(IDataReader reader)
       {

           this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
           this.fecharet = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
           this.doctran = (reader["obligaciones"] != DBNull.Value) ? (string)reader["obligaciones"] : null;
           this.agente = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
           this.persona = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
           this.ciruc = reader["per_ciruc"].ToString();
           this.impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
           this.subtotal = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
           this.subimpuesto = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
           this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;
           this.desc = (reader["tot_desc1_0"] != DBNull.Value) ? (Decimal?)reader["tot_desc1_0"] : null;
           this.desc1 = (reader["tot_desc2_0"] != DBNull.Value) ? (Decimal?)reader["tot_desc2_0"] : null;
           this.impnombre = (reader["imp_nombre"] != DBNull.Value) ? (string)reader["imp_nombre"] : null;
       }


       public string GetSQLALL()
       {

           string sql = "SELECT "+
                         " dre.crea_fecha,co.com_fecha,per_nombres, t.tot_subtot_0,p.per_razon, p.per_ciruc,t.tot_timpuesto,t.tot_total,t.tot_subtotal,t.tot_desc1_0,t.tot_desc2_0,imp.imp_nombre, (select c.com_doctran  from comprobante c where c.com_codigo =cco.cdoc_factura) as obligaciones " +
                         " FROM comprobante co "+
                         " inner join ccomdoc cco on cco.cdoc_comprobante = co.com_codigo " +
                         " inner join dretencion dre on dre.drt_comprobante = co.com_codigo " +
                         " inner join impuesto imp on   dre.drt_impuesto = imp.imp_codigo "+
                         " inner join persona p on co.com_codclipro = p.per_codigo " + 
                         " inner join total t on t.tot_comprobante = cco.cdoc_factura "+
                         " GROUP BY dre.crea_fecha,co.com_fecha, p.per_razon, p.per_ciruc,t.tot_timpuesto,t.tot_total,t.tot_subtotal,t.tot_desc1_0,t.tot_desc2_0,imp.imp_nombre, (select c.com_doctran  from comprobante c where c.com_codigo =cco.cdoc_factura) ,imp.imp_codigo,co.com_tipodoc,per_nombres,t.tot_subtot_0  " +
                          " %whereclause%  %orderby% ";

                         
                        ;



           return sql;
       }
       public List<vRetenciones> GetStruc()
       {
           return new List<vRetenciones>();
       }

    }

  

}

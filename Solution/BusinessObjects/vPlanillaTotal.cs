using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace BusinessObjects
{
 public class vPlanillaTotal
    {

     public string producto { get; set; }
     public decimal? cantidad { get; set; }
     public decimal? total { get; set; }
     public string pro_id{ get; set; }
     public string ruta { get; set; }
     public int? pro_unidad { get; set; }
     public decimal? precio { get; set; }
     public decimal? descuento { get; set; }
     public int? pro_codigo { get; set; }
     public Int64? numero { get; set; }
     public int? per_codigo { get; set; }
     public string per_nombres { get; set; }
     public string per_apellidos { get; set; }
     public int? veh_codigo { get; set; }
     public string veh_placa { get; set; }
     public vPlanillaTotal()
     {

     }

     public vPlanillaTotal(IDataReader reader)
     {
         this.producto = (reader["producto"] != DBNull.Value) ? (string)reader["producto"] : null;
         this.cantidad = (reader["cantidad"] != DBNull.Value) ? (decimal?)reader["cantidad"] : null;
         this.total = (reader["total"] != DBNull.Value) ? (decimal?)reader["total"] : null;
         this.pro_id = (reader["pro_id"] != DBNull.Value) ? (string)reader["pro_id"] : null;
         this.ruta = (reader["rut_nombre"] != DBNull.Value) ? (string)reader["rut_nombre"] : null;
         this.pro_unidad = (reader["pro_unidad"] != DBNull.Value) ? (int?)reader["pro_unidad"] : null;
         this.precio = (reader["precio"] != DBNull.Value) ? (decimal?)reader["precio"] : null;
         this.descuento = (reader["descuento"] != DBNull.Value) ? (decimal?)reader["descuento"] : null;
         this.pro_codigo = (reader["pro_codigo"] != DBNull.Value) ? (int?)reader["pro_codigo"] : null;
         this.numero = (reader["numero"] != DBNull.Value) ? (Int64?)reader["numero"] : null;
         this.per_codigo = (reader["per_codigo"] != DBNull.Value) ? (int?)reader["per_codigo"] : null;
         this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
         this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
         this.veh_codigo = (reader["veh_codigo"] != DBNull.Value) ? (int?)reader["veh_codigo"] : null;
         this.veh_placa = (reader["veh_placa"] != DBNull.Value) ? (string)reader["veh_placa"] : null;
     }


     public string getSQL()
     {
         string sql = " select  p.pro_nombre as producto,sum(d.ddoc_cantidad) as cantidad,sum(d.ddoc_total) as  total, " +
                      " p.pro_id,r.rut_nombre,p.pro_unidad,dlpr_precio as precio,sum(d.ddoc_dscitem) as descuento,p.pro_codigo,null as per_codigo,null as per_nombres,null as per_apellidos,null as veh_codigo, null as veh_placa,null as numero " +
                       " from comprobante c " +
                       " inner join planillacli pl on c.com_empresa = pl.plc_empresa and c.com_codigo = pl.plc_comprobante_pla  " +
                       " inner join comprobante de on pl.plc_empresa = de.com_empresa and  de.com_codigo= pl.plc_comprobante " +
                       " left join total t on de.com_empresa = t.tot_empresa and de.com_codigo = t.tot_comprobante " +
                       " left join dcomdoc d on d.ddoc_empresa = de.com_empresa and de.com_codigo = d.ddoc_comprobante " +
                       " left join producto p on d.ddoc_empresa = p. pro_empresa and p.pro_codigo = d.ddoc_producto " +
                       " left join ccomenv cen on cen.cenv_empresa = de.com_empresa and de.com_codigo = cen.cenv_comprobante " +
                       " left join ruta r on cen.cenv_empresa = r.rut_empresa  and r.rut_codigo = cen.cenv_ruta "+
                       " left join dlistaprecio dl on d.ddoc_empresa =dl.dlpr_empresa and p.pro_codigo =dl.dlpr_producto   " +
                       " and cen.cenv_empresa = dl.dlpr_empresa and cen.cenv_ruta = dl.dlpr_ruta   ";
         return sql;


     }
     public string getSQLS()
     {
         string sql = " select  p.per_codigo,p.per_nombres,p.per_apellidos,ve.veh_codigo,ve.veh_placa, sum(d.ddoc_total) as total,null as producto,null as cantidad, null as pro_id, null as rut_nombre, null as  pro_unidad, null as precio, null as descuento, null as  pro_codigo,null as numero " +
                      " from comprobante c  " +
                      " inner join planillacli pl on c.com_empresa = pl.plc_empresa and c.com_codigo = pl.plc_comprobante_pla " +
                      " inner join comprobante de on pl.plc_empresa = de.com_empresa and  de.com_codigo= pl.plc_comprobante " +
                      " left join total t on de.com_empresa = t.tot_empresa and de.com_codigo = t.tot_comprobante " +
                      " left join dcomdoc d on d.ddoc_empresa = de.com_empresa and de.com_codigo = d.ddoc_comprobante " +
                      " left join ccomenv cen on cen.cenv_empresa = de.com_empresa and de.com_codigo = cen.cenv_comprobante " +
                      " left join persona p on cen.cenv_empresa = p.per_empresa and cen.cenv_socio = p.per_codigo " +
                      " left join vehiculo ve on cen.cenv_empresa= ve.veh_empresa and cen.cenv_vehiculo = ve.veh_codigo ";
                     

         return sql;



     }
     public string getSQLNV()
     {

         string sql = " select  sum(cenv_guias) as numero,null as pro_id, null as rut_nombre, null as  cantidad, null as precio, null as pro_unidad, null as  pro_codigo,null as per_codigo,null as per_nombres,null as per_apellidos,null as veh_codigo, null as veh_placa,null as total,null as producto,null as descuento    " +
                      " from comprobante c "+
                      " inner join planillacli pl on c.com_empresa = pl.plc_empresa and c.com_codigo = pl.plc_comprobante_pla   "+
                      " inner join comprobante de on pl.plc_empresa = de.com_empresa and  de.com_codigo= pl.plc_comprobante " +
                      " left join ccomenv cen on cen.cenv_empresa = de.com_empresa and de.com_codigo = cen.cenv_comprobante  ";
         return sql;
     }


     public string getSQLN()
     {

         string sql = " select  count(de.com_doctran) as numero, null as pro_id, null as rut_nombre, null as  cantidad, null as precio, null as pro_unidad, null as  pro_codigo,null as per_codigo,null as per_nombres,null as per_apellidos,null as veh_codigo, null as veh_placa,null as total,null as producto,null as descuento " +
                      " from comprobante c " +
                      " inner join planillacli pl on c.com_empresa = pl.plc_empresa and c.com_codigo = pl.plc_comprobante_pla " +
                      " inner join comprobante de on pl.plc_empresa = de.com_empresa and  de.com_codigo= pl.plc_comprobante   "+
                      " left join total t on de.com_empresa = t.tot_empresa and de.com_codigo = t.tot_comprobante " ;
                       
         return sql;
     }


     public List<vPlanillaTotal> GetStruc()
     {
         return new List<vPlanillaTotal>();


     }

    }
}

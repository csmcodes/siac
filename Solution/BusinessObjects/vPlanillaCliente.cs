using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vPlanillaCliente
    {
        public int? cabecera_empresa { get; set; }
        public Int64? cabecera_codigo { get; set; }
        public int? cabecera_transacc { get; set; }
        public string cabecera_doctran { get; set; }
        public int? cabecera_codclipro { get; set; }
        public DateTime? cabecera_fecha { get; set; }
        public int? cabecera_cenv_socio { get; set; }
        public string cabecera_per_ciruc { get; set; }
        public string cabecera_nombres { get; set; }
        public string cabecera_apellidos { get; set; }
        public string cabecera_razon { get; set; }
        public string cabecera_representante { get; set; }
        public decimal? cabecera_total { get; set; }

        public string nombrecliente { get; set; }
        public string apellidocliente { get; set; }
        
        public int? detalle_empresa { get; set; }
        public Int64? detalle_codigo { get; set; }
        public int? detalle_transacc { get; set; }
        public string detalle_doctran { get; set; }
        public int? detalle_codclipro { get; set; }
        public DateTime? detalle_fecha { get; set; }
        public int? detalle_cenv_socio { get; set; }
        public string detalle_per_ciruc { get; set; }        
        public string detalle_razon { get; set; }
        
        public decimal? detalle_subtot_0 { get; set; }
        public decimal? detalle_subtotal { get; set; }        
        public decimal? detalle_desc{ get; set; }
        public decimal? detalle_iva { get; set; }
        public decimal? detalle_seguro{ get; set; }
        public decimal? detalle_transporte{ get; set; }
        public decimal? detalle_total { get; set; }
        public decimal? detalle_declarado { get; set; }
        public decimal? detalle_porcseguro { get; set; }

        public string detalle_nombres { get; set; }
        public string detalle_apellidos { get; set; }
        public int? detalle_cenv_chofer { get; set; }
        public string detalle_nombrescho { get; set; }
        public string detalle_apellidoscho { get; set; }


        public string detalle_origen { get; set; }
        public string detalle_destino { get; set; }

        public decimal? detalle_bultos{ get; set; }
        public string  detalle_items { get; set; }


        public Int64? factura { get; set; }
        //Columnas reporte auditoria
        public decimal? cancelado { get; set; }
        public decimal? saldo { get; set; }
        public decimal? cancela1 { get; set; }
        public decimal? cancela2 { get; set; }
        public decimal? cancela3 { get; set; }
        public decimal? cancela4 { get; set; }
        public decimal? cancela5 { get; set; }
        public decimal? cancela6 { get; set; }
        public decimal? cancela7 { get; set; }
        public decimal? cancela8 { get; set; }
        public decimal? cancela9 { get; set; }
        public decimal? cancela10 { get; set; }





        #region Constructors


        public vPlanillaCliente()
        {

        }



        public vPlanillaCliente(IDataReader reader)
        {
            this.cabecera_empresa = (reader["cabecera_empresa"] != DBNull.Value) ? (int?)reader["cabecera_empresa"] : null;
            this.cabecera_codigo = (reader["cabecera_codigo"] != DBNull.Value) ? (Int64?)reader["cabecera_codigo"] : null;
            this.cabecera_transacc = (reader["cabecera_transacc"] != DBNull.Value) ? (int?)reader["cabecera_transacc"] : null;
            this.cabecera_doctran = (reader["cabecera_doctran"] != DBNull.Value) ? (string)reader["cabecera_doctran"] : null;
            this.cabecera_codclipro = (reader["cabecera_codclipro"] != DBNull.Value) ? (int?)reader["cabecera_codclipro"] : null;
            this.cabecera_fecha = (reader["cabecera_fecha"] != DBNull.Value) ? (DateTime?)reader["cabecera_fecha"] : null;
            this.cabecera_cenv_socio = (reader["cabecera_cenv_socio"] != DBNull.Value) ? (int?)reader["cabecera_cenv_socio"] : null;
            this.cabecera_per_ciruc = (reader["cabecera_per_ciruc"] != DBNull.Value) ? (string)reader["cabecera_per_ciruc"] : null;
            this.cabecera_nombres = (reader["cabecera_nombres"] != DBNull.Value) ? (string)reader["cabecera_nombres"] : null;
            this.cabecera_apellidos = (reader["cabecera_apellidos"] != DBNull.Value) ? (string)reader["cabecera_apellidos"] : null;
            this.cabecera_razon = (reader["cabecera_razon"] != DBNull.Value) ? (string)reader["cabecera_razon"] : null;
            this.cabecera_representante = (reader["cabecera_representante"] != DBNull.Value) ? (string)reader["cabecera_representante"] : null;
            
            this.cabecera_total = (reader["cabecera_total"] != DBNull.Value) ? (decimal?)reader["cabecera_total"] : null;

            this.nombrecliente = (reader["nombrecliente"] != DBNull.Value) ? (string)reader["nombrecliente"] : null;
            this.apellidocliente = (reader["apellidocliente"] != DBNull.Value) ? (string)reader["apellidocliente"] : null;

            this.detalle_empresa = (reader["detalle_empresa"] != DBNull.Value) ? (int?)reader["detalle_empresa"] : null;
            this.detalle_codigo = (reader["detalle_codigo"] != DBNull.Value) ? (Int64?)reader["detalle_codigo"] : null;
            this.detalle_transacc = (reader["detalle_transacc"] != DBNull.Value) ? (int?)reader["detalle_transacc"] : null;
            this.detalle_doctran = (reader["detalle_doctran"] != DBNull.Value) ? (string)reader["detalle_doctran"] : null;
            this.detalle_codclipro = (reader["detalle_codclipro"] != DBNull.Value) ? (int?)reader["detalle_codclipro"] : null;
            this.detalle_fecha = (reader["detalle_fecha"] != DBNull.Value) ? (DateTime?)reader["detalle_fecha"] : null;
            this.detalle_cenv_socio = (reader["detalle_cenv_socio"] != DBNull.Value) ? (int?)reader["detalle_cenv_socio"] : null;
            this.detalle_per_ciruc = (reader["detalle_per_ciruc"] != DBNull.Value) ? (string)reader["detalle_per_ciruc"] : null;
            this.detalle_nombres = (reader["detalle_nombres"] != DBNull.Value) ? (string)reader["detalle_nombres"] : null;
            this.detalle_apellidos = (reader["detalle_apellidos"] != DBNull.Value) ? (string)reader["detalle_apellidos"] : null;
            this.detalle_razon = (reader["detalle_razon"] != DBNull.Value) ? (string)reader["detalle_razon"] : null;
            this.detalle_subtot_0 = (reader["detalle_subtot_0"] != DBNull.Value) ? (decimal?)reader["detalle_subtot_0"] : null;
            this.detalle_subtotal = (reader["detalle_subtotal"] != DBNull.Value) ? (decimal?)reader["detalle_subtotal"] : null;
            this.detalle_desc= (reader["detalle_desc"] != DBNull.Value) ? (decimal?)reader["detalle_desc"] : null;
            this.detalle_iva = (reader["detalle_iva"] != DBNull.Value) ? (decimal?)reader["detalle_iva"] : null;
            this.detalle_seguro = (reader["detalle_seguro"] != DBNull.Value) ? (decimal?)reader["detalle_seguro"] : null;
            this.detalle_transporte = (reader["detalle_transporte"] != DBNull.Value) ? (decimal?)reader["detalle_transporte"] : null;
            this.detalle_declarado= (reader["detalle_declarado"] != DBNull.Value) ? (decimal?)reader["detalle_declarado"] : null;
            this.detalle_porcseguro = (reader["detalle_porcseguro"] != DBNull.Value) ? (decimal?)reader["detalle_porcseguro"] : null;

            this.detalle_origen = (reader["detalle_origen"] != DBNull.Value) ? (string)reader["detalle_origen"] : null;
            this.detalle_destino = (reader["detalle_destino"] != DBNull.Value) ? (string)reader["detalle_destino"] : null;

            this.detalle_total = (reader["detalle_total"] != DBNull.Value) ? (decimal?)reader["detalle_total"] : null;

            this.detalle_bultos= (reader["detalle_bultos"] != DBNull.Value) ? (decimal?)reader["detalle_bultos"] : null;
            this.detalle_items = (reader["detalle_items"] != DBNull.Value) ? (string)reader["detalle_items"] : null;

            this.detalle_cenv_chofer = (reader["detalle_cenv_chofer"] != DBNull.Value) ? (int?)reader["detalle_cenv_chofer"] : null;
            this.detalle_nombrescho = (reader["detalle_nombrescho"] != DBNull.Value) ? (string)reader["detalle_nombrescho"] : null;
            this.detalle_apellidoscho = (reader["detalle_apellidoscho"] != DBNull.Value) ? (string)reader["detalle_apellidoscho"] : null;

            this.factura = (reader["factura"] != DBNull.Value) ? (Int64?)reader["factura"] : null;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "SELECT " +
       "cabecera.com_empresa as cabecera_empresa,    " +
       "cabecera.com_codigo as cabecera_codigo,  " +
       "cabecera.com_transacc as cabecera_transacc, " +
       "cabecera.com_doctran as cabecera_doctran,       " +
       "cabecera.com_codclipro as cabecera_codclipro,     " +
       "cabecera.com_fecha as cabecera_fecha,              " +
       "cabeceraccomenv.cenv_socio as cabecera_cenv_socio,   " +
       "cabecerapersona.per_ciruc as cabecera_per_ciruc,   " +
       "cabecerapersona.per_nombres as cabecera_nombres,   " +
       "cabecerapersona.per_apellidos  as cabecera_apellidos,   " +
       "cabecerapersona.per_razon as cabecera_razon,  " +
       "cabecerapersona.per_representantelegal as cabecera_representante, " +
       "cabeceratotal.tot_total as cabecera_total,     " +
       "pco_comprobante_fac as factura, "+

       "detalleccomenv.cenv_nombres_des nombrecliente, " +
       "detalleccomenv.cenv_apellidos_des apellidocliente, " +
      


       "detalle.com_empresa as detalle_empresa,   " +
       "detalle.com_codigo as detalle_codigo, " +
       "detalle.com_transacc as detalle_transacc ,   " +
       "detalle.com_doctran as detalle_doctran,           " +
       "detalle.com_codclipro as detalle_codclipro,       " +
       "detalle.com_fecha as detalle_fecha," +
       "detalleccomenv.cenv_socio as detalle_cenv_socio,  " +
       "detallepersona.per_ciruc as detalle_per_ciruc,   " +
       "detallepersona.per_nombres as detalle_nombres,    " +
       "detallepersona.per_apellidos  as detalle_apellidos,   " +
       "detallepersona.per_razon as detalle_razon,   " +

       "detalleccomenv.cenv_chofer as detalle_cenv_chofer,  " +
       "detallechofer.per_nombres as detalle_nombrescho,    " +
       "detallechofer.per_apellidos  as detalle_apellidoscho,   " +


       "detalletotal.tot_subtot_0 as detalle_subtot_0,     " +
       "detalletotal.tot_subtotal as detalle_subtotal,     " +
       "detalletotal.tot_desc1_0 as detalle_desc,     " +
       "detalletotal.tot_timpuesto as detalle_iva,     " +
       "detalletotal.tot_tseguro as detalle_seguro,     " +
       "detalletotal.tot_transporte as detalle_transporte,     " +
       "detalletotal.tot_vseguro as detalle_declarado,     " +
       "detalletotal.tot_porc_seguro as detalle_porcseguro,     " +

       "null as detalle_bultos," +
       "null as detalle_items," +
       "detalleruta.rut_origen as detalle_origen,     " +
       "detalleruta.rut_destino as detalle_destino,     " +
       "detalletotal.tot_total as detalle_total     " +


"FROM " +
 "planillacli " +
      "INNER JOIN comprobante cabecera ON cabecera.com_empresa= plc_empresa AND cabecera.com_codigo = plc_comprobante_pla  " +
      "INNER JOIN comprobante detalle ON plc_comprobante = detalle.com_codigo AND plc_empresa = detalle.com_empresa  " +
      "INNER JOIN total cabeceratotal ON plc_empresa = tot_empresa AND plc_comprobante_pla = tot_comprobante    " +
      "left JOIN ccomenv cabeceraccomenv ON plc_comprobante_pla = cenv_comprobante  AND plc_empresa = cenv_empresa  " +
      "LEFT JOIN persona cabecerapersona ON cabecera.com_empresa = per_empresa AND cabecera.com_codclipro = per_codigo   " +

      "INNER JOIN total detalletotal  ON plc_empresa = detalletotal.tot_empresa AND plc_comprobante = detalletotal.tot_comprobante    " +
      "left JOIN ccomenv detalleccomenv ON plc_comprobante = detalleccomenv.cenv_comprobante AND plc_empresa = detalleccomenv.cenv_empresa     " +
      "LEFT JOIN persona detallepersona ON detalleccomenv.cenv_empresa = detallepersona.per_empresa AND detalleccomenv.cenv_socio = detallepersona.per_codigo    " +
      "LEFT JOIN persona detallechofer ON detalleccomenv.cenv_empresa = detallechofer.per_empresa AND detalleccomenv.cenv_chofer = detallechofer.per_codigo    " +
      "LEFT JOIN ruta detalleruta ON detalleccomenv.cenv_ruta = detalleruta.rut_codigo and detalleccomenv.cenv_empresa=detalleruta.rut_empresa "+
      "LEFT JOIN planillacomprobante on pco_comprobante_pla = plc_comprobante_pla";


            return sql;
        }

        public string GetSQLDet()
        {
            string sql = "SELECT " +
       "cabecera.com_empresa as cabecera_empresa,    " +
       "cabecera.com_codigo as cabecera_codigo,  " +
       "cabecera.com_transacc as cabecera_transacc, " +
       "cabecera.com_doctran as cabecera_doctran,       " +
       "cabecera.com_codclipro as cabecera_codclipro,     " +
       "cabecera.com_fecha as cabecera_fecha,              " +
       "cabeceraccomenv.cenv_socio as cabecera_cenv_socio,   " +
       "cabecerapersona.per_ciruc as cabecera_per_ciruc,   " +
       "cabecerapersona.per_nombres as cabecera_nombres,   " +
       "cabecerapersona.per_apellidos  as cabecera_apellidos,   " +
       "cabecerapersona.per_razon as cabecera_razon,  " +
       "cabecerapersona.per_representantelegal as cabecera_representante, " +
       "cabeceratotal.tot_total as cabecera_total,     " +
       "pco_comprobante_fac as factura, "+
       "detalle.com_empresa as detalle_empresa,   " +
       "detalle.com_codigo as detalle_codigo, " +
       "detalle.com_transacc as detalle_transacc ,   " +
       "detalle.com_doctran as detalle_doctran,           " +
       "detalle.com_codclipro as detalle_codclipro,       " +
       "detalle.com_fecha as detalle_fecha," +
       "detalleccomenv.cenv_nombres_des nombrecliente, " +
       "detalleccomenv.cenv_apellidos_des apellidocliente, " +
       "detalleccomenv.cenv_socio as detalle_cenv_socio,  " +
       "detallepersona.per_ciruc as detalle_per_ciruc,   " +
       "detallepersona.per_nombres as detalle_nombres,    " +
       "detallepersona.per_apellidos  as detalle_apellidos,   " +
       "detallepersona.per_razon as detalle_razon,   " +

       "detalleccomenv.cenv_chofer as detalle_cenv_chofer,  " +
       "detallechofer.per_nombres as detalle_nombrescho,    " +
       "detallechofer.per_apellidos  as detalle_apellidoscho,   " +

       "detalletotal.tot_subtot_0 as detalle_subtot_0,     " +
       "detalletotal.tot_subtotal as detalle_subtotal,     " +
       "detalletotal.tot_desc1_0 as detalle_desc,     " +
       "detalletotal.tot_timpuesto as detalle_iva,     " +
       "detalletotal.tot_tseguro as detalle_seguro,     " +
       "detalletotal.tot_transporte as detalle_transporte,     " +
       "detalletotal.tot_vseguro as detalle_declarado,     " +
       "detalletotal.tot_porc_seguro as detalle_porcseguro,     " +

       "dcomdoc.ddoc_cantidad as detalle_bultos," +
       "dcomdoc.ddoc_observaciones as detalle_items, " +
        "detalleruta.rut_origen as detalle_origen,     " +
       "detalleruta.rut_destino as detalle_destino,     " +
       "detalletotal.tot_total as detalle_total     " +

"FROM " +
 "planillacli " +
      "INNER JOIN comprobante cabecera ON cabecera.com_empresa= plc_empresa AND cabecera.com_codigo = plc_comprobante_pla  " +
      "INNER JOIN comprobante detalle ON plc_comprobante = detalle.com_codigo AND plc_empresa = detalle.com_empresa  " +
      "INNER JOIN total cabeceratotal ON plc_empresa = tot_empresa AND plc_comprobante_pla = tot_comprobante    " +
      "left JOIN ccomenv cabeceraccomenv ON plc_comprobante_pla = cenv_comprobante  AND plc_empresa = cenv_empresa  " +
      "LEFT JOIN persona cabecerapersona ON cabecera.com_empresa = per_empresa AND cabecera.com_codclipro = per_codigo   " +      
      "INNER JOIN total detalletotal  ON plc_empresa = detalletotal.tot_empresa AND plc_comprobante = detalletotal.tot_comprobante    " +
      "left JOIN ccomenv detalleccomenv ON plc_comprobante = detalleccomenv.cenv_comprobante AND plc_empresa = detalleccomenv.cenv_empresa     " +
      "LEFT JOIN persona detallepersona ON detalleccomenv.cenv_empresa = detallepersona.per_empresa AND detalleccomenv.cenv_socio = detallepersona.per_codigo " +
      "LEFT JOIN persona detallechofer ON detalleccomenv.cenv_empresa = detallechofer.per_empresa AND detalleccomenv.cenv_chofer = detallechofer.per_codigo    " +
      "LEFT JOIN ruta detalleruta ON detalleccomenv.cenv_ruta = detalleruta.rut_codigo and detalleccomenv.cenv_empresa=detalleruta.rut_empresa  " +
            "left join ccomdoc on ccomdoc.cdoc_comprobante = plc_comprobante and ccomdoc.cdoc_empresa = plc_empresa " +
            "left join dcomdoc  on dcomdoc.ddoc_comprobante = plc_comprobante and dcomdoc.ddoc_empresa = plc_empresa " +
            "LEFT JOIN planillacomprobante on pco_comprobante_pla = plc_comprobante_pla" +
            "";
            return sql;
        }


        public List<vPlanillaCliente> GetStruc()
        {
            return new List<vPlanillaCliente>();
        }


    }
}

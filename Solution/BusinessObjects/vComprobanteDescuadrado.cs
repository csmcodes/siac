using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BusinessObjects
{
    public class vComprobanteDescuadrado
    {
        public Int64? codigo { get; set; }
        public Int32? empresa { get; set; }
        public string doctran { get; set; }
        public Int32? tipodoc { get; set; }
        public DateTime? fecha { get; set; }
        public Decimal? debito { get; set; }
        public Decimal? credito { get; set; }
        public Decimal? total{ get; set; }

        public Decimal? subtotal { get; set; }
        public Decimal? transporte { get; set; }
        public Decimal? seguro { get; set; }
        public Decimal? impuesto { get; set; }
        public Decimal? totalc { get; set; }
        public Decimal? diferencia { get; set; }
        public Decimal? tot_subtotal { get; set; }
         #region Constructors


        public vComprobanteDescuadrado()
        {

        }


        public vComprobanteDescuadrado(IDataReader reader)
        {
            this.codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.tipodoc= (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.empresa= (reader["com_empresa"] != DBNull.Value) ? (Int32?)reader["com_empresa"] : null;
            this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.debito= (reader["debito"] != DBNull.Value) ? (Decimal?)reader["debito"] : null;
            this.credito= (reader["credito"] != DBNull.Value) ? (Decimal?)reader["credito"] : null;
            this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;

            this.subtotal = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
            this.transporte = (reader["tot_transporte"] != DBNull.Value) ? (Decimal?)reader["tot_transporte"] : null;
            this.seguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
            this.impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
            this.totalc = (reader["total_calc"] != DBNull.Value) ? (Decimal?)reader["total_calc"] : null;
            this.diferencia = (reader["diferencia"] != DBNull.Value) ? (Decimal?)reader["diferencia"] : null;
            this.tot_subtotal = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
                 
        }

        #endregion

        public string GetSQL()
        {
            /*string sql = "SELECT DISTINCT t.com_codigo, t.com_doctran, t.com_periodo, t.com_mes, t.com_fecha, t.com_concepto, t.com_estado, t.com_tipodoc, t.com_ctipocom, t.com_almacen, t.com_pventa, t.com_numero, " +
                         "    t.com_codclipro, t.per_ciruc,t.per_apellidos, t.per_nombres, t.per_razon, " +
                         "    t.cenv_remitente, t.cenv_ciruc_rem, t.cenv_nombres_rem, t.cenv_apellidos_rem, " +
                         "    t.cenv_destinatario ,t.cenv_ciruc_des, t.cenv_nombres_des, t.cenv_apellidos_des, " +
                         "   t.cenv_vehiculo, t.cenv_placa, t.cenv_disco,  " +
                         "    t.cenv_socio, t.socionombres, " +
                         "    t.cenv_ruta, t.cenv_despachado_ret	 " +
                         "FROM (SELECT ROW_NUMBER() OVER(ORDER BY %orderby%) RowNr,  " +
                         "    c.com_codigo, c.com_doctran,c.com_periodo, c.com_mes, c.com_fecha, c.com_concepto, c.com_estado, c.com_tipodoc, c.com_ctipocom, c.com_almacen, c.com_pventa, c.com_numero, " +
                         "    c.com_codclipro, p.per_ciruc,p.per_apellidos, p.per_nombres, p.per_razon, " +
                         "    e.cenv_remitente, e.cenv_ciruc_rem, e.cenv_nombres_rem, e.cenv_apellidos_rem, " +
                         "    e.cenv_destinatario ,e.cenv_ciruc_des, e.cenv_nombres_des, e.cenv_apellidos_des, " +
                         "    e.cenv_vehiculo, e.cenv_placa, e.cenv_disco,  " +
                         "    e.cenv_socio, p1.per_apellidos || ' ' || p1.per_nombres socionombres, " +
                         "    e.cenv_ruta, e.cenv_despachado_ret	 " +
                         "FROM comprobante c " +
                         "    LEFT JOIN persona p ON c.com_codclipro = p.per_codigo " +
                         "    LEFT JOIN ccomenv e ON c.com_codigo = e.cenv_comprobante " +
                         "    LEFT JOIN persona p1 ON e.cenv_socio = p1.per_codigo %whereclause%) t " +
                         "WHERE RowNr BETWEEN %desde% AND %hasta% ";*/

            string sql = "select com_codigo, com_doctran, com_empresa, com_tipodoc, com_fecha, " +
                        "SUM(CASE WHEN dco_debcre = 1 THEN dco_valor_nac ELSE 0 END) as debito," +
                        "SUM(CASE WHEN dco_debcre = 2 THEN dco_valor_nac ELSE 0 END) as credito, null as tot_subtot_0, null as tot_transporte, null as tot_tseguro, null as tot_timpuesto, null as  total_calc, null as tot_subtotal, " +
                        "sum(CASE WHEN dco_debcre =1 THEN dco_valor_nac ELSE  0 END) - sum(CASE WHEN dco_debcre =2 THEN dco_valor_nac ELSE  0 END) as diferencia, " +
	                    "tot_total "+
                        " from dcontable inner join cuenta on cue_codigo = dco_cuenta inner join comprobante c on dco_comprobante = com_codigo and dco_empresa = com_empresa " +
                        " left join total on tot_comprobante = com_codigo and tot_empresa = com_empresa " +
                        " group by com_empresa,com_codigo, com_doctran,com_tipodoc, com_estado, com_periodo, com_mes, com_fecha, tot_total " +
                /*" having com_tipodoc = 4 " +
                " and com_estado = 2 " +
                " and com_periodo =2014 " +
                " and com_mes = 8 " +
                " and (sum(CASE WHEN dco_debcre =1  THEN dco_valor_nac ELSE 0 END) - sum(CASE WHEN dco_debcre =2  THEN dco_valor_nac ELSE 0 END)) <> 0 ";*/

                        " %whereclause%  %orderby%";

            return sql;
        }


        public string GetSQLALL()
        {


            string sql = "select com_codigo, com_fecha, com_doctran, tot_subtot_0, tot_transporte, tot_subtotal, tot_tseguro, tot_timpuesto, tot_total,  com_empresa,com_tipodoc, null as debito, null as credito,    " +
                        "tot_subtot_0 + tot_transporte + tot_subtotal + tot_tseguro + tot_timpuesto  as total_calc," +
                        "tot_total - (tot_subtot_0 + tot_transporte + tot_subtotal + tot_tseguro + tot_timpuesto)  as diferencia " +
                      
                        " from " +
                        " comprobante c " +
                        " inner join total on com_empresa = tot_empresa and com_codigo = tot_comprobante" +
              
                        "";

            return sql;
        }


        public List<vComprobanteDescuadrado> GetStruc()
        {
            return new List<vComprobanteDescuadrado>();
        }

        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }    
    }
}

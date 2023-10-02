using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vEstadoCuenta
    {


        /*public string per_id { get; set; }
        public int? per_codigo { get; set; }
        public string per_nombres { get; set; }
        public string per_apellidos { get; set; }
        public string per_razon { get; set; }
        public Decimal? per_cupo { get; set; }
        public Decimal? valor { get; set; }*/


        public int? ddo_empresa { get; set; }
        public Int64? ddo_comprobante { get; set; }
        public int? ddo_transacc { get; set; }
        public string ddo_doctran { get; set; }
        public int? ddo_pago { get; set; }        
        public Int64? ddo_comprobante_guia { get; set; }
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


        public int? dca_empresa { get; set; }
        public Int64? dca_comprobante { get; set; }
        public int? dca_transacc { get; set; }
        public string dca_doctran { get; set; }
        public int? dca_pago { get; set; }
        public Int32? dca_secuencia { get; set; }
        public Int64? dca_comprobante_can { get; set; }
        public int? dca_debcre { get; set; }
        public int? dca_transacc_can { get; set; }
        public Decimal? dca_monto { get; set; }
        public Decimal? dca_monto_ext { get; set; }
        public Decimal? dca_monto_pla { get; set; }


        public Int64? com_codigo { get; set; }
        public string com_doctran { get; set; }
        public int? com_tipodoc { get; set; }
        public DateTime? com_fecha { get; set; }
        public int? com_codclipro { get; set; }


        public string per_id { get; set; }
       public int? per_codigo { get; set; }
       public string per_nombres { get; set; }
       public string per_apellidos { get; set; }
       public string per_razon { get; set; }

        public decimal? valordebito { get; set; }
        public decimal? valorcredito{ get; set; }
        public decimal? valorsaldo { get; set; }
        public decimal? valortotal { get; set; }

        #region Constructors


        public vEstadoCuenta()
       {

       }



       public vEstadoCuenta(IDataReader reader,int tipo)
       {
           if (tipo==0)//SUMATORIA
           {
                this.per_id = (reader["per_id"] != DBNull.Value) ? (string)reader["per_id"] : null;
                this.per_codigo = (reader["per_codigo"] != DBNull.Value) ? (int?)reader["per_codigo"] : null;
                this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
                this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
                this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;
                this.valordebito = (reader["valordebito"] != DBNull.Value) ? (decimal?)reader["valordebito"] : null;
                this.valorcredito = (reader["valorcredito"] != DBNull.Value) ? (Decimal?)reader["valorcredito"] : null;

            }
           if (tipo==1)//DOCUMENTOS
           {
               this.ddo_empresa = (reader["ddo_empresa"] != DBNull.Value) ? (int?)reader["ddo_empresa"] : null;
               this.ddo_comprobante = (reader["ddo_comprobante"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante"] : null;
               this.ddo_transacc = (reader["ddo_transacc"] != DBNull.Value) ? (int?)reader["ddo_transacc"] : null;
               this.ddo_doctran = (reader["ddo_doctran"] != DBNull.Value) ? (string)reader["ddo_doctran"] : null;
               this.ddo_comprobante_guia= (reader["ddo_comprobante_guia"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante_guia"] : null;
                this.ddo_pago = (reader["ddo_pago"] != DBNull.Value) ? (int?)reader["ddo_pago"] : null;
                this.ddo_codclipro = (reader["ddo_codclipro"] != DBNull.Value) ? (int?)reader["ddo_codclipro"] : null;
               this.ddo_debcre = (reader["ddo_debcre"] != DBNull.Value) ? (int?)reader["ddo_debcre"] : null;
               this.ddo_tipo_cambio = (reader["ddo_tipo_cambio"] != DBNull.Value) ? (int?)reader["ddo_tipo_cambio"] : null;
               this.ddo_monto_ext = (reader["ddo_monto_ext"] != DBNull.Value) ? (decimal?)reader["ddo_monto_ext"] : null;
               this.ddo_cancela_ext = (reader["ddo_cancela_ext"] != DBNull.Value) ? (decimal?)reader["ddo_cancela_ext"] : null;
               this.ddo_agente = (reader["ddo_agente"] != DBNull.Value) ? (int?)reader["ddo_agente"] : null;
               this.ddo_cuenta = (reader["ddo_cuenta"] != DBNull.Value) ? (int?)reader["ddo_cuenta"] : null;
               this.ddo_modulo = (reader["ddo_modulo"] != DBNull.Value) ? (int?)reader["ddo_modulo"] : null;
               this.ddo_fecha_emi = (reader["ddo_fecha_emi"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_emi"] : null;
               this.ddo_fecha_ven = (reader["ddo_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_ven"] : null;
               this.ddo_monto = (reader["ddo_monto"] != DBNull.Value) ? (decimal?)reader["ddo_monto"] : null;
               this.ddo_cancela = (reader["ddo_cancela"] != DBNull.Value) ? (decimal?)reader["ddo_cancela"] : null;
               this.ddo_cancelado = (reader["ddo_cancelado"] != DBNull.Value) ? (int?)reader["ddo_cancelado"] : null;

                this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
                this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
                this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
                this.com_tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
                this.com_codclipro = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;

                this.per_id = (reader["per_id"] != DBNull.Value) ? (string)reader["per_id"] : null;
                this.per_codigo = (reader["per_codigo"] != DBNull.Value) ? (int?)reader["per_codigo"] : null;
                this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
                this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
                this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;

            }
           if (tipo==2)//CANCELACIONES
           {
                this.ddo_doctran = (reader["ddo_doctran"] != DBNull.Value) ? (string)reader["ddo_doctran"] : null;

                this.dca_empresa = (reader["dca_empresa"] != DBNull.Value) ? (int?)reader["dca_empresa"] : null;
                this.dca_comprobante = (reader["dca_comprobante"] != DBNull.Value) ? (Int64?)reader["dca_comprobante"] : null;
                this.dca_transacc = (reader["dca_transacc"] != DBNull.Value) ? (int?)reader["dca_transacc"] : null;
                this.dca_doctran = (reader["dca_doctran"] != DBNull.Value) ? (string)reader["dca_doctran"] : null;
                this.dca_pago = (reader["dca_pago"] != DBNull.Value) ? (int?)reader["dca_pago"] : null;
                this.dca_secuencia = (reader["dca_secuencia"] != DBNull.Value) ? (Int32?)reader["dca_secuencia"] : null;
                this.dca_comprobante_can = (reader["dca_comprobante_can"] != DBNull.Value) ? (Int64?)reader["dca_comprobante_can"] : null;
                this.dca_monto = (reader["dca_monto"] != DBNull.Value) ? (Decimal?)reader["dca_monto"] : null;
                this.dca_monto_ext = (reader["dca_monto_ext"] != DBNull.Value) ? (Decimal?)reader["dca_monto_ext"] : null;
                this.dca_monto_pla = (reader["dca_monto_pla"] != DBNull.Value) ? (Decimal?)reader["dca_monto_pla"] : null;
                this.dca_debcre = (reader["dca_debcre"] != DBNull.Value) ? (int?)reader["dca_debcre"] : null;
                this.dca_transacc_can = (reader["dca_transacc_can"] != DBNull.Value) ? (int?)reader["dca_transacc_can"] : null;
                
                this.com_codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
                this.com_fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
                this.com_doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
                this.com_tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
                this.com_codclipro = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;

                this.per_id = (reader["per_id"] != DBNull.Value) ? (string)reader["per_id"] : null;
                this.per_codigo = (reader["per_codigo"] != DBNull.Value) ? (int?)reader["per_codigo"] : null;
                this.per_nombres = (reader["per_nombres"] != DBNull.Value) ? (string)reader["per_nombres"] : null;
                this.per_apellidos = (reader["per_apellidos"] != DBNull.Value) ? (string)reader["per_apellidos"] : null;
                this.per_razon = (reader["per_razon"] != DBNull.Value) ? (string)reader["per_razon"] : null;

            }


        }

       #endregion

       public string GetSQL()
       {
           string sql = "select "+
           "per_id, "+
           "per_codigo, " +
           "per_nombres, "+
           "per_apellidos, "+
           "per_razon, " +
           "per_cupo, " +
           " sum(case when ddo_debcre =1 then ddo_monto-ddo_cancela else -(ddo_monto-ddo_cancela) END) as valor  " +
           "from ddocumento "+
           "inner join persona ON ddo_codclipro=per_codigo and ddo_empresa=per_empresa " +
           "inner join comprobante ON ddo_comprobante=com_codigo and ddo_empresa=com_empresa " +
           "inner join personaxtipo on per_codigo=pxt_persona and per_empresa=pxt_empresa "+         
           "%whereclause% " +
           "group by  per_id,per_codigo,per_nombres,per_apellidos,per_razon,per_cupo";
           //  "where  dcg_empresa={0} and dcg_planilla={1} order by 	detalle.com_fecha     ";

           return sql;
       }

        public string GetSqlSumDoc()
        {
            string sql = "select per_id, per_codigo, per_nombres, per_apellidos, per_razon, " +
                        " sum(CASE WHEN ddo_debcre = 1 THEN ddo_monto ELSE  0 END) valordebito, sum(CASE WHEN ddo_debcre = 2 THEN ddo_monto ELSE  0 END) valorcredito " +
                        " from ddocumento " +
                        "     inner join comprobante on com_empresa = ddo_empresa and com_codigo = ddo_comprobante " +
                        "     left join persona on per_empresa = ddo_empresa and per_codigo = ddo_codclipro " +
                        //"     left join persona on per_empresa = com_empresa and per_codigo = com_codclipro " +
                        //" where com_fecha between '2016-01-01' and '2017-01-01' " +
                        //" and ddo_cuenta in (61,62) " +
                        //" and com_estado = 2 " +
                        " %whereclause% " +
                        " group by per_id, per_codigo, per_nombres, per_apellidos, per_razon " +
                        //" %orderby% "+ //" order by per_razon " +
                        "";
            return sql;
        }

        public string GetSqlSumCan()
        {
            string sql = "select per_id, per_codigo, per_nombres, per_apellidos, per_razon, " +
                          "  sum(CASE WHEN dca_debcre = 1 THEN dca_monto ELSE  0 END) valordebito, sum(CASE WHEN dca_debcre = 2 THEN dca_monto ELSE  0 END) valorcredito " +
                          "  from ddocumento " +
                          "      inner join comprobante cd on cd.com_empresa = ddo_empresa and cd.com_codigo = ddo_comprobante " +
                          "     left join persona on per_empresa = ddo_empresa and per_codigo = ddo_codclipro " +//"      left join persona on per_empresa = cd.com_empresa and per_codigo = cd.com_codclipro " +
                          "      inner join dcancelacion on dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago = ddo_pago " +
                          "      inner join comprobante cc on cc.com_empresa = dca_empresa and cc.com_codigo = dca_comprobante_can " +
                          //"  where cc.com_fecha between '2016-01-01' and '2017-01-01' " +
                          //"      and ddo_cuenta in (61,62) " +
                          //"      and cc.com_estado = 2 " +
                          " %whereclause% " +
                          "  group by per_id, per_codigo, per_nombres, per_apellidos, per_razon " +
                          //" %orderby% " + //"  order by per_razon " +
                          "";
            return sql;
        }


        public string GetSQLDoc()
        {
            string sql = "select per_id, per_codigo, per_nombres, per_apellidos, per_razon,  " +
                         "   com_codigo, com_fecha, com_doctran, com_tipodoc, com_codclipro, " +
                         "   ddo_empresa, ddo_comprobante, ddo_transacc, ddo_doctran, ddo_pago,ddo_comprobante_guia, ddo_codclipro,  " +
                         "   ddo_debcre, ddo_tipo_cambio, ddo_monto_ext,ddo_cancela_ext, ddo_agente,ddo_cuenta,  " +
                         "   ddo_modulo, ddo_fecha_emi,ddo_fecha_ven,ddo_monto, ddo_cancela,ddo_cancelado " +
                         "   from ddocumento " +
                         "       inner join comprobante on com_empresa = ddo_empresa and com_codigo = ddo_comprobante " +
                         "       left join persona on per_empresa = ddo_empresa and per_codigo = ddo_codclipro " +//"       left join persona on per_empresa = com_empresa and per_codigo = com_codclipro " +
                         //"   where com_fecha between '2016-01-01' and '2017-01-01' " +
                         //"       and ddo_cuenta in (61,62) " +
                         //"       and com_estado = 2 " +
                         //" %whereclause% " +
                         //" %orderby% " +//"   order by per_razon, com_fecha " +
                         "";
            return sql;
        }

        public string GetSQLCan()
        {
            string sql = "select per_id, per_codigo, per_nombres, per_apellidos, per_razon,  ddo_doctran, " +
                         "   cc.com_codigo, cc.com_fecha, cc.com_doctran, cc.com_tipodoc, cc.com_codclipro,  " +
                         "   dca_empresa, dca_comprobante, dca_transacc,dca_doctran,dca_pago,dca_secuencia, " +
                         "   dca_comprobante_can,dca_monto, dca_monto_ext, dca_monto_pla,dca_debcre,dca_transacc_can " +
                         "   from ddocumento " +
                         "       inner join comprobante cd on cd.com_empresa = ddo_empresa and cd.com_codigo = ddo_comprobante " +
                         "       left join persona on per_empresa = ddo_empresa and per_codigo = ddo_codclipro " +//"       left join persona on per_empresa = cd.com_empresa and per_codigo = cd.com_codclipro " +
                         "       inner join dcancelacion on dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago = ddo_pago " +
                         "       inner join comprobante cc on cc.com_empresa = dca_empresa and cc.com_codigo = dca_comprobante_can " +
                         //"   where cc.com_fecha between '2016-01-01' and '2017-01-01' " +
                         //"       and ddo_cuenta in (61,62) " +
                         //"       and cc.com_estado = 2 " +
                         //" %whereclause% " +
                         //" %orderby% " +//"   order by per_razon, cc.com_fecha " +
                         "";

            return sql;


        }


        public string GetSQLDoc1()
        {
            string sql = "select per_id, per_codigo, per_nombres, per_apellidos, per_razon,  " +
                         "   com_codigo, com_fecha, com_doctran, com_tipodoc, com_codclipro, " +
                         "   ddo_empresa, ddo_comprobante, ddo_transacc, ddo_doctran, null as ddo_pago, null as ddo_comprobante_guia, ddo_codclipro,  " +
                         "   ddo_debcre, ddo_tipo_cambio, sum(ddo_monto_ext) as ddo_monto_ext, sum(ddo_cancela_ext) as ddo_cancela_ext, ddo_agente,ddo_cuenta,  " +
                         "   ddo_modulo, ddo_fecha_emi,min(ddo_fecha_ven) as ddo_fecha_ven,sum(ddo_monto) as ddo_monto, sum(ddo_cancela) as ddo_cancela, null as ddo_cancelado " +
                         "   from ddocumento " +
                         "       inner join comprobante on com_empresa = ddo_empresa and com_codigo = ddo_comprobante " +
                         "       left join persona on per_empresa = ddo_empresa and per_codigo = ddo_codclipro " +//"       left join persona on per_empresa = com_empresa and per_codigo = com_codclipro " +
                         //"   where com_fecha between '2016-01-01' and '2017-01-01' " +
                         //"       and ddo_cuenta in (61,62) " +
                         //"       and com_estado = 2 " +
                         " %whereclause% " +
                         "  group by per_id, per_codigo, per_nombres, per_apellidos, per_razon,com_codigo, com_fecha, com_doctran, com_tipodoc, com_codclipro, "+
                         " ddo_empresa, ddo_comprobante, ddo_transacc, ddo_doctran, ddo_codclipro, "+
                         " ddo_debcre, ddo_tipo_cambio,ddo_agente,ddo_cuenta, ddo_modulo, ddo_fecha_emi "+
                         //" %orderby% " +//"   order by per_razon, com_fecha " +
                         "";
            return sql;
        }

        public string GetSQLCan1()
        {
            string sql = "select per_id, per_codigo, per_nombres, per_apellidos, per_razon,  ddo_doctran, " +
                         "   cc.com_codigo, cc.com_fecha, cc.com_doctran, cc.com_tipodoc, cc.com_codclipro,  " +
                         "   dca_empresa, dca_comprobante, dca_transacc,dca_doctran, null as dca_pago, null as dca_secuencia, " +
                         "   dca_comprobante_can,sum(dca_monto) as dca_monto, sum(dca_monto_ext) as dca_monto_ext, sum(dca_monto_pla) as dca_monto_pla, dca_debcre,dca_transacc_can " +
                         "   from ddocumento " +
                         "       inner join comprobante cd on cd.com_empresa = ddo_empresa and cd.com_codigo = ddo_comprobante " +
                         "       left join persona on per_empresa = ddo_empresa and per_codigo = ddo_codclipro " +//"       left join persona on per_empresa = cd.com_empresa and per_codigo = cd.com_codclipro " +
                         "       inner join dcancelacion on dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago = ddo_pago " +
                         "       inner join comprobante cc on cc.com_empresa = dca_empresa and cc.com_codigo = dca_comprobante_can " +
                          //"   where cc.com_fecha between '2016-01-01' and '2017-01-01' " +
                          //"       and ddo_cuenta in (61,62) " +
                          //"       and cc.com_estado = 2 " +
                          " %whereclause% " +
                          " group by per_id, per_codigo, per_nombres, per_apellidos, per_razon, ddo_doctran, cc.com_codigo, cc.com_fecha, cc.com_doctran, cc.com_tipodoc, cc.com_codclipro, " +
                          " dca_empresa, dca_comprobante, dca_transacc,dca_doctran,dca_comprobante_can , dca_debcre,dca_transacc_can " +
                         //" %orderby% " +//"   order by per_razon, cc.com_fecha " +
                         "";

            return sql;


        }


        public List<vEstadoCuenta> GetStruc()
        {
            return new List<vEstadoCuenta>();
        }


    }
}

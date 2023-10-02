using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vCancelacion
    {
        public Int32? ddo_empresa { get; set; }
        public Int64? ddo_comprobante { get; set; }        
        public Int32? ddo_transacc { get; set; }
        public String ddo_doctran { get; set; }
        public Int32? ddo_pago { get; set; }
        public Int64? ddo_comprobante_guia { get; set; }
        public Int32? ddo_codclipro { get; set; }                
        public DateTime? ddo_fecha_emi { get; set; }
        public DateTime? ddo_fecha_ven { get; set; }
        public Decimal? ddo_monto { get; set; }
        public Decimal? ddo_monto_ext { get; set; }
        public Decimal? ddo_cancela { get; set; }
        public Decimal? ddo_cancela_ext { get; set; }
        public Int32? ddo_cancelado { get; set; }
        public Int32? ddo_cuenta { get; set; }
        public Int32? ddo_modulo { get; set; }
        public Int32? dca_secuencia { get; set; }
        public Int64? dca_comprobante_can { get; set; }
        public Decimal? dca_monto { get; set; }
        public Decimal? dca_monto_ext { get; set; }
        public Decimal? dca_monto_pla { get; set; }
        public Int64? dca_planilla { get; set; }

        public Int32? socio { get; set; }
        public string idsocio { get; set; }
        public string nombressocio { get; set; }
        public string apellidossocio { get; set; }

        public Int32? vehiculo { get; set; }
        public string disco { get; set; }
        public string placa { get; set; }



        public DateTime? fecha_can { get; set; }
        public string doctran_can { get; set; }
        public string doctran_guia { get; set; }        

        public string ruccliente { get; set; }
        public string razoncliente { get; set; }
        public String ddo_doctranpla { get; set; }
        public Decimal? subtotal { get; set; }
        /*public Int32? dfp_tipopago { get; set; }
        public Decimal? dfp_monto { get; set; }
        public Decimal? dfp_monto_ext { get; set; }
        
        public Int32? dfp_tarjeta { get; set; }
        public Int32? dfp_banco { get; set; }
        public String dfp_nro_cheque { get; set; }
        public String dfp_beneficiario { get; set; }
        public DateTime? dfp_fecha_ven { get; set; }
        public Int32? dfp_cuenta { get; set; }
        public Int64? dfp_ref_comprobante { get; set; }
        public String dfp_nro_documento { get; set; }
        public String dfp_nro_cuenta { get; set; }
        public String dfp_emisor { get; set; }
        public Int32? dfp_tclipro { get; set; }

        public string tpa_nombre { get; set; }
        public string ban_nombre { get; set; }*/

          #region Constructors


        public vCancelacion()
        {

        }



        public vCancelacion(IDataReader reader)
        {
            this.ddo_empresa = (reader["ddo_empresa"] != DBNull.Value) ? (Int32?)reader["ddo_empresa"] : null;
            this.ddo_comprobante=(reader["ddo_comprobante"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante"] : null;            
            this.ddo_transacc = (reader["ddo_transacc"] != DBNull.Value) ? (int?)reader["ddo_transacc"] : null;
            this.ddo_doctran = (reader["ddo_doctran"] != DBNull.Value) ? (string)reader["ddo_doctran"] : null;
            this.ddo_pago = (reader["ddo_pago"] != DBNull.Value) ? (int?)reader["ddo_pago"] : null;
            this.ddo_comprobante_guia = (reader["ddo_comprobante_guia"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante_guia"] : null;            
            this.ddo_codclipro = (reader["ddo_codclipro"] != DBNull.Value) ? (int?)reader["ddo_codclipro"] : null;
            this.ddo_fecha_emi = (reader["ddo_fecha_emi"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_emi"] : null;
            this.ddo_fecha_ven = (reader["ddo_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["ddo_fecha_ven"] : null;
            this.ddo_monto = (reader["ddo_monto"] != DBNull.Value) ? (Decimal?)reader["ddo_monto"] : null;
            this.ddo_monto_ext = (reader["ddo_monto_ext"] != DBNull.Value) ? (Decimal?)reader["ddo_monto_ext"] : null;
            this.ddo_cancela = (reader["ddo_cancela"] != DBNull.Value) ? (Decimal?)reader["ddo_cancela"] : null;
            this.ddo_cancela_ext = (reader["ddo_cancela_ext"] != DBNull.Value) ? (Decimal?)reader["ddo_cancela_ext"] : null;
            this.ddo_cancelado = (reader["ddo_cancelado"] != DBNull.Value) ? (Int32?)reader["ddo_cancelado"] : null;
            this.ddo_cuenta = (reader["ddo_cuenta"] != DBNull.Value) ? (Int32?)reader["ddo_cuenta"] : null;
            this.ddo_modulo = (reader["ddo_modulo"] != DBNull.Value) ? (Int32?)reader["ddo_modulo"] : null;
            this.dca_secuencia= (reader["dca_secuencia"] != DBNull.Value) ? (Int32?)reader["dca_secuencia"] : null;
            this.dca_comprobante_can = (reader["dca_comprobante_can"] != DBNull.Value) ? (Int64?)reader["dca_comprobante_can"] : null;
            this.dca_monto = (reader["dca_monto"] != DBNull.Value) ? (Decimal?)reader["dca_monto"] : null;
            this.dca_monto_ext = (reader["dca_monto_ext"] != DBNull.Value) ? (Decimal?)reader["dca_monto_ext"] : null;
            this.dca_monto_pla = (reader["dca_monto_pla"] != DBNull.Value) ? (Decimal?)reader["dca_monto_pla"] : null;
            this.dca_planilla = (reader["dca_planilla"] != DBNull.Value) ? (Int64?)reader["dca_planilla"] : null;            

            this.socio = (reader["socio"] != DBNull.Value) ? (Int32?)reader["socio"] : null;
            this.idsocio = (reader["idsocio"] != DBNull.Value) ? (string)reader["idsocio"] : null;
            this.nombressocio = (reader["nombressocio"] != DBNull.Value) ? (string)reader["nombressocio"] : null;
            this.apellidossocio = (reader["apellidossocio"] != DBNull.Value) ? (string)reader["apellidossocio"] : null;

            this.vehiculo = (reader["vehiculo"] != DBNull.Value) ? (Int32?)reader["vehiculo"] : null;
            this.disco = (reader["disco"] != DBNull.Value) ? (string)reader["disco"] : null;
            this.placa = (reader["placa"] != DBNull.Value) ? (string)reader["placa"] : null;

            this.doctran_can = (reader["doctran_can"] != DBNull.Value) ? (string)reader["doctran_can"] : null;
            this.doctran_guia = (reader["doctran_guia"] != DBNull.Value) ? (string)reader["doctran_guia"] : null;
            this.fecha_can= (reader["fecha_can"] != DBNull.Value) ? (DateTime?)reader["fecha_can"] : null;

            this.ruccliente = (reader["ruccliente"] != DBNull.Value) ? (string)reader["ruccliente"] : null;
            this.razoncliente = (reader["razoncliente"] != DBNull.Value) ? (string)reader["razoncliente"] : null;
            this.ddo_doctranpla = (reader["doctran_pla"] != DBNull.Value) ? (string)reader["doctran_pla"] : null;
            this.subtotal = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;

            /*this.dfp_tipopago = (reader["dfp_tipopago"] != DBNull.Value) ? (Int32?)reader["dfp_tipopago"] : null;
            this.dfp_monto = (reader["dfp_monto_ext"] != DBNull.Value) ? (Decimal?)reader["dfp_monto_ext"] : null;
            this.dfp_monto_ext = (reader["dfp_monto_ext"] != DBNull.Value) ? (Decimal?)reader["dfp_monto_ext"] : null;                        
            this.dfp_tarjeta = (reader["dfp_tarjeta"] != DBNull.Value) ? (Int32?)reader["dfp_tarjeta"] : null;
            this.dfp_banco = (reader["dfp_banco"] != DBNull.Value) ? (Int32?)reader["dfp_banco"] : null;
            this.dfp_nro_cheque = reader["dfp_nro_cheque"].ToString();
            this.dfp_beneficiario = reader["dfp_beneficiario"].ToString();
            this.dfp_fecha_ven = (reader["dfp_fecha_ven"] != DBNull.Value) ? (DateTime?)reader["dfp_fecha_ven"] : null;
            this.dfp_cuenta = (reader["dfp_cuenta"] != DBNull.Value) ? (Int32?)reader["dfp_cuenta"] : null;
            this.dfp_ref_comprobante = (reader["dfp_ref_comprobante"] != DBNull.Value) ? (Int64?)reader["dfp_ref_comprobante"] : null;
            this.dfp_nro_documento = reader["dfp_nro_documento"].ToString();
            this.dfp_nro_cuenta = reader["dfp_nro_cuenta"].ToString();
            this.dfp_emisor = reader["dfp_emisor"].ToString();
            this.dfp_tclipro = (reader["dfp_tclipro"] != DBNull.Value) ? (Int32?)reader["dfp_tclipro"] : null;

            this.tpa_nombre = (reader["tpa_nombre"] != DBNull.Value) ? (string)reader["tpa_nombre"] : null;
            this.ban_nombre = (reader["ban_nombre"] != DBNull.Value) ? (string)reader["ban_nombre"] : null;
            */
        
        }

        #endregion

        public string  GetSQL()
        {
            string sql = "SELECT " +
                         "   ddo_empresa, " +
                         "   ddo_comprobante, " +
                         "   ddo_transacc, " +
                         "   ddo_doctran, " +
                         "   ddo_pago, " +
                         "   ddo_comprobante_guia, " +
                         "   ddo_codclipro, " +
                         //"   ddo_fecha_emi, " +
                         "   CASE WHEN ddo_comprobante_guia IS NOT NULL THEN g.com_fecha ELSE ddo_fecha_emi END as ddo_fecha_emi, " +                         
                         "   ddo_fecha_ven, " +
                         "   ddo_debcre, " +
                         "   ddo_monto, " +
                         "   ddo_monto_ext, " +
                         "   ddo_cancela, " +
                         "   ddo_cancela_ext, " +
                         "   ddo_cancelado,	 " +
                         "   ddo_cuenta, " +
                         "   ddo_modulo, " +
                         "   dca_secuencia, " +
                         "   dca_comprobante_can, " +
                         "   dca_monto, " +
                         "   dca_monto_ext, " +
                         "   dca_monto_pla, " +
                         "   dca_planilla, " +
                         "  null as  tot_subtot_0 ," +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_codigo ELSE pg.per_codigo END as socio, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_id ELSE pg.per_id END as idsocio, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_nombres ELSE pg.per_nombres END as nombressocio, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_apellidos ELSE pg.per_apellidos END as apellidossocio,	 " +

                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_vehiculo ELSE cg.cenv_vehiculo END as vehiculo, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_disco ELSE cg.cenv_disco END as disco, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_placa ELSE cg.cenv_placa END as placa,	 " +


                         //"   CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_codigo ELSE pg.per_codigo END as socio, " +
                         //"   CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_id ELSE pg.per_id END as idsocio, " +
                         //"   CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_nombres ELSE pg.per_nombres END as nombressocio, " +
                         //"   CASE WHEN cf.cenv_socio IS NOT NULL THEN pf.per_apellidos ELSE pg.per_apellidos END as apellidossocio,	 " +
                         "   c.com_doctran doctran_can, " +
                         "   c.com_fecha fecha_can, " +
                         "   g.com_doctran doctran_guia, " +
                         "   cl.per_ciruc ruccliente, " +
                         "   cl.per_razon razoncliente, " +
                         "   p.com_doctran doctran_pla,"+
                         //"   dfp_tipopago, " +
                         //"   dfp_monto, " +
                         //"   dfp_monto_ext, " +
                         //"   dfp_nro_documento, " +
                         //"   dfp_nro_cuenta, " +
                         //"   dfp_emisor, " +
                         //"   dfp_debcre, " +
                         //"   dfp_tarjeta, " +
                         //"   dfp_banco, " +
                         //"   dfp_nro_cheque, " +
                         //"   dfp_beneficiario,	 " +
                         //"   dfp_fecha_ven, " +
                         //"   dfp_cuenta, " +
                         //"    dfp_ref_comprobante, " +
                         //"    dfp_tclipro, " +
                         //"   tpa_nombre, " +
                         //"   ban_nombre, " +
                         "   '' " +
                         " FROM " +
                         "   ddocumento " +
                         "   INNER JOIN dcancelacion ON ddo_empresa = dca_empresa AND ddo_comprobante = dca_comprobante AND ddo_transacc = dca_transacc AND ddo_doctran = dca_doctran AND ddo_pago = dca_pago " +
                         "   INNER JOIN comprobante f ON ddo_empresa = f.com_empresa AND ddo_comprobante = f.com_codigo " +
                         "   LEFT JOIN ccomenv cf ON f.com_empresa = cf.cenv_empresa AND f.com_codigo = cf.cenv_comprobante " +
                         "   LEFT JOIN persona pf ON cf.cenv_empresa = pf.per_empresa AND cf.cenv_socio = pf.per_codigo " +
                         "   LEFT JOIN persona cl ON f.com_empresa = cl.per_empresa AND f.com_codclipro  = cl.per_codigo " +
                         "   LEFT JOIN comprobante g ON ddo_empresa = g.com_empresa AND ddo_comprobante_guia = g.com_codigo " +
                         "   LEFT JOIN ccomenv cg ON g.com_empresa = cg.cenv_empresa AND g.com_codigo = cg.cenv_comprobante " +
                         "   LEFT JOIN persona pg ON cg.cenv_empresa = pg.per_empresa AND cg.cenv_socio = pg.per_codigo " +
                         "   LEFT JOIN comprobante c ON dca_empresa = c.com_empresa AND dca_comprobante_can = c.com_codigo " +
                         "   LEFT JOIN comprobante p ON dca_empresa = p.com_empresa AND dca_planilla = p.com_codigo "+
                         //"   LEFT JOIN drecibo ON dfp_empresa = c.com_empresa AND dfp_comprobante = c.com_codigo AND dfp_monto > 0 " +
                         //"   LEFT JOIN tipopago ON dfp_empresa = tpa_empresa  AND dfp_tipopago = tpa_codigo  " +
                         //"   LEFT JOIN banco ON dfp_empresa = ban_empresa AND dfp_banco = ban_codigo	 " +
                         //"WHERE " +
                         //"   f.com_tipodoc = 4 " +
                         //"   and f.com_fecha BETWEEN '15/08/2014' AND '30/09/2014' " +
                         "";
	
      //  "where  dcg_empresa={0} and dcg_planilla={1} order by 	detalle.com_fecha     ";

            return sql;
        }


        public string GetSQLAll()
        {

            string sql = "SELECT " +
                        "   ddo_empresa, " +
                        "   ddo_comprobante, " +
                        "   ddo_transacc, " +
                        "   ddo_doctran, " +
                        "   ddo_pago, " +
                        "   ddo_comprobante_guia, " +
                        "   ddo_codclipro, " +
                        //"   ddo_fecha_emi, " +
                        //"   CASE WHEN pf.per_codigo IS NOT NULL THEN ddo_fecha_emi ELSE g.com_fecha END as ddo_fecha_emi, " +
                        "   CASE WHEN ddo_comprobante_guia IS NOT NULL THEN g.com_fecha ELSE ddo_fecha_emi END as ddo_fecha_emi, " +                         
                        "   ddo_fecha_ven, " +
                        "   ddo_debcre, " +
                        "   ddo_monto, " +
                        "   ddo_monto_ext, " +
                        "   ddo_cancela, " +
                        "   ddo_cancela_ext, " +
                        "   ddo_cancelado,	 " +
                        "   ddo_cuenta, " +
                        "   ddo_modulo, " +
                        "   dca_secuencia, " +
                        "   dca_comprobante_can, " +
                        "   dca_monto, " +
                        "   dca_monto_ext, " +
                        "   dca_monto_pla, " +
                        "   dca_planilla, " +
                         "  null as  tot_subtot_0 , " +
                        "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_codigo ELSE pg.per_codigo END as socio,  " +
                        "    CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_id ELSE pg.per_id END as idsocio,    " +
                        "    CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_nombres ELSE pg.per_nombres END as nombressocio,     " +
                        "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_apellidos ELSE pg.per_apellidos END as apellidossocio,	 " +

                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_vehiculo ELSE cg.cenv_vehiculo END as vehiculo, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_disco ELSE cg.cenv_disco END as disco, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_placa ELSE cg.cenv_placa END as placa,	 " +


                        "   c.com_doctran doctran_can, " +
                        "   c.com_fecha fecha_can, " +
                        "   g.com_doctran doctran_guia, " +
                        "   cl.per_ciruc ruccliente, " +
                        "   cl.per_razon razoncliente, " +
                        "   p.com_doctran doctran_pla," +
                //"   dfp_tipopago, " +
                //"   dfp_monto, " +
                //"   dfp_monto_ext, " +
                //"   dfp_nro_documento, " +
                //"   dfp_nro_cuenta, " +
                //"   dfp_emisor, " +
                //"   dfp_debcre, " +
                //"   dfp_tarjeta, " +
                //"   dfp_banco, " +
                //"   dfp_nro_cheque, " +
                //"   dfp_beneficiario,	 " +
                //"   dfp_fecha_ven, " +
                //"   dfp_cuenta, " +
                //"    dfp_ref_comprobante, " +
                //"    dfp_tclipro, " +
                //"   tpa_nombre, " +
                //"   ban_nombre, " +
                        "   '' " +
                        " FROM " +
                        "   ddocumento " +
                        "   LEFT JOIN dcancelacion ON ddo_empresa = dca_empresa AND ddo_comprobante = dca_comprobante AND ddo_transacc = dca_transacc AND ddo_doctran = dca_doctran AND ddo_pago = dca_pago " +
                        "   INNER JOIN comprobante f ON ddo_empresa = f.com_empresa AND ddo_comprobante = f.com_codigo " +
                        "   LEFT JOIN ccomenv cf ON f.com_empresa = cf.cenv_empresa AND f.com_codigo = cf.cenv_comprobante " +
                        "   LEFT JOIN persona pf ON cf.cenv_empresa = pf.per_empresa AND cf.cenv_socio = pf.per_codigo " +
                        "   LEFT JOIN persona cl ON f.com_empresa = cl.per_empresa AND f.com_codclipro  = cl.per_codigo " +
                        "   LEFT JOIN comprobante g ON ddo_empresa = g.com_empresa AND ddo_comprobante_guia = g.com_codigo " +
                        "   LEFT JOIN ccomenv cg ON g.com_empresa = cg.cenv_empresa AND g.com_codigo = cg.cenv_comprobante " +
                        "   LEFT JOIN persona pg ON cg.cenv_empresa = pg.per_empresa AND cg.cenv_socio = pg.per_codigo " +
                        "   LEFT JOIN comprobante c ON dca_empresa = c.com_empresa AND dca_comprobante_can = c.com_codigo " +
                        "   LEFT JOIN comprobante p ON dca_empresa = p.com_empresa AND dca_planilla = p.com_codigo " +
                //"   LEFT JOIN drecibo ON dfp_empresa = c.com_empresa AND dfp_comprobante = c.com_codigo AND dfp_monto > 0 " +
                //"   LEFT JOIN tipopago ON dfp_empresa = tpa_empresa  AND dfp_tipopago = tpa_codigo  " +
                //"   LEFT JOIN banco ON dfp_empresa = ban_empresa AND dfp_banco = ban_codigo	 " +
                //"WHERE " +
                //"   f.com_tipodoc = 4 " +
                //"   and f.com_fecha BETWEEN '15/08/2014' AND '30/09/2014' " +
                        "";

            //  "where  dcg_empresa={0} and dcg_planilla={1} order by 	detalle.com_fecha     ";

            return sql;


        }

        public string GetSQLAll1()
        {
            string sql = "SELECT " +
                         "   ddo_empresa, " +
                        "   ddo_comprobante, " +
                        "   ddo_transacc, " +
                        "   ddo_doctran, " +
                        "   ddo_pago, " +
                        "   ddo_comprobante_guia, " +
                        "   ddo_codclipro, " +
                        //"   ddo_fecha_emi, " +
                        //"   CASE WHEN pf.per_codigo IS NOT NULL THEN ddo_fecha_emi ELSE g.com_fecha END as ddo_fecha_emi, " +
                        "   CASE WHEN ddo_comprobante_guia IS NOT NULL THEN g.com_fecha ELSE ddo_fecha_emi END as ddo_fecha_emi, " +                         
                        "   ddo_fecha_ven, " +
                        "   ddo_debcre, " +
                        "   ddo_monto, " +
                        "   ddo_monto_ext, " +
                        "   ddo_cancela, " +
                        "   ddo_cancela_ext, " +
                        "   ddo_cancelado,	 " +
                        "   ddo_cuenta, " +
                        "   ddo_modulo, " +
                        "   dca_secuencia, " +
                        "   dca_comprobante_can, " +
                        "   dca_monto, " +
                        "   dca_monto_ext, " +
                        "   dca_monto_pla, " +
                        "   dca_planilla, " +
                        "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_codigo ELSE pg.per_codigo END as socio,  " +
                        "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_id ELSE pg.per_id END as idsocio,    " +
                        "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_nombres ELSE pg.per_nombres END as nombressocio,     " +
                        "   CASE WHEN pf.per_codigo IS NOT NULL THEN pf.per_apellidos ELSE pg.per_apellidos END as apellidossocio,	 " +

                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_vehiculo ELSE cg.cenv_vehiculo END as vehiculo, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_disco ELSE cg.cenv_disco END as disco, " +
                         "   CASE WHEN pf.per_codigo IS NOT NULL THEN cf.cenv_placa ELSE cg.cenv_placa END as placa,	 " +

                        "   c.com_doctran doctran_can, " +
                        "   c.com_fecha fecha_can, " +
                        "   g.com_doctran doctran_guia, " +
                        "   cl.per_ciruc ruccliente, " +
                        "   cl.per_razon razoncliente, " +
                        "   p.com_doctran doctran_pla," +
                        "   CASE WHEN pf.per_codigo IS NOT NULL THEN tf.tot_subtot_0 ELSE tg.tot_subtot_0 END as tot_subtot_0 " +

                          " FROM   ddocumento" +
                         
                          " INNER JOIN dcancelacion ON ddo_empresa = dca_empresa AND ddo_comprobante = dca_comprobante AND ddo_transacc = dca_transacc AND ddo_doctran = dca_doctran AND ddo_pago = dca_pago " +                          
                          "   INNER JOIN comprobante f ON ddo_empresa = f.com_empresa AND ddo_comprobante = f.com_codigo " +
                          "   LEFT JOIN ccomenv cf ON f.com_empresa = cf.cenv_empresa AND f.com_codigo = cf.cenv_comprobante " +
                         "   LEFT JOIN persona pf ON cf.cenv_empresa = pf.per_empresa AND cf.cenv_socio = pf.per_codigo " +
                         "   LEFT JOIN total tf ON  ddo_empresa = tf.tot_empresa AND ddo_comprobante = tf.tot_comprobante "+
                         "   LEFT JOIN persona cl ON f.com_empresa = cl.per_empresa AND f.com_codclipro  = cl.per_codigo " +
                         "   LEFT JOIN comprobante g ON ddo_empresa = g.com_empresa AND ddo_comprobante_guia = g.com_codigo " +
                         "   LEFT JOIN ccomenv cg ON g.com_empresa = cg.cenv_empresa AND g.com_codigo = cg.cenv_comprobante " +
                         "   LEFT JOIN persona pg ON cg.cenv_empresa = pg.per_empresa AND cg.cenv_socio = pg.per_codigo " +
                         "   LEFT JOIN comprobante c ON dca_empresa = c.com_empresa AND dca_comprobante_can = c.com_codigo " +
                         "   LEFT JOIN comprobante p ON dca_empresa = p.com_empresa AND dca_planilla = p.com_codigo " +
                         "   LEFT JOIN total tg ON  ddo_empresa = tg.tot_empresa AND ddo_comprobante_guia = tg.tot_comprobante  " +
                         "";




            return sql;

        }

         public List<vCancelacion> GetStruc()
        {
            return new List<vCancelacion>();
        }
        
    }

}

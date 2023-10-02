using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vGuiaPlanilla
    {

        public Int64? codigoguia { get; set; }
        public string guia { get; set; }
        public DateTime? fecha { get; set; }
        public string razon { get; set; }
        public string destinatario { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public decimal? total { get; set; }
        public string planilla { get; set; }
        public string factura { get; set; }
        public string usuario { get; set; }
        public string socio { get; set; }


        #region Constructors


        public vGuiaPlanilla()
        {

        }



        public vGuiaPlanilla(IDataReader reader)
        {
            this.codigoguia = (reader["codigoguia"] != DBNull.Value) ? (Int64?)reader["codigoguia"] : null;
            this.guia = (reader["guia"] != DBNull.Value) ? (string)reader["guia"] : null;
            this.fecha= (reader["fecha"] != DBNull.Value) ? (DateTime?)reader["fecha"] : null;
            this.razon= (reader["razon"] != DBNull.Value) ? (string)reader["razon"] : null;
            this.total = (reader["total"] != DBNull.Value) ? (decimal?)reader["total"] : null;
            this.planilla = (reader["planilla"] != DBNull.Value) ? (string)reader["planilla"] : null;
            this.factura = (reader["factura"] != DBNull.Value) ? (string)reader["factura"] : null;
            this.usuario  = (reader["usuario"] != DBNull.Value) ? (string)reader["usuario"] : null;
            this.destinatario = ((reader["cenv_nombres_des"] != DBNull.Value) ? (string)reader["cenv_nombres_des"] : "") + " " + ((reader["cenv_apellidos_des"] != DBNull.Value) ? (string)reader["cenv_apellidos_des"] : "");
            this.origen = (reader["rut_origen"] != DBNull.Value) ? (string)reader["rut_origen"] : null;
            this.destino= (reader["rut_destino"] != DBNull.Value) ? (string)reader["rut_destino"] : null;
            this.socio= (reader["razonsocio"] != DBNull.Value) ? (string)reader["razonsocio"] : null;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select " +
                        "    g.com_codigo as codigoguia, " +
                        "	g.com_fecha as fecha, " +
                        "	g.com_doctran as guia, " +
                        "	g.com_codclipro, " +
                        "   tot_total as total,"+
                        "   per.per_razon as razon, "+
                        "   cenv_nombres_des, " +
                        "   cenv_apellidos_des, " +
                        "   soc.per_razon as razonsocio, "+
                        "   rut_origen, "+
                        "   rut_destino, " +
                        "	p.com_doctran as planilla, " +
                        "	f.com_doctran as factura, " +
                        "	g.crea_usr as usuario, " +
                        "	'' " +
                        "from comprobante g " +
                        "    left join ccomdoc on cdoc_empresa = g.com_empresa and cdoc_comprobante = g.com_codigo " +
                        "    left join ccomenv on cenv_empresa = g.com_empresa and cenv_comprobante = g.com_codigo " +
                        "    left join ruta on cenv_empresa = rut_empresa and cenv_ruta = rut_codigo " +
                        "    left join persona per on per.per_empresa = g.com_empresa and per.per_codigo = g.com_codclipro " +
                        "    left join total on tot_empresa = g.com_empresa and tot_comprobante = g.com_codigo " +
                        "    left JOIN persona soc ON cenv_socio = soc.per_codigo and cenv_empresa=soc.per_empresa " +
                        "    left join planillacli on g.com_empresa = plc_empresa and g.com_codigo = plc_comprobante " +
                        "    left join comprobante p on p.com_empresa = plc_empresa and p.com_codigo = plc_comprobante_pla " +
                        "    left join planillacomprobante on p.com_empresa = pco_empresa and p.com_codigo = pco_comprobante_pla " +
                        "    left join comprobante f on f.com_empresa = pco_empresa and f.com_codigo = pco_comprobante_fac " +
                        //"where g.com_tipodoc = 13 and g.com_fecha between '01/04/2016' and '01/05/2016' and g.com_estado  in (1,2) " +
                        //"order by g.com_doctran";
                        "";
            return sql;
        }

        public List<vGuiaPlanilla> GetStruc()
        {
            return new List<vGuiaPlanilla>();
        }

    }

    
}

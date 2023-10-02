using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vGuiasFacturas
    {

        public Int64? codigo { get; set; }
        public DateTime? fecha { get; set; }
        public string numero { get; set; }       
        public string ruccliente { get; set; }
        public string razoncliente { get; set; }
        public string destinatario { get; set; }       
        public string origen { get; set; }
        public string destino { get; set; }
        public decimal? total { get; set; }
        public string usuario { get; set; }
        public string socio { get; set; }

        public int? estado{ get; set; }
        public string strestado { get; set; }

        #region Constructors


        public vGuiasFacturas()
        {

        }



        public vGuiasFacturas(IDataReader reader)
        {
            this.codigo= (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.numero = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.ruccliente = (reader["ruccliente"] != DBNull.Value) ? (string)reader["ruccliente"] : null;
            this.razoncliente = (reader["razoncliente"] != DBNull.Value) ? (string)reader["razoncliente"] : null;
            this.total = (reader["total"] != DBNull.Value) ? (decimal?)reader["total"] : null;
            this.usuario = (reader["usuario"] != DBNull.Value) ? (string)reader["usuario"] : null;
            this.destinatario = ((reader["cenv_nombres_des"] != DBNull.Value) ? (string)reader["cenv_nombres_des"] : "") + " " + ((reader["cenv_apellidos_des"] != DBNull.Value) ? (string)reader["cenv_apellidos_des"] : "");
            this.origen = (reader["rut_origen"] != DBNull.Value) ? (string)reader["rut_origen"] : null;
            this.destino = (reader["rut_destino"] != DBNull.Value) ? (string)reader["rut_destino"] : null;
            this.socio = (reader["razonsocio"] != DBNull.Value) ? (string)reader["razonsocio"] : null;
            this.estado = (reader["com_estado"] != DBNull.Value) ? (int?)reader["com_estado"] : null;
            this.strestado = (estado ?? 0) == 1 ? "GRABADO" : "MAYORIZADO";
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select " +
                        "    c.com_codigo, c.com_doctran, c.com_fecha, c.com_codclipro, c.crea_usr usuario ,c.com_estado, " +
                        "    per.per_ciruc ruccliente, per.per_razon razoncliente," + "" +
                        "    cenv_nombres_des, cenv_apellidos_des,soc.per_razon as razonsocio,rut_origen,rut_destino,tot_total as total, " +
                        "	'' " +
                        "from comprobante c " +
                        "    left join ccomdoc on cdoc_empresa = c.com_empresa and cdoc_comprobante = c.com_codigo " +
                        "    left join ccomenv on cenv_empresa = c.com_empresa and cenv_comprobante = c.com_codigo  " +
                        "    left join ruta on cenv_empresa = rut_empresa and cenv_ruta = rut_codigo " +
                        "    left join persona per on per.per_empresa = c.com_empresa and per.per_codigo = c.com_codclipro " +
                        "    left join total on tot_empresa = c.com_empresa and tot_comprobante = c.com_codigo " +
                        "    left JOIN persona soc ON cenv_socio = soc.per_codigo and cenv_empresa=soc.per_empresa  " +
                        //"where g.com_tipodoc = 13 and g.com_fecha between '01/04/2016' and '01/05/2016' and g.com_estado  in (1,2) " +
                        //"order by g.com_doctran";
                        "";
            return sql;
        }


        public List<vGuiasFacturas> GetStruc()
        {
            return new List<vGuiasFacturas>();
        }
    }
}

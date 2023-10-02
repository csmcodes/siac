using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;
using System.Reflection;

namespace BusinessObjects
{
    public class vComprobanteHojaRuta
    {
        public Int32? empresa { get; set; }
        public Int64? codigocomp { get; set; }
        public DateTime? fechacomp { get; set; }
        public string doctrancomp { get; set; }
        public Int32? estadocomp { get; set; }
        public Int32? clientecomp { get; set; }
        public String clientenom { get; set; }
        public String clienteciruc { get; set; }
        public String politicanom { get; set; }
        public String remitenteciruc { get; set; }
        public String remitentenom { get; set; }
        public String destinatariociruc { get; set; }
        public String destinatarionom { get; set; }

        public Decimal? subtotal0 { get; set; }
        public Decimal? transporte { get; set; }
        public Decimal? subtotal { get; set; }
        public Decimal? seguro { get; set; }
        public Decimal? impuesto { get; set; }        
        public Decimal? tseguro { get; set; }        
        public Decimal? porc_seguro { get; set; }
        public Decimal? total { get; set; }

        public Int64? codigohr{ get; set; }
        public DateTime? fechahr{ get; set; }
        public string doctranhr{ get; set; }

        public Int32? sociohr{ get; set; }
        public String socionom { get; set; }
        public String sociociruc { get; set; }

        public Int32? vehiculo { get; set; }
        public String vehiculoplaca { get; set; }
        public String vehiculodisco { get; set; }

        public Int32? ruta { get; set; }
        public String rutaorigen { get; set; }
        public String rutadestino { get; set; }


        public vComprobanteHojaRuta()
        {

        }

        public vComprobanteHojaRuta(IDataReader reader)
        {
            this.empresa = (reader["empresa"] != DBNull.Value) ? (Int32?)reader["empresa"] : null;
            this.codigocomp = (reader["codigocomp"] != DBNull.Value) ? (Int64?)reader["codigocomp"] : null;
            this.doctrancomp = (reader["doctrancomp"] != DBNull.Value) ? (string)reader["doctrancomp"] : null;
            this.fechacomp = (reader["fechacomp"] != DBNull.Value) ? (DateTime?)reader["fechacomp"] : null;
            this.estadocomp = (reader["estadocomp"] != DBNull.Value) ? (Int32?)reader["estadocomp"] : null;

            this.clientecomp = (reader["clientecomp"] != DBNull.Value) ? (Int32?)reader["clientecomp"] : null;
            this.clienteciruc = reader["clienteciruc"].ToString();            
            this.clientenom = reader["clientenom"].ToString();

            this.politicanom= reader["pol_nombre"].ToString();

            this.remitenteciruc = reader["cenv_ciruc_rem"].ToString();
            this.remitentenom = reader["cenv_nombres_rem"].ToString();
            this.destinatariociruc = reader["cenv_ciruc_des"].ToString();
            this.destinatarionom = reader["cenv_nombres_des"].ToString();


            this.subtotal0 = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
            this.transporte = (reader["tot_transporte"] != DBNull.Value) ? (Decimal?)reader["tot_transporte"] : null;
            this.subtotal = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
            this.seguro = (reader["tot_vseguro"] != DBNull.Value) ? (Decimal?)reader["tot_vseguro"] : null;
            this.impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
            this.tseguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
            this.porc_seguro = (reader["tot_porc_seguro"] != DBNull.Value) ? (Decimal?)reader["tot_porc_seguro"] : null;            
            this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;

            this.codigohr= (reader["codigohr"] != DBNull.Value) ? (Int64?)reader["codigohr"] : null;
            this.doctranhr= (reader["doctranhr"] != DBNull.Value) ? (string)reader["doctranhr"] : null;
            this.fechahr= (reader["fechahr"] != DBNull.Value) ? (DateTime?)reader["fechahr"] : null;

            this.sociohr = (reader["sociohr"] != DBNull.Value) ? (Int32?)reader["sociohr"] : null;
            this.sociociruc = reader["sociociruc"].ToString();
            this.socionom = reader["socionom"].ToString();

            this.vehiculo = (reader["veh_codigo"] != DBNull.Value) ? (Int32?)reader["veh_codigo"] : null;
            this.vehiculoplaca = reader["veh_placa"].ToString();
            this.vehiculodisco = reader["veh_disco"].ToString();


            this.ruta = (reader["rut_codigo"] != DBNull.Value) ? (Int32?)reader["rut_codigo"] : null;
            this.rutadestino = reader["rut_destino"].ToString();
            this.rutaorigen= reader["rut_origen"].ToString();



        }

        public string GetSQL()
        {
            string sql = "select fg.com_empresa empresa, fg.com_codigo codigocomp, fg.com_fecha fechacomp, fg.com_doctran doctrancomp, fg.com_estado estadocomp, cli.per_codigo clientecomp, cli.per_ciruc clienteciruc, cli.per_razon clientenom, pol_nombre, " +
                         "   cenv_ciruc_rem,cenv_nombres_rem, cenv_ciruc_des, cenv_nombres_des, " +
                         "   tot_subtot_0, tot_transporte, tot_subtotal, tot_tseguro,tot_vseguro,tot_porc_seguro, tot_timpuesto, tot_total,  " +
                         "   hr.com_codigo codigohr, hr.com_doctran doctranhr, hr.com_fecha fechahr, soc.per_codigo sociohr, soc.per_ciruc sociociruc,  soc.per_razon socionom, " + 
                         "   veh_codigo, veh_placa, veh_disco, rut_codigo, rut_origen, rut_destino " +
                         "   from " +
                         "   comprobante fg " +
                         "   left join persona cli on cli.per_codigo = fg.com_codclipro and cli.per_empresa = fg.com_empresa " +
                         "   left join ccomdoc on fg.com_empresa = cdoc_empresa and fg.com_codigo = cdoc_comprobante " +
                         "   left join politica on pol_empresa = cdoc_empresa and pol_codigo = cdoc_politica " +
                         "   left join ccomenv on fg.com_empresa = cenv_empresa and fg.com_codigo = cenv_comprobante " +
                         "   left join total on fg.com_empresa = tot_empresa and fg.com_codigo = tot_comprobante " +
                         "   left join rutaxfactura on rfac_empresa = fg.com_empresa and rfac_comprobantefac = fg.com_codigo " +
                         "   left join comprobante hr on hr.com_empresa = rfac_empresa and hr.com_codigo = rfac_comprobanteruta " +
                         "   left join persona soc on soc.per_codigo = hr.com_codclipro and soc.per_empresa = hr.com_empresa " +
                         "   left join vehiculo on veh_empresa = hr.com_empresa and veh_codigo = hr.com_vehiculo " +
                         "   left join ruta on rut_empresa = hr.com_empresa and rut_codigo = hr.com_ruta ";

            return sql;
        }

        public List<vComprobanteHojaRuta> GetStruc()
        {
            return new List<vComprobanteHojaRuta>();
        }

    }
}

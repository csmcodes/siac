using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vHojadeRuta
    {
        public Int64? codigocabecera { get; set; }
        public string doctrancabecera { get; set; }
        public DateTime? fechacabecera { get; set; }
        
        public int? codigovehiculo { get; set; }
        public string idvehiculo { get; set; }
        public string placavehiculo { get; set; }
        public string discovehiculo { get; set; }

        public int? codigosocio { get; set; }
        public string idsocio { get; set; }
        public string nombresocio { get; set; }
        public string apellidosocio { get; set; }

        public int? codigochofer { get; set; }
        public string idchofer { get; set; }
        public string nombrechofer { get; set; }
        public string apellidochofer { get; set; }
        
        public Int64? codigodetalle { get; set; }
        public string nombredestinatario { get; set; }
        public string apellidosdestinatario { get; set; }
        public string nombreremitente { get; set; }
        public string apellidoremitente { get; set; }
        public decimal? subtotalcabecera { get; set; }
        public decimal? subtotal12cabecera { get; set; }
        public decimal? totalcabecera { get; set; }
        public decimal? impuestocabecera { get; set; }
        public decimal? segurocabecera { get; set; }
        public decimal? transportecabecera { get; set; }
        public string doctrandetalle { get; set; }
        public DateTime? fechadetalle { get; set; }
        public string ciruccliente { get; set; }
        public string nombrecliente { get; set; }
        public string apellidocliente { get; set; }
        public decimal? bultosdetalle { get; set; }
        public decimal? itemsdetalle { get; set; }
        public decimal? subtotaldetalle { get; set; }
        public decimal? subtotal12detalle { get; set; }        
        public decimal? totaldetalle { get; set; }
        public decimal? impuestodetalle { get; set; }
        public decimal? segurodetalle { get; set; }
        public decimal? transportedetalle { get; set; }
        public string nombreruta { get; set; }
        public decimal? porcentajeruta { get; set; }
        public string idpolitica { get; set; }
        public string nombrepolitica { get; set; }

        public string usrid { get; set; }
        public string usrnombres { get; set; }

        public decimal? cancela { get; set; }
        public decimal? cancelasocio { get; set; }



        #region Constructors


        public vHojadeRuta()
        {

        }



        public vHojadeRuta(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;
            this.codigodetalle = (reader["codigodetalle"] != DBNull.Value) ? (Int64?)reader["codigodetalle"] : null;
            this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
            this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
            this.codigovehiculo = (reader["codigovehiculo"] != DBNull.Value) ? (int?)reader["codigovehiculo"] : null;
            this.idvehiculo = (reader["idvehiculo"] != DBNull.Value) ? (string)reader["idvehiculo"] : null;
            this.placavehiculo = (reader["placavehiculo"] != DBNull.Value) ? (string)reader["placavehiculo"] : null;
            this.discovehiculo = (reader["discovehiculo"] != DBNull.Value) ? (string)reader["discovehiculo"] : null;
            this.codigosocio = (reader["codigosocio"] != DBNull.Value) ? (int?)reader["codigosocio"] : null;
            this.idsocio = (reader["idsocio"] != DBNull.Value) ? (string)reader["idsocio"] : null;
            this.nombresocio = (reader["nombresocio"] != DBNull.Value) ? (string)reader["nombresocio"] : null;
            this.apellidosocio = (reader["apellidosocio"] != DBNull.Value) ? (string)reader["apellidosocio"] : null;
            this.codigochofer = (reader["codigochofer"] != DBNull.Value) ? (int?)reader["codigochofer"] : null;
            this.idchofer = (reader["idchofer"] != DBNull.Value) ? (string)reader["idchofer"] : null;
            this.nombrechofer = (reader["nombrechofer"] != DBNull.Value) ? (string)reader["nombrechofer"] : null;
            this.apellidochofer = (reader["apellidochofer"] != DBNull.Value) ? (string)reader["apellidochofer"] : null;
            this.nombrechofer = (reader["nombrechofer"] != DBNull.Value) ? (string)reader["nombrechofer"] : null;
            this.apellidochofer = (reader["apellidochofer"] != DBNull.Value) ? (string)reader["apellidochofer"] : null;
            this.nombredestinatario = (reader["nombredestinatario"] != DBNull.Value) ? (string)reader["nombredestinatario"] : null;
            this.apellidosdestinatario = (reader["apellidosdestinatario"] != DBNull.Value) ? (string)reader["apellidosdestinatario"] : null;
            this.nombreremitente = (reader["nombreremitente"] != DBNull.Value) ? (string)reader["nombreremitente"] : null;
            this.apellidoremitente = (reader["apellidoremitente"] != DBNull.Value) ? (string)reader["apellidoremitente"] : null;
            this.subtotalcabecera = (reader["subtotalcabecera"] != DBNull.Value) ? (decimal?)reader["subtotalcabecera"] : null;
            this.subtotal12cabecera = (reader["subtotal12cabecera"] != DBNull.Value) ? (decimal?)reader["subtotal12cabecera"] : null;
            this.totalcabecera = (reader["totalcabecera"] != DBNull.Value) ? (decimal?)reader["totalcabecera"] : null;
            this.impuestocabecera = (reader["impuestocabecera"] != DBNull.Value) ? (decimal?)reader["impuestocabecera"] : null;
            this.segurocabecera = (reader["segurocabecera"] != DBNull.Value) ? (decimal?)reader["segurocabecera"] : null;
            this.transportecabecera = (reader["transportecabecera"] != DBNull.Value) ? (decimal?)reader["transportecabecera"] : null;
            this.doctrandetalle = (reader["doctrandetalle"] != DBNull.Value) ? (string)reader["doctrandetalle"] : null;
            this.fechadetalle = (reader["fechadetalle"] != DBNull.Value) ? (DateTime?)reader["fechadetalle"] : null;
            this.ciruccliente = (reader["ciruccliente"] != DBNull.Value) ? (string)reader["ciruccliente"] : null;
            this.nombrecliente = (reader["nombrecliente"] != DBNull.Value) ? (string)reader["nombrecliente"] : null;
            this.apellidocliente = (reader["apellidocliente"] != DBNull.Value) ? (string)reader["apellidocliente"] : null;
            this.bultosdetalle = (reader["bultosdetalle"] != DBNull.Value) ? (decimal?)reader["bultosdetalle"] : null;
            this.subtotaldetalle = (reader["subtotaldetalle"] != DBNull.Value) ? (decimal?)reader["subtotaldetalle"] : null;
            this.subtotal12detalle = (reader["subtotal12detalle"] != DBNull.Value) ? (decimal?)reader["subtotal12detalle"] : null;
            this.totaldetalle = (reader["totaldetalle"] != DBNull.Value) ? (decimal?)reader["totaldetalle"] : null;
            this.impuestodetalle = (reader["impuestodetalle"] != DBNull.Value) ? (decimal?)reader["impuestodetalle"] : null;
            this.segurodetalle = (reader["segurodetalle"] != DBNull.Value) ? (decimal?)reader["segurodetalle"] : null;
            this.transportedetalle = (reader["transportedetalle"] != DBNull.Value) ? (decimal?)reader["transportedetalle"] : null;
            this.nombreruta = (reader["nombreruta"] != DBNull.Value) ? (string)reader["nombreruta"] : null;
            this.porcentajeruta = (reader["porcentajeruta"] != DBNull.Value) ? (decimal?)reader["porcentajeruta"] : null;
            this.idpolitica = (reader["idpolitica"] != DBNull.Value) ? (string)reader["idpolitica"] : null;
            this.nombrepolitica = (reader["nombrepolitica"] != DBNull.Value) ? (string)reader["nombrepolitica"] : null;
            this.usrid = (reader["usr_id"] != DBNull.Value) ? (string)reader["usr_id"] : null;
            this.usrnombres = (reader["usr_nombres"] != DBNull.Value) ? (string)reader["usr_nombres"] : null;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select " +
    "cabecera.com_codigo codigocabecera,	" +
    "cabecera.com_doctran  doctrancabecera," +
    "cabecera.com_fecha		fechacabecera," +
    "vehiculo.veh_codigo    codigovehiculo," +
    "vehiculo.veh_id        idvehiculo," +
    "vehiculo.veh_disco       discovehiculo," +
    "vehiculo.veh_placa       placavehiculo," +
    "socio.per_codigo		codigosocio," +
    "socio.per_id		idsocio," +
    "socio.per_nombres		nombresocio," +
    "socio.per_apellidos		apellidosocio," +
    "chofer.per_codigo		codigochofer," +
    "chofer.per_id		idchofer," +
    "chofer.per_nombres		nombrechofer," +
    "chofer.per_apellidos	apellidochofer," +
    "ccomenv.cenv_nombres_des	nombredestinatario," +
    "ccomenv.cenv_apellidos_des  apellidosdestinatario," +
    "ccomenv.cenv_nombres_rem	nombreremitente," +
    "ccomenv.cenv_apellidos_rem	apellidoremitente," +
    "cabecaratotal.tot_subtot_0		subtotalcabecera," +
    "cabecaratotal.tot_subtotal		subtotal12cabecera," +
    "cabecaratotal.tot_total		totalcabecera," +
    "cabecaratotal.tot_timpuesto impuestocabecera," +
    "cabecaratotal.tot_tseguro   segurocabecera," +
    "cabecaratotal.tot_transporte transportecabecera," +
    "detalle.com_codigo           codigodetalle," +
    "detalle.com_doctran           doctrandetalle," +
    "detalle.com_fecha           fechadetalle," +
    "cliente.per_nombres		nombrecliente," +
    "cliente.per_apellidos		apellidocliente," +
    "cliente.per_ciruc		ciruccliente," +
    "(select SUM(ddoc_cantidad) from  dcomdoc where dcomdoc.ddoc_comprobante=detalle.com_codigo and detalle.com_empresa =dcomdoc.ddoc_empresa ) bultosdetalle," +
    "detalletotal.tot_subtot_0   subtotaldetalle," +
    "detalletotal.tot_subtotal   subtotal12detalle," +
    "detalletotal.tot_total   totaldetalle," +
    "detalletotal.tot_timpuesto  impuestodetalle," +
    "detalletotal.tot_tseguro segurodetalle," +
    "detalletotal.tot_transporte transportedetalle," +
    "ruta.rut_nombre nombreruta," +
    "ruta.rut_porcentaje porcentajeruta,"+
    "politica.pol_id idpolitica, " +
    "politica.pol_nombre nombrepolitica, " +
    "usr_id," +
    "usr_nombres " +

"from rutaxfactura " +
"inner join comprobante cabecera on cabecera.com_codigo =rutaxfactura.rfac_comprobanteruta  and  cabecera.com_empresa =rutaxfactura.rfac_empresa " +
"left join comprobante detalle on detalle.com_codigo =rutaxfactura.rfac_comprobantefac and  detalle.com_empresa =rutaxfactura.rfac_empresa " +
"left join persona cliente on detalle.com_codclipro = cliente.per_codigo and detalle.com_empresa = cliente.per_empresa " +
"left join ccomenv on ccomenv.cenv_comprobante=detalle.com_codigo and detalle.com_empresa =ccomenv.cenv_empresa " +
"left join persona socio on socio.per_codigo=ccomenv.cenv_socio and socio.per_empresa =ccomenv.cenv_empresa " +
"left join vehiculo on vehiculo.veh_codigo=ccomenv.cenv_vehiculo and vehiculo.veh_empresa =ccomenv.cenv_empresa " +
"left join persona chofer on chofer.per_codigo=ccomenv.cenv_chofer and chofer.per_empresa =ccomenv.cenv_empresa " +
"inner join total cabecaratotal on cabecaratotal.tot_comprobante=cabecera.com_codigo and cabecaratotal.tot_empresa = cabecera.com_empresa " +
"left join total detalletotal on detalletotal.tot_comprobante=detalle.com_codigo and detalletotal.tot_empresa =detalle.com_empresa " +
//"left join ruta  on ruta.rut_codigo=ccomenv.cenv_ruta and ruta.rut_empresa =ccomenv.cenv_empresa " +
"left join ruta  on ruta.rut_codigo=cabecera.com_ruta and ruta.rut_empresa =cabecera.com_empresa " +
"left join ccomdoc  on ccomdoc.cdoc_comprobante=detalle.com_codigo and ccomdoc.cdoc_empresa=detalle.com_empresa " +
"left join politica  on ccomdoc.cdoc_politica=politica.pol_codigo and ccomdoc.cdoc_empresa=politica.pol_empresa " +
"left join usuario on detalle.crea_usr = usr_id ";


            return sql;
        }
        

        public List<vHojadeRuta> GetStruc()
        {
            return new List<vHojadeRuta>();
        }


    }
}

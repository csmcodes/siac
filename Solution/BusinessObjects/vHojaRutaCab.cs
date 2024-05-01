using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vHojaRutaCab
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

        public decimal? subtotalcabecera { get; set; }
        public decimal? subtotal12cabecera { get; set; }
        public decimal? totalcabecera { get; set; }
        public decimal? impuestocabecera { get; set; }
        public decimal? segurocabecera { get; set; }
        public decimal? transportecabecera { get; set; }
        public string nombreruta { get; set; }
        public decimal? porcentajeruta { get; set; }
        public string origenruta { get; set; }
        public string destinoruta { get; set; }



        #region Constructors


        public vHojaRutaCab()
        {

        }



        public vHojaRutaCab(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;
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
            this.subtotalcabecera = (reader["subtotalcabecera"] != DBNull.Value) ? (decimal?)reader["subtotalcabecera"] : null;
            this.subtotal12cabecera = (reader["subtotal12cabecera"] != DBNull.Value) ? (decimal?)reader["subtotal12cabecera"] : null;
            this.totalcabecera = (reader["totalcabecera"] != DBNull.Value) ? (decimal?)reader["totalcabecera"] : null;
            this.impuestocabecera = (reader["impuestocabecera"] != DBNull.Value) ? (decimal?)reader["impuestocabecera"] : null;
            this.segurocabecera = (reader["segurocabecera"] != DBNull.Value) ? (decimal?)reader["segurocabecera"] : null;
            this.transportecabecera = (reader["transportecabecera"] != DBNull.Value) ? (decimal?)reader["transportecabecera"] : null;
            this.nombreruta = (reader["nombreruta"] != DBNull.Value) ? (string)reader["nombreruta"] : null;
            this.porcentajeruta = (reader["porcentajeruta"] != DBNull.Value) ? (decimal?)reader["porcentajeruta"] : null;
            this.origenruta = (reader["origenruta"] != DBNull.Value) ? (string)reader["origenruta"] : null;
            this.destinoruta = (reader["destinoruta"] != DBNull.Value) ? (string)reader["destinoruta"] : null;
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
    "cabecaratotal.tot_subtot_0		subtotalcabecera," +
    "cabecaratotal.tot_subtotal		subtotal12cabecera," +
    "cabecaratotal.tot_total		totalcabecera," +
    "cabecaratotal.tot_timpuesto impuestocabecera," +
    "cabecaratotal.tot_tseguro   segurocabecera," +
    "cabecaratotal.tot_transporte transportecabecera," +
    "ruta.rut_nombre nombreruta," +
    "ruta.rut_porcentaje porcentajeruta," +
    "ruta.rut_origen origenruta, " +
    "ruta.rut_destino destinoruta " +

"from rutaxfactura " +
"inner join comprobante cabecera on cabecera.com_codigo =rutaxfactura.rfac_comprobanteruta  and  cabecera.com_empresa =rutaxfactura.rfac_empresa " +
"left join comprobante detalle on detalle.com_codigo =rutaxfactura.rfac_comprobantefac and  detalle.com_empresa =rutaxfactura.rfac_empresa " +
"left join ccomenv on ccomenv.cenv_comprobante=detalle.com_codigo and detalle.com_empresa =ccomenv.cenv_empresa " +
"left join persona socio on socio.per_codigo=ccomenv.cenv_socio and socio.per_empresa =ccomenv.cenv_empresa " +
"left join vehiculo on vehiculo.veh_codigo=ccomenv.cenv_vehiculo and vehiculo.veh_empresa =ccomenv.cenv_empresa " +
"left join persona chofer on chofer.per_codigo=ccomenv.cenv_chofer and chofer.per_empresa =ccomenv.cenv_empresa " +
"inner join total cabecaratotal on cabecaratotal.tot_comprobante=cabecera.com_codigo and cabecaratotal.tot_empresa = cabecera.com_empresa " +
"left join ruta  on ruta.rut_codigo=cabecera.com_ruta and ruta.rut_empresa =cabecera.com_empresa " +
" %whereclause% " +
" group by cabecera.com_codigo, cabecera.com_doctran, cabecera.com_fecha,vehiculo.veh_codigo,vehiculo.veh_id,vehiculo.veh_disco,vehiculo.veh_placa,socio.per_codigo,socio.per_id,socio.per_nombres,socio.per_apellidos,chofer.per_codigo,chofer.per_id,chofer.per_nombres,chofer.per_apellidos,cabecaratotal.tot_subtot_0,cabecaratotal.tot_subtotal,cabecaratotal.tot_total,cabecaratotal.tot_timpuesto,cabecaratotal.tot_tseguro,cabecaratotal.tot_transporte,ruta.rut_nombre,ruta.rut_porcentaje,ruta.rut_origen,ruta.rut_destino ";


            return sql;
        }


        public List<vHojaRutaCab> GetStruc()
        {
            return new List<vHojaRutaCab>();
        }


    }
}

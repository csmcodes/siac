using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
   public  class vHojaRutaReporte
    {

        public Int64? codigocabecera { get; set; }
        public string doctrancabecera { get; set; }
        public DateTime? fechacabecera { get; set; }

        public int? codigovehiculo { get; set; }
        public string idvehiculo { get; set; }
        public string placavehivulo { get; set; }
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
        public string nombrecliente { get; set; }
        public string apellidocliente { get; set; }
        public decimal? bultosdetalle { get; set; }
        public decimal? subtotaldetalle { get; set; }
        public decimal? subtotal12detalle { get; set; }
        public decimal? totaldetalle { get; set; }
        public decimal? impuestodetalle { get; set; }
        public decimal? segurodetalle { get; set; }
        public decimal? transportedetalle { get; set; }
        public string nombreruta { get; set; }
        public string origenruta { get; set; }
        public string destinoruta { get; set; }
        public decimal? porcentajeruta { get; set; }
        public string idpolitica { get; set; }
        public string nombrepolitica { get; set; }

        public string usrid { get; set; }
        public string usrnombres { get; set; }
        public string nombreclientecab { get; set; }
        public string apellidoclientecab { get; set; }


        public int? codigovehiculocab { get; set; }
        public string idvehiculocab { get; set; }
        public string placavehiculocab { get; set; }
        public string discovehiculocab { get; set; }

        public vHojaRutaReporte()
        {


        }

        public vHojaRutaReporte(IDataReader reader)
        {
            this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;
            this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
            this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
            this.porcentajeruta = (reader["porcentajeruta"] != DBNull.Value) ? (decimal?)reader["porcentajeruta"] : null;
            this.codigosocio = (reader["codigosocio"] != DBNull.Value) ? (Int32?)reader["codigosocio"] : null;
            this.nombreclientecab = (reader["nombreclientecab"] != DBNull.Value) ? (string)reader["nombreclientecab"] : null;
            this.apellidoclientecab = (reader["apellidoclientecab"] != DBNull.Value) ? (string)reader["apellidoclientecab"] : null;
            this.codigovehiculocab = (reader["codigovehiculocab"] != DBNull.Value) ? (int?)reader["codigovehiculocab"] : null;
            this.idvehiculocab = (reader["idvehiculocab"] != DBNull.Value) ? (string)reader["idvehiculocab"] : null;
            this.placavehiculocab = (reader["placavehiculocab"] != DBNull.Value) ? (string)reader["placavehiculocab"] : null;
            this.discovehiculocab = (reader["discovehiculocab"] != DBNull.Value) ? (string)reader["discovehiculocab"] : null;
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
            this.origenruta = (reader["origenruta"] != DBNull.Value) ? (string)reader["origenruta"] : null;
            this.destinoruta = (reader["destinoruta"] != DBNull.Value) ? (string)reader["destinoruta"] : null;
        }

       

        public string GetSQL()
        {
            string sql = "select " +
    "cabecera.com_codigo codigocabecera,	" +
    "cabecera.com_doctran  doctrancabecera," +
    "cabecera.com_fecha		fechacabecera," +
     "vehiculocab.veh_codigo    codigovehiculocab," +
    "vehiculocab.veh_id        idvehiculocab," +
    "vehiculocab.veh_disco       discovehiculocab," +
    "vehiculocab.veh_placa       placavehiculocab," +
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
    "clientecab.per_codigo codigosocio," +
    "clientecab.per_nombres		nombreclientecab," +
    "clientecab.per_apellidos		apellidoclientecab," +
    "ruta.rut_nombre nombreruta," +
    "ruta.rut_origen origenruta," +
    "ruta.rut_destino destinoruta," +
    "ruta.rut_porcentaje porcentajeruta " +

"from comprobante cabecera " +
"left join persona clientecab on cabecera.com_codclipro = clientecab.per_codigo and cabecera.com_empresa = clientecab.per_empresa " +
"left join vehiculo vehiculocab on vehiculocab.veh_codigo=cabecera.com_vehiculo and vehiculocab.veh_empresa =cabecera.com_empresa " +             
"left join persona chofer on chofer.per_codigo=vehiculocab.veh_chofer1 and chofer.per_empresa =vehiculocab.veh_empresa  " +
"inner join total cabecaratotal on cabecaratotal.tot_comprobante=cabecera.com_codigo and cabecaratotal.tot_empresa = cabecera.com_empresa " +           
"left join ruta  on ruta.rut_codigo=cabecera.com_ruta and ruta.rut_empresa =cabecera.com_empresa " +               
"";
//"left join usuario on detalle.crea_usr = usr_id ";


            return sql;
        }
        

        public List<vHojaRutaReporte> GetStruc()
        {
            return new List<vHojaRutaReporte>();
        }

    }

}

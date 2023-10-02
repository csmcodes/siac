using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDetalle
    {
        public Int64? codigo { get; set; }
        public string doctran { get; set; }
        public DateTime? fecha { get; set; }

        public string nombrecliente { get; set; }
        public string apellidocliente { get; set; }
        public string razoncliente { get; set; }

        public string nombredestinatario { get; set; }
        public string apellidosdestinatario { get; set; }
        public string nombreremitente { get; set; }
        public string apellidoremitente { get; set; }


        public int? codigosocio { get; set; }
        public string idsocio { get; set; }
        public string nombresocio { get; set; }
        public string apellidosocio { get; set; }

         public string nombreruta { get; set; }
        public string destinoruta { get; set; }


        public decimal? subtotal{ get; set; }
        public decimal? subtotal12{ get; set; }
        public decimal? total{ get; set; }
        public decimal? impuesto{ get; set; }
        public decimal? seguro{ get; set; }
        public decimal? transporte{ get; set; }

       

        public decimal? bultosdetalle { get; set; }
        public string itemsdetalle { get; set; }

       
        public string usrid { get; set; }
        public string usrnombres { get; set; }



        #region Constructors


        public vDetalle()
        {

        }



        public vDetalle(IDataReader reader)
        {
            this.codigo= (reader["codigo"] != DBNull.Value) ? (Int64?)reader["codigo"] : null;
            this.doctran= (reader["doctran"] != DBNull.Value) ? (string)reader["doctran"] : null;
            this.fecha= (reader["fecha"] != DBNull.Value) ? (DateTime?)reader["fecha"] : null;

            this.codigosocio = (reader["codigosocio"] != DBNull.Value) ? (int?)reader["codigosocio"] : null;
            this.idsocio = (reader["idsocio"] != DBNull.Value) ? (string)reader["idsocio"] : null;
            this.nombresocio = (reader["nombresocio"] != DBNull.Value) ? (string)reader["nombresocio"] : null;
            this.apellidosocio = (reader["apellidosocio"] != DBNull.Value) ? (string)reader["apellidosocio"] : null;
            this.nombredestinatario = (reader["nombredestinatario"] != DBNull.Value) ? (string)reader["nombredestinatario"] : null;
            this.apellidosdestinatario = (reader["apellidosdestinatario"] != DBNull.Value) ? (string)reader["apellidosdestinatario"] : null;
            this.nombreremitente = (reader["nombreremitente"] != DBNull.Value) ? (string)reader["nombreremitente"] : null;
            this.apellidoremitente = (reader["apellidoremitente"] != DBNull.Value) ? (string)reader["apellidoremitente"] : null;
            this.subtotal= (reader["subtotal"] != DBNull.Value) ? (decimal?)reader["subtotal"] : null;
            this.subtotal12 = (reader["subtotal12"] != DBNull.Value) ? (decimal?)reader["subtotal12"] : null;
            this.total = (reader["total"] != DBNull.Value) ? (decimal?)reader["total"] : null;
            this.impuesto= (reader["impuesto"] != DBNull.Value) ? (decimal?)reader["impuesto"] : null;
            this.seguro= (reader["seguro"] != DBNull.Value) ? (decimal?)reader["seguro"] : null;
            this.transporte= (reader["transporte"] != DBNull.Value) ? (decimal?)reader["transporte"] : null;
            this.nombrecliente = (reader["nombrecliente"] != DBNull.Value) ? (string)reader["nombrecliente"] : null;
            this.apellidocliente = (reader["apellidocliente"] != DBNull.Value) ? (string)reader["apellidocliente"] : null;
            this.razoncliente = (reader["razoncliente"] != DBNull.Value) ? (string)reader["razoncliente"] : null;
            this.bultosdetalle = (reader["bultosdetalle"] != DBNull.Value) ? (decimal?)reader["bultosdetalle"] : null;
            this.itemsdetalle = (reader["itemsdetalle"] != DBNull.Value) ? (string)reader["itemsdetalle"] : null;
            this.nombreruta = (reader["nombreruta"] != DBNull.Value) ? (string)reader["nombreruta"] : null;
            this.usrid = (reader["usr_id"] != DBNull.Value) ? (string)reader["usr_id"] : null;
            this.usrnombres = (reader["usr_nombres"] != DBNull.Value) ? (string)reader["usr_nombres"] : null;
        }

        #endregion

        public string GetSQL()
        {
            string sql = "select " +
    "comprobante.com_codigo codigo, " +
    "comprobante.com_doctran doctran, " +
    "comprobante.com_fecha fecha, " +
    "cliente.per_nombres nombrecliente, " +
    "cliente.per_apellidos apellidocliente, " +
    "cliente.per_razon razoncliente, " +
    "ccomenv.cenv_nombres_des nombredestinatario, " +
    "ccomenv.cenv_apellidos_des apellidosdestinatario, " +
    "ccomenv.cenv_nombres_rem nombreremitente, " +
    "ccomenv.cenv_apellidos_rem apellidoremitente, " +
    "socio.per_codigo codigosocio, " +
    "socio.per_id idsocio, " +
    "socio.per_nombres nombresocio, " +
    "socio.per_apellidos apellidosocio, " +
    "ruta.rut_nombre nombreruta, " +
    "ruta.rut_destino destinoruta, " +
    //"(select SUM(ddoc_cantidad) from dcomdoc where dcomdoc.ddoc_comprobante = comprobante.com_codigo and comprobante.com_empresa = dcomdoc.ddoc_empresa ) bultosdetalle, " +
    "null bultosdetalle, " +
    "null itemsdetalle, "+
    "total.tot_subtot_0 subtotal, " +
    "total.tot_subtotal subtotal12, " +
    "total.tot_total total, " +
    "total.tot_timpuesto impuesto, " +
    "total.tot_tseguro seguro, " +
    "total.tot_transporte transporte, " +
    "usr_id, " +
    "usr_nombres " +
    "from comprobante " +
    "left join persona cliente on comprobante.com_codclipro = cliente.per_codigo and comprobante.com_empresa = cliente.per_empresa " +
    "left join total on tot_comprobante = comprobante.com_codigo and tot_empresa = comprobante.com_empresa " +
    "left join ccomenv on ccomenv.cenv_comprobante = comprobante.com_codigo and comprobante.com_empresa = ccomenv.cenv_empresa " +
    "left join persona socio on socio.per_codigo = ccomenv.cenv_socio and socio.per_empresa = ccomenv.cenv_empresa " +
    "left join ccomdoc on ccomdoc.cdoc_comprobante = comprobante.com_codigo and ccomdoc.cdoc_empresa = comprobante.com_empresa " +
    "left join ruta on ruta.rut_codigo = ccomenv.cenv_ruta and ruta.rut_empresa = ccomenv.cenv_empresa " +
    "left join usuario on comprobante.crea_usr = usr_id " +
    "";
            return sql;
        }


        public string GetSQLDet()
        {
            string sql = "select " +
    "comprobante.com_codigo codigo, " +
    "comprobante.com_doctran doctran, " +
    "comprobante.com_fecha fecha, " +
    "cliente.per_nombres nombrecliente, " +
    "cliente.per_apellidos apellidocliente, " +
    "cliente.per_razon razoncliente, " +
    "ccomenv.cenv_nombres_des nombredestinatario, " +
    "ccomenv.cenv_apellidos_des apellidosdestinatario, " +
    "ccomenv.cenv_nombres_rem nombreremitente, " +
    "ccomenv.cenv_apellidos_rem apellidoremitente, " +
    "socio.per_codigo codigosocio, " +
    "socio.per_id idsocio, " +
    "socio.per_nombres nombresocio, " +
    "socio.per_apellidos apellidosocio, " +
    "ruta.rut_nombre nombreruta, " +
    "ruta.rut_destino destinoruta, " +
    "dcomdoc.ddoc_cantidad bultosdetalle," +
    "dcomdoc.ddoc_observaciones itemsdetalle, " +    
    "total.tot_subtot_0 subtotal, " +
    "total.tot_subtotal subtotal12, " +
    "total.tot_total total, " +
    "total.tot_timpuesto impuesto, " +
    "total.tot_tseguro seguro, " +
    "total.tot_transporte transporte, " +
    "usr_id, " +
    "usr_nombres " +
    "from comprobante " +
    "left join persona cliente on comprobante.com_codclipro = cliente.per_codigo and comprobante.com_empresa = cliente.per_empresa " +
    "left join total on tot_comprobante = comprobante.com_codigo and tot_empresa = comprobante.com_empresa " +
    "left join ccomenv on ccomenv.cenv_comprobante = comprobante.com_codigo and comprobante.com_empresa = ccomenv.cenv_empresa " +
    "left join persona socio on socio.per_codigo = ccomenv.cenv_socio and socio.per_empresa = ccomenv.cenv_empresa " +
    "left join ccomdoc on ccomdoc.cdoc_comprobante = comprobante.com_codigo and ccomdoc.cdoc_empresa = comprobante.com_empresa " +
    "left join dcomdoc  on dcomdoc.ddoc_comprobante = comprobante.com_codigo and dcomdoc.ddoc_empresa = comprobante.com_empresa " +
    "left join ruta on ruta.rut_codigo = ccomenv.cenv_ruta and ruta.rut_empresa = ccomenv.cenv_empresa " +
    "left join usuario on comprobante.crea_usr = usr_id " +
    "";
            return sql;
        }


        public List<vDetalle> GetStruc()
        {
            return new List<vDetalle>();
        }

    }
}

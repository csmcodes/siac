using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vDcancelacion
    {

        public Int64? dca_comprobante { get; set; }
        public int? dca_empresa { get; set; }
        public int? dca_transacc { get; set; }
        public Int64? dca_comprobante_can { get; set; }
        public int? dca_secuencia { get; set; }
        public int? dca_pago { get; set; }
        public decimal? montocancela { get; set; }
        public int? ddo_codclipro { get; set; }
        public decimal? montodocumento { get; set; }
        public decimal? ddo_cancela { get; set; }
        public int? ddo_cancelado { get; set; }
        public string nombres_remfac { get; set; }
        public string apellidos_remfac { get; set; }
        public string ciruc_remfacfac { get; set; }
        public int? cenv_sociofac { get; set; }
        public string alm_nombre { get; set; }
        public string nombresocio { get; set; }
        public string apellidosocio { get; set; }
        public string nombrecliente { get; set; }
        public string apellidocliente { get; set; }
        public string doctrancabecera { get; set; }
        public decimal? totalcabecera { get; set; }
        public DateTime? fechacabecera { get; set; }
        public Int64? codigocabecera { get; set; }
        public DateTime? fechadetalle { get; set; }
        public string doctrandetalle { get; set; }
        public Int64? ddo_comprobante_guia { get; set; }
        public string nombres_remguia { get; set; }
        public string apellidos_remguia { get; set; }
        public string ciruc_remfacguia { get; set; }
        public int? cenv_socioguia { get; set; }
        public string doctranguia { get; set; }




        #region Constructors


        public vDcancelacion()
        {

        }



        public vDcancelacion(IDataReader reader)
        {
            this.dca_comprobante=(reader["dca_comprobante"] != DBNull.Value) ? (Int64?)reader["dca_comprobante"] : null;
            this.dca_empresa = (reader["dca_empresa"] != DBNull.Value) ? (int?)reader["dca_empresa"] : null;
            this.dca_transacc = (reader["dca_transacc"] != DBNull.Value) ? (int?)reader["dca_transacc"] : null;
            this.dca_comprobante_can = (reader["dca_comprobante_can"] != DBNull.Value) ? (Int64?)reader["dca_comprobante_can"] : null;
        this.dca_secuencia = (reader["dca_secuencia"] != DBNull.Value) ? (int?)reader["dca_secuencia"] : null;
        this.dca_pago = (reader["dca_pago"] != DBNull.Value) ? (int?)reader["dca_pago"] : null;       
        this.ddo_codclipro = (reader["ddo_codclipro"] != DBNull.Value) ? (int?)reader["ddo_codclipro"] : null;       
        this.ddo_cancela = (reader["ddo_cancela"] != DBNull.Value) ? (decimal?)reader["ddo_cancela"] : null;
       this.ddo_cancelado = (reader["ddo_cancelado"] != DBNull.Value) ? (int?)reader["ddo_cancelado"] : null;
       this.nombres_remfac = (reader["nombres_remfac"] != DBNull.Value) ? (string)reader["nombres_remfac"] : null;
       this.apellidos_remfac = (reader["apellidos_remfac"] != DBNull.Value) ? (string)reader["apellidos_remfac"] : null;
       this.ciruc_remfacfac = (reader["ciruc_remfacfac"] != DBNull.Value) ? (string)reader["ciruc_remfacfac"] : null;
       this.cenv_sociofac = (reader["cenv_sociofac"] != DBNull.Value) ? (int?)reader["cenv_sociofac"] : null;
       this.alm_nombre = (reader["alm_nombre"] != DBNull.Value) ? (string)reader["alm_nombre"] : null;
       this.nombresocio = (reader["nombresocio"] != DBNull.Value) ? (string)reader["nombresocio"] : null;
       this.apellidosocio = (reader["apellidosocio"] != DBNull.Value) ? (string)reader["apellidosocio"] : null;
       this.nombrecliente = (reader["nombrecliente"] != DBNull.Value) ? (string)reader["nombrecliente"] : null;
       this.apellidocliente = (reader["apellidocliente"] != DBNull.Value) ? (string)reader["apellidocliente"] : null;
       this.doctrancabecera = (reader["doctrancabecera"] != DBNull.Value) ? (string)reader["doctrancabecera"] : null;
       this.totalcabecera = (reader["totalcabecera"] != DBNull.Value) ? (decimal?)reader["totalcabecera"] : null;
       this.fechacabecera = (reader["fechacabecera"] != DBNull.Value) ? (DateTime?)reader["fechacabecera"] : null;
       this.codigocabecera = (reader["codigocabecera"] != DBNull.Value) ? (Int64?)reader["codigocabecera"] : null;
       this.fechadetalle = (reader["fechadetalle"] != DBNull.Value) ? (DateTime?)reader["fechadetalle"] : null;
       this.doctrandetalle = (reader["doctrandetalle"] != DBNull.Value) ? (string)reader["doctrandetalle"] : null;
       this.montodocumento = (reader["montodocumento"] != DBNull.Value) ? (decimal?)reader["montodocumento"] : null;
       this.montocancela = (reader["montocancela"] != DBNull.Value) ? (decimal?)reader["montocancela"] : null;
       this.nombres_remguia = (reader["nombres_remguia"] != DBNull.Value) ? (string)reader["nombres_remguia"] : null;
       this.apellidos_remguia = (reader["apellidos_remguia"] != DBNull.Value) ? (string)reader["apellidos_remguia"] : null;
       this.ciruc_remfacguia = (reader["ciruc_remfacguia"] != DBNull.Value) ? (string)reader["ciruc_remfacguia"] : null;
       this.cenv_socioguia = (reader["cenv_socioguia"] != DBNull.Value) ? (int?)reader["cenv_socioguia"] : null;
       this.ddo_comprobante_guia = (reader["ddo_comprobante_guia"] != DBNull.Value) ? (Int64?)reader["ddo_comprobante_guia"] : null;
         this.doctranguia = (reader["doctranguia"] != DBNull.Value) ? (string)reader["doctranguia"] : null;
        }

        #endregion

        public string  GetSQL()
        {
            string sql = "SELECT   " +
	"dca_comprobante,  "+ 
	"dca_empresa,   "+
	"dca_transacc,   "+
	"dca_comprobante_can,   "+
	"dca_secuencia,        "+
	"dca_pago,   "+
	"dca_monto montocancela,       "+
	"ddo_codclipro,   "+
	"ddo_monto montodocumento,    "+
	"ddo_cancela,   "+
	"ddo_cancelado,   "+
	"enviofacturas.cenv_nombres_rem nombres_remfac,   "+
	"enviofacturas.cenv_apellidos_rem apellidos_remfac,   "+
	"enviofacturas.cenv_ciruc_rem ciruc_remfacfac,   "+
	"enviofacturas.cenv_socio cenv_sociofac,   "+
	"alm_nombre,  "+
	"socio.per_nombres nombresocio,  "+
	"socio.per_apellidos apellidosocio,  "+
	"cliente.per_nombres nombrecliente,  "+
	"cliente.per_apellidos apellidocliente,	  "+
	"cabecera.com_doctran doctrancabecera,  "+
	"cabeceratotal.tot_total totalcabecera,  "+
	"cabecera.com_fecha fechacabecera,  "+
	"cabecera.com_codigo codigocabecera, "+	 
	"detalle.com_fecha fechadetalle,  "+
	"detalle.com_doctran doctrandetalle,  "+
	"ddo_comprobante_guia,	 "+
	"envioguias.cenv_nombres_rem nombres_remguia,   "+
	"envioguias.cenv_apellidos_rem apellidos_remguia,   "+
	"envioguias.cenv_ciruc_rem ciruc_remfacguia,   "+
	"envioguias.cenv_socio cenv_socioguia,   "+
	"guia.com_doctran doctranguia "+
	"FROM    "+
	"dcancelacion    "+
	"INNER JOIN ddocumento ON dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago =ddo_pago    "+
	"left JOIN ccomenv enviofacturas ON dca_empresa = enviofacturas.cenv_empresa and dca_comprobante = enviofacturas.cenv_comprobante    "+
	"INNER JOIN comprobante detalle ON dca_empresa = com_empresa and dca_comprobante = com_codigo    "+
	"INNER JOIN almacen ON dca_empresa = alm_empresa and com_almacen =  alm_codigo    "+
	"left JOIN persona socio ON dca_empresa = socio.per_empresa and socio.per_codigo =  cenv_socio   "+
	"INNER JOIN total detalletotal ON dca_empresa = detalletotal.tot_empresa and detalletotal.tot_comprobante =  dca_comprobante   "+
	"INNER JOIN persona cliente ON dca_empresa = cliente.per_empresa and cliente.per_codigo =  detalle.com_codclipro   "+
	"left JOIN comprobante cabecera ON dca_planilla = cabecera.com_codigo AND dca_empresa = cabecera.com_empresa           "+
	"left JOIN total cabeceratotal ON dca_empresa = cabeceratotal.tot_empresa and cabeceratotal.tot_comprobante =  dca_planilla    "+
	"left JOIN ccomenv envioguias ON dca_empresa = envioguias.cenv_empresa and ddo_comprobante_guia = envioguias.cenv_comprobante "+
	"left JOIN comprobante guia ON dca_empresa = guia.com_empresa and ddo_comprobante_guia = guia.com_codigo ";
      //  "where  dcg_empresa={0} and dcg_planilla={1} order by 	detalle.com_fecha     ";

            return sql;
        }

        public string GetSQL1()
        {

            string sql = "select " +
                "dca_comprobante,  " +
                "dca_empresa,   " +
                "dca_transacc,   " +
                "dca_comprobante_can,   " +
                "dca_secuencia,        " +
                "dca_pago,   " +
                "dca_monto montocancela,       " +
                "ddo_codclipro,   " +
                "ddo_monto montodocumento,    " +
                "ddo_cancela,   " +
                "ddo_cancelado,   " +
                "null nombres_remfac,   " +
                "null apellidos_remfac,   " +
                "null ciruc_remfacfac,   " +
                "null cenv_sociofac,   " +
                "null alm_nombre,  " +
                "null nombresocio,  " +
                "null apellidosocio,  " +
                "null nombrecliente,  " +
                "null apellidocliente,	  " +
                "null doctrancabecera,  " +
                "null totalcabecera,  " +
                "null fechacabecera,  " +
                "null codigocabecera, " +
                "null fechadetalle,  " +
                "null doctrandetalle,  " +
                "null ddo_comprobante_guia,	 " +
                "null nombres_remguia,   " +
                "null apellidos_remguia,   " +
                "null ciruc_remfacguia,   " +
                "null cenv_socioguia,   " +
                "null doctranguia "+
                "FROM " +
                "dcancelacion " +
                " inner join comprobante on dca_empresa = com_empresa and dca_comprobante = com_codigo " +
                " INNER JOIN ddocumento ON dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago = ddo_pago";
               
            

            return sql;
        }

        /*     public string GetCargadasSQL()
             {
                 string sql = "SELECT  " +
             "dca_comprobante,  " +
             "dca_empresa,  " +
             "dca_transacc,  " +
             "dca_comprobante_can,  " +
             "dca_secuencia,     " +
             "dca_pago,  " +
             "dca_monto,     " +
             "ddo_codclipro,  " +
             "ddo_monto,  " +
             "ddo_cancela,  " +
             "ddo_cancelado,  " +
             "cenv_nombres_rem,  " +
             "cenv_apellidos_rem, " +
             "cenv_ciruc_rem, " +
             "cenv_socio, " +
             "alm_nombre, " +
             "socio.per_nombres nombresocio, " +
             "socio.per_apellidos apellidosocio, " +
             "cliente.per_nombres nombrecliente, " +
             "cliente.per_apellidos apellidocliente,	" +
             "cabecera.com_doctran doctrancabecera, " +
             "cabeceratotal.tot_total totalcabecera, " +
             "cabecera.com_fecha fechacabecera, " +
             "cabecera.com_codigo codigocabecera,	" +
             "detalle.com_fecha fechadetalle, " +
             "detalle.com_doctran doctrandetalle, " +
             "dca_monto montodocumento, " +
             "ddo_monto montocancela " +

             "FROM   " +
             "dcancelacion   " +
             "INNER JOIN ddocumento ON dca_empresa = ddo_empresa and dca_comprobante = ddo_comprobante and dca_transacc = ddo_transacc and dca_doctran = ddo_doctran and dca_pago =ddo_pago  " +
             "INNER JOIN ccomenv  ON dca_empresa = cenv_empresa and dca_comprobante = cenv_comprobante   " +
             "INNER JOIN comprobante detalle ON dca_empresa = com_empresa and dca_comprobante = com_codigo   " +
             "INNER JOIN almacen ON dca_empresa = alm_empresa and com_almacen =  alm_codigo   " +
             "INNER JOIN persona socio ON dca_empresa = socio.per_empresa and socio.per_codigo =  cenv_socio  " +
             "INNER JOIN total detalletotal ON dca_empresa = detalletotal.tot_empresa and detalletotal.tot_comprobante =  dca_comprobante  " +
             "INNER JOIN persona cliente ON dca_empresa = cliente.per_empresa and cliente.per_codigo =  detalle.com_codclipro " +
             "left JOIN comprobante cabecera ON dca_planilla = cabecera.com_codigo AND dca_empresa = cabecera.com_empresa          " +
             "left JOIN total cabeceratotal ON dca_empresa = cabeceratotal.tot_empresa and cabeceratotal.tot_comprobante =  dca_planilla   " +
             "where  dca_empresa={0} and cenv_socio={1} and dca_planilla IS NULL   " +
     "union all " +
     "SELECT  " +
             "dcg_comprobante_guia,  " +
             "dcg_empresa,  " +
             "dcg_transacc,  " +
             "dcg_comprobante_can,  " +
             "dcg_secuencia,       " +
             "dcg_pago,  " +
             "dcg_monto,        " +
             "ddo_codclipro,  " +
             "ddo_monto,  " +
             "ddo_cancela,  " +
             "ddo_cancelado,  " +
             "cenv_nombres_rem,  " +
             "cenv_apellidos_rem,  " +
             "cenv_ciruc_rem,  " +
             "cenv_socio,  " +
             "alm_nombre, " +
             "socio.per_nombres nombresocio, " +
             "socio.per_apellidos apellidosocio, " +
             "cliente.per_nombres nombrecliente, " +
             "cliente.per_apellidos apellidocliente,	 " +
             "cabecera.com_doctran doctrancabecera, " +
             "cabeceratotal.tot_total totalcabecera, " +
             "cabecera.com_fecha fechacabecera, " +
             "cabecera.com_codigo codigocabecera,	 " +
             "detalle.com_fecha fechadetalle, " +
             "detalle.com_doctran doctrandetalle, " +
             "dcg_monto montodocumento, " +
             "ddo_monto montocancela " +
             "FROM   " +
             "dcancelacionguias   " +
             "INNER JOIN ddocumento ON dcg_empresa = ddo_empresa and dcg_comprobante = ddo_comprobante and dcg_transacc = ddo_transacc and dcg_doctran = ddo_doctran and dcg_pago =ddo_pago   " +
             "INNER JOIN ccomenv  ON dcg_empresa = cenv_empresa and dcg_comprobante_guia = cenv_comprobante   " +
             "INNER JOIN comprobante detalle ON dcg_empresa = com_empresa and dcg_comprobante_guia = com_codigo   " +
             "INNER JOIN almacen ON dcg_empresa = alm_empresa and com_almacen =  alm_codigo   " +
             "INNER JOIN persona socio ON dcg_empresa = socio.per_empresa and socio.per_codigo =  cenv_socio  " +
             "INNER JOIN total detalletotal ON dcg_empresa = detalletotal.tot_empresa and detalletotal.tot_comprobante =  dcg_comprobante_guia  " +
             "INNER JOIN persona cliente ON dcg_empresa = cliente.per_empresa and cliente.per_codigo =  detalle.com_codclipro  " +
             "left JOIN comprobante cabecera ON dcg_planilla = cabecera.com_codigo AND dcg_empresa = cabecera.com_empresa          " +
             "left JOIN total cabeceratotal ON dcg_empresa = cabeceratotal.tot_empresa and cabeceratotal.tot_comprobante =  dcg_planilla    " +
             "where   dcg_empresa={0} and cenv_socio={1} and  dcg_planilla IS NULL    order by 	detalle.com_fecha     ";

                 return sql;
             } */
        public List<vDcancelacion> GetStruc()
        {
            return new List<vDcancelacion>();
        }


    }
}

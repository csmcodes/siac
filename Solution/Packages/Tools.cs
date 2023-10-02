using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using BusinessLogicLayer;
using System.IO;
using System.Web.Script.Serialization;

namespace Packages
{

    public class CompResumen
    {
        public long codigo { get; set; }
        public string recibo { get; set; }
        public string factura { get; set; }
        public decimal? monto { get; set; }

    }

    public class CompAnu
    {
        public long codigo { get; set; }
        public string doctran { get; set; }
    }

    public class Tools
    {

        public static string AddSerieDuplicados(string[] doctrans)
        {
            StringBuilder mensaje = new StringBuilder();
            string where = string.Join("','", doctrans);
            List<Comprobante> comprobantes = ComprobanteBLL.GetAll("com_doctran in ('" + where + "')", "");
            foreach (var item in doctrans)
            {
                List<Comprobante> lst = comprobantes.FindAll(f => f.com_doctran == item);
                if (lst.Count > 1)
                {
                    int i = 0;
                    foreach (Comprobante com in lst)
                    {
                        if (i>0)
                        {
                            com.com_doctran = com.com_doctran + " "+i.ToString();
                            com.com_empresa_key = com.com_empresa;
                            com.com_codigo_key = com.com_codigo;
                            ComprobanteBLL.Update(com);
                            mensaje.AppendFormat("{0} {1} seire {2}...<br>", com.com_codigo, com.com_doctran, com.com_serie);
                        }
                        i++;
                    }
                }
            }
            return mensaje.ToString();
        }

        public static string GetCancelacionesNoPlanilla(int empresa, DateTime? desde, DateTime? hasta)
        {
         
            WhereParams where = new WhereParams();
            List<object> parametros = new List<object>();
            where.where = "dca_planilla is null and ddo_comprobante_guia is not null and c.com_fecha between {0} and {1} and c.com_tipodoc=" + Services.Constantes.cRecibo.tpd_codigo;
            parametros.Add(desde);
            parametros.Add(hasta);
            where.valores = parametros.ToArray();
            List<vCancelacion> lst = vCancelacionBLL.GetAll(where, "ddo_doctran");

            WhereParams whererec = new WhereParams();
            List<object> parametrosrec = new List<object>();
            whererec.where = "com_fecha between {0} and {1}";
            parametrosrec.Add(desde);
            parametrosrec.Add(hasta);
            whererec.valores = parametrosrec.ToArray();
            List<Drecibo> drecibos = DreciboBLL.GetAll(whererec, "");

            StringBuilder html = new StringBuilder();


            List<CompResumen> resumen = new List<CompResumen>();


            html.Append("<p>Resumen Detallado</p>");
            html.Append("<table style='border: 1px solid;'>");
            foreach (vCancelacion item in lst)
            {                


                List<Drecibo> recs = drecibos.FindAll(f => f.dfp_comprobante == item.dca_comprobante_can);
                if (recs!=null)
                {
                    if (HasTipoPago(recs.Select(s => s.dfp_tipopago).ToArray()))
                    {
                        CompResumen res = resumen.Find(f => f.codigo == item.dca_comprobante_can);
                        if (res == null)
                        {
                            res = new CompResumen();
                            res.codigo = item.dca_comprobante_can ?? 0;
                            res.recibo = item.doctran_can;
                            res.factura = item.ddo_doctran;
                            res.monto = item.dca_monto;
                            resumen.Add(res);
                        }
                        else
                            res.monto += item.dca_monto;

                        html.AppendFormat("<tr><td style='border: 1px solid;'>{0}</td><td style='border: 1px solid;'>{1}</td><td style='border: 1px solid;'>{2}</td><td style='border: 1px solid;'>{3}</td><td style='border: 1px solid;'>{4}</td><tr>", item.fecha_can, item.doctran_can, item.ddo_doctran, item.ddo_pago, item.dca_monto);
                    }
                }
                
            }
            html.Append("</table>");

            StringBuilder reshtml = new StringBuilder();
            reshtml.Append("<p>Resumen Consolidado</p>");
            reshtml.Append("<table style='border: 1px solid;'>");
            foreach (var item in resumen)
            {
                reshtml.AppendFormat("<tr><td style='border: 1px solid;'>{0}</td><td style='border: 1px solid;'>{1}</td><td style='border: 1px solid;'>{2}</td><tr>", item.recibo, item.factura, item.monto);
            }
            reshtml.Append("</table>");


            return reshtml.ToString() +  html.ToString();

        }



        public static bool HasTipoPago(int[] dfp_tipos)
        {
            List<int> tipospagos = new List<int>() { 13, 14, 15, 1, 2, 4, 5, 34 };

            foreach (var item in dfp_tipos)
            {
                if (!tipospagos.Contains(item))
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Util pare eliminar retenciones duplicadas 
        /// En caso de que la retencion se du
        /// </summary>
        /// <returns></returns>
        /// 


        public static string FixDuplicadosElectronicos(int empresa, string clave, DateTime? desde)
        {
            WhereParams where = new WhereParams();
            //where.where = "com_empresa=" + empresa + " and com_claveelec='" + clave + "' and com_fecha>={0}";
            where.where = "com_empresa=" + empresa + " and com_claveelec='" + clave + "' ";
            List<object> parametros = new List<object>();
            if (desde.HasValue)
            {
                where.where += "and com_fecha>={0} ";
                parametros.Add(desde);
            }
            where.valores = parametros.ToArray();
            List<Comprobante> comprobantes = ComprobanteBLL.GetAll(where, "com_codigo");
            string wherecomp = string.Join(",", comprobantes.Select(s=>s.com_codigo.ToString()).ToArray());
            List<Dcancelacion> cancelaciones = DcancelacionBLL.GetAll("dca_empresa=" + empresa + " and dca_comprobante_can in (" + wherecomp + ")", "");
            

            int i= 0;
            int a = 0;
            List<CompAnu> lstanu = new List<CompAnu>();
            foreach (Comprobante item in comprobantes)
            {

                if (i > 0)
                {                    
                    //mensaje.Append("{'codigo':{"+item.com_codigo+"}, 'doctran':'"+item.com_doctran+"'},");
                    General.AnulaComprobante(item);
                    
                    lstanu.Add(new CompAnu() { codigo = item.com_codigo, doctran = item.com_doctran });
                }
                i++;

            }            

            if (cancelaciones.Count>0)
            {
                Comprobante factura = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = empresa, com_empresa_key = empresa, com_codigo = cancelaciones[0].dca_comprobante, com_codigo_key = cancelaciones[0].dca_comprobante });
                Auto.actualizar_documentos(factura);
//                Auto.actualiza_documentos(empresa, null, factura.com_codigo);                               
            }
            return new JavaScriptSerializer().Serialize(lstanu);
        }



        delegate string DelegadoRemoveDuplcateRecibos(int empresa, DateTime? desde, DateTime? hasta, string tipospagos);

        public static void RemoveDuplcateRecibosAsync(int empresa, DateTime? desde, DateTime? hasta, string tipospagos)
        {
            DelegadoRemoveDuplcateRecibos delegado = new DelegadoRemoveDuplcateRecibos(RemoveDuplcateRecibos);
            IAsyncResult result = delegado.BeginInvoke(empresa,desde,hasta,tipospagos, null, null);
        }


        public static string RemoveDuplcateRecibos(int empresa, DateTime? desde, DateTime? hasta, string tipospagos)
        {
            WhereParams where = new WhereParams();
            //where.where += "com_empresa=" + empresa + " and com_fecha between {0} and {1} and com_estado=2 and dfp_tipopago in (" + tipospagos + ")";
            where.where += "com_empresa=" + empresa + " and com_fecha between {0} and {1} and com_estado=2 and com_claveelec is not null";
            List<object> parametros = new List<object>();
            parametros.Add(desde);
            parametros.Add(hasta);
            where.valores = parametros.ToArray();
            List<Drecibo> recibos = DreciboBLL.GetAll(where, "");

            var duplicados = recibos.GroupBy(g => g.dfp_comprobanteclaveelec).Select(n => new
            {
                clave = n.Key,
                cantidad = n.Count()
            }).Where(w => w.cantidad > 1).ToList();


            int i = 0;
            try
            {
                //LogBLL.Insert(new Log(empresa, DateTime.Now, "Inicio " + duplicados.Count()));
                
                foreach (var item in duplicados)
                {
                    if (!string.IsNullOrEmpty(item.clave))
                    {
                        string mensaje = FixDuplicadosElectronicos(empresa, item.clave, desde);
                        //LogBLL.Insert(new Log(empresa, DateTime.Now, mensaje));

                    }
                    i++;
                }
                //LogBLL.Insert(new Log(empresa, DateTime.Now, "Fin " + i));
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                //LogBLL.Insert(new Log(empresa, DateTime.Now, ex.Message));

            }
            return new JavaScriptSerializer().Serialize(duplicados);
            //return i.ToString();


        }

        public static string GetErroresPlanillas(int empresa, DateTime? desde, DateTime? hasta, int? codplanilla, int? codsocio)
        {


            WhereParams whererec = new WhereParams();
            List<object> parametrosrec = new List<object>();
            whererec.where = "com_empresa=1 and com_tipodoc= 7 and com_fecha between {0} and {1}";
            parametrosrec.Add(desde);
            parametrosrec.Add(hasta);
            if (codplanilla.HasValue)
                whererec.where += " and com_codigo=" + codplanilla;
            if (codsocio.HasValue)
                whererec.where += " and com_codclipro=" + codsocio;


            whererec.valores = parametrosrec.ToArray();                       

            

            List<Comprobante> comprobantes = ComprobanteBLL.GetAll(whererec, "");
            string wherein = string.Join(",", comprobantes.Select(s => s.com_codigo).ToArray());
            List<vCancelacion> lst = vCancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_planilla in ("+wherein+")", empresa),"");

            StringBuilder html = new StringBuilder();
            string estilo = "style='border: 1px solid;'";

            //html.Append("<table style='border: 1px solid; border-collapse: collapse;margin-top:5px'>");
            //html.AppendFormat("<tr><td {0}>Cod Planilla</td><td {0}>Planilla</td><td {0}>Socio</td><td {0}>Cod Cancela</td><td {0}>Cancelacion</td><td {0}>Monto Planilla</td><td {0}>Monto Cancela</td><td {0}>Diferencia</td></tr>", estilo);
            html.AppendFormat("Cod Planilla;Planilla;Socio;Cod Cancela;Cancelacion;Monto Planilla;Monto Cancela;Diferencia<br>", estilo);

            foreach (Comprobante planilla in comprobantes)
            {
                List<vCancelacion> detalle = lst.FindAll(f => f.dca_planilla == planilla.com_codigo);                               
                foreach (vCancelacion item in detalle)
                {
                    if (item.dca_monto_pla> item.dca_monto)
                    {
                        html.AppendFormat("{0};{1};{2};{3};{4};{5};{6};{7}<br>", planilla.com_codigo, planilla.com_doctran, planilla.com_nombresocio, item.dca_comprobante_can, item.doctran_can, item.dca_monto, item.dca_monto_pla, item.dca_monto_pla - item.dca_monto, estilo);
                        //html.AppendFormat("<tr><td {8}>{0}</td><td {8}>{1}</td><td {8}>{2}</td><td {8}>{3}</td><td {8}>{4}</td><td {8}>{5}</td><td {8}>{6}</td><td {8}>{7}</td></tr>", planilla.com_codigo, planilla.com_doctran, planilla.com_nombresocio, item.dca_comprobante_can, item.doctran_can, item.dca_monto, item.dca_monto_pla, item.dca_monto_pla - item.dca_monto, estilo);
                    }
                }                         

            }
            //html.Append("</table>");




            return html.ToString();
        }
    

    }
}

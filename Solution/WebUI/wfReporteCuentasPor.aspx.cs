using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessLogicLayer;
using System.Web.Services;
using System.Text;
using System.Data;
using Services;
using System.Web.Script.Serialization;
using System.Collections;
using HtmlObjects;
using Functions;
using Packages;


namespace WebUI
{
    public partial class wfReporteCuentasPor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static string GetFiltros(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object tipo = null;

            tmp.TryGetValue("tipo", out tipo);



            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(9, 2);
            tdatos.rows[0].cells[0].valor = "Almacen:";
            tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Punto Venta";
            tdatos.rows[1].cells[1].valor = new Select { id = "cmbPVENTA", clase = Css.large, diccionario = Dictionaries.Empty(), withempty = true }.ToString();
            tdatos.rows[2].cells[0].valor = "Desde:";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtDESDE", placeholder = "DESDE", clase = Css.small, datepicker = true }.ToString();
            tdatos.rows[3].cells[0].valor = "Hasta:";
            tdatos.rows[3].cells[1].valor = new Input { id = "txtHASTA", placeholder = "HASTA", clase = Css.small, datepicker = true }.ToString();

            string ctas = "";

            if (tipo.ToString() == "CLI")
            {
                tdatos.rows[4].cells[0].valor = "Cliente:";
                ctas = Constantes.GetParameter("ctasclientes");
                tdatos.rows[4].cells[1].valor = new Input { id = "txtPERSONA", placeholder = "CLIENTE", clase = Css.medium }.ToString();
                tdatos.rows[5].cells[0].valor = "Tipo Cliente:";
                tdatos.rows[5].cells[1].valor = new Select { id = "cmbTIPO", clase = Css.large, diccionario = Dictionaries.GetTipoCxC(), withempty = true };

                tdatos.rows[6].cells[0].valor = "Politica:";
                tdatos.rows[6].cells[1].valor = new Select { id = "cmbPOLITICA", clase = Css.large, diccionario = Dictionaries.GetPolitica(Constantes.cCliente), withempty = true };

                tdatos.rows[7].cells[0].valor = "Cuentas Contables";
                tdatos.rows[7].cells[1].valor = new Select { id = "cmbCUENTAS", valor = ctas, clase = Css.large, diccionario = Dictionaries.GetCuentaMovi(), multiselect = true, data = "data-placeholder=\"Seleccione Cuentas...\"", withempty = true }.ToString();

                tdatos.rows[8].cells[0].valor = "";
                //tdatos.rows[7].cells[1].valor = new Boton { click = "LoadReporte();return false;", valor = "Generar Reporte" }.ToString() + " " + new Boton { click = "LoadReporteAnexo();return false;", valor = "Generar Reporte Anexo" }.ToString() + " " + new Boton { click = "LoadReporteDocAnexo();return false;", valor = "Generar Documento Anexo" }.ToString() + " " + new Boton { click = "GetDescuadres();return false;", valor = "Descuadres" }.ToString();
                tdatos.rows[8].cells[1].valor = new Boton { click = "LoadReporte(1);return false;", valor = "Reporte" }.ToString() + " " +
                                                new Boton { click = "LoadReporte(4);return false;", valor = "Reporte Columnas" }.ToString() + " " +
                                                new Boton { click = "LoadReporte(2);return false;", valor = "Reporte Vencimientos" }.ToString() + " " +
                                                new Boton { click = "LoadReporte(3);return false;", valor = "Reporte Vencimientos Consolidado" }.ToString() + " " +
                                                new Boton { click = "LoadReporteAnexo();return false;", valor = "Generar Reporte Anexo" }.ToString() + " " + 
                                                new Boton { click = "LoadReporteDocAnexo();return false;", valor = "Generar Documento Anexo" }.ToString() + " " + 
                                                new Boton { click = "GetDescuadres();return false;", valor = "Descuadres" }.ToString() + 

                                                "";



            }
            else
            {
                tdatos.rows[4].cells[0].valor = "Proveedor:";
                ctas = Constantes.GetParameter("ctasproveedores");
                tdatos.rows[4].cells[1].valor = new Input { id = "txtPERSONA", placeholder = "PROVEEDOR", clase = Css.medium }.ToString();
                tdatos.rows[5].cells[0].valor = "Cuentas Contables";
                tdatos.rows[5].cells[1].valor = new Select { id = "cmbCUENTAS", valor = ctas, clase = Css.large, diccionario = Dictionaries.GetCuentaMovi(), multiselect = true, data = "data-placeholder=\"Seleccione Cuentas...\"", withempty = true }.ToString();

                tdatos.rows[6].cells[0].valor = "";
                //tdatos.rows[6].cells[1].valor = new Boton { click = "LoadReporte();return false;", valor = "Generar Reporte" }.ToString() + " " + new Boton { click = "LoadReporteAnexo();return false;", valor = "Generar Reporte Anexo" }.ToString() + " " + new Boton { click = "LoadReporteDocAnexo();return false;", valor = "Generar Documento Anexo" }.ToString() + " " + new Boton { click = "GetDescuadres();return false;", valor = "Descuadres" }.ToString();

                tdatos.rows[6].cells[1].valor = new Boton { click = "LoadReporte(1);return false;", valor = "Reporte" }.ToString() + " " +
                                                new Boton { click = "LoadReporte(4);return false;", valor = "Reporte Columnas" }.ToString() + " " +
                                                new Boton { click = "LoadReporte(2);return false;", valor = "Reporte Vencimientos" }.ToString() + " " +
                                                new Boton { click = "LoadReporte(3);return false;", valor = "Reporte Vencimientos Consolidado" }.ToString() + " " +
                                                new Boton { click = "LoadReporteAnexo();return false;", valor = "Generar Reporte Anexo" }.ToString() + " " + 
                                                new Boton { click = "LoadReporteDocAnexo();return false;", valor = "Generar Documento Anexo" }.ToString() + " " + 
                                                new Boton { click = "GetDescuadres();return false;", valor = "Descuadres" }.ToString() + 
                                                "";//new Boton { click = "GetDescuadres();return false;", valor = "Descuadres" }.ToString();

                tdatos.rows[7].cells[0].valor = "";
                tdatos.rows[7].cells[1].valor = "";
                tdatos.rows[8].cells[0].valor = "";
                tdatos.rows[8].cells[1].valor = "";
            }



            //tdatos.rows[6].cells[1].valor = new Input { id = "txtHASTA", placeholder = "HASTA", clase = Css.small, datepicker = true }.ToString();
            //tdatos.rows[6].cells[1].valor = new Boton { click = "LoadReporte();return false;", valor = "Generar Reporte" }.ToString();


            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static string GetPuntoVenta(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object id = null;
            object empresa = null;
            object almacen = null;
            object usuario = null;
            tmp.TryGetValue("id", out id);
            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("almacen", out almacen);
            tmp.TryGetValue("usuario", out usuario);
            if (!string.IsNullOrEmpty((string)almacen))
            {
                Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa.ToString()), uxe_empresa_key = int.Parse(empresa.ToString()), uxe_usuario = usuario.ToString(), uxe_usuario_key = usuario.ToString() });

                //return new Select { id = "cmbPVENTA_P", diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())),clase = Css.large, valor= uxe.uxe_puntoventa }.ToString();
                return new Select { id = id.ToString(), diccionario = Dictionaries.GetPuntoVenta(int.Parse(empresa.ToString()), int.Parse(almacen.ToString())), clase = Css.large, valor = uxe.uxe_puntoventa }.ToString();
            }
            return new Select { id = id.ToString(), diccionario = Dictionaries.Empty(), clase = Css.large }.ToString();

        }




        [WebMethod]
        public static string GetDescuadres(object objeto)
        {

            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object empresa = null;
            object desde = null;
            object hasta = null;
            object almacen = null;
            object pventa = null;
            object persona = null;
            object tipo = null;
            object cuentas = null;
            object userid = null;


            tmp.TryGetValue("empresa", out empresa);
            tmp.TryGetValue("desde", out desde);
            tmp.TryGetValue("hasta", out hasta);
            tmp.TryGetValue("almacen", out almacen);
            tmp.TryGetValue("pventa", out pventa);
            tmp.TryGetValue("persona", out persona);
            tmp.TryGetValue("tipo", out tipo);
            tmp.TryGetValue("cuentas", out cuentas);
            tmp.TryGetValue("crea_usr", out userid);

            string[] arrayctas = Conversiones.ObjectToString(cuentas).Split('|');
            List<int> lstcuentas = new List<int>();
            foreach (string item in arrayctas)
            {
                int cta = -1;
                int.TryParse(item, out cta);
                if (cta > 0)
                    lstcuentas.Add(cta);
            }

            return General.GetDescuadresCuentasPor(Conversiones.ObjectToDateTimeNull(desde), Conversiones.ObjectToDateTimeNull(hasta), Conversiones.ObjectToIntNull(empresa).Value, Conversiones.ObjectToIntNull(almacen), Conversiones.ObjectToIntNull(pventa), Conversiones.ObjectToString(persona), Conversiones.ObjectToString(tipo), lstcuentas.ToArray());



        }




    }
}
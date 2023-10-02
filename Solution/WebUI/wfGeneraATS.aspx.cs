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
using System.Xml;

namespace WebUI
{
    public partial class wfGeneraATS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GeneraATS();
            }
        }

        public void GeneraATS()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["periodo"]))
            {
                try
                {
                    int periodo = int.Parse(Request.QueryString["periodo"]);
                    int mes = int.Parse(Request.QueryString["mes"]);
                    string strignorar = Request.QueryString["ignorar"];
                    string mensaje = "";
                    string filename = "ATS" + mes.ToString("00") + periodo.ToString() + ".xml";
                    XmlDocument xml = Packages.SRI.GenerarATS(periodo, mes,out mensaje);
                    
                    if (!string.IsNullOrEmpty(mensaje))
                    {
                        if (strignorar != "1")
                            throw new Exception(mensaje);
                    }
                    
                    Response.Clear();
                    Response.ContentType = "text/xml";
                    //Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xml");
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                    XmlTextWriter xWriter = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
                    xml.Save(xWriter);
                    xWriter.Close();
                    Response.End();
                }
                catch (Exception ex)
                {
                    errores.InnerHtml = "ERROR: "+ex.Message;
                }
            }
        }

        [WebMethod]
        public static string GetFiltros(object objeto)
        {

            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(4, 2);
            //tdatos.rows[0].cells[0].valor = "Almacen:";
            //tdatos.rows[0].cells[1].valor = new Select { id = "cmbALMACEN", clase = Css.large, diccionario = Dictionaries.GetAlmacen(), withempty = true }.ToString();
            //tdatos.rows[1].cells[0].valor = "Punto Venta";
            //tdatos.rows[1].cells[1].valor = new Select { id = "cmbPVENTA", clase = Css.large, diccionario = Dictionaries.Empty(), withempty = true }.ToString();
            //tdatos.rows[2].cells[0].valor = "Impuesto";
            //tdatos.rows[2].cells[1].valor = new Select { id = "cmbImpuesto", clase = Css.large, diccionario = Dictionaries.GetImpuesto(), withempty = true }.ToString();
            tdatos.rows[0].cells[0].valor = "Periodo:";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtPERIODO", placeholder = "PERIODO", clase = Css.mini}.ToString();
            tdatos.rows[1].cells[0].valor = "Mes:";
            tdatos.rows[1].cells[1].valor = new Input { id = "txtMES", placeholder = "MES", clase = Css.mini}.ToString();
            tdatos.rows[2].cells[0].valor = "Ignorar mensajes:";
            tdatos.rows[2].cells[1].valor = new Check { id = "chkMENS", clase = Css.mini }.ToString();
            tdatos.rows[3].cells[0].valor = "";
            tdatos.rows[3].cells[1].valor = new Boton { click = "GenATS();return false;", valor = "Generar ATS" }.ToString();

            //tdatos.rows[4].cells[0].valor = "";
            //tdatos.rows[4].cells[1].valor = "";
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

       

        //protected void btngen_Click(object sender, EventArgs e)
        //{

        //    XmlDocument xml = Packages.SRI.GenerarATS(2014, 8);
        //    Response.Clear();
        //    Response.ContentType = "text/xml";
        //    Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xml");
        //    XmlTextWriter xWriter = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
        //    xml.Save(xWriter);
        //    xWriter.Close();
        //    Response.End();

        //    //ShowXML(xml);
        //}

        /*
        public void ShowXML(XmlDocument xml)
        {
            Response.Clear();
            Response.ContentType = "text/xml";
            Response.AppendHeader("Content-Disposition", "attachment;filename=FileName.xml");
            XmlTextWriter xWriter = new XmlTextWriter(Response.OutputStream, System.Text.Encoding.UTF8);
            xml.Save(xWriter);
            xWriter.Close();
            Response.End();
        }*/
    }
}
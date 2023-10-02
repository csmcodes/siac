using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
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
using System.Drawing;

namespace WebUI.templates
{
    public partial class FAC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodigo.Text = Request.QueryString["codigo"];


            }
        }





        [WebMethod]
        public static string GetComprobante(object objeto)
        {
           


            Comprobante fac = new Comprobante(objeto);
            fac.com_codigo_key = fac.com_codigo;
            fac.com_empresa_key = fac.com_empresa;
            fac = ComprobanteBLL.GetByPK(fac);

            fac.ccomdoc = new Ccomdoc();
            fac.ccomenv = new Ccomenv();
            fac.total = new Total();

            fac.ccomdoc.cdoc_comprobante = fac.com_codigo;
            fac.ccomdoc.cdoc_empresa = fac.com_empresa;
            fac.ccomdoc.cdoc_comprobante_key = fac.com_codigo;
            fac.ccomdoc.cdoc_empresa_key = fac.com_empresa;
            fac.ccomdoc = CcomdocBLL.GetByPK(fac.ccomdoc);

            fac.ccomenv.cenv_comprobante = fac.com_codigo;
            fac.ccomenv.cenv_empresa = fac.com_empresa;
            fac.ccomenv.cenv_comprobante_key = fac.com_codigo;
            fac.ccomenv.cenv_empresa_key = fac.com_empresa;
            fac.ccomenv = CcomenvBLL.GetByPK(fac.ccomenv);

            fac.total.tot_comprobante = fac.com_codigo;
            fac.total.tot_empresa = fac.com_empresa;
            fac.total.tot_comprobante_key = fac.com_codigo;
            fac.total.tot_empresa_key = fac.com_empresa;
            fac.total = TotalBLL.GetByPK(fac.total);

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");
            fac.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            List<Dcalculoprecio> lst = DcalculoprecioBLL.GetAll(new WhereParams("dcpr_empresa={0} and dcpr_comprobante={1}", fac.com_empresa, fac.com_codigo), "");

            foreach (Dcomdoc item in fac.ccomdoc.detalle)
            {
                item.detallecalculo =  lst.FindAll(delegate(Dcalculoprecio d) { return d.dcpr_dcomdoc == item.ddoc_secuencia; });                
            }


            Impresion imp = new Impresion();

            imp.imp_empresa = fac.com_empresa;
            imp.imp_empresa_key = fac.com_empresa;
            imp.imp_almacen = fac.com_almacen.Value;
            imp.imp_almacen_key = fac.com_almacen.Value;
            imp.imp_pventa = fac.com_pventa.Value;
            imp.imp_pventa_key = fac.com_pventa.Value;
            imp.imp_tipodoc = fac.com_tipodoc;
            imp.imp_tipodoc_key = fac.com_tipodoc;

            imp = ImpresionBLL.GetByPK(imp);

            fac.impresora = imp.imp_impresora;
                       

            return new JavaScriptSerializer().Serialize(fac);

        }

    }
}
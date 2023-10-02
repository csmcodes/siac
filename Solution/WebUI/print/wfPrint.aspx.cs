using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessLogicLayer;

namespace WebUI.print
{
    public partial class wfPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                string usuario = Request.QueryString["usuario"];
                string empresa = Request.QueryString["empresa"];
                string tipodoc = Request.QueryString["tipodoc"];

                txtcodigo.Text = Request.QueryString["codigo"];
                txtpath.Text = Server.MapPath("../pdf");
                txtfilename.Text = Services.Pdf.ComprobantePDF(int.Parse(empresa), txtcodigo.Text, txtpath.Text, false, "",null);

                Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = int.Parse(empresa), uxe_empresa_key = int.Parse(empresa), uxe_usuario = usuario, uxe_usuario_key = usuario }); 

                txtprinter.Text = Services.Constantes.GetPrinter(usuario, int.Parse(empresa), uxe.uxe_almacen, uxe.uxe_puntoventa, int.Parse(tipodoc));
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI
{
    public partial class wfValidaCIRUC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnvalidar_Click(object sender, EventArgs e)
        {
            if (Functions.Validaciones.valida_cedularuc(txtciruc.Text))
                lblmensaje.Text = "Identificacion correcta";
            else
                lblmensaje.Text = "Identificacion ERROR";
        }
    }
}
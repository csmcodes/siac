using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.print
{
    public partial class wfPrintJS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                content.InnerHtml = "<div style='position:absolute;top:50px;left:200px;'>HOLA</div>";
            }
        }
    }
}
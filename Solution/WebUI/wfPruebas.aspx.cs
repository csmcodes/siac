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

namespace WebUI
{
    public partial class wfPruebas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetTabla()
        {
            List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0}",1), "");              

            StringBuilder html = new StringBuilder();

            HtmlGrid gdatos = new HtmlGrid();
            gdatos.id = "datos";
            gdatos.titulo = "Deudas";

            gdatos.AddColumn(new HtmlGridCol{ id="documento", titulo ="Documento", estilo = "width30" });
            gdatos.AddColumn(new HtmlGridCol { id = "nro", titulo = "Nro" });
            gdatos.AddColumn(new HtmlGridCol { id = "emision", titulo = "Emision" });
            gdatos.AddColumn(new HtmlGridCol { id = "vence", titulo = "Vence" });
            gdatos.AddColumn(new HtmlGridCol { id = "socio", titulo = "Socio" });
            gdatos.AddColumn(new HtmlGridCol { id = "monto", titulo = "Monto", estilo = "width5" });
            gdatos.AddColumn(new HtmlGridCol { id = "cancelado", titulo = "Cancelado", estilo = "width5" });
            gdatos.AddColumn(new HtmlGridCol { id = "saldo", titulo = "Saldo", estilo = "width5" });
            gdatos.AddColumn(new HtmlGridCol { id = "valor", titulo = "Valor", estilo = "width5" });

            
            gdatos.editable = true;
            gdatos.footer = true;  

            foreach (vDdocumento item in lista)
            {
                HtmlGridRow row = new HtmlGridRow();
                row.data = "data-comprobante=" + item.ddo_comprobante + " data-transac=" + item.ddo_transacc;
                //row.clickevent = "EditAfec(this)";

                row.cells.Add(new HtmlGridCell { valor = item.ddo_doctran});
                row.cells.Add(new HtmlGridCell { valor = item.ddo_pago});
                row.cells.Add(new HtmlGridCell { valor = (item.ddo_fecha_emi.HasValue) ? item.ddo_fecha_emi.Value.ToShortDateString() : "" });
                row.cells.Add(new HtmlGridCell { valor = (item.ddo_fecha_ven.HasValue) ? item.ddo_fecha_ven.Value.ToShortDateString() : "" });
                row.cells.Add(new HtmlGridCell { valor = item.per_razon});
                row.cells.Add(new HtmlGridCell { valor = Formatos.CurrencyFormat(item.ddo_monto), clase = Css.right});
                row.cells.Add(new HtmlGridCell { valor = Formatos.CurrencyFormat(item.ddo_cancela), clase = Css.right});
                decimal saldo = ((item.ddo_monto.HasValue) ? item.ddo_monto.Value : 0) - ((item.ddo_cancela.HasValue) ? item.ddo_cancela.Value : 0);
                row.cells.Add(new HtmlGridCell { valor = Formatos.CurrencyFormat(saldo), clase = Css.right});
                row.cells.Add(new HtmlGridCell { valor = new Input() { clase =  Css.blocklevel + Css.amount, valor = 0.00 }.ToString(), clase = Css.right});
                gdatos.AddRow(row);

            }

            html.AppendLine(gdatos.ToString());
            return html.ToString();
        }
    }
}
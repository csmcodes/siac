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
using System.IO;
using System.Text;
using System.Reflection;
namespace WebUI
{
    public partial class wfPersonaAutorizacion : System.Web.UI.Page
    {

        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cpr_nombre";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
        static string codigo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                codigo = Request.QueryString["codigo"];
                pageIndex = 1;
                pageSize = 10;

            }


        }


        [WebMethod]
        public static string GetCabecera()
        {
            //  SetWhereClause(new Usuario(objeto));
            List<Persona> lst = PersonaBLL.GetAll("per_codigo = " + "'" + codigo + "'", "");



            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(2, 2);
            tdatos.rows[0].cells[0].valor = "id:";
            tdatos.rows[0].cells[1].valor = new Input { id = "cmbID_S", clase = Css.medium, valor = lst[0].per_codigo, habilitado = false }.ToString();
            tdatos.rows[1].cells[0].valor = "Razon Social:";
            tdatos.rows[1].cells[1].valor = new Input { id = "cmbRAZONS_S", clase = Css.medium, valor = lst[0].per_razon, habilitado = false }.ToString();

            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        [WebMethod]
        public static string GetForm()
        {
            return ShowObject(new Autpersona());
        }

        [WebMethod]


        public static string ShowObject(Autpersona obj)
        {
           
            Persona persona = new Persona();
           // persona.per_empresa = obj.ape_empresa;
           // persona.per_empresa_key = obj.ape_empresa;
            if (obj.ape_persona.HasValue)
            {
                persona.per_codigo = obj.ape_persona.Value;
                persona.per_codigo_key = obj.ape_persona.Value;
                persona = PersonaBLL.GetByPK(persona);
            }
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span11\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(9, 2);
            tdatos.rows[0].cells[0].valor = "Nro Autorizacion";
            tdatos.rows[0].cells[1].valor = new Input { id = "txtNRO_AUTORIZA", valor = obj.ape_nro_autoriza, clase = Css.large, obligatorio = true }.ToString();
            tdatos.rows[1].cells[0].valor = "Valido hasta";
            tdatos.rows[1].cells[1].valor = new Input { id = "cmbFECHA",  datepicker = true, datetimevalor = (obj.ape_val_fecha.HasValue) ? obj.ape_val_fecha.Value : DateTime.Now, clase = Css.large}.ToString();
            tdatos.rows[2].cells[0].valor = "Almacen";
            tdatos.rows[2].cells[1].valor = new Input { id = "txtFAC1", valor= obj.ape_fac1, clase = Css.mini }.ToString();
            tdatos.rows[3].cells[0].valor = "Punto Venta";
            tdatos.rows[3].cells[1].valor = new Input { id = "txtFAC2", valor = obj.ape_fac2, clase = Css.mini }.ToString();
            tdatos.rows[4].cells[0].valor = "Desde";
            tdatos.rows[4].cells[1].valor = new Input { id = "txtFAC3", valor = obj.ape_fac3, clase = Css.mini }.ToString();
            tdatos.rows[5].cells[0].valor = "Hasta";
            tdatos.rows[5].cells[1].valor = new Input { id = "txtFACT3", valor = obj.ape_fact3, clase = Css.mini }.ToString();
            tdatos.rows[6].cells[0].valor = "Retdato";
            tdatos.rows[6].cells[1].valor = new Select { id = "cmbRETDATO", valor = obj.ape_retdato.ToString(), clase = Css.large, diccionario = Dictionaries.GetRetdato() }.ToString();
            tdatos.rows[7].cells[0].valor = "Tablacoa";
            tdatos.rows[7].cells[1].valor = new Select { id = "cmbTABLACOA", valor = obj.ape_tablacoa.ToString(), clase = Css.large, diccionario = Dictionaries.GetTablacoa() }.ToString();
            tdatos.rows[8].cells[0].valor = "Activo";
            tdatos.rows[8].cells[1].valor = new Check { id = "chkESTADO", valor = obj.ape_estado }.ToString();
            html.AppendLine(tdatos.ToString());
            // html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            html.AppendLine(new Input { id = "txtRETDATO_key", valor = obj.ape_retdato_key.ToString(), visible = false }.ToString());
            html.AppendLine(new Input { id = "txtNRO_AUTORIZA_key", valor = obj.ape_nro_autoriza_key, visible = false }.ToString());
            //       html.AppendLine(new Input { id = "txtFAC1", etiqueta = "Almacen", placeholder = "Almacen", valor = obj.ape_fac1, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtFAC1_key", valor = obj.ape_fac1_key, visible = false }.ToString());
            //      html.AppendLine(new Input { id = "txtFAC2", etiqueta = "Punto de Vente", placeholder = "Punto de Vente", valor = obj.ape_fac2, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtFAC2_key", valor = obj.ape_fac2_key, visible = false }.ToString());
            html.AppendLine(new Input { id = "cmbPERSONA", visible = false, valor = codigo }.ToString());
            html.AppendLine(" </div><!--span11-->");
            html.AppendLine("</div><!--row-fluid-->");
            return html.ToString();
        }

        public static string ShowData(Autpersona obj)
        {
            return new HtmlObjects.ListItem { titulo = new string[] { obj.ape_nombrepersona + " " + obj.ape_fac1, "-", obj.ape_fac2, "-", obj.ape_fac3.ToString(), "/", obj.ape_fact1, "-", obj.ape_fact2, "-", obj.ape_fact3.ToString() }, descripcion = new string[] { "Aut. SRI:", obj.ape_nro_autoriza, " / Valido hasta", obj.ape_val_fecha + "" }, logico = new LogicItem[] { new LogicItem("Activos", obj.ape_estado) } }.ToString();
        }


         [WebMethod]
        public static string SaveObject(object objeto)
        {
            Autpersona obj = new Autpersona(objeto);

            if (!string.IsNullOrEmpty(obj.ape_nro_autoriza_key))
            {
                if (AutpersonaBLL.Update(obj) > 0)
                {
                    obj=AutpersonaBLL.GetByPK(obj);
                    return ShowData(obj);
                }
                else
                    return "ERROR";
            }
            else
            {
                if (AutpersonaBLL.Insert(obj) > 0)
                {
                    return "OK";
                }
                else
                    return "ERROR";
            }

        }

        [WebMethod]
        public static string DeleteObject(object objetos)
        {
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            transaction.BeginTransaction();

            foreach (object item in (Array)objetos)
            {
                Autpersona obj = new Autpersona(item);
                obj.ape_empresa_key = obj.ape_empresa;
                obj.ape_nro_autoriza_key = obj.ape_nro_autoriza;
                obj.ape_fac1_key = obj.ape_fac1;
                obj.ape_fac2_key = obj.ape_fac2;
                obj.ape_retdato_key = obj.ape_retdato;
              
               
                AutpersonaBLL.Delete(transaction, obj);
            }
            transaction.Commit();
            return "OK";
        }

        [WebMethod]
        public static string GetFormM(object objetos)
        {
            Autpersona obj = null;

            foreach (object item in (Array)objetos)
            {
                 obj = new Autpersona(item);
                obj.ape_empresa_key = obj.ape_empresa;
                obj.ape_nro_autoriza_key = obj.ape_nro_autoriza;
                obj.ape_fac1_key = obj.ape_fac1;
                obj.ape_fac2_key = obj.ape_fac2;
                obj.ape_retdato_key = obj.ape_retdato;
                obj = AutpersonaBLL.GetByPK(obj);
               
            }
            return ShowObject(obj);
        }



        [WebMethod]

        public static string GetDetallePersonaAu(object id)
        {

            
            List<Autpersona> lista = new List<Autpersona>();
            Dictionary<string, object> tmp = (Dictionary<string, object>)id;
            object usuario = null;
            object empresa = null;
          
            tmp.TryGetValue("udo_usuario", out usuario);
            tmp.TryGetValue("udo_empresa", out empresa);
            lista = AutpersonaBLL.GetAll(new WhereParams("ape_persona = {0} and ape_empresa = {1}  ", int.Parse(usuario.ToString()), int.Parse(empresa.ToString())), "");


            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();            
            tdatos.id = "tdaut";
            //tdatos.invoice = true;
            //tdatos.clase = "scrolltable";


            tdatos.AddColumn("<a class='btn' href='#' onclick='AddAutorizacion()'><i class='iconfa-plus'></i></a>", "", "");
            tdatos.AddColumn("Nro Autorización", "", "");
            tdatos.AddColumn("Tipo", "", "");
            tdatos.AddColumn("Fecha", "", "", "");
            tdatos.AddColumn("Desde", "", "", "");
            tdatos.AddColumn("Hasta", "", "", "");
            tdatos.AddColumn("Estado", "", "", "");


            foreach (Autpersona item in lista)
            {

                HtmlRow row = new HtmlRow();
                row.data = "data-empresa='" + item.ape_empresa + "' data-nroautoriza='" + item.ape_nro_autoriza+ "' data-fac1='" + item.ape_fac1+ "' data-fac2='" + item.ape_fac2+ "' data-retdato='" + item.ape_retdato+ "' ";
                row.removable = false;

                row.cells.Add(new HtmlCell { valor = "<a class='btn' href='#' onclick='EditAutorizacion(this.parentNode.parentNode)'><i class='iconfa-edit'></i></a> <a class='btn' href='#' onclick='DeleteAutorizacion(this.parentNode.parentNode)'><i class='iconfa-remove'></i></a> " });
                row.cells.Add(new HtmlCell { valor = item.ape_nro_autoriza});//ID CUENTA
                row.cells.Add(new HtmlCell { valor = item.ape_retdatocampo});//ID CUENTA
                row.cells.Add(new HtmlCell { valor = item.ape_val_fecha });//NOMBRE CUENTA
                row.cells.Add(new HtmlCell { valor = item.ape_fac1+"-"+item.ape_fac2+"-"+item.ape_fac3 });
                row.cells.Add(new HtmlCell { valor = item.ape_fact1 + "-" + item.ape_fact2 + "-" + item.ape_fact3});                
                row.cells.Add(new HtmlCell { valor = item.ape_estado.Value == 1 ? "SI" : "NO" });
                tdatos.AddRow(row);


                //ArrayList array = new ArrayList();
                //array.Add("");
                //array.Add(item.ape_nro_autoriza);
                //array.Add(item.ape_val_fecha);
                //array.Add(item.ape_fac1);
                //array.Add(item.ape_fac2);
                //array.Add(item.ape_fac3);
                //array.Add(item.ape_fact3);
                //array.Add(item.ape_estado);
                //string strid = "{\"ape_empresa\":\"" + item.ape_empresa + "\", \"ape_nro_autoriza\":\"" + item.ape_nro_autoriza + "\", \"ape_fac1\":\"" + item.ape_fac1 + "\", \"ape_fac2\":\"" + item.ape_fac2 + "\", \"ape_retdato\":\"" +item.ape_retdato+ "\"}";//ID COMPUESTO
                //html.AppendLine(HtmlElements.TablaRow(array, strid));
            }
            html.AppendLine(tdatos.ToString());


            return html.ToString();


        }



        [WebMethod]
        public static string EditAutorizacion(object objeto)
        {
            Autpersona aut = new Autpersona(objeto);
            aut.ape_empresa_key = aut.ape_empresa;
            aut.ape_nro_autoriza_key = aut.ape_nro_autoriza;
            aut.ape_fac1_key = aut.ape_fac1;
            aut.ape_fac2_key = aut.ape_fac2;
            aut.ape_retdato_key = aut.ape_retdato;
            aut = AutpersonaBLL.GetByPK(aut);
            
            
             
            return ShowObject(aut);
        }


        [WebMethod]
        public static string DeleteAutorizacion(object objeto)
        {
            Autpersona aut = new Autpersona(objeto);
            aut.ape_empresa_key = aut.ape_empresa;
            aut.ape_nro_autoriza_key = aut.ape_nro_autoriza;
            aut.ape_fac1_key = aut.ape_fac1;
            aut.ape_fac2_key = aut.ape_fac2;
            aut.ape_retdato_key = aut.ape_retdato;
            AutpersonaBLL.Delete(aut);




            return "OK";
        }
    }


     




    }


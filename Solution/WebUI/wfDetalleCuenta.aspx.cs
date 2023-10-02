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
    public partial class wfDetalleCuenta : System.Web.UI.Page
    {

        protected static int pageIndex;
        protected static int pageSize;
        protected static string OrderByClause = "cue_codigo";
        protected static string WhereClause = "";
        protected static WhereParams parametros;
       
       
        protected static Int32 codigocue;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                pageIndex = 1;
                pageSize = 20;
            }

        }

             [WebMethod]
        public static string GetCabecera(object objeto)
        {
            Empresa emp = new Empresa(objeto);
            emp.emp_codigo_key = emp.emp_codigo;
            emp = EmpresaBLL.GetByPK(emp);

            Cuenta cuenta = new Cuenta();
            cuenta.cue_codigo_key = codigocue;
            cuenta.cue_empresa_key = emp.emp_codigo;
            cuenta = CuentaBLL.GetByPK(cuenta);
           
            StringBuilder html = new StringBuilder();
            html.AppendLine("<div class=\"row-fluid\">");
            html.AppendLine("<div class=\"span6\">");
            HtmlTable tdatos = new HtmlTable();
            tdatos.CreteEmptyTable(3, 2);
            tdatos.rows[0].cells[0].valor = "Id:";
            tdatos.rows[0].cells[1].valor = new Input { id = "id_S", autocomplete = "GetCuentaObj", obligatorio = true, valor = cuenta.cue_id, clase = Css.medium, placeholder = "Cuenta_id" }.ToString();
            tdatos.rows[1].cells[0].valor = "Cuenta:";
            tdatos.rows[1].cells[1].valor = new Input { id = "cuenta_S", autocomplete = "GetCuentaObj", obligatorio = true, valor = cuenta.cue_nombre, clase = Css.medium, placeholder = "Cuenta_nombre" }.ToString();
            tdatos.rows[2].cells[0].valor = "Nivel";
            tdatos.rows[2].cells[1].valor = new Input { id = "nivel_C", clase = Css.medium, placeholder = "Nivel" }.ToString();
            html.AppendLine(tdatos.ToString());
            html.AppendLine(" </div><!--span6-->");
            html.AppendLine("<div class=\"span6\">");
            
            return html.ToString();
        }
  
        [WebMethod]
        public static string GetDetalle()
        {
            pageIndex = 1;
            StringBuilder html = new StringBuilder();
            HtmlTable tdatos = new HtmlTable();
            tdatos.id = "tddatos";
            tdatos.invoice = true;
            tdatos.clase = "scrolltable";
            tdatos.AddColumn("", "", "");
            
            tdatos.AddColumn("ID", "", "", "");
            tdatos.AddColumn("Cuenta", "", "", "");
            tdatos.AddColumn("Modulo", "", "", "");
            tdatos.AddColumn("Genero", "", "", "");
            tdatos.AddColumn("Movimiento", "", "", "");
            tdatos.AddColumn("Visualiza", "", "", "");
            tdatos.AddColumn("Negrita", "", "", "");
            tdatos.AddColumn("Estado", "", "", "");
          
            tdatos.editable = false;
            html.AppendLine(tdatos.ToString());
           
            return html.ToString();
        }

    

        [WebMethod]
        public static string GetDetalleData(object objeto)
        {
           
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object cuenta = null;
         
            tmp.TryGetValue("cuenta", out cuenta);
           
            SetWhereClause(new Cuenta(cuenta));

          
           
           
            int desde = (pageIndex * pageSize) - pageSize + 1;
            int hasta = (pageIndex * pageSize);
            pageIndex++;
            List<Cuenta> lst = CuentaBLL.GetAllByPage(parametros, OrderByClause, desde, hasta);
            StringBuilder html = new StringBuilder();
            foreach (Cuenta item in lst)
            {
                string opc = "<div class=\"btn-group\"> " +
                           "<button data-toggle=\"dropdown\" class=\"btn btn-inverse dropdown-toggle\"><span class=\"caret\"></span></button> " +
                           "<ul class=\"dropdown-menu\">";
                opc += "<li><a href=\"#\" onclick=\"Agregarhijo('" + item.cue_codigo + "')\">Agregar hijo</a></li>";//ANULAR  
                opc += "<li><a href=\"#\" onclick=\"Editar('" + item.cue_codigo + "')\">Editar</a></li>";//ANULAR  
                opc += "<li><a href=\"#\" onclick=\"Eliminar('" + item.cue_codigo + "')\">Eliminar</a></li>";//ANULAR
                ArrayList array = new ArrayList();
                array.Add(opc);

                array.Add(item.cue_id);
                array.Add(item.cue_nombre);
                array.Add(item.cue_modulo);
                array.Add(item.cue_genero);

                string check;
                if (item.cue_movimiento == 1)
                {
                    check = "<input type=\"checkbox\"checked= \"\" disabled=\"disabled\"name=\"vehicle\" value=\"\"><br>";
                }
                else
                {

                    check = "<input type=\"checkbox\" disabled=\" disabled\" name=\"vehicle\" value=\"\"><br>";
                }
                      array.Add(check);

                      if (item.cue_visualiza == 1)
                      {
                          check = "<input type=\"checkbox\"checked= \"\" disabled=\"disabled\" name=\"vehicle\" value=\"\"><br>";
                      }
                      else
                      {

                          check = "<input type=\"checkbox\" disabled=\" disabled\" name=\"vehicle\" value=\"\"><br>";
                      }
                     array.Add(check);

                     if (item.cue_negrita == 1)
                     {
                         check = "<input type=\"checkbox\"checked= \"\" disabled=\"disabled\" name=\"vehicle\" value=\"\"><br>";
                     }
                     else
                     {

                         check = "<input type=\"checkbox\" disabled=\" disabled\" name=\"vehicle\" value=\"\"><br>";
                     }
                     array.Add(check);

                     if (item.cue_estado == 1)
                     {
                         check = "<input type=\"checkbox\"checked= \"\" disabled=\"disabled\" name=\"vehicle\" value=\"\"><br>";
                     }
                     else
                     {

                         check = "<input type=\"checkbox\" disabled=\"disabled\" name=\"vehicle\" value=\"\"><br>";

                     }



                     array.Add(check);

              
                string strid = "{\"cue_codigo\":\"" + item.cue_codigo + "\"}";//ID COMPUESTO
                html.AppendLine(HtmlElements.TablaRow(array, strid));
            }

          
            return html.ToString();
        }

        public static void SetWhereClause(Cuenta obj)
        {
            int contador = 0;
            parametros = new WhereParams();
            List<object> valores = new List<object>();
         
          
            if (obj.cue_nivel > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_nivel <= {" + contador + "} ";
                valores.Add(obj.cue_nivel);
                contador++;
            }
            if (obj.cue_movimiento > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_movimiento = {" + contador + "} ";
                valores.Add(obj.cue_movimiento);
                contador++;
            }
            if (obj.cue_modulo > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_modulo = {" + contador + "} ";
                valores.Add(obj.cue_modulo);
                contador++;
            } if (obj.cue_id != "")
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_id like {" + contador + "} ";
                valores.Add("%" + obj.cue_id + "%");
                contador++;
            }
            if (obj.cue_nombre != "")
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_nombre like {" + contador + "} ";
                valores.Add("%" + obj.cue_nombre + "%");
                contador++;
            }
            parametros.valores = valores.ToArray();
        }

        [WebMethod]
        public static string GetFormS()
        {
            return ShowObjectEditar(new Cuenta());
        }
          [WebMethod]

        public static string ShowObject(Cuenta obj)
        {
            StringBuilder html = new StringBuilder();

            List<Cuenta> lst = CuentaBLL.GetAll("cue_codigo =" + obj.cue_reporta, "");
            List<Cuenta> lstn = CuentaBLL.GetAll("cue_orden =" + obj.cue_orden, "");
            html.AppendLine(new Input { id = "txtCODIGO", valor = lstn[0].cue_codigo, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtCODIGO_key", valor = lstn[0].cue_codigo, visible = false }.ToString());
            html.AppendLine(new Input { id = "txtIDPadre", etiqueta = "IdPadre", placeholder = "Id", valor = lst[0].cue_id, clase = Css.large, habilitado= false }.ToString());
            html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.cue_id, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cue_nombre, clase = Css.large, obligatorio = true }.ToString());
            html.AppendLine(new Select { id = "cmbMODULO", etiqueta = "Modulo", valor = obj.cue_modulo.ToString(), clase = Css.large, diccionario = Dictionaries.GetModulos(), obligatorio = true }.ToString());
            //  html.AppendLine(new Input { id = "txtGENERO", etiqueta = "Genero", placeholder = "Genero", valor = obj.cue_genero.ToString(), clase = Css.large, obligatorio = true }.ToString());
            //    html.AppendLine(new Input { id = "", etiqueta = "Movimiento", placeholder = "Movimiento", valor = obj.cue_movimiento.ToString(), clase = Css.large }.ToString());
            html.AppendLine(new Select { id = "txtGENERO", etiqueta = "Genero", valor = obj.cue_genero.ToString(), clase = Css.large, diccionario = Dictionaries.GetGenCuenta(), obligatorio = true }.ToString());



            html.AppendLine(new Check { id = "chkMOVIMIENTO", etiqueta = "Movimiento ", valor = obj.cue_movimiento }.ToString());
            html.AppendLine(new Check { id = "chkNEGRITA", etiqueta = "Visualiza ", valor = obj.cue_negrita }.ToString());
            html.AppendLine(new Check { id = "chkVISUALIZA", etiqueta = "Negrita ", valor = obj.cue_visualiza }.ToString());
            html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.cue_estado }.ToString());
          
            html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
            return html.ToString();
        }


          [WebMethod]
          public static string SaveObjectEditar(object objeto)
          {

              Cuenta obj = new Cuenta(objeto);
              obj.cue_codigo_key = obj.cue_codigo;
              obj.cue_empresa_key = obj.cue_empresa;

              Cuenta opt = obj;
              obj = CuentaBLL.GetByPK(obj);
              opt.cue_orden = obj.cue_orden;
              opt.cue_reporta = obj.cue_reporta;


              if (opt.cue_movimiento > 0)
              {

                  parametros = new WhereParams("cue_reporta = {0} ", opt.cue_codigo);
                  if (CuentaBLL.GetRecordCount(parametros, "") > 0)
                  {
                      opt.cue_movimiento = 0;
                  }
              }



              if (CuentaBLL.Update(opt) > 0)
              {
                  //return ShowData(obj);
                  return "OK";
              }
              else
                  return "ERROR";

          }

          [WebMethod]
          public static string GetForm(object objeto)
          {
              return ShowObject(new Cuenta());
          }

          [WebMethod]
           public static string GetFormEditar(object objeto)
              {
              Cuenta obj = new Cuenta(objeto);
              obj.cue_codigo_key = obj.cue_codigo;
              obj.cue_empresa_key = obj.cue_empresa;
              

              Cuenta opt = obj;
              obj = CuentaBLL.GetByPK(obj);
              return ShowObjectEditar(obj);
             }
          [WebMethod]

          public static string ShowObjectEditar(Cuenta obj)
          {
               StringBuilder html = new StringBuilder();
             

              html.AppendLine(new Input { id = "txtCODIGO", valor = obj.cue_codigo.ToString(), visible = false }.ToString());
              html.AppendLine(new Input { id = "txtCODIGO_key", valor = obj.cue_codigo_key.ToString(), visible = false }.ToString());
             
              html.AppendLine(new Input { id = "txtID", etiqueta = "Id", placeholder = "Id", valor = obj.cue_id, clase = Css.large, obligatorio = true }.ToString());
              html.AppendLine(new Input { id = "txtNOMBRE", etiqueta = "Nombre", placeholder = "Nombre", valor = obj.cue_nombre, clase = Css.large, obligatorio = true }.ToString());
              html.AppendLine(new Select { id = "cmbMODULO", etiqueta = "Modulo", valor = obj.cue_modulo.ToString(), clase = Css.large, diccionario = Dictionaries.GetModulos(), obligatorio = true }.ToString());
              //  html.AppendLine(new Input { id = "txtGENERO", etiqueta = "Genero", placeholder = "Genero", valor = obj.cue_genero.ToString(), clase = Css.large, obligatorio = true }.ToString());
              //    html.AppendLine(new Input { id = "", etiqueta = "Movimiento", placeholder = "Movimiento", valor = obj.cue_movimiento.ToString(), clase = Css.large }.ToString());
              html.AppendLine(new Select { id = "txtGENERO", etiqueta = "Genero", valor = obj.cue_genero.ToString(), clase = Css.large, diccionario = Dictionaries.GetGenCuenta(), obligatorio = true }.ToString());



              html.AppendLine(new Check { id = "chkMOVIMIENTO", etiqueta = "Movimiento ", valor = obj.cue_movimiento }.ToString());
              html.AppendLine(new Check { id = "chkNEGRITA", etiqueta = "Visualiza ", valor = obj.cue_negrita }.ToString());
              html.AppendLine(new Check { id = "chkVISUALIZA", etiqueta = "Negrita ", valor = obj.cue_visualiza }.ToString());
              html.AppendLine(new Check { id = "chkESTADO", etiqueta = "Activo ", valor = obj.cue_estado }.ToString());

              html.AppendLine(new Auditoria { creausr = obj.crea_usr, creafecha = obj.crea_fecha, modusr = obj.mod_usr, modfecha = obj.mod_fecha }.ToString());
              return html.ToString();
          }

          [WebMethod]
          public static string AddChildOption(object objeto)
          {


              Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
              object codigo = null;
              object empresa = null;
              tmp.TryGetValue("cue_codigo", out codigo);
              tmp.TryGetValue("cue_empresa", out empresa);

              Cuenta parentobj = new Cuenta();

              if (empresa != null && !empresa.Equals(""))
              {
                  parentobj.cue_empresa = int.Parse(empresa.ToString());
                  parentobj.cue_empresa_key = int.Parse(empresa.ToString());
              }
              if (codigo != null && !codigo.Equals(""))
              {
                  parentobj.cue_codigo = int.Parse(codigo.ToString());
                  parentobj.cue_codigo_key = int.Parse(codigo.ToString());
              }
              parentobj = CuentaBLL.GetByPK(parentobj);


              int max = CuentaBLL.GetMax("cue_codigo") + 1;


              Cuenta obj = new Cuenta();

              obj.cue_empresa = parentobj.cue_empresa;
              obj.cue_modulo = parentobj.cue_modulo;
              obj.cue_genero = parentobj.cue_genero;
              obj.cue_nombre = "Nueva Opción";
              obj.cue_orden = max;
              obj.cue_reporta = int.Parse(codigo.ToString());
              obj.cue_estado = 1;
              obj.cue_nivel = parentobj.cue_nivel + 1;
              if (CuentaBLL.Insert(obj) > 0)
              {
                  return ShowObject(obj);
                 
              }
              else
                  return "ERROR";

          }


          [WebMethod]
          public static string DeleteObject(object objeto)
          {
              Cuenta obj = new Cuenta(objeto);
              List<Cuenta> lst = CuentaBLL.GetAll("cue_codigo =" + obj.cue_codigo, "");
              obj.cue_codigo_key = obj.cue_codigo;
              obj.cue_empresa_key = obj.cue_empresa;

              if (lst[0].cue_movimiento > 0)
              {

                  return "ERROR";
              }
              else
              {
                  if (CuentaBLL.Delete(obj) > 0)
                  {
                      return "OK";
                  }
                  else
                      return "ERROR";
              }
          }


          [WebMethod]
          public static string SaveObject(object objeto)
          {

              Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
             
              object empresa = null;
              object modulo = null;
              object genero = null;
              object nombre = null;
              object estado = null;
              object cue_id = null;
              object movimiento = null;
              object negrita = null;
              object visualiza = null;
            
              tmp.TryGetValue("cue_empresa", out empresa);
              tmp.TryGetValue("cue_modulo", out modulo);
              tmp.TryGetValue("cue_genero", out genero);
              tmp.TryGetValue("cue_nombre", out nombre);
              tmp.TryGetValue("cue_estado", out estado);
              tmp.TryGetValue("cue_id", out cue_id);
              tmp.TryGetValue("cue_movimiento", out movimiento);
              tmp.TryGetValue("cue_negrita", out negrita);
              tmp.TryGetValue("cue_visualiza", out visualiza);
              int max = CuentaBLL.GetMax("cue_codigo") + 1;


              Cuenta obj = new Cuenta();
              
              obj.cue_empresa = int.Parse(empresa.ToString());
              obj.cue_id = cue_id.ToString();
              obj.cue_modulo = int.Parse(modulo.ToString());
              obj.cue_genero = int.Parse(genero.ToString());
              obj.cue_nombre = nombre.ToString();
              obj.cue_orden = max;
              obj.cue_estado = int.Parse(estado.ToString());
              obj.cue_movimiento = int.Parse(movimiento.ToString());
              obj.cue_negrita = int.Parse(negrita.ToString());
              obj.cue_visualiza = int.Parse(visualiza.ToString());
              if (CuentaBLL.Insert(obj) > 0)
              {
                  return "OK";
              }
              else
                  return "ERROR";

          }

        }
    }



   
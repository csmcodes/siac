using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using BusinessLogicLayer;
using System.Text;

namespace WebUI
{
    public partial class wfHojaRutaTxt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GeneraTXT(Request.QueryString["empresa"], Request.QueryString["codigo"]);
            }
        }


        public void GeneraTXT(string empresa, string codigo)
        {


            Comprobante comprobante = new Comprobante();
            comprobante.com_empresa = int.Parse(empresa);
            comprobante.com_codigo = long.Parse(codigo);
            comprobante.com_empresa_key = comprobante.com_empresa;
            comprobante.com_codigo_key = comprobante.com_codigo;
            comprobante = ComprobanteBLL.GetByPK(comprobante);



            Vehiculo vehiculo = new Vehiculo();
            vehiculo.veh_empresa = comprobante.com_empresa;
            vehiculo.veh_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                vehiculo.veh_codigo = comprobante.com_vehiculo.Value;
                vehiculo.veh_codigo_key = comprobante.com_vehiculo.Value;
                vehiculo = VehiculoBLL.GetByPK(vehiculo);
            }



            Ruta ruta = new Ruta();
            ruta.rut_empresa = comprobante.com_empresa;
            ruta.rut_empresa_key = comprobante.com_empresa;
            if (comprobante.com_ruta.HasValue)
            {
                ruta.rut_codigo = comprobante.com_ruta.Value;
                ruta.rut_codigo_key = comprobante.com_ruta.Value;
                ruta = RutaBLL.GetByPK(ruta);
            }


            Persona socio = new Persona();
            socio.per_empresa = comprobante.com_empresa;
            socio.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                socio.per_codigo = vehiculo.veh_duenio.Value;
                socio.per_codigo_key = vehiculo.veh_duenio.Value;
                socio = PersonaBLL.GetByPK(socio);
            }


            Persona chofer = new Persona();
            chofer.per_empresa = comprobante.com_empresa;
            chofer.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                chofer.per_codigo = vehiculo.veh_chofer1.Value;
                chofer.per_codigo_key = vehiculo.veh_chofer1.Value;
                chofer = PersonaBLL.GetByPK(chofer);
            }

            Persona chofer2 = new Persona();
            chofer2.per_empresa = comprobante.com_empresa;
            chofer2.per_empresa_key = comprobante.com_empresa;
            if (comprobante.com_vehiculo.HasValue)
            {
                chofer2.per_codigo = vehiculo.veh_chofer2.Value;
                chofer2.per_codigo_key = vehiculo.veh_chofer2.Value;
                chofer2 = PersonaBLL.GetByPK(chofer2);
            }

            StringBuilder csv = new StringBuilder();
            string separador = ";";

            List<vHojadeRuta> lst = vHojadeRutaBLL.GetAll(new WhereParams("cabecera.com_codigo={0}", comprobante.com_codigo), "cabecera.com_fecha");
            if (lst.Count > 0)
            {
                csv.Append("HOJARUTA" + separador);
                csv.Append("FECHA_CABECERA" + separador);
                csv.Append("ORIGEN" + separador);
                csv.Append("DESTINO" + separador);
                csv.Append("CIRUC_SOCIO" + separador);
                csv.Append("SOCIO" + separador);                
                csv.Append("PLACA_VEH" + separador);
                csv.Append("DISCO_VEH" + separador);
                csv.Append("CIRUC_CHOFER" + separador);
                csv.Append("CHOFER" + separador);

                csv.Append("FECHA_DETALLE" + separador);
                csv.Append("COMPROBANTE_DETALLE" + separador);
                csv.Append("CIRUC_CLIENTE"+separador);
                csv.Append("CLIENTE" + separador);
                csv.Append("REMITENTE" + separador);
                csv.Append("DESTINATARIO" + separador);

                csv.Append("SUBTOTAL_0" + separador);
                csv.Append("SUBTOTAL_12" + separador);
                csv.Append("SEGURO" + separador);
                csv.Append("IMPUESTO" + separador);
                csv.Append("TOTAL" + separador);
                csv.AppendLine();


                foreach (vHojadeRuta item in lst)
                {

                    csv.Append(comprobante.com_doctran + separador);
                    csv.Append(comprobante.com_fecha.ToString("dd/MM/yyyy HH:mm") + separador);
                    csv.Append(ruta.rut_origen + separador);
                    csv.Append(ruta.rut_destino + separador);
                    csv.Append(socio.per_ciruc + separador);                                        
                    csv.Append(socio.per_razon + separador);
                    csv.Append(vehiculo.veh_placa + separador);
                    csv.Append(vehiculo.veh_disco + separador);
                    csv.Append(chofer.per_ciruc + separador);
                    csv.Append(chofer.per_razon + separador);

                    csv.Append(item.fechadetalle + separador);
                    csv.Append(item.doctrandetalle + separador);
                    csv.Append(item.ciruccliente + separador);
                    csv.Append(item.nombrecliente + " " + item.apellidocliente + separador);                    
                    csv.Append(item.nombreremitente + " " + item.apellidoremitente + separador);
                    csv.Append(item.nombredestinatario + " " + item.apellidosdestinatario + separador);

                    csv.Append(Functions.Formatos.CurrencyFormat(item.subtotaldetalle) + separador);
                    csv.Append(Functions.Formatos.CurrencyFormat(item.subtotal12detalle) + separador);
                    csv.Append(Functions.Formatos.CurrencyFormat(item.segurodetalle) + separador);
                    csv.Append(Functions.Formatos.CurrencyFormat(item.impuestodetalle) + separador);
                    csv.Append(Functions.Formatos.CurrencyFormat(item.totaldetalle) + separador);
                    csv.AppendLine();

                }


                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + comprobante.com_doctran + ".csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv.ToString());
                Response.Flush();
                Response.End();
            }

            
        }
    }
}
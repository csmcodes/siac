using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using System.Web.Script.Serialization;
using System.Globalization;

namespace Packages
{
    public class XmlReader
    {
        public XmlReader()
        {

        }


        public static string GetString(XmlNode nodo)
        {
            if (nodo != null)
                return nodo.InnerText;
            else
                return null;
        }

        public static decimal? GetDecimal(XmlNode nodo)
        {
            if (nodo != null)
            {
                return GetDecimal(nodo.InnerText);
            }
            return null;
        }

        public static decimal GetDecimal(string valor)
        {
            decimal valordecimal = 0;
            decimal.TryParse(valor.Replace('.', ','), out valordecimal);
            return valordecimal;
        }

        public static int? GetEntero(XmlNode nodo)
        {
            if (nodo != null)
            {
                return GetEntero(nodo.InnerText);
            }
            return null;
        }

        public static int GetEntero(string valor)
        {
            int valorentero = 0;
            int.TryParse(valor.Replace('.', ','), out valorentero);
            return valorentero;
        }

        public static DateTime? GetDateTime(XmlNode nodo)
        {
            if (nodo != null)
            {
                return GetDateTime(nodo.InnerText);
            }
            return null;
        }
        public static DateTime? GetDateTimeFormat(XmlNode nodo, string format)
        {
            if (nodo != null)
            {
                return GetDateTimeFormat(nodo.InnerText,format);
            }
            return null;
        }


        public static DateTime GetDateTime(string valor)
        {
            DateTime date;
            DateTime.TryParse(valor, out date);
            return date;
        }

        public static DateTime GetDateTimeFormat(string valor, string format)
        {
            DateTime date;
            DateTime.TryParseExact(valor, format,CultureInfo.InvariantCulture,DateTimeStyles.None, out date);
            return date;
        }

        public static Comprobante CargarComprobante(string xml, string nroautoriza, DateTime? fechaautoriza)
        {
            string plantillaobl = Constantes.GetParameter("plantillaobl");
            string plantillaret = Constantes.GetParameter("plantillaret");
            var serializer = new JavaScriptSerializer();
            Comprobante comprobante = new Comprobante();
            if (!string.IsNullOrEmpty(xml))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);
                var tipo = xmldoc.SelectSingleNode("/").LastChild.Name;
                if (tipo.ToUpper() == "FACTURA")
                {
                    comprobante = serializer.Deserialize<Comprobante>(plantillaobl);
                    return CargarObligacion(xml, comprobante, nroautoriza, fechaautoriza);
                }
                if (tipo.ToUpper() == "COMPROBANTERETENCION")
                {
                    comprobante = serializer.Deserialize<Comprobante>(plantillaret);
                    return CargarReciboRetencionNew(xml, comprobante, nroautoriza, fechaautoriza);
                }

            }

            return comprobante;
        }

        

        public static Comprobante CargarObligacion(string xml, Comprobante comprobante, string nroautoriza, DateTime? fechaautoriza)
        {

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var serializer = new JavaScriptSerializer();

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.total = new Total();


            try
            {

                if (!string.IsNullOrEmpty(xml))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xml);




                    comprobante.com_claveelec = xmldoc.SelectSingleNode("/factura/infoTributaria/claveAcceso").InnerText;

                    //comprobante.ccomdoc.cdoc_acl_nroautoriza = comprobante.

                    string establecimiento = xmldoc.SelectSingleNode("/factura/infoTributaria/estab").InnerText;
                    string puntoemision = xmldoc.SelectSingleNode("/factura/infoTributaria/ptoEmi").InnerText;
                    string secuencia = xmldoc.SelectSingleNode("/factura/infoTributaria/secuencial").InnerText;

                    comprobante.ccomdoc.cdoc_acl_nroautoriza = nroautoriza;
                    comprobante.ccomdoc.cdoc_acl_retdato = 20000004;
                    comprobante.ccomdoc.cdoc_acl_tablacoa = 4;
                    comprobante.ccomdoc.cdoc_aut_factura = string.Format("{0}-{1}-{2}", establecimiento, puntoemision, secuencia);
                    comprobante.ccomdoc.cdoc_aut_fecha = fechaautoriza;

                    //comprobante.com_autorizacion = 
                    comprobante.com_ambiente = xmldoc.SelectSingleNode("/factura/infoTributaria/ambiente").InnerText;
                    comprobante.com_emision = xmldoc.SelectSingleNode("/factura/infoTributaria/tipoEmision").InnerText;

                    comprobante.ccomdoc.cdoc_ced_ruc = xmldoc.SelectSingleNode("/factura/infoTributaria/ruc").InnerText;
                    comprobante.ccomdoc.cdoc_nombre = xmldoc.SelectSingleNode("/factura/infoTributaria/razonSocial").InnerText;
                    comprobante.ccomdoc.cdoc_direccion = xmldoc.SelectSingleNode("/factura/infoTributaria/dirMatriz").InnerText;

                    comprobante.com_fecha = GetDateTime(xmldoc.SelectSingleNode("/factura/infoFactura/fechaEmision")).Value;
                    comprobante.com_fechastr = GetString(xmldoc.SelectSingleNode("/factura/infoFactura/fechaEmision"));

                    //comprobante.com_contribuyente = GetString(xmldoc.SelectSingleNode("/factura/infoFactura/contribuyenteEspecial"));
                    //comprobante.com_contabilidad = xmldoc.SelectSingleNode("/factura/infoFactura/obligadoContabilidad").InnerText;

                    comprobante.total.tot_subtotal = GetDecimal(xmldoc.SelectSingleNode("/factura/infoFactura/totalSinImpuestos").InnerText);
                    comprobante.total.tot_total = GetDecimal(xmldoc.SelectSingleNode("/factura/infoFactura/importeTotal").InnerText);


                    //Busca o crea Persona




                    Persona per = new Persona();
                    List<Persona> lstper = PersonaBLL.GetAll("per_empresa=" + comprobante.com_empresa + " and per_ciruc='" + comprobante.ccomdoc.cdoc_ced_ruc + "'", "");
                    if (lstper.Count > 0)
                    {
                        per = lstper[0];
                        /// Autorizacion
                        Autpersona aut = AutpersonaBLL.GetByPK(new Autpersona { ape_empresa = comprobante.com_empresa, ape_empresa_key = comprobante.com_empresa, ape_nro_autoriza = nroautoriza, ape_nro_autoriza_key = nroautoriza, ape_fac1 = establecimiento, ape_fac1_key = establecimiento, ape_fac2 = puntoemision, ape_fac2_key = puntoemision, ape_retdato = 20000004, ape_retdato_key = 20000004 });
                        if (!aut.crea_fecha.HasValue)
                        {
                            aut = new Autpersona();
                            aut.ape_empresa = comprobante.com_empresa;
                            aut.ape_persona = per.per_codigo;
                            aut.ape_tclipro = Constantes.cProveedor;
                            aut.ape_val_fecha = fechaautoriza;
                            aut.ape_nro_autoriza = nroautoriza;
                            aut.ape_fac1 = establecimiento;
                            aut.ape_fac2 = puntoemision;
                            aut.ape_fac3 = secuencia;
                            aut.ape_fact1 = establecimiento;
                            aut.ape_fact2 = puntoemision;
                            aut.ape_fact3 = secuencia;
                            aut.ape_retdato = 20000004;//FACTURAS DE COMPRA
                            aut.ape_tablacoa = 4;
                            aut.ape_estado = 1;
                            aut.crea_usr = comprobante.crea_usr;
                            aut.crea_fecha = DateTime.Now;

                            AutpersonaBLL.Insert(aut);
                        }
                    }
                    else
                    {

                        Empresa emp = new Empresa();
                        emp.emp_codigo_key = comprobante.com_empresa;
                        emp = EmpresaBLL.GetByPK(emp);

                        Usuario usr = new Usuario();
                        usr.usr_id_key = comprobante.crea_usr;
                        usr = UsuarioBLL.GetByPK(usr);

                        BLL transaction = new BLL();
                        transaction.CreateTransaction();

                        try
                        {
                            transaction.BeginTransaction();

                            per.per_empresa = comprobante.com_empresa;
                            per.per_ciruc = comprobante.ccomdoc.cdoc_ced_ruc;
                            per.per_nombres = comprobante.ccomdoc.cdoc_nombre;
                            per.per_razon = per.per_nombres;
                            per.per_direccion = comprobante.ccomdoc.cdoc_direccion;
                            per.per_tipoid = "RUC";
                            per.per_id = General.GetIdPersona(emp, usr);
                            per.per_retfuente = Constantes.GetImpRteFte().imp_codigo;
                            per.per_retiva = Constantes.GetImpRteIVA().imp_codigo;
                            per.per_estado = Constantes.cEstadoGrabado;


                            per.per_codigo = PersonaBLL.InsertIdentity(transaction, per);

                            Politica politica = Constantes.GetPoliticaProv();//Obtiene politica por defecto
                            per.per_politica = politica.pol_codigo;
                            per.per_politicaid = politica.pol_id;
                            per.per_politicanombre = politica.pol_nombre;
                            per.per_politicadesc = politica.pol_porc_desc;
                            per.per_politicanropagos = politica.pol_nro_pagos;
                            per.per_politicadiasplazo = politica.pol_dias_plazo;
                            per.per_politicaporpagocon = politica.pol_porc_pago_con;

                            Catcliente catcliente = Constantes.GetCatProv();//Obtiene politica por defecto


                            Personaxtipo pxt = new Personaxtipo();
                            pxt.pxt_empresa = per.per_empresa;
                            pxt.pxt_persona = per.per_codigo;
                            pxt.pxt_tipo = Constantes.cProveedor;
                            pxt.pxt_politicas = politica.pol_codigo;
                            pxt.pxt_cat_persona = catcliente.cat_codigo;
                            pxt.crea_usr = per.crea_usr;
                            pxt.crea_fecha = per.crea_fecha;
                            pxt.mod_usr = per.mod_usr;
                            pxt.mod_fecha = per.mod_fecha;
                            PersonaxtipoBLL.Insert(transaction, pxt);



                            //Autorizacion
                            Autpersona aut = new Autpersona();
                            aut.ape_empresa = comprobante.com_empresa;
                            aut.ape_persona = per.per_codigo;
                            aut.ape_tclipro = Constantes.cProveedor;
                            aut.ape_val_fecha = fechaautoriza;
                            aut.ape_nro_autoriza = nroautoriza;
                            aut.ape_fac1 = establecimiento;
                            aut.ape_fac2 = puntoemision;
                            aut.ape_fac3 = secuencia;
                            aut.ape_fact1 = establecimiento;
                            aut.ape_fact2 = puntoemision;
                            aut.ape_fact3 = secuencia;
                            aut.ape_retdato = 20000004;//FACTURAS DE COMPRA
                            aut.ape_tablacoa = 4;
                            aut.ape_estado = 1;
                            aut.crea_usr = comprobante.crea_usr;
                            aut.crea_fecha = DateTime.Now;

                            AutpersonaBLL.Insert(transaction, aut);





                            transaction.Commit();
                            //return codigo.ToString();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }




                    }







                    comprobante.com_codclipro = per.per_codigo;

                    string incomp = "(14,26,36)";
                    List<vObligacion> list = vObligacionBLL.GetAll(new WhereParams("cdoc_aut_factura={0} and com_estado <> {1} and com_tipodoc in " + incomp + " and com_codclipro={2}", comprobante.ccomdoc.cdoc_aut_factura, Constantes.cEstadoEliminado, comprobante.com_codclipro), "");

                    if (list.Count > 0)
                        throw new ArgumentException("La factura " + comprobante.ccomdoc.cdoc_aut_factura + " ya fue ingresada");


                    XmlNode totalimpuestos = xmldoc.SelectSingleNode("/factura/infoFactura/totalConImpuestos");



                    decimal porciva = Constantes.GetValorIVA(comprobante.com_fecha);
                    decimal subtotaliva = 0;
                    decimal valoriva = 0;
                    decimal subtotalice = 0;
                    decimal valorice = 0;
                    decimal subtotal0 = 0;
                    decimal subtotalnoiva = 0;
                    decimal subtotalextiva = 0;

                    foreach (XmlNode totalimp in totalimpuestos.ChildNodes)
                    {
                        XmlNode codigo = totalimp.SelectSingleNode("codigo");
                        XmlNode codigoporcentaje = totalimp.SelectSingleNode("codigoPorcentaje");
                        XmlNode descuento = totalimp.SelectSingleNode("descuentoAdicional");
                        XmlNode baseimp = totalimp.SelectSingleNode("baseImponible");
                        XmlNode tarifa = totalimp.SelectSingleNode("tarifa");
                        XmlNode valor = totalimp.SelectSingleNode("valor");


                        if (codigo.InnerText == "2" && (codigoporcentaje.InnerText == "2" || codigoporcentaje.InnerText == "3"))//TARIFA 12 o 14
                        {
                            subtotaliva += GetDecimal(baseimp.InnerText);
                            valoriva += GetDecimal(valor.InnerText);
                            if (codigoporcentaje.InnerText == "2")
                                porciva = 12;
                            if (codigoporcentaje.InnerText == "3")
                                porciva = 14;
                        }
                        if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "0")//TARIFA 0
                        {
                            subtotal0 += GetDecimal(baseimp.InnerText);
                        }
                        if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "6")//NO OBJETO DE IVA
                        {
                            subtotalnoiva += GetDecimal(baseimp.InnerText);
                        }
                        if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "7")//EXENTO DE IVA
                        {
                            subtotalextiva += GetDecimal(baseimp.InnerText);
                        }
                        if (codigo.InnerText == "3")//TARIFA ICE
                        {
                            subtotalice += GetDecimal(baseimp.InnerText);
                            valorice += GetDecimal(valor.InnerText);
                        }
                    }
                    comprobante.total.tot_subtot_0 = subtotal0;
                    comprobante.total.tot_subtotal = subtotaliva;
                    //comprobante.com_subtotalnoiva = subtotalnoiva;
                    //comprobante.com_subtotalextiva = subtotalextiva;
                    comprobante.total.tot_timpuesto = valoriva;
                    comprobante.total.tot_porc_impuesto = porciva;
                    comprobante.total.tot_ice = valorice;


                    ///*PAGOS*/
                    //XmlNode pagos = xmldoc.SelectSingleNode("/factura/infoFactura/pagos");


                    //List<FormaPago> formas = serializer.Deserialize<List<FormaPago>>(Constantes.GetParameterValue("formaspago"));

                    //comprobante.formas = new List<Formapago>();

                    //if (pagos != null)
                    //{
                    //    foreach (XmlNode pago in pagos.ChildNodes)
                    //    {
                    //        Formapago fp = new Formapago();
                    //        fp.secuencia = comprobante.formas.Count() + 1;
                    //        fp.codigo = pago.SelectSingleNode("formaPago").InnerText;
                    //        FormaPago forma = formas.Find(delegate (FormaPago f) { return f.codigo == fp.codigo; });
                    //        fp.forma = forma.forma;
                    //        fp.valor = GetDecimal(pago.SelectSingleNode("total"));
                    //        fp.plazo = GetEntero(pago.SelectSingleNode("plazo"));
                    //        fp.tiempo = GetString(pago.SelectSingleNode("unidadTiempo"));
                    //        comprobante.formas.Add(fp);

                    //    }
                    //}

                    /*DETALLES*/
                    XmlNode detalles = xmldoc.SelectSingleNode("/factura/detalles");


                    comprobante.ccomdoc.detalle = new List<Dcomdoc>();




                    string ctagasto = Constantes.GetParameter("ctagasto");
                    int cgasto = 0;
                    int.TryParse(ctagasto, out cgasto);

                    foreach (XmlNode detalle in detalles.ChildNodes)
                    {
                        Dcomdoc det = new Dcomdoc();
                        det.ddoc_secuencia = comprobante.ccomdoc.detalle.Count() + 1;
                        det.ddoc_cuenta = cgasto;
                        det.ddoc_cuentanombre = "GASTOS";
                        det.ddoc_observaciones = GetString(detalle.SelectSingleNode("descripcion"));

                        det.ddoc_cantidad = GetDecimal(detalle.SelectSingleNode("cantidad").InnerText);
                        det.ddoc_precio = GetDecimal(detalle.SelectSingleNode("precioUnitario").InnerText);
                        det.ddoc_dscitem = GetDecimal(detalle.SelectSingleNode("descuento").InnerText);
                        det.ddoc_total = GetDecimal(detalle.SelectSingleNode("precioTotalSinImpuesto").InnerText);

                        XmlNode impuestosdet = detalle.SelectSingleNode("impuestos");
                        foreach (XmlNode impuesto in impuestosdet.ChildNodes)
                        {
                            if (impuesto.SelectSingleNode("codigo").InnerText == "2")//IVA
                            {
                                det.ddoc_grabaiva = 1;
                                //det.porciva = GetDecimal(impuesto.SelectSingleNode("tarifa").InnerText);
                                //det.valiva = GetDecimal(impuesto.SelectSingleNode("valor").InnerText);
                            }
                            //if (impuesto.SelectSingleNode("codigo").InnerText == "3")//ICE
                            //{
                            //    det.codice = GetString(impuesto.SelectSingleNode("codigoPorcentaje"));
                            //    det.porcice = GetDecimal(impuesto.SelectSingleNode("tarifa").InnerText);
                            //    det.valice = GetDecimal(impuesto.SelectSingleNode("valor").InnerText);
                            //}

                        }

                        //XmlNode adicionalesdet = detalle.SelectSingleNode("detallesAdicionales");
                        //if (adicionalesdet != null)
                        //{
                        //    foreach (XmlNode detadi in adicionalesdet.ChildNodes)
                        //    {
                        //        if (detadi.Attributes["nombre"].Value == "detadicional1")
                        //            det.adicional1 = detadi.Attributes["valor"].Value;
                        //        if (detadi.Attributes["nombre"].Value == "detadicional2")
                        //            det.adicional2 = detadi.Attributes["valor"].Value;
                        //        if (detadi.Attributes["nombre"].Value == "detadicional3")
                        //            det.adicional3 = detadi.Attributes["valor"].Value;
                        //    }
                        //}


                        comprobante.ccomdoc.detalle.Add(det);




                    }



                }
            }
            catch (Exception ex)
            {
                comprobante.com_codigo = -1;
                comprobante.com_doctran = ex.Message;
            }


            return comprobante;
        }

        public static void ElectronicoExists(int empresa, string clave, DateTime? fecha)
        {

            WhereParams where = new WhereParams("com_empresa = {0} and com_estado = 2 and com_claveelec = {1} and com_fecha between {2} and {3}", empresa, clave, fecha.Value.Date, fecha.Value.Date.AddDays(1));
            List<Comprobante> comprobantes = ComprobanteBLL.GetAll(where, "");
            if (comprobantes.Count > 0)
                throw new System.ArgumentException("El comprobante con nro de autorizacion " + clave + " ya se encuentra registrada en el sistema..." + comprobantes[0].com_doctran);

        }

 





        public static Comprobante CargarReciboRetencion(string xml, Comprobante comprobante, string nroautoriza, DateTime? fechaautoriza)
        {

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var serializer = new JavaScriptSerializer();

            comprobante.total = new Total();

            try
            {
                ElectronicoExists(comprobante.com_empresa, nroautoriza, fechaautoriza);
                if (!string.IsNullOrEmpty(xml))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xml);

                    string version = xmldoc.SelectSingleNode("/comprobanteRetencion").Attributes["version"].Value;


                    comprobante.com_claveelec = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/claveAcceso").InnerText;

                    //comprobante.ccomdoc.cdoc_acl_nroautoriza = comprobante.

                    string establecimiento = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/estab").InnerText;
                    string puntoemision = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/ptoEmi").InnerText;
                    string secuencia = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/secuencial").InnerText;

                    string ruccli = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/ruc").InnerText;
                    string cli = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/razonSocial").InnerText;


                    string periodofiscal = xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/periodoFiscal").InnerText;


                    Persona per = new Persona();
                    List<Persona> lstper = PersonaBLL.GetAll("per_empresa=" + comprobante.com_empresa + " and per_ciruc='" + ruccli + "'", "");
                    if (lstper.Count > 0)
                    {
                        per = lstper[0];
                    }
                    else
                        throw new System.ArgumentException("El cliente " + cli + " con el número de RUC " + ruccli + ", no se encuentra registrado en el sistema ");

                    comprobante.com_codclipro = per.per_codigo;
                    comprobante.com_fecha = GetDateTime(xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/fechaEmision")).Value;
                    comprobante.com_fechastr = GetString(xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/fechaEmision"));


                    List<Tipopago> lsttip = TipopagoBLL.GetAll("tpa_empresa=" + comprobante.com_empresa + " and tpa_tclipro=" + Constantes.cTipoCliente + " and (tpa_iva=1  or tpa_ret=1)", "tpa_codigo DESC");


                    List<Drecibo> lst = new List<Drecibo>();
                    List<Dcancelacion> lstcan = new List<Dcancelacion>();
                    int debcre = Constantes.cDebito;
                    string nrodoc = ""; //Aqui esta el numero de factura a la que se le hace la retencion
                    string nroret = string.Format("{0}-{1}-{2}", establecimiento, puntoemision, secuencia); // Numero de retencion


                    //nuevos campos para el control de la reparticion de valores retenidos

                    decimal? porcentajesubtotal0 = 0;
                    decimal? porcentajesubtotaliva = 0;
                    decimal? porcentajeiva = 0;

                    decimal? retsubtotal0 = 0;
                    decimal? retsubtotaliva = 0;
                    decimal? retiva = 0;

                    if (version == "1.0.0")
                    {
                        XmlNode detalles = xmldoc.SelectSingleNode("/comprobanteRetencion/impuestos");
                        foreach (XmlNode detalle in detalles.ChildNodes)
                        {



                            Drecibo rec = new Drecibo();
                            rec.dfp_empresa = comprobante.com_empresa;

                            string codigo = GetString(detalle.SelectSingleNode("codigo"));
                            string codigoret = GetString(detalle.SelectSingleNode("codigoRetencion"));
                            string porcentaje = GetString(detalle.SelectSingleNode("porcentajeRetener"));
                            decimal? valorret = GetDecimal(detalle.SelectSingleNode("valorRetenido")).Value;
                            //rec.dfp_tipopago = 7; Ahora no se pone un tipo de pago por defecto, si no encuentra el tipo de pago queda en proceso

                            if (codigo == "1") // 1:RENTA
                            {
                                //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_nombre.Contains(porcentaje); });
                                Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) && t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                if (tpa != null)
                                {
                                    rec.dfp_tipopago = tpa.tpa_codigo;
                                    //   if (codigoret == "310" || codigoret == "312") //retenciones 1% de servicio de trans sobre el subtotal 0
                                    if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTAL0.ToString())
                                    {
                                        porcentajesubtotal0 = tpa.tpa_porcentaje;
                                        retsubtotal0 = valorret;



                                    }
                                    //if (codigoret == "3440") //retenciones 2.75% otra retenciones sobre el subtotal iva
                                    if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTALIVA.ToString())
                                    {
                                        porcentajesubtotaliva = tpa.tpa_porcentaje;
                                        retsubtotaliva = valorret;
                                    }

                                }
                                else
                                    throw new System.ArgumentException("Forma de pago no registrada (RENTA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                            }
                            if (codigo == "2") // 2: IVA
                            {
                                //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_nombre.Contains(porcentaje); });
                                Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) && t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                if (tpa != null)
                                {
                                    porcentajeiva = tpa.tpa_porcentaje;
                                    retiva = valorret;
                                    rec.dfp_tipopago = tpa.tpa_codigo;
                                }
                                else
                                    throw new System.ArgumentException("Forma de pago no registrada (IVA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                            }



                            nrodoc = GetString(detalle.SelectSingleNode("numDocSustento"));
                            rec.dfp_monto = valorret ?? 0;
                            rec.dfp_nro_documento = nroret;
                            rec.dfp_debcre = Constantes.cDebito;
                            rec.crea_usr = "auto";
                            rec.crea_fecha = DateTime.Now;
                            lst.Add(rec);



                        }
                    }
                    else if (version == "2.0.0")
                    {
                        XmlNode detalles = xmldoc.SelectSingleNode("/comprobanteRetencion/docsSustento");
                        foreach (XmlNode detalle in detalles.ChildNodes)
                        {

                            XmlNode retenciones = detalle.SelectSingleNode("retenciones");
                            foreach (XmlNode retencion in retenciones)
                            {
                                Drecibo rec = new Drecibo();
                                rec.dfp_empresa = comprobante.com_empresa;

                                string codigo = GetString(retencion.SelectSingleNode("codigo"));
                                string codigoret = GetString(retencion.SelectSingleNode("codigoRetencion"));
                                string porcentaje = GetString(retencion.SelectSingleNode("porcentajeRetener"));
                                decimal? valorret = GetDecimal(retencion.SelectSingleNode("valorRetenido")).Value;
                                //rec.dfp_tipopago = 7; Ahora no se pone un tipo de pago por defecto, si no encuentra el tipo de pago queda en proceso

                                if (codigo == "1") // 1:RENTA
                                {
                                    //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_nombre.Contains(porcentaje); });
                                    Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) && t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                    if (tpa != null)
                                    {
                                        rec.dfp_tipopago = tpa.tpa_codigo;
                                        //   if (codigoret == "310" || codigoret == "312") //retenciones 1% de servicio de trans sobre el subtotal 0
                                        if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTAL0.ToString())
                                        {
                                            porcentajesubtotal0 = tpa.tpa_porcentaje;
                                            retsubtotal0 = valorret;



                                        }
                                        //if (codigoret == "3440") //retenciones 2.75% otra retenciones sobre el subtotal iva
                                        if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTALIVA.ToString())
                                        {
                                            porcentajesubtotaliva = tpa.tpa_porcentaje;
                                            retsubtotaliva = valorret;
                                        }

                                    }
                                    else
                                        throw new System.ArgumentException("Forma de pago no registrada (RENTA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                                }
                                if (codigo == "2") // 2: IVA
                                {
                                    //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_nombre.Contains(porcentaje); });
                                    Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) && t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                    if (tpa != null)
                                    {
                                        porcentajeiva = tpa.tpa_porcentaje;
                                        retiva = valorret;
                                        rec.dfp_tipopago = tpa.tpa_codigo;
                                    }
                                    else
                                        throw new System.ArgumentException("Forma de pago no registrada (IVA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                                }



                                nrodoc = GetString(detalle.SelectSingleNode("numDocSustento"));
                                rec.dfp_monto = valorret ?? 0;
                                rec.dfp_nro_documento = nroret;
                                rec.dfp_debcre = Constantes.cDebito;
                                rec.crea_usr = "auto";
                                rec.crea_fecha = DateTime.Now;
                                lst.Add(rec);

                            }

                        }
                    }

                    comprobante.com_concepto = "AUT: " + nroautoriza + " RET FAC " + nrodoc;
                    comprobante.recibos = lst;

                    comprobante.total = new Total();
                    comprobante.total.tot_empresa = comprobante.com_empresa;
                    comprobante.total.tot_total = lst.Sum(s => s.dfp_monto);
                    comprobante.com_total = lst.Sum(s => s.dfp_monto);

                    decimal monto = comprobante.total.tot_total;


                    string est = nrodoc.Substring(0, 3);
                    string pem = nrodoc.Substring(3, 3);
                    string sec = nrodoc.Substring(8, nrodoc.Length - 8);
                    int secnum = int.Parse(sec);


                    //string nrofac = est + "%" + pem + "%" + sec;                    
                    //List<Comprobante> lstfac = ComprobanteBLL.GetAll("com_empresa=" + comprobante.com_empresa + " and com_codclipro=" + comprobante.com_codclipro + " and com_doctran ilike '%FAC%" + nrofac + "%'", "");
                    List<Comprobante> lstfac = ComprobanteBLL.GetAll("com_empresa=" + comprobante.com_empresa + " and com_tipodoc = 4  and com_codclipro=" + comprobante.com_codclipro + " and alm_id='" + est + "' and pve_id='" + pem + "' and com_numero= " + secnum.ToString() + " and com_estado=" + Constantes.cEstadoMayorizado, "");

                    if (lstfac.Count > 0)
                    {
                        Comprobante fac = lstfac[0];
                        fac.total = new Total();
                        fac.total = TotalBLL.GetByPK(new Total { tot_empresa = fac.com_empresa, tot_empresa_key = fac.com_empresa, tot_comprobante = fac.com_codigo, tot_comprobante_key = fac.com_codigo });
                        //Valida si las formas de pago corresponden
                        //Validacion RENTA
                        if ((porcentajesubtotal0 ?? 0) > 0 && (fac.total.tot_subtot_0 + fac.total.tot_transporte) == 0)//Retenciones 1%
                        {
                            throw new ArgumentException("El porcentaje " + porcentajesubtotal0 + "% de RENTA no puede retenerse...");
                        }
                        if ((porcentajesubtotaliva ?? 0) > 0 && (fac.total.tot_subtotal + (fac.total.tot_tseguro ?? 0)) == 0)//Retenciones 2.75%
                        {
                            throw new ArgumentException("El porcentaje " + porcentajesubtotaliva + "% de RENTA no puede retenerse...");
                        }
                        //Validacion IVA
                        if ((porcentajeiva ?? 0) > 0 && (fac.total.tot_timpuesto) == 0)//Retenciones 2.75%
                        {
                            throw new ArgumentException("El porcentaje " + porcentajeiva + "% de IVA no puede retenerse...");
                        }

                        /*List<vCancelacionDetalle> lvcan = vCancelacionDetalleBLL.GetAll1(new WhereParams("f.com_codigo={0} and f.com_estado={1} and dfp_nro_documento in ({2},{3}) ", fac.com_codigo, 2, nroret, nrodoc), "");
                        if (lvcan.Count > 0 && nroret != "")
                        {
                            throw new System.ArgumentException("El número de retención " + nroret + " ya ha sido ingresado anteriormente " + lvcan[0].doctran_can, "(Nro Documento)");
                        }
                        */

                        Auto.actualiza_documentos(fac.com_empresa, fac.com_codclipro, fac.com_codigo, 0, monto);
                        if (monto == 0)
                        {
                            List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_codclipro={1} AND ddo_debcre ={2} and ddo_comprobante={3}", fac.com_empresa, fac.com_codclipro, debcre, fac.com_codigo), "");
                            if (lista.Count > 0)
                            {
                                vDdocumento item = lista[0];
                                Dcancelacion dca = new Dcancelacion();
                                dca.dca_empresa = item.ddo_empresa.Value;
                                dca.dca_comprobante = item.ddo_comprobante.Value;
                                dca.dca_transacc = item.ddo_transacc.Value;
                                dca.dca_doctran = item.ddo_doctran;
                                dca.dca_pago = item.ddo_pago.Value;
                                dca.dca_debcre = debcre;
                                dca.dca_monto = monto;
                                dca.dca_monto_ext = dca.dca_monto;
                                lstcan.Add(dca);
                            }
                        }

                        else
                        {


                            List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2} and ddo_comprobante={3}", fac.com_empresa, fac.com_codclipro, debcre, fac.com_codigo), "");

                            bool end = false;

                            do //New control to use all balance until it's empty or doesn't exist more
                            {

                                end = true;

                                foreach (vDdocumento item in lista)
                                {
                                    if (monto > 0)
                                    {

                                        decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                                        if (saldo > 0)
                                        {
                                            bool calcula = false;
                                            decimal valorretsub0 = 0;
                                            if (((item.tot_subtot_0 ?? 0) + (item.tot_transporte ?? 0)) > 0 && (porcentajesubtotal0 ?? 0) > 0)//retencion del 1%
                                            {
                                                calcula = true;
                                                valorretsub0 = Math.Round(((item.tot_subtot_0 ?? 0) + (item.tot_transporte ?? 0)) * (porcentajesubtotal0.Value / 100), 2);
                                            }
                                            decimal valorretsubiva = 0;
                                            if ((item.tot_subtotal ?? 0) > 0 && (porcentajesubtotaliva ?? 0) > 0)//retencion del 2.75%
                                            {
                                                calcula = true;
                                                valorretsubiva = Math.Round(item.tot_subtotal.Value * (porcentajesubtotaliva.Value / 100), 2);
                                            }
                                            decimal valorretiva = 0;
                                            if ((item.tot_impuesto ?? 0) > 0 && (porcentajeiva ?? 0) > 0)//retencion del valor de iva
                                            {
                                                calcula = true;
                                                valorretiva = Math.Round(item.tot_impuesto.Value * (porcentajeiva.Value / 100), 2);
                                            }


                                            decimal valorret = valorretsub0 + valorretsubiva + valorretiva;

                                            if (valorret > monto)//En caso de que el valor de retencion sea mayor al monto
                                                valorret = monto;


                                            if (!calcula && monto > 0)
                                                throw new ArgumentException("El valor de retencion no coincide con el tipo retenido");


                                            Dcancelacion dca = lstcan.Find(f => f.dca_empresa == item.ddo_empresa && f.dca_comprobante == item.ddo_comprobante && f.dca_transacc == item.ddo_transacc && f.dca_doctran == item.ddo_doctran && f.dca_pago == item.ddo_pago);
                                            if (dca != null)
                                            {
                                                if (valorret > saldo)
                                                {
                                                    dca.dca_monto += saldo;
                                                    monto = monto - saldo;
                                                    item.ddo_cancela += saldo;
                                                }
                                                else
                                                {
                                                    dca.dca_monto += valorret;
                                                    monto = monto - valorret;
                                                    item.ddo_cancela += valorret;
                                                }
                                                dca.dca_monto_ext = dca.dca_monto;

                                            }
                                            else
                                            {
                                                dca = new Dcancelacion();
                                                dca.dca_empresa = item.ddo_empresa.Value;
                                                dca.dca_comprobante = item.ddo_comprobante.Value;
                                                dca.dca_transacc = item.ddo_transacc.Value;
                                                dca.dca_doctran = item.ddo_doctran;
                                                dca.dca_pago = item.ddo_pago.Value;
                                                dca.dca_debcre = debcre;
                                                if (valorret > saldo)
                                                {
                                                    dca.dca_monto = saldo;
                                                    monto = monto - saldo;
                                                }
                                                else
                                                {
                                                    dca.dca_monto = valorret;
                                                    monto = monto - valorret;
                                                }


                                                /*if (monto > saldo)
                                                {
                                                    dca.dca_monto = saldo;
                                                    monto = monto - saldo;
                                                }
                                                else
                                                {
                                                    dca.dca_monto = monto;
                                                    monto = 0;
                                                }*/
                                                dca.dca_monto_ext = dca.dca_monto;
                                                lstcan.Add(dca);

                                                //
                                                item.ddo_cancela += dca.dca_monto;
                                            }
                                            end = false;
                                        }

                                    }
                                }

                            } while (!end);
                        }





                    }
                    else
                        throw new System.ArgumentException("La factura " + nrodoc + " no se encuentra registrada en el sistema");

                    comprobante.cancelaciones = lstcan;



                }

            }
            catch (Exception ex)
            {
                comprobante.com_codigo = -1;
                comprobante.com_doctran = ex.Message;
            }


            return comprobante;
        }

        public static Comprobante CargarReciboRetencionNew(string xml, Comprobante comprobante, string nroautoriza, DateTime? fechaautoriza)
        {

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var serializer = new JavaScriptSerializer();

            comprobante.total = new Total();

            try
            {
              
                if (!string.IsNullOrEmpty(xml))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xml);

                    string version = xmldoc.SelectSingleNode("/comprobanteRetencion").Attributes["version"].Value;


                    comprobante.com_claveelec = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/claveAcceso").InnerText;
                    
                    string establecimiento = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/estab").InnerText;
                    string puntoemision = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/ptoEmi").InnerText;
                    string secuencia = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/secuencial").InnerText;

                    string ruccli = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/ruc").InnerText;
                    string cli = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/razonSocial").InnerText;


                    string periodofiscal = xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/periodoFiscal").InnerText;


                    Persona per = new Persona();
                    List<Persona> lstper = PersonaBLL.GetAll("per_empresa=" + comprobante.com_empresa + " and per_ciruc='" + ruccli + "'", "");
                    if (lstper.Count > 0)
                    {
                        per = lstper[0];
                    }
                    else
                        throw new System.ArgumentException("El cliente " + cli + " con el número de RUC " + ruccli + ", no se encuentra registrado en el sistema ");

                    comprobante.com_codclipro = per.per_codigo;
                    comprobante.com_fecha = GetDateTimeFormat(xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/fechaEmision"),"dd/MM/yyyy").Value;
                    comprobante.com_fechastr = GetString(xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/fechaEmision"));

                    ElectronicoExists(comprobante.com_empresa, nroautoriza, comprobante.com_fecha);

                    List<Tipopago> lsttip = TipopagoBLL.GetAll("tpa_empresa=" + comprobante.com_empresa + " and tpa_tclipro=" + Constantes.cTipoCliente + " and (tpa_iva=1  or tpa_ret=1)", "tpa_codigo DESC");


                    List<Drecibo> lst = new List<Drecibo>();
                    List<Dcancelacion> lstcan = new List<Dcancelacion>();
                    int debcre = Constantes.cDebito;
                    string nrodoc = ""; //Aqui esta el numero de factura a la que se le hace la retencion
                    string nroret = string.Format("{0}-{1}-{2}", establecimiento, puntoemision, secuencia); // Numero de retencion


                    //nuevos campos para el control de la reparticion de valores retenidos

                    decimal? porcentajesubtotal0 = 0;
                    decimal? porcentajesubtotaliva = 0;
                    decimal? porcentajeiva = 0;

                    decimal? retsubtotal0 = 0;
                    decimal? retsubtotaliva = 0;
                    decimal? retiva = 0;

                    if (version == "1.0.0")
                    {
                        XmlNode detalles = xmldoc.SelectSingleNode("/comprobanteRetencion/impuestos");
                        foreach (XmlNode detalle in detalles.ChildNodes)
                        {



                            Drecibo rec = new Drecibo();
                            rec.dfp_empresa = comprobante.com_empresa;

                            string codigo = GetString(detalle.SelectSingleNode("codigo"));
                            string codigoret = GetString(detalle.SelectSingleNode("codigoRetencion"));
                            string porcentaje = GetString(detalle.SelectSingleNode("porcentajeRetener"));
                            decimal? valorret = GetDecimal(detalle.SelectSingleNode("valorRetenido")).Value;
                            //rec.dfp_tipopago = 7; Ahora no se pone un tipo de pago por defecto, si no encuentra el tipo de pago queda en proceso

                            if (codigo == "1") // 1:RENTA
                            {
                                //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_nombre.Contains(porcentaje); });
                                Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) &&   t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                if (tpa != null)
                                {
                                    rec.dfp_tipopago = tpa.tpa_codigo;
                                    //   if (codigoret == "310" || codigoret == "312") //retenciones 1% de servicio de trans sobre el subtotal 0
                                    if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTAL0.ToString())
                                    {
                                        porcentajesubtotal0 = tpa.tpa_porcentaje;
                                        retsubtotal0 = valorret;



                                    }
                                    //if (codigoret == "3440") //retenciones 2.75% otra retenciones sobre el subtotal iva
                                    if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTALIVA.ToString())
                                    {
                                        porcentajesubtotaliva = tpa.tpa_porcentaje;
                                        retsubtotaliva = valorret;
                                    }

                                }
                                else
                                    throw new System.ArgumentException("Forma de pago no registrada (RENTA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                            }
                            if (codigo == "2") // 2: IVA
                            {
                                //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_nombre.Contains(porcentaje); });
                                Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) && t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                if (tpa != null)
                                {
                                    porcentajeiva = tpa.tpa_porcentaje;
                                    retiva = valorret;
                                    rec.dfp_tipopago = tpa.tpa_codigo;
                                }
                                else
                                    throw new System.ArgumentException("Forma de pago no registrada (IVA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                            }



                            nrodoc = GetString(detalle.SelectSingleNode("numDocSustento"));
                            rec.dfp_monto = valorret ?? 0;
                            rec.dfp_nro_documento = nroret;
                            rec.dfp_debcre = Constantes.cDebito;
                            rec.crea_usr = "auto";
                            rec.crea_fecha = DateTime.Now;
                            lst.Add(rec);



                        }
                    }
                    else if (version == "2.0.0")
                    {
                        XmlNode detalles = xmldoc.SelectSingleNode("/comprobanteRetencion/docsSustento");
                        foreach (XmlNode detalle in detalles.ChildNodes)
                        {

                            XmlNode retenciones = detalle.SelectSingleNode("retenciones");
                            foreach (XmlNode retencion in retenciones)
                            {
                                Drecibo rec = new Drecibo();
                                rec.dfp_empresa = comprobante.com_empresa;

                                string codigo = GetString(retencion.SelectSingleNode("codigo"));
                                string codigoret = GetString(retencion.SelectSingleNode("codigoRetencion"));
                                string porcentaje = GetString(retencion.SelectSingleNode("porcentajeRetener"));
                                decimal? valorret = GetDecimal(retencion.SelectSingleNode("valorRetenido")).Value;
                                //rec.dfp_tipopago = 7; Ahora no se pone un tipo de pago por defecto, si no encuentra el tipo de pago queda en proceso

                                if (codigo == "1") // 1:RENTA
                                {
                                    //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_nombre.Contains(porcentaje); });
                                    Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_ret == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) && t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                    if (tpa != null)
                                    {
                                        rec.dfp_tipopago = tpa.tpa_codigo;
                                        //   if (codigoret == "310" || codigoret == "312") //retenciones 1% de servicio de trans sobre el subtotal 0
                                        if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTAL0.ToString())
                                        {
                                            porcentajesubtotal0 = tpa.tpa_porcentaje;
                                            retsubtotal0 = valorret;



                                        }
                                        //if (codigoret == "3440") //retenciones 2.75% otra retenciones sobre el subtotal iva
                                        if (tpa.tpa_aplicaret == Enums.AplicaRetencion.SUBTOTALIVA.ToString())
                                        {
                                            porcentajesubtotaliva = tpa.tpa_porcentaje;
                                            retsubtotaliva = valorret;
                                        }

                                    }
                                    else
                                        throw new System.ArgumentException("Forma de pago no registrada (RENTA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                                }
                                if (codigo == "2") // 2: IVA
                                {
                                    //Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_nombre.Contains(porcentaje); });
                                    Tipopago tpa = lsttip.Find(delegate (Tipopago t) { return t.tpa_iva == 1 && t.tpa_porcentaje == Functions.Conversiones.ObjectToDecimalNull(porcentaje) && t.tpa_codigoxml.Split(',').Contains(codigoret); });
                                    if (tpa != null)
                                    {
                                        porcentajeiva = tpa.tpa_porcentaje;
                                        retiva = valorret;
                                        rec.dfp_tipopago = tpa.tpa_codigo;
                                    }
                                    else
                                        throw new System.ArgumentException("Forma de pago no registrada (IVA Codigo:" + codigoret + " Porcentaje:" + porcentaje + ")");
                                }



                                nrodoc = GetString(detalle.SelectSingleNode("numDocSustento"));
                                rec.dfp_monto = valorret ?? 0;
                                rec.dfp_nro_documento = nroret;
                                rec.dfp_debcre = Constantes.cDebito;
                                rec.crea_usr = "auto";
                                rec.crea_fecha = DateTime.Now;
                                lst.Add(rec);

                            }

                        }
                    }

                    comprobante.com_concepto = "AUT: " + nroautoriza + " RET FAC " + nrodoc;
                    comprobante.recibos = lst;

                    comprobante.total = new Total();
                    comprobante.total.tot_empresa = comprobante.com_empresa;
                    comprobante.total.tot_total = lst.Sum(s => s.dfp_monto);
                    comprobante.com_total = lst.Sum(s => s.dfp_monto);

                    decimal monto = comprobante.total.tot_total;


                    string est = nrodoc.Substring(0, 3);
                    string pem = nrodoc.Substring(3, 3);
                    string sec = nrodoc.Substring(8, nrodoc.Length - 8);
                    int secnum = int.Parse(sec);


                    //string nrofac = est + "%" + pem + "%" + sec;                    
                    //List<Comprobante> lstfac = ComprobanteBLL.GetAll("com_empresa=" + comprobante.com_empresa + " and com_codclipro=" + comprobante.com_codclipro + " and com_doctran ilike '%FAC%" + nrofac + "%'", "");
                    List<Comprobante> lstfac = ComprobanteBLL.GetAll("com_empresa=" + comprobante.com_empresa + " and com_tipodoc = 4  and com_codclipro=" + comprobante.com_codclipro + " and alm_id='" + est + "' and pve_id='" + pem + "' and com_numero= " + secnum.ToString() + " and com_estado=" + Constantes.cEstadoMayorizado, "");

                    if (lstfac.Count > 0)
                    {
                        Comprobante fac = lstfac[0];
                        fac.total = new Total();
                        fac.total = TotalBLL.GetByPK(new Total { tot_empresa = fac.com_empresa, tot_empresa_key = fac.com_empresa, tot_comprobante = fac.com_codigo, tot_comprobante_key = fac.com_codigo });
                        //Valida si las formas de pago corresponden
                        //Validacion RENTA
                        if ((porcentajesubtotal0 ?? 0) > 0 && (fac.total.tot_subtot_0 + fac.total.tot_transporte) == 0)//Retenciones 1%
                        {
                            throw new ArgumentException("El porcentaje " + porcentajesubtotal0 + "% de RENTA no puede retenerse...");
                        }
                        if ((porcentajesubtotaliva ?? 0) > 0 && (fac.total.tot_subtotal + (fac.total.tot_tseguro ?? 0)) == 0)//Retenciones 2.75%
                        {
                            throw new ArgumentException("El porcentaje " + porcentajesubtotaliva + "% de RENTA no puede retenerse...");
                        }
                        //Validacion IVA
                        if ((porcentajeiva ?? 0) > 0 && (fac.total.tot_timpuesto) == 0)//Retenciones 2.75%
                        {
                            throw new ArgumentException("El porcentaje " + porcentajeiva + "% de IVA no puede retenerse...");
                        }

                        List<vCancelacionDetalle> lvcan = vCancelacionDetalleBLL.GetAll1(new WhereParams("f.com_codigo={0} and f.com_estado={1} and dfp_nro_documento in ({2},{3}) ", fac.com_codigo, 2, nroret, nrodoc), "");
                        if (lvcan.Count > 0 && nroret != "")
                        {
                            throw new System.ArgumentException("El número de retención " + nroret + " ya ha sido ingresado anteriormente " + lvcan[0].doctran_can, "(Nro Documento)");
                        }
                        

                        Auto.actualiza_documentos(fac.com_empresa, fac.com_codclipro, fac.com_codigo, 0,monto);
                        if (monto == 0)
                        {
                            List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_codclipro={1} AND ddo_debcre ={2} and ddo_comprobante={3}", fac.com_empresa, fac.com_codclipro, debcre, fac.com_codigo), "");
                            if (lista.Count > 0)
                            {
                                vDdocumento item = lista[0];
                                Dcancelacion dca = new Dcancelacion();
                                dca.dca_empresa = item.ddo_empresa.Value;
                                dca.dca_comprobante = item.ddo_comprobante.Value;
                                dca.dca_transacc = item.ddo_transacc.Value;
                                dca.dca_doctran = item.ddo_doctran;
                                dca.dca_pago = item.ddo_pago.Value;
                                dca.dca_debcre = debcre;
                                dca.dca_monto = monto;
                                dca.dca_monto_ext = dca.dca_monto;
                                lstcan.Add(dca);
                            }
                        }

                        else
                        {


                            List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_codclipro={1} AND ddo_debcre ={2} and ddo_comprobante={3}", fac.com_empresa, fac.com_codclipro, debcre, fac.com_codigo), "");

                            bool end = false;

                            do //New control to use all balance until it's empty or doesn't exist more
                            {

                                end = true;

                                foreach (vDdocumento item in lista)
                                {
                                    if (monto > 0)
                                    {

                                        decimal saldo = item.ddo_monto.Value - item.ddo_cancela.Value;
                                        if (saldo > 0)
                                        {
                                            bool calcula = false;
                                            decimal valorretsub0 = 0;
                                            if (((item.tot_subtot_0 ?? 0) + (item.tot_transporte ?? 0)) > 0 && (porcentajesubtotal0 ?? 0) > 0)//retencion del 1%
                                            {
                                                calcula = true;
                                                valorretsub0 = Math.Round(((item.tot_subtot_0 ?? 0) + (item.tot_transporte ?? 0)) * (porcentajesubtotal0.Value / 100), 2);
                                            }
                                            decimal valorretsubiva = 0;
                                            if ((item.tot_subtotal ?? 0) > 0 && (porcentajesubtotaliva ?? 0) > 0)//retencion del 2.75%
                                            {
                                                calcula = true;
                                                valorretsubiva = Math.Round(item.tot_subtotal.Value * (porcentajesubtotaliva.Value / 100), 2);
                                            }
                                            decimal valorretiva = 0;
                                            if ((item.tot_impuesto ?? 0) > 0 && (porcentajeiva ?? 0) > 0)//retencion del valor de iva
                                            {
                                                calcula = true;
                                                valorretiva = Math.Round(item.tot_impuesto.Value * (porcentajeiva.Value / 100), 2);
                                            }


                                            decimal valorret = valorretsub0 + valorretsubiva + valorretiva;

                                            if (valorret > monto)//En caso de que el valor de retencion sea mayor al monto
                                                valorret = monto;


                                            if (!calcula && monto > 0)
                                                throw new ArgumentException("El valor de retencion no coincide con el tipo retenido");




                                            /*Dcancelacion dca = lstcan.Find(f => f.dca_empresa == item.ddo_empresa && f.dca_comprobante == item.ddo_comprobante && f.dca_transacc == item.ddo_transacc && f.dca_doctran == item.ddo_doctran && f.dca_pago == item.ddo_pago);
                                            if (dca != null)
                                            {
                                                if (valorret > saldo)
                                                {
                                                    dca.dca_monto += saldo;
                                                    monto = monto - saldo;
                                                    item.ddo_cancela += saldo;
                                                }
                                                else
                                                {
                                                    dca.dca_monto += valorret;
                                                    monto = monto - valorret;
                                                    item.ddo_cancela += valorret;
                                                }
                                                dca.dca_monto_ext = dca.dca_monto;

                                            }
                                            else
                                            {*/
                                                Dcancelacion dca = new Dcancelacion();
                                                dca.dca_empresa = item.ddo_empresa.Value;
                                                dca.dca_comprobante = item.ddo_comprobante.Value;
                                                dca.dca_transacc = item.ddo_transacc.Value;
                                                dca.dca_doctran = item.ddo_doctran;
                                                dca.dca_pago = item.ddo_pago.Value;
                                                dca.dca_debcre = debcre;
                                                if (valorret > saldo)
                                                {
                                                    dca.dca_monto = saldo;
                                                    monto = monto - saldo;
                                                }
                                                else
                                                {
                                                    dca.dca_monto = valorret;
                                                    monto = monto - valorret;
                                                }


                                                /*if (monto > saldo)
                                                {
                                                    dca.dca_monto = saldo;
                                                    monto = monto - saldo;
                                                }
                                                else
                                                {
                                                    dca.dca_monto = monto;
                                                    monto = 0;
                                                }*/
                                                dca.dca_monto_ext = dca.dca_monto;
                                                lstcan.Add(dca);

                                                //
                                                item.ddo_cancela += dca.dca_monto;
                                            //}
                                            end = false;
                                        }

                                    }
                                }

                            } while (!end);
                        }





                    }
                    else
                        throw new System.ArgumentException("La factura " + nrodoc + " no se encuentra registrada en el sistema");

                    comprobante.cancelaciones = lstcan;



                }

            }
            catch (Exception ex)
            {
                comprobante.com_codigo = -1;
                comprobante.com_doctran = ex.Message;
            }


            return comprobante;
        }

        public static Comprobante CargarFacturaTAO(string xml, Comprobante comprobante)
        {

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var serializer = new JavaScriptSerializer();

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.detalle = new List<Dcomdoc>();
            comprobante.total = new Total();


            try
            {

                if (!string.IsNullOrEmpty(xml))
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xml);

                    comprobante.com_claveelec = xmldoc.SelectSingleNode("/factura/infoTributaria/claveAcceso").InnerText;
                    comprobante.com_ambiente = xmldoc.SelectSingleNode("/factura/infoTributaria/ambiente").InnerText;
                    comprobante.com_emision = xmldoc.SelectSingleNode("/factura/infoTributaria/tipoEmision").InnerText;

                    comprobante.ccomdoc.cdoc_tipoid = xmldoc.SelectSingleNode("/factura/infoFactura/tipoIdentificacionComprador").InnerText;
                    comprobante.ccomdoc.cdoc_ced_ruc = xmldoc.SelectSingleNode("/factura/infoFactura/identificacionComprador").InnerText;
                    comprobante.ccomdoc.cdoc_nombre = xmldoc.SelectSingleNode("/factura/infoFactura/razonSocialComprador").InnerText;
                    comprobante.ccomdoc.cdoc_direccion = xmldoc.SelectSingleNode("/factura/infoFactura/direccionComprador").InnerText;

                    comprobante.total.tot_subtotal = GetDecimal(xmldoc.SelectSingleNode("/factura/infoFactura/totalSinImpuestos").InnerText);
                    comprobante.total.tot_total = GetDecimal(xmldoc.SelectSingleNode("/factura/infoFactura/importeTotal").InnerText);



                    XmlNode totalimpuestos = xmldoc.SelectSingleNode("/factura/infoFactura/totalConImpuestos");



                    decimal porciva = Constantes.GetValorIVA(comprobante.com_fecha);
                    decimal subtotaliva = 0;
                    decimal valoriva = 0;
                    decimal subtotalice = 0;
                    decimal valorice = 0;
                    decimal subtotal0 = 0;
                    decimal subtotalnoiva = 0;
                    decimal subtotalextiva = 0;

                    foreach (XmlNode totalimp in totalimpuestos.ChildNodes)
                    {
                        XmlNode codigo = totalimp.SelectSingleNode("codigo");
                        XmlNode codigoporcentaje = totalimp.SelectSingleNode("codigoPorcentaje");
                        XmlNode descuento = totalimp.SelectSingleNode("descuentoAdicional");
                        XmlNode baseimp = totalimp.SelectSingleNode("baseImponible");
                        XmlNode tarifa = totalimp.SelectSingleNode("tarifa");
                        XmlNode valor = totalimp.SelectSingleNode("valor");


                        if (codigo.InnerText == "2" && (codigoporcentaje.InnerText == "2" || codigoporcentaje.InnerText == "3"))//TARIFA 12 o 14
                        {
                            subtotaliva += GetDecimal(baseimp.InnerText);
                            valoriva += GetDecimal(valor.InnerText);
                            if (codigoporcentaje.InnerText == "2")
                                porciva = 12;
                            if (codigoporcentaje.InnerText == "3")
                                porciva = 14;
                        }
                        if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "0")//TARIFA 0
                        {
                            subtotal0 += GetDecimal(baseimp.InnerText);
                        }
                        if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "6")//NO OBJETO DE IVA
                        {
                            subtotalnoiva += GetDecimal(baseimp.InnerText);
                        }
                        if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "7")//EXENTO DE IVA
                        {
                            subtotalextiva += GetDecimal(baseimp.InnerText);
                        }
                        if (codigo.InnerText == "3")//TARIFA ICE
                        {
                            subtotalice += GetDecimal(baseimp.InnerText);
                            valorice += GetDecimal(valor.InnerText);
                        }
                    }
                    comprobante.total.tot_subtot_0 = subtotal0;
                    comprobante.total.tot_subtotal = subtotaliva;
                    //comprobante.com_subtotalnoiva = subtotalnoiva;
                    //comprobante.com_subtotalextiva = subtotalextiva;
                    comprobante.total.tot_dias_plazo = 30;

                    comprobante.total.tot_timpuesto = valoriva;
                    comprobante.total.tot_impuesto = 2;//IVA EN VENTAS

                    comprobante.total.tot_porc_impuesto = porciva;
                    comprobante.total.tot_ice = valorice;


                    ///*PAGOS*/
                    //XmlNode pagos = xmldoc.SelectSingleNode("/factura/infoFactura/pagos");


                    //List<FormaPago> formas = serializer.Deserialize<List<FormaPago>>(Constantes.GetParameterValue("formaspago"));

                    //comprobante.formas = new List<Formapago>();

                    //if (pagos != null)
                    //{
                    //    foreach (XmlNode pago in pagos.ChildNodes)
                    //    {
                    //        Formapago fp = new Formapago();
                    //        fp.secuencia = comprobante.formas.Count() + 1;
                    //        fp.codigo = pago.SelectSingleNode("formaPago").InnerText;
                    //        FormaPago forma = formas.Find(delegate (FormaPago f) { return f.codigo == fp.codigo; });
                    //        fp.forma = forma.forma;
                    //        fp.valor = GetDecimal(pago.SelectSingleNode("total"));
                    //        fp.plazo = GetEntero(pago.SelectSingleNode("plazo"));
                    //        fp.tiempo = GetString(pago.SelectSingleNode("unidadTiempo"));
                    //        comprobante.formas.Add(fp);

                    //    }
                    //}

                    /*DETALLES*/
                    XmlNode detalles = xmldoc.SelectSingleNode("/factura/detalles");


                    comprobante.ccomdoc.detalle = new List<Dcomdoc>();

                    foreach (XmlNode detalle in detalles.ChildNodes)
                    {
                        Dcomdoc det = new Dcomdoc();
                        det.ddoc_secuencia = comprobante.ccomdoc.detalle.Count() + 1;
                        det.ddoc_productoid = GetString(detalle.SelectSingleNode("codigoAuxiliar"));
                        det.ddoc_observaciones = GetString(detalle.SelectSingleNode("descripcion"));

                        det.ddoc_cantidad = GetDecimal(detalle.SelectSingleNode("cantidad").InnerText);
                        det.ddoc_precio = GetDecimal(detalle.SelectSingleNode("precioUnitario").InnerText);
                        det.ddoc_dscitem = GetDecimal(detalle.SelectSingleNode("descuento").InnerText);
                        det.ddoc_total = GetDecimal(detalle.SelectSingleNode("precioTotalSinImpuesto").InnerText);
                        det.ddco_udigitada = 1;

                        XmlNode impuestosdet = detalle.SelectSingleNode("impuestos");
                        foreach (XmlNode impuesto in impuestosdet.ChildNodes)
                        {
                            if (impuesto.SelectSingleNode("codigo").InnerText == "2")//IVA
                            {
                                det.ddoc_grabaiva = 1;

                            }
                            //if (impuesto.SelectSingleNode("codigo").InnerText == "3")//ICE
                            //{
                            //    det.codice = GetString(impuesto.SelectSingleNode("codigoPorcentaje"));
                            //    det.porcice = GetDecimal(impuesto.SelectSingleNode("tarifa").InnerText);
                            //    det.valice = GetDecimal(impuesto.SelectSingleNode("valor").InnerText);
                            //}

                        }

                        //XmlNode adicionalesdet = detalle.SelectSingleNode("detallesAdicionales");
                        //if (adicionalesdet != null)
                        //{
                        //    foreach (XmlNode detadi in adicionalesdet.ChildNodes)
                        //    {
                        //        if (detadi.Attributes["nombre"].Value == "detadicional1")
                        //            det.adicional1 = detadi.Attributes["valor"].Value;
                        //        if (detadi.Attributes["nombre"].Value == "detadicional2")
                        //            det.adicional2 = detadi.Attributes["valor"].Value;
                        //        if (detadi.Attributes["nombre"].Value == "detadicional3")
                        //            det.adicional3 = detadi.Attributes["valor"].Value;
                        //    }
                        //}


                        comprobante.ccomdoc.detalle.Add(det);




                    }



                }
            }
            catch (Exception ex)
            {
                comprobante.com_codigo = -1;
                comprobante.com_doctran = ex.Message;
            }


            return comprobante;
        }


        public static string GetFacturaRet(string autfac)
        {
            //001700000009251
            string est = autfac.Substring(0, 3);
            string pem = autfac.Substring(3, 3);
            string num = autfac.Substring(6, autfac.Length - 6);

            //"001-011-000000004"
            return est + "-" + pem + "-" + num;

        }

        public static string GetFacturaNC(string nrofac)
        {
            string[] array = nrofac.Split('-');
            return string.Format("FAC-{0:000}-{1:000}-{2:0000000}", int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));

        }

        public static Comprobante CargarRetencionSICE(string xml, Comprobante comprobante, List<Ccomdoc> lstcdoc)
        {
            List<Impuesto> lstimpuestos = ImpuestoBLL.GetAll("imp_empresa=" + comprobante.com_empresa, "");
            List<Concepto> lstconceptos = ConceptoBLL.GetAll("con_empresa=" + comprobante.com_empresa, "");

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var serializer = new JavaScriptSerializer();

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.retenciones = new List<Dretencion>();
            comprobante.total = new Total();

            if (!string.IsNullOrEmpty(xml))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);



                

                //comprobante.ccomdoc.cdoc_tipoid = xmldoc.SelectSingleNode("/factura/infoFactura/tipoIdentificacionComprador").InnerText;
                comprobante.ccomdoc.cdoc_ced_ruc = xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/identificacionSujetoRetenido").InnerText;
                comprobante.ccomdoc.cdoc_nombre = xmldoc.SelectSingleNode("/comprobanteRetencion/infoCompRetencion/razonSocialSujetoRetenido").InnerText;


                comprobante.com_claveelec = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/claveAcceso").InnerText;
                comprobante.com_ambiente = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/ambiente").InnerText;
                comprobante.com_emision = xmldoc.SelectSingleNode("/comprobanteRetencion/infoTributaria/tipoEmision").InnerText;



                //comprobante.com_periodo = comprobante.com_fecha.Value.ToString("MM/yyyy");

                //obj["cdoc_factura"] = $("#txtFACTURAOBL").val();
                //obj["cdoc_aut_factura"] = $("#txtNUMCOMPROBANTE").val()          

                string autfactura = "";
                DateTime? autfecha = null;

                /*DETALLES*/
                XmlNode detalles = xmldoc.SelectSingleNode("/comprobanteRetencion/impuestos");
                comprobante.retenciones = new List<Dretencion>();


                foreach (XmlNode detalle in detalles.ChildNodes)
                {

                    //1: RENTA
                    //2: IVA

                    Dretencion det = new Dretencion();

                    det.drt_valor = GetDecimal(detalle.SelectSingleNode("baseImponible").InnerText);
                    det.drt_porcentaje = GetDecimal(detalle.SelectSingleNode("porcentajeRetener").InnerText);
                    det.drt_total= GetDecimal(detalle.SelectSingleNode("valorRetenido").InnerText);

                    string cod = GetString(detalle.SelectSingleNode("codigo"));
                    string codret = GetString(detalle.SelectSingleNode("codigoRetencion"));

                    //det.drt_con_codigo = //codigo del concepto;
                    det.drt_factura = GetFacturaRet(GetString(detalle.SelectSingleNode("numDocSustento")));
                    autfactura = det.drt_factura;
                    autfecha = Functions.Conversiones.ObjectToDateTimeNull(detalle.SelectSingleNode("fechaEmisionDocSustento").InnerText);

                    if (cod =="1")//RENTA
                    {
                        Concepto con = lstconceptos.Find(delegate (Concepto c) { return c.con_id == codret && c.con_tipo == cod; });
                        if (con!=null)
                        {
                            Impuesto imp = lstimpuestos.Find(delegate (Impuesto i) { return i.imp_concepto == con.con_codigo && i.imp_cuenta != null && i.imp_ret==1; });
                            if (imp==null)
                                imp = lstimpuestos.Find(delegate (Impuesto i) { return i.imp_porcentaje == det.drt_porcentaje && i.imp_cuenta != null; });
                            det.drt_impuesto = imp.imp_codigo;
                            det.drt_con_codigo = con.con_codigo;
                            det.drt_cuenta = imp.imp_cuenta;
                        }
                        else
                        {
                            Impuesto imp = lstimpuestos.Find(delegate (Impuesto i) { return i.imp_porcentaje == det.drt_porcentaje && i.imp_cuenta != null; });
                            con = lstconceptos.Find(delegate (Concepto c) { return c.con_codigo == imp.imp_concepto; });
                            det.drt_impuesto = imp.imp_codigo;
                            det.drt_con_codigo = con.con_codigo;
                            det.drt_cuenta = imp.imp_cuenta;
                        }



                    }

                    if (cod =="2")//IVA
                    {

                        Impuesto imp = lstimpuestos.Find(delegate (Impuesto i) { return i.imp_porcentaje == det.drt_porcentaje && i.imp_cuenta != null && i.imp_iva == 1; });                        
                        Concepto con = lstconceptos.Find(delegate (Concepto c) { return c.con_codigo == imp.imp_concepto; });

                        det.drt_impuesto = imp.imp_codigo;
                        det.drt_con_codigo = con.con_codigo;
                        det.drt_cuenta = imp.imp_cuenta;

                    }
                    

                        


                  


                    //obj["drt_impuesto"] = parseInt($("#txtCODIMP").val());
                    //obj["drt_cuenta"] = parseInt($("#txtIMPCUENTA").val());
                    //obj["drt_valor"] = $("#txtBASE").val();
                    //obj["drt_porcentaje"] = $("#txtPORCTJRETENCION").val();
                    //obj["drt_total"] = $("#txtTOTAL").val();
                    //obj["drt_con_codigo"] = $("#cmbCONCEPTO").val();
                    //obj["drt_factura"] = $("#txtNUMCOMPROBANTE").val();
                    comprobante.retenciones.Add(det);

                 

                }

                comprobante.ccomdoc.cdoc_aut_fecha = autfecha;
                comprobante.ccomdoc.cdoc_aut_factura = autfactura;

                Ccomdoc cdoc = lstcdoc.Find(delegate (Ccomdoc c) { return c.cdoc_aut_factura == autfactura && c.cdoc_ced_ruc == comprobante.ccomdoc.cdoc_ced_ruc; });
                if (cdoc != null)
                    comprobante.ccomdoc.cdoc_factura = cdoc.cdoc_comprobante;


                comprobante.total = new Total();
                comprobante.total.tot_total = comprobante.retenciones.Sum(s => s.drt_total).Value;







            }
            return comprobante;

        }


        public static Comprobante CargarNotaCreditoSICE(string xml, Comprobante comprobante, List<Comprobante> lstcom)
        {
            
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            var serializer = new JavaScriptSerializer();

            comprobante.ccomdoc = new Ccomdoc();
            comprobante.retenciones = new List<Dretencion>();
            comprobante.total = new Total();

            if (!string.IsNullOrEmpty(xml))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);


                                
                //comprobante.ccomdoc.cdoc_tipoid = xmldoc.SelectSingleNode("/factura/infoFactura/tipoIdentificacionComprador").InnerText;
                comprobante.ccomdoc.cdoc_ced_ruc = xmldoc.SelectSingleNode("/notaCredito/infoNotaCredito/identificacionComprador").InnerText;
                comprobante.ccomdoc.cdoc_nombre = xmldoc.SelectSingleNode("/notaCredito/infoNotaCredito/razonSocialComprador").InnerText;
                

                comprobante.com_claveelec = xmldoc.SelectSingleNode("/notaCredito/infoTributaria/claveAcceso").InnerText;
                comprobante.com_ambiente = xmldoc.SelectSingleNode("/notaCredito/infoTributaria/ambiente").InnerText;
                comprobante.com_emision = xmldoc.SelectSingleNode("/notaCredito/infoTributaria/tipoEmision").InnerText;



                //comprobante.com_periodo = comprobante.com_fecha.Value.ToString("MM/yyyy");

                //obj["cdoc_factura"] = $("#txtFACTURAOBL").val();
                //obj["cdoc_aut_factura"] = $("#txtNUMCOMPROBANTE").val()          

                comprobante.total = new Total();
                comprobante.total.tot_subtotal = GetDecimal(xmldoc.SelectSingleNode("/notaCredito/infoNotaCredito/totalSinImpuestos").InnerText);
                comprobante.total.tot_total = GetDecimal(xmldoc.SelectSingleNode("/notaCredito/infoNotaCredito/valorModificacion").InnerText);

                string autfactura = GetString(xmldoc.SelectSingleNode("/notaCredito/infoNotaCredito/numDocModificado"));
                string autfecha = GetString(xmldoc.SelectSingleNode("/notaCredito/infoNotaCredito/fechaEmisionDocSustento"));

                string doctranfactura = GetFacturaNC(autfactura);

                Comprobante fac = lstcom.Find(delegate (Comprobante c) { return c.com_doctran == doctranfactura; });
                if (fac != null)
                {
                    Auto.actualiza_documentos(fac.com_empresa, null, null, null, fac.com_codigo, null, null, 0);
                    List<vDdocumento> lista = vDdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} AND ddo_cancelado=0 AND ddo_comprobante={1}", fac.com_empresa, fac.com_codigo), "");                                        
                    comprobante.ccomdoc.cdoc_factura = fac.com_codigo;
                    comprobante.cancelaciones = new List<Dcancelacion>();
                    decimal saldo = comprobante.total.tot_total;
                    foreach (vDdocumento item in lista)
                    {
                        if (saldo > 0)
                        {

                            Dcancelacion dca = new Dcancelacion();
                            dca.dca_empresa = fac.com_empresa;
                            dca.dca_comprobante = fac.com_codigo;
                            dca.dca_transacc = fac.com_transacc;
                            dca.dca_doctran = fac.com_doctran;
                            dca.dca_pago = item.ddo_pago.Value;

                            decimal saldoddo = item.ddo_monto.Value - item.ddo_cancela.Value;
                            if (saldo > saldoddo)
                            {
                                dca.dca_monto = saldoddo;
                            }
                            else
                            {
                                dca.dca_monto = saldo;
                            }
                            comprobante.cancelaciones.Add(dca);
                            saldo = saldo - saldoddo;
                        }
                    }
                    
                }
                else
                    comprobante.ccomdoc.cdoc_aut_factura = doctranfactura;



                //comprobante.ccomdoc.cdoc_aut_factura = autfactura;
                //comprobante.ccomdoc.cdoc_aut_fecha= Functions.Conversiones.ObjectToDateTimeNull(autfecha);

                


                XmlNode totalimpuestos = xmldoc.SelectSingleNode("/notaCredito/infoNotaCredito/totalConImpuestos");



                decimal porciva = Constantes.GetValorIVA(comprobante.com_fecha);
                decimal subtotaliva = 0;
                decimal valoriva = 0;
                decimal subtotalice = 0;
                decimal valorice = 0;
                decimal subtotal0 = 0;
                decimal subtotalnoiva = 0;
                decimal subtotalextiva = 0;

                foreach (XmlNode totalimp in totalimpuestos.ChildNodes)
                {
                    XmlNode codigo = totalimp.SelectSingleNode("codigo");
                    XmlNode codigoporcentaje = totalimp.SelectSingleNode("codigoPorcentaje");
                    XmlNode descuento = totalimp.SelectSingleNode("descuentoAdicional");
                    XmlNode baseimp = totalimp.SelectSingleNode("baseImponible");
                    XmlNode tarifa = totalimp.SelectSingleNode("tarifa");
                    XmlNode valor = totalimp.SelectSingleNode("valor");


                    if (codigo.InnerText == "2" && (codigoporcentaje.InnerText == "2" || codigoporcentaje.InnerText == "3"))//TARIFA 12 o 14
                    {
                        subtotaliva += GetDecimal(baseimp.InnerText);
                        valoriva += GetDecimal(valor.InnerText);
                        if (codigoporcentaje.InnerText == "2")
                            porciva = 12;
                        if (codigoporcentaje.InnerText == "3")
                            porciva = 14;
                    }
                    if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "0")//TARIFA 0
                    {
                        subtotal0 += GetDecimal(baseimp.InnerText);
                    }
                    if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "6")//NO OBJETO DE IVA
                    {
                        subtotalnoiva += GetDecimal(baseimp.InnerText);
                    }
                    if (codigo.InnerText == "2" && codigoporcentaje.InnerText == "7")//EXENTO DE IVA
                    {
                        subtotalextiva += GetDecimal(baseimp.InnerText);
                    }
                    if (codigo.InnerText == "3")//TARIFA ICE
                    {
                        subtotalice += GetDecimal(baseimp.InnerText);
                        valorice += GetDecimal(valor.InnerText);
                    }
                }
                comprobante.total.tot_subtot_0 = subtotal0;
                comprobante.total.tot_subtotal = subtotaliva;
                //comprobante.com_subtotalnoiva = subtotalnoiva;
                //comprobante.com_subtotalextiva = subtotalextiva;
                comprobante.total.tot_dias_plazo = 30;
                comprobante.total.tot_timpuesto = valoriva;
                comprobante.total.tot_impuesto = 2;//IVA EN VENTAS

                comprobante.total.tot_porc_impuesto = porciva;
                comprobante.total.tot_ice = valorice;

                




                XmlNode detalles = xmldoc.SelectSingleNode("/notaCredito/detalles");
                comprobante.notascre = new List<Dnotacre>();

                //List<CodigoImpuesto> lstiva = new JavaScriptSerializer().Deserialize<List<CodigoImpuesto>>(Constantes.GetParameterValue("codigosiva"));
                //List<CodigoImpuesto> lstice = new JavaScriptSerializer().Deserialize<List<CodigoImpuesto>>(Constantes.GetParameterValue("codigosice"));
                //List<Detallecomp> lstdet = DetallecompBLL.GetAll("det_empresa=" + comprobante.com_empresa + " and det_comprobante=" + comprobante.com_codigo, "");

                List<Tiponc> lsttipos = TiponcBLL.GetAll("tnc_tclipro=4", "");
              

                foreach (XmlNode detalle in detalles.ChildNodes)
                {
                    Dnotacre det = new Dnotacre();
                                                           
                    string codigo = GetString(detalle.SelectSingleNode("codigoInterno"));
                    Tiponc tipo = lsttipos.Find(delegate (Tiponc t) { return t.tnc_codigo.ToString() == codigo; });

                    det.dnc_empresa = comprobante.com_empresa;
                    det.dnc_tiponc = tipo.tnc_codigo;
                    det.dnc_tiponcid = tipo.tnc_id;
                    det.dnc_tiponcnombre = tipo.tnc_nombre;                    
                    det.dnc_valor = GetDecimal(detalle.SelectSingleNode("precioTotalSinImpuesto").InnerText);
                    //det.dnc_cheque = 

                    
                    XmlNode impuestosdet = detalle.SelectSingleNode("impuestos");
                    foreach (XmlNode impuesto in impuestosdet.ChildNodes)
                    {
                        if (impuesto.SelectSingleNode("codigo").InnerText == "2")//IVA
                        {
                            det.dnc_cheque = 1;
                            //det.codiva = GetString(impuesto.SelectSingleNode("codigoPorcentaje"));
                            //det.porciva = GetDecimal(impuesto.SelectSingleNode("tarifa").InnerText);
                            //det.valiva = GetDecimal(impuesto.SelectSingleNode("valor").InnerText);
                        }
                        //if (impuesto.SelectSingleNode("codigo").InnerText == "3")//ICE
                        //{
                        //    //det.grabaice = 1;
                        //    //det.codice = GetString(impuesto.SelectSingleNode("codigoPorcentaje"));
                        //    //det.porcice = GetDecimal(impuesto.SelectSingleNode("tarifa").InnerText);
                        //    //det.valice = GetDecimal(impuesto.SelectSingleNode("valor").InnerText);
                        //}

                    }

                   
                    comprobante.notascre.Add(det);


                }
                       

              

            }
            return comprobante;

        }
    }
}

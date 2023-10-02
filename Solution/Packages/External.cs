using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;

namespace Packages
{
    public class External
    {
        public static string  SaveFacturaGuia(string strxml)
        {
            string usuarioexterno = Constantes.GetParameter("usrexterno");
            string productoexterno = Constantes.GetParameter("productoexterno");

            string retorno = "";
            bool save = true;
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(strxml);

            bool error = false;
            

            XmlNode xmlcabecera = xmldoc.SelectSingleNode("/comprobante/cabecera");
            XmlNode xmlusuario = xmlcabecera.SelectSingleNode("usuario");

            if (xmlusuario!=null)
            {
                if (xmlusuario.InnerText != "")
                    usuarioexterno = xmlusuario.InnerText;
            }

            XmlNode xmlfecha = xmlcabecera.SelectSingleNode("fecha");
            XmlNode xmldia = xmlfecha.SelectSingleNode("dia");
            XmlNode xmlmes = xmlfecha.SelectSingleNode("mes");
            XmlNode xmlanio = xmlfecha.SelectSingleNode("anio");
            XmlNode xmlhora = xmlfecha.SelectSingleNode("hora");
            XmlNode xmlminuto = xmlfecha.SelectSingleNode("minuto");            
            XmlNode xmltipodoc= xmlcabecera.SelectSingleNode("tipodoc");
            XmlNode xmlctipocom = xmlcabecera.SelectSingleNode("ctipocom");
            XmlNode xmlalmacen= xmlcabecera.SelectSingleNode("almacen");
            XmlNode xmlpventa= xmlcabecera.SelectSingleNode("puntoventa");
            XmlNode xmlsecuencia = xmlcabecera.SelectSingleNode("secuencia");
            XmlNode xmlconcepto = xmlcabecera.SelectSingleNode("concepto");

            XmlNode xmlcliente = xmlcabecera.SelectSingleNode("cliente");
            XmlNode xmlremitente= xmlcabecera.SelectSingleNode("remitente");
            XmlNode xmldestinatario= xmlcabecera.SelectSingleNode("destinatario");

            XmlNode xmlpolitica = xmlcabecera.SelectSingleNode("politica");
            XmlNode xmlruta = xmlcabecera.SelectSingleNode("ruta");
            XmlNode xmlentrega = xmlcabecera.SelectSingleNode("entrega");
            XmlNode xmlguia = xmlcabecera.SelectSingleNode("guia");
            XmlNode xmlnum1 = xmlguia.SelectSingleNode("numero1");
            XmlNode xmlnum2 = xmlguia.SelectSingleNode("numero2");
            XmlNode xmlnum3 = xmlguia.SelectSingleNode("numero3");

            XmlNode xmltransporte = xmlcabecera.SelectSingleNode("transporte");
            XmlNode xmlvalordeclarado = xmltransporte.SelectSingleNode("valordeclarado");       
            XmlNode xmlporcseguro= xmltransporte.SelectSingleNode("porcseguro");
            XmlNode xmldomicilio= xmltransporte.SelectSingleNode("domicilio");


            XmlNode xmldetalle = xmldoc.SelectSingleNode("/comprobante/detalle");



            DateTime fecha = new DateTime(GetEnteroXML(xmlanio), GetEnteroXML(xmlmes), GetEnteroXML(xmldia), GetEnteroXML(xmlhora), GetEnteroXML(xmlminuto), 0);
            List<Almacen> almacen = AlmacenBLL.GetAll(new WhereParams("alm_empresa={0} and alm_id={1}", 1, xmlalmacen.InnerText), "");
            List<Puntoventa> pventa = PuntoventaBLL.GetAll(new WhereParams("pve_empresa={0} and pve_id={1}", 1, xmlpventa.InnerText), "");
            if (almacen.Count == 0)
            {
                save = false;
                retorno = "No existe el almacen " + xmlalmacen.InnerText;
            }
            if (pventa.Count == 0)
            {
                save = false;
                retorno = "No existe el punto de venta" + xmlpventa.InnerText;
            }

            int numero = -1;
            if (!int.TryParse(xmlsecuencia.InnerText,out numero))
            {
                save = false;
                retorno = "La secuencia no es numérica " + xmlsecuencia.InnerText;
            }
            //Valida si no existe un comprobante con la numeracion enviada
            if (save)
            {
                Comprobante c = new Comprobante();
                c.com_empresa = 1;
                c.com_fecha = fecha;
                c.com_almacen = almacen[0].alm_codigo;
                c.com_pventa = pventa[0].pve_secuencia;
                c.com_numero = numero;               
                c.com_periodo = fecha.Year;
                c.com_tipodoc = GetEnteroXML(xmltipodoc);
                c.com_ctipocom = GetEnteroXML(xmlctipocom);
                c.com_doctran = General.GetNumeroComprobante(c);
                List<Comprobante> lstcom = ComprobanteBLL.GetAll(new WhereParams("com_empresa={0} and com_doctran={1}", 1, c.com_doctran), "");
                if (lstcom.Count>0)
                {
                    
                    save = false;
                    retorno = string.Format("El comprobante {0} ya existe ({1}) ", c.com_doctran, lstcom[0].com_codigo);
                }
            }


            //Validación de usuario
            List<Usuario> lstusr = UsuarioBLL.GetAll("usr_id='" + usuarioexterno + "'", "");
            if (lstusr.Count==0)
            {
                save = false;
                retorno = "El usuario " + usuarioexterno + " no esta registrado...";
            }



            Persona cliente = new Persona();
            Persona remitente = new Persona();
            Persona destinatario = new Persona();   

            try
            {
                cliente = GetPersona(xmlcliente,true);
                remitente = GetPersona(xmlremitente,false);
                destinatario = GetPersona(xmldestinatario,false);

            }
            catch(Exception ex)
            {
                save = false;
                retorno = ex.Message;
            }

            List<Politica> politica = PoliticaBLL.GetAll(new WhereParams("pol_empresa={0} and pol_id={1}", 1, xmlpolitica.InnerText), "");

            if (politica.Count == 0)
            {
                save = false;
                retorno = "No existe la politica" + xmlpolitica.InnerText;
            }

            List<Ruta> ruta = RutaBLL.GetAll(new WhereParams("rut_empresa={0} and rut_id={1}", 1, xmlruta.InnerText), "");
            if (ruta.Count == 0)
            {
                save = false;
                retorno = "No existe la ruta" + xmlruta.InnerText;
            }

            bool factura = false;

            if (save)
            {
                Comprobante comp = new Comprobante();
                comp.com_empresa = 1;
                comp.com_fecha = fecha;
                comp.com_almacen = almacen[0].alm_codigo;
                comp.com_pventa = pventa[0].pve_secuencia;
                comp.com_numero = numero;
                //comp.com_doctran = General.GetNumeroComprobante(comp);
                comp.com_periodo = fecha.Year;
                comp.com_mes = fecha.Month;
                comp.com_dia = fecha.Day;

                comp.com_concepto = xmlconcepto.InnerText;
                comp.com_codclipro = cliente.per_codigo;

                comp.com_tipodoc = GetEnteroXML(xmltipodoc);
                comp.com_ctipocom = GetEnteroXML(xmlctipocom);
                if (comp.com_tipodoc == Constantes.cFactura.tpd_codigo)
                {
                    factura = true;
                    comp.com_nocontable = 0;
                }
                else
                    comp.com_nocontable = 1;


                comp.crea_usr = usuarioexterno;
                comp.crea_fecha = DateTime.Now;


                //CCOMDOC
                comp.ccomdoc = new Ccomdoc();
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                //comp.ccomdoc.cdoc_acl_nroautoriza = 
                comp.ccomdoc.cdoc_politica = politica[0].pol_codigo;
                comp.ccomdoc.cdoc_listaprecio = cliente.per_listaprecio;
                comp.ccomdoc.cdoc_nombre = cliente.per_razon;
                comp.ccomdoc.cdoc_direccion = cliente.per_direccion;
                comp.ccomdoc.cdoc_telefono = cliente.per_telefono;
                comp.ccomdoc.cdoc_ced_ruc = cliente.per_ciruc;
                comp.ccomdoc.detalle = new List<Dcomdoc>();

                comp.ccomdoc.crea_usr = usuarioexterno;
                comp.ccomdoc.crea_fecha = DateTime.Now;

                //DETALLE
                decimal valortotal = 0;
                List<Producto> lstproductos = ProductoBLL.GetAll("pro_empresa=" + comp.com_empresa, "");

                
                foreach (XmlNode detalle in xmldetalle.ChildNodes)
                {
                    XmlNode xmlid = detalle.SelectSingleNode("id");
                    XmlNode xmlobs= detalle.SelectSingleNode("observaciones");
                    XmlNode xmlpeso= detalle.SelectSingleNode("peso");
                    XmlNode xmlcantidad = detalle.SelectSingleNode("cantidad");
                    XmlNode xmlprecio = detalle.SelectSingleNode("precio");
                    XmlNode xmlvalor = detalle.SelectSingleNode("valor");

                    Producto producto = lstproductos.Find(delegate (Producto p) { return p.pro_id == xmlid.InnerText; });
                    if (producto==null)//CARGA CON PRODUCTO EXTERNO
                        producto = lstproductos.Find(delegate (Producto p) { return p.pro_id == productoexterno; });
                    if (producto != null)
                    {
                        

                        Dcomdoc ddoc = new Dcomdoc();
                        ddoc.ddoc_empresa = comp.com_empresa;
                        ddoc.ddoc_producto = producto.pro_codigo;
                        ddoc.ddoc_bodega = 0;
                        ddoc.ddoc_observaciones = xmlobs.InnerText;
                        ddoc.ddoc_peso = GetDecimalXML(xmlpeso);
                        ddoc.ddco_udigitada = 1; //UNIDADES
                        ddoc.ddoc_cantidad = GetDecimalXML(xmlcantidad);
                        ddoc.ddoc_precio = GetDecimalXML(xmlprecio);
                        ddoc.ddoc_total = GetDecimalXML(xmlvalor);
                        ddoc.ddoc_grabaiva = 0; //NO GRABA IVA
                        //AUDITORIA
                        ddoc.detallecalculo = new List<Dcalculoprecio>();
                        ddoc.crea_usr = usuarioexterno;
                        ddoc.crea_fecha = DateTime.Now;
                        comp.ccomdoc.detalle.Add(ddoc);
                                                
                        valortotal += GetDecimalXML(xmlvalor);
                    }
                    else
                    {
                        error = true;
                        return "NO EXISTE PRODUCTO " + xmlid.InnerText;
                    }

                }

                //CCOMENV
                comp.ccomenv = new Ccomenv();
                comp.ccomenv.cenv_empresa = comp.com_empresa;

                comp.ccomenv.cenv_remitente = remitente.per_codigo;
                comp.ccomenv.cenv_empresa_rem = remitente.per_empresa;
                comp.ccomenv.cenv_nombres_rem = remitente.per_razon;
                comp.ccomenv.cenv_direccion_rem = remitente.per_direccion;
                comp.ccomenv.cenv_telefono_rem = remitente.per_telefono;
                comp.ccomenv.cenv_ciruc_rem = remitente.per_ciruc;

                comp.ccomenv.cenv_destinatario = destinatario.per_codigo;
                comp.ccomenv.cenv_empresa_des= destinatario.per_empresa;
                comp.ccomenv.cenv_nombres_des= destinatario.per_razon;
                comp.ccomenv.cenv_direccion_des= destinatario.per_direccion;
                comp.ccomenv.cenv_telefono_des = destinatario.per_telefono;
                comp.ccomenv.cenv_ciruc_des= destinatario.per_ciruc;

                comp.ccomenv.cenv_ruta = ruta[0].rut_codigo;
                comp.ccomenv.cenv_observacion = xmlentrega.InnerText;
                comp.ccomenv.cenv_guia1 = xmlnum1.InnerText;
                comp.ccomenv.cenv_guia2 = xmlnum2.InnerText;
                comp.ccomenv.cenv_guia3 = xmlnum3.InnerText;
                comp.ccomenv.crea_usr = usuarioexterno;
                comp.ccomenv.crea_fecha = DateTime.Now;
                //TOTAL
                comp.total = new Total();
                comp.total.tot_empresa = comp.com_empresa;
                comp.total.tot_impuesto = 2;
                comp.total.tot_porc_desc = politica[0].pol_porc_desc;
                comp.total.tot_dias_plazo = politica[0].pol_dias_plazo;
                comp.total.tot_nro_pagos = politica[0].pol_nro_pagos;
                
                comp.total.tot_porc_impuesto = Constantes.GetValorIVA(comp.com_fecha);
                comp.total.crea_usr = usuarioexterno;
                comp.total.crea_fecha = DateTime.Now;
                string[] arrayporcseguro = Constantes.cPorcSeguro.Split('|');
                decimal valorporcseguro = 0;

                if (arrayporcseguro.Length > 0)
                    valorporcseguro = decimal.Parse(arrayporcseguro[0]);
                if (arrayporcseguro.Length > 1)
                {
                    if (comp.com_almacen.HasValue && comp.com_pventa.HasValue)
                    {
                        for (int i = 1; i < arrayporcseguro.Length; i++)
                        {
                            string[] arrayvalores = arrayporcseguro[i].Split(';');

                            if (comp.com_almacen.Value.ToString() == arrayvalores[0] && comp.com_pventa.Value.ToString() == arrayvalores[1])
                            {
                                valorporcseguro = decimal.Parse(arrayvalores[2]);
                                break;
                            }

                        }
                    }

                }

                decimal vs = GetDecimalXML(xmlporcseguro);
                if (vs > 0)
                    valorporcseguro = vs;

                comp.total.tot_porc_seguro = valorporcseguro;
                comp.total.tot_vseguro = GetDecimalXML(xmlvalordeclarado);
                comp.total.tot_transporte = GetDecimalXML(xmldomicilio);

                //CAMPOS CALCULADOS
                decimal iva = 0;
                if (comp.total.tot_vseguro.HasValue && comp.total.tot_porc_seguro.HasValue)
                {
                    comp.total.tot_tseguro = comp.total.tot_vseguro * (comp.total.tot_porc_seguro / 100);

                    iva = comp.total.tot_tseguro.Value * (comp.total.tot_porc_impuesto.Value / 100);
                }
                else
                    comp.total.tot_tseguro = 0;

              

                comp.total.tot_timpuesto = iva; //IVA
                comp.total.tot_subtot_0 = valortotal;//+ comp.total.tot_transporte;
                comp.total.tot_subtotal = 0;// comp.total.tot_tseguro.Value;
                comp.total.tot_total = comp.total.tot_subtot_0 + comp.total.tot_subtotal + comp.total.tot_transporte + comp.total.tot_tseguro.Value + comp.total.tot_timpuesto;

                comp.planillacomp = new Planillacomprobante(); 



                try
                {
                   comp = FAC.save_factura(comp);
                    
                   if (factura)
                        comp = FAC.account_factura(comp);                    
                   return comp.com_doctran;
                }
                catch (Exception ex)
                {
                    ExceptionHandling.Log.AddExepcion(ex);
                    return "ERROR: "+ex.Message;
                }
               




            }
            else
            {
                return retorno;
            }


        }


        public static int GetEnteroXML(XmlNode nodo)
        {
            string text = nodo.InnerText;
            int retorno = 0;
            int.TryParse(text, out retorno);
            return retorno;
        }

        public static decimal GetDecimalXML(XmlNode nodo)
        {
            string text = nodo.InnerText;
            decimal retorno = 0;
            decimal.TryParse(text.Replace(".", ","), out retorno);
            return retorno;
        }

        public static Persona GetPersona(XmlNode persona,bool save)
        {

            XmlNode xmlruc = persona.SelectSingleNode("ruc");
            XmlNode xmlrazon = persona.SelectSingleNode("razon");
            XmlNode xmldireccion = persona.SelectSingleNode("direccion");
            XmlNode xmltelefono = persona.SelectSingleNode("telefono");
            
            Persona per = new Persona();
            per.per_razon = xmlrazon.InnerText;
            per.per_ciruc = xmlruc.InnerText;
            per.per_direccion = xmldireccion.InnerText;
            per.per_telefono = xmltelefono.InnerText;
            per.per_empresa = 1;

            WhereParams whereparams = new WhereParams("per_ciruc = {0} and pxt_tipo={1} and per_empresa={2}", per.per_ciruc, Constantes.cCliente, 1);
            List<Persona> lstPersona = vPersonaBLL.GetAll(whereparams, "");
            if (lstPersona.Count > 0)
                return lstPersona[0];
            else
            {
                if (save)
                    return SavePersona(per);
                else
                    return per;
            }
                     

        }

        public static Persona SavePersona(Persona per)
        {
            if (Functions.Validaciones.valida_cedularuc(per.per_ciruc))
            {
                BLL transaction = new BLL();
                transaction.CreateTransaction();

                try
                {
                    transaction.BeginTransaction();
                    Listaprecio lista = Constantes.GetListaPrecio();//Obtiene lista de precio por defecto
                    per.per_listaprecio = lista.lpr_codigo;
                    per.per_listanombre = lista.lpr_nombre;
                    per.per_listaid = lista.lpr_id;

                    Politica politica = Constantes.GetPoliticacli();//Obtiene politica por defecto

                    per.per_politica = politica.pol_codigo;
                    per.per_politicaid = politica.pol_id;
                    per.per_politicanombre = politica.pol_nombre;
                    per.per_politicadesc = politica.pol_porc_desc;
                    per.per_politicanropagos = politica.pol_nro_pagos;
                    per.per_politicadiasplazo = politica.pol_dias_plazo;
                    per.per_politicaporpagocon = politica.pol_porc_pago_con;


                    //FALTA VENDEDOR
                    Empresa emp = new Empresa();
                    emp.emp_codigo_key = per.per_empresa;
                    emp = EmpresaBLL.GetByPK(emp);
                    Usuario usr = new Usuario();
                    usr.usr_id_key = per.crea_usr;
                    usr = UsuarioBLL.GetByPK(usr);

                    per.per_id = General.GetIdPersona(emp, usr);
                    per.per_retfuente = Constantes.GetImpRteFte().imp_codigo;
                    per.per_retiva = Constantes.GetImpRteIVA().imp_codigo;
                    per.per_estado = Constantes.cEstadoGrabado;
                    per.per_codigo = PersonaBLL.InsertIdentity(transaction, per);

                    Personaxtipo pxt = new Personaxtipo();
                    pxt.pxt_persona = per.per_codigo;
                    pxt.pxt_empresa = per.per_empresa;
                    pxt.pxt_estado = Constantes.cEstadoGrabado;
                    pxt.pxt_tipo = Constantes.cCliente;
                    Catcliente catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto                        


                    pxt.pxt_politicas = politica.pol_codigo;
                    pxt.pxt_cat_persona = catcliente.cat_codigo;
                    pxt.crea_usr = per.crea_usr;
                    pxt.crea_fecha = per.crea_fecha;
                    pxt.mod_usr = per.mod_usr;
                    pxt.mod_fecha = per.mod_fecha;
                    PersonaxtipoBLL.Insert(transaction, pxt);

                    transaction.Commit();

                    return per;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            else
                throw new System.ArgumentException("Cédula/RUC incorrecto");
       
        }



        delegate string DelegadoGenerarComprobante(Comprobante comprobante);

        public static void GenerarComprobanteAsync(Comprobante comprobante)
        {
            DelegadoGenerarComprobante delegado = new DelegadoGenerarComprobante(GenerarComprobante);
            IAsyncResult result = delegado.BeginInvoke(comprobante,null, null);
        }


        public static string GenerarComprobante(Comprobante comp)
        {
            string portalexterno = Constantes.GetParameter("portalexterno");

            if (portalexterno == "1")
            {

                comp.com_empresa_key = comp.com_empresa;
                comp.com_codigo_key = comp.com_codigo;
                comp = ComprobanteBLL.GetByPK(comp);

                comp.ccomdoc = new Ccomdoc();
                comp.ccomenv = new Ccomenv();
                comp.total = new Total();

                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante_key = comp.com_codigo;
                comp.ccomdoc.cdoc_empresa_key = comp.com_empresa;
                comp.ccomdoc = CcomdocBLL.GetByPK(comp.ccomdoc);

                comp.ccomenv.cenv_comprobante = comp.com_codigo;
                comp.ccomenv.cenv_empresa = comp.com_empresa;
                comp.ccomenv.cenv_comprobante_key = comp.com_codigo;
                comp.ccomenv.cenv_empresa_key = comp.com_empresa;
                comp.ccomenv = CcomenvBLL.GetByPK(comp.ccomenv);

                comp.total.tot_comprobante = comp.com_codigo;
                comp.total.tot_empresa = comp.com_empresa;
                comp.total.tot_comprobante_key = comp.com_codigo;
                comp.total.tot_empresa_key = comp.com_empresa;
                comp.total = TotalBLL.GetByPK(comp.total);


                comp.total.tot_subtot_0 += comp.total.tot_transporte;
                comp.total.tot_subtotal += (comp.total.tot_tseguro.HasValue) ? comp.total.tot_tseguro.Value : 0;

                comp.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", comp.com_codigo, comp.com_empresa), "");
                comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", comp.com_codigo, comp.com_empresa), "ddoc_secuencia");

                comp.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobantefac = {0} and rfac_empresa = {1}", comp.com_codigo, comp.com_empresa), "");

                string hojaruta = "";
                if (comp.rutafactura.Count > 0)
                {
                    Comprobante hr = new Comprobante();
                    hr.com_empresa = comp.com_empresa;
                    hr.com_empresa_key = comp.com_empresa;
                    hr.com_codigo = comp.rutafactura[0].rfac_comprobanteruta;
                    hr.com_codigo_key = comp.rutafactura[0].rfac_comprobanteruta;
                    hr = ComprobanteBLL.GetByPK(hr);
                    hojaruta = hr.com_doctran;
                }


                ComprobanteExt c = new ComprobanteExt();

                c.com_empresa = comp.com_empresa;
                c.com_codigo = comp.com_codigo;
                c.com_fecha = comp.com_fecha;
                c.com_tipo = comp.com_tipodoc;
                c.com_establecimiento = comp.com_almacenid;
                c.com_puntoemision = comp.com_pventaid;
                c.com_secuencia = comp.com_numero.ToString("0000000");
                c.com_autoriza = comp.com_claveelec;
                c.com_ciruccli = comp.ccomdoc.cdoc_ced_ruc;
                c.com_nombrescli = comp.ccomdoc.cdoc_nombre;
                c.com_direccioncli = comp.ccomdoc.cdoc_direccion;
                c.com_telefonocli = comp.ccomdoc.cdoc_telefono;
                //c.com_emailcli = comp.
                c.com_cirucdes = comp.ccomenv.cenv_ciruc_des;
                c.com_nombresdes = comp.ccomenv.cenv_nombres_des + " " + comp.ccomenv.cenv_apellidos_des;
                c.com_direcciondes = comp.ccomenv.cenv_direccion_des;
                c.com_telefonodes = comp.ccomenv.cenv_telefono_des;
                //c.com_emaildes= comp.ccomenv.cenv_ma;

                c.com_cirucrem = comp.ccomenv.cenv_ciruc_rem;
                c.com_nombresrem = comp.ccomenv.cenv_nombres_rem + " " + comp.ccomenv.cenv_apellidos_rem;
                c.com_direccionrem = comp.ccomenv.cenv_direccion_rem;
                c.com_telefonorem = comp.ccomenv.cenv_telefono_rem;

                c.com_formapago = comp.ccomdoc.cdoc_politicanombre;
                c.com_ruta = comp.ccomenv.cenv_rutaorigen + "-" + comp.ccomenv.cenv_rutadestino;
                c.com_entrega = comp.ccomenv.cenv_observacion;
                c.com_hojaruta = hojaruta;
                //c.com_cirucsoc = comp.ccomenv.cenv_nombres_soc
                c.com_nombressoc = comp.ccomenv.cenv_nombres_soc;
                c.com_nombrescho = comp.ccomenv.cenv_nombres_cho;
                c.com_guiaremision = comp.ccomenv.cenv_guia1 + "-" + comp.ccomenv.cenv_guia2 + "-" + comp.ccomenv.cenv_guia3;
                c.com_placaveh = comp.ccomenv.cenv_placa;
                c.com_discoveh = comp.ccomenv.cenv_disco;

                c.com_cirucret = comp.ccomenv.cenv_ciruc_ret;
                c.com_nombresret = comp.ccomenv.cenv_nombres_ret;
                c.com_fecharet = comp.ccomenv.cenv_fecha_ret;
                c.com_retirado = comp.ccomenv.cenv_despachado_ret;
                c.com_observacionret = comp.ccomenv.cenv_observaciones_ret;
                c.com_observacion = comp.com_concepto;

                c.com_valordeclarado = comp.total.tot_vseguro;
                c.com_domicilio = comp.total.tot_transporte;
                c.com_seguro = comp.total.tot_tseguro;
                c.com_subtotal0 = comp.total.tot_subtot_0;
                c.com_subtotaliva = comp.total.tot_subtotal;
                c.com_descuento0 = comp.total.tot_descuento1;
                c.com_descuentoiva = comp.total.tot_descuento2;
                c.com_iva = comp.total.tot_impuesto;
                c.com_total = comp.total.tot_total;
                c.com_estado = comp.com_estado;

                List<DetalleExt> lst = new List<DetalleExt>();

                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    DetalleExt d = new DetalleExt();
                    d.det_empresa = item.ddoc_empresa;
                    d.det_comprobante = item.ddoc_comprobante;
                    d.det_secuencia = item.ddoc_secuencia;
                    d.det_producto = item.ddoc_producto.ToString();
                    d.det_observacion = item.ddoc_productonombre + " " + item.ddoc_observaciones;
                    d.det_cantidad = item.ddoc_cantidad;
                    d.det_precio = item.ddoc_precio;
                    d.det_descuento = item.ddoc_dscitem;
                    d.det_valor = item.ddoc_total;
                    //d.det_estado = item.
                    lst.Add(d);
                }
                c.detalle = lst;

                string json = new JavaScriptSerializer().Serialize(c);
                COMP.Metodos ws = new COMP.Metodos();
                ws.SaveFacturaGuiaAsync(json);
                ws.SaveFacturaGuiaCompleted += Ws_SaveFacturaGuiaCompleted;
             
            }
            return "OK";

        }

        private static void Ws_SaveFacturaGuiaCompleted(object sender, COMP.SaveFacturaGuiaCompletedEventArgs e)
        {
            string a = "";
            //throw new NotImplementedException();
        }
    }
}

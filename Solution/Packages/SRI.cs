using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using System.Web;
using System.Data;
using System.Xml;
using System.Reflection;

namespace Packages
{
    public class SRI
    {

        public static int tipofac = Constantes.cFactura.tpd_codigo;
        public static int tipoobl = Constantes.cObligacion.tpd_codigo;
        public static int tipoliq = Constantes.cLiquidacionCompra.tpd_codigo;
        public static int tiporet = Constantes.cRetencion.tpd_codigo;
        public static int tiponc = Constantes.cNotacre.tpd_codigo;
        public static int tipond = Constantes.cNotadeb.tpd_codigo;
        public static int tiponcp = Constantes.cNotacrePro.tpd_codigo;

        public static decimal valoriva = 0;
        public static StringBuilder strmensajes = new StringBuilder(); 
        public static XmlDocument xmldoc = new XmlDocument();

        public static int periodo;
        public static int mes;
        public static Empresa empresa = new Empresa();
        public static PropertyInfo[] empresaprop;
        public static List<vVenta> ventas = new List<vVenta>();
        public static List<vVenta> ventasres = new List<vVenta>();
        public static List<vVenta> notascc = new List<vVenta>();
        public static List<vVenta> notasccres = new List<vVenta>();
        public static PropertyInfo[] ventasprop;

        public static List<vRetencionVenta> retencionesventas = new List<vRetencionVenta>();

        

            
        public static List<vCompras> comprasret = new List<vCompras>();
        public static List<vCompras> comprasnoret = new List<vCompras>();
        public static List<vCompras> notascp = new List<vCompras>();
        public static PropertyInfo[] comprasprop;
        public static List<vRetencion> retenciones = new List<vRetencion>();
        public static PropertyInfo[] retencionesprop;

        public static List<vEstablecimiento> establecimientos = new List<vEstablecimiento>();
        public static PropertyInfo[] establecimientosprop;

        public static List<vComprobante> anulados = new List<vComprobante>();
        public static PropertyInfo[] anuladosprop;

        public static List<string> rucsOmite = new List<string>();

        

        public static void CreateXmlNode(XmlNode parent, XmlNode nodtemp, Int64? codigo, object datasource)
        {


            switch (nodtemp.NodeType)
            {
                case XmlNodeType.XmlDeclaration:
                    XmlNode xmldec = xmldoc.CreateXmlDeclaration(((XmlDeclaration)nodtemp).Version, ((XmlDeclaration)nodtemp).Encoding, ((XmlDeclaration)nodtemp).Standalone); 
                    xmldoc.AppendChild(xmldec);
                    break;
                case XmlNodeType.Text:
                    XmlNode xmltex = xmldoc.CreateTextNode(nodtemp.InnerText);                    
                    if (parent == null)
                        xmldoc.AppendChild(xmltex);
                    else
                        parent.AppendChild(xmltex);
                    break;
                default:

                    bool add = true;
                    XmlNode xmlnode = xmldoc.CreateElement(nodtemp.Name);                    

                    if (nodtemp.Attributes.Count > 0)
                    {
                        string source = (nodtemp.Attributes.Count > 0) ? nodtemp.Attributes[0].Value : "";                        
                        string function =  (nodtemp.Attributes.Count > 1) ?nodtemp.Attributes[1].Value:"";
                        string filter =  (nodtemp.Attributes.Count > 2) ?nodtemp.Attributes[2].Value:"";
                        
                        string empty = (nodtemp.Attributes["empty"] != null) ? nodtemp.Attributes["empty"].Value : "";

                        xmlnode.InnerText = GetSourceValue(source, function,filter, datasource);
                        if (empty == "no" && string.IsNullOrEmpty(xmlnode.InnerText))
                            add = false;
                    }


                    foreach (XmlNode nod in nodtemp.ChildNodes)
                    {
                        CreateXmlNode(xmlnode, nod, codigo, datasource);
                    }

                    if (nodtemp.Name == "compras")
                        GetCompras(xmlnode);
                    if (nodtemp.Name == "ventas")
                        GetVentas(xmlnode);
                    if (nodtemp.Name == "air")
                        GetAIR(xmlnode,codigo,(vCompras)datasource);
                    if (nodtemp.Name == "ventasEstablecimiento")
                        GetVentasEstablecimientos(xmlnode);
                    if (nodtemp.Name == "anulados")
                        GetAnulados(xmlnode);
                    if (nodtemp.Name == "formasDePago")
                    {
                        add = GetFormasPago(xmlnode, datasource);
                    }

                    if (add)
                    {
                        if (parent == null)
                            xmldoc.AppendChild(xmlnode);
                        else
                            parent.AppendChild(xmlnode);
                    }
                    break;
            }

            

            
 
            
        }
        

        public static string GetSourceValue(string source, string function, string filter, object datasource)
        {
            string valor = "";
            string[] arraysource = source.Split('.');
            if (arraysource.Length > 1)
            {
                string table = arraysource[0];
                string field = arraysource[1];
                return GetData(table, field, function, filter, datasource);

            }
            return valor;

        }



        public static string GetValueByType(object valor)
        {
            if (valor != null)
            {
                Type tipo = valor.GetType();
                if (tipo == typeof(DateTime))
                {
                    //return ((DateTime)valor).ToShortDateString();
                    return ((DateTime)valor).ToString("dd/MM/yyyy");

                }
                if (tipo == typeof(decimal) || tipo == typeof(float))
                {
                    return  ((decimal)valor).ToString("0.00").Replace(",",".");

                }
                return valor.ToString();
            }
            else
                return "";


        }

        public static string GetData(string table, string field, string function, string filter, object datasource)
        {
             
            switch (table.ToUpper())
            {
                case "EMPRESA":
                    foreach (PropertyInfo property in empresaprop)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(datasource, null)); 
                        }

                    }
            
                    break;
                case "PARAMS":
                    if (field == "anio")
                        return periodo.ToString("0000");
                    if (field == "mes")
                        return mes.ToString("00");
                    break;
                case "VENTAS":

                    if (function == "sum")
                    {
                        decimal valor = 0;
                        foreach (vVenta item in ventas)
                        {
                            decimal? v = item.subtotal + item.subimpuesto + item.transporte+ item.seguro - item.desc0;
                            valor += (v.HasValue) ? v.Value : 0;
                        }
                        foreach (vVenta item in notascc)
                        {
                            decimal? v = item.subtotal + item.subimpuesto + item.transporte + item.seguro;
                            valor -= (v.HasValue) ? v.Value : 0;
                        }



                        /*foreach (PropertyInfo property in ventasprop)
                        {

                            if (property.Name == field)
                            {

                                foreach (vVenta item in ventas)
                                {
                                    decimal? v = (decimal?)property.GetValue(item, null);
                                    valor += (v.HasValue) ? v.Value : 0;
                                }
                            }
                        }*/
                        return valor.ToString("0.00").Replace(",", "."); 
                    }
                    else
                    {
                        foreach (PropertyInfo property in ventasprop)
                        {
                            if (property.Name == field)
                            {
                                return GetValueByType(property.GetValue(datasource, null));
                                //return property.GetValue(datasource, null).ToString();
                            }

                        }
                    }
                    break;
                
                case "COMPRAS":
                    foreach (PropertyInfo property in comprasprop)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(datasource, null));
                            //object retorno = property.GetValue(datasource, null);

                            //return (retorno != null) ? retorno.ToString() : "";
                        }

                    }
                    break;
                case "RETENCION":
                    foreach (PropertyInfo property in retencionesprop)
                    {
                        if (property.Name == field)
                        {
                            return GetValueByType(property.GetValue(datasource, null));
                            //object retorno = property.GetValue(datasource, null);

                            //return (retorno != null) ? retorno.ToString() : "";
                        }

                    }
                    break;
                case "ESTABLECIMIENTO":

                    if (function == "count")
                    {
                        
                        return establecimientos.Count().ToString("000");
                    }
                    else
                    {
                        foreach (PropertyInfo property in establecimientosprop)
                        {
                            if (property.Name == field)
                            {
                                return GetValueByType(property.GetValue(datasource, null));
                                //object retorno = property.GetValue(datasource, null);

                                //return (retorno != null) ? retorno.ToString() : "";
                            }

                        }
                    }
                    break;
                case "ANULADOS":

                    object valorret = ((DataRow)datasource)[field];
                    return (valorret != null) ? valorret.ToString() : "";                   
                    break;


            }
            return "";
        }


        public static void LoadData()
        {

            empresa.emp_codigo = 1;
            empresa.emp_codigo_key = 1;
            empresa = EmpresaBLL.GetByPK(empresa);
            empresaprop = empresa.GetProperties();

            string strrucsomite = Constantes.GetParameter("rucsomite");
            if (!String.IsNullOrEmpty(strrucsomite))
                rucsOmite = new List<string>(strrucsomite.Split(','));
                


           

            ventas = vVentaBLL.GetAll(new WhereParams("f.com_periodo ={0} and f.com_mes={1} and f.com_tipodoc={2} and f.com_estado={3}", periodo, mes, tipofac, Constantes.cEstadoMayorizado), "");
            //NOTAS DE CREDITO CLIENTES
            notascc = vVentaBLL.GetAll(new WhereParams("f.com_periodo ={0} and f.com_mes={1} and f.com_tipodoc={2} and f.com_estado={3}", periodo, mes, tiponc, Constantes.cEstadoMayorizado), "");

            vVenta v = new vVenta();
            ventasprop = v.GetProperties();
            vEstablecimiento e = new vEstablecimiento();
            establecimientosprop = e.GetProperties();

            retencionesventas = vRetencionVentaBLL.GetAll(new WhereParams("com_estado={0} and com_periodo={1} and com_mes={2}", Constantes.cEstadoMayorizado, periodo, mes), "");



            SetTablaVentas();
            SetTablaNotasCC();
            comprasret = vComprasBLL.GetAll(new WhereParams("o.com_periodo ={0} and o.com_mes={1} and (o.com_tipodoc={2} or o.com_tipodoc={3}) and o.com_estado={4} and r.com_estado={4} and drt_comprobante  is not null", periodo, mes, tipoobl, tipoliq, Constantes.cEstadoMayorizado), "");
            comprasnoret = vComprasBLL.GetAll(new WhereParams("o.com_periodo ={0} and o.com_mes={1} and (o.com_tipodoc={2} or o.com_tipodoc={3}) and o.com_estado={4} and drt_comprobante  is null", periodo, mes, tipoobl, tipoliq, Constantes.cEstadoMayorizado), "");
            notascp= vComprasBLL.GetAll(new WhereParams("o.com_periodo ={0} and o.com_mes={1} and (o.com_tipodoc={2}) and o.com_estado={3}", periodo, mes, tiponcp, Constantes.cEstadoMayorizado), "");
            vCompras c = new vCompras();
            comprasprop = c.GetProperties();

            //retenciones = vRetencionBLL.GetAll(new WhereParams("f.com_periodo={0}  and f.com_mes={1} and r.com_tipodoc = {2} and r.com_estado = {3} and imp_ret = 1", periodo, mes, tiporet, Constantes.cEstadoMayorizado), "");
            retenciones = vRetencionBLL.GetAll(new WhereParams("r.com_periodo={0}  and r.com_mes={1} and r.com_tipodoc = {2} and r.com_estado = {3} and imp_ret = 1", periodo, mes, tiporet, Constantes.cEstadoMayorizado), "");
            vRetencion r = new vRetencion();
            retencionesprop = r.GetProperties();

            anulados = vComprobanteBLL.GetAll(new WhereParams("c.com_periodo ={0} and c.com_mes={1} and (c.com_tipodoc={2} or c.com_tipodoc={3} or c.com_tipodoc={4} or c.com_tipodoc={5}) and c.com_estado={6}", periodo, mes, tipofac, tiporet, tiponc, tipond, Constantes.cEstadoEliminado), "c.com_tipodoc, c.com_almacen, c.com_pventa, c.com_numero");
            vComprobante a = new vComprobante();
            anuladosprop = a.GetProperties();

            


        }

        public static void GetCompras(XmlNode nodocompras)
        {
            string path = HttpContext.Current.Server.MapPath("xml");
            XmlDocument xmlcompras = new XmlDocument();
            xmlcompras.Load(path + "\\compras.xml");
            foreach (vCompras item in comprasret)
            {
                foreach (XmlNode nod in xmlcompras.ChildNodes)
                {
                    CreateXmlNode(nodocompras, nod, item.codigo, item);
                }
                
            }
            foreach (vCompras item in comprasnoret)
            {
                foreach (XmlNode nod in xmlcompras.ChildNodes)
                {
                    CreateXmlNode(nodocompras, nod, null, item);
                }

            }
            foreach (vCompras item in notascp)
            {
                foreach (XmlNode nod in xmlcompras.ChildNodes)
                {
                    CreateXmlNode(nodocompras, nod, null, item);
                }
            }




        }

        public static void SetTablaVentas()
        {
            ventasres = new List<vVenta>();
            establecimientos = new List<vEstablecimiento>();
            string tipoatsventa = Constantes.GetParameter("tipoatsventa");
            foreach (vVenta item in ventas)
            {
                item.ruc = (Functions.Validaciones.valida_cedularuc(item.ruc)) ? item.ruc : "9999999999999";
                //Valida Omite RUCS
                if (rucsOmite.Contains(item.ruc))
                    item.ruc = "9999999999999";

                vVenta venta = ventasres.Find(delegate(vVenta v) { return v.ruc == item.ruc; });
                if (venta == null)
                {
                    venta = new vVenta();
                    venta.ruc = item.ruc;
                    //venta.tipoid = (item.tipoid.Substring(0, 1).ToUpper() == "C") ? "05" : (item.tipoid.Substring(0, 1).ToUpper() == "R" ? "04" : "");
                    venta.tipodoc = item.tipodoc;
                    venta.tipoid = (item.ruc == "9999999999999") ? "07" : (item.ruc.Length == 13 ? "04" : "05");// (item.tipoid.Substring(0, 1).ToUpper() == "R") ? "04" : "05";
                    venta.tipoats = (item.ruc == "9999999999999") ? "18" : (!string.IsNullOrEmpty(tipoatsventa)) ? tipoatsventa : item.tipoats;
                    venta.parterel = (venta.tipoid == "04" || venta.tipoid == "05") ? "NO" : null;
                    venta.comprobantes = 0;
                    venta.subtotal = 0;
                    venta.subimpuesto = 0;
                    venta.transporte = 0;
                    venta.seguro = 0;
                    venta.impuesto = 0;
                    venta.total = 0;
                    venta.retfue= 0;
                    venta.retiva = 0;
                    venta.monto = 0;
                    venta.desc0 = 0;
                    ventasres.Add(venta);
                }

                if (!item.seguro.HasValue)
                    item.seguro = 0;

                venta.comprobantes = venta.comprobantes + 1;
                venta.subtotal += item.subtotal + item.transporte - item.desc0;
                venta.desc0 += item.desc0;
                venta.subimpuesto += item.subimpuesto + item.seguro;


                //venta.transporte += item.transporte;
                // venta.seguro += item.seguro;
                

                decimal ivareal = (item.subimpuesto.Value + item.seguro.Value) * valoriva;

                venta.impuesto += ivareal;// item.impuesto;
                
                venta.total += item.total;
                //venta.retfue+= item.retfue;                
                //venta.retiva += item.retiva;
                venta.monto += item.monto;

                string almid = "";
                string pveid = "";
                string[] arraydoctran = item.doctran.Split('-');                
                if (arraydoctran.Length > 3)
                {
                    almid = arraydoctran[1];
                    pveid = arraydoctran[2];                    
                }
                
                
                vEstablecimiento est = establecimientos.Find(delegate(vEstablecimiento e) { return e.id == almid; });

                if (est == null)
                {
                    est = new vEstablecimiento();
                    est.id = almid;
                    est.total = 0;
                    establecimientos.Add(est);                    
                }
                est.total += item.subimpuesto.Value + item.seguro.Value + item.subtotal.Value + item.transporte.Value - item.desc0.Value;
                
            }

            //CARGA LOS VALORES DE RETENCIONES
            foreach (vRetencionVenta item in retencionesventas)
            {
                vVenta venta = ventasres.Find(delegate(vVenta v) { return v.ruc == item.per_ciruc; });
                if (venta == null)
                {
                    venta = ventasres.Find(delegate(vVenta v) { return v.ruc == "9999999999999"; });
                  
                }

                if (venta == null)
                {
                    strmensajes.AppendFormat("Se han generado retenciones a ventas que no se encuentran en el mes para el  Cliente: {0} RUC:{1}<br>", item.per_razon, item.per_ciruc);
                    //throw new ArgumentException("Se han generado retenciones a ventas que no se encuentran en el mes para el  Cliente: " + item.per_razon + " RUC: " + item.per_ciruc);
                }
                else
                {
                    venta.retfue += item.ret;
                    venta.retiva += item.iva;
                }
            }
            

        }

        public static void SetTablaNotasCC()
        {
            notasccres = new List<vVenta>();
            
            foreach (vVenta item in notascc)
            {
                item.ruc = (Functions.Validaciones.valida_cedularuc(item.ruc)) ? item.ruc : "9999999999999";
                vVenta venta = notasccres.Find(delegate (vVenta v) { return v.ruc == item.ruc; });
                if (venta == null)
                {
                    venta = new vVenta();
                    venta.ruc = item.ruc;
                    //venta.tipoid = (item.tipoid.Substring(0, 1).ToUpper() == "C") ? "05" : (item.tipoid.Substring(0, 1).ToUpper() == "R" ? "04" : "");
                    venta.tipodoc = item.tipodoc;
                    venta.tipoid = (item.ruc == "9999999999999") ? "07" : (item.ruc.Length == 13 ? "04" : "05");// (item.tipoid.Substring(0, 1).ToUpper() == "R") ? "04" : "05";
                    venta.tipoats = item.tipoats;
                    venta.parterel = (venta.tipoid == "04" || venta.tipoid == "05") ? "NO" : null;
                    venta.comprobantes = 0;
                    venta.subtotal = 0;
                    venta.subimpuesto = 0;
                    venta.transporte = 0;
                    venta.seguro = 0;
                    venta.impuesto = 0;
                    venta.total = 0;
                    venta.retfue = 0;
                    venta.retiva = 0;
                    venta.monto = 0;
                    notasccres.Add(venta);
                }

                if (!item.seguro.HasValue)
                    item.seguro = 0;

                venta.comprobantes = venta.comprobantes + 1;
                venta.subtotal += item.subtotal + item.transporte;
                venta.subimpuesto += item.subimpuesto + item.seguro;


                //venta.transporte += item.transporte;
                // venta.seguro += item.seguro;


                decimal ivareal = (item.subimpuesto.Value + item.seguro.Value) * valoriva;

                venta.impuesto += ivareal;// item.impuesto;

                venta.total += item.total;
                //venta.retfue+= item.retfue;                
                //venta.retiva += item.retiva;
                venta.monto += item.monto;

                string almid = "";
                string pveid = "";
                string[] arraydoctran = item.doctran.Split('-');
                if (arraydoctran.Length > 3)
                {
                    almid = arraydoctran[1];
                    pveid = arraydoctran[2];
                }


                vEstablecimiento est = establecimientos.Find(delegate (vEstablecimiento e) { return e.id == almid; });

                if (est == null)
                {
                    est = new vEstablecimiento();
                    est.id = almid;
                    est.total = 0;
                    establecimientos.Add(est);
                }
                est.total -= (item.subimpuesto.Value + item.seguro.Value + item.subtotal.Value + item.transporte.Value);

            }

            //CARGA LOS VALORES DE RETENCIONES
            //foreach (vRetencionVenta item in retencionesventas)
            //{
            //    vVenta venta = ventasres.Find(delegate (vVenta v) { return v.ruc == item.per_ciruc; });
            //    if (venta == null)
            //    {
            //        venta = ventasres.Find(delegate (vVenta v) { return v.ruc == "9999999999999"; });
            //    }
            //    venta.retfue += item.ret;
            //    venta.retiva += item.iva;
            //}


        }

        public static void GetVentas(XmlNode nodoventas)
        {


            string path = HttpContext.Current.Server.MapPath("xml");
            XmlDocument xmlventas = new XmlDocument();
            xmlventas.Load(path + "\\ventas.xml");
            foreach (vVenta item in ventasres)
            {
                foreach (XmlNode nod in xmlventas.ChildNodes)
                {
                    CreateXmlNode(nodoventas, nod, null, item);
                }

            }
            foreach (vVenta item in notasccres)
            {
                foreach (XmlNode nod in xmlventas.ChildNodes)
                {
                    CreateXmlNode(nodoventas, nod, null, item);
                }
            }




        }

        public static void GetVentasEstablecimientos(XmlNode nodoventas)
        {


            string path = HttpContext.Current.Server.MapPath("xml");
            XmlDocument xmlventas = new XmlDocument();
            xmlventas.Load(path + "\\ventasest.xml");
            foreach (vEstablecimiento item in establecimientos)
            {
                foreach (XmlNode nod in xmlventas.ChildNodes)
                {
                    CreateXmlNode(nodoventas, nod, null, item);
                }

            }

        }

        public static bool Update332AIR(XmlNode nodoair, decimal? monto)
        {            
            foreach (XmlNode nod in nodoair.ChildNodes)
            {
                XmlNode codigo = nod.SelectSingleNode("codRetAir");
                XmlNode baseImpAir = nod.SelectSingleNode("baseImpAir");
                if (codigo.InnerText == "332")
                {
                    baseImpAir.InnerText = GetValueByType(monto);
                    return true;
                }

            }
            return false;
        }



        public static void GetAIR(XmlNode nodoair, Int64? codigo, vCompras compra)
        {
            string path = HttpContext.Current.Server.MapPath("xml");
            XmlDocument xmlair = new XmlDocument();
            xmlair.Load(path + "\\air.xml");
            if (codigo.HasValue)
            {
                List<vRetencion> lstair = retenciones.FindAll(delegate(vRetencion r) { return r.codigofac == codigo; });


                decimal baseimp = 0;
                foreach (vRetencion item in lstair)
                {
                    baseimp += item.baseimp.HasValue? item.baseimp.Value:0;
                    foreach (XmlNode nod in xmlair.ChildNodes)
                    {
                        CreateXmlNode(nodoair, nod, item.codigoret, item);
                    }
                }
                if ((compra.subtotal + compra.subtotal0) > baseimp)
                {
                    if (!Update332AIR(nodoair, (compra.subtotal + compra.subtotal0)))
                    {
                        vRetencion vret = new vRetencion();
                        vret.conceptoid = "332";
                        vret.baseimp = compra.subtotal + compra.subtotal0 - baseimp;
                        vret.porcentaje = 0;
                        vret.valor = 0;
                        foreach (XmlNode nod in xmlair.ChildNodes)
                        {
                            CreateXmlNode(nodoair, nod, null, vret);
                        }
                    }
                }
            }
            else
            {
                if (compra.tipodoc != 24)//NO hace retencion para notas de credito
                {
                    vRetencion vret = new vRetencion();
                    vret.conceptoid = "332";
                    vret.baseimp = compra.subtotal + compra.subtotal0;
                    vret.porcentaje = 0;
                    vret.valor = 0;
                    foreach (XmlNode nod in xmlair.ChildNodes)
                    {
                        CreateXmlNode(nodoair, nod, null, vret);
                    }
                }

            }



        }

        public static string GetTipoComprobanteAnulado(int tipo)
        {
            if (tipo == tipofac)
                return "01";
            if (tipo == tiporet)
                return "07";
            if (tipo == tiponc)
                return "04";
            if (tipo == tipond)
                return "05";
            if (tipo == tipofac)
                return "01";
            return "";
        }

        public static void GetAnulados(XmlNode nodoanulados)
        {
            DataTable dtanulados = new DataTable();
            dtanulados.Columns.Add(new DataColumn("tipo",typeof(string)));
            dtanulados.Columns.Add(new DataColumn("almacen", typeof(string)));
            dtanulados.Columns.Add(new DataColumn("pventa", typeof(string)));
            dtanulados.Columns.Add(new DataColumn("desde", typeof(string)));
            dtanulados.Columns.Add(new DataColumn("hasta", typeof(string)));
            dtanulados.Columns.Add(new DataColumn("autorizacion", typeof(string)));

            DataRow row = dtanulados.NewRow();
            bool nuevo = true;
            foreach (vComprobante item in anulados)
            {
                string tipo= GetTipoComprobanteAnulado(item.tipodoc.Value);
                string[] arraynumero = item.doctran.Split('-');
                if (arraynumero.Length>3)
                {
                    //if (!string.IsNullOrEmpty(row["almacen"].ToString()))
                    if (!nuevo)
                    {
                        int desde = int.Parse(row["desde"].ToString());
                        int hasta = int.Parse(row["hasta"].ToString());
                        int numero = int.Parse(arraynumero[3]);
                        if (row["almacen"].ToString() == arraynumero[1] && row["pventa"].ToString() == arraynumero[2] && row["tipo"].ToString() == tipo && numero == (hasta + 1)) //&& row["autorizacion"]== item.
                        {
                            row["hasta"] = arraynumero[3];
                            nuevo = false;
                        }
                        else
                        {
                            dtanulados.Rows.Add(row);
                            nuevo = true;
                        }
                    }
                    if (nuevo)
                    {
                        row = dtanulados.NewRow();
                        row["tipo"] = tipo;
                        row["almacen"] = arraynumero[1];
                        row["pventa"] = arraynumero[2];
                        row["desde"] = arraynumero[3];
                        row["hasta"] = arraynumero[3];
                        row["autorizacion"] = "";//FALTA AUTORIZACION
                        nuevo = false;
                    }

                    

                }
            }



            
            string path = HttpContext.Current.Server.MapPath("xml");
            XmlDocument xmlanulados = new XmlDocument();
            xmlanulados.Load(path + "\\anulados.xml");
            foreach (DataRow r in dtanulados.Rows)
            {
                foreach (XmlNode nod in xmlanulados.ChildNodes)
                {
                    CreateXmlNode(nodoanulados, nod, null, r);
                }

            }




        }


        public static bool GetFormasPago(XmlNode nodoventas, object datasource)
        {


            string path = HttpContext.Current.Server.MapPath("xml");
            XmlDocument xmlventas = new XmlDocument();
            xmlventas.Load(path + "\\formaspago.xml");

            if (datasource.GetType() == typeof(vCompras))
            {
                vCompras compra = (vCompras)datasource;
                if ((compra.subtotal0.Value + compra.subtotal.Value + compra.impuesto) >= 1000)
                {
                    foreach (XmlNode nod in xmlventas.ChildNodes)
                    {
                        CreateXmlNode(nodoventas, nod, null, datasource);
                    }

                    return true;
                }
                else
                    return false;
            }
            else if (datasource.GetType() == typeof(vVenta))
            {
                vVenta venta = (vVenta)datasource;
                if (venta.tipodoc == tipofac)
                {
                    foreach (XmlNode nod in xmlventas.ChildNodes)
                    {
                        CreateXmlNode(nodoventas, nod, null, datasource);
                    }
                    return true;
                }
                else
                    return false;
            }
            else
                return false;


        }




        public static XmlDocument GenerarATS(int periodo, int mes, out string mensajes)
        {
            SRI.periodo= periodo;
            SRI.mes = mes;

            DateTime fecha = new DateTime(periodo, mes, 1);
            SRI.valoriva = Constantes.GetValorIVA(fecha) / 100;

            LoadData();

            xmldoc = new XmlDocument(); 

            string path = HttpContext.Current.Server.MapPath("xml");
          

            XmlDocument xmltemp = new XmlDocument();
            xmltemp.Load(path+"\\ATSTemplate.xml");


            foreach (XmlNode nod in xmltemp.ChildNodes)
            {
                CreateXmlNode(null, nod,null, empresa);
            }


            mensajes = strmensajes.ToString();                     

            return xmldoc;

        }



    }
}

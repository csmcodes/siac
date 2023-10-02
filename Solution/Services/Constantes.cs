using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogicLayer;
using BusinessObjects;
using Functions;
using System.Web.Script.Serialization;

namespace Services
{
    public static class Constantes
    {

        #region Propiedades

        #region Parametros

        public static string cValorIVA
        {
            get
            {
                return GetParameter("valoriva");
                
            }
        }

        public static string cListaPrecio {
            get
            {
                return GetParameter("listaprecio");            
            }            

        }

        public static string cPolitica {
            get
            {
                return GetParameter("politica");            
            }            

        }

        public static string cCatcliente
        {
            get
            {
                return GetParameter("catcliente");
            }

        }
        public static string cPoliticaProv
        {
            get
            {
                return GetParameter("politica_prov");
            }

        }

        public static string cImpRteFte
        {
            get
            {
                return GetParameter("impretfte");
            }

        }
        public static string cImpRteIVA
        {
            get
            {
                return GetParameter("impretiva");
            }

        }


        public static string cCatProv
        {
            get
            {
                return GetParameter("cat_prove");
            }

        }

        public static string cVendedor {
            get
            {
                return GetParameter("vendedor");
            }
        }

        public static  int cTipoCliente {
            get
            {
                return (int)Conversiones.GetValueByType(GetParameter("tipocliente"), typeof(int));                
            }
        }

        public static string cPorcSeguro
        {
            get
            {
                return GetParameter("porcseguro");
            }
        }


        public static string cSinCentro
        {
            get
            {
                return GetParameter("sincentro");
            }

        }

        public static string cProductoPlanilla
        {
            get
            {
                return GetParameter("productoplanilla");
            }

        }


        public static List<PoliticaTipoPago> cPoliticaTipoPago {
            get
            {
                List<PoliticaTipoPago> lst = new List<PoliticaTipoPago>();  
                string parametro = GetParameter("politicatipopago");
                if (!string.IsNullOrEmpty(parametro))
                {
                    var serializer = new JavaScriptSerializer();
                    lst = serializer.Deserialize<List<PoliticaTipoPago>>(parametro); 
                }
                return lst;
            }
        }





        #region Parametros Cuentas Contables

        public static int cCuentaTransporte
        {
            get
            {
                return (int)Conversiones.GetValueByType(GetParameter("ctatransporte"), typeof(int));
            }
        }

        public static int cCuentaSeguro
        {
            get
            {
                return (int)Conversiones.GetValueByType(GetParameter("ctaseguro"), typeof(int));
            }
        }

        public static int cCuentaICE
        {
            get
            {
                return (int)Conversiones.GetValueByType(GetParameter("ctaice"), typeof(int));
            }
        }


        public static int? cCuentaDescuentoIva
        {
            get
            {
                string cta = GetParameter("ctadescuentoiva");
                if (!string.IsNullOrEmpty(cta))
                    return (int)Conversiones.GetValueByType(cta, typeof(int));
                else
                    return null;
            }
        }
        public static int? cCuentaDescuento0
        {
            get
            {
                string cta = GetParameter("ctadescuento0");
                if (!string.IsNullOrEmpty(cta))
                    return (int)Conversiones.GetValueByType(cta, typeof(int));
                else
                    return null;
            }
        }

        #endregion


        #endregion

        #region Contables
        /// <summary>
        /// 1
        /// </summary>
        public static int cDebito
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// 2
        /// </summary>
        public static int cCredito
        {
            get
            {
                return 2;
            }
        }

        #endregion

        #region Autorizaciones

        public static int cCantidadFacturas {
            get
            {
                return 100;
            }
        }

        public static int cDiasAutorizacion
        {
            get
            {
                return 45;
            }
        }

        #endregion

        #region Estados Comprobantes

        /// <summary>
        /// Valor = 0
        /// </summary>
        public static int cEstadoProceso
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Valor = 1
        /// </summary>
        public static int cEstadoGrabado
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Valor =2
        /// </summary>
        public static int cEstadoMayorizado
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Valor = 3
        /// </summary>
        public static int cEstadoPorAutorizar
        {
            get
            {
                return 3;
            }
        }


        /// <summary>
        /// Valor = 9
        /// </summary>
        public static int cEstadoEliminado
        {
            get
            {
                return 9;
            }
        }

        #endregion

        #region Perfiles        
        public static string cPerfilRoot
        {
            get
            {
                return "root";
            }
        }

        public static string cPerfilAdministrador
        {
            get
            {
                return "admin";
            }
        }
        public static string cPerfilAsistente
        {
            get
            {
                return "assist";
            }
        }
        public static string cPerfilUsuario
        {
            get
            {
                return "user";
            }
        }

        public static string cPerfilAuditoria
        {
            get
            {
                return "audit";
            }
        }

        #endregion

        #region Tipos Persona

        public static int cCliente
        {
            get
            {
                return 4;
            }
        }

        public static int cProveedor
        {
            get
            {
                return 5;
            }
        }

        public static int cSocio
        {
            get
            {
                return 6;
            }
        }

        public static int cChofer
        {
            get
            {
                return 8;
            }
        }

        public static int cAyudante
        {
            get
            {
                return 7;
            }
        }
      



        #endregion

        #region Tipos Modulos

        public static Modulo  cInicial
        {
            get
            {
                return GetModulo("INI");
            }
        }
        public static Modulo cControl
        {
            get
            {
                return GetModulo("CTL");
            }
        }
        public static Modulo cContabilidad
        {
            get
            {
                return GetModulo("CNT");
            }
        }
        public static Modulo cBancos
        {
            get
            {
                return GetModulo("BAN");
            }
        }
        public static Modulo cCuentasxCobrar
        {
            get
            {
                return GetModulo("CXC");
            }
        }
        public static Modulo cCuentasxPagar
        {
            get
            {
                return GetModulo("CXP");
            }
        }
        public static Modulo cInventario
        {
            get
            {
                return GetModulo("INV");
            }
        }
        public static Modulo cFacturacion
        {
            get
            {
                return GetModulo("FAC");
            }
        }
        public static Modulo cCompras
        {
            get
            {
                return GetModulo("COM");
            }
        }

        public static Modulo cActivos
        {
            get
            {
                return GetModulo("ACT");
            }
        }
        public static Modulo cNomina
        {
            get
            {
                return GetModulo("NOM");
            }
        }
        public static Modulo cProduccion
        {
            get
            {
                return GetModulo("PRO");
            }
        }
        public static Modulo cExterno
        {
            get
            {
                return GetModulo("EXT");
            }
        }
                                
        public static Modulo cAccionistas
        {
            get
            {
                return GetModulo("ACC");
            }
        }

        public static Modulo cGastos
        {
            get
            {
                return GetModulo("GAS");
            }
        }        
            public static Modulo cImportacion
        {
            get
            {
                return GetModulo("IMP");
            }
         }                       
               
        public static Modulo cSistemas
        {
            get
            {
                return GetModulo("SIS");
            }
        }
               
        public static Modulo cExportacion
        {
            get
            {
                return GetModulo("EXP");
            }
        }
                
        public static Modulo cSinModulo
        {
            get
            {
                return GetModulo("SIN");
            }
        }
                
   

        #endregion



        #region Tipos Documentos

        public static Tipodoc cRecibo
        {
            get
            {
                return GetTipoDocumento("RECCLI");
            }
        }

        public static Tipodoc cFactura
        {
            get
            {
                return GetTipoDocumento("FACCLI");
            }
        }
        public static Tipodoc cGuia
        {
            get
            {
                return GetTipoDocumento("GUICLI");
            }
        }

        public static Tipodoc cHojaRuta
        {
            get
            {
                return GetTipoDocumento("HOJRUT");
            }
        }
        public static Tipodoc cPlanillaClientes
        {
            get
            {
                return GetTipoDocumento("PLACLI");
            }
        }
        public static Tipodoc cPlanillaSocios
        {
            get
            {
                return GetTipoDocumento("PLASOC");
            }
        }

        public static Tipodoc cObligacion
        {
            get
            {
                return GetTipoDocumento("OBLPRO");
            }
        }
        public static Tipodoc cPago
        {
            get
            {
                return GetTipoDocumento("PAGPRO");
            }
        }

        public static Tipodoc cRetencion
        {
            get
            {
                return GetTipoDocumento("RETEN");
            }
        }

        public static Tipodoc cNotacre
        {
            get
            {
                return GetTipoDocumento("NOTACRE");
            }
        }
        public static Tipodoc cNotadeb
        {
            get
            {
                return GetTipoDocumento("NOTDEB");
            }
        }

        public static Tipodoc cNotacrePro
        {
            get
            {
                return GetTipoDocumento("NOTACRPROV");
            }
        }
        public static Tipodoc cNotadebPro
        {
            get
            {
                return GetTipoDocumento("NOTADEBPRO");
            }
        }



        public static Tipodoc cDiario
        {
            get
            {
                return GetTipoDocumento("DIACON");
            }
        }

        public static Tipodoc cDeposito
        {
            get
            {
                return GetTipoDocumento("DPB");
            }
        }
        public static Tipodoc cNotaDebitoBan
        {
            get
            {
                return GetTipoDocumento("NDBA");
            }
        }
      
        public static Tipodoc cNotaCreditoBan
        {
            get
            {
                return GetTipoDocumento("NCBA");
            }
        }
        public static Tipodoc cPagoBan
        {
            get
            {
                return GetTipoDocumento("PAGBAN");
            }
        }

        public static Tipodoc cLiquidacionCompra
        {
            get
            {
                return GetTipoDocumento("LIQCOM");
            }
        }

        public static Tipodoc cPagoSocio
        {
            get
            {
                return GetTipoDocumento("PAGSOC");
            }
        }


        public static Tipodoc cTransferenciaDirecta
        {
            get
            {
                return GetTipoDocumento("ETRDIR");
            }
        }

        public static Tipodoc cEgresoTransferencia
        {
            get
            {
                return GetTipoDocumento("EGRTRA");
            }
        }

        public static Tipodoc cIngresoTransferencia
        {
            get
            {
                return GetTipoDocumento("INGTRA");
            }
        }


        public static Tipodoc cNotaEntregaBodega
        {
            get
            {
                return GetTipoDocumento("NOTENT");
            }
        }

        public static Tipodoc cDevolucionCliente
        {
            get
            {
                return GetTipoDocumento("DEVCLI");
            }
        }

        public static Tipodoc cIngresoCantidades
        {
            get
            {
                return GetTipoDocumento("INGCAN");
            }
        }




        #endregion

        #region Tipos Comprobantes (ctipocom)

        public static Ctipocom cComRetencion
        {
            get
            {
                return GetTipoComprobante("RET");
            }
        }

        public static Ctipocom cComRecibo
        {
            get
            {
                return GetTipoComprobante("REC");
            }
        }

        public static Ctipocom cComDiario
        {
            get
            {
                return GetTipoComprobante("DG");
            }
        }


        #endregion

        #region Tipos ATS


        public static string cFAC_ATS
        {
            get
            {
                return "370"; //370: operadores de transporte desde marzo 2018 | 18: para otros tipos de empresa
            }
        }

        public static string cNC_ATS
        {
            get
            {
                return "372"; //372: operadores de transporte desde marzo 2018 | 04: para otros tipos de empresa
            }
        }



        #endregion




        #endregion

        #region Metodos

        public static string GetParameter(string id)
        {
            Parametro par = ParametroBLL.GetByPK(new Parametro { par_nombre = id, par_nombre_key = id });
            return par.par_valor; 
        }

        public static Listaprecio GetListaPrecio()
        {
            return ListaprecioBLL.GetById(new Listaprecio { lpr_id = cListaPrecio });
        }

        public static Politica GetPoliticacli()
        {
            return PoliticaBLL.GetById(new Politica{ pol_id = cPolitica });
        }

        public static Catcliente GetCatcliente()
        {
            return CatclienteBLL.GetById(new Catcliente { cat_id = cCatcliente });
        }


        public static Politica GetPoliticaProv()
        {
            return PoliticaBLL.GetById(new Politica { pol_id = cPoliticaProv });
        }

        public static Catcliente GetCatProv()
        {
            return CatclienteBLL.GetById(new Catcliente { cat_id = cCatProv });
        }

        public static Impuesto GetImpRteFte()
        {
            return ImpuestoBLL.GetById(new Impuesto { imp_id = cImpRteFte });
        }
        public static Impuesto GetImpRteIVA()
        {
            return ImpuestoBLL.GetById(new Impuesto { imp_id = cImpRteIVA });
        }



        public static Tipodoc GetTipoDocumento(string id)
        {
            return TipodocBLL.GetById(new Tipodoc{tpd_id = id });
        }


        public static Modulo GetModulo(string id)
        {
            return ModuloBLL.GetById(new Modulo { mod_id = id });
        }

        public static Tipodoc GetCatalogo(string id)
        {
            return TipodocBLL.GetById(new Tipodoc { tpd_id = id });
        }

        public static Ctipocom GetTipoComprobante(string id)
        {
            return CtipocomBLL.GetById(new Ctipocom { cti_id = id });
        }

        public static Centro GetSinCentro()
        {
            return CentroBLL.GetById(new Centro { cen_id = cSinCentro });
        }

        public static Producto GetProductoPlanilla()
        {
            return ProductoBLL.GetById(new Producto{ pro_id = cProductoPlanilla});
        }

        public static string GetEstadoName(int estado)
        {
            if (estado == cEstadoEliminado)
                return "ANULADO";
            if (estado == cEstadoGrabado)
                return "GRABADO";
            if (estado == cEstadoMayorizado)
                return "MAYORIZADO";
            if (estado == cEstadoPorAutorizar)
                return "POR AUTORIZAR";
            if (estado == cEstadoProceso)
                return "PROCESO";
            return "";


        }



        public static WhereParams GetConstanteClause(string usuario, int? empresa, int? almacen, int? pventa , string nombre)
        {
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            if (!string.IsNullOrEmpty(usuario))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_usuario = {" + contador + "} ";
                valores.Add(usuario);
                contador++;
            }
            else
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_usuario is null ";
                

            if (empresa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_empresa = {" + contador + "} ";
                valores.Add(empresa.Value);
                contador++;

            }
            else
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_empresa is null ";

            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_almacen = {" + contador + "} ";
                valores.Add(almacen.Value);
                contador++;
            }
            else
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_almacen is null ";

            if (pventa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_pventa = {" + contador + "} ";
                valores.Add(pventa.Value);
                contador++;
            }
            else
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_pventa is null ";
         
            if (!string.IsNullOrEmpty(nombre))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " con_nombre= {" + contador + "} ";
                valores.Add(nombre);
                contador++;
            }

            parametros.valores = valores.ToArray();
            return parametros;
        }
        
        public static string GetConstanteValor(string usuario, int? empresa, int? almacen, int? pventa, string nombre)
        {

            string valor = "";
            bool fin = false;
            bool usr = true;
            bool emp = true;
            bool alm = true;
            bool pve = true;            
            
            do
            {

                WhereParams parametros = GetConstanteClause(usr ? usuario : null, emp ? empresa : null, alm ? almacen : null, pve ? pventa : null,  nombre);
                List<Constante> lst = ConstanteBLL.GetAll(parametros, "");
                if (lst.Count > 0)
                {
                    valor = lst[0].con_valor;
                    fin = true;
                }
                if (!fin)
                {
                    if (usr)
                        usr = false;                    
                    else if (!usr && pve)
                        pve = false;
                    else if (!usr && !pve && alm)
                        alm = false;
                    else if (!usr && !pve && !alm && emp)
                        emp = false;
                    else if (!usr && !pve && !alm && !emp)
                        fin = true;
                }
            } while (!fin);
            return valor;


        }

        public static string GetConstanteValorFix(string usuario, int? empresa, int? almacen, int? pventa, string nombre)
        {

            string valor = "";


            WhereParams parametros = GetConstanteClause(usuario, empresa, almacen, pventa, nombre);
            List<Constante> lst = ConstanteBLL.GetAll(parametros, "");
            if (lst.Count > 0)
            {
                valor = lst[0].con_valor;
            }

            return valor;


        }

        /*public static string GetConstanteValor(string usuario, int? empresa, int? almacen, int? pventa, string nombre)
        {

            string valor = "";
            bool fin = false;
            bool usr = false;
            bool emp = true;
            bool alm = false;
            bool pve = false;

            do
            {

                WhereParams parametros = GetConstanteClause(usr ? usuario : null, emp ? empresa : null, alm ? almacen : null, pve ? pventa : null, nombre);
                List<Constante> lst = ConstanteBLL.GetAll(parametros, "");
                if (lst.Count > 0)
                {
                    valor = lst[0].con_valor;
                }
                else
                    fin = true;
                if (!fin)
                {
                    if (emp && !alm)
                        alm = true;                    
                    else if (emp && alm && !pve)
                        pve = true;
                    else if (emp && alm && pve && !usr)
                        usr = true;
                    else if (emp && alm && pve && usr)
                        fin = true;                    
                }
            } while (!fin);
            return valor;


        }*/


        
        public static string GetPrinter(string usuario, int? empresa, int? almacen, int? pventa, int? tipodoc)
        {

            string printer = "";
            string valor = GetConstanteValor(usuario, empresa, almacen, pventa, "PRINTER");

            if (valor != "")
            {
                string[] arrayprintersdoc = valor.Split('|');
                for (int i = 0; i < arrayprintersdoc.Length; i++)
                {
                    string[] arrayprinter = arrayprintersdoc[i].Split(',');
                    if (arrayprinter.Length > 1)
                    {
                        if (tipodoc.Value.ToString() == arrayprinter[0])
                        {
                            printer = arrayprinter[1];
                            break;
                        }

                    }
                }
            }
            if (printer == "")
                printer = valor;
            return printer;


           


        }

        public static string GetPrintVersion(string usuario, int? empresa, int? almacen, int? pventa, int? tipodoc)
        {
            return GetConstanteValor(usuario, empresa, almacen, pventa, "PRINTVERSION");            
        }

        public static string GetPrintHTML(string usuario, int? empresa, int? almacen, int? pventa)
        {
            return GetConstanteValor(usuario, empresa, almacen, pventa, "PRINTHTML");
        }

        public static string GetPrintFormat(string usuario, int? empresa, int? almacen, int? pventa)
        {
            return GetConstanteValor(usuario, empresa, almacen, pventa, "PRINTFORMAT");
        }


        public static string GetPoliticas(string usuario, int? empresa, int? almacen, int? pventa, int? tipodoc)
        {

            string politicas = "";
            string valor = GetConstanteValorFix(usuario, empresa, almacen, pventa, "POLITICAS");
            bool varios = false;
            if (valor != "")
            {                
                string[] arraypoliticasdoc = valor.Split('|');
                for (int i = 0; i < arraypoliticasdoc.Length; i++)
                {                    
                    string[] arraypoliticas = arraypoliticasdoc[i].Split(',');
                    if (arraypoliticas.Length > 1)
                    {
                        varios = true;
                        if (tipodoc.Value.ToString() == arraypoliticas[0])
                        {                            
                            politicas = arraypoliticas[1];
                            break;
                        }

                    }
                }
            }
            if (politicas == "")
                politicas = (!varios) ? valor : "";
            return politicas;





        }

        public static string GetRutas(string usuario, int? empresa, int? almacen, int? pventa)
        {
            return GetConstanteValor(usuario, empresa, almacen, pventa, "RUTAS");

        }


        public static decimal GetValorIVA(DateTime? fecha)
        {
            if (!fecha.HasValue)
                fecha = DateTime.Now;
            if (fecha.Value == DateTime.MinValue)
                fecha = DateTime.Now;


            var serializer = new JavaScriptSerializer();
            List<ValorIVA> lst = serializer.Deserialize<List<ValorIVA>>(cValorIVA);

            foreach (ValorIVA item in lst)
            {
                DateTime desde = DateTime.Parse(item.desde);
                DateTime hasta = DateTime.Parse(item.hasta);

                //if (DateTime.Compare(desde, fecha.Value) >= 0 && DateTime.Compare(fecha.Value, hasta) <= 0)

                if (fecha>=desde && fecha<=hasta)
                    return item.valor;
            }
            return 12;
            
        }

        #endregion
    }
}

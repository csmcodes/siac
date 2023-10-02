using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using Functions;
using System.Data;
using System.Web.Script.Serialization;

namespace Packages
{
    public class FAC
    {
        public FAC()
        {

        }


        public static Comprobante create_factura(Comprobante comp)
        {


            //DateTime fecha = DateTime.Now;


              Tipodoc tipo = TipodocBLL.GetByPK(new Tipodoc() { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });


            comp.com_concepto = tipo.tpd_nombre;
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            comp.com_tclipro = Constantes.cCliente;
            comp.com_periodo = comp.com_fecha.Year;
            comp.com_mes = comp.com_fecha.Month;
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC


                comp.ccomenv.cenv_empresa = comp.com_empresa;
                comp.ccomenv.cenv_comprobante = comp.com_codigo;
                CcomenvBLL.Insert(transaction, comp.ccomenv); //INSERT CCOMDOC


                // au.ape_retdato

                //    List<Autpersona> autorizaciones = Dictionaries.GetAutorizacionesPersona(comp.com_empresa, comp.com_codclipro.Value);

                /*List<Autpersona> autorizaciones = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_estado=1", comp.com_empresa, comp.com_codclipro.Value), "ape_val_fecha desc");
                Autpersona au = autorizaciones.Find(delegate(Autpersona d) { return d.ape_nro_autoriza == comp.ccomdoc.cdoc_acl_nroautoriza; });
                string aux = comp.ccomdoc.cdoc_aut_factura.Substring(comp.ccomdoc.cdoc_aut_factura.LastIndexOf("-") + 1);
                int num = (Int32)Conversiones.GetValueByType(aux, typeof(Int32));
                int desde = (Int32)Conversiones.GetValueByType(au.ape_fac3, typeof(Int32));
                int hasta = (Int32)Conversiones.GetValueByType(au.ape_fact3, typeof(Int32));
                if ((desde > num) || (hasta < num))
                    throw new Exception("El numero no se encuentra en la autorización " + desde + " a " + hasta);*/


                int contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    item.ddco_udigitada = item.ddco_udigitada == 0 ? 1 : item.ddco_udigitada;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }

                comp.total.tot_empresa = comp.com_empresa;
                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL

                


                transaction.Commit();
            }
            catch (Exception ex)
            {
                comp.com_codigo = -1;
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante update_factura(Comprobante comp, Rutaxfactura rfac)
        {
            Comprobante hr = new Comprobante();
            if (rfac.rfac_comprobanteruta > 0)
            {
                hr.com_empresa = comp.com_empresa;
                hr.com_empresa_key = comp.com_empresa;
                hr.com_codigo = rfac.rfac_comprobanteruta;
                hr.com_codigo_key = rfac.rfac_comprobanteruta;
                hr = ComprobanteBLL.GetByPK(hr);
                hr.total = new Total();
                hr.total.tot_empresa = hr.com_empresa;
                hr.total.tot_empresa_key = hr.com_empresa;
                hr.total.tot_comprobante = hr.com_codigo;
                hr.total.tot_comprobante_key = hr.com_codigo;
                hr.total = TotalBLL.GetByPK(hr.total);
            }


            DateTime fecha = DateTime.Now;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(comp);

            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_periodo = comp.com_fecha.Year;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.mod_usr = comp.mod_usr;
            objU.mod_fecha = comp.mod_fecha;

            Dtipocom dti = new Dtipocom();
            if (string.IsNullOrEmpty(objU.com_doctran))
            {

                
                objU.com_estado = Constantes.cEstadoGrabado;
                objU.com_concepto = !string.IsNullOrEmpty(comp.com_concepto) ? comp.com_concepto : objU.com_concepto;
                objU.com_modulo = General.GetModulo(comp.com_tipodoc); ;
                objU.com_transacc = General.GetTransacc(comp.com_tipodoc);
                objU.com_centro = Constantes.GetSinCentro().cen_codigo;
                objU.com_descuadre = 0;
                objU.com_adestino = 0;
                objU.com_almacen = comp.com_almacen;
                objU.com_pventa = comp.com_pventa;

                objU.com_doctran = comp.com_doctran;
                objU.com_numero = comp.com_numero;
                objU.com_tclipro = Constantes.cCliente;
                dti = General.GetDtipocom(objU.com_empresa, objU.com_fecha.Year, objU.com_ctipocom, objU.com_almacen.Value, objU.com_pventa.Value);
                dti.dti_numero = dti.dti_numero + 1;

                if (objU.com_numero < dti.dti_numero)
                {
                    objU.com_numero = dti.dti_numero.Value;
                    objU.com_doctran = General.GetNumeroComprobante(objU);
                }

            }

          



            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                objU.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
                objU.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
                objU.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
                objU.ccomenv =CcomenvBLL.GetByPK(new Ccomenv { cenv_empresa = comp.com_empresa, cenv_empresa_key = comp.com_empresa, cenv_comprobante = comp.com_codigo, cenv_comprobante_key = comp.com_codigo });
                objU.planillacomp = PlanillacomprobanteBLL.GetByPK(new Planillacomprobante { pco_empresa = comp.com_empresa, pco_empresa_key = comp.com_empresa, pco_comprobante_fac = comp.com_codigo, pco_comprobante_fac_key = comp.com_codigo });


                /*Elimina el comprobante de la hoja de ruta y resta el valor*/
                List<Rutaxfactura> lsthojas = RutaxfacturaBLL.GetAll(new WhereParams("rfac_empresa ={0} and rfac_comprobantefac={1}", comp.com_empresa, comp.com_codigo), "");                
                if (lsthojas.Count > 0)
                {
                    hr.total.tot_total -= comp.total.tot_total;
                }

                /****/
            



                int contador = 0;
                foreach (Dcomdoc item in objU.ccomdoc.detalle)
                {
                    item.ddoc_empresa_key = item.ddoc_empresa;
                    item.ddoc_comprobante_key = item.ddoc_comprobante;
                    item.ddoc_secuencia_key = item.ddoc_secuencia;
                    if (item.detallecalculo!=null)
                    //LOOP INSER CADA DCALCULOPRECIO DE CADA DCOMDOC
                    foreach (Dcalculoprecio dc in item.detallecalculo)
                    {
                        dc.dcpr_empresa_key = dc.dcpr_empresa;
                        dc.dcpr_comprobante_key = dc.dcpr_comprobante;
                        dc.dcpr_dcomdoc_key = dc.dcpr_dcomdoc;
                        dc.dcpr_secuencia_key = dc.dcpr_secuencia;
                        DcalculoprecioBLL.Delete(transaction, dc);
                    }
                    DcomdocBLL.Delete(transaction, item);
                    contador++;
                }

                objU.ccomdoc.cdoc_comprobante_key = objU.ccomdoc.cdoc_comprobante;
                objU.ccomdoc.cdoc_empresa_key = objU.ccomdoc.cdoc_empresa;
                CcomdocBLL.Delete(transaction, objU.ccomdoc);

                objU.total.tot_comprobante_key = objU.total.tot_comprobante_key;
                objU.total.tot_empresa_key = objU.total.tot_empresa_key;
                TotalBLL.Delete(transaction, objU.total);

                objU.ccomenv.cenv_comprobante_key = objU.ccomenv.cenv_comprobante;
                objU.ccomenv.cenv_empresa_key = objU.ccomenv.cenv_empresa;
                CcomenvBLL.Delete(transaction, objU.ccomenv);

                



                ComprobanteBLL.Update(transaction, objU);// INSERT COMPROBANTE
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC

                contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);

                    //LOOP INSER CADA DCALCULOPRECIO DE CADA DCOMDOC
                    foreach (Dcalculoprecio dc in item.detallecalculo)
                    {
                        dc.dcpr_dcomdoc = contador;
                        dc.dcpr_comprobante = comp.com_codigo;
                        DcalculoprecioBLL.Insert(transaction, dc);
                    }
                    contador++;
                }

                //ASIGNA LA FACTURA A LA PLANILLA DE CLIENTES
                if (comp.planillacomp.pco_comprobante_pla > 0)
                {

                    PlanillacomprobanteBLL.Delete(transaction, comp.planillacomp);
                    comp.planillacomp.pco_comprobante_fac = comp.com_codigo;
                    PlanillacomprobanteBLL.Insert(transaction, comp.planillacomp);
                }
                ////////////////////////////////////////////////////////////////
                else
                {
                    comp.ccomenv.cenv_empresa = comp.com_empresa;
                    comp.ccomenv.cenv_comprobante = comp.com_codigo;
                    CcomenvBLL.Insert(transaction, comp.ccomenv);//INSERT CCOMENV
                }

                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL


                
                int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;

                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////
                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                #region Elimina de la Hoja de Ruta y actualiza valores


                foreach (Rutaxfactura item in lsthojas)
                {
                    RutaxfacturaBLL.Delete(transaction, item);
                }
                if (hr.total != null)
                {
                    hr.total.tot_comprobante_key = hr.total.tot_comprobante;
                    hr.total.tot_empresa_key = hr.total.tot_empresa;
                    TotalBLL.Update(transaction, hr.total);
                }
                #endregion
              
                if (rfac.rfac_comprobanteruta > 0)
                {
                    rfac.rfac_comprobantefac = comp.com_codigo;
                    RutaxfacturaBLL.Insert(transaction, rfac);
                    hr.total.tot_tseguro = hr.total.tot_tseguro + comp.total.tot_tseguro;
                    hr.total.tot_timpuesto = hr.total.tot_timpuesto + comp.total.tot_timpuesto;
                    hr.total.tot_transporte = hr.total.tot_transporte + comp.total.tot_transporte;
                    hr.total.tot_total = hr.total.tot_total + comp.total.tot_total;
                    TotalBLL.Update(transaction, hr.total);
                }
                General.save_historial(transaction, objU);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante save_factura(Comprobante comp)
        {
            
            //DateTime fecha = DateTime.Now;
           
            if (comp.com_nocontable == 1)
            {
                comp.com_concepto = (string.IsNullOrEmpty(comp.com_concepto) ? "GUIA DE VENTA " + comp.ccomdoc.cdoc_nombre + " " + comp.com_concepto : comp.com_concepto);
            }
            else
            {
                comp.com_concepto = (string.IsNullOrEmpty(comp.com_concepto) ? "FACTURA DE VENTA " + comp.ccomdoc.cdoc_nombre + "  " + comp.com_concepto : comp.com_concepto);

            }
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            comp.com_tclipro = Constantes.cCliente;
            //obj.com_fecha = fecha;

            /*ACTUALIZA DATOS DE HOJA DE RUTA EN CCOMENV */



            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();



                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE                
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC

                int contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);
                    if (item.detallecalculo!=null)
                    {
                        //LOOP INSER CADA DCALCULOPRECIO DE CADA DCOMDOC
                        foreach (Dcalculoprecio dc in item.detallecalculo)
                    {
                        dc.dcpr_dcomdoc = contador;
                        dc.dcpr_comprobante = comp.com_codigo;
                        DcalculoprecioBLL.Insert(transaction, dc);
                    }
                    }
                    contador++;
                }

                //ASIGNA LA FACTURA A LA PLANILLA DE CLIENTES
                if (comp.planillacomp!=null)
                {
                    if (comp.planillacomp.pco_comprobante_pla > 0)
                {
                    comp.planillacomp.pco_comprobante_fac = comp.com_codigo;
                    PlanillacomprobanteBLL.Insert(transaction, comp.planillacomp);
                }
                }
                ////////////////////////////////////////////////////////////////
                //  else
                // {
                if (comp.ccomenv!=null)
                {
                    comp.ccomenv.cenv_empresa = comp.com_empresa;
                comp.ccomenv.cenv_comprobante = comp.com_codigo;
                CcomenvBLL.Insert(transaction, comp.ccomenv);//INSERT CCOMENV
                }                                             //}

                comp.total.tot_empresa = comp.com_empresa;
                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL


                int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;
                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////

                General.save_historial(transaction, comp);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante save_factura(Comprobante comp, Rutaxfactura rfac)
        {            
            Comprobante hr = new Comprobante();
            if (rfac.rfac_comprobanteruta > 0)
            {
                hr.com_empresa = comp.com_empresa;
                hr.com_empresa_key = comp.com_empresa;
                hr.com_codigo = rfac.rfac_comprobanteruta;
                hr.com_codigo_key = rfac.rfac_comprobanteruta;
                hr = ComprobanteBLL.GetByPK(hr);
                hr.total = new Total();
                hr.total.tot_empresa = hr.com_empresa;
                hr.total.tot_empresa_key = hr.com_empresa;
                hr.total.tot_comprobante = hr.com_codigo;
                hr.total.tot_comprobante_key = hr.com_codigo;
                hr.total = TotalBLL.GetByPK(hr.total);
            }


            //DateTime fecha = DateTime.Now;

            Tipodoc tdoc = TipodocBLL.GetByPK(new Tipodoc { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });
            TipoDocOpc tdocopc = new JavaScriptSerializer().Deserialize<TipoDocOpc>(tdoc.tpd_opciones);
            if (tdocopc == null)
                tdocopc = new TipoDocOpc();


            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            //dti.dti_numero = dti.dti_numero.Value + 1;
            if (!tdocopc.numeromanual)
                comp.com_numero = General.GetNumeroLibre(dti).dti_numero.Value;
            else
            {
                List<Comprobante> lst = ComprobanteBLL.GetAll("com_doctran='" + comp.com_doctran.Trim() + "' and com_empresa=" + comp.com_empresa, "");
                if (lst.Count > 0)
                    throw new ArgumentException("No se puede guardar el comprobante " + comp.com_doctran + ", la numeración ingresada ya existe...");                    
            }

           
            if (comp.com_nocontable == 1)
            {
                comp.com_concepto = (string.IsNullOrEmpty(comp.com_concepto) ? "GUIA DE VENTA " + comp.ccomdoc.cdoc_nombre + " " + comp.com_concepto : comp.com_concepto);
            }
            else
            {
                comp.com_concepto = (string.IsNullOrEmpty(comp.com_concepto) ? "FACTURA DE VENTA " + comp.ccomdoc.cdoc_nombre + "  " + comp.com_concepto : comp.com_concepto);
                
            }
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            comp.com_tclipro = Constantes.cCliente;
            //obj.com_fecha = fecha;

            /*ACTUALIZA DATOS DE HOJA DE RUTA EN CCOMENV */



            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE                
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC

                int contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);

                    //LOOP INSER CADA DCALCULOPRECIO DE CADA DCOMDOC
                    foreach (Dcalculoprecio dc in item.detallecalculo)
                    {
                        dc.dcpr_dcomdoc = contador;
                        dc.dcpr_comprobante = comp.com_codigo;
                        DcalculoprecioBLL.Insert(transaction, dc);
                    }
                    contador++;
                }

                //ASIGNA LA FACTURA A LA PLANILLA DE CLIENTES
                if (comp.planillacomp.pco_comprobante_pla > 0)
                {
                    comp.planillacomp.pco_comprobante_fac = comp.com_codigo;
                    PlanillacomprobanteBLL.Insert(transaction, comp.planillacomp);
                }
                ////////////////////////////////////////////////////////////////
                //  else
                // {
                comp.ccomenv.cenv_empresa = comp.com_empresa;
                comp.ccomenv.cenv_comprobante = comp.com_codigo;
                CcomenvBLL.Insert(transaction, comp.ccomenv);//INSERT CCOMENV
                                                             //}

                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL


                int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;
                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////
                if (!tdocopc.numeromanual)
                    General.UpdateDtipocom(transaction, dti, comp.com_numero);

                if (rfac.rfac_comprobanteruta > 0)
                {
                    rfac.rfac_comprobantefac = comp.com_codigo;
                    RutaxfacturaBLL.Insert(transaction, rfac);
                    hr.total.tot_tseguro = hr.total.tot_tseguro + comp.total.tot_tseguro;
                    hr.total.tot_timpuesto = hr.total.tot_timpuesto + comp.total.tot_timpuesto;
                    hr.total.tot_transporte = hr.total.tot_transporte + comp.total.tot_transporte;
                    hr.total.tot_total = hr.total.tot_total + comp.total.tot_total;
                    TotalBLL.Update(transaction, hr.total);
                }
                General.save_historial(transaction, comp);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }
        public static Comprobante account_factura(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;

            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            List<Planillacomprobante> planillacomp = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa={0} and pco_comprobante_fac={1}", comp.com_empresa, comp.com_codigo), "");
            if (planillacomp.Count > 0)
                comp.planillacomp = planillacomp[0];

            int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                #region Elimina documentos y contabilizaciones existentes

                foreach (Dcontable item in comp.contables)
                {
                    DcontableBLL.Delete(transaction, item);
                }
                comp.contables = new List<Dcontable>();
                foreach (Ddocumento item in comp.documentos)
                {
                    DdocumentoBLL.Delete(transaction, item);
                }
                comp.documentos = new List<Ddocumento>();

                #endregion

                //CREACION DE DOCUMENTOS
                comp.documentos = CXCP.crear_documentos(comp, vdebcre);

                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    ////////////////
                    comp.contables.AddRange(CXCP.contables_detalle(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_impuesto(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_transporte(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_seguro(comp, vdebcre_m));


                    //comp.contables.AddRange(CXCP.contable_descuento(comp, vdebcre_m));
                    

                    ////////////////////////                   
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contables_cancelaciones(comp));
                    //////////////////////////
                    //Auto.actualizar_saldo(transaction, comp, 1); 

                    #endregion

                }

                #region Guarda Documentos y Contabilizaciones

                foreach (Ddocumento item in comp.documentos)
                {
                   
                    DdocumentoBLL.Insert(transaction, item);
                }
                int sec = 0;
                foreach (Dcontable item in comp.contables)
                {
                    sec++;
                    item.dco_secuencia = sec;
                    DcontableBLL.Insert(transaction, item);
                }


                #endregion

                General.save_historial(transaction, comp);

                if (Auto.actualizar_saldo(transaction, comp, 1))
                    transaction.Commit();
                else
                    throw new Exception("ERROR no se puede actualzar saldos");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

        public static Comprobante close_guia(Comprobante comp)
        {
            string lockmod = Constantes.GetParameter("lockmod");
            if (!string.IsNullOrEmpty(lockmod))
            {
                comp.com_empresa_key = comp.com_empresa;
                comp.com_codigo_key = comp.com_codigo;
                comp = ComprobanteBLL.GetByPK(comp);
                comp.com_empresa_key = comp.com_empresa;
                comp.com_codigo_key = comp.com_codigo;
                comp.com_estado = Constantes.cEstadoMayorizado;
                ComprobanteBLL.Update(comp);
            }
            return comp;
        }

        public static Comprobante reaccount_factura(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;

            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            List<Planillacomprobante> planillacomp = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa={0} and pco_comprobante_fac={1}", comp.com_empresa, comp.com_codigo), "");
            if (planillacomp.Count > 0)
                comp.planillacomp = planillacomp[0];

            int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                #region Elimina  contabilizaciones existentes

                foreach (Dcontable item in comp.contables)
                {
                    DcontableBLL.Delete(transaction, item);
                }
                comp.contables = new List<Dcontable>();
                
                #endregion

                foreach (Ddocumento doc in comp.documentos)
                {
                    if (doc.ddo_cuenta==0)
                    {
                        doc.ddo_cuenta = CXCP.cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                        doc.ddo_empresa_key = doc.ddo_empresa;
                        doc.ddo_comprobante_key = doc.ddo_comprobante;
                        doc.ddo_transacc_key = doc.ddo_transacc;
                        doc.ddo_doctran_key = doc.ddo_doctran;
                        doc.ddo_pago_key = doc.ddo_pago;
                        DdocumentoBLL.Update(transaction, doc);
                    }
                    
                }

                
                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    ////////////////
                    comp.contables.AddRange(CXCP.contables_detalle(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_impuesto(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_transporte(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_seguro(comp, vdebcre_m));

                    ////////////////////////                   
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre_m));
                    //comp.contables.AddRange(CXCP.contables_cancelaciones(comp));
                    //////////////////////////
                    //Auto.actualizar_saldo(transaction, comp, 1); 

                    #endregion

                }

                #region Guarda Contabilizaciones

                int sec = 0;
                foreach (Dcontable item in comp.contables)
                {
                    sec++;
                    item.dco_secuencia = sec;
                    DcontableBLL.Insert(transaction, item);
                }


                #endregion

                General.save_historial(transaction, comp);

                if (Auto.actualizar_saldo(transaction, comp, 1))
                    transaction.Commit();
                else
                    throw new Exception("ERROR no se puede actualzar saldos");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

        public static Comprobante save_cancelacion_factura(Comprobante comp, List<Drecibo> recibos, DateTime fecha)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);

            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.total = new Total();
            comp.total.tot_empresa = comp.com_empresa;
            comp.total.tot_empresa_key = comp.com_empresa;
            comp.total.tot_comprobante = comp.com_codigo;
            comp.total.tot_comprobante_key = comp.com_codigo;
            comp.total = TotalBLL.GetByPK(comp.total);

            //DateTime fecha = DateTime.Now;
            int ctipocom = Constantes.cComRecibo.cti_codigo;  // SE DEBE OBTENER DE ALGUN LADO ?????

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            

            Comprobante c = new Comprobante();
            c.com_empresa = comp.com_empresa;
            c.com_periodo = comp.com_periodo;
            c.com_tipodoc = Constantes.cRecibo.tpd_codigo;
            c.com_ctipocom = ctipocom;
            c.com_fecha = fecha;
            c.com_dia = c.com_fecha.Day;
            c.com_mes = c.com_fecha.Month;
            c.com_anio = c.com_fecha.Year;
            c.com_almacen = comp.com_almacen;
            c.com_pventa = comp.com_pventa;
            c.com_codclipro = comp.com_codclipro;
            c.com_tclipro = comp.com_tclipro;
            c.com_agente = comp.com_agente;
            c.com_centro = Constantes.GetSinCentro().cen_codigo;
            //c.com_serie 
            c.com_numero = dti.dti_numero.Value;
            Persona per = new Persona();
            per.per_codigo_key = comp.com_codclipro.Value;
            per.per_empresa_key = comp.com_empresa;
            per = PersonaBLL.GetByPK(per);
            c.com_concepto = "CANCELACION " + per.per_nombres + " " + per.per_apellidos;

            //     c.com_concepto = "CANCELACION";
            c.com_modulo = General.GetModulo(c.com_tipodoc); ;
            c.com_transacc = General.GetTransacc(c.com_tipodoc);
            c.com_nocontable = 0;
            c.com_estado = Constantes.cEstadoGrabado;
            c.com_descuadre = 0;
            c.com_adestino = 0;
            c.com_doctran = General.GetNumeroComprobante(c);

            c.crea_usr = comp.crea_usr;
            c.crea_fecha = comp.crea_fecha;
            c.mod_usr = comp.mod_usr;
            c.mod_fecha = comp.mod_fecha;


            c.total = new Total();
            c.total.tot_empresa = comp.com_empresa;
            //c.total.tot_total = comp.total.tot_total;
            c.total.tot_total = recibos.Sum(s => s.dfp_monto);


            int debcredoc = (c.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int debcrecan = (c.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                c.com_codigo = ComprobanteBLL.InsertIdentity(transaction, c);//GUARDA CABECERA CANCELACION
                int contador = 0;
                decimal totalcancela = 0;
                foreach (Drecibo drec in recibos)
                {
                    drec.dfp_empresa = c.com_empresa;
                    drec.dfp_comprobante = c.com_codigo;
                    drec.dfp_secuencia = contador;
                    drec.dfp_tclipro = comp.com_tclipro;
                    drec.dfp_fecha_ven = fecha;
                    drec.dfp_monto_ext = drec.dfp_monto;
                    drec.dfp_ref_comprobante = comp.com_codigo;
                    totalcancela += drec.dfp_monto;                   
                    DreciboBLL.Insert(transaction, drec);  //GUARDA EL RECIBO 
                    contador++;
                }

                contador = 0;
                     foreach (Ddocumento doc in comp.documentos)
                     {
                         Dcancelacion dca = new Dcancelacion();
                         dca.dca_empresa = doc.ddo_empresa;
                         dca.dca_comprobante = doc.ddo_comprobante;
                         dca.dca_transacc = doc.ddo_transacc;
                         dca.dca_doctran = doc.ddo_doctran;
                         dca.dca_pago = doc.ddo_pago;
                         dca.dca_comprobante_can = c.com_codigo;
                         dca.dca_secuencia = 0;
                         dca.dca_debcre = debcrecan; //(doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                         dca.dca_transacc_can = c.com_transacc;
                         dca.dca_tipo_cambio = doc.ddo_tipo_cambio;
                         if (doc.ddo_monto <= totalcancela)
                         {
                             dca.dca_monto = doc.ddo_monto;
                             dca.dca_monto_ext = doc.ddo_monto; 
                         }
                         else
                         {
                             dca.dca_monto = totalcancela;
                             dca.dca_monto_ext = totalcancela;                             
                         }

                         totalcancela = totalcancela - dca.dca_monto.Value;
                         if (dca.dca_monto.Value>0)                        
                         {                             
                             DcancelacionBLL.Insert(transaction, dca);
                         }
                          //GUARDA EL DETALLE DE CANCELACION
                         contador++;
                                       
 
                         //Cambia el estado del documento que tambien se debe actualizar
                       
                         //
                     }
                /*
                     if (totalcancela > 0)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                     {                    
                         Ddocumento doc = new Ddocumento();
                         doc.ddo_empresa = c.com_empresa;
                         doc.ddo_comprobante = c.com_codigo;
                         doc.ddo_transacc = General.GetTransacc(c.com_tipodoc);
                         doc.ddo_doctran = c.com_doctran;
                         doc.ddo_pago = 1;
                         doc.ddo_codclipro = c.com_codclipro;
                         doc.ddo_debcre = debcrecan;
                         //doc.ddo_tipo_cambio = 
                         doc.ddo_fecha_emi = c.com_fecha;
                         doc.ddo_fecha_ven = fecha;
                         doc.ddo_monto = totalcancela;
                         //doc.ddo_monto_ext = 
                         doc.ddo_cancela = 0;
                         //doc.ddo_cancela_ext =
                         doc.ddo_cancelado = 0;
                         doc.ddo_agente = c.com_agente;
                         //doc.ddo_cuenta = 
                         doc.ddo_modulo = c.com_modulo;
                         DdocumentoBLL.Insert(transaction, doc);
                     }
             
                     */
                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                c.total.tot_comprobante = c.com_codigo;
                
                TotalBLL.Insert(transaction, c.total);//INSERT TOTAL
                General.save_historial(transaction, c);
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;

            }
            return c;
        }
        public static Comprobante save_cancelacion(Comprobante comp, bool permitecero)
        {
            DateTime fecha = DateTime.Now;
            int ctipocom = Constantes.cComRecibo.cti_codigo; // SE DEBE OBTENER DE ALGUN LADO ?????

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            comp.com_numero = dti.dti_numero.Value;

            Persona per = new Persona();
            per.per_codigo_key = comp.com_codclipro.Value;
            per.per_empresa_key = comp.com_empresa;
            per = PersonaBLL.GetByPK(per);
            if (string.IsNullOrEmpty(comp.com_concepto))
                comp.com_concepto = "CANCELACION " + per.per_nombres + " " + per.per_apellidos;

            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);

            comp.com_tclipro = (comp.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCliente : Constantes.cProveedor;

            int debcredoc = (comp.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int debcrecan = (comp.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();
                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);
                decimal totalrecibo = 0;
                int contador = 0;
                foreach (Drecibo item in comp.recibos)
                {
                    item.dfp_comprobante = comp.com_codigo;
                    item.dfp_secuencia = contador;
                    item.dfp_debcre = debcredoc;
                    item.dfp_monto_ext = item.dfp_monto;
                    totalrecibo += item.dfp_monto;                    
                    DreciboBLL.Insert(transaction, item);
                    contador++;
                }

                decimal totalcancela = 0;
                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    bool savedca = true;
                    if (!permitecero)
                        savedca = dca.dca_monto > 0;
                    //if (dca.dca_monto > 0)
                    if (savedca)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = debcrecan;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        contador++;
                    }
                }/* 
                        //Cambia el estado del documento que tambien se debe actualizar
                        Ddocumento doc = new Ddocumento();
                        doc.ddo_empresa = dca.dca_empresa;
                        doc.ddo_empresa_key = dca.dca_empresa;
                        doc.ddo_comprobante = dca.dca_comprobante;
                        doc.ddo_comprobante_key = dca.dca_comprobante;
                        doc.ddo_transacc = dca.dca_transacc;
                        doc.ddo_transacc_key = dca.dca_transacc;
                        doc.ddo_doctran = dca.dca_doctran;
                        doc.ddo_doctran_key = dca.dca_doctran;
                        doc.ddo_pago = dca.dca_pago;
                        doc.ddo_pago_key = dca.dca_pago;


                        doc = DdocumentoBLL.GetByPK(doc);
                        doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;
                        if (doc.ddo_cancela >= doc.ddo_monto)
                            doc.ddo_cancelado = 1;
                        totalcancela += dca.dca_monto.Value;
                        DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                    }
                    //
                }
                /*
                if (totalrecibo > totalcancela)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                {
                    decimal valordoc = totalrecibo - totalcancela;
                    Ddocumento doc = new Ddocumento();
                    doc.ddo_empresa = comp.com_empresa;
                    doc.ddo_comprobante = comp.com_codigo;
                    doc.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                    doc.ddo_doctran = comp.com_doctran;
                    doc.ddo_pago = 1;
                    doc.ddo_codclipro = comp.com_codclipro;
                    doc.ddo_debcre = debcrecan;
                    //doc.ddo_tipo_cambio = 
                    doc.ddo_fecha_emi = comp.com_fecha;
                    doc.ddo_fecha_ven = fecha;
                    doc.ddo_monto = valordoc;
                    //doc.ddo_monto_ext = 
                    doc.ddo_cancela = 0;
                    //doc.ddo_cancela_ext =
                    doc.ddo_cancelado = 0;
                    doc.ddo_agente = comp.com_agente;
                    //doc.ddo_cuenta = 
                    doc.ddo_modulo = comp.com_modulo;
                    DdocumentoBLL.Insert(transaction, doc);
                }
                */
                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL
                General.save_historial(transaction, comp);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }                
        public static Comprobante update_cancelacion(Comprobante comp)
        {
            
            DateTime fecha = DateTime.Now;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.com_estado = Constantes.cEstadoGrabado;
            objU.com_concepto = comp.com_concepto;
            objU.mod_usr = comp.mod_usr;
            objU.mod_fecha = comp.mod_fecha;
            objU.com_tclipro = (comp.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCliente : Constantes.cProveedor;


            Dtipocom dti = new Dtipocom();
            if (string.IsNullOrEmpty(objU.com_doctran))
            {

                objU.com_modulo = General.GetModulo(comp.com_tipodoc); ;
                objU.com_transacc = General.GetTransacc(comp.com_tipodoc);
                objU.com_centro = Constantes.GetSinCentro().cen_codigo;
                objU.com_estado = Constantes.cEstadoGrabado;

                objU.com_doctran = comp.com_doctran;
                objU.com_numero = comp.com_numero;

                dti = General.GetDtipocom(objU.com_empresa, objU.com_fecha.Year, objU.com_ctipocom, objU.com_almacen.Value, objU.com_pventa.Value);
                dti.dti_numero = dti.dti_numero + 1;

                if (objU.com_numero < dti.dti_numero)
                {
                    objU.com_numero = dti.dti_numero.Value;
                    objU.com_doctran = General.GetNumeroComprobante(objU);
                }
            }




            BLL transaction = new BLL();
            transaction.CreateTransaction();
            int debcredoc = (comp.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int debcrecan = (comp.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;

            try
            {

                transaction.BeginTransaction();

                objU.recibos = DreciboBLL.GetAll(new WhereParams("dfp_empresa ={0} and dfp_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
                objU.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
                //foreach (Drecibo item in comp.recibos)
                foreach (Drecibo item in objU.recibos)
                {
                    //item.dfp_comprobante_key = item.dfp_comprobante;
                    //item.dfp_secuencia_key = item.dfp_secuencia;
                    //item.dfp_empresa_key = item.dfp_empresa;                    
                    DreciboBLL.Delete(transaction, item);                    
                }
                
                objU.total.tot_comprobante_key = objU.total.tot_comprobante_key;
                objU.total.tot_empresa_key = objU.total.tot_empresa_key;
                TotalBLL.Delete(transaction, objU.total);

                ComprobanteBLL.Update(transaction, objU);// INSERT COMPROBANTE

                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL

                decimal totalrecibo = 0;
                int contador = 0;
                foreach (Drecibo item in comp.recibos)
                {
                    item.dfp_comprobante = comp.com_codigo;
                    item.dfp_secuencia = contador;
                    item.dfp_debcre = debcredoc;
                    item.dfp_monto_ext = item.dfp_monto;
                    totalrecibo += item.dfp_monto;                   
                    DreciboBLL.Insert(transaction, item);
                    contador++;
                }

                objU.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
                foreach (Dcancelacion item in objU.cancelaciones)
                {
                    DcancelacionBLL.Delete(transaction, item);
                }

              //  decimal totalcancela = 0;
                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = debcrecan;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        contador++;
                    }
                } /*
                        //Cambia el estado del documento que tambien se debe actualizar
                        Ddocumento doc = new Ddocumento();
                        doc.ddo_empresa = dca.dca_empresa;
                        doc.ddo_empresa_key = dca.dca_empresa;
                        doc.ddo_comprobante = dca.dca_comprobante;
                        doc.ddo_comprobante_key = dca.dca_comprobante;
                        doc.ddo_transacc = dca.dca_transacc;
                        doc.ddo_transacc_key = dca.dca_transacc;
                        doc.ddo_doctran = dca.dca_doctran;
                        doc.ddo_doctran_key = dca.dca_doctran;
                        doc.ddo_pago = dca.dca_pago;
                        doc.ddo_pago_key = dca.dca_pago;


                        doc = DdocumentoBLL.GetByPK(doc);
                        doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;
                        if (doc.ddo_cancela >= doc.ddo_monto)
                            doc.ddo_cancelado = 1;
                        totalcancela += dca.dca_monto.Value;
                        DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                    }
                    //
                }
                /*
                if (totalrecibo > totalcancela)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                {
                    decimal valordoc = totalrecibo - totalcancela;
                    Ddocumento doc = new Ddocumento();
                    doc.ddo_empresa = comp.com_empresa;
                    doc.ddo_comprobante = comp.com_codigo;
                    doc.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                    doc.ddo_doctran = comp.com_doctran;
                    doc.ddo_pago = 1;
                    doc.ddo_codclipro = comp.com_codclipro;
                    doc.ddo_debcre = debcrecan;
                    //doc.ddo_tipo_cambio = 
                    doc.ddo_fecha_emi = comp.com_fecha;
                    doc.ddo_fecha_ven = fecha;
                    doc.ddo_monto = valordoc;
                    //doc.ddo_monto_ext = 
                    doc.ddo_cancela = 0;
                    //doc.ddo_cancela_ext =
                    doc.ddo_cancelado = 0;
                    doc.ddo_agente = comp.com_agente;
                    //doc.ddo_cuenta = 
                    doc.ddo_modulo = comp.com_modulo;
                    DdocumentoBLL.Insert(transaction, doc);
                }
                */
                  //     DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                General.save_historial(transaction, objU);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante create_cancelacion(Comprobante comp, List<Drecibo> recibos, DateTime fecha)
        {




            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);

            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.total = new Total();
            comp.total.tot_empresa = comp.com_empresa;
            comp.total.tot_empresa_key = comp.com_empresa;
            comp.total.tot_comprobante = comp.com_codigo;
            comp.total.tot_comprobante_key = comp.com_codigo;
            comp.total = TotalBLL.GetByPK(comp.total);

            //DateTime fecha = DateTime.Now;
            int ctipocom = Constantes.cComRecibo.cti_codigo;  // SE DEBE OBTENER DE ALGUN LADO ?????

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;


            Comprobante c = new Comprobante();
            c.com_empresa = comp.com_empresa;
            c.com_periodo = comp.com_periodo;
            c.com_tipodoc = Constantes.cRecibo.tpd_codigo;
            c.com_ctipocom = ctipocom;
            c.com_fecha = fecha;
            c.com_dia = c.com_fecha.Day;
            c.com_mes = c.com_fecha.Month;
            c.com_anio = c.com_fecha.Year;
            c.com_almacen = comp.com_almacen;
            c.com_pventa = comp.com_pventa;
            c.com_codclipro = comp.com_codclipro;
            c.com_tclipro = comp.com_tclipro;
            c.com_agente = comp.com_agente;
            c.com_centro = Constantes.GetSinCentro().cen_codigo;
            //c.com_serie 
            c.com_numero = dti.dti_numero.Value;
            Persona per = new Persona();
            per.per_codigo_key = comp.com_codclipro.Value;
            per.per_empresa_key = comp.com_empresa;
            per = PersonaBLL.GetByPK(per);
            c.com_concepto = "CANCELACION " + per.per_nombres + " " + per.per_apellidos;

            //     c.com_concepto = "CANCELACION";
            c.com_modulo = General.GetModulo(c.com_tipodoc); ;
            c.com_transacc = General.GetTransacc(c.com_tipodoc);
            c.com_nocontable = 0;
            c.com_estado = Constantes.cEstadoGrabado;
            c.com_descuadre = 0;
            c.com_adestino = 0;
            c.com_doctran = General.GetNumeroComprobante(c);

            c.crea_usr = comp.crea_usr;
            c.crea_fecha = comp.crea_fecha;
            c.mod_usr = comp.mod_usr;
            c.mod_fecha = comp.mod_fecha;


            c.total = new Total();
            c.total.tot_empresa = comp.com_empresa;
            //c.total.tot_total = comp.total.tot_total;
            c.total.tot_total = recibos.Sum(s => s.dfp_monto);


            int debcredoc = (c.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int debcrecan = (c.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                c.com_codigo = ComprobanteBLL.InsertIdentity(transaction, c);//GUARDA CABECERA CANCELACION
                int contador = 0;
                decimal totalcancela = 0;
                foreach (Drecibo drec in recibos)
                {
                    drec.dfp_empresa = c.com_empresa;
                    drec.dfp_comprobante = c.com_codigo;
                    drec.dfp_secuencia = contador;
                    drec.dfp_tclipro = comp.com_tclipro;
                    drec.dfp_fecha_ven = fecha;
                    drec.dfp_monto_ext = drec.dfp_monto;
                    drec.dfp_ref_comprobante = comp.com_codigo;
                    totalcancela += drec.dfp_monto;
                    DreciboBLL.Insert(transaction, drec);  //GUARDA EL RECIBO 
                    contador++;
                }

                contador = 0;
                foreach (Ddocumento doc in comp.documentos)
                {
                    Dcancelacion dca = new Dcancelacion();
                    dca.dca_empresa = doc.ddo_empresa;
                    dca.dca_comprobante = doc.ddo_comprobante;
                    dca.dca_transacc = doc.ddo_transacc;
                    dca.dca_doctran = doc.ddo_doctran;
                    dca.dca_pago = doc.ddo_pago;
                    dca.dca_comprobante_can = c.com_codigo;
                    dca.dca_secuencia = 0;
                    dca.dca_debcre = debcrecan; //(doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                    dca.dca_transacc = c.com_transacc;
                    dca.dca_tipo_cambio = doc.ddo_tipo_cambio;
                    if (doc.ddo_monto <= totalcancela)
                    {
                        dca.dca_monto = doc.ddo_monto;
                        dca.dca_monto_ext = doc.ddo_monto;
                    }
                    else
                    {
                        dca.dca_monto = totalcancela;
                        dca.dca_monto_ext = totalcancela;
                    }

                    totalcancela = totalcancela - dca.dca_monto.Value;
                    if (dca.dca_monto.Value > 0)
                    {
                        DcancelacionBLL.Insert(transaction, dca);
                    }
                    //GUARDA EL DETALLE DE CANCELACION
                    contador++;


                    //Cambia el estado del documento que tambien se debe actualizar

                    //
                }
                /*
                     if (totalcancela > 0)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                     {                    
                         Ddocumento doc = new Ddocumento();
                         doc.ddo_empresa = c.com_empresa;
                         doc.ddo_comprobante = c.com_codigo;
                         doc.ddo_transacc = General.GetTransacc(c.com_tipodoc);
                         doc.ddo_doctran = c.com_doctran;
                         doc.ddo_pago = 1;
                         doc.ddo_codclipro = c.com_codclipro;
                         doc.ddo_debcre = debcrecan;
                         //doc.ddo_tipo_cambio = 
                         doc.ddo_fecha_emi = c.com_fecha;
                         doc.ddo_fecha_ven = fecha;
                         doc.ddo_monto = totalcancela;
                         //doc.ddo_monto_ext = 
                         doc.ddo_cancela = 0;
                         //doc.ddo_cancela_ext =
                         doc.ddo_cancelado = 0;
                         doc.ddo_agente = c.com_agente;
                         //doc.ddo_cuenta = 
                         doc.ddo_modulo = c.com_modulo;
                         DdocumentoBLL.Insert(transaction, doc);
                     }
             
                     */
                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                c.total.tot_comprobante = c.com_codigo;

                TotalBLL.Insert(transaction, c.total);//INSERT TOTAL
                General.save_historial(transaction, c);
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;

            }
            return c;
        }

        public static Comprobante account_recibo(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            //    comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            //     comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            //    comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba ={1}", comp.com_empresa, comp.com_codigo), "");
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.recibos = DreciboBLL.GetAll(new WhereParams("dfp_empresa ={0} and dfp_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            int vdebcre = (comp.com_tclipro == Constantes.cCliente) ? Constantes.cCredito : Constantes.cDebito;
            int vdebcre_m = (comp.com_tclipro == Constantes.cCliente) ? Constantes.cDebito : Constantes.cCredito;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                #region Elimina documentos y contabilizaciones existentes
                foreach (Dcontable item in comp.contables)
                {
                    DcontableBLL.Delete(transaction, item);
                }
                comp.contables = new List<Dcontable>();
                foreach (Ddocumento item in comp.documentos)
                {
                    DdocumentoBLL.Delete(transaction, item);
                }
                comp.documentos = new List<Ddocumento>();
                foreach (Dcancelacion item in comp.cancelaciones)
                {
                    DcancelacionBLL.Delete(transaction, item);
                }
                comp.cancelaciones = new List<Dcancelacion>();
                #endregion


                comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
                 foreach (Dcancelacion dca in comp.cancelaciones)
                {

                    dca.dca_empresa_key = dca.dca_empresa;
                    dca.dca_comprobante_key = dca.dca_comprobante;
                    dca.dca_transacc_key = dca.dca_transacc;
                    dca.dca_doctran_key = dca.dca_doctran;
                    dca.dca_pago_key = dca.dca_pago;
                    dca.dca_comprobante_can_key = dca.dca_comprobante_can;
                    dca.dca_secuencia_key = dca.dca_secuencia;
                    DcancelacionBLL.Delete(transaction, dca);
                 }



                decimal totalrecibo = 0;
                decimal totalcancela = 0;


                foreach (Drecibo item in comp.recibos)
                {
                    totalrecibo += item.dfp_monto;
                }



                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    dca.dca_monto_ext = dca.dca_monto;
                    Ddocumento doc = new Ddocumento();
                    doc.ddo_empresa = dca.dca_empresa;
                    doc.ddo_empresa_key = dca.dca_empresa;
                    doc.ddo_comprobante = dca.dca_comprobante;
                    doc.ddo_comprobante_key = dca.dca_comprobante;
                    doc.ddo_transacc = dca.dca_transacc;
                    doc.ddo_transacc_key = dca.dca_transacc;
                    doc.ddo_doctran = dca.dca_doctran;
                    doc.ddo_doctran_key = dca.dca_doctran;
                    doc.ddo_pago = dca.dca_pago;
                    doc.ddo_pago_key = dca.dca_pago;
                    doc = DdocumentoBLL.GetByPK(doc);
                    doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;                    
                    if (doc.ddo_cancela > doc.ddo_monto)
                        doc.ddo_cancela = doc.ddo_monto;                        
                    doc.ddo_cancela_ext = doc.ddo_cancela;
                    doc.ddo_monto_ext = doc.ddo_monto;
                    if (doc.ddo_cancela >= doc.ddo_monto)
                        doc.ddo_cancelado = 1;
                    totalcancela += dca.dca_monto.Value;
                    DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO  
                    comp.documentos.Add(doc);

                }/* 












                    Comprobante c = new Comprobante();
                    c.com_empresa_key = item.dfp_empresa;
                    c.com_codigo_key = item.dfp_ref_comprobante??0;
                    c = ComprobanteBLL.GetByPK(c);
                    c.com_empresa_key = c.com_empresa;
                    c.com_codigo_key = c.com_codigo;
                    c.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1} and ddo_cancelado={2}", c.com_empresa, c.com_codigo,0), "");
                    decimal monto=  item.dfp_monto;
                    foreach (Ddocumento doc in c.documentos)
                    {
                        Dcancelacion dca = new Dcancelacion();
                        dca.dca_empresa = doc.ddo_empresa;
                        dca.dca_comprobante = doc.ddo_comprobante;                       
                        dca.dca_doctran = doc.ddo_doctran;
                        dca.dca_pago = doc.ddo_pago;
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_secuencia = 0;
                        dca.dca_debcre = vdebcre; //(doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                        dca.dca_transacc = comp.com_transacc;
                        dca.dca_tipo_cambio = doc.ddo_tipo_cambio;
                        if ((doc.ddo_monto - doc.ddo_cancela) <= item.dfp_monto)
                        {
                            dca.dca_monto = (doc.ddo_monto - doc.ddo_cancela);
                            dca.dca_monto_ext = (doc.ddo_monto - doc.ddo_cancela);
                            doc.ddo_cancela = doc.ddo_monto;
                            doc.ddo_cancelado = 1;
                            item.dfp_monto = item.dfp_monto - dca.dca_monto.Value;
                        }
                        else
                        {
                            dca.dca_monto = item.dfp_monto;
                            dca.dca_monto_ext = item.dfp_monto;
                            doc.ddo_cancela = doc.ddo_cancela + item.dfp_monto;
                            item.dfp_monto =0;
                        }
                        totalcancela += dca.dca_monto.Value;
                        comp.cancelaciones.Add(dca);
                        DdocumentoBLL.Update(transaction, doc);
                        comp.documentos.Add(doc);
                    }
                        item.dfp_monto = monto;
                    totalrecibo += item.dfp_monto;                
                }*/
                if (totalrecibo > totalcancela)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                {
                    decimal valordoc = totalrecibo - totalcancela;
                    Ddocumento doc2 = new Ddocumento();
                    doc2.ddo_empresa = comp.com_empresa;
                    doc2.ddo_comprobante = comp.com_codigo;
                    doc2.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                    doc2.ddo_doctran = comp.com_doctran;
                    List<Ddocumento> lst2 = new List<Ddocumento>();
                    lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
                    doc2.ddo_pago = lst2.Count + 1;
                    doc2.ddo_codclipro = comp.com_codclipro;
                    doc2.ddo_debcre = vdebcre;
                    doc2.ddo_fecha_emi = comp.com_fecha;
                    doc2.ddo_fecha_ven = DateTime.Now;
                    doc2.ddo_monto = valordoc;
                    doc2.ddo_monto_ext = valordoc;
                    doc2.ddo_cancela = 0;
                    doc2.ddo_cancelado = 0;
                    Personaxtipo pxt = new Personaxtipo();
                    pxt.pxt_empresa_key = comp.com_empresa;
                    pxt.pxt_persona_key = comp.com_codclipro ?? 0;
                    pxt.pxt_tipo_key = comp.com_tclipro ?? 0;
                    pxt = PersonaxtipoBLL.GetByPK(pxt);
                    Cuetransacc cuetransac = new Cuetransacc();
                    cuetransac.ctr_categoria_key = pxt.pxt_cat_persona ?? 0;
                    cuetransac.ctr_empresa_key = pxt.pxt_empresa;
                    cuetransac.ctr_transacc_key = doc2.ddo_transacc;
                    cuetransac = CuetransaccBLL.GetByPK(cuetransac);
                    doc2.ddo_cuenta = cuetransac.ctr_cuenta;
                    doc2.ddo_agente = comp.com_agente;
                    doc2.ddo_modulo = comp.com_modulo;
                    comp.documentos.Add(doc2);

                }


                //CREACION DE DBANCARIOS
                foreach (Dbancario item in comp.bancario)
                {
                    DbancarioBLL.Delete(transaction, item);
                }
                comp.bancario = BAN.getdbancario(comp, vdebcre_m);
                foreach (Dbancario item in comp.bancario)
                {
                    DbancarioBLL.Insert(transaction, item);
                }




                //CREACION DE DOCUMENTOS
                //   comp.documentos = CXCP.crear_documentos(comp, vdebcre);

                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    ////////////////
                    //     comp.contables.AddRange(CXCP.contables_detalle(comp, vdebcre_m));
                    //     comp.contables.AddRange(CXCP.contable_impuesto(comp, vdebcre_m));
                    //      comp.contables.AddRange(CXCP.contable_transporte(comp, vdebcre_m));
                    //      comp.contables.AddRange(CXCP.contable_seguro(comp, vdebcre_m));

                    /*     
                          comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre));
                          comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre_m));
                     */
                    //       comp.contables.AddRange(CXCP.contables_cancelaciones(comp));
                    //////////////////////////
                    //Auto.actualizar_saldo(transaction, comp, 1); 


                    comp.contables.AddRange(CXCP.contables_recibos(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contables_cancelaciones_recibo(comp));


                    List<Ddocumento> lst = comp.documentos;
                    List<Ddocumento> lst2 = new List<Ddocumento>();
                    lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
                    comp.documentos = lst2;
                    comp.contables.AddRange(CXCP.contables_documentos_recibos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos_recibos(comp, vdebcre_m));
                    //  comp.documentos = lst;                   
                    #endregion

                }

                #region Guarda Documentos y Contabilizaciones
                foreach (Ddocumento item in comp.documentos)
                {                    
                    DdocumentoBLL.Insert(transaction, item);
                }
                int sec = 0;
                foreach (Dcancelacion item in comp.cancelaciones)
                {
                    sec++;
                    item.dca_secuencia = sec;
                    DcancelacionBLL.Insert(transaction, item);
                }
                sec = 0;
                foreach (Dcontable item in comp.contables)
                {
                    sec++;
                    item.dco_secuencia = sec;
                    DcontableBLL.Insert(transaction, item);
                }
                #endregion
                General.save_historial(transaction, comp);
                if (Auto.actualizar_saldo(transaction, comp, 1))
                    transaction.Commit();
                else
                    throw new Exception("ERROR no se puede actualzar saldos");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

       

              


        public static Comprobante update_obligacion(Comprobante comp)
        {

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            Tipodoc tipo = TipodocBLL.GetByPK(new Tipodoc() { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });

            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.com_estado = Constantes.cEstadoGrabado;            
            objU.com_concepto = !string.IsNullOrEmpty(comp.com_concepto) ? comp.com_concepto : tipo.tpd_nombre + " " + comp.ccomdoc.cdoc_nombre + " FACTURA:" + comp.ccomdoc.cdoc_aut_factura;
            objU.com_tclipro = Constantes.cProveedor;
            objU.com_centro = Constantes.GetSinCentro().cen_codigo;


            Dtipocom dti = new Dtipocom();
            if (string.IsNullOrEmpty(objU.com_doctran))
            {
                objU.com_modulo = General.GetModulo(comp.com_tipodoc); ;
                objU.com_transacc = General.GetTransacc(comp.com_tipodoc);
                objU.com_descuadre = 0;
                objU.com_adestino = 0;
                objU.com_doctran = comp.com_doctran;
                objU.com_numero = comp.com_numero;
                dti = General.GetDtipocom(objU.com_empresa, objU.com_fecha.Year, objU.com_ctipocom, objU.com_almacen.Value, objU.com_pventa.Value);
                dti.dti_numero = dti.dti_numero + 1;

                if (objU.com_numero < dti.dti_numero)
                {
                    objU.com_numero = dti.dti_numero.Value;
                    objU.com_doctran = General.GetNumeroComprobante(objU);
                }

            }




            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {







                transaction.BeginTransaction();
                objU.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
                objU.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
                objU.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
                objU.ccomenv = CcomenvBLL.GetByPK(new Ccomenv { cenv_empresa = comp.com_empresa, cenv_empresa_key = comp.com_empresa, cenv_comprobante = comp.com_codigo, cenv_comprobante_key = comp.com_codigo });
                objU.planillacomp = PlanillacomprobanteBLL.GetByPK(new Planillacomprobante { pco_empresa = comp.com_empresa, pco_empresa_key = comp.com_empresa, pco_comprobante_fac = comp.com_codigo, pco_comprobante_fac_key = comp.com_codigo });


                int contador = 0;
                foreach (Dcomdoc item in objU.ccomdoc.detalle)
                {
                    item.ddoc_empresa_key = item.ddoc_empresa;
                    item.ddoc_comprobante_key = item.ddoc_comprobante;
                    item.ddoc_secuencia_key = item.ddoc_secuencia;
                    if (item.detallecalculo != null)
                        //LOOP INSER CADA DCALCULOPRECIO DE CADA DCOMDOC
                        foreach (Dcalculoprecio dc in item.detallecalculo)
                        {
                            dc.dcpr_empresa_key = dc.dcpr_empresa;
                            dc.dcpr_comprobante_key = dc.dcpr_comprobante;
                            dc.dcpr_dcomdoc_key = dc.dcpr_dcomdoc;
                            dc.dcpr_secuencia_key = dc.dcpr_secuencia;
                            DcalculoprecioBLL.Delete(transaction, dc);
                        }
                    DcomdocBLL.Delete(transaction, item);
                    contador++;
                }

                objU.ccomdoc.cdoc_comprobante_key = objU.ccomdoc.cdoc_comprobante;
                objU.ccomdoc.cdoc_empresa_key = objU.ccomdoc.cdoc_empresa;
                CcomdocBLL.Delete(transaction, objU.ccomdoc);

                objU.total.tot_comprobante_key = objU.total.tot_comprobante_key;
                objU.total.tot_empresa_key = objU.total.tot_empresa_key;
                TotalBLL.Delete(transaction, objU.total);

                objU.ccomenv.cenv_comprobante_key = objU.ccomenv.cenv_comprobante;
                objU.ccomenv.cenv_empresa_key = objU.ccomenv.cenv_empresa;
                CcomenvBLL.Delete(transaction, objU.ccomenv);


                ComprobanteBLL.Update(transaction, objU);// INSERT COMPROBANTE
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;   
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC



                // au.ape_retdato

                //    List<Autpersona> autorizaciones = Dictionaries.GetAutorizacionesPersona(comp.com_empresa, comp.com_codclipro.Value);
                /*List<Autpersona> autorizaciones = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_estado=1", comp.com_empresa, comp.com_codclipro.Value), "ape_val_fecha desc");
                Autpersona au = autorizaciones.Find(delegate(Autpersona d) { return d.ape_nro_autoriza == comp.ccomdoc.cdoc_acl_nroautoriza; });
                string aux = comp.ccomdoc.cdoc_aut_factura.Substring(comp.ccomdoc.cdoc_aut_factura.LastIndexOf("-") + 1);
                int num = (Int32)Conversiones.GetValueByType(aux, typeof(Int32));
                int desde = (Int32)Conversiones.GetValueByType(au.ape_fac3, typeof(Int32));
                int hasta = (Int32)Conversiones.GetValueByType(au.ape_fact3, typeof(Int32));
                if ((desde > num) || (hasta < num))
                    throw new Exception();
                */
                 contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }

                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL



                int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;


                objU.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
                foreach (Dcancelacion item in objU.cancelaciones)
                {
                    DcancelacionBLL.Delete(transaction, item);
                }

                //  decimal totalcancela = 0;
                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = Constantes.cCredito;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        contador++;
                    }
                }


                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////
                
                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                General.save_historial(transaction, objU);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante save_obligacion(Comprobante comp)
        {


            //DateTime fecha = DateTime.Now;

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            comp.com_numero = dti.dti_numero.Value;            

            Tipodoc tipo = TipodocBLL.GetByPK(new Tipodoc() { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });


            comp.com_concepto = tipo.tpd_nombre + " " + comp.ccomdoc.cdoc_nombre + " FACTURA:" + comp.ccomdoc.cdoc_aut_factura;
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            comp.com_tclipro = Constantes.cProveedor;
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC



                // au.ape_retdato

                //    List<Autpersona> autorizaciones = Dictionaries.GetAutorizacionesPersona(comp.com_empresa, comp.com_codclipro.Value);
                
                /*List<Autpersona> autorizaciones = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_estado=1", comp.com_empresa, comp.com_codclipro.Value), "ape_val_fecha desc");
                Autpersona au = autorizaciones.Find(delegate(Autpersona d) { return d.ape_nro_autoriza == comp.ccomdoc.cdoc_acl_nroautoriza; });
                string aux = comp.ccomdoc.cdoc_aut_factura.Substring(comp.ccomdoc.cdoc_aut_factura.LastIndexOf("-") + 1);
                int num = (Int32)Conversiones.GetValueByType(aux, typeof(Int32));
                int desde = (Int32)Conversiones.GetValueByType(au.ape_fac3, typeof(Int32));
                int hasta = (Int32)Conversiones.GetValueByType(au.ape_fact3, typeof(Int32));
                if ((desde > num) || (hasta < num))
                    throw new Exception("El numero no se encuentra en la autorización " + desde + " a " + hasta);*/
                

                int contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }

                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL


                int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo || comp.com_tipodoc == Constantes.cGuia.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;


                decimal totalcancela = 0;
                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = Constantes.cCredito;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        contador++;
                    }
                } 

                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////

                DtipocomBLL.Update(transaction, dti); //UPDATE DTIPOCOM
                General.save_historial(transaction, comp);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                comp.com_codigo = -1;
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }
        public static Comprobante create_obligacion(Comprobante comp)
        {


            //DateTime fecha = DateTime.Now;


            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            if (comp.com_numero == 0)
                comp.com_numero = dti.dti_numero.Value;


            Tipodoc tipo = TipodocBLL.GetByPK(new Tipodoc() { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });


            comp.com_concepto = tipo.tpd_nombre;
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            comp.com_tclipro = Constantes.cProveedor;
            comp.com_periodo = comp.com_fecha.Year;
            comp.com_mes = comp.com_fecha.Month;
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC



                // au.ape_retdato

                //    List<Autpersona> autorizaciones = Dictionaries.GetAutorizacionesPersona(comp.com_empresa, comp.com_codclipro.Value);

                /*List<Autpersona> autorizaciones = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_estado=1", comp.com_empresa, comp.com_codclipro.Value), "ape_val_fecha desc");
                Autpersona au = autorizaciones.Find(delegate(Autpersona d) { return d.ape_nro_autoriza == comp.ccomdoc.cdoc_acl_nroautoriza; });
                string aux = comp.ccomdoc.cdoc_aut_factura.Substring(comp.ccomdoc.cdoc_aut_factura.LastIndexOf("-") + 1);
                int num = (Int32)Conversiones.GetValueByType(aux, typeof(Int32));
                int desde = (Int32)Conversiones.GetValueByType(au.ape_fac3, typeof(Int32));
                int hasta = (Int32)Conversiones.GetValueByType(au.ape_fact3, typeof(Int32));
                if ((desde > num) || (hasta < num))
                    throw new Exception("El numero no se encuentra en la autorización " + desde + " a " + hasta);*/


                int contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    item.ddco_udigitada = item.ddco_udigitada == 0 ? 1 : item.ddco_udigitada;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }

                comp.total.tot_empresa = comp.com_empresa;
                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL

                General.UpdateDtipocom(transaction, dti, comp.com_numero);


                transaction.Commit();
            }
            catch (Exception ex)
            {
                comp.com_codigo = -1;
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante account_obligacion(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;

            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            int vdebcre = Constantes.cCredito;
            int vdebcre_m = Constantes.cDebito;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                #region Elimina documentos y contabilizaciones existentes

                foreach (Dcontable item in comp.contables)
                {
                    DcontableBLL.Delete(transaction, item);
                }
                comp.contables = new List<Dcontable>();
                foreach (Ddocumento item in comp.documentos)
                {
                    DdocumentoBLL.Delete(transaction, item);
                }
                comp.documentos = new List<Ddocumento>();
               
                foreach (Dcancelacion item in comp.cancelaciones)
                {
                    DcancelacionBLL.Delete(transaction, item);
                }
                comp.cancelaciones = new List<Dcancelacion>();
               


                comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
                 foreach (Dcancelacion dca in comp.cancelaciones)
                {

                    dca.dca_empresa_key = dca.dca_empresa;
                    dca.dca_comprobante_key = dca.dca_comprobante;
                    dca.dca_transacc_key = dca.dca_transacc;
                    dca.dca_doctran_key = dca.dca_doctran;
                    dca.dca_pago_key = dca.dca_pago;
                    dca.dca_comprobante_can_key = dca.dca_comprobante_can;
                    dca.dca_secuencia_key = dca.dca_secuencia;
                    DcancelacionBLL.Delete(transaction, dca);
                 }

                List<Ddocumento> docs = new List<Ddocumento>(); 
                decimal totalrecibo = 0;
                decimal totalcancela = 0;
                int contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_debcre == vdebcre)
                    {
                        dca.dca_monto_ext = dca.dca_monto;
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;                        
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA 
                        contador++;
                    }
                    Ddocumento doc = new Ddocumento();
                    doc.ddo_empresa = dca.dca_empresa;
                    doc.ddo_empresa_key = dca.dca_empresa;
                    doc.ddo_comprobante = dca.dca_comprobante;
                    doc.ddo_comprobante_key = dca.dca_comprobante;
                    doc.ddo_transacc = dca.dca_transacc;
                    doc.ddo_transacc_key = dca.dca_transacc;
                    doc.ddo_doctran = dca.dca_doctran;
                    doc.ddo_doctran_key = dca.dca_doctran;
                    doc.ddo_pago = dca.dca_pago;
                    doc.ddo_pago_key = dca.dca_pago;
                    doc = DdocumentoBLL.GetByPK(doc);
                    doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;
                    doc.ddo_cancela_ext = doc.ddo_cancela;
                    doc.ddo_monto_ext = doc.ddo_monto;
                    if (doc.ddo_cancela >= doc.ddo_monto)
                        doc.ddo_cancelado = 1;
                    totalcancela += dca.dca_monto.Value;
                    DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO  
                    docs.Add(doc);

                }

                 comp.total.tot_total = comp.total.tot_total - totalcancela;

                #endregion


                 //CREACION DE DOCUMENTOS                
                comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                

                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    ////////////////
                    comp.contables.AddRange(CXCP.contables_detalleobl(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_impuesto(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_ice(comp, vdebcre_m));
                    ////////////////////////                   
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contables_cancelaciones(comp, docs));
                    //////////////////////////
                    //Auto.actualizar_saldo(transaction, comp, 1); 

                    #endregion

                }

                #region Guarda Documentos y Contabilizaciones

                foreach (Ddocumento item in comp.documentos)                
                {                  

                    DdocumentoBLL.Insert(transaction, item);
                }
                int sec = 0;
                foreach (Dcontable item in comp.contables)
                {
                    sec++;
                    item.dco_secuencia = sec;
                    DcontableBLL.Insert(transaction, item);
                }


                #endregion
                General.save_historial(transaction, comp);
                if (Auto.actualizar_saldo(transaction, comp, 1))
                    transaction.Commit();
                else
                    throw new Exception("ERROR no se puede actualzar saldos");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

        public static Comprobante modifyGuia(Comprobante comp)
        {
            comp = General.AnulaComprobante(comp);
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            ComprobanteBLL.Update(comp);
            General.save_historial(comp);
            return comp;

        }

        public static Comprobante close_comprobanteplanilla(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            if (comp.com_estado != Constantes.cEstadoMayorizado)
            {
                List<Planillacli> lst = PlanillacliBLL.GetAll(new WhereParams("plc_comprobante={0} and plc_empresa={1}", comp.com_codigo, comp.com_empresa), "");
                if (lst.Count > 0)
                {
                    comp.com_estado = Constantes.cEstadoMayorizado;
                    comp.com_empresa_key = comp.com_empresa;
                    comp.com_codigo_key = comp.com_codigo;
                    ComprobanteBLL.Update(comp);
                    General.save_historial(comp);
                }
            }                              
            return comp;

        }


        public static Comprobante close_comprobantehojaruta(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            if (comp.com_estado != Constantes.cEstadoMayorizado)
            {
                //Verifica si esta en hoja de ruta mayorizada
                //List<vHojadeRuta> lst = vHojadeRutaBLL.GetAll(new WhereParams("detalle.com_codigo={0} and detalle.com_empresa={1} and cabecera.com_estado=2", comp.com_codigo, comp.com_empresa), "");                
                List<vHojadeRuta> lst = vHojadeRutaBLL.GetAll(new WhereParams("detalle.com_codigo={0} and detalle.com_empresa={1} ", comp.com_codigo, comp.com_empresa), "");
                if (lst.Count > 0)
                {
                    comp.com_estado = Constantes.cEstadoMayorizado;
                    comp.com_empresa_key = comp.com_empresa;
                    comp.com_codigo_key = comp.com_codigo;
                    ComprobanteBLL.Update(comp);
                    General.save_historial(comp);
                }
            }
            return comp;

        }



        public static int update_planilla(Comprobante comp)
        {                        
            Total total = TotalBLL.GetByPK(new Total { tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo, tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa });

            List<Planillacli> lst = PlanillacliBLL.GetAll(new WhereParams("plc_comprobante={0} and plc_empresa={1}",comp.com_codigo, comp.com_empresa),"");
            if (lst.Count > 0)
            {
                //Comprobante comppla = ComprobanteBLL.GetByPK(new Comprobante { com_codigo = lst[0].plc_comprobante_pla, com_codigo_key = lst[0].plc_comprobante_pla, com_empresa = comp.com_empresa, com_empresa_key = comp.com_empresa_key });
                Total totalpla = TotalBLL.GetByPK(new Total { tot_comprobante = lst[0].plc_comprobante_pla, tot_comprobante_key = lst[0].plc_comprobante_pla, tot_empresa = lst[0].plc_empresa, tot_empresa_key = lst[0].plc_empresa });
                totalpla.tot_total = totalpla.tot_total - total.tot_total + comp.total.tot_total;
                return TotalBLL.Update(totalpla);
            }
            return 0;
        }

        public static Comprobante getFacturaGuia(long codigo)
        {
            Comprobante fac = new Comprobante();
            fac.com_empresa_key = 1;
            fac.com_codigo_key = codigo;
            fac.com_empresa = 1;
            fac.com_codigo = codigo;
            fac = ComprobanteBLL.GetByPK(fac);

            fac.ccomdoc = new Ccomdoc();
            fac.ccomenv = new Ccomenv();
            fac.total = new Total();

            fac.ccomdoc.cdoc_comprobante = fac.com_codigo;
            fac.ccomdoc.cdoc_empresa = fac.com_empresa;
            fac.ccomdoc.cdoc_comprobante_key = fac.com_codigo;
            fac.ccomdoc.cdoc_empresa_key = fac.com_empresa;
            fac.ccomdoc = CcomdocBLL.GetByPK(fac.ccomdoc);

            fac.ccomenv.cenv_comprobante = fac.com_codigo;
            fac.ccomenv.cenv_empresa = fac.com_empresa;
            fac.ccomenv.cenv_comprobante_key = fac.com_codigo;
            fac.ccomenv.cenv_empresa_key = fac.com_empresa;
            fac.ccomenv = CcomenvBLL.GetByPK(fac.ccomenv);

            fac.total.tot_comprobante = fac.com_codigo;
            fac.total.tot_empresa = fac.com_empresa;
            fac.total.tot_comprobante_key = fac.com_codigo;
            fac.total.tot_empresa_key = fac.com_empresa;
            fac.total = TotalBLL.GetByPK(fac.total);

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");
            fac.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_comprobante = {0} and ddoc_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            return fac;
        }
        
        public static DataTable getFacturaDataTable(long codigo)
        {

            Comprobante fac = getFacturaGuia(codigo);
            return fac.ToDataTable();
        }


        #region Nota Credito Proveedor

        public static Comprobante update_notacreditopro(Comprobante comp)
        {

            Tipodoc tipo = TipodocBLL.GetByPK(new Tipodoc() { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;



            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.com_estado = Constantes.cEstadoGrabado;            
            objU.com_concepto = !string.IsNullOrEmpty(comp.com_concepto) ? comp.com_concepto : tipo.tpd_nombre + " " + comp.ccomdoc.cdoc_nombre;
            objU.com_tclipro = Constantes.cProveedor;
            objU.com_centro = Constantes.GetSinCentro().cen_codigo;

            Dtipocom dti = new Dtipocom();
            if (string.IsNullOrEmpty(objU.com_doctran))
            {
                objU.com_modulo = General.GetModulo(comp.com_tipodoc); ;
                objU.com_transacc = General.GetTransacc(comp.com_tipodoc);
                objU.com_descuadre = 0;
                objU.com_adestino = 0;
                objU.com_doctran = comp.com_doctran;
                objU.com_numero = comp.com_numero;
                dti = General.GetDtipocom(objU.com_empresa, objU.com_fecha.Year, objU.com_ctipocom, objU.com_almacen.Value, objU.com_pventa.Value);
                dti.dti_numero = dti.dti_numero + 1;

                if (objU.com_numero < dti.dti_numero)
                {
                    objU.com_numero = dti.dti_numero.Value;
                    objU.com_doctran = General.GetNumeroComprobante(objU);
                }

            }


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();
                objU.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
                objU.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
                objU.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
                objU.ccomenv = CcomenvBLL.GetByPK(new Ccomenv { cenv_empresa = comp.com_empresa, cenv_empresa_key = comp.com_empresa, cenv_comprobante = comp.com_codigo, cenv_comprobante_key = comp.com_codigo });
                objU.planillacomp = PlanillacomprobanteBLL.GetByPK(new Planillacomprobante { pco_empresa = comp.com_empresa, pco_empresa_key = comp.com_empresa, pco_comprobante_fac = comp.com_codigo, pco_comprobante_fac_key = comp.com_codigo });


                int contador = 0;
                foreach (Dcomdoc item in objU.ccomdoc.detalle)
                {
                    item.ddoc_empresa_key = item.ddoc_empresa;
                    item.ddoc_comprobante_key = item.ddoc_comprobante;
                    item.ddoc_secuencia_key = item.ddoc_secuencia;
                    if (item.detallecalculo != null)
                        //LOOP INSER CADA DCALCULOPRECIO DE CADA DCOMDOC
                        foreach (Dcalculoprecio dc in item.detallecalculo)
                        {
                            dc.dcpr_empresa_key = dc.dcpr_empresa;
                            dc.dcpr_comprobante_key = dc.dcpr_comprobante;
                            dc.dcpr_dcomdoc_key = dc.dcpr_dcomdoc;
                            dc.dcpr_secuencia_key = dc.dcpr_secuencia;
                            DcalculoprecioBLL.Delete(transaction, dc);
                        }
                    DcomdocBLL.Delete(transaction, item);
                    contador++;
                }

                objU.ccomdoc.cdoc_comprobante_key = objU.ccomdoc.cdoc_comprobante;
                objU.ccomdoc.cdoc_empresa_key = objU.ccomdoc.cdoc_empresa;
                CcomdocBLL.Delete(transaction, objU.ccomdoc);

                objU.total.tot_comprobante_key = objU.total.tot_comprobante_key;
                objU.total.tot_empresa_key = objU.total.tot_empresa_key;
                TotalBLL.Delete(transaction, objU.total);

                objU.ccomenv.cenv_comprobante_key = objU.ccomenv.cenv_comprobante;
                objU.ccomenv.cenv_empresa_key = objU.ccomenv.cenv_empresa;
                CcomenvBLL.Delete(transaction, objU.ccomenv);


                ComprobanteBLL.Update(transaction, objU);// INSERT COMPROBANTE
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC



                contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }

                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL

                int vdebcre = (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;
                

                objU.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
                foreach (Dcancelacion item in objU.cancelaciones)
                {
                    DcancelacionBLL.Delete(transaction, item);
                }

                //  decimal totalcancela = 0;
                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = vdebcre;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        contador++;
                    }                    
                }


                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////

                //    DtipocomBLL.Update(transaction, dti); //UPDATE DTIPOCOM

                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE

                General.save_historial(transaction, objU);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante save_notacreditopro(Comprobante comp)
        {


            //DateTime fecha = DateTime.Now;

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;            
            comp.com_numero = dti.dti_numero.Value;

            Tipodoc tipo = TipodocBLL.GetByPK(new Tipodoc() { tpd_codigo = comp.com_tipodoc, tpd_codigo_key = comp.com_tipodoc });


            comp.com_concepto = tipo.tpd_nombre + " " + comp.ccomdoc.cdoc_nombre;
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            comp.com_tclipro = Constantes.cProveedor;
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE
                comp.ccomdoc.cdoc_empresa = comp.com_empresa;
                comp.ccomdoc.cdoc_comprobante = comp.com_codigo;
                CcomdocBLL.Insert(transaction, comp.ccomdoc); //INSERT CCOMDOC
             
                int contador = 0;
                //LOOP INSERT CADA DCOMDOC
                foreach (Dcomdoc item in comp.ccomdoc.detalle)
                {
                    item.ddoc_empresa = comp.com_empresa;
                    item.ddoc_comprobante = comp.com_codigo;
                    item.ddoc_secuencia = contador;
                    DcomdocBLL.Insert(transaction, item);
                    contador++;
                }

                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL


                int vdebcre = (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;


                decimal totalcancela = 0;
                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = vdebcre;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        contador++;
                    }
                }

                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////

                DtipocomBLL.Update(transaction, dti); //UPDATE DTIPOCOM

                General.save_historial(transaction, comp);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                comp.com_codigo = -1;
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante account_notacreditopro(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;

            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            int vdebcre = Constantes.cDebito;
            int vdebcre_m = Constantes.cCredito;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                #region Elimina documentos y contabilizaciones existentes

                foreach (Dcontable item in comp.contables)
                {
                    DcontableBLL.Delete(transaction, item);
                }
                comp.contables = new List<Dcontable>();
                foreach (Ddocumento item in comp.documentos)
                {
                    DdocumentoBLL.Delete(transaction, item);
                }
                comp.documentos = new List<Ddocumento>();

                foreach (Dcancelacion item in comp.cancelaciones)
                {
                    DcancelacionBLL.Delete(transaction, item);
                }
                comp.cancelaciones = new List<Dcancelacion>();



                comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
                foreach (Dcancelacion dca in comp.cancelaciones)
                {

                    dca.dca_empresa_key = dca.dca_empresa;
                    dca.dca_comprobante_key = dca.dca_comprobante;
                    dca.dca_transacc_key = dca.dca_transacc;
                    dca.dca_doctran_key = dca.dca_doctran;
                    dca.dca_pago_key = dca.dca_pago;
                    dca.dca_comprobante_can_key = dca.dca_comprobante_can;
                    dca.dca_secuencia_key = dca.dca_secuencia;
                    DcancelacionBLL.Delete(transaction, dca);
                }

                List<Ddocumento> docs = new List<Ddocumento>();
                decimal totalrecibo = 0;
                decimal totalcancela = 0;
                int contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_debcre == vdebcre)
                    {
                        dca.dca_monto_ext = dca.dca_monto;
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA 
                        contador++;
                    }
                    Ddocumento doc = new Ddocumento();
                    doc.ddo_empresa = dca.dca_empresa;
                    doc.ddo_empresa_key = dca.dca_empresa;
                    doc.ddo_comprobante = dca.dca_comprobante;
                    doc.ddo_comprobante_key = dca.dca_comprobante;
                    doc.ddo_transacc = dca.dca_transacc;
                    doc.ddo_transacc_key = dca.dca_transacc;
                    doc.ddo_doctran = dca.dca_doctran;
                    doc.ddo_doctran_key = dca.dca_doctran;
                    doc.ddo_pago = dca.dca_pago;
                    doc.ddo_pago_key = dca.dca_pago;
                    doc = DdocumentoBLL.GetByPK(doc);
                    doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;
                    doc.ddo_cancela_ext = doc.ddo_cancela;
                    doc.ddo_monto_ext = doc.ddo_monto;
                    if (doc.ddo_cancela >= doc.ddo_monto)
                        doc.ddo_cancelado = 1;
                    totalcancela += dca.dca_monto.Value;
                    DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO  
                    docs.Add(doc);

                }

                comp.total.tot_total = comp.total.tot_total - totalcancela;

                #endregion


                //CREACION DE DOCUMENTOS                
                comp.documentos = CXCP.crear_documentos(comp, vdebcre);


                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    ////////////////
                    comp.contables.AddRange(CXCP.contables_detalleobl(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_impuesto(comp, vdebcre_m));
                    ////////////////////////                   
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contables_cancelaciones(comp, docs));
                    //////////////////////////
                    //Auto.actualizar_saldo(transaction, comp, 1); 

                    #endregion

                }

                #region Guarda Documentos y Contabilizaciones

                foreach (Ddocumento item in comp.documentos)
                {

                    DdocumentoBLL.Insert(transaction, item);
                }
                int sec = 0;
                foreach (Dcontable item in comp.contables)
                {
                    sec++;
                    item.dco_secuencia = sec;
                    DcontableBLL.Insert(transaction, item);
                }


                #endregion
                General.save_historial(transaction, comp);
                if (Auto.actualizar_saldo(transaction, comp, 1))
                    transaction.Commit();
                else
                    throw new Exception("ERROR no se puede actualzar saldos");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

        #endregion
    }
}

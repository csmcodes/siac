using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;


namespace Packages
{
    public class BAN
    {
        public BAN()
        {

        }

        public static List<Dbancario> getdbancario(Comprobante comp, int debcr)
        {
            int secuencia = 0;
            List<Dbancario> lst = new List<Dbancario>();
            foreach (Drecibo rec in comp.recibos)
            {
                
                
                Tipopago tp = new Tipopago();
                tp.tpa_empresa_key = rec.dfp_empresa;
                tp.tpa_codigo_key = rec.dfp_tipopago;
                tp = TipopagoBLL.GetByPK(tp);
                tp.tpa_empresa = tp.tpa_empresa;
                tp.tpa_codigo_key = tp.tpa_codigo;
                if (tp.tpa_contabiliza == 4 || (tp.tpa_contabiliza ==2 &&  tp.tpa_transacc==4))
                {

                    Banco ban = new Banco();
                    ban.ban_codigo = rec.dfp_banco.Value;
                    ban.ban_codigo_key = rec.dfp_banco.Value;
                    ban.ban_empresa = comp.com_empresa;
                    ban.ban_empresa_key = comp.com_empresa_key;
                    ban = BancoBLL.GetByPK(ban);
                    Dbancario dban = new Dbancario();
                    dban.dban_empresa = comp.com_empresa;
                    dban.dban_cco_comproba = comp.com_codigo;
                    dban.dban_secuencia = secuencia;
                    dban.dban_banco = rec.dfp_banco.Value;
                    dban.dban_transacc = (tp.tpa_contabiliza == 4) ? 1 : 4;//PARCHE
                    dban.dban_documento = rec.dfp_nro_cheque;
                    dban.dban_cliente = comp.com_codclipro;
                    dban.dban_beneficiario = rec.dfp_beneficiario;
                    dban.dban_valor_nac = rec.dfp_monto;
                    dban.dban_valor_ext = rec.dfp_monto_ext;
                    dban.dban_conciliacion = 0;
                    dban.dban_debcre = debcr;
                    //dban.dban_impreso
                    //dban.dban_cruzado

                    dban.crea_usr = comp.crea_usr;
                    dban.crea_fecha = DateTime.Now;
                    

                    lst.Add(dban);
                    secuencia++;
                }
            }
            return lst;
        }


        public static Comprobante update_bancario(Comprobante comp)
        {
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
                objU.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
                objU.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
                
                int contador = 0;
                foreach (Dbancario item in objU.bancario)
                {
                    item.dban_cco_comproba_key = item.dban_cco_comproba;
                    item.dban_secuencia_key = item.dban_secuencia;
                    item.dban_empresa_key = item.dban_empresa;
                    DbancarioBLL.Delete(transaction, item);
                
                }

                foreach (Dcontable item in objU.contables)
                {                    
                    DcontableBLL.Delete(transaction, item);

                }


                             
                ComprobanteBLL.Update(transaction, objU);
               
                //LOOP INSERT CADA DCOMDOC
                decimal totalrecibo = 0;
                contador = 0;
                foreach (Dbancario item in comp.bancario)
                {
                    item.dban_cco_comproba = comp.com_codigo;
                    item.dban_secuencia = contador;
                    item.dban_transacc = comp.com_transacc;                   
                    totalrecibo += item.dban_valor_nac;
                    DbancarioBLL.Insert(transaction, item);
                    contador++;
                }

                contador = 0;
                foreach (Dcontable item in comp.contables)
                {
                    item.dco_empresa = comp.com_empresa;
                    item.dco_comprobante = comp.com_codigo;
                    item.dco_secuencia = contador;
                    DcontableBLL.Insert(transaction, item);
                    contador++;
                }
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
        public static Comprobante save_bancario(Comprobante comp)
        {
            Comprobante hr = new Comprobante();
            //DateTime fecha = DateTime.Now;
            #region Actualiza el numero de comprobante en 1

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            comp.com_numero = dti.dti_numero.Value;

            
            #endregion
            

            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            //comp.com_tclipro = Constantes.cCliente;
            //obj.com_fecha = fecha;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE
                
                
                decimal totalrecibo = 0;
                int contador = 0;
                foreach (Dbancario item in comp.bancario)
                {
                    item.dban_cco_comproba = comp.com_codigo;
                    item.dban_secuencia = contador;
                    item.dban_transacc = comp.com_transacc;
                    // item.deb = debcredoc;
                    totalrecibo += item.dban_valor_nac;
                    DbancarioBLL.Insert(transaction, item);
                    contador++;
                }

                contador = 0;
                foreach (Dcontable item in comp.contables)
                {
                    item.dco_empresa = comp.com_empresa;
                    item.dco_comprobante = comp.com_codigo;
                    item.dco_secuencia = contador;
                    DcontableBLL.Insert(transaction, item);
                    contador++;
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
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

       
        public static Comprobante account_bancario(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            List<Dcontable> contables = comp.contables;
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

            int vdebcre = 0;
            int vdebcre_m = 0;
            if (comp.com_tipodoc == Constantes.cDeposito.tpd_codigo)
            {
                vdebcre = Constantes.cDebito;
                vdebcre_m = Constantes.cCredito;
            }
            if (comp.com_tipodoc == Constantes.cNotaCreditoBan.tpd_codigo)
            {
                vdebcre = Constantes.cDebito;
                vdebcre_m = Constantes.cCredito;
            }
            if (comp.com_tipodoc == Constantes.cNotaDebitoBan.tpd_codigo)
            {
                vdebcre = Constantes.cCredito;
                vdebcre_m = Constantes.cDebito;

            }
            if (comp.com_tipodoc == Constantes.cPagoBan.tpd_codigo)
            {
                vdebcre = Constantes.cCredito;
                vdebcre_m = Constantes.cDebito;
            }

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
                comp.contables = contables;
                foreach (Ddocumento item in comp.documentos)
                {
                    DdocumentoBLL.Delete(transaction, item);
                }
                comp.documentos = new List<Ddocumento>();

                #endregion

                //CREACION DE DOCUMENTOS
                //  comp.documentos = CXCP.crear_documentos(comp, vdebcre);            

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
                    item.dco_comprobante = comp.com_codigo;
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



        public static Comprobante update_notacredeb(Comprobante comp)
        {

            string Doc = "";
            switch (comp.com_tipo_doc)
            {
                case "NOTACRE":
                    Doc = "NOTA CREDITO ";
                    break;
                case "NOTDEB":
                    Doc = "NOTA DEBITO ";
                    break;
                case "NOTACRPROV":
                    Doc = "NOTA CREDITO ";
                    break;
                case "NOTADEBPRO":
                    Doc = "NOTA DEBITO ";
                    break;

            }
            comp.com_concepto = (!string.IsNullOrEmpty(comp.com_concepto)) ? comp.com_concepto : Doc + " " + comp.ccomdoc.cdoc_nombre;

            //DateTime fecha = DateTime.Now;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.com_estado = Constantes.cEstadoGrabado;
            objU.com_centro = Constantes.GetSinCentro().cen_codigo;
            objU.com_concepto = comp.com_concepto;
            switch (comp.com_tipo_doc)
            {
                case "NOTACRE":
                    objU.com_tclipro = Constantes.cCliente;
                    Doc = "NOTA CREDITO ";
                    break;
                case "NOTDEB":
                    objU.com_tclipro = Constantes.cCliente;
                    Doc = "NOTA DEBITO ";
                    break;
                case "NOTACRPROV":
                    objU.com_tclipro = Constantes.cProveedor;
                    Doc = "NOTA CREDITO ";
                    break;
                case "NOTADEBPRO":
                    objU.com_tclipro = Constantes.cProveedor;
                    Doc = "NOTA DEBITO ";
                    break;

            }





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
                objU.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_empresa ={0} and dnc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
                objU.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");

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


                foreach (Dnotacre item in objU.notascre)
                {
                    item.dnc_comprobante_key = item.dnc_comprobante;
                    item.dnc_empresa_key = item.dnc_empresa;
                    item.dnc_secuencia_key = item.dnc_secuencia;
                    DnotacreBLL.Delete(transaction,item);
                }

                foreach (Dcancelacion item in objU.cancelaciones)
                {
                    item.dca_empresa_key = item.dca_empresa;
                    item.dca_comprobante_key = item.dca_comprobante;
                    item.dca_transacc_key = item.dca_transacc;
                    item.dca_doctran_key = item.dca_doctran;
                    item.dca_pago_key = item.dca_pago;
                    item.dca_comprobante_can_key = item.dca_comprobante_can;
                    item.dca_secuencia_key = item.dca_secuencia;
                    DcancelacionBLL.Delete(transaction, item);
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


                //LOOP INSERT CADA DCOMDOC

               contador = 0;
                foreach (Dnotacre item in comp.notascre)
                {
                    item.dnc_comprobante = comp.com_codigo;
                    item.dnc_secuencia = contador;
                    // item.deb = debcredoc;

                    DnotacreBLL.Insert(transaction, item);
                    contador++;
                }


                ////////////////////////////////////////////////////////////////
                int vdebcre = 0;
                int vdebcre_m = 0;
                if (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    vdebcre = Constantes.cDebito;
                    vdebcre_m = Constantes.cCredito;
                }
                if (comp.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {

                    vdebcre = Constantes.cCredito;
                    vdebcre_m = Constantes.cDebito;
                }


                if (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                {
                    vdebcre = Constantes.cCredito;
                    vdebcre_m = Constantes.cDebito;
                }
                if (comp.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
                {
                    vdebcre = Constantes.cDebito;
                    vdebcre_m = Constantes.cCredito;
                }



                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = vdebcre_m;
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
                comp.total.tot_comprobante = comp.com_codigo;
                //obj.total.tot_impuesto =

                TotalBLL.Insert(transaction, comp.total);
                //   DtipocomBLL.Update(transaction, dti); //UPDATE DTIPOCOM

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



        public static Comprobante save_notacredeb1(Comprobante comp)
        {

            string Doc = "NOTA CREDITO";
            comp.com_tclipro = Constantes.cCliente;
            comp.com_concepto = (!string.IsNullOrEmpty(comp.com_concepto)) ? comp.com_concepto : Doc + " " + comp.ccomdoc.cdoc_nombre;
            comp.com_modulo = General.GetModulo(comp.com_tipodoc);
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            //    
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


                //LOOP INSERT CADA DCOMDOC

                int contador = 0;
                foreach (Dnotacre item in comp.notascre)
                {
                    item.dnc_comprobante = comp.com_codigo;
                    item.dnc_secuencia = contador;
                    // item.deb = debcredoc;

                    DnotacreBLL.Insert(transaction, item);
                    contador++;
                }


                ////////////////////////////////////////////////////////////////
                int vdebcre = 0;
                int vdebcre_m = 0;
                if (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    vdebcre = Constantes.cDebito;
                    vdebcre_m = Constantes.cCredito;
                }
                if (comp.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {

                    vdebcre = Constantes.cCredito;
                    vdebcre_m = Constantes.cDebito;
                }


                if (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                {
                    vdebcre = Constantes.cCredito;
                    vdebcre_m = Constantes.cDebito;
                }
                if (comp.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
                {
                    vdebcre = Constantes.cDebito;
                    vdebcre_m = Constantes.cCredito;
                }



                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = vdebcre_m;
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
                ///
                comp.total.tot_empresa = comp.com_empresa;
                comp.total.tot_comprobante = comp.com_codigo;
                //obj.total.tot_impuesto =

                TotalBLL.Insert(transaction, comp.total);               
                //General.save_historial(transaction, comp);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }



        public static Comprobante save_notacredeb(Comprobante comp)
        {

            Comprobante hr = new Comprobante();



            //DateTime fecha = DateTime.Now;

            #region Actualiza el numero de comprobante en 1

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            comp.com_numero = dti.dti_numero.Value;


            #endregion

            
            string Doc="";
            switch (comp.com_tipo_doc)
            {
                case "NOTACRE":
                    comp.com_tclipro = Constantes.cCliente;
                    Doc = "NOTA CREDITO ";
                    break;
                case "NOTDEB":
                    comp.com_tclipro = Constantes.cCliente;
                    Doc = "NOTA DEBITO ";
                    break;
                case "NOTACRPROV":
                    comp.com_tclipro = Constantes.cProveedor;
                    Doc = "NOTA CREDITO ";
                    break;
                case "NOTADEBPRO":
                    comp.com_tclipro = Constantes.cProveedor;
                    Doc = "NOTA DEBITO ";
                    break;

    }


            comp.com_concepto = (!string.IsNullOrEmpty(comp.com_concepto))?comp.com_concepto:Doc+" "+comp.ccomdoc.cdoc_nombre;
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); 
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
        //    
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


                //LOOP INSERT CADA DCOMDOC

                int contador = 0;
                foreach (Dnotacre item in comp.notascre)
                {
                    item.dnc_comprobante = comp.com_codigo;
                    item.dnc_secuencia = contador;
                    // item.deb = debcredoc;

                    DnotacreBLL.Insert(transaction, item);
                    contador++;
                }

                
                ////////////////////////////////////////////////////////////////
                int vdebcre = 0;
                int vdebcre_m = 0;
                if (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    vdebcre = Constantes.cDebito;
                    vdebcre_m = Constantes.cCredito;
                }
                if (comp.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {
                   
                    vdebcre = Constantes.cCredito;
                    vdebcre_m = Constantes.cDebito;
                }


                if (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
                {
                    vdebcre = Constantes.cCredito;
                    vdebcre_m = Constantes.cDebito;
                }
                if (comp.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
                {
                    vdebcre = Constantes.cDebito;
                    vdebcre_m = Constantes.cCredito;
                }


            
                contador = 0;
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;
                        dca.dca_debcre = vdebcre_m;
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
                comp.total.tot_comprobante = comp.com_codigo;
                //obj.total.tot_impuesto =

                TotalBLL.Insert(transaction, comp.total);
                DtipocomBLL.Update(transaction, dti); //UPDATE DTIPOCOM
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

        /*
        public static Comprobante account_notacredeb(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            List<Dcancelacion> cancelaciones = comp.cancelaciones;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_empresa ={0} and dnc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = cancelaciones;


            int vdebcre = (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;
            int vdebcre_m = (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;




            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                decimal totalrecibo = 0;
                foreach (Dnotacre item in comp.notascre)
                { totalrecibo += item.dnc_valor ?? 0; }


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

                int contador = 0;
                decimal totalcancela = 0;
                List<Dcancelacion> cancelaciones2 = new List<Dcancelacion>();
                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    if (dca.dca_monto > 0)
                    {
                        dca.dca_monto_ext = dca.dca_monto;
                        dca.dca_comprobante_can = comp.com_codigo;
                        dca.dca_transacc_can = comp.com_transacc;
                        dca.dca_secuencia = contador;


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
                        dca.dca_debcre = (doc.ddo_debcre == Constantes.cCredito) ? Constantes.cDebito : Constantes.cCredito;
                        DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                        cancelaciones2.Add(dca);
                  //      comp.cancelaciones.Add(dca);
                        contador++;
                        
                        DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                        comp.documentos.Add(doc);
                    }
                    //
                }
                comp.cancelaciones = cancelaciones2;
                if (comp.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
                {
                    if (totalrecibo > totalcancela)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                    {
                        totalrecibo = totalrecibo - totalcancela;
                        List<Ddocumento> lst3 = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa = {0} and ddo_comprobante = {1}", comp.com_empresa, comp.ccomdoc.cdoc_factura), "");
                        int debcre = Constantes.cDebito;
                        if (lst3.Count > 0)
                        {
                            Ddocumento item = lst3.ElementAt(0);
                            debcre = (item.ddo_debcre == Constantes.cCredito) ? Constantes.cCredito : Constantes.cDebito;
                        }

                        Ddocumento doc = new Ddocumento();
                        doc.ddo_empresa = comp.com_empresa;
                        doc.ddo_comprobante = comp.com_codigo;
                        doc.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                        doc.ddo_doctran = comp.com_doctran;
                        doc.ddo_pago = 1;
                        doc.ddo_codclipro = comp.com_codclipro;
                        doc.ddo_debcre = debcre;
                        //doc.ddo_tipo_cambio = 
                        doc.ddo_fecha_emi = comp.com_fecha;
                        doc.ddo_fecha_ven = DateTime.Now;
                        doc.ddo_monto = totalrecibo;
                        //doc.ddo_monto_ext = 
                        doc.ddo_cancela = 0;
                        //doc.ddo_cancela_ext =
                        doc.ddo_cancelado = 0;
                        doc.ddo_agente = comp.com_agente;
                        //doc.ddo_cuenta = 
                        doc.ddo_modulo = comp.com_modulo;                        
                        DdocumentoBLL.Insert(transaction, doc);
                        comp.documentos.Add(doc);

                    }
                }
                else if (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo)
                {
                    if (totalrecibo > totalcancela)//CREA UN DOCUMENTO LIGADO AL RECIBO EN CASO DE EXISTIR VALORES A FAVOR
                    {
                        totalrecibo = totalrecibo - totalcancela;
                        decimal valordoc = totalrecibo - totalcancela;
                        List<Ddocumento> lst3 = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa = {0} and ddo_comprobante = {1}", comp.com_empresa, comp.ccomdoc.cdoc_factura), "");
                        foreach (Ddocumento item in lst3)
                        {
                            //  DdocumentoBLL.Delete(transaction, item);
                            List<Dcancelacion> lst2 = DcancelacionBLL.GetAll(new WhereParams("dca_empresa = {0} and dca_comprobante_can = {1} and dca_comprobante={2}", item.ddo_empresa, comp.com_codigo, item.ddo_comprobante), "");
                            foreach (Dcancelacion item2 in lst2)
                            {
                                item.ddo_cancela = ((item.ddo_cancela.HasValue) ? item.ddo_cancela.Value : 0) - item2.dca_monto;
                                if (item.ddo_monto >= item.ddo_cancela)
                                    item.ddo_cancelado = 0;
                                DcancelacionBLL.Delete(transaction, item2);
                            }
                            Dcancelacion dca = new Dcancelacion();
                            //dca.dca_empresa = doc.ddo_empresa;
                            //dca.dca_comprobante = doc.ddo_comprobante;
                            dca.dca_empresa = comp.com_empresa;
                            dca.dca_comprobante = item.ddo_comprobante;
                            dca.dca_transacc = item.ddo_transacc;
                            dca.dca_doctran = item.ddo_doctran;
                            dca.dca_pago = item.ddo_pago;
                            dca.dca_comprobante_can = comp.com_codigo;
                            dca.dca_secuencia = 0;
                            dca.dca_debcre = (item.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                            dca.dca_transacc_can = comp.com_transacc;
                            dca.dca_tipo_cambio = item.ddo_tipo_cambio;
                            if (totalrecibo > item.ddo_monto.Value)
                            {
                                dca.dca_monto = item.ddo_monto;
                                dca.dca_monto_ext = item.ddo_monto;
                            }
                            else
                            {
                                dca.dca_monto = totalrecibo;
                                dca.dca_monto_ext = totalrecibo;
                            }
                            DcancelacionBLL.Insert(transaction, dca);
                            comp.cancelaciones.Add(dca);
                            item.ddo_empresa_key = item.ddo_empresa;
                            item.ddo_comprobante_key = item.ddo_comprobante;
                            item.ddo_transacc_key = item.ddo_transacc;
                            item.ddo_doctran_key = item.ddo_doctran;
                            item.ddo_pago_key = item.ddo_pago;
                            item.ddo_cancela = ((item.ddo_cancela.HasValue) ? item.ddo_cancela.Value : 0) + dca.dca_monto;
                            if (item.ddo_cancela >= item.ddo_monto)
                                item.ddo_cancelado = 1;                      
                            DdocumentoBLL.Update(transaction, item);// ACTUALIZA EL DOCUMENTO CANCELANDOLO  

                            comp.documentos.Add(item);
                            if (totalrecibo > item.ddo_monto.Value)
                                totalrecibo = totalrecibo - item.ddo_monto.Value;
                            else
                                break;
                        }


                    }
                }

                //CREACION DE DOCUMENTOS
                //   comp.documentos = CXCP.crear_documentos(comp, vdebcre);

                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    ////////////////
                    comp.contables.AddRange(CXCP.contables_detalle(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_impuesto(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_transporte(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_seguro(comp, vdebcre_m));


                    comp.contables.AddRange(CXCP.contables_notascredeb(comp));
                    
                    ////////////////////////    
                    
                    
                        List<Ddocumento> lst = comp.documentos;
                        List<Ddocumento> lst2 = new List<Ddocumento>();
                        lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
                            
                      
                    comp.documentos = lst2;
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre_m));
                    comp.documentos = lst;
                    
                    comp.contables.AddRange(CXCP.contables_cancelaciones(comp));
                    //////////////////////////
                    //Auto.actualizar_saldo(transaction, comp, 1); 

                    #endregion

                }

                #region Guarda Documentos y Contabilizaciones

               //      foreach (Ddocumento item in comp.documentos)
             //   {
             //       DdocumentoBLL.Insert(transaction, item);
             //   }
            
                int sec = 0;
                foreach (Dcontable item in comp.contables)
                {
                    sec++;
                    item.dco_secuencia = sec;
                    DcontableBLL.Insert(transaction, item);
                }


                #endregion

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

 
        */
        public static Comprobante account_notacredeb(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
        //    List<Dcancelacion> cancelaciones = comp.cancelaciones;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            comp.ccomdoc.detalle = DcomdocBLL.GetAll(new WhereParams("ddoc_empresa ={0} and ddoc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_empresa ={0} and dnc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
          /*  int vdebcre = (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;
            int vdebcre_m = (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;

            */
            int vdebcre = 0;
            int vdebcre_m = 0;
            if (comp.com_tipodoc == Constantes.cNotacre.tpd_codigo)
            {
                vdebcre =Constantes.cCredito;
                vdebcre_m = Constantes.cDebito;
            }
            if (comp.com_tipodoc == Constantes.cNotadeb.tpd_codigo)
            {

                vdebcre = Constantes.cDebito;
                vdebcre_m = Constantes.cCredito;
            }


            if (comp.com_tipodoc == Constantes.cNotacrePro.tpd_codigo)
            {
                vdebcre = Constantes.cDebito;
                vdebcre_m = Constantes.cCredito;
            }
            if (comp.com_tipodoc == Constantes.cNotadebPro.tpd_codigo)
            {
                vdebcre = Constantes.cCredito;
                vdebcre_m = Constantes.cDebito;
            }



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

                //foreach (Dcancelacion item in comp.cancelaciones)
                //{
                //    DcancelacionBLL.Delete(transaction, item);
                //}
                //comp.cancelaciones = new List<Dcancelacion>();
                #endregion

                //comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");

                
                Comprobante obj = new Comprobante();
                obj.com_empresa_key = comp.com_empresa;
                obj.com_codigo_key = comp.ccomdoc.cdoc_factura.Value;
                obj = ComprobanteBLL.GetByPK(obj);
                obj.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa = {0} and ddo_comprobante = {1}", comp.com_empresa, comp.ccomdoc.cdoc_factura), "");

                #region Elimina cancelaciones y actualiza documentos

                foreach (Ddocumento ddo in obj.documentos)
                {
                    List<Dcancelacion> lstdca = comp.cancelaciones.FindAll(delegate (Dcancelacion dc) { return dc.dca_empresa == ddo.ddo_empresa && dc.dca_comprobante == ddo.ddo_comprobante && dc.dca_transacc == ddo.ddo_transacc && dc.dca_doctran == ddo.ddo_doctran && dc.dca_pago == ddo.ddo_pago; });
                    ddo.ddo_cancela = ddo.ddo_cancela - lstdca.Sum(s => s.dca_monto.Value);
                    if (ddo.ddo_cancela < ddo.ddo_monto)
                    {
                        ddo.ddo_cancelado = 0;
                        if (ddo.ddo_cancela < 0)
                            ddo.ddo_cancela = 0;
                    }
                    ddo.ddo_empresa_key = ddo.ddo_empresa;
                    ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                    ddo.ddo_transacc_key = ddo.ddo_transacc;
                    ddo.ddo_doctran_key = ddo.ddo_doctran;
                    ddo.ddo_pago_key = ddo.ddo_pago;
                    DdocumentoBLL.Update(transaction, ddo);
                }


                //foreach (Dcancelacion dca in comp.cancelaciones)
                //{
                //    DcancelacionBLL.Delete(transaction, dca);
                //}
                //comp.cancelaciones = new List<Dcancelacion>();

                #endregion
                decimal totalcancela = 0;
                decimal totalnc = comp.total.tot_total;
                int secuencia = 0;
                decimal valor = totalnc - totalcancela;


                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    Ddocumento ddo = obj.documentos.Find(delegate (Ddocumento d) { return d.ddo_empresa == dca.dca_empresa && d.ddo_comprobante == dca.dca_comprobante && d.ddo_transacc == dca.dca_transacc && d.ddo_doctran == dca.dca_doctran && d.ddo_pago == dca.dca_pago; });
                    if (ddo != null)
                    {
                        
                        ddo.ddo_empresa_key = ddo.ddo_empresa;
                        ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                        ddo.ddo_transacc_key = ddo.ddo_transacc;
                        ddo.ddo_doctran_key = ddo.ddo_doctran;
                        ddo.ddo_pago_key = ddo.ddo_pago;
                        ddo.ddo_cancela = (ddo.ddo_cancela ?? 0) + dca.dca_monto;
                        if (ddo.ddo_cancela >= ddo.ddo_monto)
                        {
                            ddo.ddo_cancelado = 1;
                            if (ddo.ddo_cancela > ddo.ddo_monto)
                                ddo.ddo_cancela = ddo.ddo_monto;
                        }
                        DdocumentoBLL.Update(transaction, ddo);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                        comp.documentos.Add(ddo);
                        totalcancela += dca.dca_monto.Value;
                        valor -= totalcancela;
                    }

                }



                  

                

                #region Crea documento a favor en caso de no afectar deudas


                if (totalnc> totalcancela)//CREA UN DOCUMENTO LIGADO A LA RETENCIÓN EN CASO DE EXISTIR VALORES A FAVOR
                {
                    decimal valordoc = totalnc- totalcancela;

                    Ddocumento doc2 = new Ddocumento();
                    doc2.ddo_empresa = comp.com_empresa;
                    doc2.ddo_comprobante = comp.com_codigo;
                    doc2.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                    doc2.ddo_doctran = comp.com_doctran;
                    List<Ddocumento> lst2 = new List<Ddocumento>();
                    lst2 = comp.documentos.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
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

                #endregion







                /*
                decimal totalcancela = 0;
                decimal totalrecibo = comp.total.tot_total;
                
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
                    doc.ddo_cancela_ext = doc.ddo_cancela;
                    doc.ddo_monto_ext = doc.ddo_monto;
                    if (doc.ddo_cancela >= doc.ddo_monto)
                        doc.ddo_cancelado = 1;
                    totalcancela += dca.dca_monto.Value;
                    DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO  
                    comp.documentos.Add(doc);
                }

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

                }*/

              
                //CREACION DE DOCUMENTOS
                //   comp.documentos = CXCP.crear_documentos(comp, vdebcre);

                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    ////////////////
                    comp.contables.AddRange(CXCP.contables_detalle(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_impuesto(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_transporte(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contable_seguro(comp, vdebcre_m));


                    comp.contables.AddRange(CXCP.contables_notascredeb(comp));

                    ////////////////////////    
                    comp.contables.AddRange(CXCP.contables_cancelaciones(comp));

                    List<Ddocumento> lst = comp.documentos;
                    List<Ddocumento> lst2 = new List<Ddocumento>();
                    lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });


                    comp.documentos = lst2;
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos(comp, vdebcre_m));
                    
                    
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


        public static List<Dbancario> GetDetalleBanco(int empresa, int? banco, int? tipotran ,string beneficiario, DateTime? desde, DateTime? hasta, int[] estados )
        {
            


            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where = " dban_empresa=" + empresa;
            if ((banco??0)>0)
            {
                parametros.where +=  " and dban_banco = {" + contador + "} ";
                valores.Add(banco);
                contador++;
            }

            if ((tipotran??0) > 0)
            {
                parametros.where +=  " and com_tipodoc = {" + contador + "} ";
                valores.Add(tipotran);
                contador++;
            }
            if (!string.IsNullOrEmpty(beneficiario))
            {
                parametros.where +=  " and ( dban_beneficiario like  {" + contador + "} or  com_concepto like  {" + contador + "}  )";
                valores.Add("%" + beneficiario + "%");
                contador++;
            }
            if (desde.HasValue)
            {
                parametros.where += " and com_fecha >= {" + contador + "} ";
                valores.Add(desde);
                contador++;
            }
            if (hasta.HasValue)
            {
                parametros.where += " and com_fecha <= {" + contador + "} ";                
                valores.Add(hasta.Value.AddDays(1).AddSeconds(-1));
                contador++;
            }
            if (estados.Length>0)
            {
                parametros.where += " and com_estado in (" + string.Join(",", estados) + ")";                
            }            
            parametros.valores = valores.ToArray();

            List<Dbancario> lst = DbancarioBLL.GetAll(parametros, "com_fecha,com_doctran");            
            Decimal saldo = General.SaldoBancos("a", 3, 1, empresa, banco??0, 0, 0, desde.Value.AddSeconds(-1));            
            foreach (Dbancario item in lst)
            {
                if (item.dban_debcre == Constantes.cCredito)
                {
                    saldo -= item.dban_valor_nac;                    
                }
                if (item.dban_debcre == Constantes.cDebito)
                {
                    saldo += item.dban_valor_nac;
                }
                item.dban_saldo = saldo;
            }

            return lst;

        }


    }
}

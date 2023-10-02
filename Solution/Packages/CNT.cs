using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using System.Data;

namespace Packages
{
    public class CNT
    {

        public static Comprobante update_diario(Comprobante comp)
        {

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            


            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_estado = comp.com_estado;
            objU.com_concepto = comp.com_concepto;
            objU.mod_usr = comp.mod_usr;
            objU.mod_fecha = comp.mod_fecha;
            objU.contables = comp.contables;


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


            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();


                #region Elimina contabilizaciones existentes

                foreach (Dcontable item in comp.contables)
                {
                    DcontableBLL.Delete(transaction, item);
                }
                #endregion

                #region Actualiza Comprobantes y guarda contables

                int sec = 0;
                foreach (Dcontable item in objU.contables)
                {
                    item.dco_empresa = objU.com_empresa;
                    item.dco_comprobante = objU.com_codigo;
                    sec++;
                    item.dco_secuencia = sec;
                    DcontableBLL.Insert(transaction, item);
                }
                
                ComprobanteBLL.Update(transaction, objU);

                #endregion
                if (dti.dti_numero.HasValue)                
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                transaction.Commit();
                               
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return objU;

            

        }


        public static Comprobante save_diario(Comprobante comp)
        {



            //DateTime fecha = DateTime.Now;

            #region Actualiza el numero de comprobante en 1

            Dtipocom dti = General.GetDtipocom(comp.com_empresa, comp.com_fecha.Year, comp.com_ctipocom, comp.com_almacen.Value, comp.com_pventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            comp.com_numero = dti.dti_numero.Value;

            #endregion

                       
            //comp.com_numero = dti.dti_numero.Value;
            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);            
            

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                comp.com_codigo = ComprobanteBLL.InsertIdentity(transaction, comp);// INSERT COMPROBANTE


                int contador = 0;
                foreach (Dcontable item in comp.contables)
                {
                    item.dco_empresa = comp.com_empresa;
                    item.dco_comprobante = comp.com_codigo;
                    item.dco_secuencia = contador;
                    DcontableBLL.Insert(transaction, item);
                    contador++;
                }
                

                int vdebcre = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
                int vdebcre_m = (comp.com_tipodoc == Constantes.cFactura.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;

                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////

                DtipocomBLL.Update(transaction, dti); //UPDATE DTIPOCOM

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante account_diario(Comprobante comp)
        {

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");            
            List<Ddocumento> documentoscancelaciones = General.GetDocumentosCancelaciones(comp.cancelaciones);

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                

                #region Elimina documentos y contabilizaciones existentes


                foreach (Ddocumento item in comp.documentos)
                {
                    DdocumentoBLL.Delete(transaction, item);
                }
                foreach (Dcancelacion item in comp.cancelaciones)
                {
                    Ddocumento doccan = documentoscancelaciones.Find(delegate(Ddocumento d) { return d.ddo_comprobante == item.dca_comprobante && d.ddo_empresa == item.dca_empresa && d.ddo_transacc == item.dca_transacc && d.ddo_doctran == item.dca_doctran && d.ddo_pago == item.dca_pago; });
                    doccan.ddo_cancela = doccan.ddo_cancela - item.dca_monto;
                    if (doccan.ddo_cancela == 0)
                        doccan.ddo_cancelado = 0;
                    doccan.ddo_empresa_key = doccan.ddo_empresa;
                    doccan.ddo_comprobante_key = doccan.ddo_comprobante;
                    doccan.ddo_transacc_key = doccan.ddo_transacc;
                    doccan.ddo_doctran_key = doccan.ddo_doctran;
                    doccan.ddo_pago_key = doccan.ddo_pago;
                    DdocumentoBLL.Update(transaction, doccan);

                    DcancelacionBLL.Delete(transaction, item);
                }

                /*foreach (Dbancario item in comp.bancario)
                {
                    DbancarioBLL.Delete(transaction, item);
                }*/




                #endregion

                             
                decimal totalcancela = 0;

                int sec =0;
                int pag = 1;
                foreach (Dcontable item in comp.contables)
                {
                    if (item.dco_ddo_comproba.HasValue)
                    {
                        if (item.dco_ddo_comproba.Value > 0)
                        {
                            Dcancelacion dca = new Dcancelacion();
                            dca.dca_empresa = item.dco_empresa;
                            dca.dca_comprobante = item.dco_ddo_comproba.Value;
                            dca.dca_transacc = item.dco_ddo_transacc.Value;
                            dca.dca_doctran = item.dco_doctran;
                            dca.dca_pago = item.dco_nropago.Value;
                            dca.dca_comprobante_can = comp.com_codigo;
                            dca.dca_secuencia = sec;
                            //dca.dca_debcre = (item.dco_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                            dca.dca_debcre = item.dco_debcre;
                            dca.dca_transacc_can = item.dco_transacc;
                            dca.dca_monto = item.dco_valor_nac;
                            dca.dca_monto_ext = item.dco_valor_ext.HasValue?item.dco_valor_ext.Value:item.dco_valor_nac;

                            DcancelacionBLL.Insert(transaction, dca);

                            Ddocumento doc = new Ddocumento();
                            doc.ddo_empresa = item.dco_empresa;
                            doc.ddo_empresa_key = item.dco_empresa;
                            doc.ddo_comprobante = item.dco_ddo_comproba.Value;
                            doc.ddo_comprobante_key = item.dco_ddo_comproba.Value;
                            doc.ddo_transacc = item.dco_ddo_transacc.Value;
                            doc.ddo_transacc_key = item.dco_ddo_transacc.Value;
                            doc.ddo_doctran = item.dco_doctran;
                            doc.ddo_doctran_key = item.dco_doctran;
                            doc.ddo_pago = item.dco_nropago.Value;
                            doc.ddo_pago_key = item.dco_nropago.Value;
                            doc = DdocumentoBLL.GetByPK(doc);

                            doc.ddo_empresa_key = item.dco_empresa;
                            doc.ddo_comprobante_key = item.dco_ddo_comproba.Value;
                            doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;
                            doc.ddo_cancela_ext = doc.ddo_cancela;
                            doc.ddo_monto_ext = doc.ddo_monto;
                            if (doc.ddo_cancela >= doc.ddo_monto)
                                doc.ddo_cancelado = 1;
                            totalcancela += dca.dca_monto.Value;
                            DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO  

                            //comp.documentos.Add(doc);                        
                            sec++;
                        }
                    }


                    //CREA DOCUMENTOS 
                    if (item.dco_cuentamodulo == 5 || item.dco_cuentamodulo == 6) //5:Cuentas x Cobrar  6: Cuentas x Pagar
                    {
                        if (item.dco_transacc == 1)//FACTURAS
                        {
                            Ddocumento doc = new Ddocumento();
                            doc.ddo_empresa = item.dco_empresa;
                            doc.ddo_empresa_key = item.dco_empresa;
                            doc.ddo_comprobante = comp.com_codigo;
                            doc.ddo_transacc = item.dco_transacc;
                            doc.ddo_doctran = comp.com_doctran;
                            doc.ddo_pago = pag;

                            doc.ddo_codclipro = item.dco_cliente;
                            doc.ddo_debcre = item.dco_debcre;
                            doc.ddo_fecha_emi = comp.com_fecha;
                            doc.ddo_fecha_ven = comp.com_fecha;
                            doc.ddo_monto = item.dco_valor_nac;
                            doc.ddo_monto_ext = item.dco_valor_nac;
                            doc.ddo_cancela = 0;
                            doc.ddo_cancelado = 0;
                            doc.ddo_cuenta = item.dco_cuenta;
                            doc.ddo_agente = comp.com_agente;
                            doc.ddo_modulo = comp.com_modulo;
                            DdocumentoBLL.Insert(transaction, doc);
                            pag++;
                        }
                    }

                }





                //CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);                
                
                #region Guarda Documentos y Contabilizaciones

                //foreach (Ddocumento item in comp.documentos)
                //{
                //    DdocumentoBLL.Insert(transaction, item);
                //}
                
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

        public static Comprobante modify_diario(Comprobante comp)
        {
            comp = General.AnulaComprobanteDiario(comp);
            comp.com_estado = Constantes.cEstadoProceso;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            ComprobanteBLL.Update(comp);
            return comp;

        }


        public static void registrar_dcontable(BLL transaccion, Dcontable dcontable)
        {
            
            int vsecuencia = 0;//OBTENER MAXIMO SI NO ENCUENTRA PONE EN CERO

            List<Dcontable> lst = DcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante= {1}", dcontable.dco_empresa , dcontable.dco_comprobante),"dco_secuencia desc" );

            bool insert = true;

            if (lst.Count > 0)
            {
                vsecuencia = lst[0].dco_secuencia;

                List<Dcontable> l = lst.FindAll(delegate(Dcontable d) { 
                    return d.dco_cuenta == dcontable.dco_cuenta &&
                        d.dco_centro == dcontable.dco_centro &&
                        d.dco_debcre== dcontable.dco_debcre &&
                        d.dco_almacen == dcontable.dco_almacen &&
                        d.dco_doctran == dcontable.dco_doctran &&
                        d.dco_cliente == dcontable.dco_cliente &&
                        d.dco_agente== dcontable.dco_agente &&
                        d.dco_ddo_comproba == d.dco_ddo_comproba &&
                        d.dco_ddo_transacc == dcontable.dco_ddo_transacc &&
                        d.dco_producto == dcontable.dco_producto&&
                        d.dco_bodega == dcontable.dco_bodega; 
                });
                if (l.Count > 0)
                {
                    l[0].dco_valor_nac += dcontable.dco_valor_nac;
                    l[0].dco_valor_ext += dcontable.dco_valor_ext;
                    dcontable = l[0];
                    insert = false; //UPDATE
                }
            }

            if (insert)
            {
                vsecuencia++;//AUMENTA EN 1 LA SECUENCIA
                if (DcontableBLL.Insert(transaccion, dcontable) < 1)
                {
                    throw new Exception("NO SE PUEDE REGISTRAR EL DETALLE CONTABLE");
                    //ERROR
                }
            }
            else
            {
                if (DcontableBLL.Update(transaccion, dcontable) < 1)
                {
                    throw new Exception("NO SE PUEDE ACTUALIZAR EL DETALLE CONTABLE");
                    //ERROR
                }
            }
  

        }


        public static decimal suma_dcontable(List<Dcontable> detalle, int? modulo, int debcre)
        {
            decimal valor = 0;
            bool suma = true;
            if (detalle != null)
            {
                foreach (Dcontable item in detalle)
                {
                    if (modulo.HasValue)
                    {
                        if (item.dco_cuentamodulo == modulo)
                        {
                            suma = true;
                        }
                        else

                            suma = false;
                    }
                    if (suma)
                    {
                        if (item.dco_debcre == debcre)
                        {
                            valor += item.dco_valor_nac;
                        }
                        /*else
                        {
                            valor += item.dco_valor_nac;
                        }*/
                    }
                }
            }
            return valor;
        }
        public static decimal suma_dbancario(List<Dbancario> detalle, int debcre)
        {
            decimal valor = 0;
            if (detalle != null)
            {
                foreach (Dbancario item in detalle)
                {

                    if (item.dban_debcre == debcre)
                    {
                        valor += item.dban_valor_nac;
                    }
                    else
                    {
                        valor += item.dban_valor_nac;
                    }
                }
            }
            return valor;
        }
        public static decimal suma_ddocumento(List<Ddocumento> detalle, int debcre)
        {
            decimal valor = 0;
            if (detalle != null)
            {
                foreach (Ddocumento item in detalle)
                {

                    if (item.ddo_debcre == debcre)
                    {
                        valor += (item.ddo_monto.HasValue) ? item.ddo_monto.Value : 0;
                    }
                    else
                    {
                        valor += (item.ddo_monto.HasValue) ? item.ddo_monto.Value : 0;
                    }
                }
            }
            return valor;
        }
        public static decimal suma_dcancelacion(List<Dcancelacion> detalle, int debcre)
        {
            decimal valor = 0;
            if (detalle != null)
            {
                foreach (Dcancelacion item in detalle)
                {

                    if (item.dca_debcre == debcre)
                    {
                        valor += (item.dca_monto.HasValue) ? item.dca_monto.Value : 0;
                    }
                    else
                    {
                        valor += (item.dca_monto.HasValue) ? item.dca_monto.Value : 0;
                    }
                }
            }
            return valor;
        }


        public static bool comprobante_cuadrado(int empresa, long comprobante)
        {
            bool resultado = true;
            Comprobante comp = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = empresa, com_empresa_key = empresa, com_codigo = comprobante, com_codigo_key = comprobante });

            //decimal totdebito = DcontableBLL.GetSum("dco_valor_nac", new WhereParams("dco_empresa={0} and dco_comprobante={1}"), "");     
            List<Dcontable> detalle = DcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante={1}", empresa, comprobante), "");
            List<Dbancario> detalleban = DbancarioBLL.GetAll(new WhereParams("dban_empresa={0} and dban_comprobante={1}", empresa, comprobante), "");  
            List<Ddocumento> documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} and ddo_comprobante={1} and ddo_modulo <> {2}", empresa, comprobante, 3), "");  
            List<Dcancelacion> cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_comprobante={1}", empresa, comprobante), "");  

            decimal valordebito = suma_dcontable(detalle,null,Constantes.cDebito);
            decimal valorcredito = suma_dcontable(detalle, null, Constantes.cCredito);
           
            //COMPRUEBA QUE DEBITO Y CREDITO SEAN IGUALES
            if (valorcredito != valordebito)
            {
                resultado = false;
                //ERROR
            }
            
            //BANCOS
            if (resultado)
            {
                decimal valordebitoban = suma_dbancario(detalleban, Constantes.cDebito);  
                valordebito = suma_dcontable(detalle, 4, Constantes.cDebito);
                if (valordebito != valordebitoban)
                {
                    resultado = false;
                    //ERROR
                }   
            }
             if (resultado)
            {
                decimal valorcreditoban = suma_dbancario(detalleban, Constantes.cCredito);  
                valorcredito = suma_dcontable(detalle, 4, Constantes.cCredito);
                if (valorcredito!= valorcreditoban)
                {
                    resultado = false;
                    //ERROR
                }   
            }
            ////DOCUMENTOS
            //if (resultado)
            //{
            //    decimal valordebitodoc = suma_ddocumento(documentos, Constantes.cDebito) + suma_dcancelacion(cancelaciones,Constantes.cDebito);
            //    valordebito = suma_dcontable(detalle, 5, Constantes.cDebito) + suma_dcontable(detalle, 6, Constantes.cDebito) + suma_dcontable(detalle, 11, Constantes.cDebito) suma_dcontable(detalle, 14, Constantes.cDebito);
            //    if (valordebito != valordebitodoc)
            //    {
            //        resultado = false;
            //        //ERROR
            //    }
            //}
            //if (resultado)
            //{
            //    decimal valorcreditodoc = suma_ddocumento(documentos, Constantes.cCredito) + suma_dcancelacion(cancelaciones,Constantes.cCredito);
            //    valorcredito= suma_dcontable(detalle, 5, Constantes.cCredito) + suma_dcontable(detalle, 6, Constantes.cCredito) + suma_dcontable(detalle, 11, Constantes.cCredito) suma_dcontable(detalle, 14, Constantes.cCredito);
            //    if (valorcredito!= valorcreditodoc)
            //    {
            //        resultado = false;
            //        //ERROR
            //    }
            //}
            return resultado;

        }

        public static bool comprobante_cuadrado(Comprobante comp)
        {
            bool resultado = true;            

            //decimal totdebito = DcontableBLL.GetSum("dco_valor_nac", new WhereParams("dco_empresa={0} and dco_comprobante={1}"), "");     
            /*List<Dcontable> detalle = DcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante={1}", empresa, comprobante), "");
            List<Dbancario> detalleban = DbancarioBLL.GetAll(new WhereParams("dban_empresa={0} and dban_comprobante={1}", empresa, comprobante), "");
            List<Ddocumento> documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa={0} and ddo_comprobante={1} and ddo_modulo <> {2}", empresa, comprobante, 3), "");
            List<Dcancelacion> cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_comprobante={1}", empresa, comprobante), "");
            */
            decimal valordebito = suma_dcontable(comp.contables, null, Constantes.cDebito);
            decimal valorcredito = suma_dcontable(comp.contables, null, Constantes.cCredito);

            //COMPRUEBA QUE DEBITO Y CREDITO SEAN IGUALES
            if (valorcredito != valordebito)
            {
                //DEJA CUADRAR VALORES MENORES A 2,99 centavo de descuadre
                decimal dif = valorcredito - valordebito;
                dif = dif < 0 ? dif * -1 : dif;
                if (dif > (decimal)0.0299)
                {
                    resultado = false;
                }
                //ERROR
            }

            //BANCOS
            if (resultado)
            {
                decimal valordebitoban = suma_dbancario(comp.bancario, Constantes.cDebito);
                valordebito = suma_dcontable(comp.contables, 4, Constantes.cDebito);
                //if (valordebito != valordebitoban)
                //{
                //    resultado = false;
                //    //ERROR
                //}
            }
            if (resultado)
            {
                decimal valorcreditoban = suma_dbancario(comp.bancario, Constantes.cCredito);
                valorcredito = suma_dcontable(comp.contables, 4, Constantes.cCredito);
                //if (valorcredito != valorcreditoban)
                //{
                //    resultado = false;
                //    //ERROR
                //}
            }
            ////DOCUMENTOS
            //if (resultado)
            //{
            //    decimal valordebitodoc = suma_ddocumento(documentos, Constantes.cDebito) + suma_dcancelacion(cancelaciones,Constantes.cDebito);
            //    valordebito = suma_dcontable(detalle, 5, Constantes.cDebito) + suma_dcontable(detalle, 6, Constantes.cDebito) + suma_dcontable(detalle, 11, Constantes.cDebito) suma_dcontable(detalle, 14, Constantes.cDebito);
            //    if (valordebito != valordebitodoc)
            //    {
            //        resultado = false;
            //        //ERROR
            //    }
            //}
            //if (resultado)
            //{
            //    decimal valorcreditodoc = suma_ddocumento(documentos, Constantes.cCredito) + suma_dcancelacion(cancelaciones,Constantes.cCredito);
            //    valorcredito= suma_dcontable(detalle, 5, Constantes.cCredito) + suma_dcontable(detalle, 6, Constantes.cCredito) + suma_dcontable(detalle, 11, Constantes.cCredito) suma_dcontable(detalle, 14, Constantes.cCredito);
            //    if (valorcredito!= valorcreditodoc)
            //    {
            //        resultado = false;
            //        //ERROR
            //    }
            //}
            return resultado;

        }


        public static List<Cuenta> cuentas = new List<Cuenta>(); 

        public static void CalculaSaldos(Saldo saldo, int codigo)
        {
            Cuenta cta = cuentas.Find(delegate(Cuenta c) { return c.cue_codigo == codigo; });
            if (cta != null)
            {
                cta.debito += saldo.sal_debito;
                cta.credito += saldo.sal_credito;
                cta.final = cta.inicial+cta.debito - cta.credito;
                if (cta.cue_reporta.HasValue)
                    CalculaSaldos(saldo, cta.cue_reporta.Value);  
            }
        }

        public static void CalculaSaldosIniciales(Saldo saldo, int codigo)
        {
            Cuenta cta = cuentas.Find(delegate(Cuenta c) { return c.cue_codigo == codigo; });
            if (cta != null)
            {
                cta.inicial += saldo.sal_debito - saldo.sal_credito;
                cta.final += saldo.sal_debito - saldo.sal_credito;                 
                if (cta.cue_reporta.HasValue)
                    CalculaSaldosIniciales(saldo, cta.cue_reporta.Value);
            }
        }

        public static List<Cuenta> getBalance(int empresa, int? almacen, int? centro, int? transacc, DateTime fecha, string tipo, int debcre)
        {

            fecha = fecha.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

            int periodo = fecha.Year;
            int mes = fecha.Month;

            int dia = fecha.Day;
            int? mes_ini = 0;
            int? mes_fin = 0;
            int? mes_fin_aux = 0;
            if (tipo.Equals("i"))
            {
                mes_ini = 0;
                mes_fin = 0;
            }
            else if (tipo.Equals("a"))
            {
                mes_ini = 0;
                mes_fin = mes - 1;
                if (mes_fin < 0)
                    mes_fin = 0;

                /*if (debcre == 3)
                {
                    mes_ini = 0;
                    mes_fin = mes - 1;
                    if (mes_fin < 0)
                        mes_fin = 0;
                }
                else
                {
                    mes_ini = null;
                    mes_fin = null;
                }*/
            }
            else if (tipo.Equals("m"))
            {
                /*if (debcre == 3)
                    mes_ini = 0;
                else
                    mes_ini = mes;
                mes_fin = mes;*/
                mes_ini = mes;
                mes_fin = mes;
            }

            mes_fin_aux = mes_fin - 1;
            if (mes_fin_aux < 0)
                mes_fin_aux = 0;

            WhereParams parametrosi = new WhereParams();
            List<object> valoresi = new List<object>();
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            parametros.where += "sal_empresa={0} and sal_periodo={1} and sal_mes = {2} ";
            parametrosi.where += "sal_empresa={0} and sal_periodo={1} and sal_mes between {2} and {3} ";

            valores.Add(empresa);
            valores.Add(periodo);
            valores.Add(mes_fin);
            valoresi.Add(empresa);
            valoresi.Add(periodo);
            valoresi.Add(mes_ini);
            valoresi.Add(mes_fin_aux);
            int contador = 3;
            int contadori = 4;
            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + "  (COALESCE({" + contador + "},0)      = 0 OR COALESCE(sal_almacen,0)   = COALESCE({" + contador + "},0))";
                valores.Add(almacen.Value);
                parametrosi.where += ((parametrosi.where != "") ? " and " : "") + " (COALESCE({" + contadori + "},0)      = 0 OR COALESCE(sal_almacen,0)   = COALESCE({" + contadori + "},0))";
                valoresi.Add(almacen.Value);
                contador++;
                contadori++;
            }
            if (centro.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (COALESCE({" + contador + "},0)      = 0 OR COALESCE(sal_centro,0)   = COALESCE({" + contador + "},0))";
                valores.Add(centro.Value);
                parametrosi.where += ((parametrosi.where != "") ? " and " : "") + " (COALESCE({" + contadori + "},0)      = 0 OR COALESCE(sal_centro,0)   = COALESCE({" + contadori + "},0))";
                valoresi.Add(centro.Value);
                contador++;
                contadori++;
            }
            if (transacc.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (COALESCE({" + contador + "},0)      = 0 OR COALESCE(sal_transacc,0)   = COALESCE({" + contador + "},0))";
                valores.Add(transacc.Value);
                parametrosi.where += ((parametrosi.where != "") ? " and " : "") + " (COALESCE({" + contadori + "},0)      = 0 OR COALESCE(sal_transacc,0)   = COALESCE({" + contadori + "},0))";
                valoresi.Add(transacc.Value);
                contador++;
                contadori++;
            }
            parametros.valores = valores.ToArray();
            parametrosi.valores = valoresi.ToArray();
            List<Saldo> saldosi = SaldoBLL.GetAll(parametrosi, "");
            List<Saldo> saldos = SaldoBLL.GetAll(parametros, "");
            cuentas = CuentaBLL.GetAll(new WhereParams("cue_empresa={0}", empresa), "cue_id");


            //var date1 = new DateTime(periodo, mes, 1, fecha.Hour, fecha.Minute, fecha.Second);
            var date1 = new DateTime(periodo, mes, 1, 0, 0, 0);

            string wheresaldo = " dco_empresa     = {0} " +
                                " and  com_fecha BETWEEN {1} and {2} " +
                                " AND (com_codigo     = dco_comprobante " +
                                " AND  com_empresa    = dco_empresa) " +
                                " AND  com_estado     IN ({6},{7})  " +
                                " AND  com_nocontable = 0 " +
                                " AND (COALESCE({3},0)= 0 OR COALESCE(dco_almacen,0)= COALESCE({3},0)) " +
                                " AND (COALESCE({4},0)= 0 OR COALESCE(dco_transacc,0)= COALESCE({4},0)) " +
                                " AND (COALESCE({5},0)= 0 OR COALESCE(dco_centro,0)=COALESCE({5},0));";

            List<Dcontable> lst3 = DcontableBLL.GetAll(new WhereParams(wheresaldo, empresa, date1, fecha, almacen ?? 0, transacc ?? 0, centro ?? 0, Constantes.cEstadoPorAutorizar, Constantes.cEstadoMayorizado), "");
            foreach (Dcontable item in lst3)
            {
                Cuenta cta = cuentas.Find(delegate(Cuenta c) { return c.cue_codigo == item.dco_cuenta; });
                if (cta != null)
                {
                    if (item.dco_debcre == Constantes.cCredito)
                    {
                        cta.credito += item.dco_valor_nac;
                        cta.final -= item.dco_valor_nac;
                    }
                    if (item.dco_debcre == Constantes.cDebito)
                    {
                        cta.debito += item.dco_valor_nac;
                        cta.final += item.dco_valor_nac;
                    }
                }
            }
            if (tipo.Equals("a"))
            {
                foreach (Saldo sal in saldosi)
                {
                    CalculaSaldosIniciales(sal, sal.sal_cuenta);
                }

                foreach (Saldo sal in saldos)
                {
                    CalculaSaldos(sal, sal.sal_cuenta);
                }
            }
            return cuentas;
        }


        public static void SetValorCuenta(Dcontable contable, int codcue)
        {
               Cuenta cta = cuentas.Find(delegate(Cuenta c) { return c.cue_codigo == codcue; });
               if (cta != null)
               {
                   if (contable.dco_debcre == Constantes.cCredito)
                   {
                       cta.credito += contable.dco_valor_nac;
                       cta.final -= contable.dco_valor_nac;
                   }
                   if (contable.dco_debcre == Constantes.cDebito)
                   {
                       cta.debito += contable.dco_valor_nac;
                       cta.final += contable.dco_valor_nac;
                   }

                   if (cta.cue_reporta.HasValue)
                       SetValorCuenta(contable, cta.cue_reporta.Value);

               }
            
        }

        public static List<Cuenta> getBalanceNew(int? empresa, int? almacen, int? centro, int? transacc, DateTime fecha, string tipo, int debcre, bool all, bool saldo)
        {               
            if (tipo!="i")
                fecha = fecha.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

            int periodo = fecha.Year;
            int mes = fecha.Month;
            int dia = fecha.Day;

            int mesdesde = 0;
            int meshasta = (mes > 0) ? mes - 1 : mes;


            List<Saldo> saldos = SaldoBLL.GetAll(new WhereParams("sal_empresa={0} and sal_periodo={1} and sal_mes between {2} and {3}",empresa,periodo,mesdesde,meshasta), "");  
            //List<Saldo> saldos = SaldoBLL.GetAll(new WhereParams("sal_empresa={0} and sal_periodo={1} and sal_mes ={2}", empresa, periodo, meshasta), "");  

            DateTime desde = new DateTime(periodo, mes, 1, 0, 0, 0);


            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " dco_almacen = {" + contador + "} ";
                    valores.Add(almacen.Value);
                    contador++;
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " dco_empresa = {" + contador + "} ";
                    valores.Add(empresa.Value);
                    contador++;
                }
            }
            if (centro.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_centro = {" + contador + "} ";
                valores.Add(centro.Value);
                contador++;
            }
            if (transacc.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_transacc = {" + contador + "} ";
                valores.Add(transacc.Value);
                contador++;
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? "  " : "") + " and {" + contador + "} ";
            valores.Add(fecha);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + " com_estado = {" + contador + "} ";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;
            parametros.valores = valores.ToArray();

            
            List<Dcontable> lst = DcontableBLL.GetAll(parametros, "com_fecha,com_doctran");
            
            cuentas = CuentaBLL.GetAll(new WhereParams("cue_empresa={0}", empresa), "cue_id");

            if (tipo.Equals("a") || tipo.Equals("i"))
            {
                foreach (Saldo sal in saldos)
                {
                    CalculaSaldosIniciales(sal, sal.sal_cuenta);
                }
            }

            
                foreach (Dcontable item in lst)
                {
                    SetValorCuenta(item, item.dco_cuenta);
                    //Cuenta cta = cuentas.Find(delegate(Cuenta c) { return c.cue_codigo == item.dco_cuenta; });
                    //if (cta != null)
                    //{
                    //    if (item.dco_debcre == Constantes.cCredito)
                    //    {
                    //        cta.credito += item.dco_valor_nac;
                    //        cta.final -= item.dco_valor_nac;
                    //    }
                    //    if (item.dco_debcre == Constantes.cDebito)
                    //    {
                    //        cta.debito += item.dco_valor_nac;
                    //        cta.final += item.dco_valor_nac;
                    //    }
                    //}
                }
            

          

            /*

            if (tipo.Equals("i"))
            {
                mesdesde = 0;
                meshasta = 0;
            }
            else if (tipo.Equals("a"))
            {
                mesdesde = 0;
                meshasta = mes - 1;
                if (meshasta < 0)
                    meshasta = 0;
            }
            else if (tipo.Equals("m"))
            {
                
                mesdesde = mes;
                meshasta = mes;
            }

            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();            
            parametros.where += "sal_empresa={0} and sal_periodo={1} and sal_mes between {2} and {3} ";

            valores.Add(empresa);
            valores.Add(periodo);
            valores.Add(mesdesde);
            valores.Add(meshasta);
            
            int contador = 4;
            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + "  (COALESCE({" + contador + "},0)      = 0 OR COALESCE(sal_almacen,0)   = COALESCE({" + contador + "},0))";
                //parametros.where += ((parametros.where != "") ? " and " : "") + "  sal_almacen = {"+contador+"} ";
                valores.Add(almacen.Value);
                contador++;                
            }
            if (centro.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " sal_centro ={" + contador + "} ";
                valores.Add(centro.Value);
                contador++;                
            }
            if (transacc.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " sal_transacc ={" + contador + "} ";
                valores.Add(transacc.Value);
                contador++;                
            }
            parametros.valores = valores.ToArray();
            List<Saldo> saldos = SaldoBLL.GetAll(parametros, "");

            cuentas = CuentaBLL.GetAll(new WhereParams("cue_empresa={0}", empresa), "cue_id");
          


            //var date1 = new DateTime(periodo, mes, 1, fecha.Hour, fecha.Minute, fecha.Second);
            var desde = new DateTime(periodo, mes, 1, 0, 0, 0);

            string wheresaldo = " dco_empresa     = {0} " +
                                " and  com_fecha BETWEEN {1} and {2} " +
                                " AND (com_codigo     = dco_comprobante " +
                                " AND  com_empresa    = dco_empresa) " +
                                " AND  com_estado     IN ({6},{7})  " +
                                " AND  com_nocontable = 0 " +
                                " AND (COALESCE({3},0)= 0 OR COALESCE(dco_almacen,0)= COALESCE({3},0)) " +
                                " AND (COALESCE({4},0)= 0 OR COALESCE(dco_transacc,0)= COALESCE({4},0)) " +
                                " AND (COALESCE({5},0)= 0 OR COALESCE(dco_centro,0)=COALESCE({5},0));";

            List<Dcontable> lst3 = DcontableBLL.GetAll(new WhereParams(wheresaldo, empresa, desde, fecha, almacen ?? 0, transacc ?? 0, centro ?? 0, Constantes.cEstadoPorAutorizar, Constantes.cEstadoMayorizado), "");
            foreach (Dcontable item in lst3)
            {
                Cuenta cta = cuentas.Find(delegate(Cuenta c) { return c.cue_codigo == item.dco_cuenta; });
                if (cta != null)
                {
                    if (item.dco_debcre == Constantes.cCredito)
                    {
                        cta.credito += item.dco_valor_nac;
                        cta.final -= item.dco_valor_nac;
                    }
                    if (item.dco_debcre == Constantes.cDebito)
                    {
                        cta.debito += item.dco_valor_nac;
                        cta.final += item.dco_valor_nac;
                    }
                }
            }
            if (tipo.Equals("a"))
            {
                foreach (Saldo sal in saldos)
                {
                    CalculaSaldosIniciales(sal, sal.sal_cuenta);
                }
            }
            */
            if (all)
                return cuentas;
            else if (saldo)
                return cuentas.FindAll(c => c.final != 0);
            else
                return cuentas.FindAll(c => c.inicial!=0 || c.debito!=0 || c.credito!=0 || c.final!=0);
        }


        


        public static List<vMayor> getMayor(int? empresa, int? almacen, int? cuenta, int? cliente, int? debcre, DateTime desde, DateTime hasta)
        {

            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (cuenta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_cuenta = {" + contador + "} ";
                valores.Add(cuenta.Value);
                contador++;
            }
            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_almacen = {" + contador + "} ";
                valores.Add(almacen.Value);
                contador++;
            }
            if (empresa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_empresa = {" + contador + "} ";
                valores.Add(empresa.Value);
                contador++;
            }
            if (cliente.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_cliente = {" + contador + "} ";
                valores.Add(cliente.Value);
                contador++;
            }
            if (debcre.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " dco_debcre = {" + contador + "} ";
                valores.Add(debcre.Value);
                contador++;
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? "  " : "") + " and {" + contador + "} ";
            valores.Add(hasta);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + " com_estado = {" + contador + "} ";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;
            parametros.valores = valores.ToArray();

            decimal total = 0;

            if (cuenta.HasValue)
            {
                //List<Cuenta> lstctas = CNT.getBalance(empresa, almacen, null, null, desde, "a", 3);
                List<Cuenta> lstctas = CNT.getBalanceNew(empresa, almacen, null, null, desde, "i", 3, true,false);
                Cuenta cta = lstctas.Find(delegate(Cuenta c) { return c.cue_codigo == cuenta; });
                total = cta.final;

            }
        
                      

            List<Dcontable> lst = DcontableBLL.GetAll(parametros, "com_fecha,com_doctran");
            List<vMayor> mayor = new List<vMayor>();             
            foreach (Dcontable item in lst)
            {
                vMayor m = new vMayor();
                m.fecha = item.dco_comprobantefecha;
                m.comprobante = item.dco_compdoctran;
                m.concepto =  item.dco_compconcepto;
                if (item.dco_debcre == Constantes.cCredito)
                {
                    m.credito = item.dco_valor_nac;
                    m.debito = 0;

                    total -= item.dco_valor_nac;

                }
                if (item.dco_debcre == Constantes.cDebito)
                {
                    m.credito = 0;
                    m.debito = item.dco_valor_nac;

                    total += item.dco_valor_nac;
                }
                m.final = total;
                mayor.Add(m);

            }
            return mayor;

        }





        /*  public static List<Cuenta> getBalance(int empresa, int? almacen, int? centro, int? transacc, DateTime fecha, int tipo)
        {
            int periodo = fecha.Year;
            int mes = fecha.Month;
            int mesdesdei = 0;
            int meshastai = mes - 1;
            WhereParams parametrosi = new WhereParams();
            List<object> valoresi = new List<object>();
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            parametros.where += "sal_empresa={0} and sal_periodo={1} and sal_mes = {2} ";
            parametrosi.where += "sal_empresa={0} and sal_periodo={1} and sal_mes between {2} and {3} ";
            valores.Add(empresa);
            valores.Add(periodo);
            valores.Add(mes);
            valoresi.Add(empresa);
            valoresi.Add(periodo);
            valoresi.Add(mesdesdei);
            valoresi.Add(meshastai);
            int contador = 3;
            int contadori = 4;
            if (almacen.HasValue)
            {                
                parametros.where += ((parametros.where != "") ? " and " : "") + "  (COALESCE({" + contador + "},0)      = 0 OR COALESCE(sal_almacen,0)   = COALESCE({" + contador + "},0))";
                valores.Add(almacen.Value);
                parametrosi.where += ((parametrosi.where != "") ? " and " : "") + " (COALESCE({" + contadori + "},0)      = 0 OR COALESCE(sal_almacen,0)   = COALESCE({" + contadori + "},0))";
                valoresi.Add(almacen.Value);
                contador++;
                contadori++;
            }
            if (centro.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") +" (COALESCE({" + contador + "},0)      = 0 OR COALESCE(sal_centro,0)   = COALESCE({" + contador + "},0))";
                valores.Add(centro.Value);
                //
                contador++;
            }
            if (transacc.HasValue)
            {             
                parametros.where += ((parametros.where != "") ? " and " : "") + " (COALESCE({" + contador + "},0)      = 0 OR COALESCE(sal_transacc,0)   = COALESCE({" + contador + "},0))";
                valores.Add(transacc.Value);
                //
                contador++;
            }
            parametros.valores = valores.ToArray();
            parametrosi.valores = valoresi.ToArray();
            List<Saldo> saldosi = SaldoBLL.GetAll(parametrosi, "");
            List<Saldo> saldos = SaldoBLL.GetAll(parametros ,"");            
            cuentas = CuentaBLL.GetAll(new WhereParams("cue_empresa={0}", empresa), "cue_id");
            foreach (Saldo sal in saldosi)
            {
                CalculaSaldosIniciales(sal, sal.sal_cuenta);
            }
            foreach (Saldo sal in saldos)
            {
                CalculaSaldos(sal, sal.sal_cuenta);                  
            }
            return cuentas;
        }*/



    }
}


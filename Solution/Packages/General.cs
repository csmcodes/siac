using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.Net.Cache;

namespace Packages
{
    public class General
    {

        #region Actualizar Documentos
        //public static Comprobante ActualizarDocumentos(int empresa, int comprobante Comprobante comp)
        //{
        //    comp.com_empresa_key = comp.com_empresa;
        //    comp.com_codigo_key = comp.com_codigo;
        //    comp = ComprobanteBLL.GetByPK(comp);

        //    comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
        //    comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");


        //    return comp;

        //}


        #endregion


        #region Comprobante

        public static void save_historial(Comprobante comp)
        {
            save_historial(null, comp);
        }

        public static void save_historial(BLL transaccion, Comprobante comp)
        {
            string savehistorial = System.Configuration.ConfigurationSettings.AppSettings["savehistorial"].ToString();

            if (savehistorial == "1")
            {
                StackTrace stackTrace = new StackTrace();
                JavaScriptSerializer ser = new JavaScriptSerializer();

                Comprobantehistorial coh = new Comprobantehistorial();
                coh.coh_empresa = comp.com_empresa;
                coh.coh_codigo = comp.com_codigo;
                coh.coh_fecha = DateTime.Now;
                if (transaccion != null)
                    coh.coh_stack = stackTrace.GetFrame(1).GetMethod().Name;
                else
                    coh.coh_stack = stackTrace.GetFrame(2).GetMethod().Name;
                coh.coh_data = ser.Serialize(comp);
                coh.crea_usr = comp.crea_usr;
                coh.crea_fecha = comp.crea_fecha;
                coh.mod_usr = comp.mod_usr;
                coh.mod_fecha = comp.mod_fecha;

                if (transaccion != null)
                    ComprobantehistorialBLL.Insert(transaccion, coh);
                else
                    ComprobantehistorialBLL.Insert(coh);
            }

        }

        public static Comprobante NuevoComprobante(Comprobante comprobante)
        {
            //List<Comprobante> lst = ComprobanteBLL.GetAllTop(new WhereParams("com_empresa={0} and com_estado={1} and com_tipodoc={2} and comprobante.crea_usr={3} ", comprobante.com_empresa, Constantes.cEstadoProceso,comprobante.com_tipodoc, comprobante.crea_usr), "com_codigo DESC", 1);
            List<vComp> lst = vCompBLL.GetAllTop(new WhereParams("com_empresa={0} and com_estado={1} and com_tipodoc={2} and crea_usr={3} and com_doctran is null ", comprobante.com_empresa, Constantes.cEstadoProceso, comprobante.com_tipodoc, comprobante.crea_usr), "", 1);
            if (lst.Count > 0)
            {
                comprobante.com_codigo = lst[0].com_codigo.Value;
                comprobante.com_tipodoc = lst[0].com_tipodoc.Value;
                comprobante.com_estado = lst[0].com_estado.Value;
                comprobante.com_numero = 0;
                comprobante.com_concepto = "";
                return comprobante;
            }
            else
            {

                comprobante.com_numero = 0;
                comprobante.com_concepto = "";
                comprobante.com_estado = Constantes.cEstadoProceso;
                comprobante.com_codigo = ComprobanteBLL.InsertIdentity(comprobante);
                save_historial(comprobante);
                return comprobante;
            }
        }

        public static string NumeroComprobante(int empresa, int comprobante)
        {
            Comprobante comp = new Comprobante();
            comp.com_empresa = empresa;
            comp.com_empresa_key = empresa;
            comp.com_codigo = comprobante;
            comp.com_codigo_key = comprobante;
            comp = ComprobanteBLL.GetByPK(comp);
            return NumeroComprobante(comp);
        }

        public static string NumeroComprobante(Comprobante comp)
        {
            return FormatNumeroComprobante(comp.com_ctipocomid, comp.com_almacenid, comp.com_pventaid, comp.com_numero);
        }

        public static string FormatNumeroComprobante(string sigla, string almacen, string punto, int numero)
        {
            return sigla + "-" + almacen + "-" + punto + "-" + string.Format("{0:0000000}", numero);
        }

        public static Comprobante Autoriza_comp(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                #region Elimina documentos y contabilizaciones existentes


                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    Comprobante c = new Comprobante();
                    c.com_empresa_key = comp.com_empresa;
                    c.com_codigo_key = dca.dca_comprobante_can;
                    c = ComprobanteBLL.GetByPK(c);
                    throw new Exception("El comprobante tiene cancelaciones no se puede modificar ");
                    //    Autoriza_comp(c);
                }
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



                comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
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
                    doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) - dca.dca_monto;
                    doc.ddo_cancela_ext = doc.ddo_cancela;
                    doc.ddo_monto_ext = doc.ddo_monto;
                    if (doc.ddo_cancela >= doc.ddo_monto)
                        doc.ddo_cancelado = 1;
                    else
                        doc.ddo_cancelado = 0;

                    DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO  
                                                           //    comp.documentos.Add(doc);
                    DcancelacionBLL.Delete(transaction, dca);
                }


                transaction.Commit();

                #endregion
                comp.com_estado = Constantes.cEstadoGrabado;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }


        public static int GetNextNumeroComprobante(int empresa, int periodo, int ctipocom, int almacen, int pventa)
        {

            Dtipocom dti = GetDtipocom(empresa, periodo, ctipocom, almacen, pventa);
            dti.dti_numero = dti.dti_numero.Value + 1;
            return GetNumeroLibre(dti).dti_numero.Value;

        }



        public static Dtipocom GetNumeroLibre(Dtipocom dti)
        {
            if ((dti.dti_numero ?? 0) == 0)
                dti.dti_numero = 1;
            bool libre = false;
            do
            {

                //List<Comprobante> lst = ComprobanteBLL.GetAll(new WhereParams("com_empresa={0} and com_ctipocom={1} and com_almacen={2} and com_pventa={3} and com_numero={4} and com_estado not in ({5},{6})", empresa, tipo, establecimiento, puntoemision, secuencia, (int)Enums.EstadoComprobante.ANULADO, (int)Enums.EstadoComprobante.ELIMINADO), "");
                List<Comprobante> lst = ComprobanteBLL.GetAll(new WhereParams("com_empresa={0} and com_ctipocom={1} and com_almacen={2} and com_pventa={3} and com_numero={4}", dti.dti_empresa, dti.dti_ctipocom, dti.dti_almacen, dti.dti_puntoventa, dti.dti_numero), "");
                if (lst.Count == 0)
                    libre = true;
                else
                    dti.dti_numero = dti.dti_numero + 1;
            }
            while (!libre);
            return dti;
        }


        public static Dtipocom GetDtipocom(int empresa, int periodo, int ctipocom, int almacen, int pventa)
        {
            Dtipocom dti = new Dtipocom();

            //INTENTA ENCONTRAR SECUENCIA CON PERIODO 
            dti.dti_empresa = empresa;
            dti.dti_empresa_key = empresa;
            dti.dti_periodo = periodo;
            dti.dti_periodo_key = periodo;
            dti.dti_ctipocom = ctipocom;
            dti.dti_ctipocom_key = ctipocom;
            dti.dti_almacen = almacen;
            dti.dti_almacen_key = almacen;
            dti.dti_puntoventa = pventa;
            dti.dti_puntoventa_key = pventa;
            dti = DtipocomBLL.GetByPK(dti);
            if (!dti.dti_numero.HasValue)
            {
                //INTENTA ENCONTRAR SECUENCIA SIN PERIODO, periodo=0
                dti.dti_empresa = empresa;
                dti.dti_empresa_key = empresa;
                dti.dti_periodo = 0;
                dti.dti_periodo_key = 0;
                dti.dti_ctipocom = ctipocom;
                dti.dti_ctipocom_key = ctipocom;
                dti.dti_almacen = almacen;
                dti.dti_almacen_key = almacen;
                dti.dti_puntoventa = pventa;
                dti.dti_puntoventa_key = pventa;
                dti = DtipocomBLL.GetByPK(dti);
                if (!dti.dti_numero.HasValue)
                {
                    dti = new Dtipocom();
                    dti.dti_empresa = empresa;
                    dti.dti_periodo = 0; //periodo;
                    dti.dti_ctipocom = ctipocom;
                    dti.dti_almacen = almacen;
                    dti.dti_puntoventa = pventa;
                    dti.dti_numero = 0;
                    dti.dti_estado = 1;
                    DtipocomBLL.Insert(dti);
                }

            }
            return dti;



        }

        public static void UpdateDtipocom(BLL transaccion, Dtipocom dti, int numero)
        {

            if (dti.dti_numero == numero)
            {
                DtipocomBLL.Update(transaccion, dti);
            }
        }

        public static string GetNumeroComprobante(Comprobante comp)
        {


            Ctipocom cti = new Ctipocom();
            cti.cti_empresa = comp.com_empresa;
            cti.cti_empresa_key = comp.com_empresa;
            cti.cti_codigo = comp.com_ctipocom;
            cti.cti_codigo_key = comp.com_ctipocom;
            cti = CtipocomBLL.GetByPK(cti);

            Almacen alm = new Almacen();
            alm.alm_empresa = comp.com_empresa;
            alm.alm_empresa_key = comp.com_empresa;
            alm.alm_codigo = comp.com_almacen.Value;
            alm.alm_codigo_key = comp.com_almacen.Value;
            alm = AlmacenBLL.GetByPK(alm);
            if (comp.com_pventa.HasValue)
            {
                Puntoventa pve = new Puntoventa();
                pve.pve_empresa = comp.com_empresa;
                pve.pve_empresa_key = comp.com_empresa;
                pve.pve_almacen = comp.com_almacen.Value;
                pve.pve_almacen_key = comp.com_almacen.Value;
                pve.pve_secuencia = comp.com_pventa.Value;
                pve.pve_secuencia_key = comp.com_pventa.Value;
                pve = PuntoventaBLL.GetByPK(pve);


                return FormatNumeroComprobante(cti.cti_id, alm.alm_id, pve.pve_id, comp.com_numero);
            }
            if (comp.com_bodega.HasValue)
            {
                Bodega bod = new Bodega();
                bod.bod_empresa = comp.com_empresa;
                bod.bod_empresa_key = comp.com_empresa;
                bod.bod_codigo = comp.com_bodega.Value;
                bod.bod_codigo_key = comp.com_bodega.Value;
                bod = BodegaBLL.GetByPK(bod);


                return FormatNumeroComprobante(cti.cti_id, alm.alm_id, bod.bod_id, comp.com_numero);
            }
            return "";
        }

        //NUEVA FUNCION PARA CONTROL DE REPETICIONES
        #region Control TOKEN

        public static long GetNewTokenComprobante(int empresa, string usr)
        {
            Token tok = new Token();
            tok.tok_empresa = empresa;
            tok.crea_usr = usr;
            tok.crea_fecha = DateTime.Now;
            tok.tok_codigo = TokenBLL.InsertIdentity(tok);
            return tok.tok_codigo;
        }

        public static bool UseToken(Comprobante comprobante)
        {
            if (comprobante.com_codigo == 0)
            {
                Token tok = TokenBLL.GetByPK(new Token { tok_empresa = comprobante.com_empresa, tok_empresa_key = comprobante.com_empresa, tok_codigo = comprobante.com_token.Value, tok_codigo_key = comprobante.com_token.Value });
                if (string.IsNullOrEmpty(tok.tok_data))
                {
                    tok.tok_empresa_key = comprobante.com_empresa;
                    tok.tok_codigo_key = comprobante.com_token.Value;
                    tok.tok_data = DateTime.Now + " - using";
                    tok.mod_fecha = DateTime.Now;
                    TokenBLL.Update(tok);
                    return true;
                }
                else
                    return false;
            }
            return true;

        }


        public static long GetSetComprobanteToken(Comprobante comprobante, ref bool update)
        {
            Token tok = TokenBLL.GetByPK(new Token { tok_empresa = comprobante.com_empresa, tok_empresa_key = comprobante.com_empresa, tok_codigo = comprobante.com_token.Value, tok_codigo_key = comprobante.com_token.Value });
            if (!tok.tok_comprobante.HasValue)
            {
                tok.tok_empresa = comprobante.com_empresa;
                tok.tok_empresa_key = comprobante.com_empresa;
                tok.tok_codigo = comprobante.com_token.Value;
                tok.tok_codigo_key = comprobante.com_token.Value;
                tok.tok_comprobante = comprobante.com_codigo;
                tok.tok_doctran = comprobante.com_doctran;
                tok.tok_data += DateTime.Now + "-" + comprobante.mod_usr + ",";
                tok.mod_usr = comprobante.mod_usr;
                tok.mod_fecha = DateTime.Now;
                TokenBLL.Update(tok);
                update = false;
            }
            else
                update = true;

            return tok.tok_comprobante.Value;
        }

        public static long? GetComprobanteToken(int empresa, long codigo)
        {
            Token tok = TokenBLL.GetByPK(new Token { tok_empresa = empresa, tok_empresa_key = empresa, tok_codigo = codigo, tok_codigo_key = codigo });
            return tok.tok_comprobante;
        }

        public static void UpdateTokenComprobante(Comprobante comprobante)
        {
            if (comprobante.com_token.HasValue)
            {
                Token tok = TokenBLL.GetByPK(new Token { tok_empresa = comprobante.com_empresa, tok_empresa_key = comprobante.com_empresa, tok_codigo = comprobante.com_token.Value, tok_codigo_key = comprobante.com_token.Value });
                tok.tok_empresa = comprobante.com_empresa;
                tok.tok_empresa_key = comprobante.com_empresa;
                tok.tok_codigo = comprobante.com_token.Value;
                tok.tok_codigo_key = comprobante.com_token.Value;
                tok.tok_comprobante = comprobante.com_codigo;
                tok.tok_doctran = comprobante.com_doctran;
                tok.tok_data += "," + DateTime.Now + " - saved by " + comprobante.mod_usr;
                tok.mod_usr = comprobante.mod_usr;
                tok.mod_fecha = DateTime.Now;
                TokenBLL.Update(tok);
            }
        }

        public static void UpdateTokenIsUsed(Comprobante comprobante)
        {
            if (comprobante.com_token.HasValue)
            {
                Token tok = TokenBLL.GetByPK(new Token { tok_empresa = comprobante.com_empresa, tok_empresa_key = comprobante.com_empresa, tok_codigo = comprobante.com_token.Value, tok_codigo_key = comprobante.com_token.Value });
                tok.tok_empresa_key = comprobante.com_empresa;
                tok.tok_codigo_key = comprobante.com_token.Value;
                tok.tok_data += "," + DateTime.Now + " - not saved token used " + comprobante.mod_usr;
                tok.mod_usr = comprobante.mod_usr;
                tok.mod_fecha = DateTime.Now;
                TokenBLL.Update(tok);
            }
        }

        #endregion

        public static string GetIdPersona(Empresa emp, Usuario usr)
        {

            Usuarioxempresa uxe = new Usuarioxempresa();
            uxe.uxe_usuario_key = usr.usr_id;
            uxe.uxe_empresa_key = emp.emp_codigo;
            uxe = UsuarioxempresaBLL.GetByPK(uxe);
            Almacen alm = new Almacen();
            alm.alm_codigo_key = uxe.uxe_almacen ?? 0;
            alm.alm_empresa_key = uxe.uxe_empresa;
            alm = AlmacenBLL.GetByPK(alm);
            int max = PersonaBLL.GetMax("per_codigo");
            return alm.alm_subfijo + string.Format("{0:0000}", max);
        }

        #region Modificacion Comprobante

        public static Comprobante ModificaDatosComprobante(Comprobante comp)
        {
            int numero = comp.com_numero;
            DateTime fecha = comp.com_fecha;

            string modusr = comp.mod_usr;
            DateTime? modfecha = comp.mod_fecha;

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;

            string doctran = comp.com_doctran;
            string newdoctran = doctran.Replace(comp.com_numero.ToString(), numero.ToString());
            comp.com_doctran = newdoctran;
            comp.com_numero = numero;
            comp.com_fecha = fecha;
            comp.com_periodo = fecha.Year;
            comp.com_mes = fecha.Month;
            comp.com_dia = fecha.Day;
            comp.mod_usr = modusr;
            comp.mod_fecha = modfecha;

            List<Dcontable> contables = DcontableBLL.GetAll(new WhereParams("dco_empresa = {0} and dco_doctran = {1}", comp.com_empresa, doctran), "");
            List<Dcancelacion> cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa = {0} and dca_doctran = {1}", comp.com_empresa, doctran), "");
            List<Ddocumento> documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa = {0} and ddo_doctran = {1}", comp.com_empresa, doctran), "");



            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();


                foreach (Dcontable item in contables)
                {
                    item.dco_empresa_key = item.dco_empresa;
                    item.dco_comprobante_key = item.dco_comprobante;
                    item.dco_secuencia_key = item.dco_secuencia;
                    item.dco_doctran = newdoctran;
                    DcontableBLL.Update(transaction, item);

                }
                foreach (Dcancelacion item in cancelaciones)
                {
                    item.dca_empresa_key = item.dca_empresa_key;
                    item.dca_comprobante_key = item.dca_comprobante;
                    item.dca_transacc_key = item.dca_transacc;
                    item.dca_doctran_key = item.dca_doctran;
                    item.dca_pago_key = item.dca_pago;
                    item.dca_comprobante_can_key = item.dca_comprobante_can;
                    item.dca_secuencia_key = item.dca_secuencia;
                    item.dca_doctran = newdoctran;
                    DcancelacionBLL.Update(transaction, item);

                }
                foreach (Ddocumento item in documentos)
                {
                    item.ddo_empresa_key = item.ddo_empresa_key;
                    item.ddo_comprobante_key = item.ddo_comprobante;
                    item.ddo_transacc_key = item.ddo_transacc;
                    item.ddo_doctran_key = item.ddo_doctran;
                    item.ddo_pago_key = item.ddo_pago;
                    item.ddo_doctran = newdoctran;
                    DdocumentoBLL.Update(transaction, item);
                }

                ComprobanteBLL.Update(transaction, comp);
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


        #endregion

        #region Anulación Comprobantes

        public static bool ValidaAnulaComprobante(Comprobante comp)
        {

            List<Dcancelacion> cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_comprobante = {0} and com_estado=2", comp.com_codigo), "");
            if (cancelaciones.Count > 0)
            {
                Comprobante can = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = cancelaciones[0].dca_empresa, com_empresa_key = cancelaciones[0].dca_empresa, com_codigo = cancelaciones[0].dca_comprobante_can, com_codigo_key = cancelaciones[0].dca_comprobante_can });
                throw new ArgumentException("No se puede anular el comprobante, antes debe anular el comprobante " + can.com_doctran);
            }

            /*WhereParams parametros = new WhereParams();
            int contador = 0;
            List<object> valores = new List<object>();
            foreach (Ddocumento item in comp.documentos)
            {
                if (item.ddo_cancela.HasValue)
                {
                    if (item.ddo_cancela.Value > 0)
                    {
                        parametros.where += ((parametros.where != "") ? " or " : "") + " dca_comprobante = {" + contador + "} ";
                        valores.Add(item.ddo_comprobante);
                        contador++;
                    }
                }
            }
            if (!string.IsNullOrEmpty(parametros.where))
            {
                parametros.valores = valores.ToArray();
                List<Dcancelacion> cancelaciones = DcancelacionBLL.GetAll(parametros, "");
                if (cancelaciones.Count > 0)
                {
                    Comprobante can = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = cancelaciones[0].dca_empresa, com_empresa_key = cancelaciones[0].dca_empresa, com_codigo = cancelaciones[0].dca_comprobante_can, com_codigo_key = cancelaciones[0].dca_comprobante_can });
                    throw new ArgumentException("No se puede anular el comprobante, antes debe anular el comprobante " + can.com_doctran);
                }
            }*/
            return true;

        }


        public static List<Ddocumento> GetDocumentosCancelaciones(List<Dcancelacion> cancelaciones)
        {
            WhereParams parametros = new WhereParams();
            int contador = 0;
            List<object> valores = new List<object>();
            foreach (Dcancelacion item in cancelaciones)
            {
                parametros.where += ((parametros.where != "") ? " or " : "") + " ddo_comprobante = {" + contador + "} ";
                valores.Add(item.dca_comprobante);
                contador++;

            }
            List<Ddocumento> documentos = new List<Ddocumento>();

            if (!string.IsNullOrEmpty(parametros.where))
            {
                parametros.valores = valores.ToArray();
                documentos = DdocumentoBLL.GetAll(parametros, "");
            }
            return documentos;
        }

        public static Comprobante ActivarComprobante(Comprobante comp)
        {
            string concepto = comp.com_concepto;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            if (concepto != "")
                comp.com_concepto = concepto;
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            try
            {

                comp.com_estado = Constantes.cEstadoGrabado;
                ComprobanteBLL.Update(comp);
                General.save_historial(comp);
                if (comp.com_tipodoc == Constantes.cFactura.tpd_codigo)
                    FAC.account_factura(comp);

            }
            catch (Exception ex)
            {
                comp.com_estado = Constantes.cEstadoEliminado;
                ComprobanteBLL.Update(comp);
                ExceptionHandling.Log.AddExepcion(ex);
                throw ex;
            }
            return comp;


        }
        
        public static Comprobante AnulaComprobante(Comprobante comp)
        {
            Comprobante comorg = comp;

            string concepto = comp.com_concepto;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            if (!string.IsNullOrEmpty(concepto))
                comp.com_concepto = concepto;
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });
            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
            List<Ddocumento> documentoscancelaciones = GetDocumentosCancelaciones(comp.cancelaciones);

            comp.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_empresa ={0} and rfac_comprobanteruta={1}", comp.com_empresa, comp.com_codigo), "");
            comp.planillas = PlanillacliBLL.GetAll(new WhereParams("plc_empresa ={0} and plc_comprobante_pla={1}", comp.com_empresa, comp.com_codigo), "");


            /*Elimina el comprobante de la hoja de ruta y resta el valor*/
            List<Rutaxfactura> lsthojas = RutaxfacturaBLL.GetAll(new WhereParams("rfac_empresa ={0} and rfac_comprobantefac={1}", comp.com_empresa, comp.com_codigo), "");
            Comprobante hr = new Comprobante();
            if (lsthojas.Count > 0)
            {
                hr = ComprobanteBLL.GetByPK(new Comprobante { com_codigo = lsthojas[0].rfac_comprobanteruta, com_codigo_key = lsthojas[0].rfac_comprobanteruta, com_empresa = lsthojas[0].rfac_empresa, com_empresa_key = lsthojas[0].rfac_empresa });
                hr.total = TotalBLL.GetByPK(new Total { tot_empresa = lsthojas[0].rfac_empresa, tot_empresa_key = lsthojas[0].rfac_empresa, tot_comprobante = lsthojas[0].rfac_comprobanteruta, tot_comprobante_key = lsthojas[0].rfac_comprobanteruta });
                hr.total.tot_total -= comp.total.tot_total;
            }

            /****/

            /*Elimina el comprobante de la planilla comprobante*/
            List<Planillacomprobante> lstplanillacomp = PlanillacomprobanteBLL.GetAll(new WhereParams("pco_empresa ={0} and pco_comprobante_fac={1}", comp.com_empresa, comp.com_codigo), "");
            /****/
            /*Elimina el comprobante de la planilla de clientes y resta el valor*/
            List<Planillacli> lstplanilla = PlanillacliBLL.GetAll(new WhereParams("plc_empresa ={0} and plc_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            Comprobante pc = new Comprobante();
            if (lstplanilla.Count > 0)
            {
                pc = ComprobanteBLL.GetByPK(new Comprobante { com_codigo = lstplanilla[0].plc_comprobante_pla, com_codigo_key = lstplanilla[0].plc_comprobante_pla, com_empresa = lstplanilla[0].plc_empresa, com_empresa_key = lstplanilla[0].plc_empresa });

                pc.total = TotalBLL.GetByPK(new Total { tot_empresa = lstplanilla[0].plc_empresa, tot_empresa_key = lstplanilla[0].plc_empresa, tot_comprobante = lstplanilla[0].plc_comprobante_pla, tot_comprobante_key = lstplanilla[0].plc_comprobante_pla });
                pc.total.tot_total -= comp.total.tot_total;
            }

            /****/



            if (ValidaAnulaComprobante(comp))
            {

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
                    foreach (Ddocumento item in comp.documentos)
                    {
                        DdocumentoBLL.Delete(transaction, item);
                    }
                    foreach (Dcancelacion item in comp.cancelaciones)
                    {                        
                        Ddocumento doccan = documentoscancelaciones.Find(delegate (Ddocumento d) { return d.ddo_comprobante == item.dca_comprobante && d.ddo_empresa == item.dca_empresa && d.ddo_transacc == item.dca_transacc && d.ddo_doctran == item.dca_doctran && d.ddo_pago == item.dca_pago; });
                        if (doccan != null)
                        {


                            doccan.ddo_cancela = doccan.ddo_cancela - item.dca_monto;
                            //if (doccan.ddo_monto > doccan.ddo_cancela)
                            doccan.ddo_cancelado = 0;
                            //if (doccan.ddo_cancela == 0)
                            //    doccan.ddo_cancelado = 0;
                            doccan.ddo_empresa_key = doccan.ddo_empresa;
                            doccan.ddo_comprobante_key = doccan.ddo_comprobante;
                            doccan.ddo_transacc_key = doccan.ddo_transacc;
                            doccan.ddo_doctran_key = doccan.ddo_doctran;
                            doccan.ddo_pago_key = doccan.ddo_pago;
                            DdocumentoBLL.Update(transaction, doccan);
                        }

                        DcancelacionBLL.Delete(transaction, item);
                    }
                    foreach (Rutaxfactura item in comp.rutafactura)
                    {
                        RutaxfacturaBLL.Delete(transaction, item);
                    }

                    foreach (Planillacli item in comp.planillas)
                    {
                        PlanillacliBLL.Delete(transaction, item);
                    }

                    foreach (Dbancario item in comp.bancario)
                    {
                        DbancarioBLL.Delete(transaction, item);
                    }




                    #endregion


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
                    #region Elimina de la Planilla Clientes  y actualiza valores


                    foreach (Planillacli item in lstplanilla)
                    {
                        PlanillacliBLL.Delete(transaction, item);
                    }
                    if (pc.total != null)
                    {
                        pc.total.tot_comprobante_key = pc.total.tot_comprobante;
                        pc.total.tot_empresa_key = pc.total.tot_empresa;
                        TotalBLL.Update(transaction, pc.total);


                    }



                    #endregion

                    #region Elimina Planilla Comprobante
                    foreach (Planillacomprobante item in lstplanillacomp)
                    {
                        PlanillacomprobanteBLL.Delete(transaction, item);
                    }

                    #endregion

                    #region elimina factura ligada

                    comp.ccomdoc.cdoc_factura = null;
                    comp.ccomdoc.cdoc_empresa_key = comp.ccomdoc.cdoc_empresa;
                    comp.ccomdoc.cdoc_comprobante_key = comp.ccomdoc.cdoc_comprobante;
                    CcomdocBLL.Update(transaction, comp.ccomdoc);

                    #endregion
                    comp.com_estado = Constantes.cEstadoEliminado;
                    comp.mod_usr = comorg.mod_usr;
                    comp.mod_fecha = comorg.mod_fecha;

                    ComprobanteBLL.Update(transaction, comp);
                    General.save_historial(transaction, comp);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }


            foreach (Dcancelacion item in comp.cancelaciones)
            {
                Auto.actualiza_documentos(item.dca_empresa, null, item.dca_comprobante, 0, null);
            }

            return comp;


        }


        public static Comprobante AnulaComprobanteDiario(Comprobante comp)
        {
            Comprobante comorg = comp;

            string concepto = comp.com_concepto;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);

            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            if (concepto != "")
                comp.com_concepto = concepto;
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.total = TotalBLL.GetByPK(new Total { tot_empresa = comp.com_empresa, tot_empresa_key = comp.com_empresa, tot_comprobante = comp.com_codigo, tot_comprobante_key = comp.com_codigo });

            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");
            List<Ddocumento> documentoscancelaciones = GetDocumentosCancelaciones(comp.cancelaciones);

            /*Elimina el comprobante de la hoja de ruta y resta el valor*/



            if (ValidaAnulaComprobante(comp))
            {

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
                        Ddocumento doccan = documentoscancelaciones.Find(delegate (Ddocumento d) { return d.ddo_comprobante == item.dca_comprobante && d.ddo_empresa == item.dca_empresa && d.ddo_transacc == item.dca_transacc && d.ddo_doctran == item.dca_doctran && d.ddo_pago == item.dca_pago; });
                        if (doccan != null)
                        {
                            doccan.ddo_cancela = doccan.ddo_cancela - item.dca_monto;
                            //if (doccan.ddo_cancela == 0)
                            //if (item.dca_monto> 0)
                            doccan.ddo_cancelado = 0;
                            doccan.ddo_empresa_key = doccan.ddo_empresa;
                            doccan.ddo_comprobante_key = doccan.ddo_comprobante;
                            doccan.ddo_transacc_key = doccan.ddo_transacc;
                            doccan.ddo_doctran_key = doccan.ddo_doctran;
                            doccan.ddo_pago_key = doccan.ddo_pago;
                            DdocumentoBLL.Update(transaction, doccan);
                        }
                        DcancelacionBLL.Delete(transaction, item);
                    }

                    /*foreach (Dbancario item in comp.bancario)
                    {
                        DbancarioBLL.Delete(transaction, item);
                    }*/




                    #endregion


                    comp.com_estado = Constantes.cEstadoEliminado;
                    comp.mod_usr = comorg.mod_usr;
                    comp.mod_fecha = comorg.mod_fecha;

                    ComprobanteBLL.Update(transaction, comp);
                    General.save_historial(transaction, comp);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return comp;


        }

        #endregion


        #endregion

        #region Cancelaciones



        public static Comprobante CreateCancelacion(Comprobante comp, List<Drecibo> detalle, ref string mensaje)
        {
            DateTime fecha = DateTime.Now;
            int ctipocom = 3; // SE DEBE OBTENER DE ALGUN LADO ?????
            Dtipocom dti = new Dtipocom();
            dti.dti_empresa = comp.com_empresa;
            dti.dti_empresa_key = comp.com_empresa;
            dti.dti_periodo = comp.com_periodo;
            dti.dti_periodo_key = comp.com_periodo;
            dti.dti_ctipocom = ctipocom;
            dti.dti_ctipocom_key = ctipocom;
            dti.dti_almacen = comp.com_almacen.Value;
            dti.dti_almacen_key = comp.com_almacen.Value;
            dti.dti_puntoventa = comp.com_pventa.Value;
            dti.dti_puntoventa_key = comp.com_pventa.Value;
            dti = DtipocomBLL.GetByPK(dti);
            dti.dti_numero = dti.dti_numero.Value + 1;

            Comprobante c = new Comprobante();
            c.com_empresa = comp.com_empresa;
            c.com_periodo = comp.com_periodo;
            c.com_tipodoc = Constantes.cRecibo.tpd_codigo;
            c.com_ctipocom = ctipocom;
            c.com_fecha = fecha;
            c.com_almacen = comp.com_almacen;
            c.com_pventa = comp.com_pventa;
            c.com_codclipro = comp.com_codclipro;
            c.com_tclipro = comp.com_tclipro;
            c.com_agente = comp.com_agente;
            //c.com_serie 
            c.com_numero = dti.dti_numero.Value;
            c.com_concepto = "CANCELACION";
            c.com_modulo = GetModulo(c.com_tipodoc); ;
            c.com_transacc = GetTransacc(c.com_tipodoc);
            c.com_nocontable = 0;
            c.com_estado = 1;
            c.com_descuadre = 0;
            c.com_adestino = 0;
            c.com_doctran = GetNumeroComprobante(c);

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                c.com_codigo = ComprobanteBLL.InsertIdentity(transaction, c);//GUARDA CABECERA CANCELACION
                int contador = 0;
                foreach (Drecibo drec in detalle)
                {
                    drec.dfp_empresa = c.com_empresa;
                    drec.dfp_comprobante = c.com_codigo;
                    drec.dfp_secuencia = contador;
                    drec.dfp_tclipro = comp.com_tclipro;
                    drec.dfp_fecha_ven = fecha;
                    drec.dfp_ref_comprobante = comp.com_codigo;
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
                    dca.dca_debcre = (doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                    dca.dca_transacc = c.com_transacc;
                    dca.dca_tipo_cambio = doc.ddo_tipo_cambio;
                    dca.dca_monto = doc.ddo_monto;
                    dca.dca_monto_ext = doc.ddo_monto;
                    DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION
                    contador++;

                    //Cambia el estado del documento que tambien se debe actualizar
                    doc.ddo_empresa_key = doc.ddo_empresa;
                    doc.ddo_comprobante_key = doc.ddo_comprobante;
                    doc.ddo_transacc_key = doc.ddo_transacc;
                    doc.ddo_doctran_key = doc.ddo_doctran;
                    doc.ddo_pago_key = doc.ddo_pago;
                    doc.ddo_cancela = dca.dca_monto;
                    doc.ddo_cancelado = 1;
                    DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                    //
                }

                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                General.save_historial(transaction, c);
                transaction.Commit();
                mensaje = "OK";
            }
            catch
            {
                transaction.Rollback();
                mensaje = "ERROR";
            }

            return c;

        }


        #endregion

        #region Reteciones

        public static Comprobante GenRetencion(int empresa, long comprobante, ref string mensaje)
        {
            Comprobante comp = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = empresa, com_empresa_key = empresa, com_codigo = comprobante, com_codigo_key = comprobante });

            comp.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });

            comp.total = new Total();
            comp.total.tot_empresa = empresa;
            comp.total.tot_empresa_key = empresa;
            comp.total.tot_comprobante = comprobante;
            comp.total.tot_comprobante_key = comprobante;
            comp.total = TotalBLL.GetByPK(comp.total);

            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1} and ddo_cancela=0", empresa, comprobante), "");

            return GenRetencion(comp, ref mensaje);
        }


        public static Comprobante GenRetencion(Comprobante comp, ref string mensaje)
        {
            List<Dretencion> lst = DretencionBLL.GetAll(new WhereParams("drt_empresa ={0} and drt_factura = {1} ", comp.com_empresa, comp.com_doctran), "");
            if (lst.Count > 0)
            {
                mensaje = "ERROR";
                return null;
            }
            int ctipocom = 0;
            DateTime fecha = DateTime.Now;
            //  int ctipocom = Constantes.cComRetencion.cti_codigo;
            List<Ctipocom> list = CtipocomBLL.GetAll(new WhereParams("cti_empresa={0} and cti_id = {1}", comp.com_empresa, "RET"), "");
            if (list.Count > 0) {

                ctipocom = list[0].cti_codigo;

            }
            Dtipocom dti = new Dtipocom();
            dti.dti_empresa = comp.com_empresa;
            dti.dti_empresa_key = comp.com_empresa;
            dti.dti_periodo = comp.com_periodo;
            dti.dti_periodo_key = comp.com_periodo;
            dti.dti_ctipocom = ctipocom;
            dti.dti_ctipocom_key = ctipocom;
            dti.dti_almacen = comp.com_almacen.Value;
            dti.dti_almacen_key = comp.com_almacen.Value;
            dti.dti_puntoventa = comp.com_pventa.Value;
            dti.dti_puntoventa_key = comp.com_pventa.Value;
            dti = DtipocomBLL.GetByPK(dti);
            dti.dti_numero = dti.dti_numero.Value + 1;


            Persona per = PersonaBLL.GetByPK(new Persona { per_empresa = comp.com_empresa, per_empresa_key = comp.com_empresa, per_codigo = comp.com_codclipro.Value, per_codigo_key = comp.com_codclipro.Value });


            Impuesto impiva = ImpuestoBLL.GetByPK(new Impuesto { imp_empresa = comp.com_empresa, imp_empresa_key = comp.com_empresa, imp_codigo = per.per_retiva.Value, imp_codigo_key = per.per_retiva.Value });
            Impuesto impfue = ImpuestoBLL.GetByPK(new Impuesto { imp_empresa = comp.com_empresa, imp_empresa_key = comp.com_empresa, imp_codigo = per.per_retfuente.Value, imp_codigo_key = per.per_retfuente.Value });


            Comprobante c = new Comprobante();
            c.com_empresa = comp.com_empresa;
            c.com_periodo = comp.com_periodo;
            c.com_tipodoc = Constantes.cRetencion.tpd_codigo;
            c.com_ctipocom = ctipocom;
            c.com_tclipro = Constantes.cProveedor;
            c.com_fecha = fecha;
            c.com_almacen = comp.com_almacen;
            c.com_pventa = comp.com_pventa;
            c.com_codclipro = comp.com_codclipro;
            c.com_agente = comp.com_agente;
            //c.com_serie 
            c.com_numero = dti.dti_numero.Value;
            c.com_concepto = "RETENCION " + per.per_apellidos + " " + per.per_nombres;
            c.com_modulo = GetModulo(c.com_tipodoc); ;
            c.com_transacc = GetTransacc(c.com_tipodoc);
            c.com_nocontable = 0;
            c.com_estado = Constantes.cEstadoProceso;
            c.com_descuadre = 0;
            c.com_adestino = 0;
            c.com_doctran = GetNumeroComprobante(c);
            c.com_centro = Constantes.GetSinCentro().cen_codigo;
            c.com_dia = c.com_fecha.Day;
            c.com_mes = c.com_fecha.Month;
            c.com_anio = c.com_fecha.Year;
            c.crea_usr = comp.crea_usr;
            c.crea_fecha = DateTime.Now;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                c.com_codigo = ComprobanteBLL.InsertIdentity(transaction, c);//GUARDA CABECERA CANCELACION
                int contador = 0;

                c.ccomdoc = new Ccomdoc();
                c.ccomdoc.cdoc_empresa = c.com_empresa;
                c.ccomdoc.cdoc_comprobante = c.com_codigo;
                c.ccomdoc.cdoc_factura = comp.com_codigo; //LIGA LA RETENCION CON LA FACTURA
                c.ccomdoc.cdoc_nombre = per.per_apellidos + " " + per.per_nombres;
                c.ccomdoc.cdoc_direccion = per.per_direccion;
                c.ccomdoc.cdoc_telefono = per.per_telefono;
                c.ccomdoc.cdoc_ced_ruc = per.per_ciruc;
                CcomdocBLL.Insert(transaction, c.ccomdoc);

                Dretencion retiva = new Dretencion();
                retiva.drt_empresa = c.com_empresa;
                retiva.drt_comprobante = c.com_codigo;
                retiva.drt_impuesto = impiva.imp_codigo;
                //retiva.drt_cuenta= 
                retiva.drt_valor = comp.total.tot_timpuesto;
                retiva.drt_porcentaje = impiva.imp_porcentaje;
                retiva.drt_total = retiva.drt_valor * (retiva.drt_porcentaje / 100);
                retiva.drt_debcre = Constantes.cDebito;
                //retiva.drt_cuenta_transacc 
                //retiva.drt_con_codigo
                //retiva.drt_factura = comp.com_doctran;
                retiva.drt_factura = comp.ccomdoc.cdoc_aut_factura;
                DretencionBLL.Insert(transaction, retiva);


                Dretencion retfue = new Dretencion();
                retfue.drt_empresa = c.com_empresa;
                retfue.drt_comprobante = c.com_codigo;
                retfue.drt_impuesto = impfue.imp_codigo;
                //retfue.drt_cuenta= 
                retfue.drt_valor = comp.total.tot_subtotal + comp.total.tot_subtot_0;
                retfue.drt_porcentaje = impfue.imp_porcentaje;
                retfue.drt_total = retfue.drt_valor * (retfue.drt_porcentaje / 100);
                retfue.drt_debcre = Constantes.cDebito;
                //retfue.drt_cuenta_transacc 
                //retfue.drt_con_codigo
                retfue.drt_factura = comp.com_doctran;
                DretencionBLL.Insert(transaction, retfue);


                ////CANCELACION DEL DOCUMENTO A PARTIR DEL VALOR DE LA RETENCION
                decimal totaretencion = retiva.drt_total.Value + retfue.drt_total.Value;
                foreach (Ddocumento doc in comp.documentos)
                {
                    Dcancelacion dca = new Dcancelacion();
                    //dca.dca_empresa = doc.ddo_empresa;
                    //dca.dca_comprobante = doc.ddo_comprobante;
                    dca.dca_empresa = c.com_empresa;
                    dca.dca_comprobante = comp.com_codigo;
                    dca.dca_transacc = doc.ddo_transacc;
                    dca.dca_doctran = doc.ddo_doctran;
                    dca.dca_pago = doc.ddo_pago;
                    dca.dca_comprobante_can = c.com_codigo;
                    dca.dca_secuencia = 0;
                    dca.dca_debcre = (doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                    dca.dca_transacc = comp.com_transacc;
                    dca.dca_tipo_cambio = doc.ddo_tipo_cambio;
                    if (totaretencion > doc.ddo_monto.Value)
                    {
                        dca.dca_monto = doc.ddo_monto;
                        dca.dca_monto_ext = doc.ddo_monto;
                    }
                    else
                    {
                        dca.dca_monto = totaretencion;
                        dca.dca_monto_ext = totaretencion;
                    }
                    DcancelacionBLL.Insert(transaction, dca);   //GUARDA EL DETALLE DE CANCELACION

                    //Cambia el estado del documento que tambien se debe actualizar
                    doc.ddo_empresa_key = doc.ddo_empresa;
                    doc.ddo_comprobante_key = doc.ddo_comprobante;
                    doc.ddo_transacc_key = doc.ddo_transacc;
                    doc.ddo_doctran_key = doc.ddo_doctran;
                    doc.ddo_pago_key = doc.ddo_pago;
                    doc.ddo_cancela = ((doc.ddo_cancela.HasValue) ? doc.ddo_cancela.Value : 0) + dca.dca_monto;
                    if (doc.ddo_cancela >= doc.ddo_monto)
                        doc.ddo_cancelado = 1;
                    DdocumentoBLL.Update(transaction, doc);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                    if (totaretencion > doc.ddo_monto.Value)
                        totaretencion = totaretencion - doc.ddo_monto.Value;
                    else
                        break;
                    //
                }



                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                General.save_historial(transaction, c);
                transaction.Commit();
                mensaje = "OK";
            }
            catch
            {
                transaction.Rollback();
                mensaje = "ERROR";
            }

            return c;

        }


        #endregion


        #region Saldos
        /*  public static Decimal SaldoCuenta(string Pv_indica, int Pn_debcre, int Pn_Nac_ext, int empresa, int cuenta, int Pn_centro, int Pn_almacen, int Pn_transacc, DateTime Pn_fecha)
          {
              int Vn_periodo = Pn_fecha.Year;
              int Vn_mes = Pn_fecha.Month;
              int Vn_dia = Pn_fecha.Day;
              int? Vn_mes_ini = 0;
              int? Vn_mes_fin = 0;
              decimal Vn_Saldo = 0;
              decimal Vn_Saldo_ext = 0;
              if (Pv_indica.Equals("i"))
              {
                  Vn_mes_ini = 0;
                  Vn_mes_fin = 0;
              }
              else if (Pv_indica.Equals("a"))
              {
                  if (Pn_debcre == 3)
                  {
                      Vn_mes_ini = 0;
                      Vn_mes_fin = Vn_mes - 1;
                      if (Vn_mes_fin < 0)
                          Vn_mes_fin = 0;
                  }
                  else
                  {
                      Vn_mes_ini = null;
                      Vn_mes_fin = null;
                  }
              }
              else if (Pv_indica.Equals("m"))
              {
                  if (Pn_debcre == 3)
                      Vn_mes_ini = 0;
                  else
                      Vn_mes_ini = Vn_mes;
                  Vn_mes_fin = Vn_mes;
              }
              List<vCuentas> lst = vCuentasBLL.GetAll(new WhereParams("cue_empresa ={0} and cue_codigo={1}", empresa, cuenta), "cue_id");
              foreach (vCuentas obj in lst)
              {
                  string where = "sal_mes BETWEEN {0} and {1} and sal_periodo={2} " +
                  "AND (COALESCE({3},0)    = 0 OR COALESCE(sal_transacc,0) = COALESCE({3},0)) " +
                  "AND (COALESCE({4},0)    = 0 OR COALESCE(sal_almacen,0)  = COALESCE({4},0)) " +
                  "AND (COALESCE({5},0)      = 0 OR COALESCE(sal_centro,0)   = COALESCE({5},0)) " +
                  "AND sal_cuenta       = {6} " +
                  "AND sal_empresa     = {7} ";
                  List<Saldo> lst2 = SaldoBLL.GetAll(new WhereParams(where, Vn_mes_ini, Vn_mes_fin, Vn_periodo, Pn_transacc, Pn_almacen, Pn_centro, obj.cue_codigo, empresa), "");
                  foreach (Saldo item in lst2)
                  {
                      if (Pn_debcre == 1 || Pn_debcre == 3 || Pn_debcre == 4)
                      {
                          Vn_Saldo = Vn_Saldo + item.sal_debito;
                          Vn_Saldo_ext = Vn_Saldo_ext + item.sal_debext;
                      }
                      if (Pn_debcre == 2 || Pn_debcre == 3 || Pn_debcre == 4)
                      {
                          Vn_Saldo = Vn_Saldo - item.sal_credito;
                          Vn_Saldo_ext = Vn_Saldo_ext - item.sal_creext;
                      }
                  }

                  var date1 = new DateTime(Pn_fecha.Year, Pn_fecha.Month, 1, Pn_fecha.Hour, Pn_fecha.Minute, Pn_fecha.Second);
                  if (Pv_indica.Equals("a"))
                  {
                      string wheresaldo = " dco_cuenta      = {0} " +
                                          " AND dco_empresa     = {1} " +
                                          " AND dco_debcre      = {2} " +
                                          " and  com_fecha BETWEEN {3} and {4} " +
                                          " AND (com_codigo     = dco_comprobante " +
                                          " AND  com_empresa    = dco_empresa) " +
                                          " AND  com_estado     IN ({8},{9})  " +
                                          " AND  com_nocontable = 0 " +
                                          " AND (COALESCE({5},0)= 0 OR COALESCE(dco_almacen,0)= COALESCE({5},0)) " +
                                          " AND (COALESCE({6},0)= 0 OR COALESCE(dco_transacc,0)= COALESCE({6},0)) " +
                                          " AND (COALESCE({7},0)= 0 OR COALESCE(dco_centro,0)=COALESCE({7},0));";
                      if (Pn_debcre == 1 || Pn_debcre == 3)
                      {
                          List<vDcontable> lst3 = vDcontableBLL.GetAll(new WhereParams(wheresaldo, obj.cue_codigo, empresa, 1, date1, Pn_fecha, Pn_almacen, Pn_transacc, Pn_centro, Constantes.cEstadoPorAutorizar, Constantes.cEstadoMayorizado), "");
                          foreach (vDcontable item in lst3)
                          {
                              Vn_Saldo = Vn_Saldo + item.dco_valor_nac ?? 0; ;
                              Vn_Saldo_ext = Vn_Saldo_ext + item.dco_valor_ext ?? 0; ;
                          }
                      }
                      if (Pn_debcre == 2 || Pn_debcre == 3)
                      {
                          List<vDcontable> lst3 = vDcontableBLL.GetAll(new WhereParams(wheresaldo, obj.cue_codigo, empresa, 2, date1, Pn_fecha, Pn_almacen, Pn_transacc, Pn_centro), "");
                          foreach (vDcontable item in lst3)
                          {
                              Vn_Saldo = Vn_Saldo - item.dco_valor_nac ?? 0; ;
                              Vn_Saldo_ext = Vn_Saldo_ext - item.dco_valor_ext ?? 0; ;
                          }
                      }
                  }
              }
              if (Pn_debcre == 2)
              {
                  Vn_Saldo = Vn_Saldo * -1;
                  Vn_Saldo_ext = Vn_Saldo_ext * -1;
              }
              if (Pn_Nac_ext == 2)
              {
                  return Vn_Saldo_ext;
              }
              else
              {
                  return Vn_Saldo;
              }
          }

          */

        public static Decimal SaldoBancos(string Pv_indica, int Pn_debcre, int Pn_Nac_ext, int Pn_empresa, int Pn_banco, int Pn_almacen, int Pn_transacc, DateTime Pn_fecha)
        {
            int Vn_periodo = Pn_fecha.Year;
            int Vn_mes = Pn_fecha.Month;
            int Vn_dia = Pn_fecha.Day;
            int? Vn_mes_ini = 0;
            int? Vn_mes_fin = 0;
            decimal Vn_Saldo = 0;
            decimal Vn_Saldo_ext = 0;

            if (Pv_indica.Equals("i"))
            {
                Vn_mes_ini = 0;
                Vn_mes_fin = 0;
            }
            else if (Pv_indica.Equals("a"))
            {
                if (Pn_debcre == 3)
                {
                    Vn_mes_ini = 0;
                    Vn_mes_fin = Vn_mes - 1;
                    if (Vn_mes_fin < 0)
                        Vn_mes_fin = 0;
                }
                else
                {
                    Vn_mes_ini = null;
                    Vn_mes_fin = null;
                }
            }
            else if (Pv_indica.Equals("m"))
            {
                if (Pn_debcre == 3)
                    Vn_mes_ini = 0;
                else
                    Vn_mes_ini = Vn_mes;
                Vn_mes_fin = Vn_mes;
            }

            string where =
          "slb_empresa = {0} " +
          "AND slb_banco  = {1} " +
          "AND slb_periodo = {2} " +
          "AND slb_mes BETWEEN {3} AND {4} " +
          "AND (COALESCE({5},0) = 0 OR COALESCE(slb_almacen,0)  = COALESCE({5},0)) " +
          "AND (COALESCE({6},0) = 0 OR COALESCE(slb_transacc,0) = COALESCE({6},0)) ";
            List<Salban> lst1 = SalbanBLL.GetAll(new WhereParams(where, Pn_empresa, Pn_banco, Vn_periodo, Vn_mes_ini, Vn_mes_fin, Pn_almacen, Pn_transacc), "");
            foreach (Salban item in lst1)
            {
                if (Pn_debcre == 1 || Pn_debcre == 3 || Pn_debcre == 4)
                {
                    Vn_Saldo = Vn_Saldo + item.slb_debito ?? 0;
                    Vn_Saldo_ext = Vn_Saldo_ext + item.slb_debext ?? 0;
                }
                if (Pn_debcre == 2 || Pn_debcre == 3 || Pn_debcre == 4)
                {
                    Vn_Saldo = Vn_Saldo - item.slb_credito ?? 0;
                    Vn_Saldo_ext = Vn_Saldo_ext - item.slb_creext ?? 0;
                }
            }
            //var date1 = new DateTime(Pn_fecha.Year, Pn_fecha.Month, 1, Pn_fecha.Hour, Pn_fecha.Minute, Pn_fecha.Second);
            var date1 = new DateTime(Pn_fecha.Year, Pn_fecha.Month, 1);
            if (Pv_indica.Equals("a"))
            {
                string wheresaldo = "  dban_banco       =  {0} " +
                                    "and dban_empresa     =  {1} " +
                                    "AND dban_debcre      =  {2} " +
                                    "and  com_fecha BETWEEN {3} and {4} " +
                                    "AND (COALESCE( {5},0)    = 0 OR COALESCE(com_almacen,0)  = COALESCE( {5},0)) " +
                                    "AND (COALESCE( {6},0)    = 0 OR COALESCE(dban_transacc,0) = COALESCE( {6},0)) " +
                                    "AND com_empresa    = dban_empresa " +
                                    "AND  com_codigo     = dban_cco_comproba " +
                                    "AND  com_estado    IN ({7},{8}) " +
                                    "AND  com_nocontable = 0 ";

                if (Pn_debcre == 1 || Pn_debcre == 3)
                {
                    List<vDbancario> lst3 = vDbancarioBLL.GetAll(new WhereParams(wheresaldo, Pn_banco, Pn_empresa, 1, date1, Pn_fecha, Pn_almacen, Pn_transacc, Constantes.cEstadoPorAutorizar, Constantes.cEstadoMayorizado), "");
                    foreach (vDbancario item in lst3)
                    {
                        Vn_Saldo = Vn_Saldo + item.dban_valor_nac ?? 0; ;
                        Vn_Saldo_ext = Vn_Saldo_ext + item.dban_valor_ext ?? 0; ;
                    }
                }
                if (Pn_debcre == 2 || Pn_debcre == 3)
                {
                    List<vDbancario> lst3 = vDbancarioBLL.GetAll(new WhereParams(wheresaldo, Pn_banco, Pn_empresa, 2, date1, Pn_fecha, Pn_almacen, Pn_transacc, Constantes.cEstadoPorAutorizar, Constantes.cEstadoMayorizado), "");
                    foreach (vDbancario item in lst3)
                    {
                        Vn_Saldo = Vn_Saldo - item.dban_valor_nac ?? 0; ;
                        Vn_Saldo_ext = Vn_Saldo_ext - item.dban_valor_ext ?? 0; ;
                    }
                }
            }

            if (Pn_debcre == 2)
            {
                Vn_Saldo = Vn_Saldo * -1;
                Vn_Saldo_ext = Vn_Saldo_ext * -1;
            }
            if (Pn_Nac_ext == 2)
            {
                return Vn_Saldo_ext;
            }
            else
            {
                return Vn_Saldo;
            }
        }
        #endregion
        #region Autorizaciones

        public static Autpersona GetAutorizacion(Comprobante comprobante, ref string mensaje)
        {
            bool encontro = false;
            mensaje = "";
            Persona informante = new Persona();
            informante.per_empresa = comprobante.com_empresa;
            informante.per_empresa_key = comprobante.com_empresa;

            //s Empresa emp = (Empresa)Session["empresa"];
            //  Empresa emp = (Empresa)HttpContext.Current.Session["empresa"];
            Empresa emp = new Empresa();
            emp.emp_codigo_key = Dictionaries.cod_empresa;
            emp = EmpresaBLL.GetByPK(emp);


            informante.per_codigo = emp.emp_informante ?? 0;//DEBE OBTENERSE DE EMPRESA
            informante.per_codigo_key = emp.emp_informante ?? 0;

            informante = PersonaBLL.GetByPK(informante);

            Ctipocom ctipocom = new Ctipocom();
            ctipocom.cti_empresa = comprobante.com_empresa;
            ctipocom.cti_empresa_key = comprobante.com_empresa;
            ctipocom.cti_codigo = comprobante.com_ctipocom;
            ctipocom.cti_codigo_key = comprobante.com_ctipocom;

            ctipocom = CtipocomBLL.GetByPK(ctipocom);

            List<Autpersona> lst = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_fac1={2} and ape_fac2={3} and ape_retdato={4} and ape_estado=1", comprobante.com_empresa, informante.per_codigo, comprobante.com_almacenid, comprobante.com_pventaid, ctipocom.cti_retdato), "ape_val_fecha");

            foreach (Autpersona item in lst)
            {
                int desde = int.Parse(item.ape_fac3);
                int hasta = int.Parse(item.ape_fact3);
                if ((comprobante.com_numero >= desde && comprobante.com_numero <= hasta) && DateTime.Compare(comprobante.com_fecha, item.ape_val_fecha.Value) < 0)
                {
                    encontro = true;
                    int faltan = int.Parse(item.ape_fact3) - comprobante.com_numero;
                    if (faltan < Constantes.cCantidadFacturas)
                        mensaje = "FALTAN " + faltan + " COMPROBANTES PARA LLEGAR A RANGO FINAL AUTORIZADO PARA EMISION DE COMPROBANTE POR EL SRI ";
                    int dias = item.ape_val_fecha.Value.Subtract(comprobante.com_fecha).Days;
                    if (dias < Constantes.cDiasAutorizacion)
                        mensaje += "FALTAN " + dias + " DIAS PARA LLEGAR A FECHA FINAL AUTORIZADA PARA EMISION DE COMPROBANTE POR EL SRI ";

                    return item;
                }
            }
            if (!encontro)
                mensaje = "No existe autorización vigente, revise autorización ";
            return null;


        }

        public static Autpersona GetAutorizacionComprobante(Comprobante comprobante)
        {
            Persona informante = new Persona();
            informante.per_empresa = comprobante.com_empresa;
            informante.per_empresa_key = comprobante.com_empresa;
            Empresa emp = new Empresa();
            emp.emp_codigo_key = comprobante.com_empresa;
            emp = EmpresaBLL.GetByPK(emp);

            informante.per_codigo = emp.emp_informante.Value;//DEBE OBTENERSE DE EMPRESA
            informante.per_codigo_key = emp.emp_informante.Value;

            informante = PersonaBLL.GetByPK(informante);

            if (comprobante.ccomdoc == null)
                comprobante.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comprobante.com_empresa, cdoc_empresa_key = comprobante.com_empresa, cdoc_comprobante = comprobante.com_codigo, cdoc_comprobante_key = comprobante.com_codigo });


            List<Autpersona> lst = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_nro_autoriza={2}", comprobante.com_empresa, informante.per_codigo, comprobante.ccomdoc.cdoc_acl_nroautoriza), "ape_val_fecha");
            if (lst.Count > 0)
            {
                return lst[0];
            }

            return null;


        }


        #endregion

        #region Modulo
        public static int GetModulo(int tipodoc)
        {
            Tipodoc td = new Tipodoc();
            td.tpd_codigo_key = tipodoc;
            td = TipodocBLL.GetByPK(td);
            return td.tpd_modulo ?? 1;
        }
        #endregion

        #region Transsac
        public static int GetTransacc(int tipodoc)
        {
            Tipodoc td = new Tipodoc();
            td.tpd_codigo_key = tipodoc;
            td = TipodocBLL.GetByPK(td);

            if (td.tpd_modulo == Constantes.cCuentasxCobrar.mod_codigo)
            {
                if (td.tpd_codigo == Constantes.cFactura.tpd_codigo)
                {
                    return 1;
                }
                if (td.tpd_codigo == Constantes.cNotacre.tpd_codigo)
                {
                    return 6;
                }
                if (td.tpd_codigo == Constantes.cNotadeb.tpd_codigo)
                {
                    return 5;
                }
                if (td.tpd_codigo == Constantes.cRecibo.tpd_codigo)
                {
                    return 7;
                }
            }

            if (td.tpd_modulo == Constantes.cBancos.mod_codigo)
            {
                if (td.tpd_codigo == Constantes.cDeposito.tpd_codigo)
                {
                    return 4;
                }
                if (td.tpd_codigo == Constantes.cNotaCreditoBan.tpd_codigo)
                {
                    return 3;
                }
                if (td.tpd_codigo == Constantes.cNotaDebitoBan.tpd_codigo)
                {
                    return 2;
                }

            }

            if (td.tpd_modulo == Constantes.cCuentasxPagar.mod_codigo)
            {
                if (td.tpd_codigo == Constantes.cFactura.tpd_codigo)
                {
                    return 1;
                }
                if (td.tpd_codigo == Constantes.cNotacre.tpd_codigo)
                {
                    return 6;
                }
                if (td.tpd_codigo == Constantes.cNotadeb.tpd_codigo)
                {
                    return 5;
                }
                if (td.tpd_codigo == Constantes.cPago.tpd_codigo || td.tpd_codigo == Constantes.cPagoBan.tpd_codigo || td.tpd_codigo == Constantes.cRetencion.tpd_codigo)
                {
                    return 7;
                }
            }
            if (td.tpd_modulo == Constantes.cInventario.mod_codigo)
            {
                if (td.tpd_codigo == Constantes.cTransferenciaDirecta.tpd_codigo)
                    return 6;
            }


            return 1;
        }
        #endregion


        #region impresiones 


        public static List<vHojadeRuta> getHojaRutaDataTable(long codigo)
        {
            Comprobante fac = new Comprobante();
            fac.com_empresa_key = 1;
            fac.com_codigo_key = codigo;
            fac.com_empresa = 1;
            fac.com_codigo = codigo;
            fac = ComprobanteBLL.GetByPK(fac);
            fac.total = new Total();

            fac.total.tot_comprobante = fac.com_codigo;
            fac.total.tot_empresa = fac.com_empresa;
            fac.total.tot_comprobante_key = fac.com_codigo;
            fac.total.tot_empresa_key = fac.com_empresa;
            fac.total = TotalBLL.GetByPK(fac.total);

            fac.rutafactura = RutaxfacturaBLL.GetAll(new WhereParams("rfac_comprobanteruta = {0} and rfac_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            List<vHojadeRuta> planillas = vHojadeRutaBLL.GetAll(new WhereParams(" cabecera.com_codigo={0}", fac.com_codigo), "");

            return planillas;
        }
        public static List<vDnotacre> getNotaCreditoDataTable(long codigo)
        {
            Comprobante fac = new Comprobante();
            fac.com_empresa_key = 1;
            fac.com_codigo_key = codigo;
            fac.com_empresa = 1;
            fac.com_codigo = codigo;
            fac = ComprobanteBLL.GetByPK(fac);
            fac.total = new Total();

            fac.total.tot_comprobante = fac.com_codigo;
            fac.total.tot_empresa = fac.com_empresa;
            fac.total.tot_comprobante_key = fac.com_codigo;
            fac.total.tot_empresa_key = fac.com_empresa;
            fac.total = TotalBLL.GetByPK(fac.total);

            fac.notascre = DnotacreBLL.GetAll(new WhereParams("dnc_comprobante = {0} and dnc_empresa = {1}", fac.com_codigo, fac.com_empresa), "");

            List<vDnotacre> planillas = vDnotacreBLL.GetAll(new WhereParams(" comprobante.com_codigo={0}", fac.com_codigo), "");

            return planillas;
        }

        #endregion


        public static List<Persona> getPersonas(string type)
        {
            if (type == "RUC")
            {
                List<Persona> lst = new List<Persona>();
                lst = PersonaBLL.GetAll("per_tipoid='RUC'", "");
                return lst;
            }
            else if (type == "Cédula")
            {
                List<Persona> lst = new List<Persona>();
                lst = PersonaBLL.GetAll("per_tipoid='Cédula'", "");
                return lst;
            }
            else
            {
                List<Persona> lst = new List<Persona>();
                lst = PersonaBLL.GetAll("", "");
                return lst;
            }

        }


        public static List<vHojadeRuta> getCobrosSocio(DateTime desde, DateTime hasta, int? codigosocio, int? empresa)
        {
            hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));


            WhereParams parametros = new WhereParams();

            if (codigosocio.HasValue)
                parametros = new WhereParams(" detalle.com_fecha BETWEEN {0} and {1} and ccomdoc.cdoc_politica = {2} and detalle.com_almacen = {3}  and socio.per_codigo= {4} and detalle.com_empresa={5}", desde, hasta, 4, 10, codigosocio, empresa);
            else
                parametros = new WhereParams(" detalle.com_fecha BETWEEN {0} and {1} and ccomdoc.cdoc_politica = {2} and detalle.com_almacen = {3} and detalle.com_empresa = {4} ", desde, hasta, 4, 10, empresa);

            List<vHojadeRuta> planillas = vHojadeRutaBLL.GetAll(parametros, "");
            return planillas;
        }

        public static List<vVenta> getVentasRet(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa, string politicas)
        {
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

            parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
            valores.Add(hasta);
            contador++;


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_almacen = {" + contador + "} ";
                    valores.Add(almacen);
                    contador++;
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_pventa = {" + contador + "} ";
                    valores.Add(pventa);
                    contador++;
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_empresa = {" + contador + "} ";
                    valores.Add(empresa);
                    contador++;
                }
            }
            if (!string.IsNullOrEmpty(politicas))
            {
                if (politicas.StartsWith(","))
                    politicas = politicas.Substring(1, politicas.Length - 1);
                parametros.where += " and cdoc_politica in (" + politicas + ")";
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_tipodoc = {" + contador + "} ";
            valores.Add(Constantes.cFactura.tpd_codigo);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_estado = {" + contador + "} ";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;

            parametros.valores = valores.ToArray();
            List<vVenta> lista = vVentaBLL.GetAll1(parametros, "f.com_numero");

            List<vRetencionVenta> retencionesventas = vRetencionVentaBLL.GetAllCom(new WhereParams("com_estado={0} and com_fecha between {1} and  {2}", Constantes.cEstadoMayorizado, desde, hasta), "");

            foreach (vRetencionVenta item in retencionesventas)
            {
                //vVenta venta = lista.Find(delegate (vVenta v) { return v.ruc == item.per_ciruc; });
                vVenta venta = lista.Find(delegate (vVenta v) { return v.codigo == item.refcomprobante; });
                if (venta == null)
                {
                    venta = lista.Find(delegate (vVenta v) { return v.ruc == "9999999999999"; });

                }

                if (venta != null)
                {
                    venta.retfue += item.ret;
                    venta.retiva += item.iva;
                }
            }



            return lista;

        }





        public static List<vComprobante> getVentas(DateTime desde, DateTime hasta, int? almacen, int? pventa, bool detallado, int? empresa, string politicas)
        {
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
            valores.Add(hasta);
            contador++;


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(almacen);
                    contador++;
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(pventa);
                    contador++;
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                    valores.Add(empresa);
                    contador++;
                }
            }
            if (!string.IsNullOrEmpty(politicas))
            {
                if (politicas.StartsWith(","))
                    politicas = politicas.Substring(1, politicas.Length - 1);
                parametros.where += " and cdoc_politica in (" + politicas + ")";
            }

            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
            valores.Add(Constantes.cFactura.tpd_codigo);
            contador++;
            if (!detallado)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
                valores.Add(Constantes.cEstadoMayorizado);
                contador++;
            }
            parametros.valores = valores.ToArray();
            List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
            return lista;

        }
    
       public static List<vComprobante> getVentasSocio(DateTime desde, DateTime hasta, int? almacen, int? pventa,int? empresa)
       {
           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;
           parametros.valores = valores.ToArray();
           List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
           return lista;

       }


        public static List<vComprobante> getComprobantesTransporte(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa, int? ruta, string estado)
        {

            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            hasta = hasta.AddDays(1).AddSeconds(-1);

            parametros.where = " c.com_tipodoc IN (4,13) "; //4:Factura 13:GUIA
            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + valores.Count() + "} ";
            valores.Add(desde);
            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + valores.Count() + "} ";
            valores.Add(hasta);


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + valores.Count() + "} ";
                    valores.Add(almacen);
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + valores.Count() + "} ";
                    valores.Add(pventa);
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + valores.Count() + "} ";
                    valores.Add(empresa);
                }
            }

            if (ruta.HasValue)
            {
                if (ruta.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_ruta = {" + valores.Count() + "} ";
                    valores.Add(ruta);
                }
            }

            if (estado!= "")
            {
                parametros.where += " and c.com_estado IN (" + estado + ")";

                /*string[] arrayestado = estado.Split(',');
                foreach (string item in arrayestado)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado IN ({" + valores.Count() + "},{" + (valores.Count() + 1) + "}) ";
                    valores.Add(Constantes.cEstadoMayorizado);

                }*/
            }

            

            parametros.valores = valores.ToArray();
            List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
            return lista;

        }

        public static List<vDocumentoVenta> getCobroSocio(DateTime desde, DateTime hasta, int? almacen, int? pventa)
       {
           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;
           parametros.valores = valores.ToArray();
           List<vDocumentoVenta> lista = vDocumentoVentaBLL.GetAll(parametros, "");
           return lista;

       }

        public static List<vVentasRetenciones> getVentasRetenciones(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa)
        {
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            WhereParams parcan = new WhereParams();
            List<object> valcan = new List<object>();

            hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

            parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + valores.Count() + "} ";
            valores.Add(desde);

            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + valores.Count() + "} ";
            valores.Add(hasta);

            parcan.where += ((parcan.where != "") ? " and " : "") + " c.com_fecha between {" + valcan.Count() + "} ";
            valcan.Add(desde);

            parcan.where += ((parcan.where != "") ? " and " : "") + "   {" + valcan.Count() + "} ";
            valcan.Add(hasta);

            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + valores.Count() + "} ";
                    valores.Add(almacen);
                    parcan.where += ((parcan.where != "") ? " and " : "") + " c.com_almacen = {" + valcan.Count() + "} ";
                    valcan.Add(almacen);
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_pventa = {" + valores.Count() + "} ";
                    valores.Add(pventa);
                    parcan.where += ((parcan.where != "") ? " and " : "") + " c.com_pventa = {" + valcan.Count() + "} ";
                    valcan.Add(pventa);
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_empresa = {" + valores.Count() + "} ";
                    valores.Add(empresa);
                    parcan.where += ((parcan.where != "") ? " and " : "") + " c.com_empresa = {" + valcan.Count() + "} ";
                    valcan.Add(empresa);
                }
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " (tpa_iva=1 or tpa_ret = 1)  ";
            // valores.Add(Constantes.cFactura.tpd_codigo);

            parametros.where += ((parametros.where != "") ? " and " : "") + " com_estado =  " + Constantes.cEstadoMayorizado;
            parcan.where += ((parcan.where != "") ? " and " : "") + " c.com_estado =  " + Constantes.cEstadoMayorizado;
            //valores.Add(Constantes.cEstadoMayorizado);           

            parametros.valores = valores.ToArray();
            
            List<vVentasRetenciones> lista = vVentasRetencionesBLL.GetAll(parametros, "com_doctran");
            string wherein = "";
            foreach (vVentasRetenciones item in lista)
            {
                wherein += (wherein != "" ? "," : "") + item.com_codigo;
            }

            parcan.where += " and c.com_codigo in (" + wherein + ")";
            parcan.valores = valcan.ToArray();
            List<vCancelacion> lstcan = vCancelacionBLL.GetAll(parcan, "");

            foreach (vVentasRetenciones item in lista)
            {
                List<vCancelacion> lst = lstcan.FindAll(delegate (vCancelacion v) { return v.dca_comprobante_can == item.com_codigo; });
                string strfac = "";
                foreach (vCancelacion c in lst)
                {
                    if (strfac.IndexOf(c.ddo_doctran)<0)
                        strfac += (strfac != "" ? "," : "") + c.ddo_doctran;
                }

                item.factura = strfac;
            }

            return lista;

        }


        public static List<vDetalle> getDetalles(DateTime desde, DateTime hasta, int? cliente, int? ruta, int? politica, int? empresa)
        {
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            //DateTime desde = fecha.Date;
            //DateTime hasta = desde.AddDays(1).AddSeconds(-1);
            hasta = hasta.AddDays(1).AddSeconds(-1);

            parametros.where += ((parametros.where != "") ? " and " : "") + " comprobante.com_empresa=  {" + contador + "} ";
            valores.Add(empresa);
            contador++;

            if (cliente.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " comprobante.com_codclipro=  {" + contador + "} ";
                valores.Add(cliente);
                contador++;
            }

            parametros.where += ((parametros.where != "") ? " and " : "") + " comprobante.com_fecha BETWEEN  {" + contador + "} and {" + (contador+1) + "} ";
            valores.Add(desde);
            contador++;
            valores.Add(hasta);
            contador++;
            if (ruta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ccomenv.cenv_ruta =  {" + contador + "} ";
                valores.Add(ruta);
                contador++;
            }
            if (politica.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ccomdoc.cdoc_politica =  {" + contador + "} ";
                valores.Add(politica);
                contador++;
            }

            parametros.where += ((parametros.where != "") ? " and " : "") + " comprobante.com_tipodoc = {" + contador + "} ";
            valores.Add(Constantes.cGuia.tpd_codigo);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + " comprobante.com_estado = {" + contador + "} ";
            valores.Add(Constantes.cEstadoGrabado);
            contador++;
            parametros.valores = valores.ToArray();
            List<vDetalle> lista = vDetalleBLL.GetAll(parametros, "");
            List<vDetalle> listadet = vDetalleBLL.GetAllDet(parametros, "");

            foreach (vDetalle item in lista)
            {
                List<vDetalle> det = listadet.FindAll(delegate (vDetalle d) { return d.codigo == item.codigo; });
                foreach (vDetalle d in det)
                {
                    if (!item.bultosdetalle.HasValue)
                        item.bultosdetalle = 0;
                    item.bultosdetalle += d.bultosdetalle;
                    item.itemsdetalle += (!string.IsNullOrEmpty(item.itemsdetalle) ? "," : "") + d.itemsdetalle;
                }
            }

            return lista;
        }

        public static List<vPlanillaCliente> getPlanillaClienteDet(long codigo, int empresa, ref string subtotal, ref string iva, ref string total)
        {
            Comprobante planilla = new Comprobante();
            //codigo = "349";
            planilla.com_empresa_key = empresa;
            planilla.com_codigo_key = codigo;
            planilla.com_empresa = empresa;
            planilla.com_codigo = codigo;
            planilla = ComprobanteBLL.GetByPK(planilla);

            planilla.total = new Total();
            planilla.total.tot_empresa = planilla.com_empresa;
            planilla.total.tot_empresa_key = planilla.com_empresa;
            planilla.total.tot_comprobante = planilla.com_codigo;
            planilla.total.tot_comprobante_key = planilla.com_codigo;
            planilla.total = TotalBLL.GetByPK(planilla.total);

            decimal st = planilla.total.tot_subtot_0 + planilla.total.tot_transporte + planilla.total.tot_subtotal + planilla.total.tot_tseguro.Value;

            subtotal = st.ToString("0.00");
            iva = planilla.total.tot_timpuesto.ToString("0.00");
            total = planilla.total.tot_total.ToString("0.00");

            List<vPlanillaCliente> planillas = vPlanillaClienteBLL.GetAll(new WhereParams("cabecera.com_codigo={0}", planilla.com_codigo), "detalle.com_numero");
            List<vPlanillaCliente> planillasdet = vPlanillaClienteBLL.GetAllDet(new WhereParams("cabecera.com_codigo={0}", planilla.com_codigo), "detalle.com_numero");

            foreach (vPlanillaCliente item in planillas)
            {
                List<vPlanillaCliente> det = planillasdet.FindAll(delegate (vPlanillaCliente d) { return d.detalle_codigo == item.detalle_codigo; });
                foreach (vPlanillaCliente d in det)
                {
                    if (!item.detalle_bultos.HasValue)
                        item.detalle_bultos = 0;
                    item.detalle_bultos += d.detalle_bultos;
                    item.detalle_items += (!string.IsNullOrEmpty(item.detalle_items) ? "," : "") + d.detalle_items;
                }
            }
            return planillas;
        }

        public static List<vPlanillaCliente> getPlanillaCliente(long codigo, int empresa, ref string subtotal, ref string iva, ref string total)
        {
            Comprobante planilla = new Comprobante();
            //codigo = "349";
            planilla.com_empresa_key = empresa;
            planilla.com_codigo_key = codigo;
            planilla.com_empresa = empresa;
            planilla.com_codigo = codigo;
            planilla = ComprobanteBLL.GetByPK(planilla);

            planilla.total = new Total();
            planilla.total.tot_empresa = planilla.com_empresa;
            planilla.total.tot_empresa_key = planilla.com_empresa;
            planilla.total.tot_comprobante = planilla.com_codigo;
            planilla.total.tot_comprobante_key = planilla.com_codigo;
            planilla.total = TotalBLL.GetByPK(planilla.total);

            decimal st = planilla.total.tot_subtot_0 + planilla.total.tot_transporte + planilla.total.tot_subtotal + planilla.total.tot_tseguro.Value;

            subtotal = st.ToString("0.00");
            iva = planilla.total.tot_timpuesto.ToString("0.00");
            total = planilla.total.tot_total.ToString("0.00");
            

            List<vPlanillaCliente> planillas = vPlanillaClienteBLL.GetAll(new WhereParams("cabecera.com_codigo={0}", planilla.com_codigo), "detalle.com_numero");
            return planillas;
        }


       


        public static List<vHojadeRuta> getHojasRutaSocio(DateTime desde, DateTime hasta,int empresa, int? almacen, int? pventa, int? socio )
        {
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            hasta = hasta.AddDays(1).AddSeconds(-1);
            

            parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_empresa=  {" + contador + "} ";
            valores.Add(empresa);
            contador++;

            parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_fecha BETWEEN  {" + contador + "} and {" + (contador + 1) + "} ";
            valores.Add(desde);
            contador++;
            valores.Add(hasta);
            contador++;

            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_almacen=  {" + contador + "} ";
                valores.Add(almacen);
                contador++;
            }
            if (pventa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_pventa=  {" + contador + "} ";
                valores.Add(pventa);
                contador++;
            }
            if (socio.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ccomenv.cenv_socio =  {" + contador + "} ";
                valores.Add(socio);
                contador++;
            }
            parametros.valores = valores.ToArray();
            List<vHojadeRuta> lst = vHojadeRutaBLL.GetAll(parametros, "");
            return lst;
        }

        #region Reportes SRI

        public static List<vComprobante> getVentasSri(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa)
       {
            hasta = hasta.AddDays(1).AddSeconds(-1);

            int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;

            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;

            parametros.valores = valores.ToArray();
           List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
           return lista;

       }





       public static List<vComprobante> getVentasBienes(DateTime desde, DateTime hasta, int? almacen, int? pventa,int? empresa)
       {
           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = {" + contador + "} or c.com_tipodoc = {" + (contador+1) + "}) ";
           valores.Add(Constantes.cObligacion.tpd_codigo);
           contador++;
           valores.Add(Constantes.cLiquidacionCompra.tpd_codigo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;
           parametros.valores = valores.ToArray();
           List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
           return lista;

       }






       public static List<vRetencion> getReporteRetenciones(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? impuesto,int? empresa)
       {
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));


           WhereParams parametros = new WhereParams();
            //parametros = new WhereParams(" f.com_fecha BETWEEN {0} and {1} and r.com_tipodoc = {2} and r.com_estado = {3} and f.com_empresa={4} ", desde, hasta, Constantes.cRetencion.tpd_codigo, Constantes.cEstadoMayorizado,empresa);
            parametros = new WhereParams(" r.com_fecha BETWEEN {0} and {1} and r.com_tipodoc = {2} and r.com_estado = {3} and r.com_empresa={4}  ", desde, hasta, Constantes.cRetencion.tpd_codigo, Constantes.cEstadoMayorizado, empresa);
            List<vRetencion> lst = vRetencionBLL.GetAll(parametros, "r.com_doctran");
           return lst;
           //if (impuesto.HasValue)
           //    parametros = new WhereParams(" co.com_fecha BETWEEN {0} and {1} and imp.imp_codigo = {2} and co.com_tipodoc = {3}  ", desde, hasta, impuesto, 16);
           //else
           //    parametros = new WhereParams(" co.com_fecha BETWEEN {0} and {1} and imp.imp_codigo = {2} and co.com_tipodoc = {3} ", desde, hasta, impuesto, 16);

           //List<vRetenciones> planillas = vRetencionesBLL.GetAll(parametros, "");
           //return planillas;

       }




       public static List<vComprobante> getNotasCredito(DateTime desde, DateTime hasta, int? almacen, int? pventa,int? empresa)
       {
           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();

           hasta = hasta.AddDays(1).AddSeconds(-1);

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cNotacre.tpd_codigo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;
           parametros.valores = valores.ToArray();
           List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
           return lista;

       }





       public static List<vComprobante> getAnuladas(DateTime desde, DateTime hasta, int? almacen, int? pventa,int? empresa)
       {
           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).AddSeconds(-1);
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoEliminado);
           contador++;
           parametros.valores = valores.ToArray();
           List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
           return lista;

       }


       public static List<vCancelacionDetalle> getVentasSocios(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? politica, string usuario,int? empresa)
       {
           

           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }

          
               if (usuario !="" )
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.crea_usr = {" + contador + "} ";
                   valores.Add(usuario);
                   contador++;
               }
           
         
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }

           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           if (politica.HasValue)
           {
               if (politica.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " cdoc_politica = {" + contador + "} ";
                   valores.Add(politica);
                   contador++;
               }
           }
           //parametros.where += ((parametros.where != "") ? " and " : "") + " (f.com_tipodoc = {" + contador + "} or c.com_tipodoc = {" + (contador + 1) + "}) ";
           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;
          

           parametros.valores = valores.ToArray();
           List<vCancelacionDetalle> lista = vCancelacionDetalleBLL.GetAll(parametros, "");


            int?[] agentes = lista.Where(w => (w.agente ?? 0) > 0).Select(s => s.agente).ToArray();
            if (agentes.Length > 0)
            {
                List<Persona> lstage = PersonaBLL.GetAll("per_empresa=" + empresa + " and per_codigo in (" + string.Join(",", agentes) + ")", "");
                foreach (vCancelacionDetalle item in lista)
                {
                    if ((item.agente??0)>0)
                    {
                        Persona age = lstage.Find(delegate (Persona p) { return p.per_codigo == item.agente.Value; });
                        item.razonagente = age.per_razon;
                    }
                }
            }



           return lista;


       }


       public static List<vCancelacionDetalle> getVentasTiposPagos(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? tpago,int? empresa)
       {
           //hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));


           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }

           if (tpago.HasValue)
           {
               if (tpago.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " dfp_tipopago = {" + contador + "} ";
                   valores.Add(tpago);
                   contador++;
               }
           }

           //parametros.where += ((parametros.where != "") ? " and " : "") + " (f.com_tipodoc = {" + contador + "} or c.com_tipodoc = {" + (contador + 1) + "}) ";
           parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = {" + contador + "} ) ";
           valores.Add(Constantes.cRecibo.tpd_codigo);
           contador++;

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;

           parametros.valores = valores.ToArray();
           List<vCancelacionDetalle> lista = vCancelacionDetalleBLL.GetAll(parametros, "");
           return lista;


       }

       public static List<vCompras> getComprasRetencioines(DateTime desde, DateTime hasta, int? almacen, int? pventa, bool ret,int? empresa)
       {
           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " o.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " o.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " o.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " o.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }


           parametros.where += ((parametros.where != "") ? " and " : "") + " (o.com_tipodoc = " + Constantes.cObligacion.tpd_codigo + " or o.com_tipodoc = " + Constantes.cLiquidacionCompra.tpd_codigo + ") ";
           //   valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;

           parametros.where += ((parametros.where != "") ? " and " : "") + " o.com_estado = " + Constantes.cEstadoMayorizado + "";
           //  valores.Add(Constantes.cEstadoMayorizado);
           contador++;

           if (ret)
           {
               //parametros.where += ((parametros.where != "") ? " and " : "") + " r.com_estado = " + Constantes.cEstadoMayorizado + "";
               parametros.where += ((parametros.where != "") ? " and " : "") + " drt_comprobante  is not null ";
               contador++;
           }
           else
           {
               
               parametros.where += ((parametros.where != "") ? " and " : "") + " drt_comprobante  is null ";
               contador++;
           }
           //parametros.where += ((parametros.where != "") ? " and " : "") + " drt_comprobante  is not null ";
           //valores.Add(i);
           //contador++;

           parametros.valores = valores.ToArray();
           List<vCompras> lista = vComprasBLL.GetAll(parametros, "");
           return lista;
       }

       public static List<vEgresoBanco> getEgresoBanco(DateTime desde, DateTime hasta, int? almacen, int? pventa,int? empresa)
       {
           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = " + Constantes.cPago.tpd_codigo + " or c.com_tipodoc = " + Constantes.cPagoBan.tpd_codigo + ") ";
           //   valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;
           parametros.valores = valores.ToArray();
           List<vEgresoBanco> lista = vEgresoBancoBLL.GetAll(parametros, "com_numero");
           return lista;

       }





       public static List<vCobroSocio> ReporteVentasSocios(int? periodo, int? mes, int? codigosocio, int? almacen, int? pventa,int? empresa)
       {


           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();


           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_periodo = {" + contador + "} ";
           valores.Add(periodo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "  f.com_mes = {" + contador + "} ";
           valores.Add(mes);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }

           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           if (codigosocio.HasValue)
           {
               if (codigosocio.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " (cf.cenv_socio = " + codigosocio + " or cg.cenv_socio = " + codigosocio + ") ";
                   valores.Add(codigosocio);
                   contador++;
               }
           }

           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;
           
           //   valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_tipodoc = 4 ";

           //FILTRADO DE PLETES PAGADOS
           //contador++;
           //parametros.where += ((parametros.where != "") ? " and " : "") + " cd.cdoc_politica = 4 ";
           contador++;
           parametros.valores = valores.ToArray();
           List<vCobroSocio> lista = vCobroSocioBLL.GetAll(parametros, "");
           return lista;
       /*    WhereParams parametros = new WhereParams();

           if (codigosocio.HasValue)
               parametros = new WhereParams(" f.com_tipodoc = {0}  and f.com_estado = {1} and f.com_periodo = {2}  and f.com_mes= {3} and (cf.cenv_socio = {4} or cg.cenv_socio= {5})", 4, 2, periodo, mes, codigosocio, codigosocio);
          

           List<vCobroSocio> planillas = vCobroSocioBLL.GetAll(parametros, "");
           return planillas;*/
       }




       public static List<vComprobanteDescuadrado> ReporteComprobantesDescuadrados(int? periodo, int? mes, int? tipodoc, int? almacen, int? pventa,int? empresa)
       {

           tipodoc = 4;

           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();


           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_periodo = {" + contador + "} ";
           valores.Add(periodo);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "  c.com_mes = {" + contador + "} ";
           valores.Add(mes);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           if (tipodoc.HasValue)
           {
               if (tipodoc.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
                   valores.Add(tipodoc);
                   contador++;
               }
           }

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;


           parametros.where += ((parametros.where != "") ? " and " : "") + "(tot_total- (tot_subtot_0+ tot_transporte+ tot_subtotal+ tot_tseguro+ tot_timpuesto))<>0";
           contador++;

           //   valores.Add(Constantes.cFactura.tpd_codigo);
         

        
           contador++;
           parametros.valores = valores.ToArray();
           List<vComprobanteDescuadrado> lista = vComprobanteDescuadradoBLL.GetAll1(parametros, "");
           return lista;
           /*    WhereParams parametros = new WhereParams();

               if (codigosocio.HasValue)
                   parametros = new WhereParams(" f.com_tipodoc = {0}  and f.com_estado = {1} and f.com_periodo = {2}  and f.com_mes= {3} and (cf.cenv_socio = {4} or cg.cenv_socio= {5})", 4, 2, periodo, mes, codigosocio, codigosocio);
          

               List<vCobroSocio> planillas = vCobroSocioBLL.GetAll(parametros, "");
               return planillas;*/
       }

       public static List<vComprobanteDescuadrado> ReporteComprobantesDescuadradosC(int? periodo, int? mes, int? tipodoc, int? almacen, int? pventa,int? empresa)
       {

        

           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();

            if (periodo.HasValue)
            {
                if (periodo.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_periodo = {" + contador + "} ";
                    valores.Add(periodo);
                    contador++;
                }
            }
            if (mes.HasValue)
            {
                if (mes.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + "  c.com_mes = {" + contador + "} ";
                    valores.Add(mes);
                    contador++;
                }
            }


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }
           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }
           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }
           if (tipodoc.HasValue)
           {
               if (tipodoc.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
                   valores.Add(tipodoc);
                   contador++;
               }
           }

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;

           parametros.where += ((parametros.where != "") ? " and " : "") + "(sum(CASE WHEN dco_debcre =1  THEN dco_valor_nac ELSE 0 END) - sum(CASE WHEN dco_debcre =2  THEN dco_valor_nac ELSE 0 END)) <> 0 ";
           contador++;
           //   valores.Add(Constantes.cFactura.tpd_codigo);



           contador++;
           parametros.valores = valores.ToArray();
           List<vComprobanteDescuadrado> lista = vComprobanteDescuadradoBLL.GetAll(parametros, "");
           return lista;
           /*    WhereParams parametros = new WhereParams();

               if (codigosocio.HasValue)
                   parametros = new WhereParams(" f.com_tipodoc = {0}  and f.com_estado = {1} and f.com_periodo = {2}  and f.com_mes= {3} and (cf.cenv_socio = {4} or cg.cenv_socio= {5})", 4, 2, periodo, mes, codigosocio, codigosocio);
          

               List<vCobroSocio> planillas = vCobroSocioBLL.GetAll(parametros, "");
               return planillas;*/
       }



       public static List<vCancelacionDetalle> getVentasSociosCaja(DateTime desde, DateTime hasta, int? almacen, int? pventa, string usuario)
       {


           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }


           if (usuario != "")
           {
               parametros.where += ((parametros.where != "") ? " and " : "") + " c.crea_usr = {" + contador + "} ";
               valores.Add(usuario);
               contador++;
           }


           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }


           //parametros.where += ((parametros.where != "") ? " and " : "") + " (f.com_tipodoc = {" + contador + "} or c.com_tipodoc = {" + (contador + 1) + "}) ";
           parametros.where += ((parametros.where != "") ? " and " : "") + " f.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cFactura.tpd_codigo);
           contador++;

           parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           valores.Add(Constantes.cEstadoMayorizado);
           contador++;

           parametros.where += ((parametros.where != "") ? " and " : "") + " cdoc_politica <>  4 ";
           contador++;

           parametros.valores = valores.ToArray();
           List<vCancelacionDetalle> lista = vCancelacionDetalleBLL.GetAll(parametros, "");
           return lista;


       }

       

       public static List<vHojaRutaReporte> getVentasSociosHojas(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? codigosocio,int? empresa, int? ruta)
       {


           int contador = 0;
           WhereParams parametros = new WhereParams();
           List<object> valores = new List<object>();
           hasta = hasta.AddDays(1).Subtract(new TimeSpan(0, 0, 1));

           parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_fecha between {" + contador + "} ";
           valores.Add(desde);
           contador++;
           parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
           valores.Add(hasta);
           contador++;


           if (almacen.HasValue)
           {
               if (almacen.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_almacen = {" + contador + "} ";
                   valores.Add(almacen);
                   contador++;
               }
           }


           if (empresa.HasValue)
           {
               if (empresa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_empresa = {" + contador + "} ";
                   valores.Add(empresa);
                   contador++;
               }
           }


           if (pventa.HasValue)
           {
               if (pventa.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_pventa = {" + contador + "} ";
                   valores.Add(pventa);
                   contador++;
               }
           }

           if (codigosocio.HasValue)
           {
               if (codigosocio.Value > 0)
               {
                   parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_codclipro = {" + contador + "} ";
                   valores.Add(codigosocio);
                   contador++;
               }

            }

            if (ruta.HasValue)
            {
                if (ruta.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_ruta= {" + contador + "} ";
                    valores.Add(ruta);
                    contador++;
                }
            }


            //parametros.where += ((parametros.where != "") ? " and " : "") + " (f.com_tipodoc = {" + contador + "} or c.com_tipodoc = {" + (contador + 1) + "}) ";
            parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_tipodoc = {" + contador + "} ";
           valores.Add(Constantes.cHojaRuta.tpd_codigo);
           contador++;

           // parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado = {" + contador + "} ";
           //valores.Add(Constantes.cEstadoMayorizado);
           //contador++;

           //   parametros.where += ((parametros.where != "") ? " and " : "") + " cdoc_politica =  12 ";
           //  contador++;

           parametros.valores = valores.ToArray();
           List<vHojaRutaReporte> lista = vHojaRutaReporteBLL.GetAll(parametros, "");
           return lista;


       }




        public static List<vComprobante> getValoresSeguros(DateTime desde, DateTime hasta, int? empresa, int? almacen, int? pventa, int? remitente, string destinatario)
        {
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
            valores.Add(hasta);
            contador++;


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(almacen);
                    contador++;
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(pventa);
                    contador++;
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                    valores.Add(empresa);
                    contador++;
                }
            }
            if (remitente.HasValue)
            {
                if (remitente.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_remitente = {" + contador + "} ";
                    valores.Add(remitente);
                    contador++;
                }
            }
            if (!string.IsNullOrEmpty(destinatario))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (e.cenv_nombres_des ILIKE {"+contador+"} or e.cenv_apellidos_des ILIKE {" + contador + "}) ";
                valores.Add("%" + destinatario + "%");
                contador++;
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_estado = {" + contador + "} or c.com_estado= {" + (contador + 1) + "})";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;
            valores.Add(Constantes.cEstadoGrabado);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = {" + contador + "} or c.com_tipodoc = {" + (contador + 1) + "} )";
            valores.Add(Constantes.cFactura.tpd_codigo);
            contador++;
            valores.Add(Constantes.cGuia.tpd_codigo);
            contador++;

            parametros.where += ((parametros.where != "") ? " and " : "") + " tot_vseguro >0 ";


            parametros.valores = valores.ToArray();
            List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
            return lista;

        }

        public static List<vComprobante> getValoresDomicilios(DateTime desde, DateTime hasta, int? empresa, int? almacen, int? pventa, int?tipodoc, int? ruta)
        {
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
            valores.Add(hasta);
            contador++;


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(almacen);
                    contador++;
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(pventa);
                    contador++;
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                    valores.Add(empresa);
                    contador++;
                }
            }
            if (ruta.HasValue)
            {
                if (ruta.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " e.cenv_ruta = {" + contador + "} ";
                    valores.Add(ruta);
                    contador++;
                }
            }
            if (tipodoc.HasValue)
            {
                if (tipodoc.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
                    valores.Add(tipodoc);
                    contador++;
                }
            }
            else
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_tipodoc = {" + contador + "} or c.com_tipodoc = {" + (contador + 1) + "} )";
                valores.Add(Constantes.cFactura.tpd_codigo);
                contador++;
                valores.Add(Constantes.cGuia.tpd_codigo);
                contador++;
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " (c.com_estado = {" + contador + "} or c.com_estado= {" + (contador + 1) + "})";
            valores.Add(Constantes.cEstadoMayorizado);
            contador++;
            valores.Add(Constantes.cEstadoGrabado);
            contador++;
            

            parametros.where += ((parametros.where != "") ? " and " : "") + " tot_transporte >0 ";


            parametros.valores = valores.ToArray();
            List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
            return lista;

        }


        #endregion


        public static List<vDEstadoCuenta> GetEstadoCuentaDet(DateTime? fecha,int empresa,  int codclipro, int debcre, int codalmacen)
        {            
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (codclipro > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_codclipro = {" + valores.Count + "} ";
                valores.Add(codclipro);
            }
            if (codalmacen > 0)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + valores.Count + "} ";
                valores.Add(codalmacen);
            }
            if (fecha.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " ddo_fecha_emi <= {" + valores.Count + "} ";
                valores.Add(fecha);
            }
            if (debcre == Constantes.cCredito)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + "  com_tipodoc <> 4 and com_tipodoc <>6 and com_tipodoc <> 13 and com_tipodoc<> 17 and com_tipodoc <> 18 ";

            }

            if (debcre == Constantes.cDebito)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_tclipro = {" + valores.Count + "} ";
                valores.Add(4);                
            }
            
            parametros.valores = valores.ToArray();
            string OrderByClause = "ddo_doctran,ddo_pago,ddo_fecha_ven,com_fecha";

            List<vDEstadoCuenta> planillas = vDEstadoCuentaBLL.GetAll(parametros, OrderByClause);
            return planillas;

        }

        public static List<vCuentasPor> GetCuentasPor(DateTime? desde,DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, string tipo)
        {
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            WhereParams parrango= new WhereParams();
            List<object> valrango= new List<object>();

            parametros.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;

            
          
            if (desde.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha >= {" + valores.Count + "} ";
                valores.Add(desde.Value);
                parrango.where += ((parrango.where != "") ? " and " : "") + " can.com_fecha >= {" + valrango.Count + "} ";
                valrango.Add(desde.Value);
            }
            if (hasta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha <= {" + valores.Count + "} ";
                valores.Add(hasta.Value.AddDays(1).AddSeconds(-1));
                parrango.where += ((parrango.where != "") ? " and " : "") + " can.com_fecha <= {" + valrango.Count + "} ";
                valrango.Add(hasta.Value.AddDays(1).AddSeconds(-1));
            }

            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + valores.Count + "} ";
                valores.Add(almacen.Value);
            }
            if (pventa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_pventa = {" + valores.Count + "} ";
                valores.Add(pventa.Value);
            }

            if (tipo=="CLI")
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + "  com_tipodoc IN (4) ";
            }
            else
                parametros.where += ((parametros.where != "") ? " and " : "") + "  com_tipodoc IN (14,26)";

            if (!string.IsNullOrEmpty(persona))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " per_razon ILIKE {" + valores.Count + "} ";
                valores.Add("%" + persona + "%");
            }

            parametros.valores = valores.ToArray();
            parrango.valores = valrango.ToArray();


            string OrderByClause = "per_razon, com_fecha";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, parrango, OrderByClause);
            return lst;

        }


        public static List<vCuentasPor> GetCuentasPorNew(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, string tipo, int[] cuentas, string tcxc)
        {
            WhereParams pardoc= new WhereParams();
            List<object> valdoc= new List<object>();
            WhereParams parcan= new WhereParams();
            List<object> valcan = new List<object>();

            pardoc.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            parcan.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;



            if (desde.HasValue)
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_fecha >= {" + valdoc.Count + "} ";
                valdoc.Add(desde.Value);
                parcan.where += ((parcan.where != "") ? " and " : "") + " com_fecha >= {" + valcan.Count + "} ";
                valcan.Add(desde.Value);
            }
            if (hasta.HasValue)
            {
                hasta = hasta.Value.AddDays(1).AddSeconds(-1);
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_fecha <= {" + valdoc.Count + "} ";
                valdoc.Add(hasta.Value);
                parcan.where += ((parcan.where != "") ? " and " : "") + " com_fecha <= {" + valcan.Count + "} ";
                valcan.Add(hasta.Value);
            }

            if (almacen.HasValue)
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_almacen = {" + valdoc.Count + "} ";
                valdoc.Add(almacen.Value);
                parcan.where += " and com_almacen = {" + valcan.Count + "} ";
                valcan.Add(almacen.Value);
            }
            if (pventa.HasValue)
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_pventa = {" + valdoc.Count + "} ";
                valdoc.Add(pventa.Value);
                parcan.where +=  " and com_pventa = {" + valcan.Count + "} ";
                valcan.Add(pventa.Value);
            }

            if (!string.IsNullOrEmpty(tcxc))
            {
                if (tcxc=="1")//SOLO PLANILLAS
                {
                    pardoc.where += " and ddo_comprobante_guia is not null ";
                }
                if (tcxc == "2")//SIN PLANILLAS
                {
                    pardoc.where += " and ddo_comprobante_guia is null ";
                }
            }


            pardoc.where += " and ddo_cuenta IN (" + string.Join(",", cuentas) + ") ";
            parcan.where += " and ddo_cuenta IN (" + string.Join(",", cuentas) + ") ";



            //if (tipo == "CLI")
            //{

            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + "  com_tipodoc IN (4) ";
            //}
            //else
            //    //pardoc.where += ((pardoc.where != "") ? " and " : "") + "  com_tipodoc IN (14,26)";
            //    //pardoc.where += ((pardoc.where != "") ? " and " : "") + "  ddo_debcre = 2 ";


            if (!string.IsNullOrEmpty(persona))
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " per_razon ILIKE {" + valdoc.Count + "} ";
                valdoc.Add("%" + persona + "%");
            }

            //pardoc.where += " and ddo_cancela=0 ";

            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();


            string OrderByClause = "per_razon, com_fecha";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<vCuentasPor> lstdoc = vCuentasPorBLL.GetAllDoc(pardoc, OrderByClause);
            List<vCuentasPor> lstcan = vCuentasPorBLL.GetAllCanDet(parcan, "");

            List<vCuentasPor> lst = new List<vCuentasPor>();
            int i = 0;
            foreach (vCuentasPor item in lstdoc)
            {
                i++;
                List<vCuentasPor> can = lstcan.FindAll(delegate (vCuentasPor v) { return v.com_codigo == item.com_codigo; });

                if (can != null)
                {
                    item.cancela = can.Sum(s => s.cancela);
                    item.saldo = item.monto - item.cancela;
                    foreach (vCuentasPor vc in can)
                    {
                        lstcan.Remove(vc);
                    }

                }
                else
                {
                    item.cancela = 0;
                    item.saldo = item.monto;
                }
                //if (item.saldo > 0)
                //    lst.Add(item);



                //vCuentasPor can = lstcan.Find(delegate (vCuentasPor v) { return v.com_codigo == item.com_codigo; });
                //if (can != null)
                //{
                //    item.cancela = can.cancela;
                //    item.saldo = item.monto - item.cancela;
                //}
                //else
                //{
                //    item.cancela = 0;
                //    item.saldo = item.monto;
                //}

                //if (item.saldo > 0)
                //    lst.Add(item);

              
            }

                lst = lstdoc.Where(w => w.saldo < 0).ToList();//AQUI SE INDICAN LOS QUE TIENEN SALDO NEGATIVO



            decimal sumadoc = lstdoc.Where(w => w.saldo > 0).Sum(s => s.monto ?? 0);
            decimal sumacan = lstdoc.Where(w => w.saldo > 0).Sum(s => s.cancela ?? 0);
            decimal saldo = sumadoc - sumacan;

            decimal sumadoc1 = lstdoc.Sum(s => s.monto ?? 0);
            decimal sumacan1 = lstdoc.Sum(s => s.cancela ?? 0);

            decimal sumcan2 = lstcan.Sum(s => s.cancela?? 0);


            decimal saldo1= sumadoc1 - sumacan1;

            decimal saldo2 = sumadoc1 - sumcan2;


            return lstdoc.Where(w => w.saldo != 0).ToList();
            //return lstdoc;

        }


        public static List<vCuentasPor> GetCuentasPorFull(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, string tipo, int[] cuentas, string tcxc, string pol)
        {
            WhereParams pardoc = new WhereParams();
            List<object> valdoc = new List<object>();

            pardoc.where = "fac.com_empresa = " + empresa + " and fac.com_estado= " + Constantes.cEstadoMayorizado;

            if (hasta.HasValue)
            {
                hasta = hasta.Value.AddDays(1).AddSeconds(-1);
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " fac.com_fecha <= {" + valdoc.Count + "} ";
                valdoc.Add(hasta.Value);
            }


            if (desde.HasValue)
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " fac.com_fecha >= {" + valdoc.Count + "} ";
                valdoc.Add(desde.Value);
            }

            if (almacen.HasValue)
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " fac.com_almacen = {" + valdoc.Count + "} ";
                valdoc.Add(almacen.Value);
            }
            if (pventa.HasValue)
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " fac.com_pventa = {" + valdoc.Count + "} ";
                valdoc.Add(pventa.Value);
            }

            if (!string.IsNullOrEmpty(tcxc))
            {
                if (tcxc == "1")//SOLO PLANILLAS
                {
                    pardoc.where += " and ddo_comprobante_guia is not null ";
                }
                if (tcxc == "2")//SIN PLANILLAS
                {
                    pardoc.where += " and ddo_comprobante_guia is null ";
                }
            }


            pardoc.where += " and ddo_cuenta IN (" + string.Join(",", cuentas) + ") ";


            if (!string.IsNullOrEmpty(persona))
            {
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " per_razon ILIKE {" + valdoc.Count + "} ";
                valdoc.Add("%" + persona + "%");
            }

            if (!string.IsNullOrEmpty(pol))
            {
                pardoc.where += " and cdoc_politica = " + pol;
            }


            //pardoc.where += " and ddo_cancela=0 ";

            pardoc.valores = valdoc.ToArray();


            string OrderByClause = "per_razon, fac.com_fecha";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<vCuentasPor> lstdoc = vCuentasPorBLL.GetFull(pardoc, OrderByClause);

            DateTime hoy = DateTime.Now;
            foreach (vCuentasPor item in lstdoc)
            {
                item.diasvence = Convert.ToInt32(hoy.Subtract(item.com_fecha.Value).TotalDays);
                if (item.diasvence > 0 && item.diasvence < 31)
                    item.monto1 = item.saldo;
                if (item.diasvence > 30 && item.diasvence < 61)
                    item.monto31 = item.saldo;
                if (item.diasvence > 60 && item.diasvence < 91)
                    item.monto61 = item.saldo;
                if (item.diasvence > 90 && item.diasvence < 121)
                    item.monto91 = item.saldo;
                if (item.diasvence > 120 )
                    item.monto121 = item.saldo;

            }                       

            ///return lstdoc.Where(w => w.saldo != 0).ToList();
            return lstdoc;

        }



        /// <summary>
        /// Proceso que ayuda encontrar descuadres entre el anexo de proveedores, clietnes, y el balance
        /// </summary>
        /// <param name="desde"></param>
        /// <param name="hasta"></param>
        /// <param name="empresa"></param>
        /// <param name="almacen"></param>
        /// <param name="pventa"></param>
        /// <param name="persona"></param>
        /// <param name="politica"></param>
        /// <returns></returns>
        public static string GetDescuadresCuentasPor(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, string tipo, int[] cuentas)
        {

            //List<vCuentasPor> lstcxcp = GetCuentasPorNew(desde, hasta, empresa, almacen, pventa, persona, tipo, cuentas);



            WhereParams parcon = new WhereParams();
            List<object> valcon = new List<object>();
            WhereParams pardoc = new WhereParams();
            List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            List<object> valcan = new List<object>();

            parcon.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            pardoc.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            parcan.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;

            

            if (desde.HasValue)
            {
                parcon.where += " and  com_fecha >= {" + valcon.Count + "} ";
                valcon.Add(desde.Value);
                pardoc.where += " and  com_fecha >= {" + valdoc.Count + "} ";
                valdoc.Add(desde.Value);
                parcan.where +=  " and com_fecha >= {" + valcan.Count + "} ";
                valcan.Add(desde.Value);
            }
            if (hasta.HasValue)
            {
                hasta = hasta.Value.AddDays(1).AddSeconds(-1);
                parcon.where += " and  com_fecha <= {" + valcon.Count + "} ";
                valcon.Add(hasta.Value);                
                pardoc.where += " and com_fecha <= {" + valdoc.Count + "} ";
                valdoc.Add(hasta.Value);
                parcan.where += " and com_fecha <= {" + valcan.Count + "} ";
                valcan.Add(hasta.Value);

            }

            if (almacen.HasValue)
            {
                parcon.where +=  " and com_almacen = {" + valcon.Count + "} ";
                valcon.Add(almacen.Value);
                pardoc.where += " and com_almacen = {" + valdoc.Count + "} ";
                valdoc.Add(almacen.Value);

            }
            if (pventa.HasValue)
            {
                parcon.where +=" and com_pventa = {" + valcon.Count + "} ";
                valcon.Add(pventa.Value);
                pardoc.where +=  " and com_pventa = {" + valdoc.Count + "} ";
                valdoc.Add(pventa.Value);
            }


            parcon.where += " and dco_cuenta IN (" + string.Join(",", cuentas) + ") ";
            pardoc.where += " and ddo_cuenta IN (" + string.Join(",", cuentas) + ") ";
            parcan.where += " and ddo_cuenta IN (" + string.Join(",", cuentas) + ") ";


            if (!string.IsNullOrEmpty(persona))
            {
                pardoc.where += " and per_razon ILIKE {" + valdoc.Count + "} ";
                valdoc.Add("%" + persona + "%");
            }

            
            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();
            parcon.valores = valcon.ToArray();

            List<Dcontable> lstcon = DcontableBLL.GetAll(parcon, "");
            List<vCuentasPor> lstdoc = vCuentasPorBLL.GetAllDoc(pardoc, "");
            List<vCuentasPor> lstcan = vCuentasPorBLL.GetAllCanDet(parcan, "");

            int debcre = Constantes.cCredito;
            int vdebcre = Constantes.cDebito;

            if (tipo.ToString() == "CLI")
            {
                debcre = Constantes.cDebito;
                vdebcre = Constantes.cCredito;
            }

            StringBuilder html = new StringBuilder();

            int c = 0;
            int i = 0;

            foreach (vCuentasPor item in lstdoc)
            {
                i++;
                List<vCuentasPor> can = lstcan.FindAll(delegate (vCuentasPor v) { return v.com_codigo == item.com_codigo; });
                
                if (can != null)
                {
                    item.uso = 1;
                    foreach (vCuentasPor cp in can)
                    {
                        cp.uso = 1;
                    }
                    c += can.Count();
                    item.cancela = can.Sum(s=>s.cancela);
                    item.saldo = item.monto - item.cancela;
                }
                else
                {
                    item.cancela = 0;
                    item.saldo = item.monto;
                }

                if (item.saldo<0)
                {
                    html.AppendFormat("Saldo NEGATIVO  Comprobante:{0} Codigo:{1} Saldo:{2}<br>", item.com_doctran, item.com_codigo, item.saldo);
                }

                

                List<Dcontable> lstcondoc = lstcon.FindAll(delegate (Dcontable d) { return d.dco_comprobante == item.com_codigo; });
                decimal descdoc = (item.monto ?? 0) - lstcondoc.Sum(s => s.dco_valor_nac);

                if (descdoc != 0)
                {
                    html.AppendFormat("Descuadre DOCUMENTOS Comprobante:{0} Codigo:{1} Descuadre:{2}<br>", item.com_doctran, item.com_codigo, descdoc);
                }


                foreach (vCuentasPor itemcan in can)
                {
                    List<Dcontable> lstconcan = lstcon.FindAll(delegate (Dcontable d) { return d.dco_comprobante == itemcan.com_cancela && d.dco_ddo_comproba== item.com_codigo; });
                    decimal descan = (itemcan.cancela ?? 0) - lstconcan.Sum(s => s.dco_valor_nac);
                    if (descan != 0)
                    {
                        html.AppendFormat("Descuadre CANCELACIONES Comprobante:{0} Codigo:{1} Descuadre:{2}<br>", item.com_doctran, item.com_codigo, descan);
                    }
                }
              
            }


            decimal sumadoc = lstdoc.Sum(s => s.monto ?? 0);
            decimal sumacan = lstdoc.Sum(s => s.cancela ?? 0);
            decimal saldo = sumadoc - sumacan;

            List<vCuentasPor> lstcanNO = lstcan.FindAll(delegate (vCuentasPor vp) { return vp.uso != 1; });

            foreach (vCuentasPor item in lstcanNO)
            {
                html.AppendFormat("Cancelacion NO AFECTA DOCUMENTO Comprobante:{0} {1}, Cancela:{2}, Monto cancela:{3}<br>", item.com_codigo, item.com_doctran, item.com_cancela, item.cancela);
            }


            List<vCuentasPor> lstdocNO = lstdoc.FindAll(delegate (vCuentasPor vp) { return vp.uso != 1; });

            decimal debito = lstcon.Where(w => w.dco_debcre == Constantes.cDebito).Sum(s => s.dco_valor_nac);
            decimal credito = lstcon.Where(w => w.dco_debcre == Constantes.cCredito).Sum(s => s.dco_valor_nac);
            decimal saldocon = debito - credito;


            decimal documentos = lstdoc.Sum(s => s.monto ?? 0);
            decimal cancelado = lstcan.Sum(s => s.cancela ?? 0);
            decimal saldodoc = documentos - cancelado;

            i = 0;

            foreach (Dcontable dcodoc in lstcon)
            {
                i++;
                if (dcodoc.dco_debcre == debcre)
                {
                    vCuentasPor doc = lstdoc.Find(delegate (vCuentasPor v) { return v.com_codigo == dcodoc.dco_comprobante; });
                    if (doc == null)
                    {
                        html.AppendFormat("No existe anexo para el contable cod:{0}, comprobante:{1}, monto:{2}<br>", dcodoc.dco_comprobante, dcodoc.dco_doctran, dcodoc.dco_valor_nac);

                    }
                }
                else
                {
                    vCuentasPor can = lstcan.Find(delegate (vCuentasPor v) { return v.com_cancela == dcodoc.dco_comprobante; });
                    if (can  == null)
                    {
                        html.AppendFormat("No existe anexo para el contable cod:{0}, comprobante:{1}, monto:{2}<br>", dcodoc.dco_comprobante, dcodoc.dco_compdoctran, dcodoc.dco_valor_nac);

                    }
                }

            }






           return html.ToString();

        }






        public static List<vGuiaPlanilla> GetGuiasPlanillas(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, int? politica)
        {
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where = "g.com_empresa = " + empresa + " and g.com_tipodoc = 13  and g.com_estado IN (1,2) ";

            
            if (desde.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " g.com_fecha >= {" + valores.Count + "} ";
                valores.Add(desde.Value);
                
            }
            if (hasta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " g.com_fecha <= {" + valores.Count + "} ";
                valores.Add(hasta.Value.AddDays(1).AddSeconds(-1));
            }

            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " g.com_almacen = {" + valores.Count + "} ";
                valores.Add(almacen.Value);
            }
            if (pventa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " g.com_pventa = {" + valores.Count + "} ";
                valores.Add(pventa.Value);
            }

            if (politica.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cdoc_politica = {" + valores.Count + "} ";
                valores.Add(politica.Value);
            }


            if (!string.IsNullOrEmpty(persona))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (per.per_ciruc ilike {" + valores.Count + "} or  per.per_nombres ILIKE {" + valores.Count + "} or per.per_apellidos ILIKE {" + valores.Count + "} OR per.per_razon ILIKE {" + valores.Count + "} )";
                valores.Add("%" + persona + "%");
            }

            parametros.valores = valores.ToArray();
            


            string OrderByClause = "g.com_doctran";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<vGuiaPlanilla> lst = vGuiaPlanillaBLL.GetAll(parametros,OrderByClause);
            return lst;

        }

        public static List<vGuiasFacturas> GetGuiasFacturas(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona,string socio, string tipo,  int? politica)
        {
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where = "c.com_empresa = " + empresa + " and c.com_tipodoc in ("+tipo+")  and c.com_estado IN (1,2) ";

            



            if (desde.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha >= {" + valores.Count + "} ";
                valores.Add(desde.Value);

            }
            if (hasta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha <= {" + valores.Count + "} ";
                valores.Add(hasta.Value.AddDays(1).AddSeconds(-1));
            }

            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + valores.Count + "} ";
                valores.Add(almacen.Value);
            }
            if (pventa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + valores.Count + "} ";
                valores.Add(pventa.Value);
            }

            if (politica.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cdoc_politica = {" + valores.Count + "} ";
                valores.Add(politica.Value);
            }

            if (!string.IsNullOrEmpty(socio))
            {
                parametros.where +=  " and cenv_socio = " + socio;                
            }


            if (!string.IsNullOrEmpty(persona))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (per.per_ciruc ilike {" + valores.Count + "} or per.per_nombres ILIKE {" + valores.Count + "} or per.per_apellidos ILIKE {" + valores.Count + "} OR per.per_razon ILIKE {" + valores.Count + "} )";
                valores.Add("%" + persona + "%");
            }

            parametros.valores = valores.ToArray();



            string OrderByClause = "c.com_doctran";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<vGuiasFacturas> lst = vGuiasFacturasBLL.GetAll(parametros, OrderByClause);
            return lst;

        }

        public static List<vCancelacion> GetValoresSocioSinPlanilla(int empresa, int? socio)
        {
            List<vCancelacion> lst = new List<vCancelacion>();
            if (socio.HasValue)
                lst = vCancelacionBLL.GetAll(new WhereParams("dca_monto_pla>0 and f.com_tipodoc = 4 AND dca_planilla is null and (cf.cenv_socio = {0} or cg.cenv_socio = {0}) and (f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante ) or  f.com_codigo IN (select plc_comprobante  FROM planillacli ))", socio.Value), "ddo_doctran");
            else
                lst = vCancelacionBLL.GetAll(new WhereParams("dca_monto_pla>0 and f.com_tipodoc = 4 AND dca_planilla is null and (pf.per_codigo is null and pg.per_codigo is null)  and (f.com_codigo IN (select pco_comprobante_fac FROM planillacomprobante ) or  f.com_codigo IN (select plc_comprobante  FROM planillacli ))"), "ddo_doctran");
            return lst;
        }



        public static List<vContableAnexo> ContablesAnexos(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, int[] cuentas)
        {

            WhereParams parcon = new WhereParams();
            List<object> valcon = new List<object>();                          
            WhereParams pardoc = new WhereParams();
            List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            List<object> valcan = new List<object>();

            parcon.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            pardoc.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            parcan.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;



            if (desde.HasValue)
            {
                parcon.where += ((parcon.where != "") ? " and " : "") + " com_fecha >= {" + valcon.Count + "} ";
                valcon.Add(desde.Value);
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_fecha >= {" + valdoc.Count + "} ";
                valdoc.Add(desde.Value);
                parcan.where += ((parcan.where != "") ? " and " : "") + " com_fecha >= {" + valcan.Count + "} ";
                valcan.Add(desde.Value);
            }
            if (hasta.HasValue)
            {
                hasta = hasta.Value.AddDays(1).AddSeconds(-1);
                parcon.where += ((parcon.where != "") ? " and " : "") + " com_fecha <= {" + valcon.Count + "} ";
                valcon.Add(hasta.Value);
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_fecha <= {" + valdoc.Count + "} ";
                valdoc.Add(hasta.Value);
                parcan.where += ((parcan.where != "") ? " and " : "") + " com_fecha <= {" + valcan.Count + "} ";
                valcan.Add(hasta.Value);
            }

            parcon.where += " and dco_cuenta IN (" + string.Join(",", cuentas) + ") ";
            pardoc.where += " and ddo_cuenta IN (" + string.Join(",", cuentas) + ") ";


            //if (almacen.HasValue)
            //{
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_almacen = {" + valdoc.Count + "} ";
            //    valdoc.Add(almacen.Value);
            //}
            //if (pventa.HasValue)
            //{
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_pventa = {" + valdoc.Count + "} ";
            //    valdoc.Add(pventa.Value);
            //}

            //if (tipo == "CLI")
            //{
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + "  com_tipodoc IN (4) ";
            //}
            //else                
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + "  ddo_debcre = 2 ";

            if (!string.IsNullOrEmpty(persona))
            {
                parcon.where += " and per_razon ILIKE {" + valcon.Count + "} ";
                valcon.Add("%" + persona + "%");
                pardoc.where += " and per_razon ILIKE {" + valdoc.Count + "} ";
                valdoc.Add("%" + persona + "%");
            }

            //pardoc.where += " and ddo_cancela=0 ";
            parcon.valores = valcon.ToArray();
            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();


            string OrderByClause = "per_razon, com_fecha";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<Dcontable> lstcon = DcontableBLL.GetAll(parcon, "dco_comprobante,dco_secuencia");
            List<Ddocumento> lstdoc = DdocumentoBLL.GetAll(pardoc, "");
            List<Dcancelacion> lstcan = DcancelacionBLL.GetAll(parcan, "");
                      


          List<vContableAnexo> lst = new List<vContableAnexo>();


            foreach (Dcontable dco in lstcon)
            {

                vContableAnexo item = lst.Find(delegate (vContableAnexo v) { return v.cuenta == dco.dco_cuenta && v.codigo == dco.dco_comprobante && v.cliente == dco.dco_cliente && v.ddo_comproba == dco.dco_ddo_comproba && v.debcre==dco.dco_debcre; });
                if (item == null)
                {

                    item = new vContableAnexo();
                    item.cuenta = dco.dco_cuenta;
                    item.cuentanombre = dco.dco_cuentanombre;
                    item.fecha = dco.dco_comprobantefecha;
                    item.codigo = dco.dco_comprobante;
                    item.doctran = dco.dco_doctran;
                    item.cliente = dco.dco_cliente;
                    item.razon = dco.dco_clienteapellidos + " " + dco.dco_clientenombres;
                    item.debcre = dco.dco_debcre;
                    if (dco.dco_debcre == Constantes.cCredito)
                        item.credito = dco.dco_valor_nac;
                    if (dco.dco_debcre == Constantes.cDebito)
                        item.debito = dco.dco_valor_nac;

                    item.ddo_comproba = dco.dco_ddo_comproba;
                    item.almacen = dco.dco_almacen;


                    item.coddoc = "";
                    item.documento = "";
                    item.montodoc = 0;
                    item.saldo = 0;
                    item.codcan = "";
                    item.cancelacion = "";
                    item.montocan = 0;

                    item.montodocdeb = 0;
                    item.canceldocdeb = 0;
                    item.saldodocdeb = 0;

                    item.montodoccre = 0;
                    item.canceldoccre = 0;
                    item.saldodoccre = 0;

                    item.montocandeb = 0;
                    item.montocancre = 0;


                    lst.Add(item);
                }
                else
                {
                    if (dco.dco_debcre == Constantes.cCredito)
                        item.credito += dco.dco_valor_nac;
                    if (dco.dco_debcre == Constantes.cDebito)
                        item.debito += dco.dco_valor_nac;
                }
            }



            //foreach (Dcontable dco in lstcon)
            foreach (vContableAnexo vco in lst)
            {
                List<Ddocumento> lstddo = new List<Ddocumento>();

                long? codcom = null;

                if (vco.codigo == vco.ddo_comproba)
                    codcom = vco.ddo_comproba;
                else if (vco.ddo_comproba.HasValue)
                {
                    if (vco.ddo_comproba.Value == 0)
                        codcom = vco.codigo;
                }




                if (codcom.HasValue)
                {
                    if (vco.cliente.HasValue)
                        lstddo = lstdoc.FindAll(delegate (Ddocumento dd) { return dd.ddo_comprobante == codcom && dd.ddo_codclipro == vco.cliente.Value; });
                    else
                        lstddo = lstdoc.FindAll(delegate (Ddocumento dd) { return dd.ddo_comprobante == codcom; });
                }



                List<Dcancelacion> lstdca = lstcan.FindAll(delegate (Dcancelacion dc) { return dc.dca_comprobante_can == vco.codigo && dc.dca_comprobante == vco.ddo_comproba && dc.dca_debcre== vco.debcre; });

                decimal cancela = 0;
                decimal canceladeb = 0;
                decimal cancelacre = 0;

                foreach (Ddocumento ddo in lstddo)
                {
                    //List<Dcancelacion> candoc = lstcan.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == ddo.ddo_comprobante && d.dca_transacc == ddo.ddo_transacc && d.dca_doctran == ddo.ddo_doctran && d.dca_pago == ddo.ddo_pago; });
                    List<Dcancelacion> candoc = lstcan.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == ddo.ddo_comprobante && d.dca_doctran == ddo.ddo_doctran && d.dca_pago == ddo.ddo_pago; });
                    if (candoc != null)
                    {
                        cancela += candoc.Sum(s => s.dca_monto ?? 0);
                        canceladeb += candoc.Where(w => w.dca_debcre == Constantes.cCredito).Sum(s => s.dca_monto ?? 0);
                        cancelacre += candoc.Where(w => w.dca_debcre == Constantes.cDebito).Sum(s => s.dca_monto ?? 0);
                    }


                    vco.coddoc += (!string.IsNullOrEmpty(vco.coddoc) ? "," : "") + ddo.ddo_comprobante.ToString();
                    vco.documento += (!string.IsNullOrEmpty(vco.documento) ? "," : "") + ddo.ddo_doctran;
                    vco.montodoc += ddo.ddo_monto;

                    if (ddo.ddo_debcre==Constantes.cDebito)
                    {
                        vco.montodocdeb += ddo.ddo_monto;
                        vco.canceldocdeb += canceladeb;
                    }
                    if (ddo.ddo_debcre == Constantes.cCredito)
                    {
                        vco.montodoccre += ddo.ddo_monto;
                        vco.canceldoccre += cancelacre;
                    }

                }

                vco.saldo = vco.montodoc - cancela;
                vco.saldodocdeb = vco.montodocdeb - vco.canceldocdeb;
                vco.saldodoccre = vco.montodoccre - vco.canceldoccre;

                
                foreach (Dcancelacion dca in lstdca)
                {
                    vco.codcan += (!string.IsNullOrEmpty(vco.codcan) ? "," : "") + dca.dca_comprobante_can.ToString();
                    vco.cancelacion += (!string.IsNullOrEmpty(vco.cancelacion) ? "," : "") + dca.dca_compcandoctran;
                    vco.montocan += dca.dca_monto;
                    if (dca.dca_debcre == Constantes.cDebito)
                        vco.montocandeb += dca.dca_monto;
                    if (dca.dca_debcre == Constantes.cCredito)
                        vco.montocancre+= dca.dca_monto;
                }
                


              






                /*


                Ddocumento ddo = null;
                if (dco.dco_cliente.HasValue)
                    ddo = lstdoc.Find(delegate (Ddocumento d) { return d.ddo_comprobante == dco.dco_comprobante && d.ddo_cuenta == dco.dco_cuenta && d.ddo_codclipro == dco.dco_cliente; });
                else
                    ddo = lstdoc.Find(delegate (Ddocumento d) { return d.ddo_comprobante == dco.dco_comprobante && d.ddo_cuenta == dco.dco_cuenta; });
                List<Dcancelacion> lstdca = lstcan.FindAll(delegate(Dcancelacion d) { return d.dca_comprobante_can == dco.dco_comprobante; });

                List<Dcancelacion> candoc = null;
                if (ddo != null)
                {
                    candoc = lstcan.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == ddo.ddo_comprobante && d.dca_transacc == ddo.ddo_transacc && d.dca_doctran == ddo.ddo_doctran && d.dca_pago== ddo.ddo_pago; });

                }

                vContableAnexo item = new vContableAnexo();
                item.cuenta = dco.dco_cuenta;
                item.cuentanombre = dco.dco_cuentanombre;
                item.fecha = dco.dco_comprobantefecha;
                item.codigo = dco.dco_comprobante;
                item.doctran = dco.dco_doctran;
                item.cliente = dco.dco_cliente;
                item.razon = dco.dco_clienteapellidos + " " + dco.dco_clientenombres;
                item.debcre = dco.dco_debcre;
                if (dco.dco_debcre == Constantes.cCredito)
                    item.credito = dco.dco_valor_nac;
                if (dco.dco_debcre == Constantes.cDebito)
                    item.debito = dco.dco_valor_nac;
                if (ddo != null)
                {
                    item.coddoc = ddo.ddo_comprobante.ToString();
                    item.documento = ddo.ddo_doctran;
                    item.montodoc = ddo.ddo_monto;
                }
                else
                    item.montodoc = 0;

                decimal cancela = 0;
                if (candoc != null)
                    cancela = candoc.Sum(s => s.dca_monto ?? 0);
                item.saldo = item.montodoc - cancela;


                item.montocan = 0;

                foreach (Dcancelacion dca in lstdca)
                {
                    item.codcan += (!string.IsNullOrEmpty(item.codcan) ? "," : "") + dca.dca_comprobante_can.ToString();
                    item.cancelacion+= (!string.IsNullOrEmpty(item.cancelacion)? "," : "") + dca.dca_compcandoctran;
                    item.montocan += dca.dca_monto;
                }


                lst.Add(item);

                */



            }


            return lst;

        }

        public static List<vContableAnexo> DocumentosAnexos(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, int[] cuentas)
        {

            WhereParams parcon = new WhereParams();
            List<object> valcon = new List<object>();
            WhereParams pardoc = new WhereParams();
            List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            List<object> valcan = new List<object>();

            parcon.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            pardoc.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            parcan.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;



            if (desde.HasValue)
            {
                parcon.where += ((parcon.where != "") ? " and " : "") + " com_fecha >= {" + valcon.Count + "} ";
                valcon.Add(desde.Value);
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_fecha >= {" + valdoc.Count + "} ";
                valdoc.Add(desde.Value);
                parcan.where += ((parcan.where != "") ? " and " : "") + " com_fecha >= {" + valcan.Count + "} ";
                valcan.Add(desde.Value);
            }
            if (hasta.HasValue)
            {
                hasta = hasta.Value.AddDays(1).AddSeconds(-1);
                parcon.where += ((parcon.where != "") ? " and " : "") + " com_fecha <= {" + valcon.Count + "} ";
                valcon.Add(hasta.Value);
                pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_fecha <= {" + valdoc.Count + "} ";
                valdoc.Add(hasta.Value);
                parcan.where += ((parcan.where != "") ? " and " : "") + " com_fecha <= {" + valcan.Count + "} ";
                valcan.Add(hasta.Value);
            }

            parcon.where += " and dco_cuenta IN (" + string.Join(",", cuentas) + ") ";
            pardoc.where += " and ddo_cuenta IN (" + string.Join(",", cuentas) + ") ";


            //if (almacen.HasValue)
            //{
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_almacen = {" + valdoc.Count + "} ";
            //    valdoc.Add(almacen.Value);
            //}
            //if (pventa.HasValue)
            //{
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + " com_pventa = {" + valdoc.Count + "} ";
            //    valdoc.Add(pventa.Value);
            //}

            //if (tipo == "CLI")
            //{
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + "  com_tipodoc IN (4) ";
            //}
            //else                
            //    pardoc.where += ((pardoc.where != "") ? " and " : "") + "  ddo_debcre = 2 ";

            if (!string.IsNullOrEmpty(persona))
            {
                parcon.where += " and per_razon ILIKE {" + valcon.Count + "} ";
                valcon.Add("%" + persona + "%");
                pardoc.where += " and per_razon ILIKE {" + valdoc.Count + "} ";
                valdoc.Add("%" + persona + "%");
            }

            //pardoc.where += " and ddo_cancela=0 ";
            parcon.valores = valcon.ToArray();
            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();


            string OrderByClause = "per_razon, com_fecha";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<Dcontable> lstcon = DcontableBLL.GetAll(parcon, "dco_comprobante,dco_secuencia");
            List<Ddocumento> lstdoc = DdocumentoBLL.GetAll(pardoc, "");
            List<Dcancelacion> lstcan = DcancelacionBLL.GetAll(parcan, "");



            List<vContableAnexo> lst = new List<vContableAnexo>();


            foreach (Ddocumento ddo in lstdoc)
            {
                List<Dcancelacion> candoc = lstcan.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == ddo.ddo_comprobante && d.dca_doctran == ddo.ddo_doctran && d.dca_pago == ddo.ddo_pago; });

                foreach (Dcancelacion dca in candoc)
                {

                    vContableAnexo item = new vContableAnexo();
                    item.ddo_comproba = ddo.ddo_comprobante;
                    item.doctran = ddo.ddo_doctran;
                    item.fecha = ddo.ddo_fecha_emi;
                    item.cliente = ddo.ddo_codclipro;
                    item.razon = ddo.ddo_clienteapellidos + " " + ddo.ddo_clientenombres;
                    item.montodoc = ddo.ddo_monto;

                    item.cancelacion = dca.dca_compcandoctran;
                    item.codcan = dca.dca_comprobante_can.ToString();
                    item.cuentanombre = dca.dca_comprobantecanfecha.ToString();
                    item.montocan = dca.dca_monto;
                    ddo.ddo_monto = ddo.ddo_monto - dca.dca_monto;
                    item.saldo = ddo.ddo_monto;
                    lst.Add(item);




                }

/*
                vContableAnexo item = lst.Find(delegate (vContableAnexo v) { return v.cuenta == dco.dco_cuenta && v.codigo == dco.dco_comprobante && v.cliente == dco.dco_cliente && v.ddo_comproba == dco.dco_ddo_comproba && v.debcre == dco.dco_debcre; });
                if (item == null)
                {

                    item = new vContableAnexo();
                    item.cuenta = dco.dco_cuenta;
                    item.cuentanombre = dco.dco_cuentanombre;
                    item.fecha = dco.dco_comprobantefecha;
                    item.codigo = dco.dco_comprobante;
                    item.doctran = dco.dco_doctran;
                    item.cliente = dco.dco_cliente;
                    item.razon = dco.dco_clienteapellidos + " " + dco.dco_clientenombres;
                    item.debcre = dco.dco_debcre;
                    if (dco.dco_debcre == Constantes.cCredito)
                        item.credito = dco.dco_valor_nac;
                    if (dco.dco_debcre == Constantes.cDebito)
                        item.debito = dco.dco_valor_nac;

                    item.ddo_comproba = dco.dco_ddo_comproba;
                    item.almacen = dco.dco_almacen;


                    item.coddoc = "";
                    item.documento = "";
                    item.montodoc = 0;
                    item.saldo = 0;
                    item.codcan = "";
                    item.cancelacion = "";
                    item.montocan = 0;

                    item.montodocdeb = 0;
                    item.canceldocdeb = 0;
                    item.saldodocdeb = 0;

                    item.montodoccre = 0;
                    item.canceldoccre = 0;
                    item.saldodoccre = 0;

                    item.montocandeb = 0;
                    item.montocancre = 0;


                    lst.Add(item);
                }
                else
                {
                    if (dco.dco_debcre == Constantes.cCredito)
                        item.credito += dco.dco_valor_nac;
                    if (dco.dco_debcre == Constantes.cDebito)
                        item.debito += dco.dco_valor_nac;
                }
            }



            //foreach (Dcontable dco in lstcon)
            foreach (vContableAnexo vco in lst)
            {
                List<Ddocumento> lstddo = new List<Ddocumento>();

                long? codcom = null;

                if (vco.codigo == vco.ddo_comproba)
                    codcom = vco.ddo_comproba;
                else if (vco.ddo_comproba.HasValue)
                {
                    if (vco.ddo_comproba.Value == 0)
                        codcom = vco.codigo;
                }




                if (codcom.HasValue)
                {
                    if (vco.cliente.HasValue)
                        lstddo = lstdoc.FindAll(delegate (Ddocumento dd) { return dd.ddo_comprobante == codcom && dd.ddo_codclipro == vco.cliente.Value; });
                    else
                        lstddo = lstdoc.FindAll(delegate (Ddocumento dd) { return dd.ddo_comprobante == codcom; });
                }



                List<Dcancelacion> lstdca = lstcan.FindAll(delegate (Dcancelacion dc) { return dc.dca_comprobante_can == vco.codigo && dc.dca_comprobante == vco.ddo_comproba && dc.dca_debcre == vco.debcre; });

                decimal cancela = 0;
                decimal canceladeb = 0;
                decimal cancelacre = 0;

                foreach (Ddocumento ddo in lstddo)
                {
                    //List<Dcancelacion> candoc = lstcan.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == ddo.ddo_comprobante && d.dca_transacc == ddo.ddo_transacc && d.dca_doctran == ddo.ddo_doctran && d.dca_pago == ddo.ddo_pago; });
                    List<Dcancelacion> candoc = lstcan.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == ddo.ddo_comprobante && d.dca_doctran == ddo.ddo_doctran && d.dca_pago == ddo.ddo_pago; });
                    if (candoc != null)
                    {
                        cancela += candoc.Sum(s => s.dca_monto ?? 0);
                        canceladeb += candoc.Where(w => w.dca_debcre == Constantes.cCredito).Sum(s => s.dca_monto ?? 0);
                        cancelacre += candoc.Where(w => w.dca_debcre == Constantes.cDebito).Sum(s => s.dca_monto ?? 0);
                    }


                    vco.coddoc += (!string.IsNullOrEmpty(vco.coddoc) ? "," : "") + ddo.ddo_comprobante.ToString();
                    vco.documento += (!string.IsNullOrEmpty(vco.documento) ? "," : "") + ddo.ddo_doctran;
                    vco.montodoc += ddo.ddo_monto;

                    if (ddo.ddo_debcre == Constantes.cDebito)
                    {
                        vco.montodocdeb += ddo.ddo_monto;
                        vco.canceldocdeb += canceladeb;
                    }
                    if (ddo.ddo_debcre == Constantes.cCredito)
                    {
                        vco.montodoccre += ddo.ddo_monto;
                        vco.canceldoccre += cancelacre;
                    }

                }

                vco.saldo = vco.montodoc - cancela;
                vco.saldodocdeb = vco.montodocdeb - vco.canceldocdeb;
                vco.saldodoccre = vco.montodoccre - vco.canceldoccre;


                foreach (Dcancelacion dca in lstdca)
                {
                    vco.codcan += (!string.IsNullOrEmpty(vco.codcan) ? "," : "") + dca.dca_comprobante_can.ToString();
                    vco.cancelacion += (!string.IsNullOrEmpty(vco.cancelacion) ? "," : "") + dca.dca_compcandoctran;
                    vco.montocan += dca.dca_monto;
                    if (dca.dca_debcre == Constantes.cDebito)
                        vco.montocandeb += dca.dca_monto;
                    if (dca.dca_debcre == Constantes.cCredito)
                        vco.montocancre += dca.dca_monto;
                }








    */

             


            }


            return lst;

        }

        public static List<vContableAnexo> CuentasPorAnexos(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, string persona, int[] cuentas)
        {

            List<vContableAnexo> lst = ContablesAnexos(desde, hasta, empresa, almacen, pventa, persona, cuentas);

            return lst.Where(w => w.saldo != 0).ToList();
        

        }


        public static List<vEstadoCuenta> EstadoCuentaDetalle(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa, int persona, string cuentas, int debcre, string orden, ref decimal saldoinicial, ref decimal saldofinal)
        {
            WhereParams parsumdoc = new WhereParams();
            List<object> valsumdoc = new List<object>();
            WhereParams parsumcan = new WhereParams();
            List<object> valsumcan = new List<object>();

            WhereParams pardoc = new WhereParams();
            List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            List<object> valcan = new List<object>();

            parsumdoc.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + persona + " and ddo_cuenta in (" + cuentas + ") ";
            parsumcan.where = "cc.com_empresa = " + empresa + " and cc.com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + persona + " and ddo_cuenta in (" +  cuentas + ") ";

            pardoc.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + persona + " and ddo_cuenta in (" +  cuentas + ") ";
            parcan.where = "cc.com_empresa = " + empresa + " and cc.com_estado= " + Constantes.cEstadoMayorizado + " and per_codigo=" + persona + " and ddo_cuenta in (" + cuentas + ") ";


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parsumdoc.where += " and com_almacen = {" + valsumdoc.Count + "} ";
                    valsumdoc.Add(almacen);
                    parsumcan.where += " and cd.com_almacen = {" + valsumcan.Count + "} ";
                    valsumcan.Add(almacen);

                    pardoc.where += " and com_almacen = {" + valdoc.Count + "} ";
                    valdoc.Add(almacen);
                    parcan.where += " and cd.com_almacen = {" + valcan.Count + "} ";
                    valcan.Add(almacen);

                }
            }
            parsumdoc.where += " and com_fecha<{" + valsumdoc.Count + "} ";
            valsumdoc.Add(desde);
            parsumcan.where += " and cc.com_fecha<{" + valsumcan.Count + "} ";
            valsumcan.Add(desde);

            pardoc.where += " and com_fecha between {" + valdoc.Count + "} and {" + (valdoc.Count + 1) + "}";
            valdoc.Add(desde);
            valdoc.Add(hasta);

            parcan.where += " and cc.com_fecha between {" + valcan.Count + "} and {" + (valcan.Count + 1) + "}";
            valcan.Add(desde);
            valcan.Add(hasta);

            parsumdoc.valores = valsumdoc.ToArray();
            parsumcan.valores = valsumcan.ToArray();

            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();


            List<vEstadoCuenta> lstsumdoc = vEstadoCuentaBLL.GetAllSumDoc(parsumdoc, "");
            List<vEstadoCuenta> lstsumcan = vEstadoCuentaBLL.GetAllSumCan(parsumcan, "");

            List<vEstadoCuenta> lstdoc = vEstadoCuentaBLL.GetAllDoc1(pardoc, "com_fecha");
            List<vEstadoCuenta> lstcan = vEstadoCuentaBLL.GetAllCan1(parcan, "cc.com_fecha");

            List<vEstadoCuenta> lstall = new List<vEstadoCuenta>();
            lstall.AddRange(lstdoc);
            lstall.AddRange(lstcan);

            lstall.Sort(delegate (vEstadoCuenta x, vEstadoCuenta y)
            {
                return x.com_fecha.Value.CompareTo(y.com_fecha.Value);
            });


            saldoinicial = 0;
            if (debcre == 1)
            {
                saldoinicial += lstsumdoc.Sum(s => s.valordebito ?? 0);
                saldoinicial -= lstsumcan.Sum(s => s.valorcredito ?? 0);
            }
            else

            {
                if (lstsumdoc.Count > 0)
                    saldoinicial += lstsumdoc[0].valorcredito.Value;
                if (lstsumcan.Count > 0)
                    saldoinicial -= lstsumcan[0].valordebito.Value;
            }

            saldofinal = saldoinicial;
            saldofinal += lstdoc.Sum(s => s.ddo_monto ?? 0);
            saldofinal -= lstcan.Sum(s => s.dca_monto ?? 0);

            decimal total = saldoinicial;


            List<vEstadoCuenta> retorno = new List<vEstadoCuenta>();
            if (string.IsNullOrEmpty(orden) || orden== "1")
            {
                foreach (vEstadoCuenta item in lstall)
                {
                    decimal debito = 0;
                    decimal credito = 0;
                    decimal saldo = 0;
                    if (item.ddo_comprobante.HasValue)
                    {
                        debito = item.ddo_debcre == Constantes.cDebito ? item.ddo_monto.Value : 0;
                        credito = item.ddo_debcre == Constantes.cCredito ? item.ddo_monto.Value : 0;
                        saldo = debito - credito;
                        total += item.ddo_monto ?? 0;

                        vEstadoCuenta e = new vEstadoCuenta();
                        e.com_fecha = item.com_fecha;
                        e.com_doctran = item.com_doctran;
                        e.ddo_fecha_ven = item.ddo_fecha_ven;
                        e.valordebito = debito;
                        e.valorcredito = credito;
                        e.valorsaldo = saldo;
                        e.valortotal = total;
                        retorno.Add(e);

                    }
                    else
                    {
                        debito = item.dca_debcre == Constantes.cDebito ? item.dca_monto.Value : 0;
                        credito = item.dca_debcre == Constantes.cCredito ? item.dca_monto.Value : 0;
                        saldo = debito - credito;
                        total -= item.dca_monto ?? 0;


                        vEstadoCuenta e = new vEstadoCuenta();
                        e.com_fecha = item.com_fecha;
                        e.com_doctran = item.com_doctran;
                        e.ddo_doctran= item.ddo_doctran;
                        e.valordebito = debito;
                        e.valorcredito = credito;
                        e.valorsaldo = saldo;
                        e.valortotal = total;
                        retorno.Add(e);

                    }

                }
            } 
            if (orden=="2")
            {
                foreach (vEstadoCuenta item in lstdoc)
                {
                    decimal debito = item.ddo_debcre == Constantes.cDebito ? item.ddo_monto.Value : 0;
                    decimal credito = item.ddo_debcre == Constantes.cCredito ? item.ddo_monto.Value : 0;
                    decimal saldo = debito - credito;
                    total += item.ddo_monto ?? 0;

                    List<vEstadoCuenta> lstc = lstcan.FindAll(delegate (vEstadoCuenta v) { return v.dca_comprobante == item.ddo_comprobante && v.dca_transacc == item.ddo_transacc && v.dca_doctran == item.ddo_doctran && v.dca_pago == item.ddo_pago; });


                    vEstadoCuenta e = new vEstadoCuenta();
                    e.com_fecha = item.com_fecha;
                    e.com_doctran = item.com_doctran;
                    e.ddo_fecha_ven = item.ddo_fecha_ven;
                    e.valordebito = debito;
                    e.valorcredito = credito;
                    e.valorsaldo = saldo;
                    e.valortotal = total;
                    retorno.Add(e);
                   
                    vEstadoCuenta vec = lstall.Find(delegate (vEstadoCuenta v) { return v.ddo_empresa == item.ddo_empresa && v.ddo_comprobante == item.ddo_comprobante && v.ddo_transacc == item.ddo_transacc && v.ddo_doctran == item.ddo_doctran; });
                    lstall.Remove(vec);


                    foreach (vEstadoCuenta dca in lstc)
                    {
                        decimal dcadebito = dca.dca_debcre == Constantes.cDebito ? dca.dca_monto.Value : 0;
                        decimal dcacredito = dca.dca_debcre == Constantes.cCredito ? dca.dca_monto.Value : 0;
                        //decimal dcasaldo = dcadebito - dcacredito;
                        saldo += dcadebito - dcacredito;

                        total -= dca.dca_monto ?? 0;



                        e = new vEstadoCuenta();
                        e.com_fecha = dca.com_fecha;
                        e.com_doctran = dca.com_doctran;
                        e.ddo_doctran = dca.ddo_doctran;
                        e.valordebito = dcadebito;
                        e.valorcredito = dcacredito;
                        e.valorsaldo = saldo;
                        e.valortotal = total;
                        retorno.Add(e);

                      

                        //vec = lstall.Find(delegate (vEstadoCuenta v) { return v.dca_empresa == dca.dca_empresa && v.dca_comprobante == dca.dca_comprobante && v.dca_transacc == dca.dca_transacc && v.dca_doctran == dca.dca_doctran && v.dca_pago == dca.dca_pago && v.dca_comprobante_can == dca.dca_comprobante_can && v.dca_secuencia == dca.dca_secuencia; });
                        vec = lstall.Find(delegate (vEstadoCuenta v) { return v.dca_empresa == dca.dca_empresa && v.dca_comprobante == dca.dca_comprobante && v.dca_transacc == dca.dca_transacc && v.dca_doctran == dca.dca_doctran && v.dca_comprobante_can == dca.dca_comprobante_can; });
                        lstall.Remove(vec);
                    }
                }

                if (lstall.Count > 0)
                {

                    vEstadoCuenta e = new vEstadoCuenta();
                    
                    e.com_doctran = "COMPROBATE FUERA RANGO";
                    retorno.Add(e);

                    foreach (vEstadoCuenta dca in lstall)
                    {
                        decimal dcadebito = dca.dca_debcre == Constantes.cDebito ? dca.dca_monto.Value : 0;
                        decimal dcacredito = dca.dca_debcre == Constantes.cCredito ? dca.dca_monto.Value : 0;
                        decimal dcasaldo = dcadebito - dcacredito;

                        total -= dca.dca_monto ?? 0;


                        e = new vEstadoCuenta();
                        e.com_fecha = dca.com_fecha;
                        e.com_doctran = dca.com_doctran;
                        e.ddo_doctran = dca.ddo_doctran;
                        e.valordebito = dcadebito;
                        e.valorcredito = dcacredito;
                        e.valorsaldo = dcasaldo;
                        e.valortotal = total;
                        retorno.Add(e);
                    }
                }
            }


            return retorno;

           


        }


        public static List<Comprobante> ComprobantesElectronicos(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa, int? tipodoc, string estado)
        {

            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            hasta = hasta.AddDays(1).AddSeconds(-1);


            parametros.where = "com_empresa=" + empresa;

            parametros.where +=  " and  com_fecha between {" + valores.Count() + "} ";
            valores.Add(desde);
            parametros.where += " and  {" + valores.Count() + "} ";
            valores.Add(hasta);

            
            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + "  com_almacen = {" + valores.Count() + "} ";
                    valores.Add(almacen);
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + "  com_pventa = {" + valores.Count() + "} ";
                    valores.Add(pventa);
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_empresa = {" + valores.Count() + "} ";
                    valores.Add(empresa);
                }
            }

            bool hastipo = false;
            if (tipodoc.HasValue)
            {
                if (tipodoc.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_tipodoc = {" + valores.Count() + "} ";
                    valores.Add(tipodoc);
                    hastipo = true;
                }
            }

            if (!hastipo)
            {
                string parelectronicos = Constantes.GetParameter("electronicos");
                var serializer = new JavaScriptSerializer();
                List<Electronicos> lst = serializer.Deserialize<List<Electronicos>>(parelectronicos);
                string strtipo = "";
                foreach (Electronicos item in lst)
                {
                    strtipo += (strtipo != "" ? "," : "") + item.tipodoc.ToString();
                }
                if (!string.IsNullOrEmpty(strtipo))
                {
                    parametros.where += " and  com_tipodoc IN (" + strtipo + ") ";                    
                }

            }

            if (estado != "")
            {
                parametros.where += " and com_estadoelec = '" + estado + "'";                
            }

            //    parametros.where += " and c.com_estado IN (" + estado + ")";

            //    /*string[] arrayestado = estado.Split(',');
            //    foreach (string item in arrayestado)
            //    {
            //        parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado IN ({" + valores.Count() + "},{" + (valores.Count() + 1) + "}) ";
            //        valores.Add(Constantes.cEstadoMayorizado);

            //    }*/
            //}



            parametros.valores = valores.ToArray();
            List<Comprobante> lista = ComprobanteBLL.GetAll(parametros, "com_numero");
            return lista;

        }

        public static string VerificarComprobantesElectronicos(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa, int? tipodoc, string estado)
        {


            List<Comprobante> lista = ComprobantesElectronicos(desde, hasta, almacen, pventa, empresa, tipodoc, estado);

            foreach (Comprobante item in lista)
            {
                Electronico.UpdateElectronicoDataAsync(item);
            }


            return "OK";

        }



        public static string CancelacionesNegativas()
        {
            string ctas = Constantes.GetParameter("ctasclientes");
            string[] arrayctas = ctas.Split(',');
            List<int> lstcuentas = new List<int>();
            foreach (string item in arrayctas)
            {
                int cta = -1;
                int.TryParse(item, out cta);
                if (cta > 0)
                    lstcuentas.Add(cta);
            }
            

            List<vCuentasPor> lst = Packages.General.GetCuentasPorFull(null, DateTime.Now, 1, null, null, null, "", lstcuentas.ToArray(), "1","");
            List<Dcancelacion> lstcan = new List<Dcancelacion>(); 
            StringBuilder html = new StringBuilder(); 
            foreach (vCuentasPor item in lst)
            {
                if (item.saldo < 0)
                {
                    lstcan.AddRange(CancelacionesNegativas(1, item.com_codigo.Value, item.monto.Value));
                    html.AppendFormat("{0} Monto:{1} Cancelado:{2} Saldo:{3}", item.com_doctran, item.monto, item.cancela, item.saldo);
                }
                    

            }


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                foreach (Dcancelacion dca in lstcan)
                {
                    dca.dca_empresa_key = dca.dca_empresa;
                    dca.dca_comprobante_key = dca.dca_comprobante;
                    dca.dca_transacc_key = dca.dca_transacc;
                    dca.dca_doctran_key = dca.dca_doctran;
                    dca.dca_pago_key = dca.dca_pago;
                    dca.dca_comprobante_can_key = dca.dca_comprobante_can;
                    dca.dca_secuencia_key = dca.dca_secuencia_key;
                    DcancelacionBLL.Update(transaction, dca);
                }

               
                transaction.Commit();

            
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }







            return html.ToString();
        }


        public static List<Dcancelacion> CancelacionesNegativas(int empresa, long codigo, decimal monto)
        {
            List<Dcancelacion> lst = DcancelacionBLL.GetAll("dca_empresa=" + empresa + " and dca_comprobante=" + codigo, "");

            decimal valor = 0;
            foreach (Dcancelacion item in lst)
            {
                if ((valor + item.dca_monto.Value) > monto)
                {
                    decimal diferencia = (valor  + item.dca_monto.Value) - monto;
                    item.dca_monto = item.dca_monto - diferencia;

                }
                valor += item.dca_monto.Value;
            }

            return lst;

        }


        public static List<vComprobanteHojaRuta> GetComprobanteHojaRuta(DateTime? desde, DateTime? hasta, int empresa, int? almacen, int? pventa)
        {
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where = "fg.com_empresa = " + empresa + " and fg.com_tipodoc in (4,13)";


            if (desde.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " fg.com_fecha >= {" + valores.Count + "} ";
                valores.Add(desde.Value);

            }
            if (hasta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " fg.com_fecha <= {" + valores.Count + "} ";
                valores.Add(hasta.Value.AddDays(1).AddSeconds(-1));
            }

            if (almacen.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " fg.com_almacen = {" + valores.Count + "} ";
                valores.Add(almacen.Value);
            }
            if (pventa.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " fg.com_pventa = {" + valores.Count + "} ";
                valores.Add(pventa.Value);
            }

            //if (politica.HasValue)
            //{
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " cdoc_politica = {" + valores.Count + "} ";
            //    valores.Add(politica.Value);
            //}


            //if (!string.IsNullOrEmpty(persona))
            //{
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " (per_nombres ILIKE {" + valores.Count + "} or per_apellidos ILIKE {" + valores.Count + "} OR per_razon ILIKE {" + valores.Count + "} )";
            //    valores.Add("%" + persona + "%");
            //}

            parametros.valores = valores.ToArray();



            string OrderByClause = "fg.com_doctran";

            //List<vCuentasPor> lst = vCuentasPorBLL.GetAll1(parametros, new WhereParams("sum(ddo_monto) - sum(ddo_cancela) <> 0 "), OrderByClause);
            List<vComprobanteHojaRuta> lst = vComprobanteHojaRutaBLL.GetAll(parametros, OrderByClause);
            return lst;

        }

        public static List<vPlanillaSocioTot> GetPlanillaSocioTot(DateTime? desde, DateTime? hasta, int empresa,  string socio)
        {
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            WhereParams parametrosRUB = new WhereParams();
            List<object> valoresRUB = new List<object>();

            parametros.where = "com_empresa = " + empresa + " and  com_tipodoc= 7";
            parametrosRUB.where = "com_empresa = " + empresa + " and  com_tipodoc= 7";


            if (desde.HasValue)
            {
                parametros.where +=  " and com_fecha >= {" + valores.Count + "} ";
                valores.Add(desde.Value);
                parametrosRUB.where += " and com_fecha >= {" + valoresRUB.Count + "} ";
                valoresRUB.Add(desde.Value);

            }
            if (hasta.HasValue)
            {
                parametros.where +=  " and com_fecha <= {" + valores.Count + "} ";
                valores.Add(hasta.Value.AddDays(1).AddSeconds(-1));
                parametrosRUB.where += " and com_fecha <= {" + valoresRUB.Count + "} ";
                valoresRUB.Add(hasta.Value.AddDays(1).AddSeconds(-1));
            }

                   

            if (!string.IsNullOrEmpty(socio))
            {
                parametros.where +=  " and (per_nombres ILIKE {" + valores.Count + "} or per_apellidos ILIKE {" + valores.Count + "} OR per_razon ILIKE {" + valores.Count + "} )";
                valores.Add("%" + socio + "%");
            }

            parametros.valores = valores.ToArray();
            parametrosRUB.valores = valoresRUB.ToArray();


            string OrderByClause = " com_doctran";

            List<vPlanillaSocioTot> lst = vPlanillaSocioTotBLL.GetAll(parametros, OrderByClause);
            List<vPlanillaSocioTot> lstrub = vPlanillaSocioTotBLL.GetAllRub(parametrosRUB, OrderByClause);

            foreach (vPlanillaSocioTot item in lst)
            {
                vPlanillaSocioTot vpst = lstrub.Find(delegate (vPlanillaSocioTot v) { return v.com_codigo == item.com_codigo; });
                if (vpst != null)
                {
                    item.ingresos = vpst.ingresos;
                    item.egresos = vpst.egresos;
                }

            }
            return lst;          
        }


        //public static 


        public static string RemoveRecibos(int empresa, DateTime? desde, DateTime? hasta, string tipos)
        {


            WhereParams parfac= new WhereParams();
            List<object> valfac= new List<object>();

            WhereParams pardoc = new WhereParams();
            List<object> valdoc= new List<object>();

            WhereParams parcan= new WhereParams();
            List<object> valcan= new List<object>();
            WhereParams parrec= new WhereParams();
            List<object> valrec= new List<object>();

            WhereParams parcon = new WhereParams();
            List<object> valcon= new List<object>();


            parfac.where = "com_empresa = " + empresa + " and com_tipodoc=4  and com_estado= " + Constantes.cEstadoMayorizado ;            
            parfac.where += " and com_fecha between {" + valfac.Count + "} and {" + (valfac.Count + 1) + "}";
            valfac.Add(desde);            
            valfac.Add(hasta);

            pardoc.where = "ddo_comprobante in (select com_codigo from comprobante  where com_tipodoc in (4) and com_estado=2 and com_fecha between {" + valdoc.Count + "} and {" + (valdoc.Count + 1) + "})";
            valdoc.Add(desde);
            valdoc.Add(hasta);



            parcan.where = "dca_comprobante in (select com_codigo from comprobante  where com_tipodoc in (4) and com_estado=2 and com_fecha between {" + valcan.Count + "} and {" + (valcan.Count + 1) + "})";            
            valcan.Add(desde);
            valcan.Add(hasta);
            parcan.where += "and dca_comprobante_can in (select dfp_comprobante from drecibo where  dfp_tipopago in (1,18))";

            parrec.where = " dfp_comprobante in (select dca_comprobante_can from dcancelacion inner join comprobante on dca_comprobante=com_codigo where com_tipodoc in (4) and com_estado=2 and com_fecha between {" + valrec.Count + "} and {" + (valrec.Count + 1) + "})";
            valrec.Add(desde);
            valrec.Add(hasta);
            parrec.where += "and dfp_tipopago in (1,18)";

            parcon.where = " dco_comprobante in (select dca_comprobante_can from dcancelacion inner join comprobante on dca_comprobante=com_codigo where com_tipodoc in (4) and com_estado=2 and com_fecha between {" + valcon.Count + "} and {" + (valcon.Count + 1) + "})";
            valcon.Add(desde);
            valcon.Add(hasta);
            


            parfac.valores = valfac.ToArray();
            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();
            parrec.valores = valrec.ToArray();
            parcon.valores = valcon.ToArray();


            List<Comprobante> lstfac = ComprobanteBLL.GetAll(parfac, "");
            List<Ddocumento> lstdoc = DdocumentoBLL.GetAll(pardoc, "");
            List<Dcancelacion> lstcan = DcancelacionBLL.GetAll(parcan, "");
            List<Drecibo> lstrec = DreciboBLL.GetAll(parrec, "");
            List<Dcontable> lstcon = DcontableBLL.GetAll(parcon, "");

            StringBuilder html = new StringBuilder();

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                foreach (Comprobante fac in lstfac)
                {

                    html.AppendFormat("{0:dd/MM/yyyy HH:mm},{1},{2}<br>", fac.com_fecha, fac.com_codigo, fac.com_doctran);

                    List<Ddocumento> docfac = lstdoc.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == fac.com_codigo; });

                    foreach (Ddocumento ddo in docfac)
                    {
                        List<Dcancelacion> canfac = lstcan.FindAll(delegate (Dcancelacion dc) { return dc.dca_comprobante == ddo.ddo_comprobante && dc.dca_transacc == ddo.ddo_transacc  && dc.dca_doctran== ddo.ddo_doctran && dc.dca_pago == ddo.ddo_pago; });
                        foreach (Dcancelacion dca in canfac)
                        {
                            Drecibo drec = lstrec.Find(delegate (Drecibo r) { return r.dfp_comprobante == dca.dca_comprobante_can; });
                            DreciboBLL.Delete(transaction, drec);

                            html.AppendFormat("{0:dd/MM/yyyy HH:mm},{1},{2},{3},{4},{5},{6}<br>", fac.com_fecha, fac.com_codigo, fac.com_doctran, drec.dfp_comprobante, drec.dfp_tipopagonombre, drec.dfp_monto, dca.dca_monto);

                            List<Dcontable> dcorec = lstcon.FindAll(delegate (Dcontable c) { return c.dco_comprobante == dca.dca_comprobante_can; });
                            foreach (Dcontable dco in dcorec)
                            {
                                DcontableBLL.Delete(transaction, dco);
                            }

                            DcancelacionBLL.Delete(transaction, dca);

                            Total tot = new Total();
                            tot.tot_empresa = dca.dca_empresa;
                            tot.tot_comprobante = dca.dca_comprobante_can;
                            TotalBLL.Delete(transaction, tot);

                            Comprobante rec = new Comprobante();
                            rec.com_empresa = dca.dca_empresa;
                            rec.com_codigo = dca.dca_comprobante_can;
                            ComprobanteBLL.Delete(transaction, rec);

                            
                            ddo.ddo_cancela = ddo.ddo_cancela - dca.dca_monto;
                            if (ddo.ddo_cancela < ddo.ddo_monto)
                            {
                                ddo.ddo_cancelado = 0;
                                if (ddo.ddo_cancela < 0)
                                    ddo.ddo_cancela = 0;
                            }
                        }

                        ddo.ddo_empresa_key = ddo.ddo_empresa;
                        ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                        ddo.ddo_transacc_key = ddo.ddo_transacc;
                        ddo.ddo_doctran_key = ddo.ddo_doctran;
                        ddo.ddo_pago_key = ddo.ddo_pago;
                        DdocumentoBLL.Update(transaction, ddo);

                    }
                    


                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


            return html.ToString();
            






        }



        public static List<vComprobante> GetComprobantes(vComprobante obj, Usuario usr, int tipodocfac, int tipodocgui, string detalle, int limit, int offset)
        {

            bool vacio = true;
            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();
            if (!string.IsNullOrEmpty(obj.crea_usr))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " comprobante.crea_usr = {" + contador + "} ";
                valores.Add(obj.crea_usr);
                contador++;
            }

            if (obj.periodo.HasValue)
            {    
                if (obj.periodo.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_periodo = {" + contador + "} ";
                    valores.Add(obj.periodo.Value);
                    contador++;
                    vacio = false;
                }
            }
            if (obj.mes.HasValue)
            {
                if (obj.mes.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_mes = {" + contador + "} ";
                    valores.Add(obj.mes);
                    contador++;
                    vacio = false;

                }
            }
            if (obj.ctipocom.HasValue)
            {
                if (obj.ctipocom.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_ctipocom = {" + contador + "} ";
                    valores.Add(obj.ctipocom);
                    contador++;
                    vacio = false;
                }
            }
            if (obj.almacen.HasValue)
            {
                if (obj.almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_almacen = {" + contador + "} ";
                    valores.Add(obj.almacen);
                    contador++;
                    vacio = false;
                }
            }
            if (obj.pventa.HasValue)
            {
                if (obj.pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_pventa = {" + contador + "} ";
                    valores.Add(obj.pventa);
                    contador++;
                    vacio = false;
                }
            }
            if (obj.numero.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_numero = {" + contador + "} ";
                valores.Add(obj.numero);
                contador++;
                vacio = false;
            }
            if (obj.estado.HasValue)
            {
                if (obj.estado.Value > -1)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " com_estado = {" + contador + "} ";
                    valores.Add(obj.estado);
                    contador++;
                    vacio = false;
                }

            }
            if (obj.tipodoc.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_tipodoc = {" + contador + "} ";
                valores.Add(obj.tipodoc);
                contador++;
                vacio = false;
            }


            if (!string.IsNullOrEmpty(obj.politica))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " pol_nombre LIKE {" + contador + "} ";
                valores.Add("%" + obj.politica + "%");
                contador++;
                vacio = false;
            }

            if (!string.IsNullOrEmpty(obj.concepto))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_concepto like {" + contador + "} ";
                valores.Add("%" + obj.concepto + "%");
                contador++;
            }
          
            if (obj.fecha.Value > DateTime.MinValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + contador + "} ";
                valores.Add(obj.fecha);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(obj.fecha.Value.AddDays(1));
                contador++;
                vacio = false;

            }

            if (!string.IsNullOrEmpty(obj.nombres))//CLIENTE O PROVEEDOR
            {

                string[] arraynombres = obj.nombres.Split(' ');
                for (int i = 0; i < arraynombres.Length; i++)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "} or dban_beneficiario ILIKE{" + contador + "}) ";
                    valores.Add("%" + arraynombres[i] + "%");
                    contador++;

                }

                //vacio = false;
            }

         

            if (!string.IsNullOrEmpty(obj.estadoenvio))//ESTADO ENVIO
            {
                //if (obj.estadoenvio == "1") //POR COBRAR
                //{
                //    parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) > SUM(ddo_cancela) and c.com_tipodoc= " + tipodocfac;
                //}
                if (obj.estadoenvio == "2") //POR DESPACHAR
                {
                    //parametros.where += ((parametros.where != "") ? " and " : "") + " SUM(ddo_monto) = SUM(ddo_cancela) and cenv_despachado_ret IS NULL";
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cenv_despachado_ret IS NULL";
                }
                if (obj.estadoenvio == "3")//DESPACHADOS
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cenv_despachado_ret = 1 ";
                }
            }

            if (!string.IsNullOrEmpty(obj.nombres_rem)) //REMITENTE
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (cenv_ciruc_rem ILIKE {" + contador + "} or  cenv_nombres_rem ILIKE {" + contador + "} or cenv_apellidos_rem ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_rem + "%");
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres_des)) //DESTINATARIO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (cenv_ciruc_des ILIKE {" + contador + "} or cenv_nombres_des ILIKE {" + contador + "} or cenv_apellidos_des ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_des + "%");
                contador++;
            }
            if (!string.IsNullOrEmpty(obj.nombres_soc)) //SOCIO
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " (p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "}) ";
                valores.Add("%" + obj.nombres_soc + "%");
                contador++;
            }
            if ((obj.socio ?? 0) > 0)
            {
                parametros.where += " and cenv_socio= " + obj.socio;
            }

            if (!string.IsNullOrEmpty(obj.placa)) //VEHICULO
            {

                parametros.where += ((parametros.where != "") ? " and " : "") + " (cenv_placa ILIKE {" + contador + "} or cenv_disco ILIKE {" + contador + "}) ";
                valores.Add("%" + obj.placa + "%");
                contador++;
            }


            if (!string.IsNullOrEmpty(usr.usr_id))
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                valores.Add(usr.usr_id);
                contador++;
            }


            parametros.where += ((parametros.where != "") ? " and " : "") + " com_empresa = " + obj.empresa + " ";



            string wheredocs = "";
            foreach (object item in obj.tipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " com_tipodoc = " + item.ToString();
                    vacio = false;
                }
            }

            string wheredetalles = "";
            if (!string.IsNullOrEmpty(detalle))
            {
                string[] arraydetalle = detalle.Split(',');
                for (int i = 0; i < arraydetalle.Length; i++)
                {
                    if (arraydetalle[i] != "")
                    {
                        wheredetalles += ((wheredetalles != "") ? " or " : "") + " ddoc_observaciones ilike '%" + arraydetalle[i] + "%'  ";
                        // wheredetalles += ((wheredetalles != "") ? " or " : "") + " c.com_codigo IN (select ddoc_comprobante from dcomdoc where ddoc_observaciones ilike '%" + arraydetalle[i] + "%')  ";
                        vacio = false;
                    }
                }
            }



            //if (usr.usr_id == "admin")
            //{
            //    HtmlRow fr = new HtmlRow();
            //    string sqlwhere = parametros.where;
            //    for (int i = 0; i < parametros.valores.Length; i++)
            //    {
            //        sqlwhere = sqlwhere.Replace("{" + i.ToString() + "}", parametros.valores[i].ToString());
            //    }

            //    fr.cells.Add(new HtmlCell { valor = sqlwhere, colspan = 8, clase = "oculto" });
            //    html.AppendLine(fr.ToString());
            //    //html.AppendFormat("<div id='where' style='display:none'>{0}</div>",parametros.where);
            //}



            if (vacio)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_fecha between {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(-30).Date);
                contador++;
                parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(1).Date);
                contador++;
                //parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = " + obj.empresa + " ";


            }



            parametros.where += (wheredocs != "") ? " and (" + wheredocs + ")" : "";
            parametros.where += (wheredetalles != "") ? "  and  com_codigo IN (select ddoc_comprobante from dcomdoc where " + wheredetalles + ")" : "";
            parametros.valores = valores.ToArray();



            List<vComprobante> lstcom = vComprobanteBLL.GetAllRange(parametros, "com_fecha desc", limit, offset);

            if (lstcom.Count > 0)
            {

                string[] wherein = lstcom.Select(s => s.codigo.ToString()).ToArray();

                //Carga ccomdoc
                WhereParams where = new WhereParams();
                //where.where = "cdoc_empresa=" + obj.empresa;
                //where.where += " and cdoc_comprobante in (" + string.Join(",", wherein) + ")";
                //List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll(where, "");

                //Carga ddocumento
                where = new WhereParams();
                where.where = "ddo_empresa=" + obj.empresa;
                where.where += " and ddo_comprobante in (" + string.Join(",", wherein) + ")";
                List<Ddocumento> lstddo = DdocumentoBLL.GetAll(where, "");

                //Carga cancelaciones
                where = new WhereParams();
                where.where = "dca_empresa=" + obj.empresa;
                where.where += " and dca_comprobante in (" + string.Join(",", wherein) + ")";
                List<Dcancelacion> lstdca = DcancelacionBLL.GetAll(where, "");



                //Carga comprobanteruta
                where = new WhereParams();
                where.where = "rfac_empresa=" + obj.empresa;
                where.where += " and rfac_comprobantefac in (" + string.Join(",", wherein) + ")";
                List<Rutaxfactura> lstrxf = RutaxfacturaBLL.GetAll(where, "");

                ////Carga total
                //where = new WhereParams();
                //where.where = "tot_empresa=" + obj.empresa;
                //where.where += " and tot_comprobante in (" + string.Join(",", wherein) + ")";
                //List<Total> lsttot = TotalBLL.GetAll(where, "");





                foreach (vComprobante item in lstcom)
                {
                    //Ccomdoc ccomdoc = lstcdoc.Find(delegate (Ccomdoc c) { return c.cdoc_comprobante == item.codigo; });
                    //Ccomenv ccomenv = lstcenv.Find(delegate (Ccomenv c) { return c.cenv_comprobante == item.codigo; });
                    //Total total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == item.codigo; });
                    Rutaxfactura rfac = lstrxf.Find(delegate (Rutaxfactura r) { return r.rfac_comprobantefac == item.codigo; });

                    item.monto = lstddo.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == item.codigo; }).Sum(s => s.ddo_monto);
                    item.cancela = lstdca.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == item.codigo; }).Sum(s => s.dca_monto);

                    //if (ccomenv != null)
                    //{
                    //    item.remitente = ccomenv.cenv_remitente;
                    //    item.ciruc_rem = ccomenv.cenv_ciruc_rem;
                    //    item.nombres_rem = ccomenv.cenv_nombres_rem;

                    //    item.destinatario = ccomenv.cenv_destinatario;
                    //    item.ciruc_des = ccomenv.cenv_ciruc_des;
                    //    item.nombres_des = ccomenv.cenv_nombres_des;

                    //    item.vehiculo = ccomenv.cenv_vehiculo;
                    //    item.placa = ccomenv.cenv_placa;
                    //    item.disco = ccomenv.cenv_disco;

                    //    item.socio = ccomenv.cenv_socio;
                    //    item.nombres_soc = ccomenv.cenv_nombres_soc;
                    //    //item.nom_soc = reader["socionom"].ToString();
                    //    //item.ape_soc = reader["socioape"].ToString();

                    //    item.ruta = ccomenv.cenv_ruta;
                    //}

                    //if (ccomdoc != null)
                    //{


                    //    item.nfactura = ccomdoc.cdoc_aut_factura;
                    //    item.factura = ccomdoc.cdoc_factura;
                    //}

                    if (rfac!=null)
                    {
                        item.hojaruta = rfac.rfac_comprobanterutadoctran;
                    }

                    //if (total != null)
                    //{
                    //    item.total = total.tot_total;
                    //    item.subtotal = total.tot_subtot_0;
                    //    item.subimpuesto = total.tot_subtotal;
                    //    item.tseguro = total.tot_tseguro;
                    //    item.porc_seguro = total.tot_porc_seguro;
                    //    item.valordeclarado = total.tot_vseguro;
                    //    item.impuesto = total.tot_timpuesto;
                    //    item.transporte = total.tot_transporte;

                    //    item.desc = total.tot_desc1_0;
                    //    item.desc1 = total.tot_desc2_0;
                    //    item.ice = total.tot_ice;
                    //}



                    //
                    
                    //this.despachado = (reader["cenv_despachado_ret"] != DBNull.Value) ? (Int32?)reader["cenv_despachado_ret"] : null;
                    //this.cancelado = (reader["cancelado"] != DBNull.Value) ? (Int64?)reader["cancelado"] : null;
                    //this.monto = (reader["monto"] != DBNull.Value) ? (Decimal?)reader["monto"] : null;
                    //this.cancela = (reader["cancela"] != DBNull.Value) ? (Decimal?)reader["cancela"] : null;

                    //this.idpolitica = reader["pol_id"].ToString();
                    //this.politica = reader["pol_nombre"].ToString();

                    //this.seguro = (reader["seguro"] != DBNull.Value) ? (Decimal?)reader["seguro"] : null;


                    //this.beneficiario = (reader["dban_beneficiario"] != DBNull.Value) ? (string)reader["dban_beneficiario"] : null;
                    //this.montoban = (reader["dban_valor_nac"] != DBNull.Value) ? (Decimal?)reader["dban_valor_nac"] : null;
                    

                    //this.debito = (reader["debito"] != DBNull.Value) ? (Decimal?)reader["debito"] : null;
                    //this.credito = (reader["credito"] != DBNull.Value) ? (Decimal?)reader["credito"] : null;
                }


            }


            return lstcom;


        }

        public static List<vComprobante> GetComprobantesRet(vComprobante obj, Usuario usr, DateTime? desde, DateTime? hasta, int? estadocobro, int limit, int offset)
        {

            bool vacio = true;
            int contador = 0;
            WhereParams parametros = new WhereParams();
            parametros.where = " comprobante.com_empresa= " + obj.empresa;
            List<object> valores = new List<object>();
            //if (!string.IsNullOrEmpty(obj.crea_usr))
            //{
            //    parametros.where +=  " and comprobante.crea_usr = {" + contador + "} ";
            //    valores.Add(obj.crea_usr);
            //    contador++;
            //}

            if ((obj.periodo??0)>0)
            {
                    parametros.where += " and com_periodo = {" + contador + "} ";
                    valores.Add(obj.periodo.Value);
                    contador++;
                    vacio = false;
            }
            if ((obj.mes ?? 0) > 0)
            {
                parametros.where += " and com_mes = {" + contador + "} ";
                valores.Add(obj.mes);
                contador++;
                vacio = false;

            }
            //if (obj.ctipocom.HasValue)
            //{
            //    if (obj.ctipocom.Value > 0)
            //    {
            //        parametros.where += ((parametros.where != "") ? " and " : "") + " com_ctipocom = {" + contador + "} ";
            //        valores.Add(obj.ctipocom);
            //        contador++;
            //        vacio = false;
            //    }
            //}
            if ((obj.almacen??0)>0)
            {
                    parametros.where += " and com_almacen = {" + contador + "} ";
                    valores.Add(obj.almacen);
                    contador++;
                    vacio = false;
            }
            if ((obj.pventa??0)>0)
            {
                    parametros.where += " and com_pventa = {" + contador + "} ";
                    valores.Add(obj.pventa);
                    contador++;
                    vacio = false;
            }
            if ((obj.numero??0)>0)
            {
                parametros.where +=  " and com_numero = {" + contador + "} ";
                valores.Add(obj.numero);
                contador++;
                vacio = false;
            }
            if (obj.estado.HasValue)
            {
                if (obj.estado.Value > -1)
                {
                    parametros.where += " and com_estado = {" + contador + "} ";
                    valores.Add(obj.estado);
                    contador++;
                    vacio = false;
                }

            }
           


            if (!string.IsNullOrEmpty(obj.politica))
            {
                parametros.where += " and pol_nombre LIKE {" + contador + "} ";
                valores.Add("%" + obj.politica + "%");
                contador++;
                vacio = false;
            }

            //if (!string.IsNullOrEmpty(obj.concepto))
            //{
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " com_concepto like {" + contador + "} ";
            //    valores.Add("%" + obj.concepto + "%");
            //    contador++;
            //}

            if (desde.HasValue)
            {
                parametros.where += " and com_fecha >={" + contador + "} ";
                valores.Add(desde);
                contador++;
            }
            if (hasta.HasValue)
            {
                parametros.where += " and com_fecha <={" + contador + "} ";
                valores.Add(hasta);
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres))//CLIENTE O PROVEEDOR
            {

                string[] arraynombres = obj.nombres.Split(' ');
                for (int i = 0; i < arraynombres.Length; i++)
                {
                    parametros.where +=  " and (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "} or dban_beneficiario ILIKE{" + contador + "}) ";
                    valores.Add("%" + arraynombres[i] + "%");
                    contador++;
                }

                //vacio = false;
            }

            if ((obj.socio ?? 0) > 0)
            {
                parametros.where += " and cenv_socio= " + obj.socio;
            }


            if (!string.IsNullOrEmpty(usr.usr_id))
            {
                parametros.where +=  " and com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                valores.Add(usr.usr_id);
                contador++;
            }
            



            string wheredocs = "";
            foreach (object item in obj.tipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " com_tipodoc = " + item.ToString();
                    vacio = false;
                }
            }

           


            //if (usr.usr_id == "admin")
            //{
            //    HtmlRow fr = new HtmlRow();
            //    string sqlwhere = parametros.where;
            //    for (int i = 0; i < parametros.valores.Length; i++)
            //    {
            //        sqlwhere = sqlwhere.Replace("{" + i.ToString() + "}", parametros.valores[i].ToString());
            //    }

            //    fr.cells.Add(new HtmlCell { valor = sqlwhere, colspan = 8, clase = "oculto" });
            //    html.AppendLine(fr.ToString());
            //    //html.AppendFormat("<div id='where' style='display:none'>{0}</div>",parametros.where);
            //}



            if (vacio)
            {
                parametros.where += " and com_fecha between {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(-30).Date);
                contador++;
                parametros.where += " and  {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(1).Date);
                contador++;
                //parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = " + obj.empresa + " ";


            }



            parametros.where += (wheredocs != "") ? " and (" + wheredocs + ")" : "";            
            parametros.valores = valores.ToArray();


            List<vComprobante> lstcom = vComprobanteBLL.GetAllRange(parametros, "com_fecha desc", limit, offset);

            if (lstcom.Count > 0)
            {

                string[] wherein = lstcom.Select(s => s.codigo.ToString()).ToArray();

                //Carga ccomdoc
                WhereParams where = new WhereParams();
                //where.where = "cdoc_empresa=" + obj.empresa;
                //where.where += " and cdoc_comprobante in (" + string.Join(",", wherein) + ")";
                //List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll(where, "");

                //Carga ddocumento
                where = new WhereParams();
                where.where = "ddo_empresa=" + obj.empresa;
                where.where += " and ddo_comprobante in (" + string.Join(",", wherein) + ")";
                List<Ddocumento> lstddo = DdocumentoBLL.GetAll(where, "");

                //Carga cancelaciones
                where = new WhereParams();
                where.where = "dca_empresa=" + obj.empresa;
                where.where += " and dca_comprobante in (" + string.Join(",", wherein) + ")";
                List<Dcancelacion> lstdca = DcancelacionBLL.GetAll(where, "");


                string[] whereindca = lstdca.Select(s => s.dca_comprobante_can.ToString()).ToArray();

                //Carga detalle de recibos
                where = new WhereParams();
                where.where = "dfp_empresa=" + obj.empresa;
                where.where += " and dfp_comprobante in (" + string.Join(",", whereindca) + ")";
                List<Drecibo> lstdfp = DreciboBLL.GetAll(where, "");



                //Carga comprobanteruta
                where = new WhereParams();
                where.where = "rfac_empresa=" + obj.empresa;
                where.where += " and rfac_comprobantefac in (" + string.Join(",", wherein) + ")";
                List<Rutaxfactura> lstrxf = RutaxfacturaBLL.GetAll(where, "");

                ////Carga total
                //where = new WhereParams();
                //where.where = "tot_empresa=" + obj.empresa;
                //where.where += " and tot_comprobante in (" + string.Join(",", wherein) + ")";
                //List<Total> lsttot = TotalBLL.GetAll(where, "");





                foreach (vComprobante item in lstcom)
                {
                    //Ccomdoc ccomdoc = lstcdoc.Find(delegate (Ccomdoc c) { return c.cdoc_comprobante == item.codigo; });
                    //Ccomenv ccomenv = lstcenv.Find(delegate (Ccomenv c) { return c.cenv_comprobante == item.codigo; });
                    //Total total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == item.codigo; });
                    Rutaxfactura rfac = lstrxf.Find(delegate (Rutaxfactura r) { return r.rfac_comprobantefac == item.codigo; });

                    item.monto = lstddo.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == item.codigo; }).Sum(s => s.ddo_monto);

                    List<Dcancelacion> lstdcaitem = lstdca.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == item.codigo; });
                    item.cancela = lstdcaitem.Sum(s => s.dca_monto);
                    foreach (Dcancelacion dca in lstdcaitem)
                    {
                        item.retiva = (item.retiva ?? 0) + lstdfp.FindAll(delegate (Drecibo r) { return r.dfp_comprobante == dca.dca_comprobante_can; }).Where(w => (w.dfp_tipopagoiva ?? 0) == 1).Sum(s => s.dfp_monto);
                        item.retren= (item.retren?? 0) + lstdfp.FindAll(delegate (Drecibo r) { return r.dfp_comprobante == dca.dca_comprobante_can; }).Where(w => (w.dfp_tipopagoret ?? 0) == 1).Sum(s => s.dfp_monto);
                    }



                    

                   

                    if (rfac != null)
                    {
                        item.hojaruta = rfac.rfac_comprobanterutadoctran;
                    }

                }


                if ((estadocobro ?? 0) == 1) //pendiente
                    return lstcom.Where(w => ((w.monto ?? 0) - (w.cancela ?? 0)) > 0).ToList();
                if ((estadocobro ?? 0) == 2) //pagado
                    return lstcom.Where(w => ((w.monto ?? 0) - (w.cancela ?? 0)) == 0).ToList();
            }


            return lstcom;


        }

        public static Comprobante GetComprobante(int empresa, long codigo)
        {

            Comprobante com = new Comprobante();
            com.com_empresa = empresa;
            com.com_codigo = codigo;
            com.com_empresa_key = empresa;
            com.com_codigo_key = codigo;
            com = ComprobanteBLL.GetByPK(com);

            com.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = empresa, cdoc_empresa_key = empresa, cdoc_comprobante = codigo, cdoc_comprobante_key = codigo });
            com.ccomenv = CcomenvBLL.GetByPK(new Ccomenv { cenv_empresa = empresa, cenv_empresa_key = empresa, cenv_comprobante = codigo, cenv_comprobante_key = codigo });
            com.total= TotalBLL.GetByPK(new Total { tot_empresa = empresa, tot_empresa_key = empresa, tot_comprobante = codigo, tot_comprobante_key = codigo });

            com.documentos = DdocumentoBLL.GetAll("ddo_empresa= "+empresa+" and ddo_comprobante =" + codigo, "");
            com.cancelaciones = DcancelacionBLL.GetAll("dca_empresa=" + empresa + " and dca_comprobante=" + codigo,"");
            if (com.cancelaciones.Count > 0)
            {
                string[] whereindca = com.cancelaciones.Select(s => s.dca_comprobante_can.ToString()).ToArray();
                com.recibos = DreciboBLL.GetAll("dfp_empresa=" + empresa + " and dfp_comprobante in (" + string.Join(",", whereindca) + ")", "");
            }
                        

            return com;

        }


        public static List<vComprobante> GetComprobantesCobroSocio(vComprobante obj, Usuario usr, DateTime? desde, DateTime? hasta, long? hojaruta, int? estadocobro, int limit, int offset)
        {

            bool vacio = true;
            int contador = 0;
            WhereParams parametros = new WhereParams();
            parametros.where = " comprobante.com_empresa= " + obj.empresa;
            List<object> valores = new List<object>();
            //if (!string.IsNullOrEmpty(obj.crea_usr))
            //{
            //    parametros.where +=  " and comprobante.crea_usr = {" + contador + "} ";
            //    valores.Add(obj.crea_usr);
            //    contador++;
            //}

            if ((obj.periodo ?? 0) > 0)
            {
                parametros.where += " and com_periodo = {" + contador + "} ";
                valores.Add(obj.periodo.Value);
                contador++;
                vacio = false;
            }
            if ((obj.mes ?? 0) > 0)
            {
                parametros.where += " and com_mes = {" + contador + "} ";
                valores.Add(obj.mes);
                contador++;
                vacio = false;

            }
          
            if ((obj.almacen ?? 0) > 0)
            {
                parametros.where += " and com_almacen = {" + contador + "} ";
                valores.Add(obj.almacen);
                contador++;
                vacio = false;
            }
            if ((obj.pventa ?? 0) > 0)
            {
                parametros.where += " and com_pventa = {" + contador + "} ";
                valores.Add(obj.pventa);
                contador++;
                vacio = false;
            }
            if ((obj.numero ?? 0) > 0)
            {
                parametros.where += " and com_numero = {" + contador + "} ";
                valores.Add(obj.numero);
                contador++;
                vacio = false;
            }
            if (obj.estado.HasValue)
            {
                if (obj.estado.Value > -1)
                {
                    parametros.where += " and com_estado = {" + contador + "} ";
                    valores.Add(obj.estado);
                    contador++;
                    vacio = false;
                }

            }



            if (!string.IsNullOrEmpty(obj.politica))
            {
                parametros.where += " and pol_nombre LIKE {" + contador + "} ";
                valores.Add("%" + obj.politica + "%");
                contador++;
                vacio = false;
            }

            //if (!string.IsNullOrEmpty(obj.concepto))
            //{
            //    parametros.where += ((parametros.where != "") ? " and " : "") + " com_concepto like {" + contador + "} ";
            //    valores.Add("%" + obj.concepto + "%");
            //    contador++;
            //}

            if (desde.HasValue)
            {
                parametros.where += " and com_fecha >={" + contador + "} ";
                valores.Add(desde);
                contador++;
            }
            if (hasta.HasValue)
            {
                parametros.where += " and com_fecha <={" + contador + "} ";
                valores.Add(hasta);
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres))//CLIENTE O PROVEEDOR
            {

                string[] arraynombres = obj.nombres.Split(' ');
                for (int i = 0; i < arraynombres.Length; i++)
                {
                    parametros.where += " and (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "} or dban_beneficiario ILIKE{" + contador + "}) ";
                    valores.Add("%" + arraynombres[i] + "%");
                    contador++;
                }

                //vacio = false;
            }

            if ((obj.socio ?? 0) > 0)
            {
                //if (agrupado == "HR")
                //    parametros.where += " and com_vehiculo IN (SELECT veh_codigo FROM vehiculo WHERE veh_duenio=" + obj.socio + ")";
                //else
                    parametros.where += " and cenv_socio= " + obj.socio;
            }


            if (!string.IsNullOrEmpty(usr.usr_id))
            {
                parametros.where += " and com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                valores.Add(usr.usr_id);
                contador++;
            }

            if ((hojaruta??0)>0)
            {
                parametros.where += " and com_codigo in (select rfac_comprobantefac from rutaxfactura where rfac_comprobanteruta={" + contador + "})";
                valores.Add(hojaruta);
                contador++;
            }

            string wheredocs = "";
            foreach (object item in obj.tipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " com_tipodoc = " + item.ToString();
                    vacio = false;
                }
            }




            //if (usr.usr_id == "admin")
            //{
            //    HtmlRow fr = new HtmlRow();
            //    string sqlwhere = parametros.where;
            //    for (int i = 0; i < parametros.valores.Length; i++)
            //    {
            //        sqlwhere = sqlwhere.Replace("{" + i.ToString() + "}", parametros.valores[i].ToString());
            //    }

            //    fr.cells.Add(new HtmlCell { valor = sqlwhere, colspan = 8, clase = "oculto" });
            //    html.AppendLine(fr.ToString());
            //    //html.AppendFormat("<div id='where' style='display:none'>{0}</div>",parametros.where);
            //}



            if (vacio)
            {
                parametros.where += " and com_fecha between {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(-30).Date);
                contador++;
                parametros.where += " and  {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(1).Date);
                contador++;
                //parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = " + obj.empresa + " ";


            }



            parametros.where += (wheredocs != "") ? " and (" + wheredocs + ")" : "";
            parametros.valores = valores.ToArray();


            List<vComprobante> lstcom = vComprobanteBLL.GetAllRange(parametros, "com_fecha desc", limit, offset);

            if (lstcom.Count > 0)
            {

                string[] wherein = lstcom.Select(s => s.codigo.ToString()).ToArray();

                //Carga ccomdoc
                WhereParams where = new WhereParams();
                //where.where = "cdoc_empresa=" + obj.empresa;
                //where.where += " and cdoc_comprobante in (" + string.Join(",", wherein) + ")";
                //List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll(where, "");

                //Carga ddocumento
                where = new WhereParams();
                where.where = "ddo_empresa=" + obj.empresa;
                where.where += " and ddo_comprobante in (" + string.Join(",", wherein) + ")";
                List<Ddocumento> lstddo = DdocumentoBLL.GetAll(where, "");

                //Carga cancelaciones
                where = new WhereParams();
                where.where = "dca_empresa=" + obj.empresa;
                where.where += " and dca_comprobante in (" + string.Join(",", wherein) + ")";
                List<Dcancelacion> lstdca = DcancelacionBLL.GetAll(where, "");

                if (lstdca.Count > 0)
                {

                    string[] whereindca = lstdca.Select(s => s.dca_comprobante_can.ToString()).ToArray();

                    //Carga detalle de recibos
                    where = new WhereParams();
                    where.where = "dfp_empresa=" + obj.empresa;
                    where.where += " and dfp_comprobante in (" + string.Join(",", whereindca) + ")";
                    List<Drecibo> lstdfp = DreciboBLL.GetAll(where, "");



                    //Carga comprobanteruta
                    where = new WhereParams();
                    where.where = "rfac_empresa=" + obj.empresa;
                    where.where += " and rfac_comprobantefac in (" + string.Join(",", wherein) + ")";
                    List<Rutaxfactura> lstrxf = RutaxfacturaBLL.GetAll(where, "");

                    ////Carga total
                    //where = new WhereParams();
                    //where.where = "tot_empresa=" + obj.empresa;
                    //where.where += " and tot_comprobante in (" + string.Join(",", wherein) + ")";
                    //List<Total> lsttot = TotalBLL.GetAll(where, "");





                    foreach (vComprobante item in lstcom)
                    {
                        //Ccomdoc ccomdoc = lstcdoc.Find(delegate (Ccomdoc c) { return c.cdoc_comprobante == item.codigo; });
                        //Ccomenv ccomenv = lstcenv.Find(delegate (Ccomenv c) { return c.cenv_comprobante == item.codigo; });
                        //Total total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == item.codigo; });
                        Rutaxfactura rfac = lstrxf.Find(delegate (Rutaxfactura r) { return r.rfac_comprobantefac == item.codigo; });

                        item.monto = lstddo.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == item.codigo; }).Sum(s => s.ddo_monto);

                        List<Dcancelacion> lstdcaitem = lstdca.FindAll(delegate (Dcancelacion d) { return d.dca_comprobante == item.codigo; });
                        item.cancela = lstdcaitem.Sum(s => s.dca_monto);
                        foreach (Dcancelacion dca in lstdcaitem)
                        {
                            item.retiva = (item.retiva ?? 0) + lstdfp.FindAll(delegate (Drecibo r) { return r.dfp_comprobante == dca.dca_comprobante_can; }).Where(w => (w.dfp_tipopagoiva ?? 0) == 1).Sum(s => s.dfp_monto);
                            item.retren = (item.retren ?? 0) + lstdfp.FindAll(delegate (Drecibo r) { return r.dfp_comprobante == dca.dca_comprobante_can; }).Where(w => (w.dfp_tipopagoret ?? 0) == 1).Sum(s => s.dfp_monto);
                        }







                        if (rfac != null)
                        {
                            item.hojaruta = rfac.rfac_comprobanterutadoctran;
                        }

                    }


                    if ((estadocobro ?? 0) == 1) //pendiente
                        return lstcom.Where(w => ((w.monto ?? 0) - (w.cancela ?? 0)) > 0).ToList();
                    if ((estadocobro ?? 0) == 2) //pagado
                        return lstcom.Where(w => ((w.monto ?? 0) - (w.cancela ?? 0)) == 0).ToList();
                }
            }


            return lstcom;


        }

        public static List<vComprobante> GetComprobantesHRSocio(vComprobante obj, Usuario usr, DateTime? desde, DateTime? hasta, int? estadocobro, int limit, int offset)
        {

            bool vacio = true;
            int contador = 0;
            WhereParams parametros = new WhereParams();
            parametros.where = " comprobante.com_empresa= " + obj.empresa;
            List<object> valores = new List<object>();
          
            if ((obj.periodo ?? 0) > 0)
            {
                parametros.where += " and com_periodo = {" + contador + "} ";
                valores.Add(obj.periodo.Value);
                contador++;
                vacio = false;
            }
            if ((obj.mes ?? 0) > 0)
            {
                parametros.where += " and com_mes = {" + contador + "} ";
                valores.Add(obj.mes);
                contador++;
                vacio = false;

            }
          
            if ((obj.almacen ?? 0) > 0)
            {
                parametros.where += " and com_almacen = {" + contador + "} ";
                valores.Add(obj.almacen);
                contador++;
                vacio = false;
            }
            if ((obj.pventa ?? 0) > 0)
            {
                parametros.where += " and com_pventa = {" + contador + "} ";
                valores.Add(obj.pventa);
                contador++;
                vacio = false;
            }
            if ((obj.numero ?? 0) > 0)
            {
                parametros.where += " and com_numero = {" + contador + "} ";
                valores.Add(obj.numero);
                contador++;
                vacio = false;
            }
            if (obj.estado.HasValue)
            {
                if (obj.estado.Value > -1)
                {
                    parametros.where += " and com_estado = {" + contador + "} ";
                    valores.Add(obj.estado);
                    contador++;
                    vacio = false;
                }

            }

            if (desde.HasValue)
            {
                parametros.where += " and com_fecha >={" + contador + "} ";
                valores.Add(desde);
                contador++;
            }
            if (hasta.HasValue)
            {
                parametros.where += " and com_fecha <={" + contador + "} ";
                valores.Add(hasta);
                contador++;
            }

            if (!string.IsNullOrEmpty(obj.nombres))//
            {

                string[] arraynombres = obj.nombres.Split(' ');
                for (int i = 0; i < arraynombres.Length; i++)
                {
                    parametros.where += " and (p.per_ciruc ILIKE {" + contador + "} or p.per_nombres ILIKE {" + contador + "} or p.per_apellidos ILIKE{" + contador + "} or e.cenv_ciruc_rem ILIKE {" + contador + "} or e.cenv_nombres_rem ILIKE {" + contador + "} or e.cenv_apellidos_rem ILIKE{" + contador + "} or e.cenv_ciruc_des ILIKE {" + contador + "} or e.cenv_nombres_des ILIKE {" + contador + "} or e.cenv_apellidos_des ILIKE{" + contador + "} or p1.per_apellidos ILIKE {" + contador + "} or p1.per_nombres ILIKE{" + contador + "} or dban_beneficiario ILIKE{" + contador + "}) ";
                    valores.Add("%" + arraynombres[i] + "%");
                    contador++;
                }

                //vacio = false;
            }

            if ((obj.ruta ?? 0) > 0)
                parametros.where += " and com_ruta=" + obj.ruta;

            if ((obj.socio ?? 0) > 0)
            {
                parametros.where += " and com_vehiculo IN (SELECT veh_codigo FROM vehiculo WHERE veh_duenio=" + obj.socio + ")";
            }
            if ((obj.vehiculo ?? 0) > 0)
                parametros.where += " and com_vehiculo =" + obj.vehiculo;


            if (!string.IsNullOrEmpty(usr.usr_id))
            {
                parametros.where += " and com_tipodoc IN (SELECT udo_tipodoc FROM usrdoc WHERE udo_usuario = {" + contador + "}) ";
                valores.Add(usr.usr_id);
                contador++;
            }



            string wheredocs = "";
            foreach (object item in obj.tipos)
            {
                if (item.ToString() != "")
                {
                    wheredocs += ((wheredocs != "") ? " or " : "") + " com_tipodoc = " + item.ToString();
                    vacio = false;
                }
            }




            if (vacio)
            {
                parametros.where += " and com_fecha between {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(-30).Date);
                contador++;
                parametros.where += " and  {" + contador + "} ";
                valores.Add(DateTime.Now.AddDays(1).Date);
                contador++;
                //parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = " + obj.empresa + " ";


            }



            parametros.where += (wheredocs != "") ? " and (" + wheredocs + ")" : "";
            parametros.valores = valores.ToArray();


            List<vComprobante> lstcom = vComprobanteBLL.GetAllRange(parametros, "com_fecha desc", limit, offset);


            //Funciones para calcular totales cobrados y pendintes de cada hoja de ruta
            if (lstcom.Count > 0)
            {

                List<Ruta> lstrutas = RutaBLL.GetAll("rut_empresa=" + obj.empresa, "");

                string[] wherein = lstcom.Select(s => s.codigo.ToString()).ToArray();

                //Carga ccomdoc
                WhereParams where = new WhereParams();

                //Obtiene todos los comprobantes de las hojas de rutas
                where = new WhereParams();
                where.where = "rfac_empresa=" + obj.empresa;
                where.where += " and rfac_comprobanteruta in (" + string.Join(",", wherein) + ")";
                List<Rutaxfactura> lstrfac = RutaxfacturaBLL.GetAll(where, "");

                if (lstrfac.Count>0)
                {

                    string[] whereincom = lstrfac.Select(s => s.rfac_comprobantefac.ToString()).ToArray();
                    //Carga ddocumento
                    where = new WhereParams();
                    where.where = "com_empresa=" + obj.empresa;
                    where.where += " and com_codigo in (" + string.Join(",", whereincom) + ")";
                    List<Comprobante> lstcomdet = ComprobanteBLL.GetAll(where, "");


                    string[] whereinfac = lstcomdet.Where(w => w.com_tipodoc == 4).Select(s => s.com_codigo.ToString()).ToArray();
                    string[] whereingui = lstcomdet.Where(w => w.com_tipodoc == 13).Select(s => s.com_codigo.ToString()).ToArray();
                    string wherefacgui = "";
                    if (whereinfac.Length > 0)
                        wherefacgui += " (ddo_comprobante in (" + string.Join(",", whereinfac) + ") ";
                    if (whereingui.Length > 0)
                        wherefacgui += (wherefacgui != "" ? " or " : "(") + " ddo_comprobante_guia in (" + string.Join(",", whereingui) + ") ";
                    wherefacgui += ")";

                    //Carga ddocumento
                    where = new WhereParams();
                    where.where = "ddo_empresa=" + obj.empresa;
                    //where.where += " and ddo_comprobante in (" + string.Join(",", whereinfac) + ") or ddo_comprobante_guia in (" + string.Join(",", whereingui) + ")";
                    where.where += " and " + wherefacgui;
                    List<Ddocumento> lstddo = DdocumentoBLL.GetAll(where, "");

                   

                    string[] whereinddo = lstddo.Select(s => s.ddo_comprobante.ToString()).ToArray();

                    //Carga cancelaciones
                    where = new WhereParams();
                    where.where = "dca_empresa=" + obj.empresa;
                    where.where += " and dca_comprobante in (" + string.Join(",", whereinddo) + ")";
                    List<Dcancelacion> lstdca = DcancelacionBLL.GetAll(where, "");

                    //Carga cancelaciones socios
                    where = new WhereParams();
                    where.where = "dcs_empresa=" + obj.empresa;
                    where.where += " and dcs_comprobante in (" + string.Join(",", whereinddo) + ")";
                    List<Dcancelacionsocio> lstdcs = DcancelacionsocioBLL.GetAll(where, "");


                    if (lstdca.Count>0 || lstdcs.Count>0 )
                    {

                        foreach (vComprobante item in lstcom)
                        {
                            decimal? monto = 0;
                            decimal? cancela = 0;
                            decimal? cancelasocio = 0;

                            Ruta ruta = lstrutas.Find(f => f.rut_codigo == item.rutahr);
                            item.nombreruta = ruta.rut_origen + " - " + ruta.rut_destino;
                           List<Rutaxfactura> lsthr = lstrfac.FindAll(f => f.rfac_comprobanteruta == item.codigo);

                            //foreach (Comprobante comdet in lstcomdet)
                            foreach (Rutaxfactura rfac in lsthr)
                            {
                                Comprobante comdet = lstcomdet.Find(f => f.com_codigo == rfac.rfac_comprobantefac);
                                if (comdet.com_tipodoc == 4)
                                {
                                    monto += lstddo.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == comdet.com_codigo; }).Sum(s => s.ddo_monto);
                                    cancela += lstdca.FindAll(f => f.dca_comprobante == comdet.com_codigo).Sum(s => s.dca_monto);
                                    cancelasocio += lstdcs.FindAll(f => f.dcs_comprobante == comdet.com_codigo).Sum(s => s.dcs_monto);
                                }
                                if (comdet.com_tipodoc == 13)
                                {
                                    Ddocumento ddofac = lstddo.Find(f => f.ddo_comprobante_guia == comdet.com_codigo);                                    
                                    monto += lstddo.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante_guia == comdet.com_codigo; }).Sum(s => s.ddo_monto);
                                    if (ddofac != null)
                                    {
                                        cancela += lstdca.FindAll(f => f.dca_comprobante == ddofac.ddo_comprobante && f.dca_pago == ddofac.ddo_pago).Sum(s => s.dca_monto);
                                        cancelasocio += lstdcs.FindAll(f => f.dcs_comprobante == ddofac.ddo_comprobante && f.dcs_pago == ddofac.ddo_pago).Sum(s => s.dcs_monto);
                                    }
                                }
                            }

                            item.cancela = cancela;
                            item.cancelasocio = cancelasocio;
                        }

                    }

                }            
            }
            return lstcom;
        }


        public static List<vComprobante> GetComprobantesPlanillaCliente(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa, string cliente)
        {
            hasta = hasta.AddDays(1).AddSeconds(-1);

            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
            valores.Add(hasta);
            contador++;


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_almacen = {" + contador + "} ";
                    valores.Add(almacen);
                    contador++;
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_pventa = {" + contador + "} ";
                    valores.Add(pventa);
                    contador++;
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_empresa = {" + contador + "} ";
                    valores.Add(empresa);
                    contador++;
                }
            }
            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_tipodoc = {" + contador + "} ";
            valores.Add(Constantes.cPlanillaClientes.tpd_codigo);
            contador++;

            parametros.where += ((parametros.where != "") ? " and " : "") + " c.com_estado  not in ("+Constantes.cEstadoEliminado +")";            

            parametros.valores = valores.ToArray();
            List<vComprobante> lista = vComprobanteBLL.GetAll(parametros, "com_numero");
            return lista;

        }

        public static List<vPlanillaCliente> GetPlanillasClientesDet(DateTime desde, DateTime hasta, int? almacen, int? pventa, int? empresa, string cliente, out List<string> ctas)
        {
            hasta = hasta.AddDays(1).AddSeconds(-1);

            int contador = 0;
            WhereParams parametros = new WhereParams();
            List<object> valores = new List<object>();

            parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_fecha between {" + contador + "} ";
            valores.Add(desde);
            contador++;
            parametros.where += ((parametros.where != "") ? " and " : "") + "   {" + contador + "} ";
            valores.Add(hasta);
            contador++;


            if (almacen.HasValue)
            {
                if (almacen.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_almacen = {" + contador + "} ";
                    valores.Add(almacen);
                    contador++;
                }
            }
            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_pventa = {" + contador + "} ";
                    valores.Add(pventa);
                    contador++;
                }
            }
            if (empresa.HasValue)
            {
                if (empresa.Value > 0)
                {
                    parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_empresa = {" + contador + "} ";
                    valores.Add(empresa);
                    contador++;
                }
            }
            if (!string.IsNullOrEmpty(cliente))
            {
                parametros.where += " and (cabecerapersona.per_ciruc ilike '%" + cliente + "%' or cabecerapersona.per_nombres ilike '%" + cliente + "%' or  cabecerapersona.per_apellidos ilike '%" + cliente + "%' or cabecerapersona.per_razon ilike '%" + cliente + "%') ";

            }
            //parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_tipodoc = {" + contador + "} ";
            //valores.Add(Constantes.cPlanillaClientes.tpd_codigo);
            //contador++;

            parametros.where += ((parametros.where != "") ? " and " : "") + " cabecera.com_estado  not in (" + Constantes.cEstadoEliminado + ")";

            parametros.valores = valores.ToArray();

            List<vPlanillaCliente> planillas = vPlanillaClienteBLL.GetAll(parametros, "cabecera.com_numero");
            List<vPlanillaCliente> planillasres = new List<vPlanillaCliente>();

            List<long?> facturas = planillas.Where(w => w.factura != null).GroupBy(g => g.factura).Select(g => g.Key).ToList();
            List<long?> facturas1 = planillas.Where(w => w.factura == null).GroupBy(g => g.detalle_codigo).Select(g => g.Key).ToList();


            facturas.AddRange(facturas1);
            string wherein = string.Join(",", facturas);

            List<Dcontable> contables = DcontableBLL.GetAll("dco_ddo_comproba in (" + wherein + ") and dco_debcre=2 and com_estado=2 ", "");

            List<long> cancelaciones = contables.GroupBy(g=>g.dco_comprobante).Select(s => s.Key).ToList();
            string whereincan = string.Join(",", cancelaciones);
            List<Dcontable> contablescan = DcontableBLL.GetAll("dco_comprobante in (" + whereincan + ") and dco_debcre=1 and com_estado=2 ", "");

            List<int> cuentasint = contablescan.Where(w=>w.dco_cuenta>0).GroupBy(g => g.dco_cuenta).Select(g => g.Key).ToList();
            List<string> cuentas = contablescan.Where(w => w.dco_cuenta > 0).GroupBy(g => g.dco_cuentaid + " " + g.dco_cuentanombre).Select(g => g.Key).ToList();




            List<long?> codigosplanillas = planillas.GroupBy(g => g.cabecera_codigo).Select(g => g.Key).ToList();

            foreach (var codpla in codigosplanillas)
            {

                vPlanillaCliente itemres  = new vPlanillaCliente();
                itemres.cabecera_codigo = codpla;                              
                List<vPlanillaCliente> lst = planillas.FindAll(f=>f.cabecera_codigo == codpla);
                if (lst.Count>0)
                {
                    itemres.cabecera_doctran = lst[0].cabecera_doctran;
                    itemres.cabecera_fecha = lst[0].cabecera_fecha;
                    itemres.cabecera_codclipro = lst[0].cabecera_codclipro;
                    itemres.cabecera_razon = lst[0].cabecera_razon;
                    itemres.cabecera_total = lst[0].cabecera_total;
                    itemres.factura = lst[0].factura;
                }
                itemres.detalle_subtot_0 = lst.Sum(s => s.detalle_subtot_0);
                itemres.detalle_transporte = lst.Sum(s => s.detalle_transporte);
                itemres.detalle_subtotal = lst.Sum(s => s.detalle_subtotal);
                itemres.detalle_seguro = lst.Sum(s => s.detalle_seguro);
                itemres.detalle_iva = lst.Sum(s => s.detalle_iva);
                itemres.detalle_total = lst.Sum(s => s.detalle_total);

                itemres.cancelado = 0;
                itemres.cancela1 = 0;
                itemres.cancela2 = 0;
                itemres.cancela3 = 0;
                itemres.cancela4 = 0;
                itemres.cancela5 = 0;
                itemres.cancela6 = 0;
                itemres.cancela7 = 0;
                itemres.cancela8 = 0;
                itemres.cancela9 = 0;
                itemres.cancela10 = 0;

                List<Dcontable> faccon = new List<Dcontable>();
                if (itemres.factura.HasValue)
                {
                    faccon = contables.FindAll(f => f.dco_ddo_comproba == itemres.factura.Value);//GUIAS
                    itemres = GetDetallesCancelacion(faccon, itemres, contablescan, cuentasint);
                }
                else
                {
                    List<long?> codigosfacturas = lst.Select(g => g.detalle_codigo).ToList();
                    foreach (var cf in codigosfacturas)
                    {
                        faccon = contables.FindAll(f => f.dco_ddo_comproba == cf);//GUIAS
                        itemres = GetDetallesCancelacion(faccon, itemres, contablescan, cuentasint);
                    }
                    //faccon = contables.FindAll(f => codigosfacturas.Contains(f.dco_ddo_comproba));//FACTURAS
                }
                itemres.saldo = itemres.cabecera_total - itemres.cancelado;

                /*itemres.cancelado = faccon.Sum(s => s.dco_valor_nac);
                itemres.saldo = itemres.cabecera_total - itemres.cancelado;
                itemres.cancela1 = 0;
                itemres.cancela2 = 0;
                itemres.cancela3 = 0;
                itemres.cancela4 = 0;
                itemres.cancela5 = 0;
                itemres.cancela6 = 0;
                itemres.cancela7 = 0;
                itemres.cancela8 = 0;
                itemres.cancela9 = 0;
                itemres.cancela10 = 0;
                List<long> codcan = faccon.GroupBy(g => g.dco_comprobante).Select(s => s.Key).ToList();
                for (int c = 0; c < codcan.Count(); c++)
                {
                    List<Dcontable> detcon = contablescan.FindAll(f => f.dco_comprobante == codcan[c]);
                    for (int i = 0; i < cuentasint.Count; i++)
                    {
                        var valorcan = detcon.Where(w => w.dco_cuenta == cuentasint[i]).Sum(s => s.dco_valor_nac);
                        if (valorcan > 0)
                        {
                            if (i == 0)
                                //item.cancela1 = valorcan;
                                itemres.cancela1 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            if (i == 1)
                                //item.cancela2 = valorcan;
                                itemres.cancela2 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            if (i == 2)
                                //item.cancela3 = valorcan;
                                itemres.cancela3 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            if (i == 3)
                                itemres.cancela4 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            //item.cancela4 = valorcan;
                            if (i == 4)
                                itemres.cancela5 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            //item.cancela5 = valorcan;
                            if (i == 5)
                                itemres.cancela6 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            //item.cancela6 = valorcan;
                            if (i == 6)
                                itemres.cancela7 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            //item.cancela7 = valorcan;

                            if (i == 7)
                                itemres.cancela8 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            //item.cancela8 = valorcan;
                            if (i == 8)
                                itemres.cancela9 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            //item.cancela9 = valorcan;
                            if (i >= 9)
                                itemres.cancela10 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                            //item.cancela10 = valorcan;
                        }
                    }
                }
                */
                planillasres.Add(itemres);
            }




            ctas = cuentas;
            return planillasres;
        }

        public static vPlanillaCliente GetDetallesCancelacion(List<Dcontable> faccon, vPlanillaCliente itemres, List<Dcontable> contablescan, List<int> cuentasint)
        {
            itemres.cancelado += faccon.Sum(s => s.dco_valor_nac);
            /*itemres.saldo = itemres.cabecera_total - itemres.cancelado;
            itemres.cancela1 = 0;
            itemres.cancela2 = 0;
            itemres.cancela3 = 0;
            itemres.cancela4 = 0;
            itemres.cancela5 = 0;
            itemres.cancela6 = 0;
            itemres.cancela7 = 0;
            itemres.cancela8 = 0;
            itemres.cancela9 = 0;
            itemres.cancela10 = 0;*/
            List<long> codcan = faccon.GroupBy(g => g.dco_comprobante).Select(s => s.Key).ToList();
            for (int c = 0; c < codcan.Count(); c++)
            {
                List<Dcontable> detcon = contablescan.FindAll(f => f.dco_comprobante == codcan[c]);
                for (int i = 0; i < cuentasint.Count; i++)
                {
                    var valorcan = detcon.Where(w => w.dco_cuenta == cuentasint[i]).Sum(s => s.dco_valor_nac);
                    if (valorcan > 0)
                    {
                        if (i == 0)
                            //item.cancela1 = valorcan;
                            itemres.cancela1 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        if (i == 1)
                            //item.cancela2 = valorcan;
                            itemres.cancela2 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        if (i == 2)
                            //item.cancela3 = valorcan;
                            itemres.cancela3 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        if (i == 3)
                            itemres.cancela4 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        //item.cancela4 = valorcan;
                        if (i == 4)
                            itemres.cancela5 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        //item.cancela5 = valorcan;
                        if (i == 5)
                            itemres.cancela6 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        //item.cancela6 = valorcan;
                        if (i == 6)
                            itemres.cancela7 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        //item.cancela7 = valorcan;

                        if (i == 7)
                            itemres.cancela8 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        //item.cancela8 = valorcan;
                        if (i == 8)
                            itemres.cancela9 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        //item.cancela9 = valorcan;
                        if (i >= 9)
                            itemres.cancela10 += faccon.Where(w => w.dco_comprobante == codcan[c]).Sum(s => s.dco_valor_nac);
                        //item.cancela10 = valorcan;
                    }
                }
            }
            return itemres;
        }

        public static List<vHojadeRuta> GetComprobantesHRDetSocio(int? empresa, long? codigo, int?numero , int? estadocobro)
        {

            bool vacio = true;
            int contador = 0;
            WhereParams parametros = new WhereParams();
            parametros.where = " cabecera.com_empresa= " + empresa + " and cabecera.com_codigo="+codigo;
            List<object> valores = new List<object>();

            if ((numero ?? 0) > 0)
            {
                parametros.where += " and detalle.com_numero = "+ numero;
            }
           


            parametros.valores = valores.ToArray();
            List<vHojadeRuta> lst = vHojadeRutaBLL.GetAll(parametros, "cabecera.com_fecha");


            //Funciones para calcular totales cobrados y pendintes de cada hoja de ruta
            if (lst.Count > 0)
            {


                //Carga ccomdoc
                WhereParams where = new WhereParams();


                string[] whereincom = lst.Select(s => s.codigodetalle.ToString()).ToArray();
                //Carga ddocumento
                where = new WhereParams();
                where.where = "com_empresa=" + empresa;
                where.where += " and com_codigo in (" + string.Join(",", whereincom) + ")";
                List<Comprobante> lstcomdet = ComprobanteBLL.GetAll(where, "");

                string[] whereinfac = lstcomdet.Where(w => w.com_tipodoc == 4).Select(s => s.com_codigo.ToString()).ToArray();
                string[] whereingui = lstcomdet.Where(w => w.com_tipodoc == 13).Select(s => s.com_codigo.ToString()).ToArray();
                string wherefacgui = "";
                if (whereinfac.Length > 0)
                    wherefacgui += " (ddo_comprobante in (" + string.Join(",", whereinfac) + ") ";
                if (whereingui.Length > 0)
                    wherefacgui += (wherefacgui != "" ? " or " : "(") + " ddo_comprobante_guia in (" + string.Join(",", whereingui) + ") ";
                wherefacgui += ")";

                //Carga ddocumento
                where = new WhereParams();
                where.where = "ddo_empresa=" + empresa;
                //where.where += " and ddo_comprobante in (" + string.Join(",", whereinfac) + ") or ddo_comprobante_guia in (" + string.Join(",", whereingui) + ")";
                where.where += " and " + wherefacgui;
                List<Ddocumento> lstddo = DdocumentoBLL.GetAll(where, "");

                string[] whereinddo = lstddo.Select(s => s.ddo_comprobante.ToString()).ToArray();

                //Carga cancelaciones
                where = new WhereParams();
                where.where = "dca_empresa=" + empresa;
                where.where += " and dca_comprobante in (" + string.Join(",", whereinddo) + ")";
                List<Dcancelacion> lstdca = DcancelacionBLL.GetAll(where, "");

                //Carga cancelaciones socios
                where = new WhereParams();
                where.where = "dcs_empresa=" + empresa;
                where.where += " and dcs_comprobante in (" + string.Join(",", whereinddo) + ")";
                List<Dcancelacionsocio> lstdcs = DcancelacionsocioBLL.GetAll(where, "");


                if (lstdca.Count > 0 || lstdcs.Count > 0)
                {

                    foreach (vHojadeRuta item in lst)
                    {
                        decimal? monto = 0;
                        decimal? cancela = 0;
                        decimal? cancelasocio = 0;


                        Comprobante comdet = lstcomdet.Find(f => f.com_codigo == item.codigodetalle);
                        if (comdet.com_tipodoc == 4)
                        {
                            monto += lstddo.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == comdet.com_codigo; }).Sum(s => s.ddo_monto);
                            cancela += lstdca.FindAll(f => f.dca_comprobante == comdet.com_codigo).Sum(s => s.dca_monto);
                            cancelasocio += lstdcs.FindAll(f => f.dcs_comprobante == comdet.com_codigo).Sum(s => s.dcs_monto);
                        }
                        if (comdet.com_tipodoc == 13)
                        {
                            Ddocumento ddofac = lstddo.Find(f => f.ddo_comprobante_guia == comdet.com_codigo);
                            monto += lstddo.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante_guia == comdet.com_codigo; }).Sum(s => s.ddo_monto);
                            if (ddofac != null)
                            {
                                cancela += lstdca.FindAll(f => f.dca_comprobante == ddofac.ddo_comprobante && f.dca_pago == ddofac.ddo_pago).Sum(s => s.dca_monto);
                                cancelasocio += lstdcs.FindAll(f => f.dcs_comprobante == ddofac.ddo_comprobante && f.dcs_pago == ddofac.ddo_pago).Sum(s => s.dcs_monto);
                            }
                        }


                        item.cancela = cancela;
                        item.cancelasocio = cancelasocio;
                    }

                }


            }
            return lst;
        }

        public static List<Dcancelacionsocio> SaveCancelaSocio(Dcancelacionsocio dcs)
        {
            Comprobante comprobante = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = dcs.dcs_empresa, com_empresa_key = dcs.dcs_empresa, com_codigo = dcs.dcs_comprobante, com_codigo_key = dcs.dcs_comprobante });

            List<Ddocumento> lstddo = new List<Ddocumento>();
            if (comprobante.com_tipodoc == 4)//Factura
                lstddo = DdocumentoBLL.GetAll("ddo_empresa=" + dcs.dcs_empresa + " and ddo_comprobante=" + dcs.dcs_comprobante, "");
            else
                lstddo = DdocumentoBLL.GetAll("ddo_empresa=" + dcs.dcs_empresa + " and ddo_comprobante_guia=" + dcs.dcs_comprobante, "");

            string[] whereinddo = lstddo.Select(s => s.ddo_comprobante.ToString()).ToArray();

            //Carga cancelaciones        
            List<Dcancelacion> lstdca = DcancelacionBLL.GetAll("dca_empresa="+dcs.dcs_empresa+ " and dca_comprobante in (" + string.Join(",", whereinddo) + ")", "");

            //Carga cancelaciones socios
            List<Dcancelacionsocio> lstdcs = DcancelacionsocioBLL.GetAll("dcs_empresa=" + dcs.dcs_empresa + " and dcs_comprobante in (" + string.Join(",", whereinddo) + ")", "");

            List<Dcancelacionsocio> lstins = new List<Dcancelacionsocio>();

            foreach (Ddocumento ddo in lstddo)
            {
                List<Dcancelacion> cancelaciones = lstdca.FindAll(f => f.dca_pago == ddo.ddo_pago);
                List<Dcancelacionsocio> cancelacionessocio = lstdcs.FindAll(f => f.dcs_pago == ddo.ddo_pago);
                decimal saldo = (ddo.ddo_monto ?? 0) - cancelaciones.Sum(s => s.dca_monto ?? 0) - cancelacionessocio.Sum(s => s.dcs_monto ?? 0);
                if (saldo > 0)
                {
                    Dcancelacionsocio cancelasocio = new Dcancelacionsocio();
                    cancelasocio.dcs_empresa = dcs.dcs_empresa;
                    cancelasocio.dcs_fecha = dcs.dcs_fecha.HasValue ? dcs.dcs_fecha : DateTime.Now;
                    cancelasocio.dcs_comprobante =ddo.ddo_comprobante;
                    cancelasocio.dcs_doctran = ddo.ddo_doctran;
                    cancelasocio.dcs_transacc = ddo.ddo_transacc;
                    cancelasocio.dcs_pago = ddo.ddo_pago;
                    cancelasocio.dcs_socio = dcs.dcs_socio;
                    cancelasocio.dcs_tipo = dcs.dcs_tipo;
                    cancelasocio.dcs_nrodoc = dcs.dcs_nrodoc;
                    cancelasocio.dcs_observacion = dcs.dcs_observacion;
                    cancelasocio.crea_usr = dcs.crea_usr;
                    cancelasocio.crea_fecha = DateTime.Now;
                    if (saldo>=dcs.dcs_monto)
                    {
                        cancelasocio.dcs_monto = dcs.dcs_monto;
                        dcs.dcs_monto = 0;
                        cancelasocio.dcs_codigo = DcancelacionsocioBLL.InsertIdentity(cancelasocio);
                        lstins.Add(cancelasocio);
                        break;
                    }
                    else
                    {
                        cancelasocio.dcs_monto = saldo;
                        dcs.dcs_monto = dcs.dcs_monto - saldo;
                        cancelasocio.dcs_codigo = DcancelacionsocioBLL.InsertIdentity(cancelasocio);
                        lstins.Add(cancelasocio);
                    }

                }


            }

            return lstins;                   
        }

        public static List<Ddocumento> GetCancelacionesSocios(int empresa, long? codigo, int tipodoc, out List<Dcancelacion> cancelaciones, out List<Dcancelacionsocio> cancelacionessocios)
        {
            List<Ddocumento> lstddo = new List<Ddocumento>();
            if (tipodoc == 4)//Factura
                lstddo = DdocumentoBLL.GetAll("ddo_empresa=" + empresa + " and ddo_comprobante=" + codigo, "");
            else
                lstddo = DdocumentoBLL.GetAll("ddo_empresa=" + empresa + " and ddo_comprobante_guia=" + codigo, "");

            string[] whereinddo = lstddo.Select(s => s.ddo_comprobante.ToString()).ToArray();

            //Carga cancelaciones        
            cancelaciones = DcancelacionBLL.GetAll("dca_empresa=" + empresa + " and dca_comprobante in (" + string.Join(",", whereinddo) + ")", "");
            //Carga cancelaciones socios
            cancelacionessocios = DcancelacionsocioBLL.GetAll("dcs_empresa=" + empresa + " and dcs_comprobante in (" + string.Join(",", whereinddo) + ")", "");

            return lstddo;



        }



    }
}

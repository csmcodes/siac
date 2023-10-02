using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using System.Collections;
using System.IO;

namespace Packages
{
    public class Auto
    {
        #region  Actualizar Documentos


        public static void eliminar_cancelaciones(BLL transaccion, Comprobante comp)
        {


            string whereddo = "";
            foreach (Dcancelacion dca in comp.cancelaciones)
            {
                whereddo += (whereddo != "" ? "," : "") + dca.dca_comprobante;
                DcancelacionBLL.Delete(transaccion, dca);
            }
            if (whereddo != "")
            {

                List<Ddocumento> documentos = DdocumentoBLL.GetAll("ddo_empresa=" + comp.com_empresa + " and ddo_comprobante in (" + whereddo + ")", "");
                foreach (Ddocumento ddo in documentos)
                {
                    List<Dcancelacion> lstdca = comp.cancelaciones.FindAll(delegate(Dcancelacion dc) { return dc.dca_empresa== ddo.ddo_empresa && dc.dca_comprobante== ddo.ddo_comprobante &&  dc.dca_transacc == ddo.ddo_transacc && dc.dca_doctran == ddo.ddo_doctran && dc.dca_pago == ddo.ddo_pago; });
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
                    DdocumentoBLL.Update(transaccion, ddo);
                }
                
            }
            



        }

        public static Comprobante actualizar_documentos(BLL transaccion, Comprobante comp)
        {

            foreach (Ddocumento ddo in comp.documentos)
            {
                ddo.ddo_cancela = 0;
                ddo.ddo_cancelado = 0;

                List<Dcancelacion> lstdca = comp.cancelaciones.FindAll(delegate (Dcancelacion dc) { return dc.dca_transacc == ddo.ddo_transacc && dc.dca_doctran == ddo.ddo_doctran && dc.dca_pago == ddo.ddo_pago; });
                foreach (Dcancelacion dca in lstdca)
                {
                    ddo.ddo_cancela += dca.dca_monto;
                    if (ddo.ddo_cancela>=ddo.ddo_monto)
                    {
                        ddo.ddo_cancelado = 1;
                        if (ddo.ddo_cancela > ddo.ddo_monto)
                            ddo.ddo_cancela = ddo.ddo_monto;
                    }
                }
                ddo.ddo_empresa_key = ddo.ddo_empresa;
                ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                ddo.ddo_transacc_key = ddo.ddo_transacc;
                ddo.ddo_doctran_key = ddo.ddo_doctran;
                ddo.ddo_pago_key = ddo.ddo_pago;
                DdocumentoBLL.Update(transaccion, ddo);

            }
            return comp;


      
        }

        public static Comprobante actualizar_documentos(Comprobante comp)
        {
            
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");


            foreach (Ddocumento ddo in comp.documentos)
            {
                ddo.ddo_cancela = 0;
                ddo.ddo_cancelado = 0;

                List<Dcancelacion> lstdca = comp.cancelaciones.FindAll(delegate (Dcancelacion dc) { return dc.dca_transacc == ddo.ddo_transacc && dc.dca_doctran == ddo.ddo_doctran && dc.dca_pago == ddo.ddo_pago; });
                foreach (Dcancelacion dca in lstdca)
                {
                    ddo.ddo_cancela += dca.dca_monto;
                    if (ddo.ddo_cancela >= ddo.ddo_monto)
                    {
                        ddo.ddo_cancelado = 1;
                        if (ddo.ddo_cancela > ddo.ddo_monto)
                            ddo.ddo_cancela = ddo.ddo_monto;
                    }
                }
                ddo.ddo_empresa_key = ddo.ddo_empresa;
                ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                ddo.ddo_transacc_key = ddo.ddo_transacc;
                ddo.ddo_doctran_key = ddo.ddo_doctran;
                ddo.ddo_pago_key = ddo.ddo_pago;
                DdocumentoBLL.Update(ddo);
            }
            return comp;



        }

        #endregion


        #region Actualiza Saldos CNT



        public static List<Saldo> saldos_cnt(Comprobante comp, int tipo)
        {
            int periodo = comp.com_fecha.Year;
            int mes = comp.com_fecha.Month;
            List<Saldo> saldos = new List<Saldo>();
            decimal valordebito = 0;
            decimal valordebito_ext = 0;
            decimal valorcredito = 0;
            decimal valorcredito_ext = 0;

            if (comp.contables != null)
            {
                foreach (Dcontable item in comp.contables)
                {

                    if (item.dco_debcre == Constantes.cDebito)
                    {
                        valordebito = item.dco_valor_nac;
                        valordebito_ext = (item.dco_valor_ext.HasValue) ? item.dco_valor_ext.Value : 0;
                        valorcredito = 0;
                        valorcredito_ext = 0;
                    }
                    else
                    {
                        valordebito = 0;
                        valordebito_ext = 0;
                        valorcredito = item.dco_valor_nac;
                        valorcredito_ext = (item.dco_valor_ext.HasValue) ? item.dco_valor_ext.Value : 0;
                    }
                    if (tipo == 2)
                    {
                        valordebito = valordebito * -1;
                        valordebito_ext = valordebito_ext * -1;
                        valorcredito = valorcredito * -1;
                        valorcredito_ext = valorcredito_ext * -1;
                    }
                    if (comp.com_nocontable == 0)
                    {
                        Saldo sal = saldos.Find(delegate(Saldo s) { return s.sal_cuenta == item.dco_cuenta; });
                        if (sal != null)
                        {

                            sal.sal_debito = sal.sal_debito + valordebito;
                            sal.sal_debext = sal.sal_debext + valordebito_ext;
                            sal.sal_credito = sal.sal_credito + valorcredito;
                            sal.sal_creext = sal.sal_creext + valorcredito_ext;
                        }
                        else
                        {

                            sal = new Saldo();
                            sal.sal_empresa = item.dco_empresa;
                            sal.sal_empresa_key = item.dco_empresa;
                            sal.sal_cuenta = item.dco_cuenta;
                            sal.sal_cuenta_key = item.dco_cuenta;
                            sal.sal_centro = item.dco_centro;
                            sal.sal_centro_key = item.dco_centro;
                            sal.sal_almacen = (item.dco_almacen.HasValue) ? item.dco_almacen.Value : comp.com_almacen.Value;
                            sal.sal_almacen_key = (item.dco_almacen.HasValue) ? item.dco_almacen.Value : comp.com_almacen.Value;
                            sal.sal_transacc = item.dco_transacc;
                            sal.sal_transacc_key = item.dco_transacc;
                            sal.sal_periodo = periodo;
                            sal.sal_periodo_key = periodo;
                            sal.sal_mes = mes;
                            sal.sal_mes_key = mes;

                            sal = SaldoBLL.GetByPK(sal);
                            if (sal != null)
                            {
                                sal.sal_debito = sal.sal_debito + valordebito;
                                sal.sal_debext = sal.sal_debext + valordebito_ext;
                                sal.sal_credito = sal.sal_credito + valorcredito;
                                sal.sal_creext = sal.sal_creext + valorcredito_ext;
                            }
                            else
                            {
                                sal = new Saldo();
                                sal.sal_empresa = item.dco_empresa;
                                sal.sal_empresa_key = item.dco_empresa;
                                sal.sal_cuenta = item.dco_cuenta;
                                sal.sal_cuenta_key = item.dco_cuenta;
                                sal.sal_centro = item.dco_centro;
                                sal.sal_centro_key = item.dco_centro;
                                sal.sal_almacen = (item.dco_almacen.HasValue) ? item.dco_almacen.Value : comp.com_almacen.Value;
                                sal.sal_almacen_key = (item.dco_almacen.HasValue) ? item.dco_almacen.Value : comp.com_almacen.Value;
                                sal.sal_transacc = item.dco_transacc;
                                sal.sal_transacc_key = item.dco_transacc;
                                sal.sal_periodo = periodo;
                                sal.sal_periodo_key = periodo;
                                sal.sal_mes = mes;
                                sal.sal_mes_key = mes;
                                sal.sal_debito = valordebito;
                                sal.sal_debext = valordebito_ext;
                                sal.sal_credito = valorcredito;
                                sal.sal_creext = valorcredito_ext;
                            }
                            saldos.Add(sal);
                        }
                    }

                }
            }
            return saldos; 
 
        }


        public static List<Saldo> saldos_cnt(int empresa, int periodo, int mes, int? cuenta, int tipo)
        {

            WhereParams parametros = new WhereParams();
            parametros.where = " com_empresa={0} and com_periodo={1} and com_mes={2} and com_estado ={3} ";
            List<object> valores = new List<object>();
            valores.Add(empresa);
            valores.Add(periodo);
            valores.Add(mes);
            valores.Add(Constantes.cEstadoMayorizado);

            if (cuenta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_codigo = {4} ";
                valores.Add(cuenta.Value);
            }

            parametros.valores = valores.ToArray();            



            List<vDcontable> contables = vDcontableBLL.GetAll(parametros,"");
            List<Saldo> saldos = new List<Saldo>();



            decimal valordebito = 0;
            decimal valordebito_ext = 0;
            decimal valorcredito = 0;
            decimal valorcredito_ext = 0;






            foreach (vDcontable item in contables)
            {

                if (item.dco_debcre == Constantes.cDebito)
                {
                    valordebito = item.dco_valor_nac.Value;
                    valordebito_ext = (item.dco_valor_ext.HasValue) ? item.dco_valor_ext.Value : 0;
                    valorcredito = 0;
                    valorcredito_ext = 0;
                }
                else
                {
                    valordebito = 0;
                    valordebito_ext = 0;
                    valorcredito = item.dco_valor_nac.Value;
                    valorcredito_ext = (item.dco_valor_ext.HasValue) ? item.dco_valor_ext.Value : 0;
                }
                if (tipo == 2)
                {
                    valordebito = valordebito * -1;
                    valordebito_ext = valordebito_ext * -1;
                    valorcredito = valorcredito * -1;
                    valorcredito_ext = valorcredito_ext * -1;
                }
                Saldo sal = saldos.Find(delegate(Saldo s) { return s.sal_cuenta == item.cue_codigo.Value; });
                if (sal != null)
                {

                    sal.sal_debito = sal.sal_debito + valordebito;
                    sal.sal_debext = sal.sal_debext + valordebito_ext;
                    sal.sal_credito = sal.sal_credito + valorcredito;
                    sal.sal_creext = sal.sal_creext + valorcredito_ext;
                }
                else
                {

                    sal = new Saldo();
                    sal.sal_empresa = empresa;
                    sal.sal_empresa_key = empresa;
                    sal.sal_cuenta = item.cue_codigo.Value;
                    sal.sal_cuenta_key = item.cue_codigo.Value;
                    sal.sal_centro = item.dco_centro.Value;
                    sal.sal_centro_key = item.dco_centro.Value;
                    sal.sal_almacen = (item.dco_almacen.HasValue) ? item.dco_almacen.Value : item.com_almacen.Value;
                    sal.sal_almacen_key = (item.dco_almacen.HasValue) ? item.dco_almacen.Value : item.com_almacen.Value;
                    sal.sal_transacc = item.dco_transacc.Value;
                    sal.sal_transacc_key = item.dco_transacc.Value;
                    sal.sal_periodo = periodo;
                    sal.sal_periodo_key = periodo;
                    sal.sal_mes = mes;
                    sal.sal_mes_key = mes;
                    sal.sal_debito = valordebito;
                    sal.sal_debext = valordebito_ext;
                    sal.sal_credito = valorcredito;
                    sal.sal_creext = valorcredito_ext;                  
                    saldos.Add(sal);
                }             

            }
            return saldos;

        }

        public static List<Saldo> get_saldos_cnt(int empresa, int periodo, int mes, int? cuenta)
        {

            WhereParams parametros = new WhereParams();
            parametros.where = " sal_empresa={0} and sal_periodo={1} and sal_mes={2} ";
            List<object> valores = new List<object>();
            valores.Add(empresa);
            valores.Add(periodo);
            valores.Add(mes);

            if (cuenta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " sal_cuenta = {3} ";
                valores.Add(cuenta.Value);
            }

            parametros.valores = valores.ToArray();
            List<Saldo> saldos = SaldoBLL.GetAll(parametros, "");

            return saldos;

        }

        
      
        public static void actualizar_cnt(BLL transaccion, Comprobante comp, int tipo)
        {

        

            int periodo = comp.com_fecha.Year;        
            int mes = comp.com_fecha.Month;

            List<Saldo> saldos = SaldoBLL.GetAll(new WhereParams("sal_empresa={0} and sal_centro={1} and sal_almacen={2} and sal_transacc={3} and sal_periodo={4} and sal_mes={5}", comp.com_empresa, comp.com_centro, comp.com_almacen, comp.com_transacc, periodo, mes), "");

            decimal valordebito = 0;
            decimal valordebito_ext = 0;
            decimal valorcredito = 0;
            decimal valorcredito_ext = 0;

            //List<Dcontable> detalle = DcontableBLL.GetAll(new WhereParams("dco_empresa={0} and dco_comprobante={1}",comp.com_empresa, comp.com_codigo), "");
            //foreach (Dcontable item in detalle)
            foreach (Dcontable item in comp.contables)
            {
                if (item.dco_debcre == Constantes.cDebito)
                {
                    valordebito = item.dco_valor_nac;
                    valordebito_ext = (item.dco_valor_ext.HasValue) ? item.dco_valor_ext.Value : 0;
                    valorcredito = 0;
                    valorcredito_ext = 0;
                }
                else
                {
                    valordebito = 0;
                    valordebito_ext = 0;
                    valorcredito = item.dco_valor_nac;
                    valorcredito_ext = (item.dco_valor_ext.HasValue) ? item.dco_valor_ext.Value : 0;
                }
                if (tipo == 2)
                {
                    valordebito = valordebito * -1;
                    valordebito_ext = valordebito_ext * -1;
                    valorcredito = valorcredito * -1;
                    valorcredito_ext = valorcredito_ext * -1;
                }
                if (comp.com_nocontable == 0)
                {
                    Saldo sal = saldos.Find(delegate(Saldo s) { return s.sal_cuenta == item.dco_cuenta; });
                    if (sal != null)
                    {

                        sal.sal_debito = sal.sal_debito + valordebito;
                        sal.sal_debext = sal.sal_debext + valordebito_ext;
                        sal.sal_credito = sal.sal_credito + valorcredito;
                        sal.sal_creext = sal.sal_creext + valorcredito_ext;
                        SaldoBLL.Update(transaccion, sal); 
                    }
                    else
                    {
                        sal = new Saldo();
                        sal.sal_empresa = item.dco_empresa;
                        sal.sal_cuenta = item.dco_cuenta;
                        sal.sal_centro = item.dco_centro;
                        sal.sal_almacen = item.dco_almacen.Value;
                        sal.sal_transacc = item.dco_transacc;
                        sal.sal_periodo= periodo;
                        sal.sal_mes = mes;
                        sal.sal_debito = valordebito ;
                        sal.sal_debext = valordebito_ext;
                        sal.sal_credito = valorcredito;
                        sal.sal_creext = valorcredito_ext;                       
                        SaldoBLL.Insert(transaccion, sal);
                    }
                    //actualizar_saldo_cnt(transaccion, new Saldo { sal_empresa = item.dco_empresa, sal_cuenta = item.dco_cuenta, sal_centro = item.dco_centro, sal_almacen = item.dco_almacen.Value, sal_transacc = item.dco_transacc, sal_periodo = periodo, sal_mes = mes, sal_debito = valordebito, sal_debext = valordebito_ext, sal_credito = valorcredito, sal_creext = valorcredito_ext });
                }
            }
        }
        
     
        #endregion

        #region Actualiza Saldos BAN

        public static List<Salban> saldos_ban(Comprobante comp, int tipo)
        {
            int periodo = comp.com_fecha.Year;
            int mes = comp.com_fecha.Month;
            List<Salban> saldos = new List<Salban>();
            decimal valordebito = 0;
            decimal valordebito_ext = 0;
            decimal valorcredito = 0;
            decimal valorcredito_ext = 0;

            if (comp.bancario != null)
            {
                foreach (Dbancario item in comp.bancario)
                {
                    if (item.dban_debcre == Constantes.cDebito)
                    {
                        valordebito = item.dban_valor_nac;
                        valordebito_ext = (item.dban_valor_ext.HasValue) ? item.dban_valor_ext.Value : 0;
                        valorcredito = 0;
                        valorcredito_ext = 0;
                    }
                    else
                    {
                        valordebito = 0;
                        valordebito_ext = 0;
                        valorcredito = item.dban_valor_nac;
                        valorcredito_ext = (item.dban_valor_ext.HasValue) ? item.dban_valor_ext.Value : 0;
                    }
                    if (tipo == 2)
                    {
                        valordebito = valordebito * -1;
                        valordebito_ext = valordebito_ext * -1;
                        valorcredito = valorcredito * -1;
                        valorcredito_ext = valorcredito_ext * -1;
                    }
                    if (comp.com_nocontable == 0)
                    {
                        Salban sal = saldos.Find(delegate(Salban s) { return s.slb_banco == item.dban_banco; });
                        if (sal != null)
                        {
                            sal.slb_debito = sal.slb_debito + valordebito;
                            sal.slb_debext = sal.slb_debext + valordebito_ext;
                            sal.slb_credito = sal.slb_credito + valorcredito;
                            sal.slb_creext = sal.slb_creext + valorcredito_ext;
                        }
                        else
                        {
                            sal = new Salban();
                            sal.slb_empresa = item.dban_empresa;
                            sal.slb_banco = item.dban_banco;
                            sal.slb_almacen = comp.com_almacen.Value;
                            sal.slb_transacc = item.dban_transacc;
                            sal.slb_periodo = periodo;
                            sal.slb_mes = mes;
                            sal.slb_empresa_key = item.dban_empresa;
                            sal.slb_banco_key = item.dban_banco;
                            sal.slb_almacen_key = comp.com_almacen.Value;
                            sal.slb_transacc_key = item.dban_transacc;
                            sal.slb_periodo_key = periodo;
                            sal.slb_mes_key = mes;
                            sal.slb_debito = 0;
                            sal.slb_debext = 0;
                            sal.slb_credito = 0;
                            sal.slb_creext = 0;
                            sal = SalbanBLL.GetByPK(sal);
                            if (sal != null)
                            {
                                sal.slb_debito = sal.slb_debito+ valordebito;
                                sal.slb_debext = sal.slb_debext + valordebito_ext;
                                sal.slb_credito = sal.slb_credito+  valorcredito;
                                sal.slb_creext = sal.slb_creext +valorcredito_ext;
                            }
                            else
                            {
                                sal = new Salban();
                                sal.slb_empresa = item.dban_empresa;
                                sal.slb_banco = item.dban_banco;
                                sal.slb_almacen = comp.com_almacen.Value;
                                sal.slb_transacc = item.dban_transacc;
                                sal.slb_periodo = periodo;
                                sal.slb_mes = mes;

                                sal.slb_empresa_key = item.dban_empresa;
                                sal.slb_banco_key = item.dban_banco;
                                sal.slb_almacen_key = comp.com_almacen.Value;
                                sal.slb_transacc_key = item.dban_transacc;
                                sal.slb_periodo_key = periodo;
                                sal.slb_mes_key = mes;

                               
                                sal.slb_debito = valordebito;
                                sal.slb_debext = valordebito_ext;
                                sal.slb_credito = valorcredito;
                                sal.slb_creext = valorcredito_ext;
                            }
                            saldos.Add(sal);
                        }
                    }
                }

            }
            return saldos;
        }

        public static List<Salban> saldos_ban(int empresa, int periodo, int mes, int? cuenta, int tipo)
        {

            WhereParams parametros = new WhereParams();
            parametros.where = " com_empresa={0} and com_periodo={1} and com_mes={2} and com_estado ={3}";
            List<object> valores = new List<object>();
            valores.Add(empresa);
            valores.Add(periodo);
            valores.Add(mes);
            valores.Add(Constantes.cEstadoMayorizado);

            if (cuenta.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " cue_codigo = {4} ";
                valores.Add(cuenta.Value);
            }

            parametros.valores = valores.ToArray();



            List<vDbancario> bancarios = vDbancarioBLL.GetAll(parametros, "");
            List<Salban> saldos = new List<Salban>();



            decimal valordebito = 0;
            decimal valordebito_ext = 0;
            decimal valorcredito = 0;
            decimal valorcredito_ext = 0;






            foreach (vDbancario item in bancarios)
            {

                if (item.dban_debcre == Constantes.cDebito)
                {
                    valordebito = item.dban_valor_nac.Value;
                    valordebito_ext = (item.dban_valor_ext.HasValue) ? item.dban_valor_ext.Value : 0;
                    valorcredito = 0;
                    valorcredito_ext = 0;
                }
                else
                {
                    valordebito = 0;
                    valordebito_ext = 0;
                    valorcredito = item.dban_valor_nac.Value;
                    valorcredito_ext = (item.dban_valor_ext.HasValue) ? item.dban_valor_ext.Value : 0;
                }
                if (tipo == 2)
                {
                    valordebito = valordebito * -1;
                    valordebito_ext = valordebito_ext * -1;
                    valorcredito = valorcredito * -1;
                    valorcredito_ext = valorcredito_ext * -1;
                }
                Salban sal = saldos.Find(delegate(Salban s) { return s.slb_banco == item.ban_codigo.Value; });
                if (sal != null)
                {
                    sal.slb_debito = sal.slb_debito + valordebito;
                    sal.slb_debext = sal.slb_debext + valordebito_ext;
                    sal.slb_credito = sal.slb_credito + valorcredito;
                    sal.slb_creext = sal.slb_creext + valorcredito_ext;
                }
                else
                {
                    sal = new Salban();
                    sal.slb_empresa = empresa;
                    sal.slb_banco = item.ban_codigo.Value;
                    sal.slb_almacen = item.com_almacen.Value;
                    sal.slb_transacc = item.dban_transacc.Value;
                    sal.slb_periodo = periodo;
                    sal.slb_mes = mes;
                    sal.slb_empresa_key = empresa;
                    sal.slb_banco_key = item.ban_codigo.Value;
                    sal.slb_almacen_key = item.com_almacen.Value;
                    sal.slb_transacc_key = item.dban_transacc.Value;
                    sal.slb_periodo_key = periodo;
                    sal.slb_mes_key = mes;
                    sal.slb_debito = valordebito;
                    sal.slb_debext = valordebito_ext;
                    sal.slb_credito = valorcredito;
                    sal.slb_creext = valorcredito_ext;

                    saldos.Add(sal);
                }

            }
            return saldos;

        }


        public static List<Salban> get_saldos_ban(int empresa, int periodo, int mes, int? banco)
        {

            WhereParams parametros = new WhereParams();
            parametros.where = " slb_empresa={0} and slb_periodo={1} and slb_mes={2} ";
            List<object> valores = new List<object>();
            valores.Add(empresa);
            valores.Add(periodo);
            valores.Add(mes);

            if (banco.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " slb_banco = {3} ";
                valores.Add(banco.Value);
            }

            parametros.valores = valores.ToArray();


            List<Salban> saldos = SalbanBLL.GetAll(parametros, "");

            return saldos;

        }


        

        public static void actualizar_ban(BLL transaccion, Comprobante comp, int tipo)
        {
            int periodo = comp.com_fecha.Year;
            int mes = comp.com_fecha.Month;

            decimal valordebito = 0;
            decimal valordebito_ext = 0;
            decimal valorcredito = 0;
            decimal valorcredito_ext = 0;


            //List<Dbancario> detalle = DbancarioBLL.GetAll(new WhereParams("dban_empresa={0} and dban_cco_comproba={1}", empresa, comprobante), "");
            //foreach (Dbancario item in detalle)
            foreach (Dbancario item in comp.bancario)
            {
                if (item.dban_debcre == Constantes.cDebito)
                {
                    valordebito = item.dban_valor_nac;
                    valordebito_ext = (item.dban_valor_ext.HasValue) ? item.dban_valor_ext.Value : 0;
                    valorcredito = 0;
                    valorcredito_ext = 0;
                }
                else
                {
                    valordebito = 0;
                    valordebito_ext = 0;
                    valorcredito = item.dban_valor_nac;
                    valorcredito_ext = (item.dban_valor_ext.HasValue) ? item.dban_valor_ext.Value : 0;
                }
                if (tipo == 2)
                {
                    valordebito = valordebito * -1;
                    valordebito_ext = valordebito_ext * -1;
                    valorcredito = valorcredito * -1;
                    valorcredito_ext = valorcredito_ext * -1;
                }
                if (comp.com_nocontable == 0)
                {
                    //actualizar_saldo_cnt(new Salban { slb_empresa= item.dban_empresa,  slb_banco = item.dban_banco, slb_almacen = item.  sal_cuenta = item.dban_bancocuenta, sal_centro = item.ce, sal_almacen = item.dco_almacen.Value, sal_transacc = item.dco_transacc, sal_periodo = periodo, sal_mes = mes, sal_debito = valordebito, sal_debext = valordebito_ext, sal_credito = valorcredito, sal_creext = valorcredito_ext });
                    //actualizar_saldo_ban(transaccion, new Salban { slb_empresa = item.dban_empresa, slb_banco = item.dban_banco, slb_almacen = comp.com_almacen.Value, slb_transacc = item.dban_transacc, slb_periodo = periodo, slb_mes = mes, slb_debito = valordebito, slb_debext = valordebito_ext, slb_credito = valorcredito, slb_creext = valorcredito_ext });
                }
            }
        }
       

      

        #endregion

        #region Actualiza CXC


        public static List<Ddocumento> actualizar_cxc(Comprobante comp, int tipo)
        {
            int periodo = comp.com_fecha.Year;
            int mes = comp.com_fecha.Month;

            //comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            List<Ddocumento> documentos = new List<Ddocumento>(); 

            decimal valor = 0;
            decimal valor_ext = 0;

            if (comp.cancelaciones != null)
            {

                foreach (Dcancelacion item in comp.cancelaciones)
                {
                    valor = item.dca_monto.Value;
                    valor_ext = item.dca_monto_ext.HasValue ? item.dca_monto_ext.Value : item.dca_monto.Value;
                    if (tipo == 2) //Desmayorizacion
                    {
                        valor = valor * -1;
                        valor_ext = valor_ext * -1;
                    }

                    Ddocumento documento = comp.documentos.Find(delegate(Ddocumento d) { return d.ddo_pago == item.dca_pago; });
                    if (documento != null)
                    {
                        documento.ddo_cancela = documento.ddo_cancela.Value + valor;
                        documento.ddo_cancela_ext = documento.ddo_cancela_ext + valor_ext;
                        if ((documento.ddo_cancela.Value) >= documento.ddo_monto.Value)
                            documento.ddo_cancelado = 1;
                        else
                            documento.ddo_cancelado = 0;
                    }

                    documentos.Add(documento);
                }
            }
            return documentos;
        }

        public static void actualizar_cxc(BLL transaccion, Comprobante comp, int tipo)
        {
            int periodo = comp.com_fecha.Year;
            int mes = comp.com_fecha.Month;

            //comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_comprobante={1}", comp.com_empresa, comp.com_codigo), "");

            decimal valor = 0;
            decimal valor_ext = 0;

            foreach (Dcancelacion item in comp.cancelaciones)
            {
                valor = item.dca_monto.Value;
                valor_ext = item.dca_monto_ext.Value;
                if (tipo == 2) //Desmayorizacion
                {
                    valor = valor * -1;
                    valor_ext = valor_ext * -1;
                }


                Ddocumento documento = DdocumentoBLL.GetByPK(new Ddocumento { ddo_empresa = item.dca_empresa, ddo_empresa_key = item.dca_empresa, ddo_comprobante = item.dca_comprobante, ddo_comprobante_key = item.dca_comprobante, ddo_transacc = item.dca_transacc, ddo_transacc_key = item.dca_transacc, ddo_doctran = item.dca_doctran, ddo_doctran_key = item.dca_doctran, ddo_pago = item.dca_pago, ddo_pago_key = item.dca_pago });
                documento.ddo_cancela = documento.ddo_cancela.Value + valor;
                documento.ddo_cancela_ext = documento.ddo_cancela_ext + valor_ext;
                if ((documento.ddo_cancela.Value) >= documento.ddo_monto.Value)
                    documento.ddo_cancelado = 1;
                else
                    documento.ddo_cancelado = 0;
                DdocumentoBLL.Update(transaccion , documento);

            }
        }



        /// <summary>
        /// Actualiza saldo de los clientes
        /// </summary>
        /// <param name="empresa">codigo de emmpresa</param>
        /// <param name="comprobante">codigo del comprobante</param>
        /// <param name="periodo">anio de corte</param>
        /// <param name="mes">mes de corte</param>
        /// <param name="nocontable">determina si el comprobante es o no contable</param>
        /// <param name="tipo">
        ///     1 - proceso de actualizacion (mayorizacion)
        ///     2 - Proceso de desactualizacion (desmayorizacion)
        ///     3 - Proceso de remayorizacion (reconstruccion de saldos)        
        /// </param>
        public static void actualizar_cxc(int empresa, long comprobante, int periodo, int mes, int nocontable, int tipo)
        {
            Comprobante comp = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = empresa, com_empresa_key = empresa, com_codigo = comprobante, com_codigo_key = comprobante });

            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa={0} and dca_comprobante={1}",comp.com_empresa, comp.com_codigo), "");            

            decimal valor = 0;
            decimal valor_ext = 0;

            foreach (Dcancelacion item in comp.cancelaciones)
            {
                valor = item.dca_monto.Value;
                valor_ext = item.dca_monto_ext.Value;
                if (tipo == 2) //Desmayorizacion
                {
                    valor = valor * -1;
                    valor_ext = valor_ext * -1;
                }


                Ddocumento documento = DdocumentoBLL.GetByPK(new Ddocumento { ddo_empresa = item.dca_empresa, ddo_empresa_key = item.dca_empresa, ddo_comprobante = item.dca_comprobante, ddo_comprobante_key = item.dca_comprobante, ddo_transacc = item.dca_transacc, ddo_transacc_key = item.dca_transacc, ddo_doctran = item.dca_doctran, ddo_doctran_key = item.dca_doctran, ddo_pago = item.dca_pago, ddo_pago_key = item.dca_pago });
                documento.ddo_cancela = documento.ddo_cancela.Value + valor;
                documento.ddo_cancela_ext = documento.ddo_cancela_ext + valor_ext;  
                if ((documento.ddo_cancela.Value) >= documento.ddo_monto.Value)
                    documento.ddo_cancelado = 1;                
                else
                    documento.ddo_cancelado = 0;
                DdocumentoBLL.Update(documento);                   

            }           
        }

        #endregion


        public static bool pasar_secuencias(BLL transaccion, int empresa, int deperiodo, int aperiodo)
        {
            ////ELIMINA LOS SALDOS EXISTENTES  DEL MES 0 DEL APERIODO//

            List<Dtipocom> secuencias = DtipocomBLL.GetAll(new WhereParams("dti_empresa={0} and dti_periodo={1}",empresa, deperiodo),"");
            foreach (Dtipocom item in secuencias)
            {
                item.dti_periodo = aperiodo;
                DtipocomBLL.Insert(transaccion, item);               
            }


            return true;

        }


        public static bool pasar_saldos(BLL transaccion,int empresa, int deperiodo, int aperiodo)
        {
            ////ELIMINA LOS SALDOS EXISTENTES  DEL MES 0 DEL APERIODO//

            List<Saldo> lstsaldodel = get_saldos_cnt(empresa, aperiodo, 0,null);
            List<Salban> lstsalbandel = get_saldos_ban(empresa, aperiodo, 0, null);


            foreach (Saldo item in lstsaldodel)
            {
                SaldoBLL.Delete(transaccion, item);
            }

            foreach (Salban item in lstsalbandel)
            {
                SalbanBLL.Delete(transaccion, item);
            }


            //List<vSaldo> saldos = vSaldoBLL.GetAll(new WhereParams("sal_empresa={0} and sal_periodo={1} and cue_genero between 0 and 3", empresa, deperiodo), "");
            List<vSaldo> saldos = vSaldoBLL.GetAll(new WhereParams("sal_empresa={0} and sal_periodo={1}", empresa, deperiodo), "");
            foreach (vSaldo item in saldos)
            {
                Saldo saldo = new Saldo();
                saldo.sal_empresa = item.sal_empresa.Value;
                saldo.sal_cuenta = item.sal_cuenta.Value;
                saldo.sal_almacen = item.sal_almacen.Value;
                saldo.sal_centro = item.sal_centro.Value;
                saldo.sal_transacc = item.sal_transacc.Value;
                saldo.sal_periodo = aperiodo;
                saldo.sal_mes = 0;
                saldo.sal_debext = item.sal_debext.Value;
                saldo.sal_debito = item.sal_debito.Value;
                saldo.sal_credito = item.sal_credito.Value;
                saldo.sal_creext = item.sal_creext.Value;
                
                SaldoBLL.Insert(transaccion, saldo);
                
            }



            //////CARGA LOS SALDOS DEL MES 12 DEL DEPERIODO//
            //List<Saldo> lstsaldode = get_saldos_cnt(empresa, deperiodo,12,null);
            List<Salban> lstsalbande = get_saldos_ban(empresa, deperiodo,12, null);
            
            
            //foreach (Saldo item in lstsaldode)
            //{
            //    item.sal_periodo = aperiodo;
            //    item.sal_periodo_key = aperiodo;
            //    item.sal_mes = 0;
            //    item.sal_mes_key = 0;
            //    if (item.sal_cuegenero > 0 && item.sal_cuegenero < 4) //CUENTAS DE BALANCE UNICAMENTE
            //    {
            //        List<Saldo> saldoanterior = saldos.FindAll(delegate(Saldo s) { return s.sal_cuenta == item.sal_cuenta; });

            //        foreach (Saldo saldo in saldoanterior)
            //        {
            //            item.sal_debito += saldo.sal_debito;
            //            item.sal_credito += saldo.sal_credito;
            //        }

          

            //        if (SaldoBLL.Update(transaccion, item) == 0)
            //            SaldoBLL.Insert(transaccion, item);
            //    }

            //}

            foreach (Salban item in lstsalbande)
            {
                item.slb_periodo = aperiodo;
                item.slb_mes = 0;
                if (SalbanBLL.Update(transaccion, item) == 0)
                    SalbanBLL.Insert(transaccion, item);

            }

            return true;

        }


        public static bool actualizar_saldos(BLL transaccion, int empresa, int periodo, int mes, int? cuenta, int tipo)
        {
            ////ELIMINA LOS SALDOS EXISTENTES //

            List<Saldo> lstsaldodel = get_saldos_cnt(empresa, periodo, mes, cuenta);
            List<Salban> lstsalbandel = get_saldos_ban(empresa, periodo, mes,null);
            foreach (Saldo item in lstsaldodel)
            {
                SaldoBLL.Delete(transaccion, item);
            }

            foreach (Salban item in lstsalbandel)
            {
                SalbanBLL.Delete(transaccion, item);
            }

            
            ////CARGA LOS NUEVOS SALDOS CALCULADOS //

            List<Saldo> lstsaldo = saldos_cnt(empresa,periodo,mes,cuenta, tipo);
            List<Salban> lstsalban = saldos_ban(empresa,periodo,mes,cuenta, tipo);
            foreach (Saldo item in lstsaldo)
            {
                if (SaldoBLL.Update(transaccion, item) == 0)
                    SaldoBLL.Insert(transaccion, item);

            }

            foreach (Salban item in lstsalban)
            {
                if (SalbanBLL.Update(transaccion, item) == 0)
                    SalbanBLL.Insert(transaccion, item);

            }

            return true;
          
        }


        public static bool actualizar_saldo(BLL transaccion, Comprobante comp, int tipo)
        {
            bool resultado = false;

            int periodo = comp.com_fecha.Year;
            int mes = comp.com_fecha.Month;                       

            if ((tipo == 3 && (comp.com_estado == Constantes.cEstadoMayorizado || comp.com_estado == Constantes.cEstadoPorAutorizar)) || (tipo == 1 && (comp.com_estado == Constantes.cEstadoGrabado|| comp.com_estado==Constantes.cEstadoProceso)) || (tipo == 2 && (comp.com_estado == Constantes.cEstadoMayorizado || comp.com_estado == Constantes.cEstadoPorAutorizar )))
            {                

                if (comp.com_nocontable == 0)
                {
                    List<Saldo> lstsaldo = saldos_cnt(comp, tipo);
                    List<Salban> lstsalban = saldos_ban(comp, tipo);                    
                    if (tipo != 3)
                        comp.documentos = actualizar_cxc(comp, tipo);


                    foreach (Saldo item in lstsaldo)
                    {
                        if (SaldoBLL.Update(transaccion, item) == 0)
                            SaldoBLL.Insert(transaccion, item); 
                        
                    }

                    foreach (Salban item in lstsalban)
                    {
                        if (SalbanBLL.Update(transaccion, item) == 0)
                            SalbanBLL.Insert(transaccion, item);

                    }



                }
                if (tipo == 1 || tipo == 2)
                {
                    
                    resultado = actualizar_estado(transaccion,comp, tipo);
                }
            }
            else
            {
                if ((tipo == 1 && (comp.com_estado == 2 || comp.com_estado == 3)) || (tipo == 2 && (comp.com_estado == 0 || comp.com_estado == 1 || comp.com_estado == 4)))
                    resultado = true;
                else
                    throw new Exception("Estado de comprobante incorrecto ");
            }

            return resultado; 
        }


        /// <summary>
        /// Proceso que llama a todos los actualizadores, CNT, INV, CXC
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="comprobante"></param>
        /// <param name="tipo">
        ///     1 - proceso de actualizacion (mayorizacion)
        ///     2 - Proceso de desactualizacion (desmayorizacion)
        ///     3 - Proceso de remayorizacion (reconstruccion de saldos)        
        /// </param>
        public static bool actualizar_saldo(int empresa, long comprobante, int tipo)
        {
            bool resultado = false;
            Comprobante comp = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = empresa, com_empresa_key = empresa, com_codigo = comprobante, com_codigo_key = comprobante });
            int periodo = comp.com_fecha.Year;
            int mes = comp.com_fecha.Month;           
            if ((tipo ==3 && (comp.com_estado == 2 || comp.com_estado == 3)) ||  (tipo == 1 && comp.com_estado ==1) || (tipo==2 && (comp.com_estado == 2 || comp.com_estado == 3)))
            {
                if (comp.com_nocontable == 0)
                {
                    //actualizar_cnt(empresa, comprobante, periodo, mes, comp.com_nocontable, tipo);
                    //actualizar_ban(empresa, comprobante, periodo, mes, comp.com_nocontable, tipo);
                    if (tipo != 3)
                        actualizar_cxc(empresa, comprobante, periodo, mes, comp.com_nocontable, tipo);

                }
                if (tipo == 1 || tipo == 2)
                {
                    resultado = actualizar_estado(empresa, comprobante, tipo);
                }
            }
            else
            {
                if ((tipo == 1 && (comp.com_estado == 2 || comp.com_estado == 3)) || (tipo == 2 && (comp.com_estado == 0 || comp.com_estado == 1 || comp.com_estado == 4)))
                    resultado = true;
                else
                    throw new Exception("Estado de comprobante incorrecto ");
            }
            return resultado;
        }

        /// <summary>
        /// Actualiza el estado del comprobante dependiendo de su perfil del tipo de comprobante
        /// </summary>
        /// <param name="empresa"></param>
        /// <param name="comprobante"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static bool actualizar_estado(int empresa, long comprobante, int tipo)
        {
            Comprobante comp = ComprobanteBLL.GetByPK(new Comprobante { com_empresa = empresa, com_empresa_key = empresa, com_codigo = comprobante, com_codigo_key = comprobante });
            if (tipo == 1)
            {
                if (CNT.comprobante_cuadrado(empresa, comprobante))//COMPROBANTE CUADRADO
                    comp.com_descuadre = 0;
                else
                    comp.com_descuadre = 1;
                if (comp.com_ctipocomautoriza == 1 || comp.com_descuadre == 1)
                {
                    comp.com_estado = Constantes.cEstadoMayorizado ;//2
                }
                else
                    comp.com_estado = Constantes.cEstadoPorAutorizar;//3
            }
            else if (tipo == 2)
            {
                comp.com_estado = Constantes.cEstadoGrabado;//1
            }

            if (ComprobanteBLL.Update(comp) > 0)
                return true;
            else
                return false;
        }


        public static bool actualizar_estado(BLL transaccion, Comprobante comp, int tipo)
        {            
            if (tipo == 1)
            {
                if (CNT.comprobante_cuadrado(comp))//COMPROBANTE CUADRADO
                {
                    comp.com_descuadre = 0;
                    comp.com_estado = Constantes.cEstadoMayorizado;//2
                }
                else
                {
                    comp.com_descuadre = 1;
                    comp.com_estado = Constantes.cEstadoGrabado;//1
                }
                ///COMENTADO EL 17 de Dic 2016 por CSM 
                ///NO SE TIENE CLARO EL FUNCIONAMIENTO DE ESTO 
                ///PROVOCA QUE LOS COMPROBANTES SE GUARDEN MAYORIZADOS AUN CUANDO ESTAN
                ///DESCUADRADOS

                //if (comp.com_ctipocomautoriza == 1 || comp.com_descuadre == 1)
                //{
                //    comp.com_estado = Constantes.cEstadoMayorizado;//2
                //}
                //else
                //    comp.com_estado = Constantes.cEstadoPorAutorizar;//3
            }
            else if (tipo == 2)
            {
                comp.com_estado = Constantes.cEstadoGrabado;//1
            }
            
            if (ComprobanteBLL.Update(transaccion , comp) > 0)
                return true;
            else
                return false;
        }



        public static string cierre_automatico(int empresa, int periodo, int cuenta, DateTime fecha, string usuario)
        {

            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = empresa, uxe_empresa_key = empresa, uxe_usuario = usuario, uxe_usuario_key = usuario });

            int ctipocom = Constantes.cComDiario.cti_codigo;  // SE DEBE OBTENER DE ALGUN LADO ?????

            Dtipocom dti = General.GetDtipocom(empresa, fecha.Year, ctipocom, uxe.uxe_almacen.Value, uxe.uxe_puntoventa.Value);
            dti.dti_numero = dti.dti_numero.Value + 1;
            
            Comprobante c = new Comprobante();
            c.com_empresa = empresa;
            c.com_periodo = fecha.Year;
            c.com_tipodoc = Constantes.cDiario.tpd_codigo;
            c.com_ctipocom = ctipocom; 
            c.com_fecha = fecha;
            c.com_dia = fecha.Day;
            c.com_mes = fecha.Month;
            c.com_anio = fecha.Year;
            c.com_almacen = uxe.uxe_almacen;
            c.com_pventa = uxe.uxe_puntoventa;
            //c.com_codclipro = comp.com_codclipro;
            //c.com_tclipro = comp.com_tclipro;
            //c.com_agente = comp.com_agente;
            c.com_centro = Constantes.GetSinCentro().cen_codigo;
            //c.com_serie 
            c.com_numero = dti.dti_numero.Value;

            c.com_concepto = "CIERRE AUTOMATICO DE CUENTAS ESTADO RESULTADOS PERIODO " + fecha.Year;

            c.com_modulo = General.GetModulo(c.com_tipodoc); ;
            c.com_transacc = General.GetTransacc(c.com_tipodoc);
            c.com_nocontable = 0;
            c.com_estado = Constantes.cEstadoMayorizado;
            c.com_descuadre = 0;
            c.com_adestino = 0;
            c.com_doctran = General.GetNumeroComprobante(c);

            c.crea_usr = usuario;
            c.crea_fecha = DateTime.Now;
            c.mod_usr = usuario;
            c.mod_fecha = DateTime.Now;

            List<vSaldo> saldos = vSaldoBLL.GetAll1(new WhereParams("sal_empresa={0} and sal_periodo={1} and cue_genero between 4 and 8", empresa, periodo), "sal_cuenta");
            
            int debcredoc = (c.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;
            int debcrecan = (c.com_tipodoc == Constantes.cRecibo.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                c.com_codigo = ComprobanteBLL.InsertIdentity(transaction, c);//GUARDA CABECERA DIARIO
                int contador = 0;
                //decimal totaldeb = 0;
                //decimal totalcre = 0;
                decimal resultado = 0;

                foreach (vSaldo item in saldos)
                {
                    Dcontable dco = new Dcontable();
                    dco.dco_empresa = c.com_empresa;
                    dco.dco_comprobante = c.com_codigo;
                    dco.dco_secuencia = contador;
                    dco.dco_cuenta = item.sal_cuenta.Value;
                    dco.dco_centro = c.com_centro.Value;// item.sal_centro.Value;
                    dco.dco_transacc = c.com_transacc;// item.sal_transacc.Value;

                    //decimal saldo = 0;
                    //if (item.sal_debito.Value > 0)
                    //{
                    //    saldo = item.sal_debito.Value;
                    //    totaldeb += saldo;
                    //    dco.dco_debcre = Constantes.cCredito;
                    //}
                    //else if (item.sal_credito.Value > 0)
                    //{
                    //    saldo = item.sal_credito.Value;
                    //    totalcre += saldo;
                    //    dco.dco_debcre = Constantes.cDebito;
                    //}

                    decimal saldo = item.sal_debito.Value- item.sal_credito.Value;
                    resultado += saldo;

                    dco.dco_debcre = saldo > 0 ? Constantes.cCredito: Constantes.cDebito;
                    dco.dco_valor_nac = saldo > 0 ? saldo : saldo * -1;
                    dco.dco_valor_ext = saldo > 0 ? saldo : saldo * -1;
                    
                    //dco.dco_valor_ext = saldo;
                    //dco.dco_valor_nac = saldo;

                    dco.dco_concepto = "CIERRE AUTOMATICO PERIODO " + fecha.Year + " COMPROBANTE " + c.com_doctran;
                    dco.dco_almacen = c.com_almacen;
                    dco.dco_doctran = c.com_doctran;
                    dco.crea_usr = usuario;
                    dco.crea_fecha = DateTime.Now;
                    DcontableBLL.Insert(transaction, dco);
                    contador++;
                }

                //resultado = totaldeb - totalcre;

                //Contable que cruza con la cuenta de cierre

                Dcontable dc = new Dcontable();
                dc.dco_empresa = c.com_empresa;
                dc.dco_comprobante = c.com_codigo;
                dc.dco_secuencia = contador;
                dc.dco_cuenta = cuenta;
                dc.dco_centro = c.com_centro.Value ;
                dc.dco_transacc = c.com_transacc;
                dc.dco_debcre = resultado > 0 ? Constantes.cDebito: Constantes.cCredito;
                dc.dco_valor_nac = resultado > 0 ? resultado : resultado* -1;
                dc.dco_valor_ext = resultado > 0 ? resultado : resultado * -1;
                dc.dco_concepto = "CIERRE AUTOMATICO PERIODO " + fecha.Year + " COMPROBANTE " + c.com_doctran;
                dc.dco_almacen = c.com_almacen;
                dc.dco_doctran = c.com_doctran;
                dc.crea_usr = usuario;
                dc.crea_fecha = DateTime.Now;
                DcontableBLL.Insert(transaction, dc);
                contador++;


                //


                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE

                //if (Auto.actualizar_saldo(transaction, c, 1))
                //{
                //    transaction.Commit();
                //    return true;
                //}
                //else
                //    throw new Exception("ERROR no se puede actualzar saldos");
                General.save_historial(transaction, c);
                if (actualizar_saldos(transaction, empresa, periodo, 12, null, 1))
                {
                    transaction.Commit();



                    return c.com_doctran;
                }
                else
                {
                    return "ERROR";
                }

                

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHandling.Log.AddExepcion(ex);
                return "ERROR";

            }






            
         
        }


        public static bool actualiza_saldos_documentos(int empresa, int periodo, int? mes)
        {
            List<vDdocumento> lstdocumentos = vDdocumentoBLL.GetAll1(new WhereParams("com_empresa={0} and com_tipodoc in (14, 26) and com_estado = 2 and com_periodo = {1}", empresa, periodo), "");
            List<vDcancelacion> lstcancelaciones = vDcancelacionBLL.GetAll1(new WhereParams("com_empresa={0} and  com_tipodoc in (14, 26) and  com_estado = 2 and  com_periodo = {1}", empresa, periodo), "");

            List<Ddocumento> ddoup = new List<Ddocumento>(); 

            foreach (vDdocumento ddo in lstdocumentos)
            {
                decimal cancela = 0;
                List<vDcancelacion> ddocan = lstcancelaciones.FindAll(delegate (vDcancelacion c) { return c.dca_comprobante == ddo.ddo_comprobante; });
                foreach (vDcancelacion dca in ddocan)
                {
                    cancela += dca.montocancela.Value;
                }
                if (ddo.ddo_cancela != cancela || ddo.ddo_cancela>ddo.ddo_monto)
                {
                    Ddocumento d = new Ddocumento();
                    d.ddo_empresa = ddo.ddo_empresa.Value;
                    d.ddo_empresa_key = ddo.ddo_empresa.Value;
                    d.ddo_comprobante = ddo.ddo_comprobante.Value;
                    d.ddo_comprobante_key = ddo.ddo_comprobante.Value;
                    d.ddo_transacc = ddo.ddo_transacc.Value;
                    d.ddo_transacc_key = ddo.ddo_transacc.Value;
                    d.ddo_doctran = ddo.ddo_doctran;
                    d.ddo_doctran_key = ddo.ddo_doctran;
                    d.ddo_pago = ddo.ddo_pago.Value;
                    d.ddo_pago_key = ddo.ddo_pago.Value;

                    d.ddo_comprobante_guia = ddo.ddo_comprobante_guia;
                    d.ddo_codclipro = ddo.ddo_codclipro;
                    d.ddo_debcre = ddo.ddo_debcre;
                    d.ddo_tipo_cambio = ddo.ddo_tipo_cambio;
                    d.ddo_fecha_emi = ddo.ddo_fecha_emi;
                    d.ddo_fecha_ven = ddo.ddo_fecha_ven;
                    d.ddo_monto = ddo.ddo_monto;
                    d.ddo_monto_ext = ddo.ddo_monto_ext;
                    d.ddo_cancela = (cancela > ddo.ddo_monto) ? ddo.ddo_monto : cancela; //AQUI ACTUALIZA EL VALOR;
                    d.ddo_cancela_ext = d.ddo_cancela;//
                    d.ddo_cancelado = (d.ddo_monto == d.ddo_cancela.Value) ? 1 : 0;
                    d.ddo_agente = ddo.ddo_agente;
                    d.ddo_cuenta = ddo.ddo_cuenta;
                    d.ddo_modulo = ddo.ddo_modulo;
                    d.mod_fecha = DateTime.Now;
                    ddoup.Add(d);
                }
            }

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                foreach (Ddocumento item in ddoup)
                {
                    DdocumentoBLL.Update(transaction, item);
                }

                transaction.Commit();
                return true;





            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHandling.Log.AddExepcion(ex);
                return false;

            }

            
        }


        public static string cancel_facturas_cobro(int empresa, int? periodo, int? mes)
        {

                        

            WhereParams parametros = new WhereParams();
            parametros.where = " com_empresa={0} and com_tipodoc={1} and com_estado={2}  ";//and pol_codigo in (12,13,14,15) ";
            List<object> valores = new List<object>();
            valores.Add(empresa);
            valores.Add(Constantes.cFactura.tpd_codigo);
            valores.Add(Constantes.cEstadoMayorizado);

            if (periodo.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_periodo=  {" + valores.Count + "} ";
                valores.Add(periodo.Value);
            }
            if (mes.HasValue)
            {
                parametros.where += ((parametros.where != "") ? " and " : "") + " com_mes=  {" + valores.Count + "} ";
                valores.Add(mes.Value);
            }

            parametros.valores = valores.ToArray();
            List<Comprobante> lst = ComprobanteBLL.GetAll(parametros, "");

            Tipopago tpa = TipopagoBLL.GetByPK(new Tipopago { tpa_empresa = empresa, tpa_empresa_key = empresa, tpa_codigo = 18, tpa_codigo_key = 18 });
            int i = 0;
            int c = 0;
            ExceptionHandling.Log.AddLog1("Inicia " + lst.Count + " registros");

            foreach (Comprobante item in lst)
            {

                item.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = item.com_empresa, cdoc_empresa_key = item.com_empresa, cdoc_comprobante = item.com_codigo, cdoc_comprobante_key = item.com_codigo });
                item.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", item.com_empresa, item.com_codigo), "");
                item.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante={1}", item.com_empresa, item.com_codigo), "");

                decimal valortotal = 0;
                foreach (Ddocumento ddo in item.documentos)
                {
                    valortotal += ddo.ddo_monto.Value;
                }
                foreach (Dcancelacion dca in item.cancelaciones)
                {
                    valortotal -= dca.dca_monto.Value;
                }

                if ((item.ccomdoc.cdoc_politica==7 || item.ccomdoc.cdoc_politica == 12 || item.ccomdoc.cdoc_politica == 13 || item.ccomdoc.cdoc_politica == 14 || item.ccomdoc.cdoc_politica == 15 || item.ccomdoc.cdoc_politica == 35) && valortotal > 0)
                {

                    try
                    {
                        List<Drecibo> detalle = new List<Drecibo>();
                        Drecibo rec = new Drecibo();
                        rec.dfp_empresa = item.com_empresa;
                        rec.dfp_tipopago = tpa.tpa_codigo;
                        rec.dfp_monto = valortotal;
                        rec.dfp_nro_documento = item.com_doctran;
                        rec.dfp_debcre = Constantes.cDebito;
                        detalle.Add(rec);

                        Comprobante can = FAC.save_cancelacion_factura(item, detalle, item.com_fecha);

                        ExceptionHandling.Log.AddLog1(item.com_doctran + " - " + can.com_doctran + " Reg:" + i.ToString() + " Pos:" + c.ToString());
                        FAC.account_recibo(can);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandling.Log.AddExepcion(ex);

                    }
                }
                c++;
            }

            string mens = i + " recibos creados de " + lst.Count + " facturas de cobro";

            ExceptionHandling.Log.AddLog1(mens);

            return mens;
        }

        public static Comprobante create_recibo_faccobro(int empresa, DateTime fecha, int almacen, int pventa, int? codclipro, int? tclipro, int? agente,  int tipodoc, int ctipocom, int numero, int modulo, int transacc, string doctran, long faccodigo, string facdoctran, decimal factotal, List<Ddocumento> documentos ,  string usuario  )
        {
                        
            Comprobante c = new Comprobante();
            c.com_empresa = empresa;
            c.com_periodo = fecha.Year;
            c.com_tipodoc = tipodoc;
            c.com_ctipocom = ctipocom;
            c.com_fecha = fecha;
            c.com_dia = c.com_fecha.Day;
            c.com_mes = c.com_fecha.Month;
            c.com_anio = c.com_fecha.Year;
            c.com_almacen = almacen;
            c.com_pventa = pventa;
            c.com_codclipro = codclipro;
            c.com_tclipro = tclipro;
            c.com_agente = agente;
            c.com_centro = 1;
            c.com_numero = numero;

            c.com_concepto = "CANCELACION AUTOMATICA " + facdoctran;

            c.com_modulo = modulo;
            c.com_transacc = transacc;
            c.com_nocontable = 0;
            c.com_estado = Constantes.cEstadoGrabado;
            c.com_descuadre = 0;
            c.com_adestino = 0;
            c.com_doctran = doctran;

            c.crea_usr = usuario;
            c.crea_fecha = DateTime.Now;
            c.mod_usr = usuario;
            c.mod_fecha = DateTime.Now;


            c.total = new Total();
            c.total.tot_empresa = empresa;
            c.total.tot_total = factotal;


            int debcredoc = Constantes.cDebito;
            int debcrecan = Constantes.cCredito;


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                c.com_codigo = ComprobanteBLL.InsertIdentity(transaction, c);//GUARDA CABECERA CANCELACION
                
                decimal totalcancela = factotal;

                //GUARDA EL RECIBO
                Drecibo drec = new Drecibo();
                drec.dfp_empresa = c.com_empresa;
                drec.dfp_comprobante = c.com_codigo;
                drec.dfp_secuencia = 0;
                drec.dfp_tclipro = c.com_codclipro;
                drec.dfp_fecha_ven = fecha;
                drec.dfp_monto_ext = totalcancela;
                drec.dfp_ref_comprobante = faccodigo;                
                DreciboBLL.Insert(transaction, drec);  
                /////



                foreach (Ddocumento doc in documentos)
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
                }          

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


        public static bool actualiza_documentos(int empresa, int? almacen, int? pventa, int? persona, long? codigo, DateTime? desde, DateTime? hasta, int tipo)
        {


            WhereParams pardoc = new WhereParams();
            List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            List<object> valcan = new List<object>();

            pardoc.where = "com_empresa = " + empresa + " and com_estado= " + Constantes.cEstadoMayorizado;
            parcan.where = "cd.com_empresa = " + empresa + " and cd.com_estado= " + Constantes.cEstadoMayorizado + " and cc.com_estado=" + Constantes.cEstadoMayorizado;

            if (almacen.HasValue)
            {
                if (almacen.Value>0)
                {
                    pardoc.where += " and com_almacen = {" + valdoc.Count + "} ";
                    valdoc.Add(almacen);
                    parcan.where += " and cd.com_almacen = {" + valcan.Count + "} ";
                    valcan.Add(almacen);
                }
            }

            if (pventa.HasValue)
            {
                if (pventa.Value > 0)
                {
                    pardoc.where += " and com_pventa = {" + valdoc.Count + "} ";
                    valdoc.Add(pventa);
                    parcan.where += " and cd.com_pventa= {" + valcan.Count + "} ";
                    valcan.Add(pventa);
                }
            }

            if (persona.HasValue)
            {
                if (persona.Value > 0)
                {
                    pardoc.where += " and ddo_codclipro = {" + valdoc.Count + "} ";
                    valdoc.Add(persona);
                    parcan.where += " and ddo_codclipro = {" + valcan.Count + "} ";
                    valcan.Add(persona);
                }
            }

            if (codigo.HasValue)
            {
                if (codigo.Value > 0)
                {
                    pardoc.where += " and ddo_comprobante = {" + valdoc.Count + "} ";
                    valdoc.Add(codigo);
                    parcan.where += " and ddo_comprobante = {" + valcan.Count + "} ";
                    valcan.Add(codigo);
                }
            }

            if (desde.HasValue)
            {
                pardoc.where += " and com_fecha>={" + valdoc.Count + "}";
                valdoc.Add(desde);
                parcan.where += " and cd.com_fecha>={" + valcan.Count + "}";
                valcan.Add(desde);
            }

            if (hasta.HasValue)
            {
                pardoc.where += " and com_fecha<={" + valdoc.Count + "}";
                valdoc.Add(hasta);
                parcan.where += " and cd.com_fecha<={" + valcan.Count + "}";
                valcan.Add(hasta);
            }

            if (tipo==1)// 0= Todos 1=Pendientes 2=Cancelados
            {
                pardoc.where += " and ddo_cancela=0";
                parcan.where += " and ddo_cancela=0";
            }

            if (tipo==2)
            {
                pardoc.where += " and ddo_cancela=1";
                parcan.where += " and ddo_cancela=1";
            }



            //Cuadre proveedores fijos
            //pardoc.where += " and ddo_cuenta in (221)";
            //parcan.where += " and ddo_cuenta in (221)";

            pardoc.valores = valdoc.ToArray();
            parcan.valores = valcan.ToArray();

            List<vEstadoCuenta> lstdoc = vEstadoCuentaBLL.GetAllDoc(pardoc, "com_fecha");
            List<vEstadoCuenta> lstcan = vEstadoCuentaBLL.GetAllCan(parcan, "cd.com_fecha");
            
            List<Ddocumento> ddoup = new List<Ddocumento>();

            foreach (vEstadoCuenta ddo in lstdoc)
            {
                List<vEstadoCuenta> ddocan = lstcan.FindAll(delegate (vEstadoCuenta c) { return c.dca_empresa == ddo.ddo_empresa && c.dca_comprobante == ddo.ddo_comprobante && c.dca_transacc == ddo.ddo_transacc && c.dca_doctran == ddo.ddo_doctran && c.dca_pago == ddo.ddo_pago; });
                decimal cancela = ddocan.Sum(s => s.dca_monto ?? 0);
                
                if (ddo.ddo_cancela != cancela || ddo.ddo_cancela > ddo.ddo_monto || (ddo.ddo_cancela== ddo.ddo_monto && ddo.ddo_cancelado==0) || (ddo.dca_monto!= cancela && ddo.ddo_cancelado==1))
                {
                    Ddocumento d = new Ddocumento();
                    d.ddo_empresa = ddo.ddo_empresa.Value;
                    d.ddo_empresa_key = ddo.ddo_empresa.Value;
                    d.ddo_comprobante = ddo.ddo_comprobante.Value;
                    d.ddo_comprobante_key = ddo.ddo_comprobante.Value;
                    d.ddo_transacc = ddo.ddo_transacc.Value;
                    d.ddo_transacc_key = ddo.ddo_transacc.Value;
                    d.ddo_doctran = ddo.ddo_doctran;
                    d.ddo_doctran_key = ddo.ddo_doctran;
                    d.ddo_pago = ddo.ddo_pago.Value;
                    d.ddo_pago_key = ddo.ddo_pago.Value;
                    d.ddo_comprobante_guia = ddo.ddo_comprobante_guia;
                    d.ddo_codclipro = ddo.ddo_codclipro;
                    d.ddo_debcre = ddo.ddo_debcre;
                    d.ddo_tipo_cambio = ddo.ddo_tipo_cambio;
                    d.ddo_fecha_emi = ddo.ddo_fecha_emi;
                    d.ddo_fecha_ven = ddo.ddo_fecha_ven;
                    d.ddo_monto = ddo.ddo_monto;
                    d.ddo_monto_ext = ddo.ddo_monto_ext;
                    d.ddo_cancela = (cancela > ddo.ddo_monto) ? ddo.ddo_monto : cancela; //AQUI ACTUALIZA EL VALOR;
                    d.ddo_cancela_ext = d.ddo_cancela;//
                    d.ddo_cancelado = (d.ddo_monto == d.ddo_cancela.Value) ? 1 : 0;
                    d.ddo_agente = ddo.ddo_agente;
                    d.ddo_cuenta = ddo.ddo_cuenta;
                    d.ddo_modulo = ddo.ddo_modulo;
                    d.mod_fecha = DateTime.Now;
                    ddoup.Add(d);
                }
            }

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                foreach (Ddocumento item in ddoup)
                {
                    DdocumentoBLL.Update(transaction, item);
                }

                transaction.Commit();
                return true;





            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHandling.Log.AddExepcion(ex);
                return false;

            }


        }


        public static bool actualiza_documentos(int empresa, int? persona, long? codigo, int tipo, decimal? monto)
        {

            string modificadocumento = Constantes.GetParameter("modificadocumento");

            bool moddoc = true;
            if (!string.IsNullOrEmpty(modificadocumento))
                moddoc = modificadocumento == "1";

            WhereParams pardoc = new WhereParams();
            //List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            //List<object> valcan = new List<object>();

            pardoc.where = "ddo_empresa = " + empresa + " and ddo_comprobante = " + codigo;
            parcan.where = "dca_empresa = " + empresa + " and dca_comprobante= " + codigo;                        

            List<Ddocumento> lstdoc = DdocumentoBLL.GetAll(pardoc, "ddo_pago");
            List<Dcancelacion> lstcan = DcancelacionBLL.GetAll(parcan, "dca_pago");
            List<Total> lsttot = new List<Total>(); 
            string wheretot = "";
            foreach (Dcancelacion item in lstcan)
            {
                wheretot += (wheretot != "" ? "," : "") + item.dca_comprobante_can;
            }

            if (wheretot != "")
            {
                wheretot = "(" + wheretot + ")";
                lsttot = TotalBLL.GetAll("tot_empresa=" + empresa + " and tot_comprobante in " + wheretot, "");
            }

          
            //ACTUALIZA DOCUMENTOS
           
            foreach (Ddocumento ddo in lstdoc)
            {
                List<Dcancelacion> dcas = lstcan.FindAll(delegate (Dcancelacion c) { return c.dca_empresa == ddo.ddo_empresa && c.dca_comprobante == ddo.ddo_comprobante && c.dca_transacc == ddo.ddo_transacc && c.dca_doctran == ddo.ddo_doctran && c.dca_pago == ddo.ddo_pago; });
                decimal cancela = dcas.Sum(s => s.dca_monto ?? 0);
                if (ddo.ddo_cancela != cancela || ddo.ddo_cancela > ddo.ddo_monto || (ddo.ddo_cancela == ddo.ddo_monto && ddo.ddo_cancelado == 0))
                {
                    ddo.ddo_empresa_key = ddo.ddo_empresa;
                    ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                    ddo.ddo_transacc_key = ddo.ddo_transacc;
                    ddo.ddo_doctran_key = ddo.ddo_doctran;
                    ddo.ddo_pago_key = ddo.ddo_pago;
                    ddo.ddo_cancela = (cancela > ddo.ddo_monto) ? ddo.ddo_monto : cancela; //AQUI ACTUALIZA EL VALOR;
                    ddo.ddo_cancela_ext = ddo.ddo_cancela;//
                    ddo.ddo_cancelado = (ddo.ddo_monto == ddo.ddo_cancela.Value) ? 1 : 0;
                }
            }

            decimal totalddo = lstdoc.Sum(s => s.ddo_monto ?? 0);
            decimal totaldca = lstcan.Sum(s => s.dca_monto ?? 0);
            decimal saldo = totalddo - totaldca;

            //SI EXISTE MONTO ADICIONAL MODIFICA CANCELACIONES Y DOCUMENTOS PARA PODER AGREGAR EL MONTO 

            if (monto > saldo && moddoc) //Agrega espacio para el monto necesario
            {
                monto = monto - saldo;

                foreach (Ddocumento ddo in lstdoc)
                {
                    if (monto > 0)
                    {
                        List<Dcancelacion> dcas = lstcan.FindAll(delegate (Dcancelacion c) { return c.dca_empresa == ddo.ddo_empresa && c.dca_comprobante == ddo.ddo_comprobante && c.dca_transacc == ddo.ddo_transacc && c.dca_doctran == ddo.ddo_doctran && c.dca_pago == ddo.ddo_pago; });

                        foreach (Dcancelacion dca in dcas)
                        {
                            Total tot = lsttot.Find(delegate (Total t) { return t.tot_comprobante == dca.dca_comprobante_can; });

                            if (dca.dca_monto >= monto)
                            {
                                dca.dca_monto = dca.dca_monto - monto;
                                tot.tot_total = tot.tot_total - monto.Value;
                                monto = 0;
                                
                            }
                            else
                            {
                                monto = monto - dca.dca_monto;
                                tot.tot_total = tot.tot_total - dca.dca_monto.Value;
                                dca.dca_monto = 0;
                            }

                            tot.tot_empresa_key = tot.tot_empresa;
                            tot.tot_comprobante_key = tot.tot_comprobante;

                            dca.dca_empresa_key = dca.dca_empresa;
                            dca.dca_comprobante_key = dca.dca_comprobante;
                            dca.dca_transacc_key = dca.dca_transacc;
                            dca.dca_doctran_key = dca.dca_doctran;
                            dca.dca_pago_key = dca.dca_pago;
                            dca.dca_comprobante_can_key = dca.dca_comprobante_can;
                            dca.dca_secuencia_key = dca.dca_secuencia;

                        }

                        decimal cancela = dcas.Sum(s => s.dca_monto ?? 0);

                        if (ddo.ddo_cancela != cancela || ddo.ddo_cancela > ddo.ddo_monto || (ddo.ddo_cancela == ddo.ddo_monto && ddo.ddo_cancelado == 0))
                        {
                            ddo.ddo_empresa_key = ddo.ddo_empresa;
                            ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                            ddo.ddo_transacc_key = ddo.ddo_transacc;
                            ddo.ddo_doctran_key = ddo.ddo_doctran;
                            ddo.ddo_pago_key = ddo.ddo_pago;
                            ddo.ddo_cancela = (cancela > ddo.ddo_monto) ? ddo.ddo_monto : cancela; //AQUI ACTUALIZA EL VALOR;
                            ddo.ddo_cancela_ext = ddo.ddo_cancela;//
                            ddo.ddo_cancelado = (ddo.ddo_monto == ddo.ddo_cancela.Value) ? 1 : 0;
                        }
                    }
                }


            }


         

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                foreach (Total item in lsttot)
                {
                    TotalBLL.Update(transaction, item);
                }
                foreach (Dcancelacion  item in lstcan)
                {
                    DcancelacionBLL.Update(transaction, item);
                }

                foreach (Ddocumento item in lstdoc)
                {
                    DdocumentoBLL.Update(transaction, item);
                }

                transaction.Commit();
                return true;





            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHandling.Log.AddExepcion(ex);
                return false;

            }


        }


        public static bool actualiza_documentos(int empresa, int? persona, long? codigo)
        {

            WhereParams pardoc = new WhereParams();
            //List<object> valdoc = new List<object>();
            WhereParams parcan = new WhereParams();
            //List<object> valcan = new List<object>();

            pardoc.where = "ddo_empresa = " + empresa + " and com_estado = 2";
            parcan.where = "dca_empresa = " + empresa + " and com_estado = 2" ;
            if (persona.HasValue)
            {
                pardoc.where += " and ddo_codclipro=" + persona;
                parcan.where += " and ddo_codclipro=" + persona;
            }
            if ((codigo??0)>0)
            {
                pardoc.where += " and ddo_comprobante=" + codigo;
                parcan.where += " and ddo_comprobante=" + codigo;
            }
            List<Ddocumento> lstdoc = DdocumentoBLL.GetAll(pardoc, "");
            List<Dcancelacion> lstcan = DcancelacionBLL.GetAll(parcan, "");
            List<Ddocumento> lstdocup = new List<Ddocumento>();

            foreach (Ddocumento ddo in lstdoc)
            {
                List<Dcancelacion> dcas = lstcan.FindAll(delegate (Dcancelacion c) { return c.dca_empresa == ddo.ddo_empresa && c.dca_comprobante == ddo.ddo_comprobante && c.dca_transacc == ddo.ddo_transacc && c.dca_doctran == ddo.ddo_doctran && c.dca_pago == ddo.ddo_pago; });
                decimal cancela = dcas.Sum(s => s.dca_monto ?? 0);
                if (ddo.ddo_cancela != cancela || ddo.ddo_cancela > ddo.ddo_monto || (ddo.ddo_cancela == ddo.ddo_monto && ddo.ddo_cancelado == 0))
                {
                    ddo.ddo_empresa_key = ddo.ddo_empresa;
                    ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                    ddo.ddo_transacc_key = ddo.ddo_transacc;
                    ddo.ddo_doctran_key = ddo.ddo_doctran;
                    ddo.ddo_pago_key = ddo.ddo_pago;
                    ddo.ddo_cancela = (cancela > ddo.ddo_monto) ? ddo.ddo_monto : cancela; //AQUI ACTUALIZA EL VALOR;
                    ddo.ddo_cancela_ext = ddo.ddo_cancela;//
                    ddo.ddo_cancelado = (ddo.ddo_monto == ddo.ddo_cancela.Value) ? 1 : 0;
                    lstdocup.Add(ddo);
                }
            }




            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();                
                foreach (Ddocumento item in lstdocup)
                {
                    DdocumentoBLL.Update(transaction, item);
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHandling.Log.AddExepcion(ex);
                return false;

            }


        }


        public static string actualiza_cancelaciones(int empresa, int? persona, long? codigo)
        {

            WhereParams parcon = new WhereParams();
            WhereParams parcan = new WhereParams();
            parcon.where = "dco_empresa = " + empresa + " and dco_ddo_comproba is not null and dco_ddo_comproba >0 and dco_ddo_comproba <> com_codigo and com_estado = 2";
            parcan.where = "dca_empresa = " + empresa + " and com_estado = 2";
            if (persona.HasValue)
            {
                parcon.where += " and dco_cliente=" + persona;
                parcan.where += " and ddo_codclipro=" + persona;
            }
            if ((codigo ?? 0) > 0)
            {
                parcon.where += " and dco_ddo_comproba=" + codigo;
                parcan.where += " and ddo_comprobante=" + codigo;
            }
            List<Dcancelacion> lstcan = DcancelacionBLL.GetAll(parcan, "");
            List<Dcontable> lstdco = DcontableBLL.GetAll(parcon, "");



            //List<Dcontable> lstdocup = new List<Ddocumento>();
            StringBuilder html = new StringBuilder();
            int i = 0;

            foreach (Dcontable dco in lstdco)
            {
                i++;
                Dcancelacion dca = lstcan.Find(f => f.dca_comprobante == dco.dco_ddo_comproba && f.dca_pago == dco.dco_nropago && f.dca_comprobante_can == dco.dco_comprobante);
                if (dca==null)
                {
                    html.AppendFormat("{0} {1} no existe anexo<br>", dco.dco_doctran, dco.dco_compdoctran);
                }
            }


            /*

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();
                foreach (Ddocumento item in lstdocup)
                {
                    DdocumentoBLL.Update(transaction, item);
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ExceptionHandling.Log.AddExepcion(ex);
                return false;

            }*/
            return html.ToString();


        }











        public static void AddLogCancela(string texto)
        {
            string filename = string.Format("{0:00}{1:00}{2:0000}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            string path = @"C:\LOG";

            StreamWriter sw;
            sw = File.AppendText(path + @"\" + filename);
            sw.WriteLine(DateTime.Now.ToString() + "," + texto);
            sw.Close();
        }


        public static string CancelaFacturas(int empresa, int cuenta, string[] facturas, DateTime fecha, string usuario)
        {


            AddLogCancela("Inicia " + facturas.Length);

            List<Dcontable> lstcon = new List<Dcontable>();
            List<Ddocumento> lstdocumento = new List<Ddocumento>();
            List<Dcancelacion> lstcancela = new List<Dcancelacion>();

            decimal suma = 0;
            for (int i = 0; i < facturas.Length; i++)
            {
                string where = "";
                string wheredocs = "";
                string wherecan = "";


                where = "com_empresa=" + empresa + " and com_doctran = '" + facturas[i] + "'";
                wheredocs = " ddo_empresa=" + empresa + " and ddo_doctran = '" + facturas[i] + "'";
                wherecan = "  dca_empresa=" + empresa + " and dca_doctran = '" + facturas[i] + "'";

                List<Comprobante> lst = ComprobanteBLL.GetAll(where, "");
                List<Ddocumento> lstdoc = DdocumentoBLL.GetAll(wheredocs, "");
                List<Dcancelacion> lstcan = DcancelacionBLL.GetAll(wherecan, "");

                int seccan = 1;
                if (lstcan.Count>0)
                    seccan = lstcan.Max(m => m.dca_secuencia) + 1;

                

                AddLogCancela("Factura " + facturas[i] + " pos:" + i);

                foreach (Comprobante item in lst)
                {
                    List<Ddocumento> documentos = lstdoc.FindAll(delegate (Ddocumento d) { return d.ddo_doctran == item.com_doctran; });
                    foreach (Ddocumento doc in documentos)
                    {

                        List<Dcancelacion> cancelaciones = lstcan.FindAll(delegate (Dcancelacion ca) { return ca.dca_comprobante == doc.ddo_comprobante && ca.dca_transacc == doc.ddo_transacc && ca.dca_doctran == doc.ddo_doctran && ca.dca_pago == doc.ddo_pago; });

                        decimal can = cancelaciones.Sum(s => s.dca_monto ?? 0);
                        decimal dif = (doc.ddo_monto ?? 0) - can;

                        if (dif > 0)
                        {

                            AddLogCancela("Factura " + facturas[i] + " Documento:" + doc.ddo_doctran + ", pago:" + doc.ddo_pago + " saldo:" + dif);


                            Dcontable dco = new Dcontable();
                            dco.dco_empresa = empresa;
                            dco.dco_secuencia = lstcon.Count();
                            dco.dco_cuenta = doc.ddo_cuenta.Value;
                            dco.dco_centro = item.com_centro.Value;
                            dco.dco_transacc = doc.ddo_transacc;
                            dco.dco_debcre = Constantes.cCredito;
                            dco.dco_valor_nac = dif;
                            dco.dco_concepto = item.com_doctran + " CANCELACIÓN AUTO";
                            dco.dco_almacen = item.com_almacen;
                            dco.dco_cliente = item.com_codclipro;
                            dco.dco_doctran = item.com_doctran;
                            dco.dco_nropago = doc.ddo_pago;
                            dco.dco_fecha_vence = doc.ddo_fecha_ven;
                            dco.dco_ddo_comproba = (int)doc.ddo_comprobante;
                            dco.crea_fecha = DateTime.Now;
                            dco.crea_usr = "auto";
                            suma = suma + dif;
                            lstcon.Add(dco);

                            Dcancelacion dca = new Dcancelacion();
                            dca.dca_empresa = empresa;
                            dca.dca_comprobante = doc.ddo_comprobante;
                            dca.dca_transacc = doc.ddo_transacc;
                            dca.dca_doctran = doc.ddo_doctran;
                            dca.dca_pago = doc.ddo_pago;
                            //dca.dca_comprobante_can = comp.com_codigo;
                            dca.dca_secuencia = seccan;
                            //dca.dca_debcre = (item.dco_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                            dca.dca_debcre = dco.dco_debcre;
                            dca.dca_transacc_can = dco.dco_transacc;
                            dca.dca_monto = dco.dco_valor_nac;
                            dca.dca_monto_ext = dco.dco_valor_ext.HasValue ? dco.dco_valor_ext.Value : dco.dco_valor_nac;
                            lstcancela.Add(dca);
                            seccan++;



                            doc.ddo_cancela = (doc.ddo_cancela ?? 0) + dca.dca_monto;
                            doc.ddo_cancela_ext = doc.ddo_cancela;
                            doc.ddo_monto_ext = doc.ddo_monto;
                            if (doc.ddo_cancela >= doc.ddo_monto)
                                doc.ddo_cancelado = 1;

                            lstdocumento.Add(doc);


                        }
                    }
                }

            }


            AddLogCancela("Fin " + facturas.Length);

            AddLogCancela("Inicia SAVING DB ");



            Usuarioxempresa uxe = UsuarioxempresaBLL.GetByPK(new Usuarioxempresa { uxe_empresa = empresa, uxe_empresa_key = empresa, uxe_usuario = usuario, uxe_usuario_key = usuario });






            int ctipocom = Constantes.cComDiario.cti_codigo;  // SE DEBE OBTENER DE ALGUN LADO ?????

            Dtipocom dti = new Dtipocom();
            dti.dti_empresa = empresa;
            dti.dti_empresa_key = empresa;
            dti.dti_periodo = fecha.Year;
            dti.dti_periodo_key = fecha.Year;
            dti.dti_ctipocom = ctipocom;
            dti.dti_ctipocom_key = ctipocom;
            dti.dti_almacen = uxe.uxe_almacen.Value;
            dti.dti_almacen_key = uxe.uxe_almacen.Value;
            dti.dti_puntoventa = uxe.uxe_puntoventa.Value;
            dti.dti_puntoventa_key = uxe.uxe_puntoventa.Value;
            dti = DtipocomBLL.GetByPK(dti);
            dti.dti_numero = dti.dti_numero.Value + 1;

            Comprobante c = new Comprobante();
            c.com_empresa = empresa;
            c.com_periodo = fecha.Year;
            c.com_tipodoc = Constantes.cDiario.tpd_codigo;
            c.com_ctipocom = ctipocom;
            c.com_fecha = fecha;
            c.com_dia = fecha.Day;
            c.com_mes = fecha.Month;
            c.com_anio = fecha.Year;
            c.com_almacen = uxe.uxe_almacen;
            c.com_pventa = uxe.uxe_puntoventa;
            //c.com_codclipro = comp.com_codclipro;
            //c.com_tclipro = comp.com_tclipro;
            //c.com_agente = comp.com_agente;
            c.com_centro = Constantes.GetSinCentro().cen_codigo;
            //c.com_serie 
            c.com_numero = dti.dti_numero.Value;

            c.com_concepto = "CANCELACION AUTOMATICA DE CUENTAS POR COBRAR " + fecha.ToShortDateString();

            c.com_modulo = General.GetModulo(c.com_tipodoc); ;
            c.com_transacc = General.GetTransacc(c.com_tipodoc);
            c.com_nocontable = 0;
            c.com_estado = Constantes.cEstadoMayorizado;
            c.com_descuadre = 0;
            c.com_adestino = 0;
            c.com_doctran = General.GetNumeroComprobante(c);

            c.crea_usr = usuario;
            c.crea_fecha = DateTime.Now;
            c.mod_usr = usuario;
            c.mod_fecha = DateTime.Now;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {
                transaction.BeginTransaction();

                c.com_codigo = ComprobanteBLL.InsertIdentity(transaction, c);//GUARDA CABECERA DIARIO
                int contador = 0;

                foreach (Dcontable dco in lstcon)
                {
                    dco.dco_comprobante = c.com_codigo;
                    DcontableBLL.Insert(transaction, dco);
                }


                if (suma > 0)
                {
                    Dcontable dco = new Dcontable();
                    dco.dco_empresa = empresa;
                    dco.dco_comprobante = c.com_codigo;
                    dco.dco_secuencia = lstcon.Count();
                    dco.dco_cuenta = cuenta;
                    dco.dco_centro = 1;
                    dco.dco_transacc = 1;
                    dco.dco_debcre = Constantes.cDebito;
                    dco.dco_valor_nac = suma;
                    dco.dco_concepto = "  CAJA CANCELACIÓN AUTO";
                    dco.dco_almacen = c.com_almacen;
                    dco.dco_doctran = c.com_doctran;
                    dco.crea_fecha = DateTime.Now;
                    dco.crea_usr = "auto";
                    DcontableBLL.Insert(transaction, dco);
                }


                foreach (Dcancelacion dca in lstcancela)
                {
                    dca.dca_comprobante_can = c.com_codigo;
                    DcancelacionBLL.Insert(transaction, dca);
                }


                foreach (Ddocumento doc in lstdocumento)
                {
                    doc.ddo_empresa_key = doc.ddo_empresa;
                    doc.ddo_comprobante_key = doc.ddo_comprobante;
                    doc.ddo_transacc_key = doc.ddo_transacc;
                    doc.ddo_doctran_key = doc.ddo_doctran;
                    doc.ddo_pago_key = doc.ddo_pago;

                    DdocumentoBLL.Update(transaction, doc);
                }


                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE

                //if (Auto.actualizar_saldo(transaction, c, 1))
                //{
                //    transaction.Commit();
                //    return true;
                //}
                //else
                //    throw new Exception("ERROR no se puede actualzar saldos");
                //General.save_historial(transaction, c);


                if (Auto.actualizar_saldo(transaction, c, 1))
                    transaction.Commit();
                else
                    throw new Exception("ERROR no se puede actualzar saldos");

                transaction.Commit();

                AddLogCancela("FIN SAVING DB " + c.com_doctran);

                return c.com_doctran;



            }
            catch (Exception ex)
            {
                transaction.Rollback();
                AddLogCancela("ERROR SAVING DB " + ex.Message);
                ExceptionHandling.Log.AddExepcion(ex);
                return "ERROR";

            }


        }

        
        public static string SetDuplicados(int empresa)
        {
            List<Comprobante> lst = ComprobanteBLL.GetAll("com_doctran in (select com_doctran from comprobante group by com_numero, com_doctran having count(com_doctran)>1)", "com_doctran,com_estado");

            List<Comprobante> lstup = new List<Comprobante>();
            int i = 0;
            int r = 0;
            foreach (Comprobante item in lst)
            {
                if (item.com_numero > 0)
                {
                    List<Comprobante> lstdup = lst.FindAll(delegate (Comprobante c) { return c.com_doctran == item.com_doctran && c.com_codigo != item.com_codigo; });

                    int a = 1;
                    foreach (Comprobante com in lstdup)
                    {
                        com.com_numero = com.com_numero * a * -1;
                        com.com_empresa_key = com.com_empresa;
                        com.com_codigo_key = com.com_codigo;
                        ComprobanteBLL.Update(com);
                        i++;
                        a++;
                    }
                }
                r++;
            }
            return i + " comprobantes actualizados...";


        }




        public static string AnularRetencionesDuplicadas(int empresa)
        {

            List<Drecibo> lst = DreciboBLL.GetAll("dfp_nro_documento in (select dfp_nro_documento from drecibo inner join comprobante on dfp_empresa = com_empresa and dfp_comprobante = com_codigo where com_tipodoc = 6  and dfp_tipopago in (7, 8, 9, 10, 27, 17) and com_estado = 2 group by dfp_nro_documento, com_codclipro having count(dfp_nro_documento) > 1)", "");

            List<String> lstnro = new List<string>();

            StringBuilder html = new StringBuilder();

            int i = 0;
            foreach (Drecibo dfp in lst)
            {
                if (lstnro.Contains(dfp.dfp_nro_documento))
                {
                    Comprobante com = new Comprobante();
                    com.com_empresa = dfp.dfp_empresa;
                    com.com_codigo = dfp.dfp_comprobante;

                    html.AppendFormat("Cod:{0}, Nro:{1}<br>", dfp.dfp_comprobante, dfp.dfp_nro_documento);

                    General.AnulaComprobante(com);
                    i++;

                }
                else
                    lstnro.Add(dfp.dfp_nro_documento);                

            }

            html.AppendFormat("{0} comprobantes anulados...", i);

            return html.ToString();

        }




    }
}

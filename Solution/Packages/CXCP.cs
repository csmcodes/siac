using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;


namespace Packages
{
    public class CXCP
    {

        public static List<Dcontable> contables_socio(Comprobante comp, int socio, int debcr)
        {
            List<Dcontable> lst = new List<Dcontable>();
            List<Ddocumento> lst2 = new List<Ddocumento>();
            lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
            int pago = lst2.Count;
            foreach (Drecibo rec in comp.recibos)
            {

                pago += 1;
                int cuenta = 0;
                Tipopago tp = new Tipopago();
                tp.tpa_empresa_key = rec.dfp_empresa;
                tp.tpa_codigo_key = rec.dfp_tipopago;
                tp = TipopagoBLL.GetByPK(tp);
                tp.tpa_empresa = tp.tpa_empresa;
                tp.tpa_codigo_key = tp.tpa_codigo;
                if (tp.tpa_contabiliza == 1)
                {
                    Almacen alm = new Almacen();
                    alm.alm_codigo_key = comp.com_almacen ?? 0;
                    alm.alm_empresa_key = comp.com_empresa;
                    alm = AlmacenBLL.GetByPK(alm);
                    cuenta = alm.alm_cuentacaja.Value;
                }
                if (tp.tpa_contabiliza == 4)
                {
                    Banco ban = new Banco();
                    ban.ban_codigo = rec.dfp_banco.Value;
                    ban.ban_codigo_key = rec.dfp_banco.Value;
                    ban.ban_empresa = comp.com_empresa;
                    ban.ban_empresa_key = comp.com_empresa_key;
                    ban = BancoBLL.GetByPK(ban);

                    cuenta = ban.ban_cuenta.Value;

                }
                if (tp.tpa_contabiliza == 3)
                {
                    Personaxtipo pxt = new Personaxtipo();
                    pxt.pxt_empresa_key = comp.com_empresa;
                    pxt.pxt_persona_key = tp.tpa_codclipro ?? 0;
                    pxt.pxt_tipo_key = comp.com_tclipro ?? 0;
                    pxt = PersonaxtipoBLL.GetByPK(pxt);
                    Cuetransacc cuetransac = new Cuetransacc();
                    cuetransac.ctr_categoria_key = pxt.pxt_cat_persona ?? 0;
                    cuetransac.ctr_empresa_key = pxt.pxt_empresa;
                    cuetransac.ctr_transacc_key = 1;
                    cuetransac = CuetransaccBLL.GetByPK(cuetransac);
                    cuenta = cuetransac.ctr_cuenta;

                    Ddocumento doc2 = new Ddocumento();
                    doc2.ddo_empresa = comp.com_empresa;
                    doc2.ddo_comprobante = comp.com_codigo;
                    doc2.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                    doc2.ddo_doctran = comp.com_doctran;

                    doc2.ddo_pago = pago;
                    doc2.ddo_codclipro = tp.tpa_codclipro.Value;
                    doc2.ddo_debcre = debcr;
                    doc2.ddo_fecha_emi = comp.com_fecha;
                    doc2.ddo_fecha_ven = rec.dfp_fecha_ven ?? comp.com_fecha;
                    doc2.ddo_monto = rec.dfp_monto;
                    doc2.ddo_monto_ext = rec.dfp_monto;
                    doc2.ddo_cancela = 0;
                    doc2.ddo_cancelado = 0;
                    doc2.ddo_cuenta = cuetransac.ctr_cuenta;
                    doc2.ddo_agente = comp.com_agente;
                    doc2.ddo_modulo = comp.com_modulo;
                    DdocumentoBLL.Insert(doc2);
                }

                if (tp.tpa_contabiliza == 2)
                {
                    if (tp.tpa_transacc == 4)
                    {
                        Banco ban = new Banco();
                        ban.ban_codigo = rec.dfp_banco.Value;
                        ban.ban_codigo_key = rec.dfp_banco.Value;
                        ban.ban_empresa = comp.com_empresa;
                        ban.ban_empresa_key = comp.com_empresa_key;
                        ban = BancoBLL.GetByPK(ban);

                        cuenta = ban.ban_cuenta.Value;
                    }
                    else
                    {

                        Personaxtipo pxt = new Personaxtipo();
                        pxt.pxt_empresa_key = comp.com_empresa;
                        pxt.pxt_persona_key = comp.com_codclipro ?? 0;
                        pxt.pxt_tipo_key = comp.com_tclipro ?? 0;
                        pxt = PersonaxtipoBLL.GetByPK(pxt);
                        Cuetransacc cuetransac = new Cuetransacc();
                        cuetransac.ctr_categoria_key = pxt.pxt_cat_persona ?? 0;
                        cuetransac.ctr_empresa_key = pxt.pxt_empresa;
                        cuetransac.ctr_transacc_key = tp.tpa_transacc;
                        cuetransac = CuetransaccBLL.GetByPK(cuetransac);
                        cuenta = cuetransac.ctr_cuenta;

                        Ddocumento doc2 = new Ddocumento();
                        doc2.ddo_empresa = comp.com_empresa;
                        doc2.ddo_comprobante = comp.com_codigo;
                        doc2.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                        doc2.ddo_doctran = comp.com_doctran;
                        doc2.ddo_pago = pago;
                        doc2.ddo_codclipro = comp.com_codclipro;
                        doc2.ddo_debcre = debcr;
                        doc2.ddo_fecha_emi = comp.com_fecha;
                        doc2.ddo_fecha_ven = rec.dfp_fecha_ven ?? comp.com_fecha;
                        doc2.ddo_monto = rec.dfp_monto;
                        doc2.ddo_monto_ext = rec.dfp_monto;
                        doc2.ddo_cancela = 0;
                        doc2.ddo_cancelado = 0;
                        doc2.ddo_cuenta = cuetransac.ctr_cuenta;
                        doc2.ddo_agente = comp.com_agente;
                        doc2.ddo_modulo = comp.com_modulo;
                        DdocumentoBLL.Insert(doc2);
                    }
                }
                if (tp.tpa_contabiliza == 5)
                {
                    cuenta = tp.tpa_cuenta.Value;
                }
                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = cuenta;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcr;
                dco.dco_valor_nac = rec.dfp_monto;
                dco.dco_tipo_cambio = rec.dfp_tipo_cambio;

                string cheque = (!string.IsNullOrEmpty(rec.dfp_beneficiario) ? " BENEFICIARIO: " + rec.dfp_beneficiario : "") + (!string.IsNullOrEmpty(rec.dfp_nro_cheque) ? " CHEQUE:" + rec.dfp_nro_cheque : "");

                dco.dco_concepto = "CANCELACION " + comp.com_doctran + cheque;
                dco.dco_almacen = comp.com_almacen;
                //dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;

        }


        public static List<Dcontable> contables_detalleobl(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();
            foreach (Dcomdoc item in comp.ccomdoc.detalle)
            {
                int vcuenta = item.ddoc_cuenta.Value;//CUENTA DE VENTA
                decimal vvalor = item.ddoc_total;
                if (vvalor > 0)
                {

                    Dcontable dco = new Dcontable();
                    dco.dco_empresa = comp.com_empresa;
                    dco.dco_comprobante = comp.com_codigo;
                    dco.dco_cuenta = vcuenta;
                    dco.dco_centro = comp.com_centro.Value;
                    dco.dco_transacc = comp.com_transacc;
                    dco.dco_debcre = debcre;
                    dco.dco_valor_nac = vvalor;
                    //dco.dco_valor_ext = 
                    dco.dco_tipo_cambio = comp.com_tipo_cambio;
                    dco.dco_concepto = comp.ccomdoc.cdoc_nombre + " - " + item.ddoc_cuentanombre;
                    dco.dco_almacen = comp.com_almacen;
                    dco.dco_cliente = null;
                    dco.dco_agente = null;
                    dco.dco_doctran = comp.com_doctran;
                    dco.dco_nropago = null;
                    dco.dco_fecha_vence = null;
                    dco.dco_ddo_comproba = null;
                    dco.dco_ddo_transacc = null;
                    dco.dco_producto = null;
                    dco.dco_bodega = null;
                    lst.Add(dco);
                }

            }

            return lst;
        }

        public static List<Dcontable> contables_detalle(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();
            List<Gproducto> lstgproductos = GproductoBLL.GetAll(new WhereParams("gpr_empresa={0}", comp.com_empresa), "");
            foreach (Dcomdoc item in comp.ccomdoc.detalle)
            {
                Gproducto gpro = lstgproductos.Find(delegate(Gproducto g) { return g.gpr_codigo == item.ddoc_productogrupo; });
                if (gpro != null)
                {
                    int vcuenta = gpro.gpr_cta_venta.Value;//CUENTA DE VENTA
                    int vcuentades = gpro.gpr_cta_des.Value;//CUENTA DE DESCUENTO
                    decimal vvalor = item.ddoc_total;

                    if (vvalor > 0)
                    {

                        Dcontable dco = new Dcontable();
                        dco.dco_empresa = comp.com_empresa;
                        dco.dco_comprobante = comp.com_codigo;
                        dco.dco_cuenta = gpro.gpr_cta_venta.Value;
                        dco.dco_centro = comp.com_centro.Value;
                        dco.dco_transacc = comp.com_transacc;
                        dco.dco_debcre = debcre;
                        dco.dco_valor_nac = vvalor;
                        //dco.dco_valor_ext = 
                        dco.dco_tipo_cambio = comp.com_tipo_cambio;
                        dco.dco_concepto = comp.ccomdoc.cdoc_nombre + " - " + gpro.gpr_nombrecta_venta;
                        dco.dco_almacen = comp.com_almacen;
                        dco.dco_cliente = null;
                        dco.dco_agente = null;
                        dco.dco_doctran = comp.com_doctran;
                        dco.dco_nropago = null;
                        dco.dco_fecha_vence = null;
                        dco.dco_ddo_comproba = null;
                        dco.dco_ddo_transacc = null;
                        dco.dco_producto = null;
                        dco.dco_bodega = null;


                        lst.Add(dco);
                    }
                }

            }

            return lst;
        }

        public static List<Dcontable> contable_impuesto(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();

            if (comp.total.tot_impuesto.HasValue && comp.total.tot_timpuesto > 0)
            {

                Impuesto imp = ImpuestoBLL.GetByPK(new Impuesto { imp_codigo = comp.total.tot_impuesto.Value, imp_codigo_key = comp.total.tot_impuesto.Value, imp_empresa = comp.com_empresa, imp_empresa_key = comp.com_empresa });

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = imp.imp_cuenta.Value;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = comp.total.tot_timpuesto;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = null;// comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
                //return dco;
            }
            return lst;
        }

        public static List<Dcontable> contable_ice(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();

            if (comp.total.tot_ice  > 0)
            {


                //Impuesto imp = ImpuestoBLL.GetByPK(new Impuesto { imp_codigo = comp.total.tot_impuesto.Value, imp_codigo_key = comp.total.tot_impuesto.Value, imp_empresa = comp.com_empresa, imp_empresa_key = comp.com_empresa });

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = Constantes.cCuentaICE;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = comp.total.tot_ice;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = null;// comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
                //return dco;
            }
            return lst;
        }


        public static List<Dcontable> contable_transporte(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();
            if (comp.total.tot_transporte > 0)
            {

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = Constantes.cCuentaTransporte;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = comp.total.tot_transporte;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = null;// comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;

        }

        public static List<Dcontable> contable_seguro(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();

            if (comp.total.tot_tseguro > 0)
            {

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = Constantes.cCuentaSeguro;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = comp.total.tot_tseguro.Value;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = null;// comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;

        }

        public static List<Dcontable> contable_descuento(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();

            decimal descuentoiva = 0;
            descuentoiva = comp.total.tot_descuento1 + comp.total.tot_descuento2;
            int? ctaiva = Constantes.cCuentaDescuentoIva;

            decimal descuento0 = 0;
            descuento0 = comp.total.tot_desc1_0 + comp.total.tot_desc2_0;
            int? cta0 = Constantes.cCuentaDescuento0;

            if (descuentoiva > 0 && ctaiva.HasValue)
            {
                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = ctaiva.Value;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = descuentoiva;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = "DESCUENTO IVA "+ comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = null;// comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }

            if (descuento0 > 0 && cta0.HasValue)
            {
                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = cta0.Value;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = descuento0;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = "DESCUENTO IVA 0 "+ comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = null;// comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;
        }







        public static List<Ddocumento> crear_documentos(Comprobante comp, int debcre)
        {
            List<Ddocumento> lista = new List<Ddocumento>();
            DateTime fecha = comp.com_fecha;
            bool pasa = true;

            if (comp.planillacomp != null)
            {
                if (comp.planillacomp.pco_comprobante_pla > 0)
                {
                    List<vGuias> lst = vGuiasBLL.GetAll(new WhereParams("plc_empresa={0} and plc_comprobante_pla={1}", comp.com_empresa, comp.planillacomp.pco_comprobante_pla), "");

                    decimal porcdesc = 0;                    
                    decimal descuento0 = comp.total.tot_desc1_0 + comp.total.tot_desc2_0;
                    if (descuento0 > 0)
                        porcdesc = (descuento0 * 100 / (comp.total.tot_total + descuento0));
                    //porcdesc = Math.Round((descuento0 * 100 / comp.total.tot_total), 2);




                    int contadorpago = 1;
                    foreach (vGuias item in lst)
                    {
                        decimal valor = (item.tot_total+item.tot_desc1_0) / (decimal)Functions.Conversiones.GetValueByType(comp.total.tot_nro_pagos.Value, typeof(decimal));
                        //decimal valor = (item.) / (decimal)Functions.Conversiones.GetValueByType(comp.total.tot_nro_pagos.Value, typeof(decimal));
                        for (int i = 0; i < comp.total.tot_nro_pagos.Value; i++)
                        {

                            int diasplazo = comp.total.tot_dias_plazo.HasValue ? comp.total.tot_dias_plazo.Value : 0;
                            
                            //if (comp.total.tot_dias_plazo.HasValue)
                            //    fechaven = fechaven.AddDays(comp.total.tot_dias_plazo.Value);

                            Ddocumento doc = new Ddocumento();
                            doc.ddo_empresa = comp.com_empresa;
                            doc.ddo_comprobante = comp.com_codigo;
                            doc.ddo_transacc = comp.com_transacc;
                            doc.ddo_doctran = comp.com_doctran;
                            doc.ddo_pago = contadorpago;
                            doc.ddo_codclipro = comp.com_codclipro;
                            doc.ddo_debcre = debcre;
                            //doc.ddo_tipo_cambio = 
                            doc.ddo_fecha_emi = comp.com_fecha;
                            doc.ddo_fecha_ven = fecha.AddDays(diasplazo);
                            doc.ddo_monto = Math.Round(valor, 2);
                            if (porcdesc > 0)
                                doc.ddo_monto = Math.Round((decimal)(doc.ddo_monto - ((porcdesc/100) * doc.ddo_monto)), 2);
                                
                            //doc.ddo_monto_ext = 
                            doc.ddo_cancela = 0;
                            //doc.ddo_cancela_ext =
                            doc.ddo_cancelado = 0;
                            doc.ddo_agente = comp.com_agente;
                            doc.ddo_cuenta = cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                            doc.ddo_modulo = comp.com_modulo;
                            doc.ddo_comprobante_guia = item.com_codigo;
                            lista.Add(doc);
                            contadorpago++;
                        }
                    }

                    if (descuento0 > 0)
                    {
                        decimal dif = (comp.total.tot_total) - lista.Sum(s => s.ddo_monto ?? 0);
                        if (dif!=0)
                        {
                            int i = 0;
                            do
                            {
                                decimal resta = (decimal)0.01;
                                if (dif > 0)
                                {
                                    lista[i].ddo_monto = lista[i].ddo_monto + resta;
                                    dif = dif - resta;
                                }
                                if (dif<0)
                                {
                                    lista[i].ddo_monto = lista[i].ddo_monto - resta;
                                    dif = dif + resta;
                                }
                                i++;
                                if (i > lista.Count)
                                    i = 0;                                
                            } while (dif != 0);
                        }
                    }
                    

                    pasa = false;
                }
            }            

            if (pasa)
            {
                decimal valor = comp.total.tot_total;
                if (valor > 0)
                {
                    if (comp.total.tot_nro_pagos > 1)
                        valor = comp.total.tot_total / (decimal)Functions.Conversiones.GetValueByType(comp.total.tot_nro_pagos.Value, typeof(decimal));
                    else
                        comp.total.tot_nro_pagos = 1;
                    for (int i = 0; i < comp.total.tot_nro_pagos.Value; i++)
                    {
                        fecha = fecha.AddDays(comp.total.tot_dias_plazo??0);

                        Ddocumento doc = new Ddocumento();
                        doc.ddo_empresa = comp.com_empresa;
                        doc.ddo_comprobante = comp.com_codigo;
                        doc.ddo_transacc = comp.com_transacc;
                        doc.ddo_doctran = comp.com_doctran;
                        doc.ddo_pago = i + 1;
                        doc.ddo_codclipro = comp.com_codclipro;
                        doc.ddo_debcre = debcre;
                        //doc.ddo_tipo_cambio = 
                        doc.ddo_fecha_emi = comp.com_fecha;
                        doc.ddo_fecha_ven = fecha;
                        doc.ddo_monto = valor;
                        //doc.ddo_monto_ext = 
                        doc.ddo_cancela = 0;
                        //doc.ddo_cancela_ext =
                        doc.ddo_cancelado = 0;
                        doc.ddo_agente = comp.com_agente;
                        doc.ddo_cuenta = cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                        doc.ddo_modulo = comp.com_modulo;
                        lista.Add(doc);

                    }
                }
            }
            return lista;
        }

        


        public static int cuenta_persona(int empresa, int transacc, int codclipro, int tipopersona)
        {
            Personaxtipo pxt = PersonaxtipoBLL.GetByPK(new Personaxtipo { pxt_empresa = empresa, pxt_empresa_key = empresa, pxt_persona = codclipro, pxt_persona_key = codclipro, pxt_tipo = tipopersona, pxt_tipo_key = tipopersona });
            if (pxt.pxt_cat_persona.HasValue)

            {
                List<Cuetransacc> cuentas = CuetransaccBLL.GetAll(new WhereParams("ctr_empresa={0} and ctr_transacc={1} and ctr_categoria={2}", empresa, transacc, pxt.pxt_cat_persona.Value), "");
            if (cuentas.Count > 0)
            {
                    return cuentas[0].ctr_cuenta;
            }
            }
            return 0;

        }

        public static List<Dcontable> contables_documentos(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();
            List<Ddocumento> lista = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_debcre == debcre; });
            foreach (Ddocumento doc in lista)
            {
                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (doc.ddo_cuenta.HasValue) ? doc.ddo_cuenta.Value : cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = doc.ddo_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = (doc.ddo_monto.HasValue) ? doc.ddo_monto.Value : 0;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = doc.ddo_tipo_cambio;
                dco.dco_concepto = doc.ddo_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = doc.ddo_doctran;
                dco.dco_nropago = doc.ddo_pago;
                dco.dco_fecha_vence = doc.ddo_fecha_ven;
                dco.dco_ddo_comproba = int.Parse(doc.ddo_comprobante.ToString());
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;
        }

        public static List<Dcontable> contables_notascredeb(Comprobante comp)
        {            
            List<Dcontable> lst = new List<Dcontable>();
            List<Dnotacre> lista = comp.notascre;
            foreach (Dnotacre doc in lista)
            {
                Tiponc imp = TiponcBLL.GetByPK(new Tiponc { tnc_codigo = doc.dnc_tiponc.Value, tnc_codigo_key = doc.dnc_tiponc.Value, tnc_empresa = comp.com_empresa, tnc_empresa_key = comp.com_empresa });


                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (imp.tnc_cuentanc.HasValue) ? imp.tnc_cuentanc.Value : imp.tnc_cuentand.Value;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = (imp.tnc_cuentanc.HasValue) ? Constantes.cDebito : Constantes.cCredito;                
                dco.dco_valor_nac = (doc.dnc_valor.HasValue) ? doc.dnc_valor.Value : 0;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = doc.dnc_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;
        }

        public static List<Dcontable> contabilizar_retenciones(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();
            List<Dretencion> lista = comp.retenciones;
            foreach (Dretencion doc in lista)
            {
                if (doc.drt_impuesto.HasValue)
                {
                    Impuesto imp = ImpuestoBLL.GetByPK(new Impuesto { imp_codigo = doc.drt_impuesto.Value, imp_codigo_key = doc.drt_impuesto.Value, imp_empresa = comp.com_empresa, imp_empresa_key = comp.com_empresa });


                    Dcontable dco = new Dcontable();
                    dco.dco_empresa = comp.com_empresa;
                    dco.dco_comprobante = comp.com_codigo;
                    dco.dco_cuenta = imp.imp_cuenta.Value;
                    dco.dco_centro = comp.com_centro.Value;
                    dco.dco_transacc = comp.com_transacc;
                    dco.dco_debcre = debcre;
                    dco.dco_valor_nac = doc.drt_total.Value;
                    //dco.dco_valor_ext = 
                    dco.dco_tipo_cambio = comp.com_tipo_cambio;
                    dco.dco_concepto = comp.com_doctran + " " + comp.ccomdoc.cdoc_nombre;
                    dco.dco_almacen = comp.com_almacen;
                    dco.dco_cliente = comp.com_codclipro;
                    dco.dco_agente = null;
                    dco.dco_doctran = comp.com_doctran;
                    dco.dco_nropago = null;
                    dco.dco_fecha_vence = null;
                    dco.dco_ddo_comproba = null;
                    dco.dco_ddo_transacc = null;
                    dco.dco_producto = null;
                    dco.dco_bodega = null;
                    lst.Add(dco);
                }

            }
            return lst;
        }

        public static List<Dcontable> contables_cancelaciones(Comprobante comp)
        {
            List<Dcontable> lst = new List<Dcontable>();
            foreach (Dcancelacion can in comp.cancelaciones)
            {
                Ddocumento doc = comp.documentos.Find(delegate(Ddocumento d) { return d.ddo_pago == can.dca_pago && d.ddo_doctran == can.dca_doctran && d.ddo_transacc == can.dca_transacc; });


                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (doc.ddo_cuenta.HasValue) ? doc.ddo_cuenta.Value : cuenta_persona(comp.com_empresa, doc.ddo_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = can.dca_transacc;
                dco.dco_debcre = (doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                dco.dco_valor_nac = can.dca_monto.Value;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = can.dca_tipo_cambio;
                dco.dco_concepto = "AFECTA A " + can.dca_doctran + " PG. " + can.dca_pago + " DE " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = can.dca_doctran;
                dco.dco_nropago = can.dca_pago;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = int.Parse(doc.ddo_comprobante.ToString());
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;

        }


        public static List<Dcontable> contables_cancelaciones(Comprobante comp,  List<Ddocumento> docs)
        {
            List<Dcontable> lst = new List<Dcontable>();
            foreach (Dcancelacion can in comp.cancelaciones)
            {
                Ddocumento doc = docs.Find(delegate(Ddocumento d) { return d.ddo_pago == can.dca_pago && d.ddo_doctran == can.dca_doctran && d.ddo_transacc == can.dca_transacc; });


                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (doc.ddo_cuenta.HasValue) ? doc.ddo_cuenta.Value : cuenta_persona(comp.com_empresa, doc.ddo_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = can.dca_transacc;
                dco.dco_debcre = (doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                dco.dco_valor_nac = can.dca_monto.Value;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = can.dca_tipo_cambio;
                dco.dco_concepto = "AFECTA A " + can.dca_doctran + " PG. " + can.dca_pago + " DE " + comp.ccomdoc.cdoc_nombre;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = can.dca_doctran;
                dco.dco_nropago = can.dca_pago;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = int.Parse(doc.ddo_comprobante.ToString());
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;

        }


        public static List<Dcontable> contables_cancelaciones_recibo(Comprobante comp)
        {
            List<Dcontable> lst = new List<Dcontable>();
            foreach (Dcancelacion can in comp.cancelaciones)
            {
                Ddocumento doc = comp.documentos.Find(delegate(Ddocumento d) { return d.ddo_pago == can.dca_pago && d.ddo_doctran == can.dca_doctran && d.ddo_transacc == can.dca_transacc; });
                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (doc.ddo_cuenta.HasValue) ? doc.ddo_cuenta.Value : cuenta_persona(comp.com_empresa, doc.ddo_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = can.dca_transacc;
                dco.dco_debcre = (doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                dco.dco_valor_nac = can.dca_monto.Value;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = can.dca_tipo_cambio;
                //dco.dco_concepto = "AFECTA A " + can.dca_doctran + " PG. " + can.dca_pago + " DE " + comp.com_nombresocio;
                dco.dco_concepto = "AFECTA A " + can.dca_doctran + " DE " + comp.com_nombresocio;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = can.dca_doctran;
                dco.dco_nropago = can.dca_pago;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = int.Parse(doc.ddo_comprobante.ToString());
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;

        }


        public static List<Dcontable> contables_documentos_recibos(Comprobante comp, int debcre)
        {
            List<Dcontable> lst = new List<Dcontable>();
            List<Ddocumento> lista = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_debcre == debcre; });
            foreach (Ddocumento doc in lista)
            {
                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (doc.ddo_cuenta.HasValue) ? doc.ddo_cuenta.Value : cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = doc.ddo_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = (doc.ddo_monto.HasValue) ? doc.ddo_monto.Value : 0;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = doc.ddo_tipo_cambio;
                dco.dco_concepto = doc.ddo_doctran + " " + comp.com_nombresocio;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = doc.ddo_doctran;
                dco.dco_nropago = doc.ddo_pago;
                dco.dco_fecha_vence = doc.ddo_fecha_ven;
                dco.dco_ddo_comproba = int.Parse(doc.ddo_comprobante.ToString());
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;
        }

        public static List<Dcontable> contables_recibos(Comprobante comp, int debcr)
        {
            List<Dcontable> lst = new List<Dcontable>();
            List<Ddocumento> lst2 = new List<Ddocumento>();
            lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
            int pago = lst2.Count;
            foreach (Drecibo rec in comp.recibos)
            {

                pago  += 1;
                int cuenta = 0;
                Tipopago tp = new Tipopago();
                tp.tpa_empresa_key = rec.dfp_empresa;
                tp.tpa_codigo_key = rec.dfp_tipopago;
                tp = TipopagoBLL.GetByPK(tp);
                tp.tpa_empresa = tp.tpa_empresa;
                tp.tpa_codigo_key = tp.tpa_codigo;
                if (tp.tpa_contabiliza == 1)
                {
                    Almacen alm = new Almacen();
                    alm.alm_codigo_key = comp.com_almacen ?? 0;
                    alm.alm_empresa_key = comp.com_empresa;
                    alm = AlmacenBLL.GetByPK(alm);
                    cuenta = alm.alm_cuentacaja.Value;
                }
                if (tp.tpa_contabiliza == 10) // 10 nuevo tipo de contabilizacion para pagos 
                {
                    Almacen alm = new Almacen();
                    alm.alm_codigo_key = comp.com_almacen ?? 0;
                    alm.alm_empresa_key = comp.com_empresa;
                    alm = AlmacenBLL.GetByPK(alm);
                    cuenta = alm.alm_cuentapago.Value;
                }

                if (tp.tpa_contabiliza == 4)
                {
                    Banco ban = new Banco();
                    ban.ban_codigo = rec.dfp_banco.Value;
                    ban.ban_codigo_key = rec.dfp_banco.Value;
                    ban.ban_empresa = comp.com_empresa;
                    ban.ban_empresa_key = comp.com_empresa_key;
                    ban = BancoBLL.GetByPK(ban);

                    cuenta = ban.ban_cuenta.Value;

                }
                if (tp.tpa_contabiliza == 3)
                {
                    Personaxtipo pxt = new Personaxtipo();
                    pxt.pxt_empresa_key = comp.com_empresa;
                    pxt.pxt_persona_key = tp.tpa_codclipro ?? 0;
                    pxt.pxt_tipo_key = comp.com_tclipro ?? 0;
                    pxt = PersonaxtipoBLL.GetByPK(pxt);
                    Cuetransacc cuetransac = new Cuetransacc();
                    cuetransac.ctr_categoria_key = pxt.pxt_cat_persona ?? 0;
                    cuetransac.ctr_empresa_key = pxt.pxt_empresa;
                    cuetransac.ctr_transacc_key = 1;
                    cuetransac = CuetransaccBLL.GetByPK(cuetransac);
                    cuenta = cuetransac.ctr_cuenta;

                    Ddocumento doc2 = new Ddocumento();
                    doc2.ddo_empresa = comp.com_empresa;
                    doc2.ddo_comprobante = comp.com_codigo;
                    doc2.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                    doc2.ddo_doctran = comp.com_doctran;

                    doc2.ddo_pago = pago;
                    doc2.ddo_codclipro = tp.tpa_codclipro.Value;
                    doc2.ddo_debcre = debcr;
                    doc2.ddo_fecha_emi = comp.com_fecha;
                    doc2.ddo_fecha_ven = rec.dfp_fecha_ven??comp.com_fecha;
                    doc2.ddo_monto = rec.dfp_monto;
                    doc2.ddo_monto_ext = rec.dfp_monto;
                    doc2.ddo_cancela = 0;
                    doc2.ddo_cancelado = 0;
                    doc2.ddo_cuenta = cuetransac.ctr_cuenta;
                    doc2.ddo_agente = comp.com_agente;
                    doc2.ddo_modulo = comp.com_modulo;                    
                    DdocumentoBLL.Insert(doc2);
                }

                if (tp.tpa_contabiliza == 2)
                {
                    if (tp.tpa_transacc == 4)
                    {
                        Banco ban = new Banco();
                        ban.ban_codigo = rec.dfp_banco.Value;
                        ban.ban_codigo_key = rec.dfp_banco.Value;
                        ban.ban_empresa = comp.com_empresa;
                        ban.ban_empresa_key = comp.com_empresa_key;
                        ban = BancoBLL.GetByPK(ban);

                        cuenta = ban.ban_cuenta.Value;
                    }
                    else
                    {

                        Personaxtipo pxt = new Personaxtipo();
                        pxt.pxt_empresa_key = comp.com_empresa;
                        pxt.pxt_persona_key = comp.com_codclipro ?? 0;
                        pxt.pxt_tipo_key = comp.com_tclipro ?? 0;
                        pxt = PersonaxtipoBLL.GetByPK(pxt);
                        Cuetransacc cuetransac = new Cuetransacc();
                        cuetransac.ctr_categoria_key = pxt.pxt_cat_persona ?? 0;
                        cuetransac.ctr_empresa_key = pxt.pxt_empresa;
                        cuetransac.ctr_transacc_key = tp.tpa_transacc;
                        cuetransac = CuetransaccBLL.GetByPK(cuetransac);
                        cuenta = cuetransac.ctr_cuenta;

                        Ddocumento doc2 = new Ddocumento();
                        doc2.ddo_empresa = comp.com_empresa;
                        doc2.ddo_comprobante = comp.com_codigo;
                        doc2.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                        doc2.ddo_doctran = comp.com_doctran;
                        doc2.ddo_pago = pago;
                        doc2.ddo_codclipro = comp.com_codclipro;
                        doc2.ddo_debcre = debcr;
                        doc2.ddo_fecha_emi = comp.com_fecha;
                        doc2.ddo_fecha_ven = rec.dfp_fecha_ven ?? comp.com_fecha;
                        doc2.ddo_monto = rec.dfp_monto;
                        doc2.ddo_monto_ext = rec.dfp_monto;
                        doc2.ddo_cancela = 0;
                        doc2.ddo_cancelado = 0;
                        doc2.ddo_cuenta = cuetransac.ctr_cuenta;
                        doc2.ddo_agente = comp.com_agente;
                        doc2.ddo_modulo = comp.com_modulo;
                        DdocumentoBLL.Insert(doc2);
                    }
                }
                if (tp.tpa_contabiliza == 5)
                {
                    cuenta = tp.tpa_cuenta.Value;
                }
                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = cuenta;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcr;
                dco.dco_valor_nac = rec.dfp_monto;
                dco.dco_tipo_cambio = rec.dfp_tipo_cambio;

                string cheque = (!string.IsNullOrEmpty(rec.dfp_beneficiario) ? " BENEFICIARIO: " + rec.dfp_beneficiario : "") + (!string.IsNullOrEmpty(rec.dfp_nro_cheque) ? " CHEQUE:" + rec.dfp_nro_cheque : "");   

                dco.dco_concepto = "CANCELACION " + comp.com_doctran + cheque ;
                dco.dco_almacen = comp.com_almacen;
                //dco.dco_cliente = comp.com_codclipro;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;
                lst.Add(dco);
            }
            return lst;

        }

        public static List<Dcontable> contables_recibossocio(Comprobante comp, int debcr, int socio)
        {
            List<Dcontable> lst = new List<Dcontable>();
            List<Ddocumento> lst2 = new List<Ddocumento>();
            lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
            int pago = lst2.Count;
            decimal monto = 0;
            foreach (Drecibo rec in comp.recibos)
            {

                pago += 1;
                int cuenta = 0;
                Tipopago tp = new Tipopago();
                tp.tpa_empresa_key = rec.dfp_empresa;
                tp.tpa_codigo_key = rec.dfp_tipopago;
                tp = TipopagoBLL.GetByPK(tp);
                tp.tpa_empresa = tp.tpa_empresa;
                tp.tpa_codigo_key = tp.tpa_codigo;
                if (tp.tpa_contabiliza == 1)
                {
                    Almacen alm = new Almacen();
                    alm.alm_codigo_key = comp.com_almacen ?? 0;
                    alm.alm_empresa_key = comp.com_empresa;
                    alm = AlmacenBLL.GetByPK(alm);
                    cuenta = alm.alm_cuentacaja.Value;
                    Dcontable dco = new Dcontable();
                    dco.dco_empresa = comp.com_empresa;
                    dco.dco_comprobante = comp.com_codigo;
                    dco.dco_cuenta = cuenta;
                    dco.dco_centro = comp.com_centro.Value;
                    dco.dco_transacc = comp.com_transacc;
                    dco.dco_debcre = debcr;
                    dco.dco_valor_nac = rec.dfp_monto;
                    dco.dco_tipo_cambio = rec.dfp_tipo_cambio;
                    string cheque = (!string.IsNullOrEmpty(rec.dfp_beneficiario) ? " BENEFICIARIO: " + rec.dfp_beneficiario : "") + (!string.IsNullOrEmpty(rec.dfp_nro_cheque) ? " CHEQUE:" + rec.dfp_nro_cheque : "");
                    dco.dco_concepto = "CANCELACION " + comp.com_doctran + cheque;
                    dco.dco_almacen = comp.com_almacen;
                    //dco.dco_cliente = comp.com_codclipro;
                    dco.dco_agente = null;
                    dco.dco_doctran = comp.com_doctran;
                    dco.dco_nropago = null;
                    dco.dco_fecha_vence = null;
                    dco.dco_ddo_comproba = null;
                    dco.dco_ddo_transacc = null;
                    dco.dco_producto = null;
                    dco.dco_bodega = null;
                    monto += rec.dfp_monto;
                    lst.Add(dco);
                }
            }


            Dcontable dco1 = new Dcontable();
            dco1.dco_empresa = comp.com_empresa;
            dco1.dco_comprobante = comp.com_codigo;
            dco1.dco_cuenta = cuenta_persona(comp.com_empresa, comp.com_transacc, socio, 5);
            dco1.dco_centro = comp.com_centro.Value;
            dco1.dco_transacc = comp.com_transacc;
            dco1.dco_debcre = (debcr == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
            dco1.dco_valor_nac = monto;
            dco1.dco_tipo_cambio = monto;
            dco1.dco_concepto = "ENVIO AUTOMATICO DE LA CAJA A LA CUENTA SOCIO ";
            dco1.dco_almacen = comp.com_almacen;
            //dco.dco_cliente = comp.com_codclipro;
            dco1.dco_agente = null;
            dco1.dco_doctran = comp.com_doctran;
            dco1.dco_nropago = null;
            dco1.dco_fecha_vence = null;
            dco1.dco_ddo_comproba = null;
            dco1.dco_ddo_transacc = null;
            dco1.dco_producto = null;
            dco1.dco_bodega = null;
            lst.Add(dco1);


            return lst;

        }





        public static void contabilizar_impuesto(BLL transaccion, Comprobante comp, Persona persona, int debcre)
        {

            if (comp.total.tot_impuesto.HasValue && comp.total.tot_timpuesto > 0)
            {

                Impuesto imp = ImpuestoBLL.GetByPK(new Impuesto { imp_codigo = comp.total.tot_impuesto.Value, imp_codigo_key = comp.total.tot_impuesto.Value, imp_empresa = comp.com_empresa, imp_empresa_key = comp.com_empresa });

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = imp.imp_cuenta.Value;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = comp.total.tot_timpuesto;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + persona.per_apellidos + " " + persona.per_nombres;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = persona.per_codigo;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;

                CNT.registrar_dcontable(transaccion, dco); //Registra movimiento contable
            }

        }

        public static void contabilizar_transporte(BLL transaccion, Comprobante comp, Persona persona, int debcre)
        {
            if (comp.total.tot_transporte > 0)
            {

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = Constantes.cCuentaTransporte;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = comp.total.tot_transporte;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + persona.per_apellidos + " " + persona.per_nombres;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = persona.per_codigo;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;

                CNT.registrar_dcontable(transaccion, dco); //Registra movimiento contable
            }

        }

        public static void contabilizar_seguro(BLL transaccion, Comprobante comp, Persona persona, int debcre)
        {
            if (comp.total.tot_tseguro > 0)
            {

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = Constantes.cCuentaSeguro;
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = comp.com_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = comp.total.tot_tseguro.Value;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = comp.com_tipo_cambio;
                dco.dco_concepto = comp.com_doctran + " " + persona.per_apellidos + " " + persona.per_nombres;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = persona.per_codigo;
                dco.dco_agente = null;
                dco.dco_doctran = comp.com_doctran;
                dco.dco_nropago = null;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = null;
                dco.dco_ddo_transacc = null;
                dco.dco_producto = null;
                dco.dco_bodega = null;

                CNT.registrar_dcontable(transaccion, dco); //Registra movimiento contable
            }

        }

        public static List<Ddocumento> registrar_documentos(BLL transacction, Comprobante comp, int debcre)
        {
            List<Ddocumento> lista = new List<Ddocumento>();
            DateTime fecha = comp.com_fecha;
            if (comp.planillacomp.pco_comprobante_pla > 0)
            {
                List<vGuias> lst = vGuiasBLL.GetAll(new WhereParams("plc_empresa={0} and plc_comprobante_pla={1}", comp.com_empresa, comp.planillacomp.pco_comprobante_pla), "");
                int contadorpago = 1;
                foreach (vGuias item in lst)
                {
                    decimal valor = item.tot_total / (decimal)Functions.Conversiones.GetValueByType(comp.total.tot_nro_pagos.Value, typeof(decimal));
                    for (int i = 0; i < comp.total.tot_nro_pagos.Value; i++)
                    {
                        fecha = fecha.AddDays(comp.total.tot_dias_plazo.Value);
                        Ddocumento doc = new Ddocumento();
                        doc.ddo_empresa = comp.com_empresa;
                        doc.ddo_comprobante = comp.com_codigo;
                        doc.ddo_transacc = comp.com_transacc;
                        doc.ddo_doctran = comp.com_doctran;
                        doc.ddo_pago = contadorpago;
                        doc.ddo_codclipro = comp.com_codclipro;
                        doc.ddo_debcre = debcre;
                        //doc.ddo_tipo_cambio = 
                        doc.ddo_fecha_emi = comp.com_fecha;
                        doc.ddo_fecha_ven = fecha;
                        doc.ddo_monto = valor;
                        //doc.ddo_monto_ext = 
                        doc.ddo_cancela = 0;
                        //doc.ddo_cancela_ext =
                        doc.ddo_cancelado = 0;
                        doc.ddo_agente = comp.com_agente;
                        doc.ddo_cuenta = cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                        doc.ddo_modulo = comp.com_modulo;
                        doc.ddo_comprobante_guia = item.com_codigo;
                        DdocumentoBLL.Insert(transacction, doc);
                        lista.Add(doc);
                        contadorpago++;
                    }
                }

            }
            else
            {

                decimal valor = comp.total.tot_total / (decimal)Functions.Conversiones.GetValueByType(comp.total.tot_nro_pagos.Value, typeof(decimal));
                for (int i = 0; i < comp.total.tot_nro_pagos.Value; i++)
                {
                    fecha = fecha.AddDays(comp.total.tot_dias_plazo.Value);

                    Ddocumento doc = new Ddocumento();
                    doc.ddo_empresa = comp.com_empresa;
                    doc.ddo_comprobante = comp.com_codigo;
                    doc.ddo_transacc = comp.com_transacc;
                    doc.ddo_doctran = comp.com_doctran;
                    doc.ddo_pago = i + 1;
                    doc.ddo_codclipro = comp.com_codclipro;
                    doc.ddo_debcre = Constantes.cDebito;
                    //doc.ddo_tipo_cambio = 
                    doc.ddo_fecha_emi = comp.com_fecha;
                    doc.ddo_fecha_ven = fecha;
                    doc.ddo_monto = valor;
                    //doc.ddo_monto_ext = 
                    doc.ddo_cancela = 0;
                    //doc.ddo_cancela_ext =
                    doc.ddo_cancelado = 0;
                    doc.ddo_agente = comp.com_agente;
                    doc.ddo_cuenta = cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                    doc.ddo_modulo = comp.com_modulo;
                    DdocumentoBLL.Insert(transacction, doc);
                    lista.Add(doc);

                }
            }
            return lista;
        }

        public static void contabilizar_documentos(BLL transacction, Comprobante comp, Persona persona, int debcre)
        {
            List<Ddocumento> lista = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_debcre == debcre; });

            foreach (Ddocumento doc in lista)
            {

                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (doc.ddo_cuenta.HasValue) ? doc.ddo_cuenta.Value : cuenta_persona(comp.com_empresa, comp.com_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = doc.ddo_transacc;
                dco.dco_debcre = debcre;
                dco.dco_valor_nac = (doc.ddo_monto.HasValue) ? doc.ddo_monto.Value : 0;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = doc.ddo_tipo_cambio;
                dco.dco_concepto = doc.ddo_doctran + " " + persona.per_apellidos + " " + persona.per_nombres;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = persona.per_codigo;
                dco.dco_agente = null;
                dco.dco_doctran = doc.ddo_doctran;
                dco.dco_nropago = doc.ddo_pago;
                dco.dco_fecha_vence = doc.ddo_fecha_ven;
                dco.dco_ddo_comproba = int.Parse(doc.ddo_comprobante.ToString());
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;

                CNT.registrar_dcontable(transacction, dco); //Registra movimiento contable


            }
        }

        public static void contabilizar_cancelaciones(BLL transacction, Comprobante comp, Persona persona)
        {

            foreach (Dcancelacion can in comp.cancelaciones)
            {
                Ddocumento doc = comp.documentos.Find(delegate(Ddocumento d) { return d.ddo_pago == can.dca_pago && d.ddo_doctran == can.dca_doctran && d.ddo_transacc == can.dca_transacc; });


                Dcontable dco = new Dcontable();
                dco.dco_empresa = comp.com_empresa;
                dco.dco_comprobante = comp.com_codigo;
                dco.dco_cuenta = (doc.ddo_cuenta.HasValue) ? doc.ddo_cuenta.Value : cuenta_persona(comp.com_empresa, doc.ddo_transacc, comp.com_codclipro.Value, comp.com_tclipro.Value);
                dco.dco_centro = comp.com_centro.Value;
                dco.dco_transacc = can.dca_transacc;
                dco.dco_debcre = (doc.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                dco.dco_valor_nac = can.dca_monto.Value;
                //dco.dco_valor_ext = 
                dco.dco_tipo_cambio = can.dca_tipo_cambio;
                dco.dco_concepto = "AFECTA A " + can.dca_doctran + " PG. " + can.dca_pago + " DE " + persona.per_apellidos + " " + persona.per_nombres;
                dco.dco_almacen = comp.com_almacen;
                dco.dco_cliente = persona.per_codigo;
                dco.dco_agente = null;
                dco.dco_doctran = can.dca_doctran;
                dco.dco_nropago = can.dca_pago;
                dco.dco_fecha_vence = null;
                dco.dco_ddo_comproba = int.Parse(doc.ddo_comprobante.ToString());
                dco.dco_ddo_transacc = null;// doc.ddo_transacc;
                dco.dco_producto = null;
                dco.dco_bodega = null;

                CNT.registrar_dcontable(transacction, dco); //Registra movimiento contable
            }

        }

        public static Comprobante save_retencion(Comprobante comp)
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
            comp.com_tclipro = Constantes.cCliente;
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
                decimal totalcancela = 0;
                foreach (Dretencion item in comp.retenciones)
                {
                    item.drt_empresa = comp.com_empresa;
                    item.drt_comprobante = comp.com_codigo;
                    item.drt_secuencia = contador;
                    item.drt_debcre = Constantes.cCredito;
                    totalcancela += item.drt_total.Value;
                    DretencionBLL.Insert(transaction, item);
                    contador++;
                }
                ////////////////////////////////////////////////////////////////
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


        /// <summary>
        /// Este save es para migracion
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static Comprobante save_retencion1(Comprobante comp)
        {
            Comprobante hr = new Comprobante();
            //DateTime fecha = DateTime.Now;

            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);
            comp.com_tclipro = Constantes.cProveedor;
            comp.com_concepto = !string.IsNullOrEmpty(comp.com_concepto) ? comp.com_concepto : "RETENCION " + comp.ccomdoc.cdoc_nombre;
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
                decimal totalcancela = 0;
                foreach (Dretencion item in comp.retenciones)
                {
                    item.drt_empresa = comp.com_empresa;
                    item.drt_comprobante = comp.com_codigo;
                    item.drt_secuencia = contador;
                    item.drt_debcre = Constantes.cCredito;
                    totalcancela += item.drt_total.Value;
                    DretencionBLL.Insert(transaction, item);
                    contador++;
                }
                ////////////////////////////////////////////////////////////////
                //////////////////CREACION DE DOCUMENTOS
                //comp.documentos = CXCP.crear_documentos(comp, vdebcre);
                //foreach (Ddocumento item in comp.documentos)
                //{
                //    if (item != null)
                //        DdocumentoBLL.Insert(transaction, item);
                //}
                ///////////////////////////////

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

        public static Comprobante update_retencion1(Comprobante comp)
        {
            DateTime fecha = DateTime.Now;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_periodo = comp.com_fecha.Year;
            objU.com_mes = comp.com_fecha.Month;
            objU.com_dia = comp.com_fecha.Day;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.com_estado = Constantes.cEstadoGrabado;
            objU.com_concepto = !string.IsNullOrEmpty(comp.com_concepto) ? comp.com_concepto : "RETENCION " + comp.ccomdoc.cdoc_nombre;
            objU.com_tclipro = Constantes.cProveedor;
            objU.com_centro = Constantes.GetSinCentro().cen_codigo;

            objU.ccomdoc = CcomdocBLL.GetByPK(new Ccomdoc { cdoc_empresa = comp.com_empresa, cdoc_empresa_key = comp.com_empresa, cdoc_comprobante = comp.com_codigo, cdoc_comprobante_key = comp.com_codigo });

            objU.ccomdoc.cdoc_factura = comp.ccomdoc.cdoc_factura;
            objU.ccomdoc.cdoc_aut_factura = comp.ccomdoc.cdoc_aut_factura;



            BLL transaction = new BLL();
            transaction.CreateTransaction();
            //obj.com_fecha = fecha;         
            try
            {
                transaction.BeginTransaction();

                ComprobanteBLL.Update(transaction, objU);
               
                objU.ccomdoc.cdoc_empresa_key = objU.com_empresa;
                objU.ccomdoc.cdoc_comprobante_key = objU.com_codigo;
                CcomdocBLL.Update(transaction, objU.ccomdoc);

                List<Dretencion> lst = DretencionBLL.GetAll(new WhereParams("drt_empresa = {0} and drt_comprobante = {1}", objU.com_empresa, objU.com_codigo), "");
                foreach (Dretencion item in lst)
                {
                    DretencionBLL.Delete(transaction, item);
                }
                int contador = 0;
                decimal totalcancela = 0;
                foreach (Dretencion item in comp.retenciones)
                {
                    item.drt_empresa = objU.com_empresa;
                    item.drt_comprobante = objU.com_codigo;
                    item.drt_secuencia = contador;
                    item.drt_debcre = Constantes.cCredito;
                    totalcancela += item.drt_total.Value;
                    DretencionBLL.Insert(transaction, item);
                    contador++;
                }                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

        public static Comprobante update_retencion(Comprobante comp)
        {
            DateTime fecha = DateTime.Now;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_periodo = comp.com_fecha.Year;
            objU.com_mes = comp.com_fecha.Month;
            objU.com_dia = comp.com_fecha.Day;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.com_estado = Constantes.cEstadoGrabado;
            objU.com_concepto = !string.IsNullOrEmpty(comp.com_concepto) ? comp.com_concepto : "RETENCION " + comp.ccomdoc.cdoc_nombre;
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
            //obj.com_fecha = fecha;         
            try
            {
                transaction.BeginTransaction();
             
                ComprobanteBLL.Update(transaction, objU);

                comp.ccomdoc.cdoc_empresa_key = comp.ccomdoc.cdoc_empresa;
                comp.ccomdoc.cdoc_comprobante_key = comp.ccomdoc.cdoc_comprobante;
                objU.ccomdoc = CcomdocBLL.GetByPK(comp.ccomdoc);
                comp.ccomdoc.cdoc_factura = objU.ccomdoc.cdoc_factura;
                CcomdocBLL.Update(transaction, comp.ccomdoc);
                List<Dretencion> lst = DretencionBLL.GetAll(new WhereParams("drt_empresa = {0} and drt_comprobante = {1}", comp.com_empresa, comp.com_codigo), "");
                foreach (Dretencion item in lst)
                {
                    DretencionBLL.Delete(transaction, item);
                }
                int contador = 0;
                decimal totalcancela = 0;
                foreach (Dretencion item in comp.retenciones)
                {
                    item.drt_empresa = comp.com_empresa;
                    item.drt_comprobante = comp.com_codigo;
                    item.drt_secuencia = contador;
                    item.drt_debcre = Constantes.cCredito;
                    totalcancela += item.drt_total.Value;
                    DretencionBLL.Insert(transaction, item);
                    contador++;
                }
                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            return comp;
        }

        public static Comprobante account_retencion(Comprobante comp)
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
            comp.retenciones = DretencionBLL.GetAll(new WhereParams("drt_empresa ={0} and drt_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.cancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa ={0} and dca_comprobante_can={1}", comp.com_empresa, comp.com_codigo), "");

            int vdebcre = (comp.com_tipodoc == Constantes.cRetencion.tpd_codigo) ? Constantes.cCredito : Constantes.cDebito;
            int vdebcre_m = (comp.com_tipodoc == Constantes.cRetencion.tpd_codigo) ? Constantes.cDebito : Constantes.cCredito;

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

             

                //foreach (Dretencion item in comp.retenciones)
                //{
                //    totalcancela += item.drt_total ?? 0;
                //}
                


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


                foreach (Dcancelacion dca in comp.cancelaciones)
                {
                    DcancelacionBLL.Delete(transaction, dca);                    
                }
                comp.cancelaciones = new List<Dcancelacion>(); 

                #endregion


                decimal totalcancela = 0;
                decimal totalretencion = comp.retenciones.Sum(s => s.drt_total ?? 0);
                int secuencia = 0;
                decimal valor = totalretencion - totalcancela;

                #region Crea cancelaciones y actualiza documentos


                foreach (Ddocumento ddo in obj.documentos)
                {
                    bool add = false;

                    Dcancelacion dca = new Dcancelacion();
                    dca.dca_empresa = comp.com_empresa;
                    dca.dca_comprobante = obj.com_codigo;
                    dca.dca_transacc = ddo.ddo_transacc;
                    dca.dca_doctran = ddo.ddo_doctran;
                    dca.dca_pago = ddo.ddo_pago;
                    dca.dca_comprobante_can = comp.com_codigo;
                    dca.dca_secuencia = secuencia;
                    dca.dca_debcre = (ddo.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                    dca.dca_transacc = obj.com_transacc;
                    dca.dca_tipo_cambio = ddo.ddo_tipo_cambio;

                    decimal saldo = ddo.ddo_monto.Value - (ddo.ddo_cancela ?? 0);
                    

                    if (saldo > 0)
                    {
                        add = true;
                        if (valor > saldo)
                        {
                            dca.dca_monto = saldo;
                            dca.dca_monto_ext = saldo;                            
                        }
                        else
                        {
                            dca.dca_monto = valor;
                            dca.dca_monto_ext = valor;
                        }

                        totalcancela += dca.dca_monto.Value;
                        valor -= totalcancela;
                    }

                    if (add)
                    {

                        DcancelacionBLL.Insert(transaction, dca);
                        comp.cancelaciones.Add(dca);
                        secuencia++;

                        ddo.ddo_empresa_key = ddo.ddo_empresa;
                        ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                        ddo.ddo_transacc_key = ddo.ddo_transacc;
                        ddo.ddo_doctran_key = ddo.ddo_doctran;
                        ddo.ddo_pago_key = ddo.ddo_pago;
                        ddo.ddo_cancela = ((ddo.ddo_cancela.HasValue) ? ddo.ddo_cancela.Value : 0) + dca.dca_monto;

                        if (ddo.ddo_cancela >= ddo.ddo_monto)
                        {
                            ddo.ddo_cancelado = 1;
                            if (ddo.ddo_cancela > ddo.ddo_monto)
                                ddo.ddo_cancela = ddo.ddo_monto;
                        }

                        DdocumentoBLL.Update(transaction, ddo);// ACTUALIZA EL DOCUMENTO CANCELANDOLO   
                        comp.documentos.Add(ddo);
                        if (totalcancela >= totalretencion)
                            break;
                       
                    }
               }

                #endregion

                #region Crea documento a favor en caso de no afectar deudas

                
                if (totalretencion > totalcancela)//CREA UN DOCUMENTO LIGADO A LA RETENCIÓN EN CASO DE EXISTIR VALORES A FAVOR
                {
                    decimal valordoc = totalretencion - totalcancela;

                    Ddocumento doc2 = new Ddocumento();
                    doc2.ddo_empresa = comp.com_empresa;
                    doc2.ddo_comprobante = comp.com_codigo;
                    doc2.ddo_transacc = General.GetTransacc(comp.com_tipodoc);
                    doc2.ddo_doctran = comp.com_doctran;
                    List<Ddocumento> lst2 = new List<Ddocumento>();
                    lst2 = comp.documentos.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
                    doc2.ddo_pago = lst2.Count + 1;
                    doc2.ddo_codclipro = comp.com_codclipro;
                    doc2.ddo_debcre = vdebcre_m;
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
                List<Ddocumento> lstdocumentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa = {0} and ddo_comprobante = {1}", comp.com_empresa, comp.ccomdoc.cdoc_factura), "");

                foreach (Ddocumento item in lstdocumentos)
                {
                    List<Dcancelacion> lstcancelaciones = DcancelacionBLL.GetAll(new WhereParams("dca_empresa = {0} and dca_comprobante = {1}", item.ddo_empresa, item.ddo_comprobante), "");
                    foreach (Dcancelacion item2 in lstcancelaciones)
                    {
                        item.ddo_cancela = ((item.ddo_cancela.HasValue) ? item.ddo_cancela.Value : 0) - item2.dca_monto;
                        if (item.ddo_monto >= item.ddo_cancela)
                            item.ddo_cancelado = 0;
                        DcancelacionBLL.Delete(transaction, item2);
                    }
                    Dcancelacion dca = new Dcancelacion();
                    dca.dca_empresa = comp.com_empresa;
                    dca.dca_comprobante = obj.com_codigo;
                    dca.dca_transacc = item.ddo_transacc;
                    dca.dca_doctran = item.ddo_doctran;
                    dca.dca_pago = item.ddo_pago;
                    dca.dca_comprobante_can = comp.com_codigo;
                    dca.dca_secuencia = 0;
                    dca.dca_debcre = (item.ddo_debcre == Constantes.cDebito) ? Constantes.cCredito : Constantes.cDebito;
                    dca.dca_transacc = obj.com_transacc;
                    dca.dca_tipo_cambio = item.ddo_tipo_cambio;
                    if (totalcancela > item.ddo_monto.Value)
                    {
                        dca.dca_monto = item.ddo_monto;
                        dca.dca_monto_ext = item.ddo_monto;
                    }
                    else
                    {
                        dca.dca_monto = totalcancela;
                        dca.dca_monto_ext = totalcancela;
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
                    if (totalcancela > item.ddo_monto.Value)
                        totalcancela = totalcancela - item.ddo_monto.Value;
                    else
                        break;
                }
                */
                //CREACION DE DOCUMENTOS
                //  comp.documentos = CXCP.crear_documentos(comp, vdebcre);            

                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones

                    if (comp.contables==null)
                        comp.contables = new List<Dcontable>();

                    comp.contables.AddRange(CXCP.contabilizar_retenciones(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_cancelaciones_recibo(comp));
                
                    List<Ddocumento> lst = comp.documentos;
                    List<Ddocumento> lst2 = new List<Ddocumento>();
                    lst2 = comp.documentos.FindAll(delegate (Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
                    comp.documentos = lst2;
                    comp.contables.AddRange(CXCP.contables_documentos_recibos(comp, vdebcre));
                    comp.contables.AddRange(CXCP.contables_documentos_recibos(comp, vdebcre_m));



                    ////////////////

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



        #region Pagos Socios


        public static Comprobante save_pagosocio(Comprobante comp)
        {
            //DateTime fecha = DateTime.Now;
            int ctipocom = Constantes.cComRecibo.cti_codigo; // SE DEBE OBTENER DE ALGUN LADO ?????

            Dtipocom dti = new Dtipocom();
            dti.dti_empresa = comp.com_empresa;
            dti.dti_empresa_key = comp.com_empresa;
            dti.dti_periodo = comp.com_periodo;
            dti.dti_periodo_key = comp.com_periodo;
            dti.dti_ctipocom = comp.com_ctipocom;
            dti.dti_ctipocom_key = comp.com_ctipocom;
            //dti.dti_ctipocom = ctipocom;
            //dti.dti_ctipocom_key = ctipocom;
            dti.dti_almacen = comp.com_almacen.Value;
            dti.dti_almacen_key = comp.com_almacen.Value;
            dti.dti_puntoventa = comp.com_pventa.Value;
            dti.dti_puntoventa_key = comp.com_pventa.Value;
            dti = DtipocomBLL.GetByPK(dti);
            dti.dti_numero = dti.dti_numero.Value + 1;

            comp.com_numero = dti.dti_numero.Value;
            Persona per = new Persona();
            per.per_codigo_key = comp.com_codclipro.Value;
            per.per_empresa_key = comp.com_empresa;
            per = PersonaBLL.GetByPK(per);
            if (string.IsNullOrEmpty(comp.com_concepto))
                comp.com_concepto = "PAGO DE VALORES A SOCIO " + per.per_nombres + " " + per.per_apellidos;

            comp.com_modulo = General.GetModulo(comp.com_tipodoc); ;
            comp.com_transacc = General.GetTransacc(comp.com_tipodoc);
            comp.com_centro = Constantes.GetSinCentro().cen_codigo;
            comp.com_estado = Constantes.cEstadoGrabado;
            comp.com_descuadre = 0;
            comp.com_adestino = 0;
            comp.com_doctran = General.GetNumeroComprobante(comp);

            comp.com_tclipro = Constantes.cProveedor;

            int debcredoc = Constantes.cCredito;
            int debcrecan = Constantes.cDebito;

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

                
                DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE
                comp.total.tot_comprobante = comp.com_codigo;
                TotalBLL.Insert(transaction, comp.total);//INSERT TOTAL
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }
        public static Comprobante update_pagosocio(Comprobante comp)
        {

            DateTime fecha = DateTime.Now;
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;

            Persona per = new Persona();
            per.per_codigo_key = comp.com_codclipro.Value;
            per.per_empresa_key = comp.com_empresa;
            per = PersonaBLL.GetByPK(per);
           
            Comprobante objU = ComprobanteBLL.GetByPK(comp);
            objU.com_empresa_key = objU.com_empresa;
            objU.com_codigo_key = objU.com_codigo;
            objU.com_fecha = comp.com_fecha;
            objU.com_codclipro = comp.com_codclipro;
            objU.com_agente = comp.com_agente;
            objU.com_estado = Constantes.cEstadoGrabado;
            objU.com_centro = Constantes.GetSinCentro().cen_codigo;
            objU.com_concepto = !string.IsNullOrEmpty(comp.com_concepto) ? comp.com_concepto : "PAGO DE VALORES A SOCIO " + per.per_nombres + " " + per.per_apellidos;
            objU.com_tclipro = Constantes.cProveedor;
            objU.mod_usr = comp.mod_usr;
            objU.mod_fecha = comp.mod_fecha;

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
            int debcredoc = Constantes.cCredito;
            int debcrecan = Constantes.cDebito;

            try
            {

                transaction.BeginTransaction();

                objU.recibos = DreciboBLL.GetAll(new WhereParams("dfp_empresa ={0} and dfp_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
                //foreach (Drecibo item in comp.recibos)
                foreach (Drecibo item in objU.recibos)
                {
                    //item.dfp_comprobante_key = item.dfp_comprobante;
                    //item.dfp_secuencia_key = item.dfp_secuencia;
                    //item.dfp_empresa_key = item.dfp_empresa;                    
                    DreciboBLL.Delete(transaction, item);
                }


                ComprobanteBLL.Update(transaction, objU);// INSERT COMPROBANTE
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
                if (dti.dti_numero.HasValue)
                    DtipocomBLL.Update(transaction, dti);//ACTUALIZA LA SECUENCIA DEL COMPROBANTE


                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }

            return comp;

        }

        public static Comprobante account_pagosocio(Comprobante comp)
        {
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp = ComprobanteBLL.GetByPK(comp);
            comp.com_empresa_key = comp.com_empresa;
            comp.com_codigo_key = comp.com_codigo;
            comp.contables = DcontableBLL.GetAll(new WhereParams("dco_empresa ={0} and dco_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.recibos = DreciboBLL.GetAll(new WhereParams("dfp_empresa ={0} and dfp_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.documentos = DdocumentoBLL.GetAll(new WhereParams("ddo_empresa ={0} and ddo_comprobante={1}", comp.com_empresa, comp.com_codigo), "");
            comp.bancario = DbancarioBLL.GetAll(new WhereParams("dban_empresa ={0} and dban_cco_comproba ={1}", comp.com_empresa, comp.com_codigo), "");
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
               

                foreach (Drecibo item in comp.recibos)
                {
                    totalrecibo += item.dfp_monto;
                }



                
                if (totalrecibo > 0)//CREA UN DOCUMENTO DE ANTICIPO LIGADO AL PAGO 
                {
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
                    doc2.ddo_monto = totalrecibo;
                    doc2.ddo_monto_ext = totalrecibo;
                    doc2.ddo_cancela = 0;
                    doc2.ddo_cancelado = 0;
                    doc2.ddo_cuenta = cuenta_persona(comp.com_empresa, 7,comp.com_codclipro.Value,comp.com_tclipro.Value);
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




               

                if (comp.com_nocontable == 0) //SI ES CONTABLE
                {
                    #region Crea contabilizaciones
                   

                    comp.contables.AddRange(CXCP.contables_recibos(comp, vdebcre_m));
                    List<Ddocumento> lst = comp.documentos;
                    List<Ddocumento> lst2 = new List<Ddocumento>();
                    lst2 = comp.documentos.FindAll(delegate(Ddocumento d) { return d.ddo_comprobante == comp.com_codigo && d.ddo_empresa == comp.com_empresa; });
                    comp.documentos = lst2;
                    comp.contables.AddRange(CXCP.contables_documentos_recibos(comp, vdebcre_m));
                    comp.contables.AddRange(CXCP.contables_documentos_recibos(comp, vdebcre));
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

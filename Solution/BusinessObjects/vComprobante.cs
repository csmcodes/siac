using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Functions;
using System.Reflection;

namespace BusinessObjects
{
    public class vComprobante
    {
        public Int32? empresa { get; set; }
        public Int64? codigo { get; set; }
        public string doctran { get; set; }
        public Int32? periodo { get; set; }
        public Int32? mes { get; set; }
        public DateTime? fecha { get; set; }
        public string concepto { get; set; }
        public Int32? estado { get; set; }
        public Int32? tipodoc { get; set; }
        public Int32? ctipocom { get; set; }
        public Int32? almacen { get; set; }
        public string almacenid { get; set; }
        public string almacennombre { get; set; }
        public Int32? pventa { get; set; }
        public string pventaid { get; set; }
        public string pventanombre { get; set; }
        public Int32? numero { get; set; }
        public string crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public string usrnombres { get; set; }

        public Int32? persona { get; set; }
        public string ciruc { get; set; }
        public string nombres { get; set; }
        public string razon { get; set; }

        public Int32? remitente { get; set; }
        public string ciruc_rem { get; set; }
        public string nombres_rem { get; set; }

        public Int32? destinatario { get; set; }
        public string ciruc_des{ get; set; }
        public string nombres_des { get; set; }

        
        public Int32? vehiculo { get; set; }
        public string placa { get; set; }
        public string disco { get; set; }

        public Int32? socio { get; set; }
        public string nombres_soc { get; set; }
        public string nom_soc { get; set; }
        public string ape_soc { get; set; }

        public Int32? ruta { get; set; }
        public Int32? rutahr { get; set; }
        public string nombreruta { get; set; }
        public Int32? despachado { get; set; }
        public Int64? cancelado { get; set; }
        public Decimal? monto { get; set; }
        public Decimal? cancela { get; set; }
        public Decimal? cancelasocio { get; set; }

        public string nfactura { get; set; }
        public Int64? factura { get; set; }
        public string numfactura { get; set; }


        public Decimal? total { get; set; }
        public Decimal? subtotal { get; set; }
        public Decimal? subimpuesto { get; set; }
        public Decimal? impuesto { get; set; }
        public Decimal? tseguro { get; set; }
        public Decimal? seguro { get; set; }
        public Decimal? porc_seguro { get; set; }
        public Decimal? valordeclarado { get; set; }
        public Decimal? transporte { get; set; }
        public string idpolitica { get; set; }
        public string politica { get; set; }

        public Decimal? desc { get; set; }
        public Decimal? desc1 { get; set; }
        public Decimal? ice { get; set; }
        //Propiedades de ayuda
        public string operador { get; set; }
        public string estadoenvio { get; set; }

        public string beneficiario { get; set; }
        public Decimal? montoban { get; set; }
        public string hojaruta { get; set; }


        public string claveelec { get; set; }
        public string estadoelec { get; set; }

        public decimal? debito { get; set; }
        public decimal? credito { get; set; }

        public decimal? retiva { get; set; }
        public decimal? retren { get; set; }


        public object[] tipos { get; set; }

         #region Constructors


        public vComprobante()
        {

        }


        public vComprobante(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

                object empresa = null;
                object codigo = null;
                object doctran = null;
                object periodo = null;
                object mes = null;
                object fecha = null;
                object concepto = null;                
                object estado = null;
                object tipodoc = null;
                object ctipocom= null;
                object almacen = null;
                object almacenid = null;
                object almacennombre = null;
                object pventa = null;
                object pventaid = null;
                object pventanombre = null;
                object numero = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object usrnombres = null;

                object persona = null;
                object ciruc = null;
                object nombres = null;
                object razon = null;
                object remitente = null;
                object ciruc_rem = null;
                object nombres_rem = null;
                object destinatario = null;
                object ciruc_des = null;
                object nombres_des = null;                
                object vehiculo = null;
                object placa = null;
                object disco = null;
                object socio = null;
                object nombres_soc = null;
                object nom_soc = null;
                object ape_soc = null;
                object ruta = null;
                object nombreruta = null;
                object despachado = null;
                object cancelado = null;
                object monto = null;
                object cancela = null;

                object nfactura = null;
                

                object idpolitica = null;
                object politica = null;
                
                object operador = null;
                object estadoenvio= null;
                
                object total = null;
                object subtotal = null;                
                object subimpuesto = null;
                object impuesto = null;
                object tseguro = null;
                object porc_seguro = null;
                object valordeclarado = null;
                object seguro = null;
                object transporte = null;

                object tipos = null;

                object desc = null;
                object desc1 = null;
                object ice = null;

                object hojaruta = null;

                object debito = null;
                object credito = null;

                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("codigo", out codigo);
                tmp.TryGetValue("doctran", out doctran);
                tmp.TryGetValue("periodo", out periodo);
                tmp.TryGetValue("mes", out mes);
                tmp.TryGetValue("fecha", out fecha);
                tmp.TryGetValue("concepto", out concepto);
                tmp.TryGetValue("estado", out estado);
                tmp.TryGetValue("tipodoc", out tipodoc);
                tmp.TryGetValue("ctipocom", out ctipocom);
                tmp.TryGetValue("almacen", out almacen);
                tmp.TryGetValue("almacenid", out almacenid);
                tmp.TryGetValue("almacennombre", out almacennombre);
                tmp.TryGetValue("pventa", out pventa);
                tmp.TryGetValue("pventaid", out pventaid);
                tmp.TryGetValue("pventanombre", out pventanombre);
                tmp.TryGetValue("numero", out numero);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("usrnombres", out usrnombres);

                tmp.TryGetValue("persona", out persona);
                tmp.TryGetValue("ciruc", out ciruc);
                tmp.TryGetValue("nombres", out nombres);
                tmp.TryGetValue("razon", out razon);
                tmp.TryGetValue("remitente", out remitente);
                tmp.TryGetValue("ciruc_rem", out ciruc_rem);
                tmp.TryGetValue("nombres_rem", out nombres_rem);
                tmp.TryGetValue("destinatario", out destinatario);
                tmp.TryGetValue("ciruc_des", out ciruc_des);
                tmp.TryGetValue("nombres_des", out nombres_des);
                tmp.TryGetValue("vehiculo", out vehiculo);
                tmp.TryGetValue("placa", out placa);
                tmp.TryGetValue("disco", out disco);
                tmp.TryGetValue("socio", out socio);
                tmp.TryGetValue("nombres_soc", out nombres_soc);
                tmp.TryGetValue("nom_soc", out nom_soc);
                tmp.TryGetValue("ape_soc", out ape_soc);
                tmp.TryGetValue("ruta", out ruta);
                tmp.TryGetValue("nombreruta", out nombreruta);
                tmp.TryGetValue("despachado", out despachado);
                tmp.TryGetValue("cancelado", out cancelado);
                tmp.TryGetValue("monto", out monto);
                tmp.TryGetValue("cancela", out cancela);                
                tmp.TryGetValue("total", out total);
                tmp.TryGetValue("idpolitica", out idpolitica);
                tmp.TryGetValue("politica", out politica);
                tmp.TryGetValue("nfactura", out nfactura);


                tmp.TryGetValue("operador", out operador);
                tmp.TryGetValue("estadoenvio", out estadoenvio);
                tmp.TryGetValue("subtotal", out subtotal);
                tmp.TryGetValue("subimpuesto", out subimpuesto);
                tmp.TryGetValue("impuesto", out impuesto);
                tmp.TryGetValue("tseguro", out tseguro);
                tmp.TryGetValue("porc_seguro", out porc_seguro);
                tmp.TryGetValue("valordeclarado", out valordeclarado);
                tmp.TryGetValue("seguro", out seguro);
                tmp.TryGetValue("transporte", out transporte);
                tmp.TryGetValue("tipos", out tipos);
                tmp.TryGetValue("desc", out desc);
                tmp.TryGetValue("desc1", out desc1);
                tmp.TryGetValue("ice", out ice);
                tmp.TryGetValue("hojaruta", out hojaruta);

                tmp.TryGetValue("debito", out debito);
                tmp.TryGetValue("credito", out credito);


                this.empresa= (Int32?)Conversiones.GetValueByType(empresa, typeof(Int32?));
                this.codigo = (Int64?)Conversiones.GetValueByType(codigo, typeof(Int64?));
                this.doctran = (String)Conversiones.GetValueByType(doctran, typeof(String));
                this.periodo = (Int32)Conversiones.GetValueByType(periodo, typeof(Int32));
                this.mes = (Int32)Conversiones.GetValueByType(mes, typeof(Int32));
                this.fecha = (DateTime?)Conversiones.GetValueByType(fecha , typeof(DateTime?));
                this.concepto= (string)Conversiones.GetValueByType(concepto, typeof(string));
                this.estado = (Int32?)Conversiones.GetValueByType(estado, typeof(Int32?));
                this.tipodoc = (Int32?)Conversiones.GetValueByType(tipodoc, typeof(Int32?));
                this.ctipocom = (Int32?)Conversiones.GetValueByType(ctipocom, typeof(Int32?));
                this.almacen = (Int32?)Conversiones.GetValueByType(almacen, typeof(Int32?));
                this.almacenid = (string)Conversiones.GetValueByType(almacenid, typeof(string));
                this.almacennombre= (string)Conversiones.GetValueByType(almacennombre, typeof(string));
                this.pventa = (Int32?)Conversiones.GetValueByType(pventa, typeof(Int32?));
                this.pventaid = (string)Conversiones.GetValueByType(pventaid, typeof(string));
                this.pventanombre = (string)Conversiones.GetValueByType(pventanombre, typeof(string));
                this.numero= (Int32?)Conversiones.GetValueByType(numero, typeof(Int32?));
                this.crea_usr = (string)Conversiones.GetValueByType(crea_usr, typeof(string));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (string)Conversiones.GetValueByType(mod_usr, typeof(string));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.usrnombres = (string)Conversiones.GetValueByType(usrnombres, typeof(string));

                this.persona = (Int32?)Conversiones.GetValueByType(persona, typeof(Int32?));
                this.ciruc = (string)Conversiones.GetValueByType(ciruc, typeof(string));
                this.nombres = (string)Conversiones.GetValueByType(nombres, typeof(string));
                this.razon = (string)Conversiones.GetValueByType(razon, typeof(string));
                this.remitente = (Int32?)Conversiones.GetValueByType(remitente, typeof(Int32?));
                this.ciruc_rem = (string)Conversiones.GetValueByType(ciruc_rem, typeof(string));
                this.nombres_rem = (string)Conversiones.GetValueByType(nombres_rem, typeof(string));
                this.destinatario = (Int32?)Conversiones.GetValueByType(destinatario, typeof(Int32?));
                this.ciruc_des = (string)Conversiones.GetValueByType(ciruc_des, typeof(string));
                this.nombres_des = (string)Conversiones.GetValueByType(nombres_des, typeof(string));
                this.vehiculo= (Int32?)Conversiones.GetValueByType(vehiculo, typeof(Int32?));
                this.placa = (string)Conversiones.GetValueByType(placa, typeof(string));
                this.disco= (string)Conversiones.GetValueByType(disco, typeof(string));
                this.socio= (Int32?)Conversiones.GetValueByType(socio, typeof(Int32?));
                this.nombres_soc= (string)Conversiones.GetValueByType(nombres_soc, typeof(string));
                this.nom_soc = (string)Conversiones.GetValueByType(nom_soc, typeof(string));
                this.ape_soc = (string)Conversiones.GetValueByType(ape_soc, typeof(string));
                this.ruta= (Int32?)Conversiones.GetValueByType(ruta, typeof(Int32?));
                this.nombreruta = (string)Conversiones.GetValueByType(nombreruta, typeof(string));
                this.despachado = (Int32?)Conversiones.GetValueByType(despachado, typeof(Int32?));
                this.cancelado= (Int64?)Conversiones.GetValueByType(cancelado, typeof(Int64?));
                this.monto = (Decimal?)Conversiones.GetValueByType(monto, typeof(Decimal?));
                this.cancela = (Decimal?)Conversiones.GetValueByType(cancela, typeof(Decimal?));                
                this.total= (Decimal?)Conversiones.GetValueByType(total, typeof(Decimal?));
                this.idpolitica = (string)Conversiones.GetValueByType(idpolitica, typeof(string));
                this.politica = (string)Conversiones.GetValueByType(politica, typeof(string));
                this.nfactura = (string)Conversiones.GetValueByType(nfactura, typeof(string));

                this.operador = (string)Conversiones.GetValueByType(operador, typeof(string));
                this.estadoenvio = (string)Conversiones.GetValueByType(estadoenvio, typeof(string));
                this.subtotal = (Decimal?)Conversiones.GetValueByType(subtotal, typeof(Decimal?));
                this.subimpuesto = (Decimal?)Conversiones.GetValueByType(subimpuesto, typeof(Decimal?));
                this.seguro = (Decimal?)Conversiones.GetValueByType(seguro, typeof(Decimal?));
                this.porc_seguro = (Decimal?)Conversiones.GetValueByType(porc_seguro, typeof(Decimal?));
                this.valordeclarado = (Decimal?)Conversiones.GetValueByType(valordeclarado, typeof(Decimal?));
                this.tseguro = (Decimal?)Conversiones.GetValueByType(tseguro, typeof(Decimal?));
                this.impuesto = (Decimal?)Conversiones.GetValueByType(impuesto, typeof(Decimal?));
                this.transporte = (Decimal?)Conversiones.GetValueByType(transporte, typeof(Decimal?));
                this.desc = (Decimal?)Conversiones.GetValueByType(desc, typeof(Decimal?));
                this.desc1 = (Decimal?)Conversiones.GetValueByType(desc1, typeof(Decimal?));
                this.ice = (Decimal?)Conversiones.GetValueByType(ice, typeof(Decimal?));

                this.hojaruta = (string)Conversiones.GetValueByType(hojaruta, typeof(string));
                this.tipos = (object[])tipos;

                this.debito = (Decimal?)Conversiones.GetValueByType(debito, typeof(Decimal?));
                this.credito = (Decimal?)Conversiones.GetValueByType(credito, typeof(Decimal?));

            }


        }
        public vComprobante(IDataReader reader)
        {
            this.empresa= (reader["com_empresa"] != DBNull.Value) ? (Int32?)reader["com_empresa"] : null;
            this.codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.periodo = (reader["com_periodo"] != DBNull.Value) ? (Int32?)reader["com_periodo"] : null;            
            this.mes = (reader["com_mes"] != DBNull.Value) ? (Int32?)reader["com_mes"] : null;
            this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.concepto= (reader["com_concepto"] != DBNull.Value) ? (string)reader["com_concepto"] : null;
            this.estado = (reader["com_estado"] != DBNull.Value) ? (Int32?)reader["com_estado"] : null;
            this.tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.ctipocom= (reader["com_ctipocom"] != DBNull.Value) ? (Int32?)reader["com_ctipocom"] : null;
            this.tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.almacen = (reader["com_almacen"] != DBNull.Value) ? (Int32?)reader["com_almacen"] : null;
            this.almacenid = (reader["alm_id"] != DBNull.Value) ? (string)reader["alm_id"] : null;
            this.almacennombre = (reader["alm_nombre"] != DBNull.Value) ? (string)reader["alm_nombre"] : null;
            this.pventa = (reader["com_pventa"] != DBNull.Value) ? (Int32?)reader["com_pventa"] : null;
            this.pventaid = (reader["pve_id"] != DBNull.Value) ? (string)reader["pve_id"] : null;
            this.pventanombre = (reader["pve_nombre"] != DBNull.Value) ? (string)reader["pve_nombre"] : null;

            this.claveelec = (reader["com_claveelec"] != DBNull.Value) ? (string)reader["com_claveelec"] : null;
            this.estadoelec = (reader["com_estadoelec"] != DBNull.Value) ? (string)reader["com_estadoelec"] : null;

            this.crea_usr = (reader["crea_usr"] != DBNull.Value) ? (string)reader["crea_usr"] : null;
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = (reader["mod_usr"] != DBNull.Value) ? (string)reader["mod_usr"] : null;
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.usrnombres = (reader["usr_nombres"] != DBNull.Value) ? (string)reader["usr_nombres"] : null;

            this.persona = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;
            this.ciruc= reader["per_ciruc"].ToString();
            this.nombres= reader["per_apellidos"].ToString() + " " + reader["per_nombres"].ToString();
            this.razon= reader["per_razon"].ToString();

            this.remitente = (reader["cenv_remitente"] != DBNull.Value) ? (Int32?)reader["cenv_remitente"] : null;
            this.ciruc_rem = reader["cenv_ciruc_rem"].ToString();
            this.nombres_rem = reader["cenv_apellidos_rem"].ToString() + " " + reader["cenv_nombres_rem"].ToString();

            this.destinatario = (reader["cenv_destinatario"] != DBNull.Value) ? (Int32?)reader["cenv_destinatario"] : null;
            this.ciruc_des = reader["cenv_ciruc_des"].ToString();
            this.nombres_des = reader["cenv_apellidos_des"].ToString() + " " + reader["cenv_nombres_des"].ToString();

            this.vehiculo = (reader["cenv_vehiculo"] != DBNull.Value) ? (Int32?)reader["cenv_vehiculo"] : null;
            this.placa = reader["cenv_placa"].ToString();
            this.disco = reader["cenv_disco"].ToString();

            this.socio = (reader["cenv_socio"] != DBNull.Value) ? (Int32?)reader["cenv_socio"] : null;
            this.nombres_soc = reader["socionombres"].ToString();
            this.nom_soc = reader["socionom"].ToString();
            this.ape_soc = reader["socioape"].ToString();

            this.nfactura = reader["cdoc_aut_factura"].ToString();
            this.factura = (reader["cdoc_factura"] != DBNull.Value) ? (Int64?)reader["cdoc_factura"] : null;
            this.numfactura = (reader["numfactura"] != DBNull.Value) ? (string)reader["numfactura"] : null;

            this.rutahr = (reader["com_ruta"] != DBNull.Value) ? (Int32?)reader["com_ruta"] : null;
            this.ruta = (reader["cenv_ruta"] != DBNull.Value) ? (Int32?)reader["cenv_ruta"] : null;
            this.nombreruta = (reader["rut_nombre"] != DBNull.Value) ? (string)reader["rut_nombre"] : null;
            this.despachado = (reader["cenv_despachado_ret"] != DBNull.Value) ? (Int32?)reader["cenv_despachado_ret"] : null;
            this.cancelado= (reader["cancelado"] != DBNull.Value) ? (Int64?)reader["cancelado"] : null;
            this.monto = (reader["monto"] != DBNull.Value) ? (Decimal?)reader["monto"] : null;
            this.cancela= (reader["cancela"] != DBNull.Value) ? (Decimal?)reader["cancela"] : null;
            this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;
            this.idpolitica = reader["pol_id"].ToString();
            this.politica = reader["pol_nombre"].ToString();
           this.subtotal = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
           this.subimpuesto = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
            this.tseguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
            this.porc_seguro = (reader["tot_porc_seguro"] != DBNull.Value) ? (Decimal?)reader["tot_porc_seguro"] : null;
            this.valordeclarado= (reader["tot_vseguro"] != DBNull.Value) ? (Decimal?)reader["tot_vseguro"] : null;
            this.seguro = (reader["seguro"] != DBNull.Value) ? (Decimal?)reader["seguro"] : null;
            this.impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
            this.transporte = (reader["tot_transporte"] != DBNull.Value) ? (Decimal?)reader["tot_transporte"] : null;

            this.desc = (reader["tot_desc1_0"] != DBNull.Value) ? (Decimal?)reader["tot_desc1_0"] : null;
            this.desc1 = (reader["tot_desc2_0"] != DBNull.Value) ? (Decimal?)reader["tot_desc2_0"] : null;
            this.ice = (reader["tot_ice"] != DBNull.Value) ? (Decimal?)reader["tot_ice"] : null;

            this.beneficiario = (reader["dban_beneficiario"] != DBNull.Value) ? (string)reader["dban_beneficiario"] : null;
            this.montoban = (reader["dban_valor_nac"] != DBNull.Value) ? (Decimal?)reader["dban_valor_nac"] : null;
            this.hojaruta= (reader["hojaruta"] != DBNull.Value) ? (string)reader["hojaruta"] : null;

            this.debito = (reader["debito"] != DBNull.Value) ? (Decimal?)reader["debito"] : null;
            this.credito = (reader["credito"] != DBNull.Value) ? (Decimal?)reader["credito"] : null;

        }

        public vComprobante(IDataReader reader, bool comp)
        {
            this.empresa = (reader["com_empresa"] != DBNull.Value) ? (Int32?)reader["com_empresa"] : null;
            this.codigo = (reader["com_codigo"] != DBNull.Value) ? (Int64?)reader["com_codigo"] : null;
            this.doctran = (reader["com_doctran"] != DBNull.Value) ? (string)reader["com_doctran"] : null;
            this.periodo = (reader["com_periodo"] != DBNull.Value) ? (Int32?)reader["com_periodo"] : null;
            this.mes = (reader["com_mes"] != DBNull.Value) ? (Int32?)reader["com_mes"] : null;
            this.fecha = (reader["com_fecha"] != DBNull.Value) ? (DateTime?)reader["com_fecha"] : null;
            this.concepto = (reader["com_concepto"] != DBNull.Value) ? (string)reader["com_concepto"] : null;
            this.estado = (reader["com_estado"] != DBNull.Value) ? (Int32?)reader["com_estado"] : null;
            this.tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.ctipocom = (reader["com_ctipocom"] != DBNull.Value) ? (Int32?)reader["com_ctipocom"] : null;
            this.tipodoc = (reader["com_tipodoc"] != DBNull.Value) ? (Int32?)reader["com_tipodoc"] : null;
            this.almacen = (reader["com_almacen"] != DBNull.Value) ? (Int32?)reader["com_almacen"] : null;
            //this.almacenid = (reader["alm_id"] != DBNull.Value) ? (string)reader["alm_id"] : null;
            //this.almacennombre = (reader["alm_nombre"] != DBNull.Value) ? (string)reader["alm_nombre"] : null;
            this.pventa = (reader["com_pventa"] != DBNull.Value) ? (Int32?)reader["com_pventa"] : null;
            //this.pventaid = (reader["pve_id"] != DBNull.Value) ? (string)reader["pve_id"] : null;
            //this.pventanombre = (reader["pve_nombre"] != DBNull.Value) ? (string)reader["pve_nombre"] : null;

            this.claveelec = (reader["com_claveelec"] != DBNull.Value) ? (string)reader["com_claveelec"] : null;
            this.estadoelec = (reader["com_estadoelec"] != DBNull.Value) ? (string)reader["com_estadoelec"] : null;

            this.crea_usr = (reader["crea_usr"] != DBNull.Value) ? (string)reader["crea_usr"] : null;
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = (reader["mod_usr"] != DBNull.Value) ? (string)reader["mod_usr"] : null;
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.usrnombres = (reader["usr_nombres"] != DBNull.Value) ? (string)reader["usr_nombres"] : null;

            this.persona = (reader["com_codclipro"] != DBNull.Value) ? (Int32?)reader["com_codclipro"] : null;
            this.ciruc = reader["per_ciruc"].ToString();
            this.nombres = reader["per_apellidos"].ToString() + " " + reader["per_nombres"].ToString();
            this.razon = reader["per_razon"].ToString();

            
            this.remitente = (reader["cenv_remitente"] != DBNull.Value) ? (Int32?)reader["cenv_remitente"] : null;
            this.ciruc_rem = reader["cenv_ciruc_rem"].ToString();
            this.nombres_rem = reader["cenv_apellidos_rem"].ToString() + " " + reader["cenv_nombres_rem"].ToString();

            this.destinatario = (reader["cenv_destinatario"] != DBNull.Value) ? (Int32?)reader["cenv_destinatario"] : null;
            this.ciruc_des = reader["cenv_ciruc_des"].ToString();
            this.nombres_des = reader["cenv_apellidos_des"].ToString() + " " + reader["cenv_nombres_des"].ToString();

            this.vehiculo = (reader["cenv_vehiculo"] != DBNull.Value) ? (Int32?)reader["cenv_vehiculo"] : null;
            this.placa = reader["cenv_placa"].ToString();
            this.disco = reader["cenv_disco"].ToString();

            this.socio = (reader["cenv_socio"] != DBNull.Value) ? (Int32?)reader["cenv_socio"] : null;

            //this.nombres_soc = reader["cenv_nombres_soc"].ToString();
            this.nombres_soc = reader["socionombres"].ToString();
            //this.nom_soc = reader["socionom"].ToString();
            //this.ape_soc = reader["socioape"].ToString();

            this.nfactura = reader["cdoc_aut_factura"].ToString();
            this.factura = (reader["cdoc_factura"] != DBNull.Value) ? (Int64?)reader["cdoc_factura"] : null;
            //this.numfactura = (reader["numfactura"] != DBNull.Value) ? (string)reader["numfactura"] : null;

            this.rutahr = (reader["com_ruta"] != DBNull.Value) ? (Int32?)reader["com_ruta"] : null;
            this.ruta = (reader["cenv_ruta"] != DBNull.Value) ? (Int32?)reader["cenv_ruta"] : null;
            this.nombreruta = (reader["rut_nombre"] != DBNull.Value) ? (string)reader["rut_nombre"] : null;
            this.despachado = (reader["cenv_despachado_ret"] != DBNull.Value) ? (Int32?)reader["cenv_despachado_ret"] : null;
            
            //this.cancelado = (reader["cancelado"] != DBNull.Value) ? (Int64?)reader["cancelado"] : null;
            //this.monto = (reader["monto"] != DBNull.Value) ? (Decimal?)reader["monto"] : null;
            //this.cancela = (reader["cancela"] != DBNull.Value) ? (Decimal?)reader["cancela"] : null;
            

            this.idpolitica = reader["pol_id"].ToString();
            this.politica = reader["pol_nombre"].ToString();
            this.total = (reader["tot_total"] != DBNull.Value) ? (Decimal?)reader["tot_total"] : null;
            this.subtotal = (reader["tot_subtot_0"] != DBNull.Value) ? (Decimal?)reader["tot_subtot_0"] : null;
            this.subimpuesto = (reader["tot_subtotal"] != DBNull.Value) ? (Decimal?)reader["tot_subtotal"] : null;
            this.tseguro = (reader["tot_tseguro"] != DBNull.Value) ? (Decimal?)reader["tot_tseguro"] : null;
            this.porc_seguro = (reader["tot_porc_seguro"] != DBNull.Value) ? (Decimal?)reader["tot_porc_seguro"] : null;
            this.valordeclarado = (reader["tot_vseguro"] != DBNull.Value) ? (Decimal?)reader["tot_vseguro"] : null;
            //this.seguro = (reader["seguro"] != DBNull.Value) ? (Decimal?)reader["seguro"] : null;
            this.impuesto = (reader["tot_timpuesto"] != DBNull.Value) ? (Decimal?)reader["tot_timpuesto"] : null;
            this.transporte = (reader["tot_transporte"] != DBNull.Value) ? (Decimal?)reader["tot_transporte"] : null;

            this.desc = (reader["tot_desc1_0"] != DBNull.Value) ? (Decimal?)reader["tot_desc1_0"] : null;
            this.desc1 = (reader["tot_desc2_0"] != DBNull.Value) ? (Decimal?)reader["tot_desc2_0"] : null;
            this.ice = (reader["tot_ice"] != DBNull.Value) ? (Decimal?)reader["tot_ice"] : null;

            this.beneficiario = (reader["dban_beneficiario"] != DBNull.Value) ? (string)reader["dban_beneficiario"] : null;
            this.montoban = (reader["dban_valor_nac"] != DBNull.Value) ? (Decimal?)reader["dban_valor_nac"] : null;
            
            //this.hojaruta = (reader["hojaruta"] != DBNull.Value) ? (string)reader["hojaruta"] : null;
            //this.debito = (reader["debito"] != DBNull.Value) ? (Decimal?)reader["debito"] : null;
            //this.credito = (reader["credito"] != DBNull.Value) ? (Decimal?)reader["credito"] : null;

        }

        #endregion

        public string GetSQL()
        {
            /*string sql = "SELECT DISTINCT t.com_codigo, t.com_doctran, t.com_periodo, t.com_mes, t.com_fecha, t.com_concepto, t.com_estado, t.com_tipodoc, t.com_ctipocom, t.com_almacen, t.com_pventa, t.com_numero, " +
                         "    t.com_codclipro, t.per_ciruc,t.per_apellidos, t.per_nombres, t.per_razon, " +
                         "    t.cenv_remitente, t.cenv_ciruc_rem, t.cenv_nombres_rem, t.cenv_apellidos_rem, " +
                         "    t.cenv_destinatario ,t.cenv_ciruc_des, t.cenv_nombres_des, t.cenv_apellidos_des, " +
                         "   t.cenv_vehiculo, t.cenv_placa, t.cenv_disco,  " +
                         "    t.cenv_socio, t.socionombres, " +
                         "    t.cenv_ruta, t.cenv_despachado_ret	 " +
                         "FROM (SELECT ROW_NUMBER() OVER(ORDER BY %orderby%) RowNr,  " +
                         "    c.com_codigo, c.com_doctran,c.com_periodo, c.com_mes, c.com_fecha, c.com_concepto, c.com_estado, c.com_tipodoc, c.com_ctipocom, c.com_almacen, c.com_pventa, c.com_numero, " +
                         "    c.com_codclipro, p.per_ciruc,p.per_apellidos, p.per_nombres, p.per_razon, " +
                         "    e.cenv_remitente, e.cenv_ciruc_rem, e.cenv_nombres_rem, e.cenv_apellidos_rem, " +
                         "    e.cenv_destinatario ,e.cenv_ciruc_des, e.cenv_nombres_des, e.cenv_apellidos_des, " +
                         "    e.cenv_vehiculo, e.cenv_placa, e.cenv_disco,  " +
                         "    e.cenv_socio, p1.per_apellidos || ' ' || p1.per_nombres socionombres, " +
                         "    e.cenv_ruta, e.cenv_despachado_ret	 " +
                         "FROM comprobante c " +
                         "    LEFT JOIN persona p ON c.com_codclipro = p.per_codigo " +
                         "    LEFT JOIN ccomenv e ON c.com_codigo = e.cenv_comprobante " +
                         "    LEFT JOIN persona p1 ON e.cenv_socio = p1.per_codigo %whereclause%) t " +
                         "WHERE RowNr BETWEEN %desde% AND %hasta% ";*/

            string sql = "SELECT DISTINCT t.com_empresa,t.com_codigo, t.com_doctran, t.com_periodo, t.com_mes, t.com_fecha, t.com_concepto, t.com_estado, t.com_tipodoc, t.com_ctipocom, t.com_almacen, t.alm_id, t.alm_nombre, t.com_pventa, t.pve_id, t.pve_nombre, t.com_numero, " +
                           "    t.com_codclipro,t.com_claveelec, t.com_estadoelec, t.com_ruta, t.crea_usr, t.crea_fecha, t.mod_usr, t.mod_fecha, t.usr_nombres,  t.per_ciruc,t.per_apellidos, t.per_nombres, t.per_razon, " +
                           "    t.cenv_remitente, t.cenv_ciruc_rem, t.cenv_nombres_rem, t.cenv_apellidos_rem, " +
                           "    t.cenv_destinatario ,t.cenv_ciruc_des, t.cenv_nombres_des, t.cenv_apellidos_des, " +
                           "   t.cenv_vehiculo, t.cenv_placa, t.cenv_disco, t.cdoc_aut_factura, t.cdoc_factura, t.numfactura,  " +
                           "    t.cenv_socio, t.socionombres, t.socionom, t.socioape, " +
                           "    t.cenv_ruta, t.rut_nombre, t.cenv_despachado_ret,  t.tot_total,t.tot_subtotal,t.tot_subtot_0, t.tot_timpuesto,t.tot_tseguro,t.tot_porc_seguro,t.tot_vseguro,t.seguro,t.tot_transporte , t.pol_nombre, t.pol_id,t.tot_desc1_0,t.tot_desc2_0,t.tot_ice, " +
                           "    t.cancelado, t.monto, t.cancela, t.dban_beneficiario, t.dban_valor_nac,t.hojaruta, t.debito,t.credito	 " +
                           "FROM (SELECT ROW_NUMBER() OVER(ORDER BY c.com_fecha DESC) RowNr,  " +
                           "    c.com_empresa,c.com_codigo, c.com_doctran,c.com_periodo, c.com_mes, c.com_fecha, c.com_concepto, c.com_estado, c.com_tipodoc, c.com_ctipocom, c.com_almacen, alm_id, alm_nombre, c.com_pventa, pve_id, pve_nombre, c.com_numero, " +
                           "    c.com_codclipro, c.com_claveelec, c.com_estadoelec, c.com_ruta,c.crea_usr, c.crea_fecha, c.mod_usr, c.mod_fecha, u.usr_nombres , p.per_ciruc,p.per_apellidos, p.per_nombres, p.per_razon, " +
                           "    e.cenv_remitente, e.cenv_ciruc_rem, e.cenv_nombres_rem, e.cenv_apellidos_rem, " +
                           "    e.cenv_destinatario ,e.cenv_ciruc_des, e.cenv_nombres_des, e.cenv_apellidos_des, " +
                           "    e.cenv_vehiculo, e.cenv_placa, e.cenv_disco, cdoc_aut_factura, cdoc_factura, fc.com_doctran numfactura, " +
                           "    e.cenv_socio, p1.per_apellidos || ' ' || p1.per_nombres socionombres, p1.per_apellidos socioape, p1.per_nombres socionom, " +
                           "    e.cenv_ruta, rut_nombre, e.cenv_despachado_ret,	tot_total,tot_subtotal,tot_subtot_0,tot_timpuesto,tot_tseguro,tot_porc_seguro,tot_vseguro,tot_vseguro *(tot_porc_seguro/100) seguro, tot_transporte, pol_nombre, pol_id,tot_desc1_0,tot_desc2_0,tot_ice, dban_beneficiario, hr.com_doctran hojaruta," +
                           "    dban_valor_nac, SUM(ddo_cancelado) cancelado, SUM(ddo_monto) monto , SUM(ddo_cancela) cancela " +
                           "    ,sum(CASE WHEN dco_debcre =1 THEN dco_valor_nac ELSE  0 END) as debito, sum(CASE WHEN dco_debcre =2 THEN dco_valor_nac ELSE  0 END) as credito " +
                           "FROM comprobante c " +
                           "    LEFT JOIN usuario u ON c.crea_usr = u.usr_id " +
                           "    LEFT JOIN persona p ON c.com_codclipro = p.per_codigo " +
                           "    LEFT JOIN ccomenv e ON c.com_codigo = e.cenv_comprobante " +
                           "    LEFT JOIN ruta ON cenv_ruta = rut_codigo and cenv_empresa=rut_empresa " +
                           "    LEFT JOIN total ON c.com_codigo = tot_comprobante " +
                           "    LEFT JOIN persona p1 ON e.cenv_socio = p1.per_codigo " +
                           "    LEFT JOIN ddocumento d ON c.com_codigo = d.ddo_comprobante   " +
                           "    LEFT JOIN ccomdoc ON c.com_codigo = cdoc_comprobante " +
                           "    LEFT JOIN politica ON cdoc_politica = pol_codigo " +                           
                           "    LEFT JOIN almacen ON c.com_almacen = alm_codigo and alm_empresa = c.com_empresa " +
                           "    LEFT JOIN puntoventa ON c.com_almacen = pve_almacen and c.com_pventa= pve_secuencia and pve_empresa = c.com_empresa " +
                           "    LEFT JOIN dbancario on dban_cco_comproba = c.com_codigo and dban_empresa = c.com_empresa  " +

                           "    LEFT JOIN rutaxfactura on rfac_empresa = c.com_empresa and rfac_comprobantefac = c.com_codigo " +
                           "    LEFT JOIN comprobante hr  on rfac_empresa = hr.com_empresa and rfac_comprobanteruta = hr.com_codigo " +
                           "    LEFT JOIN comprobante fc  on cdoc_empresa = fc.com_empresa and cdoc_factura = fc.com_codigo " +
                           "    LEFT JOIN dcontable  on dco_empresa = c.com_empresa and dco_comprobante = c.com_codigo " +


                           " GROUP BY c.com_empresa,c.com_codigo, c.com_doctran,c.com_periodo, c.com_mes, c.com_fecha, c.com_concepto, c.com_estado, c.com_tipodoc, c.com_ctipocom, c.com_almacen,alm_id, alm_nombre, c.com_pventa,pve_id, pve_nombre, c.com_numero, " +
                           "  c.com_codclipro, c.com_claveelec, c.com_estadoelec, c.com_ruta,c.crea_usr, c.crea_fecha, c.mod_usr, c.mod_fecha, u.usr_nombres , p.per_ciruc,p.per_apellidos, p.per_nombres, p.per_razon,  " +
                           "  e.cenv_remitente, e.cenv_ciruc_rem, e.cenv_nombres_rem, e.cenv_apellidos_rem,  " +
                           "  e.cenv_destinatario ,e.cenv_ciruc_des, e.cenv_nombres_des, e.cenv_apellidos_des,  " +
                           "  e.cenv_vehiculo, e.cenv_placa, e.cenv_disco, cdoc_aut_factura, cdoc_factura, fc.com_doctran, " +
                           "  e.cenv_socio, p1.per_apellidos, p1.per_nombres, cdoc_politica,  " +
                           //"  e.cenv_socio, p1.per_apellidos || ' ' || p1.per_nombres, " +
                           "  e.cenv_ruta,rut_nombre, e.cenv_despachado_ret, tot_total, tot_subtotal,tot_subtot_0,tot_timpuesto,tot_tseguro,tot_porc_seguro, tot_vseguro, tot_transporte, pol_codigo, pol_nombre, pol_id,tot_desc1_0,tot_desc2_0,tot_ice, dban_beneficiario,dban_valor_nac, hr.com_doctran" +
                           "    %whereclause%) t " +
                           "WHERE RowNr BETWEEN %desde% AND %hasta%  ORDER BY %orderby% ";

            return sql;
        }

        public string GetSQLALL()
        {

            string sql = "SELECT  " +
                           "    c.com_empresa,c.com_codigo, c.com_doctran,c.com_periodo, c.com_mes, c.com_fecha, c.com_concepto, c.com_estado, c.com_tipodoc, c.com_ctipocom, c.com_almacen,alm_id, alm_nombre, c.com_pventa,pve_id, pve_nombre, c.com_numero, " +
                           "    c.com_codclipro, c.com_claveelec, c.com_estadoelec, c.com_ruta,c.crea_usr, c.crea_fecha, c.mod_usr, c.mod_fecha ,u.usr_nombres, p.per_ciruc,p.per_apellidos, p.per_nombres, p.per_razon, " +
                           "    e.cenv_remitente, e.cenv_ciruc_rem, e.cenv_nombres_rem, e.cenv_apellidos_rem, " +
                           "    e.cenv_destinatario ,e.cenv_ciruc_des, e.cenv_nombres_des, e.cenv_apellidos_des, " +
                           "    e.cenv_vehiculo, e.cenv_placa, e.cenv_disco, cdoc_aut_factura, cdoc_factura, fc.com_doctran numfactura, " +
                           "    e.cenv_socio, p1.per_apellidos || ' ' || p1.per_nombres socionombres, p1.per_apellidos socioape, p1.per_nombres socionom, " +
                           "    e.cenv_ruta, rut_nombre, e.cenv_despachado_ret,	tot_total,tot_subtotal,tot_subtot_0,tot_timpuesto,tot_tseguro,tot_porc_seguro,tot_vseguro, tot_vseguro *(tot_porc_seguro/100) seguro, tot_transporte, pol_nombre, pol_id,tot_desc1_0,tot_desc2_0,tot_ice,dban_beneficiario,dban_valor_nac, hr.com_doctran hojaruta, " +
                           "    SUM(ddo_cancelado) cancelado, SUM(ddo_monto) monto , SUM(ddo_cancela) cancela " +
                           "    ,sum(CASE WHEN dco_debcre =1 THEN dco_valor_nac ELSE  0 END) as debito, sum(CASE WHEN dco_debcre =2 THEN dco_valor_nac ELSE  0 END) as credito " +
                           "FROM comprobante c " +
                           "    LEFT JOIN usuario u ON c.crea_usr = u.usr_id " +
                           "    LEFT JOIN persona p ON c.com_codclipro = p.per_codigo " +
                           "    LEFT JOIN ccomenv e ON c.com_codigo = e.cenv_comprobante " +
                           "    LEFT JOIN ruta ON cenv_ruta = rut_codigo and cenv_empresa=rut_empresa " +
                           "    LEFT JOIN total ON c.com_codigo = tot_comprobante " +
                           "    LEFT JOIN persona p1 ON e.cenv_socio = p1.per_codigo " +
                           "    LEFT JOIN ddocumento d ON c.com_codigo = d.ddo_comprobante   " +
                           "    LEFT JOIN ccomdoc ON c.com_codigo = cdoc_comprobante " +
                           "    LEFT JOIN politica ON cdoc_politica = pol_codigo " +
                           "    LEFT JOIN almacen ON c.com_almacen = alm_codigo and alm_empresa = c.com_empresa " +
                           "    LEFT JOIN puntoventa ON c.com_almacen = pve_almacen and c.com_pventa= pve_secuencia and pve_empresa = c.com_empresa " +
                           "    LEFT JOIN dbancario on dban_cco_comproba = c.com_codigo and dban_empresa = c.com_empresa  " +

                           "    LEFT JOIN rutaxfactura on rfac_empresa = c.com_empresa and rfac_comprobantefac = c.com_codigo " +
                           "    LEFT JOIN comprobante hr  on rfac_empresa = hr.com_empresa and rfac_comprobanteruta = hr.com_codigo " +
                           "    LEFT JOIN comprobante fc  on cdoc_empresa = fc.com_empresa and cdoc_factura = fc.com_codigo " +
                           "    LEFT JOIN dcontable  on dco_empresa = c.com_empresa and dco_comprobante = c.com_codigo " +

                           " GROUP BY c.com_empresa,c.com_codigo, c.com_doctran,c.com_periodo, c.com_mes, c.com_fecha, c.com_concepto, c.com_estado, c.com_tipodoc, c.com_ctipocom, c.com_almacen,alm_id, alm_nombre, c.com_pventa,pve_id, pve_nombre, c.com_numero, " +
                           "  c.com_codclipro, c.com_claveelec, c.com_estadoelec, c.com_ruta,c.crea_usr, c.crea_fecha, c.mod_usr, c.mod_fecha ,u.usr_nombres , p.per_ciruc,p.per_apellidos, p.per_nombres, p.per_razon,  " +
                           "  e.cenv_remitente, e.cenv_ciruc_rem, e.cenv_nombres_rem, e.cenv_apellidos_rem,  " +
                           "  e.cenv_destinatario ,e.cenv_ciruc_des, e.cenv_nombres_des, e.cenv_apellidos_des,  " +
                           "  e.cenv_vehiculo, e.cenv_placa, e.cenv_disco, cdoc_aut_factura, cdoc_factura, fc.com_doctran," +
                           "  e.cenv_socio, p1.per_apellidos, p1.per_nombres, cdoc_politica," +
                           "  e.cenv_ruta,rut_nombre, e.cenv_despachado_ret, tot_total, tot_subtotal,tot_subtot_0,tot_timpuesto,tot_tseguro,tot_porc_seguro,tot_vseguro, tot_transporte,pol_codigo, pol_nombre, pol_id,tot_desc1_0,tot_desc2_0,tot_ice, dban_beneficiario,dban_valor_nac, hr.com_doctran " +
                           "    %whereclause%  %orderby% ";
                
                
                
                

            return sql;
        }


        public string GetSQLRange()
        {

            string sql = "SELECT *, p1.per_apellidos || ' ' || p1.per_nombres socionombres FROM comprobante" +
                " LEFT JOIN usuario ON comprobante.crea_usr = usr_id " +
                " LEFT JOIN persona p ON com_codclipro = p.per_codigo and com_empresa=p.per_empresa " +
                " LEFT JOIN ccomenv e ON com_codigo = cenv_comprobante  and com_empresa=cenv_empresa " +
                " LEFT JOIN ccomdoc ON com_codigo = cdoc_comprobante and com_empresa=cdoc_empresa " +
                " LEFT JOIN politica ON cdoc_politica = pol_codigo and cdoc_empresa=pol_empresa " +
                " LEFT JOIN ruta ON cenv_ruta = rut_codigo and cenv_empresa=rut_empresa " +
                " LEFT JOIN total ON com_codigo = tot_comprobante  and com_empresa=tot_empresa " +
                " LEFT JOIN persona p1 ON cenv_socio = p1.per_codigo and cenv_empresa=p1.per_empresa " +
                " LEFT JOIN dbancario on dban_cco_comproba = com_codigo and dban_empresa = com_empresa  " +
                "";


            return sql;
        }




        public List<vComprobante> GetStruc()
        {
            return new List<vComprobante>();
        }

        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Functions;

namespace Services
{

    public class TipoDocOpc
    {
        public bool numeromanual { get; set; }
        public bool? lockfecha { get; set; }
    }

    public class OpcionPagina
    {
        public string opcion { get; set; }
        public string onclick { get; set; }
        public string perfil { get; set; }
    }

    public class PoliticaTipoPago
    {
        public string politica { get; set; }
        public string tipopago { get; set; }        

    }

    public class ValorIVA
    {
        public string desde { get; set; }
        public string hasta { get; set; }
        public decimal valor { get; set; }
    }


    public class Electronicos
    {
        public int empresa { get; set; }
        public int tipodoc { get; set; }
        public int ambiente { get; set; }
        public string especial { get; set; }
        public string contabilidad { get; set; }
        public int formato { get; set; }
        public DateTime? desde { get; set; }
        public DateTime? hasta { get; set; }
        public string usuarios { get; set; }
    }

    public class ElectronicIds
    {
        public string tipo { get; set; }
        public string codigo { get; set; }
    }

    public class ElectronicImp
    {
        public decimal porcentaje { get; set; }
        public int codigo { get; set; }
    }


    public class ElectronicPago
    {
        public string codigo { get; set; }
        public string forma { get; set; }
        public string tipopago { get; set; }
        public string politica { get; set; }
        public string defecto { get; set; }

    }


    public class ElectronicRet
    {
        public string impuesto { get; set; }
        public int codigo { get; set; }

    }



    public class ElectronicPorcRet
    {
        public decimal porcentaje { get; set; }
        public int codigo { get; set; }

    }

    public class JsonObj
    {
        public int empresa { get; set; }
        public string crea_usr { get; set; }
        public DateTime crea_fecha { get; set; }
        public string crea_ip { get; set; }
        public string mod_usr { get; set; }
        public DateTime mod_fecha { get; set; }
        public string mod_ip { get; set; }


        public JsonObj(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object empresa = null;
                object crea_usr = null;
                object crea_fecha = null;
                object crea_ip = null;
                object mod_usr = null;
                object mod_fecha = null;
                object mod_ip = null;


                tmp.TryGetValue("empresa", out empresa);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("crea_ip", out crea_ip);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("mod_ip", out mod_ip);


                this.empresa = (int)Conversiones.GetValueByType(empresa, typeof(int));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime)Conversiones.GetValueByType(crea_fecha, typeof(DateTime));
                this.crea_ip = (String)Conversiones.GetValueByType(crea_ip, typeof(String));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime)Conversiones.GetValueByType(mod_fecha, typeof(DateTime));
                this.mod_ip = (String)Conversiones.GetValueByType(mod_ip, typeof(String));

            }
        }

    }

    public class FormatoCuenta
    {
        public Int32 nivel { get; set; }
        public string formato { get; set; }
    }

    public class EmpresaOpciones
    {
        public List<FormatoCuenta> formatocuenta { get; set; }
    }


    public class PeriodoContable
    {
        public int periodo { get; set; }
        public int mes { get; set; }
        public string estado { get; set; }
        public string audit { get; set; }
    }


    public class ComprobanteExt
    {
        public Int32 com_empresa { get; set; }
        public Int64 com_codigo { get; set; }
        public DateTime? com_fecha { get; set; }
        public Int32? com_tipo { get; set; }
        public String com_establecimiento { get; set; }
        public String com_puntoemision { get; set; }
        public String com_secuencia { get; set; }
        public String com_autoriza { get; set; }
        public String com_ciruccli { get; set; }
        public String com_nombrescli { get; set; }
        public String com_direccioncli { get; set; }
        public String com_telefonocli { get; set; }
        public String com_emailcli { get; set; }
        public String com_cirucdes { get; set; }
        public String com_nombresdes { get; set; }
        public String com_direcciondes { get; set; }
        public String com_telefonodes { get; set; }
        public String com_emaildes { get; set; }
        public String com_cirucrem { get; set; }
        public String com_nombresrem { get; set; }
        public String com_direccionrem { get; set; }
        public String com_telefonorem { get; set; }
        public String com_emailrem { get; set; }
        public String com_formapago { get; set; }
        public String com_ruta { get; set; }
        public String com_entrega { get; set; }
        public String com_hojaruta { get; set; }
        public String com_cirucsoc { get; set; }
        public String com_nombressoc { get; set; }
        public String com_ciruccho { get; set; }
        public String com_nombrescho { get; set; }
        public String com_guiaremision { get; set; }
        public String com_placaveh { get; set; }
        public String com_discoveh { get; set; }
        public String com_cirucret { get; set; }
        public String com_nombresret { get; set; }
        public DateTime? com_fecharet { get; set; }
        public Int32? com_retirado { get; set; }
        public String com_observacionret { get; set; }
        public String com_observacion { get; set; }
        public Decimal? com_valordeclarado { get; set; }
        public Decimal? com_domicilio { get; set; }
        public Decimal? com_seguro { get; set; }
        public Decimal? com_subtotal0 { get; set; }
        public Decimal? com_subtotaliva { get; set; }
        public Decimal? com_descuento0 { get; set; }
        public Decimal? com_descuentoiva { get; set; }
        public Decimal? com_iva { get; set; }
        public Decimal? com_total { get; set; }
        public Int32? com_estado { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public String mod_fecha { get; set; }
        public List<DetalleExt> detalle { get; set; }

    }

    public class DetalleExt
    {
        public Int32 det_empresa { get; set; }
        public Int32 det_empresa_key { get; set; }
        public Int64 det_comprobante { get; set; }
        public Int64 det_comprobante_key { get; set; }
        public Int32 det_secuencia { get; set; }
        public Int32 det_secuencia_key { get; set; }
        public String det_producto { get; set; }
        public String det_observacion { get; set; }
        public Decimal? det_cantidad { get; set; }
        public Decimal? det_precio { get; set; }
        public Decimal? det_descuento { get; set; }
        public Decimal? det_valor { get; set; }
        public Int32? det_estado { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public String mod_fecha { get; set; }
    }

    public class PorcentajeRetencion
    {
        public decimal? porcentaje { get; set; }
        public int? codigo { get; set; }
        public string tipo { get; set; }

    }
    public class TipoPagoCob
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public int? codigo { get; set; }
        public bool emisor { get; set; }
        public bool banco { get; set; }
        public bool nro { get; set; }
        public List<PorcentajeRetencion> porcentajes { get; set; }
        public string tiporet { get; set; }
        public bool observacion { get; set; }
        public bool add { get; set; }
    }


    public class ElectronicoClave
    {
        public string numero { get; set; }
        public string clave { get; set; }

    }


    class ConstantesJSON
    {
    }
}

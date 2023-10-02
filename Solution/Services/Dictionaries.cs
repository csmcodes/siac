﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using BusinessObjects;
using BusinessLogicLayer;

namespace Services
{
    public class Dictionaries
    {
        public static int cod_empresa = 0;

        public static Dictionary<string, string> Empty()
        {
            return new Dictionary<string, string>();
        }

        #region Autorizaciones
        public static Dictionary<string, string> GetAutorizacionesPersona(int empresa, int persona)
        {
            List<Autpersona> lst = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_estado=1", empresa, persona), "ape_val_fecha desc");
           // return lst.ToDictionary(p => p.ape_nro_autoriza.ToString()+p.ape_fac1.ToString()+p.ape_fac2.ToString(), p => p.ape_nro_autoriza);

          return lst.ToDictionary(p => p.ape_nro_autoriza.ToString() + "-"+ p.ape_fact1 +"-"+p.ape_fact2+"-"+p.ape_retdato, p => p.ape_nro_autoriza);
          
        }



        public static Dictionary<string, string> GetAutorizacionesPersonaByRetdato(int empresa, int persona, int retdato)
        {
            List<Autpersona> lst = AutpersonaBLL.GetAll(new WhereParams("ape_empresa = {0} and ape_persona={1} and ape_retdato={2} and ape_estado=1", empresa, persona, retdato), "ape_val_fecha desc");
            //return lst.ToDictionary(p => p.ape_nro_autoriza.ToString(), p => p.ape_nro_autoriza);
            return lst.ToDictionary(p => p.ape_nro_autoriza.ToString() + "-" + p.ape_fact1 + "-" + p.ape_fact2 + "-" + p.ape_retdato, p => p.ape_nro_autoriza);
        }
        #endregion
        #region AutorizacionesComprobantes
        public static Dictionary<string, string> GetAutorizacionesComprobante()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(1 + "", "Modificar");
            dic.Add(2 + "", "Eliminar");           
            dic.Add(3 + "", "Duplicar");
            dic.Add(4 + "", "Cambiar fecha");
            dic.Add(5 + "", "Reimprimir");
            return dic; 
        }


        #endregion
        #region Tipo Identificacion

        public static Dictionary<string, string> GetTipoIdentificacion()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Cédula", "Cédula"); dic.Add("RUC", "RUC"); dic.Add("Pasaporte", "Pasaporte");
            return dic;
        }

        #endregion
        #region Genero Cuenta
        public static Dictionary<string, string> GetGenCuenta()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(1 + "", "Activo");
            dic.Add(2 + "", "Pasivo");
            dic.Add(3 + "", "Patrimonio");
            dic.Add(4 + "", "Ingresos");
            dic.Add(5 + "", "Costos");
            dic.Add(6 + "", "Gasto");
            
            
            return dic;
        }
        #endregion
        #region DebCre

        public static Dictionary<string, string> GetDebCre()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
             dic.Add(Constantes.cDebito + "", "Debito");
            dic.Add(Constantes.cCredito + "", "Credito");
            
            
            return dic;
        
        }

        #endregion
        #region Contabilizacion
        public static Dictionary<string, string> GetContabilizacion()
        {
           
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(1 + "", "Caja Pto Venta");
            dic.Add(2 + "", "Transaccion");
            dic.Add(3 + "", "Tarjeta");
            dic.Add(4 + "", "Cheque Fecha");
            dic.Add(5 + "", "Cuenta Contable");
            return dic;
        }


        public static Dictionary<string, string> GetTipoInforme()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("a", "Acumulado");
            dic.Add("m", "Mensual");

            return dic;
        }
      


        #endregion
        #region Transaccion
        public static Dictionary<string, string> GetTransaccion()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(1 + "", "Letra de Cambio");
            dic.Add(2 + "", "Pagare");
            dic.Add(3 + "", "Cheque posfechado");
            dic.Add(4 + "", "Depositos");      
            return dic;
        }
        #endregion
        #region Banco

        public static Dictionary<string, string> GetBancos()
        {
            List<Banco> lst = BancoBLL.GetAll("ban_estado=1 and ban_empresa=" + cod_empresa, "");
            return lst.ToDictionary(p => p.ban_codigo.ToString(), p => p.ban_nombre);
        }

        #endregion
        #region Tipo Banco

        public static Dictionary<string, string> GetTipoBanco()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(0 + "", "Ahorros"); dic.Add(1 + "", "Corriente");
            return dic;
        }

        #endregion
        #region Estado
        public static Dictionary<string, string> GetEstado()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "--Activo--"); dic.Add("1", "Si"); dic.Add("0", "No");
            return dic;
        }

        public static Dictionary<string, string> GetEstadoComprobante()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(0 + "", "Proceso");
            dic.Add(1 + "", "Guardado");
            dic.Add(2 + "", "Mayorizado");
            dic.Add(3 + "", "PorAutorizar");
            dic.Add(9 + "", "Eliminado");          
              return dic;        
        }

        public static Dictionary<string, string> GetEstadoCobro()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "Pendiente");
            dic.Add("2", "Cobrado");
            return dic;
        }

        #endregion
        #region Genero
        public static Dictionary<string, string> GetGenero()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("M", "Masculino"); dic.Add("F", "Femenino");
            return dic;
        }
        #endregion
        #region Contribuyente

        public static Dictionary<string, string> GetContribuyente()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Natural", "Natural"); dic.Add("Sociedad", "Sociedad");
            return dic;
        }

        #endregion
        #region Empresa
        public static Dictionary<string, string> GetEmpresa()
        {
            List<Empresa> lstempresa = EmpresaBLL.GetAll("emp_estado=1", "emp_codigo");
            return lstempresa.ToDictionary(p => p.emp_codigo.ToString(), p => p.emp_nombre);
        }


        #endregion
        #region Cpersona
        public static Dictionary<string, string> GetCpersona()
        {
            List<Cpersona> lstempresa = CpersonaBLL.GetAll("cper_estado=1 and cper_empresa=" + cod_empresa, "cper_codigo");
            return lstempresa.ToDictionary(p => p.cper_codigo.ToString(), p => p.cper_nombre);
        }
        #endregion        
        #region Catpersona
        public static Dictionary<string, string> GetCatpersona()
        {
            List<Catcliente> lstempresa = CatclienteBLL.GetAll("cat_estado=1 and cat_empresa=" + cod_empresa, "cat_codigo");
            return lstempresa.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        public static Dictionary<string, string> GetCatpersona(int tipo)
        {
            if (tipo>0)
            {
                List<Catcliente> lstpersona = CatclienteBLL.GetAll(new WhereParams("cat_estado ={0} and cat_empresa = {1} and cat_tipo={2}", Constantes.cEstadoGrabado, cod_empresa, tipo), "cat_codigo");
                return lstpersona.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
            }
            else
                return GetCatpersona();
        }    





        #endregion
        #region Tpersona
        public static Dictionary<string, string> GetTpersona()
        {
            List<Tpersona> lstempresa = TpersonaBLL.GetAll("tper_estado=1 and tper_empresa=" + cod_empresa, "tper_codigo");
            return lstempresa.ToDictionary(p => p.tper_codigo.ToString(), p => p.tper_nombre);
        }
        #endregion
        #region Unida medida
        public static Dictionary<string, string> GetUmedida()
        {
            List<Umedida> lstempresa = UmedidaBLL.GetAll("umd_estado=1 and umd_empresa=" + cod_empresa, "umd_codigo");
            return lstempresa.ToDictionary(p => p.umd_codigo.ToString(), p => p.umd_nombre);
        }

        #endregion
        #region Producto
        public static Dictionary<string, string> GetProducto()
        {
            List<Producto> lstempresa = ProductoBLL.GetAll("pro_estado=1 and pro_empresa=" + cod_empresa, "pro_codigo");
            return lstempresa.ToDictionary(p => p.pro_codigo.ToString(), p => p.pro_nombre);
        }
        #endregion
        #region Impuesto
        public static Dictionary<string, string> GetImpuesto()
        {
            List<Impuesto> lstimpuesto = ImpuestoBLL.GetAll("imp_estado=1 and imp_empresa=" + cod_empresa, "imp_codigo");
            return lstimpuesto.ToDictionary(p => p.imp_codigo.ToString(), p => p.imp_nombre.ToString());
        }

        public static Dictionary<string, string> GetImpuestoIVA()
        {
            List<Impuesto> lstimpuesto = ImpuestoBLL.GetAll("imp_iva=1 and imp_estado=1 and imp_empresa=" + cod_empresa, "imp_codigo");
            return lstimpuesto.ToDictionary(p => p.imp_codigo.ToString(), p => p.imp_nombre.ToString());
        }
        #endregion
        #region TProducto
        public static Dictionary<string, string> GetTproducto()
        {
            List<Tproducto> lstempresa = TproductoBLL.GetAll("tpr_estado=1 and tpr_empresa=" + cod_empresa, "tpr_codigo");
            return lstempresa.ToDictionary(p => p.tpr_codigo.ToString(), p => p.tpr_nombre);
        }
        public static Dictionary<string, string> GetGproducto()
        {
            List<Gproducto> lstempresa = GproductoBLL.GetAll("gpr_estado=1 and gpr_empresa=" + cod_empresa, "gpr_codigo");
            return lstempresa.ToDictionary(p => p.gpr_codigo.ToString(), p => p.gpr_nombre);
        }



        #endregion
        #region lista precios
        public static Dictionary<string, string> GetListaprecio()
        {
            List<Listaprecio> lstempresa = ListaprecioBLL.GetAll("lpr_estado=1 and lpr_empresa=" + cod_empresa, "lpr_codigo");
            return lstempresa.ToDictionary(p => p.lpr_codigo.ToString(), p => p.lpr_nombre);
        }



        #endregion
        #region Vehiculos
        public static Dictionary<string, string> GetVehiculos()
        {
            List<Vehiculo> lstvehiculo = VehiculoBLL.GetAll("veh_estado=1 and veh_empresa=" + cod_empresa, "veh_codigo");
            return lstvehiculo.ToDictionary(p => p.veh_codigo.ToString(), p => p.veh_tipovehiculo + " " + p.veh_placa + " " + p.veh_modelo);
        }

        #endregion
        #region Moneda
        public static Dictionary<string, string> GetMoneda()
        {
            List<Moneda> lstvehiculo = MonedaBLL.GetAll("mon_estado=1 and mon_empresa=" + cod_empresa, "mon_codigo");
            return lstvehiculo.ToDictionary(p => p.mon_codigo.ToString(), p => p.mon_nombre);
        }

        #endregion
        #region Modulo
        public static Dictionary<string, string> GetModulos()
        {
            List<Modulo> lstcatalogo = ModuloBLL.GetAll("mod_estado=1", "mod_codigo");
            return lstcatalogo.ToDictionary(p => p.mod_codigo.ToString(), p => p.mod_nombre);
        }
        #endregion
        #region Formulario
        public static Dictionary<string, string> GetFormularios()
        {
            List<Formulario> lstcatalogo = FormularioBLL.GetAll("for_estado=1", "for_codigo");
            return lstcatalogo.ToDictionary(p => p.for_codigo.ToString(), p => p.for_descripcion);
        }
        #endregion
        #region Usuarios
        public static Dictionary<string, string> GetUsuario()
        {
            List<Usuario> lstusuario = UsuarioBLL.GetAll("usr_estado=1", "usr_id");
            return lstusuario.ToDictionary(p => p.usr_id, p => p.usr_nombres);
        }

        public static Dictionary<string, string> GetUsuarioxEmpresa(int? empresa)
        {
            if (empresa.HasValue)
            {

                List<Usuarioxempresa> lstpersona = UsuarioxempresaBLL.GetAll(new WhereParams("uxe_empresa = {0} and uxe_estado=1", empresa), "");
                return lstpersona.ToDictionary(p => p.uxe_usuario.ToString(), p => p.uxe_usuario);
            }
            else
                return new Dictionary<string, string>();

        }
        #endregion
        #region Perfil
        public static Dictionary<string, string> GetPerfil()
        {
            List<Perfil> lstperfil = PerfilBLL.GetAll("per_estado=1", "per_id");
            return lstperfil.ToDictionary(p => p.per_id, p => p.per_descripcion);
        }
        #endregion
        #region Muenu
        public static Dictionary<string, string> GetMenu()
        {
            List<Menu> lstmenu = MenuBLL.GetAll("men_estado=1", "");
            return lstmenu.ToDictionary(p => p.men_id.ToString(), p => p.men_nombre);
        }

        public static Dictionary<string, string> GetMenu(string perfil)
        {
            List<Menu> lstmenu = MenuBLL.GetAll("men_estado=1", "");
            List<Perfilxmenu> lstperfil = PerfilxmenuBLL.GetAll(new WhereParams("per_estado={0} and pxm_perfil ilike {1}", 1, perfil), "per_id");
            List<Menu> result = new List<Menu>();
            foreach (Menu item in lstmenu)
            {
                Boolean flag = false;    
                foreach (Perfilxmenu obj in lstperfil)
                {
                    if (item.men_id == obj.pxm_menu)
                        flag = true;
                }
                if (!flag)
                    result.Add(item);
             }
            return result.ToDictionary(p => p.men_id.ToString(), p => p.men_nombre);
        }
        #endregion
        #region Almacen
        public static Dictionary<string, string> GetAlmacen()
        {
            List<Almacen> lstmenu = AlmacenBLL.GetAll("alm_estado=1 and alm_empresa=" + cod_empresa, "alm_codigo");
            return lstmenu.ToDictionary(p => p.alm_codigo.ToString(), p => p.alm_nombre);
        }
        public static Dictionary<string, string> GetIDAlmacen()
        {
            List<Almacen> lstmenu = AlmacenBLL.GetAll("alm_estado=1 and alm_empresa=" + cod_empresa, "alm_codigo");
            return lstmenu.ToDictionary(p => p.alm_codigo.ToString(), p => p.alm_id + " " + p.alm_nombre);
        }
        #endregion
        #region Bodega
        public static Dictionary<string, string> GetBodega()
        {
            List<Bodega> lst= BodegaBLL.GetAll("bod_estado=1 and bod_empresa=" + cod_empresa, "bod_codigo");
            return lst.ToDictionary(p => p.bod_codigo.ToString(), p => p.bod_nombre);
        }
        public static Dictionary<string, string> GetIDBodega()
        {
            List<Bodega> lst = BodegaBLL.GetAll("bod_estado=1 and bod_empresa=" + cod_empresa, "bod_codigo");
            return lst.ToDictionary(p => p.bod_codigo.ToString(), p => p.bod_id + " " + p.bod_nombre);
        }

        public static Dictionary<string, string> GetBodega(int empresa, int? almacen)
        {
            if (almacen.HasValue)
            {

                List<Bodega> lst= BodegaBLL.GetAll(new WhereParams("bod_empresa ={0} and bod_almacen = {1}", empresa, almacen), "bod_id");
                return lst.ToDictionary(p => p.bod_codigo.ToString(), p => p.bod_id + " " + p.bod_nombre);
            }
            else
                return new Dictionary<string, string>();

        }
      

        #endregion
        #region Persona
        public static Dictionary<string, string> GetPersonas()
        {
            List<Persona> lstpersona = PersonaBLL.GetAll("per_estado=1 and per_empresa=" + cod_empresa, "per_codigo");
            return lstpersona.ToDictionary(p => p.per_codigo.ToString(), p => p.per_razon);
        }



        #region Chofer
        public static Dictionary<string, string> GetChofer()
        {
            List<Chofer> lstpersona = ChoferBLL.GetAll("cho_estado=1 and cho_empresa=" + cod_empresa, "cho_persona");
            return lstpersona.ToDictionary(p => p.cho_persona.ToString(), p => p.cho_nombrechofer + " " + p.cho_apellidochofer);
        }
        #endregion

        public static Dictionary<string, string> GetSocios()
        {
            List<Socioempleado> lstpersona = SocioempleadoBLL.GetAll("soc_estado=1 and soc_empresa=" + cod_empresa, "");
            return lstpersona.ToDictionary(p => p.soc_persona.ToString(), p => p.soc_nombre + " " + p.soc_apellido);
        }
        #endregion
        #region Catalogos
        #region EstadoCivil
        public static Dictionary<string, string> GetEstadoCivil()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo like 'Estado Civil' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        #endregion
        #region Persona
        public static Dictionary<string, string> GetTipoPersonas()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Persona' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        #endregion
        #region Cargos
        public static Dictionary<string, string> GetCargos()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Cargo' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        #endregion
        #region Emisores
        public static Dictionary<string, string> GetEmisores()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Emisor' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        #endregion
        #region Departamentos
        public static Dictionary<string, string> GetDepartamentos()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Departamento' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        #endregion
        #region Profesion
        public static Dictionary<string, string> GetProfesiones()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Profesion' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        #endregion
        #region Instruccion
        public static Dictionary<string, string> GetInstruccion()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Instruccion' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        #endregion
        #region Vehiculo
        public static Dictionary<string, string> GetTipoVehiculos()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Vehiculo' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_nombre, p => p.cat_nombre);
        }

        public static Dictionary<string, string> GetModeloVehiculos()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Modelo' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_nombre, p => p.cat_nombre);
        }



        #endregion
        #region Funciones Pais, Provincia, Canton
        public static Dictionary<string, string> GetPaises()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll("cat_tipo = 'Pais' and cat_estado=1", "");
            return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }
        public static Dictionary<string, string> GetProvincias(int? pais)
        {
            if (pais.HasValue)
            {
                /* WhereParams parametros = new WhereParams();
                 parametros.Add(new WhereParams("cat_tipo", "=", "Provincia"));
                 parametros.Add(new WhereParams("cat_padre", "=", pais.Value));*/

                List<Catalogo> lstcatalogo = CatalogoBLL.GetAll(new WhereParams("cat_tipo = {0} and cat_padre = {1} and cat_estado=1", "Provincia", pais.Value), "cat_nombre");
                return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
            }
            else
                return new Dictionary<string, string>();

        }
        public static Dictionary<string, string> GetCantones(int? provincia)
        {
            if (provincia.HasValue)
            {
                /**  WhereParams parametros = new WhereParams();
                  parametros.Add(new WhereParams("cat_tipo", "=", "Canton"));
                  parametros.Add(new WhereParams("cat_padre", "=", provincia.Value));*/
                List<Catalogo> lstcatalogo = CatalogoBLL.GetAll(new WhereParams("cat_tipo = {0} and cat_padre = {1} and cat_estado=1", "Canton", provincia.Value), "");
                //   List<Catalogo> lstcatalogo = CatalogoBLL.GetAll(parametros, "cat_nombre");
                return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
            }
            else
                return new Dictionary<string, string>();
        }

        public static Dictionary<string, string> GetCantones()
        {
            List<Catalogo> lstcatalogo = CatalogoBLL.GetAll(new WhereParams("cat_tipo = {0}  and cat_estado=1", "Canton"), "cat_nombre");              
                return lstcatalogo.ToDictionary(p => p.cat_codigo.ToString(), p => p.cat_nombre);
        }

        #endregion
        #endregion
        #region Anios
        public static Dictionary<string, string> GetAnios()
        {
            Dictionary<string, string> dicAnio = new Dictionary<string, string>();

            DateTime time = DateTime.Today;
            for (int i = time.Year + 1; i >= 1970; i--)
                dicAnio.Add(i + "", i + "");
            return dicAnio;
        }
        #endregion
        #region TipoCtipocom
        public static Dictionary<string, string> GetTipoCtipocom()
        {
            Dictionary<string, string> dicAnio = new Dictionary<string, string>();

            dicAnio.Add(0 + "", "Secuencial");
            dicAnio.Add(1 + "", "Periodo");
            return dicAnio;
        }
        #endregion
        #region Ctipocom
        public static Dictionary<string, string> GetCtipocom()
        {
            List<Ctipocom> lstpersona = CtipocomBLL.GetAll("cti_estado=1 and cti_empresa=" + cod_empresa, "cti_codigo");
            return lstpersona.ToDictionary(p => p.cti_codigo.ToString(), p => p.cti_id);
        }
        public static Dictionary<string, string> GetCtipocom(int? tipodoc)
        {
          /*  List<Ctipocom> lstpersona = CtipocomBLL.GetAll("cti_estado=1 and cti_empresa=" + cod_empresa, "cti_codigo");
            return lstpersona.ToDictionary(p => p.cti_codigo.ToString(), p => p.cti_id);
            */


            if (tipodoc.HasValue)
            {

               // List<Puntoventa> lstpersona = PuntoventaBLL.GetAll(new WhereParams("pve_almacen = {0}", almacen), "pve_id");
                List<Dtipodoc> lstdtipodoc = DtipodocBLL.GetAll(new WhereParams("dtp_estado={0} and dtp_tipodoc={1} and dtp_empresa={2}", 1, tipodoc, cod_empresa), "dtp_ctipocom");
                List<Ctipocom> lstpersona =new List<Ctipocom>();
                foreach (Dtipodoc item in lstdtipodoc)
                { 
                    Ctipocom obj=new Ctipocom();
                    obj.cti_empresa_key=item.dtp_empresa;
                    obj.cti_codigo_key=item.dtp_ctipocom;
                    lstpersona.Add(CtipocomBLL.GetByPK(obj));
                }
               
                return lstpersona.ToDictionary(p => p.cti_codigo.ToString(), p => p.cti_id);
            }
            else
                return new Dictionary<string, string>();

        }

        #endregion
        #region PuntoVenta
        public static Dictionary<string, string> GetPuntoVenta(int empresa, int? almacen)
        {
            if (almacen.HasValue)
            {

                List<Puntoventa> lstpersona = PuntoventaBLL.GetAll(new WhereParams("pve_empresa ={0} and pve_almacen = {1}", empresa, almacen), "pve_id");
                return lstpersona.ToDictionary(p => p.pve_secuencia.ToString(), p => p.pve_id + " " + p.pve_nombre);
            }
            else
                return new Dictionary<string, string>();

        }
        public static Dictionary<string, string> GetPuntoVenta(int? almacen)
        {
            if (almacen.HasValue)
            {

                List<Puntoventa> lstpersona = PuntoventaBLL.GetAll(new WhereParams("pve_almacen = {0}", almacen), "pve_id");
                return lstpersona.ToDictionary(p => p.pve_secuencia.ToString(), p => p.pve_id + " " + p.pve_nombre);
            }
            else
                return new Dictionary<string, string>();

        }
        #endregion
        #region Factura
        public static Dictionary<string, string> GetFactura(WhereParams where)
        {
            if (where.valores.Count() > 0)
            {
                List<Ccomenv> lstpersona = CcomenvBLL.GetAll(where, "");
                return lstpersona.ToDictionary(p => p.cenv_comprobante.ToString(), p => p.cenv_doctran.ToString());
            }
            else
                return new Dictionary<string, string>();
        }
        #endregion
        #region Politica
        public static Dictionary<string, string> GetPolitica()
        {
            List<Politica> lstpolitica = PoliticaBLL.GetAll("pol_estado=1 and pol_empresa=" + cod_empresa, "pol_codigo");
            return lstpolitica.ToDictionary(p => p.pol_codigo.ToString(), p => p.pol_nombre);


        }
        public static Dictionary<string, string> GetPolitica(int tipo)
        {
            if (tipo > 0)
            {
                List<Politica> lstpolitica = PoliticaBLL.GetAll(new WhereParams("pol_estado={0} and pol_empresa={1} and pol_tclipro={2}", Constantes.cEstadoGrabado, cod_empresa, tipo), "pol_codigo");
                return lstpolitica.ToDictionary(p => p.pol_codigo.ToString(), p => p.pol_nombre);

            }
            else
                return GetPolitica();
        }


        public static Dictionary<string, string> GetPolitica(Comprobante comp)
        {
            string parampolitcas = Constantes.GetPoliticas(comp.crea_usr, comp.com_empresa, comp.com_almacen, comp.com_pventa, comp.com_tipodoc);

            WhereParams parametros = new WhereParams("pol_estado={0} and pol_empresa={1} and pol_tclipro={2}", Constantes.cEstadoGrabado, comp.com_empresa, comp.com_tclipro);
            string wherepoliticas = "";
            string[] arraypoliticas = parampolitcas.Split('-');
            for (int i = 0; i < arraypoliticas.Length; i++)
            {
                if (arraypoliticas[i]!="")
                    wherepoliticas += ((wherepoliticas!= "") ? " or " : "") + " pol_id = '" + arraypoliticas[i]+"'";
                
            }
            parametros.where += (wherepoliticas != "") ? " and ("+wherepoliticas+")" : "";
            //List<Politica> lstpolitica = PoliticaBLL.GetAll(new WhereParams("pol_estado={0} and pol_empresa={1} and pol_tclipro={2}", Constantes.cEstadoGrabado, comp.com_empresa, comp.com_tclipro), "pol_id, pol_nombre");
            try
            {
                List<Politica> lstpolitica = PoliticaBLL.GetAll(parametros, "pol_id, pol_nombre");
                return lstpolitica.ToDictionary(p => p.pol_codigo.ToString(), p => p.pol_id + " - " + p.pol_nombre.ToUpper());
            }
            catch 
            {
                List<Politica> lstpolitica = PoliticaBLL.GetAll(new WhereParams("pol_estado={0} and pol_empresa={1} and pol_tclipro={2}", Constantes.cEstadoGrabado, comp.com_empresa, comp.com_tclipro), "pol_id, pol_nombre");
                return lstpolitica.ToDictionary(p => p.pol_codigo.ToString(), p => p.pol_id + " - " + p.pol_nombre.ToUpper());
            }


        }



        #endregion
        #region tipodoc
        public static Dictionary<string, string> GetTipodoc()
        {
            List<Tipodoc> lstpersona = TipodocBLL.GetAll("tpd_estado=1", "tpd_codigo");
            return lstpersona.ToDictionary(p => p.tpd_codigo.ToString(), p => p.tpd_nombre);
        }
        #endregion
        #region Dtipodoc
        public static Dictionary<string, string> GetDtipodocByTipodoc(int tipodoc)
        {

            List<Dtipodoc> lst = DtipodocBLL.GetAll(new WhereParams("dtp_empresa ={0} and dtp_tipodoc={1} and dtp_estado=1", cod_empresa, tipodoc), "dtp_ctipocom");
            return lst.ToDictionary(p => p.dtp_ctipocom.ToString(), p => p.dtp_ctipocomid + " - " + p.dtp_ctipocomnombre);
        }

        #endregion
        #region Centro
        public static Dictionary<string, string> GetCentro()
        {
            List<Centro> lstmenu = CentroBLL.GetAll("cen_estado=1 and cen_empresa=" + cod_empresa, "cen_codigo");
            return lstmenu.ToDictionary(p => p.cen_codigo.ToString(), p => p.cen_nombre);
        }
        #endregion
        #region Cuenta
        public static Dictionary<string, string> GetCuenta()
        {
            List<Cuenta> lstmenu = CuentaBLL.GetAll("cue_estado=1 and cue_empresa=" + cod_empresa, "cue_codigo");
            return lstmenu.ToDictionary(p => p.cue_codigo.ToString(), p => p.cue_nombre);
        }
        public static Dictionary<string, string> GetCuentaMovi()
        {
            List<Cuenta> lstmenu = CuentaBLL.GetAll("cue_movimiento = 1 and cue_estado=1 and cue_empresa=" + cod_empresa, "cue_codigo");
            return lstmenu.ToDictionary(p => p.cue_codigo.ToString(), p => p.cue_nombre);
        }
        #endregion
        #region Concepto
        public static Dictionary<string, string> GetConcepto()
        {
            List<Concepto> lstmenu = ConceptoBLL.GetAll("con_estado=1 and con_empresa=" + cod_empresa, "con_codigo");
            return lstmenu.ToDictionary(p => p.con_codigo.ToString(), p => p.con_nombre);
        }
        #endregion
        #region TipoCuenta
        public static Dictionary<string, string> GetTipoCuenta()
        {
            Dictionary<string, string> dicTipoCuenta = new Dictionary<string, string>();


            dicTipoCuenta.Add("Ahorro", "Ahorro");
            dicTipoCuenta.Add("Corriente", "Corriente");

            return dicTipoCuenta;
        }
        #endregion
        #region TipoCuenta
        public static Dictionary<string, string> GetIvafuente()
        {
            Dictionary<string, string> dicTipoCuenta = new Dictionary<string, string>();


            dicTipoCuenta.Add(0 + "", "IVA");
            dicTipoCuenta.Add(1 + "", "Renta");

            return dicTipoCuenta;
        }
        #endregion
        #region Tipo Pagos

        public static Dictionary<string, string> GetTipoPago()
        {
            List<Tipopago> lst = TipopagoBLL.GetAll(new WhereParams("tpa_empresa = {0} and tpa_estado= {1}", cod_empresa, 1), "tpa_codigo");
            return lst.ToDictionary(p => p.tpa_codigo.ToString(), p => p.tpa_nombre);
        }

        public static Dictionary<string, string> GetPorcentajesRetencion(string tipo)
        {
            List<PorcentajeRetencion> lst = new JavaScriptSerializer().Deserialize<List<PorcentajeRetencion>>(Constantes.GetParameter("codigosporcret"));
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (PorcentajeRetencion item in lst.Where(w => w.tipo == tipo))
            {
                dic.Add(item.porcentaje.ToString(), item.porcentaje.ToString());
            }


            return dic;
        }


        #endregion
        #region Rutas
        public static Dictionary<string, string> GetRuta()
        {
            List<Ruta> lst = RutaBLL.GetAll("rut_estado=1 and rut_empresa=" + cod_empresa, "rut_nombre");
            return lst.ToDictionary(p => p.rut_codigo.ToString(), p => p.rut_nombre.ToUpper());
            //return lst.ToDictionary(p => p.rut_codigo.ToString(), p => p.rut_nombre + " " + p.rut_origen + "-" + p.rut_destino);
        }

        public static Dictionary<string, string> GetRutaByAlmacen(int empresa,int almacen)
        {

            Almacen alm = AlmacenBLL.GetByPK(new Almacen { alm_empresa = empresa, alm_empresa_key = empresa, alm_codigo = almacen, alm_codigo_key = almacen });

            List<Ruta> lst = RutaBLL.GetAll(new WhereParams("rut_estado={0} and rut_empresa={1} and rut_origen ILIKE {2}",1,empresa,"%"+alm.alm_nombre+"%"),"rut_nombre");
            return lst.ToDictionary(p => p.rut_codigo.ToString(), p => p.rut_nombre.ToUpper());
        }

        #endregion
        #region UsuarioEmpresa
        public static Dictionary<string, string> GetEmpresasUsuario(string idusuario)
        {
            List<Usuarioxempresa> lst = UsuarioxempresaBLL.GetAll(new WhereParams("uxe_usuario={0} and uxe_estado={1} ", idusuario, 1), "");
            return lst.ToDictionary(p => p.uxe_empresa.ToString(), p => p.uxe_nombreempresa.ToString());
        }

        #endregion
        #region Retdato
        public static Dictionary<string, string> GetRetdato()
        {
            List<Retdato> lst = RetdatoBLL.GetAll("rtd_estado=1 and rtd_empresa=" + cod_empresa, "rtd_codigo");
            return lst.ToDictionary(p => p.rtd_codigo.ToString(), p => p.rtd_campo);
        }
        #endregion
        #region Tablacoa
        public static Dictionary<string, string> GetTablacoa()
        {
            List<Tablacoa> lst = TablacoaBLL.GetAll("tab_estado=1 and tab_empresa=" + cod_empresa, "tab_codigo");
            return lst.ToDictionary(p => p.tab_codigo.ToString(), p => p.tab_nombre);
        }
        #endregion
        #region Transacc
        public static Dictionary<string, string> GetTransacc()
        {
            List<Transacc> lst = TransaccBLL.GetAll("tra_estado=1", "tra_secuencia");
            return lst.ToDictionary(p => p.tra_secuencia.ToString(), p => p.tra_nombre);
        }

        public static Dictionary<string, string> GetTransacc(int? modulo)
        {
            if (modulo.HasValue)
            {

                List<Transacc> lst = TransaccBLL.GetAll(new WhereParams("tra_modulo = {0}", modulo), "tra_secuencia");
                return lst.ToDictionary(p => p.tra_secuencia.ToString(), p => p.tra_nombre);
            }
            else
                return new Dictionary<string, string>();

        }
        


        #endregion
        #region Tarjetas

        public static Dictionary<string, string> GetTarjeta()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("0", "VISA");
            dic.Add("1", "MASTERCARD");
            return dic;
        }

        #endregion
        #region Hojas de Ruta


        public static Dictionary<string, string> GetHojasRutaByEmpresaAlmacen(int empresa, int almacen)
        {

            List<Comprobante> lst = ComprobanteBLL.GetAll(new WhereParams("com_empresa = {0} and com_almacen={1} and com_tipodoc={2} and (com_estado = {3} or com_estado={4})", empresa, almacen, 5, Constantes.cEstadoGrabado, Constantes.cEstadoProceso), "com_numero desc");
            return lst.ToDictionary(p => p.com_codigo.ToString(), p => p.com_doctran);


            //return lst.ToDictionary(p => p.codigocabecera.ToString(), p => p.doctrancabecera);
        }


        public static Dictionary<string, string> GetHojasRutaByRuta(int ruta)
        {

            List<Comprobante> lst = ComprobanteBLL.GetAll(new WhereParams("com_empresa = {0} and com_ruta={1}", cod_empresa ,ruta), "com_numero desc");
            return lst.ToDictionary(p => p.com_codigo.ToString(), p => p.com_doctran);

            
            //return lst.ToDictionary(p => p.codigocabecera.ToString(), p => p.doctrancabecera);
        }

        #endregion
        #region Usuario Documentos
        public static Dictionary<string, string> GetUsrdoc(string idusuario)
        {
            List<Usrdoc> lst = UsrdocBLL.GetAll(new WhereParams("udo_usuario={0}",idusuario),"udo_tipodoc");            
            return lst.ToDictionary(p => p.udo_tipodoc.ToString(), p => p.udo_nombretipodoc);
        }

        #endregion

        #region Electronicos

        public static Dictionary<string, string> GetElectronicos()
        {
            string parelectronicos = Constantes.GetParameter("electronicos");
            List<Tipodoc> lstdoc = TipodocBLL.GetAll("", "");
            var serializer = new JavaScriptSerializer();
            List<Electronicos> lst = serializer.Deserialize<List<Electronicos>>(parelectronicos);


            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (Electronicos item in lst)
            {

                Tipodoc td = lstdoc.Find(delegate (Tipodoc t) { return t.tpd_codigo == item.tipodoc; });
                dic.Add(td.tpd_codigo.ToString(), td.tpd_nombre);
                

            }



            return dic;
        }


        public static Dictionary<string, string> GetElectronicosRetencion()
        {
            string parelectronicos = Constantes.GetParameter("electronicos");
            List<Tipodoc> lstdoc = TipodocBLL.GetAll("", "");
            var serializer = new JavaScriptSerializer();
            List<Electronicos> lst = serializer.Deserialize<List<Electronicos>>(parelectronicos);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (Electronicos item in lst)
            {
                

                Tipodoc td = lstdoc.Find(delegate (Tipodoc t) { return t.tpd_codigo == item.tipodoc; });

                if (!td.tpd_nombre.Contains("RETEN"))
                    dic.Add(td.tpd_codigo.ToString(), td.tpd_nombre);


            }



            return dic;
        }


        public static Dictionary<string, string> GetEstadosElectronico()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] enumNames = Enum.GetNames(typeof(Enums.EstadoElectronico));
            foreach (string item in enumNames)
            {
                bool add = true;
                int value = (int)Enum.Parse(typeof(Enums.EstadoElectronico), item);

                dic.Add(item.ToString(), item.ToString());



            }

            return dic;
        }


        #endregion

        #region Varios 


        public static Dictionary<string, string> GetTiposFacGui()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            Tipodoc fac = Constantes.cFactura;
            Tipodoc gui = Constantes.cGuia;

            dic.Add(gui.tpd_codigo.ToString(), gui.tpd_nombre);
            dic.Add(fac.tpd_codigo.ToString(), fac.tpd_nombre);
            return dic;
        }

        public static Dictionary<string, string> GetEstadosEnvio()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "Por cobrar");
            dic.Add("2", "Por despachar");
            dic.Add("3", "Despachado");
            return dic;
        }

        public static Dictionary<string, string> GetOperadores()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("=", "=");
            dic.Add(">", ">");
            dic.Add("<", "<");
            return dic;
        }


        public static Dictionary<string, string> GetTotales()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("0", "Sub 0");
            dic.Add("1", "Sub IVA");
            dic.Add("2", "IVA");
            dic.Add("3", "Total");
            return dic;
        }


        public static Dictionary<string, string> GetTipoObligacionInv()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "PRODUCTO");
            dic.Add("2", "CUENTA");
            return dic;
        }

        public static Dictionary<string, string> GetEstadoCuentaOrden()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "POR FECHA");
            dic.Add("2", "POR COMPROBANTE");
            return dic;
        }


        public static Dictionary<string, string> GetTipoCxC()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("1", "De planilla");
            dic.Add("2", "Sin planilla");
            return dic;
        }



        public static Dictionary<string, string> GetTipoComprobanteRetencion()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("01", "FACTURA");
            dic.Add("03", "LIQUIDACIÓN DE COMPRA");
            dic.Add("41", "COMPROBANTE DE VENTA");
            return dic;
        }

        #endregion

        public static Dictionary<string, string> GetSINO()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("SI", "SI"); dic.Add("NO", "NO");
            return dic;
        }


        public static Dictionary<string, string> GetGeneros()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();


            string[] enumNames = Enum.GetNames(typeof(Enums.GeneroCuenta));
            foreach (string item in enumNames)
            {
                int value = (int)Enum.Parse(typeof(Enums.GeneroCuenta), item);
                dic.Add(value.ToString(), item.ToString());


            }

            return dic;
        }

        public static Dictionary<string, string> GetTipoBalance()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();


            string[] enumNames = Enum.GetNames(typeof(Enums.TipoBalance));
            foreach (string item in enumNames)
            {
                int value = (int)Enum.Parse(typeof(Enums.TipoBalance), item);
                dic.Add(value.ToString(), item.ToString());


            }

            return dic;
        }
        public static Dictionary<string, string> GetEstadosRegistro()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();


            string[] enumNames = Enum.GetNames(typeof(Enums.EstadoRegistro));
            foreach (string item in enumNames)
            {
                int value = (int)Enum.Parse(typeof(Enums.EstadoRegistro), item);
                dic.Add(value.ToString(), item.ToString());


            }

            return dic;
        }


        public static Dictionary<string, string> GetReportesVentas()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("VEN", "VENTAS ");
            dic.Add("VENTAS", "VENTAS RETENCIÓN");
            return dic;
        }

        public static Dictionary<string, string> GetGuiaFactura()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("4,13", "GUIAS Y FACTURAS");
            dic.Add("13", "GUIAS");
            dic.Add("4", "FACTURAS");
            return dic;
        }

        public static Dictionary<string, string> GetAgrupadoPor()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("HR", "Hoja Ruta");
            dic.Add("COM", "Comprobante");
            return dic;
        }


        #region Get Obj

        public static object GetObject(object objeto, string id, Type tipo)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            return GetObject(tmp, id, tipo);
        }


        public static object GetObject(Dictionary<string, object> dic, string id, Type tipo)
        {
            object obj = null;
            dic.TryGetValue(id, out obj);

            if (tipo == typeof(String))
                return Functions.Conversiones.ObjectToString(obj);
            if (tipo == typeof(Int32?))
                return Functions.Conversiones.ObjectToIntNull(obj);
            if (tipo == typeof(Decimal?))
                return Functions.Conversiones.ObjectToDecimalNull(obj);
            if (tipo == typeof(DateTime?))
                return Functions.Conversiones.ObjectToDateTimeNull(obj);
            if (tipo == typeof(Int32?))
                return Functions.Conversiones.ObjectToIntNull(obj);
            if (tipo == typeof(bool?))
                return Functions.Conversiones.ObjectToBoolNull(obj);
            if (tipo == typeof(long?))
                return Functions.Conversiones.ObjectToLongNull(obj);
            if (tipo == typeof(object[]))
                return Functions.Conversiones.ObjectToObjectArray(obj);
            return obj;
        }


        #endregion
    }
}
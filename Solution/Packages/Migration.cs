using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BusinessObjects;
using BusinessLogicLayer;
using Services;
using Functions;
using Npgsql;
using System.Data;

namespace Packages
{
    public class Migration
    {
        public static string Migrate(string path)
        {
            int i = 0;
            string line = "";

            using (StreamReader streamReader = File.OpenText(path))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                }
            }

            return "";

        }


        public static string GetArrayVal(string[] array, int pos)
        {
            string retorno = "";

            if (array.Length > pos)
                retorno = array[pos].Trim();


            return retorno;
        }

        public static string MigrateClientes(string path)
        {
            Politica politica = Constantes.GetPoliticacli();//Obtiene politica por defecto
            Catcliente catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto
            //List<Persona> personas = new List<Persona>();
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Personaxtipo> personastipos = new List<Personaxtipo>();

            int i = 0;
            string line = "";

            //0: Id
            //1: C.I/RUC
            //2: TIPO ID: CEDULA RUC PASAPORTE
            //3: NOMBRES
            //4: APELLIDOS
            //5: RAZON
            //6: DIRECCION
            //7: EMAIL
            //8: TELEFONO
            //9: CELULAR



            StringBuilder html = new StringBuilder();


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();


                //using (StreamReader streamReader = File.OpenText(path))
                using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] array = line.Split(';');
                        string id = GetArrayVal(array, 0);
                        string ciruc = GetArrayVal(array, 1).Replace("-", "");
                        string tipoid = GetArrayVal(array, 2);

                        if (tipoid.ToUpper().Contains("CED"))
                            tipoid = "Cédula";
                        if (tipoid.ToUpper().Contains("RUC"))
                            tipoid = "RUC";
                        if (tipoid.ToUpper().Contains("Pas"))
                            tipoid = "Pasaporte";

                        if (tipoid == "")
                        {
                            if (ciruc.Length == 10)
                                tipoid = "Cédula";
                            if (ciruc.Length == 13)
                                tipoid = "RUC";
                        }


                        string nombres = GetArrayVal(array, 3);
                        string apellidos = GetArrayVal(array, 4);
                        string razon = GetArrayVal(array, 5);
                        string direccion = GetArrayVal(array, 6);
                        string email = GetArrayVal(array, 7);
                        string telefono = GetArrayVal(array, 8);
                        string celular = GetArrayVal(array, 9);


                        if (!string.IsNullOrEmpty(ciruc))
                        {
                            bool pasa = true;
                            if (tipoid == "Cédula" || tipoid == "RUC")
                                pasa = Functions.Validaciones.valida_cedularuc(ciruc);
                            if (pasa)
                            {

                                Persona per = personas.Find(delegate (Persona p) { return p.per_ciruc == ciruc; });
                                if (per == null)
                                {

                                    per = new Persona();
                                    per.per_empresa = 1;
                                    per.per_id = id;
                                    per.per_ciruc = ciruc;
                                    per.per_tipoid = tipoid;
                                    per.per_nombres = nombres;
                                    per.per_apellidos = apellidos;
                                    per.per_razon = razon;
                                    per.per_direccion = direccion;
                                    per.per_mail = email;
                                    per.per_telefono = telefono;
                                    per.per_celular = celular;

                                    per.per_retfuente = Constantes.GetImpRteFte().imp_codigo;
                                    per.per_retiva = Constantes.GetImpRteIVA().imp_codigo;
                                    per.per_estado = Constantes.cEstadoGrabado;

                                    per.per_politica = politica.pol_codigo;
                                    per.per_politicaid = politica.pol_id;
                                    per.per_politicanombre = politica.pol_nombre;
                                    per.per_politicadesc = politica.pol_porc_desc;
                                    per.per_politicanropagos = politica.pol_nro_pagos;
                                    per.per_politicadiasplazo = politica.pol_dias_plazo;
                                    per.per_politicaporpagocon = politica.pol_porc_pago_con;

                                    per.crea_usr = "admin";
                                    per.crea_fecha = DateTime.Now;
                                    personas.Add(per);

                                    Personaxtipo pxt = new Personaxtipo();
                                    pxt.pxt_empresa = 1;
                                    pxt.pxt_tipo = Constantes.cCliente;
                                    pxt.pxt_politicas = politica.pol_codigo;
                                    pxt.pxt_cat_persona = catcliente.cat_codigo;
                                    pxt.crea_usr = per.crea_usr;
                                    pxt.crea_fecha = per.crea_fecha;
                                    pxt.mod_usr = per.mod_usr;
                                    pxt.mod_fecha = per.mod_fecha;



                                    per.per_codigo = PersonaBLL.InsertIdentity(transaction, per);
                                    pxt.pxt_persona = per.per_codigo;
                                    PersonaxtipoBLL.Insert(transaction, pxt);

                                    //html.AppendFormat("PERSONA: ID:{0} CI/RUC:{1} Nombres:{2} {3}<br>", id, ciruc, nombres, apellidos);


                                }
                                else
                                {
                                    html.AppendFormat("ID:{0} CI/RUC:{1} Nombres:{2} {3} repetido la persona de ID:{4}<br>", id, ciruc, nombres, apellidos, per.per_id);
                                }
                            }
                            else
                                html.AppendFormat("ID:{0} CI/RUC:{1} Nombres:{2} {3} cédula incorrecta, no se puede agregar<br>", id, ciruc, nombres, apellidos);
                        }
                        else
                            html.AppendFormat("ID:{0} Nombres:{1} {2}, sin cédula no se puede agregar<br>", id, nombres, apellidos);
                    }
                }
                html.Append("Personas agregadas correctamente " + personas.Count);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }




            return html.ToString();

        }

        public static string MigrateProveedores(string path)
        {
            Politica politica = Constantes.GetPoliticaProv();//Obtiene politica por defecto
            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto
            //List<Persona> personas = new List<Persona>();
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Personaxtipo> personastipos = PersonaxtipoBLL.GetAll("", "");

            int i = 0;
            string line = "";

            //0: Id
            //1: C.I/RUC
            //2: TIPO ID: CEDULA RUC PASAPORTE
            //3: NOMBRES
            //4: APELLIDOS
            //5: RAZON SOCIAL        
            //6: DIRECCION
            //7: EMAIL
            //8: TELEFONO
            //9: CELULAR



            StringBuilder html = new StringBuilder();


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();


                //using (StreamReader streamReader = File.OpenText(path))
                using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
                {
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] array = line.Split(';');
                        string id = GetArrayVal(array, 0);
                        string ciruc = GetArrayVal(array, 1).Replace("-", "");
                        string tipoid = GetArrayVal(array, 2);

                        if (tipoid.ToUpper().Contains("CED"))
                            tipoid = "Cédula";
                        if (tipoid.ToUpper().Contains("RUC"))
                            tipoid = "RUC";
                        if (tipoid.ToUpper().Contains("Pas"))
                            tipoid = "Pasaporte";

                        if (tipoid == "")
                        {
                            if (ciruc.Length == 10)
                                tipoid = "Cédula";
                            if (ciruc.Length == 13)
                                tipoid = "RUC";
                        }


                        string nombres = GetArrayVal(array, 3);
                        string apellidos = GetArrayVal(array, 4);
                        string razon = GetArrayVal(array, 5);
                        string direccion = GetArrayVal(array, 6);
                        string email = GetArrayVal(array, 7);
                        string telefono = GetArrayVal(array, 8);
                        string celular = GetArrayVal(array, 9);


                        if (razon == "")
                        {
                            razon = apellidos + " " + nombres;
                        }
                        if (nombres == "")
                        {
                            nombres = razon;
                        }


                        if (!string.IsNullOrEmpty(ciruc))
                        {
                            bool pasa = true;
                            if (tipoid == "Cédula" || tipoid == "RUC")
                                pasa = Functions.Validaciones.valida_cedularuc(ciruc);
                            if (pasa)
                            {

                                Persona per = personas.Find(delegate (Persona p) { return p.per_ciruc == ciruc; });
                                if (per == null)
                                {

                                    per = new Persona();
                                    per.per_empresa = 1;
                                    per.per_id = id;
                                    per.per_ciruc = ciruc;
                                    per.per_tipoid = tipoid;
                                    per.per_nombres = nombres;
                                    per.per_apellidos = apellidos;
                                    per.per_razon = razon;
                                    per.per_direccion = direccion;
                                    per.per_mail = email;
                                    per.per_telefono = telefono;
                                    per.per_celular = celular;

                                    per.per_retfuente = Constantes.GetImpRteFte().imp_codigo;
                                    per.per_retiva = Constantes.GetImpRteIVA().imp_codigo;
                                    per.per_estado = Constantes.cEstadoGrabado;

                                    per.per_politica = politica.pol_codigo;
                                    per.per_politicaid = politica.pol_id;
                                    per.per_politicanombre = politica.pol_nombre;
                                    per.per_politicadesc = politica.pol_porc_desc;
                                    per.per_politicanropagos = politica.pol_nro_pagos;
                                    per.per_politicadiasplazo = politica.pol_dias_plazo;
                                    per.per_politicaporpagocon = politica.pol_porc_pago_con;

                                    per.crea_usr = "admin";
                                    per.crea_fecha = DateTime.Now;
                                    personas.Add(per);

                                    Personaxtipo pxt = new Personaxtipo();
                                    pxt.pxt_empresa = 1;
                                    pxt.pxt_tipo = Constantes.cProveedor;
                                    pxt.pxt_politicas = politica.pol_codigo;
                                    pxt.pxt_cat_persona = categoria.cat_codigo;
                                    pxt.crea_usr = per.crea_usr;
                                    pxt.crea_fecha = per.crea_fecha;
                                    pxt.mod_usr = per.mod_usr;
                                    pxt.mod_fecha = per.mod_fecha;



                                    per.per_codigo = PersonaBLL.InsertIdentity(transaction, per);
                                    pxt.pxt_persona = per.per_codigo;
                                    PersonaxtipoBLL.Insert(transaction, pxt);
                                    personastipos.Add(pxt);
                                    //html.AppendFormat("PERSONA: ID:{0} CI/RUC:{1} Nombres:{2} {3}<br>", id, ciruc, nombres, apellidos);


                                }
                                else
                                {

                                    Personaxtipo pxt = personastipos.Find(delegate (Personaxtipo p) { return p.pxt_persona == per.per_codigo && p.pxt_tipo == Constantes.cProveedor; });
                                    if (pxt == null)
                                    {
                                        pxt = new Personaxtipo();
                                        pxt.pxt_empresa = 1;
                                        pxt.pxt_tipo = Constantes.cProveedor;
                                        pxt.pxt_politicas = politica.pol_codigo;
                                        pxt.pxt_cat_persona = categoria.cat_codigo;
                                        pxt.crea_usr = per.crea_usr;
                                        pxt.crea_fecha = per.crea_fecha;
                                        pxt.mod_usr = per.mod_usr;
                                        pxt.mod_fecha = per.mod_fecha;
                                        pxt.pxt_persona = per.per_codigo;
                                        PersonaxtipoBLL.Insert(transaction, pxt);
                                        personastipos.Add(pxt);
                                        html.AppendFormat("ID:{0} CI/RUC:{1} Nombres:{2} {3} agregado como PROVEEDOR<br>", id, ciruc, nombres, apellidos);
                                    }

                                }
                            }
                            else
                                html.AppendFormat("ID:{0} CI/RUC:{1} Nombres:{2} {3} cédula incorrecta, no se puede agregar<br>", id, ciruc, nombres, apellidos);
                        }
                        else
                            html.AppendFormat("ID:{0} Nombres:{1} {2}, sin cédula no se puede agregar<br>", id, nombres, apellidos);
                    }
                }
                html.Append("Personas agregadas correctamente " + personas.Count);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }




            return html.ToString();

        }



        public static Persona GetPersonaByNom(string nombres, List<Persona> lst)
        {

            string[] arraynom = nombres.Split(' ');
            for (int i = arraynom.Length; i > 0; i--)
            {
                string nom = "";
                for (int n = 0; n < i; n++)
                {
                    nom += (nom != "" ? " " : "") + arraynom[n];
                }
                Persona per = lst.Find(delegate (Persona p) { return p.per_razon.Contains(nom); });
                if (per!=null)
                    return per;

            }
            return null;
            //Persona per = lst.Find(delegate (Persona p) { return p.per_razon.Contains(nombres); });
            //return per;
        }
        /*
        public static Comprobante GetObligacion(Persona persona, string nrodoc, DateTime fecha, decimal? subtotal, decimal? iva)
        {
            Comprobante comprobante = new Comprobante();
            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.detalle = new List<Dcomdoc>();
            comprobante.total = new Total();

            comprobante.com_empresa = persona.per_empresa;
            comprobante.com_tipodoc = 14;
            comprobante.com_ctipocom = 12;
            comprobante.com_almacen = 1;
            comprobante.com_pventa = 1;
            comprobante.com_codclipro = persona.per_codigo;

            comprobante.ccomdoc.cdoc_acl_nroautoriza = nrodoc;
            comprobante.ccomdoc.cdoc_acl_retdato = 20000004;
            comprobante.ccomdoc.cdoc_acl_tablacoa = 4;
            comprobante.ccomdoc.cdoc_aut_factura = string.Format("{0}-{1}-{2}", "001", "001", nrodoc);
            comprobante.ccomdoc.cdoc_aut_fecha = fecha;


            comprobante.ccomdoc.cdoc_ced_ruc = persona.per_ciruc;
            comprobante.ccomdoc.cdoc_nombre = persona.per_razon;
            comprobante.ccomdoc.cdoc_direccion = persona.per_direccion;

            comprobante.com_fecha = fecha;
            comprobante.com_fechastr = fecha.ToShortDateString();



            comprobante.total.tot_subtot_0 = iva.HasValue ? 0 : subtotal.Value;
            comprobante.total.tot_subtotal = iva.HasValue ? subtotal.Value : 0;
            //comprobante.com_subtotalnoiva = subtotalnoiva;
            //comprobante.com_subtotalextiva = subtotalextiva;
            comprobante.total.tot_timpuesto = iva ?? 0;
            comprobante.total.tot_porc_impuesto = 12;
            //comprobante.total.tot_ice = valorice;
            
            
            comprobante.total.tot_total = comprobante.total.tot_subtot_0 + comprobante.total.tot_subtotal + comprobante.total.tot_timpuesto;

            Dcomdoc det = new Dcomdoc();
            det.ddoc_secuencia = comprobante.ccomdoc.detalle.Count() + 1;
            det.ddoc_cuenta = 367;
            det.ddoc_cuentanombre = "TRANSPORTES";
            det.ddoc_observaciones = "";

            det.ddoc_cantidad = 1;
            det.ddoc_precio = subtotal??0;
            det.ddoc_grabaiva = iva.HasValue ? 1 : 0;
            det.ddoc_total = subtotal??0;
            comprobante.ccomdoc.detalle.Add(det);

            return comprobante;



        }


        */

        public static Comprobante GetObligacion(Persona persona, string nrodoc, DateTime fecha, decimal? saldo)
        {
            Comprobante comprobante = new Comprobante();
            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.detalle = new List<Dcomdoc>();
            comprobante.total = new Total();

            comprobante.com_empresa = persona.per_empresa;
            comprobante.com_tipodoc = 14;
            comprobante.com_ctipocom = 12;
            comprobante.com_almacen = 1;
            comprobante.com_pventa = 1;
            comprobante.com_codclipro = persona.per_codigo;

            comprobante.ccomdoc.cdoc_acl_nroautoriza = nrodoc;
            comprobante.ccomdoc.cdoc_acl_retdato = 20000004;
            comprobante.ccomdoc.cdoc_acl_tablacoa = 4;
            comprobante.ccomdoc.cdoc_aut_factura = string.Format("{0}-{1}-{2}", "001", "001", nrodoc);
            comprobante.ccomdoc.cdoc_aut_fecha = fecha;


            comprobante.ccomdoc.cdoc_ced_ruc = persona.per_ciruc;
            comprobante.ccomdoc.cdoc_nombre = persona.per_razon;
            comprobante.ccomdoc.cdoc_direccion = persona.per_direccion;

            comprobante.com_fecha = fecha;
            comprobante.com_fechastr = fecha.ToShortDateString();



            comprobante.total.tot_subtot_0 = saldo.Value;
            comprobante.total.tot_subtotal = 0;
            //comprobante.com_subtotalnoiva = subtotalnoiva;
            //comprobante.com_subtotalextiva = subtotalextiva;
            comprobante.total.tot_timpuesto = 0;
            comprobante.total.tot_porc_impuesto = 12;
            //comprobante.total.tot_ice = valorice;


            comprobante.total.tot_total = comprobante.total.tot_subtot_0 + comprobante.total.tot_subtotal + comprobante.total.tot_timpuesto;

            Dcomdoc det = new Dcomdoc();
            det.ddoc_secuencia = comprobante.ccomdoc.detalle.Count() + 1;
            det.ddoc_cuenta = 367;
            det.ddoc_cuentanombre = "TRANSPORTES";
            det.ddoc_observaciones = "";

            det.ddoc_cantidad = 1;
            det.ddoc_precio = saldo??0;
            det.ddoc_grabaiva = 0;
            det.ddoc_total = saldo??0;
            comprobante.ccomdoc.detalle.Add(det);

            return comprobante;



        }




        public static string MigrateObligaciones(string path)
        {
            Politica politica = Constantes.GetPoliticaProv();//Obtiene politica por defecto
            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto
            //List<Persona> personas = new List<Persona>();
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Personaxtipo> personastipos = PersonaxtipoBLL.GetAll("", "");

            int i = 0;
            string line = "";

            //0: NRO FAC
            //1: NOMBRES PROV
            //2: SALDO
            //3: SUBTOTAL
            //4: IVA
            //5: RET FUE        
            //6: RET IVA
            //7: FECHA
            //8: VENCE

            DateTime fecha = new DateTime(2019, 5, 31);

            StringBuilder html = new StringBuilder();






            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');
                    string nro = GetArrayVal(array, 0);
                    string prov = GetArrayVal(array, 1);
                    decimal? saldo = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 2));
                    decimal? subtotal = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 3));
                    decimal? iva = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 4));
                    decimal? retfue = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 5));
                    decimal? retiva = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 6));

                    Persona per = GetPersonaByNom(prov, personas);
                    if (per != null)
                    {
                        try
                        {
                            //Comprobante obl = GetObligacion(per, nro, fecha, subtotal, iva);
                            Comprobante obl = GetObligacion(per, nro, fecha, saldo);
                            obl = FAC.create_obligacion(obl);
                            obl = FAC.account_obligacion(obl);
                            html.AppendFormat("Obligacion:{0} Proveedor:{1} creada correctamente...<br>", obl.com_doctran, per.per_razon);
                            i++;
                        }
                        catch (Exception ex)
                        {
                            html.AppendFormat("Proveedor:{0}, Nrodoc:{1} ERROR:{2}<br>", prov, nro, ex.Message);
                        }
                    }
                    else
                        html.AppendFormat("El proveedor {0}, no se encuentra en el registrado en el sistema<br>", prov);

                }
            }
            
            return html.ToString();

        }


        public static Comprobante GetFactura(Persona persona, string nrodoc, DateTime fecha, decimal? saldo)
        {
            Comprobante comprobante = new Comprobante();
            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomdoc.detalle = new List<Dcomdoc>();
            comprobante.total = new Total();

            comprobante.com_empresa = persona.per_empresa;
            comprobante.com_tipodoc = 4;
            comprobante.com_ctipocom = 2;
            comprobante.com_almacen = 1;
            comprobante.com_pventa = 1;
            comprobante.com_codclipro = persona.per_codigo;
            comprobante.com_numero = int.Parse(nrodoc); 
            comprobante.com_doctran = string.Format("{0}-{1}-{2}", "001", "001", nrodoc);


            //comprobante.ccomdoc.cdoc_acl_nroautoriza = nrodoc;
            //comprobante.ccomdoc.cdoc_acl_retdato = 20000004;
            //comprobante.ccomdoc.cdoc_acl_tablacoa = 4;
            //comprobante.ccomdoc.cdoc_aut_factura = string.Format("{0}-{1}-{2}", "001", "001", nrodoc);
            //comprobante.ccomdoc.cdoc_aut_fecha = fecha;


            comprobante.ccomdoc.cdoc_ced_ruc = persona.per_ciruc;
            comprobante.ccomdoc.cdoc_nombre = persona.per_razon;
            comprobante.ccomdoc.cdoc_direccion = persona.per_direccion;

            comprobante.com_fecha = fecha;
            comprobante.com_fechastr = fecha.ToShortDateString();



            comprobante.total.tot_subtot_0 = saldo.Value;
            comprobante.total.tot_subtotal = 0;
            //comprobante.com_subtotalnoiva = subtotalnoiva;
            //comprobante.com_subtotalextiva = subtotalextiva;
            comprobante.total.tot_timpuesto = 0;
            comprobante.total.tot_porc_impuesto = 12;
            //comprobante.total.tot_ice = valorice;


            comprobante.total.tot_total = comprobante.total.tot_subtot_0 + comprobante.total.tot_subtotal + comprobante.total.tot_timpuesto;

            Dcomdoc det = new Dcomdoc();
            det.ddoc_secuencia = comprobante.ccomdoc.detalle.Count() + 1;
            det.ddoc_producto = 247; //ENVIO
            det.ddoc_productoid = "00000001";
            det.ddoc_productonombre = "ENVIO";
            det.ddoc_observaciones = "";

            det.ddoc_cantidad = 1;
            det.ddoc_precio = saldo ?? 0;
            det.ddoc_grabaiva = 0;
            det.ddoc_total = saldo ?? 0;
            comprobante.ccomdoc.detalle.Add(det);

            return comprobante;



        }


        public static string MigrateFacturas(string path)
        {
            Politica politica = Constantes.GetPoliticaProv();//Obtiene politica por defecto
            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto
            //List<Persona> personas = new List<Persona>();
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Personaxtipo> personastipos = PersonaxtipoBLL.GetAll("", "");

            int i = 0;
            string line = "";

            //0: NRO FAC
            //1: NOMBRES PROV
            //2: SALDO
            //3: SUBTOTAL
            //4: IVA
            //5: RET FUE        
            //6: RET IVA
            //7: FECHA
            //8: VENCE

            DateTime fecha = new DateTime(2019, 5, 31);

            StringBuilder html = new StringBuilder();






            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');
                    string nro = GetArrayVal(array, 0);
                    string cli = GetArrayVal(array, 1);
                    decimal? saldo = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 2));
                    decimal? subtotal = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 3));
                    decimal? iva = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 4));
                    decimal? retfue = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 5));
                    decimal? retiva = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 6));

                    Persona per = GetPersonaByNom(cli, personas);
                    if (per != null)
                    {
                        try
                        {
                            //Comprobante obl = GetObligacion(per, nro, fecha, subtotal, iva);
                            Comprobante fac = GetFactura(per, nro, fecha, saldo);
                            fac = FAC.create_factura(fac);
                            fac = FAC.account_factura(fac);
                            html.AppendFormat("Factura:{0} Cliente:{1} creada correctamente...<br>", fac.com_doctran, per.per_razon);
                            i++;
                        }
                        catch (Exception ex)
                        {
                            html.AppendFormat("Cliente:{0}, Nrodoc:{1} ERROR:{2}<br>", cli, nro, ex.Message);
                        }
                    }
                    else
                        html.AppendFormat("El Cliente {0}, no se encuentra en el registrado en el sistema<br>", cli);

                }
            }

            return html.ToString();

        }




        //MIGRACION SICE

        //public static string connectionString = "Server=216.120.250.222;Port=5432;Database=sice;User ID=postgres;Password=$AdminS2109;";
        public static string connectionString = "Server=localhost;Port=5432;Database=sice;User ID=postgres;Password=admin;";

        public static string MigrateProductsTAO(int empresa)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
            NpgsqlDataAdapter daProductos = new NpgsqlDataAdapter("select * from producto where pro_empresa = " + empresa, connection);
            daProductos.Fill(ds);
            connection.Close();


            List<Almacen> almacenes = AlmacenBLL.GetAll("alm_empresa=1", "");
            List<Producto> productos = new List<Producto>();
            List<Tproducto> tipos = new List<Tproducto>();
            List<Dlistaprecio> precios = new List<Dlistaprecio>();
            List<Factor> factores = new List<Factor>();


            StringBuilder html = new StringBuilder();

            foreach (DataRow row in ds.Tables[0].Rows)
            {

                string id = row["pro_id"].ToString();
                string nombre = row["pro_nombre"].ToString();
                string tipoproducto = "producto";
                //string strprecio = (decimal)row["pro_precio"];

                decimal precio = (decimal)row["pro_precio"];
                //decimal.TryParse(strprecio, out precio);



                Tproducto tpro = tipos.Find(delegate (Tproducto t) { return t.tpr_nombre == tipoproducto; });
                if (tpro == null)
                {
                    tpro = new Tproducto();
                    tpro.tpr_empresa = 1;
                    tpro.tpr_nombre = tipoproducto;
                    tpro.tpr_codigo = tipos.Count + 1;
                    tpro.tpr_id = tpro.tpr_codigo.ToString();
                    tpro.tpr_orden = tpro.tpr_codigo;
                    tpro.tpr_estado = 1;
                    tpro.crea_usr = "admin";
                    tpro.crea_fecha = DateTime.Now;
                    tipos.Add(tpro);
                }


                Producto pro = productos.Find(delegate (Producto p) { return p.pro_id == id; });
                if (pro == null)
                {

                    pro = new Producto();

                    pro.pro_empresa = 1;
                    pro.pro_codigo = productos.Count + 1;
                    pro.pro_id = id;
                    pro.pro_nombre = nombre;
                    pro.pro_unidad = 1; //unidad;
                    pro.pro_inventario = 1;
                    pro.pro_estado = 1;
                    pro.pro_iva = 1;
                    pro.crea_usr = "admin";
                    pro.crea_fecha = DateTime.Now;
                    pro.pro_calcula = 0;
                    pro.pro_tproducto = tpro.tpr_codigo;
                    pro.pro_grupo = 1;




                    productos.Add(pro);




                    foreach (Almacen almacen in almacenes)
                    {

                        Dlistaprecio precioA = new Dlistaprecio();
                        precioA.dlpr_empresa = 1;
                        precioA.dlpr_listaprecio = 13; //LISTA CF
                        precioA.dlpr_codigo = precios.Count + 1;
                        precioA.dlpr_almacen = almacen.alm_codigo;
                        precioA.dlpr_producto = pro.pro_codigo;
                        precioA.dlpr_umedida = 1;
                        precioA.dlpr_fecha_ini = new DateTime(1980, 12, 21);
                        precioA.dlpr_fecha_fin = new DateTime(2030, 12, 21);
                        precioA.dlpr_precio = precio;
                        precioA.dlpr_estado = 1;
                        precioA.crea_usr = "admin";
                        precioA.crea_fecha = DateTime.Now;
                        precios.Add(precioA);


                        //Dlistaprecio precioB = new Dlistaprecio();
                        //precioB.dlpr_empresa = 1;
                        //precioB.dlpr_almacen = almacen.alm_codigo;
                        //precioB.dlpr_listaprecio = 13; //LISTA B
                        //precioB.dlpr_codigo = precios.Count + 1;
                        //precioB.dlpr_producto = pro.pro_codigo;
                        //precioB.dlpr_umedida = 1;
                        //precioB.dlpr_fecha_ini = new DateTime(1980, 12, 21);
                        //precioB.dlpr_fecha_fin = new DateTime(2030, 12, 21);
                        //precioB.dlpr_precio = decimal.Parse(array[12]);
                        //precioB.dlpr_estado = 1;
                        //precioB.crea_usr = "admin";
                        //precioB.crea_fecha = DateTime.Now;
                        //precios.Add(precioB);
                    }

                    Factor fac = new Factor();
                    fac.fac_empresa = 1;
                    fac.fac_producto = pro.pro_codigo;
                    fac.fac_unidad = 1;
                    fac.fac_factor = 1;
                    fac.fac_default = 1;
                    fac.fac_estado = 1;
                    fac.crea_usr = "admin";
                    fac.crea_fecha = DateTime.Now;
                    factores.Add(fac);
                }
                else
                {
                    html.AppendFormat("ID:{0} Nombre:{1} repetido en el producto COD:{2}<br>", id, nombre, pro.pro_codigo);
                }
            }

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();

                foreach (Tproducto item in tipos)
                {
                    TproductoBLL.Insert(transaction, item);
                    html.Append("TIPO:" + item.tpr_nombre + "<br>");
                }
                foreach (Producto item in productos)
                {
                    ProductoBLL.Insert(transaction, item);
                    html.Append("PRODUCTO:" + item.pro_codigo + " " + item.pro_nombre + "<br>");
                }

                foreach (Dlistaprecio item in precios)
                {
                    DlistaprecioBLL.Insert(transaction, item);
                    html.Append("PRECIO:" + item.dlpr_precio + "<br>");
                }

                foreach (Factor item in factores)
                {
                    FactorBLL.Insert(transaction, item);
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
        

        public static string MigrateClientesTAO(int empresa)
        {
            DataSet ds = new DataSet();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
            NpgsqlDataAdapter daPersonas = new NpgsqlDataAdapter("select * from persona inner join empresapersona on epe_persona=per_id where epe_empresa=  " + empresa, connection);
            daPersonas.Fill(ds);
            connection.Close();



            Politica politica = Constantes.GetPoliticacli();//Obtiene politica por defecto
            Catcliente catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto
            //List<Persona> personas = new List<Persona>();
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Personaxtipo> personastipos = new List<Personaxtipo>();


            StringBuilder html = new StringBuilder();


            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();

                foreach (DataRow row in ds.Tables[0].Rows)
                {

                    string id = "";
                    string ciruc = row["per_id"].ToString();
                    string tipoid = row["per_tipoid"].ToString();

                    if (tipoid == "04")
                        tipoid = "RUC";
                    else if (tipoid == "05")
                        tipoid = "Cédula";
                    else
                        tipoid = "Pasaporte";



                    string nombres = row["per_nombres"].ToString();
                    string apellidos = row["per_apellidos"].ToString();
                    string direccion = row["per_direccion"].ToString();
                    string razon = row["per_razon"].ToString();
                    string email = row["per_email"].ToString();
                    string telefono = row["per_telefono"].ToString();
                    string celular = "";


                    if (!string.IsNullOrEmpty(ciruc))
                    {
                        bool pasa = true;
                        //if (tipoid == "Cédula" || tipoid == "RUC")
                        //    pasa = Functions.Validaciones.valida_cedularuc(ciruc);
                        if (pasa)
                        {

                            Persona per = personas.Find(delegate (Persona p) { return p.per_ciruc == ciruc; });
                            if (per == null)
                            {

                                per = new Persona();
                                per.per_empresa = 1;
                                per.per_id = id;
                                per.per_ciruc = ciruc;
                                per.per_tipoid = tipoid;
                                per.per_nombres = nombres;
                                per.per_apellidos = apellidos;
                                per.per_razon = razon;
                                per.per_direccion = direccion;
                                per.per_mail = email;
                                per.per_telefono = telefono;
                                per.per_celular = celular;

                                per.per_retfuente = Constantes.GetImpRteFte().imp_codigo;
                                per.per_retiva = Constantes.GetImpRteIVA().imp_codigo;
                                per.per_estado = Constantes.cEstadoGrabado;

                                per.per_politica = politica.pol_codigo;
                                per.per_politicaid = politica.pol_id;
                                per.per_politicanombre = politica.pol_nombre;
                                per.per_politicadesc = politica.pol_porc_desc;
                                per.per_politicanropagos = politica.pol_nro_pagos;
                                per.per_politicadiasplazo = politica.pol_dias_plazo;
                                per.per_politicaporpagocon = politica.pol_porc_pago_con;

                                per.crea_usr = "admin";
                                per.crea_fecha = DateTime.Now;
                                personas.Add(per);

                                Personaxtipo pxt = new Personaxtipo();
                                pxt.pxt_empresa = 1;
                                pxt.pxt_tipo = Constantes.cCliente;
                                pxt.pxt_politicas = politica.pol_codigo;
                                pxt.pxt_cat_persona = catcliente.cat_codigo;
                                pxt.crea_usr = per.crea_usr;
                                pxt.crea_fecha = per.crea_fecha;
                                pxt.mod_usr = per.mod_usr;
                                pxt.mod_fecha = per.mod_fecha;



                                per.per_codigo = PersonaBLL.InsertIdentity(transaction, per);
                                pxt.pxt_persona = per.per_codigo;
                                PersonaxtipoBLL.Insert(transaction, pxt);

                                //html.AppendFormat("PERSONA: ID:{0} CI/RUC:{1} Nombres:{2} {3}<br>", id, ciruc, nombres, apellidos);


                            }
                            else
                            {
                                html.AppendFormat("ID:{0} CI/RUC:{1} Nombres:{2} {3} repetido la persona de ID:{4}<br>", id, ciruc, nombres, apellidos, per.per_id);
                            }
                        }
                        else
                            html.AppendFormat("ID:{0} CI/RUC:{1} Nombres:{2} {3} cédula incorrecta, no se puede agregar<br>", id, ciruc, nombres, apellidos);
                    }
                    else
                        html.AppendFormat("ID:{0} Nombres:{1} {2}, sin cédula no se puede agregar<br>", id, nombres, apellidos);
                }


                html.Append("Personas agregadas correctamente " + personas.Count);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }




            return html.ToString();

        }


        public static string MigrateComprobantesSICE(int empresa, int tipo)
        {

            DateTime fechaini = new DateTime(2019, 01, 01);
            

            DataSet dscom = new DataSet();
            DataSet dsarc = new DataSet();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
            NpgsqlDataAdapter daComprobantes = new NpgsqlDataAdapter("select * from comprobante inner join archivo on com_empresa=arc_empresa and com_numero=arc_numero where com_empresa = " + empresa + " and com_fecha >= '" + fechaini.ToString("yyyy-MM-dd") + "' and com_formato=" + tipo, connection);
            daComprobantes.Fill(dscom);

            connection.Close();

            List<Almacen> lstalmancen = AlmacenBLL.GetAll("", "");
            List<Puntoventa> lstpventa = PuntoventaBLL.GetAll("", "");
            List<Persona> lstpersona = PersonaBLL.GetAll("", "");
            List<Producto> lstproducto = ProductoBLL.GetAll("", "");


            //List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll("cdoc_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fechaini.ToString("yyyy-MM-dd") + "' and com_tipodoc in(14,26) and com_estado=2)", "");
            //List<Comprobante> lstcom = ComprobanteBLL.GetAll("com_empresa=1 and com_fecha>='" + fechaini.ToString("yyyy-MM-dd") + "'", "");
            List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll("cdoc_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='2019-01-01' and com_tipodoc in(14,26) and com_estado=2)", "");
            List<Comprobante> lstcom = ComprobanteBLL.GetAll("com_empresa=1 and com_fecha>='2019-01-01'", "");

            Politica politica = Constantes.GetPoliticacli();//Obtiene politica por defecto
            Catcliente catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto


            List<Personaxtipo> lstpxt = new List<Personaxtipo>();
            List<Persona> lstper = new List<Persona>();
            List<Comprobante> lst = new List<Comprobante>();

           StringBuilder html = new StringBuilder();

            foreach (DataRow row in dscom.Tables[0].Rows)
            {



                string tiposice=  row["com_formato"].ToString(); //1:FAC 2:NC 3:RET

                Comprobante com = new Comprobante();
                com.com_empresa = 1;
                com.com_fecha = (DateTime)row["com_fecha"];
                com.com_periodo = com.com_fecha.Year;
                com.com_mes = com.com_fecha.Month;
                com.com_dia = com.com_fecha.Day;

                com.crea_usr = "auto";
                com.crea_fecha = DateTime.Now;


                Almacen alm = lstalmancen.Find(delegate (Almacen a) { return a.alm_id == row["com_almacen"].ToString(); });
                Puntoventa pve = lstpventa.Find(delegate (Puntoventa p) { return p.pve_almacen == alm.alm_codigo && p.pve_id == row["com_pventa"].ToString(); });

                com.com_almacen = alm.alm_codigo;
                com.com_pventa = pve.pve_secuencia;
                //com.com_concepto = row["com_observacion"].ToString();
                com.com_nocontable = 0;

                string estado = row["com_estado"].ToString();

                com.com_estado = estado == "5" ? 2 : (estado == "7" ? 9 : 0);
                com.com_estadoelec = estado == "5" ? "AUTORIZADO" : (estado == "4" ? "DEVUELTO" : (estado == "7" ? "ANULADO" : ""));



                if (tiposice== "1")//FACTURA
                {
                    com.com_tipodoc = Constantes.cFactura.tpd_codigo;
                    com.com_modulo = General.GetModulo(com.com_tipodoc); ;
                    com.com_transacc = General.GetTransacc(com.com_tipodoc);
                    com.com_centro = Constantes.GetSinCentro().cen_codigo;
                    com.com_estado = Constantes.cEstadoGrabado;
                    com.com_descuadre = 0;
                    com.com_adestino = 0;
                    com.com_doctran = string.Format("FAC-{0}-{1}-{2:0000000}", row["com_almacen"].ToString(), row["com_pventa"].ToString(), int.Parse(row["com_secuencia"].ToString()));
                    com.com_numero = int.Parse(row["com_secuencia"].ToString());
                    com.com_tclipro = Constantes.cCliente;
                    com.com_ctipocom = 2;
                    com = XmlReader.CargarFacturaTAO(row["arc_xml"].ToString(), com);

                    Persona per = lstpersona.Find(delegate (Persona p) { return p.per_ciruc == com.ccomdoc.cdoc_ced_ruc; });
                    if (per != null)
                        com.com_codclipro = per.per_codigo;
                    else
                        throw new ArgumentException("No existe el cliente con el id" + com.ccomdoc.cdoc_ced_ruc);

                    foreach (Dcomdoc item in com.ccomdoc.detalle)
                    {
                        Producto pro = lstproducto.Find(delegate (Producto p) { return p.pro_id == item.ddoc_productoid; });
                        item.ddoc_producto = pro.pro_codigo;
                    }

                    lst.Add(com);

                    

                }
                if (tiposice=="3")//RET
                {
                    
                    

                    com.com_tipodoc = Constantes.cRetencion.tpd_codigo;
                    com.com_modulo = General.GetModulo(com.com_tipodoc); 
                    com.com_transacc = General.GetTransacc(com.com_tipodoc);
                    com.com_centro = Constantes.GetSinCentro().cen_codigo;
                    com.com_estado = Constantes.cEstadoGrabado;
                    com.com_descuadre = 0;
                    com.com_adestino = 0;
                    com.com_doctran = string.Format("RET-{0}-{1}-{2:0000000}", row["com_almacen"].ToString(), row["com_pventa"].ToString(), int.Parse(row["com_secuencia"].ToString()));
                    com.com_numero = int.Parse(row["com_secuencia"].ToString());
                    com.com_tclipro = Constantes.cCliente;
                    com.com_ctipocom = 14;
                    com = XmlReader.CargarRetencionSICE(row["arc_xml"].ToString(), com,lstcdoc);

                    Persona per = lstpersona.Find(delegate (Persona p) { return p.per_ciruc == com.ccomdoc.cdoc_ced_ruc; });
                    if (per != null)
                        com.com_codclipro = per.per_codigo;
                    else
                        html.Append("No existe el proveedor con el id " + com.ccomdoc.cdoc_ced_ruc + " " + com.ccomdoc.cdoc_nombre + " Ret:" + com.com_doctran + " <br>");

                    if ((com.ccomdoc.cdoc_factura ?? 0) == 0)
                        html.Append("No existe la factura " + com.ccomdoc.cdoc_aut_factura + " del proveedor " + com.ccomdoc.cdoc_ced_ruc + " " + com.ccomdoc.cdoc_nombre + "  Ret:"+com.com_doctran+"<br>");
                    //throw new ArgumentException("No existe el cliente con el id" + com.ccomdoc.cdoc_ced_ruc);

                    lst.Add(com);



                }
                if (tiposice == "2")//NC
                {

                    
                    com.com_tipodoc = Constantes.cNotacre.tpd_codigo;
                    com.com_modulo = General.GetModulo(com.com_tipodoc);
                    com.com_transacc = General.GetTransacc(com.com_tipodoc);
                    com.com_centro = Constantes.GetSinCentro().cen_codigo;
                    com.com_estado = Constantes.cEstadoGrabado;
                    com.com_descuadre = 0;
                    com.com_adestino = 0;
                    com.com_doctran = string.Format("NCCL-{0}-{1}-{2:0000000}", row["com_almacen"].ToString(), row["com_pventa"].ToString(), int.Parse(row["com_secuencia"].ToString()));
                    com.com_numero = int.Parse(row["com_secuencia"].ToString());
                    com.com_tclipro = Constantes.cCliente;
                    com.com_ctipocom = 15;
                    com = XmlReader.CargarNotaCreditoSICE(row["arc_xml"].ToString(), com, lstcom);

                    Persona per = lstpersona.Find(delegate (Persona p) { return p.per_ciruc == com.ccomdoc.cdoc_ced_ruc; });
                    if (per != null)
                        com.com_codclipro = per.per_codigo;
                    else
                        html.Append("No existe el cliente con el id " + com.ccomdoc.cdoc_ced_ruc + " " + com.ccomdoc.cdoc_nombre + " NC:" + com.com_doctran + " <br>");

                    if ((com.ccomdoc.cdoc_factura ?? 0) == 0)
                        html.Append("No existe la factura " + com.ccomdoc.cdoc_aut_factura + " del proveedor " + com.ccomdoc.cdoc_ced_ruc + " " + com.ccomdoc.cdoc_nombre + "  NC:" + com.com_doctran + "<br>");
                    //throw new ArgumentException("No existe el cliente con el id" + com.ccomdoc.cdoc_ced_ruc);

                    lst.Add(com);



                }


            }




            


            if (html.ToString() == "")
            {

                int i = 0;
                //BLL transaction = new BLL();
                //transaction.CreateTransaction();

                foreach (Comprobante item in lst)
                {
                    try
                    {

                        if (item.com_tipodoc == 4)//FACTURA
                        {
                            Comprobante com = lstcom.Find(delegate (Comprobante c) { return c.com_doctran == item.com_doctran; });
                            if (com == null)
                            {

                                FAC.save_factura(item);
                                FAC.account_factura(item);
                                html.Append("Factura creada " + item.com_doctran + "<br>");
                            }
                        }
                        if (item.com_tipodoc == 16) // RETEN
                        {

                            Comprobante com = lstcom.Find(delegate (Comprobante c) { return c.com_doctran == item.com_doctran; });
                            if (com == null)
                            {
                                CXCP.save_retencion1(item);
                                CXCP.account_retencion(item);
                                html.Append("Retencion creada " + item.com_doctran + "<br>");
                            }
                            else if (com.crea_usr == "auto")
                            {
                                com.ccomdoc = new Ccomdoc();
                                com.ccomdoc.cdoc_factura = item.ccomdoc.cdoc_factura;
                                com.ccomdoc.cdoc_aut_factura = item.ccomdoc.cdoc_aut_factura;
                                com.retenciones = item.retenciones;
                                CXCP.update_retencion1(com);
                                CXCP.account_retencion(com);
                                
                                html.Append("Retencion actualizada " + item.com_doctran + "<br>");
                            }

                        }
                        if (item.com_tipodoc == 17) // NC
                        {
                            Comprobante com = lstcom.Find(delegate (Comprobante c) { return c.com_doctran == item.com_doctran; });
                            if (com == null)
                            {
                                com = BAN.save_notacredeb1(item);
                                BAN.account_notacredeb(com);
                                html.Append("NC creada " + com.com_doctran + "<br>");
                            }
                            else if (com.crea_usr == "auto")
                            {
                                com.ccomdoc = new Ccomdoc();
                                com.ccomdoc.cdoc_factura = item.ccomdoc.cdoc_factura;
                                com.ccomdoc.cdoc_aut_factura = item.ccomdoc.cdoc_aut_factura;
                                com.notascre= item.notascre;
                                BAN.update_notacredeb(com);
                                BAN.account_notacredeb(com);

                                html.Append("NC actualizada " + item.com_doctran + "<br>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        html.Append("ERROR NC " + item.com_doctran + "  " + ex.Message+  "<br>");
                        //transaction.Rollback();
                        //throw ex;
                    }

                    i++;
                }



            }


            return html.ToString();

        }



        public static string MigrateClientesSICE(int empresa)
        {


            StringBuilder html = new StringBuilder();

            DataSet dscom = new DataSet();
            DataSet dsarc = new DataSet();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
            NpgsqlDataAdapter daComprobantes = new NpgsqlDataAdapter("select * from comprobante inner join archivo on com_empresa=arc_empresa and com_numero=arc_numero where com_empresa = " + empresa + " and com_fecha >= '2019-07-01'", connection);
            daComprobantes.Fill(dscom);

            connection.Close();

            List<Almacen> lstalmancen = AlmacenBLL.GetAll("", "");
            List<Puntoventa> lstpventa = PuntoventaBLL.GetAll("", "");
            List<Persona> lstpersona = PersonaBLL.GetAll("", "");
            List<Producto> lstproducto = ProductoBLL.GetAll("", "");


            Politica politica = Constantes.GetPoliticacli();//Obtiene politica por defecto
            Catcliente catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto


            List<Personaxtipo> lstpxt = new List<Personaxtipo>();
            //List<Persona> lstper = new List<Persona>();
            List<Comprobante> lst = new List<Comprobante>();

            int pernew = 0;

            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();


                foreach (DataRow row in dscom.Tables[0].Rows)
                {



                    string tiposice = row["com_formato"].ToString(); //1:FAC 2:NC 3:RET

                    Comprobante com = new Comprobante();
                    com.com_empresa = 1;
                    com.com_fecha = (DateTime)row["com_fecha"];
                    com.com_periodo = com.com_fecha.Year;
                    com.com_mes = com.com_fecha.Month;
                    com.com_dia = com.com_fecha.Day;

                    if (tiposice == "1")//FACTURA
                    {

                        com = XmlReader.CargarFacturaTAO(row["arc_xml"].ToString(), com);

                        Persona per = lstpersona.Find(delegate (Persona p) { return p.per_ciruc == com.ccomdoc.cdoc_ced_ruc; });
                        if (per != null)
                            com.com_codclipro = per.per_codigo;
                        else
                        {
                            per = new Persona();
                            per.per_empresa = 1;
                            per.per_id = "";
                            per.per_ciruc = com.ccomdoc.cdoc_ced_ruc;

                            if (com.ccomdoc.cdoc_tipoid == "04")
                                per.per_tipoid = "RUC";
                            else if (com.ccomdoc.cdoc_tipoid == "05")
                                per.per_tipoid = "Cédula";
                            else
                                per.per_tipoid = "Pasaporte";


                            per.per_nombres = com.ccomdoc.cdoc_nombre;
                            per.per_apellidos = "";
                            per.per_razon = com.ccomdoc.cdoc_nombre;
                            per.per_direccion = com.ccomdoc.cdoc_direccion;
                            per.per_mail = "";
                            per.per_telefono = "";
                            per.per_celular = "";

                            per.per_retfuente = Constantes.GetImpRteFte().imp_codigo;
                            per.per_retiva = Constantes.GetImpRteIVA().imp_codigo;
                            per.per_estado = Constantes.cEstadoGrabado;

                            per.per_politica = politica.pol_codigo;
                            per.per_politicaid = politica.pol_id;
                            per.per_politicanombre = politica.pol_nombre;
                            per.per_politicadesc = politica.pol_porc_desc;
                            per.per_politicanropagos = politica.pol_nro_pagos;
                            per.per_politicadiasplazo = politica.pol_dias_plazo;
                            per.per_politicaporpagocon = politica.pol_porc_pago_con;

                            per.crea_usr = "admin";
                            per.crea_fecha = DateTime.Now;
                            lstpersona.Add(per);

                            Personaxtipo pxt = new Personaxtipo();
                            pxt.pxt_empresa = 1;
                            pxt.pxt_tipo = Constantes.cCliente;
                            pxt.pxt_politicas = politica.pol_codigo;
                            pxt.pxt_cat_persona = catcliente.cat_codigo;
                            pxt.crea_usr = per.crea_usr;
                            pxt.crea_fecha = per.crea_fecha;
                            pxt.mod_usr = per.mod_usr;
                            pxt.mod_fecha = per.mod_fecha;

                            per.per_codigo = PersonaBLL.InsertIdentity(transaction, per);
                            pxt.pxt_persona = per.per_codigo;
                            PersonaxtipoBLL.Insert(transaction, pxt);

                            html.Append("Nueva Persona: " + per.per_ciruc + " " + per.per_razon + "<br>");
                            pernew++;
                        }                        
                    }


                }

                html.Append("Personas agregadas correctamente " + pernew);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }


            

            return html.ToString();

        }



        public static string MigrateProductosSICE(int empresa, int tipo)
        {

            DateTime fechaini = new DateTime(2019, 07, 01);


            DataSet dscom = new DataSet();
            DataSet dsarc = new DataSet();
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
            NpgsqlDataAdapter daComprobantes = new NpgsqlDataAdapter("select * from comprobante inner join archivo on com_empresa=arc_empresa and com_numero=arc_numero where com_empresa = " + empresa + " and com_fecha >= '" + fechaini.ToString("yyyy-MM-dd") + "' and com_formato=" + tipo, connection);
            daComprobantes.Fill(dscom);

            connection.Close();

            List<Almacen> lstalmancen = AlmacenBLL.GetAll("", "");
            List<Puntoventa> lstpventa = PuntoventaBLL.GetAll("", "");
            List<Persona> lstpersona = PersonaBLL.GetAll("", "");
            List<Producto> lstproducto = ProductoBLL.GetAll("", "");
            List<Producto> lstproductonew = new List<Producto>();
            List<Factor> lstfactores = new List<Factor>();

            List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll("cdoc_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fechaini.ToString("yyyy-MM-dd") + "' and com_tipodoc in(14,26) and com_estado=2)", "");


            Politica politica = Constantes.GetPoliticacli();//Obtiene politica por defecto
            Catcliente catcliente = Constantes.GetCatcliente();//Obtiene politica por defecto


            List<Personaxtipo> lstpxt = new List<Personaxtipo>();
            List<Persona> lstper = new List<Persona>();
            List<Comprobante> lst = new List<Comprobante>();

            StringBuilder html = new StringBuilder();

            foreach (DataRow row in dscom.Tables[0].Rows)
            {



                string tiposice = row["com_formato"].ToString(); //1:FAC 2:NC 3:RET

                Comprobante com = new Comprobante();
                com.com_empresa = 1;
                com.com_fecha = (DateTime)row["com_fecha"];
                com.com_periodo = com.com_fecha.Year;
                com.com_mes = com.com_fecha.Month;
                com.com_dia = com.com_fecha.Day;

                com.crea_usr = "auto";
                com.crea_fecha = DateTime.Now;


                Almacen alm = lstalmancen.Find(delegate (Almacen a) { return a.alm_id == row["com_almacen"].ToString(); });
                Puntoventa pve = lstpventa.Find(delegate (Puntoventa p) { return p.pve_almacen == alm.alm_codigo && p.pve_id == row["com_pventa"].ToString(); });

                com.com_almacen = alm.alm_codigo;
                com.com_pventa = pve.pve_secuencia;
                //com.com_concepto = row["com_observacion"].ToString();
                com.com_nocontable = 0;

                string estado = row["com_estado"].ToString();

                com.com_estado = estado == "5" ? 2 : (estado == "7" ? 9 : 0);
                com.com_estadoelec = estado == "5" ? "AUTORIZADO" : (estado == "4" ? "DEVUELTO" : (estado == "7" ? "ANULADO" : ""));



                if (tiposice == "1")//FACTURA
                {
                    com.com_tipodoc = Constantes.cFactura.tpd_codigo;
                    com.com_modulo = General.GetModulo(com.com_tipodoc); ;
                    com.com_transacc = General.GetTransacc(com.com_tipodoc);
                    com.com_centro = Constantes.GetSinCentro().cen_codigo;
                    com.com_estado = Constantes.cEstadoGrabado;
                    com.com_descuadre = 0;
                    com.com_adestino = 0;
                    com.com_doctran = string.Format("FAC-{0}-{1}-{2:0000000}", row["com_almacen"].ToString(), row["com_pventa"].ToString(), int.Parse(row["com_secuencia"].ToString()));
                    com.com_numero = int.Parse(row["com_secuencia"].ToString());
                    com.com_tclipro = Constantes.cCliente;
                    com.com_ctipocom = 2;
                    com = XmlReader.CargarFacturaTAO(row["arc_xml"].ToString(), com);
                   

                    foreach (Dcomdoc item in com.ccomdoc.detalle)
                    {
                        Producto pro = lstproducto.Find(delegate (Producto p) { return p.pro_id == item.ddoc_productoid; });
                        if (pro == null)
                        {
                            pro = new Producto();
                            pro.pro_empresa = 1;
                            //pro.pro_codigo = productos.Count + 1;
                            pro.pro_id = item.ddoc_productoid;
                            pro.pro_nombre = item.ddoc_observaciones;
                            pro.pro_unidad = 1; //unidad;
                            pro.pro_inventario = 0;
                            pro.pro_estado = 1;
                            pro.pro_iva = item.ddoc_grabaiva;
                            pro.crea_usr = "auto";
                            pro.crea_fecha = DateTime.Now;
                            pro.pro_calcula = 0;
                            pro.pro_tproducto = 1;
                            pro.pro_grupo = 1;

                            pro.pro_codigo = ProductoBLL.InsertIdentity(pro);
                                                                                                                                     

                            Factor fac = new Factor();
                            fac.fac_empresa = 1;
                            fac.fac_producto = pro.pro_codigo;
                            fac.fac_unidad = 1;
                            fac.fac_factor = 1;
                            fac.fac_default = 1;
                            fac.fac_estado = 1;
                            fac.crea_usr = "auto";
                            fac.crea_fecha = DateTime.Now;

                            FactorBLL.Insert(fac);

                            html.AppendLine("Producto nuevo: " + pro.pro_id + " " + pro.pro_nombre + "<br>");


                        }
                        
                    }
                    



                }                


            }



                                                                


            return html.ToString();

        }











        public static Almacen GetAlmacen(string id, List<Almacen> lst)
        {
            Almacen alm = lst.Find(delegate (Almacen a) { return a.alm_id == id; });
            return alm;

        }
        public static Puntoventa GetPventa(string id, int almacen, List<Puntoventa> lst)
        {
            Puntoventa pve = lst.Find(delegate (Puntoventa p) { return p.pve_almacen == almacen && p.pve_id == id; });
            return pve;

        }
        public static Comprobante GetGuia(Persona persona,Persona destinatario, Almacen almacen, Puntoventa pventa, string nrodoc, string sigla, DateTime fecha, decimal? total, int? politica)
        {
            Comprobante comprobante = new Comprobante();
            comprobante.ccomdoc = new Ccomdoc();
            comprobante.ccomenv = new Ccomenv();
            comprobante.ccomdoc.detalle = new List<Dcomdoc>();
            comprobante.total = new Total();

            comprobante.com_empresa = persona.per_empresa;
            comprobante.com_tipodoc = 13;
            comprobante.com_ctipocom = 11;
            comprobante.com_almacen = almacen.alm_codigo;
            comprobante.com_pventa = pventa.pve_secuencia;
            comprobante.com_codclipro = persona.per_codigo;
            comprobante.com_numero = int.Parse(nrodoc);
            comprobante.com_doctran = string.Format("{0}-{1}-{2}-{3:0000000}", sigla, almacen.alm_id, pventa.pve_id, comprobante.com_numero);


            //comprobante.ccomdoc.cdoc_acl_nroautoriza = nrodoc;
            //comprobante.ccomdoc.cdoc_acl_retdato = 20000004;
            //comprobante.ccomdoc.cdoc_acl_tablacoa = 4;
            //comprobante.ccomdoc.cdoc_aut_factura = string.Format("{0}-{1}-{2}", "001", "001", nrodoc);
            //comprobante.ccomdoc.cdoc_aut_fecha = fecha;


            comprobante.ccomdoc.cdoc_ced_ruc = persona.per_ciruc;
            comprobante.ccomdoc.cdoc_nombre = persona.per_razon;
            comprobante.ccomdoc.cdoc_direccion = persona.per_direccion;

            comprobante.ccomdoc.cdoc_politica = politica;

            comprobante.com_fecha = fecha;
            comprobante.com_fechastr = fecha.ToShortDateString();

            comprobante.crea_usr = "auto";
            comprobante.crea_fecha = DateTime.Now;


            comprobante.ccomenv.cenv_empresa = persona.per_empresa;
            comprobante.ccomenv.cenv_remitente = persona.per_codigo;
            comprobante.ccomenv.cenv_ciruc_rem = persona.per_ciruc;
            comprobante.ccomenv.cenv_nombres_rem= persona.per_razon;
            comprobante.ccomenv.cenv_direccion_rem = persona.per_direccion;
            comprobante.ccomenv.cenv_telefono_rem= persona.per_telefono;


            
            comprobante.ccomenv.cenv_destinatario= destinatario.per_codigo;
            comprobante.ccomenv.cenv_ciruc_des= destinatario.per_ciruc;
            comprobante.ccomenv.cenv_nombres_des= destinatario.per_razon;
            comprobante.ccomenv.cenv_direccion_des= destinatario.per_direccion;
            comprobante.ccomenv.cenv_telefono_des= destinatario.per_telefono;





            comprobante.total.tot_subtot_0 = total.Value;
            comprobante.total.tot_subtotal = 0;
            //comprobante.com_subtotalnoiva = subtotalnoiva;
            //comprobante.com_subtotalextiva = subtotalextiva;
            comprobante.total.tot_timpuesto = 0;
            comprobante.total.tot_porc_impuesto = 12;
            //comprobante.total.tot_ice = valorice;


            comprobante.total.tot_total = comprobante.total.tot_subtot_0 + comprobante.total.tot_subtotal + comprobante.total.tot_timpuesto;
            comprobante.total.crea_usr = "auto";
            comprobante.total.crea_fecha = DateTime.Now;


            Dcomdoc det = new Dcomdoc();
            det.ddoc_secuencia = comprobante.ccomdoc.detalle.Count() + 1;
            det.ddoc_producto = 247; //ENVIO
            det.ddoc_productoid = "00000001";
            det.ddoc_productonombre = "ENVIO";
            det.ddoc_observaciones = "";

            det.ddoc_cantidad = 1;
            det.ddoc_precio = total ?? 0;
            det.ddoc_grabaiva = 0;
            det.ddoc_total = total ?? 0;
            comprobante.ccomdoc.detalle.Add(det);

            return comprobante;



        }
        public static string MigrateGuias(string path)
        {
            Politica politica = Constantes.GetPoliticaProv();//Obtiene politica por defecto
            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto
            //List<Persona> personas = new List<Persona>();
            List<Politica> politicas = PoliticaBLL.GetAll("", "");
            List<Almacen> almacenes = AlmacenBLL.GetAll("", "");
            List<Puntoventa> puntosven = PuntoventaBLL.GetAll("", "");
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Personaxtipo> personastipos = PersonaxtipoBLL.GetAll("", "");

            int i = 0;
            string line = "";

            //0: NRO FAC
            //1: NOMBRES PROV
            //2: SALDO
            //3: SUBTOTAL
            //4: IVA
            //5: RET FUE        
            //6: RET IVA
            //7: FECHA
            //8: VENCE

            DateTime fecha = new DateTime(2019, 5, 31);

            StringBuilder html = new StringBuilder();






            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');

                    string alm = GetArrayVal(array, 0);
                    string pve = GetArrayVal(array, 1);
                    string nro = GetArrayVal(array, 2);
                    string ciu = GetArrayVal(array, 3);
                    DateTime? fec = Conversiones.ObjectToDateTimeNull(GetArrayVal(array, 4));
                    string rem = GetArrayVal(array, 5);
                    string des = GetArrayVal(array, 6);
                    Decimal? tot = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 7));
                    string soc = GetArrayVal(array, 8);

                    Almacen almobj = GetAlmacen(alm, almacenes);
                    Puntoventa pveobj = GetPventa(pve, almobj.alm_codigo, puntosven);

                    Persona per = GetPersonaByNom(rem, personas);
                    if (per != null)
                    {
                        try
                        {
                            //Comprobante obl = GetObligacion(per, nro, fecha, subtotal, iva);
                            Comprobante fac = GetGuia(per, per, almobj,pveobj, nro,"", fecha, tot, null);
                            fac = FAC.create_factura(fac);
                            //fac = FAC.account_factura(fac);
                            html.AppendFormat("GUIA:{0} Cliente:{1} creada correctamente...<br>", fac.com_doctran, per.per_razon);
                            i++;
                        }
                        catch (Exception ex)
                        {
                            html.AppendFormat("Cliente:{0}, Nrodoc:{1} ERROR:{2}<br>", rem, nro, ex.Message);
                        }
                    }
                    else
                        html.AppendFormat("El Cliente {0}, no se encuentra en el registrado en el sistema<br>", rem);

                }
            }

            return html.ToString();

        }



        public static int GetNumero(string doctran)
        {
            int num = 0;
            string[] array = doctran.Split('-');
            if (array.Length > 3)
                num = int.Parse(array[3]);

            return num;
        }

        public static string GetAlmacenId(string doctran)
        {            
            string[] array = doctran.Split('-');
            if (array.Length > 1)
               return array[1];
            return "";
        }

        public static string GetPventaId(string doctran)
        {
            string[] array = doctran.Split('-');
            if (array.Length > 2)
                return array[2];
            return "";
        }
        public static string GetSigla(string doctran)
        {
            string[] array = doctran.Split('-');
            if (array.Length > 0)
                return array[0];
            return "";
        }

        public static string GetSigla1(string tipo)
        {
            if (tipo.ToUpper() == "F")
                return "FAC";
            if (tipo.ToUpper() == "G")
                return "GUI";
            return "";
        }



        public static Ruta GetRuta(string destino, List<Ruta> lst)
        {
            Ruta rut = new Ruta();
            rut = lst.Find(delegate (Ruta r) { return r.rut_destino.ToUpper() == destino.ToUpper(); });
            if (rut == null)
                rut = lst[0];
            return rut;
        }

        public static Politica GetPolitica(string id, List<Politica> lst)
        {
            Politica pol = new Politica();
            pol = lst.Find(delegate (Politica p) { return p.pol_id == id; });
            if (pol==null)
            {
                pol = lst.Find(delegate (Politica p) { return p.pol_id == "CRE"; });
            }

            return pol;
        }

        /// <summary>
        /// Migracion HR TMC
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MigrateHR(string path)
        {
            
            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto           
            List<Almacen> almacenes = AlmacenBLL.GetAll("", "");
            List<Puntoventa> puntosven = PuntoventaBLL.GetAll("", "");
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Vehiculo> vehiculos = VehiculoBLL.GetAll("", "");
            List<Politica> politicas = PoliticaBLL.GetAll("", "");
            //List<Personaxtipo> personastipos = PersonaxtipoBLL.GetAll("", "");

            //List<Ruta> rutas = RutaBLL.GetAll("rut_origen='Guayaquil'", "");
            //int almacen = 2; //Gyq
            //int pventa = 1;
            //string fecha = "2019-09-20";

            List<Ruta> rutas = RutaBLL.GetAll("rut_origen='Quito'", "");
            int almacen = 1; //Uio
            int pventa = 1;
            string fecha = "2019-09-20";
            string sociodef = "TRANSMORACASTRO";

            //List<Comprobante> lsthr = ComprobanteBLL.GetAll("com_empresa=1 and com_tipodoc= 5 and com_almacen=" + almacen + " and com_fecha>='" + fecha + "'", "");
            List<Total> lsttot = TotalBLL.GetAll("tot_comprobante in (select com_codigo from comprobante where com_empresa=1  and com_fecha>='" + fecha + "')", "");
            List<Comprobante> lst = ComprobanteBLL.GetAll("com_empresa=1  and com_fecha>='" + fecha + "'", "");

            Comprobante hojaruta = new Comprobante();
            List<Rutaxfactura> rutfac = new List<Rutaxfactura>();
            decimal total = 0;


            int i = 0;
            string line = "";
            

            StringBuilder html = new StringBuilder();

            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');

                    string soc = GetArrayVal(array, 0);
                    string nrohr = GetArrayVal(array, 1);
                    string desti = GetArrayVal(array, 2);
                    DateTime? fechr = Conversiones.ObjectToDateTimeNull(GetArrayVal(array, 3));
                    string tipo = GetArrayVal(array, 4);
                    string nrofg = GetArrayVal(array, 5);
                    string rem = GetArrayVal(array, 6);
                    string des = GetArrayVal(array, 7);
                    Decimal? tot = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 8));
                    string fp= GetArrayVal(array, 9);

                    Politica politica = GetPolitica(fp, politicas);

                    Persona socio = GetPersonaByNom(soc, personas);
                    if (socio == null)
                        socio = GetPersonaByNom(sociodef, personas);

                    List<Vehiculo> lstveh = vehiculos.FindAll(delegate (Vehiculo v) { return v.veh_duenio == socio.per_codigo; });
                    if (lstveh.Count == 0)
                    {
                        socio = GetPersonaByNom(sociodef, personas);
                        lstveh = vehiculos.FindAll(delegate (Vehiculo v) { return v.veh_duenio == socio.per_codigo; });
                    }                    
                    Persona remitente = GetPersonaByNom(rem, personas);
                    if (remitente == null)
                        remitente = socio;
                    Persona destinatario = GetPersonaByNom(des, personas);
                    if (destinatario == null)
                        destinatario = socio;

                    Comprobante hr = lst.Find(delegate (Comprobante h) { return h.com_doctran == nrohr; });
                    if (hr == null) //No existe la HR
                    {                     
                        if (hojaruta.com_doctran != nrohr)
                        {

                            if (rutfac.Count>0)//Guardamos hr
                            {

                                hojaruta.total = new Total();

                                hojaruta.total.tot_empresa = 1;
                                hojaruta.total.tot_total = total;
                                hojaruta.total.crea_usr = "auto";
                                hojaruta.total.crea_fecha = DateTime.Now;

                                BLL transaction = new BLL();
                                transaction.CreateTransaction();
                                try
                                {
                                    transaction.BeginTransaction();
                                    hojaruta.com_codigo = ComprobanteBLL.InsertIdentity(transaction, hojaruta);
                                    hojaruta.total.tot_comprobante = hojaruta.com_codigo;
                                    TotalBLL.Insert(transaction, hojaruta.total);
                                    foreach (Rutaxfactura item in rutfac)
                                    {
                                                                              
                                        item.rfac_comprobanteruta = hojaruta.com_codigo;
                                        item.rfac_comprobanteruta_key = hojaruta.com_codigo;                                        
                                        RutaxfacturaBLL.Insert(transaction, item);
                                    }
                                    transaction.Commit();
                                    
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                }
                            }


                            //Aqui guardar la hoja de ruta, detalleguias
                            hojaruta = new Comprobante();
                            hojaruta.com_empresa = 1;
                            hojaruta.com_fecha = fechr.Value;
                            hojaruta.com_periodo = fechr.Value.Year;
                            hojaruta.com_mes = fechr.Value.Month;
                            hojaruta.com_dia = fechr.Value.Day;
                            hojaruta.com_almacen = almacen;
                            hojaruta.com_pventa = pventa;
                            hojaruta.com_tipodoc = 5;
                            hojaruta.com_ctipocom = 4;
                            hojaruta.com_doctran = nrohr;
                            hojaruta.com_numero = GetNumero(nrohr);
                            hojaruta.com_modulo = 3;
                            hojaruta.com_transacc = 1;
                            hojaruta.com_nocontable = 1;
                            hojaruta.com_descuadre = 0;
                            hojaruta.com_adestino = 0;
                            hojaruta.com_codclipro = socio.per_codigo;
                            hojaruta.com_vehiculo = lstveh[0].veh_codigo;
                            hojaruta.com_ruta = GetRuta(desti, rutas).rut_codigo;                            
                            hojaruta.com_concepto = "HOJA DE RUTA MIGRADA" + lstveh[0].veh_placa + " " + lstveh[0].veh_disco + " " + hojaruta.com_fecha.ToShortDateString();

                            hojaruta.crea_usr = "auto";
                            hojaruta.crea_fecha = DateTime.Now;
                            html.Append(hojaruta.com_doctran + " " + socio.per_razon + " <br>");
                            rutfac = new List<Rutaxfactura>();
                            total = 0;

                        }


                        Comprobante facgui = lst.Find(delegate (Comprobante h) { return h.com_doctran == nrofg; });
                        if (facgui == null)
                        {
                            string sigid = GetSigla(nrofg);
                            string almid = GetAlmacenId(nrofg);
                            string pveid = GetPventaId(nrofg);
                            Almacen almfg = GetAlmacen(almid, almacenes);
                            Puntoventa pvefg = GetPventa(pveid, almfg.alm_codigo, puntosven);
                            int nrofacgui = GetNumero(nrofg);
                            facgui = GetGuia(remitente, destinatario, almfg, pvefg, nrofacgui.ToString(), sigid, fechr.Value, tot, (int?)politica.pol_codigo);
                            facgui.ccomenv.cenv_ruta = hojaruta.com_ruta;
                            facgui = FAC.create_factura(facgui);
                        }
                        else
                        {
                            facgui.total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == facgui.com_codigo; });
                        }

                        Rutaxfactura rfac = new Rutaxfactura();
                        rfac.rfac_empresa = 1;
                        rfac.rfac_comprobantefac = facgui.com_codigo;
                        rfac.rfac_estado = 1;
                        rfac.crea_fecha = DateTime.Now;
                        rfac.crea_usr = "auto";
                        rutfac.Add(rfac);
                        total += facgui.total.tot_total;
                        

                    }

                   

                }
            }

            return html.ToString();



        }


        /// <summary>
        /// Migracion HR BRYSEAR
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MigrateHRBRY(string path)
        {

            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto           
            List<Almacen> almacenes = AlmacenBLL.GetAll("", "");
            List<Puntoventa> puntosven = PuntoventaBLL.GetAll("", "");
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Vehiculo> vehiculos = VehiculoBLL.GetAll("", "");
            List<Politica> politicas = PoliticaBLL.GetAll("", "");


            List<Ruta> rutas = RutaBLL.GetAll("rut_origen='Ambato'", "");
            //List<Ruta> rutas = RutaBLL.GetAll("", "");



            //int almacen = 1; //Uio
            //int pventa = 1;
            string fecha = "2019-08-01";
            string sociodef = "BRYSEAR CARGO";

            //List<Comprobante> lsthr = ComprobanteBLL.GetAll("com_empresa=1 and com_tipodoc= 5 and com_almacen=" + almacen + " and com_fecha>='" + fecha + "'", "");
            List<Total> lsttot = TotalBLL.GetAll("tot_comprobante in (select com_codigo from comprobante where com_empresa=1  and com_fecha>='" + fecha + "')", "");
            List<Comprobante> lst = ComprobanteBLL.GetAll("com_empresa=1  and com_fecha>='" + fecha + "'", "");

            Comprobante hojaruta = new Comprobante();
            List<Rutaxfactura> rutfac = new List<Rutaxfactura>();
            decimal total = 0;


            int i = 0;
            string line = "";


            StringBuilder html = new StringBuilder();

            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');

                    string soc = GetArrayVal(array, 0);
                    string almhr = GetArrayVal(array, 1);
                    string pvehr = GetArrayVal(array, 2);
                    string nrohr = GetArrayVal(array, 3);
                    string desti = GetArrayVal(array, 4);
                    DateTime? fechr = Conversiones.ObjectToDateTimeNull(GetArrayVal(array, 5));
                    string tipo = GetArrayVal(array, 6);
                    string almfg = GetArrayVal(array, 7);
                    string pvefg = GetArrayVal(array, 8);
                    string nrofg = GetArrayVal(array, 9);
                    string rem = GetArrayVal(array, 10);
                    int? bultos = Conversiones.ObjectToIntNull(GetArrayVal(array, 11));
                    string des = GetArrayVal(array, 12);
                    Decimal? tot = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 13));
                    string fp = GetArrayVal(array, 14);


                    Almacen almacenhr = GetAlmacen(almhr, almacenes);
                    Puntoventa puntohr = GetPventa(pvehr, almacenhr.alm_codigo, puntosven);


                    Almacen almacenfg = GetAlmacen(almfg, almacenes);
                    Puntoventa puntofg = GetPventa(pvefg, almacenfg.alm_codigo, puntosven);

                    Politica politica = GetPolitica(fp, politicas);
                    //Persona socio = GetPersonaByNom(soc, personas);
                    //if (socio == null)
                    //    socio = GetPersonaByNom(sociodef, personas);
                    Persona socio = GetSocioBRY(soc, personas);

                    List<Vehiculo> lstveh = vehiculos.FindAll(delegate (Vehiculo v) { return v.veh_duenio == socio.per_codigo; });
                    if (lstveh.Count == 0)
                    {
                        socio = GetPersonaByNom(sociodef, personas);
                        lstveh = vehiculos.FindAll(delegate (Vehiculo v) { return v.veh_duenio == socio.per_codigo; });
                    }
                    Persona remitente = GetPersonaByNom(rem, personas);
                    if (remitente == null)
                        remitente = socio;
                    Persona destinatario = GetPersonaByNom(des, personas);
                    if (destinatario == null)
                        destinatario = socio;

                    int? nrohojaruta = Conversiones.ObjectToIntNull(nrohr);
                    int? nrofacgui= Conversiones.ObjectToIntNull(nrofg);

                    //Comprobante hr = lst.Find(delegate (Comprobante h) { return h.com_doctran == nrohr; });
                    Comprobante hr = lst.Find(delegate (Comprobante h) { return h.com_almacen == almacenhr.alm_codigo && h.com_pventa == puntohr.pve_secuencia && h.com_numero == (nrohojaruta ?? 0) && h.com_tipodoc == 5; });
                    if (hr == null) //No existe la HR
                    {
                        //if (hojaruta.com_doctran != nrohr)
                        if (hojaruta.com_numero!= (nrohojaruta??0))
                        {

                            if (rutfac.Count > 0)//Guardamos hr
                            {

                                if (hojaruta.com_numero > 0)//Guarda solo hojas de ruta con numeracion
                                {
                                    hojaruta.total = new Total();

                                    hojaruta.total.tot_empresa = 1;
                                    hojaruta.total.tot_total = total;
                                    hojaruta.total.crea_usr = "auto";
                                    hojaruta.total.crea_fecha = DateTime.Now;

                                    BLL transaction = new BLL();
                                    transaction.CreateTransaction();
                                    try
                                    {
                                        transaction.BeginTransaction();
                                        hojaruta.com_codigo = ComprobanteBLL.InsertIdentity(transaction, hojaruta);
                                        hojaruta.total.tot_comprobante = hojaruta.com_codigo;
                                        TotalBLL.Insert(transaction, hojaruta.total);
                                        foreach (Rutaxfactura item in rutfac)
                                        {

                                            item.rfac_comprobanteruta = hojaruta.com_codigo;
                                            item.rfac_comprobanteruta_key = hojaruta.com_codigo;
                                            RutaxfacturaBLL.Insert(transaction, item);
                                        }
                                        transaction.Commit();

                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                    }
                                }
                            }


                            //Aqui guardar la hoja de ruta, detalleguias
                            hojaruta = new Comprobante();
                            hojaruta.com_empresa = 1;
                            hojaruta.com_fecha = fechr.Value;
                            hojaruta.com_periodo = fechr.Value.Year;
                            hojaruta.com_mes = fechr.Value.Month;
                            hojaruta.com_dia = fechr.Value.Day;
                            hojaruta.com_almacen = almacenhr.alm_codigo;
                            hojaruta.com_pventa = puntohr.pve_secuencia;
                            hojaruta.com_tipodoc = 5;
                            hojaruta.com_ctipocom = 4;
                            hojaruta.com_numero = nrohojaruta ?? 0;
                            hojaruta.com_doctran = string.Format("{0}-{1}-{2}-{3:0000000}", "HR", almacenhr.alm_id, puntohr.pve_id, hojaruta.com_numero);

                            //hojaruta.com_doctran = nrohr;
                            //hojaruta.com_numero = GetNumero(nrohr);
                            hojaruta.com_modulo = 3;
                            hojaruta.com_transacc = 1;
                            hojaruta.com_nocontable = 1;
                            hojaruta.com_descuadre = 0;
                            hojaruta.com_adestino = 0;
                            hojaruta.com_codclipro = socio.per_codigo;
                            hojaruta.com_vehiculo = lstveh[0].veh_codigo;
                            hojaruta.com_ruta = GetRuta(desti, rutas).rut_codigo;
                            hojaruta.com_concepto = "HOJA DE RUTA MIGRADA" + lstveh[0].veh_placa + " " + lstveh[0].veh_disco + " " + hojaruta.com_fecha.ToShortDateString();

                            hojaruta.crea_usr = "auto";
                            hojaruta.crea_fecha = DateTime.Now;
                            //html.Append(hojaruta.com_doctran + " " + socio.per_razon + " <br>");
                            rutfac = new List<Rutaxfactura>();
                            total = 0;

                        }

                        string creacion = "";
                        //Comprobante facgui = lst.Find(delegate (Comprobante h) { return h.com_doctran == nrofg; });
                        Comprobante facgui = lst.Find(delegate (Comprobante h) { return h.com_almacen == almacenfg.alm_codigo && h.com_pventa == puntofg.pve_secuencia && h.com_numero == (nrofacgui ?? 0) && h.com_tipodoc == (tipo.ToUpper() == "F" ? 4 : 13); });
                        if (facgui == null)
                        {
                            string sigid = GetSigla1(tipo);
                            //int nrofacgui = GetNumero(nrofg);
                            facgui = GetGuia(remitente, destinatario, almacenfg, puntofg, nrofacgui.ToString(), sigid, fechr.Value, tot, (int?)politica.pol_codigo);
                            facgui.ccomenv.cenv_ruta = hojaruta.com_ruta;
                            facgui = FAC.create_factura(facgui);
                            creacion = "SI";
                        }
                        else
                        {
                            facgui.total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == facgui.com_codigo; });
                        }

                        Rutaxfactura rfac = new Rutaxfactura();
                        rfac.rfac_empresa = 1;
                        rfac.rfac_comprobantefac = facgui.com_codigo;
                        rfac.rfac_estado = 1;
                        rfac.crea_fecha = DateTime.Now;
                        rfac.crea_usr = "auto";
                        rutfac.Add(rfac);
                        total += facgui.total.tot_total;


                        html.Append(hojaruta.com_doctran + "," + soc + "," + facgui.com_doctran + "," + nrofacgui + "," + creacion + "<br>");

                    }
                    else
                        html.Append(hr.com_doctran + "," + soc + ",," + nrofacgui + ",,HR EXISTE<br>");                    

                }
            }

            return html.ToString();



        }

        /// <summary>
        /// Actualiza todos los comprobantes para asignarles socio
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string UpdateHRBRY(string path, string origen)
        {

            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto           
            List<Almacen> almacenes = AlmacenBLL.GetAll("", "");
            List<Puntoventa> puntosven = PuntoventaBLL.GetAll("", "");
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Vehiculo> vehiculos = VehiculoBLL.GetAll("", "");
            List<Politica> politicas = PoliticaBLL.GetAll("", "");


            List<Ruta> rutas = RutaBLL.GetAll("rut_origen='" + origen + "'", "");
          
            //int almacen = 1; //Uio
            //int pventa = 1;
            string fecha = "2019-08-01";
            string sociodef = "BRYSEAR CARGO";

            

            //List<Total> lsttot = TotalBLL.GetAll("tot_comprobante in (select com_codigo from comprobante where com_empresa=1  and com_fecha>='" + fecha + "')", "");
            List<Comprobante> lst = ComprobanteBLL.GetAll("com_empresa=1  and com_fecha>='" + fecha + "'", "");
            //List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll("cdoc_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");

            List<Ccomenv> lstcenv = CcomenvBLL.GetAll("cenv_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");
            List<Rutaxfactura> lstrutafac = RutaxfacturaBLL.GetAll("rfac_comprobanteruta  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");
            List<Total> lsttot = TotalBLL.GetAll("tot_comprobante in (select com_codigo from comprobante where com_empresa=1  and com_fecha>='" + fecha + "')", "");

            Comprobante hojaruta = new Comprobante();
            List<Rutaxfactura> rutfac = new List<Rutaxfactura>();
            decimal total = 0;


            int i = 0;
            string line = "";


            StringBuilder html = new StringBuilder();

            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');

                    string soc = GetArrayVal(array, 0);
                    string almhr = GetArrayVal(array, 1);
                    string pvehr = GetArrayVal(array, 2);
                    string nrohr = GetArrayVal(array, 3);
                    string desti = GetArrayVal(array, 4);
                    DateTime? fechr = Conversiones.ObjectToDateTimeNull(GetArrayVal(array, 5));
                    string tipo = GetArrayVal(array, 6);
                    string almfg = GetArrayVal(array, 7);
                    string pvefg = GetArrayVal(array, 8);
                    string nrofg = GetArrayVal(array, 9);
                    string rem = GetArrayVal(array, 10);
                    int? bultos = Conversiones.ObjectToIntNull(GetArrayVal(array, 11));
                    string des = GetArrayVal(array, 12);
                    Decimal? tot = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 13));
                    string fp = GetArrayVal(array, 14);


                    Almacen almacenhr = GetAlmacen(almhr, almacenes);
                    Puntoventa puntohr = GetPventa(pvehr, almacenhr.alm_codigo, puntosven);


                    Almacen almacenfg = GetAlmacen(almfg, almacenes);
                    Puntoventa puntofg = GetPventa(pvefg, almacenfg.alm_codigo, puntosven);

                    Politica politica = GetPolitica(fp, politicas);
                    
                    Persona socio = GetSocioBRY(soc, personas);

                    List<Vehiculo> lstveh = vehiculos.FindAll(delegate (Vehiculo v) { return v.veh_duenio == socio.per_codigo; });
                    if (lstveh.Count == 0)
                    {
                        lstveh = vehiculos;
                    }
                    Persona remitente = GetPersonaByNom(rem, personas);
                    if (remitente == null)
                        remitente = socio;
                    Persona destinatario = GetPersonaByNom(des, personas);
                    if (destinatario == null)
                        destinatario = socio;

                    int? nrohojaruta = Conversiones.ObjectToIntNull(nrohr);
                    int? nrofacgui = Conversiones.ObjectToIntNull(nrofg);
                    int codigoruta = GetRuta(desti, rutas).rut_codigo;

                    

                    

                    string creacion = "";                    
                    Comprobante facgui = lst.Find(delegate (Comprobante h) { return h.com_almacen == almacenfg.alm_codigo && h.com_pventa == puntofg.pve_secuencia && h.com_numero == (nrofacgui ?? 0) && h.com_tipodoc == (tipo.ToUpper() == "F" ? 4 : 13); });
                    if (facgui == null)
                    {

                        string sigid = GetSigla1(tipo);
                        //int nrofacgui = GetNumero(nrofg);
                        facgui = GetGuia(remitente, destinatario, almacenfg, puntofg, nrofacgui.ToString(), sigid, fechr.Value, tot, (int?)politica.pol_codigo);
                        facgui.ccomenv.cenv_socio = socio.per_codigo;
                        facgui.ccomenv.cenv_nombres_soc = socio.per_razon;
                        facgui.ccomenv.cenv_vehiculo = lstveh[0].veh_codigo;
                        facgui.ccomenv.cenv_remitente = remitente.per_codigo;
                        facgui.ccomenv.cenv_ciruc_rem = remitente.per_ciruc;
                        facgui.ccomenv.cenv_nombres_rem = remitente.per_razon;
                        facgui.ccomenv.cenv_direccion_rem = remitente.per_direccion;
                        facgui.ccomenv.cenv_telefono_rem = remitente.per_telefono;
                        facgui.ccomenv.cenv_destinatario = destinatario.per_codigo;
                        facgui.ccomenv.cenv_ciruc_des = destinatario.per_ciruc;
                        facgui.ccomenv.cenv_nombres_des = destinatario.per_razon;
                        facgui.ccomenv.cenv_direccion_des = destinatario.per_direccion;
                        facgui.ccomenv.cenv_telefono_des = destinatario.per_telefono;
                        facgui.ccomenv.cenv_ruta = codigoruta;
                        //facgui = FAC.create_factura(facgui);

                        html.AppendFormat("{0},comprobante,NO EXISTE CREADO,{1}<br>", facgui.com_doctran, socio.per_razon);

                        
                    }
                    else
                    {
                        facgui.total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == facgui.com_codigo; });
                        facgui.ccomenv = lstcenv.Find(delegate (Ccomenv c) { return c.cenv_comprobante == facgui.com_codigo; });
                        if (facgui.ccomenv == null)
                        {
                            facgui.ccomenv = new Ccomenv();
                            facgui.ccomenv.cenv_empresa = facgui.com_empresa;
                            facgui.ccomenv.cenv_comprobante = facgui.com_codigo;

                            facgui.ccomenv.cenv_socio = socio.per_codigo;
                            facgui.ccomenv.cenv_nombres_soc = socio.per_razon;
                            facgui.ccomenv.cenv_vehiculo = lstveh[0].veh_codigo;
                            
                            facgui.ccomenv.cenv_remitente = remitente.per_codigo;
                            facgui.ccomenv.cenv_ciruc_rem = remitente.per_ciruc;
                            facgui.ccomenv.cenv_nombres_rem = remitente.per_razon;
                            facgui.ccomenv.cenv_direccion_rem = remitente.per_direccion;
                            facgui.ccomenv.cenv_telefono_rem = remitente.per_telefono;                                                       
                            facgui.ccomenv.cenv_destinatario = destinatario.per_codigo;
                            facgui.ccomenv.cenv_ciruc_des = destinatario.per_ciruc;
                            facgui.ccomenv.cenv_nombres_des = destinatario.per_razon;
                            facgui.ccomenv.cenv_direccion_des = destinatario.per_direccion;
                            facgui.ccomenv.cenv_telefono_des = destinatario.per_telefono;
                            facgui.ccomenv.cenv_ruta = codigoruta;
                            CcomenvBLL.Insert(facgui.ccomenv);
                            lstcenv.Add(facgui.ccomenv);
                            html.AppendFormat("{0},ccomenv,CREADO,{1}<br>", facgui.com_doctran, socio.per_razon);
                        }
                        else
                        {
                            if (facgui.ccomenv.cenv_socio != socio.per_codigo)
                            {
                                facgui.ccomenv.cenv_socio = socio.per_codigo;
                                facgui.ccomenv.cenv_nombres_soc = socio.per_razon;
                                facgui.ccomenv.cenv_vehiculo = lstveh[0].veh_codigo;
                                //facgui.ccomenv.cenv_remitente = remitente.per_codigo;
                                //facgui.ccomenv.cenv_nombres_rem = remitente.per_nombres;
                                //facgui.ccomenv.
                                facgui.ccomenv.cenv_empresa_key = facgui.com_empresa;
                                facgui.ccomenv.cenv_comprobante_key = facgui.com_codigo;
                                facgui.ccomenv.cenv_ruta = codigoruta;
                                CcomenvBLL.Update(facgui.ccomenv);
                                html.AppendFormat("{0},ccomenv,ACTUALIZADO SOCIO,{1}<br>", facgui.com_doctran, socio.per_razon);
                            }
                            else
                            {
                                facgui.ccomenv.cenv_nombres_soc = socio.per_razon;
                                facgui.ccomenv.cenv_vehiculo = lstveh[0].veh_codigo;
                                facgui.ccomenv.cenv_empresa_key = facgui.com_empresa;
                                facgui.ccomenv.cenv_comprobante_key = facgui.com_codigo;
                                facgui.ccomenv.cenv_ruta = codigoruta;
                                CcomenvBLL.Update(facgui.ccomenv);
                                html.AppendFormat("{0},ccomenv,ACTUALIZADO NOMBRES,{1}<br>", facgui.com_doctran, socio.per_razon);
                            }
                        }
                    }

                    //Asignacion hoja ruta 
                    Comprobante hr = lst.Find(delegate (Comprobante h) { return h.com_almacen == almacenhr.alm_codigo && h.com_pventa == puntohr.pve_secuencia && h.com_numero == (nrohojaruta ?? 0) && h.com_tipodoc == 5; });
                    if (hr == null)
                    {
                        html.AppendFormat("{0},hojaruta,NO EXISTE CREADO,{1}<br>", hr.com_doctran, socio.per_razon);
                    }
                    else
                    {
                        Rutaxfactura rfac = lstrutafac.Find(delegate(Rutaxfactura r) {return  r.rfac_comprobantefac == facgui.com_codigo; });
                        if (rfac == null)
                        {
                            rfac = new Rutaxfactura();
                            rfac.rfac_empresa = 1;
                            rfac.rfac_comprobanteruta = hr.com_codigo;
                            rfac.rfac_comprobantefac = facgui.com_codigo;
                            rfac.rfac_estado = 1;
                            rfac.crea_fecha = DateTime.Now;
                            rfac.crea_usr = "auto";
                            RutaxfacturaBLL.Insert(rfac);

                            Total tothr = lsttot.Find(delegate (Total t) { return t.tot_comprobante == hr.com_codigo; });
                            if (tothr!=null)
                            {
                                tothr.tot_total = tothr.tot_total + facgui.total.tot_total;
                                tothr.tot_empresa_key = tothr.tot_empresa;
                                tothr.tot_comprobante_key = tothr.tot_comprobante;
                                TotalBLL.Update(tothr);
                            }
                            html.AppendFormat("{0},hojaruta,FACGUI agregada,{1}<br>", hr.com_doctran, socio.per_razon);
                        }

                    }

                }
            }

            return html.ToString();



        }

        public static string UpdateHRBRY1(string path, string origen)
        {

            Catcliente categoria = Constantes.GetCatProv();//Obtiene politica por defecto           
            List<Almacen> almacenes = AlmacenBLL.GetAll("", "");
            List<Puntoventa> puntosven = PuntoventaBLL.GetAll("", "");
            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Vehiculo> vehiculos = VehiculoBLL.GetAll("", "");
            List<Politica> politicas = PoliticaBLL.GetAll("", "");


            List<Ruta> rutas = RutaBLL.GetAll("rut_origen='" + origen + "'", "");

            //int almacen = 1; //Uio
            //int pventa = 1;
            string fecha = "2019-08-01";
            string sociodef = "BRYSEAR CARGO";



            //List<Total> lsttot = TotalBLL.GetAll("tot_comprobante in (select com_codigo from comprobante where com_empresa=1  and com_fecha>='" + fecha + "')", "");
            List<Comprobante> lst = ComprobanteBLL.GetAll("com_empresa=1  and com_fecha>='" + fecha + "'", "");
            //List<Ccomdoc> lstcdoc = CcomdocBLL.GetAll("cdoc_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");

            List<Ccomenv> lstcenv = CcomenvBLL.GetAll("cenv_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");
            List<Rutaxfactura> lstrutafac = RutaxfacturaBLL.GetAll("rfac_comprobanteruta  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");
            List<Total> lsttot = TotalBLL.GetAll("tot_comprobante in (select com_codigo from comprobante where com_empresa=1  and com_fecha>='" + fecha + "')", "");

            Comprobante hojaruta = new Comprobante();
            List<Rutaxfactura> rutfac = new List<Rutaxfactura>();
            decimal total = 0;


            int i = 0;
            string line = "";


            StringBuilder html = new StringBuilder();

            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');

                    string soc = GetArrayVal(array, 0);
                    string almhr = GetArrayVal(array, 1);
                    string pvehr = GetArrayVal(array, 2);
                    string nrohr = GetArrayVal(array, 3);
                    string desti = GetArrayVal(array, 4);
                    DateTime? fechr = Conversiones.ObjectToDateTimeNull(GetArrayVal(array, 5));
                    string tipo = GetArrayVal(array, 6);
                    string almfg = GetArrayVal(array, 7);
                    string pvefg = GetArrayVal(array, 8);
                    string nrofg = GetArrayVal(array, 9);
                    string rem = GetArrayVal(array, 10);
                    int? bultos = Conversiones.ObjectToIntNull(GetArrayVal(array, 11));
                    string des = GetArrayVal(array, 12);
                    Decimal? tot = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 13));
                    string fp = GetArrayVal(array, 14);


                    Almacen almacenhr = GetAlmacen(almhr, almacenes);
                    Puntoventa puntohr = GetPventa(pvehr, almacenhr.alm_codigo, puntosven);


                    Almacen almacenfg = GetAlmacen(almfg, almacenes);
                    Puntoventa puntofg = GetPventa(pvefg, almacenfg.alm_codigo, puntosven);

                    Politica politica = GetPolitica(fp, politicas);

                    Persona socio = GetSocioBRY(soc, personas);

                    List<Vehiculo> lstveh = vehiculos.FindAll(delegate (Vehiculo v) { return v.veh_duenio == socio.per_codigo; });
                    if (lstveh.Count == 0)
                    {
                        lstveh = vehiculos;
                    }
                    Persona remitente = GetPersonaByNom(rem, personas);
                    if (remitente == null)
                        remitente = socio;
                    Persona destinatario = GetPersonaByNom(des, personas);
                    if (destinatario == null)
                        destinatario = socio;

                    int? nrohojaruta = Conversiones.ObjectToIntNull(nrohr);
                    int? nrofacgui = Conversiones.ObjectToIntNull(nrofg);
                    int codigoruta = GetRuta(desti, rutas).rut_codigo;

                    string creacion = "";
                    Comprobante facgui = lst.Find(delegate (Comprobante h) { return h.com_almacen == almacenfg.alm_codigo && h.com_pventa == puntofg.pve_secuencia && h.com_numero == (nrofacgui ?? 0) && h.com_tipodoc == (tipo.ToUpper() == "F" ? 4 : 13); });
                    if (facgui == null)
                    {
                        string sigid = GetSigla1(tipo);
                        string doctran = string.Format("{0}-{1}-{2}-{3:0000000}", sigid, almacenfg.alm_id, puntofg.pve_id, nrofacgui);
                        html.AppendFormat("{0},comprobante,NO EXISTE,{1}<br>", doctran, socio.per_razon);

                    }
                    else
                    {
                        facgui.total = lsttot.Find(delegate (Total t) { return t.tot_comprobante == facgui.com_codigo; });
                        facgui.ccomenv = lstcenv.Find(delegate (Ccomenv c) { return c.cenv_comprobante == facgui.com_codigo; });
                        if (facgui.ccomenv == null)
                        {
                            html.AppendFormat("{0},ccomenv,NO EXISTE,{1}<br>", facgui.com_doctran, socio.per_razon);
                        }
                        else
                        {
                            facgui.ccomenv.cenv_empresa_key = facgui.com_empresa;
                            facgui.ccomenv.cenv_comprobante_key = facgui.com_codigo;
                            facgui.ccomenv.cenv_ruta = codigoruta;
                            CcomenvBLL.Update(facgui.ccomenv);
                            html.AppendFormat("{0},ccomenv,ACTUALIZADO RUTA,{1}<br>", facgui.com_doctran, socio.per_razon);
                        }



                        //Asignacion hoja ruta 
                        Comprobante hr = lst.Find(delegate (Comprobante h) { return h.com_almacen == almacenhr.alm_codigo && h.com_pventa == puntohr.pve_secuencia && h.com_numero == (nrohojaruta ?? 0) && h.com_tipodoc == 5; });
                        if (hr == null)
                        {
                            string sigid = GetSigla1(tipo);
                            string doctran = string.Format("{0}-{1}-{2}-{3:0000000}", "HR", almacenhr.alm_id, puntohr.pve_id, nrohojaruta);
                            html.AppendFormat("{0},hojaruta,NO EXISTE,{1}<br>", doctran, socio.per_razon);
                        }
                        else
                        {
                            Rutaxfactura rfac = lstrutafac.Find(delegate (Rutaxfactura r) { return r.rfac_comprobantefac == facgui.com_codigo; });
                            if (rfac == null)
                            {
                                rfac = new Rutaxfactura();
                                rfac.rfac_empresa = 1;
                                rfac.rfac_comprobanteruta = hr.com_codigo;
                                rfac.rfac_comprobantefac = facgui.com_codigo;
                                rfac.rfac_estado = 1;
                                rfac.crea_fecha = DateTime.Now;
                                rfac.crea_usr = "auto";
                                RutaxfacturaBLL.Insert(rfac);

                                Total tothr = lsttot.Find(delegate (Total t) { return t.tot_comprobante == hr.com_codigo; });
                                if (tothr != null)
                                {
                                    tothr.tot_total = tothr.tot_total + facgui.total.tot_total;
                                    tothr.tot_empresa_key = tothr.tot_empresa;
                                    tothr.tot_comprobante_key = tothr.tot_comprobante;
                                    TotalBLL.Update(tothr);
                                }
                                html.AppendFormat("{0},hojaruta,FACGUI agregada,{1}<br>", hr.com_doctran, socio.per_razon);
                            }
                        }

                    }

                }
            }

            return html.ToString();



        }


        public static Persona GetSocioBRY(string soc, List<Persona> personas)
        {
            string id = "";
            if (soc == "PY")
                id = "0102995644001";
            if (soc == "JY")
                id = "0105970891001";
            if (soc == "BR")
                id = "0190413985001";
            if (soc == "FCH")
                id = "1714861588001";            
            if (soc == "CG")
                id = "0104334743";
            if (soc == "CA")
                id = "0102534286001";
            if (soc == "MM")
                id = "0105656201";

            Persona per = personas.Find(delegate (Persona p) { return p.per_ciruc == id; });
            return per;                       

        }


        public static string UpdateCcomenv()
        {

            string fecha = "2019-08-01";

            List<Persona> personas = PersonaBLL.GetAll("", "");
            List<Vehiculo> vehiculos = VehiculoBLL.GetAll("", "");
            List<Comprobante> lst = ComprobanteBLL.GetAll("com_empresa=1  and com_fecha>='" + fecha + "'", "");
            List<Ccomenv> lstcenv = CcomenvBLL.GetAll("cenv_comprobante  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");
            List<Rutaxfactura> lstrutafac = RutaxfacturaBLL.GetAll("rfac_comprobanteruta  in (select com_codigo from comprobante where com_empresa=1 and com_fecha>='" + fecha + "')", "");

            List<Ccomenv> lstcenvNEW = new List<Ccomenv>();
            StringBuilder html = new StringBuilder();

            int i = 0;
            
            foreach (Comprobante item in lst)
            {
                if (item.com_tipodoc == 4 || item.com_tipodoc== 13)
                {
                    Ccomenv cenv = lstcenv.Find(delegate (Ccomenv c) { return c.cenv_comprobante == item.com_codigo; });
                    if (cenv == null)
                    {
                        Rutaxfactura rxf = lstrutafac.Find(delegate (Rutaxfactura r) { return r.rfac_comprobantefac == item.com_codigo; });
                        Persona socio = new Persona();
                        Comprobante hr = new Comprobante();
                        int? codvehiculo = null;
                        if (rxf != null)
                        {
                            hr = lst.Find(delegate (Comprobante c) { return c.com_codigo == rxf.rfac_comprobanteruta; });
                            socio = personas.Find(delegate (Persona p) { return p.per_codigo == hr.com_codclipro; });
                            List<Vehiculo> lstveh = vehiculos.FindAll(delegate (Vehiculo v) { return v.veh_duenio == socio.per_codigo; });
                            if (lstveh.Count == 0)
                                lstveh = vehiculos;
                            codvehiculo = lstveh[0].veh_codigo;
                        }
                        

                        cenv = new Ccomenv();
                        cenv.cenv_empresa = 1;
                        cenv.cenv_comprobante = item.com_codigo;
                        cenv.cenv_socio = socio.per_codigo;
                        cenv.cenv_nombres_soc = socio.per_razon;
                        cenv.cenv_vehiculo = codvehiculo;
                        lstcenvNEW.Add(cenv);
                        html.Append(item.com_doctran + "," + hr.com_doctran + "," + socio.per_razon+"<br>");
                        


                    }
                }
                i++;

            }

            i = 0;
            BLL transaction = new BLL();
            transaction.CreateTransaction();
            try
            {

                transaction.BeginTransaction();

                foreach (Ccomenv item in lstcenvNEW)
                {
                    CcomenvBLL.Insert(transaction, item);
                    i++;
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



        public static string CerrarCarteraClientes(int empresa,DateTime? desde, DateTime? hasta, bool all)
        {
            WhereParams parddo = new WhereParams();
            WhereParams parcan = new WhereParams();
            
            List<object> valddo = new List<object>();
            List<object> valcan = new List<object>();

            //string tipos = "3,4,6,17,19,20";

            string ctas = Constantes.GetParameter("ctasclientes");

            parddo.where = "ddo_empresa = " + empresa + " and ddo_cuenta in ("+ctas+") and ddo_monto>0";            
            parddo.where += "  and com_estado=2 and com_fecha between {0} and {1}";
            valddo.Add(desde);
            valddo.Add(hasta);
            parddo.valores = valddo.ToArray();

            string documentosNot = "";

            //List<Planillacli> planillascli = PlanillacliBLL.GetAll("plc_empresa=1", "");


            List<Ddocumento> ddocumentos = DdocumentoBLL.GetAll(parddo, "");

            string wherein = String.Join(",", ddocumentos.Select(s => s.ddo_comprobante).ToList().Distinct().ToArray());

            parcan.where = "dca_empresa=" + empresa + " and dca_comprobante in ("+wherein+")";
            List<Dcancelacion> dcancelaciones = DcancelacionBLL.GetAll(parcan, "");


            StringBuilder html = new StringBuilder();
            int i = 0;
            foreach (Ddocumento ddo in ddocumentos)
            {
                ddo.ddo_monto = 0;                
                ddo.ddo_empresa_key = ddo.ddo_empresa;
                ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                ddo.ddo_transacc_key = ddo.ddo_transacc;
                ddo.ddo_doctran_key = ddo.ddo_doctran;
                ddo.ddo_pago_key = ddo.ddo_pago;
                DdocumentoBLL.Update(ddo);
                i++;
                List<Dcancelacion> cancelaciones = dcancelaciones.FindAll(d => d.dca_comprobante == ddo.ddo_comprobante && d.dca_pago == ddo.ddo_pago);

                foreach (Dcancelacion item in cancelaciones)
                {
                    item.dca_monto = 0;
                    item.dca_empresa_key = item.dca_empresa;
                    item.dca_comprobante_key = item.dca_comprobante;
                    item.dca_comprobante_can_key = item.dca_comprobante_can;
                    item.dca_transacc_key = item.dca_transacc;
                    item.dca_doctran_key = item.dca_doctran;
                    item.dca_pago_key = item.dca_pago;
                    item.dca_secuencia_key = item.dca_secuencia;
                    DcancelacionBLL.Update(item);

                }

                /*decimal saldo = (ddo.ddo_monto??0) - cancelaciones.Sum(s => s.dca_monto??0);
                if (saldo > 0)
                {
                    ddo.ddo_monto = saldo > 0 ? ddo.ddo_monto - saldo : ddo.ddo_monto;
                    ddo.ddo_empresa_key = ddo.ddo_empresa;
                    ddo.ddo_comprobante_key = ddo.ddo_comprobante;
                    ddo.ddo_transacc_key = ddo.ddo_transacc;
                    ddo.ddo_doctran_key = ddo.ddo_doctran;
                    ddo.ddo_pago_key = ddo.ddo_pago;
                    DdocumentoBLL.Update(ddo);
                    html.AppendFormat("{0} Saldo: {1}<br>", ddo.ddo_compdoctran, saldo);
                }*/
            }

            return html.ToString();



        }

        public static string CuadrarCatera(string path)
        {
            int i = 0;
            string line = "";

            //0: FECHA
            //1: FACTURA
            //2: MONTO


            StringBuilder html = new StringBuilder();

            //using (StreamReader streamReader = File.OpenText(path))
            using (StreamReader streamReader = new StreamReader(path, Encoding.GetEncoding("iso-8859-1")))
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] array = line.Split(';');

                    string fecha = GetArrayVal(array, 0);
                    string nro = GetArrayVal(array, 1);
                    decimal? monto = Conversiones.ObjectToDecimalNull(GetArrayVal(array, 2));

                    /*Persona per = GetPersonaByNom(cli, personas);
                    if (per != null)
                    {
                        try
                        {
                            //Comprobante obl = GetObligacion(per, nro, fecha, subtotal, iva);
                            Comprobante fac = GetFactura(per, nro, fecha, saldo);
                            fac = FAC.create_factura(fac);
                            fac = FAC.account_factura(fac);
                            html.AppendFormat("Factura:{0} Cliente:{1} creada correctamente...<br>", fac.com_doctran, per.per_razon);
                            i++;
                        }
                        catch (Exception ex)
                        {
                            html.AppendFormat("Cliente:{0}, Nrodoc:{1} ERROR:{2}<br>", cli, nro, ex.Message);
                        }
                    }
                    else
                        html.AppendFormat("El Cliente {0}, no se encuentra en el registrado en el sistema<br>", cli);
                    */
                }
            }

            return html.ToString();
        }


    }
}

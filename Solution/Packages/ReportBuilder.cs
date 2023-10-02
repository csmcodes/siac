using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Data;
using BusinessObjects;
using BusinessLogicLayer;
using Functions;
using Services;

namespace Packages
{
    public class ReportBuilder
    {
        protected static string reportfolder = "reports/";

        public static void BuildReport(ref ReportViewer reportviewer, string reportcode, string empresa, params object[] parameters)
        {
            Empresa emp = EmpresaBLL.GetByPK(new Empresa { emp_codigo = int.Parse(empresa), emp_codigo_key = int.Parse(empresa) });
            LocalReport rep = reportviewer.LocalReport;
            switch (reportcode)
            {
                case "DETALLEBAN":
                    DateTime? desde_DBAN = Functions.Conversiones.ObjectToDateTimeNull(parameters[0].ToString());
                    DateTime? hasta_DBAN = Functions.Conversiones.ObjectToDateTimeNull(parameters[1].ToString());
                    int? banco_DBAN = Functions.Conversiones.ObjectToIntNull(parameters[2].ToString());
                    string strbanco_DBAN = parameters[3].ToString();
                    string benef_DBAN = parameters[4].ToString();
                    int? tipo_DBAN = Functions.Conversiones.ObjectToIntNull(parameters[5].ToString());
                    int? alm_DBAN = Functions.Conversiones.ObjectToIntNull(parameters[6].ToString());

                    List<Dbancario> lst_DBAN = BAN.GetDetalleBanco(emp.emp_codigo, banco_DBAN, tipo_DBAN, benef_DBAN, desde_DBAN, hasta_DBAN, new int[] { 2, 3 });
                    rep.ReportPath = reportfolder + "DetalleBancario.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", lst_DBAN));
                    rep.SetParameters(new ReportParameter("desde", desde_DBAN.HasValue ? desde_DBAN.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("hasta", hasta_DBAN.HasValue ? hasta_DBAN.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("banco", strbanco_DBAN));
                    rep.SetParameters(new ReportParameter("inicial", lst_DBAN.Count > 0 ? Formatos.CurrencyFormat(lst_DBAN[0].dban_debcre== Constantes.cCredito? lst_DBAN[0].dban_valor_nac + lst_DBAN[0].dban_saldo: lst_DBAN[0].dban_valor_nac - lst_DBAN[0].dban_saldo) : ""));
                    rep.SetParameters(new ReportParameter("final", lst_DBAN.Count > 0 ? Formatos.CurrencyFormat(lst_DBAN[lst_DBAN.Count-1].dban_saldo) : ""));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;


                case "PLANILLASOCTOTCON":
                case "PLANILLASOCTOT":

                    DateTime? desde_PST = Functions.Conversiones.ObjectToDateTimeNull(parameters[0].ToString());
                    DateTime? hasta_PST = Functions.Conversiones.ObjectToDateTimeNull(parameters[1].ToString());
                    string socio_PST = parameters[2].ToString();

                    if (reportcode == "PLANILLASOCTOT")
                        rep.ReportPath = reportfolder + "PlanillaSocioTotales.rdlc";
                    if (reportcode == "PLANILLASOCTOTCON")
                        rep.ReportPath = reportfolder + "PlanillaSocioTotalesCon.rdlc";

                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetPlanillaSocioTot(desde_PST, hasta_PST, emp.emp_codigo, socio_PST)));
                    rep.SetParameters(new ReportParameter("desde", desde_PST.HasValue ? desde_PST.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("hasta", hasta_PST.HasValue ? hasta_PST.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;

                case "COMPROBANTEHR":
                    DateTime? desdechr = null;
                    DateTime? hastachr = null;

                    string srtdesdechr = parameters[0].ToString();
                    string srthastachr = parameters[1].ToString();
                    if (!string.IsNullOrEmpty(srtdesdechr))
                        desdechr = DateTime.Parse(srtdesdechr);
                    if (!string.IsNullOrEmpty(srthastachr))
                        hastachr = DateTime.Parse(srthastachr);


                    //string nompoliticagp = parameters[9].ToString();
                    //string politicagp = parameters[8].ToString();
                    //string personagp = parameters[7].ToString();
                    string usuariochr = parameters[6].ToString();

                    string almacenchr = parameters[2].ToString();
                    string pventachr = parameters[3].ToString();
                    string nomalamcenchr = parameters[4].ToString();
                    string nompventachr = parameters[5].ToString();
                    if (string.IsNullOrEmpty(nomalamcenchr))
                        nomalamcenchr = "TODOS";
                    if (string.IsNullOrEmpty(nompventachr))
                        nompventachr = "TODOS";

                    int? almchr = null;
                    int? pvechr = null;
                    //int? polchr = null;
                    if (!string.IsNullOrEmpty(almacenchr))
                        almchr = int.Parse(almacenchr);
                    if (!string.IsNullOrEmpty(pventachr))
                        pvechr = int.Parse(pventachr);
                    //if (!string.IsNullOrEmpty(politicagp))
                    //    polgp = int.Parse(politicagp);


                    rep.ReportPath = reportfolder + "ComprobanteHojaRuta.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetComprobanteHojaRuta(desdechr, hastachr, emp.emp_codigo, almchr, pvechr)));
                    rep.SetParameters(new ReportParameter("desde", desdechr.HasValue ? desdechr.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("hasta", hastachr.HasValue ? hastachr.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("almacen", nomalamcenchr));
                    rep.SetParameters(new ReportParameter("pventa", nompventachr));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    //rep.SetParameters(new ReportParameter("politica", nompoliticagp));
                    //rep.SetParameters(new ReportParameter("usuario", usuariocp));                    
                    break;
                case "ELECTRONICOS":
                    string almacen_ele = parameters[2].ToString();
                    string pventa_ele = parameters[3].ToString();
                    string stralm_ele = parameters[4].ToString();
                    string strpve_ele = parameters[5].ToString();
                    string tipo_ele = parameters[6].ToString();
                    string estado_ele = parameters[7].ToString();
                    string usr_ele = parameters[8].ToString();

                    int? almv_ele = null;
                    int? pvev_ele = null;
                    int? tdoc_ele = null;
                    if (!string.IsNullOrEmpty(almacen_ele))
                        almv_ele = int.Parse(almacen_ele);
                    if (!string.IsNullOrEmpty(pventa_ele))
                        pvev_ele = int.Parse(pventa_ele);
                    if (!string.IsNullOrEmpty(tipo_ele))
                        tdoc_ele = int.Parse(tipo_ele);

                    rep.ReportPath = reportfolder + "Electronicos.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.ComprobantesElectronicos(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almv_ele, pvev_ele, emp.emp_codigo, tdoc_ele, estado_ele)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralm_ele));
                    rep.SetParameters(new ReportParameter("pventa", strpve_ele));
                    //rep.SetParameters(new ReportParameter("usuario", usr_ct));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;

                case "COMPTRANS":

                    string almacen_ct = parameters[2].ToString();
                    string pventa_ct = parameters[3].ToString();
                    string stralm_ct = parameters[4].ToString();
                    string strpve_ct = parameters[5].ToString();
                    string ruta_ct = parameters[6].ToString();
                    string estado_ct = parameters[8].ToString();
                    string usr_ct = parameters[9].ToString();

                    int? almv_ct = null;
                    int? pvev_ct = null;
                    int? rut_ct = null;
                    if (!string.IsNullOrEmpty(almacen_ct))
                        almv_ct = int.Parse(almacen_ct);
                    if (!string.IsNullOrEmpty(pventa_ct))
                        pvev_ct = int.Parse(pventa_ct);
                    if (!string.IsNullOrEmpty(ruta_ct))
                        rut_ct = int.Parse(ruta_ct);

                    rep.ReportPath = reportfolder + "ComprobantesTransporte.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getComprobantesTransporte(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almv_ct, pvev_ct, emp.emp_codigo, rut_ct,estado_ct)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralm_ct));
                    rep.SetParameters(new ReportParameter("pventa", strpve_ct));
                    //rep.SetParameters(new ReportParameter("usuario", usr_ct));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;

                case "VALSOCIOSINPLANILLA":

                    string strsocio  = parameters[0].ToString();
                    string usuariovs = parameters[1].ToString();
                    Persona soc = new Persona();
                    int? sociovs = null;
                    if (!string.IsNullOrEmpty(strsocio))
                    {
                        sociovs = int.Parse(strsocio);
                        soc = PersonaBLL.GetByPK(new Persona { per_empresa = emp.emp_codigo, per_empresa_key = emp.emp_codigo, per_codigo = sociovs.Value, per_codigo_key = sociovs.Value });
                    }
                    rep.ReportPath = reportfolder + "ValoresSocios.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetValoresSocioSinPlanilla(emp.emp_codigo, sociovs)));
                    rep.SetParameters(new ReportParameter("socio", sociovs.HasValue ? soc.per_razon : "SIN SOCIO"));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    rep.SetParameters(new ReportParameter("usuario", usuariovs));

                    break;
                case "GUIAPLANILLA":
                case "GUIASFACTURAS":
                    DateTime? desdegp = null;
                    DateTime? hastagp = null;

                    string srtdesdegp = parameters[0].ToString();
                    string srthastagp = parameters[1].ToString();
                    if (!string.IsNullOrEmpty(srtdesdegp))
                        desdegp = DateTime.Parse(srtdesdegp);
                    if (!string.IsNullOrEmpty(srthastagp))
                        hastagp = DateTime.Parse(srthastagp);


                    string socgp = parameters[10].ToString();
                    string tipogp = parameters[11].ToString();

                    string nompoliticagp = parameters[9].ToString();
                    string politicagp = parameters[8].ToString();
                    string personagp= parameters[7].ToString();
                    string usuariogp = parameters[6].ToString();

                    string almacengp = parameters[2].ToString();
                    string pventagp = parameters[3].ToString();
                    string nomalamcengp = parameters[4].ToString();
                    string nompventagp = parameters[5].ToString();
                    if (string.IsNullOrEmpty(nomalamcengp))
                        nomalamcengp = "TODOS";
                    if (string.IsNullOrEmpty(nompventagp))
                        nompventagp = "TODOS";

                    int? almgp = null;
                    int? pvegp = null;
                    int? polgp = null;
                    if (!string.IsNullOrEmpty(almacengp))
                        almgp = int.Parse(almacengp);
                    if (!string.IsNullOrEmpty(pventagp))
                        pvegp = int.Parse(pventagp);
                    if (!string.IsNullOrEmpty(politicagp))
                        polgp = int.Parse(politicagp);

                    if (reportcode == "GUIAPLANILLA")
                    {
                        rep.ReportPath = reportfolder + "GuiaPlanilla.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetGuiasPlanillas(desdegp, hastagp, emp.emp_codigo, almgp, pvegp, personagp, polgp)));
                    }
                    if (reportcode == "GUIASFACTURAS")
                    {
                        rep.ReportPath = reportfolder + "GuiasFacturas.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetGuiasFacturas(desdegp, hastagp, emp.emp_codigo, almgp, pvegp, personagp, socgp, tipogp, polgp)));
                    }
                    rep.SetParameters(new ReportParameter("desde", desdegp.HasValue ? desdegp.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("hasta", hastagp.HasValue ? hastagp.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("almacen", nomalamcengp));
                    rep.SetParameters(new ReportParameter("pventa", nompventagp));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    rep.SetParameters(new ReportParameter("politica", nompoliticagp));
                    //rep.SetParameters(new ReportParameter("usuario", usuariocp));                    
                    break;
                case "CUENTASPORDOCANEXO":
                case "CUENTASPORANEXO":
                case "CUENTASPOR":
                case "CUENTASPORDET":
                case "CUENTASPORVEN":
                case "CUENTASPORVENCON":

                    DateTime? desdecp = null;
                    DateTime? hastacp = null;

                    string srtdesdecp = parameters[0].ToString();
                    string srthastacp = parameters[1].ToString();
                    if (!string.IsNullOrEmpty(srtdesdecp))
                        desdecp = DateTime.Parse(srtdesdecp);
                    if (!string.IsNullOrEmpty(srthastacp))
                        hastacp = DateTime.Parse(srthastacp);

                    string tcxc = parameters[10].ToString();//NUEVO PARAMETRO PARA TIPO CXC
                    string polcxc = parameters[11].ToString();//NUEVO PARAMETRO PARA POLITICA

                    string strctas = parameters[9].ToString();
                    string[] arrayctas = strctas.Split('|');

                    string tipocp = parameters[8].ToString();
                    string personacp = parameters[7].ToString();
                    string usuariocp= parameters[6].ToString();

                    string almacencp = parameters[2].ToString();
                    string pventacp = parameters[3].ToString();
                    string nomalamcencp = parameters[4].ToString();
                    string nompventacp = parameters[5].ToString();
                    if (string.IsNullOrEmpty(nomalamcencp))
                        nomalamcencp= "TODOS";
                    if (string.IsNullOrEmpty(nompventacp))
                        nompventacp= "TODOS";

                    int? almcp = null;
                    int? pvecp= null;
                    if (!string.IsNullOrEmpty(almacencp))
                        almcp= int.Parse(almacencp);
                    if (!string.IsNullOrEmpty(pventacp))
                        pvecp= int.Parse(pventacp);

                    List<int> lstcuentas = new List<int>();
                    foreach (string item in arrayctas)
                    {
                        int cta = -1;
                        int.TryParse(item, out cta);
                        if (cta > 0)
                            lstcuentas.Add(cta);
                    }
                    if (reportcode == "CUENTASPORANEXO")
                    {
                        rep.ReportPath = reportfolder + "ContableAnexo.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.ContablesAnexos(desdecp, hastacp, emp.emp_codigo, almcp, pvecp, personacp, lstcuentas.ToArray())));
                    }
                    if (reportcode == "CUENTASPOR")
                    {

                        //rep.ReportPath = reportfolder + "CuentasPorAnexo.rdlc";
                        //rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.CuentasPorAnexos(desdecp, hastacp, emp.emp_codigo, almcp, pvecp, personacp, lstcuentas.ToArray())));
                        rep.ReportPath = reportfolder + "CuentasPor.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetCuentasPorFull(desdecp, hastacp, emp.emp_codigo, almcp, pvecp, personacp, tipocp, lstcuentas.ToArray(),tcxc, polcxc)));
                    }
                    if (reportcode == "CUENTASPORDET")
                    {

                        rep.ReportPath = reportfolder + "CuentasPorDet.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetCuentasPorFull(desdecp, hastacp, emp.emp_codigo, almcp, pvecp, personacp, tipocp, lstcuentas.ToArray(), tcxc, polcxc)));
                    }
                    if (reportcode == "CUENTASPORVEN")
                    {
                        rep.ReportPath = reportfolder + "CuentasPorNew.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetCuentasPorFull(desdecp, hastacp, emp.emp_codigo, almcp, pvecp, personacp, tipocp, lstcuentas.ToArray(), tcxc, polcxc)));
                    }
                    if (reportcode == "CUENTASPORVENCON")
                    {

                        rep.ReportPath = reportfolder + "CuentasPorConsolidoNew.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetCuentasPorFull(desdecp, hastacp, emp.emp_codigo, almcp, pvecp, personacp, tipocp, lstcuentas.ToArray(), tcxc, polcxc)));
                    }



                    if (reportcode == "CUENTASPORDOCANEXO")
                    {

                        rep.ReportPath = reportfolder + "DocumentoAnexo.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.DocumentosAnexos(desdecp, hastacp, emp.emp_codigo, almcp, pvecp, personacp, lstcuentas.ToArray())));
                    }
                    rep.SetParameters(new ReportParameter("desde", desdecp.HasValue ? desdecp.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("hasta", hastacp.HasValue ? hastacp.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("almacen", nomalamcencp));
                    rep.SetParameters(new ReportParameter("pventa", nompventacp));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    rep.SetParameters(new ReportParameter("tipo", tipocp));
                    rep.SetParameters(new ReportParameter("usuario", usuariocp));
                    break;
                case "ESTADOCUENTA":

                    DateTime? desdeec = null;
                    DateTime? hastaec = null;

                    string srtdesdeec = parameters[0].ToString();
                    string srthastaec = parameters[1].ToString();
                    if (!string.IsNullOrEmpty(srtdesdeec))
                        desdeec = DateTime.Parse(srtdesdeec);
                    if (!string.IsNullOrEmpty(srthastaec))
                        hastaec = DateTime.Parse(srthastaec);

                    if (hastaec.HasValue)
                        hastaec = hastaec.Value.AddDays(1).AddSeconds(-1);
                    string almacenec = parameters[2].ToString();
                    //string nomalamcenec = parameters[4].ToString();
                    
                    int? almec = null;
                    
                    if (!string.IsNullOrEmpty(almacenec))
                        almec = int.Parse(almacenec);


                    int personaec = int.Parse(parameters[3].ToString());
                    string nombres = parameters[4].ToString();
                    int debcreec = int.Parse(parameters[5].ToString());
                    string ordenec = parameters[6].ToString();

                    string parctas = "";
                    string tipoec = "";
                    if (debcreec == 1)//Clientes
                    {
                        parctas = Services.Constantes.GetParameter("ctasclientes");
                        tipoec = "CLIENTE";
                    }
                    else//Proveedores
                    {
                        parctas = Services.Constantes.GetParameter("ctasproveedores");
                        tipoec = "PROVEEDOR";
                    }

                    decimal saldoinicial = 0;
                    decimal saldofinal = 0;

                    List<vEstadoCuenta> lstestado = Packages.General.EstadoCuentaDetalle(desdeec, hastaec, emp.emp_codigo, almec, null, personaec,  parctas, debcreec, ordenec, ref saldoinicial, ref saldofinal);

                    rep.ReportPath = reportfolder + "EstadoCuenta.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", lstestado));
                    rep.SetParameters(new ReportParameter("desde", desdeec.HasValue ? desdeec.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("hasta", hastaec.HasValue ? hastaec.Value.ToShortDateString() : ""));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    rep.SetParameters(new ReportParameter("tipo", tipoec ));
                    rep.SetParameters(new ReportParameter("persona", nombres));
                    rep.SetParameters(new ReportParameter("saldoinicial", saldoinicial.ToString("0.00")));
                    rep.SetParameters(new ReportParameter("saldofinal", saldofinal.ToString("0.00")));


                    break;

                case "ESTADOCUENTADET":

                    DateTime fechaecd = DateTime.Parse(parameters[0].ToString());
                    int codclipro = int.Parse(parameters[1].ToString());
                    int debcre = int.Parse(parameters[2].ToString());
                    int codalmancen = int.Parse(parameters[3].ToString());
                    rep.ReportPath = reportfolder + "EstadoCuenta.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetEstadoCuentaDet(fechaecd, emp.emp_codigo, codclipro, debcre, codalmancen)));
               
                    break;
                case "DOMICILIO":

                    string tipodocdom  = parameters[2].ToString();
                    string usuariodom= parameters[3].ToString();

                    string almacendom= parameters[4].ToString();
                    string pventadom= parameters[5].ToString();
                    string nomalamcendom= parameters[6].ToString();
                    string nompventavdom = parameters[7].ToString();
                    if (string.IsNullOrEmpty(nomalamcendom))
                        nomalamcendom= "TODOS";
                    if (string.IsNullOrEmpty(nompventavdom))
                        nompventavdom = "TODOS";

                    int? almdom = null;
                    int? pvedom= null;
                    if (!string.IsNullOrEmpty(almacendom))
                        almdom= int.Parse(almacendom);
                    if (!string.IsNullOrEmpty(pventadom))
                        pvedom= int.Parse(pventadom);
                    int? tdocdom = null;
                    if (!string.IsNullOrEmpty(tipodocdom))
                        tdocdom = int.Parse(tipodocdom);


                    string rutadom = parameters[8].ToString();
                    string nomrutadom = parameters[9].ToString();
                    int? rutdom = null;
                    if (!string.IsNullOrEmpty(rutadom))
                        rutdom = int.Parse(rutadom);

                    rep.ReportPath = reportfolder + "Domicilios.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getValoresDomicilios(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), emp.emp_codigo, almdom, pvedom, tdocdom, rutdom)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", nomalamcendom));
                    rep.SetParameters(new ReportParameter("pventa", nompventavdom));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    rep.SetParameters(new ReportParameter("ruta", nomrutadom));
                    break;
                case "SEGURO":

                    string remitenteseg = parameters[2].ToString();
                    string destinatarioseg = parameters[3].ToString();
                    string usuarioseg = parameters[4].ToString();

                    string almacenseg = parameters[5].ToString();
                    string pventaseg = parameters[6].ToString();
                    string nomalamcenseg = parameters[7].ToString();
                    string nompventavseg = parameters[8].ToString();
                    if (string.IsNullOrEmpty(nomalamcenseg))
                        nomalamcenseg = "TODOS";
                    if (string.IsNullOrEmpty(nompventavseg))
                        nompventavseg = "TODOS";

                    int? almseg = null;
                    int? pveseg = null;
                    if (!string.IsNullOrEmpty(almacenseg))
                        almseg = int.Parse(almacenseg);
                    if (!string.IsNullOrEmpty(pventaseg))
                        pveseg = int.Parse(pventaseg);

                    int? codperseg = null;
                    if (!string.IsNullOrEmpty(remitenteseg))
                        codperseg= int.Parse(remitenteseg);

                    rep.ReportPath = reportfolder + "Seguros.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getValoresSeguros(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), emp.emp_codigo, almseg, pveseg, codperseg, destinatarioseg)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", nomalamcenseg));
                    rep.SetParameters(new ReportParameter("pventa", nompventavseg));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;
                case "HRSOCIO"://ESTE REPORTE NO SE ENCUENTRA EN USO
                    string sociohr = parameters[2].ToString();
                    string usuariohr = parameters[3].ToString();

                    string almacenhr = parameters[4].ToString();
                    string pventahr = parameters[5].ToString();
                    string nomalamcenhr = parameters[6].ToString();
                    string nompventavhr = parameters[7].ToString();
                    if (string.IsNullOrEmpty(nomalamcenhr))
                        nomalamcenhr = "TODOS";
                    if (string.IsNullOrEmpty(nompventavhr))
                        nompventavhr = "TODOS";

                    int? almhr = null;
                    int? pvehr = null;
                    if (!string.IsNullOrEmpty(almacenhr))
                        almhr = int.Parse(almacenhr);
                    if (!string.IsNullOrEmpty(pventahr))
                        pvehr = int.Parse(pventahr);

                    int? codsociohr = null;
                    if (!string.IsNullOrEmpty(sociohr))
                        codsociohr = int.Parse(sociohr);
                    

                    rep.ReportPath = reportfolder + "HojasRutaSocio.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getHojasRutaSocio(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), emp.emp_codigo, almhr, pvehr, codsociohr)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", nomalamcenhr));
                    
                    rep.SetParameters(new ReportParameter("pventa", nompventavhr));
                    // rep.SetParameters(new ReportParameter("anuladas", "0"));
                    //rep.SetParameters(new ReportParameter("usuario", usuarioth));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;
                case "PLACLI":                    
                    rep.ReportPath = reportfolder + "PlanillaCliente.rdlc";
                    string subtotalpc = "";
                    string ivapc = "";
                    string totalpc= "";

                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getPlanillaCliente(long.Parse(parameters[0].ToString()), emp.emp_codigo,ref subtotalpc, ref ivapc, ref totalpc)));
                    rep.SetParameters(new ReportParameter("str_subtotal", subtotalpc));
                    rep.SetParameters(new ReportParameter("str_iva", ivapc));
                    rep.SetParameters(new ReportParameter("str_total", totalpc));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    //rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    //rep.SetParameters(new ReportParameter("numero", parameters[4].ToString()));
                    break;
                case "PLACLIDET":
                    string subtotalpcdet = "";
                    string ivapcdet = "";
                    string totalpcdet = "";
                    rep.ReportPath = reportfolder + "PlanillaClienteCOMY.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getPlanillaClienteDet(long.Parse(parameters[0].ToString()), emp.emp_codigo, ref subtotalpcdet, ref ivapcdet, ref totalpcdet)));
                    rep.SetParameters(new ReportParameter("str_subtotal", subtotalpcdet));
                    rep.SetParameters(new ReportParameter("str_iva", ivapcdet));
                    rep.SetParameters(new ReportParameter("str_total", totalpcdet));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    //rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    //rep.SetParameters(new ReportParameter("numero", parameters[4].ToString()));
                    break;

                case "DETGUI"://REPORTE DE DETALLE DE GUIAS DE EMBARQUE
                    DateTime desdeDG = DateTime.Parse(parameters[0].ToString());
                    DateTime hastaDG = DateTime.Parse(parameters[1].ToString());

                    string strpol = parameters[6].ToString();
                    int? pol = null;
                    if (!string.IsNullOrEmpty(strpol))
                        pol = int.Parse(strpol);

                    string strcli_DG = parameters[2].ToString();
                    int? cli_DG = null;
                    if (!string.IsNullOrEmpty(strcli_DG))
                        cli_DG = int.Parse(strcli_DG);

                    string strrut_DG = parameters[3].ToString();
                    int? rut_DG = null;
                    if (!string.IsNullOrEmpty(strrut_DG))
                        rut_DG = int.Parse(strrut_DG);




                    rep.ReportPath = reportfolder + "Detalles.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getDetalles(desdeDG, hastaDG, cli_DG, rut_DG, pol, emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    rep.SetParameters(new ReportParameter("numero", parameters[5].ToString()));
                    rep.SetParameters(new ReportParameter("desde", desdeDG.ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", hastaDG.ToShortDateString()));
                    break;
                case "CXS"://Reporte de Cobro por Socio

                    string socio = parameters[2].ToString();                    
                    int codsoc = -1;
                    int.TryParse(socio, out codsoc);

                    rep.ReportPath = reportfolder + "CobroxSocio.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getCobrosSocio(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), (codsoc == 0 ? null : (int?)codsoc),emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("empresa",emp.emp_nombre));
                    
                    break;
                case "VENTAS"://Reporte de Cobro por Socio

                    string almacen1 = parameters[2].ToString();
                    string pventa1 = parameters[3].ToString();
                    string stralm1 = parameters[4].ToString();
                    string strpve1 = parameters[5].ToString();
                    string usr1 = parameters[6].ToString();
                    string strpolve1 = parameters[7].ToString();

                    int? almv1 = null;
                    int? pvev1 = null;
                    if (!string.IsNullOrEmpty(almacen1))
                        almv1 = int.Parse(almacen1);
                    if (!string.IsNullOrEmpty(pventa1))
                        pvev1 = int.Parse(pventa1);

                    rep.ReportPath = reportfolder + "ReporteVentasSri.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasRet(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almv1, pvev1,emp.emp_codigo,strpolve1)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralm1));
                    rep.SetParameters(new ReportParameter("pventa", strpve1));
                    rep.SetParameters(new ReportParameter("usuario", usr1));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));




                    break;
                case "VEN"://Reporte de Cobro por Socio

                    string almacen = parameters[2].ToString();
                    string pventa = parameters[3].ToString();
                    string stralm = parameters[4].ToString();
                    string strpve = parameters[5].ToString();
                    string usr = parameters[6].ToString(); 
                    string strpolve = parameters[7].ToString();                    


                    int? almv = null;
                    int? pvev = null;
                    if (!string.IsNullOrEmpty(almacen))
                        almv = int.Parse(almacen);
                    if (!string.IsNullOrEmpty(pventa))
                        pvev = int.Parse(pventa);

                    rep.ReportPath = reportfolder + "Ventas.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentas(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almv,pvev,true,emp.emp_codigo,strpolve)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralm));
                    rep.SetParameters(new ReportParameter("pventa", strpve));
                    rep.SetParameters(new ReportParameter("usuario", usr));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;
                case "VENSOC"://Reporte de Cobro por Socio

                    string almacens = parameters[2].ToString();
                    string pventas = parameters[3].ToString();
                    string stralms = parameters[4].ToString();
                    string strpves = parameters[5].ToString();
                    string usrs = parameters[6].ToString();


                    int? alms = null;
                    int? pves = null;
                    if (!string.IsNullOrEmpty(almacens))
                        alms = int.Parse(almacens);
                    if (!string.IsNullOrEmpty(pventas))
                        pves = int.Parse(pventas);


                    rep.ReportPath = reportfolder + "VentasSocio.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasSocio(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), alms, pves,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralms));
                    rep.SetParameters(new ReportParameter("pventa", strpves));
                    rep.SetParameters(new ReportParameter("usuario", usrs));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;

                case "VENSRI"://Reporte de Cobro por Socio

                    string almacensri = parameters[2].ToString();
                    string pventasri = parameters[3].ToString();
                    //int alm = int.Parse(almacensri);
                    //int pve = int.Parse(pventasri);


                    int? almsri = null;
                    int? pvesri = null;
                    if (!string.IsNullOrEmpty(almacensri))
                        almsri = int.Parse(almacensri);
                    if (!string.IsNullOrEmpty(pventasri))
                        pvesri = int.Parse(pventasri);
                    
                    
                    rep.ReportPath = reportfolder + "ReporteVentasSri.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasSri(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almsri, pvesri ,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", almacensri));
                 //   rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("pventa", pventasri));
                    rep.SetParameters(new ReportParameter("usuario", "USR"));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;


                case "VENCB"://Reporte de Cobro por Socio

                    string almacenb = parameters[2].ToString();
                    string pventab = parameters[3].ToString();
                     string stralmb = parameters[4].ToString();
                    string strpveb = parameters[5].ToString();
                    string usrb = parameters[6].ToString();
                    int? almb = null;
                    int? pveb = null;
                    if (!string.IsNullOrEmpty(almacenb))
                        almb = int.Parse(almacenb);
                    if (!string.IsNullOrEmpty(pventab))
                        pveb = int.Parse(pventab);
                   
                    rep.ReportPath = reportfolder + "ReporteCompraBienes.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasBienes(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almb, pveb,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    //rep.SetParameters(new ReportParameter("almacen", parameters[2].ToString()));
                    //rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", usrb));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;


                case "VENRET"://Reporte de Cobro por Socio

                    string almacenr = parameters[2].ToString();
                    string pventar = parameters[3].ToString();
                    string stralmr = parameters[4].ToString();
                    string strpver = parameters[5].ToString();
                    string usrr = parameters[6].ToString();
                    int? almr = null;
                    int? pver = null;
                    if (!string.IsNullOrEmpty(almacenr))
                        almr = int.Parse(almacenr);
                    if (!string.IsNullOrEmpty(pventar))
                        pver = int.Parse(pventar);

                    //rep.ReportPath = reportfolder + "ReporteRetenciones.rdlc";
                    rep.ReportPath = reportfolder + "Retenciones.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getReporteRetenciones(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almr, pver, null,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("usuario", usrr));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;
                //case "FAC":
                //    rep.ReportPath = reportfolder + "Factura.rdlc";
                //    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.FAC.getFacturaDataTable(long.Parse(parameters[0].ToString()))));
                //    break;
                //case "RUT":
                //    rep.ReportPath = reportfolder + "Report1.rdlc";
                //    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getHojaRutaDataTable(long.Parse(parameters[0].ToString()))));
                //    break;
                //case "NOTCRE":
                //    rep.ReportPath = reportfolder + "Notacre.rdlc";
                //    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getNotaCreditoDataTable(long.Parse(parameters[0].ToString()))));
                //    break;
                //case "LPER":
                //    rep.ReportPath = reportfolder + "Personas.rdlc";
                //    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getPersonas("RUC")));
                //    break;

                case "NOTCRE"://Reporte de Cobro por Socio

                    string almacencre = parameters[2].ToString();
                    string pventacre = parameters[3].ToString();
                    string impuestocre = parameters[7].ToString();
                    int? almcre = null; //int.Parse(almacencre);
                    int? pvecre = null;//int.Parse(pventacre);
                    int? impcre = null;//int.Parse(impuesto);
                    //rep.ReportPath = reportfolder + "ReporteRetenciones.rdlc";
                    rep.ReportPath = reportfolder + "ReporteNcredito.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getNotasCredito(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almcre, pvecre,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    // rep.SetParameters(new ReportParameter("impuesto", parameters[2].ToString()));
                    //rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", "USR"));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;


                case "REANU"://Reporte de Cobro por Socio

                    string almacenanu = parameters[2].ToString();
                    string pventanu = parameters[3].ToString();
                    string impuestoanu = parameters[7].ToString();
                    int? almanu = null; //int.Parse(almacencre);
                    int? pveanu = null;//int.Parse(pventacre);
                    int? impanu = null;//int.Parse(impuesto);
                    //rep.ReportPath = reportfolder + "ReporteRetenciones.rdlc";
                    rep.ReportPath = reportfolder + "ReporteAnuladas.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getAnuladas(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almanu, pveanu,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    // rep.SetParameters(new ReportParameter("impuesto", parameters[2].ToString()));
                    //rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", "USR"));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;
                case "VENTASO"://Reporte de Cobro por Socio

                    string almacenso = parameters[2].ToString();
                    string pventaso = parameters[3].ToString();
                    string stralmso = parameters[4].ToString();
                    string strpveso = parameters[5].ToString();
                    string usrso = parameters[6].ToString();
                    string politicaso = parameters[7].ToString();

                    int? almso = null; 
                    int? pveso = null;
                    int? polso = null;

                    if (!string.IsNullOrEmpty(almacenso))
                        almso = int.Parse(almacenso);
                    if (!string.IsNullOrEmpty(pventaso))
                        pveso = int.Parse(pventaso);
                    if (!string.IsNullOrEmpty(politicaso))
                        polso = int.Parse(politicaso);
                   
                   

                    rep.ReportPath = reportfolder + "ReporteVentasSocios.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasSocios(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almso, pveso, polso, usrso,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralmso));
                    if (usrso == "")
                        usrso = "TODOS";
                    rep.SetParameters(new ReportParameter("pventa", strpveso));
                    // rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", usrso));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;


             
                case "RETP"://Reporte tipos de pago

                    string almacentp = parameters[2].ToString();
                    string pventatp = parameters[3].ToString();
                    string stralmtp = parameters[4].ToString();
                    string strpvetp = parameters[5].ToString();
                    string tp = parameters[7].ToString();
                    string usrtp = parameters[6].ToString();
                    int? tpi = null;
                    int? almtp = null;
                    int? pvetp = null;
                    if (!string.IsNullOrEmpty(almacentp))
                        almtp = int.Parse(almacentp);
                    if (pventatp != "null")
                        pvetp = int.Parse(pventatp);


                    if (!string.IsNullOrEmpty(tp))
                        tpi = int.Parse(tp);

                    rep.ReportPath = reportfolder + "ReporteTpagos.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasTiposPagos(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almtp, pvetp, tpi,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    //rep.SetParameters(new ReportParameter("almacen", stralmtp));
                    //rep.SetParameters(new ReportParameter("pventa", strpvetp));
                    // rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", usrtp));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;
                case "COMPRET"://Reporte de Compras con Retencion

                    string almacencor = parameters[2].ToString();
                    string pventacor = parameters[3].ToString();
                    string stralcor = parameters[4].ToString();
                    string strpvcor = parameters[5].ToString();
                    string usrcor = parameters[6].ToString();
                    int? almcor = null;
                    int? pvecor = null;
                    bool ret = false;
                    bool.TryParse(parameters[7].ToString(), out ret);                    

                    if (ret)
                    {
                        rep.ReportPath = reportfolder + "ReporteComprarsRetenciones.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getComprasRetencioines(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almcor, pvecor, true,emp.emp_codigo)));
                    }else
                    {                    
                        rep.ReportPath = reportfolder + "ComprasSinRetenciones.rdlc";
                        rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getComprasRetencioines(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almcor, pvecor, false,emp.emp_codigo)));
                    }

                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    //  rep.SetParameters(new ReportParameter("almacen", stralcor));
                    // rep.SetParameters(new ReportParameter("pventa", strpvcor));
                    // rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", usrcor));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;
                case "EGRESOBAN"://Reporte de Cobro por Socio

                    string almaceneb = parameters[2].ToString();
                    string pventaeb = parameters[3].ToString();
                    string stralmeb = parameters[4].ToString();
                    string strpveeb = parameters[5].ToString();
                    string usreb = parameters[6].ToString();

                    int? almveb = null;
                    int? pveveb = null;
                    if (almaceneb != "")
                        almveb = int.Parse(almaceneb);
                    if (pventaeb != "null")
                        pveveb = int.Parse(pventaeb);

                    rep.ReportPath = reportfolder + "ReporteEgresoBanco.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getEgresoBanco(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almveb, pveveb,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    //  rep.SetParameters(new ReportParameter("almacen", stralmeb));
                    // rep.SetParameters(new ReportParameter("pventa", strpveeb));
                    rep.SetParameters(new ReportParameter("usuario", usreb));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;



                case "COSO"://Reporte de Cobro por Socio

                    string almacenco = parameters[2].ToString();
                    string pventaco = parameters[3].ToString();
                    string stralmco = parameters[4].ToString();
                    string strpveco = parameters[5].ToString();
                 //   string usrco = parameters[6].ToString();

                    rep.ReportPath = reportfolder + "CobroSocios.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getCobroSocio(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), int.Parse(almacenco), int.Parse(pventaco))));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralmco));
                    rep.SetParameters(new ReportParameter("pventa", strpveco));
                  //  rep.SetParameters(new ReportParameter("usuario", usrco));

                    break;

                case "VENSOTIPODET":
                case "VENSOVEH":
                case "VENSOSALDO"://Reporte de ventas socios pero filtrado solo facturas con saldo por cobrar
                case "VENSO"://Reporte de Cobro por Socio

                    string sociov = parameters[2].ToString();
                    string periodov = parameters[0].ToString();
                    string mesv = parameters[1].ToString();
                    string usuario = parameters[3].ToString();
                    string alamcen = parameters[4].ToString();
                    string pventav  = parameters[5].ToString();
                    string alamcens = parameters[6].ToString();
                    string pventavs  = parameters[7].ToString();
                     if (parameters[4].ToString() == "")
                    {
                        alamcens = "TODOS";


                    }
                     if (parameters[5].ToString() == "")
                     {
                         pventavs = "TODOS";
                     }
                    int? almacenc = null;
                    int? pventac = null;
                    if (alamcen != "")
                        almacenc = int.Parse(alamcen);
                    if (pventav != "")
                        pventac = int.Parse(pventav);
                    int codsocv = -1;
                    int.TryParse(sociov, out codsocv);
                    int periodo;
                    int mes;
                    int.TryParse(periodov, out periodo);
                    int.TryParse(mesv, out mes);
                    if (reportcode== "VENSO" || reportcode == "VENSOSALDO")
                        rep.ReportPath = reportfolder + "VentasSociosTotales.rdlc";
                    if (reportcode == "VENSOVEH")
                        rep.ReportPath = reportfolder + "VentasSociosVehiculo.rdlc";
                    if (reportcode == "VENSOTIPODET")
                        rep.ReportPath = reportfolder + "CobroSocioTipoDet.rdlc";


                    var ds = Packages.General.ReporteVentasSocios(((int?)periodo), ((int?)mes), (codsocv == 0 ? null : (int?)codsocv), almacenc, pventac, emp.emp_codigo);
                    if (reportcode == "VENSOSALDO")
                        ds = ds.Where(w => (w.total - w.ddo_cancela) > 0).ToList();


                    rep.DataSources.Add(new ReportDataSource("DataSet1", ds));
                    rep.SetParameters(new ReportParameter("periodo", periodov));
                    rep.SetParameters(new ReportParameter("mes", mesv));
                    rep.SetParameters(new ReportParameter("usuario", usuario));
                    rep.SetParameters(new ReportParameter("almacen", alamcens));
                    rep.SetParameters(new ReportParameter("pventa", pventavs));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;

                case "VENSOCHOTIPO":
                case "VENSOTIPO":
               case "VENSOT"://Reporte de Cobro por Socio

                    string sociovt = parameters[2].ToString();
                    string periodovt = parameters[0].ToString();
                    string mesvt = parameters[1].ToString();
                    string usuariot = parameters[3].ToString();
                    string alamcent = parameters[4].ToString();
                    string pventavt  = parameters[5].ToString();
                    string alamcenst = parameters[6].ToString();
                    string pventavst  = parameters[7].ToString();
                    if (parameters[4].ToString() == "")
                    {
                        alamcenst = "TODOS";


                    }
                    if (parameters[5].ToString() == "")
                    
                    {
                        pventavst = "TODOS";


                    }
                    int? almacenct = null;
                    int? pventact = null;
                    if (alamcent != "")
                        almacenct = int.Parse(alamcent);
                    if (pventavt != "")
                        pventact = int.Parse(pventavt);
                    int codsocvt = -1;
                    int.TryParse(sociovt, out codsocvt);
                    int periodot;
                    int mest;
                    int.TryParse(periodovt, out periodot);
                    int.TryParse(mesvt, out mest);
                    if (reportcode == "VENSOT")
                        rep.ReportPath = reportfolder + "CobroSocios.rdlc";
                    if (reportcode == "VENSOTIPO")
                        rep.ReportPath = reportfolder + "CobroSocioTipo.rdlc";
                    if (reportcode == "VENSOCHOTIPO")
                        rep.ReportPath = reportfolder + "CobroSocioChoTipo.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.ReporteVentasSocios(((int?)periodot), ((int?)mest), (codsocvt == 0 ? null : (int?)codsocvt), almacenct, pventact,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("periodo", periodovt));
                    rep.SetParameters(new ReportParameter("mes", mesvt));
                    rep.SetParameters(new ReportParameter("usuario", usuariot));
                    rep.SetParameters(new ReportParameter("almacen", alamcenst));
                    rep.SetParameters(new ReportParameter("pventa", pventavst));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;
               case "MAY"://Reporte de MAYOR

                    DateTime desde = DateTime.Parse(parameters[0].ToString());
                    DateTime hasta =DateTime.Parse(parameters[1].ToString()).AddDays(1).Subtract(new TimeSpan(0,0,1));

                    string almacenmay = parameters[2].ToString();
                    string debcremay = parameters[3].ToString();
                 //   string empresa= parameters[4].ToString();
                    string cuentamay = parameters[5].ToString();
                    string clientemay = parameters[6].ToString();
                    string strcuentamay = parameters[7].ToString();

                    int? almacenM = null;
                    int? cuentaM = null;
                    int? clienteM = null;
                    int? debcreM = null;
                    if (almacenmay != "")
                        almacenM = int.Parse(almacenmay);
                    if (cuentamay != "")
                        cuentaM = int.Parse(cuentamay);
                    if (clientemay != "")
                        clienteM = int.Parse(clientemay);
                    if (debcremay != "")
                        debcreM = int.Parse(debcremay);


                    string strinicial = parameters[8].ToString();
                    string strfinal= parameters[9].ToString();

                    rep.ReportPath = reportfolder + "Mayor.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.CNT.getMayor(emp.emp_codigo, almacenM, cuentaM, clienteM, debcreM, desde, hasta)));
                    
                    rep.SetParameters(new ReportParameter("desde", desde.ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", hasta.ToShortDateString()));

                    rep.SetParameters(new ReportParameter("cuenta", strcuentamay));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    rep.SetParameters(new ReportParameter("inicial", strinicial));
                    rep.SetParameters(new ReportParameter("final", strfinal));
                    /*rep.SetParameters(new ReportParameter("mes", mesvt));
                    rep.SetParameters(new ReportParameter("usuario", usuariot));
                    rep.SetParameters(new ReportParameter("almacen", alamcenst));
                    rep.SetParameters(new ReportParameter("pventa", pventavst));*/
                    break;
                case "PLANCTA":
               case "BAL"://BALANCE O ESTADO DE RESULTADOS


                    DateTime? fechabal = Functions.Conversiones.ObjectToDateTimeNull(parameters[0].ToString());
                    if (!fechabal.HasValue)
                        fechabal = DateTime.Now;

                    DateTime fechadesde = new DateTime(fechabal.Value.Year, fechabal.Value.Month, 1); 
                    string almacenbal = parameters[1].ToString();
                    string debcrebal = parameters[2].ToString();
                    string empresabal = parameters[3].ToString();
                    string tipobal = parameters[4].ToString();
                    string todasbal = parameters[5].ToString();

                    if (tipobal == "a")
                        fechadesde = new DateTime(fechabal.Value.Year, 1, 1);
                    string saldos = parameters[6].ToString();
                    string sal = parameters[7].ToString();

                    int? almacenB = null;
                    int? debcreB = null;
                    if (almacenbal != "")
                        almacenB = int.Parse(almacenbal);
                    if (debcrebal != "")
                        debcreB = int.Parse(debcrebal);
                    bool allB = false;
                    bool.TryParse(todasbal, out allB);

                    bool salB = false;
                    bool.TryParse(sal, out salB);

                    List<BusinessObjects.Cuenta> cuentas = Packages.CNT.getBalanceNew(emp.emp_codigo, almacenB, null, null, fechabal.Value, tipobal, 3, allB,salB);

                    decimal ctotal = 0;
                    foreach (BusinessObjects.Cuenta item in cuentas)
                    {
                        if (debcreB == 2)//ESTADO DE RESULTADOS
                        {
                           
                            if (item.cue_genero > 3 && item.cue_genero < 8)
                            {

                                if (item.cue_movimiento == 1)
                                    ctotal += item.final;
                            }
                        }
                        if (debcreB == 1)//BALANCE GENERAL
                        {
                            if (item.cue_genero > 0 && item.cue_genero < 4)
                            {

                                if (item.cue_movimiento == 1)
                                    ctotal += item.final;
                            }

                        }
                        if (debcreB == 3)//TODAS
                        {
                            

                                if (item.cue_movimiento == 1)
                                    ctotal += item.final;                            

                        }

                    }
                    if (saldos =="0")
                        rep.ReportPath = reportfolder + "Balance.rdlc";
                    else
                        rep.ReportPath = reportfolder + "BalanceSaldos.rdlc";

                    rep.DataSources.Add(new ReportDataSource("DataSet1", cuentas));

                    if (reportcode == "PLANCTA")
                    {
                        rep.ReportPath = reportfolder + "Cuentas.rdlc";
                        rep.SetParameters(new ReportParameter("fecha", fechabal.Value.ToShortDateString()));
                        rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    }
                    else

                    {

                        rep.SetParameters(new ReportParameter("fecha", fechabal.Value.ToShortDateString()));
                        rep.SetParameters(new ReportParameter("debcre", debcrebal));
                        rep.SetParameters(new ReportParameter("tipo", tipobal));
                        rep.SetParameters(new ReportParameter("usuario", "admin"));
                        rep.SetParameters(new ReportParameter("total", ctotal.ToString()));
                        rep.SetParameters(new ReportParameter("desde", fechadesde.ToShortDateString()));
                        rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    }
                    /*rep.SetParameters(new ReportParameter("mes", mesvt));
                    rep.SetParameters(new ReportParameter("usuario", usuariot));
                    rep.SetParameters(new ReportParameter("almacen", alamcenst));
                    rep.SetParameters(new ReportParameter("pventa", pventavst));*/
                    break;
               case "VENSOHOJ"://Reporte de Cobro por Socio

                    string sociovth = parameters[2].ToString();
                    string periodovth = parameters[0].ToString();
                    string mesvth = parameters[1].ToString();
                    string usuarioth = parameters[3].ToString();
                    string alamcenth = parameters[4].ToString();
                    string pventavth = parameters[5].ToString();
                    string alamcensth = parameters[6].ToString();
                    string pventavsth = parameters[7].ToString();
                    if (parameters[4].ToString() == "")
                    {
                        alamcensth = "TODOS";


                    }
                    if (parameters[5].ToString() == "")
                    {
                        pventavsth = "TODOS";


                    }
                    int? almacencth = null;
                    int? pventacth = null;
                    if (alamcenth != "")
                        almacencth = int.Parse(alamcenth);
                    if (pventavth != "")
                        pventacth = int.Parse(pventavth);
                    int codsocvth = -1;
                    int.TryParse(sociovth, out codsocvth);


                    string rutath = parameters[8].ToString();
                    string nomrutath = parameters[9].ToString();
                    int? rutth = null;
                    if (!string.IsNullOrEmpty(rutath))
                        rutth= int.Parse(rutath);


                    rep.ReportPath = reportfolder + "VentasSociosHojas.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasSociosHojas(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almacencth, pventacth, (codsocvth == 0 ? null : (int?)codsocvth),emp.emp_codigo,rutth)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));

                    rep.SetParameters(new ReportParameter("almacen", alamcensth));
                    if (sociovth == "")
                        usrso = "TODOS";
                    rep.SetParameters(new ReportParameter("pventa", pventavsth));
                    // rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", usuarioth));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;



                case "VENSODHORD":
               case "VENSODH"://Reporte de Cobro por Socio

                    string sociodh = parameters[2].ToString();
                    string periodovdh = parameters[0].ToString();
                    string mesvdh = parameters[1].ToString();
                    string usuariodh = parameters[3].ToString();
                    string alamcendh = parameters[4].ToString();
                    string pventavdh = parameters[5].ToString();
                    string alamcensdh = parameters[6].ToString();
                    string pventavsdh = parameters[7].ToString();
                    if (parameters[4].ToString() == "")
                    {
                        alamcensdh = "TODOS";


                    }
                    if (parameters[5].ToString() == "")
                    {
                        pventavsdh = "TODOS";


                    }
                    int? almacencdh = null;
                    int? pventacdh = null;
                    if (alamcendh != "")
                        almacencdh = int.Parse(alamcendh);
                    if (pventavdh != "")
                        pventacdh = int.Parse(pventavdh);
                    int codsocvdh = -1;
                    int.TryParse(sociodh, out codsocvdh);

                    string rutadh = parameters[8].ToString();
                    string nomrutadh = parameters[9].ToString();
                    int? rutdh = null;
                    if (!string.IsNullOrEmpty(rutadh))
                        rutdh = int.Parse(rutadh);

                    if (reportcode== "VENSODH")

                    rep.ReportPath = reportfolder + "VentasSociosHojasDetallado.rdlc";
                    else if (reportcode == "VENSODHORD")
                        rep.ReportPath = reportfolder + "VentasSociosHojasCOMY.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasSociosHojas(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almacencdh, pventacdh, (codsocvdh == 0 ? null : (int?)codsocvdh), emp.emp_codigo, rutdh)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", alamcensdh));
                    if (sociodh == "")
                        usrso = "TODOS";
                    rep.SetParameters(new ReportParameter("pventa", pventavsdh));
                    // rep.SetParameters(new ReportParameter("anuladas", "0"));
                    rep.SetParameters(new ReportParameter("usuario", usuariodh));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;


               case "CUADREFAC"://Reporte de Cobro por Socio

                    string tipodocf = parameters[2].ToString();
                    string periodovf = parameters[0].ToString();
                    string mesvf = parameters[1].ToString();
                    string usuariof = parameters[3].ToString();
                    string alamcenf = parameters[4].ToString();
                    string pventavf = parameters[5].ToString();
                    string alamcensf = parameters[6].ToString();
                    string pventavsf = parameters[7].ToString();
                    if (parameters[4].ToString() == "")
                    {
                        alamcensf = "TODOS";


                    }
                    if (parameters[5].ToString() == "")
                    {
                        pventavsf = "TODOS";
                    }
                    int? almacencf = null;
                    int? pventacf = null;
                    if (alamcenf != "")
                        almacencf = int.Parse(alamcenf);
                    if (pventavf != "")
                        pventacf = int.Parse(pventavf);
                    int tipodocvf = -1;
                    int.TryParse(tipodocf, out tipodocvf);
                    int periodof;
                    int mesf;
                    int.TryParse(periodovf, out periodof);
                    int.TryParse(mesvf, out mesf);

                    rep.ReportPath = reportfolder + "ReporteDescuadres.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.ReporteComprobantesDescuadrados(((int?)periodof), ((int?)mesf), (tipodocvf == 0 ? null : (int?)tipodocvf), almacencf, pventacf,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("periodo", periodovf));
                    rep.SetParameters(new ReportParameter("mes", mesvf));
                    rep.SetParameters(new ReportParameter("usuario", usuariof));
                    rep.SetParameters(new ReportParameter("almacen", alamcensf));
                    rep.SetParameters(new ReportParameter("pventa", pventavsf));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;



               case "CUADRECON"://Reporte de Cobro por Socio

                    string tipodocc = parameters[2].ToString();
                    string periodovc = parameters[0].ToString();
                    string mesvc = parameters[1].ToString();
                    string usuarioc = parameters[3].ToString();
                    string alamcenc = parameters[4].ToString();
                    string pventavc = parameters[5].ToString();
                    string alamcensc = parameters[6].ToString();
                    string pventavsc = parameters[7].ToString();
                    if (parameters[4].ToString() == "")
                    {
                        alamcensc = "TODOS";


                    }
                    if (parameters[5].ToString() == "")
                    {
                        pventavsc = "TODOS";
                    }
                    int? almacencc = null;
                    int? pventacc = null;
                    if (alamcenc != "")
                        almacencc = int.Parse(alamcenc);
                    if (pventavc != "")
                        pventacc= int.Parse(pventavc);
                    int tipodoccv = -1;
                    int.TryParse(tipodocc, out tipodoccv);
                    int periodoc;
                    int mesc;
                    int.TryParse(periodovc, out periodoc);
                    int.TryParse(mesvc, out mesc);

                    rep.ReportPath = reportfolder + "ReporteDescuadresComprobantes.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.ReporteComprobantesDescuadradosC(((int?)periodoc), ((int?)mesc), (tipodoccv == 0 ? null : (int?)tipodoccv), almacencc, pventacc,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("periodo", periodovc));
                    rep.SetParameters(new ReportParameter("mes", mesvc));
                    rep.SetParameters(new ReportParameter("usuario", usuarioc));
                    rep.SetParameters(new ReportParameter("almacen", alamcensc));
                    rep.SetParameters(new ReportParameter("pventa", pventavsc));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    break;
               case "VENTARETE"://Reporte de Cobro por Socio

                    string almacenret = parameters[2].ToString();
                    string pventaret = parameters[3].ToString();
                    string stralmret = parameters[4].ToString();
                    string strpveret = parameters[5].ToString();
                    string usrret = parameters[6].ToString();

                    int? almvret = null;
                    int? pvevret = null;
                    if (!string.IsNullOrEmpty(almacenret))
                        almv1 = int.Parse(almacenret);
                    if (!string.IsNullOrEmpty(pventaret))
                        pvev1 = int.Parse(pventaret);

                    rep.ReportPath = reportfolder + "VentasRetenciones.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasRetenciones(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almvret, pvevret,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralmret));
                    rep.SetParameters(new ReportParameter("pventa", strpveret));
                    rep.SetParameters(new ReportParameter("usuario", usrret));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;

               case "VENTARETECON"://Reporte de Cobro por Socio

                    string almacenretc = parameters[2].ToString();
                    string pventaretc = parameters[3].ToString();
                    string stralmretc = parameters[4].ToString();
                    string strpveretc = parameters[5].ToString();
                    string usrretc = parameters[6].ToString();

                    int? almvretc = null;
                    int? pvevretc = null;
                    if (!string.IsNullOrEmpty(almacenretc))
                        almv1 = int.Parse(almacenretc);
                    if (!string.IsNullOrEmpty(pventaretc))
                        pvev1 = int.Parse(pventaretc);

                    rep.ReportPath = reportfolder + "VentasRetencionesConsolidado.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.getVentasRetenciones(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), almvretc, pvevretc,emp.emp_codigo)));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralmretc));
                    rep.SetParameters(new ReportParameter("pventa", strpveretc));
                    rep.SetParameters(new ReportParameter("usuario", usrretc));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));

                    break;
                case "PLANILLASCLIENTES":
                    string almacen_plc = parameters[2].ToString();
                    string pventa_plc = parameters[3].ToString();
                    string stralm_plc= parameters[4].ToString();
                    string strpve_plc = parameters[5].ToString();
                    string usr_plc = parameters[6].ToString();
                    string cliente_plc = parameters[7].ToString();

                    int? alm_plc = null;
                    int? pve_plc = null;
                    if (!string.IsNullOrEmpty(almacen_plc))
                        almv1 = int.Parse(almacen_plc);
                    if (!string.IsNullOrEmpty(pventa_plc))
                        pvev1 = int.Parse(pventa_plc);

                    //rep.ReportPath = reportfolder + "PlanillasClientes.rdlc";
                    //rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetComprobantesPlanillaCliente(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), alm_plc, pve_plc, emp.emp_codigo, cliente_plc)));

                    List<string> cuentasPLC = new List<string>();

                    rep.ReportPath = reportfolder + "PlanillasClientesDet.rdlc";
                    rep.DataSources.Add(new ReportDataSource("DataSet1", Packages.General.GetPlanillasClientesDet(DateTime.Parse(parameters[0].ToString()), DateTime.Parse(parameters[1].ToString()), alm_plc, pve_plc, emp.emp_codigo, cliente_plc, out cuentasPLC)));
                    //List<Cuenta> lstcuentasPLC = CuentaBLL.GetAll("cue_codigo in (" + string.Join(",", cuentasPLC) + ")", "");
                    //rep.DataSources.Add(new ReportDataSource("DataSet2", lstcuentasPLC));
                    rep.SetParameters(new ReportParameter("desde", DateTime.Parse(parameters[0].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("hasta", DateTime.Parse(parameters[1].ToString()).ToShortDateString()));
                    rep.SetParameters(new ReportParameter("almacen", stralm_plc));
                    rep.SetParameters(new ReportParameter("pventa", strpve_plc));
                    rep.SetParameters(new ReportParameter("usuario", usr_plc));
                    rep.SetParameters(new ReportParameter("empresa", emp.emp_nombre));
                    rep.SetParameters(new ReportParameter("cliente", cliente_plc));

                    rep.SetParameters(new ReportParameter("cancela1", cuentasPLC[0]));
                    if (cuentasPLC.Count > 1)
                        rep.SetParameters(new ReportParameter("cancela2", cuentasPLC[1]));
                    if (cuentasPLC.Count > 2)
                        rep.SetParameters(new ReportParameter("cancela3", cuentasPLC[2]));
                    if (cuentasPLC.Count > 3)
                        rep.SetParameters(new ReportParameter("cancela4", cuentasPLC[3]));
                    if (cuentasPLC.Count > 4)
                        rep.SetParameters(new ReportParameter("cancela5", cuentasPLC[4]));
                    if (cuentasPLC.Count > 5)
                        rep.SetParameters(new ReportParameter("cancela6", cuentasPLC[5]));
                    if (cuentasPLC.Count > 6)
                        rep.SetParameters(new ReportParameter("cancela7", cuentasPLC[6]));
                    if (cuentasPLC.Count > 7)
                        rep.SetParameters(new ReportParameter("cancela8", cuentasPLC[7]));
                    if (cuentasPLC.Count > 8)
                        rep.SetParameters(new ReportParameter("cancela9", cuentasPLC[8]));
                    if (cuentasPLC.Count > 9)
                        rep.SetParameters(new ReportParameter("cancela10", "Otras.."));
                    break;
            }
            }



        }
    }


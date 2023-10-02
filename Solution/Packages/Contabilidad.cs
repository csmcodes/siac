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
using System.Collections;


namespace Packages
{
    public class Contabilidad
    {
        #region Id Cuenta



        public static string GetIdCuenta(Cuenta padre)
        {
            Empresa emp = EmpresaBLL.GetByPK(new Empresa { emp_codigo = padre.cue_empresa, emp_codigo_key = padre.cue_empresa });
            EmpresaOpciones empopciones = new JavaScriptSerializer().Deserialize<EmpresaOpciones>(emp.emp_opciones);

            string formato = "";
            if (empopciones != null)
            {
                foreach (FormatoCuenta item in empopciones.formatocuenta)
                {
                    if (item.nivel == (padre.cue_nivel + 1))
                        formato = item.formato;
                }
            }

            List<Cuenta> lst = CuentaBLL.GetAll("cue_empresa=" + padre.cue_empresa + " and cue_reporta=" + padre.cue_codigo, "cue_orden");
            int numero = lst.Count() + 1;
            string id = padre.cue_id + numero.ToString(formato);
            bool existe = true;
            do
            {
                existe = false;
                foreach (Cuenta item in lst)
                {
                    if (item.cue_id == id)
                        existe = true;
                }
                if (existe)
                {
                    numero++;
                    id = padre.cue_id + numero.ToString(formato);
                }

            } while (existe);

            return id;






        }

        #endregion

        #region Periodos Contables

        public static List<PeriodoContable> GetPeriodosContables(int periodo, int mes, string user)
        {
            List<PeriodoContable> lst = new List<PeriodoContable>();
            string parperiodos = Constantes.GetParameter("periodos");
            if (!string.IsNullOrEmpty(parperiodos))
            {
                var serializer = new JavaScriptSerializer();
                lst = serializer.Deserialize<List<PeriodoContable>>(parperiodos);
                PeriodoContable pc = lst.Find(delegate (PeriodoContable p) { return p.periodo == periodo && p.mes == mes; });
                if (pc == null)
                {
                    pc = new PeriodoContable();
                    pc.periodo = periodo;
                    pc.mes = mes;
                    pc.estado = "open";
                    pc.audit = string.Format("{0:dd/MM/yyyy HH:mm:ss},{1},{2}", DateTime.Now, user, "open");
                    lst.Add(pc);
                    SavePeriodos(lst);
                }
            }
            else
            {
                PeriodoContable pc = new PeriodoContable();
                pc.periodo = periodo;
                pc.mes = mes;
                pc.estado = "open";
                pc.audit = string.Format("{0:dd/MM/yyyy HH:mm:ss},{1},{2}", DateTime.Now, user, "open");
                lst.Add(pc);
                SavePeriodos(lst);

            }
            return lst;
        }

        public static void OpenPeriodo(int periodo, int mes, string user)
        {
            List<PeriodoContable> lst = GetPeriodosContables(periodo, mes, user);
            PeriodoContable pc = lst.Find(delegate (PeriodoContable p) { return p.periodo == periodo && p.mes == mes; });
            pc.estado = "open";
            pc.audit += string.Format("|{0:dd/MM/yyyy HH:mm:ss},{1},{2}", DateTime.Now, user, "open");
            SavePeriodos(lst);
        }

        public static void ClosePeriodo(int periodo, int mes, string user)
        {
            List<PeriodoContable> lst = GetPeriodosContables(periodo, mes, user);
            PeriodoContable pc = lst.Find(delegate (PeriodoContable p) { return p.periodo == periodo && p.mes == mes; });
            pc.estado = "close";
            pc.audit += string.Format("|{0:dd/MM/yyyy HH:mm:ss},{1},{2}", DateTime.Now, user, "close");
            SavePeriodos(lst);
        }

        public static bool PeriodoIsOpen(int periodo, int mes, string user)
        {
            List<PeriodoContable> lst = GetPeriodosContables(periodo, mes, user);
            PeriodoContable pc = lst.Find(delegate (PeriodoContable p) { return p.periodo == periodo && p.mes == mes; });
            return pc.estado == "open";
        }

        public static void SavePeriodos(List<PeriodoContable> lst)
        {

            Parametro par = ParametroBLL.GetByPK(new Parametro { par_nombre = "periodos", par_nombre_key = "periodos" });
            par.par_nombre_key = "periodos";
            par.par_valor = new JavaScriptSerializer().Serialize(lst.OrderBy(o => o.periodo).ThenBy(m => m.mes).ToList());
            ParametroBLL.Update(par);
        }


        #endregion
    }
}

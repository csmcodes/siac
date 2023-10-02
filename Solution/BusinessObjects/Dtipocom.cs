using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
    public class Dtipocom
    {
        #region Properties

        [Data(key = true)]
        public int dti_periodo { get; set; }           
        [Data(key = true)]
        public int dti_empresa { get; set; }
        [Data(key = true)]
        public int dti_ctipocom { get; set; }       
        [Data(key = true)]
        public int dti_almacen { get; set; }        
        [Data(key = true)]
        public int dti_puntoventa { get; set; }
        [Data(originalkey = true)]       
        public int dti_periodo_key { get; set; }
        [Data(originalkey = true)]
        public int dti_empresa_key { get; set; }
        [Data(originalkey = true)]
        public int dti_ctipocom_key { get; set; }
        [Data(originalkey = true)]
        public int dti_almacen_key { get; set; }
        [Data(originalkey = true)]
        public int dti_puntoventa_key { get; set; }
        public int? dti_numero{ get; set; }     
        public int? dti_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        


        [Data(nosql = true, tablaref = "almacen", camporef = "alm_id", foreign = "dti_almacen", keyref = "alm_codigo")]
        public string dti_idalmacen { get; set; }
        [Data(nosql = true, tablaref = "almacen", camporef = "alm_nombre", foreign = "dti_almacen", keyref = "alm_codigo")]
        public string dti_nombrealmacen { get; set; }
       [Data(nosql = true, tablaref = "puntoventa", camporef = "pve_id", foreign = "dti_puntoventa ,dti_almacen", keyref = "pve_secuencia,pve_almacen")]
        public string dti_idpuntoventa { get; set; }
       [Data(nosql = true, tablaref = "puntoventa", camporef = "pve_nombre", foreign = "dti_puntoventa ,dti_almacen", keyref = "pve_secuencia,pve_almacen")]
       public string dti_nombrepuntoventa { get; set; }
        
              
        #endregion

        #region Constructors


        public Dtipocom()
        {
        }

        public Dtipocom(int periodo, int empresa, int ctipocom, int almacen, int puntoventa, int? numero, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {
            this.dti_periodo = periodo;
            this.dti_periodo_key = periodo;
            this.dti_empresa = empresa;
            this.dti_empresa_key = empresa;
            this.dti_ctipocom = ctipocom;
            this.dti_ctipocom_key = ctipocom;
            this.dti_almacen = almacen;
            this.dti_almacen_key = almacen;
            this.dti_puntoventa = puntoventa;
            this.dti_puntoventa_key = puntoventa;
            this.dti_numero =numero;
            this.dti_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Dtipocom(IDataReader reader)
        {
            this.dti_periodo = (int)reader["dti_periodo"];
            this.dti_periodo_key = (int)reader["dti_periodo"];
            this.dti_empresa = (int)reader["dti_empresa"];
            this.dti_empresa_key = (int)reader["dti_empresa"];
            this.dti_ctipocom = (int)reader["dti_ctipocom"];
            this.dti_ctipocom_key = (int)reader["dti_ctipocom"];
            this.dti_almacen = (int)reader["dti_almacen"];
            this.dti_almacen_key = (int)reader["dti_almacen"];
            this.dti_puntoventa = (int)reader["dti_puntoventa"];
            this.dti_puntoventa_key = (int)reader["dti_puntoventa"];
            this.dti_numero = (reader["dti_numero"] != DBNull.Value) ? (int?)reader["dti_numero"] : null;            
            this.dti_estado = (reader["dti_estado"] != DBNull.Value) ? (int?)reader["dti_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.dti_idalmacen = reader["dti_idalmacen"].ToString();
            this.dti_idpuntoventa = reader["dti_idpuntoventa"].ToString();
            this.dti_nombrealmacen = reader["dti_nombrealmacen"].ToString();
            this.dti_nombrepuntoventa = reader["dti_nombrepuntoventa"].ToString();
        }




        public Dtipocom(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object dti_empresa = null;
                object dti_periodo = null;
                object dti_ctipocom = null;
                object dti_almacen = null;
                object dti_puntoventa = null;
                object dti_numero = null;
                object dti_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object idalmacen = null;
                object idpuntoventa = null;
                object nombrealmacen= null;
                object nombrepuntoventa = null;

                tmp.TryGetValue("dti_empresa", out dti_empresa);
                tmp.TryGetValue("dti_periodo", out dti_periodo);
                tmp.TryGetValue("dti_ctipocom", out dti_ctipocom);
                tmp.TryGetValue("dti_almacen", out dti_almacen);
                tmp.TryGetValue("dti_puntoventa", out dti_puntoventa);
                tmp.TryGetValue("dti_numero", out dti_numero);
                tmp.TryGetValue("dti_estado", out dti_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("dti_idalmacen", out idalmacen);
                tmp.TryGetValue("dti_idpuntoventa", out idpuntoventa);
                tmp.TryGetValue("dti_nombrealmacen", out nombrealmacen);
                tmp.TryGetValue("dti_nombrepuntoventa", out nombrepuntoventa);

                this.dti_empresa = (Int32)Conversiones.GetValueByType(dti_empresa, typeof(Int32));
                this.dti_periodo = (Int32)Conversiones.GetValueByType(dti_periodo, typeof(Int32));
                this.dti_ctipocom = (Int32)Conversiones.GetValueByType(dti_ctipocom, typeof(Int32));
                this.dti_almacen = (Int32)Conversiones.GetValueByType(dti_almacen, typeof(Int32));
                this.dti_puntoventa = (Int32)Conversiones.GetValueByType(dti_puntoventa, typeof(Int32));
                this.dti_numero = (Int32?)Conversiones.GetValueByType(dti_numero, typeof(Int32?));
                this.dti_estado = (Int32?)Conversiones.GetValueByType(dti_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.dti_idalmacen =  (String)Conversiones.GetValueByType(idalmacen, typeof(String));
                this.dti_idpuntoventa = (String)Conversiones.GetValueByType(idpuntoventa, typeof(String));

                this.dti_nombrealmacen = (String)Conversiones.GetValueByType(nombrealmacen, typeof(String));
                this.dti_nombrepuntoventa = (String)Conversiones.GetValueByType(nombrepuntoventa, typeof(String));
            }
        }
        #endregion

        #region Methods
        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }        
        #endregion


    }
}

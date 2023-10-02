using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;



namespace BusinessObjects
{
    public class Dtipodoc
    {
        #region Properties

       
        [Data(key = true)]
        public int dtp_empresa { get; set; }
        [Data(originalkey = true)]
        public int dtp_empresa_key { get; set; }
        [Data(key = true)]
        public int dtp_ctipocom { get; set; }
        [Data(originalkey = true)]
        public int dtp_ctipocom_key { get; set; }
        [Data(key = true)]
        public int dtp_tipodoc { get; set; }
        [Data(originalkey = true)]
        public int dtp_tipodoc_key { get; set; }

          
        public int? dtp_estado { get; set; }       
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }



        [Data(nosql = true, tablaref = "ctipocom", camporef = "cti_id", foreign = "dtp_ctipocom", keyref = "cti_codigo")]
        public string dtp_ctipocomid { get; set; }
        [Data(nosql = true, tablaref = "ctipocom", camporef = "cti_nombre", foreign = "dtp_ctipocom", keyref = "cti_codigo")]
        public string dtp_ctipocomnombre { get; set; }

        #endregion

        #region Constructors


        public Dtipodoc()
        {
        }

        public Dtipodoc(int empresa, int ctipocom, int dtp_tipodoc, int? estado, string crea_usr, DateTime? crea_fecha, string dtp_usr, DateTime? dtp_fecha)
        {

            this.dtp_empresa = empresa;
            this.dtp_empresa_key = empresa;
            this.dtp_ctipocom = ctipocom;
            this.dtp_ctipocom_key = ctipocom;
            this.dtp_tipodoc = dtp_tipodoc;
            this.dtp_tipodoc_key = dtp_tipodoc;
            this.dtp_estado = estado;   
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
       }

        public Dtipodoc(IDataReader reader)
        {
            this.dtp_empresa = (int)reader["dtp_empresa"];
            this.dtp_empresa_key = (int)reader["dtp_empresa"];
            this.dtp_ctipocom = (int)reader["dtp_ctipocom"];
            this.dtp_ctipocom_key = (int)reader["dtp_ctipocom"];
            this.dtp_tipodoc = (int)reader["dtp_tipodoc"];
            this.dtp_tipodoc_key = (int)reader["dtp_tipodoc"];
            this.dtp_estado = (reader["dtp_estado"] != DBNull.Value) ? (int?)reader["dtp_estado"] : null;             
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.dtp_ctipocomid = reader["dtp_ctipocomid"].ToString();
            this.dtp_ctipocomnombre = reader["dtp_ctipocomnombre"].ToString();
        }


        public Dtipodoc(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object dtp_empresa = null;
                object dtp_ctipocom = null;
                object dtp_tipodoc = null;
                object dtp_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("dtp_empresa", out dtp_empresa);
                tmp.TryGetValue("dtp_ctipocom", out dtp_ctipocom);
                tmp.TryGetValue("dtp_tipodoc", out dtp_tipodoc);
                tmp.TryGetValue("dtp_estado", out dtp_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.dtp_empresa = (Int32)Conversiones.GetValueByType(dtp_empresa, typeof(Int32));
                this.dtp_ctipocom = (Int32)Conversiones.GetValueByType(dtp_ctipocom, typeof(Int32));
                this.dtp_tipodoc = (Int32)Conversiones.GetValueByType(dtp_tipodoc, typeof(Int32));
                this.dtp_estado = (Int32?)Conversiones.GetValueByType(dtp_estado, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));

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

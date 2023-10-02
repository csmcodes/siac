using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;


namespace BusinessObjects
{
    public class Chofer
    {
        #region Properties

        [Data(key = true)]
        public int cho_persona { get; set; }
        [Data(originalkey = true)]
        public int cho_persona_key { get; set; }      
        [Data(key = true)]
        public int cho_empresa { get; set; }
        [Data(originalkey = true)]
        public int cho_empresa_key { get; set; }
        public string cho_nrolicencia{ get; set; }
        public int? cho_puntoslicencia{ get; set; }
        public string cho_tiposangre{ get; set; }
        public DateTime? cho_emisionlicencia{ get; set; }
        public DateTime? cho_renovacion{ get; set; }
        public DateTime? cho_caducidadlicencia{ get; set; }
        public string cho_tipolicencia{ get; set; }
        public int? cho_paisemision{ get; set; }
        public int? cho_provinciaemision{ get; set; }
        public int? cho_cantonemision{ get; set; }
        public int? cho_paisrenovacion{ get; set; }
        public int? cho_provinciarenovacion{ get; set; }
        public int? cho_cantonrenovacion{ get; set; }
        public string cho_observacionlicencia{ get; set; }
        public int? cho_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "cho_persona", keyref = "per_codigo")]
        public string cho_nombrechofer{ get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_apellidos", foreign = "cho_persona", keyref = "per_codigo")]
        public string cho_apellidochofer { get; set; }
        #endregion

        #region Constructors


        public Chofer()
        {

        }

        public Chofer(int codigo,int empresa,string nrolicencia,int? puntoslicencia,string tiposangre, DateTime? emisionlicencia,DateTime? renovacion,DateTime? caducidadlicencia,string tipolicencia,int? paisemision,int? provinciaemision,int? cantonemision,int? paisrenovacion,int? provinciarenovacion,int? cantonrenovacion,string observacionlicencia,int? estado , string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {
            this.cho_persona = codigo;
            this.cho_persona_key = codigo;
            this.cho_empresa = empresa;
            this.cho_persona_key = empresa;
            this.cho_nrolicencia = nrolicencia;
            this.cho_puntoslicencia = puntoslicencia;
            this.cho_tiposangre = tiposangre;
            this.cho_emisionlicencia = emisionlicencia;
            this.cho_renovacion = renovacion;
            this.cho_caducidadlicencia = caducidadlicencia;
            this.cho_tipolicencia = tipolicencia;
            this.cho_paisemision = paisemision;
            this.cho_provinciaemision = provinciaemision;
            this.cho_cantonemision = cantonemision;
            this.cho_paisrenovacion = paisrenovacion;
            this.cho_provinciarenovacion = provinciarenovacion;
            this.cho_cantonrenovacion = cantonrenovacion;
            this.cho_observacionlicencia = observacionlicencia;
            this.cho_estado = estado;
            this.crea_usr=crea_usr;
            this.crea_fecha=crea_fecha;
            this.mod_usr=mod_usr;
            this.mod_fecha = mod_fecha;

        }

        public Chofer(IDataReader reader)
        {
            this.cho_persona = (int)reader["cho_persona"];
            this.cho_persona_key = (int)reader["cho_persona"];
            this.cho_empresa = (int)reader["cho_empresa"];
            this.cho_empresa_key = (int)reader["cho_empresa"];
            this.cho_nrolicencia = reader["cho_nrolicencia"].ToString();
            this.cho_puntoslicencia = (reader["cho_puntoslicencia"] != DBNull.Value) ? (int?)reader["cho_puntoslicencia"] : null;
            this.cho_tiposangre = reader["cho_tiposangre"].ToString();
            this.cho_emisionlicencia = (reader["cho_emisionlicencia"] != DBNull.Value) ? (DateTime?)reader["cho_emisionlicencia"] : null;
            this.cho_renovacion = (reader["cho_renovacion"] != DBNull.Value) ? (DateTime?)reader["cho_renovacion"] : null;
            this.cho_caducidadlicencia = (reader["cho_caducidadlicencia"] != DBNull.Value) ? (DateTime?)reader["cho_caducidadlicencia"] : null;
            this.cho_tipolicencia = reader["cho_tipolicencia"].ToString();
            this.cho_paisemision = (reader["cho_paisemision"] != DBNull.Value) ? (int?)reader["cho_paisemision"] : null;
            this.cho_provinciaemision = (reader["cho_provinciaemision"] != DBNull.Value) ? (int?)reader["cho_provinciaemision"] : null;
            this.cho_cantonemision = (reader["cho_cantonemision"] != DBNull.Value) ? (int?)reader["cho_cantonemision"] : null;
            this.cho_paisrenovacion = (reader["cho_paisrenovacion"] != DBNull.Value) ? (int?)reader["cho_paisrenovacion"] : null;
            this.cho_provinciarenovacion = (reader["cho_provinciarenovacion"] != DBNull.Value) ? (int?)reader["cho_provinciarenovacion"] : null;
            this.cho_cantonrenovacion = (reader["cho_cantonrenovacion"] != DBNull.Value) ? (int?)reader["cho_cantonrenovacion"] : null;
            this.cho_observacionlicencia = reader["cho_observacionlicencia"].ToString();
            this.cho_estado = (reader["cho_estado"] != DBNull.Value) ? (int?)reader["cho_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.cho_nombrechofer = reader["cho_nombrechofer"].ToString();
            this.cho_apellidochofer = reader["cho_apellidochofer"].ToString();
        }
        public Chofer(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object cho_emisionlicencia = null;
                object cho_persona = null;
                object cho_empresa = null;
                object cho_persona_key = null;
                object cho_empresa_key = null;
                object cho_nrolicencia = null;
                object cho_puntoslicencia = null;
                object cho_tiposangre = null;
                object cho_renovacion = null;
                object cho_caducidadlicencia = null;
                object cho_tipolicencia = null;
                object cho_paisemision = null;
                object cho_provinciaemision = null;
                object cho_cantonemision = null;
                object cho_paisrenovacion = null;
                object cho_provinciarenovacion = null;
                object cho_cantonrenovacion = null;
                object cho_observacionlicencia = null;
                object cho_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("cho_emisionlicencia", out cho_emisionlicencia);
                tmp.TryGetValue("cho_persona", out cho_persona);
                tmp.TryGetValue("cho_empresa", out cho_empresa);
                tmp.TryGetValue("cho_persona_key", out cho_persona_key);
                tmp.TryGetValue("cho_empresa_key", out cho_empresa_key);
                tmp.TryGetValue("cho_nrolicencia", out cho_nrolicencia);
                tmp.TryGetValue("cho_puntoslicencia", out cho_puntoslicencia);
                tmp.TryGetValue("cho_tiposangre", out cho_tiposangre);
                tmp.TryGetValue("cho_renovacion", out cho_renovacion);
                tmp.TryGetValue("cho_caducidadlicencia", out cho_caducidadlicencia);
                tmp.TryGetValue("cho_tipolicencia", out cho_tipolicencia);
                tmp.TryGetValue("cho_paisemision", out cho_paisemision);
                tmp.TryGetValue("cho_provinciaemision", out cho_provinciaemision);
                tmp.TryGetValue("cho_cantonemision", out cho_cantonemision);
                tmp.TryGetValue("cho_paisrenovacion", out cho_paisrenovacion);
                tmp.TryGetValue("cho_provinciarenovacion", out cho_provinciarenovacion);
                tmp.TryGetValue("cho_cantonrenovacion", out cho_cantonrenovacion);
                tmp.TryGetValue("cho_observacionlicencia", out cho_observacionlicencia);
                tmp.TryGetValue("cho_estado", out cho_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);

                if (!string.IsNullOrEmpty(cho_emisionlicencia.ToString()))
                {
                    this.cho_emisionlicencia = (DateTime)Conversiones.GetValueByType(cho_emisionlicencia, typeof(DateTime));
                }

                if (!string.IsNullOrEmpty(cho_renovacion.ToString()))
                {
                    this.cho_renovacion = (DateTime?)Conversiones.GetValueByType(cho_renovacion, typeof(DateTime?));
                }
                if (!string.IsNullOrEmpty(cho_caducidadlicencia.ToString()))
                {
                    this.cho_caducidadlicencia = (DateTime?)Conversiones.GetValueByType(cho_caducidadlicencia, typeof(DateTime?));
                }  
                this.cho_persona = (Int32)Conversiones.GetValueByType(cho_persona, typeof(Int32));
                this.cho_empresa = (Int32)Conversiones.GetValueByType(cho_empresa, typeof(Int32));
                this.cho_persona_key = (Int32)Conversiones.GetValueByType(cho_persona_key, typeof(Int32));
                this.cho_empresa_key = (Int32)Conversiones.GetValueByType(cho_empresa_key, typeof(Int32));
                this.cho_nrolicencia = (String)Conversiones.GetValueByType(cho_nrolicencia, typeof(String));
                this.cho_puntoslicencia = (Int32?)Conversiones.GetValueByType(cho_puntoslicencia, typeof(Int32?));
                this.cho_tiposangre = (String)Conversiones.GetValueByType(cho_tiposangre, typeof(String));
                
                
                this.cho_tipolicencia = (String)Conversiones.GetValueByType(cho_tipolicencia, typeof(String));
                this.cho_paisemision = (Int32?)Conversiones.GetValueByType(cho_paisemision, typeof(Int32?));
                this.cho_provinciaemision = (Int32?)Conversiones.GetValueByType(cho_provinciaemision, typeof(Int32?));
                this.cho_cantonemision = (Int32?)Conversiones.GetValueByType(cho_cantonemision, typeof(Int32?));
                this.cho_paisrenovacion = (Int32?)Conversiones.GetValueByType(cho_paisrenovacion, typeof(Int32?));
                this.cho_provinciarenovacion = (Int32?)Conversiones.GetValueByType(cho_provinciarenovacion, typeof(Int32?));
                this.cho_cantonrenovacion = (Int32?)Conversiones.GetValueByType(cho_cantonrenovacion, typeof(Int32?));
                this.cho_observacionlicencia = (String)Conversiones.GetValueByType(cho_observacionlicencia, typeof(String));
                this.cho_estado = (Int32?)Conversiones.GetValueByType(cho_estado, typeof(Int32?));
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

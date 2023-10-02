using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Usuario
    {
        #region Properties


        [Data(key = true)]
        public string usr_id { get; set; }
        [Data(originalkey = true)]
        public string usr_id_key { get; set; }        
        public string usr_perfil { get; set; }          
        public string usr_mail { get; set; }
        public string usr_password { get; set; }
        public string usr_nombres { get; set; }
        public int? usr_persona { get; set; }
        public int? usr_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        [Data(nosql = true, tablaref = "perfil", camporef = "per_descripcion", foreign = "usr_perfil", keyref = "per_id")]
        public string usr_descripcionperfil { get; set; }

        #endregion

        #region Constructors


        public Usuario()
        {

        }

        public Usuario(string id,string mail, string password, string nombres,string perfil, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {
            this.usr_id = id;
            this.usr_id_key = id;
            this.usr_mail = mail;      
            this.usr_password = password;
            this.usr_nombres = nombres;
            this.usr_perfil = perfil;
            this.usr_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;

        }

        public Usuario(IDataReader reader)
        {
            this.usr_id = reader["usr_id"].ToString();
            this.usr_id_key = reader["usr_id"].ToString();
            this.usr_perfil = reader["usr_perfil"].ToString();
            this.usr_mail = reader["usr_mail"].ToString();
            this.usr_password = reader["usr_password"].ToString();
            this.usr_nombres = reader["usr_nombres"].ToString();
            this.usr_persona = (reader["usr_persona"] != DBNull.Value) ? (int?)reader["usr_persona"] : null;
            this.usr_estado = (reader["usr_estado"] != DBNull.Value) ? (int?)reader["usr_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.usr_descripcionperfil = reader["usr_descripcionperfil"].ToString();
        }


        public Usuario(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object usr_id = null;
                object usr_id_key = null;
                object usr_password = null;
                object usr_nombres = null;
                object usr_mail = null;
                object usr_perfil = null;
                object usr_persona = null;
                object usr_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("usr_id", out usr_id);
                tmp.TryGetValue("usr_id_key", out usr_id_key);
                tmp.TryGetValue("usr_password", out usr_password);
                tmp.TryGetValue("usr_nombres", out usr_nombres);
                tmp.TryGetValue("usr_mail", out usr_mail);
                tmp.TryGetValue("usr_perfil", out usr_perfil);
                tmp.TryGetValue("usr_persona", out usr_persona);
                tmp.TryGetValue("usr_estado", out usr_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.usr_id = (String)Conversiones.GetValueByType(usr_id, typeof(String));
                this.usr_id_key = (String)Conversiones.GetValueByType(usr_id_key, typeof(String));
                this.usr_password = (String)Conversiones.GetValueByType(usr_password, typeof(String));
                this.usr_nombres = (String)Conversiones.GetValueByType(usr_nombres, typeof(String));
                this.usr_mail = (String)Conversiones.GetValueByType(usr_mail, typeof(String));
                this.usr_perfil = (String)Conversiones.GetValueByType(usr_perfil, typeof(String));
                this.usr_persona= (Int32?)Conversiones.GetValueByType(usr_persona, typeof(Int32?));
                this.usr_estado = (Int32?)Conversiones.GetValueByType(usr_estado, typeof(Int32?));
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

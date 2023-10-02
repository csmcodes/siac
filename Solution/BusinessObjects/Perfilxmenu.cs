using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;
namespace BusinessObjects
{
  public  class Perfilxmenu
    {
        #region Properties

        [Data(key = true)]
        public string pxm_perfil { get; set; }
        [Data(key = true)]
        public int pxm_menu { get; set; }
        [Data(originalkey = true)]
        public int pxm_menu_key { get; set; }
        [Data(originalkey = true)]
        public string pxm_perfil_key { get; set; }
        public int? pxm_agregar { get; set; }
        public int? pxm_modificar { get; set; }
        public int? pxm_eliminar { get; set; }
        public int? pxm_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(nosql = true, tablaref = "perfil", camporef = "per_descripcion", foreign = "pxm_perfil", keyref = "per_id")]
        public string pxm_descripcionperfil { get; set; }
        [Data(nosql = true, tablaref = "menu", camporef = "men_nombre", foreign = "pxm_menu", keyref = "men_id")]
        public string pxm_nombremenu { get; set; }        
        [Data(nosql = true, tablaref = "menu", camporef = "men_imagen", foreign = "pxm_menu", keyref = "men_id", join = "inner")]
        public string pxm_menuimagen { get; set; }
        [Data(nosql = true, tablaref = "menu", camporef = "men_orden", foreign = "pxm_menu", keyref = "men_id", join = "inner")]
        public Int32? pxm_menuorden { get; set; }
        [Data(nosql = true, tablaref = "menu", camporef = "men_formulario", foreign = "pxm_menu", keyref = "men_id", join = "inner")]
        public string pxm_menformualrio { get; set; }
        [Data(nosql = true, tablaref = "menu", camporef = "men_padre", foreign = "pxm_menu", keyref = "men_id", join = "inner")]
        public int? pxm_menupadre { get; set; }


        #endregion
        #region Constructors

        public Perfilxmenu()
        {
        }

        public Perfilxmenu(string perfil,int menu, int? agregar,int? modificar,int? eliminar , int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {
            this.pxm_menu=menu;       
            this.pxm_menu_key=menu;    
            this.pxm_perfil=perfil;
            this.pxm_perfil_key = perfil;
            this.pxm_agregar = agregar;
            this.pxm_modificar = modificar;
            this.pxm_eliminar = eliminar;
            this.pxm_estado=estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
        }

        public Perfilxmenu(IDataReader reader)
        {            
            this.pxm_menu = (int)reader["pxm_menu"];
            this.pxm_menu_key = (int)reader["pxm_menu"];
            this.pxm_perfil = reader["pxm_perfil"].ToString();
            this.pxm_perfil_key = reader["pxm_perfil"].ToString();
            this.pxm_agregar = (reader["pxm_agregar"] != DBNull.Value) ? (int?)reader["pxm_agregar"] : null;
            this.pxm_modificar = (reader["pxm_modificar"] != DBNull.Value) ? (int?)reader["pxm_modificar"] : null;
            this.pxm_eliminar = (reader["pxm_eliminar"] != DBNull.Value) ? (int?)reader["pxm_eliminar"] : null;
            this.pxm_estado = (reader["pxm_estado"] != DBNull.Value) ? (int?)reader["pxm_estado"] : null;                     
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.pxm_descripcionperfil = reader["pxm_descripcionperfil"].ToString();
            this.pxm_nombremenu = reader["pxm_nombremenu"].ToString();
        }
        public Perfilxmenu(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object pxm_perfil = null;
                object pxm_menu = null;
                object pxm_agregar = null;
                object pxm_modificar = null;
                object pxm_eliminar = null;
                object pxm_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("pxm_perfil", out pxm_perfil);
                tmp.TryGetValue("pxm_menu", out pxm_menu);
                tmp.TryGetValue("pxm_agregar", out pxm_agregar);
                tmp.TryGetValue("pxm_modificar", out pxm_modificar);
                tmp.TryGetValue("pxm_eliminar", out pxm_eliminar);
                tmp.TryGetValue("pxm_estado", out pxm_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.pxm_perfil = (String)Conversiones.GetValueByType(pxm_perfil, typeof(String));
                this.pxm_menu = (Int32)Conversiones.GetValueByType(pxm_menu, typeof(Int32));
                this.pxm_agregar = (Int32?)Conversiones.GetValueByType(pxm_agregar, typeof(Int32?));
                this.pxm_modificar = (Int32?)Conversiones.GetValueByType(pxm_modificar, typeof(Int32?));
                this.pxm_eliminar = (Int32?)Conversiones.GetValueByType(pxm_eliminar, typeof(Int32?));
                this.pxm_estado = (Int32?)Conversiones.GetValueByType(pxm_estado, typeof(Int32?));
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

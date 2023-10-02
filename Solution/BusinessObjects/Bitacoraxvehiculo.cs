using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;



namespace BusinessObjects
{

    public class Bitacoraxvehiculo
    {
        #region Properties


        [Data(key = true)]
        public DateTime bxv_fecha { get; set; }
        [Data(originalkey = true)]
        public DateTime bxv_fecha_key { get; set; }
        [Data(key = true)]
        public int bxv_vehiculo { get; set; }
        [Data(originalkey = true)]
        public int bxv_vehiculo_key { get; set; }
        [Data(key = true)]
        public int bxv_empresa{ get; set; }
        [Data(originalkey = true)]
        public int bxv_empresa_key { get; set; }      
        public string bxv_observacion { get; set; }
        public byte[] bxv_imagen { get; set; }        
        public int? bxv_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        #endregion
        
         #region Constructors
       
        public Bitacoraxvehiculo()
        {
        }

        public Bitacoraxvehiculo(DateTime fecha, int vehiculo, int empresa, string observacion, byte[] imagen, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)        
        {
            this.bxv_fecha = fecha;
            this.bxv_fecha_key = fecha;
            this.bxv_vehiculo = vehiculo;
            this.bxv_vehiculo_key = vehiculo;
            this.bxv_empresa = empresa;
            this.bxv_empresa_key = empresa;
            this.bxv_observacion = observacion;
            this.bxv_imagen = imagen;            
            this.bxv_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;

        }

        public Bitacoraxvehiculo(IDataReader reader)
        {
            this.bxv_fecha = (DateTime)reader["bxv_fecha"];
            this.bxv_fecha_key = (DateTime)reader["bxv_fecha"];
            this.bxv_vehiculo = (int)reader["bxv_vehiculo"];
            this.bxv_vehiculo_key = (int)reader["bxv_vehiculo"];
            this.bxv_empresa = (int)reader["bxv_empresa"];
            this.bxv_empresa_key = (int)reader["bxv_empresa"];
            this.bxv_observacion = reader["bxv_observacion"].ToString();
            this.bxv_imagen = (reader["bxv_imagen"] != DBNull.Value) ? (byte[])reader["bxv_imagen"] : null;
            this.bxv_estado = (reader["bxv_estado"] != DBNull.Value) ? (int?)reader["bxv_estado"] : null;               
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }




        public Bitacoraxvehiculo(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object bxv_fecha = null;
                object bxv_vehiculo = null;
                object bxv_empresa = null;
                object bxv_observacion = null;
                object bxv_imagen = null;
                object bxv_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("bxv_fecha", out bxv_fecha);
                tmp.TryGetValue("bxv_vehiculo", out bxv_vehiculo);
                tmp.TryGetValue("bxv_empresa", out bxv_empresa);
                tmp.TryGetValue("bxv_observacion", out bxv_observacion);
                tmp.TryGetValue("bxv_imagen", out bxv_imagen);
                tmp.TryGetValue("bxv_estado", out bxv_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.bxv_fecha = (DateTime)Conversiones.GetValueByType(bxv_fecha, typeof(DateTime));
                this.bxv_vehiculo = (Int32)Conversiones.GetValueByType(bxv_vehiculo, typeof(Int32));
                this.bxv_empresa = (Int32)Conversiones.GetValueByType(bxv_empresa, typeof(Int32));
                this.bxv_observacion = (String)Conversiones.GetValueByType(bxv_observacion, typeof(String));
              //  this.bxv_imagen = (Boolean?)Conversiones.GetValueByType(bxv_imagen, typeof(Boolean?));
                this.bxv_estado = (Int32?)Conversiones.GetValueByType(bxv_estado, typeof(Int32?));
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

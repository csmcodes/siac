using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Vehiculo
    {
        #region Properties


        [Data(key = true, auto = true)]
        public int veh_codigo { get; set; }
        [Data(originalkey = true)]
        public int veh_codigo_key { get; set; }
        [Data(key = true)]
        public int veh_empresa { get; set; }
        [Data(originalkey = true)]
        public int veh_empresa_key { get; set; }        
        public string veh_disco { get; set; }
        public string veh_nombre{ get; set; }
        public string veh_id { get; set; }   
        public string veh_placa { get; set; }
        public int? veh_anio { get; set; }
        public string veh_modelo { get; set; }
        public string veh_tipovehiculo { get; set; }
        public int? veh_duenio { get; set; }  
        public int? veh_estado { get; set; }
        public int? veh_chofer1 { get; set; }
        public int? veh_chofer2 { get; set; }
        public int? veh_empresa_cho2 { get; set; }
        public int? veh_empresa_cho1 { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "veh_duenio", keyref = "per_codigo")]
        public string veh_nombreduenio { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_apellidos", foreign = "veh_duenio", keyref = "per_codigo")]
        public string veh_apellidoduenio { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_ciruc", foreign = "veh_duenio", keyref = "per_codigo")]
        public string veh_ceduladuenio { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_id", foreign = "veh_duenio", keyref = "per_codigo")]
        public string veh_idduenio { get; set; }



        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "veh_chofer1", keyref = "per_codigo")]
        public string veh_nombrechofer1 { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_apellidos", foreign = "veh_chofer1", keyref = "per_codigo")]
        public string veh_apellidochofer1 { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_ciruc", foreign = "veh_chofer1", keyref = "per_codigo")]
        public string veh_cedulachofer1 { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_id", foreign = "veh_chofer1", keyref = "per_codigo")]
        public string veh_idchofer1 { get; set; }


        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "veh_chofer2", keyref = "per_codigo")]
        public string veh_nombrechofer2 { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_apellidos", foreign = "veh_chofer2", keyref = "per_codigo")]
        public string veh_apellidochofer2 { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_ciruc", foreign = "veh_chofer2", keyref = "per_codigo")]
        public string veh_cedulachofer2 { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_id", foreign = "veh_chofer2", keyref = "per_codigo")]
        public string veh_idchofer2 { get; set; }




        #endregion

        #region Constructors


        public Vehiculo()
        {

        }

        public Vehiculo(int codigo, int empresa, string id, string disco, string nombre, string placa, int año, string modelo, string tipovehiculo, int? dueño, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {
            this.veh_codigo = codigo;
            this.veh_codigo_key = codigo;
            this.veh_empresa = empresa;
            this.veh_empresa_key = empresa;
            this.veh_disco = disco;
            this.veh_id = id;
            this.veh_nombre = nombre;
            this.veh_placa=placa;
            this.veh_anio =año;
            this.veh_modelo=modelo;
            this.veh_tipovehiculo = tipovehiculo;
            this.veh_duenio=   dueño ;
            this.veh_estado = estado;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;

        }

        public Vehiculo(IDataReader reader)
        {
            this.veh_codigo = (int)reader["veh_codigo"]; 
            this.veh_codigo_key = (int)reader["veh_codigo"];
            this.veh_empresa = (int)reader["veh_empresa"];
            this.veh_empresa_key = (int)reader["veh_empresa"];
            this.veh_disco = reader["veh_disco"].ToString();
            this.veh_nombre = reader["veh_nombre"].ToString();
            this.veh_id = reader["veh_id"].ToString();
            this.veh_placa = reader["veh_placa"].ToString();
            this.veh_anio = (reader["veh_anio"] != DBNull.Value) ? (int?)reader["veh_anio"] : null;
            this.veh_modelo = reader["veh_modelo"].ToString();
            this.veh_tipovehiculo = reader["veh_tipovehiculo"].ToString();
            this.veh_duenio =(reader["veh_duenio"] != DBNull.Value) ? (int?)reader["veh_duenio"] : null;
            this.veh_estado = (reader["veh_estado"] != DBNull.Value) ? (int?)reader["veh_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.veh_nombreduenio = reader["veh_nombreduenio"].ToString();
            this.veh_apellidoduenio = reader["veh_apellidoduenio"].ToString();
            this.veh_ceduladuenio = reader["veh_ceduladuenio"].ToString();
            this.veh_id = reader["veh_id"].ToString();


            this.veh_chofer1 = (reader["veh_chofer1"] != DBNull.Value) ? (int?)reader["veh_chofer1"] : null;
            this.veh_chofer2 = (reader["veh_chofer2"] != DBNull.Value) ? (int?)reader["veh_chofer2"] : null;
            this.veh_empresa_cho2 = (reader["veh_empresa_cho2"] != DBNull.Value) ? (int?)reader["veh_empresa_cho2"] : null;
            this.veh_empresa_cho1 = (reader["veh_empresa_cho1"] != DBNull.Value) ? (int?)reader["veh_empresa_cho1"] : null;



            this.veh_nombrechofer1 = reader["veh_nombrechofer1"].ToString();
            this.veh_apellidochofer1 = reader["veh_apellidochofer1"].ToString();
            this.veh_cedulachofer1 = reader["veh_cedulachofer1"].ToString();

            this.veh_nombrechofer2 = reader["veh_nombrechofer2"].ToString();
            this.veh_apellidochofer2 = reader["veh_apellidochofer2"].ToString();
            this.veh_cedulachofer2 = reader["veh_cedulachofer2"].ToString();

            this.veh_idduenio = reader["veh_idduenio"].ToString();
            this.veh_idchofer1 = reader["veh_idchofer1"].ToString();
            this.veh_idchofer2 = reader["veh_idchofer2"].ToString();
            
        }



        public Vehiculo(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object veh_tipovehiculo = null;
                object veh_codigo = null;
                object veh_empresa = null;
                object veh_codigo_key = null;
                object veh_empresa_key = null;
                object veh_id = null;
                object veh_nombre = null;
                object veh_disco = null;
                object veh_placa = null;
                object veh_anio = null;
                object veh_modelo = null;
                object veh_duenio = null;
                object veh_empresa_cho2 = null;
                object veh_empresa_cho1 = null;
                object veh_chofer1 = null;
                object veh_chofer2 = null;
                object veh_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("veh_tipovehiculo", out veh_tipovehiculo);
                tmp.TryGetValue("veh_codigo", out veh_codigo);
                tmp.TryGetValue("veh_empresa", out veh_empresa);
                tmp.TryGetValue("veh_codigo_key", out veh_codigo_key);
                tmp.TryGetValue("veh_empresa_key", out veh_empresa_key);
                tmp.TryGetValue("veh_id", out veh_id);
                tmp.TryGetValue("veh_nombre", out veh_nombre);
                tmp.TryGetValue("veh_disco", out veh_disco);
                tmp.TryGetValue("veh_placa", out veh_placa);
                tmp.TryGetValue("veh_anio", out veh_anio);
                tmp.TryGetValue("veh_modelo", out veh_modelo);
                tmp.TryGetValue("veh_duenio", out veh_duenio);
                tmp.TryGetValue("veh_empresa_cho2", out veh_empresa_cho2);
                tmp.TryGetValue("veh_empresa_cho1", out veh_empresa_cho1);
                tmp.TryGetValue("veh_chofer1", out veh_chofer1);
                tmp.TryGetValue("veh_chofer2", out veh_chofer2);
                tmp.TryGetValue("veh_estado", out veh_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.veh_tipovehiculo = (String)Conversiones.GetValueByType(veh_tipovehiculo, typeof(String));
                this.veh_codigo = (Int32)Conversiones.GetValueByType(veh_codigo, typeof(Int32));
                this.veh_empresa = (Int32)Conversiones.GetValueByType(veh_empresa, typeof(Int32));
                this.veh_codigo_key = (Int32)Conversiones.GetValueByType(veh_codigo_key, typeof(Int32));
                this.veh_empresa_key = (Int32)Conversiones.GetValueByType(veh_empresa_key, typeof(Int32));
                this.veh_id = (String)Conversiones.GetValueByType(veh_id, typeof(String));
                this.veh_nombre = (String)Conversiones.GetValueByType(veh_nombre, typeof(String));
                this.veh_disco = (String)Conversiones.GetValueByType(veh_disco, typeof(String));
                this.veh_placa = (String)Conversiones.GetValueByType(veh_placa, typeof(String));
                this.veh_anio = (Int32?)Conversiones.GetValueByType(veh_anio, typeof(Int32?));
                this.veh_modelo = (String)Conversiones.GetValueByType(veh_modelo, typeof(String));
                this.veh_duenio = (Int32?)Conversiones.GetValueByType(veh_duenio, typeof(Int32?));
                this.veh_empresa_cho2 = (Int32?)Conversiones.GetValueByType(veh_empresa_cho2, typeof(Int32?));
                this.veh_empresa_cho1 = (Int32?)Conversiones.GetValueByType(veh_empresa_cho1, typeof(Int32?));
                this.veh_chofer1 = (Int32?)Conversiones.GetValueByType(veh_chofer1, typeof(Int32?));
                this.veh_chofer2 = (Int32?)Conversiones.GetValueByType(veh_chofer2, typeof(Int32?));
                this.veh_estado = (Int32?)Conversiones.GetValueByType(veh_estado, typeof(Int32?));
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

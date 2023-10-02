using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Ccomenv
    {
        #region Properties

        [Data(key = true)]
        public Int32 cenv_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 cenv_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 cenv_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 cenv_comprobante_key { get; set; }
        public Int32? cenv_empresa_rem { get; set; }
        public Int32? cenv_remitente { get; set; }
        public String cenv_ciruc_rem { get; set; }
        public String cenv_nombres_rem { get; set; }
        public String cenv_apellidos_rem { get; set; }
        public String cenv_direccion_rem { get; set; }
        public String cenv_telefono_rem { get; set; }
        public Int32? cenv_empresa_des { get; set; }
        public Int32? cenv_destinatario { get; set; }
        public String cenv_ciruc_des { get; set; }
        public String cenv_nombres_des { get; set; }
        public String cenv_apellidos_des { get; set; }
        public String cenv_direccion_des { get; set; }
        public String cenv_telefono_des { get; set; }
        public Int32? cenv_empresa_veh { get; set; }
        public Int32? cenv_vehiculo { get; set; }
        public String cenv_placa { get; set; }
        public String cenv_disco { get; set; }
        public Int32? cenv_empresa_cho { get; set; }
        public Int32? cenv_chofer { get; set; }
        public Int32? cenv_empresa_soc { get; set; }
        public Int32? cenv_socio { get; set; }
        public String cenv_observacion { get; set; }
        public Int32? cenv_empresa_rut { get; set; }
        public Int32? cenv_ruta { get; set; }
        public Int32? cenv_estado_ruta { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
        public String cenv_guia1 { get; set; }
        public String cenv_guia2 { get; set; }
        public String cenv_guia3 { get; set; }
        public Int32? cenv_empresa_ret { get; set; }
        public Int32? cenv_retira { get; set; }
        public String cenv_ciruc_ret { get; set; }
        public String cenv_nombres_ret { get; set; }
        public String cenv_apellidos_ret { get; set; }
        public String cenv_direccion_ret { get; set; }
        public String cenv_telefono_ret { get; set; }
        public Int32? cenv_despachado_ret { get; set; }
        public DateTime? cenv_fecha_ret { get; set; }
        public String cenv_observaciones_ret { get; set; }
        public String cenv_nombres_cho { get; set; }
        public String cenv_nombres_soc { get; set; }
        public Int32? cenv_guias{ get; set; }

        [Data(nosql = true, tablaref = "comprobante", camporef = "com_numero", foreign = "cenv_comprobante, cenv_empresa", keyref = "com_codigo,com_empresa", join = "inner")]
        public int? cenv_numero { get; set; }

        [Data(nosql = true, tablaref = "comprobante", camporef = "com_doctran", foreign = "cenv_comprobante, cenv_empresa", keyref = "com_codigo,com_empresa", join = "inner")]
        public string cenv_doctran { get; set; }


        [Data(nosql = true, tablaref = "Total", camporef = "tot_total", foreign = "cenv_comprobante, cenv_empresa", keyref = "tot_comprobante, tot_empresa", join = "left")]
        public decimal? cenv_total { get; set; }


        [Data(nosql = true, tablaref = "ruta", camporef = "rut_destino", foreign = "cenv_empresa, cenv_ruta", keyref = "rut_empresa, rut_codigo", join = "left")]
        public string cenv_rutadestino{ get; set; }
        [Data(nosql = true, tablaref = "ruta", camporef = "rut_origen", foreign = "cenv_empresa, cenv_ruta", keyref = "rut_empresa, rut_codigo", join = "left")]
        public string cenv_rutaorigen{ get; set; }

        




        #endregion

        #region Constructors


        public Ccomenv()
        {
        }

        public Ccomenv(Int32 cenv_empresa, Int64 cenv_comprobante, Int32 cenv_empresa_rem, Int32 cenv_remitente, String cenv_ciruc_rem, String cenv_nombres_rem, String cenv_apellidos_rem, String cenv_direccion_rem, String cenv_telefono_rem, Int32 cenv_empresa_des, Int32 cenv_destinatario, String cenv_ciruc_des, String cenv_nombres_des, String cenv_apellidos_des, String cenv_direccion_des, String cenv_telefono_des, Int32 cenv_empresa_veh, Int32 cenv_vehiculo, String cenv_placa, String cenv_disco, Int32 cenv_empresa_cho, Int32 cenv_chofer, Int32 cenv_empresa_soc, Int32 cenv_socio, String cenv_observacion, Int32 cenv_empresa_rut, Int32 cenv_ruta, Int32 cenv_estado_ruta, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha, Int32 cenv_empresa_ret, Int32 cenv_retira, String cenv_ciruc_ret, String cenv_nombres_ret, String cenv_apellidos_ret, String cenv_direccion_ret, String cenv_telefono_ret, Int32 cenv_despachado_ret, DateTime cenv_fecha_ret, String cenv_observaciones_ret, String cenv_nombres_cho, String cenv_nombres_soc, Int32? cenv_guias)
        {
            this.cenv_empresa = cenv_empresa;
            this.cenv_comprobante = cenv_comprobante;
            this.cenv_empresa_rem = cenv_empresa_rem;
            this.cenv_remitente = cenv_remitente;
            this.cenv_ciruc_rem = cenv_ciruc_rem;
            this.cenv_nombres_rem = cenv_nombres_rem;
            this.cenv_apellidos_rem = cenv_apellidos_rem;
            this.cenv_direccion_rem = cenv_direccion_rem;
            this.cenv_telefono_rem = cenv_telefono_rem;
            this.cenv_empresa_des = cenv_empresa_des;
            this.cenv_destinatario = cenv_destinatario;
            this.cenv_ciruc_des = cenv_ciruc_des;
            this.cenv_nombres_des = cenv_nombres_des;
            this.cenv_apellidos_des = cenv_apellidos_des;
            this.cenv_direccion_des = cenv_direccion_des;
            this.cenv_telefono_des = cenv_telefono_des;
            this.cenv_empresa_veh = cenv_empresa_veh;
            this.cenv_vehiculo = cenv_vehiculo;
            this.cenv_placa = cenv_placa;
            this.cenv_disco = cenv_disco;
            this.cenv_empresa_cho = cenv_empresa_cho;
            this.cenv_chofer = cenv_chofer;
            this.cenv_empresa_soc = cenv_empresa_soc;
            this.cenv_socio = cenv_socio;
            this.cenv_observacion = cenv_observacion;
            this.cenv_empresa_rut = cenv_empresa_rut;
            this.cenv_ruta = cenv_ruta;
            this.cenv_estado_ruta = cenv_estado_ruta;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;
            this.cenv_empresa_ret = cenv_empresa_ret;
            this.cenv_ciruc_ret = cenv_ciruc_ret;
            this.cenv_retira = cenv_retira;
            this.cenv_nombres_ret = cenv_nombres_ret;
            this.cenv_apellidos_ret = cenv_apellidos_ret;
            this.cenv_direccion_ret = cenv_direccion_ret;
            this.cenv_telefono_ret = cenv_telefono_ret;
            this.cenv_despachado_ret = cenv_despachado_ret;
            this.cenv_fecha_ret = cenv_fecha_ret;
            this.cenv_observaciones_ret = cenv_observaciones_ret;
            this.cenv_nombres_cho = cenv_nombres_cho;
            this.cenv_nombres_soc = cenv_nombres_soc;
            this.cenv_guias = cenv_guias;

        }

        public Ccomenv(IDataReader reader)
        {
            this.cenv_empresa = (Int32)reader["cenv_empresa"];
            this.cenv_comprobante = (Int64)reader["cenv_comprobante"];
            this.cenv_empresa_rem = (reader["cenv_empresa_rem"] != DBNull.Value) ? (Int32?)reader["cenv_empresa_rem"] : null;
            this.cenv_remitente = (reader["cenv_remitente"] != DBNull.Value) ? (Int32?)reader["cenv_remitente"] : null;
            this.cenv_ciruc_rem = reader["cenv_ciruc_rem"].ToString();
            this.cenv_nombres_rem = reader["cenv_nombres_rem"].ToString();
            this.cenv_apellidos_rem = reader["cenv_apellidos_rem"].ToString();
            this.cenv_direccion_rem = reader["cenv_direccion_rem"].ToString();
            this.cenv_telefono_rem = reader["cenv_telefono_rem"].ToString();
            this.cenv_empresa_des = (reader["cenv_empresa_des"] != DBNull.Value) ? (Int32?)reader["cenv_empresa_des"] : null;
            this.cenv_destinatario = (reader["cenv_destinatario"] != DBNull.Value) ? (Int32?)reader["cenv_destinatario"] : null;
            this.cenv_ciruc_des = reader["cenv_ciruc_des"].ToString();
            this.cenv_nombres_des = reader["cenv_nombres_des"].ToString();
            this.cenv_apellidos_des = reader["cenv_apellidos_des"].ToString();
            this.cenv_direccion_des = reader["cenv_direccion_des"].ToString();
            this.cenv_telefono_des = reader["cenv_telefono_des"].ToString();
            this.cenv_empresa_veh = (reader["cenv_empresa_veh"] != DBNull.Value) ? (Int32?)reader["cenv_empresa_veh"] : null;
            this.cenv_vehiculo = (reader["cenv_vehiculo"] != DBNull.Value) ? (Int32?)reader["cenv_vehiculo"] : null;
            this.cenv_placa = reader["cenv_placa"].ToString();
            this.cenv_disco = reader["cenv_disco"].ToString();
            this.cenv_empresa_cho = (reader["cenv_empresa_cho"] != DBNull.Value) ? (Int32?)reader["cenv_empresa_cho"] : null;
            this.cenv_chofer = (reader["cenv_chofer"] != DBNull.Value) ? (Int32?)reader["cenv_chofer"] : null;
            this.cenv_empresa_soc = (reader["cenv_empresa_soc"] != DBNull.Value) ? (Int32?)reader["cenv_empresa_soc"] : null;
            this.cenv_socio = (reader["cenv_socio"] != DBNull.Value) ? (Int32?)reader["cenv_socio"] : null;
            this.cenv_observacion = reader["cenv_observacion"].ToString();
            this.cenv_empresa_rut = (reader["cenv_empresa_rut"] != DBNull.Value) ? (Int32?)reader["cenv_empresa_rut"] : null;
            this.cenv_ruta = (reader["cenv_ruta"] != DBNull.Value) ? (Int32?)reader["cenv_ruta"] : null;
            this.cenv_estado_ruta = (reader["cenv_estado_ruta"] != DBNull.Value) ? (Int32?)reader["cenv_estado_ruta"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.cenv_doctran = reader["cenv_doctran"].ToString();
            this.cenv_numero = (reader["cenv_numero"] != DBNull.Value) ? (int?)reader["cenv_numero"] : null;
            this.cenv_total = (reader["cenv_total"] != DBNull.Value) ? (decimal?)reader["cenv_total"] : null;
            this.cenv_guia1 = reader["cenv_guia1"].ToString();
            this.cenv_guia2 = reader["cenv_guia2"].ToString();
            this.cenv_guia3 = reader["cenv_guia3"].ToString();
            this.cenv_rutaorigen= reader["cenv_rutaorigen"].ToString();
            this.cenv_rutadestino = reader["cenv_rutadestino"].ToString();
            this.cenv_empresa_ret = (reader["cenv_empresa_ret"] != DBNull.Value) ? (Int32?)reader["cenv_empresa_ret"] : null;
            this.cenv_retira = (reader["cenv_retira"] != DBNull.Value) ? (Int32?)reader["cenv_retira"] : null;
            this.cenv_ciruc_ret = reader["cenv_ciruc_ret"].ToString();
            this.cenv_nombres_ret = reader["cenv_nombres_ret"].ToString();
            this.cenv_apellidos_ret = reader["cenv_apellidos_ret"].ToString();
            this.cenv_direccion_ret = reader["cenv_direccion_ret"].ToString();
            this.cenv_telefono_ret = reader["cenv_telefono_ret"].ToString();
            this.cenv_despachado_ret = (reader["cenv_despachado_ret"] != DBNull.Value) ? (Int32?)reader["cenv_despachado_ret"] : null;
            this.cenv_fecha_ret = (reader["cenv_fecha_ret"] != DBNull.Value) ? (DateTime?)reader["cenv_fecha_ret"] : null;
            this.cenv_observaciones_ret = reader["cenv_observaciones_ret"].ToString();
            this.cenv_nombres_cho = reader["cenv_nombres_cho"].ToString();
            this.cenv_nombres_soc = reader["cenv_nombres_soc"].ToString();
            this.cenv_guias = (reader["cenv_guias"] != DBNull.Value) ? (Int32?)reader["cenv_guias"] : null;

         
        }


        public Ccomenv(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;

                object cenv_empresa = null;
                object cenv_comprobante = null;
                object cenv_empresa_rem = null;
                object cenv_remitente = null;
                object cenv_ciruc_rem = null;
                object cenv_nombres_rem = null;
                object cenv_apellidos_rem = null;
                object cenv_direccion_rem = null;
                object cenv_telefono_rem = null;
                object cenv_empresa_des = null;
                object cenv_destinatario = null;
                object cenv_ciruc_des = null;
                object cenv_nombres_des = null;
                object cenv_apellidos_des = null;
                object cenv_direccion_des = null;
                object cenv_telefono_des = null;
                object cenv_empresa_veh = null;
                object cenv_vehiculo = null;
                object cenv_placa = null;
                object cenv_disco = null;
                object cenv_empresa_cho = null;
                object cenv_chofer = null;
                object cenv_empresa_soc = null;
                object cenv_socio = null;
                object cenv_observacion = null;
                object cenv_empresa_rut = null;
                object cenv_ruta = null;
                object cenv_estado_ruta = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;
                object cenv_guia1 = null;
                object cenv_guia2 = null;
                object cenv_guia3 = null;
                object cenv_empresa_ret = null;
                object cenv_retira = null;
                object cenv_ciruc_ret = null;
                object cenv_nombres_ret = null;
                object cenv_apellidos_ret = null;
                object cenv_direccion_ret = null;
                object cenv_telefono_ret = null;
                object cenv_despachado_ret = null;
                object cenv_fecha_ret = null;
                object cenv_observaciones_ret = null;
                object cenv_nombres_cho = null;
                object cenv_nombres_soc = null;
                object cenv_guias = null;
                tmp.TryGetValue("cenv_empresa", out cenv_empresa);
                tmp.TryGetValue("cenv_comprobante", out cenv_comprobante);
                tmp.TryGetValue("cenv_empresa_rem", out cenv_empresa_rem);
                tmp.TryGetValue("cenv_remitente", out cenv_remitente);
                tmp.TryGetValue("cenv_ciruc_rem", out cenv_ciruc_rem);
                tmp.TryGetValue("cenv_nombres_rem", out cenv_nombres_rem);
                tmp.TryGetValue("cenv_apellidos_rem", out cenv_apellidos_rem);
                tmp.TryGetValue("cenv_direccion_rem", out cenv_direccion_rem);
                tmp.TryGetValue("cenv_telefono_rem", out cenv_telefono_rem);
                tmp.TryGetValue("cenv_empresa_des", out cenv_empresa_des);
                tmp.TryGetValue("cenv_destinatario", out cenv_destinatario);
                tmp.TryGetValue("cenv_ciruc_des", out cenv_ciruc_des);
                tmp.TryGetValue("cenv_nombres_des", out cenv_nombres_des);
                tmp.TryGetValue("cenv_apellidos_des", out cenv_apellidos_des);
                tmp.TryGetValue("cenv_direccion_des", out cenv_direccion_des);
                tmp.TryGetValue("cenv_telefono_des", out cenv_telefono_des);
                tmp.TryGetValue("cenv_empresa_veh", out cenv_empresa_veh);
                tmp.TryGetValue("cenv_vehiculo", out cenv_vehiculo);
                tmp.TryGetValue("cenv_placa", out cenv_placa);
                tmp.TryGetValue("cenv_disco", out cenv_disco);
                tmp.TryGetValue("cenv_empresa_cho", out cenv_empresa_cho);
                tmp.TryGetValue("cenv_chofer", out cenv_chofer);
                tmp.TryGetValue("cenv_empresa_soc", out cenv_empresa_soc);
                tmp.TryGetValue("cenv_socio", out cenv_socio);
                tmp.TryGetValue("cenv_observacion", out cenv_observacion);
                tmp.TryGetValue("cenv_empresa_rut", out cenv_empresa_rut);
                tmp.TryGetValue("cenv_ruta", out cenv_ruta);
                tmp.TryGetValue("cenv_estado_ruta", out cenv_estado_ruta);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                tmp.TryGetValue("cenv_guia1", out cenv_guia1);
                tmp.TryGetValue("cenv_guia2", out cenv_guia2);
                tmp.TryGetValue("cenv_guia3", out cenv_guia3);
                tmp.TryGetValue("cenv_empresa_ret", out cenv_empresa_ret);
                tmp.TryGetValue("cenv_retira", out cenv_retira);
                tmp.TryGetValue("cenv_ciruc_ret", out cenv_ciruc_ret);
                tmp.TryGetValue("cenv_nombres_ret", out cenv_nombres_ret);
                tmp.TryGetValue("cenv_apellidos_ret", out cenv_apellidos_ret);
                tmp.TryGetValue("cenv_direccion_ret", out cenv_direccion_ret);
                tmp.TryGetValue("cenv_telefono_ret", out cenv_telefono_ret);

                tmp.TryGetValue("cenv_despachado_ret", out cenv_despachado_ret);
                tmp.TryGetValue("cenv_fecha_ret", out cenv_fecha_ret);
                tmp.TryGetValue("cenv_observaciones_ret", out cenv_observaciones_ret);
                tmp.TryGetValue("cenv_nombres_cho", out cenv_nombres_cho);
                tmp.TryGetValue("cenv_nombres_soc", out cenv_nombres_soc);
                tmp.TryGetValue("cenv_guias", out cenv_guias);

                this.cenv_empresa = (Int32)Conversiones.GetValueByType(cenv_empresa, typeof(Int32));
                this.cenv_comprobante = (Int64)Conversiones.GetValueByType(cenv_comprobante, typeof(Int64));
                this.cenv_empresa_rem = (Int32?)Conversiones.GetValueByType(cenv_empresa_rem, typeof(Int32?));
                this.cenv_remitente = (Int32?)Conversiones.GetValueByType(cenv_remitente, typeof(Int32?));
                this.cenv_ciruc_rem = (String)Conversiones.GetValueByType(cenv_ciruc_rem, typeof(String));
                this.cenv_nombres_rem = (String)Conversiones.GetValueByType(cenv_nombres_rem, typeof(String));
                this.cenv_apellidos_rem = (String)Conversiones.GetValueByType(cenv_apellidos_rem, typeof(String));
                this.cenv_direccion_rem = (String)Conversiones.GetValueByType(cenv_direccion_rem, typeof(String));
                this.cenv_telefono_rem = (String)Conversiones.GetValueByType(cenv_telefono_rem, typeof(String));
                this.cenv_empresa_des = (Int32?)Conversiones.GetValueByType(cenv_empresa_des, typeof(Int32?));
                this.cenv_destinatario = (Int32?)Conversiones.GetValueByType(cenv_destinatario, typeof(Int32?));
                this.cenv_ciruc_des = (String)Conversiones.GetValueByType(cenv_ciruc_des, typeof(String));
                this.cenv_nombres_des = (String)Conversiones.GetValueByType(cenv_nombres_des, typeof(String));
                this.cenv_apellidos_des = (String)Conversiones.GetValueByType(cenv_apellidos_des, typeof(String));
                this.cenv_direccion_des = (String)Conversiones.GetValueByType(cenv_direccion_des, typeof(String));
                this.cenv_telefono_des = (String)Conversiones.GetValueByType(cenv_telefono_des, typeof(String));
                this.cenv_empresa_veh = (Int32?)Conversiones.GetValueByType(cenv_empresa_veh, typeof(Int32?));
                this.cenv_vehiculo = (Int32?)Conversiones.GetValueByType(cenv_vehiculo, typeof(Int32?));
                this.cenv_placa = (String)Conversiones.GetValueByType(cenv_placa, typeof(String));
                this.cenv_disco = (String)Conversiones.GetValueByType(cenv_disco, typeof(String));
                this.cenv_empresa_cho = (Int32?)Conversiones.GetValueByType(cenv_empresa_cho, typeof(Int32?));
                this.cenv_chofer = (Int32?)Conversiones.GetValueByType(cenv_chofer, typeof(Int32?));
                this.cenv_empresa_soc = (Int32?)Conversiones.GetValueByType(cenv_empresa_soc, typeof(Int32?));
                this.cenv_socio = (Int32?)Conversiones.GetValueByType(cenv_socio, typeof(Int32?));
                this.cenv_observacion = (String)Conversiones.GetValueByType(cenv_observacion, typeof(String));
                this.cenv_empresa_rut = (Int32?)Conversiones.GetValueByType(cenv_empresa_rut, typeof(Int32?));
                this.cenv_ruta = (Int32?)Conversiones.GetValueByType(cenv_ruta, typeof(Int32?));
                this.cenv_estado_ruta = (Int32?)Conversiones.GetValueByType(cenv_estado_ruta, typeof(Int32?));
                this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
                this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
                this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
                this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
                this.cenv_guia1 = (String)Conversiones.GetValueByType(cenv_guia1, typeof(String));
                this.cenv_guia2 = (String)Conversiones.GetValueByType(cenv_guia2, typeof(String));
                this.cenv_guia3 = (String)Conversiones.GetValueByType(cenv_guia3, typeof(String));
                this.cenv_empresa_ret = (Int32?)Conversiones.GetValueByType(cenv_empresa_ret, typeof(Int32?));
                this.cenv_retira = (Int32?)Conversiones.GetValueByType(cenv_retira, typeof(Int32?));
                this.cenv_ciruc_ret = (String)Conversiones.GetValueByType(cenv_ciruc_ret, typeof(String));
                this.cenv_nombres_ret = (String)Conversiones.GetValueByType(cenv_nombres_ret, typeof(String));
                this.cenv_apellidos_ret = (String)Conversiones.GetValueByType(cenv_apellidos_ret, typeof(String));
                this.cenv_direccion_ret = (String)Conversiones.GetValueByType(cenv_direccion_ret, typeof(String));
                this.cenv_telefono_ret = (String)Conversiones.GetValueByType(cenv_telefono_ret, typeof(String));

                this.cenv_despachado_ret = (Int32?)Conversiones.GetValueByType(cenv_empresa_ret, typeof(Int32?));
                this.cenv_fecha_ret = (DateTime?)Conversiones.GetValueByType(cenv_fecha_ret, typeof(DateTime?));
                this.cenv_observaciones_ret = (String)Conversiones.GetValueByType(cenv_observaciones_ret, typeof(String));
                this.cenv_nombres_cho = (String)Conversiones.GetValueByType(cenv_nombres_cho, typeof(String));
                this.cenv_nombres_soc = (String)Conversiones.GetValueByType(cenv_nombres_soc, typeof(String));
                this.cenv_guias= (Int32?)Conversiones.GetValueByType(cenv_guias, typeof(Int32?));
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;


namespace BusinessObjects
{
    public class Cuenta
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int cue_codigo { get; set; }
        [Data(originalkey = true)]
        public int cue_codigo_key { get; set; }      
        [Data(key = true)]
        public int cue_empresa { get; set; }
        [Data(originalkey = true)]
        public int cue_empresa_key { get; set; }
        public string cue_id { get; set; }
        public string cue_nombre { get; set; }
        public int? cue_modulo { get; set; }
        public int? cue_genero { get; set; }
        public int? cue_movimiento { get; set; }
        public int? cue_reporta { get; set; }
        public int? cue_orden { get; set; }
       
        public int? cue_nivel { get; set; }
        public int? cue_visualiza { get; set; }
        public int? cue_estado { get; set; }
        public int? cue_negrita { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(noprop = true)]
        public decimal inicial { get; set; }
        [Data(noprop = true)]
        public decimal debito { get; set; }
        [Data(noprop = true)]
        public decimal credito { get; set; }
        [Data(noprop = true)]
        public decimal final { get; set; }

              
        #endregion

        #region Constructors


        public Cuenta()
        {
        }

        public Cuenta(int codigo, int empresa, string id, string nombre, int? modulo, int? genero, int? movimiento, int? reporta, int? orden, int? visualiza, int? negrita,  int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {       
            this.cue_codigo =codigo;        
            this.cue_codigo_key =codigo;
            this.cue_empresa =empresa;      
            this.cue_empresa_key=empresa;
            this.cue_id = id;
            this.cue_nombre = nombre;
            this.cue_modulo = modulo;
            this.cue_genero = genero;
            this.cue_movimiento = movimiento;
            this.cue_reporta = reporta;
            this.cue_orden = orden;
            this.cue_visualiza = visualiza;       
            this.cue_negrita = negrita;
            this.cue_estado = estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Cuenta(IDataReader reader)
        {


            this.cue_codigo = (int)reader["cue_codigo"];
            this.cue_codigo_key = (int)reader["cue_codigo"];
            this.cue_empresa = (int)reader["cue_empresa"];
            this.cue_empresa_key = (int)reader["cue_empresa"]; 
            this.cue_id = reader["cue_id"].ToString();
            this.cue_nombre = reader["cue_nombre"].ToString();
            this.cue_modulo = (reader["cue_modulo"] != DBNull.Value) ? (int?)reader["cue_modulo"] : null;
            this.cue_genero = (reader["cue_genero"] != DBNull.Value) ? (int?)reader["cue_genero"] : null;
            this.cue_movimiento = (reader["cue_movimiento"] != DBNull.Value) ? (int?)reader["cue_movimiento"] : null;
            this.cue_reporta = (reader["cue_reporta"] != DBNull.Value) ? (int?)reader["cue_reporta"] : null;
            this.cue_orden = (reader["cue_orden"] != DBNull.Value) ? (int?)reader["cue_orden"] : null;
            this.cue_nivel = (reader["cue_nivel"] != DBNull.Value) ? (int?)reader["cue_nivel"] : null;
            this.cue_visualiza = (reader["cue_visualiza"] != DBNull.Value) ? (int?)reader["cue_visualiza"] : null;
            this.cue_negrita = (reader["cue_negrita"] != DBNull.Value) ? (int?)reader["cue_negrita"] : null;
            this.cue_estado = (reader["cue_estado"] != DBNull.Value) ? (int?)reader["cue_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
        }



        public Cuenta (object objeto)
        {
            Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
            object cue_empresa = null;
            object cue_codigo = null;
            object cue_id = null;
            object cue_nombre = null;
            object cue_genero = null;
            object cue_modulo = null;
            object cue_movimiento = null;
            object cue_reporta = null;
            object cue_orden = null;
            object cue_nivel = null;
            object cue_visualiza = null;
            object cue_negrita = null;
            object cue_estado = null;
            object crea_usr = null;
            object crea_fecha = null;
            object crea_ip = null;
            object mod_usr = null;
            object mod_fecha = null;
            object mod_ip = null;


            tmp.TryGetValue("cue_empresa", out cue_empresa);
            tmp.TryGetValue("cue_codigo", out cue_codigo);
            tmp.TryGetValue("cue_id", out cue_id);
            tmp.TryGetValue("cue_nombre", out cue_nombre);
            tmp.TryGetValue("cue_genero", out cue_genero);
            tmp.TryGetValue("cue_modulo", out cue_modulo);
            tmp.TryGetValue("cue_movimiento", out cue_movimiento);
            tmp.TryGetValue("cue_reporta", out cue_reporta);
            tmp.TryGetValue("cue_orden", out cue_orden);
            tmp.TryGetValue("cue_nivel", out cue_nivel);
            tmp.TryGetValue("cue_visualiza", out cue_visualiza);
            tmp.TryGetValue("cue_negrita", out cue_negrita);
            tmp.TryGetValue("cue_estado", out cue_estado);
            tmp.TryGetValue("crea_usr", out crea_usr);
            tmp.TryGetValue("crea_fecha", out crea_fecha);
            tmp.TryGetValue("crea_ip", out crea_ip);
            tmp.TryGetValue("mod_usr", out mod_usr);
            tmp.TryGetValue("mod_fecha", out mod_fecha);
            tmp.TryGetValue("mod_ip", out mod_ip);


            this.cue_empresa = (Int32)Conversiones.GetValueByType(cue_empresa, typeof(Int32));
            this.cue_codigo = (Int32)Conversiones.GetValueByType(cue_codigo, typeof(Int32));
            this.cue_id = (String)Conversiones.GetValueByType(cue_id, typeof(String));
            this.cue_nombre = (String)Conversiones.GetValueByType(cue_nombre, typeof(String));
            this.cue_genero = (Int32?)Conversiones.GetValueByType(cue_genero, typeof(Int32?));
            this.cue_modulo = (Int32?)Conversiones.GetValueByType(cue_modulo, typeof(Int32?));
            this.cue_movimiento = (Int32?)Conversiones.GetValueByType(cue_movimiento, typeof(Int32?));
            this.cue_reporta = (Int32?)Conversiones.GetValueByType(cue_reporta, typeof(Int32?));
            this.cue_orden = (Int32?)Conversiones.GetValueByType(cue_orden, typeof(Int32?));
            this.cue_nivel = (Int32?)Conversiones.GetValueByType(cue_nivel, typeof(Int32?));
            this.cue_visualiza = (Int32?)Conversiones.GetValueByType(cue_visualiza, typeof(Int32?));
            this.cue_negrita = (Int32?)Conversiones.GetValueByType(cue_negrita, typeof(Int32?));
            this.cue_estado = (Int32?)Conversiones.GetValueByType(cue_estado, typeof(Int32?));
            this.crea_usr = (String)Conversiones.GetValueByType(crea_usr, typeof(String));
            this.crea_fecha = (DateTime?)Conversiones.GetValueByType(crea_fecha, typeof(DateTime?));
            this.mod_usr = (String)Conversiones.GetValueByType(mod_usr, typeof(String));
            this.mod_fecha = (DateTime?)Conversiones.GetValueByType(mod_fecha, typeof(DateTime?));
            /*
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object codigo = null;
                object codigokey = null;
                object empresa = null;
                object empresakey = null;
                object nombre = null;
                object id = null;
                object modulo = null;
                object genero = null;
                object movimiento = null;
                object negrita = null;
                object visualiza = null;
                object activo = null;
                object nivel = null;
                tmp.TryGetValue("cue_codigo", out codigo);
                tmp.TryGetValue("cue_codigo_key", out codigokey);
                tmp.TryGetValue("cue_empresa", out empresa);
                tmp.TryGetValue("cue_empresa_key", out empresakey);
                tmp.TryGetValue("cue_nombre", out nombre);
                tmp.TryGetValue("cue_nivel", out nivel);

                tmp.TryGetValue("cue_id", out id);
                tmp.TryGetValue("cue_modulo", out modulo);
                tmp.TryGetValue("cue_genero", out genero);
                tmp.TryGetValue("cue_movimiento", out movimiento);
                tmp.TryGetValue("cue_negrita", out negrita);
                tmp.TryGetValue("cue_visualiza", out visualiza);

                tmp.TryGetValue("cue_estado", out activo);
                if (codigo != null)
                {
                    this.cue_codigo = int.Parse(codigo.ToString());
                    this.cue_codigo_key = int.Parse(codigokey.ToString());
                }


                if (empresa != null)
                {
                    this.cue_empresa = int.Parse(empresa.ToString());
                    this.cue_empresa_key = int.Parse(empresakey.ToString());
                }

                this.cue_nombre = (string)nombre;
                this.cue_id = (string)id;
                this.cue_modulo = Convert.ToInt32(modulo);
                this.cue_genero = Convert.ToInt32(genero);
                this.cue_nivel = Convert.ToInt32(nivel);
                if (movimiento != null && !movimiento.Equals(""))
                    this.cue_movimiento = Convert.ToInt32(movimiento);
                this.cue_negrita = (int?)negrita;
                this.cue_visualiza = (int?)visualiza;
                this.cue_estado = (int?)activo;
                this.crea_usr = "admin";
                this.crea_fecha = DateTime.Now;
                this.mod_usr = "admin";
                this.mod_fecha = DateTime.Now;
            }*/
        }


        #endregion

        #region Methods



        public PropertyInfo[] GetProperties()
        {
            return this.GetType().GetProperties();
        }

        public List<Cuenta> GetStruc()
        {
            return new List<Cuenta>();
        }
        
        #endregion


    }
}

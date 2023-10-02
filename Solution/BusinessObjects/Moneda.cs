using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace BusinessObjects
{
    public class Moneda
    {
        #region Properties

        [Data(key = true, auto = true)]
        public int mon_codigo { get; set; }
        [Data(originalkey = true)]
        public int mon_codigo_key { get; set; }      
        [Data(key = true)]
        public int mon_empresa { get; set; }
        [Data(originalkey = true)]
        public int mon_empresa_key { get; set; }
        public string mon_id { get; set; }
        public string mon_nombre { get; set; }
        public decimal? mon_cot_compra { get; set; }
        public decimal? mon_cot_venta { get; set; }
        public int? mon_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }
              
        #endregion

        #region Constructors


        public Moneda()
        {
        }

        public Moneda(int codigo, int empresa, string id, string nombre, decimal? cot_compra, decimal? cot_venta, int? estado, string crea_usr, DateTime? crea_fecha, string mod_usr, DateTime? mod_fecha)
        {                
            this.mon_codigo =codigo;    	
            this.mon_codigo_key =codigo;       
            this.mon_empresa =empresa;    
            this.mon_empresa_key =empresa;
            this.mon_id =id;
            this.mon_nombre =nombre;
            this.mon_cot_compra = cot_compra;
            this.mon_cot_venta = cot_venta;
            this.mon_estado =estado;
            this.crea_usr =crea_usr;
            this.crea_fecha =crea_fecha;
            this.mod_usr =mod_usr;
            this.mod_fecha =mod_fecha;
       }

        public Moneda(IDataReader reader)
        {
            this.mon_codigo = (int)reader["mon_codigo"];
            this.mon_codigo_key = (int)reader["mon_codigo"];
            this.mon_empresa = (int)reader["mon_empresa"];
            this.mon_empresa_key = (int)reader["mon_empresa"];
            this.mon_id = reader["mon_id"].ToString();
            this.mon_nombre = reader["mon_nombre"].ToString();
            this.mon_cot_compra = (reader["mon_cot_compra"] != DBNull.Value) ? (decimal?)reader["mon_cot_compra"] : null;
            this.mon_cot_venta = (reader["mon_cot_venta"] != DBNull.Value) ? (decimal?)reader["mon_cot_venta"] : null;
            this.mon_estado = (reader["mon_estado"] != DBNull.Value) ? (int?)reader["mon_estado"] : null;
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vCuentasSaldos
    {
        public int cue_codigo { get; set; }     
        public int cue_empresa { get; set; }
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
        public string crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }

        [Data(nosql = true)]
        public decimal? cue_saldo { get; set; }

        #region Constructors


        public vCuentasSaldos()
        {

        }
        public vCuentasSaldos(IDataReader reader)
        {
            this.cue_codigo = (int)reader["cue_codigo"];         
            this.cue_empresa = (int)reader["cue_empresa"];           
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

        #endregion

        public string GetSQL()
        {
            string sql = 
                        " SELECT * " +
                        " FROM cuenta ";
            return sql;
        }

        public List<vCuentasSaldos> GetStruc()
        {
            return new List<vCuentasSaldos>();
        }


    }
}

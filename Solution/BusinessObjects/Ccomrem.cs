using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Ccomrem
    {
        #region Properties

        [Data(key = true)]
        public Int32 crem_empresa { get; set; }
        [Data(originalkey = true)]
        public Int32 crem_empresa_key { get; set; }
        [Data(key = true)]
        public Int64 crem_comprobante { get; set; }
        [Data(originalkey = true)]
        public Int64 crem_comprobante_key { get; set; }
        public Int64? crem_factura { get; set; }
        public DateTime? crem_trasladoini { get; set; }
        public DateTime? crem_trasladofin { get; set; }
        public String crem_motivo { get; set; }
        public String crem_nroaduana { get; set; }
        public Int32? crem_remitente { get; set; }
        public String crem_ciruc_rem { get; set; }
        public String crem_nombres_rem { get; set; }
        public String crem_direccion_rem { get; set; }
        public Int32? crem_destinatario { get; set; }
        public String crem_ciruc_des { get; set; }
        public String crem_nombres_des { get; set; }
        public String crem_direccion_des { get; set; }
        public Int32? crem_chofer { get; set; }
        public String crem_ciruc_cho { get; set; }
        public String crem_nombres_cho { get; set; }
        public String crem_placa { get; set; }
        public String crea_usr { get; set; }
        public DateTime? crea_fecha { get; set; }
        public String mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        #endregion

        #region Constructors


        public Ccomrem()
        {
        }

        public Ccomrem(Int32 crem_empresa, Int64 crem_comprobante, Int64 crem_factura, DateTime crem_trasladoini, DateTime crem_trasladofin, String crem_motivo, String crem_nroaduana, Int32 crem_remitente, String crem_ciruc_rem, String crem_nombres_rem, String crem_direccion_rem, Int32 crem_destinatario, String crem_ciruc_des, String crem_nombres_des, String crem_direccion_des, Int32? crem_chofer, String crem_ciruc_cho, String crem_nombres_cho, String crem_placa, String crea_usr, DateTime crea_fecha, String mod_usr, DateTime mod_fecha)
        {
            this.crem_empresa = crem_empresa;
            this.crem_comprobante = crem_comprobante;
            this.crem_factura = crem_factura;
            this.crem_trasladoini = crem_trasladoini;
            this.crem_trasladofin = crem_trasladofin;
            this.crem_motivo = crem_motivo;
            this.crem_nroaduana = crem_nroaduana;
            this.crem_remitente = crem_remitente;
            this.crem_ciruc_rem = crem_ciruc_rem;
            this.crem_nombres_rem = crem_nombres_rem;
            this.crem_direccion_rem = crem_direccion_rem;
            this.crem_destinatario = crem_destinatario;
            this.crem_ciruc_des = crem_ciruc_des;
            this.crem_nombres_des = crem_nombres_des;
            this.crem_direccion_des = crem_direccion_des;
            this.crem_chofer = crem_chofer;
            this.crem_ciruc_cho = crem_ciruc_cho;
            this.crem_nombres_cho = crem_nombres_cho;
            this.crem_placa = crem_placa;
            this.crea_usr = crea_usr;
            this.crea_fecha = crea_fecha;
            this.mod_usr = mod_usr;
            this.mod_fecha = mod_fecha;


        }

        public Ccomrem(IDataReader reader)
        {
            this.crem_empresa = (Int32)reader["crem_empresa"];
            this.crem_comprobante = (Int64)reader["crem_comprobante"];
            this.crem_factura = (reader["crem_factura"] != DBNull.Value) ? (Int64?)reader["crem_factura"] : null;
            this.crem_trasladoini = (reader["crem_trasladoini"] != DBNull.Value) ? (DateTime?)reader["crem_trasladoini"] : null;
            this.crem_trasladofin = (reader["crem_trasladofin"] != DBNull.Value) ? (DateTime?)reader["crem_trasladofin"] : null;
            this.crem_motivo = reader["crem_motivo"].ToString();
            this.crem_nroaduana = reader["crem_nroaduana"].ToString();
            this.crem_remitente = (reader["crem_remitente"] != DBNull.Value) ? (Int32?)reader["crem_remitente"] : null;
            this.crem_ciruc_rem = reader["crem_ciruc_rem"].ToString();
            this.crem_nombres_rem = reader["crem_nombres_rem"].ToString();
            this.crem_direccion_rem = reader["crem_direccion_rem"].ToString();
            this.crem_destinatario = (reader["crem_destinatario"] != DBNull.Value) ? (Int32?)reader["crem_destinatario"] : null;
            this.crem_ciruc_des = reader["crem_ciruc_des"].ToString();
            this.crem_nombres_des = reader["crem_nombres_des"].ToString();
            this.crem_direccion_des = reader["crem_direccion_des"].ToString();
            this.crem_chofer = (reader["crem_chofer"] != DBNull.Value) ? (Int32?)reader["crem_chofer"] : null;
            this.crem_ciruc_cho = reader["crem_ciruc_cho"].ToString();
            this.crem_nombres_cho = reader["crem_nombres_cho"].ToString();
            this.crem_placa = reader["crem_placa"].ToString();
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Ccomrem(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object crem_empresa = null;
                object crem_comprobante = null;
                object crem_factura = null;
                object crem_trasladoini = null;
                object crem_trasladofin = null;
                object crem_motivo = null;
                object crem_nroaduana = null;
                object crem_remitente = null;
                object crem_ciruc_rem = null;
                object crem_nombres_rem = null;
                object crem_direccion_rem = null;
                object crem_destinatario = null;
                object crem_ciruc_des = null;
                object crem_nombres_des = null;
                object crem_direccion_des = null;
                object crem_chofer = null;
                object crem_ciruc_cho = null;
                object crem_nombres_cho = null;
                object crem_placa = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("crem_empresa", out crem_empresa);
                tmp.TryGetValue("crem_comprobante", out crem_comprobante);
                tmp.TryGetValue("crem_factura", out crem_factura);
                tmp.TryGetValue("crem_trasladoini", out crem_trasladoini);
                tmp.TryGetValue("crem_trasladofin", out crem_trasladofin);
                tmp.TryGetValue("crem_motivo", out crem_motivo);
                tmp.TryGetValue("crem_nroaduana", out crem_nroaduana);
                tmp.TryGetValue("crem_remitente", out crem_remitente);
                tmp.TryGetValue("crem_ciruc_rem", out crem_ciruc_rem);
                tmp.TryGetValue("crem_nombres_rem", out crem_nombres_rem);
                tmp.TryGetValue("crem_direccion_rem", out crem_direccion_rem);
                tmp.TryGetValue("crem_destinatario", out crem_destinatario);
                tmp.TryGetValue("crem_ciruc_des", out crem_ciruc_des);
                tmp.TryGetValue("crem_nombres_des", out crem_nombres_des);
                tmp.TryGetValue("crem_direccion_des", out crem_direccion_des);
                tmp.TryGetValue("crem_chofer", out crem_chofer);
                tmp.TryGetValue("crem_ciruc_cho", out crem_ciruc_cho);
                tmp.TryGetValue("crem_nombres_cho", out crem_nombres_cho);
                tmp.TryGetValue("crem_placa", out crem_placa);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);


                this.crem_empresa = (Int32)Conversiones.GetValueByType(crem_empresa, typeof(Int32));
                this.crem_comprobante = (Int64)Conversiones.GetValueByType(crem_comprobante, typeof(Int64));
                this.crem_factura = (Int64?)Conversiones.GetValueByType(crem_factura, typeof(Int64?));
                this.crem_trasladoini = (DateTime?)Conversiones.GetValueByType(crem_trasladoini, typeof(DateTime?));
                this.crem_trasladofin = (DateTime?)Conversiones.GetValueByType(crem_trasladofin, typeof(DateTime?));
                this.crem_motivo = (String)Conversiones.GetValueByType(crem_motivo, typeof(String));
                this.crem_nroaduana = (String)Conversiones.GetValueByType(crem_nroaduana, typeof(String));
                this.crem_remitente = (Int32?)Conversiones.GetValueByType(crem_remitente, typeof(Int32?));
                this.crem_ciruc_rem = (String)Conversiones.GetValueByType(crem_ciruc_rem, typeof(String));
                this.crem_nombres_rem = (String)Conversiones.GetValueByType(crem_nombres_rem, typeof(String));
                this.crem_direccion_rem = (String)Conversiones.GetValueByType(crem_direccion_rem, typeof(String));
                this.crem_destinatario = (Int32?)Conversiones.GetValueByType(crem_destinatario, typeof(Int32?));
                this.crem_ciruc_des = (String)Conversiones.GetValueByType(crem_ciruc_des, typeof(String));
                this.crem_nombres_des = (String)Conversiones.GetValueByType(crem_nombres_des, typeof(String));
                this.crem_direccion_des = (String)Conversiones.GetValueByType(crem_direccion_des, typeof(String));
                this.crem_chofer = (Int32?)Conversiones.GetValueByType(crem_chofer, typeof(Int32?));
                this.crem_ciruc_cho = (String)Conversiones.GetValueByType(crem_ciruc_cho, typeof(String));
                this.crem_nombres_cho = (String)Conversiones.GetValueByType(crem_nombres_cho, typeof(String));
                this.crem_placa = (String)Conversiones.GetValueByType(crem_placa, typeof(String));
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

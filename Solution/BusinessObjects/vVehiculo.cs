using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vVehiculo
    {
        public Int32? veh_codigo { get; set; }
        public Int32? veh_empresa { get; set; }
        public string veh_disco { get; set; }       
        public string veh_nombre { get; set; }
        public string veh_id { get; set; }
        public string veh_placa { get; set; }
        public int? veh_duenio { get; set; }
        public int? veh_estado { get; set; }
        public int? veh_chofer1 { get; set; }
        public int? veh_chofer2 { get; set; }
        public int? veh_empresa_cho2 { get; set; }
        public int? veh_empresa_cho1 { get; set; }       
        public string veh_nombreduenio { get; set; }
        public string veh_apellidoduenio { get; set; }
        public string veh_ceduladuenio { get; set; }
        public string veh_idduenio { get; set; }
        public string veh_nombrechofer1 { get; set; }
        public string veh_apellidochofer1 { get; set; }
        public string veh_cedulachofer1 { get; set; }
        public string veh_idchofer1 { get; set; }
        public string veh_nombrechofer2 { get; set; }
        public string veh_apellidochofer2 { get; set; }
        public string veh_cedulachofer2 { get; set; }
        public string veh_idchofer2 { get; set; }
        #region Constructors


        public vVehiculo()
        {

        }



        public vVehiculo(IDataReader reader)
        {
            this.veh_codigo = (reader["veh_codigo"] != DBNull.Value) ? (int?)reader["veh_codigo"] : null;
            this.veh_empresa = (reader["veh_empresa"] != DBNull.Value) ? (int?)reader["veh_empresa"] : null;
            this.veh_disco = (reader["veh_disco"] != DBNull.Value) ? (string)reader["veh_disco"] : null;
            this.veh_nombre = (reader["veh_nombre"] != DBNull.Value) ? (string)reader["veh_nombre"] : null;
            this.veh_id = (reader["veh_id"] != DBNull.Value) ? (string)reader["veh_id"] : null;
            this.veh_placa = (reader["veh_placa"] != DBNull.Value) ? (string)reader["veh_placa"] : null;
            this.veh_duenio = (reader["veh_duenio"] != DBNull.Value) ? (int?)reader["veh_duenio"] : null;
            this.veh_estado = (reader["veh_estado"] != DBNull.Value) ? (int?)reader["veh_estado"] : null;
            this.veh_chofer1 = (reader["veh_chofer1"] != DBNull.Value) ? (int?)reader["veh_chofer1"] : null;
            this.veh_chofer2 = (reader["veh_chofer2"] != DBNull.Value) ? (int?)reader["veh_chofer2"] : null;
            this.veh_empresa_cho2 = (reader["veh_empresa_cho2"] != DBNull.Value) ? (int?)reader["veh_empresa_cho2"] : null;
            this.veh_empresa_cho1 = (reader["veh_empresa_cho1"] != DBNull.Value) ? (int?)reader["veh_empresa_cho1"] : null;           
            this.veh_nombreduenio = (reader["veh_nombreduenio"] != DBNull.Value) ? (string)reader["veh_nombreduenio"] : null;
            this.veh_apellidoduenio = (reader["veh_apellidoduenio"] != DBNull.Value) ? (string)reader["veh_apellidoduenio"] : null;
            this.veh_ceduladuenio = (reader["veh_ceduladuenio"] != DBNull.Value) ? (string)reader["veh_ceduladuenio"] : null;
            this.veh_idduenio = (reader["veh_idduenio"] != DBNull.Value) ? (string)reader["veh_idduenio"] : null;
            this.veh_nombrechofer1 = (reader["veh_nombrechofer1"] != DBNull.Value) ? (string)reader["veh_nombrechofer1"] : null;
            this.veh_apellidochofer1 = (reader["veh_apellidochofer1"] != DBNull.Value) ? (string)reader["veh_apellidochofer1"] : null;
            this.veh_cedulachofer1 = (reader["veh_cedulachofer1"] != DBNull.Value) ? (string)reader["veh_cedulachofer1"] : null;
            this.veh_idchofer1 = (reader["veh_idchofer1"] != DBNull.Value) ? (string)reader["veh_idchofer1"] : null;
            this.veh_nombrechofer2 = (reader["veh_nombrechofer2"] != DBNull.Value) ? (string)reader["veh_nombrechofer2"] : null;
            this.veh_apellidochofer2 = (reader["veh_apellidochofer2"] != DBNull.Value) ? (string)reader["veh_apellidochofer2"] : null;
            this.veh_cedulachofer2 = (reader["veh_cedulachofer2"] != DBNull.Value) ? (string)reader["veh_cedulachofer2"] : null;
            this.veh_idchofer2 = (reader["veh_idchofer2"] != DBNull.Value) ? (string)reader["veh_idchofer2"] : null;
           
        }

        #endregion

        public string GetSQL()
        {
            string sql = "SELECT    veh_codigo,"+
		                            "veh_empresa, "+
		                            "veh_disco,"+
		                            "veh_nombre,"+
		                            "veh_id, "+
		                            "veh_placa,"+		                            
		                            "veh_duenio, "+
		                            "veh_estado, "+
		                            "veh_chofer1, "+
		                            "veh_chofer2, "+
		                            "veh_empresa_cho2, "+
		                            "veh_empresa_cho1, "+		                           
		                            "socio.per_nombres   veh_nombreduenio,"+
		                            "socio.per_apellidos  veh_apellidoduenio,"+
		                            "socio.per_ciruc veh_ceduladuenio,"+
		                            "socio.per_id veh_idduenio,"+
		                            "chofer1.per_nombres  veh_nombrechofer1,"+
		                            "chofer1.per_apellidos  veh_apellidochofer1, "+
		                            "chofer1.per_ciruc veh_cedulachofer1, "+
		                            "chofer1.per_id veh_idchofer1,"+
		                            "chofer2.per_nombres  veh_nombrechofer2,"+
		                            "chofer2.per_apellidos  veh_apellidochofer2,"+
		                            "chofer2.per_ciruc veh_cedulachofer2,"+
		                            "chofer2.per_id veh_idchofer2 "+
			                        "FROM vehiculo "+
			                            "inner join persona socio on socio.per_codigo=veh_duenio and socio.per_empresa=veh_empresa "+
                                        "left join persona chofer1 on chofer1.per_codigo=veh_chofer1 and chofer1.per_empresa=veh_empresa_cho1 " +
			                            "left join persona chofer2 on chofer2.per_codigo=veh_chofer2 and chofer2.per_empresa=veh_empresa_cho2 "	;


            return sql;
        }


        public List<vVehiculo> GetStruc()
        {
            return new List<vVehiculo>();
        }


    }
}

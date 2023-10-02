using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Dcalculoprecio
    {
        #region Properties

    	[Data(key = true)]
	public Int32 dcpr_empresa { get; set; }
	[Data(originalkey = true)]
	public Int32 dcpr_empresa_key { get; set; }
	[Data(key = true)]
    public Int64 dcpr_comprobante { get; set; }
	[Data(originalkey = true)]
    public Int64 dcpr_comprobante_key { get; set; }
	[Data(key = true)]
	public Int32 dcpr_dcomdoc { get; set; }
	[Data(originalkey = true)]
	public Int32 dcpr_dcomdoc_key { get; set; }
    [Data(key = true, auto = true)]
	public Int32 dcpr_secuencia { get; set; }
	[Data(originalkey = true)]
	public Int32 dcpr_secuencia_key { get; set; }
	public String dcpr_nombre { get; set; }
	public Decimal? dcpr_indice { get; set; }
	public Decimal? dcpr_valor { get; set; }
	public Decimal? dcpr_peso { get; set; }
	public Decimal? dcpr_indicedigitado { get; set; }
	public String crea_usr { get; set; }
	public DateTime? crea_fecha { get; set; }
	public String mod_usr { get; set; }
	public DateTime? mod_fecha { get; set; }

              
        #endregion

        #region Constructors


        public  Dcalculoprecio()
        {
        }

        public  Dcalculoprecio( Int32 dcpr_empresa,Int32 dcpr_comprobante,Int32 dcpr_dcomdoc,Int32 dcpr_secuencia,String dcpr_nombre,Decimal dcpr_indice,Decimal dcpr_valor,Decimal dcpr_peso,Decimal dcpr_indicedigitado,String crea_usr,DateTime crea_fecha,String mod_usr,DateTime mod_fecha)
        {                
    	this.dcpr_empresa = dcpr_empresa;
	this.dcpr_comprobante = dcpr_comprobante;
	this.dcpr_dcomdoc = dcpr_dcomdoc;
	this.dcpr_secuencia = dcpr_secuencia;
	this.dcpr_nombre = dcpr_nombre;
	this.dcpr_indice = dcpr_indice;
	this.dcpr_valor = dcpr_valor;
	this.dcpr_peso = dcpr_peso;
	this.dcpr_indicedigitado = dcpr_indicedigitado;
	this.crea_usr = crea_usr;
	this.crea_fecha = crea_fecha;
	this.mod_usr = mod_usr;
	this.mod_fecha = mod_fecha;

           
       }

        public  Dcalculoprecio(IDataReader reader)
        {
    	this.dcpr_empresa = (Int32)reader["dcpr_empresa"];
	this.dcpr_comprobante = (Int64)reader["dcpr_comprobante"];
	this.dcpr_dcomdoc = (Int32)reader["dcpr_dcomdoc"];
	this.dcpr_secuencia = (Int32)reader["dcpr_secuencia"];
	this.dcpr_nombre = reader["dcpr_nombre"].ToString();
	this.dcpr_indice = (reader["dcpr_indice"] != DBNull.Value) ? (Decimal?)reader["dcpr_indice"] : null;
	this.dcpr_valor = (reader["dcpr_valor"] != DBNull.Value) ? (Decimal?)reader["dcpr_valor"] : null;
	this.dcpr_peso = (reader["dcpr_peso"] != DBNull.Value) ? (Decimal?)reader["dcpr_peso"] : null;
	this.dcpr_indicedigitado = (reader["dcpr_indicedigitado"] != DBNull.Value) ? (Decimal?)reader["dcpr_indicedigitado"] : null;
	this.crea_usr = reader["crea_usr"].ToString();
	this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
	this.mod_usr = reader["mod_usr"].ToString();
	this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;

        }


        public Dcalculoprecio(object objeto)
        {            
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                	object dcpr_empresa = null;
	object dcpr_comprobante = null;
	object dcpr_dcomdoc = null;
	object dcpr_secuencia = null;
	object dcpr_nombre = null;
	object dcpr_indice = null;
	object dcpr_valor = null;
	object dcpr_peso = null;
	object dcpr_indicedigitado = null;
	object crea_usr = null;
	object crea_fecha = null;
	object mod_usr = null;
	object mod_fecha = null;


                	tmp.TryGetValue("dcpr_empresa", out dcpr_empresa);
	tmp.TryGetValue("dcpr_comprobante", out dcpr_comprobante);
	tmp.TryGetValue("dcpr_dcomdoc", out dcpr_dcomdoc);
	tmp.TryGetValue("dcpr_secuencia", out dcpr_secuencia);
	tmp.TryGetValue("dcpr_nombre", out dcpr_nombre);
	tmp.TryGetValue("dcpr_indice", out dcpr_indice);
	tmp.TryGetValue("dcpr_valor", out dcpr_valor);
	tmp.TryGetValue("dcpr_peso", out dcpr_peso);
	tmp.TryGetValue("dcpr_indicedigitado", out dcpr_indicedigitado);
	tmp.TryGetValue("crea_usr", out crea_usr);
	tmp.TryGetValue("crea_fecha", out crea_fecha);
	tmp.TryGetValue("mod_usr", out mod_usr);
	tmp.TryGetValue("mod_fecha", out mod_fecha);


                	this.dcpr_empresa = (Int32)Conversiones.GetValueByType(dcpr_empresa, typeof(Int32));
	this.dcpr_comprobante = (Int64)Conversiones.GetValueByType(dcpr_comprobante, typeof(Int64));
	this.dcpr_dcomdoc = (Int32)Conversiones.GetValueByType(dcpr_dcomdoc, typeof(Int32));
	this.dcpr_secuencia = (Int32)Conversiones.GetValueByType(dcpr_secuencia, typeof(Int32));
	this.dcpr_nombre = (String)Conversiones.GetValueByType(dcpr_nombre, typeof(String));
	this.dcpr_indice = (Decimal?)Conversiones.GetValueByType(dcpr_indice, typeof(Decimal?));
	this.dcpr_valor = (Decimal?)Conversiones.GetValueByType(dcpr_valor, typeof(Decimal?));
	this.dcpr_peso = (Decimal?)Conversiones.GetValueByType(dcpr_peso, typeof(Decimal?));
	this.dcpr_indicedigitado = (Decimal?)Conversiones.GetValueByType(dcpr_indicedigitado, typeof(Decimal?));
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

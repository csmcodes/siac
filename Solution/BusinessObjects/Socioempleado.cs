using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Functions;

namespace BusinessObjects
{
    public class Socioempleado
    {
        #region Properties
        [Data(key = true)]
        public int soc_persona { get; set; }
        [Data(originalkey = true)]
        public int soc_persona_key { get; set; }      
        [Data(key = true)]
        public int soc_empresa { get; set; }
        [Data(originalkey = true)]
        public int soc_empresa_key { get; set; }       
        public string soc_foto{ get; set; }
        public DateTime? soc_fechanacimiento{ get; set; }
        public int? soc_paisnacimiento{ get; set; }
        public int? soc_provincianacimiento{ get; set; }
        public int? soc_cantonnacimiento{ get; set; }
        public int? soc_estadocivil{ get; set; }
        public int? soc_cargas{ get; set; }
        public int? soc_inscripcion{ get; set; }
        public DateTime? soc_fechainscripcion { get; set; }
        public DateTime? soc_fechasalida { get; set; }
        public string soc_razonsalida { get; set; }
        public string soc_lugartrabajo { get; set; }
        public int? soc_cargotrabajo{ get; set; }
        public int? soc_departamento{ get; set; }
        public string soc_direcciontrabajo { get; set; }
        public string soc_telefonotrabajo { get; set; }
        public string soc_faxtrabajo { get; set; }
        public string soc_nroiess { get; set; }
        public int? soc_banco{ get; set; }
        public string soc_tipocuenta { get; set; }
        public string soc_nrocuenta{ get; set; }
        public int? soc_nivelinstruccion{ get; set; }
        public string soc_postgrado { get; set; }
        public string soc_doctorado { get; set; }
        public string soc_reconocimientos { get; set; }
        public string soc_titulos { get; set; }
        public int? soc_profesion{ get; set; }
        public DateTime? soc_fechagrado { get; set; }
        public string soc_institucion { get; set; }
        public string soc_conadis { get; set; }
        public int? soc_empresaconyuge{ get; set; }
        public int? soc_codigoconyuge { get; set; }
        public int? soc_estado { get; set; }
        [Data(noupdate = true)]
        public string crea_usr { get; set; }
        [Data(noupdate = true)]
        public DateTime? crea_fecha { get; set; }
        public string mod_usr { get; set; }
        public DateTime? mod_fecha { get; set; }


        [Data(nosql = true, tablaref = "persona", camporef = "per_nombres", foreign = "soc_persona", keyref = "per_codigo")]
        public string soc_nombre { get; set; }
        [Data(nosql = true, tablaref = "persona", camporef = "per_apellidos", foreign = "soc_persona", keyref = "per_codigo")]
        public string soc_apellido { get; set; }
        #endregion

        #region Constructors


        public Socioempleado()
        {

        }

        public Socioempleado(int codigo, int empresa,string foto,DateTime? fechanacimiento,int? paisnacimiento,int? provincianacimiento,int? cantonnacimiento,int? estadocivil,int? cargas,int? inscripcion,DateTime? fechainscripcion ,DateTime? fechasalida ,string razonsalida ,string lugartrabajo ,int? cargotrabajo,int? departamento,string direcciontrabajo ,string telefonotrabajo ,string faxtrabajo ,string nroiess ,int? banco,string tipocuenta ,string nrocuenta,int? nivelinstruccion,string postgrado ,string doctorado ,string reconocimientos ,string titulos ,int? profesion,DateTime? fechagrado ,string institucion ,string conadis ,int? empresaconyuge,int? codigoconyuge ,int? estado , string crea_usr ,DateTime? crea_fecha ,string mod_usr ,DateTime? mod_fecha )
        {        
        this.soc_persona=codigo;
        this.soc_persona_key = codigo;
        this.soc_empresa=empresa;
        this.soc_empresa_key = empresa;
        this.soc_foto=foto;
        this.soc_fechanacimiento=fechanacimiento;
        this.soc_paisnacimiento=paisnacimiento;
        this.soc_provincianacimiento=soc_provincianacimiento;
        this.soc_cantonnacimiento=cantonnacimiento;
        this.soc_estadocivil=estadocivil;
        this.soc_cargas=cargas;
        this.soc_inscripcion=inscripcion;
        this.soc_fechainscripcion=fechainscripcion;
        this.soc_fechasalida=fechasalida;
        this.soc_razonsalida=razonsalida;
        this.soc_lugartrabajo=lugartrabajo;
        this.soc_cargotrabajo=cargotrabajo;
        this.soc_departamento = departamento;
        this.soc_direcciontrabajo=direcciontrabajo;
        this.soc_telefonotrabajo=telefonotrabajo;
        this.soc_faxtrabajo=faxtrabajo;
        this.soc_nroiess=nroiess;
        this.soc_banco=banco;
        this.soc_tipocuenta=tipocuenta;
        this.soc_nrocuenta=nrocuenta;
        this.soc_nivelinstruccion=nivelinstruccion;
        this.soc_postgrado=postgrado;
        this.soc_doctorado=doctorado;
        this.soc_reconocimientos=reconocimientos;
        this.soc_titulos=titulos;
        this.soc_profesion=profesion;
        this.soc_fechagrado=fechagrado;
        this.soc_institucion=institucion;
        this.soc_conadis=conadis;
        this.soc_empresaconyuge=empresaconyuge;
        this.soc_codigoconyuge=codigoconyuge;
        this.soc_estado=estado;
        this.crea_usr=crea_usr;
        this.crea_fecha=crea_fecha;
        this.mod_usr=mod_usr;
        this.mod_fecha = mod_fecha;
        }

        public Socioempleado(IDataReader reader)
        {
            this.soc_persona = (int)reader["soc_persona"];
            this.soc_persona_key=(int)reader["soc_persona"];
            this.soc_empresa = (int)reader["soc_empresa"];
            this.soc_empresa_key=(int)reader["soc_empresa"];
            this.soc_foto = reader["soc_foto"].ToString();
            this.soc_fechanacimiento = (reader["soc_fechanacimiento"] != DBNull.Value) ? (DateTime?)reader["soc_fechanacimiento"] : null;
            this.soc_paisnacimiento = (reader["soc_paisnacimiento"] != DBNull.Value) ? (int?)reader["soc_paisnacimiento"] : null;
            this.soc_provincianacimiento = (reader["soc_provincianacimiento"] != DBNull.Value) ? (int?)reader["soc_provincianacimiento"] : null;
            this.soc_cantonnacimiento = (reader["soc_cantonnacimiento"] != DBNull.Value) ? (int?)reader["soc_cantonnacimiento"] : null;
            this.soc_estadocivil = (reader["soc_estadocivil"] != DBNull.Value) ? (int?)reader["soc_estadocivil"] : null;
            this.soc_cargas = (reader["soc_cargas"] != DBNull.Value) ? (int?)reader["soc_cargas"] : null;
            this.soc_inscripcion = (reader["soc_inscripcion"] != DBNull.Value) ? (int?)reader["soc_inscripcion"] : null;
            this.soc_fechainscripcion = (reader["soc_fechainscripcion"] != DBNull.Value) ? (DateTime?)reader["soc_fechainscripcion"] : null;
            this.soc_fechasalida = (reader["soc_fechasalida"] != DBNull.Value) ? (DateTime?)reader["soc_fechasalida"] : null;
            this.soc_razonsalida = reader["soc_razonsalida"].ToString();
            this.soc_lugartrabajo = reader["soc_lugartrabajo"].ToString();
            this.soc_cargotrabajo = (reader["soc_cargotrabajo"] != DBNull.Value) ? (int?)reader["soc_cargotrabajo"] : null;
            this.soc_departamento = (reader["soc_departamento"] != DBNull.Value) ? (int?)reader["soc_departamento"] : null;
            this.soc_direcciontrabajo = reader["soc_direcciontrabajo"].ToString();
            this.soc_telefonotrabajo = reader["soc_telefonotrabajo"].ToString();
            this.soc_faxtrabajo = reader["soc_faxtrabajo"].ToString();
            this.soc_nroiess = reader["soc_nroiess"].ToString();
            this.soc_banco = (reader["soc_banco"] != DBNull.Value) ? (int?)reader["soc_banco"] : null;
            this.soc_tipocuenta = reader["soc_tipocuenta"].ToString();
            this.soc_nrocuenta = reader["soc_nrocuenta"].ToString();
            this.soc_nivelinstruccion = (reader["soc_nivelinstruccion"] != DBNull.Value) ? (int?)reader["soc_nivelinstruccion"] : null;
            this.soc_postgrado = reader["soc_postgrado"].ToString();
            this.soc_doctorado = reader["soc_doctorado"].ToString();
            this.soc_reconocimientos = reader["soc_reconocimientos"].ToString();
            this.soc_titulos = reader["soc_titulos"].ToString();
            this.soc_profesion = (reader["soc_profesion"] != DBNull.Value) ? (int?)reader["soc_profesion"] : null;
            this.soc_fechagrado = (reader["soc_fechagrado"] != DBNull.Value) ? (DateTime?)reader["soc_fechagrado"] : null;
            this.soc_institucion = reader["soc_institucion"].ToString();
            this.soc_conadis = reader["soc_conadis"].ToString();
            this.soc_empresaconyuge = (reader["soc_empresaconyuge"] != DBNull.Value) ? (int?)reader["soc_empresaconyuge"] : null;
            this.soc_codigoconyuge = (reader["soc_codigoconyuge"] != DBNull.Value) ? (int?)reader["soc_codigoconyuge"] : null;
            this.soc_estado = (reader["soc_estado"] != DBNull.Value) ? (int?)reader["soc_estado"] : null;         
            this.crea_usr = reader["crea_usr"].ToString();
            this.crea_fecha = (reader["crea_fecha"] != DBNull.Value) ? (DateTime?)reader["crea_fecha"] : null;
            this.mod_usr = reader["mod_usr"].ToString();
            this.mod_fecha = (reader["mod_fecha"] != DBNull.Value) ? (DateTime?)reader["mod_fecha"] : null;
            this.soc_nombre = reader["soc_nombre"].ToString();
            this.soc_apellido = reader["soc_apellido"].ToString();
        }
        public Socioempleado(object objeto)
        {
            if (objeto != null)
            {
                Dictionary<string, object> tmp = (Dictionary<string, object>)objeto;
                object soc_persona = null;
                object soc_empresa = null;
                object soc_persona_key = null;
                object soc_empresa_key = null;
                object soc_foto = null;
                object soc_fechanacimiento = null;
                object soc_paisnacimiento = null;
                object soc_provincianacimiento = null;
                object soc_cantonnacimiento = null;
                object soc_estadocivil = null;
                object soc_cargas = null;
                object soc_inscripcion = null;
                object soc_fechainscripcion = null;
                object soc_fechasalida = null;
                object soc_razonsalida = null;
                object soc_lugartrabajo = null;
                object soc_cargotrabajo = null;
                object soc_departamento = null;
                object soc_direcciontrabajo = null;
                object soc_telefonotrabajo = null;
                object soc_faxtrabajo = null;
                object soc_nroiess = null;
                object soc_banco = null;
                object soc_tipocuenta = null;
                object soc_nrocuenta = null;
                object soc_nivelinstruccion = null;
                object soc_postgrado = null;
                object soc_doctorado = null;
                object soc_reconocimientos = null;
                object soc_titulos = null;
                object soc_profesion = null;
                object soc_fechagrado = null;
                object soc_institucion = null;
                object soc_conadis = null;
                object soc_empresaconyuge = null;
                object soc_codigoconyuge = null;
                object soc_estado = null;
                object crea_usr = null;
                object crea_fecha = null;
                object mod_usr = null;
                object mod_fecha = null;


                tmp.TryGetValue("soc_persona", out soc_persona);
                tmp.TryGetValue("soc_empresa", out soc_empresa);
                tmp.TryGetValue("soc_persona_key", out soc_persona_key);
                tmp.TryGetValue("soc_empresa_key", out soc_empresa_key);
                tmp.TryGetValue("soc_foto", out soc_foto);
                tmp.TryGetValue("soc_fechanacimiento", out soc_fechanacimiento);
                tmp.TryGetValue("soc_paisnacimiento", out soc_paisnacimiento);
                tmp.TryGetValue("soc_provincianacimiento", out soc_provincianacimiento);
                tmp.TryGetValue("soc_cantonnacimiento", out soc_cantonnacimiento);
                tmp.TryGetValue("soc_estadocivil", out soc_estadocivil);
                tmp.TryGetValue("soc_cargas", out soc_cargas);
                tmp.TryGetValue("soc_inscripcion", out soc_inscripcion);
                tmp.TryGetValue("soc_fechainscripcion", out soc_fechainscripcion);
                tmp.TryGetValue("soc_fechasalida", out soc_fechasalida);
                tmp.TryGetValue("soc_razonsalida", out soc_razonsalida);
                tmp.TryGetValue("soc_lugartrabajo", out soc_lugartrabajo);
                tmp.TryGetValue("soc_cargotrabajo", out soc_cargotrabajo);
                tmp.TryGetValue("soc_departamento", out soc_departamento);
                tmp.TryGetValue("soc_direcciontrabajo", out soc_direcciontrabajo);
                tmp.TryGetValue("soc_telefonotrabajo", out soc_telefonotrabajo);
                tmp.TryGetValue("soc_faxtrabajo", out soc_faxtrabajo);
                tmp.TryGetValue("soc_nroiess", out soc_nroiess);
                tmp.TryGetValue("soc_banco", out soc_banco);
                tmp.TryGetValue("soc_tipocuenta", out soc_tipocuenta);
                tmp.TryGetValue("soc_nrocuenta", out soc_nrocuenta);
                tmp.TryGetValue("soc_nivelinstruccion", out soc_nivelinstruccion);
                tmp.TryGetValue("soc_postgrado", out soc_postgrado);
                tmp.TryGetValue("soc_doctorado", out soc_doctorado);
                tmp.TryGetValue("soc_reconocimientos", out soc_reconocimientos);
                tmp.TryGetValue("soc_titulos", out soc_titulos);
                tmp.TryGetValue("soc_profesion", out soc_profesion);
                tmp.TryGetValue("soc_fechagrado", out soc_fechagrado);
                tmp.TryGetValue("soc_institucion", out soc_institucion);
                tmp.TryGetValue("soc_conadis", out soc_conadis);
                tmp.TryGetValue("soc_empresaconyuge", out soc_empresaconyuge);
                tmp.TryGetValue("soc_codigoconyuge", out soc_codigoconyuge);
                tmp.TryGetValue("soc_estado", out soc_estado);
                tmp.TryGetValue("crea_usr", out crea_usr);
                tmp.TryGetValue("crea_fecha", out crea_fecha);
                tmp.TryGetValue("mod_usr", out mod_usr);
                tmp.TryGetValue("mod_fecha", out mod_fecha);
                if (!string.IsNullOrEmpty(soc_fechanacimiento.ToString()))
                {
                    this.soc_fechanacimiento = (DateTime)Conversiones.GetValueByType(soc_fechanacimiento, typeof(DateTime));
                }
                if (!string.IsNullOrEmpty(soc_fechainscripcion.ToString()))
                {
                    this.soc_fechainscripcion = (DateTime)Conversiones.GetValueByType(soc_fechainscripcion, typeof(DateTime));
                }
                if (!string.IsNullOrEmpty(soc_fechasalida.ToString()))
                {
                    this.soc_fechasalida = (DateTime)Conversiones.GetValueByType(soc_fechasalida, typeof(DateTime));
                }

                if (!string.IsNullOrEmpty(soc_fechagrado.ToString()))
                {
                    this.soc_fechagrado = (DateTime)Conversiones.GetValueByType(soc_fechagrado, typeof(DateTime));
                }




                this.soc_persona = (Int32)Conversiones.GetValueByType(soc_persona, typeof(Int32));
                this.soc_empresa = (Int32)Conversiones.GetValueByType(soc_empresa, typeof(Int32));
                this.soc_persona_key = (Int32)Conversiones.GetValueByType(soc_persona_key, typeof(Int32));
                this.soc_empresa_key = (Int32)Conversiones.GetValueByType(soc_empresa_key, typeof(Int32));
                this.soc_foto = (String)Conversiones.GetValueByType(soc_foto, typeof(String));
                
                this.soc_paisnacimiento = (Int32?)Conversiones.GetValueByType(soc_paisnacimiento, typeof(Int32?));
                this.soc_provincianacimiento = (Int32?)Conversiones.GetValueByType(soc_provincianacimiento, typeof(Int32?));
                this.soc_cantonnacimiento = (Int32?)Conversiones.GetValueByType(soc_cantonnacimiento, typeof(Int32?));
                this.soc_estadocivil = (Int32?)Conversiones.GetValueByType(soc_estadocivil, typeof(Int32?));
                this.soc_cargas = (Int32?)Conversiones.GetValueByType(soc_cargas, typeof(Int32?));
                this.soc_inscripcion = (Int32?)Conversiones.GetValueByType(soc_inscripcion, typeof(Int32?));
             
                
                this.soc_razonsalida = (String)Conversiones.GetValueByType(soc_razonsalida, typeof(String));
                this.soc_lugartrabajo = (String)Conversiones.GetValueByType(soc_lugartrabajo, typeof(String));
                this.soc_cargotrabajo = (Int32?)Conversiones.GetValueByType(soc_cargotrabajo, typeof(Int32?));
                this.soc_departamento = (Int32?)Conversiones.GetValueByType(soc_departamento, typeof(Int32?));
                this.soc_direcciontrabajo = (String)Conversiones.GetValueByType(soc_direcciontrabajo, typeof(String));
                this.soc_telefonotrabajo = (String)Conversiones.GetValueByType(soc_telefonotrabajo, typeof(String));
                this.soc_faxtrabajo = (String)Conversiones.GetValueByType(soc_faxtrabajo, typeof(String));
                this.soc_nroiess = (String)Conversiones.GetValueByType(soc_nroiess, typeof(String));
                this.soc_banco = (Int32?)Conversiones.GetValueByType(soc_banco, typeof(Int32?));
                this.soc_tipocuenta = (String)Conversiones.GetValueByType(soc_tipocuenta, typeof(String));
                this.soc_nrocuenta = (String)Conversiones.GetValueByType(soc_nrocuenta, typeof(String));
                this.soc_nivelinstruccion = (Int32?)Conversiones.GetValueByType(soc_nivelinstruccion, typeof(Int32?));
                this.soc_postgrado = (String)Conversiones.GetValueByType(soc_postgrado, typeof(String));
                this.soc_doctorado = (String)Conversiones.GetValueByType(soc_doctorado, typeof(String));
                this.soc_reconocimientos = (String)Conversiones.GetValueByType(soc_reconocimientos, typeof(String));
                this.soc_titulos = (String)Conversiones.GetValueByType(soc_titulos, typeof(String));
                this.soc_profesion = (Int32?)Conversiones.GetValueByType(soc_profesion, typeof(Int32?));
               
                this.soc_institucion = (String)Conversiones.GetValueByType(soc_institucion, typeof(String));
                this.soc_conadis = (String)Conversiones.GetValueByType(soc_conadis, typeof(String));
                this.soc_empresaconyuge = (Int32?)Conversiones.GetValueByType(soc_empresaconyuge, typeof(Int32?));
                this.soc_codigoconyuge = (Int32?)Conversiones.GetValueByType(soc_codigoconyuge, typeof(Int32?));
                this.soc_estado = (Int32?)Conversiones.GetValueByType(soc_estado, typeof(Int32?));
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

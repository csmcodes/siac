using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vPersona
    {
      
        #region Constructors


        public vPersona()
        {

        }



        public vPersona(IDataReader reader)
        {
           
           
        }

        #endregion

        public string GetSQL()
        {
            string sql = "SELECT DISTINCT  t.per_codigo, t.per_empresa, t.per_ciruc, t.per_tipoid, t.per_nombres, t.per_apellidos, t.per_id, t.per_direccion, t.per_telefono, t.per_celular, t.per_mail, t.per_observacion, t.per_pais, t.per_provincia, t.per_canton, t.per_parroquia, t.per_contribuyente, t.per_contribuyente_especial, t.per_contacto, t.per_contacto_direccion, t.per_contacto_telefono, t.per_razon, t.per_representantelegal, t.per_paginaweb, t.per_genero, t.per_cpersona, t.per_tpersona, t.per_listaprecio, t.per_politica, t.per_retiva, t.per_retfuente, t.per_agente, t.per_bloqueo, t.per_tarjeta, t.per_cupo, t.per_ilimitado, t.per_impuesto, t.per_estado, t.crea_usr, t.crea_fecha, t.mod_usr, t.mod_fecha, t.per_listanombre, t.per_listaid, t.per_politicanombre, t.per_politicaid, t.per_politicadesc, t.per_politicanropagos, t.per_politicadiasplazo, t.per_politicaporpagocon FROM (SELECT ROW_NUMBER() OVER(ORDER BY %orderby%) RowNr, persona.per_codigo,persona.per_empresa,persona.per_ciruc,persona.per_tipoid,persona.per_nombres,persona.per_apellidos,persona.per_id,persona.per_direccion,persona.per_telefono,persona.per_celular,persona.per_mail,persona.per_observacion,persona.per_pais,persona.per_provincia,persona.per_canton,persona.per_parroquia,persona.per_contribuyente,persona.per_contribuyente_especial,persona.per_contacto,persona.per_contacto_direccion,persona.per_contacto_telefono,persona.per_razon,persona.per_representantelegal,persona.per_paginaweb,persona.per_genero,persona.per_cpersona,persona.per_tpersona,persona.per_listaprecio,persona.per_politica,persona.per_retiva,persona.per_retfuente,persona.per_agente,persona.per_bloqueo,persona.per_tarjeta,persona.per_cupo,persona.per_ilimitado,persona.per_impuesto,persona.per_estado,persona.crea_usr,persona.crea_fecha,persona.mod_usr,persona.mod_fecha,listaprecio.lpr_nombre per_listanombre, listaprecio.lpr_id per_listaid,politica.pol_nombre per_politicanombre, politica.pol_id per_politicaid, politica.pol_porc_desc per_politicadesc, politica.pol_nro_pagos per_politicanropagos, politica.pol_dias_plazo per_politicadiasplazo, politica.pol_porc_pago_con per_politicaporpagocon FROM persona  left JOIN listaprecio ON persona.per_listaprecio=listaprecio.lpr_codigo left JOIN politica ON persona.per_politica=politica.pol_codigo left JOIN personaxtipo ON personaxtipo.pxt_persona=persona.per_codigo and  personaxtipo.pxt_empresa=persona.per_empresa  %whereclause%) t WHERE RowNr BETWEEN %desde% AND %hasta% ";


            return sql;
        }

        public string GetSQLTop()
        {
            string sql = "SELECT DISTINCT persona.per_codigo,persona.per_empresa,persona.per_ciruc,persona.per_tipoid,persona.per_nombres,persona.per_apellidos,persona.per_id,persona.per_direccion,persona.per_telefono,persona.per_celular,persona.per_mail,persona.per_observacion,persona.per_pais,persona.per_provincia,persona.per_canton,persona.per_parroquia,persona.per_contribuyente,persona.per_contribuyente_especial,persona.per_contacto,persona.per_contacto_direccion,persona.per_contacto_telefono,persona.per_razon,persona.per_representantelegal,persona.per_paginaweb,persona.per_genero,persona.per_cpersona,persona.per_tpersona,persona.per_listaprecio,persona.per_politica,persona.per_retiva,persona.per_retfuente,persona.per_agente,persona.per_bloqueo,persona.per_tarjeta,persona.per_cupo,persona.per_ilimitado,persona.per_impuesto,persona.per_estado,persona.crea_usr,persona.crea_fecha,persona.mod_usr,persona.mod_fecha,listaprecio.lpr_nombre per_listanombre, listaprecio.lpr_id per_listaid,politica.pol_nombre per_politicanombre, politica.pol_id per_politicaid, politica.pol_porc_desc per_politicadesc, politica.pol_nro_pagos per_politicanropagos, politica.pol_dias_plazo per_politicadiasplazo, politica.pol_porc_pago_con per_politicaporpagocon FROM persona  left JOIN listaprecio ON persona.per_listaprecio=listaprecio.lpr_codigo left JOIN politica ON persona.per_politica=politica.pol_codigo left JOIN personaxtipo ON personaxtipo.pxt_persona=persona.per_codigo and  personaxtipo.pxt_empresa=persona.per_empresa";


            return sql;
        }

        public List<Persona> GetStruc()
        {
            return new List<Persona>();
        }


    }
}

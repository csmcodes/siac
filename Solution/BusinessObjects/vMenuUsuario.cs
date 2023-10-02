using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BusinessObjects
{
    public class vMenuUsuario
    {
        public int men_id { get; set; }
        public string men_nombre { get; set; }
        public string men_formulario { get; set; }
        public int? men_padre { get; set; }
        public string men_imagen { get; set; }
        public int? men_orden { get; set; }
        public int? men_estado { get; set; }

        public int? pxm_agregar { get; set; }
        public int? pxm_modificar { get; set; }
        public int? pxm_eliminar { get; set; }


        public vMenuUsuario()
        {
        }

        public vMenuUsuario(IDataReader reader)
        {
            this.men_id = (int)reader["men_id"];
            this.men_nombre = reader["men_nombre"].ToString();
            this.men_formulario = reader["men_formulario"].ToString();
            this.men_padre = (reader["men_padre"] != DBNull.Value) ? (int?)reader["men_padre"] : null;
            this.men_imagen = reader["men_imagen"].ToString();
            this.men_orden= (reader["men_orden"] != DBNull.Value) ? (int?)reader["men_orden"] : null;
            this.men_estado = (reader["men_estado"] != DBNull.Value) ? (int?)reader["men_estado"] : null;

            this.pxm_agregar = (reader["pxm_agregar"] != DBNull.Value) ? (int?)reader["pxm_agregar"] : null;
            this.pxm_modificar = (reader["pxm_modificar"] != DBNull.Value) ? (int?)reader["pxm_modificar"] : null;
            this.pxm_eliminar = (reader["pxm_eliminar"] != DBNull.Value) ? (int?)reader["pxm_eliminar"] : null;                           
        }

        public string GetSQL()
        {
            string sql = "select menu.* , pxm_agregar, pxm_modificar, pxm_eliminar  " +
                            " from perfilxmenu  " +
                            " INNER JOIN menu ON pxm_menu = men_id " +
                            " INNER JOIN perfil ON pxm_perfil = per_id " +
                            " INNER JOIN usuario ON usr_perfil =per_id";

                            //WHERE usr_id = 'user1' and men_estado =1";

            return sql;
        }


        public List<vMenuUsuario> GetStruc()
        {
            return new List<vMenuUsuario>();
        }
        
    }
}

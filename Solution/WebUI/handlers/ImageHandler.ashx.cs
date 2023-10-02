using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogicLayer;
using BusinessObjects;
using System.IO;


namespace WebUI.handlers
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int sec = Convert.ToInt32(context.Request.QueryString["sec"]);
            int emp = Convert.ToInt32(context.Request.QueryString["emp"]);
            int pro = Convert.ToInt32(context.Request.QueryString["pro"]);
            int car = Convert.ToInt32(context.Request.QueryString["car"]);
            Productocaracteristica pc = ProductocaracteristicaBLL.GetByPK(new Productocaracteristica { prc_empresa = emp, prc_empresa_key = emp, prc_producto = pro, prc_producto_key = pro, prc_caracteristica = car, prc_caracteristica_key = car, prc_secuencia = sec, prc_secuencia_key = sec });
            if (pc!=null)
            {
                context.Response.ContentType = pc.prc_datostipo;

                if (pc.prc_datos != null)//Imagen reside en DB
                {
                    Stream strm = new MemoryStream((byte[])pc.prc_datos);

                    byte[] buffer = new byte[(int)pc.prc_datoslargo];
                    int byteSeq = strm.Read(buffer, 0, (int)pc.prc_datoslargo);

                    while (byteSeq > 0)
                    {
                        context.Response.OutputStream.Write(buffer, 0, byteSeq);
                        byteSeq = strm.Read(buffer, 0, (int)pc.prc_datoslargo);
                    }
                }
                else //Imagen reside en File System
                {

                    context.Response.WriteFile(pc.prc_valor);

                }

            }
            
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
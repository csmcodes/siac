using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Services;
using BusinessObjects;
using BusinessLogicLayer;
//using HtmlObjects;
using Functions;
using System.Web.Script.Serialization;


namespace WebUI.upload
{
    /// <summary>
    /// Summary description for ImgFileUploader
    /// </summary>
    public class ImgFileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {

                string imgstore = Constantes.GetParameter("ImageStore");
                if (string.IsNullOrEmpty(imgstore))
                    imgstore = "FS";//FS:FileSystem  DB:DataBase Definido en paramentros (ImageStore);

                string rootpath = context.Server.MapPath("~");

                string path = rootpath + "UploadFiles"; // context.Server.MapPath("~/UploadFiles");
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                    System.IO.Directory.CreateDirectory(path);




                var file = context.Request.Files[0];
                string nombre;

                if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] files = file.FileName.Split(new char[] { '\\' });
                    nombre = files[files.Length - 1];
                }
                else
                {
                    nombre = file.FileName;
                }


                object obj = new JavaScriptSerializer().Deserialize<object>(context.Request.Form["obj"]);
                Dictionary<string, object> tmp = (Dictionary<string, object>)obj;
                object objeto = null;
                tmp.TryGetValue("objeto", out objeto);
                Productocaracteristica pc = new Productocaracteristica(objeto);

                //Elimina foto anterior
                List<Productocaracteristica> fotos = ProductocaracteristicaBLL.GetAll(new WhereParams("prc_empresa={0} and prc_producto={1} and prc_caracteristica={2}", pc.prc_empresa, pc.prc_producto, 1), "");
                foreach (Productocaracteristica foto in fotos)
                {
                    ProductocaracteristicaBLL.Delete(foto);
                    //Elimina la foto del File System si existe
                    if (File.Exists(foto.prc_valor))
                        File.Delete(foto.prc_valor);
                }




                pc.prc_caracteristica = 1; //1:FOTO;
                

                if (imgstore == "FS")
                {
                    string extension = Path.GetExtension(nombre);                   
                    nombre = Path.Combine(path, DateTime.Now.Ticks.ToString() + extension);
                    file.SaveAs(nombre);
                    pc.prc_valor = nombre;
                    pc.prc_datostipo = file.ContentType;

                }
                if (imgstore == "DB")
                {
                    pc.prc_valor = nombre;
                    int largo = file.ContentLength;
                    byte[] datos = new byte[0];
                    Stream datosstream = file.InputStream;
                    datos = new byte[largo];
                    datosstream.Read(datos, 0, largo);
                    pc.prc_datostipo = file.ContentType;
                    pc.prc_datoslargo = Convert.ToInt32(largo / 1024);
                    pc.prc_datos = datos;
                }
                pc.prc_estado = 1;

                ProductocaracteristicaBLL.InsertIdentity(pc);

                context.Response.Write("ok");




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
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
using Packages;
using System.Threading.Tasks;

namespace WebUI.handlers
{
    /// <summary>
    /// Summary description for XmlFileUploader
    /// </summary>
    public class XmlFileUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.Files.Count > 0)
            {

                object obj = new JavaScriptSerializer().Deserialize<object>(context.Request.Form["obj"]);
                Dictionary<string, object> tmp = (Dictionary<string, object>)obj;
                object objeto = null;
                tmp.TryGetValue("objeto", out objeto);
                JsonObj jobj = new JsonObj(objeto);

                string imgstore = Constantes.GetParameter("ImageStore");
                if (string.IsNullOrEmpty(imgstore))
                    imgstore = "FS";//FS:FileSystem  DB:DataBase Definido en paramentros (ImageStore);

                string rootpath = context.Server.MapPath("~");

                string path = rootpath + "/UploadXML"; // context.Server.MapPath("~/UploadFiles");
                bool exists = System.IO.Directory.Exists(path);
                if (!exists)
                    System.IO.Directory.CreateDirectory(path);

                string import = context.Request.QueryString["import"];
                if (!string.IsNullOrEmpty(import))
                {
                    //List<string> claves = new List<string>();
                    List<ElectronicoClave> claves = new List<ElectronicoClave>();
                    int i = 0;
                    string line = "";
                    var file = context.Request.Files[0];
                    using (Stream stream = file.InputStream)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] array = line.Split('\t');
                            if (array.Length > 10)
                            {
                                ElectronicoClave clave = new ElectronicoClave();
                                clave.numero = array[1];
                                clave.clave = array[10];
                                //claves.Add(array[10]);
                                claves.Add(clave);
                                //context.Response.Write(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", array[1], array[10]));
                            }
                        }                         
                    }


                    Electronicocarga electronicocarga = Packages.Electronico.CreateCargaElectronico(jobj.empresa, jobj.crea_usr, claves);                                                                                               
                    context.Response.Write(electronicocarga.eca_id);
                    

                }
                else
                {
                    List<string> claves = new List<string>();


                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        var file = context.Request.Files[i];
                        string nombre;
                        string archivo;
                        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] files = file.FileName.Split(new char[] { '\\' });
                            nombre = files[files.Length - 1];
                        }
                        else
                        {
                            nombre = file.FileName;
                        }

                        //object obj = new JavaScriptSerializer().Deserialize<object>(context.Request.Form["obj"]);
                        //Dictionary<string, object> tmp = (Dictionary<string, object>)obj;
                        //object objeto = null;
                        //tmp.TryGetValue("objeto", out objeto);
                        //JsonObj jobj = new JsonObj(objeto);
                        
                        archivo = Path.Combine(path, nombre);
                        file.SaveAs(archivo);

                        Comprobante com = Packages.Electronico.ReadFacturaCompra(archivo, jobj.crea_usr, claves);
                        if (com.com_codigo > 0)
                            claves.Add(com.com_claveelec);

                        System.IO.File.Delete(archivo);


                        context.Response.Write(string.Format("<tr style='background-color:{2}'><td>{0}</td><td>{1}</td></tr>", nombre, com.com_doctran, com.com_codigo > 0 ? "#E0FFFF" : "#FFF0F5"));
                    }




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
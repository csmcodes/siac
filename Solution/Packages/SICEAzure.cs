using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft;
using System.Threading;
using System.Diagnostics;
using Services;

namespace Packages
{
    public class SICEAzure
    {
        public class SICEAutorizacion
        {
            public string estado { get; set; }
            public string numeroAutorizacion { get; set; }
            public DateTime? fechaAutorizacion { get; set; }
            public string ambiente { get; set; }
            public string comprobante { get; set; }
            public List<SICEMensaje> mensajes { get; set; }

        }

        public class SICEMensaje
        {
            public string identificador { get; set; }
            public string mensaje { get; set; }
            public string informacionAdicional { get; set; }
            public string tipo { get; set; }
        }


        public static async Task GetAutorizacion(List<ElectronicoClave> claves, int empresa, string crea_usr, string carga)
        {

            List<SICEAutorizacion> autorizaciones = new List<SICEAutorizacion>();
            //var client = new RestClient("http://localhost:44318/api");
            var client = new RestClient("http://apisicenet.azurewebsites.net/api");

            try
            {
                List<string> errores = new List<string>();
                foreach (var clave in claves)
                {
                    try
                    {
                        Thread.Sleep(100);
                        var request = new RestRequest("autorizacion/" + clave.clave + "?comprobante=true", Method.Get);
                        //request.AddParameter("productkeys_Key", key);
                        request.Timeout = 5000;
                        RestResponse response = client.Execute(request);
                        //only throws the exception. Let target choose what to do
                        if (response.ErrorException != null)
                            throw response.ErrorException;
                        else
                        {
                            //autorizaciones.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<SICEAutorizacion>(response.Content));                                        
                            Packages.Electronico.SaveElectronico(Newtonsoft.Json.JsonConvert.DeserializeObject<SICEAutorizacion>(response.Content), empresa, crea_usr, carga,clave);
                        }

                    }
                    catch (Exception e)
                    {
                        errores.Add(clave + "" + e.Message);
                    }
                }

                Packages.Electronico.EndCargaElectronico(empresa, carga);
                //if string equals "1" the key is not activated yet
                //return autorizaciones;
            }
            catch (Exception ex)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry( ex.Message );
                }
            }



        }

    }
}

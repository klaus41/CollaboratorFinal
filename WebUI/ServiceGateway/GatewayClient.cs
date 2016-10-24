using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;

namespace WebUI.ServiceGateway
{
    public class GatewayClient
    {
        public HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            string baseAddress = WebConfigurationManager.AppSettings["CollaboratorDBAddress"];
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );

            //Create an Authorization header in order to authenticate as required
            // by the ProductStoreBLL REST Service.
            //string credentials = WebConfigurationManager.AppSettings["CollaboratorBLLRESTServiceCredentials"];
            //client.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Custom", credentials);

            return client;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebUI.Models;

namespace WebUI.ServiceGateway
{
    public class NavisionGateway : GatewayClient
    {
        public IEnumerable<NavisonContactModel> GetContacts()
        {
            HttpClient client = GetHttpClient();

            HttpResponseMessage response = client.GetAsync("api/nav/contacts").Result;
            var contacts = response.Content.ReadAsAsync<IEnumerable<NavisonContactModel>>().Result;
            return contacts;
        }
    }
}
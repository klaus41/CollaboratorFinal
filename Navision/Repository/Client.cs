using System.Configuration;
using EliteIT.Biomass.OData.navData;
using System.Net;
using System;

namespace Navision.Repository
{
    public class Client
    {
        public Client()
        {

        }

        public NAV navOData()
        {
            NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["Dynamics.URL"].ToString()));
            nav.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Dynamics.Username"], ConfigurationManager.AppSettings["Dynamics.Password"], ConfigurationManager.AppSettings["Dynamics.Domain"].ToString());
            return nav;
        }

    }
}
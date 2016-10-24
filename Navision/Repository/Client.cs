using System.Configuration;
using System.Net;
using System;
using Navision.navData;

namespace Navision.Repository
{
    public class Client
    {
        public Client()
        {

        }

        public NAV NavOData()
        {
            NAV nav = new NAV(new Uri(ConfigurationManager.AppSettings["Dynamics.URL"].ToString()))
            {
                Credentials =
                    new NetworkCredential(ConfigurationManager.AppSettings["Dynamics.Username"],
                        ConfigurationManager.AppSettings["Dynamics.Password"],
                        ConfigurationManager.AppSettings["Dynamics.Domain"].ToString())
            };
            return nav;
        }

    }
}
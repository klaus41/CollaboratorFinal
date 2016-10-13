using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Context
{
    public class ContextInitializer : DropCreateDatabaseAlways<CollaboratorContext>
    {
        protected override void Seed(CollaboratorContext context)
        {


            SearchCriteria sc1 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Pro Nummer 9045" });
            SearchCriteria sc2 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Pro Nummer 3409" });
            SearchCriteria sc3 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Eltavle" });
            SearchCriteria sc4 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Pro Automatic" });



            Email email1 = context.Emails.Add(new Email()
            {

                BodyText = "Første bodytext Første bodytext Første bodytext Første bodytext Første bodytext Første bodytext" +
                            "Første bodytext Første bodytext Første bodytext Første bodytext ",
                Recipiant = "klausgaarde@live.dk",
                Sender = "Carsten@eliteit.dk",
                Subject = "Collaborator",
                ReceivedDate = new DateTime(2016, 8, 7),
                SearchCriteria = { sc1, sc4 }
            });

            Email email2 = context.Emails.Add(new Email()
            {

                BodyText = "Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden" +
                            "Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden bodytext ",
                Recipiant = "klausgaarde@live.dk",
                Sender = "Carsten@eliteit.dk",
                Subject = "Arbejder",
                ReceivedDate = new DateTime(2015, 7, 8),
                SearchCriteria = { sc1, sc2 }
            });

            Theme theme1 = context.Themes.Add(new Theme() { Title = "Pro Nummer 9045", SearchCriterias = { sc1, sc3 } });
            Theme theme2 = context.Themes.Add(new Theme() { Title = "Pro Nummer 3409", SearchCriterias = { sc2 } });
            Theme theme3 = context.Themes.Add(new Theme() { Title = "Pro Automatic", SearchCriterias = { sc4 } });

            base.Seed(context);
        }
    }
}
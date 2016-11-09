using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Context
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<CollaboratorContext>
    {
        protected override void Seed(CollaboratorContext context)
        {


            SearchCriteria sc1 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Carsten" });
            SearchCriteria sc2 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Swienty" });
            SearchCriteria sc3 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Eltavle" });
            SearchCriteria sc4 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Pro Automatic" });
            SearchCriteria sc5 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "EliteIT" });
            SearchCriteria sc6 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Klaus" });
            SearchCriteria sc7 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = new DateTime(2016, 8, 7).ToString() });
            SearchCriteria sc8 = context.SearchCriterias.Add(new SearchCriteria() { Criteria = "Ronni" });



            //Email email1 = context.Emails.Add(new Email()
            //{

            //    BodyText = "Første bodytext Første bodytext Første bodytext Første bodytext Første bodytext Første bodytext" +
            //                "Første bodytext Første bodytext Første bodytext Første bodytext ",
            //    Recipiant = "klausgaarde@live.dk",
            //    Sender = "Carsten@eliteit.dk",
            //    Subject = "Collaborator",
            //    ReceivedDate = new DateTime(2014, 8, 7),
            //    SearchCriteria = { sc1, sc4 }
            //});

            //Email email2 = context.Emails.Add(new Email()
            //{

            //    BodyText = "Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden" +
            //                "Anden bodytext Anden bodytext Anden bodytext Anden bodytext Anden bodytext ",
            //    Recipiant = "klausgaarde@live.dk",
            //    Sender = "Carsten@eliteit.dk",
            //    Subject = "Arbejder",
            //    ReceivedDate = new DateTime(2015, 7, 8),
            //    SearchCriteria = { sc1, sc2 }
            //});

            Theme theme1 = context.Themes.Add(new Theme() { Title = "Carsten", SearchCriterias = { sc1} });
            Theme theme2 = context.Themes.Add(new Theme() { Title = "Klaus", SearchCriterias = { sc6 } });
            Theme theme3 = context.Themes.Add(new Theme() { Title = "EliteIT", SearchCriterias = { sc5, sc1, sc6, sc8 } });
            Theme theme4 = context.Themes.Add(new Theme() { Title = "Ronni", SearchCriterias = { sc8 } });
            Theme theme5 = context.Themes.Add(new Theme() { Title = "Swienty", SearchCriterias = { sc2 } });



            EmailAccount account1 = context.EmailAccounts.Add(new EmailAccount() { EmailAddress = "Klaus@eliteit.dk", Password = "Kg240789." });
            EmailAccount account2 = context.EmailAccounts.Add(new EmailAccount() { EmailAddress = "ronni@eliteit.dk", Password = "oz1akhEI" });

            base.Seed(context);
        }
    }
}
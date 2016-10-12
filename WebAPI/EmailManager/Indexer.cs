using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.EmailManager
{
    public class Indexer
    {
        private CollaboratorContext db = new CollaboratorContext();
        DbSet<Email> emails;
        DbSet<SearchCriteria> searchCriterias;
        DbSet<Theme> themes;
        Email email;
        Theme theme;

        public void IndexAllEmails()
        {
            emails = db.Emails;
            searchCriterias = db.SearchCriterias;


            foreach (var searchCriteria in searchCriterias)
            {
                foreach (var email in emails)
                {
                    if (email.BodyText != null && email.BodyText.Contains(searchCriteria.Criteria))
                    {
                        email.SearchCriteria.Add(searchCriteria);
                    }
                }
            }

            db.SaveChanges();
            debug();
        }

        private void debug()
        {

            emails = db.Emails;
            foreach (var e in emails)
            {
                if (e.ID == 1)
                {
                    email = e;
                }
            }

            themes = db.Themes;
            foreach (var t in themes)
            {
                if (t.ID == 3)
                {
                    theme = t;
                }
            }
            foreach (var email in GetEmailsFromTheme(theme))
            {
                Debug.WriteLine("email " + email.ReceivedDate);
            }

            foreach (var theme in GetThemesFromEmail(email))
            {
                Debug.WriteLine("Theme " + theme.Title);
            }
        }

        private List<Theme> GetThemesFromEmail(Email email)
        {
            List<Theme> themes = new List<Theme>();

            foreach (var s in email.SearchCriteria)
            {
                foreach (var t in s.Themes)
                {
                    themes.Add(t);
                }
            }
            return themes;
        }

        private List<Email> GetEmailsFromTheme(Theme theme)
        {
            List<Email> emailList = new List<Email>();

            foreach (var s in theme.SearchCriterias)
            {
                foreach (var e in s.Emails)
                {
                    emailList.Add(e);
                }
            }
            return emailList;
        }
    }
}
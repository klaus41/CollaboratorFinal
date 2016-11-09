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
                    CheckForNormalCase(email, searchCriteria);
                    CheckForLowerCase(email, searchCriteria);
                    CheckForUpperCase(email, searchCriteria);
                }
            }
            db.SaveChanges();

            //debug();

        }

        public void IndexNewEmails(List<Email> emails)
        {
            searchCriterias = db.SearchCriterias;

            foreach (var searchCriteria in searchCriterias)
            {
                foreach (var email in emails)
                {
                    if (email != null || searchCriteria != null)
                    {
                        CheckForNormalCase(email, searchCriteria);
                        CheckForLowerCase(email, searchCriteria);
                        CheckForUpperCase(email, searchCriteria);
                    }
                    else
                    {
                        Debug.WriteLine("Email or searchcriteria = null");
                    }
                }
            }
            db.SaveChanges();
        }

        private void CheckForUpperCase(Email email, SearchCriteria searchCriteria)
        {
            string c = searchCriteria.Criteria.ToUpper();
            Check(c, searchCriteria, email);
        }

        private void CheckForLowerCase(Email email, SearchCriteria searchCriteria)
        {
            string c = searchCriteria.Criteria.ToLower();
            Check(c, searchCriteria, email);
        }

        private void CheckForNormalCase(Email email, SearchCriteria searchCriteria)
        {
            string c = searchCriteria.Criteria;
            Check(c, searchCriteria, email);
        }

        private void Check(string c, SearchCriteria searchCriteria, Email email)
        {
            if (email.BodyText != null && email.BodyText.Contains(c) ||
                                        email.ReceivedDate != null && email.ReceivedDate.ToString().Contains(c) ||
                                        email.Recipiant != null && email.Recipiant.Contains(c) ||
                                        email.Sender != null && email.Sender.Contains(c) ||
                                        email.Subject != null && email.Subject.Contains(c) ||
                                        email.Sender != null && email.Sender.Contains(c))
            {
                //email.SearchCriteria.Add(searchCriteria);
                if (!searchCriteria.Emails.Contains(email))
                {
                    searchCriteria.Emails.Add(email);
                }
            }
            else { }

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
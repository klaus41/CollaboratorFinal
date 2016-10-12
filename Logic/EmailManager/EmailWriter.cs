using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.EmailManager
{
    public class EmailWriter
    {
        private CollaboratorContext db = new CollaboratorContext();
        private List<Email> emails;
        private Email email;

        public List<Email> EmailConverter(FindItemsResults<Item> findResults, string userName)
        {
            emails = new List<Email>();
            foreach (var item in findResults)
            {
                email = new Email();
                email.ID = new Random().Next(1, 1000).ToString();
                email.BodyText = item.TextBody;
                email.Recipiant = item.InReplyTo;
                email.Sender = userName;
                email.ReceivedDate = item.DateTimeReceived;
                email.Subject = item.Subject;

                emails.Add(email);
            }

            return emails;
        }

        public void SaveEmails(List<Email> emails)
        {
            foreach (var email in emails)
            {
                db.Emails.Add(email);
            }
        }
    }
}

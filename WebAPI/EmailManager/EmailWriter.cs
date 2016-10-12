using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
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
        private EmailMessage message;

        public List<Email> EmailConverter(FindItemsResults<Item> findResults, string userName)
        {
            PropertySet itempropertyset = new PropertySet(BasePropertySet.FirstClassProperties);
            itempropertyset.RequestedBodyType = BodyType.Text;

            Random r = new Random();
            emails = new List<Email>();
            foreach (var item in findResults)
            {
                message = (EmailMessage)item;
                item.Load(itempropertyset);

                email = new Email();
                email.BodyText = item.Body;
                try
                {
                    email.Recipiant = message.ReceivedBy.Address;
                }
                catch
                {
                    
                }

                email.Sender = message.Sender.Address;
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
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

    }
}

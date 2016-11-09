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

        public List<Email> EmailConverter(FindItemsResults<Item> findResults)
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
                if (message.ReceivedBy != null)
                {
                    email.Recipiant = message.ReceivedBy.Address;
                }
                else
                {
                    email.Recipiant = "Modtager untilgængelig";
                }

                if (message.Sender != null)
                {
                    email.Sender = message.Sender.Address;
                }
                else
                {
                    email.Sender = "Afsender utilgængelig";
                }

                if (item.DateTimeReceived != null && item.DateTimeReceived > new DateTime(1950, 1, 1))
                {
                    email.ReceivedDate = item.DateTimeReceived;
                }
                else
                {
                    email.ReceivedDate = new DateTime(2000, 1, 1);
                }

                if (item.Subject != null)
                {
                    email.Subject = item.Subject;
                }
                else
                {
                    email.Subject = "Emne utilgængelig";
                }

                emails.Add(email);
            }

            return emails;
        }

        public void SaveEmails(List<Email> emails)
        {
            using (var ctx = new CollaboratorContext())
            {
                if (emails.Count() != 0 && emails != null)
                {
                    foreach (var email in emails)
                    {
                        ctx.Emails.Add(email);
                    }
                    try
                    {
                        ctx.SaveChanges();
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

    }
}

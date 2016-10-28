using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.EmailManager
{
    public class ThreadManager
    {
        private CollaboratorContext db = new CollaboratorContext();

        private Thread thread;
        private bool done;
        private EmailReader er = new EmailReader();
        EmailWriter ew = new EmailWriter();
        Indexer indexer = new Indexer();
        List<Email> emails;
        FindItemsResults<Item> findResults;

        public void StartEmailThread()
        {
            done = false;
            thread = new Thread(new ThreadStart(DoWork));
            thread.Start();
            
        }

        private void DoWork()
        {
            while (!done)
            {
                Debug.WriteLine("I just checked for new Email");

                foreach (EmailAccount ea in db.EmailAccounts)
                {
                    findResults = er.GetNewEmails(ea.EmailAddress, ea.Password);
                    emails = ew.EmailConverter(findResults);
                    ew.SaveEmails(emails);
                    indexer.IndexNewEmails(emails);
                    foreach (var item in emails)
                    {
                        Debug.WriteLine(item.ID);
                    }
                }
                Thread.Sleep(60000);
            }
            
        }
    }
}
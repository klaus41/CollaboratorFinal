using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using WebAPI.Models;
using WebAPI.Context;
using System.Data.Entity;

namespace WebAPI.EmailManager
{
    public class EmailReader
    {
        ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
        EmailWriter ew = new EmailWriter();
        Indexer indexer = new Indexer();
        CollaboratorContext db = new CollaboratorContext();

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }
        public FindItemsResults<Item> GetUnreadEmails(string userName, string password)
        {
            Login(userName, password);

            // Bind the Inbox folder to the service object.
            Folder inbox = Folder.Bind(service, WellKnownFolderName.Inbox);

            // The search filter to get unread email.
            SearchFilter sf = new SearchFilter.SearchFilterCollection(LogicalOperator.And, new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
            ItemView view = new ItemView(100);

            // Fire the query for the unread items.
            // This method call results in a FindItem call to EWS.
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, sf, view);

            return findResults;
        }

        public FindItemsResults<Item> GetAllEmails(string userName, string password)
        {
            Login(userName, password);

            ItemView view = new ItemView(100000);
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, view);
            return findResults;
        }

        public FindItemsResults<Item> GetNewEmails(string userName, string password)
        {
            Login(userName, password);

            DbSet<Email> emails = db.Emails;
            var latest = emails.OrderByDescending(m => m.ReceivedDate).FirstOrDefault();
            DateTime searchdate = latest.ReceivedDate.AddSeconds(1);


            SearchFilter greaterthanfilter = new SearchFilter.IsGreaterThanOrEqualTo(ItemSchema.DateTimeReceived, searchdate);
            //SearchFilter lessthanfilter = new SearchFilter.IsLessThan(ItemSchema.DateTimeReceived, searchdate.AddDays(1));
            SearchFilter filter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, greaterthanfilter);
            
            ItemView view = new ItemView(100000);
            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, filter, view);

            return findResults;
        }

        private ExchangeService Login(string userName, string password)
        {


            service.Credentials = new WebCredentials(userName, password);
            if (userName.Contains("dk"))
            {
                userName.Replace("dk", "com");
            }
            service.AutodiscoverUrl(userName, RedirectionUrlValidationCallback);

            return service;
        }


    }
}

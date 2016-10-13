using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Context;
using WebAPI.EmailManager;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [RoutePrefix("api")]

    public class EmailsController : ApiController
    {
        private CollaboratorContext db = new CollaboratorContext();
        private EmailReader er = new EmailReader();

        // GET: api/Emails
        public IQueryable<Email> GetEmails()
        {
            return db.Emails.Include(s=>s.SearchCriteria);
        }

        // GET: api/Emails/5
        [ResponseType(typeof(Email))]
        public IHttpActionResult GetEmail(int id)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                return NotFound();
            }

            return Ok(email);
        }

        // PUT: api/Emails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmail(int id, Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != email.ID)
            {
                return BadRequest();
            }

            db.Entry(email).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmailExists(id.ToString()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Emails
        [ResponseType(typeof(Email))]
        public IHttpActionResult PostEmail(Email email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Emails.Add(email);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmailExists(email.ID.ToString()))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = email.ID }, email);
        }

        // DELETE: api/Emails/5
        [ResponseType(typeof(Email))]
        public IHttpActionResult DeleteEmail(string id)
        {
            Email email = db.Emails.Find(id);
            if (email == null)
            {
                return NotFound();
            }

            db.Emails.Remove(email);
            db.SaveChanges();

            return Ok(email);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmailExists(string id)
        {
            return db.Emails.Count(e => e.ID.ToString() == id) > 0;
        }

        [HttpGet]
        [Route("checkmail")]
        [ResponseType(typeof(void))]
        public IHttpActionResult SaveAndIndexEmails()
        {
            string userName = "klaus@eliteit.dk";
            string password = "Kg240789.";


            EmailReader er = new EmailReader();
            EmailWriter ew = new EmailWriter();
            Indexer indexer = new Indexer();
            List<Email> emails;
            FindItemsResults<Item> findResults;

            findResults = er.GetAllEmails(userName, password);
            emails = ew.EmailConverter(findResults, userName);
            ew.SaveEmails(emails);
            indexer.IndexAllEmails();

            return Ok(emails);
        }
        [HttpGet]
        [Route("CheckNewMail")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CheckNewMail()
        {
            string userName = "klaus@eliteit.dk";
            string password = "Kg240789.";


            EmailReader er = new EmailReader();
            EmailWriter ew = new EmailWriter();
            Indexer indexer = new Indexer();
            List<Email> emails;
            FindItemsResults<Item> findResults;

            findResults = er.GetNewEmails(userName, password);
            emails = ew.EmailConverter(findResults, userName);
            ew.SaveEmails(emails);
            indexer.IndexAllEmails();

            return Ok(emails);
        }

    }
}
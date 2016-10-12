using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Context;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class SearchCriteriasController : ApiController
    {
        private CollaboratorContext db = new CollaboratorContext();

        // GET: api/SearchCriterias
        public IQueryable<SearchCriteria> GetSearchCriterias()
        {
            return db.SearchCriterias;
        }

        // GET: api/SearchCriterias/5
        [ResponseType(typeof(SearchCriteria))]
        public IHttpActionResult GetSearchCriteria(int id)
        {
            SearchCriteria searchCriteria = db.SearchCriterias.Find(id);
            if (searchCriteria == null)
            {
                return NotFound();
            }

            return Ok(searchCriteria);
        }

        // PUT: api/SearchCriterias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSearchCriteria(int id, SearchCriteria searchCriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != searchCriteria.ID)
            {
                return BadRequest();
            }

            db.Entry(searchCriteria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SearchCriteriaExists(id))
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

        // POST: api/SearchCriterias
        [ResponseType(typeof(SearchCriteria))]
        public IHttpActionResult PostSearchCriteria(SearchCriteria searchCriteria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SearchCriterias.Add(searchCriteria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = searchCriteria.ID }, searchCriteria);
        }

        // DELETE: api/SearchCriterias/5
        [ResponseType(typeof(SearchCriteria))]
        public IHttpActionResult DeleteSearchCriteria(int id)
        {
            SearchCriteria searchCriteria = db.SearchCriterias.Find(id);
            if (searchCriteria == null)
            {
                return NotFound();
            }

            db.SearchCriterias.Remove(searchCriteria);
            db.SaveChanges();

            return Ok(searchCriteria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SearchCriteriaExists(int id)
        {
            return db.SearchCriterias.Count(e => e.ID == id) > 0;
        }
    }
}
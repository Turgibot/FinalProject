using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CSA_Project.Models;

namespace CSA_Project.Controllers
{
    public class SelectorModelsWebAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SelectorModelsWebAPI
        public IQueryable<SelectorModel> GetSelectorModels()
        {
            return db.SelectorModels;
        }

        // GET: api/SelectorModelsWebAPI/5
        [ResponseType(typeof(SelectorModel))]
        public async Task<IHttpActionResult> GetSelectorModel(long id)
        {
            SelectorModel selectorModel = await db.SelectorModels.FindAsync(id);
            if (selectorModel == null)
            {
                return NotFound();
            }

            return Ok(selectorModel);
        }

        // PUT: api/SelectorModelsWebAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSelectorModel(long id, SelectorModel selectorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != selectorModel.Id)
            {
                return BadRequest();
            }

            db.Entry(selectorModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SelectorModelExists(id))
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

        // POST: api/SelectorModelsWebAPI
        [ResponseType(typeof(SelectorModel))]
        public async Task<IHttpActionResult> PostSelectorModel(SelectorModel selectorModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SelectorModels.Add(selectorModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = selectorModel.Id }, selectorModel);
        }

        // DELETE: api/SelectorModelsWebAPI/5
        [ResponseType(typeof(SelectorModel))]
        public async Task<IHttpActionResult> DeleteSelectorModel(long id)
        {
            SelectorModel selectorModel = await db.SelectorModels.FindAsync(id);
            if (selectorModel == null)
            {
                return NotFound();
            }

            db.SelectorModels.Remove(selectorModel);
            await db.SaveChangesAsync();

            return Ok(selectorModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SelectorModelExists(long id)
        {
            return db.SelectorModels.Count(e => e.Id == id) > 0;
        }
    }
}
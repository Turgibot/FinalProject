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
    public class LoggerController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Logger
        public IQueryable<LoggerModel> GetLoggerModels()
        {
            return db.LoggerModels;
        }

        // GET: api/Logger/5
        [ResponseType(typeof(LoggerModel))]
        public async Task<IHttpActionResult> GetLoggerModel(long id)
        {
            LoggerModel loggerModel = await db.LoggerModels.FindAsync(id);
            if (loggerModel == null)
            {
                return NotFound();
            }

            return Ok(loggerModel);
        }

        // PUT: api/Logger/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLoggerModel(long id, LoggerModel loggerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loggerModel.Id)
            {
                return BadRequest();
            }

            db.Entry(loggerModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoggerModelExists(id))
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

        // POST: api/Logger
        [ResponseType(typeof(LoggerModel))]
        public async Task<IHttpActionResult> PostLoggerModel(LoggerModel loggerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoggerModels.Add(loggerModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = loggerModel.Id }, loggerModel);
        }

        // DELETE: api/Logger/5
        [ResponseType(typeof(LoggerModel))]
        public async Task<IHttpActionResult> DeleteLoggerModel(long id)
        {
            LoggerModel loggerModel = await db.LoggerModels.FindAsync(id);
            if (loggerModel == null)
            {
                return NotFound();
            }

            db.LoggerModels.Remove(loggerModel);
            await db.SaveChangesAsync();

            return Ok(loggerModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoggerModelExists(long id)
        {
            return db.LoggerModels.Count(e => e.Id == id) > 0;
        }
    }
}
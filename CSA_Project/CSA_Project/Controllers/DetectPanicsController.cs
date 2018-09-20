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
    public class DetectPanicsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DetectPanics
        public IQueryable<DetectPanic> GetDetectPanics()
        {
            return db.DetectPanics;
        }

        // GET: api/DetectPanics/5
        [ResponseType(typeof(DetectPanic))]
        public async Task<IHttpActionResult> GetDetectPanic(long id)
        {
            DetectPanic detectPanic = await db.DetectPanics.FindAsync(id);
            if (detectPanic == null)
            {
                return NotFound();
            }

            return Ok(detectPanic);
        }
        // GET: api/GetLastPanic/
        [HttpGet]
        [Route("api/GetLastPanic")]
        [ResponseType(typeof(DetectPeople))]
        public async Task<IHttpActionResult> GetLastDetection()
        {
            int last = await db.DetectPanics.CountAsync();
            DetectPanic detectPanic = await db.DetectPanics.FindAsync(last);
            if (detectPanic == null)
            {
                return NotFound();
            }

            return Ok(detectPanic);
        }
        // PUT: api/DetectPanics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDetectPanic(long id, DetectPanic detectPanic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detectPanic.Id)
            {
                return BadRequest();
            }

            db.Entry(detectPanic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetectPanicExists(id))
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

        // POST: api/DetectPanics
        [ResponseType(typeof(DetectPanic))]
        public async Task<IHttpActionResult> PostDetectPanic(DetectPanic detectPanic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetectPanics.Add(detectPanic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = detectPanic.Id }, detectPanic);
        }

        // DELETE: api/DetectPanics/5
        [ResponseType(typeof(DetectPanic))]
        public async Task<IHttpActionResult> DeleteDetectPanic(long id)
        {
            DetectPanic detectPanic = await db.DetectPanics.FindAsync(id);
            if (detectPanic == null)
            {
                return NotFound();
            }

            db.DetectPanics.Remove(detectPanic);
            await db.SaveChangesAsync();

            return Ok(detectPanic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetectPanicExists(long id)
        {
            return db.DetectPanics.Count(e => e.Id == id) > 0;
        }
    }
}
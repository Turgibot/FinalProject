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
    public class DetectPeoplesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DetectPeoples
        public IQueryable<DetectPeople> GetDetectPeoples()
        {
            return db.DetectPeoples;
        }

        // GET: api/DetectPeoples/5
        [ResponseType(typeof(DetectPeople))]
        public async Task<IHttpActionResult> GetDetectPeople(long id)
        {
            DetectPeople detectPeople = await db.DetectPeoples.FindAsync(id);
            if (detectPeople == null)
            {
                return NotFound();
            }

            return Ok(detectPeople);
        }


        // GET: api/GetLastDetection/
        [HttpGet]
        [Route("api/GetLastDetection")]
        [ResponseType(typeof(DetectPeople))]
        public async Task<IHttpActionResult> GetLastDetection()
        {
            int last = await db.DetectPeoples.CountAsync();
            DetectPeople detectPeople = await db.DetectPeoples.FindAsync(last);
            if (detectPeople == null)
            {
                return NotFound();
            }

            return Ok(detectPeople);
        }
        // PUT: api/DetectPeoples/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDetectPeople(long id, DetectPeople detectPeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detectPeople.Id)
            {
                return BadRequest();
            }

            db.Entry(detectPeople).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetectPeopleExists(id))
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

        // POST: api/DetectPeoples
        [ResponseType(typeof(DetectPeople))]
        public async Task<IHttpActionResult> PostDetectPeople(DetectPeople detectPeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetectPeoples.Add(detectPeople);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = detectPeople.Id }, detectPeople);
        }

        // DELETE: api/DetectPeoples/5
        [ResponseType(typeof(DetectPeople))]
        public async Task<IHttpActionResult> DeleteDetectPeople(long id)
        {
            DetectPeople detectPeople = await db.DetectPeoples.FindAsync(id);
            if (detectPeople == null)
            {
                return NotFound();
            }

            db.DetectPeoples.Remove(detectPeople);
            await db.SaveChangesAsync();

            return Ok(detectPeople);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetectPeopleExists(long id)
        {
            return db.DetectPeoples.Count(e => e.Id == id) > 0;
        }
    }
}
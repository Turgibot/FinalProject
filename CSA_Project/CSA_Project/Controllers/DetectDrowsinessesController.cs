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
using Microsoft.AspNet.SignalR;

namespace CSA_Project.Controllers
{
    public class DetectDrowsinessesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DetectDrowsinesses
        public IQueryable<DetectDrowsiness> GetDetectDrowsinesses()
        {
            return db.DetectDrowsinesses;
        }

        // GET: api/GetLastDrowsiness/
        [HttpGet]
        [Route("api/GetLastDrowsiness")]
        [ResponseType(typeof(DetectDrowsiness))]
        public async Task<IHttpActionResult> GetLastDetection()
        {
            int last = await db.DetectDrowsinesses.CountAsync();
            DetectDrowsiness detectDrowsiness = await db.DetectDrowsinesses.FindAsync(last);
            if (detectDrowsiness == null)
            {
                return NotFound();
            }

            return Ok(detectDrowsiness);
        }
        // GET: api/DetectDrowsinesses/5
        [ResponseType(typeof(DetectDrowsiness))]
        public async Task<IHttpActionResult> GetDetectDrowsiness(long id)
        {
            DetectDrowsiness detectDrowsiness = await db.DetectDrowsinesses.FindAsync(id);
            if (detectDrowsiness == null)
            {
                return NotFound();
            }

            return Ok(detectDrowsiness);
        }

        // PUT: api/DetectDrowsinesses/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDetectDrowsiness(long id, DetectDrowsiness detectDrowsiness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detectDrowsiness.Id)
            {
                return BadRequest();
            }

            db.Entry(detectDrowsiness).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetectDrowsinessExists(id))
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

        // POST: api/DetectDrowsinesses
        [ResponseType(typeof(DetectDrowsiness))]
        public async Task<IHttpActionResult> PostDetectDrowsiness(DetectDrowsiness detectDrowsiness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var hub = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            hub.Clients.All.broadcastMessage("Drowsiness", detectDrowsiness.IsAwake);

            db.DetectDrowsinesses.Add(detectDrowsiness);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = detectDrowsiness.Id }, detectDrowsiness);
        }

        // DELETE: api/DetectDrowsinesses/5
        [ResponseType(typeof(DetectDrowsiness))]
        public async Task<IHttpActionResult> DeleteDetectDrowsiness(long id)
        {
            DetectDrowsiness detectDrowsiness = await db.DetectDrowsinesses.FindAsync(id);
            if (detectDrowsiness == null)
            {
                return NotFound();
            }

            db.DetectDrowsinesses.Remove(detectDrowsiness);
            await db.SaveChangesAsync();

            return Ok(detectDrowsiness);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetectDrowsinessExists(long id)
        {
            return db.DetectDrowsinesses.Count(e => e.Id == id) > 0;
        }
    }
}
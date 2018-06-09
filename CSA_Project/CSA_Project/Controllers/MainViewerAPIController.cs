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
    public class MainViewerAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MainViewerAPI
        public IQueryable<MainViewerViewModels> GetViewer()
        {
            return db.Viewer;
        }

        // GET: api/MainViewerAPI/5
        [ResponseType(typeof(MainViewerViewModels))]
        public async Task<IHttpActionResult> GetMainViewerViewModels(long id)
        {
            MainViewerViewModels mainViewerViewModels = await db.Viewer.FindAsync(id);
            if (mainViewerViewModels == null)
            {
                return NotFound();
            }

            return Ok(mainViewerViewModels);
        }

        // PUT: api/MainViewerAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMainViewerViewModels(long id, MainViewerViewModels mainViewerViewModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mainViewerViewModels.Id)
            {
                return BadRequest();
            }

            db.Entry(mainViewerViewModels).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainViewerViewModelsExists(id))
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

        // POST: api/MainViewerAPI
        [ResponseType(typeof(MainViewerViewModels))]
        public async Task<IHttpActionResult> PostMainViewerViewModels(MainViewerViewModels mainViewerViewModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Viewer.Add(mainViewerViewModels);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = mainViewerViewModels.Id }, mainViewerViewModels);
        }

        // DELETE: api/MainViewerAPI/5
        [ResponseType(typeof(MainViewerViewModels))]
        public async Task<IHttpActionResult> DeleteMainViewerViewModels(long id)
        {
            MainViewerViewModels mainViewerViewModels = await db.Viewer.FindAsync(id);
            if (mainViewerViewModels == null)
            {
                return NotFound();
            }

            db.Viewer.Remove(mainViewerViewModels);
            await db.SaveChangesAsync();

            return Ok(mainViewerViewModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MainViewerViewModelsExists(long id)
        {
            return db.Viewer.Count(e => e.Id == id) > 0;
        }
    }
}
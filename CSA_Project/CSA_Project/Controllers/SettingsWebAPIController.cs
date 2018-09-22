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
    public class SettingsWebAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SettingsWebAPI
        public IQueryable<SettingsViewModels> GetSettings()
        {
            return db.Settings;
        }

        // GET: api/SettingsWebAPI/5
        [ResponseType(typeof(SettingsViewModels))]
        public async Task<IHttpActionResult> GetSettingsViewModels(long id)
        {
            SettingsViewModels settingsViewModels = await db.Settings.FindAsync(id);
            if (settingsViewModels == null)
            {
                return NotFound();
            }

            return Ok(settingsViewModels);
        }

        // PUT: api/SettingsWebAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSettingsViewModels(long id, SettingsViewModels settingsViewModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != settingsViewModels.Id)
            {
                return BadRequest();
            }

            db.Entry(settingsViewModels).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingsViewModelsExists(id))
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

        // POST: api/SettingsWebAPI
        [ResponseType(typeof(SettingsViewModels))]
        public async Task<IHttpActionResult> PostSettingsViewModels(SettingsViewModels settingsViewModels)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Settings.Add(settingsViewModels);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = settingsViewModels.Id }, settingsViewModels);
        }

        // DELETE: api/SettingsWebAPI/5
        [ResponseType(typeof(SettingsViewModels))]
        public async Task<IHttpActionResult> DeleteSettingsViewModels(long id)
        {
            SettingsViewModels settingsViewModels = await db.Settings.FindAsync(id);
            if (settingsViewModels == null)
            {
                return NotFound();
            }

            db.Settings.Remove(settingsViewModels);
            await db.SaveChangesAsync();

            return Ok(settingsViewModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SettingsViewModelsExists(long id)
        {
            return db.Settings.Count(e => e.Id == id) > 0;
        }
    }
}
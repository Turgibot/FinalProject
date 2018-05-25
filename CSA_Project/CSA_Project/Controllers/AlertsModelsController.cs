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
    public class AlertsModelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AlertsModels
        public IQueryable<AlertsModel> GetAlerts()
        {
            return db.Alerts;
        }

        // GET: api/AlertsModels/5
        [ResponseType(typeof(AlertsModel))]
        public async Task<IHttpActionResult> GetAlertsModel(long id)
        {
            AlertsModel alertsModel = await db.Alerts.FindAsync(id);
            if (alertsModel == null)
            {
                return NotFound();
            }

            return Ok(alertsModel);
        }

        // PUT: api/AlertsModels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlertsModel(long id, AlertsModel alertsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alertsModel.Id)
            {
                return BadRequest();
            }

            db.Entry(alertsModel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertsModelExists(id))
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

        // POST: api/AlertsModels
        [ResponseType(typeof(AlertsModel))]
        public async Task<IHttpActionResult> PostAlertsModel(AlertsModel alertsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alerts.Add(alertsModel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alertsModel.Id }, alertsModel);
        }

        // DELETE: api/AlertsModels/5
        [ResponseType(typeof(AlertsModel))]
        public async Task<IHttpActionResult> DeleteAlertsModel(long id)
        {
            AlertsModel alertsModel = await db.Alerts.FindAsync(id);
            if (alertsModel == null)
            {
                return NotFound();
            }

            db.Alerts.Remove(alertsModel);
            await db.SaveChangesAsync();

            return Ok(alertsModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlertsModelExists(long id)
        {
            return db.Alerts.Count(e => e.Id == id) > 0;
        }
    }
}
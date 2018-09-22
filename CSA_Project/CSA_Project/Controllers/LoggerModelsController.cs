using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSA_Project.Models;

namespace CSA_Project.Controllers
{
    public class LoggerModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LoggerModels
        public async Task<ActionResult> Index()
        {
            return View(await db.LoggerModels.OrderByDescending(x=> x.Id).ToListAsync());
        }

        // GET: LoggerModels/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoggerModel loggerModel = await db.LoggerModels.FindAsync(id);
            if (loggerModel == null)
            {
                return HttpNotFound();
            }
            return View(loggerModel);
        }

        // GET: LoggerModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoggerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DateTime,Message,Code,Key,Value,Email")] LoggerModel loggerModel)
        {
            if (ModelState.IsValid)
            {
                db.LoggerModels.Add(loggerModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(loggerModel);
        }

        // GET: LoggerModels/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoggerModel loggerModel = await db.LoggerModels.FindAsync(id);
            if (loggerModel == null)
            {
                return HttpNotFound();
            }
            return View(loggerModel);
        }

        // POST: LoggerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DateTime,Message,Code,Key,Value,Email")] LoggerModel loggerModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loggerModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(loggerModel);
        }

        // GET: LoggerModels/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoggerModel loggerModel = await db.LoggerModels.FindAsync(id);
            if (loggerModel == null)
            {
                return HttpNotFound();
            }
            return View(loggerModel);
        }

        // POST: LoggerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            LoggerModel loggerModel = await db.LoggerModels.FindAsync(id);
            db.LoggerModels.Remove(loggerModel);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

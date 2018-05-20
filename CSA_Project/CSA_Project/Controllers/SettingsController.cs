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
    public class SettingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Settings
        public async Task<ActionResult> Index()
        {
            if (!User.IsInRole("Admin"))
               return RedirectToAction("Index", "Home");
            return View(await db.Settings.ToListAsync());
        }

        // GET: Settings/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsViewModels settingsViewModels = await db.Settings.FindAsync(id);
            if (settingsViewModels == null)
            {
                return HttpNotFound();
            }
            return View(settingsViewModels);
        }

        // GET: Settings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Settings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MaxPeopleAllowed")] SettingsViewModels settingsViewModels)
        {
            if (ModelState.IsValid)
            {
                db.Settings.Add(settingsViewModels);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(settingsViewModels);
        }

        // GET: Settings/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsViewModels settingsViewModels = await db.Settings.FindAsync(id);
            if (settingsViewModels == null)
            {
                return HttpNotFound();
            }
            return View(settingsViewModels);
        }

        // POST: Settings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MaxPeopleAllowed")] SettingsViewModels settingsViewModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settingsViewModels).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(settingsViewModels);
        }

        // GET: Settings/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettingsViewModels settingsViewModels = await db.Settings.FindAsync(id);
            if (settingsViewModels == null)
            {
                return HttpNotFound();
            }
            return View(settingsViewModels);
        }

        // POST: Settings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SettingsViewModels settingsViewModels = await db.Settings.FindAsync(id);
            db.Settings.Remove(settingsViewModels);
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

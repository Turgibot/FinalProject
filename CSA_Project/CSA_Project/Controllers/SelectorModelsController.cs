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
    public class SelectorModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SelectorModels
        public async Task<ActionResult> Index()
        {
            return View(await db.SelectorModels.ToListAsync());
        }

        // GET: SelectorModels/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectorModel selectorModel = await db.SelectorModels.FindAsync(id);
            if (selectorModel == null)
            {
                return HttpNotFound();
            }
            return View(selectorModel);
        }

        // GET: SelectorModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SelectorModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SelectedValue")] SelectorModel selectorModel)
        {
            if (ModelState.IsValid)
            {
                db.SelectorModels.Add(selectorModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(selectorModel);
        }

        // GET: SelectorModels/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectorModel selectorModel = await db.SelectorModels.FindAsync(id);
            if (selectorModel == null)
            {
                return HttpNotFound();
            }
            return View(selectorModel);
        }

        // POST: SelectorModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SelectedValue")] SelectorModel selectorModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(selectorModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(selectorModel);
        }

        // GET: SelectorModels/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SelectorModel selectorModel = await db.SelectorModels.FindAsync(id);
            if (selectorModel == null)
            {
                return HttpNotFound();
            }
            return View(selectorModel);
        }

        // POST: SelectorModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            SelectorModel selectorModel = await db.SelectorModels.FindAsync(id);
            db.SelectorModels.Remove(selectorModel);
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

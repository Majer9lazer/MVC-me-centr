using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedCentr.Models.Model;

namespace MedCentr.Controllers
{
    public class MedOrganizationController : Controller
    {
        private Medical_DbEntities db = new Medical_DbEntities();

        // GET: Med_Organization
        public async Task<ActionResult> Index()
        {
            return View(await db.Med_Organization.ToListAsync());
        }

        // GET: Med_Organization/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Med_Organization med_Organization = await db.Med_Organization.FindAsync(id);
            if (med_Organization == null)
            {
                return HttpNotFound();
            }
            return View(med_Organization);
        }
        [Authorize(Roles = "admin")]
        // GET: Med_Organization/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Med_Organization/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Med_Organization_Id,Name,Address,PhoneNumber")] Med_Organization med_Organization)
        {
            if (ModelState.IsValid)
            {
                db.Med_Organization.Add(med_Organization);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(med_Organization);
        }

        // GET: Med_Organization/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Med_Organization med_Organization = await db.Med_Organization.FindAsync(id);
            if (med_Organization == null)
            {
                return HttpNotFound();
            }
            return View(med_Organization);
        }

        // POST: Med_Organization/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Med_Organization_Id,Name,Address,PhoneNumber")] Med_Organization med_Organization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(med_Organization).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(med_Organization);
        }

        // GET: Med_Organization/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Med_Organization med_Organization = await db.Med_Organization.FindAsync(id);
            if (med_Organization == null)
            {
                return HttpNotFound();
            }
            return View(med_Organization);
        }

        // POST: Med_Organization/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Med_Organization med_Organization = await db.Med_Organization.FindAsync(id);
            db.Med_Organization.Remove(med_Organization);
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

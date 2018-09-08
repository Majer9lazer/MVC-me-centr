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
    public class RequestsForAttachmentController : Controller
    {
        private Medical_DbEntities db = new Medical_DbEntities();

        // GET: Requests_for_attachment
        public async Task<ActionResult> Index()
        {
            var requestsForAttachment = db.Requests_for_attachment.Include(r => r.Patient);
            return View(await requestsForAttachment.ToListAsync());
        }

        // GET: Requests_for_attachment/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests_for_attachment requestsForAttachment = await db.Requests_for_attachment.FindAsync(id);
            if (requestsForAttachment == null)
            {
                return HttpNotFound();
            }
            return View(requestsForAttachment);
        }

        // GET: Requests_for_attachment/Create
        public ActionResult Create()
        {
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName");
            return View();
        }

        // POST: Requests_for_attachment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Requests_for_attachment_Id,DateOfCreationg,DateOfTreatments,PatientId,QueryStatus")] Requests_for_attachment requestsForAttachment)
        {
            if (ModelState.IsValid)
            {
                db.Requests_for_attachment.Add(requestsForAttachment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", requestsForAttachment.PatientId);
            return View(requestsForAttachment);
        }

        // GET: Requests_for_attachment/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests_for_attachment requestsForAttachment = await db.Requests_for_attachment.FindAsync(id);
            if (requestsForAttachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", requestsForAttachment.PatientId);
            return View(requestsForAttachment);



        }

        // POST: Requests_for_attachment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Requests_for_attachment_Id,DateOfCreationg,DateOfTreatments,PatientId,QueryStatus")] Requests_for_attachment requestsForAttachment)
        {
            if (ModelState.IsValid)
            {
                requestsForAttachment.DateOfTreatments = DateTime.Now;
                db.Entry(requestsForAttachment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", requestsForAttachment.PatientId);
            return View(requestsForAttachment);
        }

        // GET: Requests_for_attachment/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requests_for_attachment requestsForAttachment = await db.Requests_for_attachment.FindAsync(id);
            if (requestsForAttachment == null)
            {
                return HttpNotFound();
            }
            return View(requestsForAttachment);
        }

        // POST: Requests_for_attachment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Requests_for_attachment requestsForAttachment = await db.Requests_for_attachment.FindAsync(id);
            if (requestsForAttachment == null) return View("Index");
            db.Requests_for_attachment.Remove(requestsForAttachment);
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

        public async Task<ActionResult> GetPatientsByMedId(int medId)
        {
            var query = await db.Requests_for_attachment.Where(w => w.Patient.Med_Organization_Id == medId).ToListAsync();
            return PartialView(query);
        }
    }
}

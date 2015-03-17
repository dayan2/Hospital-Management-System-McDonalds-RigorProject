using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mcd.HospitalManagementSystem.Data;

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class WardDetailsController : Controller
    {
        private LP_HMSDbEntities db = new LP_HMSDbEntities();

        // GET: /WardDetails/
        public ActionResult Index()
        {
            return View(db.Wards.ToList());
        }

        // GET: /WardDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ward ward = db.Wards.Find(id);
            if (ward == null)
            {
                return HttpNotFound();
            }
            return View(ward);
        }

        //// GET: /WardDetails/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: /WardDetails/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="Id,WardNo,WardFee")] Ward ward)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Wards.Add(ward);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(ward);
        //}

        //// GET: /WardDetails/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ward ward = db.Wards.Find(id);
        //    if (ward == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ward);
        //}

        //// POST: /WardDetails/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="Id,WardNo,WardFee")] Ward ward)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(ward).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(ward);
        //}

        //// GET: /WardDetails/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ward ward = db.Wards.Find(id);
        //    if (ward == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ward);
        //}

        //// POST: /WardDetails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Ward ward = db.Wards.Find(id);
        //    db.Wards.Remove(ward);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

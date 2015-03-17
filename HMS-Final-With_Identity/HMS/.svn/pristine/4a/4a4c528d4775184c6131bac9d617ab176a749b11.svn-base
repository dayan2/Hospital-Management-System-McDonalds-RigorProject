using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mcd.HospitalManagementSystem.Data;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitaManagementSystem.Business.DTO;

namespace Mcd.HospitalManagement.Web.Controllers
{
    public class UserRoleController : Controller
    {
        private LP_HMSDbEntities db = new LP_HMSDbEntities();

         private IUserManager usermanager;

         public UserRoleController()
        {
            usermanager = new UserManager();
        }


        // GET: /UserRole/
        public ActionResult Index()
        {
            return View(usermanager.ViewtUserRole());
        }

        // GET: /UserRole/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserRoleDTO userrole = usermanager.ViewtUserRoleById(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // GET: /UserRole/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UserRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Role")] UserRoleDTO userrole)
        {
            if (ModelState.IsValid)
            {
                usermanager.InsertUserRole(userrole);
                return RedirectToAction("Index");
            }
            return View(userrole);
        }

        // GET: /UserRole/Edit/5
        public ActionResult Edit(int id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }
            UserRoleDTO userrole = usermanager.SelectUserRoleById(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // POST: /UserRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Role")] UserRoleDTO userrole)
        {
            if (ModelState.IsValid)
            {
                usermanager.EditUserRole(userrole);
                return RedirectToAction("Index");
            }
            return View(userrole);
        }

        // GET: /UserRole/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             UserRoleDTO userrole=usermanager.SelectUserRoleById(id);
            if (userrole == null)
            {
                return HttpNotFound();
            }
            return View(userrole);
        }

        // POST: /UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usermanager.DeleteUserRole(id);
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

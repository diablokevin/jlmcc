using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JLMCC.Models;

namespace JLMCC.Controllers
{
    public class SubDepartmentsController : Controller
    {
        private JlmccContext db = new JlmccContext();

        // GET: SubDepartments
        public ActionResult Index()
        {
            var subDepartments = db.SubDepartments.Include(s => s.Department);
            return View(subDepartments.ToList());
        }

        // GET: SubDepartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDepartment subDepartment = db.SubDepartments.Find(id);
            if (subDepartment == null)
            {
                return HttpNotFound();
            }
            return View(subDepartment);
        }

        // GET: SubDepartments/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            return View();
        }

        // POST: SubDepartments/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DepartmentId")] SubDepartment subDepartment)
        {
            if (ModelState.IsValid)
            {
                
                db.SubDepartments.Add(subDepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", subDepartment.DepartmentId);
            return View(subDepartment);
        }

        // GET: SubDepartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDepartment subDepartment = db.SubDepartments.Find(id);
            if (subDepartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", subDepartment.DepartmentId);
            return View(subDepartment);
        }

        // POST: SubDepartments/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DepartmentId")] SubDepartment subDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subDepartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", subDepartment.DepartmentId);
            return View(subDepartment);
        }

        // GET: SubDepartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubDepartment subDepartment = db.SubDepartments.Find(id);
            if (subDepartment == null)
            {
                return HttpNotFound();
            }
            return View(subDepartment);
        }

        // POST: SubDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubDepartment subDepartment = db.SubDepartments.Find(id);
            db.SubDepartments.Remove(subDepartment);
            db.SaveChanges();
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

        public ActionResult Multi()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Multi(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string content = Request["List"];


                ViewBag.ContentString = content;
                List<string> t = content.Split('\r', '\n').ToList();
                ViewBag.Content = t;
                ViewBag.Count = t.Count;

                foreach (string item in t)
                {
                    if (!string.IsNullOrEmpty(item))
                    {

                        SubDepartment subdep = new SubDepartment();
                        subdep.Name = item.Split('\t')[0];
                        string dep = item.Split('\t')[1];
                        subdep.DepartmentId = db.Departments.Where(m => m.Name ==dep ).First().Id;
                        db.SubDepartments.Add(subdep);

                        db.SaveChanges();
                    }
                }


                return View();
            }


            return View();
        }
    }
}

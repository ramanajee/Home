using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Home.DAL;
using Home.Models;
using Microsoft.AspNet.Identity;

namespace Home.Controllers
{
    [Authorize]
    public class GroceriesCategoriesController : Controller
    {
        private HomeContext db = new HomeContext();

        // GET: GroceriesCategories
        public ActionResult Index(string fakevalue)
        {
            var acc = User.Identity.GetUserId();
             
            if(fakevalue!=null)
            {
                return PartialView("PartialIndex", db.GroceriesCategory.Where(p=>p.AppUserId == acc).ToList());
            }
            return View(db.GroceriesCategory.Where(c=>c.AppUserId==acc ).ToList());
        }

        // GET: GroceriesCategories/Details/5
        public ActionResult Details(int? id)
        {
            var uid = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GroceriesCategories gc = db.GroceriesCategory.Where(p => p.AppUserId == uid && p.GroceriesCategoriesId == id).FirstOrDefault();
            //GroceriesCategories groceriesCategories = db.GroceriesCategory.Find(id);
            if (gc == null)
            {
                return HttpNotFound();
            }
            return View(gc);
        }

        // GET: GroceriesCategories/Create
        public ActionResult Create()
        {
            return PartialView("PartialCreate");
        }

        // POST: GroceriesCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroceriesCategoriesId,GroceriesName,Remarks,AppUserId")] GroceriesCategories EditGroceriesListForm)
        {
            var acc = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                var d = db.GroceriesCategory.Where(c => c.GroceriesName == EditGroceriesListForm.GroceriesName && c.AppUserId == acc).SingleOrDefault();
                if (d != null)
                {
                    return Json(new { success = "AlredyExist" });
                }

                db.GroceriesCategory.Add(EditGroceriesListForm);
                db.SaveChanges();
                return Json(new { success = "Success" });

                //return RedirectToAction("Index");
            }
            return Json(new { success = "fails" });


            //return View(EditGroceriesListForm);
        }

        // GET: GroceriesCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            var acc = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceriesCategories groceriesCategories = db.GroceriesCategory.Where(p=>p.AppUserId == acc && p.GroceriesCategoriesId == id).FirstOrDefault();
            if (groceriesCategories == null)
            {
                return HttpNotFound();
            }

            ViewBag.groceryCat = groceriesCategories.GroceriesName;
            var checkGroceryCategory = db.GroceriesLists.Where(c => c.GroceriesCategoriesId == groceriesCategories.GroceriesCategoriesId && c.appUserId == acc).FirstOrDefault();

            if (checkGroceryCategory != null)
            {
                var groceryListRecord = db.GroceriesLists.Where(c => c.GroceriesCategoriesId == groceriesCategories.GroceriesCategoriesId && c.appUserId == acc).ToList();

                return PartialView("PartialEditCheck", groceryListRecord);
            }
            else
            {
                return PartialView("PartialEdit", groceriesCategories);
            }
        }

        // POST: GroceriesCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroceriesCategoriesId,GroceriesName,Remarks,AppUserId")] GroceriesCategories EditGroceriesListForm)
        {
            var acc = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                var d = db.GroceriesCategory.Where(c => c.GroceriesCategoriesId != EditGroceriesListForm.GroceriesCategoriesId && c.GroceriesName == EditGroceriesListForm.GroceriesName && c.AppUserId == acc).SingleOrDefault();
                if(d!=null)
                {
                    return Json(new { success = "AlredyExist" });
                }
                
                
                db.Entry(EditGroceriesListForm).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = "naveen" });
                //return RedirectToAction("Index");
            }
            return Json(new { success = "fails" });
            //return View(groceriesCategories);
        }

        // GET: GroceriesCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            var acc = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceriesCategories groceriesCategories = db.GroceriesCategory.Where(p=>p.AppUserId == acc && p.GroceriesCategoriesId == id).FirstOrDefault();
            if (groceriesCategories == null)
            {
                return HttpNotFound();
            }
            ViewBag.groceryCat = groceriesCategories.GroceriesName;
            var checkGroceryCategory = db.GroceriesLists.Where(c => c.GroceriesCategoriesId == groceriesCategories.GroceriesCategoriesId && c.appUserId == acc).FirstOrDefault();

            if(checkGroceryCategory!=null)
            {
                var groceryListRecord = db.GroceriesLists.Where(c => c.GroceriesCategoriesId == groceriesCategories.GroceriesCategoriesId && c.appUserId == acc).ToList();

                return PartialView("PartialDeleteCheck", groceryListRecord);
            }
            else
            {
                return PartialView("PartialDelete", groceriesCategories);
            }

            //return View(groceriesCategories);
        }

        // POST: GroceriesCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var acc = User.Identity.GetUserId();
            //GroceriesCategories groceriesCategories = db.GroceriesCategory.Find(id);
            GroceriesCategories gc = db.GroceriesCategory.Where(p => p.AppUserId == acc && p.GroceriesCategoriesId == id).FirstOrDefault();
            db.GroceriesCategory.Remove(gc);
            db.SaveChanges();
            return Json(new { success = true });
           // return RedirectToAction("Index");
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

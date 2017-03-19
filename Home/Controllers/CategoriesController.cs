using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Home.DAL;
using Home.Models;
using System.Net;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace Home.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {

        // GET: Categories
        HomeContext db = new HomeContext();
         

        //this action method populates Homelist on checking the radio button
        public ActionResult GetHome(string id)
        {
            
            //string uid = Request.Cookies["userId"].Value.ToString();
            string uid = User.Identity.GetUserId();
            
            if (id.ToLower() == "income")
            {
                var drop = (from n in db.Categories
                            where n.TransactionType == "INCOME"
                            where n.ApplicationUsersIdCategory == uid || n.ApplicationUsersIdCategory == "all"
                            select new
                            {
                                n.CategoryId,
                                n.CategoryName,
                            }).ToList();
                return Json(drop.OrderBy(s => s.CategoryName), JsonRequestBehavior.AllowGet);
            }
            else if (id.ToLower() == "expenditure")
            {
                var drop = (from n in db.Categories
                            where n.TransactionType == "EXPENDITURE"
                            where n.ApplicationUsersIdCategory == uid || n.ApplicationUsersIdCategory == "all"
                            select new
                            {
                                n.CategoryId,
                                n.CategoryName
                            }).ToList();
                return Json(drop.OrderBy(s => s.CategoryName), JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "Error in loading the Items Plesase refresh" }, JsonRequestBehavior.AllowGet);
        }

        //this method add item to the Homelist dynamically

        [HttpPost]
        public ActionResult AddItem(Category category, string TransactionTypes)
        {
            if (category.CategoryName != null && TransactionTypes != null)
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, responseText = "data not saved perfectly" }, JsonRequestBehavior.AllowGet);
                }

                if (TransactionTypes.ToLower() == "income")
                {
                    var ItemCheck = db.Categories.Where(n => n.CategoryName.ToLower() == category.CategoryName.ToLower() && n.TransactionType == "INCOME").SingleOrDefault();
                    if (ItemCheck == null)
                    {
                        Category cat = new Category()
                        {
                            CategoryName = category.CategoryName,
                            TransactionType = "INCOME",
                            ApplicationUsersIdCategory = category.ApplicationUsersIdCategory
                        };
                        db.Categories.Add(cat);
                        db.SaveChanges();
                        var dbCategory = db.Categories.Where(c => c.CategoryName == category.CategoryName && c.TransactionType == "INCOME").SingleOrDefault();
                        return Json(new { category = dbCategory, success = true, responseText = category.CategoryName + " item is added to list" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = category.CategoryName + " name is already Exist In Income List" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (TransactionTypes.ToLower() == "expenditure")
                {
                    var ItemCheck = db.Categories.Where(n => n.CategoryName.ToLower() == category.CategoryName.ToLower() && n.TransactionType == "EXPENDITURE").SingleOrDefault();
                    if (ItemCheck == null)
                    {
                        Category cat = new Category()
                        {
                            CategoryName = category.CategoryName,
                            TransactionType = "EXPENDITURE",
                            ApplicationUsersIdCategory = category.ApplicationUsersIdCategory
                        };
                        db.Categories.Add(cat);
                        db.SaveChanges();
                        var dbCategory = db.Categories.Where(c => c.CategoryName == category.CategoryName && c.TransactionType == "EXPENDITURE").SingleOrDefault();
                        return Json(new { category = dbCategory, success = true, responseText = category.CategoryName + " item is added to list" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, responseText = category.CategoryName + "name is already Exist Already Exist In Expenditure List" }, JsonRequestBehavior.AllowGet);
                    }

                }

                return Json(new { success = false, responseText = "Some Error occured Please try again" }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(new { success = false, responseText = "Transaction type or category Should not be null" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Index(string TransactionCategory)
        {
            string uid = User.Identity.GetUserId();
             //string uid = Request.Cookies["userId"].Value.ToString();

            if (TransactionCategory != null)
            {
                if (TransactionCategory.ToLower() == "all")
                {
                    return PartialView("PartialIndex", db.Categories.Where(p=>p.ApplicationUsersIdCategory == uid).ToList());
                }
                else if (TransactionCategory.ToLower() == "income")
                {
                    var cat = db.Categories.Where(x => x.TransactionType.ToLower() == TransactionCategory.ToLower() && x.ApplicationUsersIdCategory == uid).ToList();
                    return PartialView("PartialIndex", cat);
                }
                else if (TransactionCategory.ToLower() == "expenditure")
                {
                    var cat = db.Categories.Where(x => x.TransactionType.ToLower() == TransactionCategory.ToLower() && x.ApplicationUsersIdCategory == uid).ToList();
                    return PartialView("PartialIndex", cat);
                }
            }
            return View(db.Categories.Where(p=>p.ApplicationUsersIdCategory == uid).ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            string uid = User.Identity.GetUserId();
            //string uid = Request.Cookies["userId"].Value.ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Where(p=>p.ApplicationUsersIdCategory == uid && p.CategoryId == id).FirstOrDefault();
            //Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            List<string> type = new List<string>();
            type.Add("Income");
            type.Add("Expenditure");
            ViewBag.type = new SelectList(type);
            return PartialView("PartialCreate");
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName,TransactionType,Remarks,ApplicationUsersIdCategory")] Category EditGroceriesListForm)
        {
            var acc = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (EditGroceriesListForm.TransactionType == "Income")
                {
                    var d = db.Categories.Where(c => c.CategoryName == EditGroceriesListForm.CategoryName && c.ApplicationUsersIdCategory == acc && c.TransactionType == EditGroceriesListForm.TransactionType).SingleOrDefault();
                    if (d != null)
                    {
                        return Json(new { success = "AlredyExist" });
                    }
                }
                else if (EditGroceriesListForm.TransactionType == "Expenditure")
                {
                    var d = db.Categories.Where(c => c.CategoryName == EditGroceriesListForm.CategoryName && c.ApplicationUsersIdCategory == acc && c.TransactionType == EditGroceriesListForm.TransactionType).SingleOrDefault();
                    if (d != null)
                    {
                        return Json(new { success = "AlredyExist" });
                    }
                }


                db.Categories.Add(EditGroceriesListForm);
                db.SaveChanges();
                return Json(new { success = "Success" });
                //return RedirectToAction("Index");
            }
            return Json(new { success = "fails" });
            //return View(EditGroceriesListForm);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            string uid = User.Identity.GetUserId();
            //string uid = Request.Cookies["userId"].Value.ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.Where(p => p.CategoryId == id && p.ApplicationUsersIdCategory == uid).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat = category.CategoryName;
            var checkCategory = db.Transactions.Where(c => c.CategoryId == category.CategoryId && c.ApplicationUsersId == uid).FirstOrDefault();
            if (checkCategory != null)
            {
                var transactionRecords = db.Transactions.Where(c => c.CategoryId == category.CategoryId && c.ApplicationUsersId == uid).ToList();
                return PartialView("PartialEditCheck", transactionRecords.ToList());
            }
            else
            {
                return PartialView("PartialEdit", category);
            }
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName,TransactionType,Remarks,ApplicationUsersIdCategory")] Category EditGroceriesListForm)
        {
            //string uid = Request.Cookies["userId"].Value.ToString();
            string uid = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                if (EditGroceriesListForm.TransactionType == "Income")
                {
                    var d = db.Categories.Where(c => c.CategoryName == EditGroceriesListForm.CategoryName && c.ApplicationUsersIdCategory == uid && c.TransactionType == EditGroceriesListForm.TransactionType).SingleOrDefault();
                    if (d != null)
                    {
                        return Json(new { success = "AlredyExist" });
                    }
                }
                else if (EditGroceriesListForm.TransactionType == "Expenditure")
                {
                    var d = db.Categories.Where(c => c.CategoryName == EditGroceriesListForm.CategoryName && c.ApplicationUsersIdCategory == uid && c.TransactionType == EditGroceriesListForm.TransactionType).SingleOrDefault();
                    if (d != null)
                    {
                        return Json(new { success = "AlredyExist" });
                    }
                }



                db.Entry(EditGroceriesListForm).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = "naveen" });
                //return RedirectToAction("Index");
            }
            return Json(new { success = "fails" });
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            string uid = User.Identity.GetUserId();
            //string uid = Request.Cookies["userId"].Value.ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.Where(p => p.ApplicationUsersIdCategory == uid && p.CategoryId == id).FirstOrDefault();
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cat = category.CategoryName;
            var checkCategory = db.Transactions.Where(c => c.CategoryId == category.CategoryId && c.ApplicationUsersId == uid).FirstOrDefault();
            if (checkCategory != null)
            {
                var transactionRecords = db.Transactions.Where(c => c.CategoryId == category.CategoryId && c.ApplicationUsersId == uid).ToList();
                return PartialView("PartialDeleteCheck", transactionRecords.ToList());
            }
            else
            {


                return PartialView("PartialDelete", category);
            }
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string uid = User.Identity.GetUserId();
            //string uid = Request.Cookies["userId"].Value.ToString();
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.Where(p => p.ApplicationUsersIdCategory== uid && p.CategoryId == id).FirstOrDefault();
            db.Categories.Remove(category);
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
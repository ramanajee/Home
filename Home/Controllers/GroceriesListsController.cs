using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Home.DAL;
using Home.Models;

namespace DropDown.Controllers
{
    [Authorize]

    public class GroceriesListsController : Controller
    {
        private HomeContext db = new HomeContext();

        // GET: GroceriesLists

        public ActionResult AddNewCategory()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddNewCategory(string NewGrocery, String AppId)
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, responseText = "data not saved perfectly" }, JsonRequestBehavior.AllowGet);
            }

            var groc =  db.GroceriesCategory.Where(n => (n.AppUserId=="all"||n.AppUserId==uid)&&n.GroceriesName.ToLower() == NewGrocery.ToLower()).SingleOrDefault();
            if (groc == null)
            {
                GroceriesCategories g = new GroceriesCategories()
                {
                    GroceriesName = NewGrocery,
                    AppUserId = AppId
                };
                db.GroceriesCategory.Add(g);
                 db.SaveChanges();
                 var dbgroc = db.GroceriesCategory.Where(n => (n.AppUserId == "all" || n.AppUserId == uid) && n.GroceriesName.ToLower() == NewGrocery.ToLower()).SingleOrDefault();
                return Json(new { category = dbgroc, success = true, responseText = NewGrocery + " item is added to list" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, responseText = NewGrocery + " name is already Exist In Income List" }, JsonRequestBehavior.AllowGet);

            }


        }

        public ActionResult Remove(int id)
        {
            GroceriesList groceriesList = db.GroceriesLists.Find(id);
            db.GroceriesLists.Remove(groceriesList);
            db.SaveChanges();
            return Json(new { success = true });
        }
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            string uid = Request.Cookies["userId"].Value.ToString();

            //var groceriesLists = db.GroceriesLists.Include(g => g.GroceriesCategory).OrderBy(c => c.GroceriesCategoriesId);
            var groceriesLists = db.GroceriesLists.Include(g => g.GroceriesCategory).Where(p => p.appUserId == uid).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                //groceriesLists = groceriesLists.Where(c => c.GroceriesCategoriesId.ToString() == (searchString)).OrderBy(c => c.GroceriesCategoriesId);
                groceriesLists = groceriesLists.Where(c => c.GroceriesCategoriesId.ToString() == (searchString)).ToList();
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return PartialView("_GroceriesList", groceriesLists.ToPagedList(pageNumber, pageSize));
        }

        // GET: GroceriesLists/Details/5

        //notused
        public ActionResult ShoppingLists()
        {
            var shoppingLists = db.ShoppingNames.ToList();
            return View(shoppingLists);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceriesList groceriesList = db.GroceriesLists.Find(id);
            if (groceriesList == null)
            {
                return HttpNotFound();
            }
            return View(groceriesList);
        }

        // GET: GroceriesLists/Create
        public ActionResult Create()
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            ViewBag.shoppingNameId = new SelectList(db.ShoppingNames.ToList().Where(p=>p.AppUserId==uid), "ShoppingNameID", "Name");
            ViewBag.GroceriesCategoriesId = new SelectList(db.GroceriesCategory.Where(p=>p.AppUserId==uid||p.AppUserId=="all"), "GroceriesCategoriesId", "GroceriesName");
            return View();
        }

        // POST: GroceriesLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroceriesListId,ItemName,GroceriesCategoriesId,appUserId")] GroceriesList createGroceriesListForm)
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            if (ModelState.IsValid)
            {
                try
                {
                    db.GroceriesLists.Add(createGroceriesListForm);
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("error", ex.Message);
                }
                 
                //List<string> l = db.GroceriesLists.OrderBy(c => c.GroceriesCategoriesId).Select(c => c.ItemName).ToList();
                //decimal page=0;
                //foreach (var item in l)
                //{
                //    page++;
                //    if(item==createGroceriesListForm.ItemName)
                //    {

                //        break;
                //    }

                //}
                //page = page / 5;
                //var wholeNumber = (int)Math.Ceiling(page);

                return Json(new { responseText = "Saved successfully" });
            }

            ViewBag.GroceriesCategoriesId = new SelectList(db.GroceriesCategory.Where(p=>p.AppUserId==uid||p.AppUserId=="all"), "GroceriesCategoriesId", "GroceriesName", createGroceriesListForm.GroceriesCategoriesId);
            return View(createGroceriesListForm);
        }

        // GET: GroceriesLists/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceriesList groceriesList = db.GroceriesLists.Find(id);
            if (groceriesList == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroceriesCategoriesId = new SelectList(db.GroceriesCategory.Where(p=>p.AppUserId==uid||p.AppUserId=="all"), "GroceriesCategoriesId", "GroceriesName", groceriesList.GroceriesCategoriesId);
            return PartialView("GroceriesEdit", groceriesList);
        }

        // POST: GroceriesLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroceriesList grocery)
        {
            int? id = grocery.GroceriesListId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var groceryList = db.GroceriesLists.Find(id);
            if (TryUpdateModel(groceryList, "", new string[] { "ItemName", "GroceriesCategoriesId" }))
            {
                try
                {
                    db.SaveChanges();

                    return Json(new { responseText = "Saved successfully" });
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            //if (ModelState.IsValid)
            //{
            //    System.Threading.Thread.Sleep(7000);
            //    db.Entry(groceriesList).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return Json(new { responseText = "Saved successfully" });
            //}
            ViewBag.GroceriesCategoriesId = new SelectList(db.GroceriesCategory, "GroceriesCategoriesId", "GroceriesName", groceryList.GroceriesCategoriesId);
            return View(groceryList);
        }

        // GET: GroceriesLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroceriesList groceriesList = db.GroceriesLists.Find(id);
            if (groceriesList == null)
            {
                return HttpNotFound();
            }
            return PartialView("GroceriesDelete", groceriesList);
        }


        //not used
        // POST: GroceriesLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GroceriesList groceriesList = db.GroceriesLists.Find(id);
        //    db.GroceriesLists.Remove(groceriesList);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

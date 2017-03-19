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

namespace Home.Controllers
{
    [Authorize]

    public class ShoppingListsController : Controller
    {
        private HomeContext db = new HomeContext();

        // GET: ShoppingLists


        public ActionResult DeleteShoppingTable(string Name)
        {
            var shop = db.ShoppingNames.Where(s => s.Name == Name).Single();
            db.ShoppingNames.Remove(shop);
            db.SaveChanges();
            return RedirectToAction("Create", "GroceriesLists");
        }
        public ActionResult Remove(int id)
        {
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            db.ShoppingLists.Remove(shoppingList);
            db.SaveChanges();
            return Json(new { success = true });
        }
        public ActionResult ShoppingNameAdd(string Name)
        {
            var shoppingName = new ShoppingName
            {
                Name = System.Uri.UnescapeDataString(Name),
                AppUserId = Request.Cookies["userId"].Value.ToString()
            };
            db.ShoppingNames.Add(shoppingName);
            db.SaveChanges();
            return Json(new { });
        }
        public ActionResult Index(int shopname)
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            var shoppingLists = db.ShoppingLists.Include(s => s.ShoppingName).Where(n => n.ShoppingName.ShoppingNameID == shopname && n.userID == uid);
            return PartialView("_Index", shoppingLists.ToList());
        }
        public ActionResult CreatedLists(int shopname)
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            var shoppingLists = db.ShoppingLists.Include(s => s.ShoppingName).Where(n => n.ShoppingName.ShoppingNameID == shopname && n.userID == uid);
            return PartialView(shoppingLists.ToList());
        }

        // GET: ShoppingLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            return View(shoppingList);
        }

        public ActionResult CreateShop(int GroceryId, string NameYou)
        {
            var grocery = db.GroceriesLists.Where(c => c.GroceriesListId == GroceryId).Include(s => s.GroceriesCategory).SingleOrDefault();
            ViewBag.groceryItem = grocery.ItemName;
            ViewBag.groceryCategory = grocery.GroceriesCategory.GroceriesName;
            int selectedName = db.ShoppingNames.Where(n => n.Name == NameYou).Select(c => c.ShoppingNameID).SingleOrDefault();

            ViewBag.ShoppingNameID = new SelectList(db.ShoppingNames, "ShoppingNameID", "Name", selectedName);
            return PartialView("ShoppingListCreate");
        }
        // GET: ShoppingLists/Create
        public ActionResult Create()
        {

            ViewBag.ShoppingNameID = new SelectList(db.ShoppingNames, "ShoppingNameID", "Name");
            return PartialView("ShoppingListCreate");
        }

        // POST: ShoppingLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "ShoppingListID,Item,Category,PricePerPacket,Quantity,Amount,ShoppingNameID")]
        public ActionResult Create(ShoppingList createShoppingListForm)
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            if (ModelState.IsValid)
            {
                createShoppingListForm.Amount = createShoppingListForm.PricePerPacket * createShoppingListForm.Quantity;
                createShoppingListForm.userID = uid;
                db.ShoppingLists.Add(createShoppingListForm);
                db.SaveChanges();
                return Json(new { });
            }

            ViewBag.ShoppingNameID = new SelectList(db.ShoppingNames, "ShoppingNameID", "Name", createShoppingListForm.ShoppingNameID);
            return View(createShoppingListForm);
        }

        // GET: ShoppingLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShoppingNameID = new SelectList(db.ShoppingNames, "ShoppingNameID", "Name", shoppingList.ShoppingNameID);
            return PartialView("_Edit", shoppingList);
        }

        // POST: ShoppingLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ShoppingListID,Item,Category,PricePerPacket,Quantity,Amount,ShoppingNameID")] ShoppingList shoppingList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingList).State = EntityState.Modified;
                shoppingList.Amount = shoppingList.PricePerPacket * shoppingList.Quantity;
                db.SaveChanges();
                return Json(new { responseText = "Saved successfully" });
            }
            ViewBag.ShoppingNameID = new SelectList(db.ShoppingNames, "ShoppingNameID", "Name", shoppingList.ShoppingNameID);
            return View(shoppingList);
        }

        // GET: ShoppingLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            if (shoppingList == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", shoppingList);
        }

        //not used

        // POST: ShoppingLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingList shoppingList = db.ShoppingLists.Find(id);
            db.ShoppingLists.Remove(shoppingList);
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
    }
}

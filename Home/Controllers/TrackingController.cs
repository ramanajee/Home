using Home.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Home.Models;
using System.Net;
using System.Net.Sockets;

namespace Home.Controllers
{
    [Authorize]
    public class TrackingController : Controller
    {
        private HomeContext db = new HomeContext();
        // GET: Tracking
        public ActionResult Index()
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            ViewBag.CategoryId = new SelectList(db.Categories.Where(n => n.TransactionType == "INCOME").OrderBy(s => s.CategoryName).Where(p=>p.ApplicationUsersIdCategory == uid).Where(p=>p.ApplicationUsersIdCategory == "all"), "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Reports(string id)
        {
            string uid = Request.Cookies["userId"].Value.ToString();
            if (id == "ShowLast")
            {
                int last = Convert.ToInt32(Request["Last"]);
                string type = Request["cash"];
                if (type == "EXPENDITURE")
                {
                    var result = (from n in db.Transactions
                                  where n.TransactionTypes == TransactionType.Expenditure
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid).Take(last);
                    return View(result.ToList());

                }
                else
                {
                    var result = (from n in db.Transactions
                                  where n.TransactionTypes == TransactionType.Income
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid).Take(last);
                    return View(result.ToList());

                }
            }
            else if (id == "FromDate")
            {
                DateTime From = Convert.ToDateTime(Request["startDate"]);
                DateTime To = Convert.ToDateTime(Request["endDate"]);

                string type = Request["cash"];
                if (type == "EXPENDITURE")
                {
                    var result = (from n in db.Transactions
                                  where n.DateAction >= From.Date
                                  where n.DateAction <= To.Date
                                  where n.TransactionTypes == TransactionType.Expenditure
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid);
                    return View(result.ToList());

                }
                else
                {
                    var result = (from n in db.Transactions
                                  where n.DateAction >= From.Date
                                  where n.DateAction <= To.Date
                                  where n.TransactionTypes == TransactionType.Income
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid);
                    return View(result.ToList());
                }
            }
            else if (id == "Range")
            {
                int start = Convert.ToInt32(Request["start"]);
                int end = Convert.ToInt32(Request["end"]);
                string type = Request["cash"];

                if (type == "EXPENDITURE")
                {
                    var result = (from n in db.Transactions
                                  where n.Amount >= start && n.Amount <= end
                                  where n.TransactionTypes == TransactionType.Expenditure
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid);
                    return View(result.ToList());

                }
                else
                {
                    var result = (from n in db.Transactions
                                  where n.Amount >= start && n.Amount <= end
                                  where n.TransactionTypes == TransactionType.Income
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid);
                    return View(result.ToList());
                }

            }
            else if (id == "Cat")
            {
                int category = Convert.ToInt32(Request["CategoryId"]);
                string type = Request["cash"];

                if (type == "EXPENDITURE")
                {
                    var result = (from n in db.Transactions
                                  where n.Categories.CategoryId == category
                                  where n.TransactionTypes == TransactionType.Expenditure
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid);
                    return View(result.ToList());

                }
                else
                {
                    var result = (from n in db.Transactions
                                  where n.Categories.CategoryId == category
                                  where n.TransactionTypes == TransactionType.Income
                                  select n).OrderByDescending(p => p.TransactionId).Where(p=>p.ApplicationUsersId == uid);
                    return View(result.ToList());
                }

            }
            else if (id == "Search")
            {
                string keyword = Request["keyword"].ToLower();
                if (keyword=="income")
                {
                    var data = (from n in db.Transactions
                               where n.TransactionTypes == TransactionType.Income
                               select n).Where(p=>p.ApplicationUsersId == uid).ToList();
                    return View(data);
                }
                else if (keyword == "expenditure")
                {
                    var data = (from n in db.Transactions
                               where n.TransactionTypes == TransactionType.Expenditure
                               select n).Where(p=>p.ApplicationUsersId == uid).ToList();
                    return View(data);
                }
                else
                {
                    var data = from n in db.Transactions
                               where n.Description.ToLower().Contains(keyword) ||
                                     n.Categories.CategoryName.ToLower().Contains(keyword) ||
                                     n.TransactionName.ToLower().Contains(keyword)
                               select n;
                    return View(data.Where(p=>p.ApplicationUsersId == uid));
                }
            }
            else
                return View();
        }
    }
}
using Home.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Home.Controllers
{
    [Authorize]


    public class ReportsController : Controller
    {
        HomeContext db = new HomeContext();
        // GET: Reports
        public ActionResult Index()
        {
            var data = (
                        from n in db.Transactions
                        select n
                       ).ToList();

            return View(data);
        }
    }
}
using Home.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Home.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.con = true;
            HomeContext db = new HomeContext();
            var uid = User.Identity.GetUserId();
            var data = (from n in db.Transactions
                        where n.ApplicationUsersId == uid
                        select n).OrderByDescending(p => p.TransactionId).Take(5).ToList();
            return View(data);
        }
    }
}
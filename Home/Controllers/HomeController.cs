using Home.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Home.Controllers
{
    public class HomeController : Controller
    {
        private HomeContext ctx = new HomeContext();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index","User");
            }
            return View();
        }


        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //public ActionResult Register(Register register)
        //{
        //    ctx.Register.Add(register);
        //    ctx.SaveChanges();
        //    return View();
        //}
    }
}
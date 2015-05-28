using DigiDepot.Models;
using DigiDepot.DataHandlers;
using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigiDepot.Controllers
{
    public class HomeController : Controller
    {
        IDataHandler<Product> iproductdat = new ProductFlatFile();
        IDataHandler<BillingInformation> ibillingdat = new BillingFlatFile();
        IDataHandler<User> iuserdat = new UserDB();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Catalog()
        {
            List<Product> passed = iproductdat.GetAllItems().ToList();
            return View(passed);
        }

        public ActionResult ProductPage(int id)
        {
            return View(iproductdat.Get(id));
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User user)
        {
            user = iuserdat.Get(user);
            if (user != null)
            {
                Session["UserInfo"] = user;
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (user != null && user.user_name.Length > 0 && user.password.Length >0)
            {
                iuserdat.Create(user);
                Session["UserInfo"] = user;
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
    }
}
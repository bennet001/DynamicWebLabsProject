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
        IDataHandler<User> iuserdat = new UserFlatFile();
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
            if (ModelState.IsValid)
            {
                iuserdat.Create(user);
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
    }
}
using DigiDepot.Models;
using DigiDepot.DataHandlers;
using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelloMVC.MyValidations;

namespace DigiDepot.Controllers
{
    public class HomeController : Controller
    {
        IDataHandler<Product> iproductdat = new ProductDB();
        IDataHandler<BillingInfo> IBilling = new IBillingInfoDB();
        //IDataHandler<BillingInformation> ibillingdat = new BillingFlatFile();
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

        #region LogIn
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
                Session["UserInfo"] = user.user_name;
                Session["UserID"] = user.Id;
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
        #endregion

        #region LogOff
        public ActionResult LogOut()
        {
            Session["UserInfo"] = null;
            Session["UserID"] = null;
            return RedirectToAction("Index");
        }
        #endregion

        #region Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (user != null && user.user_name.Length > 0 && user.password.Length > 0 && EmailValidation.IsValid(user.e_mail_address))
            {
                iuserdat.Create(user);
                IBilling.Create(new BillingInfo());
                
                Session["UserInfo"] = user.user_name;
                Session["UserID"] = user.Id;
                return RedirectToAction("index");
            }
            else
            {
                return View();
            }
        }
        #endregion


        #region User_Info
        public ActionResult UserPage()
        {
            int id = -1;
            if (int.TryParse(Session["UserID"].ToString(), out id))
            {
                return View(iuserdat.Get(id));
            }
            else
            {
                return View("Index");
            }
        }

        #region EmailEditing
        [HttpGet]
        public ActionResult EmailEdit(int id)
        {
            User got = iuserdat.Get(id);
            if (got != null)
            {
                return View(got);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        public ActionResult EmailEdit(User use)
        {
            iuserdat.Update(use);
            return View("Index");
        }
        #endregion

        #region Edit_Billing_Info
        public ActionResult BillingInfo(int id)
        {
            return View(IBilling.Get(id));
        }

        [HttpGet]
        public ActionResult EditBilling(int id)
        {
            return View(IBilling.Get(id));
        }

        [HttpPost]
        public ActionResult EditBilling(BillingInfo given)
        {
            IBilling.Update(given);
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult AccountDelete(int id)
        {
            Session["UserInfo"] = null;
            Session["UserID"] = null;
            iuserdat.Delete(iuserdat.Get(id));
            return View("Index");
        }
        #endregion
    }
}
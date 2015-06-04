using DigiDepot.DataHandlers;
using DigiDepot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigiDepot.Controllers
{
    public class ProfileController : Controller
    {
        IBillingInfoDB accountDB = new IBillingInfoDB();
        public ActionResult UserPage()
        {
            //get the user info from the session if it exists or from a database based on id 
            //we know which id to use because the user is logged in
            //alternately, hard code a user for demonstration
            if (System.Web.HttpContext.Current.Session["currentUser"] == null)
            {
                //System.Web.HttpContext.Current.Session["currentUser"] = hardCodedUser;
            }
            return View(System.Web.HttpContext.Current.Session["currentUser"]);
        }

        public ActionResult EditUser()
        {
            //we need a user object to pre populate fields in the edit form
            //so we get it from the session if it exists or get it from the database otherwise
            //again, "get it from the db" means hard code it, because we dont have a db yet
            if (System.Web.HttpContext.Current.Session["currentUser"] == null)
            {
                //System.Web.HttpContext.Current.Session["currentUser"] = hardCodedUser;
            }
            return View(System.Web.HttpContext.Current.Session["currentUser"]);
        }

        [HttpPost]
        public ActionResult UpdateProfile(BillingInfo update)
        {
            //code to contact the database and update the user
            //store the updated user object in the session
            //for the demo, as we are sans-DB, I construct a new User object using form data and replace the session data with that
            accountDB.Update(update);
            return Redirect("UserPage");//redirect to the profile page
        }
    }
}
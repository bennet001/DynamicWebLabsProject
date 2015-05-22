using DigiDepot.DAL;
using DigiDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigiDepot.Controllers
{
    public class CatalogController : Controller
    {
        CartDBDAL cartDAL = new CartDBDAL();
        // GET: Catelog
        public ActionResult CatalogPage(List<Product> testList = null)
        {
            if (testList == null)
            {
                //TestCart = new List<Product>{
                //    new Product("1", "test1", "$1.99"),
                //    new Product("2", "test2", "$5.99")
                //};
            }
            else
            {
                //TestCart = testList;
            }

            // return View("CatelogPage", TestCart);
            CreateTestCase();
            IEnumerable<Cart> display = cartDAL.GetAllCarts();
            return View("CatalogPage", display);
        }

        //   public ActionResult AddToCart(int id)
        //   {
        //  List<Product> tempCart = TestCart;
        // Product adding = new Product(p.ID, p.Name, p.Price);
        //  tempCart.Add(TestCart[id - 1]);
        //This shows that the item is being added
        //return View(test[id]);
        //  TestCart = tempCart;
        //  return RedirectToAction("ViewCart", TestCart);
        // }

        //   public ActionResult ViewCart(List<Product> cart)
        //   {
        //       return View(cart);
        //  }

        public void CreateTestCase()
        {
            IEnumerable<Product> testProducts = new List<Product>{
                new Product(0,"TestSoap","1.35",1),
                new Product(1,"TestBagel","0.99",3),
                new Product(2,"TestPaper","2.0",4),
                new Product(3,"TestGeneric","99.99",1)
            };
            Cart testCart = new Cart(1, testProducts);
            cartDAL.UpdateCart(testCart);
        }
    }
}
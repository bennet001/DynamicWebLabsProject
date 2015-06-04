//using DigiDepot.DAL;
using DigiDepot.DataHandlers;
using DigiDepot.Interfaces;
using DigiDepot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigiDepot.Controllers
{
    public class CatalogController : Controller
    {
        IDataHandler<Cart> icartdat = new CartDB();
        IDataHandler<Product> iproductdat = new ProductDB();

        // GET: Catelog
        public ActionResult CatalogPage()
        {

            //CreateTestCase();
            //IEnumerable<Cart> display = cartDAL.GetAllCarts();
            IEnumerable<Product> ProductList = iproductdat.GetAllItems();
            List<Product> theList = ProductList.ToList();
            return View("CatalogPage", theList);
        }

        public ActionResult ShowProduct(int id)
        {
            Product ShowMe = iproductdat.Get(id);
            return View(ShowMe);
        }

        public ActionResult AddToCart(int id)
        {
            Product adding = iproductdat.Get(id);
            Cart firstCart = icartdat.Get(0);
            icartdat.Update(firstCart, adding.Id, 1);
            long theId = id;
            //This shows that the item is being added
            //return RedirectToAction("ShowProduct", new { id = theId });
            return RedirectToAction("CatalogPage");
        }
        public ActionResult ViewCart(int id)
        {
            Cart ViewMe = icartdat.Get(id);

            return View(ViewMe);
        }

        public void CreateTestCase()
        {
            //IEnumerable<Product> testProducts = new List<Product>{
            //    new Product(0,"TestSoap",1.35),
            //    new Product(1,"TestBagel",0.99),
            //    new Product(2,"TestPaper",2.0),
            //    new Product(3,"TestGeneric",99.99)
            //};
            //IEnumerable<Product> empty = new List<Product>();
            //Cart addedTest = new Cart(0, empty);
            //addedTest.ItemsInCart.Add(new Product(0, "TestSoap", 1.35));
            //addedTest.ItemsInCart.Add(new Product(1, "TestBagel", 0.99));
            //IEnumerable<Cart> testCart = new List<Cart> {
            //    addedTest
            //};
            //icartdat.Create(addedTest);
            //iproductdat.Save(testProducts);
            //icartdat.Save(testCart);
            ////Cart testCart = new Cart(1, testProducts);
            ////cartDAL.UpdateCart(testCart);
        }
    }
}
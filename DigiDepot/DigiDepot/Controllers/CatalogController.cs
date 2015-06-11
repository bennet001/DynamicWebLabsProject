using DigiDepot.DataHandlers;
using DigiDepot.Interfaces;
using DigiDepot.ViewModels;
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
        IDataHandler<BillingInfo> ibillingdat = new IBillingInfoDB();
        // GET: Catelog
        public ActionResult CatalogPage(IEnumerable<Product> ProductList)
        {

            //CreateTestCase();
            //IEnumerable<Cart> display = cartDAL.GetAllCarts();
            ProductList = ProductList ?? iproductdat.GetAllItems();
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
            int userID = (int)Session["UserID"];
            Product adding = iproductdat.Get(id);
            Cart firstCart = icartdat.Get(userID);
            icartdat.Update(firstCart, adding.Id, 1);
            long theId = id;
            //This shows that the item is being added
            //return RedirectToAction("ShowProduct", new { id = theId });
            return RedirectToAction("CatalogPage");
        }

        public ActionResult RemoveFromCart(int id)
        {
            int userID = (int)Session["UserID"];
            Product removing = iproductdat.Get(id);
            Cart car = icartdat.Get(userID);
            //icartdat.Update(car,removing)
            icartdat.Remove(car, removing.Id);

            return RedirectToAction("CatalogPage");
        }

        public ActionResult ViewCart()
        {
            int id = (int)Session["UserID"];
            CartViewModel Look = new CartViewModel(id);

            return View(Look);
        }

        public ActionResult Checkout()
        {
            int id = (int)Session["UserID"];
            CartViewModel buyMe = new CartViewModel(id);

            return View(buyMe);
        }

        public ActionResult CheckBilling()
        {
            int id = (int)Session["UserID"];
            BillingInfo checkMe = ibillingdat.Get(id);
            return View(checkMe);
        }

        public ActionResult ConfirmPurchase()
        {
            int id = (int)Session["UserID"];
            CartViewModel finalize = new CartViewModel(id);
            finalize.purchaseProduct(finalize.CartID);

            return RedirectToAction("CatalogPage");
        }

        [HttpGet]
        public ActionResult EditInCart(int cartId, int prodID)
        {
            CartViewModel editing = new CartViewModel(cartId);
            ProductViewModel editMe = editing.getProductModel(prodID);

            return View(editMe);
        }

        [HttpPost]
        public ActionResult EditInCart(int id, int cID, int qty)
        {
            CartViewModel editing = new CartViewModel(cID);
            editing.editProduct(cID, id, qty);

            return RedirectToAction("ViewCart", new { id = cID });
        }

        [HttpGet]
        public ActionResult ConfirmCreditCard()
        {
            int id = (int)Session["UserID"];
            BillingInfo card = ibillingdat.Get(id);
            return View(card);
        }
        [HttpPost]
        public ActionResult ConfirmCreditCard(string code)
        {
            short input;
            short.TryParse(code, out input);
            int id = (int)Session["UserID"];
            BillingInfo card = ibillingdat.Get(id);
            if (card.Security == input)
            {
                return RedirectToAction("ConfirmPurchase");
            }
            else
            {
                return RedirectToAction("ConfirmCreditCard");
            }
        }

        public ActionResult Search(string query)
        {
            return CatalogPage(iproductdat.Search(query));
        }

        public void CreateTestCase()
        {
            Cart testCart = new Cart();
            testCart.ProductIDs = "";
            testCart.ProductQuantity = "";
            icartdat.Create(testCart);
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
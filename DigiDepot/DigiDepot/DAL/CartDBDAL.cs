using DigiDepot.Interfaces;
using DigiDepot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DigiDepot.DAL
{
    public class CartDBDAL : ICartDAL
    {
        static List<Cart> cartLot = new List<Cart>();
        string path = HttpContext.Current.Server.MapPath("~");

        string filelocal = @"\App_Data\Carts.txt";

        public IEnumerable<Cart> GetAllCarts()
        {
            //The cart will be designated by a cartID, inside it will have several productID's,
            //those id's will be used to reference to the products
            // Which will then be used to fill each cart object.
            //lines designates the individual lines to read
            string[] lines = File.ReadAllLines(path + filelocal);
            string separationPatternCart = @"CartID:(?<carID>\d+)";
            string sepPatternProducts = @"ProductID:(?<proID>\d+) Quantity:(?<quant>\d+)";
            List<Cart> returnThis = new List<Cart>();
            foreach (string cart in lines)
            {
                Match match = Regex.Match(cart, separationPatternCart);
                if (match.Success)
                {
                    int cID = -1;
                    int.TryParse(match.Groups["carID"].ToString(), out cID);
                    if (cID != -1)
                    {
                        //if(cartLot[cID] == null)
                        //{
                        //    cartLot[cID] = new Cart(cID, null);
                        //}
                        //else
                        //{
                        // List<Product> containedInCart = new List<Product>();
                        Dictionary<long, int> containedData = new Dictionary<long, int>();
                        foreach (string p in lines)
                        {
                            Match proMatch = Regex.Match(p, sepPatternProducts);
                            if (proMatch.Success)
                            {
                                int pId = -1;
                                int pQuant = 0;
                                int.TryParse(proMatch.Groups["proID"].ToString(), out pId);
                                int.TryParse(proMatch.Groups["quant"].ToString(), out pQuant);
                                if (pId != -1 && pQuant > 0)
                                {
                                    int productID = pId;
                                    int quantity = pQuant;
                                    containedData.Add(productID, quantity);
                                    //Product Item = productDAL.GetProduct(pId);
                                    //Item.GrabbedQuantity = pQuant;
                                    //containedInCart.Add(Item);
                                }
                            }
                        }
                        cartLot[cID - 1].ItemDictionary = containedData;
                        //cartLot[cID].ItemsInCart = containedInCart;
                        //}
                        returnThis.Add(cartLot[cID - 1]);
                    }
                }
            }
            cartLot = returnThis;
            return cartLot;

        }

        public Cart GetIndivudualCart(int cartID)
        {
            if (cartLot.Count() == 0)
            {
                cartLot = (List<Cart>)GetAllCarts();
            }
            Cart grabMe = (Cart)cartLot.Where(x => x.CartID == cartID);
            return grabMe;
        }

        public void AddItemToCart(int cartID, Product p, int quantity)
        {
            Cart addingTo = GetIndivudualCart(cartID);
            //Product addMe = GetIndivudualProduct(p.ID);
            //addMe.GrabbedQuantity = quantity;
            //if(addingTo.ItemsInCart.Contains(addMe))
            //{
            //    addingTo.ItemsInCart.Where(x => x.ID == addMe.ID).GrabbedQuantity += quantity;
            //}
            //else
            //{
            //    addingTo.ItemsInCart.Add(addMe);
            //}
            UpdateCart(addingTo);
            throw new NotImplementedException();

        }
        public void RemoveItemFromCart(int cartID, int productID)
        {
            Cart removingFrom = GetIndivudualCart(cartID);
            Product removeThis = (Product)removingFrom.ItemsInCart.Where(x => x.ID == productID);
            removingFrom.ItemsInCart.Remove(removeThis);
            UpdateCart(removingFrom);

        }
        public void UpdateItemInCart(int cartID, Product p)
        {
            throw new NotImplementedException();

        }
        public void UpdateCart(Cart singleCart)
        {
            StreamWriter file = new StreamWriter(path + filelocal);
            file.WriteLine(singleCart.ToString());
            file.Close();
            cartLot.Add(singleCart);

        }

    }
}
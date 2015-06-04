using DigiDepot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiDepot.Interfaces
{
    public interface ICartDAL
    {
        IEnumerable<Cart> GetAllCarts();
        //Cart GetIndivudualCart(int userID);
        //void AddItemToCart(int userID, int productID, int quantity);
        //void RemoveItemFromCart(int userID, Product p);
        //void UpdateItemInCart(int userID, Product p);

        Cart GetIndivudualCart(int cartID);
        void AddItemToCart(int cartID, Product p, int quantity);
        void RemoveItemFromCart(int cartID, int productID);
        void UpdateItemInCart(int cartID, Product p);
        //Update will act as the save method for the carts data.
        void UpdateCart(Cart singleCart);


    }
}

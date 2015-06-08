using DigiDepot.DataHandlers;
using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot.ViewModels
{
    public class CartViewModel
    {
        public CartViewModel(int ID)
        {
            CartID = ID;
            IDataHandler<Cart> icartdat = new CartDB();
            IDataHandler<Product> iproductdat = new ProductDB();

            Cart grabMe = icartdat.Get(ID);

            List<string> itemList = grabMe.ProductIDs.Split(' ').ToList();
            List<string> quantList = grabMe.ProductQuantity.Split(' ').ToList();
            List<Product> theProdList = new List<Product>();
            ItemDictionary = new Dictionary<int, int>();

            for (int i = 0; i < itemList.Count(); i++)
            {
                int itemId;
                int quant;
                int.TryParse(itemList[i], out itemId);
                int.TryParse(quantList[i], out quant);
                if (itemId != 0)
                {
                    ItemDictionary.Add(itemId, quant);
                }
            }
            foreach (KeyValuePair<int, int> k in ItemDictionary)
            {
                Product addMe = iproductdat.Get(k.Key);
                theProdList.Add(addMe);
            }
            ItemsInCart = theProdList;
        }

        public int getProductQuantity(int id)
        {
            int quant = ItemDictionary.Where(p => p.Key == id).Single().Value;
            return quant;
        }

        public ProductViewModel getProductModel(int productID)
        {
            int quant = getProductQuantity(productID);
            ProductViewModel retMe = new ProductViewModel(productID, quant, this.CartID);

            return retMe;
        }

        public void purchaseProduct(int id)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Cart emptyMe = db.Carts.Where(c => c.Id == id).Single();

            foreach (KeyValuePair<int, int> k in ItemDictionary)
            {
                ProductViewModel buying = getProductModel(k.Key);
                buying.purchaseThis();
            }
            emptyMe.ProductIDs = "";
            emptyMe.ProductQuantity = "";
            db.Entry(emptyMe).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void editProduct(int id, int proID, int qty)
        {
            IDataHandler<Cart> icartdat = new CartDB();
            Cart editMe = icartdat.Get(id);
            if (qty > 0)
            {
                qty -= ItemDictionary[proID];
                icartdat.Update(editMe, proID, qty);
            }
            else
            {
                icartdat.Remove(editMe, proID);
            }
        }
        public override string ToString()
        {

            StringBuilder s = new StringBuilder("CartID:" + CartID);
            foreach (Product p in ItemsInCart)
            {
                s.Append(" ProID:").Append(p.Id).Append("ProPrice:").Append(p.Price).Append("ProGrab:").Append(p.Stock);
            }
            //foreach (KeyValuePair<long, int> k in ItemDictionary)
            //{
            //    s.Append(" ProID:").Append(k.Key).Append(" ProPrice:").Append(k.Value);
            //}
            return s.ToString();
        }

        public string ItemID { get; set; }
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public Dictionary<int, int> ItemDictionary { get; set; }
        public List<Product> ItemsInCart { get; set; }

        public virtual Product Item { get; set; }
    }
}
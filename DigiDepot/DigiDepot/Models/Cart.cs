using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot.Models
{
    public class Cart
    {
        public Cart(long ID, IEnumerable<Product> productList)
        {
            CartID = ID;
            ItemDictionary = new Dictionary<long, int>();
            foreach (Product p in productList)
            {
                ItemDictionary.Add(p.ID, p.GrabbedQuantity);
            }
            ItemsInCart = (List<Product>)productList;
        }

        public override string ToString()
        {

            StringBuilder s = new StringBuilder("CartID:" + CartID);
            foreach (KeyValuePair<long, int> k in ItemDictionary)
            {
                s.Append(" ProductID:").Append(k.Key).Append(" Quantity:").Append(k.Value);
            }
            return s.ToString();
        }

        public string ItemID { get; set; }
        public long CartID { get; set; }
        public int Quantity { get; set; }
        public Dictionary<long, int> ItemDictionary { get; set; }
        public List<Product> ItemsInCart { get; set; }

        public virtual Product Item { get; set; }
    }
}
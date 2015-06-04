using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot
{
    public partial class Cart 
    {
        public Cart()
        {

        }

        public Cart(int ID, IEnumerable<Product> productList)
        {
            CartID = ID;
            ItemDictionary = new Dictionary<long, int>();
            foreach (Product p in productList)
            {
                if (!ItemDictionary.ContainsKey(p.ID))
                {
                    ItemDictionary.Add(p.ID, p.GrabbedQuantity);
                }
                else
                {
                    ItemDictionary[p.ID] += p.GrabbedQuantity;
                }
            }
            ItemsInCart = (List<Product>)productList;
        }

        public override string ToString()
        {

            StringBuilder s = new StringBuilder("CartID:" + CartID);
            foreach (Product p in ItemsInCart)
            {
                s.Append(" ProID:").Append(p.ID).Append("ProPrice:").Append(p.Price).Append("ProGrab:").Append(p.GrabbedQuantity);
            }
            return s.ToString();
        }

        public string ItemID { get; set; }
        public int CartID { get; set; }
        public int Quantity { get; set; }
        public Dictionary<long, int> ItemDictionary { get; set; }
        public List<Product> ItemsInCart { get; set; }

        public virtual Product Item { get; set; }
    }
}
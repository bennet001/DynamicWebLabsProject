using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.Models
{
    public class Product
    {
        public Product(int productID, string name, string priceing)
        {
            ID = productID;
            Name = name;
            Price = priceing;
            AvaliableQuantity = 20;
        }
        public Product(int productID, string name, string priceing, int grab)
        {
            ID = productID;
            Name = name;
            Price = priceing;
            GrabbedQuantity = grab;
            AvaliableQuantity = 20 - grab;
        }
        public string Name { get; set; }
        public string Price { get; set; }
        public int ID { get; private set; }
        public int AvaliableQuantity { get; set; }
        public int GrabbedQuantity { get; set; }

        //public static int ID { get; set; }
    }
}
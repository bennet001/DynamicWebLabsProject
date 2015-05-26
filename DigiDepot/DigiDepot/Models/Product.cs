using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.Models
{
    public class Product
    {
        public Product(long productID,string name, double priceing)
        {
            ID = productID;
            Name = name;
            Price = priceing;
            AvaliableQuantity = 20;
        }
        public Product(long productID, string name, double priceing, int grab)
        {
            ID = productID;
            Name = name;
            Price = priceing;
            GrabbedQuantity = grab;
            AvaliableQuantity = 20 - grab;
        }

        public string Name { get; set; }
        public double Price { get; set; }
        public long ID { get; private set; }
        public int AvaliableQuantity { get; set; }
        public int GrabbedQuantity { get; set; }

        //public static int ID { get; set; }
    }
}
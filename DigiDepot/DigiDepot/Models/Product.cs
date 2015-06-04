//using DigiDepot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot
{
    public partial class Product
    {
        public Product()
        {

        }

        public Product(long productID,string name, decimal priceing)
        {
            ID = productID;
            Name = name;
            Price = priceing;
            AvaliableQuantity = 20;
        }
        public Product(long productID, string name, decimal priceing, int grab)
        {
            ID = productID;
            Name = name;
            Price = priceing;
            GrabbedQuantity = grab;
            AvaliableQuantity = 20 - grab;
        }

        public long ID { get; private set; }
        public int AvaliableQuantity { get; set; }
        public int GrabbedQuantity { get; set; }

        private static readonly int _maxRating = 5;
        private int _rating;
        public int Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                if (value >= 0 && value <= _maxRating)
                    _rating = value;
            }
        }

        //public HashSet<Category> Categories { get; private set; }

        //public void AddCategory(Category c)
        //{
        //    Categories.Add(c);
        //}

        //public static int ID { get; set; }
    }
}
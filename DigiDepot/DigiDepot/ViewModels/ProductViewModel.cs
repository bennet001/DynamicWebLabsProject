using DigiDepot.DataHandlers;
using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(int productID, int quant, int carID)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            ID = productID;
            inCart = carID;
            IDataHandler<Product> iproductdat = new ProductDB();
            Product grabMe = db.Products.Where(p => p.Id == ID).Single();
            //Product grabMe = iproductdat.Get(ID);
            //remove the quantity actively when you place the items in the cart
            //If they are purchased then the subtracted amount is already accounted for
            //if they are removed from the cart via the remove button then the ammount is added back to the database.


            Name = grabMe.Name;
            GrabbedQuantity = quant;
            Price = grabMe.Price * GrabbedQuantity;

            db.SaveChanges();

        }

        public void purchaseThis()
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Product buyMe = db.Products.Where(p => p.Id == this.ID).Single();
            buyMe.Stock -= this.GrabbedQuantity;
            db.Entry(buyMe).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ID { get; private set; }
        public int inCart { get; private set; }
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
    }
}
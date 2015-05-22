using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot.Models
{
    public class Product
    {
        private static int PRODUCTIONID = 0;

        public int productID { get; set; }

        [Required(ErrorMessage = "You must have a name for the thing you are trying to sell")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "You must give a small summary on the object in question. It's condition for example.")]
        public string ProductDescription { get; set; }

        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public double UnitPrice;

        public Product(int id, string p1, string p2, double price)
        {
            // TODO: Complete member initialization
            if (PRODUCTIONID == id)
            {
                PRODUCTIONID++;
            }
            this.productID = id;
            this.ProductName = p1;
            this.ProductDescription = p2;
            this.UnitPrice = price;
        }

        public Product(string p3, string p4, double p5)
        {
            // TODO: Complete member initialization
            this.productID = PRODUCTIONID;
            this.ProductName = p3;
            this.ProductDescription = p4;
            this.UnitPrice = p5;
        }

        public string CurrencyPrice()
        {
            NumberFormatInfo cur = new CultureInfo("en-US", false).NumberFormat;
            return UnitPrice.ToString("C", cur);
        }

        public override string ToString()
        {
            return new StringBuilder("ID:").
                Append(this.productID).
                Append(" ProName:").
                Append(this.ProductName).
                Append(" ProDescr:").
                Append(this.ProductDescription).
                Append(" ProPrice:").
                Append(this.UnitPrice).
                ToString();
        }
    }
}
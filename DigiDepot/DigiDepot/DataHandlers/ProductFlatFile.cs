using DigiDepot.Interfaces;
using DigiDepot.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;


namespace DigiDepot.DataHandlers
{
    public class ProductFlatFile : IDataHandler<Product>
    {

        Dictionary<int, Product> catalog = new Dictionary<int, Product>();
        string path = HttpContext.Current.Server.MapPath("~");

        string filelocal = @"\App_Data\ProductPage.txt";

        public IEnumerable<Product> GetAllItems()
        {

            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProDescr:(?<ProDes>.+) ProPrice:(?<ProPri>.+)";
            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    int id = -1;
                    double price = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out id);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    if (id != -1)
                    {
                        if (catalog.ContainsKey(id))
                        {
                            catalog[id] = new Product(id, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), price);
                        }
                        else
                        {
                            catalog.Add(id, new Product(id, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), price));
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid");
                }
            }
            return catalog.Values;
        }

        public void Update(Product pro)
        {
            if (catalog.Count > 0)
            {
                catalog[pro.productID] = pro;
            }
            else
            {
                string[] lines = File.ReadAllLines(path + filelocal);
                string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProDescr:(?<ProDes>.+) ProPrice:(?<ProPri>.+)";

                StreamWriter file = new StreamWriter(path + filelocal);

                foreach (string product in lines)
                {
                    Match match = Regex.Match(product, sPattern);
                    if (match.Success)
                    {
                        int id = -1;

                        int.TryParse(match.Groups["ProID"].ToString(), out id);
                        if (id.Equals(pro.productID))
                        {
                            file.WriteLine(pro.ToString());
                        }
                        else
                        {
                            file.WriteLine(product);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine(" - invalid " + product);
                    }
                }
                file.Dispose();
            }
        }

        public void Delete(Product pro)
        {
            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProDescr:(?<ProDes>.+) ProPrice:(?<ProPri>.+)";

            StreamWriter file = new StreamWriter(path + filelocal);

            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    int id = -1;

                    int.TryParse(match.Groups["ProID"].ToString(), out id);
                    if (id.Equals(pro.productID))
                    {

                    }
                    else
                    {
                        file.WriteLine(product);
                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid " + product);
                }
            }
            file.Dispose();
        }

        public Product Get(int id)
        {
            Product returnable = null;
            if (catalog.Count > 0 && catalog.ContainsKey(id))
            {
                returnable = catalog[id];
            }
            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProDescr:(?<ProDes>.+) ProPrice:(?<ProPri>.+)";
            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    int Innerid = -1;
                    double price = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out Innerid);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    if (Innerid != -1 && Innerid.Equals(id))
                    {
                        returnable = new Product(Innerid, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), price);
                    }
                    else
                    {

                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid");
                }
            }
            return returnable;

        }

        public Product Get(Product pro)
        {
            Product returnable = null;
            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProDescr:(?<ProDes>.+) ProPrice:(?<ProPri>.+)";
            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    int Innerid = -1;
                    double price = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out Innerid);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    if (Innerid != -1 && Innerid.Equals(pro.productID))
                    {
                        returnable = new Product(Innerid, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), price);
                    }
                    else
                    {

                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid");
                }
            }
            return returnable;
        }

        public void Create(Product pro)
        {
            StreamWriter file = new StreamWriter(path + filelocal, true);
            Product returnable = new Product(pro.ProductName, pro.ProductDescription, pro.UnitPrice);
            file.WriteLine(returnable.ToString());
            file.Dispose();
        }

        public void Save(IEnumerable<Product> pro)
        {
            StreamWriter file = new StreamWriter(path + filelocal);
            foreach (Product p in pro)
            {
                file.WriteLine(p);
            }
            file.Dispose();
        }
    }
}
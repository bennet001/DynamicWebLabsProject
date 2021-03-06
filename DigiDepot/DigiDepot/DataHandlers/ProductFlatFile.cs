﻿using DigiDepot.Interfaces;
using DigiDepot.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
//using DigiDepot.Enums;


namespace DigiDepot.DataHandlers
{
    public class ProductFlatFile : IDataHandler<Product>
    {

        Dictionary<long, Product> catalog = new Dictionary<long, Product>();
        string path = HttpContext.Current.Server.MapPath("~");

        string filelocal = @"\App_Data\ProductPage.txt";
        private static string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProPrice:(?<ProPri>.+) ProGrab:(?<ProGra>.+)";

        public List<Product> GetAllItems()
        {

            string[] lines = File.ReadAllLines(path + filelocal);
            
            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    long id = -1;
                    double price = -1;
                    int grab = -1;
                    long.TryParse(match.Groups["ProID"].ToString(), out id);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    int.TryParse(match.Groups["ProGra"].ToString(), out grab);
                    if (id != -1)
                    {
                        if (catalog.ContainsKey(id))
                        {
                            catalog[id] = new Product(id, match.Groups["ProNa"].ToString(), price, grab);
                        }
                        else
                        {
                            catalog.Add(id, new Product(id, match.Groups["ProNa"].ToString(), price, grab));
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
            return catalog.Values.ToList();
        }

        public void Update(Product pro)
        {
            if (catalog.Count > 0)
            {
                catalog[pro.ID] = pro;
            }
            else
            {
                string[] lines = File.ReadAllLines(path + filelocal);

                StreamWriter file = new StreamWriter(path + filelocal);

                foreach (string product in lines)
                {
                    Match match = Regex.Match(product, sPattern);
                    if (match.Success)
                    {
                        int id = -1;

                        int.TryParse(match.Groups["ProID"].ToString(), out id);
                        if (id.Equals(pro.ID))
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
        public void Update(Product car, int itemID, int itemQuantity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product pro)
        {
            string[] lines = File.ReadAllLines(path + filelocal);
            
            StreamWriter file = new StreamWriter(path + filelocal);

            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    int id = -1;

                    int.TryParse(match.Groups["ProID"].ToString(), out id);
                    if (id.Equals(pro.ID))
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
        public void Remove(Product car, int itemID)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            Product returnable = null;
            if (catalog.Count > 0 && catalog.ContainsKey(id))
            {
                returnable = catalog[id];
            }
            string[] lines = File.ReadAllLines(path + filelocal);
            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    int Innerid = -1;
                    double price = -1;
                    int grab = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out Innerid);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    int.TryParse(match.Groups["ProGra"].ToString(), out grab);
                    if (Innerid != -1 && Innerid.Equals(id))
                    {
                        returnable = new Product(Innerid, match.Groups["ProNa"].ToString(), price, grab);
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
            foreach (string product in lines)
            {
                Match match = Regex.Match(product, sPattern);
                if (match.Success)
                {
                    int Innerid = -1;
                    double price = -1;
                    int grab = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out Innerid);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    int.TryParse(match.Groups["ProGra"].ToString(), out grab);
                    if (Innerid != -1 && Innerid.Equals(pro.ID))
                    {
                        returnable = new Product(Innerid, match.Groups["ProNa"].ToString(), price, grab);
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
            Product returnable = new Product(pro.ID,pro.Name, pro.Price, pro.GrabbedQuantity);
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

        //public IEnumerable<Product> GetProductsByCategory(Category c)
        //{
        //    var toReturn = catalog.Where(k => k.Value.Categories.Contains(c)).Select(k => k.Value);
        //    return toReturn;
        //}
    }
}
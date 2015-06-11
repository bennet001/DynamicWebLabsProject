using DigiDepot.Interfaces;
using DigiDepot.Search.SearchMyPredicates;
using DigiDepot.Search.SearchPredicate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.DataHandlers
{
    public class ProductDB : IDataHandler<Product>
    {
        public List<Product> GetAllItems()
        {
            List<Product> AllProducts = null;
            using (DigiDepotDBContext db = new DigiDepotDBContext())
            {
                IQueryable<Product> productQuery = db.Products;

                AllProducts = productQuery.ToList();
            }
            return AllProducts;
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }

        public void Update(Product pro, int itemID, int quantity)
        {
            using (DigiDepotDBContext db = new DigiDepotDBContext())
            {
                Product productQuery = db.Products.Where(p => p.Id == pro.Id).Single();

                productQuery.Stock = quantity;
                db.Entry(productQuery).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void Delete(Product item)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Product productQuery = db.Products.Where(p => p.Id == item.Id).Single();

            db.Products.Remove(productQuery);
            db.SaveChanges();
        }

        public void Remove(Product cart, int item)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Product productQuery = db.Products.Where(p => p.Id == id).Single();

            return productQuery;
        }

        public Product Get(Product item)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Product productQuery = db.Products.Where(p => p.Id == item.Id).Single();

            return productQuery;
        }

        public void Create(Product item)
        {
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                Product gotten = Get(item);
                if (gotten == null)
                {
                    mdbc.Products.Add(item);
                    mdbc.SaveChanges();
                }
            }
        }

        public void Save(IEnumerable<Product> item)
        {
            throw new NotImplementedException();
        }


        public List<Product> Search(string query)
        {
            List<Product> returnable = null;
            using (DigiDepotDBContext mdbd = new DigiDepotDBContext())
            {
                returnable = mdbd.Products.Where(m => m.Name.Contains(query)||m.Description.Contains(query)||query.Contains(m.Price.ToString())).ToList();
            }
            return returnable;
        }
    }
}
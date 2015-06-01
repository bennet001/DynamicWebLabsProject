using DigiDepot.Interfaces;
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
            DigiDepotDBContext db = new DigiDepotDBContext();
            IQueryable<Product> productQuery = db.Products;

            List<Product> AllProducts = productQuery.ToList();
            return AllProducts;
        }

        public void Update(Product item)
        {
            throw new NotImplementedException();
        }

        public void Update(Product pro, int itemID, int quantity)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Product productQuery = db.Products.Where(p => p.Id == pro.Id).Single();

            productQuery.Stock = quantity;
            db.Entry(productQuery).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
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
    }
}
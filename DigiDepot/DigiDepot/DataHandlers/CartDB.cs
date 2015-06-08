using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DigiDepot.Interfaces;


namespace DigiDepot.DataHandlers
{
    public class CartDB : IDataHandler<Cart>
    {
        public List<Cart> GetAllItems()
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            IQueryable<Cart> cartQuery = db.Carts;

            List<Cart> fullCart = cartQuery.ToList();
            return fullCart;

            //List<Cart> CartDataBase;
            //using (DigiDepotDBContextEntities mdbc = new DigiDepotDBContextEntities())
            //{
            //    CartDataBase = mdbc.Carts.ToList();
            //}
            //return CartDataBase;
        }

        public void Update(Cart item)
        {
            throw new NotImplementedException();
            #region commented
            //Cart toEdit;
            //using (DigiDepotDBContextEntities mdbc = new DigiDepotDBContextEntities())
            //{
            //    toEdit = mdbc.Carts.Where(m => m.CartID == pro.CartID && m.UserID == pro.UserID).Single();
            //    toEdit. = pro.user_name;
            //    toEdit.e_mail_address = pro.e_mail_address;
            //    toEdit.password = pro.password;
            //    mdbc.SaveChanges();
            //}
            #endregion
        }
        public void Update(Cart car, int itemID, int itemQuantity)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Cart cartQuery = db.Carts.Where(c => c.Id == car.Id).Single();
            if (car.ProductIDs != "" && car.ProductQuantity != "")
            {
                List<string> itemList = car.ProductIDs.Split(' ').ToList();
                List<string> quantList = car.ProductQuantity.Split(' ').ToList();
                if (itemList.Contains(itemID.ToString()))
                {
                    int index;
                    int newQuant;
                    int.TryParse(itemList.FindIndex(p => p == itemID.ToString()).ToString(), out index);
                    int.TryParse(quantList[index], out newQuant);
                    newQuant += itemQuantity;
                    quantList[index] = newQuant.ToString();
                }
                else
                {
                    itemList.Add(itemID.ToString() + " ");
                    quantList.Add(itemQuantity.ToString() + " ");
                }
                Dictionary<int, int> prods = new Dictionary<int, int>();
                for (int i = 0; i < itemList.Count(); i++)
                {
                    int id;
                    int quant;
                    int.TryParse(itemList[i], out id);
                    int.TryParse(quantList[i], out quant);
                    if (id != 0 && quant != 0)
                    {
                        prods.Add(id, quant);
                    }
                }
                cartQuery.ProductIDs = "";
                cartQuery.ProductQuantity = "";
                foreach (KeyValuePair<int, int> k in prods)
                {
                    cartQuery.ProductIDs += k.Key + " ";
                    cartQuery.ProductQuantity += k.Value + " ";
                }
                db.Entry(cartQuery).State = System.Data.Entity.EntityState.Modified;

            }
            else
            {
                cartQuery.ProductIDs += itemID.ToString() + " ";
                cartQuery.ProductQuantity += itemQuantity.ToString() + " ";
                db.Entry(cartQuery).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
        }


        public void Delete(Cart item)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            IQueryable<Cart> cartQuery = db.Carts;

            Cart foundItem = (Cart)cartQuery.Where(cart => cart.Id.Equals(item.Id)).First();
            db.Carts.Remove(foundItem);
            db.SaveChanges();
            #region commented
            //using (DigiDepotDBContextEntities mdbc = new DigiDepotDBContextEntities())
            //{
            //    Cart toRemove = mdbc.Users.Where(m => m.user_name == pro.user_name && m.password == pro.password).Single();
            //    mdbc.Users.Remove(toRemove);
            //    mdbc.SaveChanges();
            //}
            #endregion

        }
        public void Remove(Cart car, int itemID)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Cart cartQuery = db.Carts.Where(c => c.Id == car.Id).Single();

            List<string> itemList = car.ProductIDs.Split(' ').ToList();
            List<string> quantList = car.ProductQuantity.Split(' ').ToList();
            Dictionary<int, int> prods = new Dictionary<int, int>();
            for (int i = 0; i < itemList.Count() - 1; i++)
            {
                int id;
                int quant;
                int.TryParse(itemList[i], out id);
                int.TryParse(quantList[i], out quant);
                prods.Add(id, quant);
            }
            cartQuery.ProductIDs = "";
            cartQuery.ProductQuantity = "";
            //Don't have to use this next line if you don't remove stock until purchase
            //db.Products.Where(p => p.Id == itemID).Single().Stock += prods[itemID];
            prods.Remove(itemID);
            foreach (KeyValuePair<int, int> k in prods)
            {
                cartQuery.ProductIDs += k.Key + " ";
                cartQuery.ProductQuantity += k.Value + " ";
            }
            db.Entry(cartQuery).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public Cart Get(int id)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            IQueryable<Cart> cartQuery = db.Carts;
            Cart item = new Cart();

            foreach (Cart c in cartQuery)
            {
                if (c.Id == id)
                {
                    item = c;
                }
            }
            return item;
            #region commented
            //Cart returnable;
            //using (DigiDepotDBContextEntities mdbc = new DigiDepotDBContextEntities())
            //{
            //    returnable = mdbc.Users.Where(m => m.Id == id).Single();
            //}
            //return returnable;
            #endregion

        }

        public Cart Get(Cart item)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            IQueryable<Cart> cartQuery = db.Carts;
            Cart ret = null;

            foreach (Cart c in cartQuery)
            {
                if (c.Id.Equals(item.Id))
                {
                    ret = c;
                }
            }
            return ret;
            #region commented
            //Cart returnable;
            //using (DigiDepotDBContextEntities mdbc = new DigiDepotDBContextEntities())
            //{
            //    returnable = mdbc.Users.Where(m => m.password.Equals(pro.password) && m.user_name.Equals(pro.user_name)).SingleOrDefault();
            //}
            //return returnable;
            #endregion
        }

        public void Create(Cart pro)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            Cart gotten = Get(pro);
            if (gotten == null)
            {
                db.Carts.Add(pro);
                db.SaveChanges();
            }
        }

        public void Save(IEnumerable<Cart> pro)
        {
            throw new NotImplementedException();
        }
    }
}
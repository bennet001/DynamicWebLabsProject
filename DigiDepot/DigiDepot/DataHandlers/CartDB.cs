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

            List<string> itemList = car.ProductIDs.Split(' ').ToList();
            List<string> quantList = car.ProductQuantity.Split(' ').ToList();
            List<string> newitemList = new List<string>();
            List<string> newquantList = new List<string>();
            Dictionary<int, int> prods = new Dictionary<int, int>();
            for (int i = 0; i <= itemList.Count(); i++)
            {
                int id;
                int quant;
                int.TryParse(itemList[i], out id);
                int.TryParse(quantList[i], out quant);
                prods.Add(id, quant);
            }
            foreach (KeyValuePair<int, int> k in prods)
            {
                if (k.Key == itemID)
                {
                    prods[k.Key] = itemQuantity;
                }
                newitemList.Add(k.Key + " ");
                newquantList.Add(k.Value + " ");
            }
            Cart editMe = db.Carts.Where(c => c.CartID == car.CartID).First();
            editMe.ProductIDs = newitemList.ToString();
            editMe.ProductQuantity = newquantList.ToString();

            db.SaveChanges();
        }


        public void Delete(Cart item)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            IQueryable<Cart> cartQuery = db.Carts;

            Cart foundItem = (Cart)cartQuery.Where(cart => cart.CartID.Equals(item.CartID)).First();
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

            List<string> itemList = car.ProductIDs.Split(' ').ToList();
            List<string> quantList = car.ProductQuantity.Split(' ').ToList();
            List<string> newitemList = new List<string>();
            List<string> newquantList = new List<string>();
            Dictionary<int, int> prods = new Dictionary<int, int>();
            for (int i = 0; i <= itemList.Count(); i++)
            {
                int id;
                int quant;
                int.TryParse(itemList[i], out id);
                int.TryParse(quantList[i], out quant);
                prods.Add(id, quant);
            }
            foreach (KeyValuePair<int, int> k in prods)
            {
                if (k.Key == itemID)
                {
                    prods.Remove(k.Key);
                }
                newitemList.Add(k.Key + " ");
                newquantList.Add(k.Value + " ");
            }
            Cart editMe = db.Carts.Where(c => c.CartID == car.CartID).First();
            editMe.ProductIDs = newitemList.ToString();
            editMe.ProductQuantity = newquantList.ToString();

            db.SaveChanges();
        }
        
        public Cart Get(int id)
        {
            DigiDepotDBContext db = new DigiDepotDBContext();
            IQueryable<Cart> cartQuery = db.Carts;
            Cart item = new Cart();

            foreach (Cart c in cartQuery)
            {
                if (c.CartID.Equals(id))
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
            Cart ret = new Cart();

            foreach (Cart c in cartQuery)
            {
                if (c.CartID.Equals(item.CartID))
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
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                Cart goten = Get(pro);
                if (goten == null)
                {
                    mdbc.Carts.Add(pro);
                    mdbc.SaveChanges();
                }
            }
        }

        public void Save(IEnumerable<Cart> pro)
        {
            throw new NotImplementedException();
        }
    }
}
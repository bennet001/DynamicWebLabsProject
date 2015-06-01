using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.DataHandlers
{
    public class UserDB : IDataHandler<User>
    {

        public List<User> GetAllItems()
        {
            List<User> UserDataBase;
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                UserDataBase = mdbc.Users.ToList();
            }
            return UserDataBase;
        }

        public void Update(User pro)
        {
            User toEdit;
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                toEdit = mdbc.Users.Where(m => m.user_name == pro.user_name && m.password == pro.password).Single();
                toEdit.user_name = pro.user_name;
                toEdit.e_mail_address = pro.e_mail_address;
                toEdit.password = pro.password;
                mdbc.SaveChanges();
            }
        }
        public void Update(User car, int itemID, int itemQuantity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User pro)
        {
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                User toRemove = mdbc.Users.Where(m => m.user_name == pro.user_name && m.password == pro.password).Single();
                mdbc.Users.Remove(toRemove);
                mdbc.SaveChanges();
            }
        }
        public void Remove(User pro, int itemID)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            User returnable;
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                returnable = mdbc.Users.Where(m => m.Id == id).Single();
            }
            return returnable;
        }

        public User Get(User pro)
        {
            User returnable;
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                returnable = mdbc.Users.Where(m => m.password.Equals(pro.password) && m.user_name.Equals(pro.user_name)).SingleOrDefault();
            }
            return returnable;
        }

        public void Create(User pro)
        {
            using (DigiDepotDBContext mdbc = new DigiDepotDBContext())
            {
                User goten = Get(pro);
                if (goten == null)
                {
                    mdbc.Users.Add(pro);
                    mdbc.SaveChanges();
                }
            }
        }

        public void Save(IEnumerable<User> pro)
        {
            throw new NotImplementedException();
        }
    }
}
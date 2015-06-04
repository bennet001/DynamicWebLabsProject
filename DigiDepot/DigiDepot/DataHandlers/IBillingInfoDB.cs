using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.DataHandlers
{
    public class IBillingInfoDB : IDataHandler<BillingInfo>
    {
        public List<BillingInfo> GetAllItems()
        {
            List<BillingInfo> accounts;
            using(DigiDepotDBContext DDDB = new DigiDepotDBContext()){
                accounts = DDDB.BillingInfoes.ToList();
            }
            return accounts;
        }

        public void Update(BillingInfo item)
        {
            using (DigiDepotDBContext DDDB = new DigiDepotDBContext())
            {
                BillingInfo account = DDDB.BillingInfoes.Where(m => m.Id == item.Id).SingleOrDefault();
                if (account != null)
                {
                    account.FirstName = item.FirstName;
                    account.LastName = item.LastName;
                    account.AccountCard = item.AccountCard;
                    account.Security = item.Security;
                    account.Address = item.Address;
                    account.City = item.City;
                    account.EmailAddress = item.EmailAddress;
                    account.State = item.State;
                    account.ZipCode = item.ZipCode;
                    if (!(string.IsNullOrEmpty(account.Company)))
                    {
                        account.Company = item.Company;
                    }
                    if (!(string.IsNullOrEmpty(account.Address2)))
                    {
                        account.Address2 = item.Address2;
                    }
                    DDDB.SaveChanges();
                }
            }
        }

        public void Update(BillingInfo cart, int item, int quantity)
        {
            throw new NotImplementedException();
        }

        public void Delete(BillingInfo item)
        {
            using (DigiDepotDBContext DDDB = new DigiDepotDBContext())
            {
                DDDB.BillingInfoes.Remove(item);
                DDDB.SaveChanges();
            }
        }

        public void Remove(BillingInfo cart, int item)
        {
            throw new NotImplementedException();
        }

        public BillingInfo Get(int id)
        {
            BillingInfo returnable;
            using (DigiDepotDBContext DDDB = new DigiDepotDBContext())
            {
                returnable = DDDB.BillingInfoes.Where(m => m.Id == id).SingleOrDefault();
            }
            return returnable;
        }

        public BillingInfo Get(BillingInfo item)
        {
            BillingInfo returnable;
            using (DigiDepotDBContext DDDB = new DigiDepotDBContext())
            {
                returnable = DDDB.BillingInfoes.Where(m => m.Id == item.Id).SingleOrDefault();
            }
            return returnable;
        }

        public void Create(BillingInfo item)
        {
            using (DigiDepotDBContext DDDB = new DigiDepotDBContext())
            {
                DDDB.BillingInfoes.Add(item);
                DDDB.SaveChanges();
            }
        }

        public void Save(IEnumerable<BillingInfo> item)
        {
            throw new NotImplementedException();
        }
    }
}
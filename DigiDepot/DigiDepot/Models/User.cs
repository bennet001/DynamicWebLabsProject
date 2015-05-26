using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot.Models
{
    public class User
    {
        private static long PRODUCTIONUSERID = 0;


        public long UserId { get; set; }
        public String EmailAddress { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Company { get; set; }//Optional
        public String Address { get; set; }
        public String Address2 { get; set; }//apt#, suite, etc. Optional
        public String City { get; set; }
        public String State { get; set; }
        public int ZipCode { get; set; }

        public User()
        {

        }

        public User(string email, string first, string last, string address, string address2, string city, string state, int zipcode)
        {
            UserId = PRODUCTIONUSERID;
            PRODUCTIONUSERID++;
            EmailAddress = email;
            FirstName = first;
            LastName = last;
            Address = address;
            Address2 = address2;
            City = city;
            State = state;
            ZipCode = zipcode;
        }

        public User(long id, string email, string first, string last, string address, string address2, string city, string state, int zipcode)
        {
            if (PRODUCTIONUSERID == id)
            {
                PRODUCTIONUSERID++;
            }
            UserId = id;
            EmailAddress = email;
            FirstName = first;
            LastName = last;
            Address = address;
            Address2 = address2;
            City = city;
            State = state;
            ZipCode = zipcode;
        }

        public override string ToString()
        {
            return new StringBuilder("ID:").
                Append(this.UserId).
                Append(" FirstName:").
                Append(this.FirstName).
                Append(" LastName:").
                Append(this.LastName).
                Append(" UserEAddress:").
                Append(this.EmailAddress).
                Append(" UserAdd:").
                Append(this.Address).
                Append(" User2Add:").
                Append(this.Address2).
                Append(" UserCit:").
                Append(this.City).
                Append(" UserState:").
                Append(this.State).
                Append(" UserZip:").
                Append(ZipCode).
                ToString();
        }
    }
}
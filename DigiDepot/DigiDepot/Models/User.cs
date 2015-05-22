using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.Models
{
    public class User
    {
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
            EmailAddress = email;
            FirstName = first;
            LastName = last;
            Address = address;
            Address2 = address2;
            City = city;
            State = state;
            ZipCode = zipcode;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot.Models
{
    public class Users
    {
        private static int PRODUCTIONUSERID = 0;

        public int UserID { get; set; }

        [Required(ErrorMessage="You must have a username")]
        public string UserName { get; set; }
        public string UserBios { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }

        public Users()
        {

        }

        public Users(string name, string desc, string EAddress, string Password)
        {
            UserID = PRODUCTIONUSERID;
            PRODUCTIONUSERID++;
            UserName = name;
            UserBios = desc;
            EmailAddress = EAddress;
            this.Password = Password;
        }

        public Users(int id, string name, string desc, string EAddress, string Password)
        {
            if(PRODUCTIONUSERID == id){
                PRODUCTIONUSERID++;
            }
            UserID = id;
            UserName = name;
            UserBios = desc;
            EmailAddress = EAddress;
            this.Password = Password;
        }

        public override string ToString()
        {
            return new StringBuilder("ID:").
                Append(this.UserID).
                Append(" UserName:").
                Append(this.UserName).
                Append(" UserDescr:").
                Append(this.UserBios).
                Append(" UserEAddress:").
                Append(this.EmailAddress).
                Append(" UserPassword:").
                Append(this.Password).
                ToString();
        }
    }
}
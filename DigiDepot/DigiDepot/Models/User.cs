using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot
{
    public partial class User
    {
        //Validation code to be added to user
        //public int Id { get; set; }
        //[Required(ErrorMessage = "You must have a user name")]
        //public string user_name { get; set; }

        //[Required(ErrorMessage = "You must have password")]
        //[DataType(DataType.Password, ErrorMessage = "You must have a valid Password")]
        //public string password { get; set; }
        public override string ToString()
        {
            StringBuilder returnable = new StringBuilder();
            returnable.Append(Id).Append(" ").Append(user_name).Append(" ").Append(password).Append(" ").Append(e_mail_address);
            return returnable.ToString();
        }
    }
}
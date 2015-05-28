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
        public override string ToString()
        {
            StringBuilder returnable = new StringBuilder();
            returnable.Append(Id).Append(" ").Append(user_name).Append(" ").Append(password).Append(" ").Append(e_mail_address);
            return returnable.ToString();
        }
    }
}
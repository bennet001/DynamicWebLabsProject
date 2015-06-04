using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DigiDepot
{
    public partial class BillingInfo
    {
        public BillingInfo()
        {
            EmailAddress = "";
            FirstName = "";
            LastName = "";
            EmailAddress = "";
            Address = "";
            City = "";
            State = "";
            ZipCode = -1;
            AccountCard = "";
            Security = -1;
        }
        public override string ToString()
        {
            return new StringBuilder("ID:").
                Append(this.Id).
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
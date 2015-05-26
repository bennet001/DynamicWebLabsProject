using DigiDepot.Interfaces;
using DigiDepot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DigiDepot.DataHandlers
{
    public class UserFlatFile : IDataHandler<User>
    {
        Dictionary<long, User> catalog = new Dictionary<long, User>();
        string path = HttpContext.Current.Server.MapPath("~");

        string filelocal = @"\App_Data\UserPage.txt";
        static string sPattern = @"ID:(?<UsId>\d+) FirstName:(?<UsFiNa>.+) LastName:(?<UsLaNa>.+) UserEAddress:(?<UsEAd>.+) UserAdd:(?<UsAd>\.+) User2Add:(?<Us2Ad>\.+) UserCit:(?<UsCit>\.+) UserState:(?<UsSt>\.+) UserZip:(?<UsZi>\.+)";

        public IEnumerable<User> GetAllItems()
        {

            string[] lines = File.ReadAllLines(path + filelocal);
            
            foreach (string User in lines)
            {
                Match match = Regex.Match(User, sPattern);
                if (match.Success)
                {
                    long id = -1;
                    long.TryParse(match.Groups["UsId"].ToString(), out id);
                    int zip = -1;
                    int.TryParse(match.Groups["UsZi"].ToString(), out zip);
                    if (id != -1)
                    {
                        if (catalog.ContainsKey(id))
                        {
                            catalog[id] = new User(match.Groups["UsEmail"].ToString(), match.Groups["UsFiNa"].ToString(), match.Groups["UsLaNa"].ToString(), match.Groups["UsAd"].ToString(), match.Groups["Us2Ad"].ToString(), match.Groups["UsCit"].ToString(), match.Groups["UsSt"].ToString(), zip);
                        }
                        else
                        {
                            catalog.Add(id, new User(id, match.Groups["UsEmail"].ToString(), match.Groups["UsFiNa"].ToString(), match.Groups["UsLaNa"].ToString(), match.Groups["UsAd"].ToString(), match.Groups["Us2Ad"].ToString(), match.Groups["UsCit"].ToString(), match.Groups["UsSt"].ToString(), zip));
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid");
                }
            }
            return catalog.Values;
        }

        public void Update(User pro)
        {
            if (catalog.Count > 0)
            {
                catalog[pro.UserId] = pro;
            }
            else
            {
                string[] lines = File.ReadAllLines(path + filelocal);

                StreamWriter file = new StreamWriter(path + filelocal);

                foreach (string User in lines)
                {
                    Match match = Regex.Match(User, sPattern);
                    if (match.Success)
                    {
                        int id = -1;

                        int.TryParse(match.Groups["UsId"].ToString(), out id);
                        if (id.Equals(pro.UserId))
                        {
                            file.WriteLine(pro.ToString());
                        }
                        else
                        {
                            file.WriteLine(User);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine(" - invalid " + User);
                    }
                }
                file.Close();
            }
        }

        public void Delete(User pro)
        {
            string[] lines = File.ReadAllLines(path + filelocal);

            StreamWriter file = new StreamWriter(path + filelocal);

            foreach (string User in lines)
            {
                Match match = Regex.Match(User, sPattern);
                if (match.Success)
                {
                    int id = -1;

                    int.TryParse(match.Groups["UsId"].ToString(), out id);
                    if (id.Equals(pro.UserId))
                    {

                    }
                    else
                    {
                        file.WriteLine(User);
                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid " + User);
                }
            }
            file.Close();
        }

        public User Get(int id)
        {
            User returnable = null;
            if (catalog.Count > 0 && catalog.ContainsKey(id))
            {
                returnable = catalog[id];
            }
            string[] lines = File.ReadAllLines(path + filelocal);
            foreach (string User in lines)
            {
                Match match = Regex.Match(User, sPattern);
                if (match.Success)
                {
                    long Innerid = -1;
                    long.TryParse(match.Groups["UsId"].ToString(), out Innerid);
                    int zip = -1;
                    int.TryParse(match.Groups["UsZi"].ToString(), out zip);
                    if (Innerid != -1 && Innerid.Equals(id))
                    {
                        returnable = new User(Innerid,  match.Groups["UsEmail"].ToString(), match.Groups["UsFiNa"].ToString(), match.Groups["UsLaNa"].ToString(), match.Groups["UsAd"].ToString(), match.Groups["Us2Ad"].ToString(), match.Groups["UsCit"].ToString(), match.Groups["UsSt"].ToString(), zip);
                    }
                    else
                    {

                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid");
                }
            }
            return returnable;

        }

        public User Get(User pro)
        {
            User returnable = null;
            string[] lines = File.ReadAllLines(path + filelocal);
            foreach (string User in lines)
            {
                Match match = Regex.Match(User, sPattern);
                if (match.Success)
                {
                    long Innerid = -1;
                    long.TryParse(match.Groups["UsId"].ToString(), out Innerid);
                    int zip = -1;
                    int.TryParse(match.Groups["UsZi"].ToString(), out zip);
                    if (Innerid != -1 && Innerid.Equals(pro.UserId))
                    {
                        returnable = new User(Innerid, match.Groups["UsEmail"].ToString(), match.Groups["UsFiNa"].ToString(), match.Groups["UsLaNa"].ToString(), match.Groups["UsAd"].ToString(), match.Groups["Us2Ad"].ToString(), match.Groups["UsCit"].ToString(), match.Groups["UsSt"].ToString(), zip);
                    }
                    else
                    {

                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid");
                }
            }
            return returnable;
        }

        public void Create(User pro)
        {
            StreamWriter file = new StreamWriter(path + filelocal, true);
            User returnable = new User(email:pro.EmailAddress, first: pro.FirstName, last: pro.LastName,address:pro.Address,address2:pro.Address2, city:pro.City,state:pro.State,zipcode:pro.ZipCode);
            file.WriteLine(returnable.ToString());
            file.Close();
        }

        public void Save(IEnumerable<User> pro)
        {
            StreamWriter file = new StreamWriter(path + filelocal);
            foreach (User p in pro)
            {
                file.WriteLine(p);
            }
            file.Close();
        }
    }
}
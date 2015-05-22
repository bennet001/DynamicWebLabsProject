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
    public class UserFlatFile : IDataHandler<Users>
    {
        Dictionary<int, Users> catalog = new Dictionary<int, Users>();
        string path = HttpContext.Current.Server.MapPath("~");

        string filelocal = @"\App_Data\UserPage.txt";

        public IEnumerable<Users> GetAllItems()
        {

            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) UserName:(?<ProNa>.+) UserDescr:(?<ProDes>.+) UserEAddress:(?<ProPri>.+) UserPassword:(?<Propas>.+)";
            foreach (string Users in lines)
            {
                Match match = Regex.Match(Users, sPattern);
                if (match.Success)
                {
                    int id = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out id);
                    if (id != -1)
                    {
                        if (catalog.ContainsKey(id))
                        {
                            catalog[id] = new Users(id, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), match.Groups["ProPri"].ToString(), match.Groups["Propas"].ToString());
                        }
                        else
                        {
                            catalog.Add(id, new Users(id, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), match.Groups["ProPri"].ToString(), match.Groups["Propas"].ToString()));
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

        public void Update(Users pro)
        {
            if (catalog.Count > 0)
            {
                catalog[pro.UserID] = pro;
            }
            else
            {
                string[] lines = File.ReadAllLines(path + filelocal);
                string sPattern = @"ID:(?<ProID>\d+) UserName:(?<ProNa>.+) UserDescr:(?<ProDes>.+) UserEAddress:(?<ProPri>.+) UserPassword:(?<Propas>.+)";

                StreamWriter file = new StreamWriter(path + filelocal);

                foreach (string Users in lines)
                {
                    Match match = Regex.Match(Users, sPattern);
                    if (match.Success)
                    {
                        int id = -1;

                        int.TryParse(match.Groups["ProID"].ToString(), out id);
                        if (id.Equals(pro.UserID))
                        {
                            file.WriteLine(pro.ToString());
                        }
                        else
                        {
                            file.WriteLine(Users);
                        }
                    }
                    else
                    {
                        System.Console.WriteLine(" - invalid " + Users);
                    }
                }
                file.Close();
            }
        }

        public void Delete(Users pro)
        {
            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) UserName:(?<ProNa>.+) UserDescr:(?<ProDes>.+) UserEAddress:(?<ProPri>.+) UserPassword:(?<Propas>.+)";

            StreamWriter file = new StreamWriter(path + filelocal);

            foreach (string Users in lines)
            {
                Match match = Regex.Match(Users, sPattern);
                if (match.Success)
                {
                    int id = -1;

                    int.TryParse(match.Groups["ProID"].ToString(), out id);
                    if (id.Equals(pro.UserID))
                    {

                    }
                    else
                    {
                        file.WriteLine(Users);
                    }
                }
                else
                {
                    System.Console.WriteLine(" - invalid " + Users);
                }
            }
            file.Close();
        }

        public Users Get(int id)
        {
            Users returnable = null;
            if (catalog.Count > 0 && catalog.ContainsKey(id))
            {
                returnable = catalog[id];
            }
            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProDescr:(?<ProDes>.+) ProPrice:(?<ProPri>.+)";
            foreach (string Users in lines)
            {
                Match match = Regex.Match(Users, sPattern);
                if (match.Success)
                {
                    int Innerid = -1;
                    double price = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out Innerid);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    if (Innerid != -1 && Innerid.Equals(id))
                    {
                        returnable = new Users(Innerid, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), match.Groups["ProPri"].ToString(), match.Groups["Propas"].ToString());
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

        public Users Get(Users pro)
        {
            Users returnable = null;
            string[] lines = File.ReadAllLines(path + filelocal);
            string sPattern = @"ID:(?<ProID>\d+) ProName:(?<ProNa>.+) ProDescr:(?<ProDes>.+) ProPrice:(?<ProPri>.+)";
            foreach (string Users in lines)
            {
                Match match = Regex.Match(Users, sPattern);
                if (match.Success)
                {
                    int Innerid = -1;
                    double price = -1;
                    int.TryParse(match.Groups["ProID"].ToString(), out Innerid);
                    double.TryParse(match.Groups["ProPri"].ToString(), out price);
                    if (Innerid != -1 && Innerid.Equals(pro.UserID))
                    {
                        returnable = new Users(Innerid, match.Groups["ProNa"].ToString(), match.Groups["ProDes"].ToString(), match.Groups["ProPri"].ToString(), match.Groups["Propas"].ToString());
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

        public void Create(Users pro)
        {
            StreamWriter file = new StreamWriter(path + filelocal, true);
            Users returnable = new Users(pro.UserName, pro.UserBios, pro.EmailAddress,pro.Password);
            file.WriteLine(returnable.ToString());
            file.Close();
        }

        public void Save(IEnumerable<Users> pro)
        {
            StreamWriter file = new StreamWriter(path + filelocal);
            foreach (Users p in pro)
            {
                file.WriteLine(p);
            }
            file.Close();
        }
    }
}
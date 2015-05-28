using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace HelloMVC.MyValidations
{
    public class EmailValidation
    {
        public static bool IsValid(string Email)
        {
            bool returnable = false;
            if (!string.IsNullOrEmpty(Email))
            {
                if (Regex.IsMatch(Email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", RegexOptions.IgnoreCase))
                {
                    returnable = true;
                }
            }
            return returnable;
        }
    }
}
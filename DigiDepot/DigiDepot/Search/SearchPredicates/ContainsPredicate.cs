using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.Search.SearchPredicates
{
    public class ContainsPredicate : MyPredicate<string>
    {
        private string input = null;

        public ContainsPredicate(string provided)
        {
            input = provided;
        }

        public bool evaluate(string t)
        {
            return t.ToString().Contains(input.ToString());
        }

        public void setFirstNeed(MyPredicate<string> firstNeed)
        {

        }

        public void setSecondNeed(MyPredicate<string> secondNeed)
        {

        }
    }
}
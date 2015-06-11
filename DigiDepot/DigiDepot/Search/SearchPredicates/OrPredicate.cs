using DigiDepot.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.Search.SearchMyPredicates
{
    public class OrPredicate : MyPredicate<string>
    {
        private MyPredicate<string> firstPossible;
        private MyPredicate<string> secondPossible;

        public OrPredicate()
        {

        }

        public OrPredicate(MyPredicate<string> firstCompare, MyPredicate<string> secondCompare)
        {
            firstPossible = firstCompare;
            secondPossible = secondCompare;
        }
        public bool evaluate(string t)
        {
            return firstPossible.evaluate(t) || secondPossible.evaluate(t);
        }

        public void setFirstNeed(MyPredicate<string> firstNeed)
        {
            firstPossible = firstNeed;
        }
        public void setSecondNeed(MyPredicate<string> secondNeed)
        {
            secondPossible = secondNeed;
        }
    }
}
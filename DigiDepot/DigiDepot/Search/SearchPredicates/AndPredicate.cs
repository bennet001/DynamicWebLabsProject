using System;
using System.Collections.Generic;
using System.Linq;
using DigiDepot.Interfaces;
using System.Web;

namespace DigiDepot.Search.SearchPredicate
{
    public class AndPredicate : MyPredicate<string>
    {
        private MyPredicate<string> firstPossible;
        private MyPredicate<string> secondPossible;
        public AndPredicate()
        {

        }
        public AndPredicate(MyPredicate<string> initial, MyPredicate<string> compare)
        {
            firstPossible = initial;
            secondPossible = compare;
        }

        public bool evaluate(string t)
        {
            return firstPossible.evaluate(t) && secondPossible.evaluate(t);
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
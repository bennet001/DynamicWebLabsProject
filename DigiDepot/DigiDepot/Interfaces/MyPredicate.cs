using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigiDepot.Interfaces
{
    public interface MyPredicate<T>
    {
        bool evaluate(T t);
        void setFirstNeed(MyPredicate<T> firstNeed);
        void setSecondNeed(MyPredicate<T> secondNeed);
    }
}
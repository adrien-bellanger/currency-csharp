using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyGrpcService.Conversion
{
    public interface INumberLocale
    {
        public List<int> GetAllSpecialValues();
        public string GetStringForSpecialValue(int n);
        public string GetBinderAfterSpecialValue(int n);
    }

    public abstract class NumberLocaleBase : INumberLocale
    {
        private readonly Dictionary<int, string> dictionary;

        protected class BinderForInterval
        {
            public BinderForInterval(Interval numberInterval, string sBinder)
            {
                NumberInterval = numberInterval;
                Binder = sBinder;
            }

            public Interval NumberInterval { get; private set; }
            public string Binder { get; private set; }

            public class Interval
            {
                public Interval(int from, int to)
                {
                    From = from;
                    To = to;
                }
                public int From { get; private set; }
                public int To { get; private set; }

                public bool Contains(int n)
                {
                    return (n >= From && n <= To);
                }
            }
        }

        private readonly List<BinderForInterval> specialStrings;

        protected NumberLocaleBase(Dictionary<int, string> _dictionary, List<BinderForInterval> _specialStrings)
        {
            dictionary = _dictionary;
            specialStrings = _specialStrings;
        }

        public List<int> GetAllSpecialValues()
        {
            return dictionary.Keys.OrderByDescending(x => x).ToList();
        }
        public string GetStringForSpecialValue(int n)
        {
            if (dictionary.ContainsKey(n))
                return dictionary[n];
            else
                return "";
        }

        public string GetBinderAfterSpecialValue(int n)
        {
            // If n is exactly a special value, no binder is needed
            if (dictionary.ContainsKey(n))
                return "";

            foreach(var specialStringBetween in specialStrings)
            {
                if (specialStringBetween.NumberInterval.Contains(n))
                    return specialStringBetween.Binder;
            }

            // No binder is needed after the first special value found in n
            return " ";
        }
    }

    public class NumberEnglish : NumberLocaleBase
    {
        public NumberEnglish() : base(new Dictionary<int, string>
            {
                {0, "zero" }, 
                { 1, "one" }, {2 , "two" }, {3 , "three" }, {4 , "four" }, {5 , "five" }, {6 , "six" }, {7 , "seven" }, {8 , "eight" }, {9 , "nine" },
                {10 , "ten" }, {11 , "eleven" }, {12 , "twelve" }, {13 , "thirteen" }, {14 , "fourteen" }, {15 , "fifteen" }, {16 , "sixteen" }, {17 , "seventeen" }, {18 , "eighteen" }, {19 , "nineteen" },
                {20 , "twenty" }, {30 , "thirty" }, {40 , "forty" }, {50 , "fifty" }, {60 , "sixty" }, {70 , "seventy" }, {80 , "eighty" }, {90 , "ninety" },
                {100 , "hundred" }, {1000 , "thousand" }, {1000000 , "million" }
            }, new List<BinderForInterval>{ new BinderForInterval(new BinderForInterval.Interval(20, 99), "-") })
        {
        }
    }

}

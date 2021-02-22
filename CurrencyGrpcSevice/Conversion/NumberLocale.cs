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
        public string GetConnectorAfterSpecialValue(int n);
        public string GetConnectorBetweenUnitAndCent();
    }

    public abstract class NumberLocaleBase : INumberLocale
    {
        /// <summary>
        /// Connector to use between a special value (for example twenty) and the rest of the modulo operation (if not 0)
        /// </summary>
        protected class ConnectorInInterval
        {
            public ConnectorInInterval(Interval numberInterval, string sConnector)
            {
                NumberInterval = numberInterval;
                Connector = sConnector;
            }

            public Interval NumberInterval { get; private set; }
            public string Connector { get; private set; }

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

        protected NumberLocaleBase(Dictionary<int, string> dictionary, List<ConnectorInInterval> connectorInInterval, string connectorBetweenUnitAndCent)
        {
            _dictionary = dictionary;
            _connectorInInterval = connectorInInterval;
            _connectorBetweenUnitAndCent = connectorBetweenUnitAndCent;
        }

        public List<int> GetAllSpecialValues()
        {
            return _dictionary.Keys.OrderByDescending(x => x).ToList();
        }
        public string GetStringForSpecialValue(int n)
        {
            if (_dictionary.ContainsKey(n))
                return _dictionary[n];
            else
                return "";
        }

        public string GetConnectorAfterSpecialValue(int n)
        {
            // If n is exactly a special value, no binder is needed
            if (_dictionary.ContainsKey(n))
                return "";

            foreach(var specialStringBetween in _connectorInInterval)
            {
                if (specialStringBetween.NumberInterval.Contains(n))
                    return specialStringBetween.Connector;
            }

            // No binder is needed after the first special value found in n
            return " ";
        }

        public string GetConnectorBetweenUnitAndCent()
        {
            return _connectorBetweenUnitAndCent;
        }

        private readonly Dictionary<int, string> _dictionary;
        private readonly List<ConnectorInInterval> _connectorInInterval;
        private readonly string _connectorBetweenUnitAndCent;
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
            }, new List<ConnectorInInterval>{ new ConnectorInInterval(new ConnectorInInterval.Interval(20, 99), "-") }, "and")
        {
        }
    }

}

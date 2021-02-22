using System.Collections.Generic;
using System.Linq;

namespace CurrencyGrpcService.Conversion
{
    public interface ILocale
    {
        public List<int> GetAllSpecialValuesOrderedByDescending();
        public string GetStringForSpecialValue(int n);
        public string GetConnectorAfterSpecialValue(int n, int specialValueForN);
        public string GetConnectorBetweenUnitAndCent();
    }

    public abstract class LocaleBase : ILocale
    {
        /// <summary>
        /// Connector to use between a special value (for example twenty) and the rest of the modulo operation (if not 0)
        /// </summary>
        protected class ConnectorAfterSpecialValue
        {
            public ConnectorAfterSpecialValue(Interval numberInterval, string sConnector)
            {
                IntervalToApplyConnector = numberInterval;
                Connector = sConnector;
            }

            public bool ApplyConnectorForGivenNumber(int n, int specialValueForN)
            {
                if (!IntervalToApplyConnector.Contains(n))
                    return false;

                if (n - specialValueForN > 0)
                    return true;
                else
                    return false;
            }

            public string Connector { get; private set; }
            private Interval IntervalToApplyConnector { get; set; }

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

        protected LocaleBase(Dictionary<int, string> dictionary, List<ConnectorAfterSpecialValue> connectorInInterval, string connectorBetweenUnitAndCent)
        {
            _dictionary = dictionary;
            _connectorInInterval = connectorInInterval;
            _connectorBetweenUnitAndCent = connectorBetweenUnitAndCent;
        }

        public List<int> GetAllSpecialValuesOrderedByDescending()
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

        public string GetConnectorAfterSpecialValue(int n, int specialValueForN)
        {
            // If n is exactly a special value, no connector is needed
            if (n == specialValueForN)
                return "";

            foreach(var connectorForGivenInterval in _connectorInInterval)
            {
                if (connectorForGivenInterval.ApplyConnectorForGivenNumber(n, specialValueForN))
                    return connectorForGivenInterval.Connector;
            }

            // No connector is needed after the first special value found in n, but a space
            return " ";
        }

        public string GetConnectorBetweenUnitAndCent()
        {
            return _connectorBetweenUnitAndCent;
        }

        private readonly Dictionary<int, string> _dictionary;
        private readonly List<ConnectorAfterSpecialValue> _connectorInInterval;
        private readonly string _connectorBetweenUnitAndCent;
    }

    public class LocaleEnglish : LocaleBase
    {
        public LocaleEnglish() : base(new Dictionary<int, string>
            {
                {0, "zero" }, 
                { 1, "one" }, {2 , "two" }, {3 , "three" }, {4 , "four" }, {5 , "five" }, {6 , "six" }, {7 , "seven" }, {8 , "eight" }, {9 , "nine" },
                {10 , "ten" }, {11 , "eleven" }, {12 , "twelve" }, {13 , "thirteen" }, {14 , "fourteen" }, {15 , "fifteen" }, {16 , "sixteen" }, {17 , "seventeen" }, {18 , "eighteen" }, {19 , "nineteen" },
                {20 , "twenty" }, {30 , "thirty" }, {40 , "forty" }, {50 , "fifty" }, {60 , "sixty" }, {70 , "seventy" }, {80 , "eighty" }, {90 , "ninety" },
                {100 , "hundred" }, {1000 , "thousand" }, {1000000 , "million" }
            }, 
            new List<ConnectorAfterSpecialValue>{ new ConnectorAfterSpecialValue(new ConnectorAfterSpecialValue.Interval(20, 99), "-") }, 
            "and")
        {
        }
    }

}

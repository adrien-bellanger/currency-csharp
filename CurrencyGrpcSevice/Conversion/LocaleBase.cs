using System.Collections.Generic;
using System.Linq;

namespace CurrencyGrpcService.Conversion
{
    /// <summary>
    /// Base implementations for locales
    /// </summary>
    public abstract class LocaleBase : ILocale
    {
        /// <summary>
        /// Connector to use between a special value (for example twenty) and the rest of the modulo operation (if not 0)
        /// </summary>
        protected class ConnectorAfterSpecialValue
        {
            /// <summary>
            /// In the interval [from; to] is the connector "sConnector" used if the rest of the modulo operation is 0
            /// </summary>
            /// <param name="from">Start of the intervall</param>
            /// <param name="to">End of the intervall</param>
            /// <param name="sConnector">Connector</param>
            public ConnectorAfterSpecialValue(int from, int to, string sConnector)
            {
                IntervalUsingConnector = new Interval(from, to);
                Connector = sConnector;
            }

            /// <summary>
            /// Has the current connector to be used for n?
            /// </summary>
            /// <param name="n">Number to convert into words</param>
            /// <param name="biggestSpecialValueForN">Biggest Number with an own name in n</param>
            /// <returns>Has the current connector to be used for n?</returns>
            public bool IsConnectorUsedForGivenNumber(int n, int biggestSpecialValueForN)
            {
                // If the number is a special value, no convertor has to be used
                if (n == biggestSpecialValueForN)
                    return false;

                // Use connector if n is containing into the interval
                return IntervalUsingConnector.Contains(n);
            }

            // Current connector
            public string Connector { get; private set; }
            // Interval from number using this connector, except those wich are already special values
            private Interval IntervalUsingConnector { get; set; }

            /// <summary>
            /// Class representing a closed interval of integers
            /// </summary>
            public class Interval
            {
                public Interval(int from, int to)
                {
                    From = from;
                    To = to;
                }
                public int From { get; private set; }
                public int To { get; private set; }

                /// <summary>
                /// Does the interval contain n?
                /// </summary>
                /// <param name="n">Number to test</param>
                /// <returns>Does the interval contain n?</returns>
                public bool Contains(int n)
                {
                    return (n >= From && n <= To);
                }
            }
        }

        /// <summary>
        /// Protected constructor for locales
        /// </summary>
        /// <param name="dictionary">Dictionary with all special values (integer for which the word is not composed) between 0 and 999 999 999</param>
        /// <param name="connectorInInterval">List of all connectors to use after a special value, and the interval in which it has to be used</param>
        /// <param name="connectorBetweenUnitAndCent">Connector to use between unit and cent</param>
        protected LocaleBase(Dictionary<int, string> dictionary, List<ConnectorAfterSpecialValue> connectorInInterval, string connectorBetweenUnitAndCent)
        {
            _dictionary = dictionary;
            _connectorInInterval = connectorInInterval;
            _connectorBetweenUnitAndCent = connectorBetweenUnitAndCent;
        }

        public List<int> GetAllSpecialValuesInDescendingOrder()
        {
            return _dictionary.Keys.OrderByDescending(x => x).ToList();
        }
        
        public string GetWordForSpecialValue(int n)
        {
            if (_dictionary.ContainsKey(n))
                return _dictionary[n];
            else
                return "";
        }

        public string GetConnectorAfterSpecialValue(int n, int specialValueForN)
        {
            // If n is exactly a special value, no connector is needed
            if (n % specialValueForN == 0)
                return "";

            // Find the connector to use for the given number
            foreach(var connectorForGivenInterval in _connectorInInterval)
            {
                if (connectorForGivenInterval.IsConnectorUsedForGivenNumber(n, specialValueForN))
                    return connectorForGivenInterval.Connector;
            }

            // No connector is needed after the first special value found in n, but a space
            return " ";
        }

        public string GetConnectorBetweenUnitAndCent()
        {
            return _connectorBetweenUnitAndCent;
        }

        /// <summary>
        /// Dictionary with all special values (integer for which the word is not composed) between 0 and 999 999 999
        /// </summary>
        private readonly Dictionary<int, string> _dictionary;
        /// <summary>
        /// List of all connectors to use after a special value, and the interval in which it has to be used
        /// </summary>
        private readonly List<ConnectorAfterSpecialValue> _connectorInInterval;
        /// <summary>
        /// Connector to use between unit and cent
        /// </summary>
        private readonly string _connectorBetweenUnitAndCent;
    }
}

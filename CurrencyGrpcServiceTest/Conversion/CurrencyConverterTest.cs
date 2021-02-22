using NUnit.Framework;
using CurrencyGrpcService.Conversion;

namespace CurrencyGrpcServiceTest.Conversion
{
    public class CurrencyConverterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertToStringDollarAndEnglish()
        {
            NumberConverter numberConverter = new NumberConverter();
            CurrencyConverter currencyConverter = new CurrencyConverter(numberConverter);
            ICurrency dollar = new Dollar();
            LocaleEnglish localeEnglish = new LocaleEnglish();

            {
                double d = 0;
                string sExpected = "zero dollars";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            {
                double d = 1;
                string sExpected = "one dollar";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            {
                double d = 25.1;
                string sExpected = "twenty-five dollars and ten cents";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            {
                double d = 0.01;
                string sExpected = "zero dollars and one cent";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            {
                double d = 45100;
                string sExpected = "forty-five thousand one hundred dollars";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            {
                double d = 999999999.99;
                string sExpected = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            {
                double d = 1000000000;
                Assert.AreEqual(numberConverter.GetOutOfRangeMessage(), currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            { // Test with a negative value
                double d = -0.01;
                Assert.AreEqual(numberConverter.GetOutOfRangeMessage(), currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            { // Test with a negative value which will be rounded to 0
                double d = -0.001;
                string sExpected = "zero dollars";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            { // Test with a value greater than maximum but which will be rounded to the maximum value
                double d = 999999999.994999;
                string sExpected = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents";
                Assert.AreEqual(sExpected, currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            { // Test with a value greater than maximum but which will be rounded to a value greater than the maximum value
                double d = 999999999.9950005;
                Assert.AreEqual(numberConverter.GetOutOfRangeMessage(), currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            { // Test with a value greater than int MaxValue
                double d = (double)int.MaxValue + 1;
                Assert.AreEqual(numberConverter.GetOutOfRangeMessage(), currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
            { // Test with a value smaller than int MinValue
                double d = (double)int.MinValue - 1;
                Assert.AreEqual(numberConverter.GetOutOfRangeMessage(), currencyConverter.ConvertToWords(d, dollar, localeEnglish));
            }
        }
    }
}
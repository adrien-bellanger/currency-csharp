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
            NumberEnglish localeEnglish = new NumberEnglish();

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
        }
    }
}
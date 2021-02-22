using NUnit.Framework;
using CurrencyGrpcService.Conversion;
using System;

namespace CurrencyGrpcServiceTest.Conversion
{
    public class NumberConverterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertToStringEnglish()
        {
            NumberConverter converter = new NumberConverter();
            LocaleEnglish localeEnglish = new LocaleEnglish();

            {
                int n = 0;
                string sExpected = "zero";
                Assert.AreEqual(sExpected, converter.ConvertToWords(n, localeEnglish));
            }
            {
                int n = 1;
                string sExpected = "one";
                Assert.AreEqual(sExpected, converter.ConvertToWords(n, localeEnglish));
            }
            {
                int n = 25;
                string sExpected = "twenty-five";
                Assert.AreEqual(sExpected, converter.ConvertToWords(n, localeEnglish));
            }
            {
                int n = 45100;
                string sExpected = "forty-five thousand one hundred";
                Assert.AreEqual(sExpected, converter.ConvertToWords(n, localeEnglish));
            }
            {
                int n = 999999999;
                string sExpected = "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine";
                Assert.AreEqual(sExpected, converter.ConvertToWords(n, localeEnglish));
            }
            {
                int n = 1000000000;
                var ex = Assert.Throws<ArgumentException>(() => converter.ConvertToWords(n, localeEnglish));
                Assert.AreEqual(converter.GetOutOfRangeMessage(), ex.Message);
            }
            {
                int n = -1;
                var ex = Assert.Throws<ArgumentException>(() => converter.ConvertToWords(n, localeEnglish));
                Assert.AreEqual(converter.GetOutOfRangeMessage(), ex.Message);
            }
        }
    }
}
﻿using System.Globalization;
using Expressive.Tokenisation;
using NUnit.Framework;

namespace Expressive.Tests.Tokenisation
{
    [TestFixture]
    public class NumericTokenExtractorTests
    {
        [Test]
        public void TestWithDecimalPlace()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("123.45", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("123.45", token.CurrentToken);
        }

        [Test]
        public void TestWithDecimalPlaceLeading()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken(".45", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual(".45", token.CurrentToken);
        }

        [Test]
        public void TestWithDecimalPlaceAndNegativeSign()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("-123.45", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("-123.45", token.CurrentToken);
        }

        [Test]
        public void TestWithDecimalPlaceAndPositiveSign()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("+123.45", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("+123.45", token.CurrentToken);
        }

        [Test]
        public void TestWithInteger()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("123", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("123", token.CurrentToken);
        }

        [Test]
        public void TestWithIntegerAndNegativeSign()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("-123", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("-123", token.CurrentToken);
        }

        [Test]
        public void TestWithIntegerAndPositiveSign()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("+123", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("+123", token.CurrentToken);
        }

        [Test]
        public void TestWithInvalidNumber()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("abc", 0, new Context(ExpressiveOptions.None));

            Assert.IsNull(token);
        }

        [Test]
        public void TestWithMultipleDecimalPlaces()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("123.45.678", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("123.45", token.CurrentToken);
        }

        [Test]
        public void TestWithScientificNotationAndNoOrderOfMagnitude()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("1.23e", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("1.23", token.CurrentToken);
        }

        [Test]
        public void TestWithScientificNotation()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("1.23e2", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("1.23e2", token.CurrentToken);
        }

        [Test]
        public void TestWithScientificNotationAndNegativeSign()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("1.23e-2", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("1.23e-2", token.CurrentToken);
        }

        [Test]
        public void TestWithScientificNotationAndPositiveSign()
        {
            var extractor = new NumericTokenExtractor();

            var token = extractor.ExtractToken("1.23e+2", 0, new Context(ExpressiveOptions.None));

            Assert.IsNotNull(token);
            Assert.AreEqual("1.23e+2", token.CurrentToken);
        }
    }
}
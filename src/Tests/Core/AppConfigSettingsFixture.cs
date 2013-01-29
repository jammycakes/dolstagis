using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Dolstagis.Tests.Core
{
    [TestFixture]
    public partial class AppConfigSettingsFixture
    {
        private GoodTestData testData;

        [TestFixtureSetUp]
        public void CreateTestData()
        {
            this.testData = new GoodTestData();
        }

        [Test]
        public void CanParseIntValue()
        {
            Assert.AreEqual(1, testData.IntValue);
        }

        [Test]
        public void CanParseStringValue()
        {
            Assert.AreEqual("Dolstagis", testData.StringValue);
        }

        [Test]
        public void CanParseBoolValue()
        {
            Assert.IsTrue(testData.BoolValue);
        }

        [Test]
        public void CanParseDateTimeValue()
        {
            Assert.AreEqual(new DateTime(2013, 1, 29, 7, 34, 0), testData.DateTimeValue);
        }

        [Test]
        public void CanParseEnumValue()
        {
            Assert.AreEqual(DateTimeKind.Local, testData.EnumValue);
        }
    }
}

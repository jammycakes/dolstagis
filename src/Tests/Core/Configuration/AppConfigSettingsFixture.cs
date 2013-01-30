using NUnit.Framework;
using System;
using System.Configuration;

namespace Dolstagis.Tests.Core.Configuration
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

        [Test]
        public void CanParseTimeSpan()
        {
            Assert.AreEqual(new TimeSpan(2, 10, 30, 50), testData.TimeSpanValue);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void BadDateThrowsCorrectException()
        {
            var badDate = new BadDate();
            Assert.IsNull(badDate);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void BadIntThrowsCorrectException()
        {
            var badInt = new BadInt();
            Assert.IsNull(badInt);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void BadEnumThrowsCorrectException()
        {
            var badEnum = new BadEnum();
            Assert.IsNull(badEnum);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void BadBoolThrowsCorrectException()
        {
            var badBool = new BadBool();
            Assert.IsNull(badBool);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void BadTimeSpanThrowsCorrectException()
        {
            var badTimeSpan = new BadTimeSpan();
            Assert.IsNull(badTimeSpan);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void OutOfRangeTimeSpanThrowsCorrectException()
        {
            var badTimeSpan = new OutOfRangeTimeSpan();
            Assert.IsNull(badTimeSpan);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationErrorsException))]
        public void MissingRequiredValueThrowsException()
        {
            var missingRequired = new RequiredField();
            Assert.IsNull(missingRequired);
        }

        [Test]
        public void MissingOptionalValueDoesNotThrowException()
        {
            var missingOptional = new OptionalField();
            Assert.IsNull(missingOptional.OptionalString);
            Assert.AreEqual(42, missingOptional.OptionalIntWithDefault);
            Assert.AreEqual(default(int), missingOptional.OptionalIntWithNoDefault);
        }
    }
}

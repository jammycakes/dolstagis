using System;
using Dolstagis.Framework.Configuration;

namespace Dolstagis.Tests.Core.Configuration
{
    public partial class AppConfigSettingsFixture
    {
        private class GoodTestData : Settings
        {
            public int IntValue { get; private set; }

            public string StringValue { get; private set; }

            public bool BoolValue { get; private set; }

            public DateTime DateTimeValue { get; private set; }

            public DateTimeKind EnumValue { get; private set; }

            public TimeSpan TimeSpanValue { get; private set; }
        }

        private class BadDate : Settings
        {
            public DateTime BadDateValue { get; private set; }
        }

        private class BadInt : Settings
        {
            public int BadIntValue { get; private set; }
        }

        private class BadEnum : Settings
        {
            public DateTimeKind BadEnumValue { get; private set; }
        }

        private class BadBool : Settings
        {
            public bool BadBoolValue { get; private set; }
        }

        private class BadTimeSpan : Settings
        {
            public TimeSpan BadTimeSpanValue { get; private set; }
        }

        private class OutOfRangeTimeSpan : Settings
        {
            public TimeSpan OutOfRangeTimeSpanValue { get; private set; }
        }

        private class RequiredField : Settings
        {
            [Required]
            public string RequiredValue { get; private set; }
        }

        private class OptionalField : Settings
        {
            public OptionalField()
            {
                this.OptionalIntWithDefault = 42;
            }

            public string OptionalString { get; private set; }
            public int OptionalIntWithDefault { get; private set; }
            public int OptionalIntWithNoDefault { get; private set; }
        }
    }
}

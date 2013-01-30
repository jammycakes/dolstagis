using Dolstagis.Core.Configuration;
using System;

namespace Dolstagis.Tests.Core.Configuration
{
    public partial class AppConfigSettingsFixture
    {
        private class GoodTestData : AppConfigSettingsBase
        {
            public int IntValue { get; private set; }

            public string StringValue { get; private set; }

            public bool BoolValue { get; private set; }

            public DateTime DateTimeValue { get; private set; }

            public DateTimeKind EnumValue { get; private set; }

            public TimeSpan TimeSpanValue { get; private set; }
        }

        private class BadDate : AppConfigSettingsBase
        {
            public DateTime BadDateValue { get; private set; }
        }

        private class BadInt : AppConfigSettingsBase
        {
            public int BadIntValue { get; private set; }
        }

        private class BadEnum : AppConfigSettingsBase
        {
            public DateTimeKind BadEnumValue { get; private set; }
        }

        private class BadBool : AppConfigSettingsBase
        {
            public bool BadBoolValue { get; private set; }
        }

        private class BadTimeSpan : AppConfigSettingsBase
        {
            public TimeSpan BadTimeSpanValue { get; private set; }
        }

        private class OutOfRangeTimeSpan : AppConfigSettingsBase
        {
            public TimeSpan OutOfRangeTimeSpanValue { get; private set; }
        }

        private class RequiredField : AppConfigSettingsBase
        {
            [Required]
            public string RequiredValue { get; private set; }
        }

        private class OptionalField : AppConfigSettingsBase
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

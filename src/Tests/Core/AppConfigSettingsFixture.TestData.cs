using Dolstagis.Core;
using System;

namespace Dolstagis.Tests.Core
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
    }
}

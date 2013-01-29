using Dolstagis.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Tests.Core
{
    public class AppSettingsTestData : AppConfigSettingsBase
    {
        public int IntValue { get; private set; }

        public string StringValue { get; private set; }

        public bool BoolValue { get; private set; }

        public DateTime DateTimeValue { get; private set; }

        public DateTimeKind EnumValue { get; private set; }
    }
}

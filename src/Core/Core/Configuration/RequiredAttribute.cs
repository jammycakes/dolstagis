﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.Configuration
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
    }
}
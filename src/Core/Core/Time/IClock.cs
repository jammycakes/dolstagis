using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dolstagis.Core.Time
{
    /// <summary>
    ///  Provides a mockable abstraction layer around DateTime.Now and DateTime.UtcNow
    /// </summary>

    public interface IClock
    {
        /// <summary>
        ///  Gets the current local time.
        /// </summary>
        /// <returns>The time.</returns>

        DateTime Now();

        /// <summary>
        ///  Gets the current UTC time.
        /// </summary>
        /// <returns>The time.</returns>

        DateTime UtcNow();
    }
}

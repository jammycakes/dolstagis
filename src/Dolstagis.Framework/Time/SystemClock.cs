using System;

namespace Dolstagis.Framework.Time
{
    public class SystemClock : IClock
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}

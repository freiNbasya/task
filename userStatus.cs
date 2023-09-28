using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task
{
    public class UserStatus
    {
        public static string GetStatus(DateTime lastSeenDate, List<string> phrases)
        {
            TimeSpan timeDif = DateTime.Now - lastSeenDate;
            int daysDifference = timeDif.Days;
            int hoursDifference = timeDif.Hours;
            int minutesDifference = timeDif.Minutes;
            int secondsDifference = timeDif.Seconds;
            if (daysDifference > 7) return phrases[2];
            if (daysDifference > 1) return phrases[3];
            if (lastSeenDate.Day != DateTime.Now.Day) return phrases[4];
            if (hoursDifference > 2) return phrases[5];
            if (hoursDifference >= 1) return phrases[6];
            if (minutesDifference > 1) return phrases[7];
            if (secondsDifference > 30) return phrases[8];
            return phrases[9];

        }
    }
}

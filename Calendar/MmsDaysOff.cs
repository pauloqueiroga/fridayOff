using System;
using System.Collections.Generic;

namespace Queiroga.FridayOff.Calendar
{
    public class MmsDaysOff : DateProvider
    {
        private const string FridayOffDescription = "Friday Off";
        private static readonly Dictionary<DateTime, string> _mmsDaysOff = new Dictionary<DateTime, string>
        {
            { new DateTime(2018, 1, 1), "New Year's Day" },
            { new DateTime(2018, 3, 30), "Good Friday" },
            { new DateTime(2018, 5, 28), "Memorial Day" },
            { new DateTime(2018, 7, 4), "Independence Day" },
            { new DateTime(2018, 9, 3), "Labor Day" },
            { new DateTime(2018, 11, 22), "Thanksgiving Thursday" },
            { new DateTime(2018, 12, 24), "Christmas' Eve" },
            { new DateTime(2018, 12, 25), "Chirstmas Day" },
            { new DateTime(2018, 12, 26), "Forced Paid Time Off" },
            { new DateTime(2018, 12, 27), "Forced Paid Time Off" },
            { new DateTime(2018, 12, 31), "Forced Paid Time Off" },
        };

        public MmsDaysOff() : base(_mmsDaysOff, 5000)
        {
            var fridayOff = new DateTime(2018, 1, 12);
            var finalDay = new DateTime(2018, 12, 31);

            while (fridayOff < finalDay)
            {
                CreateDayOff(fridayOff, FridayOffDescription);
                fridayOff = fridayOff.AddDays(14);
            }
        }
    }
}

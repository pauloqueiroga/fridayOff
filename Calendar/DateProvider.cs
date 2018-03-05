using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Queiroga.FridayOff.Calendar
{
    public class DateProvider
    {
        private readonly IDictionary<DateTime, string> _daysOff;

        #region Constructors
        public DateProvider() : this(new Dictionary<DateTime, string>())
        {
            
        }

        public DateProvider(IDictionary<DateTime, string> daysOff)
        {
            _daysOff = daysOff;
        }
        #endregion

        public DateTime GetNextDayOff()
        {
            return GetNextDayOff(DateTime.Today);
        }

        public DateTime GetNextDayOff(DateTime referenceDate)
        {
            while (true)
            {
                if (referenceDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    return referenceDate;
                }

                if (referenceDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    return referenceDate;
                }

                if (_daysOff.ContainsKey(referenceDate))
                {
                    return referenceDate;
                }

                referenceDate = referenceDate.AddDays(1);
            }
        }

        public int GetDaysUntilNextDayOff()
        {
            return GetDaysUntilNextDayOff(DateTime.Today);
        }

        public int GetDaysUntilNextDayOff(DateTime referenceDate)
        {
            throw new NotImplementedException("Needs to be done, it's the main thing for this program...");
        }

        /// <summary>
        /// Creates a new day off in the calendar
        /// </summary>
        /// <param name="dayOff">Date to be set as day off</param>
        /// <param name="description">Description of the day off to be added</param>
        /// <returns>True if it's a new day off, false if it was already a day off</returns>
        public bool CreateDayOff(DateTime dayOff, string description)
        {
            if (_daysOff.ContainsKey(dayOff))
            {
                _daysOff[dayOff] = description;
                return false;
            }

            _daysOff[dayOff] = description;
            return true;
        }
    }
}

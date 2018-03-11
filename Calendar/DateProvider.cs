using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Queiroga.FridayOff.Calendar
{
    public class DateProvider
    {
        private readonly IDictionary<DateTime, string> _daysOff;
        private readonly int _operationTimeoutMilliseconds;

        #region Constructors
        public DateProvider() : this(new Dictionary<DateTime, string>(), 5000)
        {
            
        }

        public DateProvider(IDictionary<DateTime, string> daysOff, int operationTimeoutMilliseconds)
        {
            _daysOff = daysOff;
            _operationTimeoutMilliseconds = operationTimeoutMilliseconds;
        }
        #endregion

        public DateTime GetNextDayOff()
        {
            return GetNextDayOff(DateTime.Today);
        }

        public DateTime GetNextDayOff(DateTime referenceDate)
        {
            return GetNextDaysOff(referenceDate, 1).SingleOrDefault();
        }

        public IEnumerable<DateTime> GetNextDaysOff(DateTime referenceDate, int numberOfDays)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            if (numberOfDays < 1)
            {
                yield break;
            }

            var count = 0;

            while (count < numberOfDays 
                && stopWatch.ElapsedMilliseconds < _operationTimeoutMilliseconds)
            {
                if (referenceDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    count++;
                    yield return referenceDate;
                }

                if (referenceDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    count++;
                    yield return referenceDate;
                }

                if (_daysOff.ContainsKey(referenceDate))
                {
                    count++;
                    yield return referenceDate;
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
            var nextDate = GetNextDayOff();
            return (nextDate - referenceDate).Days;
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

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queiroga.FridayOff.Calendar;

namespace CalendarTest
{
    [TestClass]
    public class DateProviderTests
    {
        [TestMethod]
        public void GetNextDayOffOnSundayShouldReturnSameDay()
        {
            var knownSunday = new DateTime(2018, 3, 4); // Sunday
            var target = new DateProvider();
            var result = target.GetNextDayOff(knownSunday);
            Assert.AreEqual(knownSunday, result);
            Assert.AreEqual(DayOfWeek.Sunday, result.DayOfWeek);
        }

        [TestMethod]
        public void CreateNewDayOffShouldReturnTrue()
        {
            var testDayOff = new DateTime(2018, 3, 9);
            var target = new DateProvider();
            var result = target.CreateDayOff(testDayOff, "Black Friday");
            Assert.IsTrue(result);

            var nextDayOff = target.GetNextDayOff(testDayOff);
            Assert.AreEqual(testDayOff, nextDayOff);

            nextDayOff = target.GetNextDayOff(testDayOff.AddDays(-3));
            Assert.AreEqual(testDayOff, nextDayOff);

            result = target.CreateDayOff(testDayOff, "Black Friday");
            Assert.IsFalse(result);
        }
    }
}

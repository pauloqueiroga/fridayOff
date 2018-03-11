using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GetNextDayOffOnSaturdayShouldReturnSameDay()
        {
            var knownSaturday = new DateTime(2018, 3, 3); // Sunday
            var target = new DateProvider();
            var result = target.GetNextDayOff(knownSaturday);
            Assert.AreEqual(knownSaturday, result);
            Assert.AreEqual(DayOfWeek.Saturday, result.DayOfWeek);
        }

        [TestMethod]
        public void CreateNewDayOffShouldReturnTrueForNewDayOff()
        {
            var testDayOff = new DateTime(2018, 3, 9);
            var target = new DateProvider();
            var result = target.CreateDayOff(testDayOff, "Black Friday");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CreateNewDayOffAndRetrieveIt()
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

        [TestMethod]
        public void CreateSameDayOffTwiceShouldReturnTrueAndFalse()
        {
            var testDayOff = new DateTime(2018, 3, 23);
            var target = new DateProvider();
            var result = target.CreateDayOff(testDayOff, "Black Friday");
            Assert.IsTrue(result);

            result = target.CreateDayOff(testDayOff, "Black Friday");
            Assert.IsFalse(result);

            var nextDayOff = target.GetNextDayOff(testDayOff);
            Assert.AreEqual(testDayOff, nextDayOff);

            nextDayOff = target.GetNextDayOff(testDayOff.AddDays(-3));
            Assert.AreEqual(testDayOff, nextDayOff);
        }

        [TestMethod]
        public void GetMultipleDaysOffShouldReturnTheCorrectAmount()
        {
            const int numberOfDays = 10;
            var target = new DateProvider();

            var nextDaysOff = target.GetNextDaysOff(DateTime.Today, numberOfDays);
            Assert.IsNotNull(nextDaysOff);
            var flattenedDaysOff = nextDaysOff.ToArray();
            CollectionAssert.AllItemsAreNotNull(flattenedDaysOff);
            CollectionAssert.AllItemsAreUnique(flattenedDaysOff);
            Assert.AreEqual(numberOfDays, flattenedDaysOff.Length);
        }

        [TestMethod]
        public void GetMultipleDaysOffShouldContainAddedDaysOff()
        {
            const int numberOfDays = 20;
            var target = new DateProvider();
            
            var testDaysOff = new[] {
                new DateTime(2018, 3, 16),
                new DateTime(2018, 3, 23),
                new DateTime(2018, 3, 30),
            };

            foreach (var testDayOff in testDaysOff)
            {
                var result = target.CreateDayOff(testDayOff, "Black Friday");
                Assert.IsTrue(result);
            }

            var nextDaysOff = target.GetNextDaysOff(testDaysOff[0], numberOfDays);
            Assert.IsNotNull(nextDaysOff);
            var flattenedDaysOff = nextDaysOff.ToArray();
            CollectionAssert.AllItemsAreNotNull(flattenedDaysOff);
            CollectionAssert.AllItemsAreUnique(flattenedDaysOff);
            Assert.AreEqual(numberOfDays, flattenedDaysOff.Length);
            CollectionAssert.IsSubsetOf(testDaysOff, flattenedDaysOff);
        }

        [TestMethod]
        public void GetDaysOffShouldReturnEmptyWhenZeroOrNegative()
        {
            var target = new DateProvider();

            var nextDaysOff = target.GetNextDaysOff(DateTime.Today, 0);
            Assert.IsNotNull(nextDaysOff);
            Assert.IsFalse(nextDaysOff.Any());

            nextDaysOff = target.GetNextDaysOff(DateTime.Today, -10);
            Assert.IsNotNull(nextDaysOff);
            Assert.IsFalse(nextDaysOff.Any());
        }

        [TestMethod]
        public void GetTooManyDaysOffShouldTimeout()
        {
            const int numberOfDays = 200000;
            var target = new DateProvider(new SlowDictionary(), 50);

            var nextDaysOff = target.GetNextDaysOff(DateTime.Today, numberOfDays);
            Assert.IsNotNull(nextDaysOff);
            var flattenedDaysOff = nextDaysOff.ToArray();
            CollectionAssert.AllItemsAreNotNull(flattenedDaysOff);
            CollectionAssert.AllItemsAreUnique(flattenedDaysOff);
            Assert.IsTrue(numberOfDays > flattenedDaysOff.Length);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Queiroga.FridayOff.Calendar.Test
{
    [TestClass]
    public class MmsDaysOffTests
    {
        [TestMethod]
        public void Get2018DaysOffShouldReturn()
        {
            const int expectedDaysOff = 143;
            var january1 = new DateTime(2018, 1, 1);
            var target = new MmsDaysOff();

            var daysOff2018 = target.GetNextDaysOff(january1, expectedDaysOff);

            Assert.IsNotNull(daysOff2018);
            var flattenedDaysOff = daysOff2018.ToArray();
            CollectionAssert.AllItemsAreNotNull(flattenedDaysOff);
            CollectionAssert.AllItemsAreUnique(flattenedDaysOff);
            Assert.AreEqual(expectedDaysOff, flattenedDaysOff.Length);
            CollectionAssert.Contains(flattenedDaysOff, new DateTime(2018, 12, 31));
        }
    }
}

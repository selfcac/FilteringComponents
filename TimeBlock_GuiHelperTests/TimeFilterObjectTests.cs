using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeBlockFilter;

namespace TimeBlock_GuiHelper.Tests
{
    [TestClass()]
    public class TimeFilterObjectTests
    {
        static DateTime dateString(string date)
        {
            return DateTime.ParseExact(
                date,
                "d/M/yyyy h:mm:ss tt",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        [TestMethod()]
        public void isBlockedTest()
        {
            TimeFilterObject filter = new TimeFilterObject();
            filter.clearAllTo(true);

            filter.setPolicy(DayOfWeek.Monday, 6, false);

            // 1 April 2019 = Monday

            Assert.IsTrue(filter.isBlocked(new DateTime(2019, 4, 1, 6, 15, 00)));
            Assert.IsFalse(filter.isBlocked(new DateTime(2019, 4, 2, 6, 15, 00)));

            Assert.IsFalse(filter.isBlocked(new DateTime(2019, 4, 1, 5, 59, 59)));
            Assert.IsFalse(filter.isBlocked(new DateTime(2019, 4, 1, 7, 0, 00)));

            filter.setPolicy(DayOfWeek.Tuesday, 0, false);

            Assert.IsTrue(filter.isBlocked(dateString("2/4/2019 12:20:00 AM")));
            Assert.IsFalse(filter.isBlocked(dateString("2/4/2019 1:00:00 AM")));
            Assert.IsFalse(filter.isBlocked(dateString("2/4/2019 12:20:00 PM")));
            Assert.IsFalse(filter.isBlocked(dateString("1/4/2019 11:59:59 PM")));
        }
    }
}
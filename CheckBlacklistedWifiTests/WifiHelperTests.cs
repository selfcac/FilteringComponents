using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckBlacklistedWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBlacklistedWifi.Tests
{
    [TestClass()]
    public class WifiHelperTests
    {
        public void TestWifiBlocked(bool inBlockZone, List<string> current, List<string> rules)
        {
            Assert.AreEqual(inBlockZone, WifiHelper.fastBlockZoneCheck(
               current, rules, (s) => Console.WriteLine(s)
                ));
        }

        [TestMethod()]
        public void simpleBlockZone()
        {
            List<string> current = new List<string>()
            {
                "hash1#Name of wifi",
                "hash2#Name1 of wifi",
                "hash3#Name of2 wifi",
                "hash4#Name of wifi3",
            };

            var rules = new List<string>()
            {
                "-hash1#Name ignored"
            };

            TestWifiBlocked(true, current, rules);

            // Check new hashes added:
            Assert.IsTrue(rules.Count == 4 && rules[3].StartsWith("-hash4")) ;
        }

        [TestMethod()]
        public void simpleTrustedZone()
        {
            List<string> current = new List<string>()
            {
                "hash1#Name of wifi",
                "hash2#Name1 of wifi",
                "hash3#Name of2 wifi",
                "hash4#Name of wifi3",
            };

            var rules = new List<string>()
            {
                "+hash1#Name ignored",
                "-hash2#Name ignored"
            };

            TestWifiBlocked(false, current, rules);

            // Check new hashes added:
            Assert.IsTrue(rules.Count == 2);
        }

        [TestMethod()]
        public void simpleIgnore()
        {
            List<string> current = new List<string>()
            {
                "hash1#Name of wifi",
                "hash5#Name of wifi3",
            };

            var rules = new List<string>()
            {
                "?hash1#Name ignored",
                "-hash2#Name ignored"
            };

            var rules2 = new List<string>()
            {
                "-hash1#Name ignored",
                "-hash2#Name ignored"
            };

            TestWifiBlocked(false, current, rules);

            // Check new hashes added:
            Assert.IsTrue(rules.Count == 2);

            TestWifiBlocked(true, current, rules2);
        }


        [TestMethod()]
        public void nutralZone()
        {
            List<string> current = new List<string>()
            {
                "hash1#Name of wifi",
                "hash2#Name1 of wifi",
                "hash3#Name of2 wifi",
                "hash4#Name of wifi3",
            };

            var rules = new List<string>()
            {
                "-hash55#Name ignored"
            };

            TestWifiBlocked(false, current, rules);

            // Check new hashes added:
            Assert.IsTrue(rules.Count == 1);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckBlacklistedWifi
{
    public class WifiHelper
    {
        /// <summary>
        /// Main decision tree to see if we are in block zone.
        /// </summary>
        /// <param name="currentIDs">Wifi BSSID that currently around us</param>
        /// <param name="badIDs">Wifi BSSID that we found in bad zones</param>
        /// <param name="ignoreIDs">Wifi BSSID that are portable (like your phone hotspot)</param>
        /// <param name="trustedIDs">Wifi BSSID that will only be available in trusted zone</param>
        /// <param name="newBadIDs">A output of new wifi BSSID to block if we are in block zone</param>
        /// <param name="log">A logging function</param>
        /// <returns></returns>
        public static bool inBlockZone(
            IEnumerable<string> currentIDs,
            IEnumerable<string> badIDs, IEnumerable<string> ignoreIDs, IEnumerable<string> trustedIDs,
            out List<string> newBadIDs, Action<string> log)
        {
            bool inblockzone = true; // until proven innocent

            // Make hashset for faster searching:
            HashSet<string> currentHashes = new HashSet<string>(currentIDs),
                badsHashes = new HashSet<string>(badIDs),
                ignoredHashes = new HashSet<string>(ignoreIDs),
                trustedHashes = new HashSet<string>(trustedIDs);

            newBadIDs = new List<string>();

            HashSet<string> relevantHashes = new HashSet<string>();

            bool foundTrusted = false;
            int ignoredCount = 0;

            // Find relevant ids. If you find trusted, just stop.
            foreach(string id in currentHashes)
            {
                if (trustedHashes.Contains(id))
                {
                    log("Found trusted ('" + id +"'). So not in blockzone.");
                    foundTrusted = true;
                    break;
                }
                else if (ignoredHashes.Contains(id))
                {
                    // Dont add it to next step
                    ignoredCount++;
                }
                else 
                {
                    // not trusted and not ignored --> so need to be checked
                    relevantHashes.Add(id);
                }
            }

            if (foundTrusted)
            {
                inblockzone = false;
            }
            else
            {
                bool foundBlocked = false;

                // Now check all relevant:
                foreach (string id in relevantHashes)
                {
                    if (badsHashes.Contains(id))
                    {
                        if (!foundBlocked) // log once
                            log("Found first blocked BSSID ('" + id + "').");
                        foundBlocked = true;
                        // Don't break loop! we still want to know every new!
                    }
                    else
                    {
                        // We already filtered ignored, so you it is new and suspected to be in
                        //      block zone:
                        newBadIDs.Add(id);
                    }
                }

                if (foundBlocked)
                {
                    inblockzone = true;
                    log("In blockzone. Found " + newBadIDs.Count + " new BSSID.");
                }
                else
                {
                    inblockzone = false;
                    newBadIDs.Clear(); // no need if we are outside of black zone.
                    log("Not in blockzone. Found " + relevantHashes.Count + " BSSID around.");
                }
            }

            return inblockzone;
        }

        /// <summary>
        /// Quick call of the inBlockZone functions
        /// </summary>
        /// <param name="currentIDs">List of current Wifi BSSID</param>
        /// <param name="BSSIDsRules">prefix: '-' block, '+' trusted, '?' ignore</param>
        /// <param name="newBSSIDsRules">the entire new rule set (not only the added blocked)</param>
        /// <param name="log">A logging function</param>
        /// <returns></returns>
        public static bool fastBlockZoneCheck(
            IEnumerable<string> currentIDs,
            List<string> BSSIDsRules,
            Action<string> log)
        {
            bool inBlockZone = true;

            IEnumerable<string> trusted = BSSIDsRules
                .Where((s) => s[0] == '+')
                .Select((s) => s.Substring(1));

            IEnumerable<string> ignored = BSSIDsRules
                .Where((s) => s[0] == '?')
                .Select((s) => s.Substring(1));

            IEnumerable<string> blocked = BSSIDsRules
                .Where((s) => s[0] == '-')
                .Select((s) => s.Substring(1));

            List<string> newBSSIDs = new List<string>();

            inBlockZone = WifiHelper.inBlockZone(currentIDs, blocked, ignored, trusted, out newBSSIDs, log);

            if (inBlockZone)
            {
                foreach(string newbad in newBSSIDs)
                {
                    BSSIDsRules.Add("-" + newbad);
                }
            }

            return inBlockZone;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TimeBlock_GuiHelper
{
    public class TimeFilterObject : Common.JSONBaseClass
    {
        public bool[,] AllowDayAndTimeMatrix = new bool[7, 24];

        public bool isBlocked(DateTime dt)
        {
            int day = (int)dt.DayOfWeek;
            int hour = dt.Hour;

            return !AllowDayAndTimeMatrix[day, hour];
        }

        /// <summary>
        ///  Set value of policy
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <param name="hour">if given 6, 6AM to 7AM. from '0' = 12AM to '23' = 11PM</param>
        /// <param name="allow">allow or block?</param>
        public void setPolicy(DayOfWeek day, int hour, bool allow)
        {
            AllowDayAndTimeMatrix[(int)day, hour % 24] = allow;
        }

        public void clearAllTo(bool allow)
        {
            for (int day = 0; day < 7; day++)
            {
                for (int hour = 0; hour < 24; hour++)
                {
                    AllowDayAndTimeMatrix[day, hour] = allow;
                }
            }
        }


        public bool isBlockedNow()
        {
            return isBlocked(DateTime.Now);
        }

    }
}

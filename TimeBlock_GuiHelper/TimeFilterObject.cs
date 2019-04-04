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
    }
}

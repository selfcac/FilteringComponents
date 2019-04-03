using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeBlock_GuiHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] Days = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        string[] Hours =
        {
            "12AM",

            "1AM",
            "2AM",
            "3AM",
            "4AM",
            "5AM",
            "6AM",
            "7AM",
            "8AM",
            "9AM",
            
            "10AM",
            "11AM",
            "12PM",


            "1PM",
            "2PM",
            "3PM",
            "4PM",
            "5PM",
            "6PM",
            "7PM",
            "8PM",
            "9PM",

            "10PM",
            "11PM",
        };

        void setState(int day, int hour)
        {

        }

        private void label145_MouseEnter(object sender, EventArgs e)
        {
            Label ctrl = (Label)sender;
            int day = ctrl.TabIndex / 24;
            int hour = ctrl.TabIndex % 24;

            lblHover1.Text = Days[day] + " - " + Hours[hour];
        }
    }
}

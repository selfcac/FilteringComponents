using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeBlockFilter;

namespace TimeBlock_GuiHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reColorMatrix();
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

        TimeFilterObject timeFilter = new TimeFilterObject();

        void reColorMatrix()
        {
            foreach (Control ctrl in tabPage1.Controls)
            {
                Label lbl = ctrl as Label;
                if (lbl != null && lbl.TabIndex < 7 * 24) 
                {
                    int day = ctrl.TabIndex / 24;
                    int hour = ctrl.TabIndex % 24;

                    lbl.BackColor = (timeFilter.AllowDayAndTimeMatrix[day, hour] ?
                        lblAllowColor.BackColor : lblBlockColor.BackColor);
                }
            }
        }

        void SwitchValue(Label ctrl)
        {
           lock (timeFilter)
            {
                int day = ctrl.TabIndex / 24;
                int hour = ctrl.TabIndex % 24;

                lblHover1.Text = Days[day] + ", " + Hours[hour] + "-" + Hours[(hour + 1) % 24];

                if (MouseButtons == MouseButtons.Left)
                {
                    if (ctrl.BackColor == lblAllowColor.BackColor)
                    {
                        ctrl.BackColor = lblBlockColor.BackColor;
                        timeFilter.AllowDayAndTimeMatrix[day, hour] = false;
                    }
                    else
                    {
                        ctrl.BackColor = lblAllowColor.BackColor;
                        timeFilter.AllowDayAndTimeMatrix[day, hour] = true;
                    }
                }
            }
        }

        private void label145_MouseEnter(object sender, EventArgs e)
        {
            Label ctrl = (Label)sender;
            SwitchValue(ctrl);
        }

        private void label145_MouseDown(object sender, MouseEventArgs e)
        {
            Label ctrl = (Label)sender;
            ctrl.Capture = e.Clicks > 1;
            SwitchValue(ctrl);
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeFilter = new TimeFilterObject();
            reColorMatrix();
        }

        private void allowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeFilter.clearAllTo(true);
            reColorMatrix();
        }

        private void blockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timeFilter = new TimeFilterObject();
            reColorMatrix();
        }

        private void loadFromJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                ConnectionHelpers.TaskInfo result =
                    JSONBaseClass.FromFile<TimeFilterObject>(dlgOpen.FileName);
                if (result)
                {
                    timeFilter = ((ConnectionHelpers.TaskInfoResult<TimeFilterObject>)result).result;
                    reColorMatrix();
                }
                else
                {
                    MessageBox.Show("Can't open beacuse:\n" + result.eventReason);
                }
            }
            tmrRestoreMinimize.Enabled = true;
        }

        private void saveToJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            if (dlgSave.ShowDialog() == DialogResult.OK)
            {
                ConnectionHelpers.TaskInfo result =
                    timeFilter.ToFile(dlgSave.FileName);
                if (!result)
                {
                    MessageBox.Show("Can't save beacuse:\n" + result.eventReason);
                }
            }
            tmrRestoreMinimize.Enabled = true;
        }

        private void tmrRestoreMinimize_Tick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            tmrRestoreMinimize.Enabled = false;
        }
    }
}

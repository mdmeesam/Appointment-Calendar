using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Calendar
{
    public partial class Calendar : Form
    {
        private List<FlowLayoutPanel> listFlDay = new List<FlowLayoutPanel>();
        private DateTime currentDate = DateTime.Today;
        public Calendar()
        {
            InitializeComponent();
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            GenerateDayPanel(42);   //total 42 days displayed in main calendar
            DisplayCurrentDate();
        }
/*
 the appointment start
 */
        private DataTable QueryAsDataTable(String sql)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\host 1\Documents\Projects\Calendar\Calendar\Database.mdf; Integrated Security = True";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            /*con.Open();
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
                MessageBox.Show("Connection Succesfull at Appointment");
            else if (i == 0)
                MessageBox.Show("Not successful at appointment");*/

            DataSet ds = new DataSet();

            da.Fill(ds, "result");

            return ds.Tables["result"];
        }


        private void AddNewAppointment(object sender, EventArgs e)
        {
            //int day = (int)((FlowLayoutPanel)sender).Tag;
            int day = (int)((Label)sender).Tag;
            if (day != 0 && day<100)    //was needed before changing event clickable to label
            {
                {
                    var withBlock = new manageAppointment();
                    withBlock.AppID = 0;
                    withBlock.txtCustomer.Text = "";
                    withBlock.txtAddress.Text = "";
                    withBlock.txtComment.Text = "";
                    withBlock.dtpDate.Value = new DateTime(currentDate.Year, currentDate.Month, day);
                    withBlock.ShowDialog();
                }

                DisplayCurrentDate();
            }
            /*else if (day != 0 && day < 200)
            {
                {
                    var withBlock = new manageAppointment();
                    withBlock.AppID = 0;
                    withBlock.txtCustomer.Text = "";
                    withBlock.txtAddress.Text = "";
                    withBlock.txtComment.Text = "";
                    withBlock.dtpDate.Value = new DateTime(currentDate.Year, currentDate.AddMonths(-1).Month, (day-100));
                    withBlock.ShowDialog();
                }

                DisplayCurrentDate();
            }*/
        }
        private void ShowAppointmentDetail(object sender, EventArgs e)
        {
            int appID = (int)((LinkLabel)sender).Tag;
            string sql = $"select * from Appointments where ID = {appID}";
            DataTable dt = QueryAsDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                {
                    var withBlock = new manageAppointment();
                    withBlock.AppID = appID;
                    withBlock.txtCustomer.Text = row["ContactName"].ToString();
                    withBlock.txtAddress.Text = row["Address"].ToString();
                    withBlock.txtComment.Text = row["Comment"].ToString();
                    withBlock.dtpDate.Value = DateTime.Parse(row["AppDate"].ToString());
                    withBlock.ShowDialog();
                }

                DisplayCurrentDate();
            }
        }
        private void AddAppointmentToFlDay(int startDayAtFlNumber)
        {
            var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            string sql = $"select * from Appointments where AppDate >= '{startDate.ToShortDateString()}' and AppDate < '{endDate.ToShortDateString()}'";
            //string sql1 = "select * from Appointments where AppDate >= '23-Sep-2010' AND AppDate < '24-Sep-2010'";
            DataTable dt = QueryAsDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                var appDay = DateTime.Parse(row["AppDate"].ToString());
                var link = new LinkLabel();
                link.Tag = row["ID"];
                link.Name = $"link{row["ID"]}";
                link.Text = row["ContactName"].ToString();

                link.Click += ShowAppointmentDetail;
                listFlDay[appDay.Day - 1 + (startDayAtFlNumber - 1)].Controls.Add(link);
            }


            //need to add area to show scheduled actions inside
        }

        //SHOW DETAILS OF ACTION EVENTS SCHEDULED

        private void ShowEventDetail(object sender, EventArgs e)
        {
            int eventID = (int)((LinkLabel)sender).Tag;
            string sql = $"select * from Events where ID = {eventID}";
            DataTable dt = QueryAsDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                {
                    var eventCreation = new CreateEvent();
                    eventCreation.eventID = eventID;

                    var action = row["Action"].ToString();
                    var applicationName = row["Application"].ToString();

                    if (action == "Open an Application")
                        eventCreation.actionList.SelectedIndex = 0;
                    else if (action == "Turn off Application")
                        eventCreation.actionList.SelectedIndex = 2;
                    else 
                        eventCreation.actionList.SelectedIndex = 1;

                    //eventCreation.actionList.SelectedIndex = 0;
                    //eventCreation.actionList.SelectedText = 

                    if (applicationName == "Notepad")
                    {
                        eventCreation.applicationListOpen.SelectedIndex = 0;
                        eventCreation.applicationListClose.SelectedIndex = 0;
                    }
                    //eventCreation.applicationListOpen.Text = row["Application"].ToString();
                    //eventCreation.applicationListClose.Text = row["Application"].ToString();
                    eventCreation.EventDate.Value = DateTime.Parse(row["ActionDate"].ToString());
                    eventCreation.EventTime.Value = DateTime.Parse(row["ActionTime"].ToString());
                    eventCreation.ShowDialog();
                }

                DisplayCurrentDate();
            }
        }

        //ADD LINK OF ACTION EVENT SCHEDULED IN FLOW LAYOUT
        private void AddEventToFlDay(int startDayAtFlNumber)
        {
            var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            string sql = $"select * from Events where ActionDate >= '{startDate.ToShortDateString()}' and ActionDate < '{endDate.ToShortDateString()}'";
            //string sql1 = "select * from Appointments where AppDate >= '23-Sep-2010' AND AppDate < '24-Sep-2010'";
            DataTable dt = QueryAsDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                var eventDay = DateTime.Parse(row["ActionDate"].ToString());
                var link = new LinkLabel();
                link.Tag = row["ID"];
                link.Name = $"linkEvent{row["ID"]}";

                string openClose = row["Action"].ToString();
                if (openClose == "Open an Application")
                    openClose = "Open";
                else if (openClose == "Turn off Application")
                    openClose = "Close";
                else openClose = "Shutdown";

                link.Text = $"{openClose} {row["Application"].ToString()}";

                
                link.Click += ShowEventDetail;
                listFlDay[eventDay.Day - 1 + (startDayAtFlNumber - 1)].Controls.Add(link);
            }


            //need to add area to show scheduled actions inside
        }





        /*
        The Appointment END
        */
        private int GetFirstDayOfWeekOfCurrentDate()
        {
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            
            //MessageBox.Show(((int)firstDayOfMonth.DayOfWeek).ToString());
            return (int)firstDayOfMonth.DayOfWeek + 1;
            
            
            //return (int)firstDayOfMonth.Day;
        }

        private int GetTotalDaysOfCurrentDate()
        {
            var firstDayOfCurrentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            return firstDayOfCurrentDate.AddMonths(1).AddDays(-1).Day;
        }
        private int GetTotalDaysOfPrevMonth()
        {
            var lastDayOfPrevMonth = new DateTime(currentDate.Year, currentDate.Month, 1);//current month 1st date
            return lastDayOfPrevMonth.AddDays(-1).Day;  //current month -1 = previous month days
        }
        private void DisplayCurrentDate()
        {
            date.Text = currentDate.ToString("MMMM, yyyy");
            int firstDayAtFlNumber = GetFirstDayOfWeekOfCurrentDate();
            int totalDay = GetTotalDaysOfCurrentDate();
            //MessageBox.Show(firstDayAtFlNumber.ToString() + ' ' + totalDay.ToString());
            AddLabelDayToFlDay(firstDayAtFlNumber, totalDay);
            AddAppointmentToFlDay(firstDayAtFlNumber);
            AddEventToFlDay(firstDayAtFlNumber);
        }

        private void PrevMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            DisplayCurrentDate();
        }

        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            DisplayCurrentDate();
        }

        private void Today()
        {
            currentDate = DateTime.Today;
            DisplayCurrentDate();
        }

        //CREATE AN EVENT FORM TO SCHEDULE AND SAVE AN EVENT
        private void CreateAnEvent(object sender, EventArgs e)
        {
            int day = (int)((FlowLayoutPanel)sender).Tag;
            
            if (day != 0 && day < 100)
            {
                {
                    var eventCreation = new CreateEvent();
                    eventCreation.eventID = 0;
                    //eventCreation.dayNumber = (int)((FlowLayoutPanel)sender).Tag;
                    eventCreation.EventDate.Value = new DateTime(currentDate.Year, currentDate.Month, day);
                    eventCreation.EventTime.Value = new DateTime(DateTime.Now.Ticks);
                    eventCreation.ShowDialog();
                }

                DisplayCurrentDate();
            }
        }
        private void GenerateDayPanel(int totalDays)
        {
            //f1Days is name of Flow layout panel for main calendar
            flDays.Controls.Clear();    //clearing any objects inide currently
            //listf1Day is of type List(used for strongly arranged list of objects of Flowlayoutpanel
            listFlDay.Clear();


            //for all dates inside month display the blocks for each calendar date and set attributes
            //MessageBox.Show(totalDays.ToString());
            for (int i = 1; i <= totalDays; i++)
            {
                
                FlowLayoutPanel fl = new FlowLayoutPanel();
                fl.Name = "flDay" + i;
                /*Console.Out.WriteLine(fl.Name);
                Console.Out.WriteLine("Test other way");
                Console.Out.WriteLine($"flDay{i}");*/

                fl.Size = new Size(139, 80);
                fl.BackColor = Color.White;
                fl.BorderStyle = BorderStyle.FixedSingle;
                fl.Cursor = Cursors.Hand;
                fl.AutoScroll = true;

                //fl.Click += AddNewAppointment;
                fl.Click += CreateAnEvent;

                flDays.Controls.Add(fl);    //adds f1Days field/box to the main flow layout panel

                listFlDay.Add(fl); //adds the date box field to the list of days in fl panel

                //Console.Out.WriteLine(listFlDay);
            }
            
        }
        private void AddLabelDayToFlDay(int startDayAtFlNumber, int totalDaysInMonth)
        {
            foreach (FlowLayoutPanel fl in listFlDay)
            {
                fl.Controls.Clear();
                fl.Tag = 0;
                fl.BackColor = Color.White;
            }

            //listFlDay[startDayAtFlNumber + i].Tag = i;
            //For the previous months labels
            int lastMonthTotDays = GetTotalDaysOfPrevMonth();
            int prevDate = (1 - 1) + (startDayAtFlNumber - 1);  //previous date is the tag of the number of boxes empty before the current month
            if (prevDate > 0 && prevDate < 7)
            {
                //currentDate.AddDays(-1).Day;
                //MessageBox.Show(lastMonthTotDays.ToString());
                for (int j = prevDate; j > 0; j--)
                {
                    //Add label to that box
                    var lbl = new Label();
                    lbl.Name = $"lblDayPrev{lastMonthTotDays}";
                    lbl.AutoSize = false;
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    lbl.Size = new Size(110, 22);
                    lbl.Text = lastMonthTotDays.ToString();
                    lbl.Font = new Font("Microsoft Sans Serif", 12);
                    lbl.ForeColor = Color.Gainsboro;

                    //Give tag to the box
                    //100+lastmonth is for previous month
                    listFlDay[j - 1].Tag = 100+lastMonthTotDays;

/*1-100 is current month
 * 100-200 is previous month
 */
                    //listFlDay[j].Tag = "test";
                    lastMonthTotDays--;

                    //Add label to the date panel
                    listFlDay[j - 1].Controls.Add(lbl);
                }
                //the tag needs to be set in reverse order
                //so that the previous ones get tagged
                // all previous dates then need their lbl names with their own special names
                //then a label needs to be added with .controls.add
            }

            for (int i = 1, loopTo = totalDaysInMonth; i <= loopTo; i++)
            {
                var lbl = new Label();
                lbl.Name = $"lblDay{i}";
                lbl.AutoSize = false;
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.Size = new Size(110, 22);
                lbl.Text = i.ToString();
                lbl.Font = new Font("Microsoft Sans Serif", 12);
                listFlDay[(i - 1) + (startDayAtFlNumber - 1)].Tag = i;

                lbl.Tag = i;
                lbl.Click += AddNewAppointment;
                
                
                listFlDay[i - 1 + (startDayAtFlNumber - 1)].Controls.Add(lbl);
                if (new DateTime(currentDate.Year, currentDate.Month, i) == DateTime.Today)
                {
                    listFlDay[i - 1 + (startDayAtFlNumber - 1)].BackColor = Color.Aqua;
                }
            }
            //MessageBox.Show(listFlDay[0].Tag.ToString());

            //**********Next Month Labels***********

            int nextMonthDateStartingPosition = (totalDaysInMonth) + (startDayAtFlNumber - 1);
            if (nextMonthDateStartingPosition < 42)
            {
                int counter = 1;    //counter is the next months starting date
                
                for (int j = nextMonthDateStartingPosition; j < 42 ; j++)
                {
                    //Add label to that box
                    var lbl = new Label();
                    lbl.Name = $"lblDayNext{counter}";
                    lbl.AutoSize = false;
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    lbl.Size = new Size(110, 22);
                    lbl.Text = counter.ToString();
                    lbl.Font = new Font("Microsoft Sans Serif", 12);
                    lbl.ForeColor = Color.Gainsboro;

                    //Give tag to the box
                    //200+counter is for next month
                    listFlDay[j].Tag = 200 + counter;

                    
                    counter++;  //increment date of month

                    //Add label to the date panel
                    listFlDay[j].Controls.Add(lbl);
                }
                //the tag needs to be set in reverse order
                //so that the previous ones get tagged
                // all previous dates then need their lbl names with their own special names
                //then a label needs to be added with .controls.add
            }
        }

        private void btnPrevMonth_Click(object sender, EventArgs e)
        {
            PrevMonth();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            NextMonth();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            Today();
        }
    }
}

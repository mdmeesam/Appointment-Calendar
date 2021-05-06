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
            int day = (int)((FlowLayoutPanel)sender).Tag;
            if (day != 0)
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
        }
/*
The Appointment END
*/
        private int GetFirstDayOfWeekOfCurrentDate()
        {
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            return (int)firstDayOfMonth.DayOfWeek + 1;
            //return (int)firstDayOfMonth.Day;
        }

        private int GetTotalDaysOfCurrentDate()
        {
            var firstDayOfCurrentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
            return firstDayOfCurrentDate.AddMonths(1).AddDays(-1).Day;
        }

        private void DisplayCurrentDate()
        {
            date.Text = currentDate.ToString("MMMM, yyyy");
            int firstDayAtFlNumber = GetFirstDayOfWeekOfCurrentDate();
            int totalDay = GetTotalDaysOfCurrentDate();
            AddLabelDayToFlDay(firstDayAtFlNumber, totalDay);
            AddAppointmentToFlDay(firstDayAtFlNumber);
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
        private void GenerateDayPanel(int totalDays)
        {
            //f1Days is name of Flow layout panel for main calendar
            flDays.Controls.Clear();    //clearing any objects inide currently
            //listf1Day is of type List(used for strongly arranged list of objects of Flowlayoutpanel
            listFlDay.Clear();
            

            //for all dates inside month display the blocks for each calendar date and set attributes
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

                fl.Click += AddNewAppointment;

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
                
                listFlDay[i - 1 + (startDayAtFlNumber - 1)].Controls.Add(lbl);
                if (new DateTime(currentDate.Year, currentDate.Month, i) == DateTime.Today)
                {
                    listFlDay[i - 1 + (startDayAtFlNumber - 1)].BackColor = Color.Aqua;
                }
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

using System;
using System.Collections.Generic;
using System.Linq;

public partial class frmDisplayAppointment
{
    public frmDisplayAppointment()
    {
        base.Load += frmDisplayAppointment_Load;
    }

    private List<FlowLayoutPanel> listFlDay = new List<FlowLayoutPanel>();
    private DateTime currentDate = DateTime.Today;

    private void frmDisplayAppointment_Load(object sender, EventArgs e)
    {
        GenerateDayPanel(42);
        DisplayCurrentDate();
    }

    private void AddNewAppointment(object sender, EventArgs e)
    {
        int day = ((FlowLayoutPanel)sender).Tag;
        if (day != 0)
        {
            {
                var withBlock = frmManageAppointment;
                withBlock.AppID = 0;
                withBlock.txtName.Text = "";
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
        int appID = ((LinkLabel)sender).Tag;
        string sql = $"select * from appointment where ID = {appID}";
        DataTable dt = QueryAsDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows(0);
            {
                var withBlock = frmManageAppointment;
                withBlock.AppID = appID;
                withBlock.txtName.Text = row("ContactName");
                withBlock.txtAddress.Text = row("Address");
                withBlock.txtComment.Text = row("Comment");
                withBlock.dtpDate.Value = row("AppDate");
                withBlock.ShowDialog();
            }

            DisplayCurrentDate();
        }
    }

    private void AddAppointmentToFlDay(int startDayAtFlNumber)
    {
        var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        string sql = $"select * from appointment where AppDate between #{startDate.ToShortDateString()}# and #{endDate.ToShortDateString()}#";
        DataTable dt = QueryAsDataTable(sql);
        foreach (DataRow row in dt.Rows)
        {
            var appDay = DateTime.Parse(row("AppDate"));
            var link = new LinkLabel();
            link.Tag = row("ID");
            link.Name = $"link{row("ID")}";
            link.Text = row("ContactName");
            link.Click += ShowAppointmentDetail;
            listFlDay[appDay.Day - 1 + (startDayAtFlNumber - 1)].Controls.Add(link);
        }
    }

    private int GetFirstDayOfWeekOfCurrentDate()
    {
        var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
        return (int)firstDayOfMonth.DayOfWeek + 1;
    }

    private int GetTotalDaysOfCurrentDate()
    {
        var firstDayOfCurrentDate = new DateTime(currentDate.Year, currentDate.Month, 1);
        return firstDayOfCurrentDate.AddMonths(1).AddDays(-1).Day;
    }

    private void DisplayCurrentDate()
    {
        lblMonthAndYear.Text = currentDate.ToString("MMMM, yyyy");
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
        flDays.Controls.Clear();
        listFlDay.Clear();
        for (int i = 1, loopTo = totalDays; i <= loopTo; i++)
        {
            var fl = new FlowLayoutPanel();
            fl.Name = $"flDay{i}";
            fl.Size = new Size(128, 99);
            fl.BackColor = Color.White;
            fl.BorderStyle = BorderStyle.FixedSingle;
            fl.Cursor = Cursors.Hand;
            fl.AutoScroll = true;
            fl.Click += AddNewAppointment;
            flDays.Controls.Add(fl);
            listFlDay.Add(fl);
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
            lbl.Text = i;
            lbl.Font = new Font("Microsoft Sans Serif", 12);
            listFlDay[i - 1 + (startDayAtFlNumber - 1)].Tag = i;
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
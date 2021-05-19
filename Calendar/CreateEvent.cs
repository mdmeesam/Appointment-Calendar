using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Calendar
{
    public partial class CreateEvent : Form
    {
        public int eventID = 0;
        DateTime dateOfOld;
        string dateOfOldFormatted = "";
        String appNameOld = "";
        string openORCloseOld = "";

        // public int dayNumber = 0;
        public CreateEvent()
        {
            InitializeComponent();
        }

        private int InsertUpdateDelete(String query)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\host 1\Documents\Projects\Calendar\Calendar\Database.mdf; Integrated Security = True";
            
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();

            return i;

        }
        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            SaveEventBtn.Enabled = true;
            if (actionList.SelectedIndex == 0)
            {
                if (applicationListClose.Visible == true)
                    applicationListClose.Visible = false;
                applicationListOpen.Visible = true;
                appListLabel.Visible = true;
                //SaveEventBtn.Enabled = true;
            }
            else if(actionList.SelectedIndex==1)
            {
                
                /*if(MessageBox.Show("Are you sure you want to shutdown now", "Confirm ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("done");
                Process.Start("shutdown", "/s /t 600");*/
                //shutdown -a to abort shutdown
                //SaveEventBtn.Enabled = true;
            }
            else if(actionList.SelectedIndex == 2)
            {
                if (applicationListOpen.Visible == true)
                    applicationListOpen.Visible = false;
                appListLabel.Visible = true;
                applicationListClose.Visible = true;                
                applicationListClose.Location= new Point(156, 116);
                //SaveEventBtn.Enabled = true;
            }
        }

        private void applicationList_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            /*
            Commad for scheduling a task
           schtasks /create /sc once /tn notepad /tr notepad.exe /st 13:00

           to delete task from task scheduler
           schtasks /delete /f /tn notepad(name of task)    /f is to force delete from scheduler

           to create a task to close an application at a scheudled time
            schtasks /create /sc once /st 13:00 /tn notepadclose /tr "taskkill /im notepad.exe"     use /f in taskkill to force close
            */
            //maybe use switch case here
            
        }

        private void applicationListClose_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            if (applicationListClose.SelectedIndex == 0)
            {
                //System.Diagnostics.Process.Start("taskkill", @"/im notepad.exe");
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveEventBtn_Click(object sender, EventArgs e)
        {
            //Creating variables and storing data from the Form into Databse
            String action = actionList.Text;
            String applicationName = "";
            if (actionList.SelectedIndex == 0)
                applicationName = applicationListOpen.Text;
            else if (actionList.SelectedIndex == 1)
                applicationName = "Shutdown";
            else if (actionList.SelectedIndex == 2)
                applicationName = applicationListClose.Text;

            string date = EventDate.Value.ToShortDateString();// used to enter into database
            String time = EventTime.Value.Value.ToShortTimeString();    //used to entter into database


            //Definitions for Adding into Task Scheduler
            var timeSelected = EventTime.Value.Value.TimeOfDay;
            string timeFormat = timeSelected.ToString(@"hh\:mm");   //needed to input into Task scheduler

            var dateSelected = EventDate.Value;
            string dateFormat = dateSelected.ToString(@"MM/dd/yyyy");   //needed for input into task scheduler start date
            string dateFormat1 = dateSelected.ToString(@"MM-dd-yyyy");  //needed for input into task scheduler for task name

            //Dont need declarations because already givving names ubove for applicationName
            //var appNameOpen = applicationListOpen.Text;
            //var appNameClose = applicationListClose.Text;  


            //Send data in form to Execute SQL command and save to database
            if (eventID == 0)
            {
                String sqlQuery = $@"insert into Events (ActionDate,Action,Application,ActionTime) 
                                values ('{EventDate.Value.Date}','{action}','{applicationName}','{time}')";
                if (InsertUpdateDelete(sqlQuery) > 0)
                {
                    //Dont need too many messages
                    //MessageBox.Show("Saved Successfully");
                    //Close();

                    //TASK SCHEDULER PROCESS ADDING
                    if (actionList.SelectedIndex == 0)
                    {
                        if (applicationListOpen.SelectedIndex == 0) //if you need to Open Notepad
                        {
                            //System.Diagnostics.Process.Start(@"C:\Windows\System32\Notepad.exe");

                            Process.Start("schtasks", $@"/create /sc once /tn OpenNotepad{dateFormat1} /tr notepad.exe /sd {dateFormat} /st {timeFormat}");
                            MessageBox.Show("Your scheduled task will run at specified time");
                        }
                    }

                    //SHUTDOWN Process Add into Task Scheduler
                    else if (actionList.SelectedIndex == 1)
                    {
                        Process.Start("schtasks", $@"/create /sc once /tn ShutdownShutdown{dateFormat1} /tr shutdown.exe /sd {dateFormat} /st {timeFormat}");
                        //schtasks /create /sc once /tn Shutdown /tr shutdown.exe /sd 05/19/2021 /st 10:30
                        MessageBox.Show($"Your scheduled Shutdown will run on {dateFormat} at {timeFormat}");
                    }

                    else if (actionList.SelectedIndex == 2)
                    {    //CLOSE an application and add to task scheduler
                        if (applicationListClose.SelectedIndex == 0)
                        {
                            Process.Start("schtasks", $@"/create /sc once /tn CloseNotepad{dateFormat1} /tr ""taskkill / im notepad.exe"" /sd {dateFormat} /st {timeFormat}");
                            MessageBox.Show("Your scheduled task will run at specified time");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }
            else
            {
                string sqlQuery = $@"update Events set ActionDate = '{EventDate.Value.Date}',
                                    Action = '{action}',
                                    Application = '{applicationName}',
                                    ActionTime = '{time}'
                                    where Id = {eventID}";
                if (InsertUpdateDelete(sqlQuery) > 0)
                {
                    //DELETE EXISTING TASK FROM SCHEDULER AND ADD NEW TASK
                    Process.Start("schtasks", $@"/delete /f /tn {openORCloseOld}{appNameOld}{dateOfOldFormatted}");


                    //TASK SCHEDULER PROCESS ADDING
                    if (actionList.SelectedIndex == 0)
                    {
                        if (applicationListOpen.SelectedIndex == 0) //if you need to Open Notepad
                        {
                            //System.Diagnostics.Process.Start(@"C:\Windows\System32\Notepad.exe");

                            Process.Start("schtasks", $@"/create /sc once /tn OpenNotepad{dateFormat1} /tr notepad.exe /sd {dateFormat} /st {timeFormat}");
                            MessageBox.Show("Your scheduled task will run at specified time");
                        }
                    }

                    //SHUTDOWN Process Add into Task Scheduler
                    else if (actionList.SelectedIndex == 1)
                    {
                        Process.Start("schtasks", $@"/create /sc once /tn ShutdownShutdown{dateFormat1} /tr shutdown.exe /sd {dateFormat} /st {timeFormat}");
                        //schtasks /create /sc once /tn Shutdown /tr shutdown.exe /sd 05/19/2021 /st 10:30
                        MessageBox.Show($"Your scheduled Shutdown will run on {dateFormat} at {timeFormat}");
                    }

                    else if (actionList.SelectedIndex == 2)
                    {    //CLOSE an application and add to task scheduler
                        if (applicationListClose.SelectedIndex == 0)
                        {
                            Process.Start("schtasks", $@"/create /sc once /tn CloseNotepad{dateFormat1} /tr ""taskkill /im notepad.exe"" /sd {dateFormat} /st {timeFormat}");
                            MessageBox.Show("Your scheduled task will run at specified time");
                        }
                    }






                    MessageBox.Show("Updated");

                    Close();
                }
                else
                {
                    MessageBox.Show("Update Failed");
                }


            }

              
            


        }
        private bool IsConfirm(String question)
        {

            return MessageBox.Show(question, "Confirm ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            //MAKE SURE TO ADD DELETE TASK HERE FROM TASK SCHEDULR ALSO
           // var timeSelected = EventTime.Value.Value.TimeOfDay;
            //string timeFormat = timeSelected.ToString(@"hh\:mm");   //needed to input into Task scheduler

            var dateSelected = EventDate.Value;
            //string dateFormat = dateSelected.ToString(@"MM/dd/yyyy");   //needed for input into task scheduler start date
            string dateFormat1 = dateSelected.ToString(@"MM-dd-yyyy");  //needed for input into task scheduler for task name

            String applicationName = "";
            string openClose = "";
            if (actionList.SelectedIndex == 0)
            {
                applicationName = applicationListOpen.Text;
                openClose = "Open";
            }
            else if (actionList.SelectedIndex == 1)
            {   
                applicationName = "Shutdown";
                openClose = "Shutdown";
            } 
            else if (actionList.SelectedIndex == 2)
            {
                applicationName = applicationListClose.Text;
                openClose = "Close";
            }


            //schtasks /delete /f /tn notepad(name of task)    /f is to force delete from scheduler

            if (IsConfirm("Do you want to delete this Event ?"))
            {
                string sql = $"delete from Events where ID = {eventID}";
                

                if (InsertUpdateDelete(sql) > 0)
                {
                    try
                    {
                        Process.Start("schtasks", $@"/delete /f /tn {openClose}{applicationName}{dateFormat1}");
                        MessageBox.Show("Deleted");
                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("Delete Failed from Scheduler");
                    }                    
                }
                else
                {
                    MessageBox.Show("Delete Failed");
                }
            }
        }

        private void CreateEvent_Load(object sender, EventArgs e)
        {
            DeleteBtn.Visible = eventID != 0;

            //for update task scheduler
            if (eventID != 0)
            {
                dateOfOld = EventDate.Value;
                dateOfOldFormatted = dateOfOld.ToString(@"MM-dd-yyyy");  //needed for input into task scheduler for task name

                
                if (actionList.SelectedIndex == 0)
                {
                    appNameOld = applicationListOpen.Text;
                    openORCloseOld = "Open";
                }
                else if (actionList.SelectedIndex == 1)
                {
                    appNameOld = "Shutdown";
                    openORCloseOld = "Shutdown";
                }
                else if (actionList.SelectedIndex == 2)
                {
                    appNameOld = applicationListClose.Text;
                    openORCloseOld = "Close";
                }
            }

        }
    }
}

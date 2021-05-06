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
using System.Configuration;
using System.Collections.Specialized;

namespace Calendar
{
    public partial class manageAppointment : Form
    {
        //private static string cs = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //private static string cs = ConfigurationManager.ConnectionStrings["Calendar.Properties.Settings.cn"].ConnectionString;
        //Calendar objTest = new Calendar();

        public int AppID = 0;

        public manageAppointment()
        {
            InitializeComponent();
        }

        private int InsertUpdateDelete(String query)
        {
            //Some reason DataDirectory is not working needs exact file path
            //string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = |DataDirectory|\Database.mdf; Integrated Security = True";
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\host 1\Documents\Projects\Calendar\Calendar\Database.mdf; Integrated Security = True";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            //bool i = Convert.ToBoolean(cmd.ExecuteNonQuery());

            return i;
        }

        private bool IsConfirm(String question)
        {
            
            return MessageBox.Show(question, "Confirm ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtCustomer.Text) || string.IsNullOrWhiteSpace(txtAddress.Text))
                MessageBox.Show("Fields cannot Be Empty");
            else
            {

                String name = txtCustomer.Text;
                string address = txtAddress.Text;
                string comment = txtComment.Text;
                //DateTime time = dtpDate.Value;
                string time = dtpDate.Value.ToShortDateString();
                //Console.Out.WriteLine(tme+"  and in time format  "+time);



                //This was test query before complexity
                //String query = "INSERT INTO Appointments (AppDate, ContactName, Address, Comment) VALUES ('" + time + "','" + name + "','" + address + "','" + comment + "')";


                //Setting the query below
                if (AppID == 0)
                {
                    string sql = $@"insert into Appointments (AppDate, ContactName, Address, Comment) 
                            values('{dtpDate.Value.ToShortDateString()}', '{txtCustomer.Text}', '{txtAddress.Text}', '{txtComment.Text}')";
                    if (InsertUpdateDelete(sql)>0)
                    {
                        MessageBox.Show("Inserted");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Failed");
                    }
                }
                else
                {
                    string sql = $@"update Appointments set AppDate = '{dtpDate.Value.ToShortDateString()}', 
                                            ContactName = '{txtCustomer.Text}', Address = '{txtAddress.Text}', 
                                            Comment = '{txtComment.Text}'
                                    where ID = {AppID}";
                    if (InsertUpdateDelete(sql)>0)
                    {
                        MessageBox.Show("Updated");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Update Failed");
                    }
                }
                


                //Setting the command
                /*SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                if (i > 0)
                    MessageBox.Show("Data has been Inserted");
                else if (i == 0)
                    MessageBox.Show("Not successful");*/
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsConfirm("Do you want to delete this appointment ?"))
            {
                string sql = $"delete from Appointments where ID = {AppID}";
                if (InsertUpdateDelete(sql)>0)
                {
                    MessageBox.Show("Deleted");
                    Close();
                }
                else
                {
                    MessageBox.Show("Delete Failed");
                }
            }
        }

        private void manageAppointment_Load(object sender, EventArgs e)
        {
            btnDelete.Visible = AppID != 0;
        }
    }
}




//The following is under btnSave_Click
/*
//Link to functions that will create connection and enter into daatabase
            using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    String name = "tstname";
                    String address = "tstadd";
                    String comment = "tstcomment";

                    //SqlCommand cmd = new SqlCommand("insert into Appointments(ContactName,Address,Comment) values(@name,@address,@comment)", con);

                    SqlCommand cmd = new SqlCommand("insert into Appointments(ContactName,Address,Comment) values('"+name+"','"+address+"','"+comment+"')", con);

                    //cmd.Parameters.AddWithValue("name", "testname");
                    //cmd.Parameters.AddWithValue("address", "testaddress");
                    //cmd.Parameters.AddWithValue("comment", "testcomment");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    
                    MessageBox.Show(" Connecttion succesful");

                }
                catch
                {
                    MessageBox.Show(" NOt succesful");
                }
            }
 */
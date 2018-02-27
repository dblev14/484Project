using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data;
using System.Collections;
using System.Web.Configuration;

public partial class EditEmployee : System.Web.UI.Page
{
    //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        if (listEmployees.SelectedIndex == -1)
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

        if ((int)Session["AdminFlag"] == 0)
        {

            Response.Redirect("RewardFeed.aspx");
        }


        try
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();

        }
        catch (Exception)
        {

        }
        if (!IsPostBack)
        {
            populateListBox();
        }
    }

    protected void listEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnUpdate.Enabled = true;
        btnDelete.Enabled = true;
        try
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();

        }
        catch (Exception)
        {

        }
        int employeeID = Convert.ToInt32(listEmployees.SelectedValue);
        SqlCommand select = new SqlCommand();
        select.Connection = sc;
        select.CommandText = "Select FirstName, MiddleInitial, LastName, Position, Email, PhoneNumber, TerminationDate, AdminFlag  from Employee where EmployeeID = @employee";
        select.Parameters.AddWithValue("@employee", employeeID);
        SqlDataReader read = select.ExecuteReader();
        while (read.Read())
        {
            txtFirstName.Text = read[0].ToString();
            txtLastName.Text = read[2].ToString();
            txtMI.Text = read[1].ToString();
            txtPosition.Text = read[3].ToString();
            txtEmail.Text = read[4].ToString();
            txtPhone.Text = read[5].ToString();
            txtTerminationDate.Text = read[6].ToString();
            txtAdmin.Text = read[7].ToString();
        }
    }

    protected void populateListBox()
    {
        try
        {
            sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            sc.Open();

        }
        catch (Exception)
        {

        }
        SqlCommand select = new SqlCommand();
        select.Connection = sc;
        select.CommandText = "Select (Employee.FirstName + ' '  + Employee.LastName), Employee.Position, Employee.EmployeeID from Employee where TerminatedFlag = 0";
        SqlDataReader read = select.ExecuteReader();
        while (read.Read())
        {
            String result = read[0].ToString() + ", " + read[1].ToString();
            listEmployees.Items.Add(new ListItem(result, read[2].ToString()));
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int employeeID = Convert.ToInt32(listEmployees.SelectedValue);
        int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
        String currentUser = "";
        try
        {
            SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
            if (sc1.State == System.Data.ConnectionState.Closed)
                sc1.Open();

        }
        catch (Exception)
        {

        }
        using (SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            //sc.Open();
            // select the project name that matches what the user puts in the search box
            String pullCurrentUser = "SELECT FirstName, LastName FROM Employee WHERE EmployeeID = " + currentID;
            using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc1))
            {
                if (sc1.State == System.Data.ConnectionState.Closed)
                    sc1.Open();
                using (SqlDataReader reader = pullUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentUser = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                    }
                    reader.Close();
                }
            }
        }
        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Update Employee set TerminatedFlag = 1, LastUpdated = @lu, LastUpdatedBy = @lub where EmployeeID = @employee";
        delete.Parameters.AddWithValue("@employee", employeeID);
        delete.Parameters.AddWithValue("@lu", DateTime.Now);
        delete.Parameters.AddWithValue("@lub", currentUser);
        if (sc.State == System.Data.ConnectionState.Closed)
        {
            SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
            if (sc1.State == System.Data.ConnectionState.Closed)
                sc1.Open();
        }
        delete.ExecuteNonQuery();
        txtAdmin.Text = "";
        txtEmail.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtMI.Text = "";
        txtPhone.Text = "";
        txtPosition.Text = "";
        txtTerminationDate.Text = "";
        listEmployees.SelectedIndex = -1;
        listEmployees.Items.Clear();
        populateListBox();

        btnDelete.Enabled = false;
        btnUpdate.Enabled = false;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        bool run = true;
        run = IsValidEmail(txtEmail.Text);

        if (run == true)
        {
            if (Convert.ToInt32(txtAdmin.Text) == 0 || Convert.ToInt32(txtAdmin.Text) == 1)
            {
                try
                {
                    SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
                    if (sc1.State == System.Data.ConnectionState.Closed)
                        sc1.Open();

                }
                catch (Exception)
                {

                }
                int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
                String currentUser = "";
                using (SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc1.State == System.Data.ConnectionState.Closed)
                        sc1.Open();
                    // select the project name that matches what the user puts in the search box
                    String pullCurrentUser = "SELECT FirstName, LastName FROM Employee WHERE EmployeeID = " + currentID;
                    using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc1))
                    {
                        using (SqlDataReader reader = pullUser.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                currentUser = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                            }
                            reader.Close();
                        }
                    }
                }
                SqlCommand insert = new SqlCommand();
                insert.Connection = sc;
                insert.CommandText = "Update Employee set FirstName = @firstName, MiddleInitial = @MI, LastName = @lastName, Position = @position, TerminationDate = @term, Email = @email, PhoneNumber = @phone, AdminFlag = @admin, LastUpdated = @lu, LastUpdatedBy = @lub where EmployeeID = @employee";
                String firstName = Convert.ToString(txtFirstName.Text);
                insert.Parameters.AddWithValue("@firstName", firstName);
                String middle = Convert.ToString(txtMI.Text);
                insert.Parameters.AddWithValue("@MI", middle);
                String lastName = Convert.ToString(txtLastName.Text);
                insert.Parameters.AddWithValue("@lastName", lastName);
                String position = Convert.ToString(txtPosition.Text);
                insert.Parameters.AddWithValue("@position", position);
                String email = Convert.ToString(txtEmail.Text);
                insert.Parameters.AddWithValue("@email", email);
                String phone = Convert.ToString(txtPhone.Text);
                insert.Parameters.AddWithValue("@phone", phone);
                if (txtTerminationDate.Text == String.Empty)
                {
                    insert.Parameters.AddWithValue("@term", DBNull.Value);
                }
                else
                {
                    insert.Parameters.AddWithValue("@term", Convert.ToDateTime(txtTerminationDate.Text));
                }
                String admin = Convert.ToString(txtAdmin.Text);
                insert.Parameters.AddWithValue("@admin", admin);
                int employeeID = Convert.ToInt32(listEmployees.SelectedValue);
                insert.Parameters.AddWithValue("@employee", employeeID);
                insert.Parameters.AddWithValue("@lu", DateTime.Now);
                insert.Parameters.AddWithValue("@lub", currentUser);
                if (sc.State == System.Data.ConnectionState.Closed)
                {
                    SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
                    if (sc1.State == System.Data.ConnectionState.Closed)
                        sc1.Open();
                }
                insert.ExecuteNonQuery();
                txtAdmin.Text = "";
                txtEmail.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtMI.Text = "";
                txtPhone.Text = "";
                txtPosition.Text = "";
                txtTerminationDate.Text = "";
                listEmployees.SelectedIndex = -1;
                listEmployees.Items.Clear();
                lblMessage.Text = "";
                populateListBox();

                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                lblMessage.Text = "Please enter a valid Admin Flag";
            }
        }
        else
        {
            lblMessage.Text = "Please enter a valid email address.";
        }
    }

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            //change erro message
            return false;
        }
    }
}
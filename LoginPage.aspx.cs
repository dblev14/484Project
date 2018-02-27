using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class LoginPage : System.Web.UI.Page
{
    //SqlConnection sc = new SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    AuthenticateEventArgs auth = new AuthenticateEventArgs();
    int terminatedFlag;

    int currentEmployee = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        auth.Authenticated = false;
        Session["LoggedIn"] = false;


        //sc.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
        sc.Open();

    }

   

    protected void btnLogin_Click1(object sender, EventArgs e)
    {
        
        auth.Authenticated = false;

        String userName = txtUsername.Text;
        String password = txtPassword.Text;


        // the Authenticated property of the AuthenitaceEventArgs object is what
        // determines whether to authenticate the login or not...here we assume no
        //temporary//// e.Authenticated = false;


        SqlCommand command = new SqlCommand();
        command.Connection = sc;

        // performing the query to get the person with the entered firstname
        command.CommandText = "select top 1 FirstName, LastName, PasswordHash, PasswordSalt, Username, AccountPassword.EmployeeID, TerminatedFlag FROM AccountPassword inner join Employee on AccountPassword.EmployeeID = Employee.EmployeeID  where AccountPassword.userName = '" + userName + "' ";
        SqlDataReader reader = command.ExecuteReader();

        // if there is such a record, read it
        if (reader.HasRows)
        {
            reader.Read();
            String pwHash = reader["PasswordHash"].ToString();  // retrieve the password hash

            // use the SimpleHash object to verify the user's entered password
            bool verify = SimpleHash.VerifyHash(password, "MD5", pwHash);

            // the result of the VerifyHash is a boolean; we use this to determine authentication 
            //e.Authenticated = verify;
            auth.Authenticated = verify;

            //saves the logged in user id 

            currentEmployee = (int)reader["EmployeeID"];

            System.Web.HttpContext.Current.Session["CurrentUserID"] = currentEmployee;

            terminatedFlag = Convert.ToInt32(reader["TerminatedFlag"]);
            

        }


        // at this point the authentication has been determined
        // We will put the result in a Session variable so that other pages in the application can
        // see the value
        // Session["loggedIn"] = e.Authenticated.ToString();

        Session["loggedIn"] = auth.Authenticated.ToString();

        /* NOTE: within an application, as it is running for a user, 
         * Session variables can be used to hold data that is shared by all pages in the application.
         * A session variable remains in the server's memory for the duration of a users's session
         * of a web application. A user's session remains as long as the user's browser is open and the
         * user remains active on the site. By default, 20 minutes of inactivity terminates the session.
         */


        if (terminatedFlag == 1)
        {
            Session["loggedIn"] = false;
        }


        //Check if current employee is an administrator
        String adminSearchQuery = "SELECT FirstName, LastName, AdminFlag,CompanyID FROM EMPLOYEE WHERE EmployeeID = " + currentEmployee;

        int adminFlag = 0;
        int companyID = 0;
        String firstName = "";
        String lastName = "";


        SqlCommand adminSearch = new SqlCommand(adminSearchQuery, sc);
        adminSearch.CommandText = adminSearchQuery;
        //Session["AdminFlag"] = adminSearch.ExecuteScalar();
        //adminSearch.ExecuteReader();
        SqlDataReader Employeereader = adminSearch.ExecuteReader();
        while (Employeereader.Read())
        {
            firstName = (Employeereader[0].ToString());
            lastName =  (Employeereader[1].ToString());
            adminFlag = Convert.ToInt32(Employeereader[2].ToString());
            companyID = Convert.ToInt32(Employeereader[3].ToString());
        }

        Session["AdminFlag"] = adminFlag;
        Session["CompanyID"] = companyID;
        Session["CurrentUserFirstName"] = firstName;
        Session["CurrentUserLastname"] = lastName;











        if (Session["loggedIn"].ToString() == "True")
        {
            Gift1.shoppingCart.Clear();

            //sc.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            //sc.Open();
            SqlCommand getLogIn = new SqlCommand();
            getLogIn.Connection = sc;
            getLogIn.CommandText = "Select LastLogIn from AccountPassword where EmployeeID = @employeeID";
            getLogIn.Parameters.AddWithValue("@employeeID", currentEmployee);

            if (getLogIn.ExecuteScalar() == DBNull.Value)
            {
                DateTime lastLogIn = new DateTime();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sc;
                cmd.CommandText = "Select LastUpdated from AccountPassword where EmployeeID = @employee";
                cmd.Parameters.AddWithValue("@employee", currentEmployee);

                lastLogIn = Convert.ToDateTime(cmd.ExecuteScalar());
                HttpContext.Current.Session["LastLogIn"] = lastLogIn;

                SqlCommand updateLogIn2 = new SqlCommand();
                updateLogIn2.Connection = sc;
                updateLogIn2.CommandText = "Update AccountPassword set LastLogIn = @date where EmployeeID = @employee";
                updateLogIn2.Parameters.AddWithValue("@date", DateTime.Now);
                updateLogIn2.Parameters.AddWithValue("@employee", currentEmployee);
                updateLogIn2.ExecuteNonQuery();

                Response.Redirect("ChangePassword.aspx");


            }
            else
            {
                DateTime lastLogIn = new DateTime();
                lastLogIn = Convert.ToDateTime(getLogIn.ExecuteScalar());
                HttpContext.Current.Session["LastLogIn"] = lastLogIn;
            }


            SqlCommand updateLogIn = new SqlCommand();
            updateLogIn.Connection = sc;
            updateLogIn.CommandText = "Update AccountPassword set LastLogIn = @date where EmployeeID = @employee";
            updateLogIn.Parameters.AddWithValue("@date", DateTime.Now);
            updateLogIn.Parameters.AddWithValue("@employee", currentEmployee);
            updateLogIn.ExecuteNonQuery();

            Response.Redirect("RewardFeed.aspx");
        }

        else if (terminatedFlag == 1)
        {
            lblStatus.Text = "Your account has been terminated";
        }

        else
        {
            lblStatus.Text = "Incorrect username or password";
            lblStatus.ForeColor = System.Drawing.Color.Red;
        }


    }
}
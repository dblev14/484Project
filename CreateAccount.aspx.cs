using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.Configuration;
public partial class CreateAccount : System.Web.UI.Page
{
    int employeeID;
    String emailAddress;
    String password;
    String username;
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    //SqlConnection sc = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {




        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        if ((int)Session["AdminFlag"] == 0)
        {

            Response.Redirect("RewardFeed.aspx");
        }

        // This code queries the AdventureWorks database for a particular Person.Contact record
        // This is what it does:
        //   1) Retrieves and displays the person's name, PasswordHash, and PasswordSalt 
        //   2) Uses a hashing algorithm to create a new PasswordHash for the person
        //   3) Writes the new password hash out to the Contacts table


        // Establish a connection to the database server
        try
        {
            //sc.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;";
            sc.Open();

        }
        catch
        {

        }

        //// create a command and associate it with the connection
        //SqlCommand command = new SqlCommand();
        //command.Connection = sc;

        //// specify the query 
        //command.CommandText = "SELECT Employee.FirstName, Employee.LastName, AccountPassword.PasswordHash, AccountPassword.PasswordSalt FROM AccountPassword INNER JOIN Employee ON AccountPassword.EmployeeID = Employee.EmployeeID WHERE Employee.EmployeeID = " + employeeID;

        //// creating a data reader that contains the result set of the query
        //SqlDataReader reader = command.ExecuteReader();

        //if (reader.HasRows)
        //{

        //    // read the next row of the result set
        //    reader.Read();

        //    // get data from the columns of that row
        //    String fName = reader["FirstName"].ToString();
        //    String lName = reader["LastName"].ToString();
        //    //String email = reader["emailAddress"].ToString();

        //    String pwHash = reader["PasswordHash"].ToString();
        //    String pwSalt = reader["PasswordSalt"].ToString();

        //    // close the reader
        //    reader.Close();

        //    // display results of query...
        //    // Response.Write() is a useful way of just displaying stuff to the web page without using WebControls
        //    // ... it just takes whatever string you give it and writes it to the web page 
        //    Response.Write("<br>" + fName + " " + lName);
        //    Response.Write("<br>" + pwHash);
        //    Response.Write("<br>" + pwSalt);


        //    // now make a new password hash
        //    String password = fName;   // ...for this example, just using the first name as the password

        //    // using the SimpleHash class (which is stored in App_Code
        //    string passwordHashNew =
        //           SimpleHash.ComputeHash(password, "MD5", null);
        //    Response.Write("<br><br>New: " + passwordHashNew);

        //    // write the new hash to the database
        //    command.CommandText = "update AccountPassword set PasswordHash = '" + passwordHashNew + "' where EmployeeID = " + employeeID;
        //    command.ExecuteNonQuery();

        //}


    }




    public void clear()
    {
        txtFirstName.Text = null;
        txtLastName.Text = null;
        txtMiddleInitial.Text = null;

        txtUsername.Text = null;
        txtStartDate.Text = null;
        txtPosition.Text = null;
        txtEmail.Text = null;
        txtPhoneNumber.Text = null;

    }



    protected void btnCreateAccount_Click1(object sender, EventArgs e)
    {
        //String tempUsername;
        //bool usernameExists = false;

        //SqlCommand accountReadCmd = new SqlCommand("SELECT Username FROM AccountPassword", sc);

        //SqlDataReader accountReader = accountReadCmd.ExecuteReader();
        //while (accountReader.Read())
        //{
        //    tempUsername = accountReader[0].ToString();
        //    if (tempUsername== txtUsername.Text)
        //    {
        //        usernameExists = true;
        //        break;
        //    }   
        //}

        String error = "";




        int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
        String currentUser = "";
        using (sc)
        {
            //sc.Open();
            // select the project name that matches what the user puts in the search box
            String pullCurrentUser = "SELECT FirstName, LastName FROM Employee WHERE EmployeeID = " + currentID;
            using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc))
            {
                using (SqlDataReader reader = pullUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentUser = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                    }
                }
            }
        }

        Boolean employeeCreated = true;

        String firstName = txtFirstName.Text;
        String lastName = txtLastName.Text;
        String middleInitial = txtMiddleInitial.Text;
        int companyID = Convert.ToInt32(1); //fix this
        String position = txtPosition.Text;
        DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
        DateTime? terminationDate = null;
        username = txtUsername.Text;
        int adminFlag = 0;
        emailAddress = txtEmail.Text;
        String phoneNumber = txtPhoneNumber.Text;


        //Generate random password
        password = Membership.GeneratePassword(12, 2);


        if (chkAdminFlag.Checked)
        {
            adminFlag = 1;
        }
        else
        {
            adminFlag = 0;
        }

        Boolean run = true;
        //if ((!emailAddress.Contains("@")) || (!emailAddress.Contains(".")))
        //{
        //    error += "Email should contain an @ and domain (.edu, .com, etc.)";
        //    run = false;
        //}
        //else
        //{
        //    int i = emailAddress.IndexOf("@");
        //    String sub = emailAddress.Substring(i);
        //    if (!sub.Contains("."))
        //    {
        //        error+= "Email should contain a domain (.edu, .com, etc.)";
        //        run = false;
        //    }
        //}

        run = IsValidEmail(emailAddress);
        if(run == false)
        {
            error += "Email is invalid. ";
        }


        //Create Account object for new employee
        Account newAccount = new Account(firstName, lastName, middleInitial, companyID, position, startDate, terminationDate, adminFlag, username, phoneNumber, emailAddress);



        //verify username does not already exist
        //String usernameVerificationQuery = "Select Count(Username) FROM dbo.AccountPassword Where Username = @Username";



        // usernameExists = true;
        //if(startDate.AddMonths(6) > DateTime.Now)
        //{
        //    run = false;
        //    error += "Start date too far in advance. ";
        //}

        String tempName = "";
        using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
            String pullUsername = "SELECT Username FROM dbo.AccountPassword";
            using (SqlCommand pullVendor = new SqlCommand(pullUsername, sc))
            {
                
                using (SqlDataReader reader = pullVendor.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempName = reader["Username"].ToString();
                        if (tempName.ToUpper().Equals(username.ToUpper()))
                        {
                            run = false;
                            error += "Username already exists. ";
                        }
                        }
                }
            }
        }


        // in case it is the first time creating an account
        //sc.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;";
        //sc.Open();
        //SqlCommand usernameVerification2 = new SqlCommand(usernameVerificationQuery, sc);

        //SqlDataReader reader2 = usernameVerification2.ExecuteReader();
        if (run == true)
        {

            String passwordHashNew = SimpleHash.ComputeHash(password, "MD5", null);

            //Gets the most recent employeeID
            String selectEmployeeIDQuery = "SELECT MAX(EmployeeID) FROM Employee";

            //Inserts new employee
            String insertEmployeeQuery = "INSERT INTO [dbo].[Employee] values (@FirstName,@LastName, @MiddleInitial, @CompanyID, @Position, @Email, @PhoneNumber, @StartDate," +
                "@TerminationDate,@RewardBalance,@AdminFlag,@LUB,@LU,0)";

            //Inserts new employee's password
            String insertAccountPasswordQuery = "INSERT INTO AccountPassword values (@EmployeeID" + ", '" + newAccount.getUsername() + "'" + ", '" + passwordHashNew + "', 'Salt',@LUB,@LU, @LastLogIn)";


            //create new employee in database
            using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
            {
                if (sc.State == System.Data.ConnectionState.Closed)
                    sc.Open();
                SqlCommand insertEmployee = new SqlCommand(insertEmployeeQuery, sc);
                insertEmployee.Parameters.AddWithValue("@FirstName", newAccount.getFirstname());
                insertEmployee.Parameters.AddWithValue("@LastName", newAccount.getLastName());
                if (newAccount.getMiddleInitial() == "")
                    insertEmployee.Parameters.AddWithValue("@MiddleInitial", DBNull.Value);
                else
                    insertEmployee.Parameters.AddWithValue("@MiddleInitial", newAccount.getMiddleInitial());
                insertEmployee.Parameters.AddWithValue("@CompanyID", newAccount.getCompanyID());
                insertEmployee.Parameters.AddWithValue("@Position", newAccount.getPosition());
                insertEmployee.Parameters.AddWithValue("@Email", newAccount.getEmail());
                insertEmployee.Parameters.AddWithValue("@PhoneNumber", newAccount.getPhoneNumber());
                insertEmployee.Parameters.AddWithValue("@StartDate", newAccount.getStartDate());
                insertEmployee.Parameters.AddWithValue("@TerminationDate", DBNull.Value);
                insertEmployee.Parameters.AddWithValue("@RewardBalance", 0.0);
                insertEmployee.Parameters.AddWithValue("@AdminFlag", newAccount.getAdminFlag());
                insertEmployee.Parameters.AddWithValue("@LUB", currentUser);
                insertEmployee.Parameters.AddWithValue("@LU", DateTime.Now);

                try
                {
                    //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
                    if (sc.State == System.Data.ConnectionState.Closed)
                        sc.Open();
                    insertEmployee.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write("<br> insertEmployee " + ex.Message);
                    employeeCreated = false;
                }

                SqlCommand selectEmployeeID = new SqlCommand(selectEmployeeIDQuery, sc);
                try
                {
                    employeeID = (int)selectEmployeeID.ExecuteScalar(); //Returns the last employeeID
                }
                catch (Exception ex)
                {
                    Response.Write("<br> selectEmployeeID" + ex.Message);
                    employeeCreated = false;
                }

                SqlCommand insertAccountPassword = new SqlCommand(insertAccountPasswordQuery, sc);
                insertAccountPassword.Parameters.AddWithValue("@EmployeeID", employeeID);
                insertAccountPassword.Parameters.AddWithValue("@LUB", currentUser);
                insertAccountPassword.Parameters.AddWithValue("@LU", DateTime.Now);
                insertAccountPassword.Parameters.AddWithValue("@LastLogIn", DBNull.Value);
                try
                {
                    insertAccountPassword.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Write("<br> insertAccountPass" + ex.Message);
                    employeeCreated = false;
                }
            }
        }
        else
        {
            employeeCreated = false;
        }

        if (employeeCreated == true)
        {
            try
            {
                sendEmail();
                lblStatus.Text = "Employee created!";
                clear();
            }
            catch
            {
            }

        }
        else
        {
            lblStatus.Text ="Employee cannot be created. " + error;
        }

    }

    public void sendEmail()
    {

        String body = "Your company administrator has created a reward account for you! \nUsername: " + username + "\nTemporary Password: " + password;


        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(emailAddress);
        mail.From = new MailAddress("cis484test@gmail.com", "Automated Reward Notification", System.Text.Encoding.UTF8);
        mail.Subject = "Your account has been created!";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = body;
        mail.Priority = MailPriority.High;
        SmtpClient client = new SmtpClient();
        client.Credentials = new System.Net.NetworkCredential("cis484test@gmail.com", "484testingemail123");
        client.Port = 587;
        client.Host = "smtp.gmail.com";
        client.EnableSsl = true;
        try
        {
            client.Send(mail);
            //Page.RegisterStartupScript("UserMsg", "<script>alert('Successfully Send...');if(alert){ window.location='RewardFeed.aspx';}</script>");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
            //Page.RegisterStartupScript("UserMsg", "<script>alert('Sending Failed...');</script>");
        }
    }




    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminHome.aspx");
    }


    //https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
//using System;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;
//using Twilio.Types;
using System.Web.Configuration;

public partial class SendReward : System.Web.UI.Page
{
    SqlConnection sc;

    int rewardReceiverID;
    
    //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);

        txtTodaysDate.Text = DateTime.Now.ToShortDateString();
       // ok
        // verify that the user is logged in...if not redirect
        // the user to the login screen

        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");


        if ((int)Session["AdminFlag"] == 1)
        {

            Response.Redirect("RewardFeed.aspx");
        }


        try
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {

        }
       
        




        if (!IsPostBack)
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();

            //Read values to drop down menu
            dropValues.Items.Clear();
            System.Data.SqlClient.SqlCommand valuesInputCmd = new System.Data.SqlClient.SqlCommand();
            valuesInputCmd.Connection = sc;
            valuesInputCmd.CommandText = "SELECT valueID, valueName from CompanyValues";
            System.Data.SqlClient.SqlDataReader valueReader = valuesInputCmd.ExecuteReader();
            dropValues.Items.Add(new ListItem("Please select a value", "none"));
            while (valueReader.Read())
            {
                dropValues.Items.Add(new ListItem(valueReader[1].ToString(), valueReader[0].ToString()));
            }

            //Read Category to drop down menu
            dropCategory.Items.Clear();
            System.Data.SqlClient.SqlCommand categoryInputCmd = new System.Data.SqlClient.SqlCommand();
            categoryInputCmd.Connection = sc;
            categoryInputCmd.CommandText = "SELECT categoryID, categoryName from RewardCategory";
            System.Data.SqlClient.SqlDataReader categoryReader = categoryInputCmd.ExecuteReader();
            dropCategory.Items.Add(new ListItem("Please select a reward category", "none"));
            while (categoryReader.Read())
            {
                dropCategory.Items.Add(new ListItem(categoryReader[1].ToString(), categoryReader[0].ToString()));
            }

            //Set RewardAmount drop down
            dropRewardAmount.Items.Clear();
            dropRewardAmount.Items.Add(new ListItem("Please select a reward amount", "0"));
            dropRewardAmount.Items.Add(new ListItem("$10", "10"));
            dropRewardAmount.Items.Add(new ListItem("$25", "25"));
            dropRewardAmount.Items.Add(new ListItem("$50", "50"));



            //Read Employees to drop down
            dropEmployees.Items.Clear();
            System.Data.SqlClient.SqlCommand employeeSearchCmd = new System.Data.SqlClient.SqlCommand();
            employeeSearchCmd.Connection = sc;
            employeeSearchCmd.CommandText = "SELECT EmployeeID, FirstName, MiddleInitial, LastName, Position,AdminFlag FROM Employee WHERE CompanyID = " + Convert.ToInt32(Session["CompanyID"]);
            System.Data.SqlClient.SqlDataReader EmployeeReader = employeeSearchCmd.ExecuteReader();
            dropEmployees.Items.Add(new ListItem("Select Employee", "none"));
            while (EmployeeReader.Read())
            {
                if (Convert.ToInt32(EmployeeReader[0]) != Convert.ToInt32(Session["CurrentUserID"]) && Convert.ToInt32(EmployeeReader[5]) != 1)
                {
                    dropEmployees.Items.Add(new ListItem(EmployeeReader[1].ToString() + " " + EmployeeReader[2].ToString() + " " + EmployeeReader[3].ToString() + " (" + EmployeeReader[4].ToString() + ")", EmployeeReader[0].ToString()));
                }
               
            }



            sc.Close();

            //Set reward reciever if send reward page accessed from a user profile
           rewardReceiverID = Convert.ToInt32(Session["rewardReceiverID"]);

            //hide search functionality if reward page accessed from a user profile
            if (rewardReceiverID !=0)
            {
                dropEmployees.SelectedValue = rewardReceiverID.ToString();
            }
            

        }

      


    }




    protected void btnSendReward_Click1(object sender, EventArgs e)
    {
        SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        try
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            
            if (sc1.State == System.Data.ConnectionState.Closed)
            {
                sc1.Open();
            }
        }
        catch (Exception)
        {

        }

        SqlCommand getFund = new SqlCommand();
        getFund.Connection = sc1;
        getFund.CommandText = "Select RewardBalance from Fund where CompanyID = @company";
        getFund.Parameters.AddWithValue("@company", Convert.ToInt32(HttpContext.Current.Session["CompanyID"]));
        Double fund = Convert.ToDouble(getFund.ExecuteScalar());
        String testAmount = dropRewardAmount.SelectedItem.Text.ToString();
        testAmount = testAmount.Substring(1);
        Double testAmountDouble = Convert.ToDouble(testAmount);
        if(fund < testAmountDouble)
        {
            lblStatus.Text = "Not enough money in fund to reward selected amount";
        }
        else if (Convert.ToDateTime(txtDateOfDeed.Text) <= DateTime.Now)
        {



            //rewardReceiverID = Convert.ToInt32(Session["rewardReceiverID"]);

            if (rewardReceiverID == 0)
            {
                rewardReceiverID = Convert.ToInt32(dropEmployees.SelectedValue);
            }

            int rewardSenderID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentUserID"].ToString());

            int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
            String currentUser = "";
            String hireDate = "";
            using (sc1)
            {
                if (sc1.State == System.Data.ConnectionState.Closed)
                {
                    sc1.Open();
                }
                // select the project name that matches what the user puts in the search box
                String pullCurrentUser = "SELECT FirstName, LastName,StartDate FROM Employee WHERE EmployeeID = " + currentID;
                using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc1))
                {
                    using (SqlDataReader reader = pullUser.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currentUser = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                            hireDate = reader["StartDate"].ToString();
                        }
                    }
                }
            }

            if (Convert.ToDateTime(hireDate) <= Convert.ToDateTime(txtDateOfDeed.Text))
            {

                String sendRewardQuery = "INSERT INTO Reward VALUES (@RewardReason,@ValueID,@CategoryID,@RewardAmount,@RewardSender,@RewardReceiver,@SendDate,@DeedDate,@LUB,@LU)";

                using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    SqlCommand sendRewardCmd = new SqlCommand(sendRewardQuery, sc2);


                    sendRewardCmd.Parameters.AddWithValue("@RewardReason", txtRewardReason.Text);
                    sendRewardCmd.Parameters.AddWithValue("@ValueID", dropValues.SelectedValue);
                    sendRewardCmd.Parameters.AddWithValue("@CategoryID", dropCategory.SelectedValue);
                    sendRewardCmd.Parameters.AddWithValue("@RewardAmount", dropRewardAmount.SelectedValue);
                    sendRewardCmd.Parameters.AddWithValue("@RewardSender", rewardSenderID);
                    sendRewardCmd.Parameters.AddWithValue("@RewardReceiver", rewardReceiverID);
                    sendRewardCmd.Parameters.AddWithValue("@SendDate", DateTime.Now);
                    sendRewardCmd.Parameters.AddWithValue("@DeedDate", DateTime.Parse(txtDateOfDeed.Text));
                    sendRewardCmd.Parameters.AddWithValue("@LUB", currentUser);
                    sendRewardCmd.Parameters.AddWithValue("@LU", DateTime.Now);


                    try
                    {
                        if (sc2.State == System.Data.ConnectionState.Closed)
                            sc2.Open();
                     
                        sendRewardCmd.ExecuteNonQuery();
                        lblStatus.Text = "You have sent the reward";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        Session["RewardReceiverID"] = null;
                        

                    }

                    catch (Exception ex)
                    {
                        lblStatus.Text = ex.Message;
                        lblStatus.ForeColor = System.Drawing.Color.Red;
                    }
                    finally
                    {
                        sc2.Close();
                    }

                }
                
                String email = "";
                String pullReceiverEmail = "SELECT Email FROM Employee WHERE EmployeeID = " + rewardReceiverID;
                using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc2.State == System.Data.ConnectionState.Closed)
                        sc2.Open();
                    using (SqlCommand pullEmail = new SqlCommand(pullReceiverEmail, sc2))
                    {
                        using (SqlDataReader reader = pullEmail.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                email = (string)reader["Email"];
                            }
                        }
                    }
                }

                String senderName = "";
                String pullSenderName = "SELECT FirstName,LastName FROM Employee WHERE EmployeeID = " + rewardSenderID;
                using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc2.State == System.Data.ConnectionState.Closed)
                        sc2.Open();
                    using (SqlCommand pullSender = new SqlCommand(pullSenderName, sc2))
                    {
                        using (SqlDataReader reader = pullSender.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                senderName = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                            }
                            reader.Close();
                        }
                    }
                }

                String value = "";
                String pullValue = "SELECT ValueName FROM CompanyValues WHERE ValueID = " + dropValues.SelectedValue;
                using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc2.State == System.Data.ConnectionState.Closed)
                        sc2.Open();
                    using (SqlCommand pullVal = new SqlCommand(pullValue, sc2))
                    {
                        using (SqlDataReader reader = pullVal.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                value = (string)reader["ValueName"];
                            }
                            reader.Close();
                        }
                    }
                }

                String category = "";
                String pullCategory = "SELECT CategoryName FROM RewardCategory WHERE CategoryID = " + dropCategory.SelectedValue;
                using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc2.State == System.Data.ConnectionState.Closed)
                        sc2.Open();
                    using (SqlCommand pullCat = new SqlCommand(pullCategory, sc2))
                    {
                        using (SqlDataReader reader = pullCat.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                category = (string)reader["CategoryName"];
                            }
                            reader.Close();
                        }
                    }
                }

                decimal balance = 0;
                String pullBalance = "SELECT RewardBalance FROM Employee WHERE EmployeeID = " + rewardReceiverID;
                using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc2.State == System.Data.ConnectionState.Closed)
                        sc2.Open();
                    using (SqlCommand pullBal = new SqlCommand(pullBalance, sc2))
                    {
                        using (SqlDataReader reader = pullBal.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                balance = (decimal)reader["RewardBalance"];
                                balance = decimal.Round(balance, 2);
                            }
                            reader.Close();
                        }
                    }
                }

                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress("cis484test@gmail.com", "Automated Reward Notification", System.Text.Encoding.UTF8);
                mail.Subject = senderName + " has Rewarded You!";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = senderName + " has Rewarded You!\n\n" +
                    "Reward Value: " + value + "\n" +
                    "Reward Category: " + category + "\n" +
                    "Reward Reason: " + txtRewardReason.Text + "\n" +
                    "Reward Amount: $" + dropRewardAmount.SelectedValue +
                    "Your Current Balance: $" + balance;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("cis484test@gmail.com", "484testingemail123");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                try
                {
                    client.Send(mail);
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

                String number = "";
                String pullReceiverNumber = "SELECT PhoneNumber FROM Employee WHERE EmployeeID = " + rewardReceiverID;
                using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc2.State == System.Data.ConnectionState.Closed)
                        sc2.Open();
                    using (SqlCommand pullNumber = new SqlCommand(pullReceiverNumber, sc2))
                    {
                        using (SqlDataReader reader = pullNumber.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                number = (string)reader["PhoneNumber"];
                            }
                            reader.Close();
                        }
                    }
                }



                // Your Account SID from twilio.com/console
                var accountSid = "ACf491236fe8c39d4d1801cdd3de3788a4";
                // Your Auth Token from twilio.com/console
                var authToken = "f32769fd46b8388451c4c25dba81ae39";

                TwilioClient.Init(accountSid, authToken);

                try
                {
                    var message = MessageResource.Create(
                        to: new PhoneNumber("+1" + number),
                        from: new PhoneNumber("+12407700506"),
                        body: senderName + " has Rewarded you for " + txtRewardReason.Text.ToLower() + " for the amount of $" + dropRewardAmount.SelectedValue);
                }
                catch (Exception ex)
                {
                   lblStatus.Text = "Message has been Sent!";
                }


                clear();

            }
            else
            {
                lblStatus.Text = "Date of deed must be after the employee start date";
                lblStatus.ForeColor = lblStatus.ForeColor = System.Drawing.Color.Red;
            }





        }
        else
        {
            lblStatus.Text = "Date of deed cannot be in the future";
            lblStatus.ForeColor = lblStatus.ForeColor = System.Drawing.Color.Red;
        }


    }

    public void clear()
    {
        txtDateOfDeed.Text = "";
        txtRewardReason.Text = "";
        dropCategory.SelectedIndex = 0;
        dropRewardAmount.SelectedIndex = 0;
        dropValues.SelectedIndex = 0;
        dropEmployees.SelectedIndex = 0;
    }

   





}

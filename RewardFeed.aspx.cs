using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

public partial class RewardFeed : System.Web.UI.Page
{

    int selectedRewardID;
    int selectedRewardIndex;
    int loggedInUserID;

    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        // verify that the user is logged in...if not redirect
        // the user to the login screen

        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        loggedInUserID = Convert.ToInt32(Session["CurrentUserID"]);

        try
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            sc.Open();
            
        }
        catch (Exception)
        {
        }
   

        if (!IsPostBack)
        {
            drpLeaderFilter.Items.Add("Reward Count");
            drpLeaderFilter.Items.Add("Reward Amount");
            refreshLeaderboard();
            refreshRewardFeed();

            lstRewardFeed.SelectedIndex = 0;
            fullRewardInfo();
            btnLike.BackColor= btnLike.BackColor = System.Drawing.ColorTranslator.FromHtml("#2F496E");
            
            //sc.Open();
            String query = "Select Reward.RewardReceiver FROM Reward WHERE RewardID = " + lstRewardFeed.SelectedValue;
            SqlCommand cmd = new SqlCommand(query, sc);

            try
            {
                Session["LikeReceiver"] = cmd.ExecuteScalar();
            }
            catch
            {
                Session["LikeReceiver"] = null;
            }

            lblHeader.Text = "Welcome " + Session["CurrentUserFirstName"].ToString() + "!";
            
            
            
        }


        checkIfLiked();

        
        


    }



    protected void Timer1_Tick(Object sender, EventArgs e)
    {
        refreshRewardFeed();
        refreshLeaderboard();
    }

    public void refreshRewardFeed()
    {

        lstRewardFeed.Items.Clear();

        String rewardInformationQuery = "SELECT TOP 100 DATEPART(month, RewardSendDate) AS 'Month', " +
            "DATEPART(day, RewardSendDate) AS 'Day', " +
            "DATEPART(year, RewardSendDate) AS 'Year'," +
            "DATEPART(hour, RewardSendDate) AS 'Hour', " +
            "DATEPART(minute, RewardSendDate) AS 'Minute', " +
            "Employee.FirstName AS RFirstName, Employee.LastName AS RLastName, Reward.RewardReason, " +
            "Employee_1.FirstName AS SFirstName, Employee_1.LastName AS SLastName, " +
            "Reward.RewardID FROM Employee " +
            "INNER JOIN Reward ON Employee.EmployeeID = Reward.RewardReceiver INNER JOIN Employee AS Employee_1 ON Reward.RewardSender = Employee_1.EmployeeID ORDER BY Reward.RewardID desc";

        SqlCommand rewardReaderQuery = new SqlCommand(rewardInformationQuery, sc);

        if (sc.State == ConnectionState.Closed)
        {
            sc.Open();
        }


        SqlDataReader rewardReader = rewardReaderQuery.ExecuteReader();

        String date = "";
        int month = 0;
        int day = 0;
        int hour = 0;
        int minute = 0;
        bool afternoon = true;

        while (rewardReader.Read())
        {
            month = Convert.ToInt32(rewardReader["month"]);
            day = Convert.ToInt32(rewardReader["day"]);
            hour = Convert.ToInt32(rewardReader["hour"]);

            if (hour > 12)
            {
                hour = hour - 12;
                afternoon = true;
            }
            else
            {
                afternoon = false;
            }

            minute = Convert.ToInt32(rewardReader["minute"]);

            if (afternoon == true)
            {
                if (minute < 10)
                    date = month + "/" + day + " " + hour + ":0" + minute + " PM: ";
                else
                    date = month + "/" + day + " " + hour + ":" + minute + " PM: ";
            }
            else
            {
                if (minute < 10)
                    date = month + "/" + day + " " + hour + ":0" + minute + " AM: ";
                else
                    date = month + "/" + day + " " + hour + ":" + minute + " AM: ";
            }

            int rewardID = Convert.ToInt32(rewardReader["RewardID"]);
            String receiverFirstName = rewardReader["RFirstName"].ToString();
            String receiverLastName = rewardReader["RLastName"].ToString();
            String senderFirstName = rewardReader["SFirstName"].ToString();
            String senderLastName = rewardReader["SLastName"].ToString();
            String rewardReason = rewardReader["RewardReason"].ToString();
            String rewardIDString = rewardReader["RewardID"].ToString();

            String rewardText = date + senderFirstName + " " + senderLastName + " rewarded " + receiverFirstName + " " + receiverLastName + " for " + rewardReason + "!";

            lstRewardFeed.Items.Add(new ListItem(rewardText, rewardIDString));

        }

       

    }




    public void refreshLeaderboard()
    {
        lstLeaderBoard.Items.Clear();

        String leaderboardQuery;

        if (drpLeaderFilter.SelectedIndex == 0)
        {
            leaderboardQuery = "SELECT Employee.FirstName, Employee.MiddleInitial, Employee.LastName, Reward.RewardReceiver, Count(Reward.RewardID) as RewardCount, SUM(Reward.RewardAmount) as RewardAmount FROM Reward INNER JOIN Employee ON Reward.RewardReceiver = Employee.EmployeeID INNER JOIN Company ON Employee.CompanyID = Company.CompanyID GROUP BY Employee.FirstName, Employee.MiddleInitial, Employee.LastName, RewardReceiver ORDER BY Count(Reward.RewardID) desc";
        }
        else
        {
            leaderboardQuery = "SELECT Employee.FirstName, Employee.MiddleInitial, Employee.LastName, Reward.RewardReceiver, Count(Reward.RewardID) as RewardCount, SUM(Reward.RewardAmount) as RewardAmount FROM Reward INNER JOIN Employee ON Reward.RewardReceiver = Employee.EmployeeID INNER JOIN Company ON Employee.CompanyID = Company.CompanyID GROUP BY Employee.FirstName, Employee.MiddleInitial, Employee.LastName, RewardReceiver ORDER BY SUM(Reward.RewardAmount) desc";

        }




        SqlCommand LeaderboardReaderQuery = new SqlCommand(leaderboardQuery, sc);

        if (sc.State == ConnectionState.Closed)
        {
            sc.Open();
        }

        int leaderboardCounter = 1;

        SqlDataReader leaderboardReader = LeaderboardReaderQuery.ExecuteReader();

        while (leaderboardReader.Read())
        {

            String receiverFirstName = leaderboardReader[0].ToString();
            String receiverLastName = leaderboardReader[2].ToString();
            String employeeID = leaderboardReader[3].ToString();
            int leaderboardCount = Convert.ToInt32(leaderboardReader[4].ToString());
            double leaderboardAmount = Convert.ToDouble(leaderboardReader[5].ToString());

            String rewardsString;
            if (leaderboardCount > 1)
                rewardsString = " rewards";
            else
                rewardsString = " reward";

            String leaderboardText = leaderboardCounter + ". " + receiverFirstName + " " + receiverLastName + ": " + leaderboardCount + rewardsString + " totaling $" + leaderboardAmount;

            lstLeaderBoard.Items.Add(new ListItem(leaderboardText, employeeID));
            leaderboardCounter++;
        }

        //sc.Close();
        try
        {
            lstRewardFeed.SelectedIndex = 0; //incorrect fix this
        }
        catch
        {

        }



    }

    protected void drpLeaderFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        refreshLeaderboard();


    }


    protected void lstRewardFeed_SelectedIndexChanged(object sender, EventArgs e)
    {
        fullRewardInfo();
        checkIfLiked();
        
    }


    //Creates a display showing info for selected employee
    public void fullRewardInfo()
    {

        //String rewardInformationQuery = "SELECT Employee.EmployeeID, Employee.FirstName, Employee.LastName, Reward.RewardReason, Employee_1.FirstName AS Expr1, Employee_1.LastName AS Expr2, Reward.RewardID, CompanyValues.ValueName,  RewardCategory.CategoryName FROM Employee INNER JOIN Reward ON Employee.EmployeeID = Reward.RewardReceiver INNER JOIN Employee AS Employee_1 ON Reward.RewardSender = Employee_1.EmployeeID INNER JOIN CompanyValues ON Reward.ValueID = CompanyValues.ValueID INNER JOIN RewardCategory ON Reward.CategoryID = RewardCategory.CategoryID WHERE(Reward.RewardID = @RewardID)";
        String rewardInformationQuery = "SELECT  Employee.EmployeeID, Employee.FirstName, Employee.LastName, Reward.RewardReason, Employee_1.FirstName AS Expr1, Employee_1.LastName AS Expr2, Reward.RewardID, CompanyValues.ValueName, RewardCategory.CategoryName, EmployeeImage.Image, DATEPART(month, Reward.RewardSendDate) AS DateMonth,DATEPART(day, Reward.RewardSendDate) AS DateDay,DATEPART(year, Reward.RewardSendDate) AS DateYear FROM Employee INNER JOIN Reward ON Employee.EmployeeID = Reward.RewardReceiver INNER JOIN Employee AS Employee_1 ON Reward.RewardSender = Employee_1.EmployeeID INNER JOIN CompanyValues ON Reward.ValueID = CompanyValues.ValueID INNER JOIN RewardCategory ON Reward.CategoryID = RewardCategory.CategoryID FULL Join EmployeeImage ON Employee.EmployeeID = EmployeeImage.EmployeeID WHERE(Reward.RewardID = @RewardID)";


       SqlCommand rewardInformationCmd = new SqlCommand(rewardInformationQuery, sc);
        rewardInformationCmd.Parameters.AddWithValue("@RewardID", lstRewardFeed.SelectedValue);

        if (sc.State == ConnectionState.Closed)
        {
            sc.Open();
        }

        SqlDataReader rewardInformationReader = rewardInformationCmd.ExecuteReader();

        
        String receiverFirstName = "", receiverLastName = "", rewardReason = "", senderFirstName = "", senderLastName = "", companyValue = "", rewardCategory = "";
        byte[] imageBytes= { 0 };
        String strBase64Image;
        int receiverID = 0;

        String date = "";
        int dateMonth = 0;
        int dateDay = 0;
        int dateYear = 0;



        while (rewardInformationReader.Read())
        {
            
            try
            {
                imageBytes = (byte[])rewardInformationReader[9];
                strBase64Image = Convert.ToBase64String(imageBytes);
                String imageURL = "data:Image/png;base64," + strBase64Image;

                rewardImage.ImageUrl = imageURL;
                receiverID = Convert.ToInt32(rewardInformationReader[0]);
                receiverFirstName = rewardInformationReader[1].ToString();
                receiverLastName = rewardInformationReader[2].ToString();
                rewardReason = rewardInformationReader[3].ToString();
                senderFirstName = rewardInformationReader[4].ToString();
                senderLastName = rewardInformationReader[5].ToString();
                companyValue = rewardInformationReader[7].ToString();
                rewardCategory = rewardInformationReader[8].ToString();
                dateMonth = Convert.ToInt32(rewardInformationReader["DateMonth"]);
                dateDay = Convert.ToInt32(rewardInformationReader["DateDay"]);
                dateYear = Convert.ToInt32(rewardInformationReader["DateYear"]);
                date = dateMonth + "/" + dateDay + "/" + dateYear;


            }
            catch
            {
                rewardImage.ImageUrl = "Images/blank-face.jpg";
                receiverID = Convert.ToInt32(rewardInformationReader[0]);
                receiverFirstName = rewardInformationReader[1].ToString();
                receiverLastName = rewardInformationReader[2].ToString();
                rewardReason = rewardInformationReader[3].ToString();
                senderFirstName = rewardInformationReader[4].ToString();
                senderLastName = rewardInformationReader[5].ToString();
                companyValue = rewardInformationReader[7].ToString();
                rewardCategory = rewardInformationReader[8].ToString();
                dateMonth = Convert.ToInt32(rewardInformationReader["DateMonth"]);
                dateDay = Convert.ToInt32(rewardInformationReader["DateDay"]);
                dateYear = Convert.ToInt32(rewardInformationReader["DateYear"]);
                date = dateMonth + "/" + dateDay + "/" + dateYear;

            }

            finally
            {
                Session["likeReceiver"] = receiverID;
            }

            
            lblRewardReceiver.Text = "Receiver: " + receiverFirstName + " " + receiverLastName;
            lblRewardSender.Text = "Sender: " + senderFirstName + " " + senderLastName;
            lblRewardDate.Text = "Date: " + date;
            txtRewardReason.Text = rewardReason;
            lblCompanyValue.Text = "Value: " + companyValue;
            lblRewardCategory.Text = "Category: " + rewardCategory;


        }

        //sc.Close();



    }




    protected void btnLike_Click(object sender, EventArgs e)
    {
        int rewardID = Convert.ToInt32(lstRewardFeed.SelectedValue);
        int likeSender = (int) Session["CurrentUserID"];
        int likeReceiver = (int)Session["LikeReceiver"];
        DateTime dateOfLike = DateTime.Now;


        String insertLikeQuery = "INSERT INTO [LIKE] VALUES (" + likeSender + "," + likeReceiver + ",'" + dateOfLike + "'," + rewardID + ")";
        SqlCommand cmd = new SqlCommand(insertLikeQuery, sc);
        //sc.Open();
        cmd.ExecuteNonQuery();
        checkIfLiked();

    }

    protected void btnUnlike_Click(object sender, EventArgs e)
    {
        int rewardID = Convert.ToInt32(lstRewardFeed.SelectedValue);
        int likeSender = (int)Session["CurrentUserID"];
        int likeReceiver = (int)Session["LikeReceiver"];
        DateTime dateOfLike = DateTime.Now;

        String deleteLikeQuery = "DELETE FROM dbo.[Like] WHERE LikeSenderID = " + likeSender + " AND LikeReceiverID = " + likeReceiver;
        SqlCommand cmd = new SqlCommand(deleteLikeQuery, sc);
        cmd.ExecuteNonQuery();
        checkIfLiked();
    }


    public bool checkIfLiked()
    {
        Boolean liked = false;
        int tempRewardID;
        

        String checkLikes = "Select [RewardID] FROM [Like] WHERE [LikeSenderID] = " + Convert.ToInt32(Session["CurrentUserID"]);
        SqlCommand checkLikesCmd = new SqlCommand(checkLikes, sc);
        //sc.Open();
        SqlDataReader likeReader = checkLikesCmd.ExecuteReader();
        while (likeReader.Read())
        {
            tempRewardID = Convert.ToInt32(likeReader[0]);
            if (tempRewardID == Convert.ToInt32(lstRewardFeed.SelectedValue))
            {
                btnLike.Enabled = false;
                liked = true;
                btnUnlike.Visible = true;
                btnLike.Visible = false;
                break;
            }
            else
            {
                btnLike.Visible = true;
                btnUnlike.Visible = false;
                liked = false;
                btnLike.BackColor = System.Drawing.ColorTranslator.FromHtml("#2F496E");
                btnLike.Enabled = true;
                btnLike.Text = "like";
            }

        }


        return liked;
    }


}


    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class RewardInfoControl : System.Web.UI.UserControl
{
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");




        try
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {

        }

        fullRewardInfo(rewardID);

    }

    public void fullRewardInfo(int rewardID)
    {

        //String rewardInformationQuery = "SELECT Employee.EmployeeID, Employee.FirstName, Employee.LastName, Reward.RewardReason, Employee_1.FirstName AS Expr1, Employee_1.LastName AS Expr2, Reward.RewardID, CompanyValues.ValueName,  RewardCategory.CategoryName FROM Employee INNER JOIN Reward ON Employee.EmployeeID = Reward.RewardReceiver INNER JOIN Employee AS Employee_1 ON Reward.RewardSender = Employee_1.EmployeeID INNER JOIN CompanyValues ON Reward.ValueID = CompanyValues.ValueID INNER JOIN RewardCategory ON Reward.CategoryID = RewardCategory.CategoryID WHERE(Reward.RewardID = @RewardID)";
        String rewardInformationQuery = "SELECT Employee.EmployeeID, Employee.FirstName, Employee.LastName, " +
            "Reward.RewardReason, Employee_1.FirstName AS Expr1, Employee_1.LastName AS Expr2, Reward.RewardID, CompanyValues.ValueName, " +
            "RewardCategory.CategoryName, EmployeeImage.Image, DATEPART(month, Reward.RewardSendDate) AS DateMonth, " +
            "DATEPART(day, Reward.RewardSendDate) AS DateDay, DATEPART(year, Reward.RewardSendDate) AS DateYear " +
            "FROM Employee INNER JOIN Reward ON Employee.EmployeeID = Reward.RewardReceiver INNER JOIN Employee AS Employee_1 " +
            "ON Reward.RewardSender = Employee_1.EmployeeID INNER JOIN CompanyValues ON Reward.ValueID = CompanyValues.ValueID " +
            "INNER JOIN RewardCategory ON Reward.CategoryID = RewardCategory.CategoryID " +
            "FULL OUTER JOIN EmployeeImage ON Employee.EmployeeID = EmployeeImage.EmployeeID WHERE(Reward.RewardID = @RewardID)";


        SqlCommand rewardInformationCmd = new SqlCommand(rewardInformationQuery, sc);
        rewardInformationCmd.Parameters.AddWithValue("@RewardID", rewardID);

        if (sc.State == ConnectionState.Closed)
        {
            sc.Open();
        }

        SqlDataReader rewardInformationReader = rewardInformationCmd.ExecuteReader();

        int receiverID;
        String receiverFirstName = "", receiverLastName = "", rewardReason = "", senderFirstName = "", senderLastName = "", companyValue = "", rewardCategory = "";
        byte[] imageBytes = { 0 };
        String strBase64Image;

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
                


            }
            catch
            {
                rewardImage.ImageUrl = "Images/blank-face.jpg";

            }

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



            lblRewardReceiver.Text = "Receiver: " + receiverFirstName + " " + receiverLastName;
            lblRewardSender.Text = "Sender: " + senderFirstName + " " + senderLastName;
            lblRewardDate.Text = "Date: " + date;
            txtRewardReason.Text = rewardReason;
            lblCompanyValue.Text = "Value: " + companyValue;
            lblRewardCategory.Text = "Category: " + rewardCategory;


        }

        sc.Close();



    }

    public int rewardID;

    public int getRewardID()
    {
        return rewardID;
    }

    public void setRewardID(int rewardIDInput)
    {
        rewardID = rewardIDInput;
    }






}
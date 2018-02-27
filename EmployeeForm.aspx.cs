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

public partial class EmployeeForm : System.Web.UI.Page
{
    //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    int currentEmployeeID = 0;
    int viewedEmployeeID = 0;
    int counter = 0;
    int nbrOfNotifications = 0;
    List<String> senders = new List<String>();
    List<String> amounts = new List<String>();
    List<String> reasons = new List<String>();
    List<DateTime> dates = new List<DateTime>();
    List<String> images = new List<String>();


    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        if (!IsPostBack)
        {
            HttpContext.Current.Session["notCounter"] = 0;
        }

        // the current user's employeeID
        currentEmployeeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentUserID"].ToString());

        //the employee form being  viewed
        try
        {
            viewedEmployeeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["SearchedEmployeeID"].ToString());
            Session["SearchedEmployeeID"] = null;
        }
        catch (NullReferenceException)
        {
            viewedEmployeeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentUserID"].ToString());
        }

        if (currentEmployeeID ==viewedEmployeeID)
        {
            btnSendReward.Visible = false;
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
       
        SqlCommand selectStuff = new SqlCommand();
        selectStuff.Connection = sc;
        selectStuff.CommandText = "Select count(rewardID) from Reward where RewardReceiver = @employee and RewardSendDate > @date ";
        selectStuff.Parameters.AddWithValue("@employee", currentEmployeeID);
        selectStuff.Parameters.AddWithValue("@date", HttpContext.Current.Session["LastLogIn"]);
        HttpContext.Current.Session["nbrOfStuff"] = 0;
        HttpContext.Current.Session["nbrOfStuff"] = Convert.ToInt32(selectStuff.ExecuteScalar());
        sc.Close();

        selectStuff.CommandText = "Select Employee.FirstName + ' ' + Employee.LastName, Reward.RewardAmount, Reward.RewardReason, Reward.RewardSendDate, EmployeeImage.Image from Reward INNER JOIN Employee ON Reward.RewardSender = Employee.EmployeeID LEFT JOIN EmployeeImage ON Reward.RewardSender = EmployeeImage.EmployeeID where Reward.RewardReceiver = @employee and RewardSendDate > @date Order By Reward.RewardSendDate";
        sc.Open();


        int adminFlag = 1;
        String adminSearchQuery = "SELECT AdminFlag FROM EMPLOYEE WHERE EmployeeID = " + viewedEmployeeID;

 
        SqlCommand adminSearch = new SqlCommand(adminSearchQuery, sc);
        adminSearch.CommandText = adminSearchQuery;

        SqlDataReader adminReader = adminSearch.ExecuteReader();
        while (adminReader.Read())
        {
            adminFlag = Convert.ToInt32(adminReader[0].ToString());
            
        }

        if (adminFlag == 1)
            btnSendReward.Visible = false;




        SqlDataReader readStuff = selectStuff.ExecuteReader();
        while (readStuff.Read())
        {
            senders.Add(Convert.ToString(readStuff[0]));
            amounts.Add(Convert.ToString(readStuff[1]));
            reasons.Add(Convert.ToString(readStuff[2]));
            dates.Add(Convert.ToDateTime(readStuff[3]));
            if (Convert.ToString(readStuff[4]) == "")
            {
                images.Add("Images/no-image.jpg");
            }
            else
            {
                byte[] bytes = (byte[])readStuff[4];
                String test = Convert.ToBase64String(bytes);
                images.Add("data:Image/png;base64," + test);
            }
            
        }
        sc.Close();

        if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) == 0)
        {
            fill0();
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) == 1)
        {
            fill1();
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[0]).Substring(0, 2);
            lblActualDate1.Text = (dates[0]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[0]);
            lblActualRewardSender1.Text = Convert.ToString(senders[0]);
            notification1Image.ImageUrl = Convert.ToString(images[0]);
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) == 2)
        {
            fill2();
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[0]).Substring(0, 2);
            lblActualDate1.Text = (dates[0]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[0]);
            lblActualRewardSender1.Text = Convert.ToString(senders[0]);
            lblActualAmount2.Text = "$" + Convert.ToString(amounts[1]).Substring(0, 2);
            lblActualDate2.Text = (dates[1]).ToShortDateString();
            txtActualReason2.Text = Convert.ToString(reasons[1]);
            lblActualRewardSender2.Text = Convert.ToString(senders[1]);
            notification1Image.ImageUrl = Convert.ToString(images[0]);
            notification2Image.ImageUrl = Convert.ToString(images[1]);
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) == 3)
        {
            fill3();
            btnMoreNotifications.Visible = false;
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[0]).Substring(0,2);
            lblActualDate1.Text = (dates[0]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[0]);
            lblActualRewardSender1.Text = Convert.ToString(senders[0]);
            lblActualAmount2.Text = "$" + Convert.ToString(amounts[1]).Substring(0, 2);
            lblActualDate2.Text = (dates[1]).ToShortDateString();
            txtActualReason2.Text = Convert.ToString(reasons[1]);
            lblActualRewardSender2.Text = Convert.ToString(senders[1]);
            lblActualAmount3.Text = "$" + Convert.ToString(amounts[2]).Substring(0, 2);
            lblActualDate3.Text = (dates[2]).ToShortDateString();
            txtActualReason3.Text = Convert.ToString(reasons[2]);
            lblActualRewardSender3.Text = Convert.ToString(senders[2]);
            notification1Image.ImageUrl = Convert.ToString(images[0]);
            notification2Image.ImageUrl = Convert.ToString(images[1]);
            notification3Image.ImageUrl = Convert.ToString(images[2]);
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) > 3)
        {
            fill3();
            btnMoreNotifications.Visible = true;
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[0]).Substring(0, 2);
            lblActualDate1.Text = (dates[0]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[0]);
            lblActualRewardSender1.Text = Convert.ToString(senders[0]);
            lblActualAmount2.Text = "$" + Convert.ToString(amounts[1]).Substring(0, 2);
            lblActualDate2.Text = (dates[1]).ToShortDateString();
            txtActualReason2.Text = Convert.ToString(reasons[1]);
            lblActualRewardSender2.Text = Convert.ToString(senders[1]);
            lblActualAmount3.Text = "$" + Convert.ToString(amounts[2]).Substring(0, 2);
            lblActualDate3.Text = (dates[2]).ToShortDateString();
            txtActualReason3.Text = Convert.ToString(reasons[2]);
            lblActualRewardSender3.Text = Convert.ToString(senders[2]);
            notification1Image.ImageUrl = Convert.ToString(images[0]);
            notification2Image.ImageUrl = Convert.ToString(images[1]);
            notification3Image.ImageUrl = Convert.ToString(images[2]);
        }




        RewardInfoControl.Visible = false;
        RewardInfoControl1.Visible = false;
        RewardInfoControl2.Visible = false;
        RewardInfoControl3.Visible = false;
        RewardInfoControl4.Visible = false;
        RewardInfoControl5.Visible = false;


        
            SqlCommand select = new SqlCommand();
            sc.Open();
            select.Connection = sc;
            select.CommandText = "select FirstName from dbo.Employee where EmployeeID = @employeeID";
            select.Parameters.AddWithValue("@employeeID", viewedEmployeeID);
            String actualFirstName = select.ExecuteScalar().ToString();
            select.CommandText = "select FirstName + ' ' + IsNull(MiddleInitial, '') + ' ' + LastName from dbo.Employee where EmployeeID = @employeeID";
            String firstName = Convert.ToString(select.ExecuteScalar());
            lblFullName.Text = firstName;
            select.CommandText = "select Position from dbo.Employee where EmployeeID = @employeeID";
            String position = Convert.ToString(select.ExecuteScalar());
            lblJobTitle.Text = "Position: " + position;
            select.CommandText = "select StartDate from dbo.Employee where EmployeeID = @employeeID";
            DateTime startDate = Convert.ToDateTime(select.ExecuteScalar());
            lblStartDate.Text = "Start Date: " + startDate.ToShortDateString();
            select.CommandText = "select RewardBalance from dbo.Employee where EmployeeID = @employeeID";
            String rewardBalance = Convert.ToString(select.ExecuteScalar());
            rewardBalance = rewardBalance.Remove(rewardBalance.Length - 2);
            lblRewardBalance.Text = "Reward Balance: $" + rewardBalance.ToString();
            select.CommandText = "select Count(Reward.RewardReason) from dbo.Reward where RewardSender = @employeeID";


        String rewardsGiven="0", rewardsReceived = "0", giftsBought = "0", commonCategory = "n/a", biggestFan = "n/a";


        
            
        try
        {
            rewardsGiven = Convert.ToString(select.ExecuteScalar());      
            select.CommandText = "select Count(Reward.RewardReason) from dbo.Reward where RewardReceiver = @employeeID";
            rewardsReceived = Convert.ToString(select.ExecuteScalar());  
            select.CommandText = "select Count(EmployeeGift.TransactionID) from dbo.EmployeeGift where EmployeeID = @employeeID";
            giftsBought = Convert.ToString(select.ExecuteScalar());
            select.CommandText = "SELECT TOP 1 RewardCategory.CategoryName, COUNT(Reward.CategoryID) FROM RewardCategory INNER JOIN Reward ON RewardCategory.CategoryID = Reward.CategoryID WHERE RewardReceiver = @EmployeeID GROUP BY RewardCategory.CategoryName ORDER BY COUNT(Reward.CategoryID) desc";
            commonCategory = select.ExecuteScalar().ToString();
            select.CommandText = "SELECT CONCAT(Employee_1.FirstName,' ', Employee_1.LastName), COUNT(Employee_1.EmployeeID) FROM Employee INNER JOIN Reward ON Employee.EmployeeID = Reward.RewardReceiver INNER JOIN Employee AS Employee_1 ON Reward.RewardSender = Employee_1.EmployeeID WHERE Employee.EmployeeID = @EmployeeID GROUP BY Employee_1.FirstName, Employee_1.LastName ORDER BY COUNT(Employee_1.EmployeeID) desc";
            biggestFan = select.ExecuteScalar().ToString();
        }
        catch (System.NullReferenceException)
        {
            btnMoreTransactions.Visible = false;
        }
        


        


        lblRewardsSentNbr.Text = rewardsGiven;
        lblRewardsReceivedNbr.Text = rewardsReceived;
        lblCommonCategoryString.Text = commonCategory;
        lblBiggestFanString.Text = biggestFan;


        getProfilePic();

        btnSendReward.Text = "Send " + actualFirstName + " a reward";
        lblRecentTransactions.Text = actualFirstName + "'s Recent Transactions";


        //Find top 6 rewards involving the selected user
        String getRewardIDQuery = "SELECT TOP 6 RewardID, RewardSender, RewardReceiver FROM Reward WHERE RewardSender = @EmployeeID OR RewardReceiver = @EmployeeID";

        
        SqlCommand rewardIDReaderCmd = new SqlCommand(getRewardIDQuery, sc);
        rewardIDReaderCmd.Parameters.AddWithValue("@EmployeeID", viewedEmployeeID);

        if (sc.State == ConnectionState.Closed)
        {
            sc.Open();
        }


        SqlDataReader rewardIDReader = rewardIDReaderCmd.ExecuteReader();

        
        ArrayList rewardIDArrayList = new ArrayList();

       
        while (rewardIDReader.Read())
        {
          
            rewardIDArrayList.Add(Convert.ToInt32(rewardIDReader[0]));
            
            
        }

        try
        {
            //set rewardID properties of rewardInfoControl
            int rewardID1 = Convert.ToInt32(rewardIDArrayList[0]);
            RewardInfoControl.setRewardID(rewardID1);
            RewardInfoControl.Visible = true;
            int rewardID2 = Convert.ToInt32(rewardIDArrayList[1]);
            RewardInfoControl1.setRewardID(rewardID2);
            RewardInfoControl1.Visible = true;
            int rewardID3 = Convert.ToInt32(rewardIDArrayList[2]);
            RewardInfoControl2.setRewardID(rewardID3);
            RewardInfoControl2.Visible = true;
            int rewardID4 = Convert.ToInt32(rewardIDArrayList[3]);
            RewardInfoControl3.setRewardID(rewardID4);
            RewardInfoControl3.Visible = true;
            int rewardID5 = Convert.ToInt32(rewardIDArrayList[4]);
            RewardInfoControl4.setRewardID(rewardID5);
            RewardInfoControl4.Visible = true;
            int rewardID6 = Convert.ToInt32(rewardIDArrayList[5]);
            RewardInfoControl5.setRewardID(rewardID6);
            RewardInfoControl5.Visible = true;
        }
        catch (System.ArgumentOutOfRangeException)
        {
            
        }
        
        
        sc.Close();


    }

    public void getProfilePic()
    {
        String getEmployeeImageQuery = "SELECT Image FROM EmployeeImage WHERE EmployeeID = @EmployeeID";

        using (SqlConnection conn = new SqlConnection(sc.ConnectionString))
        {
            SqlCommand getEmployeeImageCmd = new SqlCommand(getEmployeeImageQuery, conn);
            getEmployeeImageCmd.Parameters.AddWithValue("@EmployeeID", viewedEmployeeID);

            try
            {

                conn.Open();
                byte[] bytes = (byte[])getEmployeeImageCmd.ExecuteScalar();
                String strBase64 = Convert.ToBase64String(bytes);
                profileImage.ImageUrl = "data:Image/png;base64," + strBase64;

            }

            catch (Exception ex)
            {
                profileImage.ImageUrl = "Images/blank-face.jpg";
            }

        }

    }


    protected void btnSendReward_Click1(object sender, EventArgs e)
    {
        Session["RewardReceiverID"] = viewedEmployeeID;
        Response.Redirect("SendReward.aspx");

    }

    //protected void btnImageUpload_Click(object sender, EventArgs e)
    //{
    //    HttpPostedFile postedFile = fileImageUpload.PostedFile;
    //    String fileName = Path.GetFileName(postedFile.FileName);
    //    String fileExtension = Path.GetExtension(fileName);
    //    int fileSize = postedFile.ContentLength;


    //    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
    //    {
    //        Stream stream = postedFile.InputStream;
    //        BinaryReader binaryReader = new BinaryReader(stream);
    //        byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

    //        String employeeImageUploadQuery = "INSERT INTO EmployeeImage values (@EmployeeID,@Image)";

    //        using (SqlConnection conn = new SqlConnection(sc.ConnectionString))
    //        {
    //            SqlCommand employeeImageUploadCmd = new SqlCommand(employeeImageUploadQuery, conn);
    //            employeeImageUploadCmd.Parameters.AddWithValue("@EmployeeID", currentEmployeeID);
    //            employeeImageUploadCmd.Parameters.AddWithValue("@Image", bytes);

    //            try
    //            {
    //                conn.Open();
    //                employeeImageUploadCmd.ExecuteNonQuery();
    //                getProfilePic();
    //            }


    //            catch (System.Data.SqlClient.SqlException)
    //            {

    //                try
    //                {
    //                    employeeImageUploadQuery = "UPDATE EmployeeImage SET Image = @bytes";
    //                    employeeImageUploadCmd = new SqlCommand(employeeImageUploadQuery, conn);

    //                    employeeImageUploadCmd.Parameters.AddWithValue("@EmployeeID", currentEmployeeID);
    //                    employeeImageUploadCmd.Parameters.AddWithValue("@bytes", bytes);
    //                    employeeImageUploadCmd.ExecuteNonQuery();
    //                    getProfilePic();
    //                }
    //                catch (Exception ex)
    //                {
    //                    lblStatus.Text = ex.Message;
    //                }

    //            }


    //        }

    //    }
    //}

    protected void btnMoreTransactions_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeTransactions.aspx");

    }
    protected void fill0()
    {
        btnMoreNotifications.Visible = false;
        lblNotifications.Visible = false;
        lblActualAmount1.Visible = false;
        lblActualAmount2.Visible = false;
        lblActualAmount3.Visible = false;
        lblActualDate1.Visible = false;
        lblActualDate2.Visible = false;
        lblActualDate3.Visible = false;
        txtActualReason1.Visible = false;
        txtActualReason2.Visible = false;
        txtActualReason3.Visible = false;
        lblActualRewardSender1.Visible = false;
        lblActualRewardSender2.Visible = false;
        lblActualRewardSender3.Visible = false;
        lblAmount1.Visible = false;
        lblAmount2.Visible = false;
        lblAmount3.Visible = false;
        lblDate.Visible = false;
        lblDate2.Visible = false;
        lblDate3.Visible = false;
        lblReason1.Visible = false;
        lblReason2.Visible = false;
        lblReason3.Visible = false;
        lblRewardSender1.Visible = false;
        lblRewardSender2.Visible = false;
        lblRewardSender3.Visible = false;
        notification1Image.Visible = false;
        notification2Image.Visible = false;
        notification3Image.Visible = false;
    }
    protected void fill1()
    {
        btnMoreNotifications.Visible = false;
        lblNotifications.Visible = true;
        lblActualAmount1.Visible = true;
        lblActualAmount2.Visible = false;
        lblActualAmount3.Visible = false;
        lblActualDate1.Visible = true;
        lblActualDate2.Visible = false;
        lblActualDate3.Visible = false;
        txtActualReason1.Visible = true;
        txtActualReason2.Visible = false;
        txtActualReason3.Visible = false;
        lblActualRewardSender1.Visible = true;
        lblActualRewardSender2.Visible = false;
        lblActualRewardSender3.Visible = false;
        lblAmount1.Visible = true;
        lblAmount2.Visible = false;
        lblAmount3.Visible = false;
        lblDate.Visible = true;
        lblDate2.Visible = false;
        lblDate3.Visible = false;
        lblReason1.Visible = true;
        lblReason2.Visible = false;
        lblReason3.Visible = false;
        lblRewardSender1.Visible = true;
        lblRewardSender2.Visible = false;
        lblRewardSender3.Visible = false;
        notification1Image.Visible = true;
        notification2Image.Visible = false;
        notification3Image.Visible = false;
    }
    protected void fill2()
    {
        btnMoreNotifications.Visible = false;
        lblNotifications.Visible = true;
        lblActualAmount1.Visible = true;
        lblActualAmount2.Visible = true;
        lblActualAmount3.Visible = false;
        lblActualDate1.Visible = true;
        lblActualDate2.Visible = true;
        lblActualDate3.Visible = false;
        txtActualReason1.Visible = true;
        txtActualReason2.Visible = true;
        txtActualReason3.Visible = false;
        lblActualRewardSender1.Visible = true;
        lblActualRewardSender2.Visible = true;
        lblActualRewardSender3.Visible = false;
        lblAmount1.Visible = true;
        lblAmount2.Visible = true;
        lblAmount3.Visible = false;
        lblDate.Visible = true;
        lblDate2.Visible = true;
        lblDate3.Visible = false;
        lblReason1.Visible = true;
        lblReason2.Visible = true;
        lblReason3.Visible = false;
        lblRewardSender1.Visible = true;
        lblRewardSender2.Visible = true;
        lblRewardSender3.Visible = false;
        notification1Image.Visible = true;
        notification2Image.Visible = true;
        notification3Image.Visible = false;
    }
    protected void fill3()
    {
        btnMoreNotifications.Visible = false;
        lblNotifications.Visible = true;
        lblActualAmount1.Visible = true;
        lblActualAmount2.Visible = true;
        lblActualAmount3.Visible = true;
        lblActualDate1.Visible = true;
        lblActualDate2.Visible = true;
        lblActualDate3.Visible = true;
        txtActualReason1.Visible = true;
        txtActualReason2.Visible = true;
        txtActualReason3.Visible = true;
        lblActualRewardSender1.Visible = true;
        lblActualRewardSender2.Visible = true;
        lblActualRewardSender3.Visible = true;
        lblAmount1.Visible = true;
        lblAmount2.Visible = true;
        lblAmount3.Visible = true;
        lblDate.Visible = true;
        lblDate2.Visible = true;
        lblDate3.Visible = true;
        lblReason1.Visible = true;
        lblReason2.Visible = true;
        lblReason3.Visible = true;
        lblRewardSender1.Visible = true;
        lblRewardSender2.Visible = true;
        lblRewardSender3.Visible = true;
        notification1Image.Visible = true;
        notification2Image.Visible = true;
        notification3Image.Visible = true;
    }

    protected void btnMoreNotifications_Click(object sender, EventArgs e)
    {
        counter = Convert.ToInt32(HttpContext.Current.Session["notCounter"]);
        counter += 3;
        HttpContext.Current.Session["notCounter"] = counter;
        if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) - counter == 3)
        {
            fill3();
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[counter]).Substring(0, 2); ;
            lblActualDate1.Text = (dates[counter]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[counter]);
            lblActualRewardSender1.Text = Convert.ToString(senders[counter]);
            lblActualAmount2.Text = "$" + Convert.ToString(amounts[counter+1]).Substring(0, 2);
            lblActualDate2.Text = (dates[counter+1]).ToShortDateString();
            txtActualReason2.Text = Convert.ToString(reasons[counter+1]);
            lblActualRewardSender2.Text = Convert.ToString(senders[counter+1]);
            lblActualAmount3.Text = "$" + Convert.ToString(amounts[counter+2]).Substring(0, 2);
            lblActualDate3.Text = (dates[counter+2]).ToShortDateString();
            txtActualReason3.Text = Convert.ToString(reasons[counter+2]);
            lblActualRewardSender3.Text = Convert.ToString(senders[counter+2]);
            notification1Image.ImageUrl = Convert.ToString(images[counter]);
            notification2Image.ImageUrl = Convert.ToString(images[counter+1]);
            notification3Image.ImageUrl = Convert.ToString(images[counter+2]);
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) - counter > 3)
        {
            fill3();
            btnMoreNotifications.Visible = true;
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[counter]).Substring(0, 2);
            lblActualDate1.Text = (dates[counter]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[counter]);
            lblActualRewardSender1.Text = Convert.ToString(senders[counter]);
            lblActualAmount2.Text = "$" + Convert.ToString(amounts[counter+1]).Substring(0, 2);
            lblActualDate2.Text = (dates[counter+1]).ToShortDateString();
            txtActualReason2.Text = Convert.ToString(reasons[counter + 1]);
            lblActualRewardSender2.Text = Convert.ToString(senders[counter + 1]);
            lblActualAmount3.Text = "$" + Convert.ToString(amounts[counter+2]).Substring(0, 2);
            lblActualDate3.Text = (dates[counter+2]).ToShortDateString();
            txtActualReason3.Text = Convert.ToString(reasons[counter + 2]);
            lblActualRewardSender3.Text = Convert.ToString(senders[counter + 2]);
            notification1Image.ImageUrl = Convert.ToString(images[counter]);
            notification2Image.ImageUrl = Convert.ToString(images[counter+1]);
            notification3Image.ImageUrl = Convert.ToString(images[counter+2]);
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) - counter == 2)
        {
            fill2();
            btnMoreNotifications.Visible = false;
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[counter]).Substring(0, 2);
            lblActualDate1.Text = (dates[counter]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[counter]);
            lblActualRewardSender1.Text = Convert.ToString(senders[counter]);
            lblActualAmount2.Text = "$" + Convert.ToString(amounts[counter+1]).Substring(0, 2);
            lblActualDate2.Text = (dates[counter+1]).ToShortDateString();
            txtActualReason2.Text = Convert.ToString(reasons[counter + 1]);
            lblActualRewardSender2.Text = Convert.ToString(senders[counter + 1]);
            notification1Image.ImageUrl = Convert.ToString(images[counter]);
            notification2Image.ImageUrl = Convert.ToString(images[counter+1]);
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["nbrOfStuff"]) - counter == 1)
        {
            fill1();
            btnMoreNotifications.Visible = false;
            lblActualAmount1.Text = "$" + Convert.ToString(amounts[counter]).Substring(0, 2);
            lblActualDate1.Text = (dates[counter]).ToShortDateString();
            txtActualReason1.Text = Convert.ToString(reasons[counter]);
            lblActualRewardSender1.Text = Convert.ToString(senders[counter]);
            notification1Image.ImageUrl = Convert.ToString(images[counter]);
        }
    }
}
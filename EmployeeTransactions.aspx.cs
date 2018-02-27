using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;

public partial class EmployeeTransactions : System.Web.UI.Page
{

    //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    int viewedEmployeeID;
    

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

        //the employee form being 
        try
        {
            viewedEmployeeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["SearchedEmployeeID"].ToString());
        }
        catch (NullReferenceException)
        {
            viewedEmployeeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentUserID"].ToString());
        }

        if (sc.State == ConnectionState.Closed)
            sc.Open();

        String allTransactionsQuery = "SELECT Concat(Employee.FirstName,' ', Employee.MiddleInitial,' ',Employee.LastName) as 'Sender', FORMAT(Reward.RewardAmount, 'C', 'en-us') as 'Reward Amount', RewardCategory.CategoryName as 'Category Name', CompanyValues.ValueName as 'Value Name', CONCAT(Employee_1.FirstName,' ',Employee_1.MiddleInitial,' ',Employee_1.LastName) AS Receiver" +
        " FROM Reward INNER JOIN" +
                         " Employee ON Reward.RewardSender = Employee.EmployeeID INNER JOIN" +
                         " Employee AS Employee_1 ON Reward.RewardReceiver = Employee_1.EmployeeID INNER JOIN" +
                         " CompanyValues ON Reward.ValueID = CompanyValues.ValueID INNER JOIN" +
                         " RewardCategory ON Reward.CategoryID = RewardCategory.CategoryID" +
                         " Where Reward.RewardReceiver = @EmployeeID OR Reward.RewardSender = @EmployeeID"; 




        if (sc.State == ConnectionState.Closed)
        {
            sc.Open();
        }

        DataTable TransactionData = new DataTable();
        SqlCommand gridviewTransactionCmd = new SqlCommand(allTransactionsQuery, sc);
        gridviewTransactionCmd.Parameters.AddWithValue("@EmployeeID", viewedEmployeeID);


        SqlDataReader dr = gridviewTransactionCmd.ExecuteReader();
        TransactionData.Load(dr);
        grdAllTransactions.DataSource = TransactionData;
        grdAllTransactions.DataBind();
        
        sc.Close();


     


    }






}
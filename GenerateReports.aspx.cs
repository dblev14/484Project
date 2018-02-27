using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
public partial class GenerateReports : System.Web.UI.Page
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
    }

    protected void btnGenerateReport_Click(object sender, EventArgs e)
    {
        String fileName = "";
        String query="";
        String dateColumn = "";

        //get selected index
        int selectedReportIndex=dropReportType.SelectedIndex;
        int timeSelectedIndex = dropReportType.SelectedIndex;


        switch (selectedReportIndex)
        {
            case 0: //Rewards Sent
                {
                    query = "SELECT CONCAT(Employee.FirstName, ' ', Employee.MiddleInitial, ' ', Employee.LastName) as 'Receiver Name', Reward.RewardReason, CONCAT(Employee_1.FirstName, ' ', Employee_1.MiddleInitial, ' ', Employee_1.LastName) as 'Sender Name', RewardCategory.CategoryName as 'Category Name', CompanyValues.ValueName as 'Category Value', Reward.DateOfDeed as 'Date Of Deed',Reward.RewardSendDate as 'Reward Send Date' FROM Reward INNER JOIN Employee ON Reward.RewardReceiver = Employee.EmployeeID INNER JOIN Employee AS Employee_1 ON Reward.RewardReceiver = Employee_1.EmployeeID INNER JOIN CompanyValues ON Reward.ValueID = CompanyValues.ValueID INNER JOIN RewardCategory ON Reward.CategoryID = RewardCategory.CategoryID";
                    fileName += "RewardsSentSince";
                    dateColumn = "Reward.DateOfDeed";
                    break;
                }
            case 1: //Gifts Bought
                {
                    query = "SELECT        Concat(Employee.FirstName, ' ', Employee.MiddleInitial, ' ', Employee.LastName) as 'Full Name', Gift.GiftName as 'Gift Name', EmployeeGift.BuyDate as 'Buy Date', EmployeeGift.Quantity, Gift.GiftAmount as 'Gift Amount', Gift.GiftCost as 'Gift Cost', Vendor.VendorName as 'Vendor Name' " +
                " FROM Gift INNER JOIN " +
                        " Vendor ON Gift.VendorID = Vendor.VendorID INNER JOIN" +
                        " EmployeeGift ON Gift.GiftID = EmployeeGift.GiftID INNER JOIN" +
                        " Employee ON EmployeeGift.EmployeeID = Employee.EmployeeID CROSS JOIN" +
                        " Fund";

                    dateColumn = "EmployeeGift.BuyDate";
                    fileName += "GiftsBoughtSince";
                    break;
                }
        }

        

       


        switch (timeSelectedIndex)
        {
            case 0: //past week
                {
                    String lastWeek = DateTime.Today.AddDays(-7).ToShortDateString();
                    query += " WHERE " +dateColumn + " >='" + lastWeek + "'";
                    fileName += "(" + lastWeek + ")";
                    break;
                }

            case 1: //past month
                {
                    String lastMonth = DateTime.Today.AddMonths(-1).ToShortDateString();
                    query += " WHERE " + dateColumn + "  >='" + lastMonth + "'";
                    fileName += "(" + lastMonth + ")";
                    break;
                }
            case 2: //past year
                {
                    String lastYear = DateTime.Today.AddYears(-1).ToShortDateString();
                    query += " WHERE " + dateColumn + "  >='" + lastYear +"'";
                    fileName += "(" + lastYear + ")";
                    break;
                }
            case 3: //all
                {
                    fileName += "(All)";
                    break;
                    
                }

        
        }

        //Create Table for specified report
        DataTable TransactionData = new DataTable();
        SqlCommand gridviewTransactionCmd = new SqlCommand(query, sc);
        if (sc.State == ConnectionState.Closed)
            sc.Open();
        SqlDataReader dr = gridviewTransactionCmd.ExecuteReader();
        TransactionData.Load(dr);
        gridReport.DataSource = TransactionData;
        gridReport.DataBind();
        sc.Close();

        //Create Excel Document
        try
        {
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment;filename=" + fileName + ".xls");
            Response.ContentType = "application/excel";

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            gridReport.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            lblStatus.Text = "Report generated";
            Response.End();
            
        }
        
        
        catch (System.Threading.ThreadAbortException)
        {
            lblStatus.Text = "Report generated";
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Report failed to generate";
            
        }

        lblStatus.Text = "Report generated";

    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }


    public void AllTransactions()
    {
        sc.Open();

        String allTransactionsQuery = "SELECT CONCAT(Employee.FirstName, ' ', Employee.MiddleInitial, ' ', Employee.LastName) as 'Receiver Name', Reward.RewardReason, CONCAT(Employee_1.FirstName, ' ', Employee_1.MiddleInitial, ' ', Employee_1.LastName) as 'Sender Name', RewardCategory.CategoryName as 'Category Name', CompanyValues.ValueName as 'Category Value' FROM Reward INNER JOIN Employee ON Reward.RewardReceiver = Employee.EmployeeID INNER JOIN Employee AS Employee_1 ON Reward.RewardReceiver = Employee_1.EmployeeID INNER JOIN CompanyValues ON Reward.ValueID = CompanyValues.ValueID INNER JOIN RewardCategory ON Reward.CategoryID = RewardCategory.CategoryID";


        DataTable TransactionData = new DataTable();
        SqlCommand gridviewTransactionCmd = new SqlCommand(allTransactionsQuery, sc);
        

        SqlDataReader dr = gridviewTransactionCmd.ExecuteReader();
        TransactionData.Load(dr);
        gridReport.DataSource = TransactionData;
        gridReport.DataBind();
        sc.Close();
    }


}

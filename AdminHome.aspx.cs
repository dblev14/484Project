using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class AdminHome : System.Web.UI.Page
{
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


        try
        {

            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            sc.Open();
        }
        catch (Exception)
        {
        }

        updateFund();


    }


    protected void btnViewEmployees_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditEmployee.aspx");
    }

    protected void btnEditDeleteEmployees_Click(object sender, EventArgs e)
    {
        Response.Redirect("EditEmployee.aspx");
    }

    protected void btnCreateAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }

  


    public void updateFund()
    {
        String getFund = "SELECT RewardBalance FROM Fund WHERE CompanyID = " + Convert.ToInt32(Session["CompanyID"]);
        SqlCommand getFundCmd = new SqlCommand(getFund, sc);
        decimal fundBalance = Convert.ToDecimal(getFundCmd.ExecuteScalar());
        String fundBalanceString = string.Format("{0:C}", fundBalance);
        String getCompany = "SELECT CompanyName FROM Company WHERE CompanyID = " + Convert.ToInt32(Session["CompanyID"]);
        SqlCommand getCompanyCmd = new SqlCommand(getCompany, sc);
        String companyName = getCompanyCmd.ExecuteScalar().ToString();

        lblRewardFundTitle.Text = companyName + "'s Fund Balance";
        lblFundAmount.Text = fundBalanceString;

        if (fundBalance > 3500)
            lblFundAmount.ForeColor = System.Drawing.Color.Green;
        else if (fundBalance < 1500)
            lblFundAmount.ForeColor = System.Drawing.Color.Yellow;
        else
            lblFundAmount.ForeColor = System.Drawing.Color.Red;



    }








    protected void btnGenerateReports_Click1(object sender, EventArgs e)
    {
        Response.Redirect("GenerateReports.aspx");
    }

    protected void btnManageGift_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddGift.aspx");
    }

    protected void btnManageVendor_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddVendor.aspx");
    }

    protected void btnAnalytics_Click1(object sender, EventArgs e)
    {
        Response.Redirect("Analytics.aspx");
    }
}
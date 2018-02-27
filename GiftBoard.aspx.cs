using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class GiftBoard : System.Web.UI.Page
{
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    //SqlConnection sc = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        try
        {

            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            sc.Open();
        }
        catch (Exception)
        {
            String resultMessage = "Error Connecting to Database.";
            // lblMessage.Text = resultMessage;
        }
      
        DataTable fill = new DataTable();
        
        // fill in gridview with data table
        SqlCommand gridView = new SqlCommand("SELECT Gift.GiftName, Vendor.VendorName, Gift.GiftAmount FROM Gift INNER JOIN Vendor ON Gift.VendorID = Vendor.VendorID ", sc);
        SqlDataReader dr = gridView.ExecuteReader();

     
        fill.Load(dr);

        if (fill.Rows.Count < 1)
        {
            lblNothing.Text = "There are currently no gifts available.";
            lblNothing.Visible = true;
        }
        else
        {

            gridGift.DataSource = fill;
            gridGift.DataBind();
        }


    }

    protected void gridGift_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }
        // Get the currently selected row using the SelectedRow property.
        GridViewRow row = gridGift.SelectedRow;

        // And you respective cell's value
        String giftAmount = row.Cells[3].Text.Substring(1);
        System.Web.HttpContext.Current.Session["GiftAmount"] = giftAmount;
        String giftName = row.Cells[1].Text;
        System.Web.HttpContext.Current.Session["GiftName"] = giftName;
        String vendorName = row.Cells[2].Text;
        vendorName = vendorName.Replace("&#39;", "'");
        System.Web.HttpContext.Current.Session["VendorName"] = vendorName;

        SqlCommand select = new SqlCommand();
        select.Connection = sc;
        select.CommandText = "Select Vendor.VendorID from dbo.Vendor where Vendor.VendorName = @vendorName"; //needs fix
        select.Parameters.AddWithValue("@vendorName", vendorName);
        String vendorID = Convert.ToString(select.ExecuteScalar());
        System.Web.HttpContext.Current.Session["VendorID"] = vendorID;
        select.CommandText = "Select Gift.GiftID from Gift where GiftName = @giftName AND GiftAmount = @giftAmount AND VendorID = @vendorID";
        select.Parameters.AddWithValue("@giftName", giftName);
        select.Parameters.AddWithValue("@giftAmount", giftAmount);
        select.Parameters.AddWithValue("@vendorID", vendorID);
        String giftID = Convert.ToString(select.ExecuteScalar());
        System.Web.HttpContext.Current.Session["GiftID"] = giftID;
        select.CommandText = "Select Gift.GiftDescription from Gift where GiftID = @giftID";
        select.Parameters.AddWithValue("@giftID", System.Web.HttpContext.Current.Session["GiftID"]);
        String giftDescription = Convert.ToString(select.ExecuteScalar());
        System.Web.HttpContext.Current.Session["GiftDescription"] = giftDescription;
        select.CommandText = "Select Gift.Quantity from Gift where GiftID = @giftID";
        int giftQuantity = Convert.ToInt32(select.ExecuteScalar());
        HttpContext.Current.Session["GiftQuantity"] = giftQuantity;


        Response.Redirect("Gift.aspx");
        //lblMessage.Text = giftName;
    }
}
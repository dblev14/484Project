using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class Vendor : System.Web.UI.Page
{
    //SqlConnection sc = new SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    String websiteURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        try
        {

            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }
        lblName.Text = System.Web.HttpContext.Current.Session["VendorName"].ToString();
        SqlCommand select = new SqlCommand();
        select.Connection = sc;
        select.CommandText = "Select Vendor.VendorDescription from Vendor where VendorID = @vendorID";
        select.Parameters.AddWithValue("@vendorID", System.Web.HttpContext.Current.Session["VendorID"]);
        String vendorDesc;
        vendorDesc = Convert.ToString(select.ExecuteScalar());
        lblDescription.Text = vendorDesc;

        getVendorPic();

        
        select.CommandText = "Select Vendor.VendorURL from Vendor where VendorID = @vendorID";
        websiteURL = Convert.ToString(select.ExecuteScalar());
        if (String.IsNullOrEmpty(websiteURL))
        {
            btnVisitWebsite.Visible = false;
        }

        DataTable fill = new DataTable();

        // fill in gridview with data table
        SqlCommand gridView = new SqlCommand("SELECT Gift.GiftName, Vendor.VendorName, Gift.GiftAmount FROM Gift INNER JOIN Vendor ON Gift.VendorID = Vendor.VendorID where Vendor.VendorID = @vendorID ", sc);
        gridView.Parameters.AddWithValue("@vendorID", System.Web.HttpContext.Current.Session["VendorID"]);
        SqlDataReader dr = gridView.ExecuteReader();
        fill.Load(dr);
        int count = Convert.ToInt32(fill.Rows.Count);
        for (int i =0; i < count; i++)
        {
            String amount = fill.Rows[i][2].ToString();
            amount = amount.Substring(0, amount.Length - 2);
            fill.Rows[i][2] = Convert.ToDecimal(amount);
        }
        gridGift.DataSource = fill;
        gridGift.DataBind();
    }

    public void getVendorPic()
    {
        String getVendorImageQuery = "SELECT Image FROM vendorImage WHERE vendorID = @vendorID";

        using (SqlConnection conn = new SqlConnection(sc.ConnectionString))
        {
            SqlCommand getVendorImageCmd = new SqlCommand(getVendorImageQuery, conn);
            getVendorImageCmd.Parameters.AddWithValue("@vendorID", System.Web.HttpContext.Current.Session["VendorID"]);

            try
            {

                conn.Open();
                byte[] bytes = (byte[])getVendorImageCmd.ExecuteScalar();
                String strBase64 = Convert.ToBase64String(bytes);
                vendorImage.ImageUrl = "data:Image/png;base64," + strBase64;

            }

            catch 
            {
                vendorImage.ImageUrl = "Images/blank-face.jpg";
            }


        }
    }


    protected void btnVisitWebsite_Click(object sender, EventArgs e)
    {
        try
        {
            System.Diagnostics.Process.Start(websiteURL);
        }
        catch
        {

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
        String giftAmount = row.Cells[3].Text;
        //giftAmount = giftAmount.Substring(0, giftAmount.Length - 2);
        System.Web.HttpContext.Current.Session["GiftAmount"] = giftAmount;
        String giftName = row.Cells[1].Text;
        System.Web.HttpContext.Current.Session["GiftName"] = giftName;
        String vendorName = row.Cells[2].Text;
        System.Web.HttpContext.Current.Session["VendorName"] = vendorName;

        SqlCommand select = new SqlCommand();
        select.Connection = sc;
        String vendorID = Convert.ToString(HttpContext.Current.Session["VendorID"]);
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


        Response.Redirect("Gift.aspx");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class SearchEmployeeResults : System.Web.UI.Page
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
            lblMessage.Text = resultMessage;
        }
        if (!IsPostBack)
        {
            if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 0)
            {
                lstEmployee.Items.Clear();
                SqlCommand selectEmployee = new SqlCommand();
                selectEmployee.Connection = sc;
                selectEmployee.CommandText = "select FirstName,ISNULL(MiddleInitial, ''),LastName, Position, CompanyID from dbo.Employee where (Replace(FirstName, ' ', '')+MiddleInitial+Replace(LastName, ' ', '')) = @searched";
                //String where = Convert.ToString(HttpContext.Current.Session["WhereClause"]);
                String result = Convert.ToString(HttpContext.Current.Session["SearchedResult"]);
                //selectEmployee.Parameters.AddWithValue("@where", where);
                selectEmployee.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeReader = selectEmployee.ExecuteReader();
                System.Data.SqlClient.SqlCommand selectEmployeeID = new System.Data.SqlClient.SqlCommand();
                selectEmployeeID.Connection = sc;
                selectEmployeeID.CommandText = "select EmployeeID from dbo.Employee where (Replace(FirstName, ' ', '')+MiddleInitial+Replace(LastName, ' ', '')) = @searched";
                //selectEmployeeID.Parameters.AddWithValue("@where", where);
                selectEmployeeID.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeIDReader = selectEmployeeID.ExecuteReader();
                while (selectEmployeeReader.Read() && (selectEmployeeIDReader.Read()))
                {
                    String fullName = selectEmployeeReader[0].ToString() + " " + selectEmployeeReader[1].ToString() + " " + selectEmployeeReader[2].ToString();

                    lstEmployee.Items.Add(new ListItem(fullName, (selectEmployeeIDReader[0].ToString())));
                }

                getEmployeePicture();
            }

            if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 1)
            {
                lstEmployee.Items.Clear();
                SqlCommand selectEmployee = new SqlCommand();
                selectEmployee.Connection = sc;
                selectEmployee.CommandText = "select FirstName,ISNULL(MiddleInitial, ''),LastName, Position, CompanyID from dbo.Employee where (Replace(FirstName, ' ', '')+Replace(LastName, ' ', '')) = @searched";
                //String where = Convert.ToString(HttpContext.Current.Session["WhereClause"]);
                String result = Convert.ToString(HttpContext.Current.Session["SearchedResult"]);
                //selectEmployee.Parameters.AddWithValue("@where", where);
                selectEmployee.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeReader = selectEmployee.ExecuteReader();
                System.Data.SqlClient.SqlCommand selectEmployeeID = new System.Data.SqlClient.SqlCommand();
                selectEmployeeID.Connection = sc;
                selectEmployeeID.CommandText = "select EmployeeID from dbo.Employee where (Replace(FirstName, ' ', '')+Replace(LastName, ' ', '')) = @searched";
                //selectEmployeeID.Parameters.AddWithValue("@where", where);
                selectEmployeeID.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeIDReader = selectEmployeeID.ExecuteReader();
                while (selectEmployeeReader.Read() && (selectEmployeeIDReader.Read()))
                {
                    String fullName = selectEmployeeReader[0].ToString() + " " + selectEmployeeReader[1].ToString() + " " + selectEmployeeReader[2].ToString();

                    lstEmployee.Items.Add(new ListItem(fullName, (selectEmployeeIDReader[0].ToString())));
                }
                getEmployeePicture();
            }

            if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 2)
            {
                lstEmployee.Items.Clear();
                SqlCommand selectEmployee = new SqlCommand();
                selectEmployee.Connection = sc;
                selectEmployee.CommandText = "select FirstName,ISNULL(MiddleInitial, ''),LastName, Position, CompanyID from dbo.Employee where Replace(FirstName, ' ', '') = @searched";
                //String where = Convert.ToString(HttpContext.Current.Session["WhereClause"]);
                String result = Convert.ToString(HttpContext.Current.Session["SearchedResult"]);
                //selectEmployee.Parameters.AddWithValue("@where", where);
                selectEmployee.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeReader = selectEmployee.ExecuteReader();
                System.Data.SqlClient.SqlCommand selectEmployeeID = new System.Data.SqlClient.SqlCommand();
                selectEmployeeID.Connection = sc;
                selectEmployeeID.CommandText = "select EmployeeID from dbo.Employee where Replace(FirstName, ' ', '') = @searched";
                //selectEmployeeID.Parameters.AddWithValue("@where", where);
                selectEmployeeID.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeIDReader = selectEmployeeID.ExecuteReader();
                while (selectEmployeeReader.Read() && (selectEmployeeIDReader.Read()))
                {
                    String fullName = selectEmployeeReader[0].ToString() + " " + selectEmployeeReader[1].ToString() + " " + selectEmployeeReader[2].ToString();

                    lstEmployee.Items.Add(new ListItem(fullName, (selectEmployeeIDReader[0].ToString())));
                }

                getEmployeePicture();
            }

            if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 3)
            {
                lstEmployee.Items.Clear();
                SqlCommand selectEmployee = new SqlCommand();
                selectEmployee.Connection = sc;
                selectEmployee.CommandText = "select FirstName,ISNULL(MiddleInitial, ''),LastName, Position, CompanyID from dbo.Employee where Replace(LastName, ' ', '') = @searched";
                //String where = Convert.ToString(HttpContext.Current.Session["WhereClause"]);
                String result = Convert.ToString(HttpContext.Current.Session["SearchedResult"]);
                //selectEmployee.Parameters.AddWithValue("@where", where);
                selectEmployee.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeReader = selectEmployee.ExecuteReader();
                System.Data.SqlClient.SqlCommand selectEmployeeID = new System.Data.SqlClient.SqlCommand();
                selectEmployeeID.Connection = sc;
                selectEmployeeID.CommandText = "select EmployeeID from dbo.Employee where Replace(LastName, ' ', '') = @searched";
                //selectEmployeeID.Parameters.AddWithValue("@where", where);
                selectEmployeeID.Parameters.AddWithValue("@searched", result);
                System.Data.SqlClient.SqlDataReader selectEmployeeIDReader = selectEmployeeID.ExecuteReader();
                while (selectEmployeeReader.Read() && (selectEmployeeIDReader.Read()))
                {
                    String fullName = selectEmployeeReader[0].ToString() + " " + selectEmployeeReader[1].ToString() + " " + selectEmployeeReader[2].ToString();

                    lstEmployee.Items.Add(new ListItem(fullName, (selectEmployeeIDReader[0].ToString())));
                }

                getEmployeePicture();
            }

            if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 4)
            {
                lstEmployee.Items.Clear();
                SqlCommand selectGifts = new SqlCommand();
                selectGifts.Connection = sc;
                selectGifts.CommandText = "Select GiftName, Gift.GiftAmount, Gift.GiftDescription, Gift.Quantity, Gift.GiftID, Vendor.VendorName from Gift Inner Join Vendor On Gift.VendorID = Vendor.VendorID where Replace(GiftName, ' ', '') = @searched";
                String result = Convert.ToString(HttpContext.Current.Session["SearchedResult"]);
                selectGifts.Parameters.AddWithValue("@searched", result);
                SqlDataReader selectGiftReader = selectGifts.ExecuteReader();
                while (selectGiftReader.Read())
                {
                    String gift = selectGiftReader[0].ToString() + ", " + selectGiftReader[5];
                    lstEmployee.Items.Add(new ListItem(gift, selectGiftReader[4].ToString()));
                }
            }

            if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 5)
            {
                lstEmployee.Items.Clear();
                SqlCommand selectVendors = new SqlCommand();
                selectVendors.Connection = sc;
                selectVendors.CommandText = "Select Vendor.VendorName, Vendor.VendorDescription, Vendor.VendorID from Vendor where Replace(VendorName, ' ', '') = @searched";
                String result = Convert.ToString(HttpContext.Current.Session["SearchedResult"]);
                selectVendors.Parameters.AddWithValue("@searched", result);
                SqlDataReader selectVendorReader = selectVendors.ExecuteReader();
                while (selectVendorReader.Read())
                {
                    String text = selectVendorReader[0].ToString() + ", " + selectVendorReader[1].ToString();
                    lstEmployee.Items.Add(new ListItem(text, selectVendorReader[2].ToString()));
                }
            }

        }
    }

    protected void lstEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["Type"]) < 4)
        getEmployeePicture();
        if (Convert.ToInt32(Session["Type"]) ==4)
            getGiftPicture();
        if (Convert.ToInt32(Session["Type"]) == 5)
            getVendorPicture();
    }

    protected void btnViewPage_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(HttpContext.Current.Session["Type"]) < 4)
        {
            int employeeID = Convert.ToInt32(lstEmployee.SelectedValue);
            Session["SearchedEmployeeID"] = employeeID;
            Response.Redirect("EmployeeForm.aspx");
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 4)
        {
            SqlCommand selectGifts = new SqlCommand();
            selectGifts.Connection = sc;
            selectGifts.CommandText = "Select GiftName, Gift.GiftAmount, Gift.GiftDescription, Gift.Quantity, Gift.GiftID, Vendor.VendorName from Gift Inner Join Vendor On Gift.VendorID = Vendor.VendorID where GiftID = @gift";
            String gift = Convert.ToString(lstEmployee.SelectedValue);
            selectGifts.Parameters.AddWithValue("@gift", gift);
            SqlDataReader read = selectGifts.ExecuteReader();
            while (read.Read())
            {
                System.Web.HttpContext.Current.Session["GiftName"] = Convert.ToString(read[0].ToString());
                System.Web.HttpContext.Current.Session["GiftID"] = Convert.ToString(lstEmployee.SelectedValue);
                String amount = read[1].ToString();
                amount = amount.Substring(0, amount.Length - 2);
                System.Web.HttpContext.Current.Session["GiftAmount"] = amount;
                System.Web.HttpContext.Current.Session["GiftQuantity"] = read[3].ToString();
                System.Web.HttpContext.Current.Session["GiftDescription"] = read[2].ToString();
                System.Web.HttpContext.Current.Session["VendorName"] = read[5].ToString();
            }

            getGiftPicture();
            Response.Redirect("Gift.aspx");
        }
        else if (Convert.ToInt32(HttpContext.Current.Session["Type"]) == 5)
        {

            SqlCommand selectVendors = new SqlCommand();
            selectVendors.Connection = sc;
            selectVendors.CommandText = "Select Vendor.VendorName from Vendor where vendorID = @vendor";
            String vendor = Convert.ToString(lstEmployee.SelectedValue);
            selectVendors.Parameters.AddWithValue("@vendor", vendor);
            SqlDataReader read = selectVendors.ExecuteReader();
            while (read.Read())
            {
                System.Web.HttpContext.Current.Session["VendorName"] = Convert.ToString(read[0].ToString());
                System.Web.HttpContext.Current.Session["VendorID"] = Convert.ToString(lstEmployee.SelectedValue);
            }

            getVendorPicture();
            Response.Redirect("Vendor.aspx");
        }
    }


    public void getEmployeePicture()
    {
        try
        {
            SqlCommand getPicCmd = new SqlCommand("SELECT Image FROM EmployeeImage WHERE EmployeeID = " + Convert.ToInt32(lstEmployee.SelectedValue), sc);

            byte[] imageBytes = { 0 };
            String strBase64Image;

            imageBytes = (byte[])getPicCmd.ExecuteScalar();
            strBase64Image = Convert.ToBase64String(imageBytes);
            String imageURL = "data:Image/png;base64," + strBase64Image;

            imgProfile.ImageUrl = imageURL;

        }
        catch
        {
            imgProfile.ImageUrl="Images/blank-face.jpg";
        }



    }

    public void getGiftPicture()
    {
        try
        {
            SqlCommand getPicCmd = new SqlCommand("SELECT Image FROM GiftImage WHERE GiftID = " + Convert.ToInt32(lstEmployee.SelectedValue), sc);

            byte[] imageBytes = { 0 };
            String strBase64Image;

            imageBytes = (byte[])getPicCmd.ExecuteScalar();
            strBase64Image = Convert.ToBase64String(imageBytes);
            String imageURL = "data:Image/png;base64," + strBase64Image;

            imgProfile.ImageUrl = imageURL;

        }
        catch
        {
            imgProfile.ImageUrl = "Images/no-image.jpg";
        }

    }

    public void getVendorPicture()
    {
        try
        {
            SqlCommand getPicCmd = new SqlCommand("SELECT Image FROM VendorImage WHERE VendorID = " + Convert.ToInt32(lstEmployee.SelectedValue), sc);

            byte[] imageBytes = { 0 };
            String strBase64Image;

            imageBytes = (byte[])getPicCmd.ExecuteScalar();
            strBase64Image = Convert.ToBase64String(imageBytes);
            String imageURL = "data:Image/png;base64," + strBase64Image;

            imgProfile.ImageUrl = imageURL;

        }
        catch
        {
            imgProfile.ImageUrl = "Images/no-image.jpg";
        }

    }

}
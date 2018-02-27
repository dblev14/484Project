using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Gift : System.Web.UI.Page
{
    //SqlConnection sc = new SqlConnection();
    //static List<Gift1> shoppingCart = new List<Gift1>();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);

    int maxItems = 9;
    int selectedItemCount = 0;
    Boolean exists = false;
    int check;

   



    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");


        if ((int) Session["AdminFlag"] == 1)
        {
            
            btnSave.Enabled = false;
            btnAdd.Enabled = false;
        }



        if (!IsPostBack)
        {
            try
            {

                //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
                sc.Open();
            }
            catch (Exception)
            {
            }

            lblName.Text = System.Web.HttpContext.Current.Session["GiftName"].ToString();
            txtDescription.Text = System.Web.HttpContext.Current.Session["GiftDescription"].ToString();
            lblVendor.Text = System.Web.HttpContext.Current.Session["VendorName"].ToString();
            String price = String.Format("{0:F2}", System.Web.HttpContext.Current.Session["GiftAmount"].ToString()); //incorrectly changing the price
            decimal price1 = Convert.ToDecimal(price);
            // Works for right now but need to update later
            price1 = Math.Truncate(100 * price1) / 100;
            System.Web.HttpContext.Current.Session["GiftAmount"] = price;
            lblPrice.Text = "$" + System.Web.HttpContext.Current.Session["GiftAmount"].ToString();
            btnViewVendorPage.Text = "View " + System.Web.HttpContext.Current.Session["VendorName"].ToString() + "'s Page";
            getGiftPic();
            setQuantity();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {
            lblNothing.Text = "There is already the maximum of nine different items in your cart.";
            lblNothing.Visible = true;
        }
        else
        {
            lblNothing.Visible = false;
            int giftID = Convert.ToInt32(HttpContext.Current.Session["GiftID"]);
            String giftName = Convert.ToString(HttpContext.Current.Session["GiftName"]);
            String giftDescription = Convert.ToString(HttpContext.Current.Session["GiftDescription"]);
            decimal giftAmount = Convert.ToDecimal(HttpContext.Current.Session["GiftAmount"]);
            int giftQuantity = Convert.ToInt32(HttpContext.Current.Session["GiftQuantity"]);
            int selectedQuantity = Convert.ToInt32(ddlQuantity.SelectedValue.ToString());
            // need to get gift cost
            decimal giftCost = Convert.ToDecimal(HttpContext.Current.Session["GiftAmount"]);
            String vendorName = Convert.ToString(HttpContext.Current.Session["VendorName"]);

            String getGiftImageQuery = "SELECT Image FROM GiftImage WHERE GiftID = @GiftID";

            using (SqlConnection conn = new SqlConnection(sc.ConnectionString))
            {
                SqlCommand getGiftImageCmd = new SqlCommand(getGiftImageQuery, conn);
                getGiftImageCmd.Parameters.AddWithValue("@GiftID", System.Web.HttpContext.Current.Session["GiftID"]);

                try
                {
                    if (Gift1.shoppingCart.Count > 0)
                    {
                        for (int i =0; i < Gift1.shoppingCart.Count; i++)
                        {
                            if (giftName == Gift1.shoppingCart[i].getGiftName())
                            {
                                exists = true;
                                check = i;
                            }
                        }
                    }
                    if (exists)
                    {
                        conn.Open();
                        byte[] bytes = (byte[])getGiftImageCmd.ExecuteScalar();
                        String strBase64 = Convert.ToBase64String(bytes);
                        Gift1 newGift = new Gift1(giftID, giftName, giftDescription, giftAmount, giftCost, vendorName, strBase64, selectedQuantity);
                        Gift1.shoppingCart[check] = newGift; 
                    }
                    else
                    {
                        conn.Open();
                        byte[] bytes = (byte[])getGiftImageCmd.ExecuteScalar();
                        String strBase64 = Convert.ToBase64String(bytes);
                        Gift1 newGift = new Gift1(giftID, giftName, giftDescription, giftAmount, giftCost, vendorName, strBase64, selectedQuantity);
                        Gift1.shoppingCart.Add(newGift);
                    }
                    //conn.Open();
                    //byte[] bytes = (byte[])getGiftImageCmd.ExecuteScalar();
                    //String strBase64 = Convert.ToBase64String(bytes);
                    //Gift1 newGift = new Gift1(giftID, giftName, giftDescription, giftAmount, giftCost, vendorName, strBase64, selectedQuantity);
                    //Gift1.shoppingCart.Add(newGift);
                }

                catch (System.ArgumentNullException)
                {
                    if (Gift1.shoppingCart.Count > 0)
                    {
                        for (int i = 0; i < Gift1.shoppingCart.Count; i++)
                        {
                            if (giftName == Gift1.shoppingCart[i].getGiftName())
                            {
                                exists = true;
                                check = i;
                            }
                        }
                    }
                    if (exists)
                    {
                        Gift1 newGift = new Gift1(giftID, giftName, giftDescription, giftAmount, giftCost, vendorName, selectedQuantity);
                        Gift1.shoppingCart[check] = newGift;
                    }
                    else
                    {
                        Gift1 newGift = new Gift1(giftID, giftName, giftDescription, giftAmount, giftCost, vendorName, selectedQuantity);
                        Gift1.shoppingCart.Add(newGift);
                    }
                    //Gift1 newGift = new Gift1(giftID, giftName, giftDescription, giftAmount, giftCost, vendorName, selectedQuantity);
                    //Gift1.shoppingCart.Add(newGift);
                }
            }
        }
        selectedItemCount = Convert.ToInt32(ddlQuantity.SelectedValue.ToString());
        int baseCount = Convert.ToInt32(HttpContext.Current.Session["GiftQuantity"]);
        HttpContext.Current.Session["GiftQuantity"] = baseCount - selectedItemCount;
        setQuantity();
    }

    protected void btnSave_Click(object sender, EventArgs e)
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
        SqlCommand getUserName = new SqlCommand();
        getUserName.Connection = sc;
        getUserName.CommandText = "Select Username from AccountPassword where EmployeeID = @employee";
        getUserName.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
        String userName = Convert.ToString(getUserName.ExecuteScalar());

        
        SqlCommand wishCount = new SqlCommand();
        wishCount.Connection = sc;
        wishCount.CommandText = "Select count(employeeID) from WishList where EmployeeID = @employeeID";
        wishCount.Parameters.AddWithValue("@employeeID", HttpContext.Current.Session["CurrentUserID"]);
        int wishDBCount = Convert.ToInt32(wishCount.ExecuteScalar());


        SqlCommand getGiftNames = new SqlCommand();
        getGiftNames.Connection = sc;
        getGiftNames.CommandText = "Select GiftID from WishList where EmployeeID = @employee";
        getGiftNames.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
        SqlDataReader readIDs = getGiftNames.ExecuteReader();
        List<int> giftIDs = new List<int>();
        while (readIDs.Read())
        {
            giftIDs.Add(Convert.ToInt32(readIDs[0]));
        }

        Boolean exists = false;
        int check = 0;
        int giftID = Convert.ToInt32(HttpContext.Current.Session["GiftID"]);
        if (wishDBCount > 0)
        {
            for (int i = 0; i < wishDBCount; i++)
            {
                if (giftID == giftIDs[i])
                {
                    exists = true;
                    check = i;
                }
            }
        }


        if (wishDBCount >= maxItems)
        {
            lblNothing.Text = "There is already the maximum of nine different items in your saved list.";
            lblNothing.Visible = true;
        }
        else if (exists)
        {
            lblNothing.Visible = false;
            SqlCommand updateWish = new SqlCommand();
            updateWish.Connection = sc;
            updateWish.CommandText = "Update Wishlist set Quantity = @quantity, LastUpdated = @lastUpdated where EmployeeID = @employee and GiftID = @giftID";
            updateWish.Parameters.AddWithValue("@quantity", Convert.ToInt32(ddlQuantity.SelectedItem.Text));
            updateWish.Parameters.AddWithValue("@lastUpdated", DateTime.Now);
            int employeeID = Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]);
            updateWish.Parameters.AddWithValue("@employee", employeeID);
            updateWish.Parameters.AddWithValue("@giftID", giftID);

            updateWish.ExecuteNonQuery();

        }
        else
        {
            lblNothing.Visible = false;
            SqlCommand insertWish = new SqlCommand();
            insertWish.Connection = sc;
            insertWish.CommandText = "insert into WishList Values (@employeeID, @giftID, @quantity, @lastUpdated, @lastUpdatedBy)";
            int employeeID = Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]);
            giftID = Convert.ToInt32(HttpContext.Current.Session["GiftID"]);
            int quantity = Convert.ToInt32(ddlQuantity.SelectedValue.ToString());
            insertWish.Parameters.AddWithValue("@employeeiD", employeeID);
            insertWish.Parameters.AddWithValue("@giftID", giftID);
            insertWish.Parameters.AddWithValue("@quantity", quantity);
            insertWish.Parameters.AddWithValue("@lastUpdatedBy", userName);
            insertWish.Parameters.AddWithValue("@lastUpdated", DateTime.Now);

            insertWish.ExecuteNonQuery();
        }
    }

    protected void btnViewVendorPage_Click(object sender, EventArgs e)
    {
        Response.Redirect("Vendor.aspx");
    }

    public void getGiftPic()
    {
        String getGiftImageQuery = "SELECT Image FROM GiftImage WHERE GiftID = @GiftID";

        using (SqlConnection conn = new SqlConnection(sc.ConnectionString))
        {
            SqlCommand getGiftImageCmd = new SqlCommand(getGiftImageQuery, conn);
            getGiftImageCmd.Parameters.AddWithValue("@GiftID", System.Web.HttpContext.Current.Session["GiftID"]);

            try
            {

                conn.Open();
                byte[] bytes = (byte[])getGiftImageCmd.ExecuteScalar();
                String strBase64 = Convert.ToBase64String(bytes);
                giftImage.ImageUrl = "data:Image/png;base64," + strBase64;
                
            }

            catch (Exception ex)
            {
                giftImage.ImageUrl = "Images/no-image.jpg";
            }


        }



    }

    protected void ddlQuantity_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void setQuantity()
    {
        ddlQuantity.Items.Clear();
        int count = Convert.ToInt32(HttpContext.Current.Session["GiftQuantity"]);
        for (int i = 1; i <= count; i++)
        {
            ddlQuantity.Items.Add(new ListItem(Convert.ToString(i), Convert.ToString(i)));
        }
    }
}

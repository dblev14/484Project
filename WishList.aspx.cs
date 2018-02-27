using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class WishList : System.Web.UI.Page
{
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    //SqlConnection sc = new SqlConnection();
    public List<Gift1> wishList = new List<Gift1>();
    int maxItems = 9;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        if ((int)Session["AdminFlag"] == 1)
        {

            Response.Redirect("RewardFeed.aspx");
        }



        try
        {

            ////sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        int employeeID = Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]);
        int nbrOfGifts = getGiftCount(employeeID);

        fillList(employeeID, nbrOfGifts);

        if (wishList.Count < 1)
        {
            lblNothing.Text = "There is nothing saved currently.";
            lblNothing.Visible = true;
        }
        else
        {
            lblNothing.Visible = false;
        }


        RefreshPage(nbrOfGifts);

    }

    public String getGiftPic(int i)
    {
        String imageUrl = "data:Image/png;base64," + wishList[i].getImage();
        return imageUrl;
    }

    public void fillList(int employeeID, int nbrOfGifts)
    {
        int giftCounter = 0;
        int quantityCounter = 0;
        int[] giftIDs = new int[nbrOfGifts];
        int[] giftQuantities = new int[nbrOfGifts];
        try
        {
            ////sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        SqlCommand selectGiftIDs = new SqlCommand();
        selectGiftIDs.Connection = sc;
        selectGiftIDs.CommandText = "Select GiftID from WishList where EmployeeID = @employeeID";
        selectGiftIDs.Parameters.AddWithValue("@employeeiD", employeeID);

        SqlDataReader readGiftIDs = selectGiftIDs.ExecuteReader();
        while (readGiftIDs.Read())
        {
            giftIDs[giftCounter] = Convert.ToInt32(readGiftIDs[0].ToString());
            giftCounter++;
        }

        SqlCommand selectGiftQuantities = new SqlCommand();
        selectGiftQuantities.Connection = sc;
        selectGiftQuantities.CommandText = "Select Quantity from WishList where EmployeeID = @employeeID";
        selectGiftQuantities.Parameters.AddWithValue("@employeeiD", employeeID);

        SqlDataReader readGiftQuantities = selectGiftQuantities.ExecuteReader();
        while (readGiftQuantities.Read())
        {
            giftQuantities[quantityCounter] = Convert.ToInt32(readGiftQuantities[0].ToString());
            quantityCounter++;
        }


        for (int i = 0; i < nbrOfGifts; i++)
        {
            int giftID = giftIDs[i];
            int quantity = giftQuantities[i];
            String strBase64;
            SqlCommand selectVendor = new SqlCommand();
            selectVendor.Connection = sc;
            selectVendor.CommandText = "Select VendorID from Gift where GiftID = @giftID";
            selectVendor.Parameters.AddWithValue("@giftID", giftID);
            int vendorID = Convert.ToInt32(selectVendor.ExecuteScalar());

            selectVendor.CommandText = "Select VendorName from Vendor where VendorID = @vendorID";
            selectVendor.Parameters.AddWithValue("@vendorID", vendorID);
            String vendorName = Convert.ToString(selectVendor.ExecuteScalar());

            String getGiftImageQuery = "SELECT Image FROM GiftImage WHERE GiftID = @GiftID";

            using (SqlConnection conn = new SqlConnection(sc.ConnectionString))
            {
                SqlCommand getGiftImageCmd = new SqlCommand(getGiftImageQuery, conn);
                getGiftImageCmd.Parameters.AddWithValue("@GiftID", giftID);
                try
                {
                    conn.Open();
                    byte[] bytes = (byte[])getGiftImageCmd.ExecuteScalar();
                    strBase64 = Convert.ToBase64String(bytes);
                }
                catch (System.ArgumentNullException)
                {
                    strBase64 = "";
                }
            }
            SqlCommand selectGift = new SqlCommand();
            selectGift.Connection = sc;
            selectGift.CommandText = "Select GiftName, GiftDescription, GiftAmount, GiftCost from Gift where GiftID = @giftID";
            selectGift.Parameters.AddWithValue("@giftID", giftID);

            SqlDataReader readGifts = selectGift.ExecuteReader();
            while (readGifts.Read())
            {
                Gift1 tempGift = new Gift1(giftID, Convert.ToString(readGifts[0]), Convert.ToString(readGifts[1]), Convert.ToDecimal(readGifts[2]), Convert.ToDecimal(readGifts[3]), vendorName, strBase64, quantity);
                wishList.Add(tempGift);
            }
        }

    }

    public int getGiftCount(int i)
    {
        SqlCommand select = new SqlCommand();
        select.Connection = sc;
        select.CommandText = "select count(EmployeeID) from WishList where EmployeeID = @employeeID";
        select.Parameters.AddWithValue("@employeeID", i);
        int nbrOfGifts = Convert.ToInt32(select.ExecuteScalar());
        return nbrOfGifts;
    }

    public void RefreshPage(int nbrOfItems)
    {
        for (int i = 0; i < nbrOfItems; i++)
        {
            if (i == 0)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem1.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem1.ImageUrl = getGiftPic(i);
                }
                lblGift1.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                lblAmount1.Text = "$" + amount;
                txtGift1.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift1Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 1)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem2.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem2.ImageUrl = getGiftPic(i);
                }
                lblGift2.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount2.Text = "$" + amount;
                txtGift2.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift2Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 2)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem3.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem3.ImageUrl = getGiftPic(i);
                }
                lblGift3.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount3.Text = "$" + amount;
                txtGift3.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift3Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 3)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem4.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem4.ImageUrl = getGiftPic(i);
                }
                lblGift4.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount4.Text = "$" + amount;
                txtGift4.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift4Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 4)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem5.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem5.ImageUrl = getGiftPic(i);
                }
                lblGift5.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount5.Text = "$" + amount;
                txtGift5.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift5Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 5)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem6.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem6.ImageUrl = getGiftPic(i);
                }
                lblGift6.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount6.Text = "$" + amount;
                txtGift6.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift6Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 6)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem7.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem7.ImageUrl = getGiftPic(i);
                }
                lblGift7.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount7.Text = "$" + amount;
                txtGift7.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift7Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 7)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem8.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem8.ImageUrl = getGiftPic(i);
                }
                lblGift8.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount8.Text = "$" + amount;
                txtGift8.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift8Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
            if (i == 8)
            {
                if (wishList[i].getImage() == "")
                {
                    imgItem9.ImageUrl = "Images/no-image.jpg";
                }
                else
                {
                    imgItem9.ImageUrl = getGiftPic(i);
                }
                lblGift9.Text = Convert.ToString(wishList[i].getGiftName());
                String amount = Convert.ToString(wishList[i].getGiftAmount());
                amount = amount.Substring(0, amount.Length-2);
                
                lblAmount9.Text = "$" + amount;
                txtGift9.Text = Convert.ToString(wishList[i].getGiftDescription());
                lblGift9Qty.Text = "Quantity: " + Convert.ToString(wishList[i].getQuantity());
            }
        }
        if (nbrOfItems == 0)
        {
            imgItem1.Visible = false;
            imgItem2.Visible = false;
            imgItem3.Visible = false;
            imgItem4.Visible = false;
            imgItem5.Visible = false;
            imgItem6.Visible = false;
            imgItem7.Visible = false;
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            lblGift1.Visible = false;
            lblGift2.Visible = false;
            lblGift3.Visible = false;
            lblGift4.Visible = false;
            lblGift5.Visible = false;
            lblGift6.Visible = false;
            lblGift7.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount1.Visible = false;
            lblAmount2.Visible = false;
            lblAmount3.Visible = false;
            lblAmount4.Visible = false;
            lblAmount5.Visible = false;
            lblAmount6.Visible = false;
            lblAmount7.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove1.Visible = false;
            btnRemove2.Visible = false;
            btnRemove3.Visible = false;
            btnRemove4.Visible = false;
            btnRemove5.Visible = false;
            btnRemove6.Visible = false;
            btnRemove7.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            txtGift1.Visible = false;
            txtGift2.Visible = false;
            txtGift3.Visible = false;
            txtGift4.Visible = false;
            txtGift5.Visible = false;
            txtGift6.Visible = false;
            txtGift7.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift1Qty.Visible = false;
            lblGift2Qty.Visible = false;
            lblGift3Qty.Visible = false;
            lblGift4Qty.Visible = false;
            lblGift5Qty.Visible = false;
            lblGift6Qty.Visible = false;
            lblGift7Qty.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart1.Visible = false;
            btnAddToCart2.Visible = false;
            btnAddToCart3.Visible = false;
            btnAddToCart4.Visible = false;
            btnAddToCart5.Visible = false;
            btnAddToCart6.Visible = false;
            btnAddToCart7.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;
        }
        else if (nbrOfItems == 1)
        {
            imgItem2.Visible = false;
            imgItem3.Visible = false;
            imgItem4.Visible = false;
            imgItem5.Visible = false;
            imgItem6.Visible = false;
            imgItem7.Visible = false;
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            lblGift2.Visible = false;
            lblGift3.Visible = false;
            txtGift2.Visible = false;
            txtGift3.Visible = false;
            txtGift4.Visible = false;
            txtGift5.Visible = false;
            txtGift6.Visible = false;
            txtGift7.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift2.Visible = false;
            lblGift3.Visible = false;
            lblGift4.Visible = false;
            lblGift5.Visible = false;
            lblGift6.Visible = false;
            lblGift7.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount2.Visible = false;
            lblAmount3.Visible = false;
            lblAmount4.Visible = false;
            lblAmount5.Visible = false;
            lblAmount6.Visible = false;
            lblAmount7.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove2.Visible = false;
            btnRemove3.Visible = false;
            btnRemove4.Visible = false;
            btnRemove5.Visible = false;
            btnRemove6.Visible = false;
            btnRemove7.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            lblGift2Qty.Visible = false;
            lblGift3Qty.Visible = false;
            lblGift4Qty.Visible = false;
            lblGift5Qty.Visible = false;
            lblGift6Qty.Visible = false;
            lblGift7Qty.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart2.Visible = false;
            btnAddToCart3.Visible = false;
            btnAddToCart4.Visible = false;
            btnAddToCart5.Visible = false;
            btnAddToCart6.Visible = false;
            btnAddToCart7.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;
        }
        else if (nbrOfItems == 2)
        {
            imgItem3.Visible = false;
            imgItem4.Visible = false;
            imgItem5.Visible = false;
            imgItem6.Visible = false;
            imgItem7.Visible = false;
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            lblGift3.Visible = false;
            txtGift3.Visible = false;
            txtGift4.Visible = false;
            txtGift5.Visible = false;
            txtGift6.Visible = false;
            txtGift7.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift3.Visible = false;
            lblGift4.Visible = false;
            lblGift5.Visible = false;
            lblGift6.Visible = false;
            lblGift7.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount3.Visible = false;
            lblAmount4.Visible = false;
            lblAmount5.Visible = false;
            lblAmount6.Visible = false;
            lblAmount7.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove3.Visible = false;
            btnRemove4.Visible = false;
            btnRemove5.Visible = false;
            btnRemove6.Visible = false;
            btnRemove7.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            lblGift3Qty.Visible = false;
            lblGift4Qty.Visible = false;
            lblGift5Qty.Visible = false;
            lblGift6Qty.Visible = false;
            lblGift7Qty.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart3.Visible = false;
            btnAddToCart4.Visible = false;
            btnAddToCart5.Visible = false;
            btnAddToCart6.Visible = false;
            btnAddToCart7.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;
        }
        else if (nbrOfItems == 3)
        {
            imgItem4.Visible = false;
            imgItem5.Visible = false;
            imgItem6.Visible = false;
            imgItem7.Visible = false;
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            txtGift4.Visible = false;
            txtGift5.Visible = false;
            txtGift6.Visible = false;
            txtGift7.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift4.Visible = false;
            lblGift5.Visible = false;
            lblGift6.Visible = false;
            lblGift7.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount4.Visible = false;
            lblAmount5.Visible = false;
            lblAmount6.Visible = false;
            lblAmount7.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove4.Visible = false;
            btnRemove5.Visible = false;
            btnRemove6.Visible = false;
            btnRemove7.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            lblGift4Qty.Visible = false;
            lblGift5Qty.Visible = false;
            lblGift6Qty.Visible = false;
            lblGift7Qty.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart4.Visible = false;
            btnAddToCart5.Visible = false;
            btnAddToCart6.Visible = false;
            btnAddToCart7.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;
        }
        else if (nbrOfItems == 4)
        {
            imgItem5.Visible = false;
            imgItem6.Visible = false;
            imgItem7.Visible = false;
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            txtGift5.Visible = false;
            txtGift6.Visible = false;
            txtGift7.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift5.Visible = false;
            lblGift6.Visible = false;
            lblGift7.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount5.Visible = false;
            lblAmount6.Visible = false;
            lblAmount7.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove5.Visible = false;
            btnRemove6.Visible = false;
            btnRemove7.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            lblGift5Qty.Visible = false;
            lblGift6Qty.Visible = false;
            lblGift7Qty.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart5.Visible = false;
            btnAddToCart6.Visible = false;
            btnAddToCart7.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;

        }
        else if (nbrOfItems == 5)
        {
            imgItem6.Visible = false;
            imgItem7.Visible = false;
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            txtGift6.Visible = false;
            txtGift7.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift6.Visible = false;
            lblGift7.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount6.Visible = false;
            lblAmount7.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove6.Visible = false;
            btnRemove7.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            lblGift6Qty.Visible = false;
            lblGift7Qty.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart6.Visible = false;
            btnAddToCart7.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;

        }
        else if (nbrOfItems == 6)
        {
            imgItem7.Visible = false;
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            txtGift7.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift7.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount7.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove7.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            lblGift7Qty.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart7.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;

        }
        else if (nbrOfItems == 7)
        {
            imgItem8.Visible = false;
            imgItem9.Visible = false;
            txtGift8.Visible = false;
            txtGift9.Visible = false;
            lblGift8.Visible = false;
            lblGift9.Visible = false;
            lblAmount8.Visible = false;
            lblAmount9.Visible = false;
            btnRemove8.Visible = false;
            btnRemove9.Visible = false;
            lblGift8Qty.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart8.Visible = false;
            btnAddToCart9.Visible = false;

        }
        else if (nbrOfItems == 8)
        {
            imgItem9.Visible = false;
            txtGift9.Visible = false;
            lblGift9.Visible = false;
            lblAmount9.Visible = false;
            btnRemove9.Visible = false;
            lblGift9Qty.Visible = false;
            btnAddToCart9.Visible = false;

        }
    }

    protected void btnRemove1_Click(object sender, EventArgs e)
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

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[0].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(0);

        int number = wishList.Count;

        RefreshPage(number);

        if (wishList.Count < 1)
        {
            lblNothing.Text = "There is nothing saved currently.";
            lblNothing.Visible = true;
        }
    }

    protected void btnRemove2_Click(object sender, EventArgs e)
    {
        try
        {

            // sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[1].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(1);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnRemove3_Click(object sender, EventArgs e)
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

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[2].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(2);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnRemove4_Click(object sender, EventArgs e)
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

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[3].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(3);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnRemove5_Click(object sender, EventArgs e)
    {
        try
        {

            ////sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[4].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(4);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnRemove6_Click(object sender, EventArgs e)
    {
        try
        {
            ////sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[5].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(5);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnRemove7_Click(object sender, EventArgs e)
    {
        try
        {

            ////sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[6].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(6);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnRemove8_Click(object sender, EventArgs e)
    {
        try
        {

            ////sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[7].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(7);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnRemove9_Click(object sender, EventArgs e)
    {



        try
        {

            ////sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
        }

        SqlCommand delete = new SqlCommand();
        delete.Connection = sc;
        delete.CommandText = "Delete from WishList where EmployeeID = @employeeID and GiftID = @giftID";
        delete.Parameters.AddWithValue("employeeID", HttpContext.Current.Session["CurrentUserID"]);
        delete.Parameters.AddWithValue("giftID", wishList[8].getGiftID());
        delete.ExecuteNonQuery();

        wishList.RemoveAt(8);

        int number = wishList.Count;

        RefreshPage(number);
    }

    protected void btnAddToCart1_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[0].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[0].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[0].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[0].getGiftID(), wishList[0].getGiftName(), wishList[0].getGiftDescription(), wishList[0].getGiftAmount(), wishList[0].getGiftCost(), wishList[0].getVendorName(), wishList[0].getImage(), wishList[0].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[0].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(0);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }

            
        }
        else
        {
            String amount = Convert.ToString(wishList[0].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[0].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[0].getGiftID(), wishList[0].getGiftName(), wishList[0].getGiftDescription(), wishList[0].getGiftAmount(), wishList[0].getGiftCost(), wishList[0].getVendorName(), wishList[0].getImage(), wishList[0].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[0].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(0);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }

        }
    }

    protected void btnAddToCart2_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[1].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[1].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[1].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[1].getGiftID(), wishList[1].getGiftName(), wishList[1].getGiftDescription(), wishList[1].getGiftAmount(), wishList[1].getGiftCost(), wishList[1].getVendorName(), wishList[1].getImage(), wishList[1].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[1].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(1);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[1].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[1].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[1].getGiftID(), wishList[1].getGiftName(), wishList[1].getGiftDescription(), wishList[1].getGiftAmount(), wishList[1].getGiftCost(), wishList[1].getVendorName(), wishList[1].getImage(), wishList[1].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[1].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(1);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    protected void btnAddToCart3_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[2].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[2].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[2].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[2].getGiftID(), wishList[2].getGiftName(), wishList[2].getGiftDescription(), wishList[2].getGiftAmount(), wishList[2].getGiftCost(), wishList[2].getVendorName(), wishList[2].getImage(), wishList[2].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[2].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(2);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[2].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[2].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[2].getGiftID(), wishList[2].getGiftName(), wishList[2].getGiftDescription(), wishList[2].getGiftAmount(), wishList[2].getGiftCost(), wishList[2].getVendorName(), wishList[2].getImage(), wishList[2].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[2].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(2);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    protected void btnAddToCart4_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[3].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[3].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[3].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[3].getGiftID(), wishList[3].getGiftName(), wishList[3].getGiftDescription(), wishList[3].getGiftAmount(), wishList[3].getGiftCost(), wishList[3].getVendorName(), wishList[3].getImage(), wishList[3].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[3].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(3);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[3].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[3].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[3].getGiftID(), wishList[3].getGiftName(), wishList[3].getGiftDescription(), wishList[3].getGiftAmount(), wishList[3].getGiftCost(), wishList[3].getVendorName(), wishList[3].getImage(), wishList[3].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[3].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(3);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    protected void btnAddToCart5_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[4].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[4].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[4].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[4].getGiftID(), wishList[4].getGiftName(), wishList[4].getGiftDescription(), wishList[4].getGiftAmount(), wishList[4].getGiftCost(), wishList[4].getVendorName(), wishList[4].getImage(), wishList[4].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[4].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(4);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[4].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[4].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[4].getGiftID(), wishList[4].getGiftName(), wishList[4].getGiftDescription(), wishList[4].getGiftAmount(), wishList[4].getGiftCost(), wishList[4].getVendorName(), wishList[4].getImage(), wishList[4].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[4].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(4);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    protected void btnAddToCart6_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[5].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[5].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[5].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[5].getGiftID(), wishList[5].getGiftName(), wishList[5].getGiftDescription(), wishList[5].getGiftAmount(), wishList[5].getGiftCost(), wishList[5].getVendorName(), wishList[5].getImage(), wishList[5].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[5].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(5);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[5].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[5].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[5].getGiftID(), wishList[5].getGiftName(), wishList[5].getGiftDescription(), wishList[5].getGiftAmount(), wishList[5].getGiftCost(), wishList[5].getVendorName(), wishList[5].getImage(), wishList[5].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[5].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(5);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    protected void btnAddToCart7_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[6].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[6].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[6].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[6].getGiftID(), wishList[6].getGiftName(), wishList[6].getGiftDescription(), wishList[6].getGiftAmount(), wishList[6].getGiftCost(), wishList[6].getVendorName(), wishList[6].getImage(), wishList[6].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[6].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(6);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[6].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[6].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[6].getGiftID(), wishList[6].getGiftName(), wishList[6].getGiftDescription(), wishList[6].getGiftAmount(), wishList[6].getGiftCost(), wishList[6].getVendorName(), wishList[6].getImage(), wishList[6].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[6].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(6);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    protected void btnAddToCart8_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[7].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[7].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[7].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[7].getGiftID(), wishList[7].getGiftName(), wishList[7].getGiftDescription(), wishList[7].getGiftAmount(), wishList[7].getGiftCost(), wishList[7].getVendorName(), wishList[7].getImage(), wishList[7].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[7].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(7);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[7].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[7].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[7].getGiftID(), wishList[7].getGiftName(), wishList[7].getGiftDescription(), wishList[7].getGiftAmount(), wishList[7].getGiftCost(), wishList[7].getVendorName(), wishList[7].getImage(), wishList[7].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[7].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(7);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    protected void btnAddToCart9_Click(object sender, EventArgs e)
    {
        Boolean exists = false;
        int check = 0;
        String giftName = wishList[8].getGiftName();
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
        if (Gift1.shoppingCart.Count >= maxItems)
        {

        }
        else if (exists)
        {
            String amount = Convert.ToString(wishList[8].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[8].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[8].getGiftID(), wishList[8].getGiftName(), wishList[8].getGiftDescription(), wishList[8].getGiftAmount(), wishList[8].getGiftCost(), wishList[8].getVendorName(), wishList[8].getImage(), wishList[8].getQuantity());
            Gift1.shoppingCart[check] = tempGift;
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[8].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(8);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
        else
        {
            String amount = Convert.ToString(wishList[8].getGiftAmount());
            amount = amount.Substring(0, amount.Length - 2);
            wishList[8].setGiftAmount(Convert.ToDecimal(amount));
            Gift1 tempGift = new Gift1(wishList[8].getGiftID(), wishList[8].getGiftName(), wishList[8].getGiftDescription(), wishList[8].getGiftAmount(), wishList[8].getGiftCost(), wishList[8].getVendorName(), wishList[8].getImage(), wishList[8].getQuantity());
            Gift1.shoppingCart.Add(tempGift);
            SqlCommand delete = new SqlCommand();
            delete.Connection = sc;
            delete.CommandText = "Delete from WishList where EmployeeID = @employee and GiftID = @giftID";
            delete.Parameters.AddWithValue("@employee", Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            delete.Parameters.AddWithValue("@giftID", wishList[8].getGiftID());
            delete.ExecuteNonQuery();
            int nbrOfGifts = getGiftCount(Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]));
            wishList.RemoveAt(8);

            if (wishList.Count < 1)
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Text = "There is nothing saved currently.";
                lblNothing.Visible = true;
            }
            else
            {
                RefreshPage(nbrOfGifts);
                lblNothing.Visible = false;
            }
        }
    }

    
}
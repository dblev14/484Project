using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Cart : System.Web.UI.Page
{
    SqlConnection sc;

    //SqlConnection sc = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);

        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        if (Gift1.shoppingCart.Count < 1)
        {
            lblNothing.Text = "There is nothing in your cart currently.";
            lblNothing.Visible = true;
        }
        else
        {
            lblNothing.Visible = false;
        }

        RefreshPage();

    }
    public String getGiftPic(int i)
    {
        String imageUrl = "data:Image/png;base64," + Gift1.shoppingCart[i].getImage();
        return imageUrl;
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
        String currentUser = "";
        using (sc)
        {
            String pullCurrentUser = "SELECT FirstName, LastName FROM Employee WHERE EmployeeID = " + currentID;
            using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc))
            {
                using (SqlDataReader reader = pullUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentUser = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                    }
                }
            }
        }

        int nbrOfItems = Gift1.shoppingCart.Count;
        double total = 0;

        for (int i = 0; i < nbrOfItems; i++)
        {
            total += Convert.ToDouble((Gift1.shoppingCart[i].getGiftAmount() * Gift1.shoppingCart[i].getQuantity()));

        }
        SqlCommand select = new SqlCommand();
        int employeeID = Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]);
        //sc.ConnectionString = @"Server = LOCALHOST; Database = Project; Trusted_Connection = Yes; MultipleActiveResultSets = true;";




        //if (sc.State == System.Data.ConnectionState.Closed)
        //    sc.Open();
        SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        conn.Open();
        if (conn.State == System.Data.ConnectionState.Closed)
            conn.Open();
        //sc.ConnectionString = WebConfigurationManager.ConnectionStrings["con"];
        //new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);

        select.Connection = conn;
        select.CommandText = "Select RewardBalance from Employee where EmployeeID = @employeeID";
        select.Parameters.AddWithValue("@employeeID", employeeID);
        double employeeBalance = Convert.ToDouble(select.ExecuteScalar());
        if (total > employeeBalance)
        {
            lblMessage.Text = "Not enough funds to make purchase";
        }
        else
        {
            lblMessage.Text = "";
            for (int i = 0; i < nbrOfItems; i++)
            {
                SqlCommand insert = new SqlCommand();
                insert.Connection = conn;
                insert.CommandText = "Exec EmployeeGift_buyGift @employeeID, @giftID, @buyDate, @quantity,@lastUpdatedBy,@lastUpdated";
                insert.Parameters.AddWithValue("@giftID", Gift1.shoppingCart[i].getGiftID());
                insert.Parameters.AddWithValue("@buyDate", System.DateTime.Now);
                insert.Parameters.AddWithValue("@employeeID", employeeID);
                insert.Parameters.AddWithValue("@quantity", Gift1.shoppingCart[i].getQuantity());
                insert.Parameters.AddWithValue("@lastUpdatedBy", currentUser);
                insert.Parameters.AddWithValue("@lastUpdated", DateTime.Now);

                insert.ExecuteNonQuery();
                lblStatus.Text = "You have succesfully checked out!";
                lblStatus.ForeColor = System.Drawing.Color.Green;
            }
            Gift1.shoppingCart.Clear();
            RefreshPage();
        }

    }



    protected void btnRemove1_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(0);
        RefreshPage();
        if (Gift1.shoppingCart.Count < 1)
        {
            lblNothing.Text = "There is nothing in your cart currently.";
            lblNothing.Visible = true;
        }
        else
        {
            lblNothing.Visible = false;
        }
    }

    protected void btnRemove2_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(1);
        RefreshPage();
    }

    protected void btnRemove3_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(2);
        RefreshPage();
    }

    protected void btnRemove4_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(3);
        RefreshPage();
    }

    protected void btnRemove5_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(4);
        RefreshPage();
    }

    protected void btnRemove6_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(5);
        RefreshPage();
    }

    protected void btnRemove7_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(6);
        RefreshPage();
    }

    protected void btnRemove8_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(7);
        RefreshPage();
    }

    protected void btnRemove9_Click(object sender, EventArgs e)
    {
        Gift1.shoppingCart.RemoveAt(8);
        RefreshPage();
    }

    protected void RefreshPage()
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

        SqlCommand select = new SqlCommand();
        int employeeID = Convert.ToInt32(HttpContext.Current.Session["CurrentUserID"]);
        SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        
        if (sc1.State == System.Data.ConnectionState.Closed)
            sc1.Open();
        select.Connection = sc1;
        select.CommandText = "Select RewardBalance from Employee where EmployeeID = @employeeID";
        select.Parameters.AddWithValue("@employeeID", employeeID);

        
        
        double employeeBalance = Convert.ToDouble(select.ExecuteScalar());

        lblPersonActualTotal.Text = "$" + Convert.ToString(employeeBalance);
        SqlCommand selectName = new SqlCommand();
        selectName.Connection = sc1;
        selectName.CommandText = "select FirstName from dbo.Employee where EmployeeID = @employeeID";
        selectName.Parameters.AddWithValue("@employeeID", HttpContext.Current.Session["CurrentUserID"]);
        lblPersonTotal.Text = Convert.ToString(selectName.ExecuteScalar()) + "'s Current Balance:";

        int nbrOfItems = Gift1.shoppingCart.Count;
        double total = 0;

        for (int i = 0; i < nbrOfItems; i++)
        {
            total += Convert.ToDouble((Gift1.shoppingCart[i].getGiftAmount() * Gift1.shoppingCart[i].getQuantity()));

        }
        lblSubtotalAmount.Text = "$" + Convert.ToString(total);

        lblActualProjectedBalance.Text = "$" + Convert.ToString(employeeBalance - total);

        if (nbrOfItems == 0)
        {
            lblActualProjectedBalance.Visible = false;
            lblProjectedBalance.Visible = false;
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
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems -1].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
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
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
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
            if (Gift1.shoppingCart[nbrOfItems - 3].getImage() == null || Gift1.shoppingCart[nbrOfItems - 3].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 3);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem3.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem3.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount3.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift3Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
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
            if (Gift1.shoppingCart[nbrOfItems - 4].getImage() == null || Gift1.shoppingCart[nbrOfItems - 4].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 4);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 3].getImage() == null || Gift1.shoppingCart[nbrOfItems - 3].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 3);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem3.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem3.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount3.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift3Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem4.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem4.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount4.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift4Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
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
            if (Gift1.shoppingCart[nbrOfItems - 5].getImage() == null || Gift1.shoppingCart[nbrOfItems - 5].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 5);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 4].getImage() == null || Gift1.shoppingCart[nbrOfItems - 4].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 4);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 3].getImage() == null || Gift1.shoppingCart[nbrOfItems - 3].getImage() == "")
            {
                imgItem3.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem3.ImageUrl = getGiftPic(nbrOfItems - 3);
            }
            lblGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftName());
            lblAmount3.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftAmount());
            txtGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftDescription());
            lblGift3Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem4.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem4.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount4.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift4Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem5.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem5.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount5.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift5Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
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
            if (Gift1.shoppingCart[nbrOfItems - 6].getImage() == null || Gift1.shoppingCart[nbrOfItems - 6].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 6);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 5].getImage() == null || Gift1.shoppingCart[nbrOfItems - 5].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 5);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 4].getImage() == null || Gift1.shoppingCart[nbrOfItems - 4].getImage() == "")
            {
                imgItem3.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem3.ImageUrl = getGiftPic(nbrOfItems - 4);
            }
            lblGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftName());
            lblAmount3.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftAmount());
            txtGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftDescription());
            lblGift3Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 3].getImage() == null || Gift1.shoppingCart[nbrOfItems - 3].getImage() == "")
            {
                imgItem4.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem4.ImageUrl = getGiftPic(nbrOfItems - 3);
            }
            lblGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftName());
            lblAmount4.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftAmount());
            txtGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftDescription());
            lblGift4Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem5.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem5.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount5.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift5Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem6.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem6.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount6.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift6Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
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
            if (Gift1.shoppingCart[nbrOfItems - 7].getImage() == null || Gift1.shoppingCart[nbrOfItems - 7].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 7);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 6].getImage() == null || Gift1.shoppingCart[nbrOfItems - 6].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 6);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 5].getImage() == null || Gift1.shoppingCart[nbrOfItems - 5].getImage() == "")
            {
                imgItem3.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem3.ImageUrl = getGiftPic(nbrOfItems - 5);
            }
            lblGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftName());
            lblAmount3.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftAmount());
            txtGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftDescription());
            lblGift3Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 4].getImage() == null || Gift1.shoppingCart[nbrOfItems - 4].getImage() == "")
            {
                imgItem4.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem4.ImageUrl = getGiftPic(nbrOfItems - 4);
            }
            lblGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftName());
            lblAmount4.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftAmount());
            txtGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftDescription());
            lblGift4Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 3].getImage() == null || Gift1.shoppingCart[nbrOfItems - 3].getImage() == "")
            {
                imgItem5.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem5.ImageUrl = getGiftPic(nbrOfItems - 3);
            }
            lblGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftName());
            lblAmount5.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftAmount());
            txtGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftDescription());
            lblGift5Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem6.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem6.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount6.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift6Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem7.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem7.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift7.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount7.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift7.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift7Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
        }
        else if (nbrOfItems == 8)
        {
            imgItem9.Visible = false;
            txtGift9.Visible = false;
            lblGift9.Visible = false;
            lblAmount9.Visible = false;
            btnRemove9.Visible = false;
            lblGift9Qty.Visible = false;
            if (Gift1.shoppingCart[nbrOfItems - 8].getImage() == null || Gift1.shoppingCart[nbrOfItems - 8].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 8);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 7].getImage() == null || Gift1.shoppingCart[nbrOfItems - 7].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 7);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 6].getImage() == null || Gift1.shoppingCart[nbrOfItems - 6].getImage() == "")
            {
                imgItem3.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem3.ImageUrl = getGiftPic(nbrOfItems - 6);
            }
            lblGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftName());
            lblAmount3.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftAmount());
            txtGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftDescription());
            lblGift3Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 5].getImage() == null || Gift1.shoppingCart[nbrOfItems - 5].getImage() == "")
            {
                imgItem4.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem4.ImageUrl = getGiftPic(nbrOfItems - 5);
            }
            lblGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftName());
            lblAmount4.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftAmount());
            txtGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftDescription());
            lblGift4Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 4].getImage() == null || Gift1.shoppingCart[nbrOfItems - 4].getImage() == "")
            {
                imgItem5.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem5.ImageUrl = getGiftPic(nbrOfItems - 4);
            }
            lblGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftName());
            lblAmount5.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftAmount());
            txtGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftDescription());
            lblGift5Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 3].getImage() == null || Gift1.shoppingCart[nbrOfItems - 3].getImage() == "")
            {
                imgItem6.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem6.ImageUrl = getGiftPic(nbrOfItems - 3);
            }
            lblGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftName());
            lblAmount6.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftAmount());
            txtGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftDescription());
            lblGift6Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem7.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem7.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift7.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount7.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift7.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift7Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem8.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem8.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift8.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount8.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift8.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift8Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
        }
        else if (nbrOfItems == 9)
        {
            if (Gift1.shoppingCart[nbrOfItems - 9].getImage() == null || Gift1.shoppingCart[nbrOfItems - 9].getImage() == "")
            {
                imgItem1.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem1.ImageUrl = getGiftPic(nbrOfItems - 9);
            }
            lblGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 9].getGiftName());
            lblAmount1.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 9].getGiftAmount());
            txtGift1.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 9].getGiftDescription());
            lblGift1Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 9].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 8].getImage() == null || Gift1.shoppingCart[nbrOfItems - 8].getImage() == "")
            {
                imgItem2.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem2.ImageUrl = getGiftPic(nbrOfItems - 8);
            }
            lblGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getGiftName());
            lblAmount2.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getGiftAmount());
            txtGift2.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getGiftDescription());
            lblGift2Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 8].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 7].getImage() == null || Gift1.shoppingCart[nbrOfItems - 7].getImage() == "")
            {
                imgItem3.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem3.ImageUrl = getGiftPic(nbrOfItems - 7);
            }
            lblGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftName());
            lblAmount3.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftAmount());
            txtGift3.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getGiftDescription());
            lblGift3Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 7].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 6].getImage() == null || Gift1.shoppingCart[nbrOfItems - 6].getImage() == "")
            {
                imgItem4.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem4.ImageUrl = getGiftPic(nbrOfItems - 6);
            }
            lblGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftName());
            lblAmount4.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftAmount());
            txtGift4.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getGiftDescription());
            lblGift4Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 6].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 5].getImage() == null || Gift1.shoppingCart[nbrOfItems - 5].getImage() == "")
            {
                imgItem5.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem5.ImageUrl = getGiftPic(nbrOfItems - 5);
            }
            lblGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftName());
            lblAmount5.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftAmount());
            txtGift5.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getGiftDescription());
            lblGift5Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 5].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 4].getImage() == null || Gift1.shoppingCart[nbrOfItems - 4].getImage() == "")
            {
                imgItem6.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem6.ImageUrl = getGiftPic(nbrOfItems - 4);
            }
            lblGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftName());
            lblAmount6.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftAmount());
            txtGift6.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getGiftDescription());
            lblGift6Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 4].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 3].getImage() == null || Gift1.shoppingCart[nbrOfItems - 3].getImage() == "")
            {
                imgItem7.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem7.ImageUrl = getGiftPic(nbrOfItems - 3);
            }
            lblGift7.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftName());
            lblAmount7.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftAmount());
            txtGift7.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getGiftDescription());
            lblGift7Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 3].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 2].getImage() == null || Gift1.shoppingCart[nbrOfItems - 2].getImage() == "")
            {
                imgItem8.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem8.ImageUrl = getGiftPic(nbrOfItems - 2);
            }
            lblGift8.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftName());
            lblAmount8.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftAmount());
            txtGift8.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getGiftDescription());
            lblGift8Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 2].getQuantity());
            if (Gift1.shoppingCart[nbrOfItems - 1].getImage() == null || Gift1.shoppingCart[nbrOfItems - 1].getImage() == "")
            {
                imgItem9.ImageUrl = "Images/no-image.jpg";
            }
            else
            {
                imgItem9.ImageUrl = getGiftPic(nbrOfItems - 1);
            }
            lblGift9.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftName());
            lblAmount9.Text = "$" + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftAmount());
            txtGift9.Text = Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getGiftDescription());
            lblGift9Qty.Text = "Quantity: " + Convert.ToString(Gift1.shoppingCart[nbrOfItems - 1].getQuantity());
        }
    }
}
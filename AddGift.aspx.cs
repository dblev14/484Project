using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Web.Configuration;

public partial class AddGift : System.Web.UI.Page
{
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        try
        {

            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            //sc.ConnectionString = "con";
            sc.Open();
        }
        catch (Exception)
        {
        }
        if (!IsPostBack)
        {

            populateDropDown();
            populateListBox();

        }

        lbl7.ForeColor = System.Drawing.Color.Green;
    }

    protected void ddlVendor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void populateDropDown()
    {
        ddlVendor.Items.Clear();


        SqlCommand loadVendors = new SqlCommand();
        loadVendors.Connection = sc;
        loadVendors.CommandText = "Select VendorName, VendorID from Vendor order by VendorName";
        SqlDataReader readVendors = loadVendors.ExecuteReader();
        while (readVendors.Read())
        {
            ddlVendor.Items.Add(new ListItem(readVendors[0].ToString(), readVendors[1].ToString()));
        }
    }

    protected void populateListBox()
    {
        listGifts.Items.Clear();

        SqlCommand loadGifts = new SqlCommand();
        loadGifts.Connection = sc;
        loadGifts.CommandText = "Select GiftName, GiftID, Vendor.VendorName, GiftAmount from Gift Inner Join Vendor On Gift.VendorID = Vendor.VendorID where Gift.Quantity > -1";
        SqlDataReader readGifts = loadGifts.ExecuteReader();
        while (readGifts.Read())
        {
            String gift = Convert.ToString(readGifts[0]) + ", " + Convert.ToString(readGifts[2]) + ", $";
            String amount = Convert.ToString(readGifts[3]);
            amount = amount.Substring(0, amount.Length - 2);
            gift += amount;

            listGifts.Items.Add(new ListItem(gift, readGifts[1].ToString()));
        }
    }

    protected void btnAddGift_Click(object sender, EventArgs e)
    {
        String name = txtGiftName.Text;
        String description = txtGiftDescription.Text;
        String amount = txtGiftAmount.Text;
        String quantity = txtGiftQuantity.Text;
        int vendorID = Convert.ToInt32(ddlVendor.SelectedValue);
        txtGiftAmount.Text = String.Empty;
        txtGiftDescription.Text = String.Empty;
        txtGiftName.Text = String.Empty;
        txtGiftQuantity.Text = String.Empty;

        int i = searchDB(name, description, vendorID, amount);

        if (i == 0)
        {
            lbl7.Visible = false;
            SqlCommand insert = new SqlCommand();
            insert.Connection = sc;
            if (sc.State == System.Data.ConnectionState.Closed)
            {
                sc.Open();
            }
            insert.CommandText = "Insert into Gift Values (@name, @description, @amount, @amount, @vendor, @quantity, @lastUpdatedBy, @lastUpdated)";
            insert.Parameters.AddWithValue("@name", name);
            insert.Parameters.AddWithValue("@description", description);
            insert.Parameters.AddWithValue("@amount", amount);
            insert.Parameters.AddWithValue("@vendor", vendorID);
            insert.Parameters.AddWithValue("@quantity", quantity);
            insert.Parameters.AddWithValue("@lastUpdated", DateTime.Now);
            insert.Parameters.AddWithValue("@lastUpdatedBy", "Nathan Lalande");
            insert.ExecuteNonQuery();

            HttpPostedFile postedFile = fileImageUpload.PostedFile;
            String fileName = Path.GetFileName(postedFile.FileName);
            String fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);


                String giftImageUploadQuery = "INSERT INTO GiftImage values(@GiftID, @Image,@LUB,@LU)";

                //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";

                SqlCommand getID = new SqlCommand();
                getID.Connection = sc;
                getID.CommandText = "Select MAx(giftID) from Gift";
                int giftID = Convert.ToInt32(getID.ExecuteScalar());

                SqlCommand giftImageUploadCmd = new SqlCommand(giftImageUploadQuery, sc);
                giftImageUploadCmd.Parameters.AddWithValue("@Image", bytes);
                giftImageUploadCmd.Parameters.AddWithValue("@GiftID", giftID);
                giftImageUploadCmd.Parameters.AddWithValue("@LUB", "Nathan Lalande");
                giftImageUploadCmd.Parameters.AddWithValue("@LU", DateTime.Now);

                giftImageUploadCmd.ExecuteNonQuery();
                //using (SqlConnection connection = new SqlConnection(sc.ConnectionString))
                //{



                //}

            }
        }
        else
        {
            lbl7.Text = "Gift Already Exists";
            lbl7.Visible = true;
        }

    }

    protected int searchDB(String name, String description, int vendor, String amount)
    {

        SqlCommand search = new SqlCommand();
        search.Connection = sc;
        search.CommandText = "Select GiftName from Gift where GiftName = @gift AND GiftDescription = @description AND GiftAmount = @amount AND VendorID = @vendor";
        search.Parameters.AddWithValue("@gift", name);
        search.Parameters.AddWithValue("@description", description);
        search.Parameters.AddWithValue("@amount", amount);
        search.Parameters.AddWithValue("@vendor", vendor);
        if (sc.State == System.Data.ConnectionState.Closed)
        {
            sc.Open();
        }
        try
        {
            String test = Convert.ToString(search.ExecuteScalar());
            if (test == String.Empty)
            {
                int i = 0;
                return i;
            }
            else
            {
                int i = 1;
                return i;
            }
        }
        catch
        {
            int i = 1;
            return i;
        }
    }


    //Not working
    protected void listGifts_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnAddGift.Enabled = false;

        String giftInfoQuery = "Select * FROM Gift WHERE GiftID = " +Convert.ToInt32(listGifts.SelectedValue);
        //String giftInfoQuery = "SELECT GiftImage.Image, Gift.GiftName, Gift.GiftDescription, Gift.GiftAmount, Gift.GiftCost, Gift.VendorID, Gift.Quantity FROM GiftImage INNER JOIN Gift ON GiftImage.GiftID = Gift.GiftID WHERE Gift.GiftID = '" + Convert.ToInt32(listGifts.SelectedValue) + "'";
        SqlCommand giftInfoCmd = new SqlCommand(giftInfoQuery, sc);
        SqlDataReader sqlReader = giftInfoCmd.ExecuteReader();
        while (sqlReader.Read())
        {
            txtGiftName.Text = sqlReader["GiftName"].ToString();
            txtGiftDescription.Text = sqlReader["GiftDescription"].ToString();
            txtGiftAmount.Text = sqlReader["GiftAmount"].ToString();
            txtGiftQuantity.Text = sqlReader["Quantity"].ToString();
            ddlVendor.SelectedValue = sqlReader["VendorID"].ToString();
            txtGiftCost.Text = sqlReader["GiftCost"].ToString();

        }




    }

    protected void btnDeleteGift_Click(object sender, EventArgs e)
    {
        if (listGifts.SelectedIndex == -1)
        {
            lbl7.Text = "Please select a gift first.";
        }
        else
        {
            if (sc.State == System.Data.ConnectionState.Closed)
            {
                sc.Open();
            }
            SqlCommand update = new SqlCommand();
            update.Connection = sc;
            update.CommandText = "Update Gift set Quantity = -1 where GiftID = @gift";
            int giftID = Convert.ToInt32(listGifts.SelectedValue);
            update.Parameters.AddWithValue("@gift", giftID);
            update.ExecuteNonQuery();
        }
        populateDropDown();
        populateListBox();
    }


    protected void btnEditGift_Click(object sender, EventArgs e)
    {
        int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
        String currentUser = "";
        using (SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
        {

            // select the project name that matches what the user puts in the search box
            String pullCurrentUser = "SELECT FirstName, LastName FROM Employee WHERE EmployeeID = " + currentID;
            using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc2))
            {

                if (sc2.State == System.Data.ConnectionState.Closed)
                {
                    sc2.Open();
                }
                using (SqlDataReader reader = pullUser.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        currentUser = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                    }
                }
            }
        }



        //Need to add gift costs
        String query = "UPDATE Gift SET GiftName = @GiftName, GiftDescription = @GiftDescription, GiftAmount = @GiftAmount, GiftCost = @GiftCost, VendorID = @VendorID, Quantity = @Quantity,LastUpdatedBy = @LastUpdatedBy,LastUpdated = @LastUpdated WHERE GiftID = " + Convert.ToInt32(listGifts.SelectedValue);

        SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        if (sc1.State == System.Data.ConnectionState.Closed)
        {
            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            sc1.Open();
        }

        try
        {
            

            SqlCommand cmd = new SqlCommand(query, sc);
            cmd.Parameters.AddWithValue("@GiftName", txtGiftName.Text);
            cmd.Parameters.AddWithValue("@GiftDescription", txtGiftDescription.Text);
            cmd.Parameters.AddWithValue("@GiftAmount", txtGiftAmount.Text);
            cmd.Parameters.AddWithValue("@GiftCost", txtGiftCost.Text);
            cmd.Parameters.AddWithValue("@VendorID", ddlVendor.SelectedValue);
            cmd.Parameters.AddWithValue("@Quantity", txtGiftQuantity.Text);
            cmd.Parameters.AddWithValue("@LastUpdatedBy", currentUser);
            cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

            cmd.ExecuteNonQuery();
            lbl7.Text = "Gift information updated. ";
        }
        catch (Exception ex)
        {
            lbl7.Text = ex.Message;
        }


        if (fileImageUpload != null)
        {
            HttpPostedFile postedFile = fileImageUpload.PostedFile;
            String fileName = Path.GetFileName(postedFile.FileName);
            String fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);


                String giftUploadQuery = "UPDATE GiftImage SET Image = @Image,LastUpdatedBy = @LastUpdatedBy, LastUpdated = @LastUpdated";
                String giftInsertQuery = "INSERT INTO GiftImage VALUES (" + Convert.ToInt32(listGifts.SelectedValue) + ",@Image, @LastUpdatedBy, @LastUpdated)";


                using (SqlConnection connection = new SqlConnection(sc.ConnectionString))
                {
                   
                    try
                    {
                       
                        SqlCommand giftUploadCmd = new SqlCommand(giftInsertQuery, connection);
                        giftUploadCmd.Parameters.AddWithValue("@Image", bytes);
                        giftUploadCmd.Parameters.AddWithValue("@LastUpdatedBy", currentUser);
                        giftUploadCmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                        connection.Open();
                        giftUploadCmd.ExecuteNonQuery();
                        lbl7.Text = "Image updated";


                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        try
                        {
                          

                            SqlCommand giftUploadCmd = new SqlCommand(giftUploadQuery, connection);
                            giftUploadCmd.Parameters.AddWithValue("@Image", bytes);
                            giftUploadCmd.Parameters.AddWithValue("@LastUpdatedBy", currentUser);
                            giftUploadCmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                            if (connection.State == System.Data.ConnectionState.Closed)
                                connection.Open();
                            giftUploadCmd.ExecuteNonQuery();
                            lbl7.Text += "Image updated";
                        }
                        catch 
                        {
                            lbl7.Text = "Error";
                            lbl7.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }















            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtGiftName.Text = "";
        txtGiftDescription.Text = "";
        txtGiftCost.Text = "";
        txtGiftAmount.Text = "";
        txtGiftQuantity.Text = "";
        btnAddGift.Enabled = true;
    }
}


   

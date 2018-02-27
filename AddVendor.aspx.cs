using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class AddVendor : System.Web.UI.Page
{
    //SqlConnection sc = new SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
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




        // Establish a connection to the database server
        try
        {
            //sc.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;";

            sc.Open();

        }
        catch
        {

        }

        if (!IsPostBack)
        {
            populateListBox();
        }
    }

    protected void populateListBox()
    {
        lstVendor.Items.Clear();

        SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand loadVendors = new SqlCommand();
        loadVendors.Connection = sc1;
        loadVendors.CommandText = "SELECT VendorName, VendorID FROM Vendor";
       
        if (sc1.State == System.Data.ConnectionState.Closed)
            sc1.Open();
        SqlDataReader venReader = loadVendors.ExecuteReader();
        
        while (venReader.Read())
        {
            String vendor = Convert.ToString(venReader["VendorName"]);
            int vendorID = Convert.ToInt32(venReader["VendorID"]);

            lstVendor.Items.Add(new ListItem(vendor, vendorID.ToString()));
        }
        venReader.Close();
    }

    protected void lstVendor_SelectedIndexChanged1(object sender, EventArgs e)
    {
        String vendorInfoQuery = "Select * FROM Vendor WHERE VendorID = " + Convert.ToInt32(lstVendor.SelectedValue);
        //String giftInfoQuery = "SELECT GiftImage.Image, Gift.GiftName, Gift.GiftDescription, Gift.GiftAmount, Gift.GiftCost, Gift.VendorID, Gift.Quantity FROM GiftImage INNER JOIN Gift ON GiftImage.GiftID = Gift.GiftID WHERE Gift.GiftID = '" + Convert.ToInt32(listGifts.SelectedValue) + "'";
        SqlCommand vendorInfo = new SqlCommand(vendorInfoQuery, sc);
        SqlDataReader sqlReader = vendorInfo.ExecuteReader();
        while (sqlReader.Read())
        {
            txtVenName.Text = sqlReader["VendorName"].ToString();
            txtVenDesc.Text = sqlReader["VendorDescription"].ToString();
            txtVenURL.Text = sqlReader["VendorURL"].ToString();
        }
        sqlReader.Close();
        btnAddVendor.Enabled = false;

    }


    protected void btnAddVendor_Click(object sender, EventArgs e)
    {
        bool run = true;
        String vendor = "";
        String venDesc = "";
        String venURL = "";


        vendor = txtVenName.Text;
        venDesc = txtVenDesc.Text;
        venURL = txtVenURL.Text;

        //String tempName = "";
        //String pullVendorName = "SELECT VendorName FROM Vendor";
        //using (SqlConnection conn = new SqlConnection(@"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;"))
        //{
        //    conn.Open();
        //    using (SqlCommand pullVendor = new SqlCommand(pullVendorName, conn))
        //    {
        //        using (SqlDataReader reader = pullVendor.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                tempName = reader["VendorName"].ToString();
        //                if (tempName.ToUpper().Equals(vendor.ToUpper()))
        //                {
        //                    lblStatus.Text = "This vendor has already been added, please add a new vendor.";
        //                    run = false;
        //                }
        //                else
        //                {
        //                    run = true;
        //                }
        //            }
        //        }
        //    }
        //}

        if (run == true)
        {
            int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
            String currentUser = "";
            using (sc)
            {
                String pullCurrentUser = "SELECT FirstName, LastName FROM Employee WHERE EmployeeID = " + currentID;
                using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc))
                {
                    if (sc.State == System.Data.ConnectionState.Closed)
                        sc.Open();

                    using (SqlDataReader reader = pullUser.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currentUser = (string)reader["FirstName"] + " " + (string)reader["LastName"];
                        }
                        reader.Close();
                    }
                }
            }

            String insertVendorQuery = "INSERT INTO Vendor VALUES (@venName,@venDesc,@venURL,@LUB,@LU)";
            using (sc)
            {
                try
                {
                    if(sc.State == System.Data.ConnectionState.Closed)                
                        sc.Open();

                    SqlCommand insertVendor = new SqlCommand(insertVendorQuery, sc);
                    insertVendor.Parameters.AddWithValue("@venName", vendor);
                    insertVendor.Parameters.AddWithValue("@venDesc", venDesc);
                    insertVendor.Parameters.AddWithValue("@venURL", venURL);
                    insertVendor.Parameters.AddWithValue("@LUB", currentUser);
                    insertVendor.Parameters.AddWithValue("@LU", DateTime.Now);
                    insertVendor.ExecuteNonQuery();
                    lblStatus.Text = "Vendor created. ";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                catch
                {

                }
               
            }

            int vendorID = 0;
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
            String pullVendorID = "SELECT MAX(VendorID) FROM Vendor";
            SqlCommand cmd = new SqlCommand(pullVendorID, sc);
            vendorID = (int) cmd.ExecuteScalar();

            //using (SqlConnection conn = new SqlConnection(@"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;"))
            //{
            //    String pullVendorID = "SELECT MAX(VendorID) FROM Vendor";
            //    SqlCommand cmd = new SqlCommand(pullVendorID, sc);
            //    vendorID = cmd.ExecuteScalar();

            //    using (SqlCommand pullVendor = new SqlCommand(pullVendorID, conn))
            //    {
            //        conn.Open();
            //        using (SqlDataReader reader = pullVendor.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                vendorID = Convert.ToInt32(reader["VendorID"]);
            //            }
            //        }
            //    }
            //}

            HttpPostedFile postedFile = uploadVendorImage.PostedFile;
            String fileName = Path.GetFileName(postedFile.FileName);
            String fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);


                String uploadVendorImageQuery = "INSERT INTO VendorImage VALUES (@VendorID, @Image,@LUB,@LU)";

                //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
                using (SqlConnection connection = new SqlConnection(sc.ConnectionString))
                {
                    if (sc.State == System.Data.ConnectionState.Closed)
                        sc.Open();
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    SqlCommand uploadVenImg = new SqlCommand(uploadVendorImageQuery, connection);
                    uploadVenImg.Parameters.AddWithValue("@Image", bytes);
                    uploadVenImg.Parameters.AddWithValue("@VendorID", vendorID);
                    uploadVenImg.Parameters.AddWithValue("@LUB", currentUser);
                    uploadVenImg.Parameters.AddWithValue("@LU", DateTime.Now);
                    uploadVenImg.ExecuteNonQuery();
                    lblStatus.Text = "Image uploaded. ";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    clear();
                }
            }
            populateListBox();
        }
    }


    public void clear()
    {
        txtVenDesc.Text = null;
        txtVenName.Text = null;
        txtVenURL.Text = null;
       
    }



    protected void btnEditVendor_Click(object sender, EventArgs e)
    {
        

        int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
        String currentUser = "";
        //SqlConnection conn = new SqlConnection(@"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;");
        using (sc)
        {
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();

            String pullCurrentUser = "SELECT FirstName, LastName FROM Employee WHERE EmployeeID = " + currentID;
            using (SqlCommand pullUser = new SqlCommand(pullCurrentUser, sc))
            {
                //conn.Open();
                using (SqlDataReader userReader = pullUser.ExecuteReader())
                {
                    while (userReader.Read())
                    {
                        currentUser = userReader["FirstName"].ToString() + " " + userReader["LastName"].ToString();
                    }
                    userReader.Close();
                }
            }
        }

        String vendorID = lstVendor.SelectedValue;

        SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        if (sc1.State == System.Data.ConnectionState.Closed)
        {
            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            sc1.Open();
        }
        String query = "UPDATE Vendor SET VendorName = @VendorName, VendorDescription = @VendorDescription, VendorURL = @VendorURL, LastUpdatedBy = @LastUpdatedBy, LastUpdated = @LastUpdated WHERE VendorID = @VendorID";
        try
        {
            SqlCommand cmd = new SqlCommand(query, sc1);
            cmd.Parameters.AddWithValue("@VendorName", txtVenName.Text);
            cmd.Parameters.AddWithValue("@VendorDescription", txtVenDesc.Text);
            cmd.Parameters.AddWithValue("@VendorURL", txtVenURL.Text);
            cmd.Parameters.AddWithValue("@VendorID", vendorID);
            cmd.Parameters.AddWithValue("@LastUpdatedBy", currentUser);
            cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

            cmd.ExecuteNonQuery();
            lblStatus.Text = "Vendor information updated. ";
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
        }

        if (uploadVendorImage != null)
        {
            HttpPostedFile postedFile = uploadVendorImage.PostedFile;
            String fileName = Path.GetFileName(postedFile.FileName);
            String fileExtension = Path.GetExtension(fileName);
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);


                String giftUploadQuery = "UPDATE VendorImage SET Image = @Image,LastUpdatedBy = @LastUpdatedBy, LastUpdated = @LastUpdated";
                String giftInsertQuery = "INSERT INTO VendorImage VALUES (" + Convert.ToInt32(lstVendor.SelectedValue) + ",@Image, @LastUpdatedBy, @LastUpdated)";


                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {

                    if (sc.State == System.Data.ConnectionState.Closed)
                        sc.Open();

                    try
                    {

                        SqlCommand giftUploadCmd = new SqlCommand(giftInsertQuery, sc);
                        giftUploadCmd.Parameters.AddWithValue("@Image", bytes);
                        giftUploadCmd.Parameters.AddWithValue("@LastUpdatedBy", currentUser);
                        giftUploadCmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                        ///connection.Open();
                        giftUploadCmd.ExecuteNonQuery();
                        lblStatus.Text = "Image updated";


                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        try
                        {


                            SqlCommand giftUploadCmd = new SqlCommand(giftUploadQuery, sc);
                            giftUploadCmd.Parameters.AddWithValue("@Image", bytes);
                            giftUploadCmd.Parameters.AddWithValue("@LastUpdatedBy", currentUser);
                            giftUploadCmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                            if (sc.State == System.Data.ConnectionState.Closed)
                                sc.Open();
                            giftUploadCmd.ExecuteNonQuery();
                            lblStatus.Text += "Image updated";
                        }
                        catch
                        {
                            lblStatus.Text = "Error";
                            lblStatus.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }

        populateListBox();
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtVenName.Text = "";
        txtVenDesc.Text = "";
        txtVenURL.Text = "";
        btnAddVendor.Enabled = true;
    }
}
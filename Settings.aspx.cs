using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web.Configuration;


public partial class Settings : System.Web.UI.Page
{
    String startEmail = "sample@gmail.com";
    String startPhone = "5551231234";
    int EmployeeID = 0;
    //SqlConnection sc = new SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");
        // the current user's employeeID
        EmployeeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["CurrentUserID"].ToString());

        try
        {

            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            sc.Open();
        }
        catch (Exception)
        {

        }

        //read in info from database - employee id, email, phone
        String emailQuery = "SELECT Email From dbo.Employee WHERE EmployeeID = @EmployeeID";
        String phoneQuery = "SELECT PhoneNumber FROM dbo.Employee WHERE EmployeeID = @EmployeeID";
        using (SqlConnection connection = new SqlConnection(sc.ConnectionString))
        {
            connection.Open();
            try
            {
                SqlCommand getEmail = new SqlCommand(emailQuery, connection);
                getEmail.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                startEmail = getEmail.ExecuteScalar().ToString();

                SqlCommand getPhone = new SqlCommand(phoneQuery, connection);
                getPhone.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                startPhone = getPhone.ExecuteScalar().ToString();

            }
            catch (Exception)
            {
            }

        }



        if (!IsPostBack)
        {
            txtEmail.Text = startEmail;
            txtPhone.Text = startPhone;
        }

        //lblOutput.Text = EmployeeID.ToString();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
        String currentUser = "";
        using (sc)
        {
            //if (sc.State == System.Data.ConnectionState.Closed)
            //{
            //    sc.Open();
            //}

            // select the project name that matches what the user puts in the search box
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

        String email = txtEmail.Text;
        String phone = txtPhone.Text;
        int count = 0;
        String message = "";

        String tempQuery = "Update [dbo].[Employee] SET Email = @Email, LastUpdatedBy = @LUB, LastUpdated = @LU WHERE EmployeeID = @EmployeeID";

        Boolean run = IsValidEmail(email);

        if (run == true)
        {
            if (!email.ToUpper().Equals(startEmail.ToUpper()))
            {
                using (SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc1.State == System.Data.ConnectionState.Closed)
                        sc1.Open();
                    try
                    {

                        SqlCommand updateQuery = new SqlCommand(tempQuery, sc1);
                        updateQuery.Parameters.AddWithValue("@Email", email);
                        updateQuery.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        updateQuery.Parameters.AddWithValue("@LUB", currentUser);
                        updateQuery.Parameters.AddWithValue("@LU", DateTime.Now);
                        updateQuery.ExecuteNonQuery();
                        message = "Email updated! ";
                        count++;
                    }
                    catch (Exception q)
                    {
                        lblOutput.Text = q.Message;
                    }

                }
            }
        }
        else
        {
            message = "Email is not valid. ";
        }
        String tempQuery2 = "Update [dbo].[Employee] SET PhoneNumber = @PhoneNumber, LastUpdatedBy = @LUB, LastUpdated = @LU WHERE EmployeeID = @EmployeeID";
        Boolean checkPhone = true;
        if (phone.Equals(""))
        {
            checkPhone = false;
        }
        if (checkPhone == true)
        {
            if (!phone.Equals(startPhone))
            {
                using (SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString))
                {
                    if (sc.State == System.Data.ConnectionState.Closed)
                        sc.Open();
                    try
                    {

                        SqlCommand updateQuery = new SqlCommand(tempQuery2, sc);
                        updateQuery.Parameters.AddWithValue("@PhoneNumber", phone);
                        updateQuery.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        updateQuery.Parameters.AddWithValue("@LUB", currentUser);
                        updateQuery.Parameters.AddWithValue("@LU", DateTime.Now);

                        updateQuery.ExecuteNonQuery();
                        message += "Phone number updated!";
                        count++;
                    }
                    catch (Exception q)
                    {
                        lblOutput.Text = q.Message;
                    }

                }
            }
        }
        else
        {
            message += "Phone number was not updated.";
        }
        HttpPostedFile postedFile = fileImageUpload.PostedFile;
        String fileName = Path.GetFileName(postedFile.FileName);
        String fileExtension = Path.GetExtension(fileName);
        int fileSize = postedFile.ContentLength;

        if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".png")
        {
            Stream stream = postedFile.InputStream;
            BinaryReader binaryReader = new BinaryReader(stream);
            byte[] bytes = binaryReader.ReadBytes((int)stream.Length);

            
            String employeeImageUploadQuery = "INSERT INTO EmployeeImage values(@EmployeeID, @Image,@LUB,@LU)";
            SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
            //sc.ConnectionString = @"Server =LocalHost ;Database=Project;Trusted_Connection=Yes; MultipleActiveResultSets=true;";
            using (sc1)
            {
                if (sc1.State == System.Data.ConnectionState.Closed)
                    sc1.Open();
                SqlCommand employeeImageUploadCmd = new SqlCommand(employeeImageUploadQuery, sc1);
                employeeImageUploadCmd.Parameters.AddWithValue("@Image", bytes);
                employeeImageUploadCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                employeeImageUploadCmd.Parameters.AddWithValue("@LUB", currentUser);
                employeeImageUploadCmd.Parameters.AddWithValue("@LU", DateTime.Now);

                try
                {

                    if (sc1.State == System.Data.ConnectionState.Closed)
                        sc1.Open();


                    employeeImageUploadCmd.ExecuteNonQuery();
                    getProfilePic();
                    count++;

                }


                catch (System.Data.SqlClient.SqlException)
                {

                    try
                    {
                        employeeImageUploadQuery = "UPDATE EmployeeImage SET Image = @Image, LastUpdatedBy = @LUB, LastUpdated = @LU WHERE EmployeeID = @EmployeeID";
                        employeeImageUploadCmd = new SqlCommand(employeeImageUploadQuery, sc1);

                        employeeImageUploadCmd.Parameters.AddWithValue("@Image", bytes);
                        employeeImageUploadCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        employeeImageUploadCmd.ExecuteNonQuery();
                        getProfilePic();
                        count++;

                    }
                    catch (Exception ex)
                    {
                        lblOutput.Text = ex.Message;
                    }

                }


            }

        }



        if (count == 0)
        {
            message = "Nothing was updated.";
        }
        //else
        //{
        //    message = "Settings Updated";
        //}

        //read in info from database - employee id, email, phone

        lblOutput.Text = message;

    }

    protected void btnPassword_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }

    public void getProfilePic()
    {
        String getEmployeeImageQuery = "SELECT Image FROM EmployeeImage WHERE EmployeeID = @EmployeeID";
        SqlConnection sc1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (sc1)
        {
            SqlCommand getEmployeeImageCmd = new SqlCommand(getEmployeeImageQuery, sc1);
            getEmployeeImageCmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);

            try
            {

                if (sc1.State == System.Data.ConnectionState.Closed)
                {
                    sc1.Open();
                }

                byte[] bytes = (byte[])getEmployeeImageCmd.ExecuteScalar();
                String strBase64 = Convert.ToBase64String(bytes);
                //profileImage.ImageUrl = "data:Image/png;base64," + strBase64;

            }

            catch (Exception ex)
            {
                //profileImage.ImageUrl = "Images/blank-face.jpg";
            }


        }



    }



    //https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address
    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            //change erro message
            return false;
        }
    }
}
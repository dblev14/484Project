using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
public partial class ChangePassword : System.Web.UI.Page
{
    //Create sql Connection
    //SqlConnection sc = new SqlConnection();
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loggedIn"] == null)
            Response.Redirect("LoginPage.aspx");

        if (Session["loggedIn"].ToString() != "True")
            Response.Redirect("LoginPage.aspx");

        



        try
        {
            //sc.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;";
            sc.Open();

        }
        catch
        {

        }

    }

    protected void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        

            int currentID = (int)System.Web.HttpContext.Current.Session["CurrentUserID"];
            String currentUser = "";
            using (sc)
            {

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

            String newPassword = txtNewPassword.Text;
            String oldPassword = txtOldPassword.Text;

            //Validate old passwordHash
            SqlConnection sc2 = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
            if (sc2.State == ConnectionState.Closed)
            {
                //sc.ConnectionString = "Server=localhost;Database=Project;Trusted_Connection=Yes;";
                sc2.Open();
            }
            SqlCommand getOldPasswordHash = new SqlCommand("SELECT PasswordHash FROM AccountPassword WHERE EmployeeID = " + (int)Session["CurrentUserID"], sc2);
            String oldPasswordHash = getOldPasswordHash.ExecuteScalar().ToString();

            bool veryify = SimpleHash.VerifyHash(oldPassword, "MD5", oldPasswordHash);


            //update password
            if (veryify == true)
            {
                String passwordHashNew = SimpleHash.ComputeHash(newPassword, "MD5", null);

                String updatePasswordQuery = "UPDATE AccountPassword SET PasswordHash = @PasswordHash, LastUpdatedBy = @LUB, LastUpdated = @LU WHERE EmployeeID = @EmployeeID";

                using (SqlConnection conn = new SqlConnection(sc2.ConnectionString))
                {
                    SqlCommand updatePassword = new SqlCommand(updatePasswordQuery, conn);
                    updatePassword.Parameters.AddWithValue("@PasswordHash", passwordHashNew);
                    updatePassword.Parameters.AddWithValue("@EmployeeID", (int)Session["CurrentUserID"]);
                    updatePassword.Parameters.AddWithValue("@LUB", currentUser);
                    updatePassword.Parameters.AddWithValue("@LU", DateTime.Now);

                    try
                    {
                    if (conn.State == System.Data.ConnectionState.Closed)
                        conn.Open();
                    updatePassword.ExecuteNonQuery();
                        lblStatus.Text = "Password changed";



                    }
                    catch (Exception ex)
                    {

                    }
                }
            clear();

            }
            else
            {
                lblStatus.Text = "Invalid Password";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        clear();

        }

    public void clear()
    {
        txtNewPassword.Text = null;
        txtOldPassword.Text = null;
    }
       

    }

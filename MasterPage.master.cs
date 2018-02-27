//Team 19
//Alex Hunt (PM), Daniel Blevins, Nathan Lalande, Ben Safferson
//We pledge on our honor that we have neither given nor recieved unauthorized aid on this assignment.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
public partial class MasterPage : System.Web.UI.MasterPage
{
    SqlConnection sc = new SqlConnection(WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
    //System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //sc.ConnectionString = @"Server =LOCALHOST ;Database=Project;Trusted_Connection=Yes;MultipleActiveResultSets=true;";
            if (sc.State == System.Data.ConnectionState.Closed)
                sc.Open();
        }
        catch (Exception)
        {
           
        }

        if (!IsPostBack)
        {
            try
            {
                if ((int)Session["AdminFlag"] != 1)
                {

                    menuNav.Items.RemoveAt(7);
                    
                }
                else if ((int)Session["AdminFlag"] == 1)
                {
                    menuNav.Items.RemoveAt(2);
                    menuNav.Items.RemoveAt(3);
                    menuNav.Items.RemoveAt(3);
                }

            }
            catch
            {

            }
        }
       





    }




    protected void btnSendReward_Click(object sender, EventArgs e)
    {
        Response.Redirect("SendReward.aspx");
    }

    protected void btnExit_Click(object sender, EventArgs e)
    {
        System.Environment.Exit(0);
    }


    protected void btnSettings_Click(object sender, EventArgs e)
    {
        Response.Redirect("ChangePassword.aspx");
    }

   
 

    protected void btnSearchEmployee_Click(object sender, EventArgs e)
    {
        String searchedValue = txtSearchEmployee.Text;
        if (searchedValue != null)
        {
            int countSpaces = searchedValue.Count(Char.IsWhiteSpace);
            for (int i =0; i< countSpaces; i++)
            {
                int space = searchedValue.IndexOf(" ");
                searchedValue = searchedValue.Remove(space, 1);
            }
            SqlCommand findName = new SqlCommand();
            findName.Connection = sc;
            findName.CommandText = "Select EmployeeID, FirstName, LastName As Name from Employee where (Replace(FirstName, ' ', '')+MiddleInitial+Replace(LastName, ' ', '')) = @searched";
            findName.Parameters.AddWithValue("@searched", searchedValue);
            if (findName.ExecuteScalar() != null)
            {
                try
                {
                    SqlCommand findMatches = new SqlCommand();
                    findMatches.Connection = sc;
                    findMatches.CommandText = "Select count(employeeID) from Employee where (Replace(FirstName, ' ', '')+MiddleInitial+Replace(LastName, ' ', '')) = @searched";
                    findMatches.Parameters.AddWithValue("@searched", searchedValue);
                    int numberOfMatches = (int)findMatches.ExecuteScalar();
                    if (numberOfMatches > 1)
                    {
                        HttpContext.Current.Session["SearchedResult"] = searchedValue;
                        HttpContext.Current.Session["WhereClause"] = "(FirstName+MiddleInitial+LastName)";
                        HttpContext.Current.Session["Type"] = 0;
                        Response.Redirect("SearchEmployeeResults.aspx");
                    }
                    else
                    {
                        SqlDataReader read = findName.ExecuteReader();
                        while (read.Read())
                        {
                            System.Web.HttpContext.Current.Session["SearchedEmployeeID"] = Convert.ToInt32(read[0].ToString());
                            System.Web.HttpContext.Current.Session["SearchedEmployeeFirstName"] = read[1].ToString();
                            System.Web.HttpContext.Current.Session["SearchedEmployeeLastName"] = read[2].ToString();
                        }

                        Response.Redirect("EmployeeForm.aspx");
                    }
                }
                catch
                {

                }
            }
            findName.CommandText = "Select EmployeeID, FirstName, LastName As Name from Employee where (Replace(FirstName, ' ', '')+Replace(LastName, ' ', '')) = @searched";
            if (findName.ExecuteScalar() != null)
            {
                try
                {
                    SqlCommand findMatches = new SqlCommand();
                    findMatches.Connection = sc;
                    findMatches.CommandText = "Select count(employeeID) from Employee where (Replace(FirstName, ' ', '')+Replace(LastName, ' ', '')) = @searched";
                    findMatches.Parameters.AddWithValue("@searched", searchedValue);
                    int numberOfMatches = (int)findMatches.ExecuteScalar();
                    if (numberOfMatches > 1)
                    {
                        HttpContext.Current.Session["SearchedResult"] = searchedValue;
                        HttpContext.Current.Session["WhereClause"] = "(FirstName+LastName)";
                        HttpContext.Current.Session["Type"] = 1;
                        Response.Redirect("SearchEmployeeResults.aspx");
                    }
                    else
                    {
                        SqlDataReader read = findName.ExecuteReader();
                        while (read.Read())
                        {
                            System.Web.HttpContext.Current.Session["SearchedEmployeeID"] = Convert.ToInt32(read[0].ToString());
                            System.Web.HttpContext.Current.Session["SearchedEmployeeFirstName"] = read[1].ToString();
                            System.Web.HttpContext.Current.Session["SearchedEmployeeLastName"] = read[2].ToString();
                        }

                        Response.Redirect("EmployeeForm.aspx");
                    }
                }
                catch
                {

                }
            }
            findName.CommandText = "Select EmployeeID, FirstName, LastName As Name from Employee where Replace(FirstName, ' ', '') = @searched";
            if (findName.ExecuteScalar() != null)
            {
                try
                {
                    SqlCommand findMatches = new SqlCommand();
                    findMatches.Connection = sc;
                    findMatches.CommandText = "Select count(employeeID) from Employee where Replace(FirstName, ' ', '') = @searched";
                    findMatches.Parameters.AddWithValue("@searched", searchedValue);
                    int numberOfMatches = (int)findMatches.ExecuteScalar();
                    if (numberOfMatches > 1)
                    {
                        HttpContext.Current.Session["SearchedResult"] = searchedValue;
                        HttpContext.Current.Session["WhereClause"] = "FirstName";
                        HttpContext.Current.Session["Type"] = 2;
                        Response.Redirect("SearchEmployeeResults.aspx");
                    }
                    else
                    {
                        SqlDataReader read = findName.ExecuteReader();
                        while (read.Read())
                        {
                            System.Web.HttpContext.Current.Session["SearchedEmployeeID"] = Convert.ToInt32(read[0].ToString());
                            System.Web.HttpContext.Current.Session["SearchedEmployeeFirstName"] = read[1].ToString();
                            System.Web.HttpContext.Current.Session["SearchedEmployeeLastName"] = read[2].ToString();
                        }

                        Response.Redirect("EmployeeForm.aspx");
                    }
                }
                catch
                {

                }
            }
            findName.CommandText = "Select EmployeeID, FirstName, LastName As Name from Employee where Replace(LastName, ' ', '') = @searched";
            if (findName.ExecuteScalar() != null)
            {
                try
                {
                    SqlCommand findMatches = new SqlCommand();
                    findMatches.Connection = sc;
                    findMatches.CommandText = "Select count(employeeID) from Employee where Replace(LastName, ' ', '') = @searched";
                    findMatches.Parameters.AddWithValue("@searched", searchedValue);
                    int numberOfMatches = (int)findMatches.ExecuteScalar();
                    if (numberOfMatches > 1)
                    {
                        HttpContext.Current.Session["SearchedResult"] = searchedValue;
                        HttpContext.Current.Session["WhereClause"] = "(LastName)";
                        HttpContext.Current.Session["Type"] = 3;
                        Response.Redirect("SearchEmployeeResults.aspx");
                    }
                    else
                    {
                        SqlDataReader read = findName.ExecuteReader();
                        while (read.Read())
                        {
                            System.Web.HttpContext.Current.Session["SearchedEmployeeID"] = Convert.ToInt32(read[0].ToString());
                            System.Web.HttpContext.Current.Session["SearchedEmployeeFirstName"] = read[1].ToString();
                            System.Web.HttpContext.Current.Session["SearchedEmployeeLastName"] = read[2].ToString();
                        }

                        Response.Redirect("EmployeeForm.aspx");
                    }
                }
                catch
                {

                }
            }
            findName.CommandText = "Select GiftName, Gift.GiftAmount, Gift.GiftDescription, Gift.Quantity, Gift.GiftID, Vendor.VendorName from Gift Inner Join Vendor On Gift.VendorID = Vendor.VendorID where Replace(GiftName, ' ', '') = @searched";
            if (findName.ExecuteScalar() != null)
            {
                try
                {
                    SqlCommand findMatches = new SqlCommand();
                    findMatches.Connection = sc;
                    findMatches.CommandText = "Select count(giftID) from Gift where Replace(GiftName, ' ', '') = @searched";
                    findMatches.Parameters.AddWithValue("@searched", searchedValue);
                    int numberOfMatches = (int)findMatches.ExecuteScalar();
                    if (numberOfMatches > 1)
                    {
                      HttpContext.Current.Session["SearchedResult"] = searchedValue;
                      HttpContext.Current.Session["WhereClause"] = "";
                      HttpContext.Current.Session["Type"] = 4;
                      Response.Redirect("SearchEmployeeResults.aspx");
                    }
                    else
                    {
                        SqlDataReader read = findName.ExecuteReader();
                        while (read.Read())
                        {
                            System.Web.HttpContext.Current.Session["GiftName"] = Convert.ToString(read[0].ToString());
                            System.Web.HttpContext.Current.Session["GiftID"] = read[4].ToString();
                            String amount = read[1].ToString();
                            amount = amount.Substring(0, amount.Length - 2);
                            System.Web.HttpContext.Current.Session["GiftAmount"] = amount;
                            System.Web.HttpContext.Current.Session["GiftQuantity"] = read[3].ToString();
                            System.Web.HttpContext.Current.Session["GiftDescription"] = read[2].ToString();
                            System.Web.HttpContext.Current.Session["VendorName"] = read[5].ToString();
                        }

                        Response.Redirect("Gift.aspx");
                    }
                }
                catch
                {

                }
            }
            findName.CommandText = "Select VendorID, VendorName from Vendor where Replace(VendorName, ' ', '') = @searched";
            if (findName.ExecuteScalar() != null)
            {
                try
                {
                    SqlCommand findMatches = new SqlCommand();
                    findMatches.Connection = sc;
                    findMatches.CommandText = "Select count(VendorID) from Vendor where Replace(VendorName, ' ', '') = @searched";
                    findMatches.Parameters.AddWithValue("@searched", searchedValue);
                    int numberOfMatches = (int)findMatches.ExecuteScalar();
                    if (numberOfMatches > 1)
                    {
                        HttpContext.Current.Session["SearchedResult"] = searchedValue;
                        HttpContext.Current.Session["WhereClause"] = "";
                        HttpContext.Current.Session["Type"] = 5;
                        Response.Redirect("SearchEmployeeResults.aspx");
                    }
                    else
                    {
                        SqlDataReader read = findName.ExecuteReader();
                        while (read.Read())
                        {
                            System.Web.HttpContext.Current.Session["VendorName"] = Convert.ToString(read[1].ToString());
                            System.Web.HttpContext.Current.Session["VendorID"] = read[0].ToString();
                        }

                        Response.Redirect("Vendor.aspx");
                    }
                }
                catch
                {

                }
            }
        }

    }

  




        //String employeeSearch = txtSearchEmployee.Text;

        //if (employeeSearch != null)
        //{
        //    string[] splitName;
        //    splitName = txtSearchEmployee.Text.Split();
        //    String firstName = splitName[0];
        //    String lastName = splitName[1];
        //    int employeeID = 0;

        //    String searchEmployeeQuery = "SELECT EmployeeID FROM Employee WHERE (FirstName LIKE @FirstName) AND (LastName Like @LastName)";
        //    String searchEmployeeCount = "SELECT Count(EmployeeID) FROM Employee WHERE (FirstName LIKE @FirstName) AND (LastName Like @LastName)";

        //    using (SqlConnection conn = new SqlConnection(sc.ConnectionString))
        //    {
        //        SqlCommand employeeSearchCmd = new SqlCommand(searchEmployeeQuery, conn);
        //        SqlCommand employeeCount = new SqlCommand(searchEmployeeCount, conn);

        //        employeeSearchCmd.Parameters.AddWithValue("@FirstName", "%" + firstName + "%");
        //        employeeSearchCmd.Parameters.AddWithValue("@LastName", "%" + lastName + "%");

        //        employeeCount.Parameters.AddWithValue("@FirstName", "%" + firstName + "%");
        //        employeeCount.Parameters.AddWithValue("@LastName", "%" + lastName + "%");


        //        try
        //        {
        //            conn.Open();
        //            int numberOfMatches = (int)employeeCount.ExecuteScalar();
        //            employeeID = (int)employeeSearchCmd.ExecuteScalar();
        //            if (numberOfMatches > 1)
        //            {
        //                System.Web.HttpContext.Current.Session["SearchedEmployeeID"] = employeeID;
        //                System.Web.HttpContext.Current.Session["SearchedEmployeeFirstName"] = firstName;
        //                System.Web.HttpContext.Current.Session["SearchedEmployeeLastName"] = lastName;
        //                Response.Redirect("SearchEmployeeResults.aspx");
        //            }
        //            else if (numberOfMatches == 0)
        //            {

        //            }
        //            else
        //            {
        //                System.Web.HttpContext.Current.Session["SearchedEmployeeID"] = employeeID;
        //                System.Web.HttpContext.Current.Session["SearchedEmployeeFirstName"] = firstName;
        //                System.Web.HttpContext.Current.Session["SearchedEmployeeLastName"] = lastName;
        //                Response.Redirect("EmployeeForm.aspx");
        //            }

        //        }

        //        catch (Exception ex)
        //        {

        //        }
        //        splitName = null;

        //    }
        //}

    }
